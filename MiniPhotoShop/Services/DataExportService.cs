using System;
using System.IO;
using MiniPhotoShop.Models;

namespace MiniPhotoShop.Services
{
    public class DataExportService : IDataExportService
    {
        private string ToBinaryString(int value)
        {
            return Convert.ToString(value, 2).PadLeft(8, '0');
        }

        public void SavePixelData(string filePath, int[,,] pixelArray, bool isGrayscale, bool outputAsBinary)
        {
            int width = pixelArray.GetLength(0);
            int height = pixelArray.GetLength(1);

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        string dataPoint;
                        if (outputAsBinary)
                        {
                            dataPoint = isGrayscale
                                ? ToBinaryString(pixelArray[x, y, 3])
                                : $"({ToBinaryString(pixelArray[x, y, 0])}, {ToBinaryString(pixelArray[x, y, 1])}, {ToBinaryString(pixelArray[x, y, 2])})";
                        }
                        else
                        {
                            dataPoint = isGrayscale
                                ? pixelArray[x, y, 3].ToString()
                                : $"({pixelArray[x, y, 0]}, {pixelArray[x, y, 1]}, {pixelArray[x, y, 2]})";
                        }

                        writer.Write(dataPoint);
                        if (x < width - 1) writer.Write(" ");
                    }
                    writer.WriteLine();
                }
            }
        }

        public void SaveHistogramData(string filePath, HistogramData histogram)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Tabel Data Histogram");
                writer.WriteLine("===============================================================");
                writer.WriteLine($"| {"I",-3} | {"Red",-10} | {"Green",-10} | {"Blue",-10} | {"Gray",-10} |");
                writer.WriteLine("|-----|------------|------------|------------|------------|");

                for (int i = 0; i < 256; i++)
                {
                    string line = $"| {i,-3} | {histogram.RedCounts[i],-10} | {histogram.GreenCounts[i],-10} | {histogram.BlueCounts[i],-10} | {histogram.GrayCounts[i],-10} |";
                    writer.WriteLine(line);
                }

                writer.WriteLine("===============================================================");
            }
        }
    }
}