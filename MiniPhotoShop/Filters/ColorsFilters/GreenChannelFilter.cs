using MiniPhotoShop.Filters.Base;

namespace MiniPhotoShop.Filters.ColorsFilters
{
    public class GreenChannelFilter : IImageFilter
    {
        public Color ProcessPixel(int r, int g, int b, int gray) => Color.FromArgb(0, g, 0);
    }
}