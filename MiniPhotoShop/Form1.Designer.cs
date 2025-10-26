using System.Windows.Forms;

namespace MiniPhotoShop
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
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
            toolStripSeparator1 = new ToolStripSeparator();
            restoreToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            tableDataToolStripMenuItem = new ToolStripMenuItem();
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
            panelBottomButtons = new Panel();
            lblBrightnessValue = new Label();
            trackBarBrightness = new TrackBar();
            labelBrightness = new Label();
            lblThresholdValue = new Label();
            trackBarThreshold = new TrackBar();
            labelThreshold = new Label();
            btnHistogram = new Button();
            buttonBlue = new Button();
            buttonGreen = new Button();
            buttonRed = new Button();
            menuStrip1.SuspendLayout();
            panelHistogram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGrayHistogram).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxBlueHistogram).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGreenHistogram).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRedHistogram).BeginInit();
            panelMainArea.SuspendLayout();
            panelBottomButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarBrightness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarThreshold).BeginInit();
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
            editToolStripMenuItem.Size = new Size(43, 20);
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
            // grayscaleToolStripMenuItem
            // 
            grayscaleToolStripMenuItem.Name = "grayscaleToolStripMenuItem";
            grayscaleToolStripMenuItem.Size = new Size(158, 22);
            grayscaleToolStripMenuItem.Text = "Grayscale";
            grayscaleToolStripMenuItem.Click += grayscaleToolStripMenuItem_Click;

            //
            // negationToolStripMenuItem
            //  
            negationToolStripMenuItem = new ToolStripMenuItem();
            negationToolStripMenuItem.Name = "negationToolStripMenuItem";
            negationToolStripMenuItem.Size = new Size(158, 22);
            negationToolStripMenuItem.Text = "Negation";
            negationToolStripMenuItem.Click += negationToolStripMenuItem_Click;

            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(155, 6);
            // 
            // restoreToolStripMenuItem
            // 
            restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            restoreToolStripMenuItem.Size = new Size(158, 22);
            restoreToolStripMenuItem.Text = "Restore Original";
            restoreToolStripMenuItem.Click += restoreToolStripMenuItem_Click;

            // 
            // filterToolStripMenuItem
            // 
            filterToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { grayscaleToolStripMenuItem, negationToolStripMenuItem, toolStripSeparator1, restoreToolStripMenuItem });
            filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            filterToolStripMenuItem.Size = new Size(53, 20);
            filterToolStripMenuItem.Text = "FILTER";
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tableDataToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(46, 20);
            viewToolStripMenuItem.Text = "VIEW";
            // 
            // tableDataToolStripMenuItem
            // 
            tableDataToolStripMenuItem.Name = "tableDataToolStripMenuItem";
            tableDataToolStripMenuItem.Size = new Size(129, 22);
            tableDataToolStripMenuItem.Text = "Tabel Data";
            tableDataToolStripMenuItem.Click += tableDataToolStripMenuItem_Click;
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
            tabControlCanvas.Size = new Size(639, 516);
            tabControlCanvas.TabIndex = 2;
            tabControlCanvas.DrawItem += tabControlCanvas_DrawItem;
            tabControlCanvas.SelectedIndexChanged += tabControlCanvas_SelectedIndexChanged;
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
            panelMainArea.Controls.Add(panelBottomButtons);
            panelMainArea.Controls.Add(panelHistogram);
            panelMainArea.Dock = DockStyle.Fill;
            panelMainArea.Location = new Point(161, 24);
            panelMainArea.Name = "panelMainArea";
            panelMainArea.Size = new Size(839, 576);
            panelMainArea.TabIndex = 4;
            // 
            // panelBottomButtons
            // 
            panelBottomButtons.BorderStyle = BorderStyle.FixedSingle;
            panelBottomButtons.Controls.Add(lblBrightnessValue);
            panelBottomButtons.Controls.Add(trackBarBrightness);
            panelBottomButtons.Controls.Add(labelBrightness);
            panelBottomButtons.Controls.Add(lblThresholdValue);
            panelBottomButtons.Controls.Add(trackBarThreshold);
            panelBottomButtons.Controls.Add(labelThreshold);
            panelBottomButtons.Controls.Add(btnHistogram);
            panelBottomButtons.Controls.Add(buttonBlue);
            panelBottomButtons.Controls.Add(buttonGreen);
            panelBottomButtons.Controls.Add(buttonRed);
            panelBottomButtons.Dock = DockStyle.Bottom;
            panelBottomButtons.Location = new Point(0, 516);
            panelBottomButtons.Name = "panelBottomButtons";
            panelBottomButtons.Size = new Size(639, 60);
            panelBottomButtons.TabIndex = 5;
            // 
            // lblBrightnessValue
            // 
            lblBrightnessValue.Anchor = AnchorStyles.Top;
            lblBrightnessValue.BorderStyle = BorderStyle.FixedSingle;
            lblBrightnessValue.Location = new Point(603, 10);
            lblBrightnessValue.Name = "lblBrightnessValue";
            lblBrightnessValue.Size = new Size(35, 20);
            lblBrightnessValue.TabIndex = 9;
            lblBrightnessValue.Text = "0";
            lblBrightnessValue.TextAlign = ContentAlignment.MiddleCenter;
            lblBrightnessValue.Click += lblBrightnessValue_Click;
            // 
            // trackBarBrightness
            // 
            trackBarBrightness.Anchor = AnchorStyles.Top;
            trackBarBrightness.Location = new Point(525, 10);
            trackBarBrightness.Maximum = 255;
            trackBarBrightness.Minimum = -255;
            trackBarBrightness.Name = "trackBarBrightness";
            trackBarBrightness.Size = new Size(88, 45);
            trackBarBrightness.TabIndex = 8;
            trackBarBrightness.TickFrequency = 32;
            trackBarBrightness.Scroll += new System.EventHandler(this.trackBarBrightness_Scroll);
            trackBarBrightness.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBarBrightness_MouseUp);
            // 
            // labelBrightness
            // 
            labelBrightness.Anchor = AnchorStyles.Top;
            labelBrightness.AutoSize = true;
            labelBrightness.Location = new Point(478, 15);
            labelBrightness.Name = "labelBrightness";
            labelBrightness.Size = new Size(65, 15);
            labelBrightness.TabIndex = 7;
            labelBrightness.Text = "Brightness:";
            // 
            // lblThresholdValue
            // 
            lblThresholdValue.Anchor = AnchorStyles.Top;
            lblThresholdValue.BorderStyle = BorderStyle.FixedSingle;
            lblThresholdValue.Location = new Point(446, 16);
            lblThresholdValue.Name = "lblThresholdValue";
            lblThresholdValue.Size = new Size(35, 20);
            lblThresholdValue.TabIndex = 6;
            lblThresholdValue.Text = "128";
            lblThresholdValue.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // trackBarThreshold
            // 
            trackBarThreshold.Anchor = AnchorStyles.Top;
            trackBarThreshold.Location = new Point(368, 14);
            trackBarThreshold.Maximum = 255;
            trackBarThreshold.Name = "trackBarThreshold";
            trackBarThreshold.Size = new Size(87, 45);
            trackBarThreshold.TabIndex = 5;
            trackBarThreshold.TickFrequency = 16;
            trackBarThreshold.Value = 128;
            // 
            // labelThreshold
            // 
            labelThreshold.Anchor = AnchorStyles.Top;
            labelThreshold.AutoSize = true;
            labelThreshold.Location = new Point(340, 16);
            labelThreshold.Name = "labelThreshold";
            labelThreshold.Size = new Size(33, 15);
            labelThreshold.TabIndex = 4;
            labelThreshold.Text = "B/W:";
            labelThreshold.Click += labelThreshold_Click;
            // 
            // btnHistogram
            // 
            btnHistogram.Location = new Point(259, 19);
            btnHistogram.Name = "btnHistogram";
            btnHistogram.Size = new Size(75, 23);
            btnHistogram.TabIndex = 3;
            btnHistogram.Text = "Histogram";
            btnHistogram.UseVisualStyleBackColor = true;
            btnHistogram.Click += BtnHistogramClick;
            // 
            // buttonBlue
            // 
            buttonBlue.Location = new Point(178, 19);
            buttonBlue.Name = "buttonBlue";
            buttonBlue.Size = new Size(75, 23);
            buttonBlue.TabIndex = 2;
            buttonBlue.Text = "Blue";
            buttonBlue.UseVisualStyleBackColor = true;
            buttonBlue.Click += buttonBlue_Click;
            // 
            // buttonGreen
            // 
            buttonGreen.Location = new Point(86, 19);
            buttonGreen.Name = "buttonGreen";
            buttonGreen.Size = new Size(75, 23);
            buttonGreen.TabIndex = 1;
            buttonGreen.Text = "Green";
            buttonGreen.UseVisualStyleBackColor = true;
            buttonGreen.Click += buttonGreen_Click;
            // 
            // buttonRed
            // 
            buttonRed.Location = new Point(5, 18);
            buttonRed.Name = "buttonRed";
            buttonRed.Size = new Size(75, 23);
            buttonRed.TabIndex = 0;
            buttonRed.Text = "Red";
            buttonRed.UseVisualStyleBackColor = true;
            buttonRed.Click += buttonRed_Click;
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
            panelBottomButtons.ResumeLayout(false);
            panelBottomButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarBrightness).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarThreshold).EndInit();
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
        private Panel panelBottomButtons;
        private Button buttonBlue;
        private Button buttonGreen;
        private Button buttonRed;
        private Button btnHistogram;
        private ToolStripMenuItem tableDataToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private Label labelThreshold;
        private TrackBar trackBarThreshold;
        private Label lblThresholdValue;
        private Label labelBrightness;
        private TrackBar trackBarBrightness;
        private Label lblBrightnessValue;
        private ToolStripMenuItem negationToolStripMenuItem;
    }
}