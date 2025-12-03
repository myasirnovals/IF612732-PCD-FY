using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace MiniPhotoShop.Services.Arithmetic.Constant
{
    public enum ConstantType
    {
        Multiply,
        Divide
    }

    public unsafe class ConstantOperation
    {
        private readonly ConstantType _type;
        private readonly double _constant;

        public ConstantOperation(ConstantType type, double constant)
        {
            _type = type;
            _constant = constant;
        }

        public Bitmap Process(Bitmap source)
        {
            if (source == null) return null;

            int width = source.Width;
            int height = source.Height;

            Bitmap resultBmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            BitmapData srcData = source.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly,
                source.PixelFormat);
            BitmapData resData = resultBmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly,
                resultBmp.PixelFormat);

            int bpp = Image.GetPixelFormatSize(source.PixelFormat) / 8;
            int resBpp = 4;

            byte* ptrSrc = (byte*)srcData.Scan0;
            byte* ptrRes = (byte*)resData.Scan0;

            for (int y = 0; y < height; y++)
            {
                byte* rowSrc = ptrSrc + (y * srcData.Stride);
                byte* rowRes = ptrRes + (y * resData.Stride);

                for (int x = 0; x < width; x++)
                {
                    int i = x * bpp;
                    int resI = x * resBpp;

                    int b = rowSrc[i];
                    int g = rowSrc[i + 1];
                    int r = rowSrc[i + 2];

                    double resR, resG, resB;

                    if (_type == ConstantType.Multiply)
                    {
                        resR = r * _constant;
                        resG = g * _constant;
                        resB = b * _constant;
                    }
                    else
                    {
                        double c = (_constant == 0) ? 1.0 : _constant;
                        resR = r / c;
                        resG = g / c;
                        resB = b / c;
                    }

                    rowRes[resI] = (byte)Math.Clamp(resB, 0, 255);
                    rowRes[resI + 1] = (byte)Math.Clamp(resG, 0, 255);
                    rowRes[resI + 2] = (byte)Math.Clamp(resR, 0, 255);
                    rowRes[resI + 3] = 255;
                }
            }

            source.UnlockBits(srcData);
            resultBmp.UnlockBits(resData);

            return resultBmp;
        }
    }
}