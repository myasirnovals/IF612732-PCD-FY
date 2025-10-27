using System.Drawing;
using MiniPhotoShop;

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

    public class BrightnessFilter : IImageFilter
    {
        private readonly int _brightnessValue;

        public BrightnessFilter(int brightnessValue)
        {
            _brightnessValue = brightnessValue;
        }

        public Color ProcessPixel(int r, int g, int b, int gray)
        {
            int newR = r + _brightnessValue;
            int newG = g + _brightnessValue;
            int newB = b + _brightnessValue;

            newR = Clamp(newR);
            newG = Clamp(newG);
            newB = Clamp(newB);

            return Color.FromArgb(newR, newG, newB);
        }

        private int Clamp(int value)
        {
            if (value < 0) return 0;
            if (value > 255) return 255;
            return value;
        }
    }

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