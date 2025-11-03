
using System.Drawing;
using MiniPhotoShop;

namespace MiniPhotoShop.Filters
{
    public enum ColorRanges
    {
        None,
        Red,
        Orange,
        Yellow,
        Green,
        Cyan,
        Blue,
        Purple,
        Gray
    }

    public static class ColorClassifier
    {
        public static ColorRanges GetColorRange(int r, int g, int b)
        {
            float hue = Color.FromArgb(r, g, b).GetHue();
            float saturation = Color.FromArgb(r, g, b).GetSaturation();
            float brightness = Color.FromArgb(r, g, b).GetBrightness();

            if (saturation < 0.1 || brightness < 0.1 || brightness > 0.59)
            {
                return ColorRanges.Gray;
            }

            if (hue < 15 || hue >= 345) return ColorRanges.Red;
            if (hue < 45) return ColorRanges.Orange;
            if (hue < 75) return ColorRanges.Yellow;
            if (hue < 150) return ColorRanges.Green;
            if (hue < 210) return ColorRanges.Cyan;
            if (hue < 270) return ColorRanges.Blue;
            return ColorRanges.Purple;
        }
    }

    public class ColorRangeFilter : IImageFilter
    {
        private readonly ColorRanges _targetRange;

        public ColorRangeFilter(ColorRanges targetRange)
        {
            _targetRange = targetRange;
        }

        public Color ProcessPixel(int r, int g, int b, int gray)
        {
            if (ColorClassifier.GetColorRange(r, g, b) == _targetRange)
            {
                return Color.FromArgb(r, g, b);
            }

            return Color.White;
        }
    }

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

    public class NotFilter : IImageFilter
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
        public ThresholdFilter(int threshold) { _threshold = threshold; }
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