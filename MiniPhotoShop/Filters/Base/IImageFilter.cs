using System.Drawing;

namespace MiniPhotoShop.Filters.Base
{
    public interface IImageFilter
    {
        Color ProcessPixel(int r, int g, int b, int gray);
    }
}