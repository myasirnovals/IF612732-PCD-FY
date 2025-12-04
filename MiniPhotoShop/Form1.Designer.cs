using System.Drawing;
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

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grayscaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.negationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.konvolusiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.identityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sharpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgeDetectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.embossToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bwToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brightnessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.citraBinerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.andToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.operasiAritmatikaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tambahToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kurangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bagiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kaliToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.konstantaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kaliToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.bagiToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.translasiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotasiCitraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dilatasiCitraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
<<<<<<< HEAD
            this.distorsiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePixelDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlCanvas = new System.Windows.Forms.TabControl();
            this.flowLayoutPanelThumbnails = new System.Windows.Forms.FlowLayoutPanel();
            this.panelHistogram = new System.Windows.Forms.Panel();
            this.labelGray = new System.Windows.Forms.Label();
            this.pictureBoxGrayHistogram = new System.Windows.Forms.PictureBox();
            this.labelBlue = new System.Windows.Forms.Label();
            this.pictureBoxBlueHistogram = new System.Windows.Forms.PictureBox();
            this.labelGreen = new System.Windows.Forms.Label();
            this.pictureBoxGreenHistogram = new System.Windows.Forms.PictureBox();
            this.labelRed = new System.Windows.Forms.Label();
            this.pictureBoxRedHistogram = new System.Windows.Forms.PictureBox();
            this.panelMainArea = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.panelHistogram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGrayHistogram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlueHistogram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGreenHistogram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRedHistogram)).BeginInit();
            this.panelMainArea.SuspendLayout();
            this.SuspendLayout();
=======
            this.operasiAritmatikaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            tambahToolStripMenuItem = new ToolStripMenuItem();
            kurangToolStripMenuItem = new ToolStripMenuItem();
            bagiToolStripMenuItem = new ToolStripMenuItem();
            kaliToolStripMenuItem = new ToolStripMenuItem();
            konstantaToolStripMenuItem = new ToolStripMenuItem();
            kaliToolStripMenuItem1 = new ToolStripMenuItem();
            bagiToolStripMenuItem1 = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            restoreToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            tableDataToolStripMenuItem = new ToolStripMenuItem();
            histogramToolStripMenuItem = new ToolStripMenuItem();
            savePixelDataToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            translasiToolStripMenuItem = new ToolStripMenuItem();
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
            menuStrip1.SuspendLayout();
            panelHistogram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGrayHistogram).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxBlueHistogram).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGreenHistogram).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRedHistogram).BeginInit();
            panelMainArea.SuspendLayout();
            SuspendLayout();
>>>>>>> 4e2997168d6399af8e9ddab98c69ba10dba4b79c
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.filterToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1000, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparator2,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.fileToolStripMenuItem.Text = "FILE";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(143, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.editToolStripMenuItem.Text = "EDIT";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grayscaleToolStripMenuItem,
            this.negationToolStripMenuItem,
            this.redToolStripMenuItem,
            this.greenToolStripMenuItem,
            this.blueToolStripMenuItem,
            this.toolStripSeparator3,
            this.konvolusiToolStripMenuItem,
            this.bwToolStripMenuItem,
            this.brightnessToolStripMenuItem,
            this.imageSelectionToolStripMenuItem,
            this.citraBinerToolStripMenuItem,
            this.operasiAritmatikaToolStripMenuItem,
            this.translasiToolStripMenuItem,
            this.rotasiCitraToolStripMenuItem,
            this.dilatasiCitraToolStripMenuItem,
            this.zoomInToolStripMenuItem,
            this.zoomOutToolStripMenuItem,
            this.distorsiToolStripMenuItem,
            this.toolStripSeparator1,
            this.restoreToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.filterToolStripMenuItem.Text = "FILTER";
            // 
            // grayscaleToolStripMenuItem
            // 
            this.grayscaleToolStripMenuItem.Name = "grayscaleToolStripMenuItem";
            this.grayscaleToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.grayscaleToolStripMenuItem.Text = "Grayscale";
            this.grayscaleToolStripMenuItem.Click += new System.EventHandler(this.grayscaleToolStripMenuItem_Click);
            // 
            // negationToolStripMenuItem
            // 
            this.negationToolStripMenuItem.Name = "negationToolStripMenuItem";
            this.negationToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.negationToolStripMenuItem.Text = "Negation";
            this.negationToolStripMenuItem.Click += new System.EventHandler(this.negationToolStripMenuItem_Click);
            // 
            // redToolStripMenuItem
            // 
            this.redToolStripMenuItem.Name = "redToolStripMenuItem";
            this.redToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.redToolStripMenuItem.Text = "Red Channel";
            this.redToolStripMenuItem.Click += new System.EventHandler(this.redToolStripMenuItem_Click);
            // 
            // greenToolStripMenuItem
            // 
            this.greenToolStripMenuItem.Name = "greenToolStripMenuItem";
            this.greenToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.greenToolStripMenuItem.Text = "Green Channel";
            this.greenToolStripMenuItem.Click += new System.EventHandler(this.greenToolStripMenuItem_Click);
            // 
            // blueToolStripMenuItem
            // 
            this.blueToolStripMenuItem.Name = "blueToolStripMenuItem";
            this.blueToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.blueToolStripMenuItem.Text = "Blue Channel";
            this.blueToolStripMenuItem.Click += new System.EventHandler(this.blueToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(217, 6);
            // 
            // konvolusiToolStripMenuItem
            // 
            this.konvolusiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.identityToolStripMenuItem,
            this.blurToolStripMenuItem,
            this.sharpenToolStripMenuItem,
            this.edgeDetectionToolStripMenuItem,
            this.embossToolStripMenuItem});
            this.konvolusiToolStripMenuItem.Name = "konvolusiToolStripMenuItem";
            this.konvolusiToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.konvolusiToolStripMenuItem.Text = "Konvolusi (Kernel)";
            // 
            // identityToolStripMenuItem
            // 
            this.identityToolStripMenuItem.Name = "identityToolStripMenuItem";
            this.identityToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.identityToolStripMenuItem.Text = "Identity";
            this.identityToolStripMenuItem.Click += new System.EventHandler(this.identityToolStripMenuItem_Click);
            // 
            // blurToolStripMenuItem
            // 
            this.blurToolStripMenuItem.Name = "blurToolStripMenuItem";
            this.blurToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.blurToolStripMenuItem.Text = "Blur (Gaussian)";
            this.blurToolStripMenuItem.Click += new System.EventHandler(this.blurToolStripMenuItem_Click);
            // 
            // sharpenToolStripMenuItem
            // 
            this.sharpenToolStripMenuItem.Name = "sharpenToolStripMenuItem";
            this.sharpenToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sharpenToolStripMenuItem.Text = "Sharpen";
            this.sharpenToolStripMenuItem.Click += new System.EventHandler(this.sharpenToolStripMenuItem_Click);
            // 
            // edgeDetectionToolStripMenuItem
            // 
            this.edgeDetectionToolStripMenuItem.Name = "edgeDetectionToolStripMenuItem";
            this.edgeDetectionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.edgeDetectionToolStripMenuItem.Text = "Edge Detection";
            this.edgeDetectionToolStripMenuItem.Click += new System.EventHandler(this.edgeDetectionToolStripMenuItem_Click);
            // 
            // embossToolStripMenuItem
            // 
            this.embossToolStripMenuItem.Name = "embossToolStripMenuItem";
            this.embossToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.embossToolStripMenuItem.Text = "Emboss";
            this.embossToolStripMenuItem.Click += new System.EventHandler(this.embossToolStripMenuItem_Click);
            // 
            // bwToolStripMenuItem
            // 
            this.bwToolStripMenuItem.Name = "bwToolStripMenuItem";
            this.bwToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.bwToolStripMenuItem.Text = "Black/White";
            this.bwToolStripMenuItem.Click += new System.EventHandler(this.bwToolStripMenuItem_Click);
            // 
            // brightnessToolStripMenuItem
            // 
            this.brightnessToolStripMenuItem.Name = "brightnessToolStripMenuItem";
            this.brightnessToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.brightnessToolStripMenuItem.Text = "Brightness";
            this.brightnessToolStripMenuItem.Click += new System.EventHandler(this.brightnessToolStripMenuItem_Click);
            // 
            // imageSelectionToolStripMenuItem
            // 
            this.imageSelectionToolStripMenuItem.Name = "imageSelectionToolStripMenuItem";
            this.imageSelectionToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.imageSelectionToolStripMenuItem.Text = "Seleksi Gambar";
            this.imageSelectionToolStripMenuItem.Click += new System.EventHandler(this.imageSelectionToolStripMenuItem_Click);
            // 
            // citraBinerToolStripMenuItem
            // 
            this.citraBinerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.notToolStripMenuItem,
            this.andToolStripMenuItem,
            this.orToolStripMenuItem,
            this.xorToolStripMenuItem});
            this.citraBinerToolStripMenuItem.Name = "citraBinerToolStripMenuItem";
            this.citraBinerToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.citraBinerToolStripMenuItem.Text = "Citra Biner";
            // 
            // notToolStripMenuItem
            // 
            this.notToolStripMenuItem.Name = "notToolStripMenuItem";
            this.notToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.notToolStripMenuItem.Text = "NOT";
            this.notToolStripMenuItem.Click += new System.EventHandler(this.notToolStripMenutItem_Click);
            // 
            // andToolStripMenuItem
            // 
            this.andToolStripMenuItem.Name = "andToolStripMenuItem";
            this.andToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.andToolStripMenuItem.Text = "AND";
            this.andToolStripMenuItem.Click += new System.EventHandler(this.andToolStripMenuItem_Click);
            // 
            // orToolStripMenuItem
            // 
            this.orToolStripMenuItem.Name = "orToolStripMenuItem";
            this.orToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.orToolStripMenuItem.Text = "OR";
            this.orToolStripMenuItem.Click += new System.EventHandler(this.orToolStripMenuItem_Click);
            // 
            // xorToolStripMenuItem
            // 
            this.xorToolStripMenuItem.Name = "xorToolStripMenuItem";
            this.xorToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.xorToolStripMenuItem.Text = "XOR";
            this.xorToolStripMenuItem.Click += new System.EventHandler(this.xorToolStripMenuItem_Click);
            // 
            // operasiAritmatikaToolStripMenuItem
            // 
            this.operasiAritmatikaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tambahToolStripMenuItem,
            this.kurangToolStripMenuItem,
            this.bagiToolStripMenuItem,
            this.kaliToolStripMenuItem,
            this.konstantaToolStripMenuItem});
            this.operasiAritmatikaToolStripMenuItem.Name = "operasiAritmatikaToolStripMenuItem";
            this.operasiAritmatikaToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.operasiAritmatikaToolStripMenuItem.Text = "Operasi Aritmatika";
            // 
            // tambahToolStripMenuItem
            // 
            this.tambahToolStripMenuItem.Name = "tambahToolStripMenuItem";
            this.tambahToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.tambahToolStripMenuItem.Text = "Tambah";
            this.tambahToolStripMenuItem.Click += new System.EventHandler(this.tambahToolStripMenuItem_Click);
            // 
            // kurangToolStripMenuItem
            // 
            this.kurangToolStripMenuItem.Name = "kurangToolStripMenuItem";
            this.kurangToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.kurangToolStripMenuItem.Text = "Kurang";
            this.kurangToolStripMenuItem.Click += new System.EventHandler(this.kurangToolStripMenuItem_Click);
            // 
            // bagiToolStripMenuItem
            // 
            this.bagiToolStripMenuItem.Name = "bagiToolStripMenuItem";
            this.bagiToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bagiToolStripMenuItem.Text = "Bagi";
            this.bagiToolStripMenuItem.Click += new System.EventHandler(this.bagiToolStripMenuItem_Click);
            // 
            // kaliToolStripMenuItem
            // 
            this.kaliToolStripMenuItem.Name = "kaliToolStripMenuItem";
            this.kaliToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.kaliToolStripMenuItem.Text = "Kali";
            this.kaliToolStripMenuItem.Click += new System.EventHandler(this.kaliToolStripMenuItem_Click);
            // 
            // konstantaToolStripMenuItem
            // 
            this.konstantaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kaliToolStripMenuItem1,
            this.bagiToolStripMenuItem1});
            this.konstantaToolStripMenuItem.Name = "konstantaToolStripMenuItem";
            this.konstantaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.konstantaToolStripMenuItem.Text = "Konstanta";
            // 
            // kaliToolStripMenuItem1
            // 
            this.kaliToolStripMenuItem1.Name = "kaliToolStripMenuItem1";
            this.kaliToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.kaliToolStripMenuItem1.Text = "Kali";
            this.kaliToolStripMenuItem1.Click += new System.EventHandler(this.kaliKonstantaToolStripMenuItem_Click);
            // 
            // bagiToolStripMenuItem1
            // 
            this.bagiToolStripMenuItem1.Name = "bagiToolStripMenuItem1";
            this.bagiToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.bagiToolStripMenuItem1.Text = "Bagi";
            this.bagiToolStripMenuItem1.Click += new System.EventHandler(this.bagiKonstantaToolStripMenuItem_Click);
            // 
            // translasiToolStripMenuItem
            // 
            this.translasiToolStripMenuItem.Name = "translasiToolStripMenuItem";
            this.translasiToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.translasiToolStripMenuItem.Text = "Translasi Citra";
            this.translasiToolStripMenuItem.Click += new System.EventHandler(this.translasiToolStripMenuItem_Click);
            // 
            // rotasiCitraToolStripMenuItem
            // 
            this.rotasiCitraToolStripMenuItem.Name = "rotasiCitraToolStripMenuItem";
            this.rotasiCitraToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.rotasiCitraToolStripMenuItem.Text = "Rotasi Citra";
            this.rotasiCitraToolStripMenuItem.Click += new System.EventHandler(this.rotasiCitraToolStripMenuItem_Click);
            // 
            // dilatasiCitraToolStripMenuItem
            // 
            this.dilatasiCitraToolStripMenuItem.Name = "dilatasiCitraToolStripMenuItem";
            this.dilatasiCitraToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.dilatasiCitraToolStripMenuItem.Text = "Dilatasi Citra (Custom)";
            this.dilatasiCitraToolStripMenuItem.Click += new System.EventHandler(this.dilatasiCitraToolStripMenuItem_Click);
            // 
            // zoomInToolStripMenuItem
            // 
            this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.zoomInToolStripMenuItem.Text = "Zoom In (2x)";
            this.zoomInToolStripMenuItem.Click += new System.EventHandler(this.zoomInToolStripMenuItem_Click);
            // 
            // zoomOutToolStripMenuItem
            // 
            this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.zoomOutToolStripMenuItem.Text = "Zoom Out (0.5x)";
            this.zoomOutToolStripMenuItem.Click += new System.EventHandler(this.zoomOutToolStripMenuItem_Click);
            // 
            // distorsiToolStripMenuItem
            // 
<<<<<<< HEAD
            this.distorsiToolStripMenuItem.Name = "distorsiToolStripMenuItem";
            this.distorsiToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.distorsiToolStripMenuItem.Text = "Distorsi (Ripple)";
            this.distorsiToolStripMenuItem.Click += new System.EventHandler(this.distorsiToolStripMenuItem_Click);
=======
            // operasiAritmatikaToolStripMenuItem
            // 
            operasiAritmatikaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tambahToolStripMenuItem, kurangToolStripMenuItem, bagiToolStripMenuItem, kaliToolStripMenuItem, konstantaToolStripMenuItem });
            operasiAritmatikaToolStripMenuItem.Name = "operasiAritmatikaToolStripMenuItem";
            operasiAritmatikaToolStripMenuItem.Size = new Size(180, 22);
            operasiAritmatikaToolStripMenuItem.Text = "Operasi Aritmatika";
            // 
            // tambahToolStripMenuItem
            // 
            tambahToolStripMenuItem.Name = "tambahToolStripMenuItem";
            tambahToolStripMenuItem.Size = new Size(180, 22);
            tambahToolStripMenuItem.Text = "Tambah";
            tambahToolStripMenuItem.Click += tambahToolStripMenuItem_Click;
            // 
            // kurangToolStripMenuItem
            // 
            kurangToolStripMenuItem.Name = "kurangToolStripMenuItem";
            kurangToolStripMenuItem.Size = new Size(180, 22);
            kurangToolStripMenuItem.Text = "Kurang";
            kurangToolStripMenuItem.Click += kurangToolStripMenuItem_Click;
            // 
            // bagiToolStripMenuItem
            // 
            bagiToolStripMenuItem.Name = "bagiToolStripMenuItem";
            bagiToolStripMenuItem.Size = new Size(180, 22);
            bagiToolStripMenuItem.Text = "Bagi";
            bagiToolStripMenuItem.Click += bagiToolStripMenuItem_Click;
            // 
            // kaliToolStripMenuItem
            // 
            kaliToolStripMenuItem.Name = "kaliToolStripMenuItem";
            kaliToolStripMenuItem.Size = new Size(180, 22);
            kaliToolStripMenuItem.Text = "Kali";
            kaliToolStripMenuItem.Click += kaliToolStripMenuItem_Click;
            // 
            // konstantaToolStripMenuItem
            // 
            konstantaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { kaliToolStripMenuItem1, bagiToolStripMenuItem1 });
            konstantaToolStripMenuItem.Name = "konstantaToolStripMenuItem";
            konstantaToolStripMenuItem.Size = new Size(180, 22);
            konstantaToolStripMenuItem.Text = "Konstanta";
            // 
            // kaliToolStripMenuItem1
            // 
            kaliToolStripMenuItem1.Name = "kaliToolStripMenuItem1";
            kaliToolStripMenuItem1.Size = new Size(180, 22);
            kaliToolStripMenuItem1.Text = "Kali";
            kaliToolStripMenuItem1.Click += kaliKonstantaToolStripMenuItem_Click;
            // 
            // bagiToolStripMenuItem1
            // 
            bagiToolStripMenuItem1.Name = "bagiToolStripMenuItem1";
            bagiToolStripMenuItem1.Size = new Size(180, 22);
            bagiToolStripMenuItem1.Text = "Bagi";
            bagiToolStripMenuItem1.Click += bagiKonstantaToolStripMenuItem_Click;
>>>>>>> 4e2997168d6399af8e9ddab98c69ba10dba4b79c
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(217, 6);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.restoreToolStripMenuItem.Text = "Restore Original";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tableDataToolStripMenuItem,
            this.histogramToolStripMenuItem,
            this.savePixelDataToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.viewToolStripMenuItem.Text = "VIEW";
            // 
            // tableDataToolStripMenuItem
            // 
            this.tableDataToolStripMenuItem.Name = "tableDataToolStripMenuItem";
            this.tableDataToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.tableDataToolStripMenuItem.Text = "Tabel Data";
            this.tableDataToolStripMenuItem.Click += new System.EventHandler(this.tableDataToolStripMenuItem_Click);
            // 
            // histogramToolStripMenuItem
            // 
            this.histogramToolStripMenuItem.Name = "histogramToolStripMenuItem";
            this.histogramToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.histogramToolStripMenuItem.Text = "Histogram";
            this.histogramToolStripMenuItem.Click += new System.EventHandler(this.histogramToolStripMenuItem_Click);
            // 
            // savePixelDataToolStripMenuItem
            // 
            this.savePixelDataToolStripMenuItem.Name = "savePixelDataToolStripMenuItem";
            this.savePixelDataToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.savePixelDataToolStripMenuItem.Text = "Simpan Data Pixel...";
            this.savePixelDataToolStripMenuItem.Click += new System.EventHandler(this.savePixelDataToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.helpToolStripMenuItem.Text = "HELP";
            // 
            // tabControlCanvas
            // 
            this.tabControlCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlCanvas.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControlCanvas.Location = new System.Drawing.Point(0, 0);
            this.tabControlCanvas.Name = "tabControlCanvas";
            this.tabControlCanvas.SelectedIndex = 0;
            this.tabControlCanvas.Size = new System.Drawing.Size(639, 576);
            this.tabControlCanvas.TabIndex = 2;
            // 
            // flowLayoutPanelThumbnails
            // 
            this.flowLayoutPanelThumbnails.AutoScroll = true;
            this.flowLayoutPanelThumbnails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanelThumbnails.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanelThumbnails.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelThumbnails.Location = new System.Drawing.Point(0, 24);
            this.flowLayoutPanelThumbnails.Name = "flowLayoutPanelThumbnails";
            this.flowLayoutPanelThumbnails.Size = new System.Drawing.Size(161, 576);
            this.flowLayoutPanelThumbnails.TabIndex = 3;
            this.flowLayoutPanelThumbnails.WrapContents = false;
            // 
            // panelHistogram
            // 
            this.panelHistogram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHistogram.Controls.Add(this.labelGray);
            this.panelHistogram.Controls.Add(this.pictureBoxGrayHistogram);
            this.panelHistogram.Controls.Add(this.labelBlue);
            this.panelHistogram.Controls.Add(this.pictureBoxBlueHistogram);
            this.panelHistogram.Controls.Add(this.labelGreen);
            this.panelHistogram.Controls.Add(this.pictureBoxGreenHistogram);
            this.panelHistogram.Controls.Add(this.labelRed);
            this.panelHistogram.Controls.Add(this.pictureBoxRedHistogram);
            this.panelHistogram.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelHistogram.Location = new System.Drawing.Point(639, 0);
            this.panelHistogram.Name = "panelHistogram";
            this.panelHistogram.Size = new System.Drawing.Size(200, 576);
            this.panelHistogram.TabIndex = 4;
            this.panelHistogram.Visible = false;
            // 
            // labelGray
            // 
            this.labelGray.AutoSize = true;
            this.labelGray.Location = new System.Drawing.Point(13, 310);
            this.labelGray.Name = "labelGray";
            this.labelGray.Size = new System.Drawing.Size(31, 15);
            this.labelGray.TabIndex = 0;
            this.labelGray.Text = "Gray";
            // 
            // pictureBoxGrayHistogram
            // 
            this.pictureBoxGrayHistogram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxGrayHistogram.Location = new System.Drawing.Point(13, 328);
            this.pictureBoxGrayHistogram.Name = "pictureBoxGrayHistogram";
            this.pictureBoxGrayHistogram.Size = new System.Drawing.Size(170, 70);
            this.pictureBoxGrayHistogram.TabIndex = 7;
            this.pictureBoxGrayHistogram.TabStop = false;
            // 
            // labelBlue
            // 
            this.labelBlue.AutoSize = true;
            this.labelBlue.Location = new System.Drawing.Point(13, 210);
            this.labelBlue.Name = "labelBlue";
            this.labelBlue.Size = new System.Drawing.Size(30, 15);
            this.labelBlue.TabIndex = 8;
            this.labelBlue.Text = "Blue";
            // 
            // pictureBoxBlueHistogram
            // 
            this.pictureBoxBlueHistogram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxBlueHistogram.Location = new System.Drawing.Point(13, 228);
            this.pictureBoxBlueHistogram.Name = "pictureBoxBlueHistogram";
            this.pictureBoxBlueHistogram.Size = new System.Drawing.Size(170, 70);
            this.pictureBoxBlueHistogram.TabIndex = 5;
            this.pictureBoxBlueHistogram.TabStop = false;
            // 
            // labelGreen
            // 
            this.labelGreen.AutoSize = true;
            this.labelGreen.Location = new System.Drawing.Point(13, 110);
            this.labelGreen.Name = "labelGreen";
            this.labelGreen.Size = new System.Drawing.Size(38, 15);
            this.labelGreen.TabIndex = 9;
            this.labelGreen.Text = "Green";
            // 
            // pictureBoxGreenHistogram
            // 
            this.pictureBoxGreenHistogram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxGreenHistogram.Location = new System.Drawing.Point(13, 128);
            this.pictureBoxGreenHistogram.Name = "pictureBoxGreenHistogram";
            this.pictureBoxGreenHistogram.Size = new System.Drawing.Size(170, 70);
            this.pictureBoxGreenHistogram.TabIndex = 3;
            this.pictureBoxGreenHistogram.TabStop = false;
            // 
            // labelRed
            // 
            this.labelRed.AutoSize = true;
            this.labelRed.Location = new System.Drawing.Point(13, 10);
            this.labelRed.Name = "labelRed";
            this.labelRed.Size = new System.Drawing.Size(27, 15);
            this.labelRed.TabIndex = 10;
            this.labelRed.Text = "Red";
            // 
            // pictureBoxRedHistogram
            // 
            this.pictureBoxRedHistogram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxRedHistogram.Location = new System.Drawing.Point(13, 28);
            this.pictureBoxRedHistogram.Name = "pictureBoxRedHistogram";
            this.pictureBoxRedHistogram.Size = new System.Drawing.Size(170, 70);
            this.pictureBoxRedHistogram.TabIndex = 1;
            this.pictureBoxRedHistogram.TabStop = false;
            // 
            // panelMainArea
            // 
            this.panelMainArea.Controls.Add(this.tabControlCanvas);
            this.panelMainArea.Controls.Add(this.panelHistogram);
            this.panelMainArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainArea.Location = new System.Drawing.Point(161, 24);
            this.panelMainArea.Name = "panelMainArea";
            this.panelMainArea.Size = new System.Drawing.Size(839, 576);
            this.panelMainArea.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.panelMainArea);
            this.Controls.Add(this.flowLayoutPanelThumbnails);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Mini Photoshop";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelHistogram.ResumeLayout(false);
            this.panelHistogram.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGrayHistogram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlueHistogram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGreenHistogram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRedHistogram)).EndInit();
            this.panelMainArea.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

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
        private System.Windows.Forms.ToolStripMenuItem translasiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem distorsiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotasiCitraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dilatasiCitraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelThumbnails;
        private System.Windows.Forms.ToolStripMenuItem grayscaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel panelHistogram;
        private System.Windows.Forms.Label labelGray;
        private System.Windows.Forms.PictureBox pictureBoxGrayHistogram;
        private System.Windows.Forms.Label labelBlue;
        private System.Windows.Forms.PictureBox pictureBoxBlueHistogram;
        private System.Windows.Forms.Label labelGreen;
        private System.Windows.Forms.PictureBox pictureBoxGreenHistogram;
        private System.Windows.Forms.Label labelRed;
        private System.Windows.Forms.PictureBox pictureBoxRedHistogram;
        private System.Windows.Forms.Panel panelMainArea;
        private System.Windows.Forms.ToolStripMenuItem tableDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem negationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bwToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blueToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem brightnessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histogramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem operasiAritmatikaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tambahToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kurangToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bagiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kaliToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem konstantaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kaliToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem bagiToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem notToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePixelDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem citraBinerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem andToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem konvolusiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem identityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blurToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sharpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edgeDetectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem embossToolStripMenuItem;
    }
}