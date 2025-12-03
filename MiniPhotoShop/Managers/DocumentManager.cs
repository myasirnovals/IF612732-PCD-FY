using System;
using System.Drawing;
using MiniPhotoShop.Controllers;
using MiniPhotoShop.Models;

namespace MiniPhotoShop.Managers
{
    public class DocumentManager
    {
        private DocumentController _controller;

        public DocumentManager() { }

        public void SetController(DocumentController controller)
        {
            _controller = controller;
        }

        public ImageDocument GetActiveDocument()
        {
            return _controller?.GetActiveDocument();
        }

        public void OpenDocument(Bitmap image, string name)
        {
            _controller?.OpenDocument(image, name);
        }

        public void CloseActiveDocument()
        {
            _controller?.CloseActiveDocument();
        }

        public void UpdateActiveCanvas()
        {
            _controller?.UpdateActiveCanvas();
        }

        public bool IsSelectionModeActive()
        {
            var doc = GetActiveDocument();
            return doc != null && doc.IsInSelectionMode;
        }

        public void ToggleSelectionMode(bool isActive)
        {
            _controller?.ToggleSelectionMode(isActive);
        }
    }
}