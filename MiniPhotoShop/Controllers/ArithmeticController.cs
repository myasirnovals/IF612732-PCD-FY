using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MiniPhotoShop.Managers;
using MiniPhotoShop.Models;
using MiniPhotoShop.Services;

namespace MiniPhotoShop.Controllers
{
    public class ArithmeticController
    {
        private readonly DocumentManager _docManager;
        private readonly ThumbnailManager _thumbManager;
        private readonly IImageArithmeticService _arithmeticService;
        
        // State operasi disimpan di sini
        public string CurrentOperation { get; set; } = "None";

        public ArithmeticController(DocumentManager docManager, ThumbnailManager thumbManager, IImageArithmeticService arithmeticService)
        {
            _docManager = docManager;
            _thumbManager = thumbManager;
            _arithmeticService = arithmeticService;
        }

        public void ApplyConstant(string operation, double constant)
        {
            ImageDocument activeDoc = _docManager.GetActiveDocument();
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
                _docManager.OpenDocument(result, newName);
            }
        }

        public void HandleDragDrop(string sourceName, ImageDocument targetDoc)
        {
            if (CurrentOperation == "None" || targetDoc == null) return;

            using (Bitmap sourceBmp = _thumbManager.FindThumbnailImageByName(sourceName))
            {
                if (sourceBmp == null) return;

                Bitmap resultBmp = null;
                string opName = "";

                switch (CurrentOperation)
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
                    _docManager.OpenDocument(resultBmp, newName);
                }
            }
            CurrentOperation = "None"; // Reset setelah drop
        }
    }
}