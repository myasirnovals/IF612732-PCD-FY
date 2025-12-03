using System.Drawing;
using MiniPhotoShop.Models;

namespace MiniPhotoShop.Services.Processing.Analysis
{
    public class HistogramCalculator
    {
        public HistogramData Calculate(int[,,] pixelArray)
        {
            var data = new HistogramData();
            if (pixelArray == null) return data;
            
            int width = pixelArray.GetLength(0);
            int height = pixelArray.GetLength(1);
            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    data.RedCounts[pixelArray[x, y, 0]]++;
                    data.GreenCounts[pixelArray[x, y, 1]]++;
                    data.BlueCounts[pixelArray[x, y, 2]]++;
                    data.GrayCounts[pixelArray[x, y, 3]]++;
                }
            }
            
            int maxCount = 0;
            for (int i = 0; i < 256; i++)
            {
                if (data.RedCounts[i] > maxCount) maxCount = data.RedCounts[i];
                if (data.GreenCounts[i] > maxCount) maxCount = data.GreenCounts[i];
                if (data.BlueCounts[i] > maxCount) maxCount = data.BlueCounts[i];
                if (data.GrayCounts[i] > maxCount) maxCount = data.GrayCounts[i];
            }

            data.MaxCount = (maxCount == 0) ? 1 : maxCount;
            return data;
        }

        public Bitmap Draw(int width, int height, int[] counts, int maxCount, Color barColor)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                Pen pen = new Pen(barColor);
                float barWidth = (float)width / 256;
                for (int i = 0; i < 256; i++)
                {
                    float barHeight = ((float)counts[i] / maxCount) * height;
                    float xPos = i * barWidth;
                    g.DrawLine(pen, xPos, height, xPos, height - barHeight);
                }
            }
            return bmp;
        }
    }
}