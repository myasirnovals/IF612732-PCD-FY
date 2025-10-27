using System.Drawing;
using System.Drawing.Imaging;
using MiniPhotoShop.Models;

namespace MiniPhotoShop.Services
{
    public unsafe class ImageProcessingService : IImageProcessingService
    {
        public int[,,] CreatePixelArray(Bitmap bmp)
        {
            if (bmp == null) return null;

            int width = bmp.Width;
            int height = bmp.Height;
            int[,,] pixelArray = new int[width, height, 4];

            BitmapData bmpData =
                bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, bmp.PixelFormat);

            int bytesPerPixel = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
            int stride = bmpData.Stride;
            byte* PtrFirstPixel = (byte*)bmpData.Scan0;

            for (int y = 0; y < height; y++)
            {
                byte* pCurrentRow = PtrFirstPixel + (y * stride);
                for (int x = 0; x < width; x++)
                {
                    int x_offset = x * bytesPerPixel;

                    int b = pCurrentRow[x_offset];
                    int g = pCurrentRow[x_offset + 1];
                    int r = pCurrentRow[x_offset + 2];

                    pixelArray[x, y, 0] = r;
                    pixelArray[x, y, 1] = g;
                    pixelArray[x, y, 2] = b;
                    pixelArray[x, y, 3] = (int)((r * 0.3) + (g * 0.59) + (b * 0.11));
                }
            }

            bmp.UnlockBits(bmpData);
            return pixelArray;
        }

        public Bitmap CreateBitmapFromPixelArray(int[,,] pixelArray, IImageFilter filter)
        {
            if (pixelArray == null || filter == null) return null;

            int width = pixelArray.GetLength(0);
            int height = pixelArray.GetLength(1);

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly,
                bmp.PixelFormat);

            int bytesPerPixel = 4;
            int stride = bmpData.Stride;
            byte* PtrFirstPixel = (byte*)bmpData.Scan0;

            for (int y = 0; y < height; y++)
            {
                byte* pCurrentRow = PtrFirstPixel + (y * stride);
                for (int x = 0; x < width; x++)
                {
                    int r = pixelArray[x, y, 0];
                    int g = pixelArray[x, y, 1];
                    int b = pixelArray[x, y, 2];
                    int gray = pixelArray[x, y, 3];

                    Color c = filter.ProcessPixel(r, g, b, gray);

                    int x_offset = x * bytesPerPixel;
                    pCurrentRow[x_offset] = c.B;
                    pCurrentRow[x_offset + 1] = c.G;
                    pCurrentRow[x_offset + 2] = c.R;
                    pCurrentRow[x_offset + 3] = 255;
                }
            }

            bmp.UnlockBits(bmpData);
            return bmp;
        }

        public Bitmap CreateBitmapFromPixelArray(Bitmap sourcedBitmap, IImageFilter filter)
        {
            if (sourcedBitmap == null || filter == null) return null;

            int width = sourcedBitmap.Width;
            int height = sourcedBitmap.Height;

            Bitmap resultBmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            BitmapData sourceData = sourcedBitmap.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly, sourcedBitmap.PixelFormat);

            BitmapData resultData = resultBmp.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly, resultBmp.PixelFormat);

            int srcBytesPerPixel = Image.GetPixelFormatSize(sourcedBitmap.PixelFormat) / 8;
            int resBytesPerPixel = 4;

            int srcStride = sourceData.Stride;
            int resStride = resultData.Stride;

            byte* pSrcFirst = (byte*)sourceData.Scan0;
            byte* pResFirst = (byte*)resultData.Scan0;

            for (int y = 0; y < height; y++)
            {
                byte* pSrcRow = pSrcFirst + (y * srcStride);
                byte* pResRow = pResFirst + (y * resStride);

                for (int x = 0; x < width; x++)
                {
                    int src_x_offset = x * srcBytesPerPixel;
                    int b = pSrcRow[src_x_offset];
                    int g = pSrcRow[src_x_offset + 1];
                    int r = pSrcRow[src_x_offset + 2];
                    int gray = (int)((r * 0.3) + (g * 0.59) + (b * 0.11));

                    Color newColor = filter.ProcessPixel(r, g, b, gray);

                    int res_x_offset = x * resBytesPerPixel;
                    pResRow[res_x_offset] = newColor.B;
                    pResRow[res_x_offset + 1] = newColor.G;
                    pResRow[res_x_offset + 2] = newColor.R;
                    pResRow[res_x_offset + 3] = 255;
                }
            }

            sourcedBitmap.UnlockBits(sourceData);
            resultBmp.UnlockBits(resultData);

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