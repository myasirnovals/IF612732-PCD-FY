using System;
using System.Drawing;
using System.Linq.Expressions;
using System.Windows.Forms;
using System.IO;

namespace MiniPhotoShop
{
    public partial class Form1 : Form
    {
        private enum ChannelMode
        {
            RGB,
            Grayscale,
            Red,
            Green,
            Blue
        }

        public Form1()
        {
            InitializeComponent();

            if (tabControlCanvas.TabPages.Count > 0)
            {
                tabControlCanvas.TabPages.Clear();
            }

            InitializeThumbnails();
        }

        private void InitializeThumbnails()
        {
            var imageResources = new[]
            {
                new { Name = "yuruyuri", Image = Properties.Resources.yuruyuri },
                new { Name = "aurora", Image = Properties.Resources.aurora },
                new { Name = "haikyuu", Image = Properties.Resources.haikyuu },
                new { Name = "ruri", Image = Properties.Resources.ruri },
            };

            foreach (var resource in imageResources)
            {
                PictureBox thumb = new PictureBox();
                thumb.Image = resource.Image;
                thumb.Tag = resource.Name;
                thumb.Size = new Size(120, 100);
                thumb.SizeMode = PictureBoxSizeMode.Zoom;
                thumb.BorderStyle = BorderStyle.FixedSingle;
                thumb.Cursor = Cursors.Hand;
                thumb.Margin = new Padding(10);

                thumb.Click += Thumbnail_Click;

                flowLayoutPanelThumbnails.Controls.Add(thumb);
            }
        }

        public void Thumbnail_Click(object sender, EventArgs e)
        {
            PictureBox clickedThumbnail = sender as PictureBox;
            if (clickedThumbnail == null) return;

            ProcessAndDisplayImage(clickedThumbnail.Image, clickedThumbnail.Tag.ToString());
        }

        private TabPage AddNewTab(string tabTitle)
        {
            TabPage newTabPage = new TabPage(tabTitle);
            newTabPage.Padding = new Padding(3);
            newTabPage.UseVisualStyleBackColor = true;

            PictureBox newCanvas = new PictureBox();
            newCanvas.BackColor = Color.White;
            newCanvas.BorderStyle = BorderStyle.FixedSingle;
            newCanvas.Dock = DockStyle.Fill;
            newCanvas.SizeMode = PictureBoxSizeMode.Zoom;

            newTabPage.Controls.Add(newCanvas);
            tabControlCanvas.TabPages.Add(newTabPage);
            tabControlCanvas.SelectedTab = newTabPage;

            return newTabPage;
        }

        private int[,,] CreatePixelArrayFromImage(Image image)
        {
            if (image == null) return null;

            Bitmap bmp = new Bitmap(image);
            int width = bmp.Width;
            int height = bmp.Height;
            int[,,] pixelArray = new int[width, height, 4];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    pixelArray[x, y, 0] = c.R;
                    pixelArray[x, y, 1] = c.G;
                    pixelArray[x, y, 2] = c.B;
                    pixelArray[x, y, 3] = (int)((c.R * 0.3) + (c.G * 0.59) + (c.B * 0.11));
                }
            }

            return pixelArray;
        }

        private void UpdateCanvasFromPixelArray(PictureBox canvas, int[,,] pixelArray,
            ChannelMode mode = ChannelMode.RGB)
        {
            if (canvas == null || pixelArray == null) return;

            int width = pixelArray.GetLength(0);
            int height = pixelArray.GetLength(1);
            Bitmap bmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color c;
                    int r = pixelArray[x, y, 0];
                    int g = pixelArray[x, y, 1];
                    int b = pixelArray[x, y, 2];
                    int gray = pixelArray[x, y, 3];

                    switch (mode)
                    {
                        case ChannelMode.Red:
                            c = Color.FromArgb(r, 0, 0);
                            break;
                        case ChannelMode.Green:
                            c = Color.FromArgb(0, g, 0);
                            break;
                        case ChannelMode.Blue:
                            c = Color.FromArgb(0, 0, b);
                            break;
                        case ChannelMode.Grayscale:
                            c = Color.FromArgb(gray, gray, gray);
                            break;
                        case ChannelMode.RGB:
                        default:
                            c = Color.FromArgb(r, g, b);
                            break;
                    }

                    bmp.SetPixel(x, y, c);
                }
            }

            canvas.Image = bmp;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox activeCanvas = GetActiveCanvas();
            int[,,] pixelArray = GetActivePixelArray();

            if (activeCanvas == null || activeCanvas.Image == null || pixelArray == null)
            {
                MessageBox.Show("Tidak ada gambar aktif untuk disimpan.", "info", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text File (*.txt)|*.txt";
                saveFileDialog.Title = "Simpan Data Pixel";
                saveFileDialog.FileName = tabControlCanvas.SelectedTab.Text + "_pixels.txt";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    try
                    {
                        int width = pixelArray.GetLength(0);
                        int height = pixelArray.GetLength(1);

                        Bitmap bmp = new Bitmap(activeCanvas.Image);
                        bool isGrayScale = true;
                        for (int y = 0; y < bmp.Height; y++)
                        {
                            for (int x = 0; x < bmp.Width; x++)
                            {
                                Color c = bmp.GetPixel(x, y);
                                if (c.R != c.G || c.G != c.B)
                                {
                                    isGrayScale = false;
                                    break;
                                }
                            }

                            if (!isGrayScale) break;
                        }

                        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath))
                        {
                            for (int y = 0; y < height; y++)
                            {
                                for (int x = 0; x < width; x++)
                                {
                                    if (isGrayScale)
                                    {
                                        writer.Write(pixelArray[x, y, 3].ToString());
                                    }
                                    else
                                    {
                                        writer.Write(
                                            $"({pixelArray[x, y, 0]}, {pixelArray[x, y, 1]}, {pixelArray[x, y, 2]})");
                                    }

                                    if (x < width - 1)
                                    {
                                        writer.Write(" ");
                                    }
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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Buka Gambar";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Image loadedImage = Image.FromFile(ofd.FileName);
                        string fileName = Path.GetFileNameWithoutExtension(ofd.FileName);
                        ProcessAndDisplayImage(loadedImage, fileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Gagal membuka file gambar: {ex.Message}", "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ProcessAndDisplayImage(Image image, string imageName)
        {
            if (image == null) return;

            TabPage newTab = AddNewTab(imageName);
            PictureBox activeCanvas = newTab.Controls[0] as PictureBox;

            if (activeCanvas != null)
            {
                activeCanvas.Image = image;
                activeCanvas.Tag = image.Clone();
                newTab.Tag = CreatePixelArrayFromImage(image);
            }
        }

        private PictureBox GetActiveCanvas()
        {
            if (tabControlCanvas.SelectedTab != null && tabControlCanvas.SelectedTab.Controls.Count > 0)
            {
                return tabControlCanvas.SelectedTab.Controls[0] as PictureBox;
            }

            return null;
        }

        private int[,,] GetActivePixelArray()
        {
            if (tabControlCanvas.SelectedTab != null)
            {
                return tabControlCanvas.SelectedTab.Tag as int[,,];
            }

            return null;
        }


        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fungsi CUT belum diimplementasikan.");
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox currentCanvas = GetActiveCanvas();
            if (currentCanvas != null && currentCanvas.Image != null)
            {
                try
                {
                    Clipboard.SetImage(currentCanvas.Image);
                    MessageBox.Show("Gambar telah disalin ke Clipboard.", "Copy", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Gagal menyalin gambar: {ex.Message}", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Tidak ada gambar untuk disalin.", "Copy", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox currentCanvas = GetActiveCanvas();
            if (currentCanvas != null && Clipboard.ContainsImage())
            {
                Image pastedImage = Clipboard.GetImage();
                currentCanvas.Image = pastedImage;

                if (tabControlCanvas.SelectedTab != null)
                {
                    tabControlCanvas.SelectedTab.Tag = CreatePixelArrayFromImage(pastedImage);
                }

                MessageBox.Show("Gambar ditempelkan dari Clipboard.", "Paste", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Clipboard tidak berisi gambar atau tidak ada canvas aktif.", "Paste",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox currentCanvas = GetActiveCanvas();
            if (currentCanvas != null && currentCanvas.Image != null)
            {
                currentCanvas.Image = null;

                if (tabControlCanvas.SelectedTab != null)
                {
                    tabControlCanvas.SelectedTab.Tag = null;
                }

                MessageBox.Show("Canvas berhasil dikosongkan.", "Clear", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Tidak ada gambar yang perlu dihapus.", "Clear", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox activeCanvas = GetActiveCanvas();

            if (activeCanvas != null && activeCanvas.Tag is Image)
            {
                Image originalImage = (Image)activeCanvas.Tag;

                int[,,] newPixelArray = CreatePixelArrayFromImage(originalImage);
                if (tabControlCanvas.SelectedTab != null)
                {
                    tabControlCanvas.SelectedTab.Tag = newPixelArray;
                }

                UpdateCanvasFromPixelArray(activeCanvas, newPixelArray, ChannelMode.RGB);

                MessageBox.Show("Gambar telah dikembalikan ke kondisi semula.", "Restore", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Tidak ada gambar asli untuk dipulihkan atau tidak ada gambar aktif di canvas",
                    "Restore", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox activeCanvas = GetActiveCanvas();
            int[,,] pixelArray = GetActivePixelArray();

            if (activeCanvas == null || activeCanvas.Image == null || pixelArray == null)
            {
                MessageBox.Show("Tidak ada gambar untuk difilter.", "Info", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            try
            {
                UpdateCanvasFromPixelArray(activeCanvas, pixelArray, ChannelMode.Grayscale);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat menerapkan filter: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControlCanvas_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                var tabPage = this.tabControlCanvas.TabPages[e.Index];
                var tabRect = this.tabControlCanvas.GetTabRect(e.Index);
                tabRect.Inflate(-2, -2);

                TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font, tabRect, tabPage.ForeColor,
                    TextFormatFlags.Left);

                Rectangle closeButton = new Rectangle(tabRect.Right - 15, tabRect.Top + 4, 12, 12);
                ControlPaint.DrawCaptionButton(e.Graphics, closeButton, CaptionButton.Close, ButtonState.Normal);

                e.DrawFocusRectangle();
            }
            catch (Exception)
            {
            }
        }

        private void tabControlCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < this.tabControlCanvas.TabPages.Count; i++)
            {
                Rectangle tabRect = this.tabControlCanvas.GetTabRect(i);
                tabRect.Inflate(-2, -2);
                Rectangle closeButton = new Rectangle(tabRect.Right - 15, tabRect.Top + 4, 12, 12);

                if (closeButton.Contains(e.Location))
                {
                    if (this.tabControlCanvas.TabPages[i].Tag != null)
                    {
                        this.tabControlCanvas.TabPages[i].Tag = null;
                    }

                    this.tabControlCanvas.TabPages.RemoveAt(i);
                    break;
                }
            }
        }

        private void ApplyChannelFilter(ChannelMode mode)
        {
            PictureBox activeCanvas = GetActiveCanvas();
            int[,,] pixelArray = GetActivePixelArray();

            if (activeCanvas == null || activeCanvas.Image == null || pixelArray == null)
            {
                MessageBox.Show("Tidak ada gambar untuk difilter.", "Info", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            try
            {
                UpdateCanvasFromPixelArray(activeCanvas, pixelArray, mode);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat menerapkan filter: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRed_Click(object sender, EventArgs e)
        {
            ApplyChannelFilter(ChannelMode.Red);
        }

        private void buttonGreen_Click(object sender, EventArgs e)
        {
            ApplyChannelFilter(ChannelMode.Green);
        }

        private void buttonBlue_Click(object sender, EventArgs e)
        {
            ApplyChannelFilter(ChannelMode.Blue);
        }

        private void BtnHistogramClick(object sender, EventArgs e)
        {
            DisplayHistogram();
        }

        private void DisplayHistogram()
        {
            int[,,] pixelArray = GetActivePixelArray();
            if (pixelArray == null)
            {
                ClearHistogram();
                return;
            }

            try
            {
                int[] redCounts = new int[256];
                int[] greenCounts = new int[256];
                int[] blueCounts = new int[256];
                int[] grayCounts = new int[256];

                int width = pixelArray.GetLength(0);
                int height = pixelArray.GetLength(1);

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        redCounts[pixelArray[x, y, 0]]++;
                        greenCounts[pixelArray[x, y, 1]]++;
                        blueCounts[pixelArray[x, y, 2]]++;
                        grayCounts[pixelArray[x, y, 3]]++;
                    }
                }

                int maxCount = 0;
                for (int i = 0; i < 256; i++)
                {
                    if (redCounts[i] > maxCount) maxCount = redCounts[i];
                    if (greenCounts[i] > maxCount) maxCount = greenCounts[i];
                    if (blueCounts[i] > maxCount) maxCount = blueCounts[i];
                    if (grayCounts[i] > maxCount) maxCount = grayCounts[i];
                }

                if (maxCount == 0) maxCount = 1;

                pictureBoxRedHistogram.Image = DrawHistogram(pictureBoxRedHistogram.Width,
                    pictureBoxRedHistogram.Height, redCounts, maxCount, Color.Red);
                pictureBoxGreenHistogram.Image = DrawHistogram(pictureBoxGreenHistogram.Width,
                    pictureBoxGreenHistogram.Height, greenCounts, maxCount, Color.Green);
                pictureBoxBlueHistogram.Image = DrawHistogram(pictureBoxBlueHistogram.Width,
                    pictureBoxBlueHistogram.Height, blueCounts, maxCount, Color.Blue);
                pictureBoxGrayHistogram.Image = DrawHistogram(pictureBoxGrayHistogram.Width,
                    pictureBoxGrayHistogram.Height, grayCounts, maxCount, Color.Gray);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat membuat histogram: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Bitmap DrawHistogram(int width, int height, int[] counts, int maxCount, Color barColor)
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

        private void ClearHistogram()
        {
            pictureBoxRedHistogram.Image = null;
            pictureBoxGreenHistogram.Image = null;
            pictureBoxBlueHistogram.Image = null;
            pictureBoxGrayHistogram.Image = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void tabControlCanvas_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearHistogram();
        }

        private void tableDataToolStripMenuItem_Click (object sender, EventArgs e)
        {

        }


    }
}