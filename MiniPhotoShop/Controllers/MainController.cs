using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MiniPhotoShop.Managers;
using MiniPhotoShop.Models;
using MiniPhotoShop.Services;
using System.Collections.Generic;
using MiniPhotoShop.Filters;

namespace MiniPhotoShop.Controllers
{
    public class MainController
    {
        private readonly DocumentManager _documentManager;
        private readonly ThumbnailManager _thumbnailManager;
        private readonly IImageFileService _fileService;
        private readonly IDataExportService _exportService;
        private readonly IImageProcessingService _processingService;
        private readonly IImageArithmeticService _arithmeticService;
        private readonly IDialogService _dialogService;

        public string CurrentArithmeticOperation { get; set; } = "None";
        public Dictionary<TabPage, bool> IsBitwiseDocument { get; private set; } = new Dictionary<TabPage, bool>();

        public MainController(
            DocumentManager docManager,
            ThumbnailManager thumbManager,
            IImageFileService fileService,
            IDataExportService exportService,
            IImageProcessingService procService,
            IImageArithmeticService arithService,
            IDialogService dialogService)
        {
            _documentManager = docManager;
            _thumbnailManager = thumbManager;
            _fileService = fileService;
            _exportService = exportService;
            _processingService = procService;
            _arithmeticService = arithService;
            _dialogService = dialogService;
        }

        public void OpenImage()
        {
            Bitmap loadedImage = _fileService.OpenImage(out string fileName);
            if (loadedImage != null)
            {
                _documentManager.OpenDocument(loadedImage, fileName);
                _thumbnailManager.AddThumbnail(loadedImage, fileName);
            }
        }

        public void SaveImage()
        {
            ImageDocument doc = _documentManager.GetActiveDocument();
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

        public void ApplyFilter(IImageFilter filter, TabPage activeTab)
        {
            ImageDocument doc = _documentManager.GetActiveDocument();
            if (doc == null) return;

            bool isBitwise = (filter is NotFilter);
            if (IsBitwiseDocument.ContainsKey(activeTab))
                IsBitwiseDocument[activeTab] = isBitwise;
            else
                IsBitwiseDocument.Add(activeTab, isBitwise);

            doc.ApplyFilter(filter);
            _documentManager.UpdateActiveCanvas();
        }

        public void ApplyThreshold(int threshold)
        {
            ImageDocument doc = _documentManager.GetActiveDocument();
            if (doc == null) return;
            IImageFilter filter = new ThresholdFilter(threshold);
            doc.ApplyFilter(filter);
            _documentManager.UpdateActiveCanvas();
        }

        public void ApplyBrightness(int value)
        {
            ImageDocument doc = _documentManager.GetActiveDocument();
            if (doc == null) return;
            IImageFilter filter = new BrightnessFilter(value);
            doc.ApplyFilter(filter);
            _documentManager.UpdateActiveCanvas();
        }

        public void RestoreActiveDocument()
        {
            ImageDocument doc = _documentManager.GetActiveDocument();
            if (doc != null)
            {
                doc.Restore();
                _documentManager.UpdateActiveCanvas();
            }
        }

        public void ApplyConstantArithmetic(string operation, double constant)
        {
            ImageDocument activeDoc = _documentManager.GetActiveDocument();
            if (activeDoc == null) return;

            if (operation == "Divide" && Math.Abs(constant) < 0.0001)
            {
                MessageBox.Show("Tidak dapat membagi dengan nol.", "Error");
                return;
            }

            Bitmap result = null;
            if (operation == "Multiply")
                result = _arithmeticService.MultiplyByConstant(activeDoc.CurrentBitmap, constant);
            else if (operation == "Divide")
                result = _arithmeticService.DivideByConstant(activeDoc.CurrentBitmap, constant);

            if (result != null)
            {
                string newName = $"{activeDoc.Name}_{operation}_{constant}";
                _documentManager.OpenDocument(result, newName);
            }
        }

        public void HandleDragDropArithmetic(string sourceName, ImageDocument targetDoc)
        {
            if (CurrentArithmeticOperation == "None" || targetDoc == null) return;

            using (Bitmap sourceBmp = _thumbnailManager.FindThumbnailImageByName(sourceName))
            {
                if (sourceBmp == null) return;

                Bitmap resultBmp = null;
                string opName = "";

                switch (CurrentArithmeticOperation)
                {
                    case "Add": resultBmp = _arithmeticService.AddImages(sourceBmp, targetDoc.CurrentBitmap); opName = "Addition"; break;
                    case "Subtract": resultBmp = _arithmeticService.SubtractImages(sourceBmp, targetDoc.CurrentBitmap); opName = "Difference"; break;
                    case "Multiply": resultBmp = _arithmeticService.MultiplyImages(sourceBmp, targetDoc.CurrentBitmap); opName = "Multiplication"; break;
                    case "Divide": resultBmp = _arithmeticService.DivideImages(sourceBmp, targetDoc.CurrentBitmap); opName = "Division"; break;
                    case "AND": resultBmp = _arithmeticService.AndImages(sourceBmp, targetDoc.CurrentBitmap); opName = "AND"; break;
                    case "OR": resultBmp = _arithmeticService.OrImages(sourceBmp, targetDoc.CurrentBitmap); opName = "OR"; break;
                    case "XOR": resultBmp = _arithmeticService.XorImages(sourceBmp, targetDoc.CurrentBitmap); opName = "XOR"; break;
                }

                if (resultBmp != null)
                {
                    string newName = $"{Path.GetFileNameWithoutExtension(targetDoc.Name)}_{opName}_{Path.GetFileNameWithoutExtension(sourceName)}";
                    _documentManager.OpenDocument(resultBmp, newName);
                }
            }
            CurrentArithmeticOperation = "None";
        }

        public void ApplyTransformation(string type, double param1, double param2)
        {
            ImageDocument doc = _documentManager.GetActiveDocument();
            if (doc == null) return;

            Bitmap result = null;
            string suffix = "";

            if (type == "Translate")
            {
                result = _processingService.TranslateImage(doc.CurrentBitmap, (int)param1, (int)param2);
                suffix = "Trans";
            }
            else if (type == "Rotate")
            {
                result = _processingService.RotateImage(doc.CurrentBitmap, (float)param1);
                suffix = "Rot";
            }
            else if (type == "Scale")
            {
                result = _processingService.ScaleImage(doc.CurrentBitmap, param1);
                suffix = "Scale";
            }
            else if (type == "Distort")
            {
                result = _processingService.DistortImage(doc.CurrentBitmap, param1, param2);
                suffix = "Ripple";
            }

            if (result != null)
                _documentManager.OpenDocument(result, doc.Name + "_" + suffix);
        }

        public void ExportHistogramData()
        {
            ImageDocument doc = _documentManager.GetActiveDocument();
            if (doc != null) _exportService.SaveHistogramData(doc.Name, doc.Histogram);
        }

        public void ExportPixelData(bool asBinary)
        {
            ImageDocument doc = _documentManager.GetActiveDocument();
            if (doc != null) _exportService.SavePixelData(doc.Name, doc.PixelArray, doc.IsGrayscale, asBinary);
        }
    }
}