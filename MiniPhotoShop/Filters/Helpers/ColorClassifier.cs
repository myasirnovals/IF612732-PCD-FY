namespace MiniPhotoShop.Filters.Helpers
{
    public class ColorClassifier
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
}