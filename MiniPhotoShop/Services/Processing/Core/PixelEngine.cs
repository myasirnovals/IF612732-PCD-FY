using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using MiniPhotoShop.Filters.Base;

namespace MiniPhotoShop.Services.Processing.Core
{
    public unsafe class PixelEngine
    {
        public int[,,] CreatePixelArray(Bitmap bmp)
        {
            if (bmp == null) return null;
            int width = bmp.Width;
            int height = bmp.Height;
            int[,,] pixelArray = new int[width, height, 4];

            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, bmp.PixelFormat);
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
            BitmapData sourceData = sourcedBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, sourcedBitmap.PixelFormat);
            BitmapData resultData = resultBmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, resultBmp.PixelFormat);

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

        public Bitmap ApplyConvolution(Bitmap sourceBitmap, double[,] kernel, double factor = 1.0, int bias = 0)
        {
            if (sourceBitmap == null) return null;

            int width = sourceBitmap.Width;
            int height = sourceBitmap.Height;

            Bitmap resultBitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            BitmapData srcData = sourceBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData resData = resultBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * height;
            byte[] pixelBuffer = new byte[bytes];
            byte[] resultBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, pixelBuffer, 0, bytes);
            sourceBitmap.UnlockBits(srcData);

            int filterWidth = kernel.GetLength(1);
            int filterHeight = kernel.GetLength(0);
            int filterOffset = (filterWidth - 1) / 2;
            int stride = srcData.Stride;

            for (int offsetY = filterOffset; offsetY < height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX < width - filterOffset; offsetX++)
                {
                    double blue = 0;
                    double green = 0;
                    double red = 0;

                    int byteOffset = offsetY * stride + offsetX * 4;

                    for (int filterY = -filterOffset; filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                        {
                            int calcOffset = byteOffset + (filterX * 4) + (filterY * stride);

                            blue += (double)(pixelBuffer[calcOffset]) * kernel[filterY + filterOffset, filterX + filterOffset];
                            green += (double)(pixelBuffer[calcOffset + 1]) * kernel[filterY + filterOffset, filterX + filterOffset];
                            red += (double)(pixelBuffer[calcOffset + 2]) * kernel[filterY + filterOffset, filterX + filterOffset];
                        }
                    }

                    blue = factor * blue + bias;
                    green = factor * green + bias;
                    red = factor * red + bias;

                    resultBuffer[byteOffset] = (byte)(blue > 255 ? 255 : (blue < 0 ? 0 : blue));
                    resultBuffer[byteOffset + 1] = (byte)(green > 255 ? 255 : (green < 0 ? 0 : green));
                    resultBuffer[byteOffset + 2] = (byte)(red > 255 ? 255 : (red < 0 ? 0 : red));
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            Marshal.Copy(resultBuffer, 0, resData.Scan0, bytes);
            resultBitmap.UnlockBits(resData);

            return resultBitmap;
        }
    }
}