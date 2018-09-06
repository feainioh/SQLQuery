using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PylonLiveView
{
    public partial class magnify : Form
    {
        private Color resultColor;
        private string resultStr;
        public magnify(Bitmap _picture)
        {
            InitializeComponent();
            pictureBox1.BackgroundImage = _picture;
        }

        public magnify(Bitmap _picture, string decodeStr)
        {
            InitializeComponent();
            pictureBox1.BackgroundImage = _picture;

            if (decodeStr.Length == 0)
            {
                label_sn.ForeColor = Color.Red;
                label_sn.Text = "解析失败";
                label_sn.Location = new Point(this.Width / 2 - label_sn.Width / 2, this.Height - 50);
            }
            else
            {
                label_sn.ForeColor = Color.Aqua;
                label_sn.Text = decodeStr;
            }
        }

        internal void setResult(string HalconResult,Color color)
        {
            if (HalconResult == null)
            {
              //  resultStr = "无解析结果！";
                resultColor = color;
            }else if (color == Color.Red)
            {
                resultStr = "解析失败！";
                resultColor = color;
            }
            else
            {
                resultStr = HalconResult;
                resultColor = color;
            }
        }

        private void magnify_Load(object sender, EventArgs e)
        {
            label1.Text = resultStr;
            label1.ForeColor = resultColor;
        }
    }
}
