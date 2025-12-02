using System;
using System.Windows.Forms;
using MiniPhotoShop.Managers;
using MiniPhotoShop.Services;

namespace MiniPhotoShop
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IImageProcessingService imageProcessorService = new ImageProcessingService();
            IImageArithmeticService arithmeticService = new ImageArithmeticService(); 
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
                arithmeticService,
                dialogService
            );

            Application.Run(mainForm);
        }
    }
}