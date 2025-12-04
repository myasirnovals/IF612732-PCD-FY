using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace MiniPhotoShop.Services.Processing.Core
{
    public unsafe class ConvolutionProcessor
    {
        public Bitmap Process(Bitmap source, double[,] kernel)
        {
            if (source == null) return null;

            int width = source.Width;
            int height = source.Height;
            Bitmap resultBmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            
            BitmapData srcData = source.LockBits(new Rectangle(0, 0, width, height), 
                ImageLockMode.ReadOnly, source.PixelFormat);
            BitmapData resData = resultBmp.LockBits(new Rectangle(0, 0, width, height), 
                ImageLockMode.WriteOnly, resultBmp.PixelFormat);

            int bytesPerPixel = 4;
            int stride = srcData.Stride;
            byte* srcPtr = (byte*)srcData.Scan0;
            byte* resPtr = (byte*)resData.Scan0;

            int kernelWidth = kernel.GetLength(0);
            int kernelHeight = kernel.GetLength(1);
            int radiusX = kernelWidth / 2;
            int radiusY = kernelHeight / 2;
            
            for (int y = radiusY; y < height - radiusY; y++)
            {
                byte* resRow = resPtr + (y * stride);
                
                for (int x = radiusX; x < width - radiusX; x++)
                {
                    double sumR = 0, sumG = 0, sumB = 0;
                    
                    for (int ky = -radiusY; ky <= radiusY; ky++)
                    {
                        byte* neighborRow = srcPtr + ((y + ky) * stride);
                        for (int kx = -radiusX; kx <= radiusX; kx++)
                        {
                            int pixelOffset = (x + kx) * bytesPerPixel;
                            double weight = kernel[ky + radiusY, kx + radiusX];

                            sumB += neighborRow[pixelOffset] * weight;     
                            sumG += neighborRow[pixelOffset + 1] * weight; 
                            sumR += neighborRow[pixelOffset + 2] * weight; 
                        }
                    }
                    
                    int resOffset = x * bytesPerPixel;
                    resRow[resOffset] = (byte)Math.Clamp(sumB, 0, 255);     
                    resRow[resOffset + 1] = (byte)Math.Clamp(sumG, 0, 255); 
                    resRow[resOffset + 2] = (byte)Math.Clamp(sumR, 0, 255); 
                    resRow[resOffset + 3] = 255;
                }
            }

            source.UnlockBits(srcData);
            resultBmp.UnlockBits(resData);

            return resultBmp;
        }
    }
}