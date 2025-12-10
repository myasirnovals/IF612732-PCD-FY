using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace MiniPhotoShop.Filters.Helpers
{
    public static class FilterHelper
    {
        public static Bitmap CalculateMagnitude(Bitmap bmpX, Bitmap bmpY)
        {
            if (bmpX == null || bmpY == null) return null;
            if (bmpX.Width != bmpY.Width || bmpX.Height != bmpY.Height) return null;

            int width = bmpX.Width;
            int height = bmpX.Height;
            Bitmap result = new Bitmap(width, height);

            BitmapData dataX = bmpX.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);
            BitmapData dataY = bmpY.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);
            BitmapData dataRes = result.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly,
                PixelFormat.Format24bppRgb);

            int bytes = Math.Abs(dataX.Stride) * height;
            byte[] bufferX = new byte[bytes];
            byte[] bufferY = new byte[bytes];
            byte[] bufferRes = new byte[bytes];

            Marshal.Copy(dataX.Scan0, bufferX, 0, bytes);
            Marshal.Copy(dataY.Scan0, bufferY, 0, bytes);

            for (int i = 0; i < bytes; i++)
            {
                double val = Math.Sqrt((bufferX[i] * bufferX[i]) + (bufferY[i] * bufferY[i]));
                if (val > 255) val = 255;
                if (val < 0) val = 0;
                bufferRes[i] = (byte)val;
            }

            Marshal.Copy(bufferRes, 0, dataRes.Scan0, bytes);

            bmpX.UnlockBits(dataX);
            bmpY.UnlockBits(dataY);
            result.UnlockBits(dataRes);

            return result;
        }

        public static Bitmap ApplyThreshold(Bitmap source, int threshold)
        {
            if (source == null) return null;

            int width = source.Width;
            int height = source.Height;
            Bitmap result = new Bitmap(width, height);

            BitmapData srcData = source.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);
            BitmapData resData = result.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly,
                PixelFormat.Format24bppRgb);

            int bytes = Math.Abs(srcData.Stride) * height;
            byte[] bufferSrc = new byte[bytes];
            byte[] bufferRes = new byte[bytes];

            Marshal.Copy(srcData.Scan0, bufferSrc, 0, bytes);

            for (int i = 0; i < bytes; i += 3)
            {
                byte val = bufferSrc[i];
                byte binary = (val > threshold) ? (byte)255 : (byte)0;

                bufferRes[i] = binary;
                bufferRes[i + 1] = binary;
                bufferRes[i + 2] = binary;
            }

            Marshal.Copy(bufferRes, 0, resData.Scan0, bytes);
            source.UnlockBits(srcData);
            result.UnlockBits(resData);

            return result;
        }
    }
}