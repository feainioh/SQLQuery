using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Euresys.Open_eVision_1_2;
using System.Threading;
using System.IO;
using System.Drawing.Imaging;


namespace PylonLiveView
{
    public partial class Welcome : Form
    {
        List<Panel> m_panelList = new List<Panel>();
        int count = 1; //1-6 
        MyFunctions myfunction = new MyFunctions();
        ManualResetEvent startMain = new ManualResetEvent(false);
        public Welcome()
        {
            UpdateClass UC = new UpdateClass();
            UC.GetVersion();
            MyFunctions.createNewLnk();// 创建快捷方式 [11/10/2017 617004]
            logWR.checkLogfileExist();// 延后日志操作以便移动目录 [11/8/2017 617004]
            InitializeComponent();
            //Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.NoneEnabled;

        }


        private void button_select_Click(object sender, EventArgs e)
        {
            if (radioButton_mic.Checked)
            {
                //GlobalVar.gl_FlowID = 7;
                GlobalVar.gl_LinkType = LinkType.MIC;
                label_type.Text = "MIC關聯";
            }
            else if (radioButton_prox.Checked)
            {
                //GlobalVar.gl_FlowID = 6;
                GlobalVar.gl_LinkType = LinkType.PROX;
                label_type.Text = "PROX關聯";
            }
            else if (radioButton_Barcode.Checked)
            {
                GlobalVar.gl_LinkType = LinkType.BARCODE;
                label_type.Text = "BARCODE關聯";
            }
            else if (radioButton_IC.Checked)//增加IC关联  [2018.1.10]-lqz
            {
                GlobalVar.gl_LinkType = LinkType.IC;
                label_type.Text = "IC關聯";
            }
            WriteLinkType(label_type.Text);
            try
            {
                if (GlobalVar.gl_LineName == "")
                {
                    MessageBox.Show("当前线别未选择");
                    return;
                }
                DBQuery db = new DBQuery();
                DataTable dt = db.GetLineListInfo();
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("数据库中没有线别数据，请检查线别是否选择正确！");
                    //return;
                }
            }
            catch
            {
                MessageBox.Show("线别参数读取错误", "请注意");
                //Application.Exit();
                //return;
                GlobalVar.gl_LineName = "NONET";
            }
            groupBox1.Visible = false;
            button_select.Visible = false;
            button_cancel.Visible = false;
            chooseLineName1.Visible = false;
            Thread thd_WriteFile = new Thread(writeLineNumber);
            thd_WriteFile.IsBackground = true;
            thd_WriteFile.Start();
            startUp();
        }
        /// <summary>
        /// 写入关联类型
        /// </summary>
        /// <param name="p"></param>
        private void WriteLinkType(string p)
        {
            try
            {
                string iniFilePath = GlobalVar.gl_strTargetPath + "\\" + GlobalVar.gl_iniTBS_FileName;
                myfunction.CheckFileExit();
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_LinkType, p, iniFilePath);
            }
            catch { }
        }

        private void ReadLinkType() 
        { 
            try
            {
                StringBuilder str_tmp = new StringBuilder();
                string iniFilePath = GlobalVar.gl_strTargetPath + "\\" + GlobalVar.gl_iniTBS_FileName;
                myfunction.CheckFileExit();
                MyFunctions.GetPrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_LinkType, "", str_tmp, 50, iniFilePath);
                GlobalVar.gl_Link = str_tmp.ToString().Trim();
                if (GlobalVar.gl_Link == "BARCODE關聯")
                {
                    GlobalVar.gl_LinkType = LinkType.BARCODE;
                }
                else if (GlobalVar.gl_Link == "PROX關聯")
                {
                    GlobalVar.gl_LinkType = LinkType.PROX;
                }
                else if (GlobalVar.gl_Link == "MIC關聯")
                {
                    GlobalVar.gl_LinkType = LinkType.MIC;
                }
                else if (GlobalVar.gl_Link == "IC關聯")//增加IC关联  [2018.1.10]-lqz
                {
                    GlobalVar.gl_LinkType = LinkType.IC;
                }
                else
                {
                    GlobalVar.gl_LinkType = LinkType.MIC;                    
                }                
            }
            catch { }
        }
        
        private void writeLineNumber()  //写设置线别的文件
        {
            try
            {
                string iniFilePath = GlobalVar.gl_strTargetPath + "\\" + GlobalVar.gl_iniTBS_FileName;
                myfunction.CheckFileExit();
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_inikey_Line, GlobalVar.gl_LineName, iniFilePath);
            }
            catch { }
        }

        private void startUp()
        {
            try
            {
                Thread thread_initEvision = new Thread(initEvision);
                thread_initEvision.IsBackground = true;
                thread_initEvision.Start();
                //startMain.WaitOne();
            }
            catch
            {
                /* Stop the grabbing. */
                try
                {
                    if (GlobalVar.gl_imageProvider != null)
                        GlobalVar.gl_imageProvider.Stop();
                }
                catch { }
                try
                {
                    if (GlobalVar.gl_imageProvider != null)
                        GlobalVar.gl_imageProvider.Close();
                }
                catch { }
            }
        }

        private void initEvision()
        {
            try
            {
                MatrixDecode decoder = new MatrixDecode();
                EMatrixCode EMatrixCodeReaderResult = new EMatrixCode();
                EMatrixCodeReader EMatrixCodeReader1 = new EMatrixCodeReader();
                EMatcher match = new EMatcher();
                Bitmap bmp = new Bitmap(640, 480);
                EImageBW8 bw8image = ConvertBitmapToEImageBW8(bmp);
            }
            catch { }
            finally
            {
                this.DialogResult = DialogResult.OK;
                //startMain.Set();
            }
        }

        public EImageBW8 ConvertBitmapToEImageBW8(Bitmap bmp)
        {
            try
            {
                EImageBW8 EBW8Image1 = new EImageBW8(bmp.Width, bmp.Height); // EImageBW8 instance
                //EImageC24 eimageC24 = new EImageC24(bmp.Width, bmp.Height); // EImageC24 instance

                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                //锁定位图
                System.Drawing.Imaging.BitmapData bmpdata_src = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                //获取首行地址
                IntPtr pScan0 = bmpdata_src.Scan0;
                unsafe
                {
                    try
                    {
                        for (int Height = 0; Height < bmpdata_src.Height; Height++)
                        {
                            byte* pSrc = (byte*)pScan0;
                            pSrc += bmpdata_src.Stride * Height;
                            byte* pDest = (byte*)EBW8Image1.GetImagePtr(0, Height);
                            for (int Width = 0; Width < bmpdata_src.Width; Width++)
                            {
                                pDest[0] = (byte)(pSrc[0] * 0.3 + pSrc[1] * 0.6 + pSrc[2] * 0.1);
                                pSrc += 3;
                                pDest++;
                            }
                        }
                    }
                    catch { }
                }
                bmp.UnlockBits(bmpdata_src);
                return EBW8Image1;
            }
            catch
            {
                return new EImageBW8(640, 480);
            }
        }

        private void button_quit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void Welcome_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Welcome_Shown(object sender, EventArgs e)
        {
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定退出当前程序", "退出", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                System.Environment.Exit(0);//退出当前进程
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            ReadLinkType();
            if (GlobalVar.gl_LinkType == LinkType.BARCODE)
            {
                radioButton_Barcode.Checked = true;
            }
            else if (GlobalVar.gl_LinkType == LinkType.MIC)
            {
                radioButton_mic.Checked = true;
            }
            else if (GlobalVar.gl_LinkType == LinkType.PROX)
            {
                radioButton_prox.Checked = true;
            }
            else if (GlobalVar.gl_LinkType == LinkType.IC)//增加IC关联  [2018.1.10]-lqz
            {
                radioButton_IC.Checked = true;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                GlobalVar.gl_lsChkItem = DBQuery.GetErrorList();
                if (GlobalVar.gl_lsChkItem == null || GlobalVar.gl_lsChkItem.Count <= 0)
                {
                    if (DialogResult.OK == logWR.Warning("获取错误提示信息失败，程序是否退出？"))
                    {
                        Environment.Exit(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\nOpeneVision模块初始化失败!OpeneVision可能已经过期！");
                Application.Exit();
            }
        }
    }
}