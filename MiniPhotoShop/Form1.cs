using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MiniPhotoShop.Filters;
using MiniPhotoShop.Models;
using MiniPhotoShop.Services;

namespace MiniPhotoShop
{
    public partial class Form1 : Form
    {
        private readonly IImageProcessingService _imageProcessor;
        private readonly IImageFileService _imageFileService;
        private readonly IDataExportService _dataExportService;

        private readonly Dictionary<TabPage, ImageDocument> _openDocuments = new Dictionary<TabPage, ImageDocument>();

        private readonly IImageFilter _rgbFilter = new RgbFilter();
        private readonly IImageFilter _grayFilter = new GrayscaleFilter();
        private readonly IImageFilter _redFilter = new RedChannelFilter();
        private readonly IImageFilter _greenFilter = new GreenChannelFilter();
        private readonly IImageFilter _blueFilter = new BlueChannelFilter();


        public Form1()
        {
            InitializeComponent();

            _imageProcessor = new ImageProcessingService();
            _imageFileService = new ImageFileService();
            _dataExportService = new DataExportService();

            if (tabControlCanvas.TabPages.Count > 0)
            {
                tabControlCanvas.TabPages.Clear();
            }

            InitializeThumbnails();
        }

        private void InitializeThumbnails()
        {
            var imageResources = new[]
            {
                new { Name = "yuruyuri", Image = Properties.Resources.yuruyuri },
                new { Name = "aurora", Image = Properties.Resources.aurora },
                new { Name = "haikyuu", Image = Properties.Resources.haikyuu },
                new { Name = "ruri", Image = Properties.Resources.ruri },
            };

            foreach (var resource in imageResources)
            {
                PictureBox thumb = new PictureBox
                {
                    Image = resource.Image,
                    Tag = resource.Name,
                    Size = new Size(120, 100),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.FixedSingle,
                    Cursor = Cursors.Hand,
                    Margin = new Padding(10)
                };
                thumb.Click += Thumbnail_Click;
                flowLayoutPanelThumbnails.Controls.Add(thumb);
            }
        }

        public void Thumbnail_Click(object sender, EventArgs e)
        {
            var clickedThumbnail = sender as PictureBox;
            if (clickedThumbnail?.Image == null) return;

            ProcessAndDisplayImage(new Bitmap(clickedThumbnail.Image), clickedThumbnail.Tag.ToString());
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap loadedImage = _imageFileService.OpenImage(out string fileName);
            if (loadedImage != null)
            {
                ProcessAndDisplayImage(loadedImage, fileName);
            }
        }

        private void ProcessAndDisplayImage(Bitmap image, string imageName)
        {
            if (image == null) return;

            var newDocument = new ImageDocument(image, imageName, _imageProcessor);

            TabPage newTab = AddNewTab(imageName);

            _openDocuments.Add(newTab, newDocument);

            UpdateCanvas(newTab, newDocument.CurrentBitmap);
        }

        private TabPage AddNewTab(string tabTitle)
        {
            TabPage newTabPage = new TabPage(tabTitle);
            newTabPage.Padding = new Padding(3);
            newTabPage.UseVisualStyleBackColor = true;

            PictureBox newCanvas = new PictureBox
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            newTabPage.Controls.Add(newCanvas);
            tabControlCanvas.TabPages.Add(newTabPage);
            tabControlCanvas.SelectedTab = newTabPage;

            return newTabPage;
        }

        private TabPage GetActiveTab()
        {
            return tabControlCanvas.SelectedTab;
        }

        private PictureBox GetActiveCanvas()
        {
            TabPage activeTab = GetActiveTab();
            if (activeTab != null && activeTab.Controls.Count > 0)
            {
                return activeTab.Controls[0] as PictureBox;
            }

            return null;
        }

        private ImageDocument GetActiveDocument()
        {
            TabPage activeTab = GetActiveTab();
            if (activeTab != null && _openDocuments.ContainsKey(activeTab))
            {
                return _openDocuments[activeTab];
            }

            return null;
        }

        private void UpdateCanvas(TabPage tab, Image newImage)
        {
            if (tab?.Controls[0] is PictureBox canvas)
            {
                canvas.Image = newImage;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetActiveDocument();
            if (doc == null)
            {
                MessageBox.Show("Tidak ada gambar aktif untuk disimpan.", "Info", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            _dataExportService.SavePixelData(doc.Name, doc.PixelArray, doc.IsGrayscale);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetActiveDocument();
            if (doc?.CurrentBitmap != null)
            {
                Clipboard.SetImage(doc.CurrentBitmap);
                MessageBox.Show("Gambar telah disalin ke Clipboard.", "Copy", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Tidak ada gambar untuk disalin.", "Copy", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Clipboard.ContainsImage())
            {
                MessageBox.Show("Clipboard tidak berisi gambar.", "Paste", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            Bitmap pastedImage = new Bitmap(Clipboard.GetImage());
            ProcessAndDisplayImage(pastedImage, "Pasted Image");
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage activeTab = GetActiveTab();
            if (activeTab != null)
            {
                CloseTab(activeTab);
            }
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetActiveDocument();
            if (doc == null)
            {
                MessageBox.Show("Tidak ada gambar aktif untuk dipulihkan.", "Restore", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            doc.Restore();
            UpdateCanvas(GetActiveTab(), doc.CurrentBitmap);

            MessageBox.Show("Gambar telah dikembalikan ke kondisi semula.", "Restore", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyChannelFilter(_grayFilter);
        }

        private void buttonRed_Click(object sender, EventArgs e)
        {
            ApplyChannelFilter(_redFilter);
        }

        private void buttonGreen_Click(object sender, EventArgs e)
        {
            ApplyChannelFilter(_greenFilter);
        }

        private void buttonBlue_Click(object sender, EventArgs e)
        {
            ApplyChannelFilter(_blueFilter);
        }

        private void ApplyChannelFilter(IImageFilter filter)
        {
            ImageDocument doc = GetActiveDocument();
            if (doc == null)
            {
                MessageBox.Show("Tidak ada gambar untuk difilter.", "Info", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            try
            {
                doc.ApplyFilter(filter);
                UpdateCanvas(GetActiveTab(), doc.CurrentBitmap);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat menerapkan filter: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHistogramClick(object sender, EventArgs e)
        {
            DisplayHistogram();
        }

        private void DisplayHistogram()
        {
            ImageDocument doc = GetActiveDocument();
            if (doc == null)
            {
                ClearHistogram();
                return;
            }

            try
            {
                HistogramData histo = doc.Histogram;
                int max = histo.MaxCount;

                pictureBoxRedHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxRedHistogram.Width,
                    pictureBoxRedHistogram.Height, histo.RedCounts, max, Color.Red);
                pictureBoxGreenHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxGreenHistogram.Width,
                    pictureBoxGreenHistogram.Height, histo.GreenCounts, max, Color.Green);
                pictureBoxBlueHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxBlueHistogram.Width,
                    pictureBoxBlueHistogram.Height, histo.BlueCounts, max, Color.Blue);
                pictureBoxGrayHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxGrayHistogram.Width,
                    pictureBoxGrayHistogram.Height, histo.GrayCounts, max, Color.Gray);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat membuat histogram: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearHistogram()
        {
            pictureBoxRedHistogram.Image?.Dispose();
            pictureBoxGreenHistogram.Image?.Dispose();
            pictureBoxBlueHistogram.Image?.Dispose();
            pictureBoxGrayHistogram.Image?.Dispose();

            pictureBoxRedHistogram.Image = null;
            pictureBoxGreenHistogram.Image = null;
            pictureBoxBlueHistogram.Image = null;
            pictureBoxGrayHistogram.Image = null;
        }

        private void tabControlCanvas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayHistogram();
        }

        private void tableDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetActiveDocument();
            if (doc == null)
            {
                MessageBox.Show("Tidak ada data gambar untuk diekspor.", "Info", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            _dataExportService.SaveHistogramData(doc.Name, doc.Histogram);
        }

        private void tabControlCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < this.tabControlCanvas.TabPages.Count; i++)
            {
                Rectangle tabRect = this.tabControlCanvas.GetTabRect(i);
                tabRect.Inflate(-2, -2);
                Rectangle closeButton = new Rectangle(tabRect.Right - 15, tabRect.Top + 4, 12, 12);

                if (closeButton.Contains(e.Location))
                {
                    CloseTab(this.tabControlCanvas.TabPages[i]);
                    break;
                }
            }
        }

        private void CloseTab(TabPage tab)
        {
            if (tab == null) return;

            if (_openDocuments.ContainsKey(tab))
            {
                _openDocuments[tab].CurrentBitmap?.Dispose();
                _openDocuments[tab].OriginalBitmap?.Dispose();
                _openDocuments.Remove(tab);
            }

            tabControlCanvas.TabPages.Remove(tab);
            tab.Dispose();

            DisplayHistogram();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fungsi CUT belum diimplementasikan.");
        }

        private void tabControlCanvas_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                var tabPage = this.tabControlCanvas.TabPages[e.Index];
                var tabRect = this.tabControlCanvas.GetTabRect(e.Index);
                tabRect.Inflate(-2, -2);

                TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font, tabRect, tabPage.ForeColor,
                    TextFormatFlags.Left);

                Rectangle closeButton = new Rectangle(tabRect.Right - 15, tabRect.Top + 4, 12, 12);
                ControlPaint.DrawCaptionButton(e.Graphics, closeButton, CaptionButton.Close, ButtonState.Normal);

                e.DrawFocusRectangle();
            }
            catch (Exception)
            {
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void lblBrightnessValue_Click(object sender, EventArgs e)
        {

        }
    }
}