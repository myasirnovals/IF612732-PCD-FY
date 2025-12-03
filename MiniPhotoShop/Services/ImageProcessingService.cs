using System.Drawing;
using MiniPhotoShop.Filters;
using MiniPhotoShop.Models;
using MiniPhotoShop.Services.Processing.Analysis;
using MiniPhotoShop.Services.Processing.Core;
using MiniPhotoShop.Services.Processing.Transformations;

namespace MiniPhotoShop.Services
{
    public class ImageProcessingService : IImageProcessingService
    {
        public int[,,] CreatePixelArray(Bitmap bmp) 
            => new PixelEngine().CreatePixelArray(bmp);

        public Bitmap CreateBitmapFromPixelArray(Bitmap sourcedBitmap, IImageFilter filter) 
            => new PixelEngine().CreateBitmapFromPixelArray(sourcedBitmap, filter);
        
        public HistogramData CalculateHistogram(int[,,] pixelArray) 
            => new HistogramCalculator().Calculate(pixelArray);

        public Bitmap DrawHistogram(int width, int height, int[] counts, int maxCount, Color barColor) 
            => new HistogramCalculator().Draw(width, height, counts, maxCount, barColor);

        
        public Bitmap TranslateImage(Bitmap source, int xOffset, int yOffset) 
            => new TranslateTransform().Process(source, xOffset, yOffset);

        public Bitmap RotateImage(Bitmap source, float angle) 
            => new RotateTransform().Process(source, angle);

        public Bitmap ScaleImage(Bitmap source, double scaleFactor) 
            => new ScaleTransform().Process(source, scaleFactor);

        public Bitmap DistortImage(Bitmap source, double amplitude, double frequency) 
            => new DistortTransform().Process(source, amplitude, frequency);
    }
}