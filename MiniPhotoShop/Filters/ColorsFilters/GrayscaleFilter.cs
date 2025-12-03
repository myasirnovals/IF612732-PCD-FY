using MiniPhotoShop.Filters.Base;

namespace MiniPhotoShop.Filters.ColorsFilters
{
    public class GrayscaleFilter : IImageFilter
    {
        public Color ProcessPixel(int r, int g, int b, int gray) => Color.FromArgb(gray, gray, gray);
    }
}