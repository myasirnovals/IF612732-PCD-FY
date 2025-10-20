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
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabelDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.panelBottomButtons = new System.Windows.Forms.Panel();
            this.btnHistogram = new System.Windows.Forms.Button();
            this.buttonBlue = new System.Windows.Forms.Button();
            this.buttonGreen = new System.Windows.Forms.Button();
            this.buttonRed = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.panelHistogram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGrayHistogram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlueHistogram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGreenHistogram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRedHistogram)).BeginInit();
            this.panelMainArea.SuspendLayout();
            this.panelBottomButtons.SuspendLayout();
            this.SuspendLayout();
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
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveToolStripMenuItem.Size = new Size(138, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
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
            cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            cutToolStripMenuItem.Size = new Size(180, 22);
            cutToolStripMenuItem.Text = "Cut";
            cutToolStripMenuItem.Click += cutToolStripMenuItem_Click;
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(180, 22);
            copyToolStripMenuItem.Text = "Copy";
            copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.Size = new Size(180, 22);
            pasteToolStripMenuItem.Text = "Paste";
            pasteToolStripMenuItem.Click += pasteToolStripMenuItem_Click;
            // 
            // clearToolStripMenuItem
            // 
            clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            clearToolStripMenuItem.Size = new Size(180, 22);
            clearToolStripMenuItem.Text = "Clear";
            clearToolStripMenuItem.Click += clearToolStripMenuItem_Click;
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grayscaleToolStripMenuItem,
            this.toolStripSeparator1,
            this.restoreToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.filterToolStripMenuItem.Text = "FILTER";
            // 
            // grayscaleToolStripMenuItem
            // 
            this.grayscaleToolStripMenuItem.Name = "grayscaleToolStripMenuItem";
            this.grayscaleToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.grayscaleToolStripMenuItem.Text = "Grayscale";
            this.grayscaleToolStripMenuItem.Click += new System.EventHandler(this.grayscaleToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(155, 6);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.restoreToolStripMenuItem.Text = "Restore Original";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            grayscaleToolStripMenuItem.Name = "grayscaleToolStripMenuItem";
            grayscaleToolStripMenuItem.Size = new Size(158, 22);
            grayscaleToolStripMenuItem.Text = "Grayscale";
            grayscaleToolStripMenuItem.Click += grayscaleToolStripMenuItem_Click;
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
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabelDataToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.viewToolStripMenuItem.Text = "VIEW";
            // 
            // tabelDataToolStripMenuItem
            // 
            this.tabelDataToolStripMenuItem.Name = "tabelDataToolStripMenuItem";
            this.tabelDataToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.tabelDataToolStripMenuItem.Text = "Tabel Data";
            this.tabelDataToolStripMenuItem.Click += new System.EventHandler(this.tabelDataToolStripMenuItem_Click);
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
            this.tabControlCanvas.Size = new System.Drawing.Size(639, 366);
            this.tabControlCanvas.TabIndex = 2;
            this.tabControlCanvas.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControlCanvas_DrawItem);
            this.tabControlCanvas.SelectedIndexChanged += new System.EventHandler(this.tabControlCanvas_SelectedIndexChanged);
            this.tabControlCanvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabControlCanvas_MouseClick);
            // 
            // flowLayoutPanelThumbnails
            // 
            this.flowLayoutPanelThumbnails.AutoScroll = true;
            this.flowLayoutPanelThumbnails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanelThumbnails.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanelThumbnails.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelThumbnails.Location = new System.Drawing.Point(0, 24);
            this.flowLayoutPanelThumbnails.Name = "flowLayoutPanelThumbnails";
            this.flowLayoutPanelThumbnails.Size = new System.Drawing.Size(161, 426);
            this.flowLayoutPanelThumbnails.TabIndex = 3;
            this.flowLayoutPanelThumbnails.WrapContents = false;
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
            this.panelHistogram.Size = new System.Drawing.Size(200, 426);
            this.panelHistogram.TabIndex = 4;
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
            this.panelMainArea.Controls.Add(this.panelBottomButtons);
            this.panelMainArea.Controls.Add(this.panelHistogram);
            this.panelMainArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainArea.Location = new System.Drawing.Point(161, 24);
            this.panelMainArea.Name = "panelMainArea";
            this.panelMainArea.Size = new System.Drawing.Size(839, 426);
            this.panelMainArea.TabIndex = 4;
            // 
            // panelBottomButtons
            // 
            this.panelBottomButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBottomButtons.Controls.Add(this.btnHistogram);
            this.panelBottomButtons.Controls.Add(this.buttonBlue);
            this.panelBottomButtons.Controls.Add(this.buttonGreen);
            this.panelBottomButtons.Controls.Add(this.buttonRed);
            this.panelBottomButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottomButtons.Location = new System.Drawing.Point(0, 366);
            this.panelBottomButtons.Name = "panelBottomButtons";
            this.panelBottomButtons.Size = new System.Drawing.Size(639, 60);
            this.panelBottomButtons.TabIndex = 5;
            // 
            // btnHistogram
            // 
            this.btnHistogram.Location = new System.Drawing.Point(505, 18);
            this.btnHistogram.Name = "btnHistogram";
            this.btnHistogram.Size = new System.Drawing.Size(75, 23);
            this.btnHistogram.TabIndex = 3;
            this.btnHistogram.Text = "Histogram";
            this.btnHistogram.UseVisualStyleBackColor = true;
            this.btnHistogram.Click += new System.EventHandler(this.BtnHistogramClick);
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
            this.buttonBlue.Location = new System.Drawing.Point(369, 18);
            this.buttonBlue.Name = "buttonBlue";
            this.buttonBlue.Size = new System.Drawing.Size(75, 23);
            this.buttonBlue.TabIndex = 2;
            this.buttonBlue.Text = "Blue";
            this.buttonBlue.UseVisualStyleBackColor = true;
            this.buttonBlue.Click += new System.EventHandler(this.buttonBlue_Click);
            // 
            // buttonGreen
            // 
            this.buttonGreen.Location = new System.Drawing.Point(225, 18);
            this.buttonGreen.Name = "buttonGreen";
            this.buttonGreen.Size = new System.Drawing.Size(75, 23);
            this.buttonGreen.TabIndex = 1;
            this.buttonGreen.Text = "Green";
            this.buttonGreen.UseVisualStyleBackColor = true;
            this.buttonGreen.Click += new System.EventHandler(this.buttonGreen_Click);
            // 
            // buttonRed
            // 
            this.buttonRed.Location = new System.Drawing.Point(65, 18);
            this.buttonRed.Name = "buttonRed";
            this.buttonRed.Size = new System.Drawing.Size(75, 23);
            this.buttonRed.TabIndex = 0;
            this.buttonRed.Text = "Red";
            this.buttonRed.UseVisualStyleBackColor = true;
            this.buttonRed.Click += new System.EventHandler(this.buttonRed_Click);
            buttonRed.Location = new Point(65, 18);
            buttonRed.Name = "buttonRed";
            buttonRed.Size = new Size(75, 23);
            buttonRed.TabIndex = 0;
            buttonRed.Text = "Red";
            buttonRed.UseVisualStyleBackColor = true;
            buttonRed.Click += buttonRed_Click;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 450);
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
            this.panelBottomButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private ToolStripMenuItem tabelDataToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
    }
}