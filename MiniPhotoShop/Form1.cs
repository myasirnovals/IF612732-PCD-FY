using System;
using System.Drawing;
using System.Linq.Expressions;
using System.Windows.Forms;


namespace MiniPhotoShop
{
    public partial class Form1 : Form
    {
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

            string imageName = clickedThumbnail.Tag.ToString();

            AddNewTab(imageName);

            PictureBox activeCanvas = GetActiveCanvas();
            if (activeCanvas != null)
            {
                activeCanvas.Image = clickedThumbnail.Image;
                activeCanvas.Tag = clickedThumbnail.Image.Clone();
            }
        }

        private void AddNewTab(string tabTitle)
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
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox activeCanvas = GetActiveCanvas();
            if (activeCanvas == null || activeCanvas.Image == null)
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

                            if (!isGrayScale)
                            {
                                break;
                            }
                        }

                        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath))
                        {
                            for (int y = 0; y < bmp.Height; y++)
                            {
                                for (int x = 0; x < bmp.Width; x++)
                                {
                                    Color pixelColor = bmp.GetPixel(x, y);

                                    if (isGrayScale)
                                    {
                                        writer.Write(pixelColor.R.ToString());
                                    }
                                    else
                                    {
                                        writer.Write($"({pixelColor.R}, {pixelColor.G}, {pixelColor.B})");
                                    }

                                    if (x < bmp.Width - 1)
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

        private PictureBox GetActiveCanvas()
        {
            if (tabControlCanvas.SelectedTab != null && tabControlCanvas.SelectedTab.Controls.Count > 0)
            {
                return tabControlCanvas.SelectedTab.Controls[0] as PictureBox;
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
                currentCanvas.Image = Clipboard.GetImage();
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
                activeCanvas.Image = (Image)activeCanvas.Tag;

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
            if (activeCanvas == null || activeCanvas.Image == null)
            {
                MessageBox.Show("Tidak ada gambar untuk difilter.", "Info", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            try
            {
                Bitmap originalBmp = new Bitmap(activeCanvas.Image);
                Bitmap grayscaleBmp = new Bitmap(originalBmp.Width, originalBmp.Height);

                for (int y = 0; y < originalBmp.Height; y++)
                {
                    for (int x = 0; x < originalBmp.Width; x++)
                    {
                        Color originalColor = originalBmp.GetPixel(x, y);

                        int grayScale =
                            (int)((originalColor.R * 0.3) + originalColor.G * 0.59 + originalColor.B * 0.11);

                        Color grayColor = Color.FromArgb(grayScale, grayScale, grayScale);

                        grayscaleBmp.SetPixel(x, y, grayColor);
                    }
                }

                activeCanvas.Image = grayscaleBmp;
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
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat menutup gambar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    this.tabControlCanvas.TabPages.RemoveAt(i);
                    break;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void tabControlCanvas_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}