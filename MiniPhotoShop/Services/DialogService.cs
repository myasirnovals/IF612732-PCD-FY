using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniPhotoShop.Services
{
    public class DialogService : IDialogService
    {
        public DialogResult ShowAdjustmentDialog(
            string title, int min, int max, int initialValue, int tickFreq, string labelText,
            Action<int> onPreview,
            out int finalValue)
        {
            Form dialog = new Form()
            {
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                ControlBox = false,
                Text = title,
                Size = new Size(320, 140)
            };
            Label label = new Label() { Text = labelText, Location = new Point(12, 23), AutoSize = true };
            TrackBar trackBar = new TrackBar()
            {
                Minimum = min,
                Maximum = max,
                Value = initialValue,
                TickFrequency = tickFreq,
                Location = new Point(label.Right + 5, 15),
                Size = new Size(180, 45)
            };
            Label valueLabel = new Label()
            {
                Text = initialValue.ToString(),
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(trackBar.Right + 10, 23),
                Size = new Size(40, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            trackBar.Scroll += (sender, e) =>
            {
                valueLabel.Text = trackBar.Value.ToString();
                onPreview(trackBar.Value);
            };

            Button btnOk = new Button()
                { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(130, 70), Size = new Size(75, 23) };
            Button btnCancel = new Button()
            {
                Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(215, 70),
                Size = new Size(75, 23)
            };
            dialog.Controls.Add(label);
            dialog.Controls.Add(trackBar);
            dialog.Controls.Add(valueLabel);
            dialog.Controls.Add(btnOk);
            dialog.Controls.Add(btnCancel);
            dialog.AcceptButton = btnOk;
            dialog.CancelButton = btnCancel;

            DialogResult result = dialog.ShowDialog();
            finalValue = trackBar.Value;
            dialog.Dispose();
            return result;
        }
    }
}