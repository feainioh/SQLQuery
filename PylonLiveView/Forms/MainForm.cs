using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using PylonC.NETSupportLibrary;
using PylonLiveView;
using System.IO;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using Euresys.Open_eVision_1_2;
using PylonLiveView;
using System.Reflection;
using System.Diagnostics;
using MainSpace.PCI1245;
using Advantech.Motion;
using MainSpace.CustomPubClass;
using System.IO.Ports;


namespace PylonLiveView
{
    /* The main window. */
    public partial class MainForm : Form
    {
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);
        private static PCI1245_E m_form_movecontrol = new PCI1245_E();
        private Bitmap m_bitmap = null; /* The bitmap is used for displaying the image. */
        int m_current_num = 0; //记录当前获得的图片序号，一张测完后重置
        //标志位
        //private bool m_ScanAuthorized = false; //允许作业
        private bool m_inScanFunction = false; //busy标志
        //private int m_status_MachineMoveFinished = 0;   //下位机扫描所有条码完成标志，完成=1，然后手动置0；
        //private bool m_tag_inCalibrate = false; //是否正在校准中
        private bool m_tag_CalibrateOK = false; //校準是否完成，否則照片不能當作製品照片
        private bool m_tag_InCheckAllDecodeFinished = false;  //是否在对所有BLOCK进行 decode finish check
        private bool m_tag_DBQueryFinished = false;   //检测到条码后，是否完成查询条码的被打孔不良位置
        private bool m_tag_ShifttoStage2Checked = false;  //进入第二工位，只需要检测一次标志

        //累计mark点校准次数，如果超过5次校准失败，提示校准失败，将校准偏移值设置为0,继续动作
        private int m_times_duplicateCalibrate = 0;

        //线圈状态
        private bool m_coilstatus_workmode = false;          //B00001      作业允许(0:扫描模式  1:通过模式)
        private bool m_coilstatus_EmergenceError = false;       //B00002     紧急停止(0:正常  1:紧急停止) 
        private bool m_coilstatus_Led = false;                  //B00004     光源状态(0:OFF   1:ON)
        private bool m_coilstatus_CCDTrigger = false;           //B00005     作业允许(1:触发) 
        private bool m_coilstatus_FixScan = false;              //B00006     条码补扫(0:PASS   1:需要补扫) 
        //private bool m_coilstatus_SheetBarcodeScan = false;     //B00007     整张条码扫描(0:FAIL   1:扫描成功) 
        private bool m_coilstatus_Stage2Arrived = false;        //B00008     载板到达测试，开始MARK校准
        private bool m_coilstatus_ShiftToStage2 = false;        //B00011     载板经过扫描点进入第二工位
        private bool m_coilstatus_ArrivedScanPos = false;       //

        private List<BitmapInfo> m_list_bmpReceived;
        private EventWaitHandle m_wait_picReceived = new EventWaitHandle(false, EventResetMode.AutoReset);  //运动到位，等待制品拍照完毕；

        private ScanbarcodeInfo m_barcodeinfo_preScan = new ScanbarcodeInfo();      //第一工位扫描到，临时存储(如果急停的话需要清空)
        private ScanbarcodeInfo m_barcodeinfo_CurrentUse = new ScanbarcodeInfo();    //到达第二工位，用作正式条码  
        //private string m_barcode_preScan = "";        //第一工位扫描到，临时存储(如果急停的话需要清空)
        //private List<int> m_listNGPosition_preScan = new List<int>();
        //private string m_barcode_CurrentUse = "";     //到达第二工位，用作正式条码
        //private List<int> m_listNGPosition_CurrentUse = new List<int>();
        private int m_count_BoardIn = 0;     //进板出版累计， 用于统计BOX内部是否有板

        private Thread thd_DeleteLog = null, thd_DeleteLog1 = null, thd_DeleteLog2 = null, thd_DeleteLog3 = null;
        MyFunctions myfunc = new MyFunctions();
        Modbus m_modbus;
        DBQuery m_DBQuery = new DBQuery();

        OBJ_DWGDirect m_obj_dwg;
        //实际尺寸与视图区域比例
        double m_ratio_Width = 1.00;
        double m_ratio_Height = 1.00;
        //图纸坐标中计算的电脑坐标系参考原点
        float refOrg_x = 0.00F;
        float reforg_y = 0.00F;
        bool m_initOK = true;  //用作判断构造函数是否执行成功，如果不成功则销毁
        /* Set up the controls and events to be used and update the device list. */
        public MainForm()
        {
            try
            {
                InitializeComponent();
                myfunc.ReadGlobalInfoFromTBS();
                openScanPort();
                updatetestinfo();
                //Thread t_welcome = new Thread(new ThreadStart(
                //    delegate
                //    {
                //BeginBeginInvoke(new Action(() =>
                //{
                Welcome w = new Welcome();
                if (w.ShowDialog() != DialogResult.OK)
                {
                    m_initOK = false;
                    System.Environment.Exit(0);
                }
                //}));
                //    }));
                //t_welcome.IsBackground = true;
                //t_welcome.Start();
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
                if (attributes.Length > 0)
                {
                    tsslbl_ver.Text = "軟件版本：" + ((AssemblyFileVersionAttribute)attributes[0]).Version;
                }
                logWR.checkLogfileExist();

                //initImageProvider();   //fotest ltt
                baslerCCD1.InitCCDCamera();
                baslerCCD1.event_bmpReceive += new Basler.BaslerCCD.dele_bmpReceived(baslerCCD_event_bmpReceive);

                m_form_movecontrol.Parent = tabPage_1020;
                //使用经典样式界面
                Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.NoneEnabled;

                m_obj_dwg = new OBJ_DWGDirect();
                m_obj_dwg.Dock = DockStyle.Fill;
                m_obj_dwg.eve_fileLoaded += new dele_CADFileLoaded(m_obj_dwg_eve_fileLoaded);
                //m_obj_dwg.eve_sendTPMessage += new dele_SendMessage(m_obj_dwg_eve_sendTPMessage);
                m_obj_dwg.eve_sendReFPoint += new dele_SendRefPoint(m_obj_dwg_eve_sendReFPoint);
                m_obj_dwg.eve_returnMechicalOrgPoint += new dele_SendMessage(returnMachicalOrgPoint);
                m_obj_dwg.eve_returnRefPoint += new dele_SendMessage(returnreferecePoint);
                m_obj_dwg.eve_sendFixMotion += new dele_SendFixMotion(m_obj_dwg_eve_sendFixMotion);
                m_obj_dwg.eve_sendCalPosition += new dele_SendFixMotion(m_obj_dwg_eve_sendCalPosition);
                m_obj_dwg.Parent = tabPage_CADView;
                this.MouseWheel += new MouseEventHandler(m_obj_dwg.OBJ_DWGDirect_MouseWheel);
                this.FormClosing += new FormClosingEventHandler(m_obj_dwg.On_control_Closing);

                m_form_movecontrol.eve_SheetBarcodeScan += new EventHandler(m_form_movecontrol_eve_SheetBarcodeScan);
                m_form_movecontrol.eve_BoardArrived += new EventHandler(m_form_movecontrol_eve_BoardArrived);
                m_form_movecontrol.eve_EmergeceStop += new EventHandler(m_form_movecontrol_eve_EmergeceStop);
                m_form_movecontrol.eve_EmergenceRelease += new EventHandler(m_form_movecontrol_eve_EmergenceRelease);
                m_form_movecontrol.eve_MotionMsg += new dele_MotionMsg(m_form_movecontrol_eve_MotionMsg);
                m_form_movecontrol.eve_SofetyDoor += new EventHandler(m_form_movecontrol_eve_SofetyDoor);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        /// <summary>
        /// 安全门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void m_form_movecontrol_eve_SofetyDoor(object sender, EventArgs e)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    if (!GlobalVar.gl_safetyDoor_Front && GlobalVar.gl_usermode == 0)
                    {
                        WorkPermitted(false); //安全门未关，禁止作业
                        button_status.Text = "前门未关";
                    }
                    else
                    {
                        button_status.Text = "待機中";
                    }
                }));
            }
            catch { }
        }

        /// <summary>
        /// 接收到图片并处理(只处理自动拍照)
        /// </summary>
        /// <param name="bmp">图片</param>
        /// <param name="isManualPic">是否为手动拍照</param>
        public void baslerCCD_event_bmpReceive(ImageProvider.Image image)
        {
            try
            {
                if (image != null)
                {
                    if (GlobalVar.m_ScanAuthorized && m_tag_CalibrateOK)
                    {
                        m_wait_picReceived.Set();
                    }
                    /* Check if the image is compatible with the currently used bitmap. */
                    if (BitmapFactory.IsCompatible(m_bitmap, image.Width, image.Height, image.Color))
                    {
                        /* Update the bitmap with the image data. */
                        BitmapFactory.UpdateBitmap(m_bitmap, image.Buffer, image.Width, image.Height, image.Color);
                        /* To show the new image, request the display control to update itself. */
                        pictureBox_capture.Refresh();
                    }
                    else /* A new bitmap is required. */
                    {
                        BitmapFactory.CreateBitmap(out m_bitmap, image.Width, image.Height, image.Color);
                        BitmapFactory.UpdateBitmap(m_bitmap, image.Buffer, image.Width, image.Height, image.Color);
                        /* We have to dispose the bitmap after assigning the new one to the display control. */
                        Bitmap bitmap = pictureBox_capture.Image as Bitmap;
                        pictureBox_capture.Image = m_bitmap;
                        if (bitmap != null)
                        {
                            /* Dispose the bitmap. */
                            bitmap.Dispose();
                        }
                    }

                    m_bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    if (m_bitmap_calibrate_REF == null) { m_bitmap_calibrate_REF = (Bitmap)m_bitmap.Clone(); return; }
                    if (m_bitmap_calibrate_END == null) { m_bitmap_calibrate_END = (Bitmap)m_bitmap.Clone(); return; }
                    if (GlobalVar.m_ScanAuthorized && m_tag_CalibrateOK)
                    {
                        BitmapInfo bi = new BitmapInfo();
                        bi.FlowID = GlobalVar.gl_CurrentFlowID;
                        bi.bitmap = (Bitmap)m_bitmap.Clone();
                        bi.num = m_current_num;
                        m_list_bmpReceived.Add(bi);
                    }
                }
            }
            catch { }
        }

        private void NewPartNoLoad()
        {
            this.Invoke(new Action(() =>
            {
                lbl_refMarkX.Text = "";
                lbl_refMarkY.Text = "";
            }));
            // string LinkType = GlobalVar.gl_LinkType.ToString();  //PROX or MIC
            //string iniFilePath = Application.StartupPath + "\\" + GlobalVar.gl_ProductModel + "\\" + GlobalVar.gl_LinkType + "\\" + GlobalVar.gl_ProductModel.ToUpper() + "_MAPPING.INI";
            string iniFilePath = Application.StartupPath + "\\" + GlobalVar.gl_ProductModel + "\\" + GlobalVar.gl_LinkType + "\\" + GlobalVar.gl_ProductModel.ToUpper() + "_MAPPING.INI";
            if (!File.Exists(iniFilePath))
            {
                MessageBox.Show("沒有找到特殊路径下映射配置文檔，程序關閉，請確認！");
                DialogResult = DialogResult.Cancel;
                //Application.Exit();
                return;
            }
            GlobalVar.gl_matchFileName = GlobalVar.gl_SpecialPath + "\\" + GlobalVar.gl_ProductModel.ToUpper() + ".MCH";
            GlobalVar.gl_matchFileName = Application.StartupPath + "\\" + GlobalVar.gl_ProductModel + "\\" + GlobalVar.gl_LinkType + "\\" + GlobalVar.gl_ProductModel.ToUpper() + ".MCH";//修改为读取本地模式
            if (!File.Exists(GlobalVar.gl_matchFileName))
            {
                MessageBox.Show("沒有找到本地路径下對應品目MCH文件：" + GlobalVar.gl_matchFileName + "，程序關閉，請確認!");
                //MessageBox.Show("沒有找到特殊路径下對應品目MCH文件：" + GlobalVar.gl_matchFileName + "，程序關閉，請確認!");
                DialogResult = DialogResult.Cancel;
                return;
            }
            myfunc.ReadRefPointInfoFromTBS();
            checkConfigFolderExist();
            setRefPointValue(GlobalVar.gl_Ref_Point_Axis.Pos_X.ToString("0.000"),
                GlobalVar.gl_Ref_Point_Axis.Pos_Y.ToString("0.00"));
            m_form_movecontrol.AutoHomeSearch_Manual();
            //自动导入CAD文档
            string CADFile = Application.StartupPath + "\\" + GlobalVar.gl_ProductModel + "\\" + GlobalVar.gl_LinkType + "\\" + GlobalVar.gl_ProductModel.ToUpper() + ".DWG";
            //string CADFile = GlobalVar.gl_SpecialPath  + "\\" + GlobalVar.gl_ProductModel.ToUpper() + ".DWG";
            if (!File.Exists(CADFile))
            {
                MessageBox.Show("沒有找到特殊路径下当前品目的CAD文档，请使用手动导入，請確認！");
            }
            else
            {
                m_obj_dwg.LoadCADFile(CADFile, 1);
            }
            this.Invoke(new Action(() =>
            {
                lbl_refMarkX.Text = GlobalVar.gl_point_CalPosRef.Pos_X.ToString();
                lbl_refMarkY.Text = GlobalVar.gl_point_CalPosRef.Pos_Y.ToString();
            }));
        }
        //网络加载
        private void NewPartNoNetLoad()
        {
            myfunc.LoadShare();
            this.Invoke(new Action(() =>
            {
                lbl_refMarkX.Text = "";
                lbl_refMarkY.Text = "";
            }));
            string LinkType = GlobalVar.gl_LinkType.ToString();  //PROX or MIC
            string iniFilePath = GlobalVar.gl_netPath + GlobalVar.gl_appName + "\\" + GlobalVar.gl_ProductModel + "\\" + LinkType + "\\" + GlobalVar.gl_ProductModel.ToUpper() + "_MAPPING.INI";
            if (!File.Exists(iniFilePath))
            {
                MessageBox.Show("沒有找到网络路径下映射配置文檔，程序關閉，請確認！");
                DialogResult = DialogResult.Cancel;
                //Application.Exit();
                return;
            }
            GlobalVar.gl_matchFileName = GlobalVar.gl_netPath + GlobalVar.gl_appName + "\\" + GlobalVar.gl_ProductModel + "\\" + LinkType + "\\" + GlobalVar.gl_ProductModel.ToUpper() + ".MCH";
            GlobalVar.gl_matchFileName = Application.StartupPath + "\\" + GlobalVar.gl_ProductModel + "\\" + GlobalVar.gl_LinkType + "\\" + GlobalVar.gl_ProductModel.ToUpper() + ".MCH";//修改为读取本地模式
            if (!File.Exists(GlobalVar.gl_matchFileName))
            {
                MessageBox.Show("沒有找到本地路径下對應品目MCH文件：" + GlobalVar.gl_matchFileName + "，程序關閉，請確認!");
                //MessageBox.Show("沒有找到网络路径下對應品目MCH文件：" + GlobalVar.gl_matchFileName + "，程序關閉，請確認!");
                DialogResult = DialogResult.Cancel;
                return;
            }
            myfunc.ReadRefPointInfoFromTBS();
            checkConfigFolderExist();
            setRefPointValue(GlobalVar.gl_Ref_Point_Axis.Pos_X.ToString("0.000"),
                GlobalVar.gl_Ref_Point_Axis.Pos_Y.ToString("0.00"));
            m_form_movecontrol.AutoHomeSearch_Manual();
            //自动导入CAD文档
            string CADFile = GlobalVar.gl_netPath + GlobalVar.gl_appName + "\\" + GlobalVar.gl_ProductModel + "\\" + LinkType + "\\" + GlobalVar.gl_ProductModel.ToUpper() + ".DWG";
            if (!File.Exists(CADFile))
            {
                MessageBox.Show("沒有找到当前品目的CAD文档，请使用手动导入，請確認！");
            }
            else
            {
                m_obj_dwg.LoadCADFile(CADFile, 1);
            }
            this.Invoke(new Action(() =>
            {
                lbl_refMarkX.Text = GlobalVar.gl_point_CalPosRef.Pos_X.ToString();
                lbl_refMarkY.Text = GlobalVar.gl_point_CalPosRef.Pos_Y.ToString();
            }));
        }

        //读取通用文档--2017.10.07
        private bool CommenConfigLoad()
        {
            bool result = false;
            myfunc.LoadShare();
            this.Invoke(new Action(() =>
            {
                lbl_refMarkX.Text = "";
                lbl_refMarkY.Text = "";
            }));
            string iniFilePath = GlobalVar.gl_netPath + GlobalVar.gl_appName + "\\" + GlobalVar.gl_ProductModel + "\\" + GlobalVar.gl_ProductModel.ToUpper() + "_MAPPING.INI";
            if (!File.Exists(iniFilePath))
            {
                //MessageBox.Show("沒有找到网络路径下通用映射配置文檔，程序關閉，請確認！");
                //DialogResult = DialogResult.Cancel;
                //Application.Exit();
                return result;
            }
            //GlobalVar.gl_matchFileName = GlobalVar.gl_netPath + GlobalVar.gl_appName + "\\" + GlobalVar.gl_ProductModel + "\\" + LinkType + "\\" + GlobalVar.gl_ProductModel.ToUpper() + ".MCH";
            GlobalVar.gl_matchFileName = Application.StartupPath + "\\" + GlobalVar.gl_ProductModel + "\\" + GlobalVar.gl_LinkType + "\\" + GlobalVar.gl_ProductModel.ToUpper() + ".MCH";//修改为读取本地模式
            if (!File.Exists(GlobalVar.gl_matchFileName))
            {
                //MessageBox.Show("沒有找到本地路径下對應品目MCH文件：" + GlobalVar.gl_matchFileName + "，程序關閉，請確認!");
                //MessageBox.Show("沒有找到网络路径下對應品目MCH文件：" + GlobalVar.gl_matchFileName + "，程序關閉，請確認!");
                //DialogResult = DialogResult.Cancel;
                return result;
            }
            myfunc.ReadRefPointInfoFromTBS();
            checkConfigFolderExist();
            setRefPointValue(GlobalVar.gl_Ref_Point_Axis.Pos_X.ToString("0.000"),
                GlobalVar.gl_Ref_Point_Axis.Pos_Y.ToString("0.00"));
            m_form_movecontrol.AutoHomeSearch_Manual();
            //自动导入CAD文档
            string CADFile = GlobalVar.gl_netPath + GlobalVar.gl_appName + "\\" + GlobalVar.gl_ProductModel + "\\" + GlobalVar.gl_ProductModel.ToUpper() + ".DWG";
            if (!File.Exists(CADFile))
            {
                //MessageBox.Show("沒有找到当前品目的通用CAD文档，请使用手动导入，請確認！");
                DialogResult = DialogResult.Cancel;
                return result;
            }
            else
            {
                m_obj_dwg.LoadCADFile(CADFile, 1);
                result = true;
            }
            this.Invoke(new Action(() =>
            {
                lbl_refMarkX.Text = GlobalVar.gl_point_CalPosRef.Pos_X.ToString();
                lbl_refMarkY.Text = GlobalVar.gl_point_CalPosRef.Pos_Y.ToString();
            }));
            return result;
        }

        //检测品目配置文件夹是否存在，如果不存在则创建
        private void checkConfigFolderExist()
        {
            if (!Directory.Exists(Application.StartupPath + "\\" + GlobalVar.gl_ProductModel))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\" + GlobalVar.gl_ProductModel);
            }
        }
        //获取设备编号
        private void getDeviecId()
        {
            string strPorductTypeINI = GlobalVar.gl_strTargetPath + "\\" + GlobalVar.gl_iniTBS_FileName;
            StringBuilder str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_strDeviceID, "", str_tmp, 500, strPorductTypeINI);
            GlobalVar.gl_DeviceID = str_tmp.ToString();
            txtbox_DeviceID.Text = GlobalVar.gl_DeviceID;
            //Z轴 2017.12.18
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_iniSection_AxisZRef, GlobalVar.gl_inikey_lastLinkType, "", str_tmp, 50, Application.StartupPath + "\\config.ini");
            switch (str_tmp.ToString())
            {
                case "1":
                    GlobalVar.gl_LastLinkType = LinkType.PROX;
                    break;
                case "2":
                    GlobalVar.gl_LastLinkType = LinkType.MIC;
                    break;
                case "3":
                    GlobalVar.gl_LastLinkType = LinkType.BARCODE;
                    break;
                case "4":
                    GlobalVar.gl_LastLinkType = LinkType.IC;
                    break;
                default:
                    GlobalVar.gl_LastLinkType = LinkType.DEFAULT;
                    break;
            }
            //Z轴参考点
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_iniSection_AxisZRef, GlobalVar.gl_LinkType.ToString(), "", str_tmp, 50, strPorductTypeINI);
            GlobalVar.gl_dAxisZRef = str_tmp.ToString() == "" ? 0.0 : Convert.ToDouble(str_tmp.ToString());
            m_form_movecontrol.ShowAixsZRef(GlobalVar.gl_dAxisZRef);
        }
        //获取特殊地址
        private void getConfigModel()
        {
            string strPorductTypeINI = GlobalVar.gl_strTargetPath + "\\" + GlobalVar.gl_iniTBS_FileName;
            StringBuilder str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_inikey_specialPath, "", str_tmp, 500, strPorductTypeINI);
            GlobalVar.gl_SpecialPath = str_tmp.ToString();
            if (GlobalVar.gl_SpecialPath.Trim() != "")
            {
                GlobalVar.gl_AutoLoadType = false;
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                GlobalVar.gl_IntPtr_MainWindow = this.Handle;
                m_list_bmpReceived = new List<BitmapInfo>();
                //timer_alarm.Start();
                //timer1.Start();
                tabControl1.SelectedIndex = 1;
                TipPointShow.Width = 1;
                rdb_NetLoading.Checked = true;
                if (!Directory.Exists(GlobalVar.gl_Directory_savePath))
                {
                    Directory.CreateDirectory(GlobalVar.gl_Directory_savePath);
                }
                try
                {
                    string sql = "SELECT distinct SubName FROM [BASEDATA].[dbo].[BasCheckPart]" +
                           " where (ClassName = 'MIC' or ClassName = 'PROX') and SubName <> 'TEST' and SubName <> ''";
                    DataTable dt1 = m_DBQuery.get_database_BaseData(sql);
                    if (dt1 != null)
                    {
                        for (int n = 0; n < dt1.Rows.Count; n++)
                        {
                            string str = dt1.Rows[0 + n]["SubName"].ToString();
                            GlobalVar.listProductType.Add(str);
                        }
                    }
                }
                catch { }
            }
            catch (Exception ex)
            {
                MessageBox.Show("主窗體加載異常 : " + ex.ToString());
            }
            getDeviecId();
            getConfigModel();
        }

        //删除日志
        private void ThdDeleteLog()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(2000);
                    string log = Application.StartupPath + "\\LOG\\";
                    if (!Directory.Exists(log)) continue;
                    MyFunctions.DeleteLogFunc(GlobalVar.gl_NGPicsSavePath, 15);
                    Thread.Sleep(8 * 60 * 60 * 1000); //8小时
                }
                catch { }
            }
        }
        private void ThdDeleteLog1()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(2000);
                    if (!Directory.Exists(GlobalVar.gl_path_FileBackUp)) continue;
                    MyFunctions.DeleteLogFunc(GlobalVar.gl_path_FileBackUp, 15);
                    Thread.Sleep(8 * 60 * 60 * 1000); //8小时
                }
                catch { }
            }
        }
        private void ThdDeleteLog2()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(2000);
                    if (!Directory.Exists(GlobalVar.gl_PicsSavePath)) continue;
                    MyFunctions.DeleteLogFunc(GlobalVar.gl_PicsSavePath, 15);
                    Thread.Sleep(8 * 60 * 60 * 1000); //8小时
                }
                catch { }
            }
        }
        private void ThdDeleteLog3()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(2000);
                    if (!Directory.Exists(GlobalVar.gl_NGPicsSavePath)) continue;
                    MyFunctions.DeleteLogFunc(GlobalVar.gl_NGPicsSavePath, 15);
                    Thread.Sleep(8 * 60 * 60 * 1000); //8小时
                }
                catch { }
            }
        }
        private void StartThdDelLog()
        {
            if (thd_DeleteLog == null)
            {
                thd_DeleteLog = new Thread(ThdDeleteLog);
                thd_DeleteLog.IsBackground = true;
                thd_DeleteLog.Name = "删除日志";
                thd_DeleteLog.Start();
            }
            if (thd_DeleteLog1 == null)
            {
                thd_DeleteLog1 = new Thread(ThdDeleteLog1);
                thd_DeleteLog1.IsBackground = true;
                thd_DeleteLog1.Name = "删除日志1";
                thd_DeleteLog1.Start();
            }
            if (thd_DeleteLog2 == null)
            {
                thd_DeleteLog2 = new Thread(ThdDeleteLog2);
                thd_DeleteLog2.IsBackground = true;
                thd_DeleteLog2.Name = "删除日志1";
                thd_DeleteLog2.Start();
            }
            if (thd_DeleteLog3 == null)
            {
                thd_DeleteLog3 = new Thread(ThdDeleteLog3);
                thd_DeleteLog3.IsBackground = true;
                thd_DeleteLog3.Name = "删除日志1";
                thd_DeleteLog3.Start();
            }
        }
        private void MainForm_Shown(object sender, EventArgs e)
        {
            GlobalVar.gl_strPCName = System.Net.Dns.GetHostName();
            if (!m_initOK) { Application.Exit(); }
            StartThdDelLog();
            toolStripButton_LinkType.Text = "关联作业类型:[" + GlobalVar.gl_LinkType.ToString() + "]";
            initTestInfo();
            m_ratio_Width = tabPage_mainview.Width * 1.00 / GlobalVar.gl_workArea_width;
            m_ratio_Height = tabPage_mainview.Height * 1.00 / GlobalVar.gl_workArea_height;
            //if (updateDeviceListTimer != null) //ltt
            //{
            //    updateDeviceListTimer.Enabled = true;
            //}
            //运动控制初始化
            GlobalVar.gl_Board1245EInit = m_form_movecontrol.OpenBoardAndInit();
            if (!GlobalVar.gl_Board1245EInit)
            {
                MessageBox.Show("运动控制卡初始化失败，请确认！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            m_form_movecontrol.ServerON_Axis_X();
            m_form_movecontrol.ServerON_Axis_Y();
            m_form_movecontrol.AddAxisIntoGroup(0);
            m_form_movecontrol.AddAxisIntoGroup(1);
            m_form_movecontrol.ALLIOInit();
            m_form_movecontrol.Enabled = false;
            updateLedLightStatus(0);
        }

        #region 运动控制卡回调函数
        //到达扫描位置，开始扫描
        void m_form_movecontrol_eve_SheetBarcodeScan(object sender, EventArgs e)
        {
            if ((!GlobalVar.m_ScanAuthorized) || GlobalVar.gl_inEmergence) return;
            if (!GlobalVar.gl_safetyDoor_Front && GlobalVar.gl_usermode == 0) return;
            //清除历史prescan数据
            m_barcodeinfo_preScan.Clear();
            m_count_BoardIn++;
            updateLedLightStatus(1);
            ThreadPool.QueueUserWorkItem(StartScanPanelBarcode);
        }

        //停止复位解除
        void m_form_movecontrol_eve_EmergenceRelease(object sender, EventArgs e)
        {
            updateLedLightStatus(0);
            m_form_movecontrol.Stage1ZaibanPass();
            m_form_movecontrol.Stage2ZaibanPass();
            m_form_movecontrol.AutoHomeSearch_Manual();
            //m_form_movecontrol.LedLight_Red(1);
            //m_form_movecontrol.LedLight_Beep(1);
            BeginInvoke(new Action(() =>
            {
                timer_alarm.Enabled = false;
                button_alarm.BackColor = Color.Gray;
            }));
            clearTags();
            m_manualCycleReset = true;
            Thread.Sleep(800);
            OneCircleReset();

            //m_form_movecontrol.InitDevice();
            //m_obj_dwg.setRefPointValue(GlobalVar.gl_Ref_Point_Axis.Pos_X.ToString("0.000"),
            //    GlobalVar.gl_Ref_Point_Axis.Pos_Y.ToString("0.00"));
        }

        void m_form_movecontrol_eve_EmergeceStop(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                timer_alarm.Enabled = true;
                button_sheetSNInfo.BackColor = Color.DarkGray;
                button_sheetSNInfo.Text = "";
                updateLedLightStatus(2);
            }));
            EmergenceReset();
            if (thread_calibrate != null)
            {
                if (thread_calibrate.IsAlive)
                {
                    thread_calibrate.Abort();
                }
            }
            //PCI1020.PCI1020_Reset(m_form_movecontrol.hDevice);
        }

        //载板到位
        void m_form_movecontrol_eve_BoardArrived(object sender, EventArgs e)
        {
            if ((!GlobalVar.m_ScanAuthorized) || GlobalVar.gl_inEmergence) return;
            if (!GlobalVar.gl_safetyDoor_Front && GlobalVar.gl_usermode == 0) return;
            try
            {
                addLogStr(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ": 第一工位載板到位，開始掃描SHEET條碼！");
                //richTextBox_SingleShow.AppendText(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + ":\t第一工位載板到位，開始掃描SHEET條碼！");            
                AddShowLog(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n第一工位載板到位，開始掃描SHEET條碼！", Color.Green);
                updateLedLightStatus(1);
                m_inScanFunction = true;
                m_barcodeinfo_CurrentUse.Clone(m_barcodeinfo_preScan);
                m_barcodeinfo_preScan.Clear();
                BeginInvoke(new Action(() =>
                {
                    if (m_barcodeinfo_CurrentUse.barcode.Trim().Length == 0)
                    {
                        updateLedLightStatus(2);
                        button_sheetSNInfo.BackColor = Color.DarkRed;
                        button_sheetSNInfo.Text = "SHEET條碼掃描失敗";
                        ShowPsdErrForm sp = new ShowPsdErrForm("SHEET條碼掃描失敗,請重新作業!", false);
                        sp.ShowDialog();
                        updateLedLightStatus(1);
                        m_barcodeinfo_CurrentUse.Clear();
                        m_form_movecontrol.Stage2ZaibanPass();
                        AddShowLog(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\nSHEET條碼掃描失敗,請重新作業!", Color.Red);
                        m_count_BoardIn--;
                        updateLedLightStatus(0);
                        clearTags();
                    }
                    else if (!m_barcodeinfo_CurrentUse.LotResult)
                    {
                        updateLedLightStatus(2);
                        ShowPsdErrForm sp = new ShowPsdErrForm(m_barcodeinfo_CurrentUse.ErrMsg_Lot, true);
                        sp.ShowDialog();
                        m_form_movecontrol.Stage2ZaibanPass();
                        //richTextBox_SingleShow.AppendText(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "\t二段载板退出");
                        AddShowLog(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n二段载板退出", Color.Green);
                        m_count_BoardIn--;
                        updateLedLightStatus(0);
                        clearTags();
                    }
                    else
                    {
                        if (GlobalVar.gl_LinkType == LinkType.BARCODE)// sheet条码检查存储过程 [10/18/2017 617004]
                        {
                            if (!CheckShtBarcode(m_barcodeinfo_CurrentUse.barcode.Trim()))
                            {
                                updateLedLightStatus(2);
                                button_sheetSNInfo.BackColor = Color.DarkRed;
                                button_sheetSNInfo.Text = "SHEET條碼掃描异常";
                                updateLedLightStatus(1);
                                m_barcodeinfo_CurrentUse.Clear();
                                m_form_movecontrol.Stage2ZaibanPass();
                                ShowPsdErrForm sp = new ShowPsdErrForm("SHEET條碼掃描失敗,請重新作業!", false);
                                sp.ShowDialog();
                                AddShowLog(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\nSHEET條碼数据异常,請重新作業!", Color.Red);
                                m_count_BoardIn--;
                                updateLedLightStatus(0);
                                clearTags();
                            }
                            else
                            {
                                button_sheetSNInfo.BackColor = Color.Green;
                                button_sheetSNInfo.Text = m_barcodeinfo_CurrentUse.barcode;
                                startScanFunction();
                            }

                        }
                        else if (GlobalVar.gl_LinkType == LinkType.IC)
                        {
                            if (!CheckShtBarcode_IC(m_barcodeinfo_CurrentUse.barcode.Trim()))
                            {
                                updateLedLightStatus(2);
                                button_sheetSNInfo.BackColor = Color.DarkRed;
                                button_sheetSNInfo.Text = "SHEET條碼掃描失敗";
                                ShowPsdErrForm sp = new ShowPsdErrForm("SHEET條碼无IC测试数据,請重新作業!", false);
                                sp.ShowDialog();
                                updateLedLightStatus(1);
                                m_barcodeinfo_CurrentUse.Clear();
                                m_form_movecontrol.Stage2ZaibanPass();
                                AddShowLog(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\nSHEET條碼无IC测试数据,請重新作業!", Color.Red);
                                m_count_BoardIn--;
                                updateLedLightStatus(0);
                                clearTags();
                            }
                            else
                            {
                                button_sheetSNInfo.BackColor = Color.Green;
                                button_sheetSNInfo.Text = m_barcodeinfo_CurrentUse.barcode;
                                startScanFunction();
                            }
                        }
                        else
                        {
                            button_sheetSNInfo.BackColor = Color.Green;
                            button_sheetSNInfo.Text = m_barcodeinfo_CurrentUse.barcode;
                            ////大载板运动异常增加判断  --20171011 lqz
                            //while (GlobalVar.gl_bUseLargeBoardSize && m_count_BoardIn != 1)
                            //{
                            //    Thread.Sleep(50);//如果使用大板并且机器内载板数不唯一，不发送信号
                            //}
                            startScanFunction();
                        }
                    }
                }));

            }
            catch (Exception ex)
            {
                logWR.appendNewLogMessage("到位开始运动处理异常 m_form_movecontrol_eve_BoardArrived error:  \r\n " + ex.ToString());
            }
        }

        //运动卡信息传递
        void m_form_movecontrol_eve_MotionMsg(string msg)
        {
            button_RunMsg.Invoke(new Action(() =>
            {
                button_RunMsg.Text = msg;
            }));
        }
        #endregion

        /// <summary>
        /// 0: YELLOW ON ,OTHERS OFF        ----- IDLE
        /// 1: GREEN ON ,OTHERS OFF         ----- BUSY
        /// 2: RED ON , BEEP ON, OTHERS OFF ----- ERROR
        /// </summary>
        /// <param name="Red"></param>
        private void updateLedLightStatus(uint type)
        {
            switch (type)
            {
                case 0:
                default:
                    m_form_movecontrol.LedLight_Yellow(1);
                    m_form_movecontrol.LedLight_Green(0);
                    m_form_movecontrol.LedLight_Red(0);
                    m_form_movecontrol.LedLight_Beep(0);
                    break;
                case 1:
                    m_form_movecontrol.LedLight_Yellow(0);
                    m_form_movecontrol.LedLight_Green(1);
                    m_form_movecontrol.LedLight_Red(0);
                    m_form_movecontrol.LedLight_Beep(0);
                    break;
                case 2:
                    m_form_movecontrol.LedLight_Red(1);
                    m_form_movecontrol.LedLight_Beep(1);
                    if (m_count_BoardIn > 0)
                    {
                        m_form_movecontrol.LedLight_Yellow(1);
                        m_form_movecontrol.LedLight_Green(0);
                    }
                    else
                    {
                        m_form_movecontrol.LedLight_Yellow(0);
                        m_form_movecontrol.LedLight_Green(0);
                    }
                    break;
            }
        }

        private void camConnectAutomatic() //ltt
        {
            //try
            //{
            //    if (deviceListView.Items.Count > 0)
            //    {
            //        ConnectWebCam();
            //        //ContinuousShot(); /* Start the grabbing of images until grabbing is stopped. */
            //    }
            //    /* Do not update device list while grabbing to avoid jitter because the GUI-Thread is blocked for a short time when enumerating. */
            //    updateDeviceListTimer.Stop();
            //    updateDeviceListTimer.Enabled = false;
            //    updateDeviceListTimer = null;
            //}
            //catch { }
        }

        private void updatetestinfo()
        {
            try
            {
                textBox_totalsheets.Text = GlobalVar.gl_testinfo_totalSheet.ToString();
                textBox_decodeNG.Text = GlobalVar.gl_testinfo_decodefailed.ToString();
                textBox_totalpcs.Text = GlobalVar.gl_testinfo_totalTest.ToString();
                if (GlobalVar.gl_testinfo_totalTest == 0)
                { textBox_decoderate.Text = "0.0"; }
                else
                {
                    textBox_decoderate.Text = ((GlobalVar.gl_testinfo_totalTest - GlobalVar.gl_testinfo_decodefailed) * 100.00 / GlobalVar.gl_testinfo_totalTest).ToString("0.00");
                }
                SaveTestInfo();
            }
            catch { }
        }

        //DWGDirect 模块加载CAD完毕
        void m_obj_dwg_eve_fileLoaded()
        {
            //try
            //{
            //    GlobalVar.gl_totalCount = 0;
            //    GlobalVar.gl_List_BlockInfo.Clear();
            //    tabPage_mainview.Controls.Clear();
            //    for (int i = 0; i < GlobalVar.gl_List_PointInfo.Count; i++)
            //    {
            //        DetailBlock bi = new DetailBlock();
            //        bi.Pos_X_CAD = Math.Abs(GlobalVar.gl_List_PointInfo[i].Pos_X); 
            //        bi.Pos_Y_CAD =  Math.Abs(GlobalVar.gl_List_PointInfo[i].Pos_Y );
            //        bi.Pos_Z_CAD = Math.Abs(GlobalVar.gl_List_PointInfo[i].Pos_Z ); 
            //        bi.m_PcsNo = GlobalVar.gl_List_PointInfo[i].PointNumber;
            //        bi.m_PcsNo_Mapping = GetMapNum(bi.m_PcsNo);
            //        bi.Location = newPointConvert(bi);
            //        bi.Width = GlobalVar.block_width;
            //        bi.Height = GlobalVar.block_heigt;
            //        bi.setPositionDisplay((Math.Abs(GlobalVar.gl_List_PointInfo[i].Pos_X - GlobalVar.gl_Ref_Point_CADPos.Pos_X).ToString("0.00"))
            //            , (Math.Abs(GlobalVar.gl_List_PointInfo[i].Pos_Y - GlobalVar.gl_Ref_Point_CADPos.Pos_Y)).ToString("0.00"));
            //        bi.Parent = tabPage_mainview;
            //        GlobalVar.gl_List_BlockInfo.Add(bi);
            //    }
            //    GlobalVar.gl_totalCount = GlobalVar.gl_List_BlockInfo.Count;
            //    //init_tabPage_mainview();
            //}
            //catch(Exception e)
            //{
            //    throw new Exception("初始化gl_List_BlockInfo出错" + e.ToString());
            //}
        }

        private int GetMapNum(int orgNum)
        {
            try
            {
                string iniFilePath = "";
                StringBuilder str_tmp = new StringBuilder(50);
                if (!GlobalVar.gl_AutoLoadType || textBox_LotNo.Text == "99999999999")
                    iniFilePath = Application.StartupPath + "\\" + GlobalVar.gl_ProductModel + "\\" + GlobalVar.gl_LinkType.ToString() + "\\" + GlobalVar.gl_ProductModel.ToUpper() + "_MAPPING.INI";
                else
                    iniFilePath = GlobalVar.gl_netPath + GlobalVar.gl_appName + "\\" + GlobalVar.gl_ProductModel + "\\" + GlobalVar.gl_LinkType.ToString() + "\\" + GlobalVar.gl_ProductModel.ToUpper() + "_MAPPING.INI";
                if (!File.Exists(iniFilePath))
                {
                    throw new Exception("找不到映射位置配置檔文件：" + iniFilePath);
                }

                MyFunctions.GetPrivateProfileString(GlobalVar.gl_iniSection_mapping, orgNum.ToString(), "", str_tmp, 50, iniFilePath);
                orgNum = (str_tmp.ToString().Trim() == "" ? orgNum : Convert.ToInt32(str_tmp.ToString()));
            }
            catch
            {
                MessageBox.Show("映射位置配置檔讀取出錯，請檢查配置文檔是否正確", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            return orgNum;
        }

        ////实际宽度与控件宽度的比例，显示制品时扩展
        //float ratio_Width = 1.00F;
        //float ratio_Height = 1.00F;
        private Point newPointConvert(DetailBlock bi, int a)
        {
            Point p = new Point(0, 0);
            p.X = Convert.ToInt32(Math.Abs(GlobalVar.gl_point_ScrrenRefPoint.Pos_X - bi.Pos_X_CAD) * m_ratio_Width);
            p.Y = Convert.ToInt32(Math.Abs(GlobalVar.gl_point_ScrrenRefPoint.Pos_Y - bi.Pos_Y_CAD) * m_ratio_Height);
            #region    针对A51SENSOR block过多，故调整坐标值
            if (a == 0)
                GlobalVar.firstBlockLocation = p.Y;
            if (p.Y > GlobalVar.firstBlockLocation && GlobalVar.firstBlockLocation != 0)
            {
                GlobalVar.firstBlockLocation = 0;
                GlobalVar.firstBlockCount = a - 1;
            }
            if (a > GlobalVar.firstBlockCount && GlobalVar.firstBlockCount != 0)// 修改加载全局视图时BLOCK块之间的Y坐标间距不对情况 [10/31/2017 617004]
                p.Y = /*GlobalVar.firstBlockLocation + */GlobalVar.block_width * (a / (GlobalVar.firstBlockCount + 1)) /*+ 20 * (a / (GlobalVar.firstBlockCount + 1) + 1)*/;
            #endregion
            return p;
        }

        private void init_tabPage_mainview()
        {
            try
            {
                for (int m = 0; m < GlobalVar.gl_List_PointInfo.m_List_PointInfo.Count; m++)
                {
                    List<SPoint> onegroupPoint = GlobalVar.gl_List_PointInfo.m_List_PointInfo[m].m_ListGroup;
                    List<DetailBlock> blocklist = GlobalVar.gl_List_PointInfo.m_List_PointInfo[m].m_BlockList_ByGroup.m_BlockinfoList;

                    for (int i = 0; i < blocklist.Count; i++)
                    {
                        blocklist[i].Parent = tabPage_mainview;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("初始化全局视图错误" + e.ToString());
            }
        }

        /* Closes the image provider when the window is closed. */
        private void MainForm_FormClosing(object sender, FormClosingEventArgs ev)
        {
            baslerCCD1.CCDClosing();
            m_form_movecontrol.Dispose();
            m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_U, 7, 0); //前门释放
            try
            {
                if (m_form_movecontrol.m_DeviceHandle != (IntPtr)(-1))
                {
                    m_form_movecontrol.CloseDevice();
                }
            }
            catch { }
            Application.Exit();
            //System.GC.Collect();

            //Thread.Sleep(50);
            //Application.ExitThread();
            //System.Environment.Exit(0);
        }

        #region 相机功能模块
        //private void initImageProvider()
        //{
        //    try
        //    {
        //        /* Register for the events of the image provider needed for proper operation. */
        //        GlobalVar.gl_imageProvider.GrabErrorEvent += new ImageProvider.GrabErrorEventHandler(OnGrabErrorEventCallback);
        //        GlobalVar.gl_imageProvider.DeviceRemovedEvent += new ImageProvider.DeviceRemovedEventHandler(OnDeviceRemovedEventCallback);
        //        GlobalVar.gl_imageProvider.DeviceOpenedEvent += new ImageProvider.DeviceOpenedEventHandler(OnDeviceOpenedEventCallback);
        //        GlobalVar.gl_imageProvider.DeviceClosedEvent += new ImageProvider.DeviceClosedEventHandler(OnDeviceClosedEventCallback);
        //        GlobalVar.gl_imageProvider.GrabbingStartedEvent += new ImageProvider.GrabbingStartedEventHandler(OnGrabbingStartedEventCallback);
        //        GlobalVar.gl_imageProvider.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(OnImageReadyEventCallback);
        //        GlobalVar.gl_imageProvider.GrabbingStoppedEvent += new ImageProvider.GrabbingStoppedEventHandler(OnGrabbingStoppedEventCallback);

        //        /* Provide the controls in the lower left area with the image provider object. */
        //        sliderGain.MyImageProvider = GlobalVar.gl_imageProvider;
        //        sliderExposureTime.MyImageProvider = GlobalVar.gl_imageProvider;
        //        sliderHeight.MyImageProvider = GlobalVar.gl_imageProvider;
        //        sliderWidth.MyImageProvider = GlobalVar.gl_imageProvider;
        //        comboBoxTestImage.MyImageProvider = GlobalVar.gl_imageProvider;
        //        comboBoxPixelFormat.MyImageProvider = GlobalVar.gl_imageProvider;
        //        comboBoxTriggerActivation.MyImageProvider = GlobalVar.gl_imageProvider;
        //        comboBoxTriggerSource.MyImageProvider = GlobalVar.gl_imageProvider;
        //        comboBoxExposureAuto.MyImageProvider = GlobalVar.gl_imageProvider;
        //        comboBoxTriggerMode.MyImageProvider = GlobalVar.gl_imageProvider;

        //        /* Update the list of available devices in the upper left area. */
        //        UpdateDeviceList();

        //        /* Enable the tool strip buttons according to the state of the image provider. */
        //        EnableButtons(GlobalVar.gl_imageProvider.IsOpen, false);
        //    }
        //    catch (Exception e)
        //    { throw new Exception(e.ToString()); }
        //}

        ///* Handles the click on the single frame button. */
        //private void toolStripButtonOneShot_Click(object sender, EventArgs e)
        //{
        //    OneShot(); /* Starts the grabbing of one image. */
        //}

        ///* Handles the click on the continuous frame button. */
        //private void toolStripButtonContinuousShot_Click(object sender, EventArgs e)
        //{
        //    ContinuousShot(); /* Start the grabbing of images until grabbing is stopped. */
        //}

        ///* Handles the click on the stop frame acquisition button. */
        //private void toolStripButtonStop_Click(object sender, EventArgs e)
        //{
        //    Stop(); /* Stops the grabbing of images. */
        //}

        ///* Handles the event related to the occurrence of an error while grabbing proceeds. */
        //private void OnGrabErrorEventCallback(Exception grabException, string additionalErrorMessage)
        //{
        //    if (InvokeRequired)
        //    {
        //        /* If called from a different thread, we must use the BeginInvoke method to marshal the call to the proper thread. */
        //        BeginInvoke(new ImageProvider.GrabErrorEventHandler(OnGrabErrorEventCallback), grabException, additionalErrorMessage);
        //        return;
        //    }
        //    ShowException(grabException, additionalErrorMessage);
        //}

        ///* Handles the event related to the removal of a currently open device. */
        //private void OnDeviceRemovedEventCallback()
        //{
        //    if (InvokeRequired)
        //    {
        //        /* If called from a different thread, we must use the BeginInvoke method to marshal the call to the proper thread. */
        //        BeginInvoke(new ImageProvider.DeviceRemovedEventHandler(OnDeviceRemovedEventCallback));
        //        return;
        //    }
        //    /* Disable the buttons. */
        //    EnableButtons(false, false);
        //    /* Stops the grabbing of images. */
        //    Stop();
        //    /* Close the image provider. */
        //    CloseTheImageProvider();
        //    /* Since one device is gone, the list needs to be updated. */
        //    UpdateDeviceList();
        //}

        ///* Handles the event related to a device being open. */
        //private void OnDeviceOpenedEventCallback()
        //{
        //    if (InvokeRequired)
        //    {
        //        /* If called from a different thread, we must use the BeginInvoke method to marshal the call to the proper thread. */
        //        BeginInvoke(new ImageProvider.DeviceOpenedEventHandler(OnDeviceOpenedEventCallback));
        //        return;
        //    }
        //    /* The image provider is ready to grab. Enable the grab buttons. */
        //    EnableButtons(true, false);
        //}

        ///* Handles the event related to a device being closed. */
        //private void OnDeviceClosedEventCallback()
        //{
        //    if (InvokeRequired)
        //    {
        //        /* If called from a different thread, we must use the BeginInvoke method to marshal the call to the proper thread. */
        //        BeginInvoke(new ImageProvider.DeviceClosedEventHandler(OnDeviceClosedEventCallback));
        //        return;
        //    }
        //    /* The image provider is closed. Disable all buttons. */
        //    EnableButtons(false, false);
        //}

        ///* Handles the event related to the image provider executing grabbing. */
        //private void OnGrabbingStartedEventCallback()
        //{
        //    if (InvokeRequired)
        //    {
        //        /* If called from a different thread, we must use the BeginInvoke method to marshal the call to the proper thread. */
        //        BeginInvoke(new ImageProvider.GrabbingStartedEventHandler(OnGrabbingStartedEventCallback));
        //        return;
        //    }

        //    ///* Do not update device list while grabbing to avoid jitter because the GUI-Thread is blocked for a short time when enumerating. */
        //    //updateDeviceListTimer.Stop();
        //    //updateDeviceListTimer.Enabled = false;
        //    //updateDeviceListTimer = null;

        //    /* The image provider is grabbing. Disable the grab buttons. Enable the stop button. */
        //    EnableButtons(false, true);
        //}

        ///* 获取照片  Handles the event related to an image having been taken and waiting for processing. */
        //private void OnImageReadyEventCallback()
        //{
        //    if (InvokeRequired)
        //    {
        //        /* If called from a different thread, we must use the BeginInvoke method to marshal the call to the proper thread. */
        //        BeginInvoke(new ImageProvider.ImageReadyEventHandler(OnImageReadyEventCallback));
        //        return;
        //    }
        //    try
        //    {
        //        /* Acquire the image from the image provider. Only show the latest image. The camera may acquire images faster than images can be displayed*/
        //        ImageProvider.Image image = GlobalVar.gl_imageProvider.GetLatestImage();

        //        /* Check if the image has been removed in the meantime. */
        //        if (image != null)
        //        {
        //            if (m_ScanAuthorized && m_tag_CalibrateOK)
        //            {
        //                m_wait_picReceived.Set();
        //            }
        //            /* Check if the image is compatible with the currently used bitmap. */
        //            if (BitmapFactory.IsCompatible(m_bitmap, image.Width, image.Height, image.Color))
        //            {
        //                /* Update the bitmap with the image data. */
        //                BitmapFactory.UpdateBitmap(m_bitmap, image.Buffer, image.Width, image.Height, image.Color);
        //                /* To show the new image, request the display control to update itself. */
        //                pictureBox_capture.Refresh();
        //            }
        //            else /* A new bitmap is required. */
        //            {
        //                BitmapFactory.CreateBitmap(out m_bitmap, image.Width, image.Height, image.Color);
        //                BitmapFactory.UpdateBitmap(m_bitmap, image.Buffer, image.Width, image.Height, image.Color);
        //                /* We have to dispose the bitmap after assigning the new one to the display control. */
        //                Bitmap bitmap = pictureBox_capture.Image as Bitmap;
        //                pictureBox_capture.Image = m_bitmap;
        //                if (bitmap != null)
        //                {
        //                    /* Dispose the bitmap. */
        //                    bitmap.Dispose();
        //                }
        //            }

        //            GlobalVar.gl_imageProvider.ReleaseImage();
        //            m_bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
        //            if (m_bitmap_calibrate_REF == null) { m_bitmap_calibrate_REF = (Bitmap)m_bitmap.Clone(); return; }
        //            if (m_bitmap_calibrate_END == null) { m_bitmap_calibrate_END = (Bitmap)m_bitmap.Clone(); return; }
        //            if (m_ScanAuthorized && m_tag_CalibrateOK)
        //            {
        //                BitmapInfo bi = new BitmapInfo();
        //                bi.FlowID = GlobalVar.gl_CurrentFlowID;
        //                bi.bitmap = (Bitmap)m_bitmap.Clone();
        //                bi.num = m_current_num;
        //                m_list_bmpReceived.Add(bi);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        logWR.appendNewLogMessage("照片采集OnImageReadyEventCallback error : \r\n" + e.ToString());
        //    }
        //}

        //private Bitmap DrawDiagonalLines(Bitmap bmp)
        //{
        //    try
        //    {
        //        Bitmap result = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format32bppArgb);
        //        Graphics g = Graphics.FromImage(result);
        //        //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //        //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //        //g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        //        g.DrawImage(bmp, 0, 0);
        //        Pen p = new Pen(Color.Green, 4);
        //        p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
        //        g.DrawLine(p, new Point(0, bmp.Height / 2), new Point(bmp.Width, bmp.Height / 2));
        //        g.DrawLine(p, new Point(bmp.Width / 2, 0), new Point(bmp.Width / 2, bmp.Height));
        //        g.Save();
        //        return result;
        //    }
        //    catch {
        //        return new Bitmap(640,480);
        //    }
        //}

        ///* Handles the event related to the image provider having stopped grabbing. */
        //private void OnGrabbingStoppedEventCallback()
        //{
        //    if (InvokeRequired)
        //    {
        //        /* If called from a different thread, we must use the BeginInvoke method to marshal the call to the proper thread. */
        //        BeginInvoke(new ImageProvider.GrabbingStoppedEventHandler(OnGrabbingStoppedEventCallback));
        //        return;
        //    }

        //    /* Enable device list update again */
        //    //updateDeviceListTimer.Start();

        //    /* The image provider stopped grabbing. Enable the grab buttons. Disable the stop button. */
        //    EnableButtons(GlobalVar.gl_imageProvider.IsOpen, false);
        //}

        ///* Helps to set the states of all buttons. */
        //private void EnableButtons(bool canGrab, bool canStop)
        //{
        //    toolStripButtonContinuousShot.Enabled = canGrab;
        //    toolStripButtonOneShot.Enabled = canGrab;
        //    toolStripButtonStop.Enabled = canStop;
        //}

        ///* Stops the image provider and handles exceptions. */
        //private void Stop()
        //{
        //    /* Stop the grabbing. */
        //    try
        //    {
        //        GlobalVar.gl_imageProvider.Stop();
        //    }
        //    catch (Exception e)
        //    {
        //        ShowException(e, GlobalVar.gl_imageProvider.GetLastErrorMessage());
        //    }
        //}

        ///* Closes the image provider and handles exceptions. */
        //private void CloseTheImageProvider()
        //{
        //    /* Close the image provider. */
        //    try
        //    {
        //        GlobalVar.gl_imageProvider.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        ShowException(e, GlobalVar.gl_imageProvider.GetLastErrorMessage());
        //    }
        //}

        ///* Starts the grabbing of one image and handles exceptions. */
        //private void OneShot()
        //{
        //    try
        //    {
        //        GlobalVar.gl_imageProvider.OneShot(); /* Starts the grabbing of one image. */
        //    }
        //    catch (Exception e)
        //    {
        //        ShowException(e, GlobalVar.gl_imageProvider.GetLastErrorMessage());
        //    }
        //}

        ///* Starts the grabbing of images until the grabbing is stopped and handles exceptions. */
        //private void ContinuousShot()
        //{
        //    try
        //    {
        //        GlobalVar.gl_imageProvider.ContinuousShot(); /* Start the grabbing of images until grabbing is stopped. */
        //    }
        //    catch (Exception e)
        //    {
        //        ShowException(e, GlobalVar.gl_imageProvider.GetLastErrorMessage());
        //    }
        //}

        ///* Updates the list of available devices in the upper left area. */
        //private void UpdateDeviceList()
        //{
        //    try
        //    {
        //        /* Ask the device enumerator for a list of devices. */
        //        List<DeviceEnumerator.Device> list = DeviceEnumerator.EnumerateDevices();

        //        ListView.ListViewItemCollection items = deviceListView.Items;

        //        /* Add each new device to the list. */
        //        foreach (DeviceEnumerator.Device device in list)
        //        {
        //            bool newitem = true;
        //            /* For each enumerated device check whether it is in the list view. */
        //            foreach (ListViewItem item in items)
        //            {
        //                /* Retrieve the device data from the list view item. */
        //                DeviceEnumerator.Device tag = item.Tag as DeviceEnumerator.Device;

        //                if ( tag.FullName == device.FullName)
        //                {
        //                    /* Update the device index. The index is used for opening the camera. It may change when enumerating devices. */
        //                    tag.Index = device.Index;
        //                    /* No new item needs to be added to the list view */
        //                    newitem = false;
        //                    break; 
        //                }
        //            }

        //            /* If the device is not in the list view yet the add it to the list view. */
        //            if (newitem)
        //            {
        //                ListViewItem item = new ListViewItem(device.Name);
        //                if (device.Tooltip.Length > 0)
        //                {
        //                    item.ToolTipText = device.Tooltip;
        //                }
        //                item.Tag = device;

        //                /* Attach the device data. */
        //                deviceListView.Items.Add(item);
        //                camConnectAutomatic();
        //            }
        //        }

        //        /* Delete old devices which are removed. */
        //        foreach (ListViewItem item in items)
        //        {
        //            bool exists = false;

        //            /* For each device in the list view check whether it has not been found by device enumeration. */
        //            foreach (DeviceEnumerator.Device device in list)
        //            {
        //                if (((DeviceEnumerator.Device)item.Tag).FullName == device.FullName)
        //                {
        //                    exists = true;
        //                    break; 
        //                }
        //            }
        //            /* If the device has not been found by enumeration then remove from the list view. */
        //            if (!exists)
        //            {
        //                deviceListView.Items.Remove(item);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ShowException(e, GlobalVar.gl_imageProvider.GetLastErrorMessage());
        //    }
        //}

        ///* Shows exceptions in a message box. */
        //private void ShowException(Exception e, string additionalErrorMessage)
        //{
        //    string more = "\n\nLast error message (may not belong to the exception):\n" + additionalErrorMessage;
        //    MessageBox.Show("Exception caught:\n" + e.Message + (additionalErrorMessage.Length > 0 ? more : ""), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //}

        ///* Handles the selection of cameras from the list box. The currently open device is closed and the first 
        // selected device is opened. */
        //private void deviceListView_SelectedIndexChanged(object sender, EventArgs ev)
        //{
        //    ConnectWebCam();
        //}

        //private void ConnectWebCam()
        //{
        //    /* Close the currently open image provider. */
        //    /* Stops the grabbing of images. */
        //    Stop();
        //    /* Close the image provider. */
        //    CloseTheImageProvider();

        //    /* Open the selected image provider. */
        //    //if (deviceListView.SelectedItems.Count > 0)
        //    if (deviceListView.Items.Count > 0)
        //    {
        //        /* Get the first selected item. */
        //        //ListViewItem item = deviceListView.SelectedItems[0];
        //        ListViewItem item = deviceListView.Items[0];
        //        /* Get the attached device data. */
        //        DeviceEnumerator.Device device = item.Tag as DeviceEnumerator.Device;
        //        try
        //        {
        //            /* Open the image provider using the index from the device data. */
        //            GlobalVar.gl_imageProvider.Open(device.Index);
        //        }
        //        catch (Exception e)
        //        {
        //            ShowException(e, GlobalVar.gl_imageProvider.GetLastErrorMessage());
        //        }
        //    }
        //}

        ///* If the F5 key has been pressed update the list of devices. */
        //private void deviceListView_KeyDown(object sender, KeyEventArgs ev)
        //{
        //    if (ev.KeyCode == Keys.F5)
        //    {
        //        ev.Handled = true;
        //        /* Update the list of available devices in the upper left area. */
        //        UpdateDeviceList();
        //    }
        //}

        ///* Timer callback used for periodically checking whether displayed devices are still attached to the PC. */
        //private void updateDeviceListTimer_Tick(object sender, EventArgs e)
        //{
        //    UpdateDeviceList(); //fotest
        //}
        #endregion

        /// <summary>
        /// 图片采集满后处理
        /// </summary>
        /// <param name="obj"></param>
        private void ProcessImage(object obj)
        {
            try
            {
                Thread thread = new Thread(new ThreadStart(
                    delegate
                    {
                        for (; ; )
                        {
                            try
                            {
                                if (GlobalVar.gl_inEmergence) { break; }
                                if (!GlobalVar.gl_safetyDoor_Front && GlobalVar.gl_usermode == 0) break;
                                bool finished = false;
                                //多个FLOWID * 一个循环的MIC数量
                                int TotalCircleCount = GlobalVar.gl_List_PointInfo.m_List_PointInfo[0].m_BlockList_ByGroup.m_BlockinfoList.Count
                                    * GlobalVar.gl_List_PointInfo.m_List_PointInfo.Count;
                                if (m_list_bmpReceived.Count == TotalCircleCount)  //直到采集满后再判断
                                { finished = true; }
                                //m_list_bmpReceived中包含了所有Cycle中不同FLOWID过程的照片，比如MIC1/MIC2的照片都在里面
                                for (int m = 0; m < m_list_bmpReceived.Count; m++)
                                {
                                    finished &= m_list_bmpReceived[m].m_processed;
                                    if (m_list_bmpReceived[m].m_processed)
                                    { continue; }
                                    //m_list_bmpReceived[m].m_processed = true; 移动到下面
                                    Bitmap bmp = m_list_bmpReceived[m].bitmap;
                                    for (int n = 0; n < GlobalVar.gl_List_PointInfo.m_List_PointInfo.Count; n++)
                                    {
                                        int flowid = GlobalVar.gl_List_PointInfo.m_List_PointInfo[n].FlowID;
                                        List<DetailBlock> blocklist = GlobalVar.gl_List_PointInfo.m_List_PointInfo[n].m_BlockList_ByGroup.m_BlockinfoList;

                                        for (int i = 0; i < blocklist.Count; i++)
                                        {
                                            if ((blocklist[i].m_PcsNo == m_list_bmpReceived[m].num)
                                                && (blocklist[i].flowid == m_list_bmpReceived[m].FlowID))  //因为有不同的FLOWID，但是PCSNO一样
                                            {
                                                blocklist[i].m_sheetbarcode = m_barcodeinfo_CurrentUse.barcode;
                                                blocklist[i]._bitmap = (Bitmap)(bmp.Clone());
                                                m_list_bmpReceived[m].m_processed = true; //移动到这里
                                            }
                                        }
                                    }
                                }
                                if (finished) break;
                            }
                            catch { }
                            Thread.Sleep(200);
                        }
                    }));
                thread.IsBackground = true;
                thread.Start();
            }
            catch (Exception e)
            { }
        }

        //设置参考原点坐标
        void m_obj_dwg_eve_sendReFPoint(SPoint spoint)
        {
            if (m_form_movecontrol.CheckAxisInMoving())
            {
                MessageBox.Show("设备(轴)为Not Ready状态，请停止后检查再作業！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Thread t = new Thread(new ThreadStart(delegate
                    {
                        GlobalVar.gl_Ref_Point_Axis.Pos_X = spoint.Pos_X;
                        GlobalVar.gl_Ref_Point_Axis.Pos_Y = spoint.Pos_Y;
                        myfunc.WriteRefPositionInfoToTBS();
                        if (m_form_movecontrol.AllAxisBackToMachanicalOrgPoint())
                        {
                            m_form_movecontrol.WaitAllMoveFinished();
                            m_form_movecontrol.MovetoRefPoint();
                        }
                        else
                        {
                            ShowPsdErrForm err = new ShowPsdErrForm("设备回原点操作失败，请重新启动上位机!", false);
                            err.ShowDialog();
                            m_form_movecontrol.CloseDevice();
                            Application.Exit();
                        }
                    }));
                t.IsBackground = true;
                t.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 定点运动並下位機執行拍照
        /// </summary>
        /// <param name="Pos_X"></param>
        /// <param name="Pos_Y"></param>
        /// <param name="multiple"></param>
        /// <param name="OnlyCapture">不需要运动，直接拍照,x,y设置为0即可</param>
        void FixPointMotionAndCapture(float Pos_X, float Pos_Y, int multiple, bool OnlyCapture)
        {
            try
            {
                if (!OnlyCapture)
                {
                    //等待运动完毕
                    m_form_movecontrol.WaitAllMoveFinished();
                    Thread.Sleep(20);
                    Pos_X = Pos_X * -1; //机械原点在左上,X坐标需要取反
                    m_form_movecontrol.FixPointMotion(Pos_X, Pos_Y, multiple);
                    m_form_movecontrol.WaitAllMoveFinished();
                    Thread.Sleep(120);
                }
                baslerCCD1.StartOneShot(); //Modbus通讯，运动完毕拍照
                //m_form_movecontrol.CaptureTrigger(); //拍照
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #region MODBUS通訊
        //发送到位后校正坐标值(不发送Z轴坐标);
        void SendReviseValue(SPoint spoint)
        {
            try
            {
                InputModule module = new InputModule();
                module.byFuntion = 16;
                module.bySlaveID = 1;
                module.nDataLength = 2;
                module.nStartAddr = 4;

                byte[] data = new Byte[4];
                short pos_x = short.Parse((spoint.Pos_X * GlobalVar.gl_PixelDistance).ToString("00000"));
                byte[] array_x = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder(pos_x));
                data[0] = array_x[0];
                data[1] = array_x[1];
                short pos_y = short.Parse((spoint.Pos_Y * GlobalVar.gl_PixelDistance).ToString("00000"));
                byte[] array_y = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder(pos_y));
                data[2] = array_y[0];
                data[3] = array_y[1];
                module.byWriteData = data;
                m_modbus.SendMessage(module);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //回機械原點
        void returnMachicalOrgPoint()
        {
            if (m_inScanFunction) { return; }
            try
            {
                InputModule module1 = new InputModule();
                module1.byFuntion = 5;
                module1.bySlaveID = 1;
                module1.nDataLength = 1;
                module1.nStartAddr = 8;

                byte[] data = new Byte[2];
                data[0] = 0xff;
                data[1] = 0x00;
                module1.byWriteData = data;
                m_modbus.SendMessage(module1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //回參考原點
        void returnreferecePoint()
        {
            if (m_inScanFunction) { return; }
            if (m_form_movecontrol.CheckAxisInMoving())
            {
                MessageBox.Show("设备(轴)为Not Ready状态，请停止后检查再作業！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                //等待运动完毕
                m_form_movecontrol.WaitAllMoveFinished();
                Thread.Sleep(200);
                m_form_movecontrol.FixPointMotion(0, 0, 2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //滑轨工作模式 0：正常工作模式 0x00   1: 滑轨通行方式 0xff
        void SetRailWorkMode(int mode)
        {
            if (m_inScanFunction) { return; }
            try
            {
                InputModule module1 = new InputModule();
                module1.byFuntion = 5;
                module1.bySlaveID = 1;
                module1.nDataLength = 1;
                module1.nStartAddr = 0;

                byte[] data = new Byte[2];
                if (mode == 0)
                {
                    data[0] = 0x00;
                }
                else
                {
                    data[0] = 0xff;
                }
                data[1] = 0x00;
                module1.byWriteData = data;
                m_modbus.SendMessage(module1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //整張條碼掃描結果確認---扫描到条码后，通知下位机
        void SheetBarcodeScanPass()
        {
            if (m_inScanFunction) { return; }
            try
            {
                InputModule module1 = new InputModule();
                module1.byFuntion = 5;
                module1.bySlaveID = 1;
                module1.nDataLength = 1;
                module1.nStartAddr = 6;

                byte[] data = new Byte[2];
                data[0] = 0xff;
                data[1] = 0x00;
                module1.byWriteData = data;
                m_modbus.SendMessage(module1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //开关光源
        void LEDOnOff(bool cmd)
        {
            if (m_inScanFunction) { return; }
            try
            {
                InputModule module1 = new InputModule();
                module1.byFuntion = 5;
                module1.bySlaveID = 1;
                module1.nDataLength = 1;
                module1.nStartAddr = 3;

                byte[] data = new Byte[2];
                if (cmd) { data[0] = 0xff; }
                else { data[0] = 0x00; }
                data[1] = 0x00;
                module1.byWriteData = data;
                m_modbus.SendMessage(module1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        Color forecolor = Color.Red;
        private void AppendRichText(string str)
        {
            if (forecolor == Color.Red) { forecolor = Color.Green; }
            else { forecolor = Color.Red; }
            this.BeginInvoke(new Action(() =>
                {
                    richTextBox_record.AppendText(str);
                    richTextBox_record.ScrollToCaret();

                    try
                    {
                        int p_start = richTextBox_record.TextLength - str.Length;
                        p_start = p_start < 0 ? 0 : p_start;
                        richTextBox_record.Select(p_start, str.Length);
                        richTextBox_record.SelectionColor = forecolor;
                    }
                    catch { }
                }
                ));
        }

        //紧急停止，所有数据清空
        private void EmergenceReset()
        {
            m_barcodeinfo_CurrentUse.Clear();
            m_barcodeinfo_preScan.Clear();
            m_current_num = 0;
            OneCircleReset();
            clearTags();

            m_coilstatus_CCDTrigger = m_coilstatus_EmergenceError = m_coilstatus_FixScan = m_coilstatus_Led
                = m_coilstatus_ShiftToStage2 = m_coilstatus_Stage2Arrived
                 = false;
        }

        private DataTable GetNGPositions(string Barcode)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT TOP 100 [SHEETSN] ,[PCSNO] FROM [BARDATA].[dbo].[AutoPunchDataDetail] where SHEETSN = '" + Barcode + "'" + " and PARTNO = 'BM'";
                dt = m_DBQuery.get_database_BARDATA(sql);
            }
            catch { }
            return dt;
        }

        private bool m_manualCycleReset = false;
        private void cycleCheckAllDecodeFinished()
        {
            try
            {
                Thread threadCheck = new Thread(new ThreadStart(
                    delegate
                    {
                        Thread.Sleep(20);
                        bool finished = false;
                        bool TypeCheck = true;  //制品用料检测
                        try
                        {
                            m_manualCycleReset = false;
                            int totalValidPcs = 0;
                            int totalFailed = 0;
                            while (!finished)
                            {
                                if (GlobalVar.gl_inEmergence) { return; }
                                if (!GlobalVar.gl_safetyDoor_Front && GlobalVar.gl_usermode == 0) return;
                                finished = true;
                                TypeCheck = true;
                                totalValidPcs = 0;
                                totalFailed = 0;
                                Thread.Sleep(100);
                                for (int n = 0; n < GlobalVar.gl_List_PointInfo.m_List_PointInfo.Count; n++)
                                {
                                    //所有block集合
                                    OneGroup_Blocks BlocksGroup = GlobalVar.gl_List_PointInfo.m_List_PointInfo[n].m_BlockList_ByGroup;
                                    List<DetailBlock> blockList = BlocksGroup.m_BlockinfoList;
                                    for (int i = 0; i < blockList.Count; i++)
                                    {
                                        if (GlobalVar.gl_inEmergence) { return; }
                                        if (!GlobalVar.gl_safetyDoor_Front && GlobalVar.gl_usermode == 0) return;
                                        if (blockList[i].m_receivedPics)
                                        {
                                            if (blockList[i].m_receivedPics
                                                && blockList[i].m_GoodPostion)
                                            {
                                                totalValidPcs++;
                                            }
                                            if (GlobalVar.gl_LinkType != LinkType.BARCODE)
                                            {
                                                TypeCheck &= blockList[i].m_TypeCheck;
                                            }
                                            finished &= blockList[i].m_decodeFinished;
                                            if (!blockList[i].m_result
                                                && blockList[i].m_GoodPostion)
                                            { totalFailed++; }
                                        }
                                    }
                                }
                                if (m_manualCycleReset)
                                { finished = true; }
                            }
                            GlobalVar.gl_testinfo_totalSheet++; //ltt
                            GlobalVar.gl_testinfo_totalTest += totalValidPcs;
                            GlobalVar.gl_testinfo_decodefailed += totalFailed;
                            //如果有错误，需要弹出错误，然后再让载版退出
                            if (!TypeCheck)
                            {
                                logWR.appendNewLogMessage("用料錯誤,當前品目對應料與實際用料不一致，請檢查!");
                                updateLedLightStatus(2);
                                ShowPsdErrForm sp = new ShowPsdErrForm("用料錯誤,當前品目對應料與實際用料不一致，請檢查!", true);
                                //ShowPsdErrForm sp = new ShowPsdErrForm("用料錯誤,當前品目對應料為:" + GlobalVar.gl_list_ZhiPinInfo[0]._SubName
                                //    + ", Barcode頭應為:" + GlobalVar.gl_list_ZhiPinInfo[0]._HeadStr + "，請檢查!", true);
                                sp.ShowDialog();
                                updateLedLightStatus(0);
                            }
                            else
                            {
                                int ng_Num = 0;
                                for (int n = 0; n < GlobalVar.gl_List_PointInfo.m_List_PointInfo.Count; n++)
                                {
                                    //所有block集合 
                                    OneGroup_Blocks BlocksGroup = GlobalVar.gl_List_PointInfo.m_List_PointInfo[n].m_BlockList_ByGroup;
                                    int flowid = GlobalVar.gl_List_PointInfo.m_List_PointInfo[n].FlowID;
                                    List<DetailBlock> blockList = BlocksGroup.m_BlockinfoList;
                                    logWR.appendNewLogMessage("所有解析完毕，保存数据");
                                    //所有解析完毕，保存数据
                                    string str_content = "[" + GlobalVar.gl_iniSection_Result + "]\r\n";
                                    //数据库连接信息
                                    str_content += GlobalVar.gl_iniKey_ConnStr;
                                    str_content += "=";
                                    str_content += GlobalVar.gl_DataBaseConnectString;
                                    str_content += "\r\n";
                                    str_content += GlobalVar.gl_iniKey_FlowID;
                                    str_content += "=";
                                    str_content += flowid;
                                    str_content += "\r\n";
                                    //关联信息
                                    for (int i = 0; i < blockList.Count; i++)
                                    {
                                        str_content += blockList[i].m_PcsNo_Mapping.ToString();
                                        str_content += "=";
                                        str_content += blockList[i].m_resultString;
                                        if ((!blockList[i].m_result) && blockList[i].m_GoodPostion) ng_Num++;
                                        str_content += "\r\n";
                                    }
                                    //结果存储为sheetno_flowid.ini
                                    myfunc.SaveResultINIString(m_barcodeinfo_CurrentUse.barcode + "_" + flowid.ToString(), str_content);

                                }
                                logWR.appendNewLogMessage("保存数据完成,sheetbarcode:" + m_barcodeinfo_CurrentUse.barcode);
                                if (ng_Num > 3)
                                {
                                    updateLedLightStatus(2);
                                    ShowPsdErrForm form = new ShowPsdErrForm("解析失败个数过多，重新照合关联！", true);
                                    form.ShowDialog();
                                    updateLedLightStatus(0);
                                    ng_Num = 0;
                                }
                                //GlobalVar.gl_testinfo_totalSheet++; 
                                //GlobalVar.gl_testinfo_totalTest += totalValidPcs;
                                //GlobalVar.gl_testinfo_decodefailed += totalFailed;
                                m_manualCycleReset = false;
                                //BeginInvoke(new Action(() =>
                                //{
                                //    updatetestinfo();
                                //}));
                            }
                            clearTags();
                        }
                        catch (Exception ex)
                        {
                            updateLedLightStatus(2);
                            logWR.appendNewLogMessage("解析异常:" + ex.ToString());
                            ShowPsdErrForm form = new ShowPsdErrForm("解析异常,请重新关联", true);
                            form.ShowDialog();
                            updateLedLightStatus(0);
                        }

                        m_form_movecontrol.Stage2ZaibanPass(); //通知二段载板退出
                        //richTextBox_SingleShow.AppendText(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "\t二段载板退出");
                        AddShowLog(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n二段载板退出", Color.Green);
                        BeginInvoke(new Action(() =>
                        {
                            updatetestinfo();
                        }));
                        m_count_BoardIn--;
                        updateLedLightStatus(0);
                        m_tag_InCheckAllDecodeFinished = false;
                        m_tag_DBQueryFinished = false;
                        m_coilstatus_ShiftToStage2 = false;
                        //GC.Collect();
                    }))
                {
                    IsBackground = true
                };
                threadCheck.Start();
            }
            catch
            { }
        }

        //保存测试信息
        private void SaveTestInfo()
        {
            string iniFilePath = GlobalVar.gl_strTargetPath + "\\" + GlobalVar.gl_iniTBS_FileName;
            MyFunctions.WritePrivateProfileString(GlobalVar.gl_inisection_TestInfo, GlobalVar.gl_iniKey_TotalTest, GlobalVar.gl_testinfo_totalTest.ToString(), iniFilePath);
            MyFunctions.WritePrivateProfileString(GlobalVar.gl_inisection_TestInfo, GlobalVar.gl_iniKey_TotalDecodeFailed, GlobalVar.gl_testinfo_decodefailed.ToString(), iniFilePath);
            MyFunctions.WritePrivateProfileString(GlobalVar.gl_inisection_TestInfo, GlobalVar.gl_iniKey_TotalSheets, GlobalVar.gl_testinfo_totalSheet.ToString(), iniFilePath);
        }
        #endregion

        #region 掃碼串口操作部份
        private void openScanPort()
        {
            try
            {
                if (serialPort_scan.IsOpen)
                {
                    serialPort_scan.Close();
                }
                string[] ports = SerialPort.GetPortNames();
                if (ports.Length == 1)
                    serialPort_scan.PortName = ports[0];
                else
                    serialPort_scan.PortName = GlobalVar.gl_serialPort_Scan;
                serialPort_scan.Open();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("串口打開失敗：" + ex.Message);
            }
        }

        private void StartScanPanelBarcodefortest(object obj)
        {
            byte[] byteArry_startScan = new byte[3];
            byteArry_startScan[0] = 0x16;
            byteArry_startScan[1] = 0x54;
            byteArry_startScan[2] = 0x0D;
            try
            {

                for (int i = 0; i < 100; i++)   //read time 3s
                {
                    serialPort_scan.DiscardInBuffer();
                    serialPort_scan.ReadTimeout = 500;
                    byteArry_startScan[1] = 0x54;
                    serialPort_scan.Write(byteArry_startScan, 0, 3);
                    try
                    {
                        byte[] byteArray = new byte[serialPort_scan.BytesToRead];
                        string totalstr = serialPort_scan.ReadTo("\r\n");
                        if ((totalstr.Length > 4) && (myfunc.checkStringIsLegal(totalstr, 3)))
                        {
                            m_barcodeinfo_preScan.barcode = totalstr;
                            break;
                        }
                    }
                    catch { }
                    byteArry_startScan[1] = 0x55;
                    serialPort_scan.Write(byteArry_startScan, 0, 3);
                }
                if (m_barcodeinfo_preScan.barcode.Trim().Length > 0)
                {
                }
            }
            catch (System.Exception ex)
            { }
        }


        private void StartScanPanelBarcode(object obj)
        {
            byte[] byteArry_startScan = new byte[3];
            byteArry_startScan[0] = 0x16;
            byteArry_startScan[1] = 0x54;
            byteArry_startScan[2] = 0x0D;
            //byteArry_startScan[3] = 0x0A;
            //ThreadPool.QueueUserWorkItem(AsyReceiveData);
            try
            {

                for (int i = 0; i < 3; i++)   //read time 3s
                {
                    serialPort_scan.DiscardInBuffer();
                    serialPort_scan.ReadTimeout = 500;
                    byteArry_startScan[1] = 0x54;
                    serialPort_scan.Write(byteArry_startScan, 0, 3);
                    try
                    {
                        Thread.Sleep(100);
                        byte[] byteArray = new byte[serialPort_scan.BytesToRead];
                        string totalstr = serialPort_scan.ReadTo("\r\n");
                        //string totalstr = serialPort_scan.ReadExisting().Trim('\r').Trim('\n').Trim();
                        //if ((totalstr.Length > 4) && (myfunc.checkStringIsLegal(totalstr, 3))) //ltt
                        if (totalstr.Length > 4)
                        {
                            m_barcodeinfo_preScan.barcode = totalstr;
                            break;
                        }
                    }
                    catch { }
                    byteArry_startScan[1] = 0x55;
                    serialPort_scan.Write(byteArry_startScan, 0, 3);
                }
                if (m_barcodeinfo_preScan.barcode.Trim().Length > 0)
                {
                    QueryNGPositionsFromDB(m_barcodeinfo_preScan.barcode); //一定要查询完毕才能运动，否则会数据不对。
                    QuerySheetNoLotInfo(m_barcodeinfo_preScan.barcode);
                    AutoGeneralSheetFolder(m_barcodeinfo_preScan.barcode.Trim());
                }
            }
            catch (System.Exception ex)
            {
                logWR.appendNewLogMessage("扫描SHEET条码过程异常：   StartScanPanelBarcode   \r\n" + ex.ToString());
            }
            finally
            {
                DateTime dt = DateTime.Now;
                //大载板运动异常增加判断  --20171011 lqz
                while (GlobalVar.gl_bUseLargeBoardSize && m_count_BoardIn != 1)
                {
                    Thread.Sleep(50);//如果使用大板并且机器内载板数不唯一，不发送信号
                    DateTime rt = DateTime.Now;
                    TimeSpan sp = rt - dt;
                    if (sp.Seconds > 20) break;
                }

                m_form_movecontrol.Stage1ZaibanPass();
                //richTextBox_SingleShow.AppendText(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "\t一段载板退出");
                AddShowLog(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n一段载板退出", Color.Green);
            }
        }

        #region OLD方式，废弃不用
        //private void StartScanPanelBarcode(object obj)
        //{
        //    try
        //    {
        //        byte[] byteArry_startScan = new byte[3];
        //        byteArry_startScan[0] = 0x16;
        //        byteArry_startScan[1] = 0x54;
        //        byteArry_startScan[2] = 0x0D;
        //        //byteArry_startScan[3] = 0x0A;
        //        //ThreadPool.QueueUserWorkItem(AsyReceiveData);

        //        for (int i = 0; i < 1; i++)   //read time 3s
        //        {
        //            byteArry_startScan[1] = 0x54;
        //            serialPort_scan.Write(byteArry_startScan, 0, 3);
        //            Thread.Sleep(1000);
        //            try
        //            {
        //                byte[] byteArray = new byte[serialPort_scan.BytesToRead];
        //                serialPort_scan.Read(byteArray, 0, serialPort_scan.BytesToRead);
        //                string totalstr = System.Text.Encoding.Default.GetString(byteArray);
        //                if (totalstr.IndexOf("\r\n") > 0)
        //                {
        //                    totalstr = totalstr.Substring(0, totalstr.LastIndexOf("\r\n"));  //删除无效结尾
        //                    if (totalstr.IndexOf("\r\n") > 0)
        //                    {
        //                        totalstr = totalstr.Substring(totalstr.LastIndexOf("\r\n") + 2);  //删除无效结尾
        //                    }
        //                }
        //                //if ((totalstr.Length == GlobalVar.gl_length_sheetBarcodeLength) && (myfunc.checkStringIsLegal(totalstr, 3)))
        //                if (myfunc.checkStringIsLegal(totalstr, 3))
        //                {
        //                    m_barcodeinfo_preScan.barcode = totalstr;
        //                    break;
        //                }
        //            }
        //            catch { }
        //            byteArry_startScan[1] = 0x55;
        //            serialPort_scan.Write(byteArry_startScan, 0, 3);
        //        }
        //        if (m_barcodeinfo_preScan.barcode.Trim().Length > 0)
        //        {
        //            QueryNGPositionsFromDB(m_barcodeinfo_preScan.barcode); //一定要查询完毕才能运动，否则会数据不对。
        //            QuerySheetNoLotInfo(m_barcodeinfo_preScan.barcode);
        //            AutoGeneralSheetFolder(m_barcodeinfo_preScan.barcode.Trim());
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        logWR.appendNewLogMessage("扫描SHEET条码过程异常：   StartScanPanelBarcode   \r\n" + ex.ToString());
        //    }
        //    finally
        //    {
        //        m_form_movecontrol.Stage1ZaibanPass();
        //    }
        //}
        #endregion
        private delegate void InvokeCallback(string msg, Color color);
        //增加界面显示流程
        private void AddShowLog(string msg, Color color)
        {
            this.Invoke(new Action(() =>
            {
                richTextBox_SingleShow.AppendText("\n");
                richTextBox_SingleShow.SelectionColor = color;
                richTextBox_SingleShow.AppendText(msg);
            }));
        }

        private void AutoGeneralSheetFolder(string sheetbarcode)
        {
            string saveDic = GlobalVar.gl_PicsSavePath + "\\" + sheetbarcode;
            if (!Directory.Exists(saveDic))
            {
                Directory.CreateDirectory(saveDic);
            }
            else
            {
                System.IO.Directory.Move(saveDic, saveDic + "_" + DateTime.Now.ToString("MMddHHmmssffff"));
            }
        }

        private void QueryNGPositionsFromDB(string barcode)
        {
            try
            {
                DataTable m_datatable_NGPositions = new DataTable();
                if (GlobalVar.gl_LinkType != LinkType.BARCODE)
                    m_datatable_NGPositions = GetNGPositions(m_barcodeinfo_preScan.barcode);
                //m_datatable_NGPositions = GetNGPositions("G2238706ZY");

                if (m_datatable_NGPositions.Rows.Count > 0)
                {
                    for (int i = 0; i < m_datatable_NGPositions.Rows.Count; i++)
                    {
                        try
                        {
                            m_barcodeinfo_preScan.NGPositionlist.Add(Convert.ToInt32(m_datatable_NGPositions.Rows[i]["PCSNO"].ToString()));
                        }
                        catch { }
                    }
                }
            }
            catch (Exception e)
            {
                logWR.appendNewLogMessage("数据库查询Sheet条码NG位置异常 QueryNGPositionsFromDB Err:  \r\n" + e.ToString());
            }
        }

        private void QuerySheetNoLotInfo(string barcode) //判断条码与LOT号是否匹配，如果不匹配，退出
        {
            string sql = "SELECT TOP 1 LOTNO FROM [BARDATA].[dbo].[AutoPunchData] where SHEETSN = '" + barcode + "'";
            DataTable dt = m_DBQuery.get_database_BARDATA(sql);
            string lotno_db = "";
            try
            {
                if ((dt != null) && (dt.Rows.Count > 0))
                {
                    lotno_db = dt.Rows[0]["LOTNO"].ToString();
                    if (lotno_db != GlobalVar.gl_str_LotNo)
                    {
                        m_barcodeinfo_preScan.LotResult = false;
                        m_barcodeinfo_preScan.ErrMsg_Lot = "SHEET條碼LOTNO不符";
                    }
                }
            }
            catch { }
        }

        ////判断条码与LOT号是否匹配，如果不匹配，退出
        //private void GetLotNoBySheetSN(string SheetSN)
        //{
        //    string lotno = "";
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        string sql = "SELECT TOP 1 LotNo FROM [BARDATA].[dbo].[AutoPunchData] where SHEETSN = '" + SheetSN + "'";
        //        dt = m_DBQuery.get_database_cmd(sql);

        //        if (dt.Rows.Count > 0)
        //        {
        //            lotno = dt.Rows[0]["LotNo"].ToString();
        //        }
        //    }
        //    catch { }
        //    m_barcodeinfo_preScan.LotNo = lotno;
        //}


        public void manualBarCodeScan()
        {
            if (!serialPort_scan.IsOpen)
            {
                MessageBox.Show("扫面串口未打开");
                return;
            }
            byte[] byteArry_startScan = new byte[3];
            byteArry_startScan[0] = 0x16;
            byteArry_startScan[1] = 0x54;
            byteArry_startScan[2] = 0x0D;
            serialPort_scan.Write(byteArry_startScan, 0, 3);
        }

        //旧有扫描接收方式，废弃不用
        private void AsyReceiveData2(object serialPortobj)
        {
            Thread.Sleep(100);
            for (int m = 0; m < 3; m++)
            {
                try
                {
                    string str = serialPort_scan.ReadTo("\r\n");
                    if (!myfunc.checkStringIsLegal(str, 3)) { continue; }
                    if (str.Length > 5)
                    {
                        try
                        {
                            this.BeginInvoke(new Action(() =>
                            {
                                try
                                {
                                    m_barcodeinfo_preScan.barcode = str;
                                    button_sheetSNInfo.Text = str;
                                    button_sheetSNInfo.BackColor = Color.Green;
                                }
                                catch { }
                            }
                            ));
                            //if (!m_coilstatus_workmode) { return; }
                            if (m_barcodeinfo_preScan.barcode.Trim().Length > 0)
                            {
                                SheetBarcodeScanPass();
                                //m_datatable_NGPositions = GetNGPositions(m_barcode_preScan);
                                //m_datatable_NGPositions = GetNGPositions("G2238706ZY");

                                ////---修改成独立窗体查询
                                //if (m_datatable_NGPositions != null)
                                //{
                                //    if (m_datatable_NGPositions.Rows.Count > 0)
                                //    {
                                //        for (int i = 0; i < m_datatable_NGPositions.Rows.Count; i++)
                                //        {
                                //            try
                                //            {
                                //                m_listNGPosition_preScan.Add(Convert.ToInt32(m_datatable_NGPositions.Rows[i]["PCSNO"].ToString()));
                                //            }
                                //            catch { }
                                //        }
                                //    }
                                //}
                            }
                        }
                        catch { }
                        m_tag_DBQueryFinished = true;
                        break;
                    }
                }
                catch { }
            }
        }
        #endregion

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            if (m_bitmap != null)
            {
                m_bitmap.Save("d:\\capture.bmp");
            }
        }

        private void toolStripButton_LoadCADFile_Click(object sender, EventArgs e)
        {
            m_obj_dwg.OpenFile();
        }

        private void toolStripButton_Exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確定關閉軟件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
                == System.Windows.Forms.DialogResult.Yes)
            {
                // 删除原有的程序 [11/10/2017 617004]
                //                 if (Application.StartupPath!=GlobalVar.gl_strAppPath)
                //                 {
                //                     Directory.Delete(Application.StartupPath);
                //                 }
                Application.Exit();
            }
        }

        private void toolStripButton_sendTPsMessage_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(StartScanPanelBarcodefortest);
        }

        private void button_workpermitted_Click(object sender, EventArgs e)
        {
            m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_U, 7, 1); //前门上锁
            Thread.Sleep(100);
            if (!GlobalVar.gl_safetyDoor_Front && GlobalVar.gl_usermode == 0)
            {
                MessageBox.Show("安全门未锁，請检查！");
                return;
            }
            if (!Directory.Exists(GlobalVar.gl_PicsSavePath))
            {
                try
                {
                    Directory.CreateDirectory(GlobalVar.gl_PicsSavePath);
                }
                catch
                {
                    MessageBox.Show("圖片保存路徑" + GlobalVar.gl_PicsSavePath + "不存在，請重新制定圖片保存路徑！");
                }
                //MessageBox.Show("圖片保存路徑" + GlobalVar.gl_PicsSavePath + "不存在，請重新制定圖片保存路徑！");
                return;
            }
            if (GlobalVar.gl_str_Product.Trim() == "")
            {
                MessageBox.Show("LOTNO為空，請輸入LOTNO并獲取品目信息！");
                return;
            }
            if (GlobalVar.gl_List_PointInfo.m_List_PointInfo[0].m_ListGroup.Count <= 0)
            {
                MessageBox.Show("未導入品目CAD文檔，請確認！");
                return;
            }
            //if (GlobalVar.gl_str_MICHeadStr.Trim() == "")
            //{
            //    MessageBox.Show("沒查詢到當前LOT對應的MIC芯片信息，請確認！");
            //    return;
            //}
            if (m_DBQuery == null)
            {
                MessageBox.Show("請先進行品目配置!");
                return;
            }
            WorkPermitted(true);
            baslerCCD1.StopCCD();
            startUpload();//开始上传文件
        }

        private void WorkPermitted(bool enable)
        {
            GlobalVar.m_ScanAuthorized = enable;
            button_workProhabit.Enabled = enable;
            button_workpermitted.Enabled = !enable;
            if (!enable)
            {
                button_status.BackColor = Color.Gray;
                button_status.Text = "待機中";
            }
            else
            {
                button_status.BackColor = Color.BlueViolet;
                button_status.Text = "作業中";
            }
        }
        private void button_workProhabit_Click(object sender, EventArgs e)
        {
            m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_U, 7, 0); //前门释放
            WorkPermitted(false);
        }


        DateTime timetest;
        private void startScanFunction()
        {
            try
            {
                timetest = DateTime.Now;
                OneCircleReset();
                checkBlocksIsValid();  //--移动到独立窗口进行查询
                CalibrateAction();
            }
            catch (Exception e)
            {
                logWR.appendNewLogMessage("启动扫描进程异常 startScanFunction Error:  \r\n " + e.ToString());
            }
        }

        //检查各个Block是否需要进行解析，如果打孔机位置有对应位置，则不解析
        private void checkBlocksIsValid()
        {
            for (int n = 0; n < GlobalVar.gl_List_PointInfo.m_List_PointInfo.Count; n++)
            {
                OneGroup_Blocks onegroupBlock = GlobalVar.gl_List_PointInfo.m_List_PointInfo[n].m_BlockList_ByGroup;
                List<DetailBlock> blocklist = onegroupBlock.m_BlockinfoList;
                try
                {
                    for (int i = 0; i < blocklist.Count; i++)
                    {
                        if (m_barcodeinfo_CurrentUse.NGPositionlist.Contains(blocklist[i].m_PcsNo_Mapping))
                        {
                            blocklist[i].m_GoodPostion = false;
                        }
                    }
                }
                catch { }
            }
            m_barcodeinfo_CurrentUse.NGPositionlist.Clear();
        }

        private void runCommand(object obj)
        {
            try
            {
                Thread.Sleep(80);
                m_form_movecontrol.WaitAllMoveFinished();
                Thread.Sleep(80);
                m_form_movecontrol_eve_MotionMsg("条码拍照关联作业中");
                ThreadPool.QueueUserWorkItem(ProcessImage);
                //float pos_x,pos_y;

                //Thread thread = new Thread(CycleDecodeAllBlocks);
                //thread.IsBackground = true;
                //thread.Start();
                for (int n = 0; n < GlobalVar.gl_List_PointInfo.m_List_PointInfo.Count; n++)
                {
                    if (!GlobalVar.gl_safetyDoor_Front && GlobalVar.gl_usermode == 0) break;
                    //每组开始清空m_current_num
                    m_current_num = 0;
                    //每组扫描都需要回原点
                    m_form_movecontrol.FixPointMotion(0, 0, 3);
                    //等待运动完毕 
                    m_form_movecontrol.WaitAllMoveFinished();
                    //切换光源
                    SetLedLightAndExposure(GlobalVar.gl_List_PointInfo.m_List_PointInfo[n].m_list_zhipingInfo[0]._SubName);
                    //sliderExposureTime.valueChanged(GlobalVar.gl_paras_basler_Exposure_Scan); //ltt
                    baslerCCD1.SetExposureValue(GlobalVar.gl_paras_basler_Exposure_Scan);
                    //启动后台解析线程
                    GlobalVar.gl_List_PointInfo.m_List_PointInfo[n].m_BlockList_ByGroup.CycleDecodeAllBlocks();
                    //开始扫描
                    //richTextBox_SingleShow.AppendText(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "\t开始扫描拍照");
                    AddShowLog(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n开始扫描拍照", Color.Green);
                    OnePointGroup onegroup = GlobalVar.gl_List_PointInfo.m_List_PointInfo[n];
                    List<SPoint> pointlist = onegroup.m_ListGroup;
                    GlobalVar.gl_CurrentFlowID = GlobalVar.gl_List_PointInfo.m_List_PointInfo[n].FlowID;
                    for (int i = 0; i < pointlist.Count; i++)
                    {
                        if (!GlobalVar.gl_safetyDoor_Front && GlobalVar.gl_usermode == 0) break;
                        //if (GlobalVar.gl_inEmergence) { return; }
                        float dis_X, dis_Y;
                        if (i == 0)
                        {
                            dis_X = (GlobalVar.gl_Ref_Point_CADPos.Pos_X - pointlist[i].Pos_X) * -1; //机械原点在左上,X坐标需要取反
                            dis_Y = GlobalVar.gl_Ref_Point_CADPos.Pos_Y - pointlist[i].Pos_Y;

                            //参考原点水平位置偏移只在第一次计算
                            dis_X = dis_X + GlobalVar.gl_value_CalibrateDis_X;
                            dis_Y = dis_Y + GlobalVar.gl_value_CalibrateDis_Y;
                        }
                        else
                        {
                            dis_X = (pointlist[i - 1].Pos_X - pointlist[i].Pos_X) * -1; //机械原点在左上,X坐标需要取反
                            dis_Y = pointlist[i - 1].Pos_Y - pointlist[i].Pos_Y;
                        }
                        //斜率导致的位置偏移需要每次计算
                        dis_X = dis_X + dis_Y * GlobalVar.gl_value_CalibrateRatio_X;
                        dis_Y = dis_Y + dis_X * GlobalVar.gl_value_CalibrateRatio_Y;

                        m_form_movecontrol.SetPoxEnd_X(dis_X, true);
                        m_form_movecontrol.SetPoxEnd_Y(dis_Y, true);
                        m_form_movecontrol.AxisGroup_Move(true);

                        //等待运动完毕
                        m_form_movecontrol.WaitAllMoveFinished();
                        Thread.Sleep(180);  //进光量少，拍照进光时间需要长，则等待时间长
                        m_current_num++; //
                        baslerCCD1.StartOneShot(); //运动完毕拍照 fortest
                        //m_form_movecontrol.CaptureTrigger(); //拍照
                        //Thread.Sleep(100);
                        m_wait_picReceived.WaitOne(2000); //如果2秒钟没有收到信号，自动下一张
                    }
                    if (!GlobalVar.gl_safetyDoor_Front && GlobalVar.gl_usermode == 0) break;
                    //等待运动完毕 
                    m_form_movecontrol.WaitAllMoveFinished();
                }
                if (!GlobalVar.gl_safetyDoor_Front && GlobalVar.gl_usermode == 0)
                {
                    m_tag_InCheckAllDecodeFinished = true;
                    m_form_movecontrol_eve_MotionMsg("安全门未关，停止作业");
                    return;
                }
                //等待运动完毕 
                m_form_movecontrol.WaitAllMoveFinished();
                Thread.Sleep(80);
                //设置为较慢回归速度
                m_form_movecontrol.SetProp_GPSpeed(m_form_movecontrol.m_GPValue_VelHigh_low, m_form_movecontrol.m_GPValue_VelLow_low,
                    m_form_movecontrol.m_GPValue_Acc_low, m_form_movecontrol.m_GPValue_Dec_low);
                m_form_movecontrol.FixPointMotion(0, 0, 3);  //扫描完毕回原点
                m_tag_InCheckAllDecodeFinished = true;
                //等待全部解析完毕
                //richTextBox_SingleShow.AppendText(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "\t开始解析全部图像");
                AddShowLog(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n开始解析全部图像", Color.Green);
                cycleCheckAllDecodeFinished();
                //richTextBox_SingleShow.AppendText(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "\t全部图像解析完毕");
                AddShowLog(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n全部图像解析完毕", Color.Green);
                BeginInvoke(new Action(() =>
                {
                    try
                    {
                        if (tabControl1.TabPages.Count >= 3)
                        {
                            tabControl1.SelectedIndex = 2;
                            //GC.Collect();
                        }
                    }
                    catch { }
                }));
                Thread.Sleep(80);
                m_form_movecontrol_eve_MotionMsg("条码拍照关联作业完毕，等待下一张作业");
            }
            catch (Exception ex)
            {
                logWR.appendNewLogMessage("整盘拍照运动控制出错 runCommand error : \r\n" + ex.ToString());
            }
        }

        private void runCommand_withoutCapture(object obj)
        {
            try
            {
                Thread.Sleep(80);
                m_form_movecontrol.WaitAllMoveFinished();
                Thread.Sleep(80);

                for (int n = 0; n < GlobalVar.gl_List_PointInfo.m_List_PointInfo.Count; n++)
                {
                    //每组开始清空m_current_num
                    m_current_num = 0;
                    //每组扫描都需要回原点
                    m_form_movecontrol.FixPointMotion(0, 0, 3);
                    //切换光源
                    SetLedLightAndExposure(GlobalVar.gl_List_PointInfo.m_List_PointInfo[n].m_list_zhipingInfo[0]._SubName);
                    //开始扫描
                    OnePointGroup onegroup = GlobalVar.gl_List_PointInfo.m_List_PointInfo[n];
                    List<SPoint> pointlist = onegroup.m_ListGroup;
                    for (int i = 0; i < pointlist.Count; i++)
                    {
                        float dis_X, dis_Y;
                        if (i == 0)
                        {
                            dis_X = GlobalVar.gl_Ref_Point_CADPos.Pos_X - pointlist[i].Pos_X;
                            dis_Y = GlobalVar.gl_Ref_Point_CADPos.Pos_Y - pointlist[i].Pos_Y;

                            //参考原点水平位置偏移只在第一次计算
                            dis_X = dis_X + GlobalVar.gl_value_CalibrateDis_X;
                            dis_Y = dis_Y + GlobalVar.gl_value_CalibrateDis_Y;
                        }
                        else
                        {
                            dis_X = pointlist[i - 1].Pos_X - pointlist[i].Pos_X;
                            dis_Y = pointlist[i - 1].Pos_Y - pointlist[i].Pos_Y;
                        }
                        //斜率导致的位置偏移需要每次计算
                        dis_X = dis_X + dis_Y * GlobalVar.gl_value_CalibrateRatio_X;
                        dis_Y = dis_Y + dis_X * GlobalVar.gl_value_CalibrateRatio_Y;

                        m_form_movecontrol.SetPoxEnd_X(dis_X, true);
                        m_form_movecontrol.SetPoxEnd_Y(dis_Y, true);
                        m_form_movecontrol.AxisGroup_Move(true);

                        //等待运动完毕
                        m_form_movecontrol.WaitAllMoveFinished();
                        Thread.Sleep(40);  //进光量少，拍照进光时间需要长，则等待时间长
                        m_current_num++; //
                        Thread.Sleep(100);
                    }
                    //等待运动完毕 
                    m_form_movecontrol.WaitAllMoveFinished();
                }
                //等待运动完毕 
                m_form_movecontrol.WaitAllMoveFinished();
                Thread.Sleep(80);
                //设置为较慢回归速度
                m_form_movecontrol.SetProp_GPSpeed(m_form_movecontrol.m_GPValue_VelHigh_low, m_form_movecontrol.m_GPValue_VelLow_low,
                    m_form_movecontrol.m_GPValue_Acc_low, m_form_movecontrol.m_GPValue_Dec_low);
                m_form_movecontrol.FixPointMotion(0, 0, 3);  //扫描完毕回原点
            }
            catch (Exception ex)
            {
                logWR.appendNewLogMessage("整盘拍照运动控制出错 runCommand error : \r\n" + ex.ToString());
            }
        }

        protected override void WndProc(ref Message m)
        {
            try
            {
                switch (m.Msg)
                {
                    case GlobalVar.WM_FixedMotion:  //定点运动
                        try
                        {
                            if (m_form_movecontrol.CheckAxisInMoving())
                            {
                                MessageBox.Show("设备(轴)为Not Ready状态，请停止后检查再作業！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            string pos_x = Marshal.PtrToStringAnsi(m.WParam);
                            string pos_y = Marshal.PtrToStringAnsi(m.LParam);
                            float dis_X = (GlobalVar.gl_Ref_Point_CADPos.Pos_X - float.Parse(pos_x)) * -1; //机械原点在左上,X坐标需要取反
                            float dis_Y = GlobalVar.gl_Ref_Point_CADPos.Pos_Y - float.Parse(pos_y);
                            float x = dis_X + GlobalVar.gl_value_CalibrateDis_X + dis_Y * GlobalVar.gl_value_CalibrateRatio_X;
                            float y = dis_Y + GlobalVar.gl_value_CalibrateDis_Y + dis_X * GlobalVar.gl_value_CalibrateRatio_Y;
                            m_form_movecontrol.FixPointMotion(x, y, 3);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.ToString());
                        }
                        break;
                }
            }
            catch { }
            base.WndProc(ref m);
        }

        void m_obj_dwg_eve_sendFixMotion(float x, float y)
        {
            if (m_form_movecontrol.CheckAxisInMoving())
            {
                MessageBox.Show("设备(轴)为Not Ready状态，请停止后检查再作業！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if ((x >= GlobalVar.gl_workArea_width)
                || (y >= GlobalVar.gl_workArea_height))
            {
                MessageBox.Show("運動目標範圍超出工作區，請確認!");
                return;
            }
            m_form_movecontrol.WaitAllMoveFinished();
            Thread.Sleep(200);
            m_form_movecontrol.FixPointMotion(x, y, 3);
        }

        void m_obj_dwg_eve_sendCalPosition(float x, float y)
        {
            if (m_inScanFunction)
            {
                MessageBox.Show("掃描作業中，不允許進行校準動作!");
            }
            if (m_form_movecontrol.CheckAxisInMoving())
            {
                MessageBox.Show("设备(轴)为Not Ready状态，请停止后检查再作業！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Thread thread_cal = new Thread(new ThreadStart(delegate
            {
                PositionInfo pos_Mark_Start, pos_Mark_End;
                try
                {
                    //拍照MARK点需要点亮，全部亮，exposure值为参数设定中的默认值
                    SetLedLightAndExposure("MARK");
                    //sliderExposureTime.valueChanged(GlobalVar.gl_paras_basler_Exposure_Calibrate); //ltt
                    baslerCCD1.SetExposureValue(GlobalVar.gl_exposure_Mark_default);
                    //设置为较快速度
                    m_form_movecontrol.SetProp_GPSpeed(m_form_movecontrol.m_GPValue_VelHigh_move, m_form_movecontrol.m_GPValue_VelLow_move,
                        m_form_movecontrol.m_GPValue_Acc_move, m_form_movecontrol.m_GPValue_Dec_move);
                    FixPointMotionAndCapture(0.0F, 0.0F, 2, true);  //回參考點並拍照
                    m_bitmap_calibrate_REF = null;
                    DateTime time_start = DateTime.Now;
                    MatrixDecode decoder = new MatrixDecode();
                    for (; ; )
                    {
                        Thread.Sleep(30);
                        TimeSpan ts = DateTime.Now.Subtract(time_start);
                        if (ts.TotalMilliseconds > 3000) { throw new Exception("Y軸校準超時!"); }
                        if (m_bitmap_calibrate_REF != null) break;
                    }
                    pos_Mark_Start = decoder.ShapeMatch(m_bitmap_calibrate_REF)[0];

                    ////TO MARK點
                    m_bitmap_calibrate_END = null;
                    FixPointMotionAndCapture(x, y, 3, false);
                    time_start = DateTime.Now;
                    for (; ; )
                    {
                        TimeSpan ts = DateTime.Now.Subtract(time_start);
                        if (ts.TotalMilliseconds > 3000) { throw new Exception("Y軸校準超時!"); }
                        if (m_bitmap_calibrate_END != null) break;
                        Thread.Sleep(30);
                    }
                    pos_Mark_End = decoder.ShapeMatch(m_bitmap_calibrate_END)[0];

                    float ratio_X = GlobalVar.gl_value_MarkPointDiameter * 1.0f / pos_Mark_Start.MCHPatterWidth;
                    float ratio_Y = GlobalVar.gl_value_MarkPointDiameter * 1.0f / pos_Mark_Start.MCHPatterHeight;
                    GlobalVar.gl_value_CalibrateRatio_X = -1.0f * (pos_Mark_End.CenterX - pos_Mark_Start.CenterX) * ratio_X * 1.00F
                        / (GlobalVar.gl_point_CalPos.Pos_Y + (pos_Mark_End.CenterY - pos_Mark_Start.CenterY) * ratio_Y);

                    //GlobalVar.gl_point_CalPos.Pos_X = x; //ltt
                    //GlobalVar.gl_point_CalPos.Pos_Y = y;
                    myfunc.WriteCalPositionInfoToTBS();
                }
                catch { }
            }));
            thread_cal.IsBackground = true;
            thread_cal.Start();
        }

        private void toolStripButton_manualCapture_Click(object sender, EventArgs e)
        {
            //baslerCCD1.StartOneShot(); //手动拍照
            //m_form_movecontrol.CaptureTrigger(); //拍照
        }

        #region  位置校準
        Thread thread_calibrate;   //开始照合校准线程，如果监测到紧急停止，需要对此线程进行dispose
        MatrixDecode m_cal_decoder = new MatrixDecode();
        Bitmap m_bitmap_calibrate_REF = null;      //第一次拍MARK点图片
        Bitmap m_bitmap_calibrate_END = null;      //第拍MARK点图片   -----目前放弃校准第二个MARK点，为节省时间
        private void CalibrateAction()
        {
            //richTextBox_SingleShow.AppendText(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "\t开始校准MARK点");
            AddShowLog(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n开始校准MARK点", Color.Green);
            //sliderExposureTime.valueChanged(GlobalVar.gl_paras_basler_Exposure_Calibrate); //ltt
            baslerCCD1.SetExposureValue(GlobalVar.gl_paras_basler_Exposure_Calibrate);
            m_times_duplicateCalibrate++;
            PositionInfo pos_Mark_Start = new PositionInfo();
            PositionInfo pos_Mark_End = new PositionInfo();
            m_list_bmpReceived.Clear();
            thread_calibrate = new Thread(new ThreadStart(delegate
            {
                bool Mark_I_result = true;   //第一个点是否匹配OK
                bool Mark_II_result = true;  //第二个点是否匹配OK
                DateTime time_start = DateTime.Now;
                try
                {
                    //拍照MARK点需要点亮，全部亮，exposure值为参数设定中的默认值
                    SetLedLightAndExposure("MARK");
                    //sliderExposureTime.valueChanged(GlobalVar.gl_paras_basler_Exposure_Calibrate); //ltt
                    baslerCCD1.SetExposureValue(GlobalVar.gl_exposure_Mark_default);

                    m_form_movecontrol_eve_MotionMsg("MARK点查找进行中");
                    //if (GlobalVar.gl_inEmergence) { return; }
                    //设置为较快速度
                    m_form_movecontrol.SetProp_GPSpeed(m_form_movecontrol.m_GPValue_VelHigh_move, m_form_movecontrol.m_GPValue_VelLow_move,
                        m_form_movecontrol.m_GPValue_Acc_move, m_form_movecontrol.m_GPValue_Dec_move);
                    Thread.Sleep(50);  //第一次拍照，等待相机光反应过来，不可少；
                    FixPointMotionAndCapture(0.0F, 0.0F, 2, true);  //回參考點並拍照 ltt???
                    m_bitmap_calibrate_REF = null;

                    time_start = DateTime.Now;
                    for (; ; )
                    {
                        Thread.Sleep(20);
                        TimeSpan ts = DateTime.Now.Subtract(time_start);
                        if (ts.TotalMilliseconds > 3000) { throw new Exception(); }
                        if (m_bitmap_calibrate_REF != null) { break; }
                    }

                    TimeSpan tss = DateTime.Now.Subtract(timetest);
                    BeginInvoke(new Action(() => { label_test.Text = tss.TotalMilliseconds.ToString(); }));
                    pos_Mark_Start = m_cal_decoder.ShapeMatch(m_bitmap_calibrate_REF)[0];
                    ////BeginInvoke(new Action(() => { m_bitmap_calibrate_REF.Save("c:\\DecodeFailImages\\MARK.BMP"); }));

                    float ratio_X = GlobalVar.gl_value_MarkPointDiameter * 1.0f / pos_Mark_Start.MCHPatterWidth;
                    float ratio_Y = GlobalVar.gl_value_MarkPointDiameter * 1.0f / pos_Mark_Start.MCHPatterHeight;
                    //參考原點偏移值(attention: 位圖坐標系與軸坐標系相反A0A0A0A)
                    GlobalVar.gl_value_CalibrateDis_X = (m_bitmap_calibrate_REF.Width / 2 - pos_Mark_Start.CenterX) * ratio_X;
                    GlobalVar.gl_value_CalibrateDis_Y = (pos_Mark_Start.CenterY - m_bitmap_calibrate_REF.Height / 2) * ratio_Y;

                    //TO 第二个MARK點
                    m_bitmap_calibrate_END = null;
                    FixPointMotionAndCapture(GlobalVar.gl_Ref_Point_CADPos.Pos_X - GlobalVar.gl_point_CalPos.Pos_X,
                        GlobalVar.gl_Ref_Point_CADPos.Pos_Y - GlobalVar.gl_point_CalPos.Pos_Y, 3, false); //multiple--7
                    try
                    {
                        time_start = DateTime.Now;
                        for (; ; )
                        {
                            TimeSpan ts = DateTime.Now.Subtract(time_start);
                            if (ts.TotalMilliseconds > 3000) { throw new Exception(); }
                            if (m_bitmap_calibrate_END != null) break;
                            Thread.Sleep(20);
                        }
                        pos_Mark_End = m_cal_decoder.ShapeMatch(m_bitmap_calibrate_END)[0];
                        //回參考點
                    }
                    catch
                    {
                        Mark_II_result = false;
                    }
                    m_form_movecontrol.WaitAllMoveFinished();
                    Thread.Sleep(30);
                    m_form_movecontrol.FixPointMotion(0, 0, 7);  //第二个MARK点扫描完毕，回原点
                    if (Mark_II_result)
                    {
                        //偏移斜率
                        GlobalVar.gl_value_CalibrateRatio_X = -1.0f * (pos_Mark_End.CenterX - pos_Mark_Start.CenterX) * ratio_X * 1.00F
                            / (GlobalVar.gl_Ref_Point_CADPos.Pos_Y - GlobalVar.gl_point_CalPos.Pos_Y + (pos_Mark_End.CenterY - pos_Mark_Start.CenterY) * ratio_Y);
                        GlobalVar.gl_value_CalibrateRatio_Y = 1.0f * (pos_Mark_End.CenterY - pos_Mark_Start.CenterY) * ratio_Y * 1.00F
                            / (GlobalVar.gl_Ref_Point_CADPos.Pos_X - GlobalVar.gl_point_CalPos.Pos_X + (pos_Mark_End.CenterX - pos_Mark_Start.CenterX) * ratio_X);
                    }
                    BeginInvoke(new Action(() =>
                        {
                            label_deviation_VX.Text = GlobalVar.gl_value_CalibrateDis_X.ToString("0.00000");
                            label_deviation_VY.Text = GlobalVar.gl_value_CalibrateDis_Y.ToString("0.00000");
                            label_deviation_Slopy_X.Text = (GlobalVar.gl_value_CalibrateRatio_X * 10000.00).ToString("0.00000");
                            label_deviation_Slopy_Y.Text = (GlobalVar.gl_value_CalibrateRatio_Y * 10000.00).ToString("0.00000");
                        }));
                }
                catch
                {
                    Mark_I_result = false;
                    ////BeginInvoke(new Action(() => { MessageBox.Show("坐標校準失敗，請重新校準! \r\n" + ex.ToString()); }));
                    //if (m_times_duplicateCalibrate < 5)
                    //{
                    //    m_tag_CalibrateOK = m_inScanFunction = false;
                    //    CalibrateAction();
                    //}
                    //else
                    //{
                    //    //超过5次校准都失败了，跳过此步骤
                    //    m_times_duplicateCalibrate = 0;
                    //    GlobalVar.gl_value_CalibrateDis_X = GlobalVar.gl_value_CalibrateDis_Y = 0;
                    //    m_tag_CalibrateOK = true;
                    //    runCommand(null);
                    //}
                }
                m_inScanFunction = false;
                m_tag_CalibrateOK = true;
                m_times_duplicateCalibrate = 0;
                //if (GlobalVar.gl_inEmergence) { return; }
                if (Mark_I_result && Mark_II_result)
                {
                    AddShowLog(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n校准MARK点成功", Color.Green);
                    runCommand(null);
                    m_times_duplicateCalibrate = 0;
                    //richTextBox_SingleShow.AppendText(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "\t校准MARK点成功");

                }
                else
                {
                    updateLedLightStatus(2);
                    ShowPsdErrForm sp = new ShowPsdErrForm("坐標校準失敗，調整製品位置重新作業!", false);
                    sp.ShowDialog();
                    updateLedLightStatus(1);
                    m_form_movecontrol.Stage2ZaibanPass();
                    //richTextBox_SingleShow.AppendText(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "\t二段载板退出");
                    AddShowLog(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n二段载板退出", Color.Green);
                    m_count_BoardIn--;
                    updateLedLightStatus(0);
                    clearTags();
                    //richTextBox_SingleShow.AppendText(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "\t校准MARK点失败");
                    AddShowLog(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n校准MARK点失败", Color.Green);
                }
            }));
            thread_calibrate.IsBackground = true;
            thread_calibrate.Start();
        }



        private void test_CalibrateAction()
        {
            //richTextBox_SingleShow.AppendText(DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "\t开始校准MARK点");
            AddShowLog(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n开始校准MARK点", Color.Green);
            //sliderExposureTime.valueChanged(GlobalVar.gl_paras_basler_Exposure_Calibrate); //ltt
            //baslerCCD1.SetExposureValue(GlobalVar.gl_paras_basler_Exposure_Calibrate);
            //m_times_duplicateCalibrate++;
            PositionInfo pos_Mark_Start = new PositionInfo();
            PositionInfo pos_Mark_End = new PositionInfo();
            m_list_bmpReceived.Clear();
            thread_calibrate = new Thread(new ThreadStart(delegate
            {
                bool Mark_I_result = true;   //第一个点是否匹配OK
                bool Mark_II_result = true;  //第二个点是否匹配OK
                DateTime time_start = DateTime.Now;
                try
                {
                    //拍照MARK点需要点亮，全部亮，exposure值为参数设定中的默认值
                    //SetLedLightAndExposure("MARK");
                    //sliderExposureTime.valueChanged(GlobalVar.gl_paras_basler_Exposure_Calibrate); //ltt
                    //baslerCCD1.SetExposureValue(GlobalVar.gl_exposure_Mark_default);

                    m_form_movecontrol_eve_MotionMsg("MARK点查找进行中");
                    //if (GlobalVar.gl_inEmergence) { return; }
                    //设置为较快速度
                    //m_form_movecontrol.SetProp_GPSpeed(m_form_movecontrol.m_GPValue_VelHigh_move, m_form_movecontrol.m_GPValue_VelLow_move,
                    // m_form_movecontrol.m_GPValue_Acc_move, m_form_movecontrol.m_GPValue_Dec_move);
                    // Thread.Sleep(50);  //第一次拍照，等待相机光反应过来，不可少；
                    //FixPointMotionAndCapture(0.0F, 0.0F, 2, true);  //回參考點並拍照 ltt???
                    //m_bitmap_calibrate_REF = null;

                    time_start = DateTime.Now;
                    //for (; ; )
                    //{
                    //    Thread.Sleep(20);
                    //    TimeSpan ts = DateTime.Now.Subtract(time_start);
                    //    if (ts.TotalMilliseconds > 3000) { throw new Exception(); }
                    //    if (m_bitmap_calibrate_REF != null) { break; }
                    //}

                    TimeSpan tss = DateTime.Now.Subtract(timetest);
                    BeginInvoke(new Action(() => { label_test.Text = tss.TotalMilliseconds.ToString(); }));
                    //pos_Mark_Start = m_cal_decoder.ShapeMatch(m_bitmap_calibrate_REF)[0];
                    ////BeginInvoke(new Action(() => { m_bitmap_calibrate_REF.Save("c:\\DecodeFailImages\\MARK.BMP"); }));

                    float ratio_X = 1;// GlobalVar.gl_value_MarkPointDiameter * 1.0f / pos_Mark_Start.MCHPatterWidth;
                    float ratio_Y = 1;// GlobalVar.gl_value_MarkPointDiameter * 1.0f / pos_Mark_Start.MCHPatterHeight;
                    //參考原點偏移值(attention: 位圖坐標系與軸坐標系相反A0A0A0A)
                    GlobalVar.gl_value_CalibrateDis_X = 0;// (m_bitmap_calibrate_REF.Width / 2 - pos_Mark_Start.CenterX) * ratio_X;
                    GlobalVar.gl_value_CalibrateDis_Y = 0;// (pos_Mark_Start.CenterY - m_bitmap_calibrate_REF.Height / 2) * ratio_Y;

                    //TO 第二个MARK點
                    m_bitmap_calibrate_END = null;
                    //FixPointMotionAndCapture(GlobalVar.gl_Ref_Point_CADPos.Pos_X - GlobalVar.gl_point_CalPos.Pos_X,
                    //    GlobalVar.gl_Ref_Point_CADPos.Pos_Y - GlobalVar.gl_point_CalPos.Pos_Y, 3, false); //multiple--7
                    try
                    {
                        time_start = DateTime.Now;
                        //for (; ; )
                        //{
                        //    TimeSpan ts = DateTime.Now.Subtract(time_start);
                        //    if (ts.TotalMilliseconds > 3000) { throw new Exception(); }
                        //    if (m_bitmap_calibrate_END != null) break;
                        //    Thread.Sleep(20);
                        //}
                        //pos_Mark_End = m_cal_decoder.ShapeMatch(m_bitmap_calibrate_END)[0];
                        //回參考點
                    }
                    catch
                    {
                        Mark_II_result = false;
                    }
                    //m_form_movecontrol.WaitAllMoveFinished();
                    Thread.Sleep(30);
                    // m_form_movecontrol.FixPointMotion(0, 0, 7);  //第二个MARK点扫描完毕，回原点
                    if (Mark_II_result)
                    {
                        //偏移斜率
                        GlobalVar.gl_value_CalibrateRatio_X = 0;// -1.0f * (pos_Mark_End.CenterX - pos_Mark_Start.CenterX) * ratio_X * 1.00F
                                                                /// (GlobalVar.gl_Ref_Point_CADPos.Pos_Y - GlobalVar.gl_point_CalPos.Pos_Y + (pos_Mark_End.CenterY - pos_Mark_Start.CenterY) * ratio_Y);
                        GlobalVar.gl_value_CalibrateRatio_Y = 0;// 1.0f * (pos_Mark_End.CenterY - pos_Mark_Start.CenterY) * ratio_Y * 1.00F
                                                                // / (GlobalVar.gl_Ref_Point_CADPos.Pos_X - GlobalVar.gl_point_CalPos.Pos_X + (pos_Mark_End.CenterX - pos_Mark_Start.CenterX) * ratio_X);
                    }
                    BeginInvoke(new Action(() =>
                    {
                        label_deviation_VX.Text = GlobalVar.gl_value_CalibrateDis_X.ToString("0.00000");
                        label_deviation_VY.Text = GlobalVar.gl_value_CalibrateDis_Y.ToString("0.00000");
                        label_deviation_Slopy_X.Text = (GlobalVar.gl_value_CalibrateRatio_X * 10000.00).ToString("0.00000");
                        label_deviation_Slopy_Y.Text = (GlobalVar.gl_value_CalibrateRatio_Y * 10000.00).ToString("0.00000");
                    }));
                }
                catch
                {
                    Mark_I_result = false;
                }
                m_inScanFunction = false;
                m_tag_CalibrateOK = true;
                m_times_duplicateCalibrate = 0;
                if (Mark_I_result && Mark_II_result)
                {
                    runCommand(null);
                    m_times_duplicateCalibrate = 0;
                }
                else
                {
                    updateLedLightStatus(2);
                    ShowPsdErrForm sp = new ShowPsdErrForm("坐標校準失敗，調整製品位置重新作業!", false);
                    sp.ShowDialog();
                    updateLedLightStatus(1);
                    m_form_movecontrol.Stage2ZaibanPass();
                    AddShowLog(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n二段载板退出", Color.Green);
                    m_count_BoardIn--;
                    updateLedLightStatus(0);
                    clearTags();
                    AddShowLog(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n校准MARK点失败", Color.Green);
                }
            }));
            thread_calibrate.IsBackground = true;
            thread_calibrate.Start();
        }
        /* 誤差分離校準方式，需要每次採集3各點 不採用
        private void CalibrateAction()
        {
            PositionInfo pos_REF, pos_X;
            Thread thread_cal = new Thread(new ThreadStart(delegate
            {
                try
                {
                    MatrixDecode decoder = new MatrixDecode();
                    if (GlobalVar.gl_List_PointInfo.Count == 0)
                    {
                        MessageBox.Show("未導入品目CAD文檔，請確認!");
                        return;
                    }
                    FixPointMotionAndCapture(0.0F, 0.0F);  //回參考點並拍照
                    m_bitmap_calibrate_REF = null;
                    for (; ; )
                    { if (m_bitmap_calibrate_REF != null)break; Thread.Sleep(100); }
                    pos_REF = decoder.ShapeMatch(m_bitmap_calibrate_REF)[0];
                    //TO MARK_X點
                    m_bitmap_calibrate_X = null;
                    FixPointMotionAndCapture(GlobalVar.gl_Ref_Point.Pos_X - GlobalVar.gl_point_CalibrationPoint_X.Pos_X
                        , GlobalVar.gl_Ref_Point.Pos_Y - GlobalVar.gl_point_CalibrationPoint_X.Pos_Y);
                    for (; ; )
                    { if (m_bitmap_calibrate_X != null)break; Thread.Sleep(100); }
                    pos_X = decoder.ShapeMatch(m_bitmap_calibrate_X)[0];
                    //回參考點
                    FixPointMotion(0.0F, 0.0F);  
                    float ratio_X = GlobalVar.gl_value_BMRadio / pos_REF.MCHPatterWidth;
                    float ratio_Y = GlobalVar.gl_value_BMRadio / pos_REF.MCHPatterHeight;
                    GlobalVar.gl_value_CalibrateDis_X = (pos_REF.CenterX - m_bitmap_calibrate_X.Width / 2) * ratio_X;
                    GlobalVar.gl_value_CalibrateDis_Y = (pos_REF.CenterY - m_bitmap_calibrate_X.Height / 2) * ratio_Y;
                    GlobalVar.gl_value_CalibrateRatio_X = (pos_X.CenterY - pos_REF.CenterY) * ratio_Y * 1.00F
                        / (GlobalVar.gl_Ref_Point.Pos_X - GlobalVar.gl_point_CalibrationPoint_X.Pos_X
                        + (pos_X.CenterX - pos_REF.CenterX) * ratio_X);
                }
                catch(Exception  ex)
                { BeginInvoke(new Action(() => { MessageBox.Show("坐標校準失敗，請重新校準! \r\n" + ex.ToString()); })); }
            }));
            thread_cal.IsBackground = true;
            thread_cal.Start();
        }

        //分离Y轴误差计算
        private void CalibrateAction_Y()
        {
            PositionInfo pos_REF,  pos_Y;
            Thread thread_cal = new Thread(new ThreadStart(delegate
            {
                try
                {
                    MatrixDecode decoder = new MatrixDecode();
                    if (GlobalVar.gl_List_PointInfo.Count == 0)
                    {
                        MessageBox.Show("未導入品目CAD文檔，請確認!");
                        return;
                    }
                    FixPointMotionAndCapture(0.0F, 0.0F);  //回參考點並拍照
                    m_bitmap_calibrate_REF = null;
                    for (; ; )
                    { if (m_bitmap_calibrate_REF != null)break; Thread.Sleep(100); }
                    pos_REF = decoder.ShapeMatch(m_bitmap_calibrate_REF)[0];
                    //TO MARK_Y點
                    m_bitmap_calibrate_Y = null;
                    FixPointMotionAndCapture(GlobalVar.gl_Ref_Point.Pos_X - GlobalVar.gl_point_CalibrationPoint_Y.Pos_X
                        , GlobalVar.gl_Ref_Point.Pos_Y - GlobalVar.gl_point_CalibrationPoint_Y.Pos_Y);
                    for (; ; )
                    { if (m_bitmap_calibrate_Y != null)break; Thread.Sleep(100); }
                    pos_Y = decoder.ShapeMatch(m_bitmap_calibrate_Y)[0];
                    //回參考點
                    FixPointMotion(0.0F, 0.0F);  
                    float ratio_X = GlobalVar.gl_value_BMRadio / pos_REF.MCHPatterWidth;
                    float ratio_Y = GlobalVar.gl_value_BMRadio / pos_REF.MCHPatterHeight;
                    GlobalVar.gl_value_CalibrateRatio_Y = (pos_Y.CenterX - pos_REF.CenterX) * ratio_X * 1.00F
                        / ((GlobalVar.gl_Ref_Point.Pos_Y - GlobalVar.gl_point_CalibrationPoint_Y.Pos_Y)
                        + (pos_Y.CenterY - pos_REF.CenterY) * ratio_Y);
                }
                catch (Exception ex)
                { BeginInvoke(new Action(() => { MessageBox.Show("坐標校準失敗，請重新校準! \r\n" + ex.ToString()); })); }
            }));
            thread_cal.IsBackground = true;
            thread_cal.Start();
        }
        */

        private void PositionCalibrate(Bitmap bmp)
        {

        }

        private void pictureBox_capture_Paint(object sender, PaintEventArgs e)
        {
        }
        #endregion

        private void OneCircleReset()
        {
            try
            {
                m_current_num = 0;
                m_list_bmpReceived.Clear();

                //逐个复位DetailBlock中的变量和参数。
                for (int i = 0; i < GlobalVar.gl_List_PointInfo.m_List_PointInfo.Count; i++)
                {
                    OneGroup_Blocks onegroupBlock = GlobalVar.gl_List_PointInfo.m_List_PointInfo[i].m_BlockList_ByGroup;
                    for (int m = 0; m < onegroupBlock.m_BlockinfoList.Count; m++)
                    {
                        onegroupBlock.m_BlockinfoList[m].Reset();
                    }
                }
                BeginInvoke(new Action(() =>
                {
                    try
                    {
                        if (tabControl1.TabPages.Count >= 3)
                        {
                            tabControl1.SelectedIndex = 1;
                        }
                    }
                    catch { }
                }));
            }
            catch { }
        }

        private void clearTags()
        {
            m_tag_CalibrateOK = false;
            m_tag_InCheckAllDecodeFinished = false;
            m_tag_DBQueryFinished = false;
            m_tag_ShifttoStage2Checked = false;
            m_coilstatus_ShiftToStage2 = false;
            m_inScanFunction = false;
            //m_coilstatus_MachineMoveFinished = true;
            //Thread.Sleep(1000);
            //m_coilstatus_MachineMoveFinished = false;
        }

        private void ToolStripMenuItem_save_Click(object sender, EventArgs e)
        {
            if (m_bitmap == null)
            {
                MessageBox.Show("圖片為空，存儲無效!");
                return;
            }
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                m_bitmap.Save(saveFileDialog1.FileName);
            }
        }

        private void btn_softReset_Click(object sender, EventArgs e)
        {
            clearTags();
            m_manualCycleReset = true;
            m_count_BoardIn = 0;
            //OneCircleReset();
        }

        private void timer_alarm_Tick(object sender, EventArgs e)
        {
            try
            {
                if (button_alarm.BackColor == Color.Gray)
                {
                    button_alarm.BackColor = Color.Red;
                }
                else
                {
                    button_alarm.BackColor = Color.Gray;
                }
                button_alarm.Refresh();
            }
            catch { }
        }

        private void ToolStripMenuItem_paraSetting_Click(object sender, EventArgs e)
        {
            para_Setting();
        }

        private void para_Setting()
        {
            try
            {
                string _old_ScanPort = GlobalVar.gl_serialPort_Scan;
                Parameters para = new Parameters(this);
                if (para.ShowDialog() == DialogResult.OK)
                {
                    if (GlobalVar.gl_ProductModel == "")
                    {
                        MessageBox.Show("先确定机种信息");
                        return;
                    }
                    UpdateParaSetting(para);
                    myfunc.WriteGlobalInfoToTBS();
                    if (_old_ScanPort != GlobalVar.gl_serialPort_Scan)
                    {
                        //重新打開串口
                        openScanPort();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("參數存儲失敗: " + ex.ToString());
            }
        }

        private void UpdateParaSetting(Parameters para)
        {
            try
            {
                //整盤數量
                GlobalVar.gl_OneSheetCount = para._TotalSheetPcs;
                //重复解析次数
                GlobalVar.gl_decode_times = para._RedecodeTimes;
                //超時時長
                GlobalVar.gl_decode_timeout = para._decode_timeOut;
                //匹配度
                GlobalVar.gl_MinMatchScore = para._MatchMinScore;
                //條碼長度
                //GlobalVar.gl_length_PCSBarcodeLength = para._BarcodeLength;
                GlobalVar.gl_length_sheetBarcodeLength = para._SheetBarcodeLength;
                //显示块尺寸
                GlobalVar.block_width = para._BlockWidth;
                GlobalVar.block_heigt = para._BlockHeight;
                //Mark點直徑
                try
                { GlobalVar.gl_value_MarkPointDiameter = float.Parse(para._MarkPointDiameter); }
                catch { GlobalVar.gl_value_MarkPointDiameter = 1; }
                //結果上傳、備份
                //GlobalVar.gl_path_FileResult = para._Path_ResultFileSave;
                GlobalVar.gl_path_FileBackUp = para._Path_ResultFileBackUp;
                //图片存储
                GlobalVar.gl_PicsSavePath = para._PicSavePath;
                GlobalVar.gl_saveCapturePics = para._SaveCapturePics;
                GlobalVar.gl_NGPicsSavePath = para._NGPicsSavePath;
                GlobalVar.gl_saveDecodeFailPics = para._SaveNGPics;
                //串口选择
                GlobalVar.gl_serialPort_Scan = para._BarcodeScanPort;
                //正反软件限位
                GlobalVar.gl_PosLimit_X_P = para._PosLimit_X_P;
                GlobalVar.gl_PosLimit_X_N = para._PosLimit_X_N;
                GlobalVar.gl_PosLimit_Y_P = para._PosLimit_Y_P;
                GlobalVar.gl_PosLimit_Y_N = para._PosLimit_Y_N;
                //曝光值
                GlobalVar.gl_exposure_Mark_default = para._Exposure_Mark_Default;
                GlobalVar.gl_exposure_Matrix_default = para._Exposure_Matrix_Default;

                GlobalVar.gl_exposure_Mark_Geortek = para._Exposure_Mark_GER;
                GlobalVar.gl_exposure_Matrix_Geortek = para._Exposure_Matrix_GER;
                GlobalVar.gl_exposure_Mark_ST = para._Exposure_Mark_ST;
                GlobalVar.gl_exposure_Matrix_ST = para._Exposure_Matrix_ST;
                GlobalVar.gl_exposure_Mark_AAC = para._Exposure_Mark_AAC;
                GlobalVar.gl_exposure_Matrix_AAC = para._Exposure_Matrix_AAC;
                GlobalVar.gl_exposure_Mark_Knowles = para._Exposure_Mark_KNOWLES;
                GlobalVar.gl_exposure_Matrix_Knowles = para._Exposure_Matrix_KNOWLES;
                //曝光值NEW
                GlobalVar.gl_Model_prodcutTypeMic = para._Model_ProductType;
                GlobalVar.gl_Model_exposure = para._Model_ExposureMic;
                GlobalVar.gl_Model_prodcutTypeProx = para._Model_ProductTypeProx;
                GlobalVar.gl_Model_exposureProx = para._Model_ExposureProx;
                GlobalVar.gl_Model_exposurePcs = para._Model_ExposurePcs;
                GlobalVar.gl_Model_exposureIC = para._Model_ExposureIC;
                //使用Halcon解析
                GlobalVar.gl_bUseHalcon = para._UseHalcon;
            }
            catch
            { }
        }

        private void ToolStripMenuItem_exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確定關閉軟件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
                == System.Windows.Forms.DialogResult.Yes)
            {
                // 删除原有文件夹 [11/10/2017 617004]
                //                 if (Application.StartupPath!=GlobalVar.gl_strAppPath)
                //                 {
                //                     Directory.Delete(Application.StartupPath);
                //                 }
                Application.Exit();
            }
        }

        private void button_clearTestinfo_Click(object sender, EventArgs e)
        {
            GlobalVar.gl_testinfo_decodefailed = GlobalVar.gl_testinfo_totalSheet
                = GlobalVar.gl_testinfo_totalTest = 0;
            updatetestinfo();
        }

        private void ToolStripMenuItem_normalWorkMode_Click(object sender, EventArgs e)
        {
            SetRailWorkMode(0);
            EmergenceReset();
        }

        private void ToolStripMenuItem_passOnlyMode_Click(object sender, EventArgs e)
        {
            SetRailWorkMode(1);
            EmergenceReset();
        }

        #region LOT输入查询
        private void initTestInfo()
        {
            try
            {
                //textBox_MPN.Text = GlobalVar.gl_str_MPN;
                textBox_LotNo.Text = GlobalVar.gl_str_LotNo;
                //textBox_qualifiedNo_OQC.Text = GlobalVar.gl_str_QualifiedNo;
            }
            catch { }
        }

        private void textBox_LotNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13)
            //{
            //    if (!queryLotNoInfo()) return;
            //    if (!getMICInfobyLotNo()) return;
            //    NewPartNoLoad();
            //}
        }

        private void button_OK_FT_Click(object sender, EventArgs e)
        {
            m_count_BoardIn = 0;
            autoDeleteOldPic();
            if (txtbox_DeviceID.Text == "")
            {
                MessageBox.Show("请输入设备编号");
                return;
            }
            else
            {
                string iniFilePath = GlobalVar.gl_strTargetPath + "\\" + GlobalVar.gl_iniTBS_FileName;
                myfunc.CheckFileExit();
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_iniKey_strDeviceID, GlobalVar.gl_DeviceID, iniFilePath);     //设备编号
            }
            if (GlobalVar.gl_Board1245EInit && m_form_movecontrol.CheckAxisInMoving())
            {
                MessageBox.Show("设备(轴)为Not Ready状态，请停止后检查再作業！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Thread.Sleep(100);
            m_form_movecontrol.ResetAlarmError(); //复位板卡异常状态
            Thread.Sleep(100);
            if (!queryLotNoInfo()) return;
            addLogStr("LOT:" + textBox_LotNo.Text + "\r\n参考原点X坐标:" + textBox_fixPoint_x.Text + "\r\n参考原点Y坐标:" + textBox_fixPoint_y.Text + "\r\n参考原点Z坐标:" + textBox_fixPoint_z.Text + "" + textBox_CalPos_X.Text + "" + textBox_CalPos_Y.Text + "\r\n参原点Z坐标:" + textBox_CalPos_Z.Text);// 增加日志 记录LOT [10/26/2017 617004]
            //需要先查询机种信息、获得CAD信息、FLOWID才能查询MIC/PROX信息
            if (!GlobalVar.gl_AutoLoadType || textBox_LotNo.Text == "99999999999")
            {
                NewPartNoLoad();
            }
            else
            {
                //if (!CommenConfigLoad())
                //{
                NewPartNoNetLoad();
                //   }
            }

            if (GlobalVar.gl_LinkType == LinkType.MIC)
            {
                if (!getMICInfobyLotNo()) return;
            }
            else if (GlobalVar.gl_LinkType == LinkType.PROX)
            {
                if (!getProxInfobyLotNo()) return;
            }
            else if (GlobalVar.gl_LinkType == LinkType.BARCODE)
            {
                if (!getPcsInfobyLotNo()) return;
                //if (CheckShtBarcode(GlobalVar.gl_strShtBarcode)) return;
            }
            else if (GlobalVar.gl_LinkType == LinkType.IC)
            {
                for (int i = 0; i < GlobalVar.gl_List_PointInfo.m_List_PointInfo.Count; i++)
                {
                    OnePointGroup onegroup = GlobalVar.gl_List_PointInfo.m_List_PointInfo[i];
                    onegroup.m_list_zhipingInfo.Clear();
                    //FOR TEST
                    ZhiPinInfo mi = new ZhiPinInfo();
                    mi._SubName = "IC";
                    onegroup.m_list_zhipingInfo.Add(mi);

                }
            }
            else
            {
                MessageBox.Show("未知关联作业类型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;


            }
            try
            {
                if (GlobalVar.gl_LinkType == LinkType.IC)
                {
                    GlobalVar.gl_ProductType = new string[] { "IC" };
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        List<string> listStr = new List<string>();
                        string strs = "";
                        for (int i = 0; i < GlobalVar.gl_List_PointInfo.m_List_PointInfo.Count; i++)
                        {
                            OnePointGroup onegroup = GlobalVar.gl_List_PointInfo.m_List_PointInfo[i];
                            for (int j = 0; j < onegroup.m_list_zhipingInfo.Count; j++)
                            {
                                ZhiPinInfo zhipin = onegroup.m_list_zhipingInfo[j];
                                string str = zhipin._SubName;
                                if (!listStr.Contains(str))
                                {
                                    listStr.Add(str);
                                    strs += str + ",";
                                }
                            }
                        }
                        GlobalVar.gl_ProductType = listStr.ToArray();
                        tsslB_productType.Text = "部品类型:[" + strs.Trim(',') + "]";
                    }));
                }
            }
            catch { }
            //参考校准Mark点读取
            //MoveToAxisZRef(); //Z轴2017.12.18            
            myfunc.ReadMarkDefault();
            SetCalPosValue(GlobalVar.gl_point_CalPos);
            #region 显示BLOCK、初始化加载BLOCK list相关信息
            GlobalVar.gl_totalCount = 0;
            for (int m = 0; m < GlobalVar.gl_List_PointInfo.m_List_PointInfo.Count; m++)
            {

                List<SPoint> onegroupPoint = GlobalVar.gl_List_PointInfo.m_List_PointInfo[m].m_ListGroup;
                List<DetailBlock> blocklist = GlobalVar.gl_List_PointInfo.m_List_PointInfo[m].m_BlockList_ByGroup.m_BlockinfoList;
                List<OBJ_TipPoint> m_TipPoint_List = new List<OBJ_TipPoint>();
                GlobalVar.gl_List_PointInfo.m_List_PointInfo[m].m_BlockList_ByGroup.FlowID
                    = GlobalVar.gl_List_PointInfo.m_List_PointInfo[m].FlowID;
                //按照不同FLOWID分的BLOCK组，每组n个BLOCK
                //OneGroup_Blocks blockGroup = new OneGroup_Blocks();
                //OneGroup_Blocks blockGroup = GlobalVar.gl_List_PointInfo.m_List_PointInfo[m].m_BlockList_ByGroup;
                try
                {
                    //blockGroup = new OneGroup_Blocks();
                    //blockGroup.FlowID = GlobalVar.gl_List_PointInfo.m_List_PointInfo[m].FlowID;
                    tabPage_mainview.Controls.Clear();
                    for (int i = 0; i < onegroupPoint.Count; i++)
                    {
                        DetailBlock bi = new DetailBlock();
                        bi.flowid = GlobalVar.gl_List_PointInfo.m_List_PointInfo[m].FlowID;
                        bi.Pos_X_CAD = Math.Abs(onegroupPoint[i].Pos_X);
                        bi.Pos_Y_CAD = Math.Abs(onegroupPoint[i].Pos_Y);
                        bi.Pos_Z_CAD = Math.Abs(onegroupPoint[i].Pos_Z);
                        bi.m_PcsNo = onegroupPoint[i].PointNumber;
                        bi.m_PcsNo_Mapping = GetMapNum(bi.m_PcsNo);
                        bi.Location = newPointConvert(bi, i);
                        bi.Width = GlobalVar.block_width;
                        bi.Height = GlobalVar.block_heigt;
                        bi.setPositionDisplay((Math.Abs(onegroupPoint[i].Pos_X - GlobalVar.gl_Ref_Point_CADPos.Pos_X).ToString("0.00"))
                            , (Math.Abs(onegroupPoint[i].Pos_Y - GlobalVar.gl_Ref_Point_CADPos.Pos_Y)).ToString("0.00"));
                        bi.Parent = tabPage_mainview;
                        blocklist.Add(bi);

                        #region 增加显示CAD点阵信息
                        //  [10/20/2017 617004]
                        OBJ_TipPoint TP = new OBJ_TipPoint();
                        //TP._tipIndex = ((SPoint)GlobalVal.gl_List_BlockInfo[i]).PointNumber.ToString();
                        TP.Name = "OBJ_TipPoint" + onegroupPoint[i].PointNumber.ToString("00");
                        TP._tipIndex = onegroupPoint[i].Point_name;
                        TP._Pos_X = onegroupPoint[i].Pos_X.ToString("0.00");
                        TP._Pos_Y = onegroupPoint[i].Pos_Y.ToString("0.00");
                        TP._TPSequence = onegroupPoint[i].PointNumber.ToString();
                        TP._AngleValue = onegroupPoint[i].Angle_deflection.ToString();
                        TP._LineSequence = onegroupPoint[i].Line_sequence.ToString();
                        m_TipPoint_List.Add(TP);
                        Obj_TipPoint_byTPName _comparer = new Obj_TipPoint_byTPName();
                        m_TipPoint_List.Sort(_comparer);
                        #endregion
                    }
                    for (int i = m_TipPoint_List.Count - 1; i >= 0; i--)
                    {
                        m_TipPoint_List[i].Parent = TipPointShow;
                        m_TipPoint_List[i].Dock = DockStyle.Top;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("初始化gl_List_BlockInfo出错" + ex.ToString());
                }
                GlobalVar.gl_totalCount += blocklist.Count;
                init_tabPage_mainview();
            }
            #endregion
        }

        private void MoveToAxisZRef()
        {
            if (GlobalVar.gl_LinkType != GlobalVar.gl_LastLinkType)
            {
                string strconfile = Application.StartupPath + "\\config.ini";
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_iniSection_AxisZRef, GlobalVar.gl_inikey_lastLinkType, GlobalVar.gl_LastLinkType.ToString(), strconfile);
                m_form_movecontrol.MoveToAxisZRef(GlobalVar.gl_dAxisZRef);
            }
        }

        private bool queryLotNoInfo()
        {
            try
            {
                string lotno = textBox_LotNo.Text.Trim();
                if (textBox_operator.Text.Trim().Length == 0)
                {
                    MessageBox.Show("作業員工號輸入為空，請確認！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (lotno.Length != 11)
                {
                    MessageBox.Show("LOT號條碼長度不符，請確認！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (!myfunc.checkStringIsLegal(lotno, 1))
                {
                    MessageBox.Show("LotNo輸入不合法！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //RecoverTestInfoInput();
                    textBox_LotNo.SelectAll();
                    textBox_LotNo.Focus();
                    return false;
                }
                else
                {
                    if (m_DBQuery.setDataBaseMessage(lotno))
                    {
                        GlobalVar.gl_str_LotNo = lotno;
                        textBox_MPN.Text = GlobalVar.gl_str_Product;
                        button_partName.Text = GlobalVar.gl_ProductModel;

                        //PCS管理版本  [10/18/2017 617004]
                        GlobalVar.gl_DBquery = new DBQuery();
                        GlobalVar.gl_bPcsManage = GlobalVar.gl_DBquery.CheckPcsManage(GlobalVar.gl_ProductModel);
                        //if (GlobalVar.gl_bPcsManage)
                        //{
                        //    tssl_PCSManage.Text = "PCS管理版本";
                        //    tssl_PCSManage.BackColor = Color.Orange;
                        //}
                        //else
                        //{
                        //    tssl_PCSManage.Text = "";
                        //    tssl_PCSManage.BackColor = Color.Transparent;
                        //}
                    }
                    button_OK_FT.Focus();
                    string iniFilePath = GlobalVar.gl_strTargetPath + "\\" + GlobalVar.gl_iniTBS_FileName;
                    MyFunctions.WritePrivateProfileString(GlobalVar.gl_inisection_TestInfo, GlobalVar.gl_iniKey_LotNo, GlobalVar.gl_str_LotNo, iniFilePath);
                }
                #region 品目合并方案2017.07.04
                GlobalVar.gl_strMpnAssemble = GlobalVar.gl_strAssemble = GlobalVar.gl_strAssembleX = "";
                GlobalVar.gl_bMPNPlan = m_DBQuery.CheckMPNPlan(GlobalVar.gl_ProductModel);
                if (GlobalVar.gl_bMPNPlan)
                {
                    GlobalVar.gl_strAssemble = DBQuery.GetAssemble(GlobalVar.gl_str_LotNo);//只获得品目合并尾码
                    GlobalVar.gl_listMPNPlan = DBQuery.GetAssembleX(GlobalVar.gl_str_LotNo);//获得品目合并的检查规则-组合（E75用*表示（KK*））2016.08
                    if (GlobalVar.gl_listMPNPlan == null || GlobalVar.gl_listMPNPlan.Count == 0 || GlobalVar.gl_strAssemble == "")
                    {
                        MessageBox.Show("品目合并方案\r未找到组合规则！");
                        return false;
                    }
                    for (int i = 0; i < GlobalVar.gl_listMPNPlan.Count; i++)
                    {
                        string str = GlobalVar.gl_listMPNPlan[i].Position + GlobalVar.gl_listMPNPlan[i].Code + GlobalVar.gl_listMPNPlan[i].Flowid.ToString("00");
                        GlobalVar.gl_strMpnAssemble += str + "-";
                        GlobalVar.gl_strAssembleX += GlobalVar.gl_listMPNPlan[i].Code;
                    }
                }
                #endregion
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }

        //MIC用料信息读取
        public bool getMICInfobyLotNo()
        {
            try
            {
                for (int i = 0; i < GlobalVar.gl_List_PointInfo.m_List_PointInfo.Count; i++)
                {
                    OnePointGroup onegroup = GlobalVar.gl_List_PointInfo.m_List_PointInfo[i];
                    onegroup.m_list_zhipingInfo.Clear();
                    //GlobalVar.gl_list_ZhiPinInfo.Clear();
                    if ((GlobalVar.gl_str_LotNo == "99999999999")
                        || (GlobalVar.gl_str_LotNo == "99999999998"))
                    {
                        //FOR TEST
                        ZhiPinInfo mi = new ZhiPinInfo();
                        mi._SubName = "KNOWLES";
                        mi._HeadStr = "FN8";
                        mi._BarcodeLength = 16;
                        mi._StartPos = 0;
                        mi._StartLen = 3;
                        onegroup.m_list_zhipingInfo.Add(mi);
                        onegroup.m_list_zhipingInfo.Add(mi);
                    }
                    else
                    {
                        if (GlobalVar.gl_str_Product.Trim() == "") { return false; }
                        string sql = "";
                        if (!GlobalVar.gl_bMPNPlan)
                        {
                            sql = "SELECT  SubName,Value1,BarLen,StaPosition,[Valen] FROM BasCheckPart"
                                   + " where ClassName = 'MIC' AND ProductName = '" + GlobalVar.gl_str_Product + "' AND Invalid = '0' "
                                   + " and [FlowId] = '" + onegroup.FlowID + "' ";
                        }
                        else
                        {
                            //分解组合字符串//品目合并2017.07.14
                            List<string> list_assemble = new List<string>(GlobalVar.gl_strMpnAssemble.Split('-'));
                            string _assemble = list_assemble.Find(delegate (string ass)
                            {
                                return ass.Substring(3) == onegroup.FlowID.ToString("00");
                            });
                            sql = string.Format("SELECT distinct SubName,BarLen,Value1,StaPosition,Valen,Value2,StaPosition2,Valen2,FlowId FROM BasCheckPart" +
                                " WHERE productName='{0}' and fpccmtcode='{1}' and fpcposition={2} and Flowid ={3} and Invalid='0'",
                                GlobalVar.gl_str_Product, _assemble.Substring(2, 1), _assemble.Substring(0, 2), onegroup.FlowID);
                        }

                        DataTable dt1 = m_DBQuery.get_database_BaseData(sql);
                        if (dt1 != null)
                        {
                            for (int n = 0; n < dt1.Rows.Count; n++)
                            {
                                ZhiPinInfo mi = new ZhiPinInfo();
                                mi._SubName = dt1.Rows[0 + n]["SubName"].ToString();
                                mi._HeadStr = dt1.Rows[0 + n]["Value1"].ToString();
                                mi._BarcodeLength = Convert.ToInt32(dt1.Rows[0 + n]["BarLen"].ToString());
                                mi._StartPos = Convert.ToInt32(dt1.Rows[0 + n]["StaPosition"].ToString()) - 1;
                                mi._StartLen = Convert.ToInt32(dt1.Rows[0 + n]["Valen"].ToString());
                                onegroup.m_list_zhipingInfo.Add(mi);
                            }
                        }
                        if (onegroup.m_list_zhipingInfo.Count == 0)
                        {
                            MessageBox.Show("Mic用料信息缺失!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("查询Mic用料信息出错!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool getProxInfobyLotNo()
        {
            try
            {
                for (int i = 0; i < GlobalVar.gl_List_PointInfo.m_List_PointInfo.Count; i++)
                {
                    OnePointGroup onegroup = GlobalVar.gl_List_PointInfo.m_List_PointInfo[i];
                    onegroup.m_list_zhipingInfo.Clear();
                    //GlobalVar.gl_list_ZhiPinInfo.Clear();
                    if ((GlobalVar.gl_str_LotNo == "99999999999")
                        || (GlobalVar.gl_str_LotNo == "99999999998"))
                    {
                        //FOR TEST
                        ZhiPinInfo mi = new ZhiPinInfo();
                        mi._SubName = "KNOWLES";
                        mi._HeadStr = "FN8";
                        mi._BarcodeLength = 16;
                        mi._StartPos = 0;
                        mi._StartLen = 3;
                        onegroup.m_list_zhipingInfo.Add(mi);
                        return true;
                    }
                    if (GlobalVar.gl_str_Product.Trim() == "") { return false; }
                    //根据品目号查询PROX制品条码前3位，用作检查，如果没有记录则报错---PROX一个品目只有一条记录
                    string sql = "SELECT  TOP  1  [SubName],[Value1],[BarLen],[StaPosition],[Valen] FROM [BASEDATA].[dbo].[BasCheckPart] "
                             + " where ClassName = 'PROX' AND ProductName = '" + GlobalVar.gl_str_Product + "' AND Invalid = '0' ";
                    DataTable dt1 = m_DBQuery.get_database_BARDATA(sql);
                    if ((dt1 != null) && (dt1.Rows.Count > 0))
                    {
                        ZhiPinInfo mi = new ZhiPinInfo();
                        mi._HeadStr = dt1.Rows[0]["Value1"].ToString();
                        mi._SubName = dt1.Rows[0]["SubName"].ToString();
                        mi._BarcodeLength = Convert.ToInt32(dt1.Rows[0]["BarLen"].ToString());
                        mi._StartPos = Convert.ToInt32(dt1.Rows[0]["StaPosition"].ToString()) - 1;
                        mi._StartLen = Convert.ToInt32(dt1.Rows[0]["Valen"].ToString());
                        onegroup.m_list_zhipingInfo.Add(mi);
                    }
                    else
                    {
                        MessageBox.Show("沒有對應品目的PROX條碼信息", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("品目的PROX條碼信息查詢異常:" + e.ToString());
                return false;
            }
            return true;
        }
        private bool getPcsInfobyLotNo()
        {
            try
            {
                for (int i = 0; i < GlobalVar.gl_List_PointInfo.m_List_PointInfo.Count; i++)
                {
                    OnePointGroup onegroup = GlobalVar.gl_List_PointInfo.m_List_PointInfo[i];
                    onegroup.m_list_zhipingInfo.Clear();
                    //GlobalVar.gl_list_ZhiPinInfo.Clear();
                    if ((GlobalVar.gl_str_LotNo == "99999999999")
                        || (GlobalVar.gl_str_LotNo == "99999999998"))
                    {
                        //FOR TEST
                        ZhiPinInfo mi = new ZhiPinInfo();
                        mi._SubName = "HC9X";
                        mi._HeadStr = "CKX";
                        mi._BarcodeLength = 22;
                        //mi._StartPos = 0;
                        //mi._StartLen = 3;
                        onegroup.m_list_zhipingInfo.Add(mi);
                        return true;
                    }
                    if (GlobalVar.gl_str_Product.Trim() == "") { return false; }
                    //根据品目号查询PCS制品条码前3位，用作检查，如果没有记录则报错---PCS一个品目只有一条记录
                    string sql = "SELECT DISTINCT [PPP],[EEEE],[BarLen] FROM [BASEDATA].[dbo].[ProjectBasic] WHERE [Product]='" + GlobalVar.gl_str_Product + "'";
                    DataTable dt1 = m_DBQuery.get_database_BARDATA(sql);
                    if ((dt1 != null) && (dt1.Rows.Count > 0))
                    {
                        ZhiPinInfo bi = new ZhiPinInfo();
                        bi._HeadStr = dt1.Rows[0]["PPP"].ToString();
                        bi._SubName = dt1.Rows[0]["EEEE"].ToString();
                        bi._BarcodeLength = Convert.ToInt32(dt1.Rows[0]["BarLen"].ToString());
                        //                         mi._StartPos = Convert.ToInt32(dt1.Rows[0]["StaPosition"].ToString()) - 1;
                        //                         mi._StartLen = Convert.ToInt32(dt1.Rows[0]["Valen"].ToString());
                        onegroup.m_list_zhipingInfo.Add(bi);

                    }
                    else
                    {
                        MessageBox.Show("沒有對應品目的PCS條碼信息", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("品目的PCS條碼信息查詢異常:" + e.ToString());
                return false;
            }
            return true;
        }

        //检查sheet条码合法性
        private bool CheckShtBarcode(String SheetBarcode)
        {
            try
            {
                //if (GlobalVar.gl_usermode == 1) return true;// 调试模式不判断sheet条码 [10/20/2017 617004]
                if (!myfunc.checkStringIsLegal(SheetBarcode, 3))
                {
                    SetSheetBarLabel("條碼不合規則");
                    GlobalVar.gl_strShtBarcode = "";
                    return false; //條碼不合規則
                }
                //if (SheetBarcode.Trim().Length != GlobalVar.gl_BarcodeLength_Sheet) //fortest sht
                if (SheetBarcode.Trim().Length <= 0) //fortest sht
                {
                    SetSheetBarLabel("Sheet條碼長度 <= 0");
                    GlobalVar.gl_strShtBarcode = "";
                    return false;
                }
                GlobalVar.gl_DBquery = new DBQuery(GlobalVar.gl_str_Product, "");//调用Pcs相关数据库构造方法
                int value = GlobalVar.gl_DBquery.CheckSheeBarCode(SheetBarcode);
                GlobalVar.gl_lsChkItem = DBQuery.GetErrorList();
                //if (value != 0) return false;
                int nIndex = GlobalVar.gl_lsChkItem.FindIndex(0, GlobalVar.gl_lsChkItem.Count, delegate (CheckItem chk) { return chk.ID == value; });
                if (nIndex >= 0)
                {
                    GlobalVar.gl_lsChkItem[nIndex].Count++;
                    if (value == 0)
                    {
                        SetSheetBarLabel(SheetBarcode.Trim());
                        GlobalVar.gl_strShtBarcode = SheetBarcode.Trim();
                    }
                    else
                    {
                        SetSheetBarLabel(SheetBarcode.Trim());
                        GlobalVar.gl_strShtBarcode = "";
                        ShowPsdErrForm sp = new ShowPsdErrForm("SHEET條碼" + GlobalVar.gl_lsChkItem[nIndex].Name + ",請重新作業!", false);
                        sp.ShowDialog();
                        return false;
                    }
                }
                return true;
            }
            catch { return false; }
            finally
            {
                this.Invoke(new Action(() => { button_sheetSNInfo.Text = GlobalVar.gl_strShtBarcode; }));
            }
        }

        private bool CheckShtBarcode_IC(String SheetBarcode)
        {
            try
            {
                //if (GlobalVar.gl_usermode == 1) return true;// 调试模式不判断sheet条码 [10/20/2017 617004]
                if (!myfunc.checkStringIsLegal(SheetBarcode, 3))
                {
                    SetSheetBarLabel("條碼不合規則");
                    GlobalVar.gl_strShtBarcode = "";
                    return false; //條碼不合規則
                }
                //if (SheetBarcode.Trim().Length != GlobalVar.gl_BarcodeLength_Sheet) //fortest sht
                if (SheetBarcode.Trim().Length <= 0) //fortest sht
                {
                    SetSheetBarLabel("Sheet條碼長度 <= 0");
                    GlobalVar.gl_strShtBarcode = "";
                    return false;
                }
                GlobalVar.gl_DBquery = new DBQuery(GlobalVar.gl_str_Product, "");//调用Pcs相关数据库构造方法
                int value = GlobalVar.gl_DBquery.CheckShtBarcode_IC(SheetBarcode);
                if (value != 0) return false;
                return true;
            }
            catch { return false; }
            finally
            {
                this.Invoke(new Action(() => { button_sheetSNInfo.Text = GlobalVar.gl_strShtBarcode; }));
            }
        }
        private void SetSheetBarLabel(string str)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    button_sheetSNInfo.Text = str;
                }));
            }
            catch { }
        }
        #endregion

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void ToolStripMenuItem_adminLogin_Click(object sender, EventArgs e)
        {
            if (GlobalVar.gl_usermode == 0)
            {
                LogonOn logon = new LogonOn();
                if (logon.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                { return; }
            }
            if (GlobalVar.gl_usermode == 0)
            {
                GlobalVar.gl_usermode = 1;
                ToolStripMenuItem_adminLogin.Text = "管理登出";
                toolStripLabel_opreatorType.Text = "当前模式：[管理员]";
                baslerCCD1.StopCCD();
                AdminEnable(true);

            }
            else
            {
                GlobalVar.gl_usermode = 0;
                ToolStripMenuItem_adminLogin.Text = "管理登入";
                toolStripLabel_opreatorType.Text = "当前模式：[OP]";
                AdminEnable(false);
            }
        }
        private void AdminEnable(bool enable)
        {
            groupBox_refpointSetting.Enabled = groupBox_markPointSet.Enabled = enable;
            m_obj_dwg.setRefSettingEnable(enable);
            m_form_movecontrol.Enabled = enable;
            ToolStripMenuItem_exit.Enabled = enable;
            baslerCCD1.Enabled = enable;
            ToolStripMenuItem_RunTest.Enabled = enable;
            m_form_movecontrol.SetControlEnable(enable);
            btn_safetyDoorDispose.Enabled = enable;
            btn_safetyDoorEnable.Enabled = enable;
            panel_LodeType.Enabled = enable;
            txtbox_DeviceID.Enabled = enable;
            groupBox_LightControl.Enabled = enable;
            button1.Enabled = enable;
            if (enable)
            {
                TipPointShow.Width = 162;
            }
            else
                TipPointShow.Width = 0;
        }

        private void ToolStripMenuItem_reboot_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否確定關閉電腦并重新啟動？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning
                , MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                ShutdownPC();
                Application.Exit();
            }
        }

        public static void ShutdownPC()
        {
            ProcessStartInfo PS = new ProcessStartInfo();
            PS.FileName = "shutdown.exe";
            PS.Arguments = "-r -t 1";
            Process.Start(PS);
        }

        private void ToolStripMenuItem_deleteHisPicture_Click(object sender, EventArgs e)
        {
            MessageBox.Show("一定要在设备空闲的时候执行此操作，否则会导致程序卡死.点击确定键继续.", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (m_inScanFunction) { return; }
            deleteOldPictures();
        }

        private void deleteOldPictures()
        {
            try
            {
                if (GlobalVar.gl_PicsSavePath.Trim() == "") { return; }
                DirectoryInfo DI = new DirectoryInfo(GlobalVar.gl_PicsSavePath);
                foreach (DirectoryInfo sub_DI in DI.GetDirectories())
                {
                    TimeSpan ts = DateTime.Now - sub_DI.CreationTime;
                    if (ts.TotalHours >= 48)
                    {
                        Directory.Delete(sub_DI.FullName, true);
                    }
                }
                if (GlobalVar.gl_NGPicsSavePath.Trim() == "") { return; }
                DirectoryInfo DII = new DirectoryInfo(GlobalVar.gl_NGPicsSavePath);
                foreach (DirectoryInfo sub_DI in DII.GetDirectories())
                {
                    TimeSpan ts = DateTime.Now - sub_DI.CreationTime;
                    if (ts.TotalHours >= 48)
                    {
                        Directory.Delete(sub_DI.FullName, true);
                    }
                }
                foreach (FileInfo sub_DI in DII.GetFiles())
                {
                    TimeSpan ts = DateTime.Now - sub_DI.CreationTime;
                    if (ts.TotalHours >= 48)
                    {
                        Directory.Delete(sub_DI.FullName, true);
                    }
                }
                MessageBox.Show("文件刪除完畢！");
            }
            catch
            {
                MessageBox.Show("文件刪除失敗，請進行手動刪除！");
            }
        }

        private void ToolStripMenuItem_RunTest_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(runCommand_withoutCapture);
        }

        private void button_alarm_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确定接触警报异常？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == System.Windows.Forms.DialogResult.Yes)
            {
                m_form_movecontrol.ResetAlarmError();
                m_form_movecontrol_eve_EmergenceRelease(null, null);
            }
        }

        #region 芯片厂商自动识别/光源环境设置
        private void toolStripButton_autoDetectMicType_Click(object sender, EventArgs e)
        {
            //if (GlobalVar.gl_List_PointInfo.Count < 5)
            //{
            //    MessageBox.Show("没有导入品目信息和制品参数，请重新输入LOTNO!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return; 
            //}
        }

        /// <param name="MICType">AAC/ST/GEORTEC/KNOWLES</param>
        public void SetLedLightAndExposure(string MICType)
        {
            //拍摄MARK点，全开
            if (MICType == "MARK")
            {
                GlobalVar.gl_paras_basler_Exposure_Calibrate = GlobalVar.gl_exposure_Mark_default;
                GlobalVar.gl_paras_basler_Exposure_Scan = GlobalVar.gl_exposure_Matrix_default;
                //红光ON  白光ON
                m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_Z, 6, 1);
                m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_U, 6, 1);
                return;
            }
            this.Invoke(new Action(() =>
            {
                toolStripButton_autoDetectMicType.Text = "当前作业类型:[" + MICType + "]";
            }));
            myfunc.ReadProductTypeExposure(MICType);
            if (GlobalVar.gl_LinkType == LinkType.PROX)
            {
                GlobalVar.gl_paras_basler_Exposure_Calibrate = GlobalVar.gl_exposure_Mark_default;
                GlobalVar.gl_paras_basler_Exposure_Scan = GlobalVar.gl_exposure_Matrix_default;
                //红光ON  白光OFF
                m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_Z, 6, 1);
                m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_U, 6, 0);
            }
            else if (GlobalVar.gl_LinkType == LinkType.MIC)
            {
                #region //光源曝光值
                //switch (MICType.Trim().ToUpper())
                //{
                //    case "AAC":
                //        GlobalVar.gl_paras_basler_Exposure_Calibrate = GlobalVar.gl_exposure_Mark_AAC;
                //        GlobalVar.gl_paras_basler_Exposure_Scan = GlobalVar.gl_exposure_Matrix_AAC;
                //        //红光ON  白光OFF
                //        m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_Z, 6, 1);
                //        m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_U, 6, 0);
                //        toolStripButton_autoDetectMicType.Text = "部品类型:[AAC]";
                //        break;
                //    case "ST":
                //        GlobalVar.gl_paras_basler_Exposure_Calibrate = GlobalVar.gl_exposure_Mark_ST;
                //        GlobalVar.gl_paras_basler_Exposure_Scan = GlobalVar.gl_exposure_Matrix_ST;
                //        //红光OFF 白光ON 
                //        m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_Z, 6, 0);
                //        m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_U, 6, 1);
                //        break;
                //    case "GEORTEK":
                //    case "GOERTEK":
                //        GlobalVar.gl_paras_basler_Exposure_Calibrate = GlobalVar.gl_exposure_Mark_Geortek;
                //        GlobalVar.gl_paras_basler_Exposure_Scan = GlobalVar.gl_exposure_Matrix_Geortek;
                //        //红光ON  白光OFF 
                //        m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_Z, 6, 1);
                //        m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_U, 6, 0);
                //        toolStripButton_autoDetectMicType.Text = "部品类型:[GOERTEK]";
                //        break;
                //    case "KNOWLES": //未验证
                //        GlobalVar.gl_paras_basler_Exposure_Calibrate = GlobalVar.gl_exposure_Mark_Knowles;
                //        GlobalVar.gl_paras_basler_Exposure_Scan = GlobalVar.gl_exposure_Matrix_Knowles;
                //        //红光ON  白光OFF
                //        m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_Z, 6, 1);
                //        m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_U, 6, 1);
                //        toolStripButton_autoDetectMicType.Text = "部品类型:[KNOWLES]";
                //        break;
                //    default:
                //        GlobalVar.gl_paras_basler_Exposure_Calibrate = GlobalVar.gl_exposure_Mark_default;
                //        GlobalVar.gl_paras_basler_Exposure_Scan = GlobalVar.gl_exposure_Matrix_default;
                //        //红光ON  白光OFF
                //        m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_Z, 6, 1);
                //        m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_U, 6, 0);
                //        toolStripButton_autoDetectMicType.Text = "部品类型:[KNOWLES]";
                //        break;
                //}
                #endregion
                #region 光源曝光值NEW
                GlobalVar.gl_paras_basler_Exposure_Scan = GlobalVar.gl_Model_exposure;
                //红光ON  白光OFF
                m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_Z, 6, byte.Parse(GlobalVar.gl_Model_redLight.ToString()));
                m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_U, 6, byte.Parse(GlobalVar.gl_Model_whiteLight.ToString()));
                #endregion
            }
            else if (GlobalVar.gl_LinkType == LinkType.BARCODE)
            {
                GlobalVar.gl_paras_basler_Exposure_Calibrate = GlobalVar.gl_exposure_Mark_default;
                GlobalVar.gl_paras_basler_Exposure_Scan = GlobalVar.gl_exposure_Matrix_default;
                //红光ON  白光ON
                m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_Z, 6, 1);
                m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_U, 6, 1);
            }
            else if (GlobalVar.gl_LinkType == LinkType.IC)
            {
                GlobalVar.gl_paras_basler_Exposure_Calibrate = GlobalVar.gl_exposure_Mark_default;
                GlobalVar.gl_paras_basler_Exposure_Scan = GlobalVar.gl_exposure_Matrix_default;
                //红光ON  白光ON
                m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_Z, 6, 1);
                m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_U, 6, 1);
            }
        }
        #endregion

        #region 结果文件上传
        Thread m_thread_Upload;
        bool m_inUploading = false;  //是否正在上传。
        private string m_iniFullName;
        private string m_str_Connstr = "";     //数据库连接字串--从sheet.ini中获取
        private List<pcsinfo> m_list_info = new List<pcsinfo>();
        private List<string> m_listNGPosition_preScan = new List<string>();
        private string m_SheetBarcode; //上传sheetbarcode
        public uint m_AxisNum_X = 0;
        public uint m_AxisNum_Y = 1;
        public uint m_AxisNum_Z = 2;
        public uint m_AxisNum_U = 3;
        IntPtr[] m_Axishand = new IntPtr[32];
        private bool m_tag_boardArrived = false;
        private void startUpload()
        {
            logWR.appendNewLogMessage("检查是否有需要上传的文件");
            if (!m_inUploading)
            {
                if (!Directory.Exists(GlobalVar.gl_path_FileBackUp))
                {
                    MessageBox.Show("不存在上传结果备份文件夹：" + GlobalVar.gl_path_FileBackUp + "，请在参数设置中设定。");
                    return;
                }
                if (!Directory.Exists(GlobalVar.gl_Directory_savePath))
                {
                    MessageBox.Show("不存在上传结果文件夹：" + GlobalVar.gl_Directory_savePath + "，请确认。");
                    return;
                }
                if (m_thread_Upload == null)
                {
                    m_thread_Upload = new Thread(selectFile);
                }
                if (!m_thread_Upload.IsAlive)
                {
                    if (m_thread_Upload.ThreadState == System.Threading.ThreadState.Aborted)
                    {
                        m_thread_Upload = new Thread(selectFile);
                    }
                    m_thread_Upload.Priority = ThreadPriority.AboveNormal;
                    m_thread_Upload.IsBackground = true;
                    m_thread_Upload.Start();
                }
                m_inUploading = true;
                button1.BackColor = Color.Green;
                button1.Text = "结束上传";
            }
        }

        private void button_startUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (!m_inUploading)
                {
                    if (!Directory.Exists(GlobalVar.gl_path_FileBackUp))
                    {
                        MessageBox.Show("不存在上传结果备份文件夹：" + GlobalVar.gl_path_FileBackUp + "，请在参数设置中设定。");
                        return;
                    }
                    if (!Directory.Exists(GlobalVar.gl_Directory_savePath))
                    {
                        MessageBox.Show("不存在上传结果文件夹：" + GlobalVar.gl_Directory_savePath + "，请确认。");
                        return;
                    }
                    if (m_thread_Upload == null)
                    {
                        m_thread_Upload = new Thread(selectFile);
                    }
                    if (!m_thread_Upload.IsAlive)
                    {
                        if (m_thread_Upload.ThreadState == System.Threading.ThreadState.Aborted)
                        {
                            m_thread_Upload = new Thread(selectFile);
                        }
                        m_thread_Upload.Priority = ThreadPriority.AboveNormal;
                        m_thread_Upload.IsBackground = true;
                        m_thread_Upload.Start();
                    }
                    m_inUploading = true;
                    button1.BackColor = Color.Green;
                    button1.Text = "结束上传";
                }
                else
                {
                    if ((m_thread_Upload != null) && m_thread_Upload.IsAlive)
                    { m_thread_Upload.Abort(); }
                    button1.BackColor = SystemColors.Control;
                    m_inUploading = false;
                    button1.Text = "开始上传";
                }
            }
            catch { }
        }
        private void selectFile(object obj)
        {
            for (; ; )
            {
                try
                {
                    Thread.Sleep(500);
                    FileInfo[] FileinfoList = new DirectoryInfo(GlobalVar.gl_Directory_savePath).GetFiles("*.ini");
                    if (FileinfoList.Length == 0)
                    {
                        showStatus("沒有需要上传的數據文件");
                        continue;
                    }
                    else
                    {
                        for(int j = 0; j< FileinfoList.Length; j++)
                        {
                            m_SheetBarcode = "";
                            m_list_info.Clear();
                            m_iniFullName = FileinfoList[j].FullName;
                            showStatus("數據文件" + m_iniFullName + "解析中......");
                            logWR.appendNewLogMessage("數據文件" + m_iniFullName + "解析中......");
                            string filename = Path.GetFileNameWithoutExtension(m_iniFullName);
                            m_SheetBarcode = filename.Substring(0, filename.IndexOf("_"));
                            if (m_SheetBarcode == "")
                            {
                                logWR.appendNewLogMessage("Sheet条码为空！");
                                try { File.Delete(m_iniFullName); }
                                catch { }
                                continue; 
                            }
                            StringBuilder str_tmp = new StringBuilder(500);
                            //数据库连接字串
                            GetPrivateProfileString(GlobalVar.gl_iniSection_Result, GlobalVar.gl_iniKey_ConnStr, "", str_tmp, 500, m_iniFullName);
                            m_str_Connstr = str_tmp.ToString().Trim();
                            m_str_Connstr += "uid=suzmektec;pwd=suzmek;Connect Timeout=5";
                            if (m_str_Connstr.Trim() == "")
                            {
                                logWR.appendNewLogMessage("数据库连接字符串为空!");
                                continue;
                            }
                            //获取flowid
                            int _flowid = 0; //如果出错，数据库中记录的就会是0
                            GetPrivateProfileString(GlobalVar.gl_iniSection_Result, GlobalVar.gl_iniKey_FlowID, "", str_tmp, 500, m_iniFullName);

                            if (str_tmp.ToString().Trim().Length > 0)
                            {
                                _flowid = Convert.ToInt32(str_tmp.ToString().Trim());
                            }
                            FileStream FS = new FileStream(m_iniFullName, FileMode.Open);
                            StreamReader sr = new StreamReader(FS);
                            string totalstr = sr.ReadToEnd();
                            string[] resultList = totalstr.Split('\r');
                            for (int i = 1; i <= resultList.Length - 1; i++)
                            {
                                string result = resultList[i].Trim('\r').Trim('\n').Trim();
                                if (result.IndexOf("=") < 0) { continue; }  //必须要有=
                                string posstr = result.Substring(0, result.IndexOf("="));  //位置号
                                string Barcode = result.Substring(result.IndexOf("=") + 1);

                                if (posstr.Length == 0) { continue; } //0
                                if (posstr.Length > 3) { continue; } //不会超过1000
                                if (!myfunc.checkStringIsLegal(posstr, 1)) { continue; }  //必须是数字字符

                                pcsinfo pi = new pcsinfo();
                                pi._flowID = _flowid;
                                pi._posNum = posstr;
                                GetPrivateProfileString(GlobalVar.gl_iniSection_Result, posstr, "", str_tmp, 50, m_iniFullName);
                                pi._MicBarcode = str_tmp.ToString().Trim();
                                m_list_info.Add(pi);
                            }
                            FS.Close();
                            StartDBQueryAndSave();
                            logWR.appendNewLogMessage("數據文件" + m_iniFullName + "上传完畢，等待备份完成");
                            string path = GlobalVar.gl_path_FileBackUp + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
                            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                            File.Copy(m_iniFullName, path + Path.GetFileName(m_iniFullName), true);
                            logWR.appendNewLogMessage("数据备份完成，等待删除文件完成");
                            File.Delete(m_iniFullName);
                            logWR.appendNewLogMessage("數據文件" + m_iniFullName + "已删除！");
                            showStatus("數據文件" + m_iniFullName + "解析完畢！");
                        }
                    }
                    //Thread.Sleep(500);  //等待数据存储完毕
                }
                catch (Exception ex)
                {
                    logWR.appendNewLogMessage("上传文件数据异常:" +ex.ToString());
                    updateLedLightStatus(2);
                    ShowPsdErrForm form = new ShowPsdErrForm("上传文件数据异常!", true);
                    form.ShowDialog();
                    updateLedLightStatus(0);
                }
            }
        }

        private void showStatus(string str)
        {
            try
            {
                this.BeginInvoke(new Action(() =>
                {
                    toolStripStatusLabel_uploadInfo.Text = str;
                }));
            }catch(Exception ex)
            {
                logWR.appendNewLogMessage("刷新界面 异常:"+ex.ToString());
            }
        }

        private void StartDBQueryAndSave()
        {
            logWR.appendNewLogMessage("开始上传数据");
            DBQuery query = new DBQuery(m_str_Connstr);

            for (int i = 0; i < m_list_info.Count; i++)
            {
                if (m_list_info[i] != null)
                {
                    int result;
                    if (m_list_info[i]._flowID == 5 && m_list_info[i]._MicBarcode != "")
                    {
                        query = new DBQuery(GlobalVar.gl_str_Product, GlobalVar.gl_str_LotNo);
                        GlobalVar.gl_strShtBarcode = m_SheetBarcode;
                        result = query.FPCBarcodeLink(m_list_info[i]._MicBarcode, Convert.ToInt32(m_list_info[i]._posNum));
                    }
                    else if (m_list_info[i]._MicBarcode != "")
                    {
                        switch (GlobalVar.gl_ProductModel)
                        {
                            case "A41SENSOR":
                                if (GlobalVar.gl_LinkType == LinkType.PROX)
                                    result = query.PROXBarcodeLinkSensor(m_SheetBarcode, m_list_info[i]._MicBarcode, m_list_info[i]._posNum);
                                else if (GlobalVar.gl_LinkType == LinkType.MIC)
                                    result = query.MICBarcodeLinkSensor(m_SheetBarcode, m_list_info[i]._MicBarcode, m_list_info[i]._posNum);
                                break;
                            case "A42SENSOR":
                                if (GlobalVar.gl_LinkType == LinkType.PROX)
                                    result = query.PROXBarcodeLinkSensor(m_SheetBarcode, m_list_info[i]._MicBarcode, m_list_info[i]._posNum);
                                else if (GlobalVar.gl_LinkType == LinkType.MIC)
                                    result = query.MICBarcodeLinkSensor(m_SheetBarcode, m_list_info[i]._MicBarcode, m_list_info[i]._posNum);
                                break;
                            default:
                                result = query.MICBarcodeLink(m_SheetBarcode, m_list_info[i]._MicBarcode, m_list_info[i]._posNum, m_list_info[i]._flowID);
                                if (result == 7)
                                {
                                    ShowPsdErrForm err = new ShowPsdErrForm("已存在关联记录,禁止使用!", false);
                                    err.ShowDialog();
                                    return;
                                }
                                break;
                        }
                    }
                }
            }
            logWR.appendNewLogMessage("上传数据完成");
        }

        class pcsinfo
        {
            public bool _isValidPos;
            public string _posNum;
            public string _MicBarcode;
            public string _ProductName; //机种
            public int _flowID; //MIC和Prox的FlowID
        }
        #endregion

        private void toolStripButtonStop_Click(object sender, EventArgs e)
        {

        }

        public void setRefPointValue(string x, string y)
        {
            textBox_fixPoint_x.Text = x;
            textBox_fixPoint_y.Text = y;
            textBox_fixPoint_z.Text = "0.0";
        }
        public void SetCalPosValue(SPoint sp)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    textBox_CalPos_X.Text = sp.Pos_X.ToString("000.000");
                    textBox_CalPos_Y.Text = sp.Pos_Y.ToString("000.000");
                }));
                myfunc.WriteCalPositionInfoToTBS();
            }
            catch { }
        }

        private void button_CalPosMove_Click(object sender, EventArgs e)
        {
            float Pos_X = float.Parse(textBox_CalPos_X.Text);
            float Pos_Y = float.Parse(textBox_CalPos_Y.Text);
            GlobalVar.gl_point_CalPos.Pos_X = Pos_X;
            GlobalVar.gl_point_CalPos.Pos_Y = Pos_Y;
            //m_obj_dwg_eve_sendCalPosition(GlobalVar.gl_Ref_Point_CADPos.Pos_X - Pos_X, GlobalVar.gl_Ref_Point_CADPos.Pos_Y - Pos_Y);
            myfunc.WriteCalPositionInfoToTBS();
            Thread thd = new Thread(new ThreadStart(delegate
            {
                if (m_form_movecontrol.CheckAxisInMoving())
                {
                    MessageBox.Show("设备(轴)为Not Ready状态，请停止后检查再作業！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                float dis_X = (GlobalVar.gl_Ref_Point_CADPos.Pos_X - (Pos_X)) * -1; //机械原点在左上,X坐标需要取反
                float dis_Y = GlobalVar.gl_Ref_Point_CADPos.Pos_Y - (Pos_Y);
                float x = dis_X + GlobalVar.gl_value_CalibrateDis_X + dis_Y * GlobalVar.gl_value_CalibrateRatio_X;
                float y = dis_Y + GlobalVar.gl_value_CalibrateDis_Y + dis_X * GlobalVar.gl_value_CalibrateRatio_Y;
                m_form_movecontrol.FixPointMotion(x, y, 3);
            }));
            thd.IsBackground = true;
            thd.Start();
        }

        private void btn_safetyDoorDispose_Click(object sender, EventArgs e)
        {
            m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_U, 7, 0); //前门释放
        }

        private void btn_safetyDoorEnable_Click(object sender, EventArgs e)
        {
            m_form_movecontrol.SetDO(m_form_movecontrol.m_AxisNum_U, 7, 1); //前门上锁
        }

        private void button_RtnRefOrgPoint_Click(object sender, EventArgs e)
        {
            //m_obj_dwg.button_RtnRefOrgPoint_Click(sender, e);    //回到参考原点

            try
            {
                SPoint sp = new SPoint();
                //sp.Pos_X = float.Parse(comboBox_fixPoint_x.Text + textBox_fixPoint_x.Text);
                //sp.Pos_Y = float.Parse(comboBox_fixPoint_y.Text + textBox_fixPoint_y.Text);
                //sp.Pos_Z = float.Parse(comboBox_fixPoint_z.Text + textBox_fixPoint_z.Text);
                sp.Pos_X = float.Parse(textBox_fixPoint_x.Text);
                sp.Pos_Y = float.Parse(textBox_fixPoint_y.Text);
                sp.Pos_Z = float.Parse(textBox_fixPoint_z.Text);
                m_obj_dwg_eve_sendReFPoint(sp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        private void txtbox_DeviceID_TextChanged(object sender, EventArgs e)
        {
            if (txtbox_DeviceID.Text == "")
                return;
            else
                GlobalVar.gl_DeviceID = txtbox_DeviceID.Text.Trim();
        }

        private void rdb_LocalLoading_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_LocalLoading.Checked)
            {
                GlobalVar.gl_AutoLoadType = false;
            }
            else
            {
                GlobalVar.gl_AutoLoadType = true;
            }

        }

        private void button_RefOrgPoint_Click(object sender, EventArgs e)
        {
            returnreferecePoint();
        }



        private void button_CCDTrigger_Click(object sender, EventArgs e)
        {
            CaptureTrigger();
        }

        private void CaptureTrigger()
        {
            Thread.Sleep(60); //等待到位后稳定
            m_form_movecontrol.SetDO(m_AxisNum_X, 4, 1);
            Thread.Sleep(2);
            m_form_movecontrol.SetDO(m_AxisNum_X, 4, 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            #region Z/U轴DO
            byte byte_Z = 0;
            byte byte_U = 0;
            Motion.mAcm_AxDoGetBit(m_Axishand[m_AxisNum_Z], 6, ref byte_Z);
            pictureBox_ledRed.BackColor = (byte_Z == 0) ? Color.Gray : Color.Red;
            button_LedRed.Text = (byte_Z == 0) ? "ON" : "OFF";
            Motion.mAcm_AxDoGetBit(m_Axishand[m_AxisNum_U], 6, ref byte_U);
            pictureBox_ledWhite.BackColor = (byte_U == 0) ? Color.Gray : Color.Red;
            button_LedWhite.Text = (byte_U == 0) ? "ON" : "OFF";
            #endregion

            #region U/Z轴DI
            byte BitIn_Z = 0;
            //Z轴IN0
            UInt32 Result_ZU = Motion.mAcm_AxDiGetBit(m_Axishand[m_AxisNum_Z], 0, ref BitIn_Z);
            if (Result_ZU == (uint)ErrorCode.SUCCESS)
            {
                if (BitIn_Z == 1)
                {
                    //通知到达条码扫描位置
                    pictureBox_ShtSNScan.BackColor = Color.Red;
                }
                else
                {
                    pictureBox_ShtSNScan.BackColor = Color.Gray;
                }
            }
            #endregion

            #region 读取LED I/O光源信息
            byte byte_Power = 0;
            uint Result_red = Motion.mAcm_AxDoGetBit(m_Axishand[m_AxisNum_Z], (ushort)2, ref byte_Power);
            if (Result_red == (uint)ErrorCode.SUCCESS)
            {
                if (byte_Power == 1)
                {
                    pictureBox_ledRed.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    pictureBox_ledRed.BackColor = System.Drawing.Color.Gray;
                }
            }
            uint Result_blue = Motion.mAcm_AxDoGetBit(m_Axishand[m_AxisNum_U], (ushort)2, ref byte_Power);
            if (Result_blue == (uint)ErrorCode.SUCCESS)
            {
                if (byte_Power == 1)
                {
                    pictureBox_ledWhite.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    pictureBox_ledWhite.BackColor = System.Drawing.Color.Gray;
                }
            }
            #endregion
        }

        private void button_setRefPoint_Click(object sender, EventArgs e)
        {

            try
            {
                SPoint sp = new SPoint();
                //sp.Pos_X = float.Parse(comboBox_fixPoint_x.Text + textBox_fixPoint_x.Text);
                //sp.Pos_Y = float.Parse(comboBox_fixPoint_y.Text + textBox_fixPoint_y.Text);
                //sp.Pos_Z = float.Parse(comboBox_fixPoint_z.Text + textBox_fixPoint_z.Text);
                sp.Pos_X = float.Parse(textBox_fixPoint_x.Text);
                sp.Pos_Y = float.Parse(textBox_fixPoint_y.Text);
                sp.Pos_Z = float.Parse(textBox_fixPoint_z.Text);
                m_obj_dwg_eve_sendReFPoint(sp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button_LedRedOff_Click(object sender, EventArgs e)
        {
            button_LedRedOff.Enabled = false;
            button_LedRed.BackColor = Color.SteelBlue;
            byte value = (button_LedRedOff.Text.ToUpper() == "ON") ? (byte)1 : (byte)0;
            button_LedRedOff.Text = "";
            uint result = m_form_movecontrol.SetDO(m_AxisNum_Z, 6, value);
            button_LedRed.Enabled = true;
            button_LedRedOff.BackColor = Color.Gray;
            button_LedRed.Text = "ON";
        }

        private void button_WhiteRedOff_Click(object sender, EventArgs e)
        {
            button_LedWhiteOff.Enabled = false;
            button_LedWhite.BackColor = Color.SteelBlue;
            byte value = (button_LedWhiteOff.Text.ToUpper() == "ON") ? (byte)1 : (byte)0;
            button_LedWhiteOff.Text = "";
            uint result = m_form_movecontrol.SetDO(m_AxisNum_U, 6, value);
            button_LedWhite.Enabled = true;
            button_LedWhiteOff.BackColor = Color.Gray;
            button_LedWhite.Text = "ON";
        }

        private void button_LedRed_Click(object sender, EventArgs e)
        {
            button_LedRedOff.Enabled = true;
            button_LedRedOff.BackColor = Color.SteelBlue;
            byte value = (button_LedRed.Text.ToUpper() == "ON") ? (byte)1 : (byte)0;
            button_LedRed.Text = "";
            uint result = m_form_movecontrol.SetDO(m_AxisNum_Z, 6, value);
            button_LedRed.Enabled = false;
            button_LedRed.BackColor = Color.Red;
            button_LedRedOff.Text = "OFF";
        }

        private void button_LedWhite_Click(object sender, EventArgs e)
        {
            button_LedWhiteOff.Enabled = true;
            button_LedWhiteOff.BackColor = Color.SteelBlue;
            byte value = (button_LedWhite.Text.ToUpper() == "ON") ? (byte)1 : (byte)0;
            button_LedWhite.Text = "";
            uint result = m_form_movecontrol.SetDO(m_AxisNum_U, 6, value);
            button_LedWhite.Enabled = false;
            button_LedWhite.BackColor = Color.Red;
            button_LedWhiteOff.Text = "OFF";
        }

        private void rdb_NetLoading_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_LocalLoading.Checked)
            {
                GlobalVar.gl_AutoLoadType = true;
            }
            else
            {
                GlobalVar.gl_AutoLoadType = false;
            }
        }

        private void richTextBox_SingleShow_TextChanged(object sender, EventArgs e)
        {
        }

        public void addLogStr(string Msg)
        {
            logWR.appendNewLogMessage(Msg);
            //richTextBox_SingleShow.AppendText(Msg);
        }


        private void autoDeleteOldPic()
        {
            try
            {
                if (checkHardDiskSpace("D") >= 0.8)
                {
                    if (GlobalVar.gl_PicsSavePath.Trim() == "") { return; }
                    DirectoryInfo DI = new DirectoryInfo(GlobalVar.gl_PicsSavePath);
                    foreach (DirectoryInfo sub_DI in DI.GetDirectories())
                    {
                        TimeSpan ts = DateTime.Now - sub_DI.CreationTime;
                        if (ts.TotalHours >= 48 && checkHardDiskSpace("D") >= 0.5)
                        {
                            Directory.Delete(sub_DI.FullName, true);
                        }
                    }
                    if (GlobalVar.gl_NGPicsSavePath.Trim() == "") { return; }
                    DirectoryInfo DII = new DirectoryInfo(GlobalVar.gl_NGPicsSavePath);
                    foreach (DirectoryInfo sub_DI in DII.GetDirectories())
                    {
                        TimeSpan ts = DateTime.Now - sub_DI.CreationTime;
                        if (ts.TotalHours >= 48 && checkHardDiskSpace("D") >= 0.5)
                        {
                            Directory.Delete(sub_DI.FullName, true);
                        }
                    }
                    foreach (FileInfo sub_DI in DII.GetFiles())
                    {
                        TimeSpan ts = DateTime.Now - sub_DI.CreationTime;
                        if (ts.TotalHours >= 48 && checkHardDiskSpace("D") >= 0.5)
                        {
                            Directory.Delete(sub_DI.FullName, true);
                        }
                    }
                    MessageBox.Show("文件刪除完畢！");
                }
            }
            catch
            {
                MessageBox.Show("文件刪除失敗，請進行手動刪除！");
            }
        }

        /// <summary>
        /// 获取磁盘已用空间比例
        /// </summary>
        /// <param name="str_HardDiskName"></param>
        /// <returns></returns>
        public static double checkHardDiskSpace(string str_HardDiskName)
        {
            double freeSpace = 0;
            double totalSize = 0;
            double usedSize = 0;
            double usedPercent = 0;
            str_HardDiskName = str_HardDiskName + ":\\";
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (System.IO.DriveInfo drive in drives)
            {
                if (drive.Name == str_HardDiskName)
                {
                    freeSpace = drive.TotalFreeSpace / (1024 * 1024 * 1024);
                    totalSize = drive.TotalSize / (1024 * 1024 * 1024);
                    usedSize = totalSize - freeSpace;
                    usedPercent = usedSize / totalSize;
                }
            }
            return usedPercent;
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            GlobalVar.m_ScanAuthorized = true;
            test_startScanFunction();
        }

        private void test_startScanFunction()
        {
            try
            {
                timetest = DateTime.Now;
                OneCircleReset();
                //checkBlocksIsValid();  //--移动到独立窗口进行查询
                test_CalibrateAction();
            }
            catch (Exception e)
            {
                logWR.appendNewLogMessage("启动扫描进程异常 startScanFunction Error:  \r\n " + e.ToString());
            }

        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string m_str = "Data Source=SUZSQLV01;database=A82TFLEX;uid=suzmektec;pwd=suzmek;Connect Timeout=5";
            DBQuery query = new DBQuery(m_str);
            int result = query.MICBarcodeLink("TESTsheet1", "TESTBarcode1", "1", 21);
        }
    }

    class ScanbarcodeInfo
    {
        public ScanbarcodeInfo() { }
        public string barcode = "";
        public List<int> NGPositionlist = new List<int>();
        public string LotNo = "";
        public bool LotResult = true;
        public string ErrMsg_Lot = "";
        public bool MICResult = true;
        public string ErrMsg_MIC = "";
        public void Clear()
        {
            try
            {
                barcode = "";
                NGPositionlist = new List<int>();
                LotNo = "";
                LotResult = true;
                ErrMsg_Lot = "";
                MICResult = true;
                ErrMsg_MIC = "";
            }
            catch { }
        }
        public void Clone(ScanbarcodeInfo info)
        {
            barcode = info.barcode;
            NGPositionlist.Clear();
            if (info.NGPositionlist.Count > 0)
            {
                NGPositionlist.AddRange(info.NGPositionlist);
            }
            LotNo = info.LotNo;
            LotResult = info.LotResult;
            ErrMsg_Lot = info.ErrMsg_Lot;
        }
    }

    #region CAD点阵信息相关
    public class SPoint : Object
    {
        public int FlowID;  //从DWG中获取
        public string Point_name;
        public int PointNumber; //值保持為最新的順序號
        public double Angle_deflection = 0;   //旋轉角度，保持即時更新
        public int Line_sequence = 0;   //条码队列顺序 -- 0：正向 1：反向
        public float Pos_X = 0.0F;
        public float Pos_Y = 0.0F;
        public float Pos_Z = 0.0F;

        public void CopyPoint(SPoint sp)
        {
            this.Pos_X = sp.Pos_X;
            this.Pos_Y = sp.Pos_Y;
            this.Pos_Z = sp.Pos_Z;
        }
    }

    public class ZhiPinInfo
    {
        public string _SubName;   //ST,KNOWLES...
        public string _HeadStr;      //FFC, FF9....
        public int _BarcodeLength;
        public int _StartPos;
        public int _StartLen;  //
    }

    //一个OnePointGroup == 一个FlowID的cycle，DOCK有两个MIC就有两个CYCLE
    public class OnePointGroup
    {
        public int FlowID;   //FLOWID
        public List<SPoint> m_ListGroup = new List<SPoint>();
        public List<ZhiPinInfo> m_list_zhipingInfo = new List<ZhiPinInfo>();  //当前组用的制品，用于切换光源
        public OneGroup_Blocks m_BlockList_ByGroup = new OneGroup_Blocks();  //当前组的BLOCK集合        
    }

    //CAD点阵集合信息
    public class PointInfo
    {
        public List<OnePointGroup> m_List_PointInfo = new List<OnePointGroup>();
        public void addPoint(SPoint sp)
        {
            for (int i = 0; i < m_List_PointInfo.Count; i++)
            {
                if (m_List_PointInfo[i].FlowID == sp.FlowID)
                {
                    m_List_PointInfo[i].m_ListGroup.Add(sp); //一样Flowid的点放在一组,一个Flowid就只有一个m_List_PointInfo
                    return;
                }
            }
            //如果到这里没有被return，说明m_List_PointInfo中尚没有这个FLOWID的集合
            OnePointGroup newgroup = new OnePointGroup();
            newgroup.FlowID = sp.FlowID;
            newgroup.m_ListGroup.Add(sp);
            m_List_PointInfo.Add(newgroup);
        }

        public void clearList()
        {
            m_List_PointInfo.Clear();
        }

        public void Sort()
        {
            for (int i = 0; i < m_List_PointInfo.Count; i++)
            {
                m_List_PointInfo[i].m_ListGroup.Sort(new SPointCompare_byTipSequence());
            }
        }
    }

    public class OneGroup_Blocks
    {
        public int FlowID;
        public List<DetailBlock> m_BlockinfoList = new List<DetailBlock>();
        public bool m_DecodeFinished = false;
        public void add(DetailBlock block)
        {
            m_BlockinfoList.Add(block);
        }

        //逐個順序解析BLOCK中的条码
        public void CycleDecodeAllBlocks()
        {
            Thread threaddecode = new Thread(new ThreadStart(delegate
            {
                for (; ; )
                {
                    try
                    {
                        m_DecodeFinished = true;
                        for (int i = 0; i < m_BlockinfoList.Count; i++)
                        {
                            if (GlobalVar.gl_inEmergence) { break; }
                            if (!GlobalVar.gl_safetyDoor_Front && GlobalVar.gl_usermode == 0) break;
                            if ((!m_BlockinfoList[i].m_decodeFinished)
                                && (m_BlockinfoList[i].m_receivedPics))
                            {
                                m_BlockinfoList[i].backthread_decode_Halcon(FlowID);
                            }
                            m_DecodeFinished &= m_BlockinfoList[i].m_decodeFinished;
                        }
                        if (m_DecodeFinished) { break; }
                        if (GlobalVar.gl_inEmergence) { break; }
                        if (!GlobalVar.gl_safetyDoor_Front && GlobalVar.gl_usermode == 0) break;
                        Thread.Sleep(100);
                    }
                    catch (Exception e)
                    {
                        string str = "逐個順序解析BLOCK中的条码(CycleDecodeAllBlocks)过程出错; \r\n" + e.ToString();
                        logWR.appendNewLogMessage(str);
                        MessageBox.Show(str);
                    }
                }
                m_DecodeFinished = true;  //其实是多于的，只是为了流程看起来明了
                clearALLHalcomMemory();
            }));
            threaddecode.IsBackground = true;
            threaddecode.Start();
        }

        private void clearALLHalcomMemory()
        {
            return;
            for (int i = 0; i < m_BlockinfoList.Count; i++)
            {
                try
                {
                    m_BlockinfoList[i].gl_halcon.releaseMemory();
                }
                catch (Exception e)
                {
                    string str = "清理HALCON内存(clearALLHalcomMemory)过程出错; \r\n" + e.ToString();
                    logWR.appendNewLogMessage(str);
                    MessageBox.Show(str);
                }
            }
        }
    }
    #endregion

    #region BLOCK显示、解析相关
    public class BlockInfo
    {
        public float Pos_X;
        public float Pos_Y;
        public float Pos_Z;

        public Bitmap _bmp;
        public int _Width;
        public int _Height;
    }
    #endregion
    public class BitmapInfo
    {
        public int FlowID;
        public Bitmap bitmap = new Bitmap(640, 480);
        public int num;
        //public bool m_inProcessing = false;   //是否正在处理中
        public bool m_processed = false;  //是否已經被處理
    }

    public class IPInfo
    {
        public IPInfo(string IP, string MAC)
        {
            _IP = IP;
            _MAC = MAC;
        }
        public string _IP;
        public string _MAC;
        public int _WorkPort;
    }

}