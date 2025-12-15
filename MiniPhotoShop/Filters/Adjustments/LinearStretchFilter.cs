using System;
using System.Drawing;
using System.Drawing.Imaging;
using MiniPhotoShop.Filters.Helpers;

namespace MiniPhotoShop.Filters.Adjustments
{
    public unsafe class LinearStretchFilter
    {
        public Bitmap Apply(Bitmap original)
        {
            if (original == null) return null;

            int width = original.Width;
            int height = original.Height;
            Bitmap result = new Bitmap(width, height, original.PixelFormat);

            BitmapData srcData = original.LockBits(new Rectangle(0, 0, width, height), 
                ImageLockMode.ReadOnly, original.PixelFormat);
            BitmapData resData = result.LockBits(new Rectangle(0, 0, width, height), 
                ImageLockMode.WriteOnly, result.PixelFormat);

            int bytesPerPixel = Image.GetPixelFormatSize(original.PixelFormat) / 8;
            int stride = srcData.Stride;
            byte* srcPtr = (byte*)srcData.Scan0;
            byte* resPtr = (byte*)resData.Scan0;
            
            byte minR = 255, maxR = 0;
            byte minG = 255, maxG = 0;
            byte minB = 255, maxB = 0;
            
            for (int y = 0; y < height; y++)
            {
                byte* row = srcPtr + (y * stride);
                for (int x = 0; x < width; x++)
                {
                    int i = x * bytesPerPixel;
                    
                    byte b = row[i];
                    byte g = row[i + 1];
                    byte r = row[i + 2];

                    if (b < minB) minB = b; if (b > maxB) maxB = b;
                    if (g < minG) minG = g; if (g > maxG) maxG = g;
                    if (r < minR) minR = r; if (r > maxR) maxR = r;
                }
            }
            
            byte[] lutB = FilterHelper.CalculateLinearStretchLUT(minB, maxB);
            byte[] lutG = FilterHelper.CalculateLinearStretchLUT(minG, maxG);
            byte[] lutR = FilterHelper.CalculateLinearStretchLUT(minR, maxR);
            
            for (int y = 0; y < height; y++)
            {
                byte* srcRow = srcPtr + (y * stride);
                byte* resRow = resPtr + (y * stride);

                for (int x = 0; x < width; x++)
                {
                    int i = x * bytesPerPixel;

                    resRow[i]     = lutB[srcRow[i]];     
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