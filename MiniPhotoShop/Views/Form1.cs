    using MiniPhotoShop.Controllers;
using MiniPhotoShop.Filters.Adjustments;
using MiniPhotoShop.Filters.Base;
using MiniPhotoShop.Filters.ColorsFilters;
using MiniPhotoShop.Managers;
using MiniPhotoShop.Models;
using MiniPhotoShop.Services;
using MiniPhotoShop.Services.Interfaces;

namespace MiniPhotoShop.Views
{
    public partial class Form1 : Form
    {
        private readonly MainController _controller;
        private readonly DocumentController _documentController;
        private readonly ThumbnailController _thumbnailController;

        private readonly DocumentManager _documentManager;
        private readonly ThumbnailManager _thumbnailManager;
        private readonly IImageProcessingService _imageProcessor;
        private readonly IImageArithmeticService _arithmeticService;
        private readonly IDialogService _dialogService;

        private readonly IImageFilter _grayFilter = new GrayscaleFilter();
        private readonly IImageFilter _redFilter = new RedChannelFilter();
        private readonly IImageFilter _greenFilter = new GreenChannelFilter();
        private readonly IImageFilter _blueFilter = new BlueChannelFilter();
        private readonly IImageFilter _negationFilter = new NegationFilter();
        private readonly IImageFilter _notFilter = new NotFilter();
        private Rectangle _selectionRect;
        private Point _startPos;
        private bool _isSelectingArea = false;
        private Color _targetColor;
        private Bitmap? _isolatedBitmap;
        private bool _isDraggingIsolated = false;

        public Form1(
            DocumentManager documentManager,
            ThumbnailManager thumbnailManager,
            IImageFileService imageFileService,
            IDataExportService dataExportService,
            IImageProcessingService imageProcessor,
            IImageArithmeticService arithmeticService,
            IDialogService dialogService
        )
        {
            InitializeComponent();

            _documentManager = documentManager;
            _thumbnailManager = thumbnailManager;
            _imageProcessor = imageProcessor;
            _arithmeticService = arithmeticService;
            _dialogService = dialogService;

            _documentController = new DocumentController(_imageProcessor);
            _thumbnailController = new ThumbnailController();

            _documentManager.SetController(_documentController);

            _controller = new MainController(
                documentManager,
                _documentController,
                thumbnailManager,
                _thumbnailController,
                imageFileService,
                dataExportService,
                imageProcessor,
                arithmeticService,
                dialogService

            );

            _documentController.Initialize(tabControlCanvas);
            _thumbnailController.Initialize(flowLayoutPanelThumbnails);

            _documentController.ActiveDocumentChanged += DisplayHistogram;
            _documentController.CanvasDragEnter += Canvas_DragEnter;
            _documentController.CanvasDragDrop += Canvas_DragDrop;
            _thumbnailController.ThumbnailClicked += OnThumbnailClicked;
        }

        private void OnThumbnailClicked(Bitmap image, string name)
        {
            if (_documentController.GetActiveDocument()?.IsInSelectionMode == true) return;
            _documentController.OpenDocument(image, name);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument()?.IsInSelectionMode == true) return;
            _controller.Files.OpenImage();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument()?.IsInSelectionMode == true) return;
            _controller.Files.SaveImage();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument()?.IsInSelectionMode == true) return;
            var doc = _documentController.GetActiveDocument();
            if (doc?.CurrentBitmap != null)
            {
                Clipboard.SetImage(doc.CurrentBitmap);
                _documentController.CloseActiveDocument();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument()?.IsInSelectionMode == true) return;
            var doc = _documentController.GetActiveDocument();
            if (doc?.CurrentBitmap != null) Clipboard.SetImage(doc.CurrentBitmap);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument()?.IsInSelectionMode == true) return;
            if (Clipboard.ContainsImage())
            {
                Bitmap bmp = new Bitmap(Clipboard.GetImage());
                _documentController.OpenDocument(bmp, "Pasted Image");
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument()?.IsInSelectionMode == true) return;
            _documentController.CloseActiveDocument();
        }

        private void ApplyFilterUI(IImageFilter filter)
        {
            if (_documentController.GetActiveDocument()?.IsInSelectionMode == true) return;
            _controller.Filters.ApplyFilter(filter, tabControlCanvas.SelectedTab);
            DisplayHistogram();
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e) => ApplyFilterUI(_grayFilter);
        private void negationToolStripMenuItem_Click(object sender, EventArgs e) => ApplyFilterUI(_negationFilter);
        private void redToolStripMenuItem_Click(object sender, EventArgs e) => ApplyFilterUI(_redFilter);
        private void greenToolStripMenuItem_Click(object sender, EventArgs e) => ApplyFilterUI(_greenFilter);
        private void blueToolStripMenuItem_Click(object sender, EventArgs e) => ApplyFilterUI(_blueFilter);
        private void notToolStripMenutItem_Click(object sender, EventArgs e) => ApplyFilterUI(_notFilter);

        private void bwToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument()?.IsInSelectionMode == true) return;

            TabPage currentTab = tabControlCanvas.SelectedTab;

            _dialogService.ShowAdjustmentDialog("Threshold", 0, 255, 128, 10, "Value",
                (val) => { _controller.Filters.ApplyThreshold(val, currentTab); },
                out int finalVal);
            DisplayHistogram();
        }

        private void brightnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument()?.IsInSelectionMode == true) return;

            TabPage currentTab = tabControlCanvas.SelectedTab;

            _dialogService.ShowAdjustmentDialog("Brightness", -255, 255, 0, 10, "Value",
                (val) => { _controller.Filters.ApplyBrightness(val, currentTab); },
                out int finalVal);
            DisplayHistogram();
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _documentController.RestoreActiveDocument();
            DisplayHistogram();
        }

        private void imageSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var doc = _documentController.GetActiveDocument();
            if (doc == null) return;

            _documentController.ToggleSelectionMode(true);
            PictureBox pb = tabControlCanvas.SelectedTab?.Controls.OfType<PictureBox>().FirstOrDefault();

            if (pb != null)
            {
                // Reset event handlers
                pb.MouseDown -= Pb_ColorSelect_MouseDown;
                pb.MouseMove -= Pb_ColorSelect_MouseMove;

                // Pasang event handlers
                pb.MouseDown += Pb_ColorSelect_MouseDown;
                pb.MouseMove += Pb_ColorSelect_MouseMove;

                pb.Cursor = Cursors.Hand;
                MessageBox.Show("Mode Isolasi Warna Aktif: Klik pada warna (misal: merah) untuk mengambil objek.");
            }
        }

        private void translasiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument()?.IsInSelectionMode == true) return;
            _dialogService.ShowAdjustmentDialog("Translasi X", -1000, 1000, 0, 50, "X:", (v) => { }, out int x);
            _dialogService.ShowAdjustmentDialog("Translasi Y", -1000, 1000, 0, 50, "Y:", (v) => { }, out int y);
            _controller.Transforms.ApplyTransformation("Translate", x, y);
        }

        private void distorsiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument()?.IsInSelectionMode == true) return;
            _dialogService.ShowAdjustmentDialog("Amp", 0, 100, 20, 5, "Amp:", (v) => { }, out int amp);
            _dialogService.ShowAdjustmentDialog("Freq", 0, 50, 5, 1, "Freq:", (v) => { }, out int freq);
            _controller.Transforms.ApplyTransformation("Distort", amp, freq);
        }

        private void rotasiCitraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument()?.IsInSelectionMode == true) return;
            _dialogService.ShowAdjustmentDialog("Rotasi", -360, 360, 0, 45, "Angle:", (v) => { }, out int angle);
            _controller.Transforms.ApplyTransformation("Rotate", angle, 0);
        }

        private void dilatasiCitraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double factor = GetConstantFromUser("Scale Factor");
            if (!double.IsNaN(factor)) _controller.Transforms.ApplyTransformation("Scale", factor, 0);
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e) =>
            _controller.Transforms.ApplyTransformation("Scale", 2.0, 0);

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e) =>
            _controller.Transforms.ApplyTransformation("Scale", 0.5, 0);

        private void identityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument()?.IsInSelectionMode == true) return;
            _controller.Filters.ApplyConvolutionFilter("Identity");
            DisplayHistogram();
        }

        private void blurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument()?.IsInSelectionMode == true) return;
            _controller.Filters.ApplyConvolutionFilter("Blur");
            DisplayHistogram();
        }

        private void sharpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument()?.IsInSelectionMode == true) return;
            _controller.Filters.ApplyConvolutionFilter("Sharpen");
            DisplayHistogram();
        }

        private void robertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument()?.IsInSelectionMode == true) return;
            _controller.Filters.ApplyConvolutionFilter("Roberts");
            DisplayHistogram();
        }

        private void sobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument()?.IsInSelectionMode == true) return;
            _controller.Filters.ApplyConvolutionFilter("Sobel");
            DisplayHistogram();
        }

        private void cannyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument()?.IsInSelectionMode == true) return;
            _controller.Filters.ApplyConvolutionFilter("Canny");
            DisplayHistogram();
        }

        private void embossToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument()?.IsInSelectionMode == true) return;
            _controller.Filters.ApplyConvolutionFilter("Emboss");
            DisplayHistogram();
        }

        private void SetArithmeticMode(string mode, string messageName)
        {
            // Mematikan mode seleksi agar tidak memblokir drop aritmatika
            _documentController.ToggleSelectionMode(false);

            _controller.Arithmetic.CurrentOperation = mode;
            MessageBox.Show($"Mode {messageName} Aktif. Silakan drag gambar dari sidebar ke tengah kanvas.");
        }

        private void tambahToolStripMenuItem_Click(object sender, EventArgs e) => SetArithmeticMode("Add", "Tambah");

        private void kurangToolStripMenuItem_Click(object sender, EventArgs e) =>
            SetArithmeticMode("Subtract", "Kurang");

        private void kaliToolStripMenuItem_Click(object sender, EventArgs e) => SetArithmeticMode("Multiply", "Kali");
        private void bagiToolStripMenuItem_Click(object sender, EventArgs e) => SetArithmeticMode("Divide", "Bagi");
        private void andToolStripMenuItem_Click(object sender, EventArgs e) => SetArithmeticMode("AND", "AND");
        private void orToolStripMenuItem_Click(object sender, EventArgs e) => SetArithmeticMode("OR", "OR");
        private void xorToolStripMenuItem_Click(object sender, EventArgs e) => SetArithmeticMode("XOR", "XOR");

        private void kaliKonstantaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double val = GetConstantFromUser("Konstanta Perkalian");
            if (!double.IsNaN(val)) _controller.Arithmetic.ApplyConstant("Multiply", val);
        }

        private void bagiKonstantaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double val = GetConstantFromUser("Konstanta Pembagian");
            if (!double.IsNaN(val)) _controller.Arithmetic.ApplyConstant("Divide", val);
        }

        private void tableDataToolStripMenuItem_Click(object sender, EventArgs e) =>
            _controller.Files.ExportHistogramData();

        private void savePixelDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Simpan sebagai Binary?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                _controller.Files.ExportPixelData(true);
            else
                _controller.Files.ExportPixelData(false);
        }

        private void Canvas_DragEnter(object sender, DragEventArgs e)
        {
            // Cek 1: Apakah yang sedang ditarik adalah Thumbnail (untuk Aritmatika)?
            bool isThumbnail = e.Data.GetDataPresent(DataFormats.StringFormat);

            // Cek 2: Apakah yang sedang ditarik adalah Hasil Isolasi (untuk Simpan/Pindah)?
            bool isIsolatedImage = e.Data.GetDataPresent(DataFormats.Bitmap);

            // Izinkan drag jika salah satu terpenuhi
            if (isThumbnail || isIsolatedImage)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void Canvas_DragDrop(object sender, DragEventArgs e)
        {
            // --- KONDISI 1: ISOLASI WARNA (BITMAP) ---
            // Tetap mempertahankan opsi dialog sesuai image_3f1f64.jpg
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                Bitmap droppedBmp = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
                DialogResult result = MessageBox.Show(
                    "Objek warna terdeteksi! \n\n'Yes' untuk Simpan ke File\n'No' untuk Buka di Tab Baru",
                    "Opsi Drop", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.Yes)
                {
                    using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "PNG Image|*.png" })
                    {
                        if (sfd.ShowDialog() == DialogResult.OK) droppedBmp.Save(sfd.FileName);
                    }
                }
                else if (result == DialogResult.No)
                {
                    _documentController.OpenDocument(droppedBmp, "Objek Terisolasi");
                }
                return;
            }

            // --- KONDISI 2: ARITMATIKA (STRING DARI SIDEBAR) ---
            // Digunakan untuk men-drag thumbnail ke tengah gambar yang sudah ada
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                if (_controller.Arithmetic.CurrentOperation != "None")
                {
                    try
                    {
                        string sourceName = (string)e.Data.GetData(DataFormats.StringFormat);
                        // Proses penggabungan gambar di tengah kanvas
                        _controller.Arithmetic.HandleDragDrop(sourceName, _documentController.GetActiveDocument());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error Aritmatika: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Aktifkan mode aritmatika (Tambah/Kurang) terlebih dahulu!");
                }
            }
        }

        private void DisplayHistogram()
        {
            if (!panelHistogram.Visible) return;
            ImageDocument doc = _documentController.GetActiveDocument();
            if (doc == null || doc.CurrentBitmap == null) return;

            var histo = doc.Histogram;
            if (histo == null) return;

            int max = histo.GrayCounts[0];
            for (int i = 0; i < 256; i++)
                if (histo.GrayCounts[i] > max)
                    max = histo.GrayCounts[i];
            if (max == 0) max = 1;

            if (pictureBoxGrayHistogram.Image != null) pictureBoxGrayHistogram.Image.Dispose();
            pictureBoxGrayHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxGrayHistogram.Width,
                pictureBoxGrayHistogram.Height, histo.GrayCounts, max, Color.Gray);

            if (pictureBoxRedHistogram.Image != null) pictureBoxRedHistogram.Image.Dispose();
            pictureBoxRedHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxRedHistogram.Width,
                pictureBoxRedHistogram.Height, histo.RedCounts, max, Color.Red);

            if (pictureBoxGreenHistogram.Image != null) pictureBoxGreenHistogram.Image.Dispose();
            pictureBoxGreenHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxGreenHistogram.Width,
                pictureBoxGreenHistogram.Height, histo.GreenCounts, max, Color.Green);

            if (pictureBoxBlueHistogram.Image != null) pictureBoxBlueHistogram.Image.Dispose();
            pictureBoxBlueHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxBlueHistogram.Width,
                pictureBoxBlueHistogram.Height, histo.BlueCounts, max, Color.Blue);
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelHistogram.Visible = !panelHistogram.Visible;
            if (panelHistogram.Visible) DisplayHistogram();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private double GetConstantFromUser(string title)
        {
            Form prompt = new Form()
            {
                Width = 280,
                Height = 160,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title,
                StartPosition = FormStartPosition.CenterParent
            };

            Label textLabel = new Label()
                { Left = 20, Top = 20, Text = "Nilai (Gunakan koma/titik untuk desimal):", Width = 240 };

            NumericUpDown numericInput = new NumericUpDown()
            {
                Left = 20,
                Top = 50,
                Width = 220,
                DecimalPlaces = 2,
                Maximum = 100,
                Minimum = 0.01M,
                Increment = 0.1M,
                Value = 1
            };

            Button confirmation = new Button()
                { Text = "Ok", Left = 60, Width = 70, Top = 90, DialogResult = DialogResult.OK };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(numericInput);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? (double)numericInput.Value : double.NaN;
        }

        private void customKernelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument() == null)
            {
                MessageBox.Show("Buka gambar terlebih dahulu.");
                return;
            }

            if (_documentController.GetActiveDocument().IsInSelectionMode) return;

            using (var sizeDialog = new KernelSizeDialog())
            {
                if (sizeDialog.ShowDialog() == DialogResult.OK)
                {
                    int selectedSize = sizeDialog.KernelSize;

                    using (var gridDialog = new KernelGridDialog(selectedSize))
                    {
                        if (gridDialog.ShowDialog() == DialogResult.OK)
                        {
                            double[,] kernel = gridDialog.KernelValues;
                            _controller.Filters.ApplyCustomConvolution(kernel);

                            DisplayHistogram();
                        }
                    }
                }
            }
        }

        private void histogramEqualizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument() == null) return;
            if (_documentController.GetActiveDocument().IsInSelectionMode) return;

            TabPage currentTab = tabControlCanvas.SelectedTab;

            _controller.Filters.ApplyHistogramEqualization(currentTab);

            if (panelHistogram.Visible)
            {
                DisplayHistogram();
            }
        }

        private void adaptiveHEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument() == null) return;
            if (_documentController.GetActiveDocument().IsInSelectionMode) return;

            TabPage currentTab = tabControlCanvas.SelectedTab;

            _controller.Filters.ApplyAdaptiveHistogramEqualization(currentTab);

            if (panelHistogram.Visible)
            {
                DisplayHistogram();
            }
        }

        private void linearStretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument() == null) return;
            if (_documentController.GetActiveDocument().IsInSelectionMode) return;

            TabPage currentTab = tabControlCanvas.SelectedTab;

            _controller.Filters.ApplyLinearStretchEqualization(currentTab);

            if (panelHistogram.Visible)
            {
                DisplayHistogram();
            }
        }

        private void globalStretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument() == null) return;
            _controller.Filters.ApplyGlobalContrastStretch();
        }

        private void localStretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument() == null) return;
            _controller.Filters.ApplyLocalContrastStretch();
        }

        private void pointStretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument() == null) return;
            _controller.Filters.ApplyPointContrastStretch();
        }

        private void mozaikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument() == null) return;
            // Menggunakan kernel mozaik yang telah didefinisikan di BaseKernel
            _controller.Filters.ApplyConvolutionFilter("Mozaik");
            DisplayHistogram();
        }

        private void pseudoColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument() == null) return;
            // Menerapkan filter Pseudo Color melalui metode ApplyFilter yang sudah ada
            ApplyFilterUI(new PseudoColorFilter());
        }

        private void contrastStretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentController.GetActiveDocument() == null) return;
            _controller.Filters.ApplyContrastStretchPoint();
            DisplayHistogram();
        }
        private void smoothingToolStripMenuItem_Click(object sender, EventArgs e)
            => _controller.Filters.ApplySmoothing();

        private void penajamanToolStripMenuItem_Click(object sender, EventArgs e)
            => _controller.Filters.ApplySharpening();

        private void Pb_Selection_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isSelectingArea = true;
                _startPos = e.Location;
                _selectionRect = new Rectangle(e.X, e.Y, 0, 0);
            }
        }

        private void Pb_ColorSelect_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox pb = (PictureBox)sender;
                Bitmap currentBmp = (Bitmap)pb.Image;

                // 1. Ambil warna pada koordinat yang diklik
                // Pastikan koordinat mouse sesuai dengan ukuran gambar asli
                int realX = e.X * currentBmp.Width / pb.Width;
                int realY = e.Y * currentBmp.Height / pb.Height;
                _targetColor = currentBmp.GetPixel(realX, realY);

                // 2. Isolasi Warna: Buat bitmap baru yang hanya berisi warna tersebut
                _isolatedBitmap = IsolateColor(currentBmp, _targetColor, 50); // 50 adalah toleransi warna

                // Tampilkan hasil isolasi sementara di kanvas (optional)
                pb.Image = _isolatedBitmap;
                _isDraggingIsolated = true;
            }
        }

        private void Pb_ColorSelect_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _isDraggingIsolated && _isolatedBitmap != null)
            {
                // 3. Mulai proses Drag & Drop
                // Kita membungkus bitmap ke dalam DataObject
                DataObject dragData = new DataObject(DataFormats.Bitmap, _isolatedBitmap);
                ((Control)sender).DoDragDrop(dragData, DragDropEffects.Copy);

                _isDraggingIsolated = false; // Reset setelah drop dimulai
            }
        }

        private Bitmap IsolateColor(Bitmap source, Color target, int tolerance)
        {
            Bitmap result = new Bitmap(source.Width, source.Height);

            // Gunakan LockBits jika ingin performa lebih cepat
            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    Color c = source.GetPixel(x, y);

                    // Hitung selisih warna (Euclidean distance sederhana)
                    int diffR = Math.Abs(c.R - target.R);
                    int diffG = Math.Abs(c.G - target.G);
                    int diffB = Math.Abs(c.B - target.B);

                    if (diffR <= tolerance && diffG <= tolerance && diffB <= tolerance)
                    {
                        result.SetPixel(x, y, c); // Tetapkan warna asli jika mirip
                    }
                    else
                        result.SetPixel(x, y, Color.Transparent); // Ubah jadi transparan jika tidak mirip
                }
            }
            return result;
        }

    }
}