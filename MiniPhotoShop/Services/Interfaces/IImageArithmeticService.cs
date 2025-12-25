using System.Drawing;

namespace MiniPhotoShop.Services
{
    public interface IImageArithmeticService
    {
        Bitmap AddImages(Bitmap source, Bitmap target);
        Bitmap SubtractImages(Bitmap source, Bitmap target);
        Bitmap MultiplyImages(Bitmap source, Bitmap target);
        Bitmap DivideImages(Bitmap source, Bitmap target);
        Bitmap MultiplyByConstant(Bitmap source, double constant);
        Bitmap DivideByConstant(Bitmap source, double constant);
        Bitmap AndImages(Bitmap source, Bitmap target);
        Bitmap OrImages(Bitmap source, Bitmap target);
        Bitmap XorImages(Bitmap source, Bitmap target);
        // Tambahkan di dalam interface IImageArithmeticService
        Bitmap ApplyArithmetic(Bitmap img1, Bitmap img2, string mode);
    }
}