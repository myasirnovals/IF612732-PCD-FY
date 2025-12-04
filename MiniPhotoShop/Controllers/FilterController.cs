using MiniPhotoShop.Filters.Adjustments;
using MiniPhotoShop.Filters.Base;
using MiniPhotoShop.Filters.ColorsFilters;
using MiniPhotoShop.Managers;
using MiniPhotoShop.Models;
using MiniPhotoShop.Services.Interfaces;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MiniPhotoShop.Controllers
{
    public class FilterController
    {
        private readonly DocumentManager _docManager;
        private readonly IImageProcessingService _processor;
        private Dictionary<TabPage, bool> IsBitwiseDocument = new Dictionary<TabPage, bool>();

        public FilterController(DocumentManager docManager, IImageProcessingService processor)
        {
            _docManager = docManager;
            _processor = processor;
        }

        public void ApplyFilter(IImageFilter filter, TabPage activeTab)
        {
            ImageDocument doc = _docManager.GetActiveDocument();
            if (doc != null)
            {
                doc.ApplyFilter(filter);
                doc.IsGrayscale = (filter is GrayscaleFilter);
                doc.IsBlackAndWhite = (filter is ThresholdFilter);

                if (activeTab?.Controls.Count > 0 && activeTab.Controls[0] is PictureBox canvas)
                {
                    canvas.Image = doc.CurrentBitmap;
                }
            }
            bool isBitwise = (filter is NotFilter);

            if (activeTab != null)
            {
                if (IsBitwiseDocument.ContainsKey(activeTab))
                    IsBitwiseDocument[activeTab] = isBitwise;
                else
                    IsBitwiseDocument.Add(activeTab, isBitwise);
            }

            _docManager.UpdateActiveCanvas();
        }

        public void ApplyThreshold(int threshold, TabPage activeTab)
        {
            ApplyFilter(new ThresholdFilter(threshold), activeTab);
        }

        public void ApplyBrightness(int brightness)
        {
            ApplyFilter(new BrightnessFilter(brightness), null);
        }

        public void ApplyBrightness(int value, TabPage activeTab)
        {
            ApplyFilter(new BrightnessFilter(value), activeTab);
        }

        public void RemoveDocument(TabPage tab)
        {
            if (tab != null && IsBitwiseDocument.ContainsKey(tab))
            {
                IsBitwiseDocument.Remove(tab);
            }
        }

        public void ApplyConvolutionFilter(string filterType)
        {
            var doc = _docManager.GetActiveDocument();
            if (doc == null) return;

            double[,] kernel = null;
            double factor = 1.0;
            int bias = 0;

            switch (filterType)
            {
                case "Identity":
                    kernel = BaseKernel.Identity;
                    break;

                case "Blur":
                    kernel = BaseKernel.GaussianBlur;
                    factor = 1.0 / 256.0;
                    break;

                case "Sharpen":
                    kernel = BaseKernel.Sharpen;
                    break;

                case "EdgeDetection":
                    kernel = BaseKernel.EdgeDetection;
                    break;

                case "Emboss":
                    kernel = BaseKernel.Emboss;
                    break;
            }

            if (kernel != null)
            {
                Bitmap result = _processor.ApplyFilterKernel(doc.CurrentBitmap, kernel, factor, bias);
                _docManager.OpenDocument(result, doc.Name + "_" + filterType);
            }
        }
    }
}