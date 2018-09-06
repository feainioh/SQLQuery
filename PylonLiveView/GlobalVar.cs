using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using MainSpace;
using MainSpace.CustomPubClass;
//using PylonC.NETSupportLibrary;

namespace PylonLiveView
{
    public partial class GlobalVar
    {
        public static bool m_ScanAuthorized = false; //允许作业
        public static bool gl_Board1245EInit = false; //板卡初始化
        public static float gl_PixelDistance = 100.0f; //1个脉冲走0.01mm
        public static float gl_PixelDistanceZ = 1000.0f; //1个脉冲走0.001mm
        public static string gl_strDefaultODBC = "EBSFLIB"; //默认ODBC连接
        public static LinkType gl_LinkType = LinkType.MIC;
        public static LinkType gl_LastLinkType = LinkType.DEFAULT; //上次关联类型
        public static double gl_dAxisZRef = 0.0; //Z轴参考点
        public const string ProgramName = "MICBarcodeLink";  //程序文件名称，用作版本查询
        public static int gl_usermode = 0;      //0:常规模式  1:admin模式
        public static int gl_FlowID = 7;   //7&10 --MIC    6--PROX  PS:目前没有用处2017.04.24   5--PCS BARCODE
        public static int gl_CurrentFlowID;  //当前运动拍照的FLOWID
        public static bool gl_bUseHalcon = false;  //使用Halcon解析
        public static bool gl_bUseLargeBoardSize = false;//使用大载板
        public static bool gl_bMPNPlan = false;    //是否为品目合并
        public static string gl_strMpnAssemble = "";//品目合并方案组合规则(19K07-20K10-21J00)2016.06
        public static string gl_strAssemble = "";    //品目合并方案组合(KKJ)2016.06
        public static string gl_strAssembleX = ""; //品目合并方案组合E75用*表示（KK*）2016.08
        public static List<MPNPlan> gl_listMPNPlan = new List<MPNPlan>(); //品目合并方案2016.06
       
        public static PylonC.NETSupportLibrary.ImageProvider gl_imageProvider = new PylonC.NETSupportLibrary.ImageProvider(); /* Create one image provider. */
        //全局变量
        //public static bool gl_workMode = true;   //工作模式 true：正常工作模式  false: 通过模式
        public static string gl_ProductModel = ""; //部品名称 A21DOCK/A31SENSOR/...
        public static string gl_logFileName = "";  //log记录文档,记录开机自检的错误信息与测试过程中的致命信息
        public static bool gl_inEmergence = false;  //是否在紧急状态，如果是这个状态，所有动作退出
        public static bool gl_safetyDoor_Front = false;  //安全锁是否关闭，否则轴不能运动
        //public static bool gl_safetyDoor_Back = false;  //安全锁是否关闭，否则轴不能运动
        public static string gl_DeviceID = "";     //设备编号
        public static string gl_LineName = "";      //线别
        public static string gl_Link = "";//关联类型
        public static string gl_netPath = @"\\192.168.208.237\share" + "\\SystemFile\\SoftUpdate\\SANTEC\\NETWORK\\";       //网络路径
        public static string gl_appName = "条码照合关联程序3型";  //当前程序名称 
        public static List<string> listProductType = new List<string>();

        public const string gl_password_confirm = "SANTEC1234";
        public const string gl_folderName_MCH = "MCH";
        public const string gl_folderName_Mapfiles = "MAPFILES";
        public const string gl_LifeCam_DeviceID = "@device:pnp:\\\\?\\usb#vid_045e&pid_076d&mi_00";  //HD-5000 deviceID名称
        public static string gl_Directory_savePath = Application.StartupPath + "\\ResultFiles";   //数据结果存储位置
        public static List<Bitmap> gl_captureBmpList = new List<Bitmap>();
        public static string gl_XML_SplitPicMessage = "LocationInfo"; //按Tabpage的index附加后缀，比如LocationInfo1.xml / Location2.xml
        public static PointInfo gl_List_PointInfo = new PointInfo();  //所有点阵信息类型
        //public static List<ZhiPinInfo> gl_list_ZhiPinInfo = new List<ZhiPinInfo>(); //MIC料信息，可能有两个料
        
        //public static List<DetailBlock> gl_List_BlockInfo = new List<DetailBlock>();
        public static int gl_totalCount = 0;
        public static float gl_value_CalibrateDis_X = 0.0F;    //計算載板原點校準值_X
        public static float gl_value_CalibrateDis_Y = 0.0F;    //計算載板原點校準值_Y
        public static float gl_value_CalibrateRatio_X = 0.0F;    //計算載板角度  X方向斜率
        public static float gl_value_CalibrateRatio_Y = 0.0F;    //計算載板角度  Y方向斜率
        public static float gl_value_MarkPointDiameter = 1.0f;    //TM作为MARK点 宽度，用于计算偏移

        //-----------------------主要窗体句柄与消息--------------------------------------
        public static IntPtr gl_IntPtr_MainWindow;         //主窗体句柄值
        public static IntPtr gl_IntPtr_ObjDWGDirect;
        public const int WM_FixedMotion = 0x0400 + 1;     //定點運動

        //-------------------定義自定義的實體所增加圖層名稱-------------------------------------------------
        public const string gl_str_TipPoint = "TIPPOINT";
        public const string gl_str_RefPoint = "REFPOINT";
        public const string gl_str_MARKPoint = "MARKPOINT";   //第二个MARK点
        public const string gl_str_ScrrenRefPoint = "SCRRENREFPOINT";      //CAD图纸左上方点块名称--相对应PC屏幕参考原点
        public const string gl_layer_RefPoint = "layer_RefCircle";
        public const string gl_layer_RunPathLayer = "RunPathLayer";
        public const string gl_str_CalibPoint_X = "CALIBRATIONPOINT_X";
        public const string gl_str_CalibPoint_Y = "CALIBRATIONPOINT_Y";

        //----------------测试信息-----------------------
        public static object gl_strPCName = "SANTECPC";//
        public static string gl_DataBaseConnectString = "";   //数据库字符串，初始数据库时赋值
        public static string gl_str_QualifiedNo = ""; //合格票
        public static string EEEE = "";  //工程代码
        public static string gl_str_Product = "";    //品目
        public static string gl_str_LotNo = "";  //LotNo
        public static string gl_str_OP = "";     //作業員
        public static int gl_int_QualifiedTotal = 0;  //合格票号总数
        public static string gl_str_NullMark = "";    //MARK为非空时，取消LOT合法性检查
        public static string gl_str_MICHeadStr = "";  //用作判断MIC部品是否正确，条码前3位
        public static string gl_str_MICType = "";  //MIC廠商: AAC/ST/..
        public static int gl_MICPos_StartPosition = 0;  //MIC条码中标识符的起始位置
        public static int gl_MICPos_Length = 0;  //MIC条码中标识符的长度

        public static int gl_testinfo_totalSheet = 0;
        public static int gl_testinfo_totalTest = 0;
        public static int gl_testinfo_decodefailed = 0;

        //-------------參數設置變量-------------
        public static bool gl_saveCapturePics = false;  //是否存储图片
        public static bool gl_saveDecodeFailPics = false;  //是否存儲解析失敗圖片
        public static string gl_PicsSavePath = "";    //拍照图片备份
        public static string gl_NGPicsSavePath = "";  //解析失败备份
        public static bool gl_AutoLoadType = true; //自动加载
        //public static string gl_path_FileResult = "\resultFile";   //結果文件存儲、上傳路徑
        public static string gl_path_FileBackUp = "";   //結果文件上傳后備份路徑

        public static IPInfo gl_MasterIPInfo=new IPInfo("","");
        public const int gl_max_grips = 20;  //允許CAD顯示最大的夾點數目為20個
        public static int gl_decode_timeout = 3000000;
        public static int gl_MinMatchScore = 80;  //最低匹配度
        public static int gl_decode_times = 4;    //重复解析次数
        public static int gl_OneSheetCount = 60;
        public static int gl_length_PCSBarcodeLength = 17;    //条码长度
        public static int gl_length_sheetBarcodeLength = 17;     //sheetno 長度
        public static int gl_tabpages = 4;  
        public static int gl_userLevel = 1; //使用者等级 0:user 1:junior 2:senior
        public static SPoint gl_Ref_Point_CADPos = new SPoint();   //CAD图纸中相对参考点原点
        public static SPoint gl_Ref_Point_Axis = new SPoint();      //机械轴运动原点--参考原点
        public static SPoint gl_point_CalPos = new SPoint();       //用作校準參考點
        public static SPoint gl_point_CalPosRef = new SPoint();    //校準參考點，调试显示图纸原始Mark点用
        public static SPoint gl_point_ScrrenRefPoint = new SPoint();
        //public static SPoint gl_point_CalibrationPoint_X = new SPoint();
        //public static SPoint gl_point_CalibrationPoint_Y = new SPoint();
        public static string gl_serialPort_Scan = "COM10";  //sheet扫描串口
        public static int gl_PosLimit_X_P = 30000;  //X轴正向软件限位
        public static int gl_PosLimit_X_N = -3000;  //X轴负向软件限位
        public static int gl_PosLimit_Y_P = 30000;  //Y轴正向软件限位
        public static int gl_PosLimit_Y_N = -3000;  //Y轴负向软件限位
        public static float gl_OnePulseDistance = 0.01f;   //一个脉冲0.01mm 

        public static int block_width = 70;   //BlockDetail尺寸
        public static int block_heigt = 120;
        public static int firstBlockLocation = 0; //第一排一个BlockDetail纵坐标
        public static int firstBlockCount = 0; //第一排一个BlockDetail总数
        public static string gl_matchFileName = Application.StartupPath + "\\MCH\\MATCH.MCH";
        //------------------光源参数----------------------
        public static int gl_exposure_Mark_default = 1500;
        public static int gl_exposure_Matrix_default = 3000;
        public static int gl_exposure_Mark_Geortek = 1500;
        public static int gl_exposure_Matrix_Geortek = 3000;
        public static int gl_exposure_Mark_ST = 1500;
        public static int gl_exposure_Matrix_ST = 3000;
        public static int gl_exposure_Mark_AAC = 1500;
        public static int gl_exposure_Matrix_AAC = 3000;
        public static int gl_exposure_Mark_Knowles = 1500;
        public static int gl_exposure_Matrix_Knowles = 3000;

        //------------------光源参数NEW----------------------
        public static string gl_Model_prodcutTypeMic = ""; //mic厂家类型
        public static string gl_Model_prodcutTypeProx = ""; //Prox厂家类型
        public static int gl_Model_exposure = 3500; //MIC相机曝光值
        public static int gl_Model_exposureProx = 3500; //PROX相机曝光值
        public static int gl_Model_exposurePcs = 3500;//PCS相机曝光值
        public static int gl_Model_exposureIC = 3500;//IC曝光值
        public static int gl_Model_redLight = 0;   //0: 灭 1：亮
        public static int gl_Model_whiteLight = 0; //0: 灭 1：亮


        //-------------------自定義工作區面板尺寸(默認400*400) ---主要用於BlockInfo顯示---------
        public static int gl_workArea_width = 280;
        public static int gl_workArea_height = 150;

        //-------------------设置网络相机参数--------------------------
        public static int gl_paras_basler_Exposure_Scan = 3000;
        public static int gl_paras_basler_Exposure_Calibrate = 1300;

        //-----------CONFIG.INI / EXPOSURE.INI 保存参数名称------------------------------
        #region CONFIG.INI
        public const string gl_iniTBS_FileName = "CONFIG.INI"; 
        public const string gl_iniSection_mapping = "MAP";  //映射块Section
        public const string gl_iniKey_ConnStr = "ConnStr";  //ini结果中存储机种SQL连接字符串
        public const string gl_iniKey_FlowID = "FlowID";  //ini结果中存储机种SQL连接字符串

        public const string gl_inisection_Global = "Global";
        public const string gl_iniKey_SheetCount = "SheetCount";           //整盤數量
        public const string gl_iniKey_BarcodeLength = "BarcodeLength";       //條碼長度
        public const string gl_iniKey_SheetBarcodeLength = "SheetBarcodeLength";       //條碼長度
        public const string gl_iniKey_MarkPointDiameter = "MarkPointDiameter";
        public const string gl_iniKey_TimeOut = "TimeOut";           //解析时长
        public const string gl_iniKey_MinMatchScore = "MinMatchScore";  //最低匹配度
        public const string gl_iniKey_DecodeTimes = "RedecodeTimes";           //重复解析次数
        public const string gl_inikey_Line = "LineNumber";             //线别
        public const string gl_iniKey_LinkType = "LinkType";           //关联类型
        public const string gl_iniKey_strDeviceID = "DeviceID";       //设备编号
        public const string gl_iniKey_strUseHalcon = "UseHalcon";     //使用Halcon解析
        public const string gl_inikey_specialPath = "SpecialPath";//特殊路径
        //Z轴控制2017.12.18
        public const string gl_iniSection_AxisZRef = "AxisZRef"; //Z轴参考点
        public const string gl_inikey_lastLinkType = "LastLinkType"; //上次关联类型，不一样需移动Z轴
        public const string gl_inikey_AxisZRef = "ZRef"; //

        //結果文件上傳、備份..
        //public const string gl_iniKey_SaveResultPath = "path_saveResult";     //存儲結果文件位置
        public const string gl_iniKey_SaveBackUpPath = "path_saveBackUp";     //存儲備份結果文件。
        public const string gl_iniKey_SavePics = "SavePics";         //是否存儲圖片
        public const string gl_iniKey_SaveNGPics = "SaveNGPics";     //是否存儲解析失敗路徑
        public const string gl_iniKey_PicSavePath = "PicSavePath";   //圖片存儲路徑
        public const string gl_iniKey_NGPicSavePath = "NGPicSavePath";   //解析NG圖片存儲路徑
        public const string gl_iniKey_ScanSerialPort = "SerialPort";   //串口端口
        public const string gl_iniKey_PosLimit_X_P = "PosLimit_X_P";  //X轴正向限位
        public const string gl_iniKey_PosLimit_X_N = "PosLimit_X_N";  //X轴负向限位
        public const string gl_iniKey_PosLimit_Y_P = "PosLimit_Y_P";  //Y轴正向限位
        public const string gl_iniKey_PosLimit_Y_N = "PosLimit_Y_N";  //Y轴负向限位

        public const string gl_inisection_TestInfo = "TestInfo";
        public const string gl_iniKey_LotNo = "LotNo";           //LotNo
        public const string gl_inikey_QualifiedNo = "QualifiedNo";   //合格票号

        public const string gl_iniSection_testresult = "TestResult";
        public const string gl_iniKey_TotalTest = "TotalTestCount";
        public const string gl_iniKey_TotalDecodeFailed = "DecodeFailed";
        public const string gl_iniKey_TotalSheets = "TotalSheetCount";

        public const string gl_inisection_SocketServerInfo = "SocketServerInfo";
        public const string gl_iniKey_SocketServerIP = "ServerIP";              //Socket主机IP
        public const string gl_iniKey_SocketServerMAC = "ServerMAC";            //Socket主机MAC地址
        public const string gl_iniKey_SocketServerPort = "ServerPort";          //Socket主机port

        public const string gl_iniSection_Size = "Size";
        public const string gl_iniKey_BlockWidth = "BlockWidth";
        public const string gl_iniKey_BlockHeigh = "BlockHeight";
        public const string gl_iniKey_WorkAreaWidth = "WorkAreaSize";
        public const string gl_inikey_WorkAreaHeight = "WorkAreaHeight";

        //研华板卡运动速度设定
        public const string gl_iniSection_AdvMotionSpeed = "AdvMotionSpeed";
        public const string gl_iniKey_AdvMotionSpd_VelHigh = "VelHigh";  //群组运动最快速度 
        public const string gl_iniKey_AdvMotionSpd_VelLow = "VelLow";  //群组运动最慢速度 
        public const string gl_iniKey_AdvMotionSpd_Acc = "Acc";  //群组运动最快加速度
        public const string gl_iniKey_AdvMotionSpd_Dec = "Dec";  //群组运动最快减速度

        //指定用作校準點坐標位置
        public const string gl_iniSection_CALPosition = "Calibration";
        public const string gl_iniKey_CALPos_X = "CALPosX";
        public const string gl_iniKey_CALPos_Y = "CALPosY";
        public const string gl_iniKey_CalibrateRatio_X = "CalRatio_X";   //校准斜率——X
        public const string gl_iniKey_CalibrateRatio_Y = "CalRatio_Y";   //校准斜率——Y

        //参考原点坐标
        public const string gl_iniSection_RefPoint = "RefPoint";
        public const string gl_iniKey_RefPoint_X = "Ref_X";
        public const string gl_iniKey_RefPoint_Y = "Ref_Y";

        //HD-5000参数
        public const string gl_iniSection_cam = "USB_CAM";
        public const string gl_inikey_Focus = "Focus";
        public const string gl_inikey_Zoom = "Zoom";
        public const string gl_inikey_Pan = "Pan";
        public const string gl_inikey_Tilt = "Tilt";
        public const string gl_inikey_Exposure = "Exposure";
        #endregion



        #region Exposure.ini  CCD拍照所用的曝光值参数
        public const string gl_iniExposure_FileName = "Exposure.INI";
        public const string gl_iniSection_Default = "Default";  
        public const string gl_iniSection_AAC = "AAC";  
        public const string gl_iniSection_ST = "ST";  
        public const string gl_iniSection_GEORTEK = "GEORTEK";  
        public const string gl_iniSection_KNOWLES = "KNOWLES";
        public const string gl_iniKey_Mark = "MarkExposure";  //mark点曝光值
        public const string gl_iniKey_Matrix = "MatrixExposure";  //条码点曝光值
        //参数NEW
        public const string ini_key_MExposure = "MatrixExposure"; //曝光
        public const string ini_key_MRedLight = "RedLight"; //红光
        public const string ini_key_MWhiteLight = "WhiteLight"; //白光
        #endregion

        //resultfile
        public const string gl_iniSection_Result = "Result";
        public static string gl_SpecialPath;


        public static List<CheckItem> gl_lsChkItem = new List<CheckItem> { };//错误信息提示列表
        public static bool gl_bPcsManage = false;//Pcs管理标志
        public static string gl_strShtBarcode="";//sheet条码
        public static DBQuery gl_DBquery = null;
        public static int glBonusLot = 0;        //BonusLot(0:是 1:不是)
        public static string[] gl_ProductType;
        public static string gl_strTargetPath = @"C:\条码照合程序3型\";
        public static string gl_strAppPath = @"D:\mmcsApps\条码照合程序3型\";
        
    }

    public class MPNPlan
    {
        public string LotNo;//Lot号
        public string Product;//品目
        public string Code;//编码
        public int Position;//位置
        public int Flowid;//FlowId
    }


    public enum LinkType
    {
        DEFAULT = 0,
        PROX = 1,
        MIC = 2,
        BARCODE = 3,
        IC = 4,
    }
}
