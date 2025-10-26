using System.Drawing;
using MiniPhotoShop.Models;

namespace MiniPhotoShop
{
    public interface IImageFilter
    {
        Color ProcessPixel(int r, int g, int b, int gray);
    }
    
    public interface IImageProcessingService
    {
        int[,,] CreatePixelArray(Bitmap bmp);
        Bitmap CreateBitmapFromPixelArray(int[,,] pixelArray, IImageFilter filter);
        HistogramData CalculateHistogram(int[,,] pixelArray);
        Bitmap DrawHistogram(int width, int height, int[] counts, int maxCount, Color barColor);
    }
    
    public interface IImageFileService
    {
        Bitmap OpenImage(out string fileName);
    }
    
    public interface IDataExportService
    {
        void SavePixelData(string fileName, int[,,] pixelArray, bool isGrayscale);
        void SaveHistogramData(string fileName, HistogramData histogram);
    }
}