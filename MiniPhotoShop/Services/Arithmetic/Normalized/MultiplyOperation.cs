using System;
using System.Drawing;
using System.Drawing.Imaging;
using MiniPhotoShop.Services.Helpers;

namespace MiniPhotoShop.Services.Arithmetic.Normalized
{
    public unsafe class MultiplyOperation
    {
        public Bitmap Process(Bitmap source, Bitmap target)
        {
            int width = Math.Max(source.Width, target.Width);
            int height = Math.Max(source.Height, target.Height);

            double minR = double.MaxValue, maxR = double.MinValue;
            double minG = double.MaxValue, maxG = double.MinValue;
            double minB = double.MaxValue, maxB = double.MinValue;

            double[,,] rawResults = new double[width, height, 3];

            using (Bitmap paddedTarget = ImageHelper.ResizeAndPad(target, width, height))
            using (Bitmap paddedSource = ImageHelper.ResizeAndPad(source, width, height))
            {
                BitmapData dataA = paddedTarget.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly,
                    paddedTarget.PixelFormat);
                BitmapData dataB = paddedSource.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly,
                    paddedSource.PixelFormat);
                int stride = dataA.Stride;

                byte* pA = (byte*)dataA.Scan0;
                byte* pB = (byte*)dataB.Scan0;

                for (int y = 0; y < height; y++)
                {
                    byte* rowA = pA + (y * stride);
                    byte* rowB = pB + (y * stride);
                    for (int x = 0; x < width; x++)
                    {
                        int i = x * 4;
                        double r = (double)rowA[i + 2] * rowB[i + 2];
                        double g = (double)rowA[i + 1] * rowB[i + 1];
                        double b = (double)rowA[i] * rowB[i];

                        rawResults[x, y, 0] = r;
                        rawResults[x, y, 1] = g;
                        rawResults[x, y, 2] = b;

                        if (r < minR) minR = r;
                        if (r > maxR) maxR = r;
                        if (g < minG) minG = g;
                        if (g > maxG) maxG = g;
                        if (b < minB) minB = b;
                        if (b > maxB) maxB = b;
                    }
                }

                paddedTarget.UnlockBits(dataA);
                paddedSource.UnlockBits(dataB);
            }

            Bitmap resultBmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData resData = resultBmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly,
                resultBmp.PixelFormat);

            double rangeR = maxR - minR;
            double rangeG = maxG - minG;
            double rangeB = maxB - minB;

            byte* pRes = (byte*)resData.Scan0;
            for (int y = 0; y < height; y++)
            {
                byte* rowRes = pRes + (y * resData.Stride);
                for (int x = 0; x < width; x++)
                {
                    int i = x * 4;
                    rowRes[i + 2] =
                        (byte)Math.Clamp((rangeR == 0) ? 0 : ((rawResults[x, y, 0] - minR) / rangeR) * 255.0, 0, 255);
                    rowRes[i + 1] =
                        (byte)Math.Clamp((rangeG == 0) ? 0 : ((rawResults[x, y, 1] - minG) / rangeG) * 255.0, 0, 255);
                    rowRes[i] = (byte)Math.Clamp((rangeB == 0) ? 0 : ((rawResults[x, y, 2] - minB) / rangeB) * 255.0, 0,
                        255);
                    rowRes[i + 3] = 255;
                }
            }

            resultBmp.UnlockBits(resData);
            return resultBmp;
        }
    }
}