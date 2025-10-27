using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        private readonly IImageFilter _negationFilter = new NegationFilter();
        private ContextMenuStrip _thumbnailContextMenu;


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
            InitializeThumbnailContextMenu();
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
                thumb.ContextMenuStrip = _thumbnailContextMenu;
                flowLayoutPanelThumbnails.Controls.Add(thumb);
            }
        }

        private void InitializeThumbnailContextMenu()
        {
            _thumbnailContextMenu = new ContextMenuStrip();
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Hapus");
            deleteItem.Click += DeleteThumbnail_Click;
            _thumbnailContextMenu.Items.Add(deleteItem);
        }

        private void DeleteThumbnail_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            try
            {
                ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
                if (menuItem == null) return;

                ContextMenuStrip contextMenu = menuItem.Owner as ContextMenuStrip;
                if (contextMenu == null) return;

                Control sourceControl = contextMenu.SourceControl;
                if (sourceControl != null)
                {
                    flowLayoutPanelThumbnails.Controls.Remove(sourceControl);

                    (sourceControl as PictureBox)?.Image?.Dispose();
                    sourceControl.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menghapus thumbnail: {ex.Message}", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void Thumbnail_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            var clickedThumbnail = sender as PictureBox;
            if (clickedThumbnail?.Image == null) return;

            ProcessAndDisplayImage(new Bitmap(clickedThumbnail.Image), clickedThumbnail.Tag.ToString());
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            Bitmap loadedImage = _imageFileService.OpenImage(out string fileName);
            if (loadedImage != null)
            {
                ProcessAndDisplayImage(loadedImage, fileName);

                PictureBox thumb = new PictureBox
                {
                    Image = loadedImage,
                    Tag = fileName,
                    Size = new Size(120, 100),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.FixedSingle,
                    Cursor = Cursors.Hand,
                    Margin = new Padding(10)
                };

                thumb.Click += Thumbnail_Click;
                thumb.ContextMenuStrip = _thumbnailContextMenu;
                flowLayoutPanelThumbnails.Controls.Add(thumb);
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

            newCanvas.Click += Canvas_Click;

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
            if (IsSelectionModeActive()) return;
            ImageDocument doc = GetActiveDocument();
            if (doc == null)
            {
                MessageBox.Show("Tidak ada gambar aktif untuk disimpan.", "Info", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

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
                                format = System.Drawing.Imaging.ImageFormat.Jpeg;
                                break;
                            case ".bmp":
                                format = System.Drawing.Imaging.ImageFormat.Bmp;
                                break;
                        }

                        doc.CurrentBitmap.Save(sfd.FileName, format);
                        MessageBox.Show("Gambar berhasil disimpan!", "Sukses", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Terjadi kesalahan saat menyimpan gambar: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
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
            if (IsSelectionModeActive()) return;
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
            if (IsSelectionModeActive()) return;
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
            DisplayHistogram();

            trackBarBrightness.Value = 0;
            lblBrightnessValue.Text = "0";

            trackBarThreshold.Value = 128;
            lblThresholdValue.Text = "128";

            MessageBox.Show("Gambar telah dikembalikan ke kondisi semula.", "Restore", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ApplyChannelFilter(_grayFilter);
        }

        private void negationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ApplyChannelFilter(_negationFilter);
        }

        private void buttonRed_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ApplyChannelFilter(_redFilter);
        }

        private void buttonGreen_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ApplyChannelFilter(_greenFilter);
        }

        private void buttonBlue_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
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
                trackBarBrightness.Value = 0;
                lblBrightnessValue.Text = "0";
                doc.Restore();

                doc.ApplyFilter(filter);
                UpdateCanvas(GetActiveTab(), doc.CurrentBitmap);
                DisplayHistogram();
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
            if (doc == null || doc.CurrentBitmap == null)
            {
                ClearHistogram();
                return;
            }

            try
            {
                bool isCurrentlyGrayscale = doc.IsGrayscale;

                HistogramData histo = doc.Histogram;

                if (!isCurrentlyGrayscale)
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
                }
                else
                {
                    pictureBoxRedHistogram.Image?.Dispose();
                    pictureBoxGreenHistogram.Image?.Dispose();
                    pictureBoxBlueHistogram.Image?.Dispose();
                    pictureBoxRedHistogram.Image = null;
                    pictureBoxGreenHistogram.Image = null;
                    pictureBoxBlueHistogram.Image = null;
                }

                int maxGray = histo.GrayCounts.Max();
                if (maxGray == 0) maxGray = 1;
                pictureBoxGrayHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxGrayHistogram.Width,
                    pictureBoxGrayHistogram.Height, histo.GrayCounts, maxGray, Color.Gray);
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
            if (IsSelectionModeActive()) return;
            ImageDocument doc = GetActiveDocument();
            if (doc == null)
            {
                MessageBox.Show("Tidak ada data gambar untuk diekspor.", "Info", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            int[,,] currentPixelArray = _imageProcessor.CreatePixelArray(doc.CurrentBitmap);

            HistogramData currentHistogram = _imageProcessor.CalculateHistogram(currentPixelArray);

            _dataExportService.SaveHistogramData(doc.Name, currentHistogram);
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
            if (IsSelectionModeActive()) return;
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

        private void trackBarBrightness_Scroll(object sender, EventArgs e)
        {
            lblBrightnessValue.Text = trackBarBrightness.Value.ToString();
        }

        private void trackBarBrightness_MouseUp(object sender, MouseEventArgs e)
        {
            ApplyBrightnessFilter();
        }

        private void ApplyBrightnessFilter()
        {
            if (IsSelectionModeActive()) return;
            ImageDocument doc = GetActiveDocument();
            if (doc == null) return;

            try
            {
                int brightness = trackBarBrightness.Value;

                IImageFilter brightnessFilter = new BrightnessFilter(brightness);

                doc.Restore();

                if (brightness != 0)
                {
                    doc.ApplyFilter(brightnessFilter);
                }

                UpdateCanvas(GetActiveTab(), doc.CurrentBitmap);
                DisplayHistogram();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat menerapkan brightness: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void labelThreshold_Click(object sender, EventArgs e)
        {
        }

        private void bwToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ImageDocument doc = GetActiveDocument();
            if (doc == null)
            {
                MessageBox.Show("Tidak ada gambar untuk difilter.", "Info", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            trackBarThreshold.Value = 128;
            lblThresholdValue.Text = "128";

            PerformThresholdApply();
        }

        private void trackBarThreshold_Scroll(object sender, EventArgs e)
        {
            lblThresholdValue.Text = trackBarThreshold.Value.ToString();
        }

        private void trackBarThreshold_MouseUp(object sender, MouseEventArgs e)
        {
            ImageDocument doc = GetActiveDocument();

            if (doc == null) return;

            if (!doc.IsBlackAndWhite)
            {
                MessageBox.Show("Anda harus menerapkan filter Black/White dari menu FILTER terlebih dahulu.",
                    "Peringatan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                trackBarThreshold.Value = 128;
                lblThresholdValue.Text = "128";
                return;
            }

            PerformThresholdApply();
        }

        private void PerformThresholdApply()
        {
            if (IsSelectionModeActive()) return;
            ImageDocument doc = GetActiveDocument();
            if (doc == null)
            {
                return;
            }

            try
            {
                int threshold = trackBarThreshold.Value;

                IImageFilter thresholdFilter = new ThresholdFilter(threshold);

                doc.Restore();

                doc.ApplyFilter(thresholdFilter);

                UpdateCanvas(GetActiveTab(), doc.CurrentBitmap);
                DisplayHistogram();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat menerapkan threshold: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsSelectionModeActive()
        {
            ImageDocument doc = GetActiveDocument();
            if (doc != null && doc.IsInSelectionMode)
            {
                MessageBox.Show(
                    "Fitur lain tidak dapat digunakan saat dalam mode Seleksi Gambar.\nKlik 'Restore Original' untuk keluar.",
                    "Mode Seleksi Aktif",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return true;
            }

            return false;
        }

        private void imageSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetActiveDocument();
            if (doc == null) return;

            doc.IsInSelectionMode = true;
            MessageBox.Show("Mode Seleksi Gambar diaktifkan.\nKlik pada sebuah warna di gambar untuk menyeleksinya.",
                "Mode Seleksi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Canvas_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetActiveDocument();
            PictureBox canvas = sender as PictureBox;
            MouseEventArgs mouse = e as MouseEventArgs;

            if (doc == null || canvas == null || mouse == null || !doc.IsInSelectionMode)
            {
                return;
            }

            Point? imagePoint = ConvertCoordinates(canvas, mouse.Location);
            if (!imagePoint.HasValue) return;

            Color clickedColor = doc.OriginalBitmap.GetPixel(imagePoint.Value.X, imagePoint.Value.Y);
            ColorRanges clickedRange = ColorClassifier.GetColorRange(clickedColor.R, clickedColor.G, clickedColor.B);

            if (clickedRange == doc.SelectedColorRange)
            {
                doc.Restore();
                UpdateCanvas(GetActiveTab(), doc.CurrentBitmap);
            }
            else
            {
                doc.SelectedColorRange = clickedRange;

                IImageFilter colorFilter = new ColorRangeFilter(clickedRange);
                Bitmap filteredBitmap = _imageProcessor.CreateBitmapFromPixelArray(doc.OriginalBitmap, colorFilter);

                doc.CurrentBitmap = filteredBitmap;
                doc.PixelArray = _imageProcessor.CreatePixelArray(doc.CurrentBitmap);
                doc.Histogram = _imageProcessor.CalculateHistogram(doc.PixelArray);

                UpdateCanvas(GetActiveTab(), doc.CurrentBitmap);
            }

            DisplayHistogram();
        }

        private Point? ConvertCoordinates(PictureBox pb, Point mousePos)
        {
            if (pb.Image == null) return null;

            int w_i = pb.Image.Width;
            int h_i = pb.Image.Height;
            int w_c = pb.ClientSize.Width;
            int h_c = pb.ClientSize.Height;

            float ratio_i = (float)w_i / h_i;
            float ratio_c = (float)w_c / h_c;

            if (ratio_i > ratio_c)
            {
                float h_d = w_c / ratio_i;
                float y_offset = (h_c - h_d) / 2;

                if (mousePos.Y < y_offset || mousePos.Y > y_offset + h_d) return null;

                float scale = (float)w_i / w_c;
                int x = (int)(mousePos.X * scale);
                int y = (int)((mousePos.Y - y_offset) * scale);
                return new Point(x, y);
            }
            else
            {
                float w_d = h_c * ratio_i;
                float x_offset = (w_c - w_d) / 2;
                if (mousePos.X < x_offset || mousePos.X > x_offset + w_d) return null;

                float scale = (float)h_i / h_c;
                int x = (int)((mousePos.X - x_offset) * scale);
                int y = (int)(mousePos.Y * scale);
                return new Point(x, y);
            }
        }
    }
}