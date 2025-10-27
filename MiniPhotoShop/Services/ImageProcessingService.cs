using System.Drawing;
using MiniPhotoShop.Models;

namespace MiniPhotoShop.Services
{
    public class ImageProcessingService : IImageProcessingService
    {
        public int[,,] CreatePixelArray(Bitmap bmp)
        {
            if (bmp == null) return null;

            int width = bmp.Width;
            int height = bmp.Height;
            int[,,] pixelArray = new int[width, height, 4];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    pixelArray[x, y, 0] = c.R;
                    pixelArray[x, y, 1] = c.G;
                    pixelArray[x, y, 2] = c.B;
                    pixelArray[x, y, 3] = (int)((c.R * 0.3) + (c.G * 0.59) + (c.B * 0.11));
                }
            }
            return pixelArray;
        }
        
        public Bitmap CreateBitmapFromPixelArray(int[,,] pixelArray, IImageFilter filter)
        {
            if (pixelArray == null || filter == null) return null;

            int width = pixelArray.GetLength(0);
            int height = pixelArray.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int r = pixelArray[x, y, 0];
                    int g = pixelArray[x, y, 1];
                    int b = pixelArray[x, y, 2];
                    int gray = pixelArray[x, y, 3];
                    
                    Color c = filter.ProcessPixel(r, g, b, gray);
                    
                    bmp.SetPixel(x, y, c);
                }
            }
            return bmp;
        }

        public Bitmap CreateBitmapFromPixelArray(Bitmap sourcedBitmap, IImageFilter filter)
        {
            if (sourcedBitmap == null || filter == null) return null;
            
            int width = sourcedBitmap.Width;
            int height = sourcedBitmap.Height;
            Bitmap resultBmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color c = sourcedBitmap.GetPixel(x, y);
                    int gray = (int)((c.R * 0.3) + (c.G * 0.59) + (c.B * 0.11));
                    
                    Color newColor = filter.ProcessPixel(c.R, c.G, c.B, gray);
                    
                    resultBmp.SetPixel(x, y, newColor);
                }
            }

            return resultBmp;
        }

        public HistogramData CalculateHistogram(int[,,] pixelArray)
        {
            var data = new HistogramData();
            if (pixelArray == null) return data;

            int width = pixelArray.GetLength(0);
            int height = pixelArray.GetLength(1);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    data.RedCounts[pixelArray[x, y, 0]]++;
                    data.GreenCounts[pixelArray[x, y, 1]]++;
                    data.BlueCounts[pixelArray[x, y, 2]]++;
                    data.GrayCounts[pixelArray[x, y, 3]]++;
                }
            }

            int maxCount = 0;
            for (int i = 0; i < 256; i++)
            {
                if (data.RedCounts[i] > maxCount) maxCount = data.RedCounts[i];
                if (data.GreenCounts[i] > maxCount) maxCount = data.GreenCounts[i];
                if (data.BlueCounts[i] > maxCount) maxCount = data.BlueCounts[i];
                if (data.GrayCounts[i] > maxCount) maxCount = data.GrayCounts[i];
            }
            data.MaxCount = (maxCount == 0) ? 1 : maxCount;
            
            return data;
        }

        public Bitmap DrawHistogram(int width, int height, int[] counts, int maxCount, Color barColor)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                Pen pen = new Pen(barColor);
                float barWidth = (float)width / 256;

                for (int i = 0; i < 256; i++)
                {
                    float barHeight = ((float)counts[i] / maxCount) * height;
                    float xPos = i * barWidth;
                    g.DrawLine(pen, xPos, height, xPos, height - barHeight);
                }
            }
            return bmp;
        }
    }
}