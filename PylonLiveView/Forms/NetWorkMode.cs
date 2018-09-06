using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using PylonLiveView;

namespace PylonLiveView
{
    public partial class NetWorkMode : Form
    {
        [DllImport("ZNetAdv.dll")]
        static extern UInt32 ZN_SearchAll();
        [DllImport("ZNetAdv.dll")]
        static extern UInt32 ZN_GetDevInfoUDPbyMACAndIP(
            [MarshalAs(UnmanagedType.LPStr)] string szmac,
            [MarshalAs(UnmanagedType.LPStr)] string szip,
            byte devtype);
        [DllImport("ZNetAdv.dll")]
        static extern UInt32 ZN_GetDevConfigUDP([MarshalAs(UnmanagedType.LPStr)] string szname,
            ref byte szval);
        [DllImport("ZNetAdv.dll")]
        static extern UInt32 ZN_GetSearchDev(ref byte szip,
            ref byte szver, ref byte szmac,
            ref byte pdevtype, ref byte pipmode, ref int ptcpport);
        [DllImport("ws2_32.dll")]
        private static extern int inet_addr(string cp);
        [DllImport("IPHLPAPI.dll")]
        private static extern int SendARP(Int32 DestIP, Int32 SrcIP, ref Int64 pMacAddr, ref Int32 PhyAddrLen);
        MyFunctions myFunc = new MyFunctions();
        private int m_ScanCount = 0;         //累計總共掃描了多少個IP

        public List<IPInfo> m_listScanResult = new List<IPInfo>();  //總共掃描得到的結果
        public IPInfo m_MasterIPInfo = new IPInfo("","");                             //發送作為主機的IPInfo

        private int _column, _row;
        public NetWorkMode()
        {
            InitializeComponent();
        }

        private void button_scan_Click(object sender, EventArgs e)
        {
            m_listScanResult.Clear();
            dataGridView_IPSearch.Rows.Clear();
            button_scan.Enabled = false;
            backgroundWorker_scan.RunWorkerAsync();
        }

        private void backgroundWorker_scan_DoWork(object sender, DoWorkEventArgs e)
        {
            //ZNetAdv广播搜索  -- UDP模式
            try
            {
                ZN_SearchAll();
                int times = 0;
                byte[] szip = new byte[20];
                byte[] szver = new byte[20];
                byte[] szmac = new byte[30];
                byte devtype = 0, ipmode = 0;
                int tcpport = 0;
                for (; times < 10000; times++)
                {
                    backgroundWorker_scan.ReportProgress(times);
                    while (ZN_GetSearchDev(ref szip[0], ref szver[0], ref szmac[0], ref devtype, ref ipmode, ref tcpport) == 1)
                    {
                        AnalysisDetailMessage(System.Text.Encoding.ASCII.GetString(szip).Replace("\0", "").Trim(),
                            System.Text.Encoding.ASCII.GetString(szmac).Replace("\0", "").Trim(),
                            Convert.ToByte(GetDevType(devtype).Replace("\0", "").Trim()));
                    }
                    Thread.Sleep(10);
                }
            }
            catch
            { throw new Exception("掃描錯誤，請檢查文件ZNetAdv.DLL是否存在."); }
        }

        private void backgroundWorker_scan_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == -1)
            {
                IPInfo info = (IPInfo)e.UserState;
                dataGridView_IPSearch.Rows.Add(info._IP, info._MAC, info._WorkPort);
            }
            else
            {
                progressBar1.Value = e.ProgressPercentage;
            }
        }

        private void backgroundWorker_scan_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button_scan.Enabled = true;
            progressBar1.Value = 0;
        }

        private string GetDevType(byte devtype)
        {
            Byte[] devarr = new Byte[20];
            string[] namearr = new string[20];
            devarr[0] = 1;
            devarr[1] = 2;
            devarr[2] = 3;
            devarr[3] = 4;
            devarr[4] = 5;
            devarr[5] = 6;
            devarr[6] = 7;
            devarr[7] = 8;
            devarr[8] = 11;
            devarr[9] = 12;
            devarr[10] = 13;
            devarr[11] = 48;

            namearr[0] = "ZNE-100";
            namearr[1] = "ZNE-100T";
            namearr[2] = "ZNE-10";
            namearr[3] = "ZNE-200T";
            namearr[4] = "NETCOM-10";
            namearr[5] = "ZNE-200";
            namearr[6] = "ZNE-10T";
            namearr[7] = "ZNE-100TI";
            namearr[8] = "CANET-E";
            namearr[9] = "CANET-100";
            namearr[10] = "CANET-200";
            namearr[11] = "NETCOM-400";

            int sz = 12, i = 0;
            string str1;
            for (i = 0; i < sz; i++)
            {
                if (devarr[i] == devtype)
                {
                    str1 = System.Convert.ToString(devtype) + "(" +
                        namearr[i] + ")";
                    return str1;
                }
            }
            str1 = System.Convert.ToString(devtype);
            return str1;
        }

        //解析指定IP的详细信息，包括开放工作端口等
        private void  AnalysisDetailMessage(string IP, string MAC, byte devType)
        {
            IPInfo ipinfo = new IPInfo(IP, MAC);
            try
            {
                uint res = ZN_GetDevInfoUDPbyMACAndIP(MAC, IP, devType);
                byte[] szval = new byte[100];
                if (res != 1)
                { return; }
                res = ZN_GetDevConfigUDP("C1_PORT", ref szval[0]);
                if (res == 1)
                    ipinfo._WorkPort = Convert.ToInt32(System.Text.Encoding.ASCII.GetString(szval).Replace("\0", ""));
            }
            catch
            { }
            finally
            {
                m_listScanResult.Add(ipinfo);
                backgroundWorker_scan.ReportProgress(-1, (object)ipinfo);
            }
        }

        //掃描指定IP的指定端口是否打開
        private void PortSearchThreadMethod(object IPObject)
        {
            TcpClient tcp = new TcpClient();
            //if (m_stopScan)   //---终止线程
            //{
            //    Thread.CurrentThread.Abort();
            //    tcp.Close();
            //    return;
            //}
            m_ScanCount++;

            IPAddress ScanIP = (IPAddress)IPObject;  //获得扫描IP地址
            Control.CheckForIllegalCrossThreadCalls = false;//多线程交替使用
            int detect_Port = 21000;
            try
            {
                tcp.Connect(ScanIP, detect_Port);
                //该处如果建立连接错误的话，将不执行下面的代码.. 
                string res_IP = ScanIP.ToString();
                tcp.Close();
                Thread.CurrentThread.Abort();
            }
            catch
            {
                Thread.CurrentThread.Abort();
            }
            Thread.Sleep(300);       
        }

        private void button_stopScan_Click(object sender, EventArgs e)
        {
            button_scan.Enabled = true;
            progressBar1.Value = 0;
        }

        private void dataGridView_IPSearch_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left) { return; }
            _row = e.RowIndex;
            _column = e.ColumnIndex;
        }

        private void toolStripMenuItem_AsMaster_Click(object sender, EventArgs e)
        {
            try
            {
                panel_master.Controls.Clear();
                IPModule ipmodule = new IPModule();
                if (dataGridView_IPSearch[0, _row].Value == null)
                {
                    MessageBox.Show("IP輸入不允許為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!myFunc.checkIPStringIsLegal(dataGridView_IPSearch[0, _row].Value.ToString()))
                {
                    MessageBox.Show("IP輸入不合法！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ipmodule._IPAddr = m_MasterIPInfo._IP = dataGridView_IPSearch[0, _row].Value.ToString();
                if (dataGridView_IPSearch[1, _row].Value != null)
                {
                    m_MasterIPInfo._MAC = dataGridView_IPSearch[1, _row].Value.ToString();
                }
                if (dataGridView_IPSearch[2, _row].Value == null)
                {
                    MessageBox.Show("工作端口輸入不允許為空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ipmodule._Port = m_MasterIPInfo._WorkPort = Convert.ToInt32(dataGridView_IPSearch[2, _row].Value.ToString());
                ipmodule.Parent = panel_master;
            }
            catch { }
        }

        //根據IP獲得MAC
        private string GetMacAddress(string hostip)//获取远程IP（不能跨网段）的MAC地址 
        {
            string Mac = "";
            try
            {
                Int32 ldest = inet_addr(hostip); //将IP地址从 点数格式转换成无符号长整型 
                Int64 macinfo = new Int64();
                Int32 len = 6;
                SendARP(ldest, 0, ref macinfo, ref len);
                string TmpMac = Convert.ToString(macinfo, 16).PadLeft(12, '0');//转换成16进制　　注意有些没有十二位 
                Mac = TmpMac.Substring(0, 2).ToUpper();// 
                for (int i = 2; i < TmpMac.Length; i = i + 2)
                {
                    Mac = TmpMac.Substring(i, 2).ToUpper() + "-" + Mac;
                }
            }
            catch (Exception Mye)
            {
                Mac = "获取远程主机的MAC错误：" + Mye.Message;
            }
            return Mac;
        }

        private void ToolStripMenuItem_addnew_Click(object sender, EventArgs e)
        {
            dataGridView_IPSearch.Rows.Add();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {

        }
    }
}
