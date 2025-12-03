using System.Drawing;
using MiniPhotoShop.Filters.Base;
using MiniPhotoShop.Filters.Helpers;
using MiniPhotoShop.Models;

namespace MiniPhotoShop.Filters.ColorsFilters
{
    public class ColorRangeFilter : IImageFilter
    {
        private readonly ColorRanges _targetRange;

        public ColorRangeFilter(ColorRanges targetRange)
        {
            _targetRange = targetRange;
        }

        public Color ProcessPixel(int r, int g, int b, int gray)
        {
            if (ColorClassifier.GetColorRange(r, g, b) == _targetRange)
            {
                return Color.FromArgb(r, g, b);
            }

            return Color.White;
        }
    }
}