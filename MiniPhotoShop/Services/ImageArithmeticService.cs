using System.Drawing;
using MiniPhotoShop.Services.Arithmetic.Basic;
using MiniPhotoShop.Services.Arithmetic.Bitwise;
using MiniPhotoShop.Services.Arithmetic.Normalized;
using MiniPhotoShop.Services.Arithmetic.Constant;

namespace MiniPhotoShop.Services
{
    public class ImageArithmeticService : IImageArithmeticService
    {
        public Bitmap AddImages(Bitmap source, Bitmap target) 
            => new AddOperation().Process(source, target);

        public Bitmap SubtractImages(Bitmap source, Bitmap target) 
            => new SubtractOperation().Process(source, target);
        
        public Bitmap MultiplyImages(Bitmap source, Bitmap target) 
            => new MultiplyOperation().Process(source, target);

        public Bitmap DivideImages(Bitmap source, Bitmap target) 
            => new DivideOperation().Process(source, target);
        
        public Bitmap AndImages(Bitmap source, Bitmap target) 
            => new AndOperation().Process(source, target);

        public Bitmap OrImages(Bitmap source, Bitmap target) 
            => new OrOperation().Process(source, target);

        public Bitmap XorImages(Bitmap source, Bitmap target) 
            => new XorOperation().Process(source, target);
        
        public Bitmap MultiplyByConstant(Bitmap source, double constant)
        {
            return new ConstantOperation(ConstantType.Multiply, constant).Process(source);
        }

        public Bitmap DivideByConstant(Bitmap source, double constant)
        {
            return new ConstantOperation(ConstantType.Divide, constant).Process(source);
        }
    }
}