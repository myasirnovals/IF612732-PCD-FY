using MiniPhotoShop.Managers;
using MiniPhotoShop.Models;
using MiniPhotoShop.Services;
using MiniPhotoShop.Services.Interfaces;
using System.Drawing;

namespace MiniPhotoShop.Controllers
{
    public class TransformController
    {
        private readonly DocumentManager _docManager;
        private readonly IImageProcessingService _processingService;

        public TransformController(DocumentManager docManager, IImageProcessingService processingService)
        {
            _docManager = docManager;
            _processingService = processingService;
        }

        public void ApplyTransformation(string type, double param1, double param2)
        {
            ImageDocument doc = _docManager.GetActiveDocument();
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
                _docManager.OpenDocument(result, doc.Name + "_" + suffix);
        }
    }
}