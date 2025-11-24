using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MiniPhotoShop.Filters;
using MiniPhotoShop.Managers;
using MiniPhotoShop.Models;
using MiniPhotoShop.Services;
using System.IO;
using MiniPhotoShop.Managers;


namespace MiniPhotoShop
{
    public partial class Form1 : Form
    {
        private readonly DocumentManager _documentManager;
        private readonly ThumbnailManager _thumbnailManager;
        private readonly IImageFileService _imageFileService;
        private readonly IDataExportService _dataExportService;
        private readonly IImageProcessingService _imageProcessor;
        private readonly IImageArithmeticService _imageArithmeticService;
        private readonly IDialogService _dialogService;

        private readonly IImageFilter _grayFilter = new GrayscaleFilter();
        private readonly IImageFilter _redFilter = new RedChannelFilter();
        private readonly IImageFilter _greenFilter = new GreenChannelFilter();
        private readonly IImageFilter _blueFilter = new BlueChannelFilter();
        private readonly IImageFilter _negationFilter = new NegationFilter();
        private readonly IImageFilter _notFilter = new NotFilter();

        private readonly Dictionary<TabPage, bool> _isBitwiseDocument = new Dictionary<TabPage, bool>();

        private ContextMenuStrip _thumbnailContextMenu;
        private PictureBox _dragSourceThumb = null;
        private bool _isDragging = false;
        private string _currentArithmeticOperation = "None";

        public Form1
        (
            DocumentManager documentManager,
            ThumbnailManager thumbnailManager,
            IImageFileService imageFileService,
            IDataExportService dataExportService,
            IImageProcessingService imageProcessor,
            IImageArithmeticService imageArithmeticService,
            IDialogService dialogService
        )
        {
            InitializeComponent();

            _documentManager = documentManager;
            _thumbnailManager = thumbnailManager;
            _imageFileService = imageFileService;
            _dataExportService = dataExportService;
            _imageProcessor = imageProcessor;
            _imageArithmeticService = imageArithmeticService;
            _dialogService = dialogService;

            _documentManager.Initialize(tabControlCanvas);
            _thumbnailManager.Initialize(flowLayoutPanelThumbnails);

            _documentManager.ActiveDocumentChanged += DisplayHistogram;
            _documentManager.CanvasDragEnter += Canvas_DragEnter;
            _documentManager.CanvasDragDrop += Canvas_DragDrop;
            _thumbnailManager.ThumbnailClicked += OnThumbnailClicked;

            tambahToolStripMenuItem.Click += tambahToolStripMenuItem_Click;
            kurangToolStripMenuItem.Click += kurangToolStripMenuItem_Click;
            kaliToolStripMenuItem.Click += kaliToolStripMenuItem_Click;
            bagiToolStripMenuItem.Click += bagiToolStripMenuItem_Click;
        }

        private TabPage GetActiveTab()
        {
            return tabControlCanvas.SelectedTab;
        }

        private void OnThumbnailClicked(Bitmap image, string name)
        {
            if (IsSelectionModeActive()) return;
            _documentManager.OpenDocument(image, name);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            Bitmap loadedImage = _imageFileService.OpenImage(out string fileName);
            if (loadedImage != null)
            {
                _documentManager.OpenDocument(loadedImage, fileName);
                _thumbnailManager.AddThumbnail(loadedImage, fileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ImageDocument doc = _documentManager.GetActiveDocument();
            if (doc == null) return;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Simpan Gambar";
                sfd.FileName = doc.Name;
                sfd.Filter = "PNG Image|*.png|JPEG Image|*.jpg;*.jpeg|Bitmap Image|*.bmp";
                sfd.DefaultExt = "png";
                sfd.AddExtension = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Png;
                        string ext = System.IO.Path.GetExtension(sfd.FileName).ToLower();
                        switch (ext)
                        {
                            case ".jpg":
                            case ".jpeg":
                                format = System.Drawing.Imaging.ImageFormat.Jpeg; break;
                            case ".bmp":
                                format = System.Drawing.Imaging.ImageFormat.Bmp; break;
                        }

                        doc.CurrentBitmap.Save(sfd.FileName, format);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Terjadi kesalahan saat menyimpan gambar: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ImageDocument doc = _documentManager.GetActiveDocument();
            if (doc?.CurrentBitmap != null)
            {
                Clipboard.SetImage(doc.CurrentBitmap);
                _documentManager.CloseActiveDocument();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ImageDocument doc = _documentManager.GetActiveDocument();
            if (doc?.CurrentBitmap != null)
            {
                Clipboard.SetImage(doc.CurrentBitmap);
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            if (Clipboard.ContainsImage())
            {
                Bitmap pastedImage = new Bitmap(Clipboard.GetImage());
                _documentManager.OpenDocument(pastedImage, "Pasted Image");
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            _documentManager.CloseActiveDocument();
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ApplyFilter(_grayFilter);
        }

        private void negationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ApplyFilter(_negationFilter);
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ApplyFilter(_redFilter);
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ApplyFilter(_greenFilter);
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ApplyFilter(_blueFilter);
        }

        private void bwToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ImageDocument doc = _documentManager.GetActiveDocument();
            if (doc == null) return;

            DialogResult result = _dialogService.ShowAdjustmentDialog(
                "Atur Threshold Black/White", 0, 255, 128, 16, "B/W:",
                (value) => { ApplyThresholdFilter(value, isPreview: true); },
                out int newThresholdValue
            );

            if (result == DialogResult.OK)
                ApplyThresholdFilter(newThresholdValue, isPreview: false);
            else
                doc.Restore();

            _documentManager.UpdateActiveCanvas();
            DisplayHistogram();
        }

        private void brightnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ImageDocument doc = _documentManager.GetActiveDocument();
            if (doc == null) return;

            DialogResult result = _dialogService.ShowAdjustmentDialog(
                "Atur Brightness", -255, 255, 0, 32, "Brightness:",
                (value) => { ApplyBrightnessFilter(value, isPreview: true); },
                out int newBrightnessValue
            );

            if (result == DialogResult.OK)
                ApplyBrightnessFilter(newBrightnessValue, isPreview: false);
            else
                doc.Restore();

            _documentManager.UpdateActiveCanvas();
            DisplayHistogram();
        }

        private void imageSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = _documentManager.GetActiveDocument();
            if (doc == null) return;
            _documentManager.ToggleSelectionMode(true);
            MessageBox.Show("Mode Seleksi Gambar diaktifkan.\nKlik pada sebuah warna di gambar untuk menyeleksinya.",
                "Mode Seleksi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = _documentManager.GetActiveDocument();
            if (doc == null) return;

            doc.Restore();
            _documentManager.UpdateActiveCanvas();
            DisplayHistogram();
            MessageBox.Show("Gambar telah dikembalikan ke kondisi semula.", "Restore", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void andToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentArithmeticOperation = "AND";
            MessageBox.Show("Mode AND diaktifkan. \nDrag gambar dari thumbnail ke kanvas.", "Mode Bitwise",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void orToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentArithmeticOperation = "OR";
            MessageBox.Show("Mode OR diaktifkan. \nDrag gambar dari thumbnail ke kanvas.", "Mode Bitwise",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void xorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentArithmeticOperation = "XOR";
            MessageBox.Show("Mode XOR diaktifkan. \nDrag gambar dari thumbnail ke kanvas.", "Mode Bitwise",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void notToolStripMenutItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ApplyFilter(_notFilter);
        }

        private void savePixelDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ImageDocument doc = _documentManager.GetActiveDocument();

            if (doc == null || doc.PixelArray == null)
            {
                MessageBox.Show("Tidak ada data pixel aktif untuk disimpan.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            DialogResult binaryResult = MessageBox.Show
            (
                "Simpan sebagai data biner (Yes) atau teks (No)?",
                "Format Simpan Data Pixel",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question
            );

            if (binaryResult == DialogResult.Cancel) return;

            bool outputAsBinary = (binaryResult == DialogResult.Yes);

            _dataExportService.SavePixelData(doc.Name, doc.PixelArray, doc.IsGrayscale, outputAsBinary);
        }

        private void tableDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ImageDocument doc = _documentManager.GetActiveDocument();
            if (doc == null) return;
            _dataExportService.SaveHistogramData(doc.Name, doc.Histogram);
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelHistogram.Visible = !panelHistogram.Visible;
            if (panelHistogram.Visible)
            {
                DisplayHistogram();
            }
        }

        private void tambahToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentArithmeticOperation = "Add";
            MessageBox.Show("Mode Tambah diaktifkan. \nDrag gambar dari thumbnail ke kanvas.", "Mode Aritmatika",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void kurangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentArithmeticOperation = "Subtract";
            MessageBox.Show("Mode Kurang diaktifkan. \nDrag gambar dari thumbnail ke kanvas.", "Mode Aritmatika",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void kaliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentArithmeticOperation = "Multiply";
            MessageBox.Show("Mode Kali diaktifkan. \nDrag gambar dari thumbnail ke kanvas.", "Mode Aritmatika",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bagiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentArithmeticOperation = "Divide";
            MessageBox.Show("Mode Bagi diaktifkan. \nDrag gambar dari thumbnail ke kanvas.", "Mode Aritmatika",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void kaliKonstantaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformConstantArithmetic("Multiply");
        }

        private void bagiKonstantaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformConstantArithmetic("Divide");
        }

        private void PerformConstantArithmetic(string operation)
        {
            if (IsSelectionModeActive()) return;

            ImageDocument activeDoc = _documentManager.GetActiveDocument();
            if (activeDoc == null)
            {
                MessageBox.Show("Tidak ada gambar aktif untuk diproses.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            double constant = GetConstantFromUser($"Masukkan Konstanta {operation}");

            if (double.IsNaN(constant))
            {
                return;
            }

            if (operation == "Divide" && Math.Abs(constant) < 0.0001)
            {
                MessageBox.Show("Tidak dapat membagi dengan konstanta nol.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                Bitmap sourceBmp = activeDoc.CurrentBitmap;
                Bitmap resultBmp = null;

                if (operation == "Multiply")
                {
                    resultBmp = _imageArithmeticService.MultiplyByConstant(sourceBmp, constant);
                }
                else if (operation == "Divide")
                {
                    resultBmp = _imageArithmeticService.DivideByConstant(sourceBmp, constant);
                }

                if (resultBmp != null)
                {
                    string newName = $"{activeDoc.Name}_{operation}_{constant.ToString(CultureInfo.InvariantCulture)}";
                    _documentManager.OpenDocument(resultBmp, newName);
                    resultBmp.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat melakukan operasi: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private double GetConstantFromUser(string title)
        {
            Form prompt = new Form()
            {
                Width = 280,
                Height = 160,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label textLabel = new Label() { Left = 20, Top = 20, Text = "Nilai Konstanta:", Width = 240 };

            NumericUpDown numericInput = new NumericUpDown()
            {
                Left = 20,
                Top = 50,
                Width = 220,
                DecimalPlaces = 2,
                Minimum = -10000,
                Maximum = 10000,
                Value = 1.0M
            };

            Button confirmationButton = new Button()
                { Text = "Ok", Left = 60, Width = 70, Top = 90, DialogResult = DialogResult.OK };
            Button cancelButton = new Button()
                { Text = "Batal", Left = 150, Width = 70, Top = 90, DialogResult = DialogResult.Cancel };

            confirmationButton.Click += (sender, e) => { prompt.Close(); };
            cancelButton.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(numericInput);
            prompt.Controls.Add(confirmationButton);
            prompt.Controls.Add(cancelButton);
            prompt.AcceptButton = confirmationButton;
            prompt.CancelButton = cancelButton;

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                return (double)numericInput.Value;
            }
            else
            {
                return double.NaN;
            }
        }

        private void Canvas_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat) && _currentArithmeticOperation != "None")
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private string GenerateShortTabName(string originalName, int maxLength = 20)
        {
            if (originalName.Length <= maxLength)
            {
                return originalName;
            }

            string nameWithoutExtension = Path.GetFileNameWithoutExtension(originalName);
            if (nameWithoutExtension.Length > maxLength - 3)
            {
                return nameWithoutExtension.Substring(0, maxLength - 3) + "...";
            }

            return originalName;
        }

        private void Canvas_DragDrop(object sender, DragEventArgs e)
        {
            if (IsSelectionModeActive() || _currentArithmeticOperation == "None") return;
            try
            {
                ImageDocument targetDoc = _documentManager.GetActiveDocument();
                if (targetDoc == null) return;

                string sourceName = (string)e.Data.GetData(DataFormats.StringFormat);

                using (Bitmap sourceBmp = _thumbnailManager.FindThumbnailImageByName(sourceName))
                {
                    if (sourceBmp == null) return;

                    Bitmap resultBmp = null;
                    string opName = "";

                    switch (_currentArithmeticOperation)
                    {
                        case "Add":
                            resultBmp = _imageArithmeticService.AddImages(sourceBmp, targetDoc.CurrentBitmap);
                            opName = "Addition";
                            break;
                        case "Subtract":
                            resultBmp = _imageArithmeticService.SubtractImages(sourceBmp, targetDoc.CurrentBitmap);
                            opName = "Difference";
                            break;
                        case "Multiply":
                            resultBmp = _imageArithmeticService.MultiplyImages(sourceBmp, targetDoc.CurrentBitmap);
                            opName = "Multiplication";
                            break;
                        case "Divide":
                            resultBmp = _imageArithmeticService.DivideImages(sourceBmp, targetDoc.CurrentBitmap);
                            opName = "Division";
                            break;
                        case "AND":
                            resultBmp = _imageArithmeticService.AndImages(sourceBmp, targetDoc.CurrentBitmap);
                            opName = "AND";
                            break;
                        case "OR":
                            resultBmp = _imageArithmeticService.OrImages(sourceBmp, targetDoc.CurrentBitmap);
                            opName = "OR";
                            break;
                        case "XOR":
                            resultBmp = _imageArithmeticService.XorImages(sourceBmp, targetDoc.CurrentBitmap);
                            opName = "XOR";
                            break;
                    }

                    if (resultBmp != null)
                    {
                        string newName =
                            $"{Path.GetFileNameWithoutExtension(targetDoc.Name)}_{opName}_{Path.GetFileNameWithoutExtension(sourceName)}";
                        string displayTabName = GenerateShortTabName(newName);
                        _documentManager.OpenDocument(resultBmp, newName);
                        resultBmp.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat drag-drop: {ex.Message}", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                _currentArithmeticOperation = "None";
            }
        }

        private void ApplyFilter(IImageFilter filter)
        {
            ImageDocument doc = _documentManager.GetActiveDocument();
            if (doc == null) return;
            try
            {
                bool isBitwise = (filter is NotFilter);
                if (_isBitwiseDocument.ContainsKey(GetActiveTab()))
                    _isBitwiseDocument[GetActiveTab()] = isBitwise;
                else
                    _isBitwiseDocument.Add(GetActiveTab(), isBitwise);

                doc.ApplyFilter(filter);
                _documentManager.UpdateActiveCanvas();
                DisplayHistogram();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat menerapkan filter: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyBrightnessFilter(int brightness, bool isPreview = false)
        {
            ImageDocument doc = _documentManager.GetActiveDocument();
            if (doc == null) return;
            try
            {
                if (!isPreview && _isBitwiseDocument.ContainsKey(GetActiveTab()))
                    _isBitwiseDocument[GetActiveTab()] = false;

                if (brightness < -255) brightness = -255;
                if (brightness > 255) brightness = 255;

                IImageFilter brightnessFilter = new BrightnessFilter(brightness);
                doc.ApplyFilter(brightnessFilter);
                _documentManager.UpdateActiveCanvas();
            }
            catch (Exception)
            {
                if (!isPreview) MessageBox.Show("Error saat menerapkan brightness.");
            }
        }

        private void ApplyThresholdFilter(int threshold, bool isPreview = false)
        {
            ImageDocument doc = _documentManager.GetActiveDocument();
            if (doc == null) return;
            try
            {
                if (!isPreview && _isBitwiseDocument.ContainsKey(GetActiveTab()))
                    _isBitwiseDocument[GetActiveTab()] = false;

                if (threshold < 0) threshold = 0;
                if (threshold > 255) threshold = 255;

                IImageFilter thresholdFilter = new ThresholdFilter(threshold);
                doc.ApplyFilter(thresholdFilter);
                _documentManager.UpdateActiveCanvas();
            }
            catch (Exception)
            {
                if (!isPreview) MessageBox.Show("Error saat menerapkan threshold.");
            }
        }

        private bool IsSelectionModeActive()
        {
            if (_documentManager.IsSelectionModeActive())
            {
                MessageBox.Show(
                    "Fitur lain tidak dapat digunakan saat dalam mode Seleksi Gambar.\nKlik 'Restore Original' untuk keluar.",
                    "Mode Seleksi Aktif", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }

            return false;
        }

        private void DisplayHistogram()
        {
            if (!panelHistogram.Visible)
            {
                ClearHistogram();
                return;
            }

            ImageDocument doc = _documentManager.GetActiveDocument();
            if (doc == null || doc.CurrentBitmap == null)
            {
                ClearHistogram();
                return;
            }

            try
            {
                HistogramData histo = doc.Histogram;

                if (doc.IsGrayscale || doc.IsBlackAndWhite)
                {
                    ClearHistogramRGB();

                    int maxGray = histo.GrayCounts.Max();
                    if (maxGray == 0) maxGray = 1;
                    pictureBoxGrayHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxGrayHistogram.Width,
                        pictureBoxGrayHistogram.Height, histo.GrayCounts, maxGray, Color.Gray);
                }
                else
                {
                    int maxRed = histo.RedCounts.Max();
                    if (maxRed == 0) maxRed = 1;
                    pictureBoxRedHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxRedHistogram.Width,
                        pictureBoxRedHistogram.Height, histo.RedCounts, maxRed, Color.Red);

                    int maxGreen = histo.GreenCounts.Max();
                    if (maxGreen == 0) maxGreen = 1;
                    pictureBoxGreenHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxGreenHistogram.Width,
                        pictureBoxGreenHistogram.Height, histo.GreenCounts, maxGreen, Color.Green);

                    int maxBlue = histo.BlueCounts.Max();
                    if (maxBlue == 0) maxBlue = 1;
                    pictureBoxBlueHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxBlueHistogram.Width,
                        pictureBoxBlueHistogram.Height, histo.BlueCounts, maxBlue, Color.Blue);

                    int maxGray = histo.GrayCounts.Max();
                    if (maxGray == 0) maxGray = 1;
                    pictureBoxGrayHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxGrayHistogram.Width,
                        pictureBoxGrayHistogram.Height, histo.GrayCounts, maxGray, Color.Gray);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat membuat histogram: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearHistogramRGB()
        {
            pictureBoxRedHistogram.Image?.Dispose();
            pictureBoxGreenHistogram.Image?.Dispose();
            pictureBoxBlueHistogram.Image?.Dispose();
            pictureBoxRedHistogram.Image = null;
            pictureBoxGreenHistogram.Image = null;
            pictureBoxBlueHistogram.Image = null;
        }

        private void ClearHistogram()
        {
            ClearHistogramRGB();
            pictureBoxGrayHistogram.Image?.Dispose();
            pictureBoxGrayHistogram.Image = null;
        }

        private void tabControlCanvas_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                var tabPage = this.tabControlCanvas.TabPages[e.Index];
                var tabRect = this.tabControlCanvas.GetTabRect(e.Index);
                tabRect.Inflate(-2, -2);

                e.DrawBackground();
                using (var b = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(tabPage.Text, e.Font, b, e.Bounds.Left + 5, e.Bounds.Top + 4);
                }

                using (var pen = new Pen(Color.Red, 2))
                {
                    Rectangle closeRect = new Rectangle(e.Bounds.Right - 18, e.Bounds.Top + 6, 12, 12);
                    e.Graphics.DrawLine(pen, closeRect.Left, closeRect.Top, closeRect.Right, closeRect.Bottom);
                    e.Graphics.DrawLine(pen, closeRect.Left, closeRect.Bottom, closeRect.Right, closeRect.Top);
                }

                e.DrawFocusRectangle();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error drawing tab: " + ex.Message);
            }
        }

        private void tabControlCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < this.tabControlCanvas.TabPages.Count; i++)
            {
                Rectangle r = this.tabControlCanvas.GetTabRect(i);
                Rectangle closeButton = new Rectangle(r.Right - 18, r.Top + 6, 12, 12);
                if (closeButton.Contains(e.Location))
                {
                    this.tabControlCanvas.SelectedIndex = i;

                    if (_isBitwiseDocument.ContainsKey(this.tabControlCanvas.TabPages[i]))
                    {
                        _isBitwiseDocument.Remove(this.tabControlCanvas.TabPages[i]);
                    }

                    _documentManager.CloseActiveDocument();
                    break;
                }
            }
        }

        private void translasiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;

            ImageDocument doc = _documentManager.GetActiveDocument();
            if (doc == null) return;

            int maxY = doc.CurrentBitmap.Width;
            int maxX = doc.CurrentBitmap.Height;

            DialogResult resultX = _dialogService.ShowAdjustmentDialog(
                "Translasi Horizontal (X)",
                -maxX, maxX, 0, 50,
                "Geser X:",
                (val) => { },
                out int xOffset
            );

            if (resultX != DialogResult.OK) return;

            DialogResult resultY = _dialogService.ShowAdjustmentDialog(
                "Translasi Vertikal (Y)",
                -maxY, maxY, 0, 50,
                "Geser Y:",
                (val) => { },
                out int yOffset
            );

            if (resultY != DialogResult.OK) return;

            try
            {
                Bitmap translateBmp = _imageProcessor.TranslateImage(doc.CurrentBitmap, xOffset, yOffset);

                string newName = $"{doc.Name}_Trans({xOffset},{yOffset})";

                _documentManager.OpenDocument(translateBmp, newName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat melakukan translasi: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}