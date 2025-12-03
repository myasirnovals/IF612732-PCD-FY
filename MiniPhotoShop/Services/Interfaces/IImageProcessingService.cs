using System.Drawing;
using MiniPhotoShop.Filters;
using MiniPhotoShop.Models;

namespace MiniPhotoShop.Services.Interfaces
{
    public interface IImageProcessingService : IPixelService, IHistogramService
    {
        Bitmap TranslateImage(Bitmap source, int xOffset, int yOffset);
        Bitmap DistortImage(Bitmap source, double amplitude, double frequency);
        Bitmap RotateImage(Bitmap source, float angle);
        Bitmap ScaleImage(Bitmap source, double scaleFactor);
    }
    
    public interface IPixelService
    {
        int[,,] CreatePixelArray(Bitmap bmp);
        Bitmap CreateBitmapFromPixelArray(Bitmap sourcedBitmap, IImageFilter filter);
    }
    
    public interface IHistogramService
    {
        HistogramData CalculateHistogram(int[,,] pixelArray);
        Bitmap DrawHistogram(int width, int height, int[] counts, int maxCount, Color barColor);
    }
}