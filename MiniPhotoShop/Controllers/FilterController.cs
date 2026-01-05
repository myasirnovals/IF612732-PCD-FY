using MiniPhotoShop.Filters.Adjustments;
using MiniPhotoShop.Filters.Helpers;
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

        /// PEREGANGAN KONTRAS GLOBAL: Menggunakan statistik seluruh citra 
        /// untuk memetakan kembali intensitas warna.
        public void ApplyGlobalContrastStretch()
        {
            var doc = _docManager.GetActiveDocument();
            if (doc == null) return;

            var filter = new LinearStretchFilter();
            Bitmap result = filter.Apply(doc.CurrentBitmap);

            if (result != null)
            {
                _docManager.OpenDocument(result, doc.Name + "_GlobalStretch");
            }
        }

        /// PEREGANGAN KONTRAS LOKAL: Menggunakan Adaptive Histogram Equalization 
        /// untuk meregangkan kontras berdasarkan area sekitar pixel (blok).
        public void ApplyLocalContrastStretch()
        {
            var doc = _docManager.GetActiveDocument();
            if (doc == null) return;

            var filter = new AdaptiveHistogramEqualizationFilter();
            Bitmap result = filter.Apply(doc.CurrentBitmap);

            if (result != null)
            {
                _docManager.OpenDocument(result, doc.Name + "_LocalStretch");
            }
        }

        /// PEREGANGAN KONTRAS ARAS TITIK: Manipulasi langsung pada intensitas 
        /// tiap titik pixel menggunakan transformasi linear.
        public void ApplyPointContrastStretch()
        {
            var doc = _docManager.GetActiveDocument();
            if (doc == null) return;

            // Aras titik menggunakan LinearStretch standar
            var filter = new LinearStretchFilter();
            Bitmap result = filter.Apply(doc.CurrentBitmap);

            if (result != null)
            {
                _docManager.OpenDocument(result, doc.Name + "_PointStretch");
            }
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

            Bitmap sourceBitmap = doc.CurrentBitmap;
            bool isEdgeDetection = (filterType == "Roberts" || filterType == "Sobel" || filterType == "Canny");

            if (isEdgeDetection)
            {
                sourceBitmap = _processor.CreateBitmapFromPixelArray(doc.CurrentBitmap, new GrayscaleFilter());
            }

            if (filterType == "Roberts")
            {
                Bitmap result = GetMagnitudeBitmap(BaseKernel.RobertsX, BaseKernel.RobertsY, sourceBitmap);
                _docManager.OpenDocument(result, doc.Name + "_Roberts");

                if (sourceBitmap != doc.CurrentBitmap) sourceBitmap.Dispose();
                return;
            }

            if (filterType == "Sobel")
            {
                Bitmap result = GetMagnitudeBitmap(BaseKernel.SobelX, BaseKernel.SobelY, sourceBitmap);
                _docManager.OpenDocument(result, doc.Name + "_Sobel");

                if (sourceBitmap != doc.CurrentBitmap) sourceBitmap.Dispose();
                return;
            }

            if (filterType == "Canny")
            {
                using (Bitmap grayImage =
                       _processor.CreateBitmapFromPixelArray(doc.CurrentBitmap, new GrayscaleFilter()))
                {
                    using (Bitmap blurredImage =
                           _processor.ApplyFilterKernel(grayImage, BaseKernel.GaussianBlur, 1.0 / 256.0, 0))
                    {
                        using (Bitmap magnitude =
                               GetMagnitudeBitmap(BaseKernel.SobelX, BaseKernel.SobelY, blurredImage))
                        {
                            if (magnitude != null)
                            {
                                Bitmap cannyResult = FilterHelper.ApplyThreshold(magnitude, 50);

                                _docManager.OpenDocument(cannyResult, doc.Name + "_Canny");
                            }
                        }
                    }
                }

                return;
            }

            double[,] kernel = null;
            double factor = 1.0;
            int bias = 0;

            switch (filterType)
            {
                case "Mozaik":
                    kernel = BaseKernel.Mozaik;
                    factor = 1.0 / 25.0; // Normalisasi untuk kernel 5x5
                    break;
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

        public void ApplyCustomConvolution(double[,] kernel)
        {
            ImageDocument doc = _docManager.GetActiveDocument();
            if (doc == null) return;

            Bitmap result = _processor.ApplyConvolution(doc.CurrentBitmap, kernel);

            if (result != null)
            {
                _docManager.OpenDocument(result, doc.Name + "_CustomKernel");
            }
        }

        private Bitmap GetMagnitudeBitmap(double[,] kernelX, double[,] kernelY, Bitmap sourceImage)
        {
            Bitmap bmpX = _processor.ApplyConvolution(sourceImage, kernelX);
            Bitmap bmpY = _processor.ApplyConvolution(sourceImage, kernelY);

            if (bmpX != null && bmpY != null)
            {
                return FilterHelper.CalculateMagnitude(bmpX, bmpY);
            }

            return null;
        }

        public void ApplyHistogramEqualization(TabPage activeTab)
        {
            var doc = _docManager.GetActiveDocument();
            if (doc == null) return;

            var filter = new HistogramEqualizationFilter();

            Bitmap result = filter.Apply(doc.CurrentBitmap);

            if (result != null)
            {
                _docManager.OpenDocument(result, doc.Name + "_Equalized");
            }
        }
        
        public void ApplyAdaptiveHistogramEqualization(TabPage activeTab)
        {
            var doc = _docManager.GetActiveDocument();
            if (doc == null) return;
            
            var filter = new AdaptiveHistogramEqualizationFilter();
            Bitmap result = filter.Apply(doc.CurrentBitmap);

            if (result != null)
            {
                _docManager.OpenDocument(result, doc.Name + "_AdaptiveEq");
            }
        }
        
        public void ApplyLinearStretchEqualization(TabPage activeTab)
        {
            var doc = _docManager.GetActiveDocument();
            if (doc == null) return;

            var filter = new LinearStretchFilter();
            Bitmap result = filter.Apply(doc.CurrentBitmap);

            if (result != null)
            {
                _docManager.OpenDocument(result, doc.Name + "_LinearStretch");
            }
        }
        // Tambahkan metode ini di dalam class FilterController
        public void ApplySmoothing()
        {
            var doc = _docManager.GetActiveDocument();
            if (doc == null) return;
            // Menggunakan Gaussian Blur dengan faktor normalisasi 1/256
            Bitmap result = _processor.ApplyFilterKernel(doc.CurrentBitmap, BaseKernel.GaussianBlur, 1.0 / 256.0, 0);
            _docManager.OpenDocument(result, doc.Name + "_Smoothed");
        }

        public void ApplySharpening()
        {
            var doc = _docManager.GetActiveDocument();
            if (doc == null) return;
            // Menggunakan kernel Sharpen
            Bitmap result = _processor.ApplyFilterKernel(doc.CurrentBitmap, BaseKernel.Sharpen, 1.0, 0);
            _docManager.OpenDocument(result, doc.Name + "_Sharpened");
        }

        public void ApplyContrastStretchPoint()
        {
            var doc = _docManager.GetActiveDocument();
            if (doc == null) return;

            // Logic untuk Peregangan Kontras Global (Point Processing)
            var filter = new LinearStretchFilter();
            Bitmap result = filter.Apply(doc.CurrentBitmap);

            _docManager.OpenDocument(result, doc.Name + "_PointStretch");
        }
        public void ApplyPseudoColor()
        {
            var doc = _docManager.GetActiveDocument();
            if (doc == null) return;
            // Menerapkan filter Pseudo Color melalui metode ApplyFilter yang sudah ada
            ApplyFilter(new PseudoColorFilter(), null); //
        }
    }
}