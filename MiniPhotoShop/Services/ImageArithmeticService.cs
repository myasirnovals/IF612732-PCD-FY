using System.Drawing;
using MiniPhotoShop.Services.Arithmetic.Basic;
using MiniPhotoShop.Services.Arithmetic.Bitwise;
using MiniPhotoShop.Services.Arithmetic.Normalized;
using MiniPhotoShop.Services.Arithmetic.Constant;

namespace MiniPhotoShop.Services
{
    public class ImageArithmeticService : IImageArithmeticService
    {
        // --- PERBAIKAN: Wrapper untuk menangani perbedaan ukuran gambar ---
        public Bitmap ApplyArithmetic(Bitmap img1, Bitmap img2, string mode)
        {
            if (img1 == null || img2 == null) return img1;

            Bitmap processedImg2 = img2;
            bool needsDispose = false;

            // Jika ukuran beda, samakan ukuran img2 dengan img1
            if (img1.Width != img2.Width || img1.Height != img2.Height)
            {
                processedImg2 = new Bitmap(img2, img1.Width, img1.Height);
                needsDispose = true;
            }

            Bitmap res;
            switch (mode.ToLower())
            {
                case "add": res = AddImages(img1, processedImg2); break;
                case "subtract": res = SubtractImages(img1, processedImg2); break;
                case "multiply": res = MultiplyImages(img1, processedImg2); break;
                case "divide": res = DivideImages(img1, processedImg2); break;
                default: res = new Bitmap(img1); break;
            }

            if (needsDispose) processedImg2.Dispose();
            return res;
        }

        public Bitmap AddImages(Bitmap source, Bitmap target) => new AddOperation().Process(source, target);
        public Bitmap SubtractImages(Bitmap source, Bitmap target) => new SubtractOperation().Process(source, target);
        public Bitmap MultiplyImages(Bitmap source, Bitmap target) => new MultiplyOperation().Process(source, target);
        public Bitmap DivideImages(Bitmap source, Bitmap target) => new DivideOperation().Process(source, target);
        public Bitmap AndImages(Bitmap source, Bitmap target) => new AndOperation().Process(source, target);
        public Bitmap OrImages(Bitmap source, Bitmap target) => new OrOperation().Process(source, target);
        public Bitmap XorImages(Bitmap source, Bitmap target) => new XorOperation().Process(source, target);

        public Bitmap MultiplyByConstant(Bitmap source, double constant) => new ConstantOperation(ConstantType.Multiply, constant).Process(source);
        public Bitmap DivideByConstant(Bitmap source, double constant) => new ConstantOperation(ConstantType.Divide, constant).Process(source);
    }
}