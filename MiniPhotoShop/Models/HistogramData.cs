namespace MiniPhotoShop.Models
{
    public class HistogramData
    {
        public int[] RedCounts { get; set; } = new int[256];
        public int[] GreenCounts { get; set; } = new int[256];
        public int[] BlueCounts { get; set; } = new int[256];
        public int[] GrayCounts { get; set; } = new int[256];
        public int MaxCount { get; set; } = 1;
    }
}