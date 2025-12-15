using System;
using System.Drawing;
using System.Drawing.Imaging;
using MiniPhotoShop.Filters.Helpers;

namespace MiniPhotoShop.Filters.Adjustments
{
    public unsafe class AdaptiveHistogramEqualizationFilter
    {
        private const int GridSizeX = 8;
        private const int GridSizeY = 8;

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
            
            int blockWidth = width / GridSizeX;
            int blockHeight = height / GridSizeY;
            if (blockWidth == 0) blockWidth = 1;
            if (blockHeight == 0) blockHeight = 1;

            byte[,,,] gridLuts = new byte[GridSizeY, GridSizeX, 3, 256];

            for (int by = 0; by < GridSizeY; by++)
            {
                for (int bx = 0; bx < GridSizeX; bx++)
                {
                    int startX = bx * blockWidth;
                    int startY = by * blockHeight;
                    int endX = (bx == GridSizeX - 1) ? width : (startX + blockWidth);
                    int endY = (by == GridSizeY - 1) ? height : (startY + blockHeight);
                    
                    int[] histB = new int[256];
                    int[] histG = new int[256];
                    int[] histR = new int[256];
                    int totalPixels = 0;

                    for (int y = startY; y < endY; y++)
                    {
                        byte* row = srcPtr + (y * stride);
                        for (int x = startX; x < endX; x++)
                        {
                            int i = x * bytesPerPixel;
                            histB[row[i]]++;
                            histG[row[i + 1]]++;
                            histR[row[i + 2]]++;
                            totalPixels++;
                        }
                    }
                    
                    byte[] lutB = FilterHelper.CalculateEqualizationLUT(histB, totalPixels);
                    byte[] lutG = FilterHelper.CalculateEqualizationLUT(histG, totalPixels);
                    byte[] lutR = FilterHelper.CalculateEqualizationLUT(histR, totalPixels);
                    
                    for (int k = 0; k < 256; k++)
                    {
                        gridLuts[by, bx, 0, k] = lutB[k]; 
                        gridLuts[by, bx, 1, k] = lutG[k]; 
                        gridLuts[by, bx, 2, k] = lutR[k]; 
                    }
                }
            }
            
            for (int y = 0; y < height; y++)
            {
                byte* srcRow = srcPtr + (y * stride);
                byte* resRow = resPtr + (y * stride);
                
                float ty = (y - blockHeight / 2.0f) / blockHeight;
                int iy = (int)Math.Floor(ty); 
                float beta = ty - iy;         
                
                int y0 = Math.Clamp(iy, 0, GridSizeY - 1);
                int y1 = Math.Clamp(iy + 1, 0, GridSizeY - 1);

                for (int x = 0; x < width; x++)
                {
                    float tx = (x - blockWidth / 2.0f) / blockWidth;
                    int ix = (int)Math.Floor(tx); 
                    float alpha = tx - ix;        

                    int x0 = Math.Clamp(ix, 0, GridSizeX - 1);
                    int x1 = Math.Clamp(ix + 1, 0, GridSizeX - 1);

                    int i = x * bytesPerPixel;
                    
                    byte b = srcRow[i];
                    resRow[i] = BilinearInterpolate(b, gridLuts, x0, x1, y0, y1, 0, alpha, beta);
                    
                    byte g = srcRow[i + 1];
                    resRow[i + 1] = BilinearInterpolate(g, gridLuts, x0, x1, y0, y1, 1, alpha, beta);
                    
                    byte r = srcRow[i + 2];
                    resRow[i + 2] = BilinearInterpolate(r, gridLuts, x0, x1, y0, y1, 2, alpha, beta);
                    
                    if (bytesPerPixel == 4)
                        resRow[i + 3] = srcRow[i + 3];
                }
            }

            original.UnlockBits(srcData);
            result.UnlockBits(resData);

            return result;
        }
        
        private byte BilinearInterpolate(byte val, byte[,,,] grid, int x0, int x1, int y0, int y1, int channel, float alpha, float beta)
        {
            float lutUL = grid[y0, x0, channel, val]; 
            float lutUR = grid[y0, x1, channel, val]; 
            float lutBL = grid[y1, x0, channel, val]; 
            float lutBR = grid[y1, x1, channel, val]; 
            
            float top = (1 - alpha) * lutUL + alpha * lutUR;
            float bot = (1 - alpha) * lutBL + alpha * lutBR;
            
            float result = (1 - beta) * top + beta * bot;

            return (byte)Math.Clamp(result, 0, 255);
        }
    }
}