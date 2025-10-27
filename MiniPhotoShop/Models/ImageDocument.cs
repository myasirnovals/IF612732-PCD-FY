using System.Drawing;
using MiniPhotoShop.Filters;

namespace MiniPhotoShop.Models
{
    public class ImageDocument
    {
        private readonly IImageProcessingService _processor;

        public string Name { get; private set; }
        public Bitmap OriginalBitmap { get; private set; }
        public Bitmap CurrentBitmap { get; private set; }
        public int[,,] PixelArray { get; private set; }
        public HistogramData Histogram { get; private set; }
        public bool IsGrayscale { get; private set; }
        public bool IsBlackAndWhite { get; set; }

        public ImageDocument(Bitmap originalImage, string name, IImageProcessingService processor)
        {
            _processor = processor;
            Name = name;
            OriginalBitmap = originalImage;

            Restore();
        }

        public void ApplyFilter(IImageFilter filter)
        {
            CurrentBitmap = _processor.CreateBitmapFromPixelArray(PixelArray, filter);

            IsGrayscale = filter is GrayscaleFilter;
            IsBlackAndWhite = filter is ThresholdFilter;
        }

        public void Restore()
        {
            CurrentBitmap = new Bitmap(OriginalBitmap);
            PixelArray = _processor.CreatePixelArray(CurrentBitmap);
            Histogram = _processor.CalculateHistogram(PixelArray);
            IsGrayscale = false;
            IsBlackAndWhite = false;
        }
    }

    public class HistogramData
    {
        public int[] RedCounts { get; set; } = new int[256];
        public int[] GreenCounts { get; set; } = new int[256];
        public int[] BlueCounts { get; set; } = new int[256];
        public int[] GrayCounts { get; set; } = new int[256];
        public int MaxCount { get; set; } = 1;
    }
}