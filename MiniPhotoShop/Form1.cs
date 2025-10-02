using System;
using System.Drawing;
using System.Windows.Forms;


namespace MiniPhotoShop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            AddNewTab("Canvas 1");
            AddNewTab("Canvas 2");
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
            newCanvas.TabStop = false;

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
                    // TODO: Tambahkan logika untuk memuat gambar dari filePath ke canvas
                    MessageBox.Show($"Membuka file: {filePath}");
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


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
