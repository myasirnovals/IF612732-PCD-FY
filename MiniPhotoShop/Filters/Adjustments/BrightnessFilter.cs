using MiniPhotoShop.Filters.Base;

namespace MiniPhotoShop.Filters.Adjustments
{
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
}