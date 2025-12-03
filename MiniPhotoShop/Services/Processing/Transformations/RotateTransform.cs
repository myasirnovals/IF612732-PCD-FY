using System;
using System.Drawing;

namespace MiniPhotoShop.Services.Processing.Transformations
{
    public class RotateTransform
    {
        public Bitmap Process(Bitmap source, float angle)
        {
            if (source == null) return null;
            int width = source.Width;
            int height = source.Height;
            
            double rad = angle * Math.PI / 180.0;
            double cosA = Math.Abs(Math.Cos(rad));
            double sinA = Math.Abs(Math.Sin(rad));
            int newWidth = (int)(width * cosA + height * sinA);
            int newHeight = (int)(width * sinA + height * cosA);
            
            Bitmap resultBmp = new Bitmap(newWidth, newHeight);

            using (Graphics g = Graphics.FromImage(resultBmp))
            {
                g.Clear(Color.Black);
                g.TranslateTransform(newWidth / 2f, newHeight / 2f);
                g.RotateTransform(angle);
                g.TranslateTransform(-width / 2f, -height / 2f);
                g.DrawImage(source, new Point(0, 0));
            }
            return resultBmp;
        }
    }
}