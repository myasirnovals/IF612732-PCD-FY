using System;
using System.Windows.Forms;

namespace MiniPhotoShop.Services
{
    public interface IDialogService
    {
        DialogResult ShowAdjustmentDialog(
            string title,
            int min,
            int max,
            int initialValue,
            int tickFreq,
            string labelText,
            Action<int> onPreview,
            out int finalValue);
    }
}