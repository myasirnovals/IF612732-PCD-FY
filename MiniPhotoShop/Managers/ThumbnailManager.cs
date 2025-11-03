using System;
using System.Drawing;
using System.Windows.Forms;
using MiniPhotoShop.Properties;

namespace MiniPhotoShop.Managers
{
    public class ThumbnailManager
    {
        private FlowLayoutPanel _flowLayoutPanel;
        private ContextMenuStrip _thumbnailContextMenu;
        private PictureBox _dragSourceThumb = null;
        private bool _isDragging = false;

        public event Action<Bitmap, string> ThumbnailClicked;

        public void Initialize(FlowLayoutPanel flowLayoutPanel)
        {
            _flowLayoutPanel = flowLayoutPanel;
            InitializeThumbnailContextMenu();
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
                AddThumbnail(resource.Image, resource.Name);
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

            thumb.MouseDown += Thumbnail_MouseDown;
            thumb.MouseMove += Thumbnail_MouseMove;
            thumb.MouseUp += Thumbnail_MouseUp;
            thumb.ContextMenuStrip = _thumbnailContextMenu;

            _flowLayoutPanel.Controls.Add(thumb);
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
            try
            {
                ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
                if (menuItem == null) return;
                ContextMenuStrip contextMenu = menuItem.Owner as ContextMenuStrip;
                if (contextMenu == null) return;
                Control sourceControl = contextMenu.SourceControl;
                if (sourceControl != null)
                {
                    _flowLayoutPanel.Controls.Remove(sourceControl);
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
                var clickedThumbnail = sender as PictureBox;
                if (clickedThumbnail?.Image != null)
                {
                    ThumbnailClicked?.Invoke(new Bitmap(clickedThumbnail.Image), clickedThumbnail.Tag.ToString());
                }
            }

            _dragSourceThumb = null;
            _isDragging = false;
        }

        public Bitmap FindThumbnailImageByName(string name)
        {
            foreach (Control ctrl in _flowLayoutPanel.Controls)
            {
                if (ctrl is PictureBox pb && pb.Tag.ToString().Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return new Bitmap(pb.Image);
                }
            }

            return null;
        }
    }
}