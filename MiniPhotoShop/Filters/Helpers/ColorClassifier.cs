using System.Drawing;
using MiniPhotoShop.Models;

namespace MiniPhotoShop.Filters.Helpers
{
    public class ColorClassifier
    {
        public static ColorRanges GetColorRange(int r, int g, int b)
        {
            // --- PERBAIKAN: Deteksi Manual Warna Dominan untuk Akurasi Tinggi ---
            if (r > 150 && g < 100 && b < 100) return ColorRanges.Red;
            if (g > 150 && r < 100 && b < 100) return ColorRanges.Green;
            if (b > 150 && r < 100 && g < 100) return ColorRanges.Blue;

            // Logika Berbasis Hue (Warna Standar)
            Color color = Color.FromArgb(r, g, b);
            float hue = color.GetHue();
            float saturation = color.GetSaturation();
            float brightness = color.GetBrightness();

            // Koreksi Gray Range (0.59 -> 0.9 agar warna cerah tidak dianggap abu-abu)
            if (saturation < 0.1 || brightness < 0.1 || brightness > 0.9)
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