using MiniPhotoShop.Filters.Adjustments;
using MiniPhotoShop.Filters.Base;
using MiniPhotoShop.Filters.ColorsFilters;
using MiniPhotoShop.Managers;
using MiniPhotoShop.Models;
using MiniPhotoShop.Services.Interfaces;
using System.Drawing;
using System.Windows.Forms;

namespace MiniPhotoShop.Controllers
{
    public class FilterController
    {
        private readonly DocumentManager _docManager;
        private readonly IImageProcessingService _processor;

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

<<<<<<< HEAD
                if (activeTab?.Controls.Count > 0 && activeTab.Controls[0] is PictureBox canvas)
                {
                    canvas.Image = doc.CurrentBitmap;
                }
            }
=======
            bool isBitwise = (filter is NotFilter);
            
            if (activeTab != null) 
            {
                if (IsBitwiseDocument.ContainsKey(activeTab))
                    IsBitwiseDocument[activeTab] = isBitwise;
                else
                    IsBitwiseDocument.Add(activeTab, isBitwise);
            }

            doc.ApplyFilter(filter);
            _docManager.UpdateActiveCanvas();
>>>>>>> 4e2997168d6399af8e9ddab98c69ba10dba4b79c
        }

        public void ApplyThreshold(int threshold, TabPage activeTab)
        {
            ApplyFilter(new ThresholdFilter(threshold), activeTab); 
        }

<<<<<<< HEAD
        public void ApplyBrightness(int brightness)
        {
            ApplyFilter(new BrightnessFilter(brightness), null);
=======
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
>>>>>>> 4e2997168d6399af8e9ddab98c69ba10dba4b79c
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
                    kernel = new double[,] { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } };
                    break;

                case "Blur":
                    kernel = new double[,] { { 1, 2, 1 }, { 2, 4, 2 }, { 1, 2, 1 } };
                    factor = 1.0 / 16.0;
                    break;

                case "Sharpen":
                    kernel = new double[,] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
                    break;

                case "EdgeDetection":
                    kernel = new double[,] { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } };
                    break;

                case "Emboss":
                    kernel = new double[,] { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
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