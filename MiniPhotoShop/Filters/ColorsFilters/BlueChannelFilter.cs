using MiniPhotoShop.Filters.Base;

namespace MiniPhotoShop.Filters.ColorsFilters
{
    public class BlueChannelFilter : IImageFilter
    {
        public Color ProcessPixel(int r, int g, int b, int gray) => Color.FromArgb(0, 0, b);
    }
}