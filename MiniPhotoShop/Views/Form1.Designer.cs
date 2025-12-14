namespace MiniPhotoShop.Views
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
            redToolStripMenuItem = new ToolStripMenuItem();
            greenToolStripMenuItem = new ToolStripMenuItem();
            blueToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            konvolusiToolStripMenuItem = new ToolStripMenuItem();
            identityToolStripMenuItem = new ToolStripMenuItem();
            blurToolStripMenuItem = new ToolStripMenuItem();
            sharpenToolStripMenuItem = new ToolStripMenuItem();
            edgeDetectionToolStripMenuItem = new ToolStripMenuItem();
            sobelToolStripMenuItem = new ToolStripMenuItem();
            prewitToolStripMenuItem = new ToolStripMenuItem();
            robertToolStripMenuItem = new ToolStripMenuItem();
            cannyToolStripMenuItem = new ToolStripMenuItem();
            embossToolStripMenuItem = new ToolStripMenuItem();
            customToolStripMenuItem = new ToolStripMenuItem();
            bwToolStripMenuItem = new ToolStripMenuItem();
            brightnessToolStripMenuItem = new ToolStripMenuItem();
            imageSelectionToolStripMenuItem = new ToolStripMenuItem();
            citraBinerToolStripMenuItem = new ToolStripMenuItem();
            notToolStripMenuItem = new ToolStripMenuItem();
            andToolStripMenuItem = new ToolStripMenuItem();
            orToolStripMenuItem = new ToolStripMenuItem();
            xorToolStripMenuItem = new ToolStripMenuItem();
            operasiAritmatikaToolStripMenuItem = new ToolStripMenuItem();
            tambahToolStripMenuItem = new ToolStripMenuItem();
            kurangToolStripMenuItem = new ToolStripMenuItem();
            bagiToolStripMenuItem = new ToolStripMenuItem();
            kaliToolStripMenuItem = new ToolStripMenuItem();
            konstantaToolStripMenuItem = new ToolStripMenuItem();
            kaliToolStripMenuItem1 = new ToolStripMenuItem();
            bagiToolStripMenuItem1 = new ToolStripMenuItem();
            translasiToolStripMenuItem = new ToolStripMenuItem();
            rotasiCitraToolStripMenuItem = new ToolStripMenuItem();
            dilatasiCitraToolStripMenuItem = new ToolStripMenuItem();
            zoomInToolStripMenuItem = new ToolStripMenuItem();
            zoomOutToolStripMenuItem = new ToolStripMenuItem();
            distorsiToolStripMenuItem = new ToolStripMenuItem();
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
            histogramCitraToolStripMenuItem = new ToolStripMenuItem();
            histogramEqualizationToolStripMenuItem = new ToolStripMenuItem();
            adaptiveHistogramEqualizationToolStripMenuItem = new ToolStripMenuItem();
            lineraStretchEqualizationToolStripMenuItem = new ToolStripMenuItem();
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
            filterToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { grayscaleToolStripMenuItem, negationToolStripMenuItem, redToolStripMenuItem, greenToolStripMenuItem, blueToolStripMenuItem, bwToolStripMenuItem, toolStripSeparator3, konvolusiToolStripMenuItem, histogramCitraToolStripMenuItem, brightnessToolStripMenuItem, imageSelectionToolStripMenuItem, citraBinerToolStripMenuItem, operasiAritmatikaToolStripMenuItem, translasiToolStripMenuItem, rotasiCitraToolStripMenuItem, dilatasiCitraToolStripMenuItem, zoomInToolStripMenuItem, zoomOutToolStripMenuItem, distorsiToolStripMenuItem, toolStripSeparator1, restoreToolStripMenuItem });
            filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            filterToolStripMenuItem.Size = new Size(52, 20);
            filterToolStripMenuItem.Text = "FILTER";
            // 
            // grayscaleToolStripMenuItem
            // 
            grayscaleToolStripMenuItem.Name = "grayscaleToolStripMenuItem";
            grayscaleToolStripMenuItem.Size = new Size(193, 22);
            grayscaleToolStripMenuItem.Text = "Grayscale";
            grayscaleToolStripMenuItem.Click += grayscaleToolStripMenuItem_Click;
            // 
            // negationToolStripMenuItem
            // 
            negationToolStripMenuItem.Name = "negationToolStripMenuItem";
            negationToolStripMenuItem.Size = new Size(193, 22);
            negationToolStripMenuItem.Text = "Negation";
            negationToolStripMenuItem.Click += negationToolStripMenuItem_Click;
            // 
            // redToolStripMenuItem
            // 
            redToolStripMenuItem.Name = "redToolStripMenuItem";
            redToolStripMenuItem.Size = new Size(193, 22);
            redToolStripMenuItem.Text = "Red Channel";
            redToolStripMenuItem.Click += redToolStripMenuItem_Click;
            // 
            // greenToolStripMenuItem
            // 
            greenToolStripMenuItem.Name = "greenToolStripMenuItem";
            greenToolStripMenuItem.Size = new Size(193, 22);
            greenToolStripMenuItem.Text = "Green Channel";
            greenToolStripMenuItem.Click += greenToolStripMenuItem_Click;
            // 
            // blueToolStripMenuItem
            // 
            blueToolStripMenuItem.Name = "blueToolStripMenuItem";
            blueToolStripMenuItem.Size = new Size(193, 22);
            blueToolStripMenuItem.Text = "Blue Channel";
            blueToolStripMenuItem.Click += blueToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(190, 6);
            // 
            // konvolusiToolStripMenuItem
            // 
            konvolusiToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { identityToolStripMenuItem, blurToolStripMenuItem, sharpenToolStripMenuItem, edgeDetectionToolStripMenuItem, embossToolStripMenuItem, customToolStripMenuItem });
            konvolusiToolStripMenuItem.Name = "konvolusiToolStripMenuItem";
            konvolusiToolStripMenuItem.Size = new Size(193, 22);
            konvolusiToolStripMenuItem.Text = "Konvolusi (Kernel)";
            // 
            // identityToolStripMenuItem
            // 
            identityToolStripMenuItem.Name = "identityToolStripMenuItem";
            identityToolStripMenuItem.Size = new Size(180, 22);
            identityToolStripMenuItem.Text = "Identity";
            identityToolStripMenuItem.Click += identityToolStripMenuItem_Click;
            // 
            // blurToolStripMenuItem
            // 
            blurToolStripMenuItem.Name = "blurToolStripMenuItem";
            blurToolStripMenuItem.Size = new Size(180, 22);
            blurToolStripMenuItem.Text = "Blur (Gaussian)";
            blurToolStripMenuItem.Click += blurToolStripMenuItem_Click;
            // 
            // sharpenToolStripMenuItem
            // 
            sharpenToolStripMenuItem.Name = "sharpenToolStripMenuItem";
            sharpenToolStripMenuItem.Size = new Size(180, 22);
            sharpenToolStripMenuItem.Text = "Sharpen";
            sharpenToolStripMenuItem.Click += sharpenToolStripMenuItem_Click;
            // 
            // edgeDetectionToolStripMenuItem
            // 
            edgeDetectionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { sobelToolStripMenuItem, prewitToolStripMenuItem, robertToolStripMenuItem, cannyToolStripMenuItem });
            edgeDetectionToolStripMenuItem.Name = "edgeDetectionToolStripMenuItem";
            edgeDetectionToolStripMenuItem.Size = new Size(180, 22);
            edgeDetectionToolStripMenuItem.Text = "Edge Detection";
            // 
            // sobelToolStripMenuItem
            // 
            sobelToolStripMenuItem.Name = "sobelToolStripMenuItem";
            sobelToolStripMenuItem.Size = new Size(109, 22);
            sobelToolStripMenuItem.Text = "Sobel";
            sobelToolStripMenuItem.Click += sobelToolStripMenuItem_Click;
            // 
            // prewitToolStripMenuItem
            // 
            prewitToolStripMenuItem.Name = "prewitToolStripMenuItem";
            prewitToolStripMenuItem.Size = new Size(109, 22);
            prewitToolStripMenuItem.Text = "Prewit";
            // 
            // robertToolStripMenuItem
            // 
            robertToolStripMenuItem.Name = "robertToolStripMenuItem";
            robertToolStripMenuItem.Size = new Size(109, 22);
            robertToolStripMenuItem.Text = "Robert";
            robertToolStripMenuItem.Click += robertsToolStripMenuItem_Click;
            // 
            // cannyToolStripMenuItem
            // 
            cannyToolStripMenuItem.Name = "cannyToolStripMenuItem";
            cannyToolStripMenuItem.Size = new Size(109, 22);
            cannyToolStripMenuItem.Text = "Canny";
            cannyToolStripMenuItem.Click += cannyToolStripMenuItem_Click;
            // 
            // embossToolStripMenuItem
            // 
            embossToolStripMenuItem.Name = "embossToolStripMenuItem";
            embossToolStripMenuItem.Size = new Size(180, 22);
            embossToolStripMenuItem.Text = "Emboss";
            embossToolStripMenuItem.Click += embossToolStripMenuItem_Click;
            // 
            // customToolStripMenuItem
            // 
            customToolStripMenuItem.Name = "customToolStripMenuItem";
            customToolStripMenuItem.Size = new Size(180, 22);
            customToolStripMenuItem.Text = "Custom";
            customToolStripMenuItem.Click += customKernelToolStripMenuItem_Click;
            // 
            // bwToolStripMenuItem
            // 
            bwToolStripMenuItem.Name = "bwToolStripMenuItem";
            bwToolStripMenuItem.Size = new Size(193, 22);
            bwToolStripMenuItem.Text = "Black/White";
            bwToolStripMenuItem.Click += bwToolStripMenuItem_Click;
            // 
            // brightnessToolStripMenuItem
            // 
            brightnessToolStripMenuItem.Name = "brightnessToolStripMenuItem";
            brightnessToolStripMenuItem.Size = new Size(193, 22);
            brightnessToolStripMenuItem.Text = "Brightness";
            brightnessToolStripMenuItem.Click += brightnessToolStripMenuItem_Click;
            // 
            // imageSelectionToolStripMenuItem
            // 
            imageSelectionToolStripMenuItem.Name = "imageSelectionToolStripMenuItem";
            imageSelectionToolStripMenuItem.Size = new Size(193, 22);
            imageSelectionToolStripMenuItem.Text = "Seleksi Gambar";
            imageSelectionToolStripMenuItem.Click += imageSelectionToolStripMenuItem_Click;
            // 
            // citraBinerToolStripMenuItem
            // 
            citraBinerToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { notToolStripMenuItem, andToolStripMenuItem, orToolStripMenuItem, xorToolStripMenuItem });
            citraBinerToolStripMenuItem.Name = "citraBinerToolStripMenuItem";
            citraBinerToolStripMenuItem.Size = new Size(193, 22);
            citraBinerToolStripMenuItem.Text = "Citra Biner";
            // 
            // notToolStripMenuItem
            // 
            notToolStripMenuItem.Name = "notToolStripMenuItem";
            notToolStripMenuItem.Size = new Size(99, 22);
            notToolStripMenuItem.Text = "NOT";
            notToolStripMenuItem.Click += notToolStripMenutItem_Click;
            // 
            // andToolStripMenuItem
            // 
            andToolStripMenuItem.Name = "andToolStripMenuItem";
            andToolStripMenuItem.Size = new Size(99, 22);
            andToolStripMenuItem.Text = "AND";
            andToolStripMenuItem.Click += andToolStripMenuItem_Click;
            // 
            // orToolStripMenuItem
            // 
            orToolStripMenuItem.Name = "orToolStripMenuItem";
            orToolStripMenuItem.Size = new Size(99, 22);
            orToolStripMenuItem.Text = "OR";
            orToolStripMenuItem.Click += orToolStripMenuItem_Click;
            // 
            // xorToolStripMenuItem
            // 
            xorToolStripMenuItem.Name = "xorToolStripMenuItem";
            xorToolStripMenuItem.Size = new Size(99, 22);
            xorToolStripMenuItem.Text = "XOR";
            xorToolStripMenuItem.Click += xorToolStripMenuItem_Click;
            // 
            // operasiAritmatikaToolStripMenuItem
            // 
            operasiAritmatikaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tambahToolStripMenuItem, kurangToolStripMenuItem, bagiToolStripMenuItem, kaliToolStripMenuItem, konstantaToolStripMenuItem });
            operasiAritmatikaToolStripMenuItem.Name = "operasiAritmatikaToolStripMenuItem";
            operasiAritmatikaToolStripMenuItem.Size = new Size(193, 22);
            operasiAritmatikaToolStripMenuItem.Text = "Operasi Aritmatika";
            // 
            // tambahToolStripMenuItem
            // 
            tambahToolStripMenuItem.Name = "tambahToolStripMenuItem";
            tambahToolStripMenuItem.Size = new Size(127, 22);
            tambahToolStripMenuItem.Text = "Tambah";
            tambahToolStripMenuItem.Click += tambahToolStripMenuItem_Click;
            // 
            // kurangToolStripMenuItem
            // 
            kurangToolStripMenuItem.Name = "kurangToolStripMenuItem";
            kurangToolStripMenuItem.Size = new Size(127, 22);
            kurangToolStripMenuItem.Text = "Kurang";
            kurangToolStripMenuItem.Click += kurangToolStripMenuItem_Click;
            // 
            // bagiToolStripMenuItem
            // 
            bagiToolStripMenuItem.Name = "bagiToolStripMenuItem";
            bagiToolStripMenuItem.Size = new Size(127, 22);
            bagiToolStripMenuItem.Text = "Bagi";
            bagiToolStripMenuItem.Click += bagiToolStripMenuItem_Click;
            // 
            // kaliToolStripMenuItem
            // 
            kaliToolStripMenuItem.Name = "kaliToolStripMenuItem";
            kaliToolStripMenuItem.Size = new Size(127, 22);
            kaliToolStripMenuItem.Text = "Kali";
            kaliToolStripMenuItem.Click += kaliToolStripMenuItem_Click;
            // 
            // konstantaToolStripMenuItem
            // 
            konstantaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { kaliToolStripMenuItem1, bagiToolStripMenuItem1 });
            konstantaToolStripMenuItem.Name = "konstantaToolStripMenuItem";
            konstantaToolStripMenuItem.Size = new Size(127, 22);
            konstantaToolStripMenuItem.Text = "Konstanta";
            // 
            // kaliToolStripMenuItem1
            // 
            kaliToolStripMenuItem1.Name = "kaliToolStripMenuItem1";
            kaliToolStripMenuItem1.Size = new Size(97, 22);
            kaliToolStripMenuItem1.Text = "Kali";
            kaliToolStripMenuItem1.Click += kaliKonstantaToolStripMenuItem_Click;
            // 
            // bagiToolStripMenuItem1
            // 
            bagiToolStripMenuItem1.Name = "bagiToolStripMenuItem1";
            bagiToolStripMenuItem1.Size = new Size(97, 22);
            bagiToolStripMenuItem1.Text = "Bagi";
            bagiToolStripMenuItem1.Click += bagiKonstantaToolStripMenuItem_Click;
            // 
            // translasiToolStripMenuItem
            // 
            translasiToolStripMenuItem.Name = "translasiToolStripMenuItem";
            translasiToolStripMenuItem.Size = new Size(193, 22);
            translasiToolStripMenuItem.Text = "Translasi Citra";
            translasiToolStripMenuItem.Click += translasiToolStripMenuItem_Click;
            // 
            // rotasiCitraToolStripMenuItem
            // 
            rotasiCitraToolStripMenuItem.Name = "rotasiCitraToolStripMenuItem";
            rotasiCitraToolStripMenuItem.Size = new Size(193, 22);
            rotasiCitraToolStripMenuItem.Text = "Rotasi Citra";
            rotasiCitraToolStripMenuItem.Click += rotasiCitraToolStripMenuItem_Click;
            // 
            // dilatasiCitraToolStripMenuItem
            // 
            dilatasiCitraToolStripMenuItem.Name = "dilatasiCitraToolStripMenuItem";
            dilatasiCitraToolStripMenuItem.Size = new Size(193, 22);
            dilatasiCitraToolStripMenuItem.Text = "Dilatasi Citra (Custom)";
            dilatasiCitraToolStripMenuItem.Click += dilatasiCitraToolStripMenuItem_Click;
            // 
            // zoomInToolStripMenuItem
            // 
            zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            zoomInToolStripMenuItem.Size = new Size(193, 22);
            zoomInToolStripMenuItem.Text = "Zoom In (2x)";
            zoomInToolStripMenuItem.Click += zoomInToolStripMenuItem_Click;
            // 
            // zoomOutToolStripMenuItem
            // 
            zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            zoomOutToolStripMenuItem.Size = new Size(193, 22);
            zoomOutToolStripMenuItem.Text = "Zoom Out (0.5x)";
            zoomOutToolStripMenuItem.Click += zoomOutToolStripMenuItem_Click;
            // 
            // distorsiToolStripMenuItem
            // 
            distorsiToolStripMenuItem.Name = "distorsiToolStripMenuItem";
            distorsiToolStripMenuItem.Size = new Size(193, 22);
            distorsiToolStripMenuItem.Text = "Distorsi (Ripple)";
            distorsiToolStripMenuItem.Click += distorsiToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(190, 6);
            // 
            // restoreToolStripMenuItem
            // 
            restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            restoreToolStripMenuItem.Size = new Size(193, 22);
            restoreToolStripMenuItem.Text = "Restore Original";
            restoreToolStripMenuItem.Click += restoreToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tableDataToolStripMenuItem, histogramToolStripMenuItem, savePixelDataToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(46, 20);
            viewToolStripMenuItem.Text = "VIEW";
            // 
            // tableDataToolStripMenuItem
            // 
            tableDataToolStripMenuItem.Name = "tableDataToolStripMenuItem";
            tableDataToolStripMenuItem.Size = new Size(178, 22);
            tableDataToolStripMenuItem.Text = "Tabel Data";
            tableDataToolStripMenuItem.Click += tableDataToolStripMenuItem_Click;
            // 
            // histogramToolStripMenuItem
            // 
            histogramToolStripMenuItem.Name = "histogramToolStripMenuItem";
            histogramToolStripMenuItem.Size = new Size(178, 22);
            histogramToolStripMenuItem.Text = "Histogram";
            histogramToolStripMenuItem.Click += histogramToolStripMenuItem_Click;
            // 
            // savePixelDataToolStripMenuItem
            // 
            savePixelDataToolStripMenuItem.Name = "savePixelDataToolStripMenuItem";
            savePixelDataToolStripMenuItem.Size = new Size(178, 22);
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
            // histogramCitraToolStripMenuItem
            // 
            histogramCitraToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { histogramEqualizationToolStripMenuItem, adaptiveHistogramEqualizationToolStripMenuItem, lineraStretchEqualizationToolStripMenuItem });
            histogramCitraToolStripMenuItem.Name = "histogramCitraToolStripMenuItem";
            histogramCitraToolStripMenuItem.Size = new Size(193, 22);
            histogramCitraToolStripMenuItem.Text = "Histogram Citra";
            // 
            // histogramEqualizationToolStripMenuItem
            // 
            histogramEqualizationToolStripMenuItem.Name = "histogramEqualizationToolStripMenuItem";
            histogramEqualizationToolStripMenuItem.Size = new Size(247, 22);
            histogramEqualizationToolStripMenuItem.Text = "Histogram Equalization";
            // 
            // adaptiveHistogramEqualizationToolStripMenuItem
            // 
            adaptiveHistogramEqualizationToolStripMenuItem.Name = "adaptiveHistogramEqualizationToolStripMenuItem";
            adaptiveHistogramEqualizationToolStripMenuItem.Size = new Size(247, 22);
            adaptiveHistogramEqualizationToolStripMenuItem.Text = "Adaptive Histogram Equalization";
            // 
            // lineraStretchEqualizationToolStripMenuItem
            // 
            lineraStretchEqualizationToolStripMenuItem.Name = "lineraStretchEqualizationToolStripMenuItem";
            lineraStretchEqualizationToolStripMenuItem.Size = new Size(247, 22);
            lineraStretchEqualizationToolStripMenuItem.Text = "Linear Stretch Equalization";
            lineraStretchEqualizationToolStripMenuItem.Click += lineraStretchEqualizationToolStripMenuItem_Click;
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
        private ToolStripMenuItem customToolStripMenuItem;
        private ToolStripMenuItem sobelToolStripMenuItem;
        private ToolStripMenuItem prewitToolStripMenuItem;
        private ToolStripMenuItem robertToolStripMenuItem;
        private ToolStripMenuItem cannyToolStripMenuItem;
        private ToolStripMenuItem histogramCitraToolStripMenuItem;
        private ToolStripMenuItem histogramEqualizationToolStripMenuItem;
        private ToolStripMenuItem adaptiveHistogramEqualizationToolStripMenuItem;
        private ToolStripMenuItem lineraStretchEqualizationToolStripMenuItem;
    }
}