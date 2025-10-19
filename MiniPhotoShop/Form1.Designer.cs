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
            helpToolStripMenuItem = new ToolStripMenuItem();
            tabControlCanvas = new TabControl();
            tabPage1 = new TabPage();
            canvasPictureBox1 = new PictureBox();
            tabPage2 = new TabPage();
            canvasPictureBox2 = new PictureBox();
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
            btnHistogram = new Button();
            buttonBlue = new Button();
            buttonGreen = new Button();
            buttonRed = new Button();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)canvasPictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)canvasPictureBox2).BeginInit();
            panelHistogram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGrayHistogram).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxBlueHistogram).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGreenHistogram).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRedHistogram).BeginInit();
            panelMainArea.SuspendLayout();
            panelBottomButtons.SuspendLayout();
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
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(40, 20);
            fileToolStripMenuItem.Text = "FILE";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveToolStripMenuItem.Size = new Size(180, 22);
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
            cutToolStripMenuItem.Size = new Size(180, 22);
            cutToolStripMenuItem.Text = "CUT";
            cutToolStripMenuItem.Click += cutToolStripMenuItem_Click;
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(180, 22);
            copyToolStripMenuItem.Text = "COPY";
            copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.Size = new Size(180, 22);
            pasteToolStripMenuItem.Text = "PASTE";
            pasteToolStripMenuItem.Click += pasteToolStripMenuItem_Click;
            // 
            // clearToolStripMenuItem
            // 
            clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            clearToolStripMenuItem.Size = new Size(180, 22);
            clearToolStripMenuItem.Text = "CLEAR";
            clearToolStripMenuItem.Click += clearToolStripMenuItem_Click;
            // 
            // filterToolStripMenuItem
            // 
            filterToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { grayscaleToolStripMenuItem, toolStripSeparator1, restoreToolStripMenuItem });
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
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(46, 20);
            viewToolStripMenuItem.Text = "VIEW";
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
            tabControlCanvas.Size = new Size(639, 366);
            tabControlCanvas.TabIndex = 2;
            tabControlCanvas.DrawItem += tabControlCanvas_DrawItem;
            tabControlCanvas.SelectedIndexChanged += tabControlCanvas_SelectedIndexChanged;
            tabControlCanvas.MouseClick += tabControlCanvas_MouseClick;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(0, 0);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(200, 100);
            tabPage1.TabIndex = 0;
            // 
            // canvasPictureBox1
            // 
            canvasPictureBox1.Location = new Point(0, 0);
            canvasPictureBox1.Name = "canvasPictureBox1";
            canvasPictureBox1.Size = new Size(100, 50);
            canvasPictureBox1.TabIndex = 0;
            canvasPictureBox1.TabStop = false;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(0, 0);
            tabPage2.Name = "tabPage2";
            tabPage2.Size = new Size(200, 100);
            tabPage2.TabIndex = 0;
            // 
            // canvasPictureBox2
            // 
            canvasPictureBox2.Location = new Point(0, 0);
            canvasPictureBox2.Name = "canvasPictureBox2";
            canvasPictureBox2.Size = new Size(100, 50);
            canvasPictureBox2.TabIndex = 0;
            canvasPictureBox2.TabStop = false;
            // 
            // flowLayoutPanelThumbnails
            // 
            flowLayoutPanelThumbnails.AutoScroll = true;
            flowLayoutPanelThumbnails.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanelThumbnails.Dock = DockStyle.Left;
            flowLayoutPanelThumbnails.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanelThumbnails.Location = new Point(0, 24);
            flowLayoutPanelThumbnails.Name = "flowLayoutPanelThumbnails";
            flowLayoutPanelThumbnails.Size = new Size(161, 426);
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
            panelHistogram.Size = new Size(200, 426);
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
            panelMainArea.Size = new Size(839, 426);
            panelMainArea.TabIndex = 4;
            // 
            // panelBottomButtons
            // 
            panelBottomButtons.BorderStyle = BorderStyle.FixedSingle;
            panelBottomButtons.Controls.Add(btnHistogram);
            panelBottomButtons.Controls.Add(buttonBlue);
            panelBottomButtons.Controls.Add(buttonGreen);
            panelBottomButtons.Controls.Add(buttonRed);
            panelBottomButtons.Dock = DockStyle.Bottom;
            panelBottomButtons.Location = new Point(0, 366);
            panelBottomButtons.Name = "panelBottomButtons";
            panelBottomButtons.Size = new Size(639, 60);
            panelBottomButtons.TabIndex = 5;
            // 
            // btnHistogram
            // 
            btnHistogram.Location = new Point(505, 18);
            btnHistogram.Name = "btnHistogram";
            btnHistogram.Size = new Size(75, 23);
            btnHistogram.TabIndex = 3;
            btnHistogram.Text = "Histogram";
            btnHistogram.UseVisualStyleBackColor = true;
            btnHistogram.Click += BtnHistogramClick;
            // 
            // buttonBlue
            // 
            buttonBlue.Location = new Point(369, 18);
            buttonBlue.Name = "buttonBlue";
            buttonBlue.Size = new Size(75, 23);
            buttonBlue.TabIndex = 2;
            buttonBlue.Text = "Blue";
            buttonBlue.UseVisualStyleBackColor = true;
            buttonBlue.Click += buttonBlue_Click;
            // 
            // buttonGreen
            // 
            buttonGreen.Location = new Point(225, 18);
            buttonGreen.Name = "buttonGreen";
            buttonGreen.Size = new Size(75, 23);
            buttonGreen.TabIndex = 1;
            buttonGreen.Text = "Green";
            buttonGreen.UseVisualStyleBackColor = true;
            buttonGreen.Click += buttonGreen_Click;
            // 
            // buttonRed
            // 
            buttonRed.Location = new Point(65, 18);
            buttonRed.Name = "buttonRed";
            buttonRed.Size = new Size(75, 23);
            buttonRed.TabIndex = 0;
            buttonRed.Text = "Red";
            buttonRed.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 450);
            Controls.Add(panelMainArea);
            Controls.Add(flowLayoutPanelThumbnails);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Mini Photoshop";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)canvasPictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)canvasPictureBox2).EndInit();
            panelHistogram.ResumeLayout(false);
            panelHistogram.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGrayHistogram).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxBlueHistogram).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGreenHistogram).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRedHistogram).EndInit();
            panelMainArea.ResumeLayout(false);
            panelBottomButtons.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.PictureBox canvasPictureBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox canvasPictureBox2;
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
    }
}
