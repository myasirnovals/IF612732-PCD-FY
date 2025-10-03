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

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewTab("Untitled");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    AddNewTab(Path.GetFileName(filePath));
                    
                    if (tabControlCanvas.SelectedTab != null)
                    {
                        PictureBox currentCanvas = tabControlCanvas.SelectedTab.Controls[0] as PictureBox;

                        if (currentCanvas != null)
                        {
                            try
                            {
                                
                                currentCanvas.Image = Image.FromFile(filePath);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Gagal membuka file gambar.\nError: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Tambahkan logika untuk menyimpan. 
            MessageBox.Show("Fungsi Save belum diimplementasikan.");
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    // TODO: Tambahkan logika untuk menyimpan gambar dari canvas ke filePath
                    MessageBox.Show($"Menyimpan file ke: {filePath}");
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
                    MessageBox.Show("Gambar telah disalin ke Clipboard.", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Gagal menyalin gambar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Tidak ada gambar untuk disalin.", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox currentCanvas = GetActiveCanvas();
            if (currentCanvas != null && Clipboard.ContainsImage())
            {
                
                currentCanvas.Image = Clipboard.GetImage();
                MessageBox.Show("Gambar ditempelkan dari Clipboard.", "Paste", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Clipboard tidak berisi gambar atau tidak ada canvas aktif.", "Paste", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox currentCanvas = GetActiveCanvas();
            if (currentCanvas != null && currentCanvas.Image != null)
            {
                
                currentCanvas.Image = null;
                MessageBox.Show("Canvas berhasil dikosongkan.", "Clear", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Tidak ada gambar yang perlu dihapus.", "Clear", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
