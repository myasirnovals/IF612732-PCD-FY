using MiniPhotoShop.Filters.Base;

namespace MiniPhotoShop.Filters.Adjustments
{
    public class NegationFilter : IImageFilter
    {
        public Color ProcessPixel(int r, int g, int b, int gray)
        {
            int newR = 255 - r;
            int newG = 255 - g;
            int newB = 255 - b;

            return Color.FromArgb(newR, newG, newB);
        }
    }
}