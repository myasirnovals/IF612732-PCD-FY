using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MiniPhotoShop.Controllers;
using MiniPhotoShop.Models;
using MiniPhotoShop.Services;

namespace MiniPhotoShop.Controllers
{
    public class FileController
    {
        private readonly DocumentController _documents;
        private readonly ThumbnailController _thumbnails;
        private readonly IImageFileService _fileService;
        private readonly IDataExportService _exportService;

        public FileController(
            DocumentController documents, 
            ThumbnailController thumbnails, 
            IImageFileService fileService, 
            IDataExportService exportService)
        {
            _documents = documents;
            _thumbnails = thumbnails;
            _fileService = fileService;
            _exportService = exportService;
        }

        public void OpenImage()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Buka Gambar";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string fileName = Path.GetFileNameWithoutExtension(ofd.FileName);
                        
                        Bitmap loadedImage = _fileService.LoadImage(ofd.FileName);
                        
                        if (loadedImage != null)
                        {
                            _documents.OpenDocument(loadedImage, fileName);
                            _thumbnails.AddThumbnail(loadedImage, fileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Gagal membuka file: {ex.Message}", "Error");
                    }
                }
            }
        }

        public void SaveImage()
        {
            ImageDocument doc = _documents.GetActiveDocument();
            if (doc == null) return;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Simpan Gambar";
                sfd.FileName = doc.Name;
                sfd.Filter = "PNG Image|*.png|JPEG Image|*.jpg;*.jpeg|Bitmap Image|*.bmp";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Png;
                    string ext = Path.GetExtension(sfd.FileName).ToLower();
                    if (ext == ".jpg" || ext == ".jpeg") format = System.Drawing.Imaging.ImageFormat.Jpeg;
                    else if (ext == ".bmp") format = System.Drawing.Imaging.ImageFormat.Bmp;
                    
                    doc.CurrentBitmap.Save(sfd.FileName, format);
                }
            }
        }

        public void ExportPixelData(bool asBinary)
        {
            ImageDocument doc = _documents.GetActiveDocument();
            if (doc == null) return;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text File (*.txt)|*.txt";
                sfd.Title = "Simpan Data Pixel";
                sfd.FileName = $"{doc.Name}_pixels.txt";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _exportService.SavePixelData(sfd.FileName, doc.PixelArray, doc.IsGrayscale, asBinary);
                        
                        MessageBox.Show("Data pixel berhasil disimpan!", "Sukses");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Gagal export pixel: {ex.Message}", "Error");
                    }
                }
            }
        }

        public void ExportHistogramData()
        {
            ImageDocument doc = _documents.GetActiveDocument();
            if (doc == null) return;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text File (*.txt)|*.txt";
                sfd.Title = "Simpan Data Histogram";
                sfd.FileName = $"{doc.Name}_tabel_data.txt";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _exportService.SaveHistogramData(sfd.FileName, doc.Histogram);
                        MessageBox.Show("Tabel data berhasil disimpan!", "Sukses");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Gagal export histogram: {ex.Message}", "Error");
                    }
                }
            }
        }
    }
}