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
            saveAsColorToolStripMenuItem = new ToolStripMenuItem();
            saveAsGrayscaleToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            cutToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            clearToolStripMenuItem = new ToolStripMenuItem();
            filterToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            tabControlCanvas = new TabControl();
            tabPage1 = new TabPage();
            canvasPictureBox1 = new PictureBox();
            tabPage2 = new TabPage();
            canvasPictureBox2 = new PictureBox();
            flowLayoutPanelThumbnails = new FlowLayoutPanel();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)canvasPictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)canvasPictureBox2).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[]
            {
                fileToolStripMenuItem, 
                editToolStripMenuItem, 
                filterToolStripMenuItem,
                viewToolStripMenuItem, 
                helpToolStripMenuItem
            });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(750, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                saveAsColorToolStripMenuItem, 
                saveAsGrayscaleToolStripMenuItem
            });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(40, 20);
            fileToolStripMenuItem.Text = "FILE";
            // 
            // saveToolStripMenuItem
            // 
            saveAsColorToolStripMenuItem.Name = "saveAsColorToolStripMenuItem";
            saveAsColorToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            saveAsColorToolStripMenuItem.Size = new Size(195, 22);
            saveAsColorToolStripMenuItem.Text = "Save As Color";
            saveAsColorToolStripMenuItem.Click += saveAsColorToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsGrayscaleToolStripMenuItem.Name = "saveAsGrayscaleToolStripMenuItem";
            saveAsGrayscaleToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            saveAsGrayscaleToolStripMenuItem.Size = new Size(195, 22);
            saveAsGrayscaleToolStripMenuItem.Text = "Save As Grayscale";
            saveAsGrayscaleToolStripMenuItem.Click += saveAsGrayscaleToolStripMenuItem_Click;
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
            cutToolStripMenuItem.Size = new Size(109, 22);
            cutToolStripMenuItem.Text = "CUT";
            cutToolStripMenuItem.Click += cutToolStripMenuItem_Click;
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(109, 22);
            copyToolStripMenuItem.Text = "COPY";
            copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.Size = new Size(109, 22);
            pasteToolStripMenuItem.Text = "PASTE";
            pasteToolStripMenuItem.Click += pasteToolStripMenuItem_Click;
            // 
            // clearToolStripMenuItem
            // 
            clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            clearToolStripMenuItem.Size = new Size(109, 22);
            clearToolStripMenuItem.Text = "CLEAR";
            clearToolStripMenuItem.Click += clearToolStripMenuItem_Click;
            // 
            // filterToolStripMenuItem
            // 
            filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            filterToolStripMenuItem.Size = new Size(52, 20);
            filterToolStripMenuItem.Text = "FILTER";
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
            // editToolStripMenuItem
            //
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] 
            {
                this.cutToolStripMenuItem,
                this.copyToolStripMenuItem,
                this.pasteToolStripMenuItem,
                this.clearToolStripMenuItem
            });
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(54, 24);
            this.editToolStripMenuItem.Text = "EDIT";
            //
            // flowLayoutPanelThumbnails
            //
            this.flowLayoutPanelThumbnails.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanelThumbnails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanelThumbnails.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelThumbnails.AutoScroll = true;
            this.flowLayoutPanelThumbnails.WrapContents = false;
            this.flowLayoutPanelThumbnails.Location = new System.Drawing.Point(0, 28);
            this.flowLayoutPanelThumbnails.Name = "flowLayoutPanelThumbnails";
            this.flowLayoutPanelThumbnails.TabIndex = 3;
            this.flowLayoutPanelThumbnails.Width = 200;
            //
            // tabControlCanvas
            // 
            tabControlCanvas.Dock = DockStyle.Fill;
            tabControlCanvas.Location = new Point(161, 24);
            tabControlCanvas.Name = "tabControlCanvas";
            tabControlCanvas.SelectedIndex = 0;
            tabControlCanvas.Size = new Size(589, 426);
            tabControlCanvas.TabIndex = 2;
            tabControlCanvas.SelectedIndexChanged += tabControlCanvas_SelectedIndexChanged;
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(750, 450);
            Controls.Add(tabControlCanvas);
            Controls.Add(flowLayoutPanelThumbnails);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Mini Photoshop";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)canvasPictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)canvasPictureBox2).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem saveAsColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsGrayscaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelThumbnails;
    }
}
