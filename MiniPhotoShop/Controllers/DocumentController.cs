using MiniPhotoShop.Filters.Adjustments;
using MiniPhotoShop.Filters.Base;
using MiniPhotoShop.Filters.ColorsFilters;
using MiniPhotoShop.Filters.Helpers;
using MiniPhotoShop.Helpers;
using MiniPhotoShop.Models;
using MiniPhotoShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MiniPhotoShop.Controllers
{
    public class DocumentController
    {
        private readonly IImageProcessingService _imageProcessor;
        private readonly Dictionary<TabPage, ImageDocument> _openDocuments = new Dictionary<TabPage, ImageDocument>();
        private TabControl _tabControl;
        private EventHandler _selectionChangedHandler;

        public event Action ActiveDocumentChanged;
        public event MouseEventHandler CanvasMouseWheel;
        public event DragEventHandler CanvasDragEnter;
        public event DragEventHandler CanvasDragDrop;

        public DocumentController(IImageProcessingService imageProcessor)
        {
            _imageProcessor = imageProcessor;
        }

        public void Initialize(TabControl tabControl)
        {
            _tabControl = tabControl;
            _selectionChangedHandler = (s, e) => ActiveDocumentChanged?.Invoke();

            _tabControl.SelectedIndexChanged += _selectionChangedHandler;
            _tabControl.MouseClick += TabControl_MouseClick;
            _tabControl.DrawItem += TabControlCanvas_DrawItem;
            _tabControl.TabPages.Clear();
        }


        public ImageDocument GetActiveDocument()
        {
            if (_tabControl?.SelectedTab != null && _openDocuments.ContainsKey(_tabControl.SelectedTab))
            {
                return _openDocuments[_tabControl.SelectedTab];
            }
            return null;
        }

        public void OpenDocument(Bitmap image, string imageName)
        {
            if (image == null) return;

            Bitmap safeImage = new Bitmap(image);
            var newDocument = new ImageDocument(safeImage, imageName, _imageProcessor);

            TabPage newTab = CreateNewTab(imageName);
            _openDocuments.Add(newTab, newDocument);
            UpdateCanvas(newTab, newDocument.CurrentBitmap);

            ActiveDocumentChanged?.Invoke();
        }

        public void CloseActiveDocument()
        {
            if (_tabControl.SelectedTab != null) CloseTab(_tabControl.SelectedTab);
        }

        private void CloseTab(TabPage tab)
        {
            if (tab == null) return;
            
            if (tab.Controls.Count > 0 && tab.Controls[0] is PictureBox pb)
            {
                pb.Image = null; 
                pb.Refresh();    
            }

            if (_openDocuments.ContainsKey(tab))
            {
                _openDocuments[tab].CurrentBitmap?.Dispose();
                _openDocuments[tab].OriginalBitmap?.Dispose();
                _openDocuments.Remove(tab);
            }

            _tabControl.SelectedIndexChanged -= _selectionChangedHandler;

            try
            {
                if (_openDocuments.ContainsKey(tab))
                {
                    var doc = _openDocuments[tab];
                    doc.CurrentBitmap?.Dispose();
                    doc.OriginalBitmap?.Dispose();
                    _openDocuments.Remove(tab);
                }

                if (_tabControl.TabPages.Contains(tab))
                {
                    _tabControl.TabPages.Remove(tab);
                }

                tab.Dispose();
            }
            catch (Exception)
            {
            }
            finally
            {
                _tabControl.SelectedIndexChanged += _selectionChangedHandler;
                ActiveDocumentChanged?.Invoke();
            }
        }

        public void UpdateActiveCanvas()
        {
            ImageDocument doc = GetActiveDocument();
            if (doc != null && _tabControl != null && _tabControl.SelectedTab != null)
            {
                UpdateCanvas(_tabControl.SelectedTab, doc.CurrentBitmap);
            }
        }

        public void ToggleSelectionMode(bool enable)
        {
            ImageDocument doc = GetActiveDocument();
            if (doc != null) doc.IsInSelectionMode = enable;
        }

        public void ApplyFilterToActiveDocument(IImageFilter filter)
        {
            var doc = GetActiveDocument();
            if (doc == null) return;

            doc.ApplyFilter(filter);
            doc.IsGrayscale = (filter is GrayscaleFilter);
            doc.IsBlackAndWhite = (filter is ThresholdFilter);

            UpdateActiveCanvas();
        }

        public void RestoreActiveDocument()
        {
            var doc = GetActiveDocument();
            if (doc == null) return;
            doc.Restore();
            UpdateActiveCanvas();
        }

        private void UpdateCanvas(TabPage tab, Image newImage)
        {
            if (tab != null && tab.Controls.Count > 0 && tab.Controls[0] is PictureBox canvas)
            {
                canvas.Image = newImage;
                canvas.Refresh();
            }
        }

        // Di dalam DocumentController.cs, update method CreateNewTab
        private TabPage CreateNewTab(string title)
        {
            TabPage page = new TabPage(title)
            {
                Padding = new Padding(3),
                AutoScroll = true, // PERBAIKAN: Memungkinkan scroll jika gambar besar
                AllowDrop = true   // PERBAIKAN: Memungkinkan fitur drag and drop
            };

            page.DragEnter += (s, e) => CanvasDragEnter?.Invoke(s, e);
            page.DragDrop += (s, e) => CanvasDragDrop?.Invoke(s, e);

            PictureBox canvas = new PictureBox
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.None, // Gunakan None agar AutoScroll tab berfungsi
                SizeMode = PictureBoxSizeMode.AutoSize,
                AllowDrop = true
            };

            page.Controls.Add(canvas);
            _tabControl.TabPages.Add(page);
            _tabControl.SelectedTab = page;
            return page;
        }

        private void HandleCanvasClick(object sender, EventArgs e)
        {
            ImageDocument doc = GetActiveDocument();
            PictureBox canvas = sender as PictureBox;
            MouseEventArgs mouse = e as MouseEventArgs;

            if (doc == null || canvas == null || mouse == null || !doc.IsInSelectionMode) return;

            Point? imgPoint = CoordinateHelper.ConvertToImageCoordinates(canvas, mouse.Location);

            if (imgPoint.HasValue)
            {
                try
                {
                    Color clickedColor = doc.OriginalBitmap.GetPixel(imgPoint.Value.X, imgPoint.Value.Y);
                    ColorRanges clickedRange = ColorClassifier.GetColorRange(clickedColor.R, clickedColor.G, clickedColor.B);

                    if (clickedRange == doc.SelectedColorRange)
                    {
                        RestoreActiveDocument();
                    }
                    else
                    {
                        doc.SelectedColorRange = clickedRange;
                        doc.ApplyFilter(new ColorRangeFilter(clickedRange));
                    }

                    UpdateActiveCanvas();
                    ActiveDocumentChanged?.Invoke();
                }
                catch { }
            }
        }

        private void TabControl_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = _tabControl.TabPages.Count - 1; i >= 0; i--)
            {
                Rectangle tabRect = _tabControl.GetTabRect(i);
                tabRect.Inflate(-2, -2);
                Rectangle closeButton = new Rectangle(tabRect.Right - 15, tabRect.Top + 4, 12, 12);
                if (closeButton.Contains(e.Location))
                {
                    CloseTab(_tabControl.TabPages[i]);
                    break;
                }
            }
        }

        private void TabControlCanvas_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                if (e.Index < 0 || e.Index >= _tabControl.TabPages.Count) return;

                var tabPage = _tabControl.TabPages[e.Index];
                var tabRect = _tabControl.GetTabRect(e.Index);
                tabRect.Inflate(-2, -2);

                TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font, tabRect, tabPage.ForeColor,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

                Rectangle closeButton = new Rectangle(tabRect.Right - 15, tabRect.Top + 4, 12, 12);
                ControlPaint.DrawCaptionButton(e.Graphics, closeButton, CaptionButton.Close, ButtonState.Normal);
                e.DrawFocusRectangle();
            }
            catch { }
        }
    }
}