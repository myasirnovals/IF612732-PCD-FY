using System;
using System.Windows.Forms;
using MiniPhotoShop.Managers;
using MiniPhotoShop.Services;

namespace MiniPhotoShop
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            ImageProcessingService imageProcessorService = new ImageProcessingService();
            IImageFileService imageFileService = new ImageFileService();
            IDataExportService dataExportService = new DataExportService();
            IDialogService dialogService = new DialogService();
            
            var documentManager = new DocumentManager(imageProcessorService);
            var thumbnailManager = new ThumbnailManager();
            
            var mainForm = new Form1(
                documentManager,
                thumbnailManager,
                imageFileService,
                dataExportService,
                imageProcessorService,
                imageProcessorService,
                dialogService
            );
            
            Application.Run(mainForm);
        }
    }
}