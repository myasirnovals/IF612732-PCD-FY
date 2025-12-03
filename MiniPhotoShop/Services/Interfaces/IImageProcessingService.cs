using System.Drawing;
using MiniPhotoShop.Filters.Base;
using MiniPhotoShop.Models;

namespace MiniPhotoShop.Services.Interfaces
{
    public interface IImageProcessingService
    {
        int[,,] CreatePixelArray(Bitmap bmp);
        Bitmap CreateBitmapFromPixelArray(Bitmap sourcedBitmap, IImageFilter filter);
        HistogramData CalculateHistogram(int[,,] pixelArray);
        Bitmap DrawHistogram(int width, int height, int[] counts, int maxCount, Color barColor);
        Bitmap TranslateImage(Bitmap source, int xOffset, int yOffset);
        Bitmap RotateImage(Bitmap source, float angle);
        Bitmap ScaleImage(Bitmap source, double scaleFactor);
        Bitmap DistortImage(Bitmap source, double amplitude, double frequency);
    }
}