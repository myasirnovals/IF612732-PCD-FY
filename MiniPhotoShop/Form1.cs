using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MiniPhotoShop.Filters;
using MiniPhotoShop.Models;
using MiniPhotoShop.Services;
using System.IO;
using MiniPhotoShop.Managers;


namespace MiniPhotoShop
{
    public partial class Form1 : Form
    {
        private readonly IImageProcessingService _imageProcessor;
        private readonly IImageFileService _imageFileService;
        private readonly IDataExportService _dataExportService;
        private readonly Dictionary<TabPage, ImageDocument> _openDocuments = new Dictionary<TabPage, ImageDocument>();
        private readonly Dictionary<TabPage, bool> _isBitwiseDocument = new Dictionary<TabPage, bool>();
        private string _pendingOperation = null;
        private readonly IImageFilter _rgbFilter = new RgbFilter();
        private readonly IImageFilter _grayFilter = new GrayscaleFilter();
        private readonly IImageFilter _redFilter = new RedChannelFilter();
        private readonly IImageFilter _greenFilter = new GreenChannelFilter();
        private readonly IImageFilter _blueFilter = new BlueChannelFilter();
        private readonly IImageFilter _negationFilter = new NegationFilter();
        private readonly IImageFilter _notFilter = new NotFilter();
        private ContextMenuStrip _thumbnailContextMenu;
        private PictureBox _dragSourceThumb = null;
        private bool _isDragging = false;
        private ImageProcessingService imageProcessorService1;
        private ImageProcessingService imageProcessorService2;
        private IDialogService dialogService;

        public Form1(Managers.DocumentManager documentManager, Managers.ThumbnailManager thumbnailManager)
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

        public Form1(DocumentManager documentManager, ThumbnailManager thumbnailManager, IImageFileService imageFileService, IDataExportService dataExportService, ImageProcessingService imageProcessorService1, ImageProcessingService imageProcessorService2, IDialogService dialogService) : this(documentManager, thumbnailManager)
        {
            _imageFileService = imageFileService;
            _dataExportService = dataExportService;
            this.imageProcessorService1 = imageProcessorService1;
            this.imageProcessorService2 = imageProcessorService2;
            this.dialogService = dialogService;
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

                thumb.MouseDown += Thumbnail_MouseDown;
                thumb.MouseMove += Thumbnail_MouseMove;
                thumb.MouseUp += Thumbnail_MouseUp;

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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            Bitmap loadedImage = _imageFileService.OpenImage(out string fileName);
            if (loadedImage != null)
            {
                ProcessAndDisplayImage(new Bitmap(loadedImage), fileName);
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

                thumb.MouseDown += Thumbnail_MouseDown;
                thumb.MouseMove += Thumbnail_MouseMove;
                thumb.MouseUp += Thumbnail_MouseUp;

                thumb.ContextMenuStrip = _thumbnailContextMenu;
                flowLayoutPanelThumbnails.Controls.Add(thumb);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ImageDocument doc = GetActiveDocument();
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
                        MessageBox.Show($"Terjadi kesalahan saat menyimpan gambar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            if (Clipboard.ContainsImage())
            {
                Bitmap pastedImage = new Bitmap(Clipboard.GetImage());
                ProcessAndDisplayImage(pastedImage, "Pasted Image");
            }
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

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            MessageBox.Show("Fungsi CUT belum diimplementasikan.");
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

        private void notToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ApplyChannelFilter(_notFilter);
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ApplyChannelFilter(_redFilter);
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ApplyChannelFilter(_greenFilter);
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ApplyChannelFilter(_blueFilter);
        }

        private void bwToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ImageDocument doc = GetActiveDocument();
            if (doc == null) return;

            Action<int> previewAction = (value) => { ApplyThresholdFilter(value, isPreview: true); };

            DialogResult result = ShowAdjustmentDialog(
                "Atur Threshold Black/White", 0, 255, 128, 16, "B/W:",
                previewAction, out int newThresholdValue
            );

            if (result == DialogResult.OK)
            {
                ApplyThresholdFilter(newThresholdValue, isPreview: false);
            }
            else
            {
                doc.Restore();
                UpdateCanvas(GetActiveTab(), doc.CurrentBitmap);
                DisplayHistogram();
            }
        }

        private void brightnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ImageDocument doc = GetActiveDocument();
            if (doc == null) return;

            Action<int> previewAction = (value) => { ApplyBrightnessFilter(value, isPreview: true); };

            DialogResult result = ShowAdjustmentDialog(
                "Atur Brightness", -255, 255, 0, 32, "Brightness:",
                previewAction, out int newBrightnessValue
            );

            if (result == DialogResult.OK)
            {
                ApplyBrightnessFilter(newBrightnessValue, isPreview: false);
            }
            else
            {
                doc.Restore();
                UpdateCanvas(GetActiveTab(), doc.CurrentBitmap);
                DisplayHistogram();
            }
        }

        private void imageSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_pendingOperation != null)
            {
                MessageBox.Show($"Mode '{_pendingOperation}' sedang aktif. Batalkan (Restore) sebelum memulai mode seleksi.", "Operasi Tertunda", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ImageDocument doc = GetActiveDocument();
            if (doc == null) return;
            doc.IsInSelectionMode = true;
            MessageBox.Show("Mode Seleksi Gambar diaktifkan.\nKlik pada sebuah warna di gambar untuk menyeleksinya.", "Mode Seleksi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetActiveDocument();
            if (doc == null) return;

            bool wasInOperationMode = (_pendingOperation != null);
            _pendingOperation = null;

            if (_isBitwiseDocument.ContainsKey(GetActiveTab()))
                _isBitwiseDocument[GetActiveTab()] = false;

            doc.Restore(); 
            UpdateCanvas(GetActiveTab(), doc.CurrentBitmap);
            DisplayHistogram();

            if (wasInOperationMode)
            {
                MessageBox.Show("Mode operasi dibatalkan dan gambar telah dikembalikan.", "Operasi Dibatalkan", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Gambar telah dikembalikan ke kondisi semula.", "Restore", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SetPendingOperation(string operation)
        {
            ImageDocument doc = GetActiveDocument();
            if (doc != null && doc.IsInSelectionMode)
            {
                MessageBox.Show("Batalkan 'Mode Seleksi' (Restore) sebelum memulai operasi ini.", "Mode Seleksi Aktif", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (doc == null)
            {
                MessageBox.Show("Silakan buka gambar terlebih dahulu.", "Tidak Ada Gambar Aktif", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _pendingOperation = operation;
            MessageBox.Show($"Mode '{operation}' diaktifkan.\nSeret gambar dari panel thumbnail ke kanvas ini untuk menerapkan.",
                            "Mode Operasi Aktif", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void penjumlahanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPendingOperation("Add");
        }

        private void penguranganToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPendingOperation("Subtract");
        }

        private void andToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPendingOperation("AND");
        }

        private void orToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPendingOperation("OR");
        }

        private void xorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPendingOperation("XOR");
        }


        private void tableDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ImageDocument doc = GetActiveDocument();
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

        private void savePixelDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsSelectionModeActive()) return;
            ImageDocument doc = GetActiveDocument();
            if (doc == null) return;

            TabPage activeTab = GetActiveTab();
            bool isBitwise = _isBitwiseDocument.ContainsKey(activeTab) && _isBitwiseDocument[activeTab];

            int[,,] currentPixelArray = _imageProcessor.CreatePixelArray(doc.CurrentBitmap);
            if (currentPixelArray == null) return;

            _dataExportService.SavePixelData(doc.Name, currentPixelArray, doc.IsGrayscale, isBitwise);
        }

        private void Thumbnail_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragSourceThumb = sender as PictureBox;
                _isDragging = false;
            }
        }

        private void Thumbnail_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _dragSourceThumb != null)
            {
                _isDragging = true;
                if (_dragSourceThumb == sender)
                {
                    _dragSourceThumb.DoDragDrop(_dragSourceThumb.Tag.ToString(), DragDropEffects.Copy);
                }
                _dragSourceThumb = null;
            }
        }

        private void Thumbnail_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_isDragging && _dragSourceThumb != null && e.Button == MouseButtons.Left)
            {
                if (IsSelectionModeActive()) return; 
                var clickedThumbnail = sender as PictureBox;
                if (clickedThumbnail?.Image != null)
                {
                    ProcessAndDisplayImage(new Bitmap(clickedThumbnail.Image), clickedThumbnail.Tag.ToString());
                }
            }
            _dragSourceThumb = null;
            _isDragging = false;
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
                MessageBox.Show($"Gagal menghapus thumbnail: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProcessAndDisplayImage(Bitmap image, string imageName, bool isBitwiseResult = false)
        {
            if (image == null) return;
            var newDocument = new ImageDocument(image, imageName, _imageProcessor);
            TabPage newTab = AddNewTab(imageName);
            _openDocuments.Add(newTab, newDocument);

            _isBitwiseDocument[newTab] = isBitwiseResult;

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
                SizeMode = PictureBoxSizeMode.Zoom,
                AllowDrop = true
            };

            newCanvas.Click += Canvas_Click;
            newCanvas.DragEnter += Canvas_DragEnter;
            newCanvas.DragDrop += Canvas_DragDrop;

            newTabPage.Controls.Add(newCanvas);
            tabControlCanvas.TabPages.Add(newTabPage);
            tabControlCanvas.SelectedTab = newTabPage;
            return newTabPage;
        }

        private void CloseTab(TabPage tab)
        {
            if (tab == null) return;

            if (tab == GetActiveTab())
            {
                _pendingOperation = null;
            }

            if (_openDocuments.ContainsKey(tab))
            {
                _openDocuments[tab].CurrentBitmap?.Dispose();
                _openDocuments[tab].OriginalBitmap?.Dispose();
                _openDocuments.Remove(tab);
            }

            if (_isBitwiseDocument.ContainsKey(tab))
            {
                _isBitwiseDocument.Remove(tab);
            }

            tabControlCanvas.TabPages.Remove(tab);
            tab.Dispose();
            DisplayHistogram();
        }

        private void tabControlCanvas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayHistogram();
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

        private void tabControlCanvas_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                var tabPage = this.tabControlCanvas.TabPages[e.Index];
                var tabRect = this.tabControlCanvas.GetTabRect(e.Index);
                tabRect.Inflate(-2, -2);
                TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font, tabRect, tabPage.ForeColor, TextFormatFlags.Left);
                Rectangle closeButton = new Rectangle(tabRect.Right - 15, tabRect.Top + 4, 12, 12);
                ControlPaint.DrawCaptionButton(e.Graphics, closeButton, CaptionButton.Close, ButtonState.Normal);
                e.DrawFocusRectangle();
            }
            catch (Exception) { }
        }

        private void Canvas_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
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
            string sourceName = (string)e.Data.GetData(DataFormats.StringFormat);

            using (Bitmap sourceBmp = FindThumbnailImageByName(sourceName))
            {
                if (sourceBmp == null)
                {
                    MessageBox.Show("Gagal menemukan gambar yang diseret.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_pendingOperation == null)
                {
                    ProcessAndDisplayImage(new Bitmap(sourceBmp), sourceName);
                }
                else
                {
                    try
                    {
                        ImageDocument targetDoc = GetActiveDocument();
                        if (targetDoc == null) return;

                        Bitmap resultBmp = null;
                        string opName = _pendingOperation;
                        bool isBitwise = false;

                        switch (_pendingOperation)
                        {
                            case "Add":
                                resultBmp = _imageProcessor.AddImages(sourceBmp, targetDoc.CurrentBitmap);
                                opName = "Addition";
                                break;
                            case "Subtract":
                                resultBmp = _imageProcessor.SubtractImages(sourceBmp, targetDoc.CurrentBitmap);
                                opName = "Difference";
                                break;
                            case "AND":
                                resultBmp = _imageProcessor.AndImages(sourceBmp, targetDoc.CurrentBitmap);
                                isBitwise = true;
                                break;
                            case "OR":
                                resultBmp = _imageProcessor.OrImages(sourceBmp, targetDoc.CurrentBitmap);
                                isBitwise = true;
                                break;
                            case "XOR":
                                resultBmp = _imageProcessor.XorImages(sourceBmp, targetDoc.CurrentBitmap);
                                isBitwise = true;
                                break;
                        }

                        if (resultBmp != null)
                        {
                            string newName = $"{Path.GetFileNameWithoutExtension(targetDoc.Name)}_{opName}_{Path.GetFileNameWithoutExtension(sourceName)}";
                            ProcessAndDisplayImage(resultBmp, newName, isBitwise);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Terjadi kesalahan saat drag-drop: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        _pendingOperation = null;
                    }
                }
            }
        }

        private Bitmap FindThumbnailImageByName(string name)
        {
            foreach (Control ctrl in flowLayoutPanelThumbnails.Controls)
            {
                if (ctrl is PictureBox pb && pb.Tag.ToString().Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return new Bitmap(pb.Image);
                }
            }
            foreach (var doc in _openDocuments.Values)
            {
                if (doc.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return new Bitmap(doc.OriginalBitmap);
                }
            }
            return null;
        }

        private void ApplyChannelFilter(IImageFilter filter)
        {
            ImageDocument doc = GetActiveDocument();
            if (doc == null) return;
            try
            {
                bool isBitwise = (filter is NotFilter);
                if (_isBitwiseDocument.ContainsKey(GetActiveTab()))
                    _isBitwiseDocument[GetActiveTab()] = isBitwise;
                else
                    _isBitwiseDocument.Add(GetActiveTab(), isBitwise);

                doc.ApplyFilter(filter);
                UpdateCanvas(GetActiveTab(), doc.CurrentBitmap);
                DisplayHistogram();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat menerapkan filter: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyBrightnessFilter(int brightness, bool isPreview = false)
        {
            ImageDocument doc = GetActiveDocument();
            if (doc == null) return;
            try
            {

                if (!isPreview && _isBitwiseDocument.ContainsKey(GetActiveTab()))
                    _isBitwiseDocument[GetActiveTab()] = false;

                if (brightness < -255) brightness = -255;
                if (brightness > 255) brightness = 255;

                IImageFilter brightnessFilter = new BrightnessFilter(brightness);
                doc.ApplyFilter(brightnessFilter);

                UpdateCanvas(GetActiveTab(), doc.CurrentBitmap);

                if (!isPreview)
                {
                    DisplayHistogram();
                }
            }
            catch (Exception)
            {
                if (!isPreview) MessageBox.Show("Error saat menerapkan brightness.");
            }
        }

        private void ApplyThresholdFilter(int threshold, bool isPreview = false)
        {
            ImageDocument doc = GetActiveDocument();
            if (doc == null) return;
            try
            {
                if (!isPreview && _isBitwiseDocument.ContainsKey(GetActiveTab()))
                    _isBitwiseDocument[GetActiveTab()] = false;

                if (threshold < 0) threshold = 0;
                if (threshold > 255) threshold = 255;

                IImageFilter thresholdFilter = new ThresholdFilter(threshold);
                doc.ApplyFilter(thresholdFilter);

                UpdateCanvas(GetActiveTab(), doc.CurrentBitmap);

                if (!isPreview)
                {
                    DisplayHistogram();
                }
            }
            catch (Exception)
            {
                if (!isPreview) MessageBox.Show("Error saat menerapkan threshold.");
            }
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

        private void DisplayHistogram()
        {
            if (!panelHistogram.Visible)
            {
                ClearHistogram();
                return;
            }

            ImageDocument doc = GetActiveDocument();
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

                    int maxGray = histo.GrayCounts.Max(); if (maxGray == 0) maxGray = 1;
                    pictureBoxGrayHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxGrayHistogram.Width, pictureBoxGrayHistogram.Height, histo.GrayCounts, maxGray, Color.Gray);
                }
                else
                {
                    int maxRed = histo.RedCounts.Max(); if (maxRed == 0) maxRed = 1;
                    pictureBoxRedHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxRedHistogram.Width, pictureBoxRedHistogram.Height, histo.RedCounts, maxRed, Color.Red);

                    int maxGreen = histo.GreenCounts.Max(); if (maxGreen == 0) maxGreen = 1;
                    pictureBoxGreenHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxGreenHistogram.Width, pictureBoxGreenHistogram.Height, histo.GreenCounts, maxGreen, Color.Green);

                    int maxBlue = histo.BlueCounts.Max(); if (maxBlue == 0) maxBlue = 1;
                    pictureBoxBlueHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxBlueHistogram.Width, pictureBoxBlueHistogram.Height, histo.BlueCounts, maxBlue, Color.Blue);

                    int maxGray = histo.GrayCounts.Max(); if (maxGray == 0) maxGray = 1;
                    pictureBoxGrayHistogram.Image = _imageProcessor.DrawHistogram(pictureBoxGrayHistogram.Width, pictureBoxGrayHistogram.Height, histo.GrayCounts, maxGray, Color.Gray);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat membuat histogram: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private DialogResult ShowAdjustmentDialog(
            string title, int min, int max, int initialValue, int tickFreq, string labelText,
            Action<int> onPreview,
            out int finalValue)
        {
            Form dialog = new Form()
            {
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                ControlBox = false,
                Text = title,
                Size = new Size(320, 140)
            };
            Label label = new Label() { Text = labelText, Location = new Point(12, 23), AutoSize = true };
            TrackBar trackBar = new TrackBar()
            {
                Minimum = min,
                Maximum = max,
                Value = initialValue,
                TickFrequency = tickFreq,
                Location = new Point(label.Right + 5, 15),
                Size = new Size(180, 45)
            };
            Label valueLabel = new Label()
            {
                Text = initialValue.ToString(),
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(trackBar.Right + 10, 23),
                Size = new Size(40, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            trackBar.Scroll += (sender, e) => {
                valueLabel.Text = trackBar.Value.ToString();
                onPreview(trackBar.Value);
            };

            Button btnOk = new Button() { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(130, 70), Size = new Size(75, 23) };
            Button btnCancel = new Button() { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(215, 70), Size = new Size(75, 23) };
            dialog.Controls.Add(label);
            dialog.Controls.Add(trackBar);
            dialog.Controls.Add(valueLabel);
            dialog.Controls.Add(btnOk);
            dialog.Controls.Add(btnCancel);
            dialog.AcceptButton = btnOk;
            dialog.CancelButton = btnCancel;

            DialogResult result = dialog.ShowDialog();
            finalValue = trackBar.Value;
            dialog.Dispose();
            return result;
        }

        private bool IsSelectionModeActive()
        {
            ImageDocument doc = GetActiveDocument();
            if (doc != null && doc.IsInSelectionMode)
            {
                MessageBox.Show("Fitur lain tidak dapat digunakan saat dalam mode Seleksi Gambar.\nKlik 'Restore Original' untuk keluar.", "Mode Seleksi Aktif", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }

            if (_pendingOperation != null)
            {
                MessageBox.Show($"Mode '{_pendingOperation}' sedang aktif.\n" +
                                "Seret gambar dari thumbnail ke kanvas untuk menyelesaikan.\n" +
                                "Klik 'Restore Original' untuk membatalkan.", "Operasi Tertunda", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }

            return false;
        }

        private void Canvas_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetActiveDocument();
            PictureBox canvas = sender as PictureBox;
            MouseEventArgs mouse = e as MouseEventArgs;

            // Jangan lakukan seleksi warna jika sedang menunggu operasi
            if (_pendingOperation != null)
            {
                MessageBox.Show($"Mode '{_pendingOperation}' sedang aktif. Selesaikan atau batalkan (Restore) operasi.", "Operasi Tertuda", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (doc == null || canvas == null || mouse == null || !doc.IsInSelectionMode)
            {
                return;
            }

            if (_isBitwiseDocument.ContainsKey(GetActiveTab()))
                _isBitwiseDocument[GetActiveTab()] = false;

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
                doc.ApplyFilter(colorFilter);
                UpdateCanvas(GetActiveTab(), doc.CurrentBitmap);
            }

            DisplayHistogram();
        }

        private Point? ConvertCoordinates(PictureBox pb, Point mousePos)
        {
            if (pb.Image == null) return null;
            int w_i = pb.Image.Width; int h_i = pb.Image.Height;
            int w_c = pb.ClientSize.Width; int h_c = pb.ClientSize.Height;
            float ratio_i = (float)w_i / h_i; float ratio_c = (float)w_c / h_c;
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