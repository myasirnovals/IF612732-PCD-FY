using System;
using System.Drawing;
using System.Drawing.Imaging;
using MiniPhotoShop.Filters.Helpers;

namespace MiniPhotoShop.Filters.Adjustments
{
    public unsafe class HistogramEqualizationFilter
    {
        public Bitmap Apply(Bitmap original)
        {
            if (original == null) return null;

            int width = original.Width;
            int height = original.Height;
            int totalPixels = width * height;

            Bitmap result = new Bitmap(width, height, original.PixelFormat);

            BitmapData srcData = original.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly, original.PixelFormat);
            BitmapData resData = result.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly, result.PixelFormat);

            int bytesPerPixel = Image.GetPixelFormatSize(original.PixelFormat) / 8;
            int stride = srcData.Stride;
            byte* srcPtr = (byte*)srcData.Scan0;
            byte* resPtr = (byte*)resData.Scan0;

            int[] histB = new int[256];
            int[] histG = new int[256];
            int[] histR = new int[256];

            for (int y = 0; y < height; y++)
            {
                byte* row = srcPtr + (y * stride);
                for (int x = 0; x < width; x++)
                {
                    int i = x * bytesPerPixel;
                    histB[row[i]]++;
                    histG[row[i + 1]]++;
                    histR[row[i + 2]]++;
                }
            }

            byte[] lutB = FilterHelper.CalculateEqualizationLUT(histB, totalPixels);
            byte[] lutG = FilterHelper.CalculateEqualizationLUT(histG, totalPixels);
            byte[] lutR = FilterHelper.CalculateEqualizationLUT(histR, totalPixels);

            for (int y = 0; y < height; y++)
            {
                byte* srcRow = srcPtr + (y * stride);
                byte* resRow = resPtr + (y * stride);

                for (int x = 0; x < width; x++)
                {
                    int i = x * bytesPerPixel;

                    resRow[i] = lutB[srcRow[i]];
                    resRow[i + 1] = lutG[srcRow[i + 1]];
                    resRow[i + 2] = lutR[srcRow[i + 2]];

                    if (bytesPerPixel == 4)
                        resRow[i + 3] = srcRow[i + 3];
                }
            }

            original.UnlockBits(srcData);
            result.UnlockBits(resData);

            return result;
        }
    }
}