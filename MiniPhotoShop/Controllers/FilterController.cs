using System.Collections.Generic;
using System.Windows.Forms;
using MiniPhotoShop.Filters;
using MiniPhotoShop.Filters.Adjustments;
using MiniPhotoShop.Filters.Base;
using MiniPhotoShop.Managers;
using MiniPhotoShop.Models;

namespace MiniPhotoShop.Controllers
{
    public class FilterController
    {
        private readonly DocumentManager _docManager;
        public Dictionary<TabPage, bool> IsBitwiseDocument { get; private set; } = new Dictionary<TabPage, bool>();

        public FilterController(DocumentManager docManager)
        {
            _docManager = docManager;
        }

        public void ApplyFilter(IImageFilter filter, TabPage activeTab)
        {
            ImageDocument doc = _docManager.GetActiveDocument();
            if (doc == null) return;

            bool isBitwise = (filter is NotFilter);
            if (IsBitwiseDocument.ContainsKey(activeTab))
                IsBitwiseDocument[activeTab] = isBitwise;
            else
                IsBitwiseDocument.Add(activeTab, isBitwise);

            doc.ApplyFilter(filter);
            _docManager.UpdateActiveCanvas();
        }

        public void ApplyThreshold(int threshold)
        {
            ApplyFilter(new ThresholdFilter(threshold), null);
        }

        public void ApplyBrightness(int value)
        {
            ApplyFilter(new BrightnessFilter(value), null);
        }

        public void RestoreActiveDocument()
        {
            ImageDocument doc = _docManager.GetActiveDocument();
            if (doc != null)
            {
                doc.Restore();
                _docManager.UpdateActiveCanvas();
            }
        }
    }
}