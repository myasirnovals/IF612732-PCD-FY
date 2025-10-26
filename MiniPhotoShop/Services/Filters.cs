using System.Drawing;

namespace MiniPhotoShop.Filters
{
    public class RgbFilter : IImageFilter
    {
        public Color ProcessPixel(int r, int g, int b, int gray) => Color.FromArgb(r, g, b);
    }

    public class GrayscaleFilter : IImageFilter
    {
        public Color ProcessPixel(int r, int g, int b, int gray) => Color.FromArgb(gray, gray, gray);
    }

    public class RedChannelFilter : IImageFilter
    {
        public Color ProcessPixel(int r, int g, int b, int gray) => Color.FromArgb(r, 0, 0);
    }

    public class GreenChannelFilter : IImageFilter
    {
        public Color ProcessPixel(int r, int g, int b, int gray) => Color.FromArgb(0, g, 0);
    }

    public class BlueChannelFilter : IImageFilter
    {
        public Color ProcessPixel(int r, int g, int b, int gray) => Color.FromArgb(0, 0, b);
    }
}