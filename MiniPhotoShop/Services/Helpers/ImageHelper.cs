using System.Drawing;
using System.Drawing.Imaging;

namespace MiniPhotoShop.Services.Helpers
{
    public static class ImageHelper
    {
        public static Bitmap ResizeAndPad(Bitmap image, int targetWidth, int targetHeight)
        {
            Bitmap canvas = new Bitmap(targetWidth, targetHeight, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.Clear(Color.Black);
                g.DrawImage(image, 0, 0, image.Width, image.Height);
            }

            return canvas;
        }
    }
}