using System;
using MiniPhotoShop.Services.Arithmetic.Base;

namespace MiniPhotoShop.Services.Arithmetic.Basic
{
    public unsafe class AddOperation : TwoImageOperationBase
    {
        protected override void ProcessPixels(byte* pA, byte* pB, byte* pResult, int width, int height, int stride)
        {
            for (int y = 0; y < height; y++)
            {
                byte* rowA = pA + (y * stride);
                byte* rowB = pB + (y * stride);
                byte* rowRes = pResult + (y * stride);

                for (int x = 0; x < width; x++)
                {
                    int i = x * 4;

                    rowRes[i] = (byte)Math.Min(255, rowA[i] + rowB[i]);
                    rowRes[i + 1] = (byte)Math.Min(255, rowA[i + 1] + rowB[i + 1]);
                    rowRes[i + 2] = (byte)Math.Min(255, rowA[i + 2] + rowB[i + 2]);
                    rowRes[i + 3] = 255;
                }
            }
        }
    }
}