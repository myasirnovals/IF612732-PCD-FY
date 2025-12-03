using System.Drawing;
using System.Drawing.Imaging;

namespace MiniPhotoShop.Services.Processing.Transformations
{
    public unsafe class TranslateTransform
    {
        public Bitmap Process(Bitmap source, int xOffset, int yOffset)
        {
            if (source == null) return null;
            int width = source.Width;
            int height = source.Height;
            
            Bitmap resultBmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData srcData = source.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, source.PixelFormat);
            BitmapData resData = resultBmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, resultBmp.PixelFormat);
            
            int bytesPerPixel = 4;
            int stride = srcData.Stride;
            byte* srcPtr = (byte*)srcData.Scan0;
            byte* resPtr = (byte*)resData.Scan0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int srcX = x - xOffset;
                    int srcY = y - yOffset;
                    int resIndex = (y * stride) + (x * bytesPerPixel);

                    if (srcX >= 0 && srcX < width && srcY >= 0 && srcY < height)
                    {
                        int srcIndex = (srcY * stride) + (srcX * bytesPerPixel);
                        resPtr[resIndex] = srcPtr[srcIndex];
                        resPtr[resIndex + 1] = srcPtr[srcIndex + 1];
                        resPtr[resIndex + 2] = srcPtr[srcIndex + 2];
                        resPtr[resIndex + 3] = srcPtr[srcIndex + 3];
                    }
                    else
                    {
                        resPtr[resIndex] = 0; resPtr[resIndex + 1] = 0; 
                        resPtr[resIndex + 2] = 0; resPtr[resIndex + 3] = 255;
                    }
                }
            }
            source.UnlockBits(srcData);
            resultBmp.UnlockBits(resData);
            return resultBmp;
        }
    }
}