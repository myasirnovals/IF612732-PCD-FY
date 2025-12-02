using System;
using System.Drawing;
using System.Windows.Forms;
using MiniPhotoShop.Controllers;
using MiniPhotoShop.Filters;
using MiniPhotoShop.Managers;
using MiniPhotoShop.Models;
using MiniPhotoShop.Services;

namespace MiniPhotoShop
{
    public partial class Form1 : Form
    {
        private readonly MainController _controller;
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

            _controller = new MainController(
                documentManager,
                thumbnailManager,
                imageFileService,
                dataExportService,
                imageProcessor,
                arithmeticService,
                dialogService
            );

            _documentManager.Initialize(tabControlCanvas);
            _thumbnailManager.Initialize(flowLayoutPanelThumbnails);

            _documentManager.ActiveDocumentChanged += DisplayHistogram;
            _documentManager.CanvasDragEnter += Canvas_DragEnter;
            _documentManager.CanvasDragDrop += Canvas_DragDrop;
            _thumbnailManager.ThumbnailClicked += OnThumbnailClicked;
        }

        private void OnThumbnailClicked(Bitmap image, string name)
        {
            if (_documentManager.IsSelectionModeActive()) return;
            _documentManager.OpenDocument(image, name);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentManager.IsSelectionModeActive()) return;
            _controller.OpenImage();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentManager.IsSelectionModeActive()) return;
            _controller.SaveImage();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentManager.IsSelectionModeActive()) return;
            var doc = _documentManager.GetActiveDocument();
            if (doc?.CurrentBitmap != null)
            {
                Clipboard.SetImage(doc.CurrentBitmap);
                _documentManager.CloseActiveDocument();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentManager.IsSelectionModeActive()) return;
            var doc = _documentManager.GetActiveDocument();
            if (doc?.CurrentBitmap != null) Clipboard.SetImage(doc.CurrentBitmap);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentManager.IsSelectionModeActive()) return;
            if (Clipboard.ContainsImage())
            {
                Bitmap bmp = new Bitmap(Clipboard.GetImage());
                _documentManager.OpenDocument(bmp, "Pasted Image");
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentManager.IsSelectionModeActive()) return;
            _documentManager.CloseActiveDocument();
        }

        private void ApplyFilterUI(IImageFilter filter)
        {
            if (_documentManager.IsSelectionModeActive()) return;
            _controller.ApplyFilter(filter, tabControlCanvas.SelectedTab);
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
            if (_documentManager.IsSelectionModeActive()) return;
            _dialogService.ShowAdjustmentDialog("Threshold", 0, 255, 128, 10, "Value",
                (val) => { _controller.ApplyThreshold(val); },
                out int finalVal);
            DisplayHistogram();
        }

        private void brightnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentManager.IsSelectionModeActive()) return;
            _dialogService.ShowAdjustmentDialog("Brightness", -255, 255, 0, 10, "Value",
                (val) => { _controller.ApplyBrightness(val); },
                out int finalVal);
            DisplayHistogram();
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.RestoreActiveDocument();
            DisplayHistogram();
        }

        private void imageSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _documentManager.ToggleSelectionMode(true);
            MessageBox.Show("Mode Seleksi Aktif. Klik warna pada gambar.");
        }

        private void translasiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentManager.IsSelectionModeActive()) return;
            _dialogService.ShowAdjustmentDialog("Translasi X", -1000, 1000, 0, 50, "X:", (v) => { }, out int x);
            _dialogService.ShowAdjustmentDialog("Translasi Y", -1000, 1000, 0, 50, "Y:", (v) => { }, out int y);
            _controller.ApplyTransformation("Translate", x, y);
        }

        private void distorsiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentManager.IsSelectionModeActive()) return;
            _dialogService.ShowAdjustmentDialog("Amp", 0, 100, 20, 5, "Amp:", (v) => { }, out int amp);
            _dialogService.ShowAdjustmentDialog("Freq", 0, 50, 5, 1, "Freq:", (v) => { }, out int freq);
            _controller.ApplyTransformation("Distort", amp, freq);
        }

        private void rotasiCitraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_documentManager.IsSelectionModeActive()) return;
            _dialogService.ShowAdjustmentDialog("Rotasi", -360, 360, 0, 45, "Angle:", (v) => { }, out int angle);
            _controller.ApplyTransformation("Rotate", angle, 0);
        }

        private void dilatasiCitraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double factor = GetConstantFromUser("Scale Factor");
            if (!double.IsNaN(factor)) _controller.ApplyTransformation("Scale", factor, 0);
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e) => _controller.ApplyTransformation("Scale", 2.0, 0);
        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e) => _controller.ApplyTransformation("Scale", 0.5, 0);

        private void SetArithmeticMode(string mode, string messageName)
        {
            _controller.CurrentArithmeticOperation = mode;
            MessageBox.Show($"Mode {messageName} Aktif. Drag gambar dari thumbnail ke kanvas.");
        }

        private void tambahToolStripMenuItem_Click(object sender, EventArgs e) => SetArithmeticMode("Add", "Tambah");
        private void kurangToolStripMenuItem_Click(object sender, EventArgs e) => SetArithmeticMode("Subtract", "Kurang");
        private void kaliToolStripMenuItem_Click(object sender, EventArgs e) => SetArithmeticMode("Multiply", "Kali");
        private void bagiToolStripMenuItem_Click(object sender, EventArgs e) => SetArithmeticMode("Divide", "Bagi");
        private void andToolStripMenuItem_Click(object sender, EventArgs e) => SetArithmeticMode("AND", "AND");
        private void orToolStripMenuItem_Click(object sender, EventArgs e) => SetArithmeticMode("OR", "OR");
        private void xorToolStripMenuItem_Click(object sender, EventArgs e) => SetArithmeticMode("XOR", "XOR");

        private void kaliKonstantaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double val = GetConstantFromUser("Konstanta Perkalian");
            if (!double.IsNaN(val)) _controller.ApplyConstantArithmetic("Multiply", val);
        }

        private void bagiKonstantaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double val = GetConstantFromUser("Konstanta Pembagian");
            if (!double.IsNaN(val)) _controller.ApplyConstantArithmetic("Divide", val);
        }

        private void tableDataToolStripMenuItem_Click(object sender, EventArgs e) => _controller.ExportHistogramData();

        private void savePixelDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Simpan sebagai Binary?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                _controller.ExportPixelData(true);
            else
                _controller.ExportPixelData(false);
        }

        private void Canvas_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat) && _controller.CurrentArithmeticOperation != "None")
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Canvas_DragDrop(object sender, DragEventArgs e)
        {
            if (_documentManager.IsSelectionModeActive() || _controller.CurrentArithmeticOperation == "None") return;
            try
            {
                string sourceName = (string)e.Data.GetData(DataFormats.StringFormat);
                _controller.HandleDragDropArithmetic(sourceName, _documentManager.GetActiveDocument());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void DisplayHistogram()
        {
            if (!panelHistogram.Visible) return;
            ImageDocument doc = _documentManager.GetActiveDocument();
            if (doc == null || doc.CurrentBitmap == null) return;

            var histo = doc.Histogram;
            int max = histo.GrayCounts[0];
            for (int i = 0; i < 256; i++) if (histo.GrayCounts[i] > max) max = histo.GrayCounts[i];
            if (max == 0) max = 1;

            if (pictureBoxGrayHistogram.Image != null) pictureBoxGrayHistogram.Image.Dispose();
            pictureBoxGrayHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxGrayHistogram.Width, pictureBoxGrayHistogram.Height, histo.GrayCounts, max, Color.Gray);

            if (pictureBoxRedHistogram.Image != null) pictureBoxRedHistogram.Image.Dispose();
            pictureBoxRedHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxRedHistogram.Width, pictureBoxRedHistogram.Height, histo.RedCounts, max, Color.Red);

            if (pictureBoxGreenHistogram.Image != null) pictureBoxGreenHistogram.Image.Dispose();
            pictureBoxGreenHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxGreenHistogram.Width, pictureBoxGreenHistogram.Height, histo.GreenCounts, max, Color.Green);

            if (pictureBoxBlueHistogram.Image != null) pictureBoxBlueHistogram.Image.Dispose();
            pictureBoxBlueHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxBlueHistogram.Width, pictureBoxBlueHistogram.Height, histo.BlueCounts, max, Color.Blue);
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelHistogram.Visible = !panelHistogram.Visible;
            if (panelHistogram.Visible) DisplayHistogram();
        }

        private void Form1_Load(object sender, EventArgs e) { }

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
            Label textLabel = new Label() { Left = 20, Top = 20, Text = "Nilai:", Width = 240 };
            NumericUpDown numericInput = new NumericUpDown() { Left = 20, Top = 50, Width = 220, DecimalPlaces = 4, Maximum = 100000, Minimum = 0 };
            Button confirmation = new Button() { Text = "Ok", Left = 60, Width = 70, Top = 90, DialogResult = DialogResult.OK };
            prompt.Controls.Add(textLabel); prompt.Controls.Add(numericInput); prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? (double)numericInput.Value : double.NaN;
        }
    }
}