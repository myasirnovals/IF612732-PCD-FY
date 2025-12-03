using System.Drawing;
using System.Windows.Forms;

namespace MiniPhotoShop.Helpers
{
    public static class CoordinateHelper
    {
        public static Point? ConvertToImageCoordinates(PictureBox pb, Point mousePos)
        {
            if (pb.Image == null) return null;

            int w_i = pb.Image.Width;
            int h_i = pb.Image.Height;
            int w_c = pb.ClientSize.Width;
            int h_c = pb.ClientSize.Height;

            float ratio_i = (float)w_i / h_i;
            float ratio_c = (float)w_c / h_c;

            if (ratio_i > ratio_c)
            {
                float h_d = w_c / ratio_i;
                float y_offset = (h_c - h_d) / 2;
                if (mousePos.Y < y_offset || mousePos.Y > y_offset + h_d) return null;
                float scale = (float)w_i / w_c;
                return new Point((int)(mousePos.X * scale), (int)((mousePos.Y - y_offset) * scale));
            }
            else
            {
                float w_d = h_c * ratio_i;
                float x_offset = (w_c - w_d) / 2;
                if (mousePos.X < x_offset || mousePos.X > x_offset + w_d) return null;
                float scale = (float)h_i / h_c;
                return new Point((int)((mousePos.X - x_offset) * scale), (int)(mousePos.Y * scale));
            }
        }
    }
}