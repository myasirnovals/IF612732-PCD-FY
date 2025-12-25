namespace MiniPhotoShop.Filters.Base
{
    public static class BaseKernel
    {
        // --- KERNEL BARU YANG ANDA TAMBAHKAN ---

        // Kernel Mozaik / Mean Filter (5x5)
        public static readonly double[,] Mozaik = {
            { 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1 }
        };


        public static readonly double[,] Identity = {
            { 0, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 0 }
        };

        public static readonly double[,] GaussianBlur = {
            { 1,  4,  6,  4, 1 },
            { 4, 16, 24, 16, 4 },
            { 6, 24, 36, 24, 6 },
            { 4, 16, 24, 16, 4 },
            { 1,  4,  6,  4, 1 }
        };

        public static readonly double[,] Sharpen = {
            { 0, -1, 0 },
            { -1, 5, -1 },
            { 0, -1, 0 }
        };

        public static readonly double[,] Emboss = {
            { -2, -1, 0 },
            { -1, 1, 1 },
            { 0, 1, 2 }
        };

        public static readonly double[,] RobertsX = {
            { 0, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, -1 }
        };

        public static readonly double[,] RobertsY = {
            { 0, 0, 0 },
            { 0, 0, 1 },
            { 0, -1, 0 }
        };

        public static readonly double[,] SobelX = {
            { -1, 0, 1 },
            { -2, 0, 2 },
            { -1, 0, 1 }
        };

        public static readonly double[,] SobelY = {
            { -1, -2, -1 },
            { 0, 0, 0 },
            { 1, 2, 1 }
        };
    }
}