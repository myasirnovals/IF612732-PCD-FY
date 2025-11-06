using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MiniPhotoShop.Filters;
using MiniPhotoShop.Models;
using MiniPhotoShop.Services;

namespace MiniPhotoShop.Managers
{
    public class DocumentManager
    {
        private readonly IImageProcessingService _imageProcessor;
        private readonly Dictionary<TabPage, ImageDocument> _openDocuments = new Dictionary<TabPage, ImageDocument>();
        private TabControl _tabControl;

        public event Action ActiveDocumentChanged;

        public DocumentManager(IImageProcessingService imageProcessor)
        {
            _imageProcessor = imageProcessor;
        }

        public void Initialize(TabControl tabControl)
        {
            _tabControl = tabControl;
            _tabControl.SelectedIndexChanged += (s, e) => ActiveDocumentChanged?.Invoke();
            _tabControl.MouseClick += TabControl_MouseClick;
            _tabControl.DrawItem += TabControlCanvas_DrawItem;

            if (_tabControl.TabPages.Count > 0)
                _tabControl.TabPages.Clear();
        }

        public ImageDocument GetActiveDocument()
        {
            TabPage activeTab = _tabControl?.SelectedTab;
            if (activeTab != null && _openDocuments.ContainsKey(activeTab))
            {
                return _openDocuments[activeTab];
            }

            return null;
        }

        public void OpenDocument(Bitmap image, string imageName)
        {
            if (image == null) return;
            var newDocument = new ImageDocument(image, imageName, _imageProcessor);

            TabPage newTab = AddNewTab(imageName);
            _openDocuments.Add(newTab, newDocument);
            UpdateCanvas(newTab, newDocument.CurrentBitmap);

            ActiveDocumentChanged?.Invoke();
        }

        public void CloseActiveDocument()
        {
            CloseTab(_tabControl.SelectedTab);
        }

        public void CloseTab(TabPage tab)
        {
            if (tab == null) return;
            if (_openDocuments.ContainsKey(tab))
            {
                _openDocuments[tab].CurrentBitmap?.Dispose();
                _openDocuments[tab].OriginalBitmap?.Dispose();
                _openDocuments.Remove(tab);
            }

            _tabControl.TabPages.Remove(tab);
            tab.Dispose();
            ActiveDocumentChanged?.Invoke();
        }

        public void UpdateActiveCanvas()
        {
            ImageDocument doc = GetActiveDocument();
            TabPage tab = _tabControl.SelectedTab;
            if (doc != null && tab != null)
            {
                UpdateCanvas(tab, doc.CurrentBitmap);
            }
        }

        public bool IsSelectionModeActive()
        {
            ImageDocument doc = GetActiveDocument();
            return doc != null && doc.IsInSelectionMode;
        }

        public void ToggleSelectionMode(bool enable)
        {
            ImageDocument doc = GetActiveDocument();
            if (doc != null)
            {
                doc.IsInSelectionMode = enable;
            }
        }

        private void UpdateCanvas(TabPage tab, Image newImage)
        {
            if (tab?.Controls[0] is PictureBox canvas)
            {
                canvas.Image = newImage;
            }
        }

        private TabPage AddNewTab(string tabTitle)
        {
            TabPage newTabPage = new TabPage(tabTitle) { Padding = new Padding(3) };
            PictureBox newCanvas = new PictureBox
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                AllowDrop = true
            };

            newCanvas.Click += Canvas_Click;

            newCanvas.DragEnter += (s, e) => CanvasDragEnter?.Invoke(s, e);
            newCanvas.DragDrop += (s, e) => CanvasDragDrop?.Invoke(s, e);

            newTabPage.Controls.Add(newCanvas);
            _tabControl.TabPages.Add(newTabPage);
            _tabControl.SelectedTab = newTabPage;
            return newTabPage;
        }

        public event DragEventHandler CanvasDragEnter;
        public event DragEventHandler CanvasDragDrop;

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
            }
            else
            {
                doc.SelectedColorRange = clickedRange;
                IImageFilter colorFilter = new ColorRangeFilter(clickedRange);
                doc.ApplyFilter(colorFilter);
            }

            UpdateActiveCanvas();
            ActiveDocumentChanged?.Invoke();
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

        private void TabControl_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < _tabControl.TabPages.Count; i++)
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
                var tabPage = _tabControl.TabPages[e.Index];
                var tabRect = _tabControl.GetTabRect(e.Index);
                tabRect.Inflate(-2, -2);

                Rectangle textRect = tabRect;

                TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font, textRect, tabPage.ForeColor,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

                Rectangle closeButton = new Rectangle(tabRect.Right - 15, tabRect.Top + 4, 12, 12);
                ControlPaint.DrawCaptionButton(e.Graphics, closeButton, CaptionButton.Close, ButtonState.Normal);
                e.DrawFocusRectangle();
            }
            catch (Exception)
            {
            }
        }
    }
}