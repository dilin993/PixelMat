using PixelMat.UI.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixelMat.UI
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            openFileDialog1.Filter = UIHelper.GetImageFileFilter();
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (String filename in openFileDialog1.FileNames)
                {
                    if (!imageList1.Images.ContainsKey(filename))
                    {
                        var item = listView1.Items.Add(Path.GetFileName(filename));
                        var img = UIHelper.ResizeImageToSize(Image.FromFile(filename), new Size(128, 128));
                        imageList1.Images.Add(filename, img);
                        item.ImageKey = filename;
                    }
                }
            }
        }

        private void OpenEditor()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                String filename = listView1.SelectedItems[0].ImageKey;
                FormEditor formEditor = new FormEditor(filename);
                this.Hide();
                formEditor.ShowDialog();
                this.Show();
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            OpenEditor();
        }
    }
}
