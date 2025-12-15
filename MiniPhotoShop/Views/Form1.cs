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
            _documentController.ToggleSelectionMode(true);
            MessageBox.Show("Mode Seleksi Aktif. Klik warna pada gambar.");
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
            _controller.Arithmetic.CurrentOperation = mode;
            MessageBox.Show($"Mode {messageName} Aktif. Drag gambar dari thumbnail ke kanvas.");
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
            if (e.Data.GetDataPresent(DataFormats.StringFormat) && _controller.Arithmetic.CurrentOperation != "None")
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Canvas_DragDrop(object sender, DragEventArgs e)
        {
            if (_documentController.GetActiveDocument()?.IsInSelectionMode == true ||
                _controller.Arithmetic.CurrentOperation == "None") return;
            try
            {
                string sourceName = (string)e.Data.GetData(DataFormats.StringFormat);
                _controller.Arithmetic.HandleDragDrop(sourceName, _documentController.GetActiveDocument());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
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
    }
}