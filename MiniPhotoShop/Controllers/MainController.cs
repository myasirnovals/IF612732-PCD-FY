using System.Windows.Forms;
using MiniPhotoShop.Managers;
using MiniPhotoShop.Models;
using MiniPhotoShop.Services;
using MiniPhotoShop.Filters;

namespace MiniPhotoShop.Controllers
{
    public class MainController
    {
        public FileController Files { get; private set; }
        public FilterController Filters { get; private set; }
        public ArithmeticController Arithmetic { get; private set; }
        public TransformController Transforms { get; private set; }
        public DocumentController Documents { get; private set; }
        public ThumbnailController Thumbnails { get; private set; }

        public MainController(
            DocumentManager docManager,
            DocumentController docController,
            ThumbnailManager thumbManager,
            ThumbnailController thumbController,
            IImageFileService fileService,
            IDataExportService exportService,
            IImageProcessingService procService,
            IImageArithmeticService arithService,
            IDialogService dialogService)
        {
            Documents = docController;
            Files = new FileController(docManager, thumbManager, fileService, exportService);
            Filters = new FilterController(docManager);
            Arithmetic = new ArithmeticController(docManager, thumbManager, arithService);
            Transforms = new TransformController(docManager, procService);
            Thumbnails = thumbController;
        }
    }
}