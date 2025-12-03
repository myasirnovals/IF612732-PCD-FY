using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniPhotoShop.Managers
{
    public class ThumbnailManager
    {
        public event Action<Bitmap, string> ThumbnailClicked;
        private FlowLayoutPanel _panel;

        public void Initialize(FlowLayoutPanel panel)
        {
            _panel = panel;
        }

        public void AddThumbnail(Image image, string name)
        {
        }
    }
}