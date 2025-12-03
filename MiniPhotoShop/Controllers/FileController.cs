using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MiniPhotoShop.Managers;
using MiniPhotoShop.Models;
using MiniPhotoShop.Services;

namespace MiniPhotoShop.Controllers
{
    public class FileController
    {
        private readonly DocumentManager _docManager;
        private readonly ThumbnailManager _thumbManager;
        private readonly IImageFileService _fileService;
        private readonly IDataExportService _exportService;

        public FileController(DocumentManager docManager, ThumbnailManager thumbManager, IImageFileService fileService, IDataExportService exportService)
        {
            _docManager = docManager;
            _thumbManager = thumbManager;
            _fileService = fileService;
            _exportService = exportService;
        }

        public void OpenImage()
        {
            Bitmap loadedImage = _fileService.OpenImage(out string fileName);
            if (loadedImage != null)
            {
                _docManager.OpenDocument(loadedImage, fileName);
                _thumbManager.AddThumbnail(loadedImage, fileName);
            }
        }

        public void SaveImage()
        {
            ImageDocument doc = _docManager.GetActiveDocument();
            if (doc == null) return;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Simpan Gambar";
                sfd.FileName = doc.Name;
                sfd.Filter = "PNG Image|*.png|JPEG Image|*.jpg;*.jpeg|Bitmap Image|*.bmp";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Png;
                        string ext = Path.GetExtension(sfd.FileName).ToLower();
                        if (ext == ".jpg" || ext == ".jpeg") format = System.Drawing.Imaging.ImageFormat.Jpeg;
                        else if (ext == ".bmp") format = System.Drawing.Imaging.ImageFormat.Bmp;

                        doc.CurrentBitmap.Save(sfd.FileName, format);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
        }

        public void ExportHistogramData()
        {
            ImageDocument doc = _docManager.GetActiveDocument();
            if (doc != null) _exportService.SaveHistogramData(doc.Name, doc.Histogram);
        }

        public void ExportPixelData(bool asBinary)
        {
            ImageDocument doc = _docManager.GetActiveDocument();
            if (doc != null) _exportService.SavePixelData(doc.Name, doc.PixelArray, doc.IsGrayscale, asBinary);
        }
    }
}