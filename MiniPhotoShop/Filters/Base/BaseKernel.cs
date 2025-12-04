namespace MiniPhotoShop.Filters.Base
{
    public static class BaseKernel
    {
        public static double[,] Identity => new double[,]
        {
            { 0, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 0 }
        };

        public static double[,] GaussianBlur => new double[,]
        {
            { 1,  4,  6,  4, 1 },
            { 4, 16, 24, 16, 4 },
            { 6, 24, 36, 24, 6 },
            { 4, 16, 24, 16, 4 },
            { 1,  4,  6,  4, 1 }
        };

        public static double[,] Sharpen => new double[,]
        {
            { 0, -1, 0 },
            { -1, 5, -1 },
            { 0, -1, 0 }
        };

        public static double[,] EdgeDetection => new double[,]
        {
            { -1, -1, -1 },
            { -1, 8, -1 },
            { -1, -1, -1 }
        };

        public static double[,] Emboss => new double[,]
        {
            { -2, -1, 0 },
            { -1, 1, 1 },
            { 0, 1, 2 }
        };
    }
}