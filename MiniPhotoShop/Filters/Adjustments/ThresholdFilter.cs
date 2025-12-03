using MiniPhotoShop.Filters.Base;

namespace MiniPhotoShop.Filters.Adjustments
{
    public class ThresholdFilter : IImageFilter
    {
        private readonly int _threshold;

        public ThresholdFilter(int threshold)
        {
            _threshold = threshold;
        }

        public Color ProcessPixel(int r, int g, int b, int gray)
        {
            if (gray < _threshold)
            {
                return Color.White;
            }
            else
            {
                return Color.Black;
            }
        }
    }
}