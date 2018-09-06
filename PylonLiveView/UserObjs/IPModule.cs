using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PylonLiveView
{
    public partial class IPModule : UserControl
    {
        public int m_Port_SacnEquip;
        public string m_IPAddr_ScanEquip;  //扫描机IP
        MyFunctions myfunc = new MyFunctions();
        public IPModule()
        {
            InitializeComponent();
        }

        public string _IPAddr
        {
            get { return m_IPAddr_ScanEquip; }
            set { SetIPAddr(value); }
        }

        public int _Port
        {
            get { return m_Port_SacnEquip; }
            set
            {
                m_Port_SacnEquip = value;
                textBox_port.Text = m_Port_SacnEquip.ToString();
            }
        }

        //在显示辅机连接信息时，设置为disable
        public bool _PortEnabel
        {
            get { return textBox_port.Enabled; }
            set { textBox_port.Enabled = value;}
        }

        //設定IP
        private void SetIPAddr(string str)
        {
            bool result = true;
            Regex reg = new Regex(@"[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}");
            if (!myfunc.checkIPStringIsLegal(str))
            {
                m_IPAddr_ScanEquip = "0.0.0.0";
            }
            else
            {
                string[] _strList = str.Split('.');
                for (int i = 0; i < 4; i++)
                {
                    result &= checkInputIsValid(_strList[i]);
                    result &= (Convert.ToInt32(_strList[i]) <= 255);
                }
                if (result)
                {
                    m_IPAddr_ScanEquip = str;
                    textBox_ip1.Text = _strList[0];
                    textBox_ip2.Text = _strList[1];
                    textBox_ip3.Text = _strList[2];
                    textBox_ip4.Text = _strList[3];
                }
                else
                {
                    m_IPAddr_ScanEquip = "0.0.0.0";
                }
            }
        }

        //检测IP输入是否有效，如果有效返回IP地址
        private string GetIPInput()
        {
            try
            {
                if (checkInputIsValid(textBox_ip1.Text.Trim())
                     && checkInputIsValid(textBox_ip2.Text.Trim())
                     && checkInputIsValid(textBox_ip3.Text.Trim())
                     && checkInputIsValid(textBox_ip4.Text.Trim()))
                {
                    return textBox_ip1.Text.Trim() + "." + textBox_ip2.Text.Trim() + "."
                        + textBox_ip3.Text.Trim() + "." + textBox_ip4.Text.Trim();
                }
                else
                { return "0.0.0.0"; }
            }
            catch { return "0.0.0.0"; }
        }

        //判断长度为3位，并且全部为数字，并且不大于255
        private bool checkInputIsValid(string str)
        {
            try
            {
                bool result = true;
                result = ((str.Length <= 3) ? true : false);
                for (int i = 0; i < str.Length; i++)
                {
                    char c = str[i];
                    result &= ((c >= 48) && (c <= 57));
                }
                result &= (Convert.ToInt32(str) <= 255);
                return result;
            }
            catch
            { return false; }
        }
    }
}
