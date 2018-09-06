using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace PylonLiveView
{
    public partial class LogonOn : Form
    {
        public LogonOn()
        {
            InitializeComponent();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            string psd=DateTime.Now.ToString("yyyyMMdd");
            if (textBox_input.Text.ToUpper() != psd)
            {
                MessageBox.Show("密碼輸入錯誤，請重新輸入!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox_input.SelectAll();
                this.DialogResult = DialogResult.None;
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox_input_MouseDown(object sender, MouseEventArgs e)
        {
            //Process.Start(@"C:\WINDOWS\system32\osk.exe"); 
        }
    }
}
