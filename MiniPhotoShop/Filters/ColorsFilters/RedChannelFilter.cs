using MiniPhotoShop.Filters.Adjustments;
using MiniPhotoShop.Filters.Base;

namespace MiniPhotoShop.Filters.ColorsFilters
{
    public class RedChannelFilter : IImageFilter
    {
        public Color ProcessPixel(int r, int g, int b, int gray) => Color.FromArgb(r, 0, 0);
    }
}