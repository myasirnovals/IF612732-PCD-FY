using System;
using System.Drawing;
using System.IO;

namespace MiniPhotoShop.Services
{
    public class ImageFileService : IImageFileService
    {
        public Bitmap LoadImage(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                throw new FileNotFoundException("File tidak ditemukan.", filePath);
            }
            
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    ms.Position = 0; 
                    return new Bitmap(ms);
                }
            }
        }
    }
}