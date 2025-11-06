using System.Windows.Forms;

namespace MiniPhotoShop
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            saveToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            cutToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            clearToolStripMenuItem = new ToolStripMenuItem();
            filterToolStripMenuItem = new ToolStripMenuItem();
            grayscaleToolStripMenuItem = new ToolStripMenuItem();
            negationToolStripMenuItem = new ToolStripMenuItem();
            notToolStripMenuItem = new ToolStripMenuItem();
            redToolStripMenuItem = new ToolStripMenuItem();
            greenToolStripMenuItem = new ToolStripMenuItem();
            blueToolStripMenuItem = new ToolStripMenuItem();
            perhitunganToolStripMenuItem = new ToolStripMenuItem();
            penjumlahanToolStripMenuItem = new ToolStripMenuItem();
            penguranganToolStripMenuItem = new ToolStripMenuItem();
            citraBinerToolStripMenuItem = new ToolStripMenuItem();
            andToolStripMenuItem = new ToolStripMenuItem();
            orToolStripMenuItem = new ToolStripMenuItem();
            xorToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            bwToolStripMenuItem = new ToolStripMenuItem();
            brightnessToolStripMenuItem = new ToolStripMenuItem();
            imageSelectionToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            restoreToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            tableDataToolStripMenuItem = new ToolStripMenuItem();
            histogramToolStripMenuItem = new ToolStripMenuItem();
            savePixelDataToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            tabControlCanvas = new TabControl();
            flowLayoutPanelThumbnails = new FlowLayoutPanel();
            panelHistogram = new Panel();
            labelGray = new Label();
            pictureBoxGrayHistogram = new PictureBox();
            labelBlue = new Label();
            pictureBoxBlueHistogram = new PictureBox();
            labelGreen = new Label();
            pictureBoxGreenHistogram = new PictureBox();
            labelRed = new Label();
            pictureBoxRedHistogram = new PictureBox();
            panelMainArea = new Panel();
            operasiAritmatikaToolStripMenuItem = new ToolStripMenuItem();
            tambahToolStripMenuItem = new ToolStripMenuItem();
            kurangToolStripMenuItem = new ToolStripMenuItem();
            bagiToolStripMenuItem = new ToolStripMenuItem();
            kaliToolStripMenuItem = new ToolStripMenuItem();
            konstantaToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            panelHistogram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGrayHistogram).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxBlueHistogram).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGreenHistogram).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRedHistogram).BeginInit();
            panelMainArea.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, filterToolStripMenuItem, viewToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1000, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, toolStripSeparator2, saveToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(40, 20);
            fileToolStripMenuItem.Text = "FILE";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            openToolStripMenuItem.Size = new Size(146, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(143, 6);
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveToolStripMenuItem.Size = new Size(146, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cutToolStripMenuItem, copyToolStripMenuItem, pasteToolStripMenuItem, clearToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(42, 20);
            editToolStripMenuItem.Text = "EDIT";
            // 
            // cutToolStripMenuItem
            // 
            cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            cutToolStripMenuItem.Size = new Size(102, 22);
            cutToolStripMenuItem.Text = "Cut";
            cutToolStripMenuItem.Click += cutToolStripMenuItem_Click;
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(102, 22);
            copyToolStripMenuItem.Text = "Copy";
            copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.Size = new Size(102, 22);
            pasteToolStripMenuItem.Text = "Paste";
            pasteToolStripMenuItem.Click += pasteToolStripMenuItem_Click;
            // 
            // clearToolStripMenuItem
            // 
            clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            clearToolStripMenuItem.Size = new Size(102, 22);
            clearToolStripMenuItem.Text = "Clear";
            clearToolStripMenuItem.Click += clearToolStripMenuItem_Click;
            // 
            // filterToolStripMenuItem
            // 
            filterToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                grayscaleToolStripMenuItem,
                negationToolStripMenuItem,
                redToolStripMenuItem,
                greenToolStripMenuItem,
                blueToolStripMenuItem,
                perhitunganToolStripMenuItem,
                citraBinerToolStripMenuItem,
                toolStripSeparator3,
                bwToolStripMenuItem,
                brightnessToolStripMenuItem,
                imageSelectionToolStripMenuItem,
                operasiAritmatikaToolStripMenuItem,
                toolStripSeparator1,
                restoreToolStripMenuItem
            });
            filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            filterToolStripMenuItem.Size = new Size(52, 20);
            filterToolStripMenuItem.Text = "FILTER";
            // 
            // grayscaleToolStripMenuItem
            // 
            grayscaleToolStripMenuItem.Name = "grayscaleToolStripMenuItem";
            grayscaleToolStripMenuItem.Size = new Size(180, 22);
            grayscaleToolStripMenuItem.Text = "Grayscale";
            grayscaleToolStripMenuItem.Click += grayscaleToolStripMenuItem_Click;
            // 
            // negationToolStripMenuItem
            // 
            negationToolStripMenuItem.Name = "negationToolStripMenuItem";
            negationToolStripMenuItem.Size = new Size(180, 22);
            negationToolStripMenuItem.Text = "Negation";
            negationToolStripMenuItem.Click += negationToolStripMenuItem_Click;
            // 
            // notToolStripMenuItem
            // 
            notToolStripMenuItem.Name = "notToolStripMenuItem";
            notToolStripMenuItem.Size = new Size(180, 22);
            notToolStripMenuItem.Text = "NOT (Bitwise)";
            notToolStripMenuItem.Click += notToolStripMenuItem_Click;
            // 
            // redToolStripMenuItem
            // 
            redToolStripMenuItem.Name = "redToolStripMenuItem";
            redToolStripMenuItem.Size = new Size(180, 22);
            redToolStripMenuItem.Text = "Red Channel";
            redToolStripMenuItem.Click += redToolStripMenuItem_Click;
            // 
            // greenToolStripMenuItem
            // 
            greenToolStripMenuItem.Name = "greenToolStripMenuItem";
            greenToolStripMenuItem.Size = new Size(180, 22);
            greenToolStripMenuItem.Text = "Green Channel";
            greenToolStripMenuItem.Click += greenToolStripMenuItem_Click;
            // 
            // blueToolStripMenuItem
            // 
            blueToolStripMenuItem.Name = "blueToolStripMenuItem";
            blueToolStripMenuItem.Size = new Size(180, 22);
            blueToolStripMenuItem.Text = "Blue Channel";
            blueToolStripMenuItem.Click += blueToolStripMenuItem_Click;
            // 
            // perhitunganToolStripMenuItem
            // 
            perhitunganToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                penjumlahanToolStripMenuItem,
                penguranganToolStripMenuItem});
            perhitunganToolStripMenuItem.Name = "perhitunganToolStripMenuItem";
            perhitunganToolStripMenuItem.Size = new Size(180, 22);
            perhitunganToolStripMenuItem.Text = "Perhitungan";
            // 
            // citraBinerToolStripMenuItem
            // 
            citraBinerToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                notToolStripMenuItem, 
                andToolStripMenuItem,
                orToolStripMenuItem,
                xorToolStripMenuItem});
            citraBinerToolStripMenuItem.Name = "citraBinerToolStripMenuItem";
            citraBinerToolStripMenuItem.Size = new Size(180, 22);
            citraBinerToolStripMenuItem.Text = "Citra Biner";
            // 
            // andToolStripMenuItem
            // 
            andToolStripMenuItem.Name = "andToolStripMenuItem";
            andToolStripMenuItem.Size = new Size(180, 22);
            andToolStripMenuItem.Text = "AND...";
            andToolStripMenuItem.Click += andToolStripMenuItem_Click;
            // 
            // orToolStripMenuItem
            // 
            orToolStripMenuItem.Name = "orToolStripMenuItem";
            orToolStripMenuItem.Size = new Size(180, 22);
            orToolStripMenuItem.Text = "OR...";
            orToolStripMenuItem.Click += orToolStripMenuItem_Click;
            // 
            // xorToolStripMenuItem
            // 
            xorToolStripMenuItem.Name = "xorToolStripMenuItem";
            xorToolStripMenuItem.Size = new Size(180, 22);
            xorToolStripMenuItem.Text = "XOR...";
            xorToolStripMenuItem.Click += xorToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(177, 6);
            // 
            // bwToolStripMenuItem
            // 
            bwToolStripMenuItem.Name = "bwToolStripMenuItem";
            bwToolStripMenuItem.Size = new Size(172, 22);
            bwToolStripMenuItem.Text = "Black/White";
            bwToolStripMenuItem.Click += bwToolStripMenuItem_Click;
            // 
            // brightnessToolStripMenuItem
            // 
            brightnessToolStripMenuItem.Name = "brightnessToolStripMenuItem";
            brightnessToolStripMenuItem.Size = new Size(172, 22);
            brightnessToolStripMenuItem.Text = "Brightness";
            brightnessToolStripMenuItem.Click += brightnessToolStripMenuItem_Click;
            // 
            // imageSelectionToolStripMenuItem
            // 
            imageSelectionToolStripMenuItem.Name = "imageSelectionToolStripMenuItem";
            imageSelectionToolStripMenuItem.Size = new Size(180, 22);
            imageSelectionToolStripMenuItem.Text = "Seleksi Gambar";
            imageSelectionToolStripMenuItem.Click += imageSelectionToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(177, 6);
            // 
            // restoreToolStripMenuItem
            // 
            restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            restoreToolStripMenuItem.Size = new Size(180, 22);
            restoreToolStripMenuItem.Text = "Restore Original";
            restoreToolStripMenuItem.Click += restoreToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                tableDataToolStripMenuItem,
                histogramToolStripMenuItem,
                savePixelDataToolStripMenuItem
            });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(46, 20);
            viewToolStripMenuItem.Text = "VIEW";
            // 
            // tableDataToolStripMenuItem
            // 
            tableDataToolStripMenuItem.Name = "tableDataToolStripMenuItem";
            tableDataToolStripMenuItem.Size = new Size(130, 22);
            tableDataToolStripMenuItem.Text = "Tabel Data";
            tableDataToolStripMenuItem.Click += tableDataToolStripMenuItem_Click;
            // 
            // histogramToolStripMenuItem
            // 
            histogramToolStripMenuItem.Name = "histogramToolStripMenuItem";
            histogramToolStripMenuItem.Size = new Size(130, 22);
            histogramToolStripMenuItem.Text = "Histogram";
            histogramToolStripMenuItem.Click += histogramToolStripMenuItem_Click;

            // 
            // savePixelDataToolStripMenuItem
            // 
            savePixelDataToolStripMenuItem.Name = "savePixelDataToolStripMenuItem";
            savePixelDataToolStripMenuItem.Size = new Size(180, 22);
            savePixelDataToolStripMenuItem.Text = "Simpan Data Pixel...";
            savePixelDataToolStripMenuItem.Click += savePixelDataToolStripMenuItem_Click;

            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(47, 20);
            helpToolStripMenuItem.Text = "HELP";
            // 
            // tabControlCanvas
            // 
            tabControlCanvas.Dock = DockStyle.Fill;
            tabControlCanvas.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControlCanvas.Location = new Point(0, 0);
            tabControlCanvas.Name = "tabControlCanvas";
            tabControlCanvas.SelectedIndex = 0;
            tabControlCanvas.Size = new Size(639, 576);
            tabControlCanvas.TabIndex = 2;
            tabControlCanvas.DrawItem += tabControlCanvas_DrawItem;
            tabControlCanvas.MouseClick += tabControlCanvas_MouseClick;
            // 
            // flowLayoutPanelThumbnails
            // 
            flowLayoutPanelThumbnails.AutoScroll = true;
            flowLayoutPanelThumbnails.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanelThumbnails.Dock = DockStyle.Left;
            flowLayoutPanelThumbnails.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanelThumbnails.Location = new Point(0, 24);
            flowLayoutPanelThumbnails.Name = "flowLayoutPanelThumbnails";
            flowLayoutPanelThumbnails.Size = new Size(161, 576);
            flowLayoutPanelThumbnails.TabIndex = 3;
            flowLayoutPanelThumbnails.WrapContents = false;
            // 
            // panelHistogram
            // 
            panelHistogram.BorderStyle = BorderStyle.FixedSingle;
            panelHistogram.Controls.Add(labelGray);
            panelHistogram.Controls.Add(pictureBoxGrayHistogram);
            panelHistogram.Controls.Add(labelBlue);
            panelHistogram.Controls.Add(pictureBoxBlueHistogram);
            panelHistogram.Controls.Add(labelGreen);
            panelHistogram.Controls.Add(pictureBoxGreenHistogram);
            panelHistogram.Controls.Add(labelRed);
            panelHistogram.Controls.Add(pictureBoxRedHistogram);
            panelHistogram.Dock = DockStyle.Right;
            panelHistogram.Location = new Point(639, 0);
            panelHistogram.Name = "panelHistogram";
            panelHistogram.Size = new Size(200, 576);
            panelHistogram.TabIndex = 4;
            panelHistogram.Visible = false;
            // 
            // labelGray
            // 
            labelGray.AutoSize = true;
            labelGray.Location = new Point(13, 310);
            labelGray.Name = "labelGray";
            labelGray.Size = new Size(31, 15);
            labelGray.TabIndex = 0;
            labelGray.Text = "Gray";
            // 
            // pictureBoxGrayHistogram
            // 
            pictureBoxGrayHistogram.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxGrayHistogram.Location = new Point(13, 328);
            pictureBoxGrayHistogram.Name = "pictureBoxGrayHistogram";
            pictureBoxGrayHistogram.Size = new Size(170, 70);
            pictureBoxGrayHistogram.TabIndex = 7;
            pictureBoxGrayHistogram.TabStop = false;
            // 
            // labelBlue
            // 
            labelBlue.AutoSize = true;
            labelBlue.Location = new Point(13, 210);
            labelBlue.Name = "labelBlue";
            labelBlue.Size = new Size(30, 15);
            labelBlue.TabIndex = 8;
            labelBlue.Text = "Blue";
            // 
            // pictureBoxBlueHistogram
            // 
            pictureBoxBlueHistogram.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxBlueHistogram.Location = new Point(13, 228);
            pictureBoxBlueHistogram.Name = "pictureBoxBlueHistogram";
            pictureBoxBlueHistogram.Size = new Size(170, 70);
            pictureBoxBlueHistogram.TabIndex = 5;
            pictureBoxBlueHistogram.TabStop = false;
            // 
            // labelGreen
            // 
            labelGreen.AutoSize = true;
            labelGreen.Location = new Point(13, 110);
            labelGreen.Name = "labelGreen";
            labelGreen.Size = new Size(38, 15);
            labelGreen.TabIndex = 9;
            labelGreen.Text = "Green";
            // 
            // pictureBoxGreenHistogram
            // 
            pictureBoxGreenHistogram.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxGreenHistogram.Location = new Point(13, 128);
            pictureBoxGreenHistogram.Name = "pictureBoxGreenHistogram";
            pictureBoxGreenHistogram.Size = new Size(170, 70);
            pictureBoxGreenHistogram.TabIndex = 3;
            pictureBoxGreenHistogram.TabStop = false;
            // 
            // labelRed
            // 
            labelRed.AutoSize = true;
            labelRed.Location = new Point(13, 10);
            labelRed.Name = "labelRed";
            labelRed.Size = new Size(27, 15);
            labelRed.TabIndex = 10;
            labelRed.Text = "Red";
            // 
            // pictureBoxRedHistogram
            // 
            pictureBoxRedHistogram.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxRedHistogram.Location = new Point(13, 28);
            pictureBoxRedHistogram.Name = "pictureBoxRedHistogram";
            pictureBoxRedHistogram.Size = new Size(170, 70);
            pictureBoxRedHistogram.TabIndex = 1;
            pictureBoxRedHistogram.TabStop = false;
            // 
            // panelMainArea
            // 
            panelMainArea.Controls.Add(tabControlCanvas);
            panelMainArea.Controls.Add(panelHistogram);
            panelMainArea.Dock = DockStyle.Fill;
            panelMainArea.Location = new Point(161, 24);
            panelMainArea.Name = "panelMainArea";
            panelMainArea.Size = new Size(839, 576);
            panelMainArea.TabIndex = 4;
            // 
            // operasiAritmatikaToolStripMenuItem
            // 
            operasiAritmatikaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tambahToolStripMenuItem, kurangToolStripMenuItem, bagiToolStripMenuItem, kaliToolStripMenuItem, konstantaToolStripMenuItem });
            operasiAritmatikaToolStripMenuItem.Name = "operasiAritmatikaToolStripMenuItem";
            operasiAritmatikaToolStripMenuItem.Size = new Size(172, 22);
            operasiAritmatikaToolStripMenuItem.Text = "Operasi Aritmatika";
            // 
            // tambahToolStripMenuItem
            // 
            tambahToolStripMenuItem.Name = "tambahToolStripMenuItem";
            tambahToolStripMenuItem.Size = new Size(180, 22);
            tambahToolStripMenuItem.Text = "Tambah";
            // 
            // kurangToolStripMenuItem
            // 
            kurangToolStripMenuItem.Name = "kurangToolStripMenuItem";
            kurangToolStripMenuItem.Size = new Size(180, 22);
            kurangToolStripMenuItem.Text = "Kurang";
            // 
            // bagiToolStripMenuItem
            // 
            bagiToolStripMenuItem.Name = "bagiToolStripMenuItem";
            bagiToolStripMenuItem.Size = new Size(180, 22);
            bagiToolStripMenuItem.Text = "Bagi";
            // 
            // kaliToolStripMenuItem
            // 
            kaliToolStripMenuItem.Name = "kaliToolStripMenuItem";
            kaliToolStripMenuItem.Size = new Size(180, 22);
            kaliToolStripMenuItem.Text = "Kali";
            // 
            // konstantaToolStripMenuItem
            // 
            konstantaToolStripMenuItem.Name = "konstantaToolStripMenuItem";
            konstantaToolStripMenuItem.Size = new Size(180, 22);
            konstantaToolStripMenuItem.Text = "Konstanta";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(panelMainArea);
            Controls.Add(flowLayoutPanelThumbnails);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Mini Photoshop";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panelHistogram.ResumeLayout(false);
            panelHistogram.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGrayHistogram).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxBlueHistogram).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGreenHistogram).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRedHistogram).EndInit();
            panelMainArea.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControlCanvas;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelThumbnails;
        private ToolStripMenuItem grayscaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem restoreToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private Panel panelHistogram;
        private Label labelGray;
        private PictureBox pictureBoxGrayHistogram;
        private Label labelBlue;
        private PictureBox pictureBoxBlueHistogram;
        private Label labelGreen;
        private PictureBox pictureBoxGreenHistogram;
        private Label labelRed;
        private PictureBox pictureBoxRedHistogram;
        private Panel panelMainArea;
        private ToolStripMenuItem tableDataToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem negationToolStripMenuItem;
        private ToolStripMenuItem bwToolStripMenuItem;
        private ToolStripMenuItem imageSelectionToolStripMenuItem;
        private ToolStripMenuItem redToolStripMenuItem;
        private ToolStripMenuItem greenToolStripMenuItem;
        private ToolStripMenuItem blueToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem brightnessToolStripMenuItem;
        private ToolStripMenuItem histogramToolStripMenuItem;
        private ToolStripMenuItem operasiAritmatikaToolStripMenuItem;
        private ToolStripMenuItem tambahToolStripMenuItem;
        private ToolStripMenuItem kurangToolStripMenuItem;
        private ToolStripMenuItem bagiToolStripMenuItem;
        private ToolStripMenuItem kaliToolStripMenuItem;
        private ToolStripMenuItem konstantaToolStripMenuItem;
        private ToolStripMenuItem notToolStripMenuItem;
        private ToolStripMenuItem savePixelDataToolStripMenuItem;
        private ToolStripMenuItem perhitunganToolStripMenuItem;
        private ToolStripMenuItem penjumlahanToolStripMenuItem;
        private ToolStripMenuItem penguranganToolStripMenuItem;
        private ToolStripMenuItem citraBinerToolStripMenuItem;
        private ToolStripMenuItem andToolStripMenuItem;
        private ToolStripMenuItem orToolStripMenuItem;
        private ToolStripMenuItem xorToolStripMenuItem;
    }
}