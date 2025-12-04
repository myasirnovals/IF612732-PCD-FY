using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniPhotoShop
{
    public class KernelGridDialog : Form
    {
        private NumericUpDown[,] _inputs;
        private int _size;
        public double[,] KernelValues { get; private set; }

        public KernelGridDialog(int size)
        {
            _size = size;
            this.Text = $"Input Nilai Kernel ({size}x{size})";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            int cellSize = 60;
            int margin = 20;
            this.ClientSize = new Size((size * cellSize) + (margin * 2), (size * cellSize) + 80);

            GenerateGrid(size, cellSize, margin);
            
            Button btnProcess = new Button 
            { 
                Text = "Proses", 
                DialogResult = DialogResult.OK, 
                Location = new Point(this.ClientSize.Width - 100, this.ClientSize.Height - 40),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            btnProcess.Click += (s, e) => { ParseValues(); };
            this.Controls.Add(btnProcess);
            this.AcceptButton = btnProcess;
        }

        private void GenerateGrid(int size, int cellSize, int margin)
        {
            _inputs = new NumericUpDown[size, size];
            
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    var num = new NumericUpDown();
                    num.DecimalPlaces = 2; 
                    num.Minimum = -100;    
                    num.Maximum = 100;
                    num.Width = 50;
                    num.Location = new Point(margin + (x * cellSize), margin + (y * cellSize));
                    
                    if (x == size / 2 && y == size / 2) num.Value = 1;
                    else num.Value = 0;

                    _inputs[y, x] = num;
                    this.Controls.Add(num);
                }
            }
        }

        private void ParseValues()
        {
            KernelValues = new double[_size, _size];
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    KernelValues[y, x] = (double)_inputs[y, x].Value;
                }
            }
        }
    }
}