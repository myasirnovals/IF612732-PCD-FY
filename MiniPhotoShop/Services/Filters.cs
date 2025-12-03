
using System.Drawing;
using MiniPhotoShop;

namespace MiniPhotoShop.Filters
{
    

    public class RgbFilter : IImageFilter
    {
        public Color ProcessPixel(int r, int g, int b, int gray) => Color.FromArgb(r, g, b);
    }

    

    

    

    

    

    

    

    
}