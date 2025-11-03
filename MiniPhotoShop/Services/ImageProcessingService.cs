using System.Drawing;
using System.Drawing.Imaging;
using MiniPhotoShop.Models;
using System;
using System.Runtime.InteropServices;

namespace MiniPhotoShop.Services
{
    public unsafe class ImageProcessingService : IImageProcessingService, IImageArithmeticService
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

        private Bitmap ResizeAndPad(Bitmap image, int targetWidth, int targetHeight)
        {
            Bitmap canvas = new Bitmap(targetWidth, targetHeight, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.Clear(Color.Black);
                g.DrawImage(image, 0, 0, image.Width, image.Height);
            }

            return canvas;
        }

        private Bitmap PerformArithmetic(Bitmap source, Bitmap target, string operation)
        {
            int maxWidth = Math.Max(source.Width, target.Width);
            int maxHeight = Math.Max(source.Height, target.Height);

            using (Bitmap paddedTarget = ResizeAndPad(target, maxWidth, maxHeight))
            using (Bitmap paddedSource = ResizeAndPad(source, maxWidth, maxHeight))
            {
                Bitmap resultBmp = new Bitmap(maxWidth, maxHeight, PixelFormat.Format32bppArgb);
                BitmapData dataA = paddedTarget.LockBits(new Rectangle(0, 0, maxWidth, maxHeight),
                    ImageLockMode.ReadOnly, paddedTarget.PixelFormat);
                BitmapData dataB = paddedSource.LockBits(new Rectangle(0, 0, maxWidth, maxHeight),
                    ImageLockMode.ReadOnly, paddedSource.PixelFormat);
                BitmapData dataResult = resultBmp.LockBits(new Rectangle(0, 0, maxWidth, maxHeight),
                    ImageLockMode.WriteOnly, resultBmp.PixelFormat);

                int bytesPerPixel = 4;
                int stride = dataA.Stride;
                byte* pFirstA = (byte*)dataA.Scan0;
                byte* pFirstB = (byte*)dataB.Scan0;
                byte* pFirstResult = (byte*)dataResult.Scan0;

                for (int y = 0; y < maxHeight; y++)
                {
                    byte* pRowA = pFirstA + (y * stride);
                    byte* pRowB = pFirstB + (y * stride);
                    byte* pRowResult = pFirstResult + (y * stride);
                    for (int x = 0; x < maxWidth; x++)
                    {
                        int i = x * bytesPerPixel;
                        int b1 = pRowA[i];
                        int g1 = pRowA[i + 1];
                        int r1 = pRowA[i + 2];
                        int b2 = pRowB[i];
                        int g2 = pRowB[i + 1];
                        int r2 = pRowB[i + 2];
                        int r_new, g_new, b_new;

                        if (operation == "Add")
                        {
                            r_new = Math.Min(255, r1 + r2);
                            g_new = Math.Min(255, g1 + g2);
                            b_new = Math.Min(255, b1 + b2);
                        }
                        else
                        {
                            int diffR = Math.Abs(r1 - r2);
                            int diffG = Math.Abs(g1 - g2);
                            int diffB = Math.Abs(b1 - b2);
                            int threshold = 15;

                            if (diffR <= threshold && diffG <= threshold && diffB <= threshold)
                            {
                                r_new = 0; g_new = 0; b_new = 0;
                            }
                            else
                            {
                                r_new = r2; g_new = g2; b_new = b2;
                            }
                        }

                        pRowResult[i] = (byte)b_new;
                        pRowResult[i + 1] = (byte)g_new;
                        pRowResult[i + 2] = (byte)r_new;
                        pRowResult[i + 3] = 255;
                    }
                }

                paddedTarget.UnlockBits(dataA);
                paddedSource.UnlockBits(dataB);
                resultBmp.UnlockBits(dataResult);

                return resultBmp;
            }
        }

        public Bitmap AddImages(Bitmap source, Bitmap target)
        {
            if (source == null || target == null) return null;
            return PerformArithmetic(source, target, "Add");
        }

        public Bitmap SubtractImages(Bitmap source, Bitmap target)
        {
            if (source == null || target == null) return null;
            return PerformArithmetic(source, target, "Subtract");
        }
<<<<<<< HEAD


        private Bitmap PerformBitwiseOperation(Bitmap source, Bitmap target, string operation)
        {
            int maxWidth = Math.Max(source.Width, target.Width);
            int maxHeight = Math.Max(source.Height, target.Height);

            using (Bitmap paddedTarget = ResizeAndPad(target, maxWidth, maxHeight))
            using (Bitmap paddedSource = ResizeAndPad(source, maxWidth, maxHeight))
            {
                Bitmap resultBmp = new Bitmap(maxWidth, maxHeight, PixelFormat.Format32bppArgb);
                BitmapData dataA = paddedTarget.LockBits(new Rectangle(0, 0, maxWidth, maxHeight), ImageLockMode.ReadOnly, paddedTarget.PixelFormat);
                BitmapData dataB = paddedSource.LockBits(new Rectangle(0, 0, maxWidth, maxHeight), ImageLockMode.ReadOnly, paddedSource.PixelFormat);
                BitmapData dataResult = resultBmp.LockBits(new Rectangle(0, 0, maxWidth, maxHeight), ImageLockMode.WriteOnly, resultBmp.PixelFormat);

                int bytesPerPixel = 4;
                int stride = dataA.Stride;
                byte* pFirstA = (byte*)dataA.Scan0;
                byte* pFirstB = (byte*)dataB.Scan0;
                byte* pFirstResult = (byte*)dataResult.Scan0;

                for (int y = 0; y < maxHeight; y++)
                {
                    byte* pRowA = pFirstA + (y * stride);
                    byte* pRowB = pFirstB + (y * stride); 
                    byte* pRowResult = pFirstResult + (y * stride);
                    for (int x = 0; x < maxWidth; x++)
                    {
                        int i = x * bytesPerPixel;

                        int b1 = pRowA[i];
                        int g1 = pRowA[i + 1];
                        int r1 = pRowA[i + 2];

                        int b2 = pRowB[i];
                        int g2 = pRowB[i + 1];
                        int r2 = pRowB[i + 2];

                        int r_new, g_new, b_new;

                        switch (operation)
                        {
                            case "AND":
                                r_new = r1 & r2;
                                g_new = g1 & g2;
                                b_new = b1 & b2;
                                break;
                            case "OR":
                                r_new = r1 | r2;
                                g_new = g1 | g2;
                                b_new = b1 | b2;
                                break;
                            case "XOR":
                                r_new = r1 ^ r2;
                                g_new = g1 ^ g2;
                                b_new = b1 ^ b2;
                                break;
                            default:
                                r_new = r1; g_new = g1; b_new = b1;
                                break;
                        }

                        pRowResult[i] = (byte)b_new;
                        pRowResult[i + 1] = (byte)g_new;
                        pRowResult[i + 2] = (byte)r_new;
                        pRowResult[i + 3] = 255;
                    }
                }
                paddedTarget.UnlockBits(dataA);
                paddedSource.UnlockBits(dataB);
                resultBmp.UnlockBits(dataResult);

                return resultBmp;
            }
        }

        public Bitmap AndImages(Bitmap source, Bitmap target)
        {
            if (source == null || target == null) return null;
            return PerformBitwiseOperation(source, target, "AND");
        }

        public Bitmap OrImages(Bitmap source, Bitmap target)
        {
            if (source == null || target == null) return null;
            return PerformBitwiseOperation(source, target, "OR");
        }

        public Bitmap XorImages(Bitmap source, Bitmap target)
        {
            if (source == null || target == null) return null;
            return PerformBitwiseOperation(source, target, "XOR");
        }
=======
>>>>>>> b2cca5aeae26e51f632ed1759be726b35639eff7
    }
}