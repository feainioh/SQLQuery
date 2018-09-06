using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PylonLiveView;

namespace PylonLiveView
{
    public partial class ShowPsdErrForm : Form
    {
        private bool m_needpasswordConfirm = true;
        public string errStr
        {
            set { label_errMsg.Text = value; }
        }

        public ShowPsdErrForm(string errValue, bool NeedPasswordConfirm)
        {
            InitializeComponent();
            m_needpasswordConfirm = NeedPasswordConfirm;
            label1.Visible = textBox_password.Visible = NeedPasswordConfirm;
            richTextBox1.Text = errValue;
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (m_needpasswordConfirm)
            {
                if (textBox_password.Text.Trim().ToUpper()
                    != GlobalVar.gl_password_confirm)
                {
                    MessageBox.Show("密碼錯誤", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void ShowPsdErrForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
