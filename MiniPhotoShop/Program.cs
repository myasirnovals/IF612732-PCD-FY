using System;
using System.Windows.Forms;
using MiniPhotoShop.Managers;
using MiniPhotoShop.Services;
using MiniPhotoShop.Controllers;
using MiniPhotoShop.Views;

namespace MiniPhotoShop
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var imageProcessorService = new ImageProcessingService();
            var arithmeticService = new ImageArithmeticService();
            var imageFileService = new ImageFileService();
            var dataExportService = new DataExportService();
            var dialogService = new DialogService();

            var documentManager = new DocumentManager();
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