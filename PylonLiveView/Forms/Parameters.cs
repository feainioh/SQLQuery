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
    public partial class Parameters : Form
    {
        MyFunctions myfunc=new MyFunctions();
        DBQuery m_DBQuery = new DBQuery();
        private MainForm m_para_obj = null;  //父窗體對象
        public Parameters(MainForm mfm)
        {
            m_para_obj = mfm;
            InitializeComponent();
            initSerialPortsConfig();
        }

        #region 测试参数
        public int _TotalSheetPcs
        {
            get { return Convert.ToInt32(numericUpDown_TotalSheetPcs.Value); }
        }

        public int _BarcodeLength
        {
            get { return Convert.ToInt32(numericUpDown_barcodeLength.Value); }
        }

        public int _SheetBarcodeLength
        {
            get { return Convert.ToInt32(numericUpDown_sheetbarcodelength.Value); }
        }

        public int _BlockWidth
        {
            get { return Convert.ToInt32(numericUpDown_blockWidth.Value); }
        }

        public int _BlockHeight
        {
            get { return Convert.ToInt32(numericUpDown_blockHeight.Value); }
        }

        public string _MarkPointDiameter
        {
            get { return textBox_markRadio.Text; }
        }

        public int _RedecodeTimes
        {
            get { return Convert.ToInt32(numericUpDown_redecodetimes.Value); }
            set
            {
                try { numericUpDown_redecodetimes.Value = Convert.ToInt32(value); }
                catch { numericUpDown_redecodetimes.Value = 4; }
            }
        }

        public int _decode_timeOut
        {
            get { return Convert.ToInt32(numericUpDown_timeOut.Value); }
            set
            {
                try { numericUpDown_timeOut.Value = Convert.ToInt32(value); }
                catch { numericUpDown_timeOut.Value = 100000; }
            }
        }

        public int _MatchMinScore
        {
            get { return Convert.ToInt32(numericUpDown_minScore.Value); }
            set
            {
                try
                {
                    numericUpDown_minScore.Value = value;
                }
                catch { }
            }
        }

        public int _PosLimit_X_P //X轴正向软件限位
        {
            get { return Convert.ToInt32(numericUpDown_PosLimit_X_P.Value); }
            set
            {
                try { numericUpDown_PosLimit_X_P.Value = value; }
                catch { numericUpDown_PosLimit_X_P.Value = 30000; }
            }
        }

        public int _PosLimit_X_N  //X轴负向软件限位
        {
            get { return Convert.ToInt32(numericUpDown_PosLimit_X_N.Value); }
            set
            {
                try { numericUpDown_PosLimit_X_N.Value = value; }
                catch { numericUpDown_PosLimit_X_N.Value = -3000; }
            }
        }

        public int _PosLimit_Y_P //Y轴正向软件限位
        {
            get { return Convert.ToInt32(numericUpDown_PosLimit_Y_P.Value); }
            set
            {
                try { numericUpDown_PosLimit_Y_P.Value = value; }
                catch { numericUpDown_PosLimit_Y_P.Value = 30000; }
            }
        }

        public int _PosLimit_Y_N  //Y轴负向软件限位
        {
            get { return Convert.ToInt32(numericUpDown_PosLimit_Y_N.Value); }
            set
            {
                try { numericUpDown_PosLimit_Y_N.Value = value; }
                catch { numericUpDown_PosLimit_Y_N.Value = -3000; }
            }
        }

        public string _BarcodeScanPort
        {
            get { return comboBox_scanPort.Text; }
            set 
            {
                for (int i = 0; i < comboBox_scanPort.Items.Count; i++)
                {
                    if (comboBox_scanPort.Items[i].ToString() == value)
                    {
                        comboBox_scanPort.SelectedIndex = i;
                    }
                }
            }
        }

        //結果文件存儲、上傳位置
        //public string _Path_ResultFileSave
        //{
        //    get { return textBox_selectUploadPath.Text; }
        //    set { textBox_selectUploadPath.Text = value; }
        //}

        //結果文件上傳后備份位置
        public string _Path_ResultFileBackUp
        {
            get { return textBox_selectBackUpPath.Text; }
            set { textBox_selectBackUpPath.Text = value; }
        }

        public bool _SaveCapturePics
        {
            get { return checkBox_savepics.Checked; }
            set{ checkBox_savepics.Checked = value;  }
        }

        public string _PicSavePath
        {
            get { return textBox_picssavepath.Text; }
            set { textBox_picssavepath.Text = value; }
        }

        public bool _SaveNGPics
        {
            get { return checkBox_saveDecodeFailPics.Checked; }
            set { checkBox_saveDecodeFailPics.Checked = value; }
        }

        public string _NGPicsSavePath 
        {
            get { return textBox_decodeFailPicsSavePath.Text.Trim(); }
            set { textBox_decodeFailPicsSavePath.Text = value; }
        }

        public bool _UseHalcon
        {
            get { return checkBox_useHalcon.Checked; }
            set { checkBox_useHalcon.Checked = value; }
        }

         #endregion

        #region 光源参数
        public int _Exposure_Mark_Default 
        {
            get { return (int)numericUpDown_mark_default.Value; }
            set { numericUpDown_mark_default.Value = value; }
        }

        public int _Exposure_Matrix_Default
        {
            get { return (int)numericUpDown_matrix_default.Value; }
            set { numericUpDown_matrix_default.Value = value; }
        }

        public int _Exposure_Mark_GER
        {
            get { return (int)numericUpDown_mark_GER.Value; }
            set { numericUpDown_mark_GER.Value = value; }
        }

        public int _Exposure_Matrix_GER
        {
            get { return (int)numericUpDown_matrix_GER.Value; }
            set { numericUpDown_matrix_GER.Value = value; }
        }

        public int _Exposure_Mark_AAC
        {
            get { return (int)numericUpDown_mark_AAC.Value; }
            set { numericUpDown_mark_AAC.Value = value; }
        }

        public int _Exposure_Matrix_AAC
        {
            get { return (int)numericUpDown_matrix_AAC.Value; }
            set { numericUpDown_matrix_AAC.Value = value; }
        }

        public int _Exposure_Mark_ST
        {
            get { return (int)numericUpDown_mark_ST.Value; }
            set { numericUpDown_mark_ST.Value = value; }
        }

        public int _Exposure_Matrix_ST
        {
            get { return (int)numericUpDown_matrix_ST.Value; }
            set { numericUpDown_matrix_ST.Value = value; }
        }

        public int _Exposure_Mark_KNOWLES
        {
            get { return (int)numericUpDown_mark_KWS.Value; }
            set { numericUpDown_mark_KWS.Value = value; }
        }

        public int _Exposure_Matrix_KNOWLES
        {
            get { return (int)numericUpDown_matrix_KWS.Value; }
            set { numericUpDown_matrix_KWS.Value = value; }
        }

        #endregion

        #region 光源参数NEW
        public string _Model_ProductType
        {
            get { return combox_productModelMic.Text.ToString(); }
            set 
            {
                try
                {
                    for (int n = 0; n < combox_productModelMic.Items.Count; n++)
                    {
                        if (combox_productModelMic.Items[n].ToString().ToUpper() == value)
                        {
                            combox_productModelMic.SelectedIndex = n;
                        }
                    }
                }
                catch { }
            }
        }
        public string _Model_ProductTypeProx
        {
            get { return comboBox_proxProductProx.Text.ToString(); }
            set
            {
                try
                {
                    for (int n = 0; n < comboBox_proxProductProx.Items.Count; n++)
                    {
                        if (comboBox_proxProductProx.Items[n].ToString().ToUpper() == value)
                        {
                            comboBox_proxProductProx.SelectedIndex = n;
                        }
                    }
                }
                catch { }
            }
        }
        public int _Model_ExposureMic
        {
            get { return (int)numericUpDown_exposureModel.Value; }
            set { numericUpDown_exposureModel.Value = value; }
        }
        public int _Model_ExposureProx
        {
            get { return (int)numericUpDown_exposureProxModelProx.Value; }
            set { numericUpDown_exposureProxModelProx.Value = value; }
        }
        public int _Model_ExposurePcs
        {
            get { return (int)numericUpDown_exposureModelPcs.Value; }
            set{ numericUpDown_exposureModelPcs.Value = value;}
        }
        public int _Model_ExposureIC
        {
            get { return (int)numericUpDown_exposureIC.Value; }
            set { numericUpDown_exposureIC.Value = value; }
        }
        #endregion

        private void Parameters_Load(object sender, EventArgs e)
        {
            try
            {   
                //部品类型
                try
                {
                    if (GlobalVar.listProductType.Count > 0)
                    {
                        combox_productModelMic.Items.Clear();
                        combox_productModelMic.Items.AddRange(GlobalVar.listProductType.ToArray());
                    }
                }
                catch { }

                numericUpDown_TotalSheetPcs.Value = GlobalVar.gl_OneSheetCount;
                //numericUpDown_barcodeLength.Value = GlobalVar.gl_length_PCSBarcodeLength;
                numericUpDown_redecodetimes.Value = GlobalVar.gl_decode_times;
                numericUpDown_timeOut.Value = GlobalVar.gl_decode_timeout;
                numericUpDown_minScore.Value = GlobalVar.gl_MinMatchScore;
                numericUpDown_sheetbarcodelength.Value = GlobalVar.gl_length_sheetBarcodeLength;
                numericUpDown_blockWidth.Value = GlobalVar.block_width;
                numericUpDown_blockHeight.Value = GlobalVar.block_heigt;
                numericUpDown_PosLimit_X_P.Value = GlobalVar.gl_PosLimit_X_P;
                numericUpDown_PosLimit_X_N.Value = GlobalVar.gl_PosLimit_X_N;
                numericUpDown_PosLimit_Y_P.Value = GlobalVar.gl_PosLimit_Y_P;
                numericUpDown_PosLimit_Y_N.Value = GlobalVar.gl_PosLimit_Y_N;
                textBox_markRadio.Text = GlobalVar.gl_value_MarkPointDiameter.ToString();
                checkBox_savepics.Checked = GlobalVar.gl_saveCapturePics;
                textBox_picssavepath.Text = GlobalVar.gl_PicsSavePath;
                checkBox_saveDecodeFailPics.Checked = GlobalVar.gl_saveDecodeFailPics;
                textBox_decodeFailPicsSavePath.Text = GlobalVar.gl_NGPicsSavePath;
                textBox_SpecialPath.Text = GlobalVar.gl_SpecialPath;
                //結果文件上傳、備份
                //textBox_selectUploadPath.Text = GlobalVar.gl_path_FileResult;
                textBox_selectBackUpPath.Text = GlobalVar.gl_path_FileBackUp;
                if (GlobalVar.gl_LinkType.ToString() == "MIC")
                {
                    tabPage_MIC.Parent = tabControl2;
                    tabPage_PROX.Parent = null;
                    tabPage_PCS.Parent = null;
                    tabPage_IC.Parent = null;
                }
                else if (GlobalVar.gl_LinkType.ToString() =="PROX")
                {
                    tabPage_PROX.Parent = tabControl2;
                    tabPage_MIC.Parent = null;
                    tabPage_PCS.Parent = null;
                    tabPage_IC.Parent = null;
                }
                else if (GlobalVar.gl_LinkType == LinkType.IC)
                {
                    tabPage_IC.Parent = tabControl2;
                    tabPage_MIC.Parent = null;
                    tabPage_PROX.Parent = null;
                    tabPage_PCS.Parent = null;
                }
                else
                {
                    tabPage_IC.Parent = tabControl2;
                    tabPage_MIC.Parent = tabControl2;
                    tabPage_PROX.Parent = tabControl2;
                    tabPage_MIC.Parent = tabControl2;
                }
                #region 光源参数
                numericUpDown_mark_default.Value = GlobalVar.gl_exposure_Mark_default;
                numericUpDown_mark_AAC.Value = GlobalVar.gl_exposure_Mark_AAC;
                numericUpDown_mark_ST.Value = GlobalVar.gl_exposure_Mark_ST;
                numericUpDown_mark_GER.Value = GlobalVar.gl_exposure_Mark_Geortek;
                numericUpDown_mark_KWS.Value = GlobalVar.gl_exposure_Mark_Knowles;
                numericUpDown_matrix_default.Value = GlobalVar.gl_exposure_Matrix_default;
                numericUpDown_matrix_AAC.Value = GlobalVar.gl_exposure_Matrix_AAC;
                numericUpDown_matrix_ST.Value = GlobalVar.gl_exposure_Matrix_ST;
                numericUpDown_matrix_GER.Value = GlobalVar.gl_exposure_Matrix_Geortek;
                numericUpDown_matrix_KWS.Value = GlobalVar.gl_exposure_Matrix_Knowles;
                #endregion
                _UseHalcon = GlobalVar.gl_bUseHalcon;
                for (int i = 0; i < comboBox_scanPort.Items.Count; i++)
                {
                    if (comboBox_scanPort.Items[i].ToString() == GlobalVar.gl_serialPort_Scan)
                    {
                        comboBox_scanPort.SelectedIndex = i;
                        break;
                    }
                }
            }
            catch { }
        }

        public void initSerialPortsConfig()
        {
            string[] strList_serialPorts;
            strList_serialPorts = System.IO.Ports.SerialPort.GetPortNames();
            comboBox_scanPort.Items.Clear();
            for (int i = 0; i < strList_serialPorts.Length; i++)
            {
                comboBox_scanPort.Items.Add(strList_serialPorts[i]);
            }
            if (comboBox_scanPort.Items.Count > 0)
            {
                comboBox_scanPort.SelectedIndex = 0;
            }
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            //if (textBox_selectUploadPath.Text.Trim() == "")
            //{
            //    MessageBox.Show("結果文件存儲(上傳)路徑為空，請選擇存儲文件夾！");
            //    this.DialogResult = System.Windows.Forms.DialogResult.None;
            //    return;
            //}
            if (textBox_selectBackUpPath.Text.Trim() == "")
            {
                MessageBox.Show("結果文件備份路徑為空，請選擇備份存儲文件夾！");
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
            if (checkBox_savepics.Checked && textBox_picssavepath.Text.Trim() == "")
            {
                MessageBox.Show("請選擇拍照圖片存儲路徑(已經選中需要保存圖片)");
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
            if (checkBox_saveDecodeFailPics.Checked && textBox_decodeFailPicsSavePath.Text.Trim() == "")
            {
                MessageBox.Show("請選擇解析失敗圖片存儲路徑(已經選中需要保存解析NG圖片)");
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
            if (textBox_SpecialPath.Text.Trim() == "")
            {
//                 MessageBox.Show("特殊配置文件路徑為空，請選擇特殊配置存儲文件夾！");
//                 this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
            else
            {
                GlobalVar.gl_SpecialPath = textBox_SpecialPath.Text;
                string iniFilePath = GlobalVar.gl_strTargetPath + "\\" + GlobalVar.gl_iniTBS_FileName;
                myfunc.CheckFileExit();
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_inisection_Global, GlobalVar.gl_inikey_specialPath, GlobalVar.gl_SpecialPath, iniFilePath);     //特殊路径
            }
        }

        private void checkBox_savepics_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_savepics.Checked) { textBox_picssavepath.Enabled = button_savePathBrowse.Enabled = true; }
            else { textBox_picssavepath.Enabled = button_savePathBrowse.Enabled = false; }
        }

        private void button_savePathBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.Description = "路徑選擇";
            if (fb.ShowDialog() == DialogResult.OK)
            {
                textBox_picssavepath.Text = fb.SelectedPath;
                addLogStr("照片存儲路徑更改:"+fb.SelectedPath);
            }
        }

        private void button_selectDecodeFailPicsFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.Description = "路徑選擇";
            if (fb.ShowDialog() == DialogResult.OK)
            {
                textBox_decodeFailPicsSavePath.Text = fb.SelectedPath;
                addLogStr("解析失敗存儲路徑更改:"+fb.SelectedPath);
            }
        }

        private void checkBox_saveDecodeFailPics_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_saveDecodeFailPics.Checked) { textBox_decodeFailPicsSavePath.Enabled = button_selectDecodeFailPicsFolder.Enabled = true; }
            else { textBox_decodeFailPicsSavePath.Enabled = button_selectDecodeFailPicsFolder.Enabled = false; }
        }

        private void button_selectBackUpPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.Description = "備份文件位置選擇";
            if (fb.ShowDialog() == DialogResult.OK)
            {
                textBox_selectBackUpPath.Text = fb.SelectedPath;
                addLogStr("備份文件位置路徑更改:"+fb.SelectedPath);
            }
        }

        private void either_red_Event_BtnClick(Either.LeftRightSide lr)
        {
            switch (lr)
            {
                case Either.LeftRightSide.Left:
                    GlobalVar.gl_Model_redLight = 1; //红灯亮
                    break;
                case Either.LeftRightSide.Right:
                    GlobalVar.gl_Model_redLight = 0; //红灯灭
                    break;
            }
        }

        private void either_white_Event_BtnClick(Either.LeftRightSide lr)
        {
            switch (lr)
            {
                case Either.LeftRightSide.Left:
                    GlobalVar.gl_Model_whiteLight = 1; //白灯亮
                    break;
                case Either.LeftRightSide.Right:
                    GlobalVar.gl_Model_whiteLight = 0; //白灯灭
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = combox_productModelMic.Text.ToString();
            if (str == "") return;
            SetPorductTypeConfig(str);
        }

        private void combox_productModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = combox_productModelMic.Text.ToString();
            if (str == "") return;
            GetPorductTypeConfig(str);
            either_redMic.LeftPress = GlobalVar.gl_Model_redLight == 1 ? true : false;
            either_whiteMic.LeftPress = GlobalVar.gl_Model_whiteLight == 1 ? true : false;
            _Model_ExposureMic = GlobalVar.gl_Model_exposure;
        }

        private void GetPorductTypeConfig(string strProductType)
        {
            string strProductTypeINI = Application.StartupPath + "\\" + GlobalVar.gl_ProductModel + "\\" +GlobalVar.gl_LinkType+ "\\"+ GlobalVar.gl_iniExposure_FileName;
            //strProductTypeINI = Application.StartupPath + "\\" + GlobalVar.gl_ProductModel + "\\" + GlobalVar.gl_LinkType + "\\" + GlobalVar.gl_iniTBS_FileName;//将配置写入到同一文件下
            StringBuilder str_tmp = new StringBuilder(100);
            MyFunctions.GetPrivateProfileString(strProductType.ToUpper(), GlobalVar.ini_key_MRedLight, "1", str_tmp, 100, strProductTypeINI);
            GlobalVar.gl_Model_redLight = Convert.ToInt32(str_tmp.ToString());
            MyFunctions.GetPrivateProfileString(strProductType, GlobalVar.ini_key_MWhiteLight, "1", str_tmp, 100, strProductTypeINI);
            GlobalVar.gl_Model_whiteLight = Convert.ToInt32(str_tmp.ToString());
            MyFunctions.GetPrivateProfileString(strProductType, GlobalVar.ini_key_MExposure, "5000", str_tmp, 100, strProductTypeINI);
            GlobalVar.gl_Model_exposure = Convert.ToInt32(str_tmp.ToString());
            addLogStr("光源參數更改:部品:" + strProductType + "紅光:" + GlobalVar.gl_Model_redLight + "白光:" + GlobalVar.gl_Model_whiteLight + "曝光值:" + GlobalVar.gl_Model_exposure);
        }
        private void SetPorductTypeConfig(string strProductType) 
        {
            //GlobalVar.gl_Model_exposure = _Model_Exposure;
            //MyFunctions.WritePrivateProfileString(strProductType, ini_key_RedLight, GlobalVar.gl_Model_redLight.ToString(), strPorductTypeINI);
            //MyFunctions.WritePrivateProfileString(strProductType, ini_key_WhiteLight, GlobalVar.gl_Model_whiteLight.ToString(), strPorductTypeINI);
            //MyFunctions.WritePrivateProfileString(strProductType, ini_key_Exposure, GlobalVar.gl_Model_exposure.ToString(), strPorductTypeINI);
        }

        private void either_proxRed_Event_BtnClick(Either.LeftRightSide lr)
        {
            switch (lr)
            {
                case Either.LeftRightSide.Left:
                    GlobalVar.gl_Model_redLight = 1; //红灯亮
                    break;
                case Either.LeftRightSide.Right:
                    GlobalVar.gl_Model_redLight = 0; //红灯灭
                    break;
            }
        }

        private void either_proxWhite_Event_BtnClick(Either.LeftRightSide lr)
        {
            switch (lr)
            {
                case Either.LeftRightSide.Left:
                    GlobalVar.gl_Model_whiteLight = 1; //白灯亮
                    break;
                case Either.LeftRightSide.Right:
                    GlobalVar.gl_Model_whiteLight = 0; //白灯灭
                    break;
            }
        }

        private void comboBox_proxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = comboBox_proxProductProx.Text.ToString();
            if (str == "") return;
            GetPorductTypeConfig(str);            
            either_proxRedProx.LeftPress = GlobalVar.gl_Model_redLight == 1 ? true : false;
            either_proxWhiteProx.LeftPress = GlobalVar.gl_Model_whiteLight == 1 ? true : false;
            _Model_ExposureProx= GlobalVar.gl_Model_exposureProx;
        }

        private void textBox_SpecialPath_TextChanged(object sender, EventArgs e)
        {
        }

        private void button_SelectSpecialPath_Click(object sender, EventArgs e)
        {
            
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.Description = "特殊配置文件位置選擇";
            fb.SelectedPath = @"\\192.168.208.237\share" + "\\SystemFile\\SoftUpdate\\SANTEC\\NETWORK\\";
            if (fb.ShowDialog() == DialogResult.OK)
            {
                textBox_SpecialPath.Text = fb.SelectedPath;
                addLogStr("特殊路徑更改:"+fb.SelectedPath);
            }
        }

        private void comboBox_pcsProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = comboBox_pcsProductPcs.Text.ToString();
            if (str == "") return;
            GetPorductTypeConfig(str);
            either_redPcs.LeftPress = GlobalVar.gl_Model_redLight == 1 ? true : false;
            either_whitePcs.LeftPress = GlobalVar.gl_Model_whiteLight == 1 ? true : false;
            _Model_ExposurePcs = GlobalVar.gl_Model_exposurePcs;
        }

        private void groupBox11_Layout(object sender, LayoutEventArgs e)
        {
            comboBox_pcsProductPcs.SelectedIndex = 0;
        }

        private void tabControl2_Layout(object sender, LayoutEventArgs e)
        {
            comboBox_proxProductProx.Items.Clear();
            combox_productModelMic.Items.Clear();
            foreach (string str in GlobalVar.gl_ProductType)
            {
                combox_productModelMic.Items.Add(str);
                comboBox_proxProductProx.Items.Add(str);
            }
        }
        public void addLogStr(string Msg)
        {
            logWR.appendNewLogMessage(Msg);
        }

        private void either_redIC_Event_BtnClick(Either.LeftRightSide lr)
        {
            switch (lr)
            {
                case Either.LeftRightSide.Left:
                    GlobalVar.gl_Model_redLight = 1; //红灯亮
                    break;
                case Either.LeftRightSide.Right:
                    GlobalVar.gl_Model_redLight = 0; //红灯灭
                    break;
            }
        }

        private void either_whiteIC_Event_BtnClick(Either.LeftRightSide lr)
        {
            switch (lr)
            {
                case Either.LeftRightSide.Left:
                    GlobalVar.gl_Model_redLight = 1; //红灯亮
                    break;
                case Either.LeftRightSide.Right:
                    GlobalVar.gl_Model_redLight = 0; //红灯灭
                    break;
            }

        }

        private void comboBox_ICProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = comboBox_pcsProductPcs.Text.ToString();
            if (str == "") return;
            GetPorductTypeConfig(str);
            either_redPcs.LeftPress = GlobalVar.gl_Model_redLight == 1 ? true : false;
            either_whitePcs.LeftPress = GlobalVar.gl_Model_whiteLight == 1 ? true : false;
            _Model_ExposureIC= GlobalVar.gl_Model_exposureIC;
        }

        private void groupBox12_Layout(object sender, LayoutEventArgs e)
        {
            comboBox_ICProduct.SelectedIndex = 0;
        }

    }
}
