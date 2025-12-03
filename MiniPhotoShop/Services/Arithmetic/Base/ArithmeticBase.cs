using System;
using System.Drawing;
using System.Drawing.Imaging;
using MiniPhotoShop.Services.Helpers;

namespace MiniPhotoShop.Services.Arithmetic.Base
{
    public abstract unsafe class TwoImageOperationBase
    {
        public Bitmap Process(Bitmap source, Bitmap target)
        {
            int maxWidth = Math.Max(source.Width, target.Width);
            int maxHeight = Math.Max(source.Height, target.Height);

            using (Bitmap paddedTarget = ImageHelper.ResizeAndPad(target, maxWidth, maxHeight))
            using (Bitmap paddedSource = ImageHelper.ResizeAndPad(source, maxWidth, maxHeight))
            {
                Bitmap resultBmp = new Bitmap(maxWidth, maxHeight, PixelFormat.Format32bppArgb);

                BitmapData dataA = paddedTarget.LockBits(new Rectangle(0, 0, maxWidth, maxHeight),
                    ImageLockMode.ReadOnly, paddedTarget.PixelFormat);
                BitmapData dataB = paddedSource.LockBits(new Rectangle(0, 0, maxWidth, maxHeight),
                    ImageLockMode.ReadOnly, paddedSource.PixelFormat);
                BitmapData dataResult = resultBmp.LockBits(new Rectangle(0, 0, maxWidth, maxHeight),
                    ImageLockMode.WriteOnly, resultBmp.PixelFormat);

                ProcessPixels((byte*)dataA.Scan0, (byte*)dataB.Scan0, (byte*)dataResult.Scan0, maxWidth, maxHeight,
                    dataA.Stride);

                paddedTarget.UnlockBits(dataA);
                paddedSource.UnlockBits(dataB);
                resultBmp.UnlockBits(dataResult);

                return resultBmp;
            }
        }

        protected abstract void ProcessPixels(byte* pA, byte* pB, byte* pResult, int width, int height, int stride);
    }
}