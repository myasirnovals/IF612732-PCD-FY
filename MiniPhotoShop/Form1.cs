using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MiniPhotoShop.Filters;
using MiniPhotoShop.Managers;
using MiniPhotoShop.Models;
using MiniPhotoShop.Services;

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

        public Form1(
            DocumentManager documentManager,
            ThumbnailManager thumbnailManager,
            IImageFileService imageFileService,
            IDataExportService dataExportService,
            IImageProcessingService imageProcessor,
            IImageArithmeticService imageArithmeticService,
            IDialogService dialogService)
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
        
        private void Canvas_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Canvas_DragDrop(object sender, DragEventArgs e)
        {
            if (IsSelectionModeActive()) return;
            try
            {
                ImageDocument targetDoc = _documentManager.GetActiveDocument();
                if (targetDoc == null) return;

                string sourceName = (string)e.Data.GetData(DataFormats.StringFormat);
                
                using (Bitmap sourceBmp = _thumbnailManager.FindThumbnailImageByName(sourceName))
                {
                    if (sourceBmp == null) return;

                    bool ctrlPressed = (e.KeyState & 8) == 8;
                    Bitmap resultBmp = null;
                    string opName = "";

                    if (ctrlPressed)
                    {
                        resultBmp = _imageArithmeticService.SubtractImages(sourceBmp, targetDoc.CurrentBitmap);
                        opName = "Subtract";
                    }
                    else
                    {
                        resultBmp = _imageArithmeticService.AddImages(sourceBmp, targetDoc.CurrentBitmap);
                        opName = "Add";
                    }

                    if (resultBmp != null)
                    {
                        string newName =
                            $"{Path.GetFileNameWithoutExtension(targetDoc.Name)}_{opName}_{Path.GetFileNameWithoutExtension(sourceName)}";
                        _documentManager.OpenDocument(resultBmp, newName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat drag-drop: {ex.Message}", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ApplyFilter(IImageFilter filter)
        {
            ImageDocument doc = _documentManager.GetActiveDocument();
            if (doc == null) return;
            try
            {
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
             
        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}