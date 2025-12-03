using MiniPhotoShop.Filters.Base;
using MiniPhotoShop.Services;
using MiniPhotoShop.Services.Interfaces;
using System;
using System.Drawing;

namespace MiniPhotoShop.Models
{
    public enum ColorRanges { None, Red, Green, Blue, Cyan, Magenta, Yellow, Orange, Purple, Gray }

    public class ImageDocument
    {
        public Bitmap OriginalBitmap { get; private set; }
        public Bitmap CurrentBitmap { get; set; }
        public string Name { get; set; }
        public HistogramData Histogram { get; set; }
        public int[,,] PixelArray { get; set; }

        public bool IsGrayscale { get; set; }
        public bool IsBlackAndWhite { get; set; }
        public bool IsInSelectionMode { get; set; }
        public ColorRanges SelectedColorRange { get; set; } = ColorRanges.None;

        private readonly IImageProcessingService _processor;

        public ImageDocument(Bitmap image, string name, IImageProcessingService processor)
        {
            OriginalBitmap = new Bitmap(image);
            CurrentBitmap = new Bitmap(image);
            Name = name;
            _processor = processor;

            if (_processor != null)
            {
                PixelArray = _processor.CreatePixelArray(CurrentBitmap);
                Histogram = _processor.CalculateHistogram(PixelArray);
            }
        }

        public void ApplyFilter(IImageFilter filter)
        {
            if (_processor == null) return;

            Bitmap result = _processor.CreateBitmapFromPixelArray(OriginalBitmap, filter);

            if (CurrentBitmap != OriginalBitmap) CurrentBitmap.Dispose();
            CurrentBitmap = result;

            PixelArray = _processor.CreatePixelArray(CurrentBitmap);
            Histogram = _processor.CalculateHistogram(PixelArray);
        }

        public void Restore()
        {
            if (CurrentBitmap != OriginalBitmap) CurrentBitmap.Dispose();
            CurrentBitmap = new Bitmap(OriginalBitmap);

            IsGrayscale = false;
            IsBlackAndWhite = false;
            IsInSelectionMode = false;
            SelectedColorRange = ColorRanges.None;

            if (_processor != null)
            {
                PixelArray = _processor.CreatePixelArray(CurrentBitmap);
                Histogram = _processor.CalculateHistogram(PixelArray);
            }
        }
    }
}