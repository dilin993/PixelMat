using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelMat.UI.Utility
{
    class UIHelper
    {
        public static string GetImageFileFilter()
        {
            StringBuilder allImageExtensions = new StringBuilder();
            string separator = "";
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            Dictionary<string, string> images = new Dictionary<string, string>();
            foreach (ImageCodecInfo codec in codecs)
            {
                allImageExtensions.Append(separator);
                allImageExtensions.Append(codec.FilenameExtension);
                separator = ";";
                images.Add(string.Format("{0} Files: ({1})", codec.FormatDescription, codec.FilenameExtension),
                           codec.FilenameExtension);
            }
            StringBuilder sb = new StringBuilder();
            if (allImageExtensions.Length > 0)
            {
                sb.AppendFormat("{0}|{1}", "All Images", allImageExtensions.ToString());
            }
            images.Add("All Files", "*.*");
            foreach (KeyValuePair<string, string> image in images)
            {
                sb.AppendFormat("|{0}|{1}", image.Key, image.Value);
            }
            return sb.ToString();
        }

        public static Image ResizeImageToSize(Image src, Size size)
        {
            double alpha = Math.Atan2(src.Height, src.Width);
            double beta = Math.Atan2(size.Height, size.Width);
            int width, height;
            if (alpha < beta)
            {
                width = size.Width;
                height = (int)Math.Round(width * Math.Tan(alpha));
            }
            else
            {
                height = size.Height;
                width = (int)Math.Round(height / Math.Tan(alpha));
            }
            Image dst = new Bitmap(size.Width, size.Height);
            Image srcResized = new Bitmap(src, width, height);
            Graphics graphics = Graphics.FromImage(dst);
            int x = (int)Math.Round((size.Width - width) / 2.0);
            int y = (int)Math.Round((size.Height - height) / 2.0);
            graphics.Clear(Color.Transparent);
            graphics.DrawImage(srcResized, x, y);
            return dst;
        }
    }
}
