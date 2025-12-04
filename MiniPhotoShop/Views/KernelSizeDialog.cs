using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniPhotoShop
{
    public class KernelSizeDialog : Form
    {
        public int KernelSize { get; private set; }

        public KernelSizeDialog()
        {
            this.Text = "Ukuran Kernel";
            this.Size = new Size(300, 180);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lbl = new Label
            {
                Text = "Masukkan ukuran matriks (Ganjil, min 3):\nContoh: 3 untuk (3x3)", Location = new Point(20, 20),
                AutoSize = true
            };

            var numSize = new NumericUpDown
            {
                Location = new Point(20, 60),
                Width = 240,
                Minimum = 3,
                Maximum = 9,
                Value = 3,
                Increment = 2
            };

            var btnOk = new Button { Text = "Lanjut", DialogResult = DialogResult.OK, Location = new Point(100, 100) };
            var btnCancel = new Button
                { Text = "Batal", DialogResult = DialogResult.Cancel, Location = new Point(180, 100) };

            this.Controls.Add(lbl);
            this.Controls.Add(numSize);
            this.Controls.Add(btnOk);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;

            btnOk.Click += (s, e) => { KernelSize = (int)numSize.Value; };
        }
    }
}