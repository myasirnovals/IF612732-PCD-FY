using System.Drawing;
using System.Drawing.Imaging;
using System;
using System.Windows.Forms;

namespace MiniPhotoShop.Services
{
    public unsafe class ImageArithmeticService : IImageArithmeticService
    {
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
                        int b1 = pRowA[i], g1 = pRowA[i + 1], r1 = pRowA[i + 2];
                        int b2 = pRowB[i], g2 = pRowB[i + 1], r2 = pRowB[i + 2];
                        int rNew, gNew, bNew;

                        switch (operation)
                        {
                            case "Add":
                                rNew = Math.Min(255, r1 + r2); gNew = Math.Min(255, g1 + g2); bNew = Math.Min(255, b1 + b2);
                                break;
                            case "Subtract":
                                rNew = Math.Abs(r2 - r1); gNew = Math.Abs(g2 - g1); bNew = Math.Abs(b2 - b1);
                                break;
                            default: rNew = gNew = bNew = 0; break;
                        }
                        pRowResult[i] = (byte)bNew; pRowResult[i + 1] = (byte)gNew; pRowResult[i + 2] = (byte)rNew; pRowResult[i + 3] = 255;
                    }
                }
                paddedTarget.UnlockBits(dataA); paddedSource.UnlockBits(dataB); resultBmp.UnlockBits(dataResult);
                return resultBmp;
            }
        }

        private Bitmap PerformNormalizedArithmetic(Bitmap source, Bitmap target, string operation)
        {
            int maxWidth = Math.Max(source.Width, target.Width);
            int maxHeight = Math.Max(source.Height, target.Height);
            int overlapWidth = Math.Min(source.Width, target.Width);
            int overlapHeight = Math.Min(source.Height, target.Height);
            if (overlapWidth == 0 || overlapHeight == 0) return new Bitmap(target);

            double[,,] rawResults = new double[overlapWidth, overlapHeight, 3];
            double minR = double.MaxValue, maxR = double.MinValue, minG = double.MaxValue, maxG = double.MinValue, minB = double.MaxValue, maxB = double.MinValue;

            using (Bitmap paddedTarget = ResizeAndPad(target, maxWidth, maxHeight))
            using (Bitmap paddedSource = ResizeAndPad(source, maxWidth, maxHeight))
            {
                BitmapData dataTarget = paddedTarget.LockBits(new Rectangle(0, 0, maxWidth, maxHeight), ImageLockMode.ReadOnly, paddedTarget.PixelFormat);
                BitmapData dataSource = paddedSource.LockBits(new Rectangle(0, 0, maxWidth, maxHeight), ImageLockMode.ReadOnly, paddedSource.PixelFormat);
                int stride = dataTarget.Stride;

                for (int y = 0; y < overlapHeight; y++)
                {
                    byte* pRowTarget = (byte*)dataTarget.Scan0 + (y * stride);
                    byte* pRowSource = (byte*)dataSource.Scan0 + (y * stride);
                    for (int x = 0; x < overlapWidth; x++)
                    {
                        int i = x * 4;
                        int b1 = pRowTarget[i], g1 = pRowTarget[i + 1], r1 = pRowTarget[i + 2];
                        int b2 = pRowSource[i], g2 = pRowSource[i + 1], r2 = pRowSource[i + 2];
                        double resR, resG, resB;

                        if (operation == "Multiply")
                        {
                            resR = (double)r1 * r2; resG = (double)g1 * g2; resB = (double)b1 * b2;
                        }
                        else
                        {
                            resR = (r2 == 0) ? (r1 * 255.0) : Math.Min(((double)r1 * 255.0) / r2, 255.0 * 255.0);
                            resG = (g2 == 0) ? (g1 * 255.0) : Math.Min(((double)g1 * 255.0) / g2, 255.0 * 255.0);
                            resB = (b2 == 0) ? (b1 * 255.0) : Math.Min(((double)b1 * 255.0) / b2, 255.0 * 255.0);
                        }

                        rawResults[x, y, 0] = resR; rawResults[x, y, 1] = resG; rawResults[x, y, 2] = resB;
                        if (resR < minR) minR = resR; if (resR > maxR) maxR = resR;
                        if (resG < minG) minG = resG; if (resG > maxG) maxG = resG;
                        if (resB < minB) minB = resB; if (resB > maxB) maxB = resB;
                    }
                }
                paddedTarget.UnlockBits(dataTarget); paddedSource.UnlockBits(dataSource);
            }

            double rangeR = maxR - minR, rangeG = maxG - minG, rangeB = maxB - minB;
            Bitmap resultBmp = new Bitmap(maxWidth, maxHeight, PixelFormat.Format32bppArgb);
            BitmapData dataResult = resultBmp.LockBits(new Rectangle(0, 0, maxWidth, maxHeight), ImageLockMode.WriteOnly, resultBmp.PixelFormat);

            for (int y = 0; y < overlapHeight; y++)
            {
                byte* pRowResult = (byte*)dataResult.Scan0 + (y * dataResult.Stride);
                for (int x = 0; x < overlapWidth; x++)
                {
                    int i = x * 4;
                    byte newR = (byte)Math.Clamp((rangeR == 0) ? 0 : ((rawResults[x, y, 0] - minR) / rangeR) * 255.0, 0, 255);
                    byte newG = (byte)Math.Clamp((rangeG == 0) ? 0 : ((rawResults[x, y, 1] - minG) / rangeG) * 255.0, 0, 255);
                    byte newB = (byte)Math.Clamp((rangeB == 0) ? 0 : ((rawResults[x, y, 2] - minB) / rangeB) * 255.0, 0, 255);
                    pRowResult[i] = newB; pRowResult[i + 1] = newG; pRowResult[i + 2] = newR; pRowResult[i + 3] = 255;
                }
            }
            resultBmp.UnlockBits(dataResult);
            return resultBmp;
        }

        private Bitmap PerformConstantArithmetic(Bitmap source, double constant, string operation)
        {
            int width = source.Width;
            int height = source.Height;
            Bitmap resultBmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData dataSource = source.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, source.PixelFormat);
            BitmapData dataResult = resultBmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, resultBmp.PixelFormat);
            int bpp = Image.GetPixelFormatSize(source.PixelFormat) / 8;
            int resBpp = 4;

            for (int y = 0; y < height; y++)
            {
                byte* pRowSource = (byte*)dataSource.Scan0 + (y * dataSource.Stride);
                byte* pRowResult = (byte*)dataResult.Scan0 + (y * dataResult.Stride);
                for (int x = 0; x < width; x++)
                {
                    int i = x * bpp; int resI = x * resBpp;
                    int b = pRowSource[i], g = pRowSource[i + 1], r = pRowSource[i + 2];
                    double resR, resG, resB;

                    if (operation == "Multiply") { resR = r * constant; resG = g * constant; resB = b * constant; }
                    else { double c = (constant == 0) ? 1.0 : constant; resR = r / c; resG = g / c; resB = b / c; }

                    pRowResult[resI] = (byte)Math.Clamp(resB, 0, 255);
                    pRowResult[resI + 1] = (byte)Math.Clamp(resG, 0, 255);
                    pRowResult[resI + 2] = (byte)Math.Clamp(resR, 0, 255);
                    pRowResult[resI + 3] = 255;
                }
            }
            source.UnlockBits(dataSource); resultBmp.UnlockBits(dataResult);
            return resultBmp;
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
                int overlapWidth = Math.Min(source.Width, target.Width);
                int overlapHeight = Math.Min(source.Height, target.Height);
                int stride = dataA.Stride;

                for (int y = 0; y < overlapHeight; y++)
                {
                    byte* pRowA = (byte*)dataA.Scan0 + (y * stride);
                    byte* pRowB = (byte*)dataB.Scan0 + (y * stride);
                    byte* pRowResult = (byte*)dataResult.Scan0 + (y * stride);
                    for (int x = 0; x < overlapWidth; x++)
                    {
                        int i = x * 4;
                        int b1 = pRowA[i], g1 = pRowA[i + 1], r1 = pRowA[i + 2];
                        int b2 = pRowB[i], g2 = pRowB[i + 1], r2 = pRowB[i + 2];
                        int r_new = 0, g_new = 0, b_new = 0;

                        switch (operation)
                        {
                            case "AND": r_new = r1 & r2; g_new = g1 & g2; b_new = b1 & b2; break;
                            case "OR": r_new = r1 | r2; g_new = g1 | g2; b_new = b1 | b2; break;
                            case "XOR": r_new = r1 ^ r2; g_new = g1 ^ g2; b_new = b1 ^ b2; break;
                        }
                        pRowResult[i] = (byte)b_new; pRowResult[i + 1] = (byte)g_new; pRowResult[i + 2] = (byte)r_new; pRowResult[i + 3] = 255;
                    }
                }
                paddedTarget.UnlockBits(dataA); paddedSource.UnlockBits(dataB); resultBmp.UnlockBits(dataResult);
                return resultBmp;
            }
        }

        public Bitmap AddImages(Bitmap source, Bitmap target) => PerformArithmetic(source, target, "Add");
        public Bitmap SubtractImages(Bitmap source, Bitmap target) => PerformArithmetic(source, target, "Subtract");
        public Bitmap MultiplyImages(Bitmap source, Bitmap target) => PerformNormalizedArithmetic(source, target, "Multiply");
        public Bitmap DivideImages(Bitmap source, Bitmap target) => PerformNormalizedArithmetic(source, target, "Divide");
        public Bitmap MultiplyByConstant(Bitmap source, double constant) => PerformConstantArithmetic(source, constant, "Multiply");
        public Bitmap DivideByConstant(Bitmap source, double constant) => PerformConstantArithmetic(source, constant, "Divide");
        public Bitmap AndImages(Bitmap source, Bitmap target) => PerformBitwiseOperation(source, target, "AND");
        public Bitmap OrImages(Bitmap source, Bitmap target) => PerformBitwiseOperation(source, target, "OR");
        public Bitmap XorImages(Bitmap source, Bitmap target) => PerformBitwiseOperation(source, target, "XOR");
    }
}