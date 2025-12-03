using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MiniPhotoShop.Services.Processing.Transformations
{
    public class ScaleTransform
    {
        public Bitmap Process(Bitmap source, double scaleFactor)
        {
            if (source == null || scaleFactor <= 0) return null;
            int newWidth = (int)Math.Max(1, source.Width * scaleFactor);
            int newHeight = (int)Math.Max(1, source.Height * scaleFactor);
            
            Bitmap resultBitmap = new Bitmap(newWidth, newHeight, source.PixelFormat);
            using (Graphics g = Graphics.FromImage(resultBitmap))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(source, 0, 0, newWidth, newHeight);
            }
            return resultBitmap;
        }
    }
}