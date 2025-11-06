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
            byte* ptrFirstPixel = (byte*)bmpData.Scan0;
            for (int y = 0; y < height; y++)
            {
                byte* pCurrentRow = ptrFirstPixel + (y * stride);
                for (int x = 0; x < width; x++)
                {
                    int r = pixelArray[x, y, 0];
                    int g = pixelArray[x, y, 1];
                    int b = pixelArray[x, y, 2];
                    int gray = pixelArray[x, y, 3];
                    Color c = filter.ProcessPixel(r, g, b, gray);
                    int xOffset = x * bytesPerPixel;
                    pCurrentRow[xOffset] = c.B;
                    pCurrentRow[xOffset + 1] = c.G;
                    pCurrentRow[xOffset + 2] = c.R;
                    pCurrentRow[xOffset + 3] = 255;
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

                        int rNew, gNew, bNew;

                        switch (operation)
                        {
                            case "Add":
                                rNew = Math.Min(255, r1 + r2);
                                gNew = Math.Min(255, g1 + g2);
                                bNew = Math.Min(255, b1 + b2);
                                break;

                            case "Subtract":
                                rNew = Math.Abs(r2 - r1);
                                gNew = Math.Abs(g2 - g1);
                                bNew = Math.Abs(b2 - b1);
                                break;

                            default:
                                rNew = gNew = bNew = 0;
                                break;
                        }

                        pRowResult[i] = (byte)bNew;
                        pRowResult[i + 1] = (byte)gNew;
                        pRowResult[i + 2] = (byte)rNew;
                        pRowResult[i + 3] = 255;
                    }
                }

                paddedTarget.UnlockBits(dataA);
                paddedSource.UnlockBits(dataB);
                resultBmp.UnlockBits(dataResult);

                return resultBmp;
            }
        }

        private Bitmap PerformNormalizedArithmetic(Bitmap source, Bitmap target, string operation)
        {
            int maxWidth = Math.Max(source.Width, target.Width);
            int maxHeight = Math.Max(source.Height, target.Height);

            int overlapWidth = Math.Min(source.Width, target.Width);
            int overlapHeight = Math.Min(source.Height, target.Height);

            if (overlapWidth == 0 || overlapHeight == 0)
            {
                return new Bitmap(target);
            }

            double[,,] rawResults = new double[overlapWidth, overlapHeight, 3];

            double minR = double.MaxValue, maxR = double.MinValue;
            double minG = double.MaxValue, maxG = double.MinValue;
            double minB = double.MaxValue, maxB = double.MinValue;

            using (Bitmap paddedTarget = ResizeAndPad(target, maxWidth, maxHeight))
            using (Bitmap paddedSource = ResizeAndPad(source, maxWidth, maxHeight))
            {
                BitmapData dataTarget = null;
                BitmapData dataSource = null;

                try
                {
                    dataTarget = paddedTarget.LockBits(new Rectangle(0, 0, maxWidth, maxHeight),
                        ImageLockMode.ReadOnly, paddedTarget.PixelFormat);
                    dataSource = paddedSource.LockBits(new Rectangle(0, 0, maxWidth, maxHeight),
                        ImageLockMode.ReadOnly, paddedSource.PixelFormat);

                    int bpp = 4;
                    int stride = dataTarget.Stride;

                    for (int y = 0; y < overlapHeight; y++)
                    {
                        byte* pRowTarget = (byte*)dataTarget.Scan0 + (y * stride);
                        byte* pRowSource = (byte*)dataSource.Scan0 + (y * stride);

                        for (int x = 0; x < overlapWidth; x++)
                        {
                            int i = x * bpp;

                            int b1 = pRowTarget[i];
                            int g1 = pRowTarget[i + 1];
                            int r1 = pRowTarget[i + 2];

                            int b2 = pRowSource[i];
                            int g2 = pRowSource[i + 1];
                            int r2 = pRowSource[i + 2];

                            double resR, resG, resB;

                            if (operation == "Multiply")
                            {
                                resR = (double)r1 * r2;
                                resG = (double)g1 * g2;
                                resB = (double)b1 * b2;
                            }
                            else
                            {
                                resR = (r2 == 0) ? (r1 * 255.0) : Math.Min(((double)r1 * 255.0) / r2, 255.0 * 255.0);
                                resG = (g2 == 0) ? (g1 * 255.0) : Math.Min(((double)g1 * 255.0) / g2, 255.0 * 255.0);
                                resB = (b2 == 0) ? (b1 * 255.0) : Math.Min(((double)b1 * 255.0) / b2, 255.0 * 255.0);
                            }

                            rawResults[x, y, 0] = resR;
                            rawResults[x, y, 1] = resG;
                            rawResults[x, y, 2] = resB;

                            if (resR < minR) minR = resR;
                            if (resR > maxR) maxR = resR;
                            if (resG < minG) minG = resG;
                            if (resG > maxG) maxG = resG;
                            if (resB < minB) minB = resB;
                            if (resB > maxB) maxB = resB;
                        }
                    }
                }
                finally
                {
                    if (dataTarget != null) paddedTarget.UnlockBits(dataTarget);
                    if (dataSource != null) paddedSource.UnlockBits(dataSource);
                }

                double rangeR = maxR - minR;
                double rangeG = maxG - minG;
                double rangeB = maxB - minB;

                Bitmap resultBmp = new Bitmap(maxWidth, maxHeight, PixelFormat.Format32bppArgb);

                BitmapData dataResult = null;
                BitmapData dataTargetRead = null;
                BitmapData dataSourceRead = null;

                try
                {
                    dataResult = resultBmp.LockBits(new Rectangle(0, 0, maxWidth, maxHeight),
                        ImageLockMode.WriteOnly, resultBmp.PixelFormat);
                    dataTargetRead = paddedTarget.LockBits(new Rectangle(0, 0, maxWidth, maxHeight),
                        ImageLockMode.ReadOnly, paddedTarget.PixelFormat);
                    dataSourceRead = paddedSource.LockBits(new Rectangle(0, 0, maxWidth, maxHeight),
                        ImageLockMode.ReadOnly, paddedSource.PixelFormat);

                    int bpp = 4;
                    int stride = dataResult.Stride;

                    for (int y = 0; y < maxHeight; y++)
                    {
                        byte* pRowResult = (byte*)dataResult.Scan0 + (y * stride);
                        byte* pRowTarget = (byte*)dataTargetRead.Scan0 + (y * stride);
                        byte* pRowSource = (byte*)dataSourceRead.Scan0 + (y * stride);

                        for (int x = 0; x < maxWidth; x++)
                        {
                            int i = x * bpp;

                            if (x < overlapWidth && y < overlapHeight)
                            {
                                double rawR = rawResults[x, y, 0];
                                double rawG = rawResults[x, y, 1];
                                double rawB = rawResults[x, y, 2];

                                byte newR = (byte)Math.Clamp((rangeR == 0) ? 0 : ((rawR - minR) / rangeR) * 255.0, 0,
                                    255);
                                byte newG = (byte)Math.Clamp((rangeG == 0) ? 0 : ((rawG - minG) / rangeG) * 255.0, 0,
                                    255);
                                byte newB = (byte)Math.Clamp((rangeB == 0) ? 0 : ((rawB - minB) / rangeB) * 255.0, 0,
                                    255);

                                pRowResult[i] = newB;
                                pRowResult[i + 1] = newG;
                                pRowResult[i + 2] = newR;
                                pRowResult[i + 3] = 255;
                            }
                            else if (x < target.Width && y < target.Height)
                            {
                                pRowResult[i] = pRowTarget[i];
                                pRowResult[i + 1] = pRowTarget[i + 1];
                                pRowResult[i + 2] = pRowTarget[i + 2];
                                pRowResult[i + 3] = pRowTarget[i + 3];
                            }
                            else if (x < source.Width && y < source.Height)
                            {
                                pRowResult[i] = pRowSource[i];
                                pRowResult[i + 1] = pRowSource[i + 1];
                                pRowResult[i + 2] = pRowSource[i + 2];
                                pRowResult[i + 3] = pRowSource[i + 3];
                            }
                            else
                            {
                                pRowResult[i] = 0;
                                pRowResult[i + 1] = 0;
                                pRowResult[i + 2] = 0;
                                pRowResult[i + 3] = 255;
                            }
                        }
                    }
                }
                finally
                {
                    if (dataResult != null) resultBmp.UnlockBits(dataResult);
                    if (dataTargetRead != null) paddedTarget.UnlockBits(dataTargetRead);
                    if (dataSourceRead != null) paddedSource.UnlockBits(dataSourceRead);
                }

                return resultBmp;
            }
        }

        private Bitmap PerformConstantArithmetic(Bitmap source, double constant, string operation)
        {
            int width = source.Width;
            int height = source.Height;

            Bitmap resultBmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            BitmapData dataSource = null;
            BitmapData dataResult = null;
            try
            {
                dataSource = source.LockBits(new Rectangle(0, 0, width, height),
                    ImageLockMode.ReadOnly, source.PixelFormat);
                dataResult = resultBmp.LockBits(new Rectangle(0, 0, width, height),
                    ImageLockMode.WriteOnly, resultBmp.PixelFormat);
                

                int bpp = Image.GetPixelFormatSize(source.PixelFormat) / 8;
                int resBpp = 4;
                int stride = dataSource.Stride;
                int resStride = dataResult.Stride;

                for (int y = 0; y < height; y++)
                {
                    byte* pRowSource = (byte*)dataSource.Scan0 + (y * stride);
                    byte* pRowResult = (byte*)dataResult.Scan0 + (y * resStride);
                    
                    for (int x = 0; x < width; x++)
                    {
                        int i = x * bpp;
                        int resI = x * resBpp;
                        
                        int b = pRowSource[i];
                        int g = pRowSource[i + 1];
                        int r = pRowSource[i + 2];

                        double resR, resG, resB;

                        if (operation == "Multiply")
                        {
                            resR = (double)r * constant;
                            resG = (double)g * constant;
                            resB = (double)b * constant;
                        }
                        else
                        {
                            double c = (constant == 0) ? 1.0 : constant;
                            resR = (double)r / c;
                            resG = (double)g / c;
                            resB = (double)b / c;
                        }

                        pRowResult[resI] = (byte)Math.Clamp(resB, 0, 255);
                        pRowResult[resI + 1] = (byte)Math.Clamp(resG, 0, 255);
                        pRowResult[resI + 2] = (byte)Math.Clamp(resR, 0, 255);
                        pRowResult[resI + 3] = 255;
                    }
                }
            }
            finally
            {
                if (dataSource != null) source.UnlockBits(dataSource);
                if (dataResult != null) resultBmp.UnlockBits(dataResult);
            }

            return resultBmp;
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

        public Bitmap MultiplyImages(Bitmap source, Bitmap target)
        {
            if (source == null || target == null) return null;
            return PerformNormalizedArithmetic(source, target, "Multiply");
        }

        public Bitmap DivideImages(Bitmap source, Bitmap target)
        {
            if (source == null || target == null) return null;
            return PerformNormalizedArithmetic(source, target, "Divide");
        }

        public Bitmap MultiplyByConstant(Bitmap source, double constant)
        {
            if (source == null) return null;
            return PerformConstantArithmetic(source, constant, "Multiply");
        }

        public Bitmap DivideByConstant(Bitmap source, double constant)
        {
            if (source == null) return null;

            if (Math.Abs(constant) < 0.0001)
            {
                MessageBox.Show("Tidak dapat membagi dengan konstanta nol.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return new Bitmap(source);
            }
            
            return PerformConstantArithmetic(source, constant, "Divide");       
        }


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
    }
}