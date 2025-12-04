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
            
            double kernelTotal = 0;
            for (int i = 0; i < kernelWidth; i++)
            {
                for (int j = 0; j < kernelHeight; j++)
                {
                    kernelTotal += kernel[j, i];
                }
            }
            
            if (Math.Abs(kernelTotal) < 0.00001) kernelTotal = 1.0;
            
            for (int y = 0; y < height; y++)
            {
                byte* resRow = resPtr + (y * stride);
                
                for (int x = 0; x < width; x++)
                {
                    double sumR = 0, sumG = 0, sumB = 0;
                    
                    for (int ky = -radiusY; ky <= radiusY; ky++)
                    {
                        int sampleY = y + ky;
                        if (sampleY < 0) sampleY = 0;
                        else if (sampleY >= height) sampleY = height - 1;

                        byte* neighborRow = srcPtr + (sampleY * stride);

                        for (int kx = -radiusX; kx <= radiusX; kx++)
                        {
                            int sampleX = x + kx;
                            if (sampleX < 0) sampleX = 0;
                            else if (sampleX >= width) sampleX = width - 1;

                            int pixelOffset = sampleX * bytesPerPixel;
                            
                            double weight = kernel[ky + radiusY, kx + radiusX];

                            sumB += neighborRow[pixelOffset] * weight;     
                            sumG += neighborRow[pixelOffset + 1] * weight; 
                            sumR += neighborRow[pixelOffset + 2] * weight; 
                        }
                    }
                    
                    sumB /= kernelTotal;
                    sumG /= kernelTotal;
                    sumR /= kernelTotal;
                    
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