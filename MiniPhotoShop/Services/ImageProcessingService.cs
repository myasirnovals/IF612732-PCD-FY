using System.Drawing;
using System.Drawing.Imaging;
using MiniPhotoShop.Models;
using System;
using System.Runtime.InteropServices;

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
            byte* ptrFirstPixel = (byte*)bmpData.Scan0;
            for (int y = 0; y < height; y++)
            {
                byte* pCurrentRow = ptrFirstPixel + (y * stride);
                for (int x = 0; x < width; x++)
                {
                    int xOffset = x * bytesPerPixel;
                    int b = pCurrentRow[xOffset];
                    int g = pCurrentRow[xOffset + 1];
                    int r = pCurrentRow[xOffset + 2];
                    pixelArray[x, y, 0] = r;
                    pixelArray[x, y, 1] = g;
                    pixelArray[x, y, 2] = b;
                    pixelArray[x, y, 3] = (int)((r * 0.3) + (g * 0.59) + (b * 0.11));
                }
            }

            bmp.UnlockBits(bmpData);
            return pixelArray;
        }

        public Bitmap CreateBitmapFromPixelArray(Bitmap sourcedBitmap, IImageFilter filter)
        {
            if (sourcedBitmap == null || filter == null) return null;
            int width = sourcedBitmap.Width;
            int height = sourcedBitmap.Height;
            Bitmap resultBmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData sourceData = sourcedBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly,
                sourcedBitmap.PixelFormat);
            BitmapData resultData = resultBmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly,
                resultBmp.PixelFormat);
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
                    int srcXOffset = x * srcBytesPerPixel;
                    int b = pSrcRow[srcXOffset];
                    int g = pSrcRow[srcXOffset + 1];
                    int r = pSrcRow[srcXOffset + 2];
                    int gray = (int)((r * 0.3) + (g * 0.59) + (b * 0.11));
                    Color newColor = filter.ProcessPixel(r, g, b, gray);
                    int resXOffset = x * resBytesPerPixel;
                    pResRow[resXOffset] = newColor.B;
                    pResRow[resXOffset + 1] = newColor.G;
                    pResRow[resXOffset + 2] = newColor.R;
                    pResRow[resXOffset + 3] = 255;
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

        public Bitmap DistortImage(Bitmap source, double amplitude, double frequency)
        {
            if (source == null) return null;
            int width = source.Width;
            int height = source.Height;
            Bitmap resultBmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData srcData = source.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, source.PixelFormat);
            BitmapData resData = resultBmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, resultBmp.PixelFormat);
            int bytesPerPixel = 4;
            int stride = srcData.Stride;

            byte* srcPtr = (byte*)srcData.Scan0;
            byte* resPtr = (byte*)resData.Scan0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double sineValue = Math.Sin(2 * Math.PI * y * frequency / height);
                    int srcX = x + (int)(amplitude * sineValue);
                    int srcY = y;
                    int resIndex = (y * stride) + (x * bytesPerPixel);

                    if (srcX >= 0 && srcX < width && srcY >= 0 && srcY < height)
                    {
                        int srcIndex = (srcY * stride) + (srcX * bytesPerPixel);
                        resPtr[resIndex] = srcPtr[srcIndex];
                        resPtr[resIndex + 1] = srcPtr[srcIndex + 1];
                        resPtr[resIndex + 2] = srcPtr[srcIndex + 2];
                        resPtr[resIndex + 3] = srcPtr[srcIndex + 3];
                    }
                    else
                    {
                        resPtr[resIndex] = 0; resPtr[resIndex + 1] = 0; resPtr[resIndex + 2] = 0; resPtr[resIndex + 3] = 255;
                    }
                }
            }
            source.UnlockBits(srcData);
            resultBmp.UnlockBits(resData);
            return resultBmp;
        }

        public Bitmap RotateImage(Bitmap source, float angle)
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

        public Bitmap ScaleImage(Bitmap source, double scaleFactor)
        {
            if (source == null || scaleFactor <= 0) return null;
            int oldWidth = source.Width;
            int oldHeight = source.Height;
            int newWidth = (int)Math.Max(1, oldWidth * scaleFactor);
            int newHeight = (int)Math.Max(1, oldHeight * scaleFactor);
            Bitmap resultBitmap = new Bitmap(newWidth, newHeight, source.PixelFormat);
            using (Graphics g = Graphics.FromImage(resultBitmap))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(source, 0, 0, newWidth, newHeight);
            }
            return resultBitmap;
        }

        public Bitmap TranslateImage(Bitmap source, int xOffset, int yOffset)
        {
            if (source == null) return null;
            int width = source.Width;
            int height = source.Height;
            Bitmap resultBmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData srcData = source.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, source.PixelFormat);
            BitmapData resData = resultBmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, resultBmp.PixelFormat);
            int bytesPerPixel = 4;
            int stride = srcData.Stride;

            byte* srcPtr = (byte*)srcData.Scan0;
            byte* resPtr = (byte*)resData.Scan0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int srcX = x - xOffset;
                    int srcY = y - yOffset;
                    int resIndex = (y * stride) + (x * bytesPerPixel);

                    if (srcX >= 0 && srcX < width && srcY >= 0 && srcY < height)
                    {
                        int srcIndex = (srcY * stride) + (srcX * bytesPerPixel);
                        resPtr[resIndex] = srcPtr[srcIndex];
                        resPtr[resIndex + 1] = srcPtr[srcIndex + 1];
                        resPtr[resIndex + 2] = srcPtr[srcIndex + 2];
                        resPtr[resIndex + 3] = srcPtr[srcIndex + 3];
                    }
                    else
                    {
                        resPtr[resIndex] = 255; resPtr[resIndex + 1] = 255; resPtr[resIndex + 2] = 255; resPtr[resIndex + 3] = 255;
                    }
                }
            }
            source.UnlockBits(srcData);
            resultBmp.UnlockBits(resData);
            return resultBmp;
        }
    }
}