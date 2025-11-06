using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MiniPhotoShop.Models;
using System;

namespace MiniPhotoShop.Services
{
    public class ImageFileService : IImageFileService
    {
        public Bitmap OpenImage(out string fileName)
        {
            fileName = null;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Buka Gambar";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        fileName = Path.GetFileNameWithoutExtension(ofd.FileName);
                        return new Bitmap(Image.FromFile(ofd.FileName));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Gagal membuka file gambar: {ex.Message}", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return null;
                    }
                }

                return null;
            }
        }
    }

    public class DataExportService : IDataExportService
    {
        private string ToBinaryString(int value)
        {
            return Convert.ToString(value, 2).PadLeft(8, '0');
        }

        public void SavePixelData(string fileName, int[,,] pixelArray, bool isGrayscale, bool outputAsBinary)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text File (*.txt)|*.txt";
                sfd.Title = "Simpan Data Pixel";
                sfd.FileName = $"{fileName}_pixels.txt";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        int width = pixelArray.GetLength(0);
                        int height = pixelArray.GetLength(1);

                        using (StreamWriter writer = new StreamWriter(sfd.FileName))
                        {
                            for (int y = 0; y < height; y++)
                            {
                                for (int x = 0; x < width; x++)
                                {
                                    if (outputAsBinary)
                                    {
                                        if (isGrayscale)
                                        {
                                            writer.Write(ToBinaryString(pixelArray[x, y, 3]));
                                        }
                                        else
                                        {
                                            writer.Write(
                                                $"({ToBinaryString(pixelArray[x, y, 0])}, {ToBinaryString(pixelArray[x, y, 1])}, {ToBinaryString(pixelArray[x, y, 2])})");
                                        }
                                    }
                                    else
                                    {
                                        if (isGrayscale)
                                        {
                                            writer.Write(pixelArray[x, y, 3].ToString());
                                        }
                                        else
                                        {
                                            writer.Write(
                                                $"({pixelArray[x, y, 0]}, {pixelArray[x, y, 1]}, {pixelArray[x, y, 2]})");
                                        }
                                    }

                                    if (x < width - 1) writer.Write(" ");
                                }

                                writer.WriteLine();
                            }
                        }

                        MessageBox.Show("Data pixel berhasil disimpan!", "Sukses", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Terjadi kesalahan saat menyimpan file: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void SaveHistogramData(string fileName, HistogramData histogram)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text File (*.txt)|*.txt";
                sfd.Title = "Simpan Tabel Data Pixel";
                sfd.FileName = $"{fileName}_tabel_data.txt";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter writer = new StreamWriter(sfd.FileName))
                        {
                            writer.WriteLine("Tabel Data Histogram");
                            writer.WriteLine("===============================================================");
                            writer.WriteLine(
                                $"| {"I",-3} | {"Red",-10} | {"Green",-10} | {"Blue",-10} | {"Gray",-10} |");
                            writer.WriteLine("|-----|------------|------------|------------|------------|");

                            for (int i = 0; i < 256; i++)
                            {
                                string line =
                                    $"| {i,-3} | {histogram.RedCounts[i],-10} | {histogram.GreenCounts[i],-10} | {histogram.BlueCounts[i],-10} | {histogram.GrayCounts[i],-10} |";
                                writer.WriteLine(line);
                            }

                            writer.WriteLine("===============================================================");
                        }

                        MessageBox.Show("Tabel data berhasil disimpan!", "Sukses", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Gagal menyimpan tabel data: {ex.Message}", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}