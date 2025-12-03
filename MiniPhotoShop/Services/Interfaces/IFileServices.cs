using System.Drawing;
using MiniPhotoShop.Models;

namespace MiniPhotoShop.Services
{
    public interface IImageFileService
    {
        Bitmap LoadImage(string filePath);
    }

    public interface IDataExportService
    {
        void SavePixelData(string filePath, int[,,] pixelArray, bool isGrayscale, bool outputAsBinary);
        void SaveHistogramData(string filePath, HistogramData histogram);
    }
}