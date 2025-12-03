using System;
using System.Drawing;
using System.Windows.Forms;
using MiniPhotoShop.Properties;

namespace MiniPhotoShop.Controllers
{
    public class ThumbnailController
    {
        private FlowLayoutPanel _flowLayoutPanel;
        private ContextMenuStrip _thumbnailContextMenu;
        private PictureBox _dragSourceThumb = null;
        private bool _isDragging = false;

        public event Action<Bitmap, string> ThumbnailClicked;

        public void Initialize(FlowLayoutPanel flowLayoutPanel)
        {
            _flowLayoutPanel = flowLayoutPanel;
            InitializeContextMenu();
            LoadDefaultThumbnails();
        }

        private void LoadDefaultThumbnails()
        {
            var imageResources = new[]
            {
                new { Name = "yuruyuri", Image = Resources.yuruyuri },
                new { Name = "aurora", Image = Resources.aurora },
                new { Name = "haikyuu", Image = Resources.haikyuu },
                new { Name = "ruri", Image = Resources.ruri },
            };

            foreach (var resource in imageResources)
            {
                if (resource.Image != null)
                {
                    AddThumbnail(resource.Image, resource.Name);
                }
            }
        }

        public void AddThumbnail(Image image, string name)
        {
            PictureBox thumb = new PictureBox
            {
                Image = new Bitmap(image),
                Tag = name,
                Size = new Size(120, 100),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                Margin = new Padding(10)
            };

            thumb.MouseDown += HandleMouseDown;
            thumb.MouseMove += HandleMouseMove;
            thumb.MouseUp += HandleMouseUp;
            thumb.ContextMenuStrip = _thumbnailContextMenu;

            _flowLayoutPanel.Controls.Add(thumb);
        }

        public Bitmap FindThumbnailImageByName(string name)
        {
            foreach (Control ctrl in _flowLayoutPanel.Controls)
            {
                if (ctrl is PictureBox pb && pb.Tag != null &&
                    pb.Tag.ToString().Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return new Bitmap(pb.Image);
                }
            }

            return null;
        }

        private void InitializeContextMenu()
        {
            _thumbnailContextMenu = new ContextMenuStrip();
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Hapus");
            deleteItem.Click += HandleDeleteClick;
            _thumbnailContextMenu.Items.Add(deleteItem);
        }

        private void HandleDeleteClick(object sender, EventArgs e)
        {
            try
            {
                if (sender is ToolStripMenuItem menuItem && menuItem.Owner is ContextMenuStrip contextMenu)
                {
                    Control sourceControl = contextMenu.SourceControl;
                    if (sourceControl != null)
                    {
                        _flowLayoutPanel.Controls.Remove(sourceControl);
                        (sourceControl as PictureBox)?.Image?.Dispose();
                        sourceControl.Dispose();
                    }
                }
            }
            catch (Exception) { }
        }

        private void HandleMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragSourceThumb = sender as PictureBox;
                _isDragging = false;
            }
        }

        private void HandleMouseMove(object sender, MouseEventArgs e)
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

        private void HandleMouseUp(object sender, MouseEventArgs e)
        {
            if (!_isDragging && _dragSourceThumb != null && e.Button == MouseButtons.Left)
            {
                var clickedThumbnail = sender as PictureBox;
                if (clickedThumbnail?.Image != null)
                {
                    ThumbnailClicked?.Invoke(new Bitmap(clickedThumbnail.Image), clickedThumbnail.Tag.ToString());
                }
            }
            _dragSourceThumb = null;
            _isDragging = false;
        }
    }
}