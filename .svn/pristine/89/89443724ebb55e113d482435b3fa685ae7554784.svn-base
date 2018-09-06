using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PylonC.NETSupportLibrary
{
    public partial class MySlider : UserControl
    {
        public event EventHandler ValueChanged;        
        public MySlider()
        {
            InitializeComponent();
        }

        public int _Value
        {
            get { return slider.Value; }
            set {
                slider.Value = value;
                textBox1.Text = value.ToString();
            }
        }

        public int _Maximum
        {
            get { return slider.Maximum; }
            set { slider.Maximum = value; }
        }

        public int _Minimum
        {
            get { return slider.Minimum; }
            set { slider.Minimum = value; }
        }

        public int _SmallChange
        {
            get { return slider.SmallChange; }
            set { slider.SmallChange = value; }
        }

        public string _Name
        {
            get { return labelName.Text; }
            set { labelName.Text = value; }
        }

        private void slider_Scroll(object sender, EventArgs e)
        {
            int _Value = slider.Value;
            if (_Value.ToString() != textBox1.Text.Trim())
            {
                textBox1.Text = slider.Value.ToString();
                ValueChanged(null, null);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int value = Convert.ToInt32(textBox1.Text);
                slider.Value = value;
                ValueChanged(null, null);
            }
            catch { }
        }
    }
}
