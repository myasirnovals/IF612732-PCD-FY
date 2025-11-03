using System.Drawing;
using MiniPhotoShop.Models;

namespace MiniPhotoShop
{
    public interface IImageFilter
    {
        Color ProcessPixel(int r, int g, int b, int gray);
    }

    public interface IImageProcessingService : IPixelService, IHistogramService
    {
        Bitmap? AddImages(Bitmap sourceBmp, Bitmap currentBitmap);
        Bitmap? AndImages(Bitmap sourceBmp, Bitmap currentBitmap);
        Bitmap? OrImages(Bitmap sourceBmp, Bitmap currentBitmap);
        Bitmap? SubtractImages(Bitmap sourceBmp, Bitmap currentBitmap);
        Bitmap? XorImages(Bitmap sourceBmp, Bitmap currentBitmap);
    }

    public interface IPixelService
    {
        int[,,] CreatePixelArray(Bitmap bmp);
        Bitmap CreateBitmapFromPixelArray(Bitmap sourcedBitmap, IImageFilter filter);
    }

    public interface IHistogramService
    {
        HistogramData CalculateHistogram(int[,,] pixelArray);
        Bitmap DrawHistogram(int width, int height, int[] counts, int maxCount, Color barColor);
    }

    public interface IImageArithmeticService
    {
        Bitmap AddImages(Bitmap source, Bitmap target);
        Bitmap SubtractImages(Bitmap source, Bitmap target);

        Bitmap AndImages(Bitmap source, Bitmap target);
        Bitmap OrImages(Bitmap source, Bitmap target);
        Bitmap XorImages(Bitmap source, Bitmap target);
    }

    public interface IImageFileService
    {
        Bitmap OpenImage(out string fileName);
    }

    public interface IDataExportService
    {
        void SavePixelData(string fileName, int[,,] pixelArray, bool isGrayscale, bool outputAsBinary);
        void SaveHistogramData(string fileName, HistogramData histogram);
    }

    public interface IDialogService
    {
        DialogResult ShowAdjustmentDialog(
            string title, int min, int max, int initialValue, int tickFreq, string labelText,
            Action<int> onPreview,
            out int finalValue);
    }
}