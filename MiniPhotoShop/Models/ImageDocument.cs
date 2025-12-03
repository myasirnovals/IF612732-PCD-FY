using System;
using System.Drawing;
using MiniPhotoShop.Filters.Helpers;

namespace MiniPhotoShop.Models
{
    public class ImageDocument : IDisposable
    {
        public string Name { get; set; }
        public Bitmap OriginalBitmap { get; private set; }
        public Bitmap CurrentBitmap { get; set; }
        public int[,,] PixelArray { get; set; }
        public HistogramData Histogram { get; set; }
        public bool IsGrayscale { get; set; }
        public bool IsBlackAndWhite { get; set; }
        public bool IsInSelectionMode { get; set; }
        public ColorRanges SelectedColorRange { get; set; }

        public ImageDocument(Bitmap originalImage, string name)
        {
            Name = name;

            OriginalBitmap = new Bitmap(originalImage);
            CurrentBitmap = new Bitmap(originalImage);

            IsGrayscale = false;
            IsBlackAndWhite = false;
            IsInSelectionMode = false;
            SelectedColorRange = ColorRanges.None;
        }

        public void Dispose()
        {
            OriginalBitmap?.Dispose();
            CurrentBitmap?.Dispose();
        }
    }
}