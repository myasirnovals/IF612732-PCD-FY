using System.Drawing;
using MiniPhotoShop.Filters.Base;

namespace MiniPhotoShop.Filters.ColorsFilters
{
    /// Filter untuk mengubah citra grayscale menjadi berwarna (Pseudo Color)
    /// berdasarkan rentang nilai keabuan.
    public class PseudoColorFilter : IImageFilter
    {
        public Color ProcessPixel(int r, int g, int b, int gray)
        {
            // Logika transformasi warna berdasarkan nilai Gray (0-255)
            // Jika gelap (0-127): Transisi dari Biru ke Hijau
            if (gray < 128)
            {
                return Color.FromArgb(0, gray * 2, 255 - (gray * 2));
            }
            // Jika terang (128-255): Transisi dari Hijau ke Merah
            return Color.FromArgb((gray - 128) * 2, 255 - ((gray - 128) * 2), 0);
        }
    }
}