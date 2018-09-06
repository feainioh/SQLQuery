namespace PylonLiveView
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.imageListForDeviceList = new System.Windows.Forms.ImageList(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_reboot = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Setting = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_paraSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_AxisYAutoCalibrate = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_railWorkMode = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_normalWorkMode = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_passOnlyMode = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_admin = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_adminLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_deleteHisPicture = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_RunTest = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainerConfiguration = new System.Windows.Forms.SplitContainer();
            this.panel6 = new System.Windows.Forms.Panel();
            this.richTextBox_SingleShow = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label_deviation_Slopy_Y = new System.Windows.Forms.Label();
            this.label_deviation_VY = new System.Windows.Forms.Label();
            this.label_deviation_Slopy_X = new System.Windows.Forms.Label();
            this.label_deviation_VX = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.baslerCCD1 = new Basler.BaslerCCD();
            this.TipPointShow = new System.Windows.Forms.Panel();
            this.pictureBox_capture = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_save = new System.Windows.Forms.ToolStripMenuItem();
            this.label_visioDecodeString = new System.Windows.Forms.Label();
            this.panel_camSetting111 = new System.Windows.Forms.Panel();
            this.button_RunMsg = new System.Windows.Forms.Button();
            this.button_sheetSNInfo = new System.Windows.Forms.Button();
            this.button_partName = new System.Windows.Forms.Button();
            this.panel_info_Check = new System.Windows.Forms.Panel();
            this.txtbox_DeviceID = new System.Windows.Forms.TextBox();
            this.lbl_DeviceID = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_softReset = new System.Windows.Forms.Button();
            this.textBox_LotNo = new System.Windows.Forms.TextBox();
            this.button_workProhabit = new System.Windows.Forms.Button();
            this.textBox_MPN = new System.Windows.Forms.TextBox();
            this.button_workpermitted = new System.Windows.Forms.Button();
            this.panel17 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_operator = new System.Windows.Forms.TextBox();
            this.button_OK_FT = new System.Windows.Forms.Button();
            this.panel13 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.button_alarm = new System.Windows.Forms.Button();
            this.label_test = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_CADView = new System.Windows.Forms.TabPage();
            this.tabPage_parameters = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox_markPointSet = new System.Windows.Forms.GroupBox();
            this.lbl_refMarkY = new System.Windows.Forms.Label();
            this.lbl_refMarkX = new System.Windows.Forms.Label();
            this.textBox_CalPos_Z = new System.Windows.Forms.TextBox();
            this.textBox_CalPos_Y = new System.Windows.Forms.TextBox();
            this.textBox_CalPos_X = new System.Windows.Forms.TextBox();
            this.button_CalPosMove = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBox_refpointSetting = new System.Windows.Forms.GroupBox();
            this.button_RefOrgPoint = new System.Windows.Forms.Button();
            this.button_setRefPoint = new System.Windows.Forms.Button();
            this.textBox_fixPoint_z = new System.Windows.Forms.TextBox();
            this.textBox_fixPoint_y = new System.Windows.Forms.TextBox();
            this.label_fixPoint_x = new System.Windows.Forms.Label();
            this.textBox_fixPoint_x = new System.Windows.Forms.TextBox();
            this.label_fixPoint_y = new System.Windows.Forms.Label();
            this.label_fixPoint_z = new System.Windows.Forms.Label();
            this.groupBox_LightControl = new System.Windows.Forms.GroupBox();
            this.button_LedRed = new System.Windows.Forms.Button();
            this.button_LedWhite = new System.Windows.Forms.Button();
            this.button_LedWhiteOff = new System.Windows.Forms.Button();
            this.button_LedRedOff = new System.Windows.Forms.Button();
            this.pictureBox_ProxScan = new System.Windows.Forms.PictureBox();
            this.pictureBox_ShtSNScan = new System.Windows.Forms.PictureBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.button_CCDTrigger = new System.Windows.Forms.Button();
            this.label33 = new System.Windows.Forms.Label();
            this.pictureBox_ledRed = new System.Windows.Forms.PictureBox();
            this.pictureBox_ledWhite = new System.Windows.Forms.PictureBox();
            this.panel_testinfo = new System.Windows.Forms.Panel();
            this.button_clearTestinfo = new System.Windows.Forms.Button();
            this.textBox_decoderate = new System.Windows.Forms.TextBox();
            this.textBox_totalpcs = new System.Windows.Forms.TextBox();
            this.textBox_decodeNG = new System.Windows.Forms.TextBox();
            this.textBox_totalsheets = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox_record = new System.Windows.Forms.RichTextBox();
            this.panel_status = new System.Windows.Forms.Panel();
            this.panel_LodeType = new System.Windows.Forms.Panel();
            this.rdb_NetLoading = new System.Windows.Forms.RadioButton();
            this.rdb_LocalLoading = new System.Windows.Forms.RadioButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btn_safetyDoorEnable = new System.Windows.Forms.Button();
            this.btn_safetyDoorDispose = new System.Windows.Forms.Button();
            this.button_status = new System.Windows.Forms.Button();
            this.tabPage_mainview = new System.Windows.Forms.TabPage();
            this.tabPage_1020 = new System.Windows.Forms.TabPage();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_LoadCADFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_LoadPinmuConfigFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_SelfDefinePosition = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_saveCADAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_saveConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_sendTPsMessage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_practice = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_setSize = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Exit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_manualCapture = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton_LinkType = new System.Windows.Forms.ToolStripButton();
            this.tsslB_productType = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_autoDetectMicType = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel_opreatorType = new System.Windows.Forms.ToolStripLabel();
            this.serialPort_scan = new System.IO.Ports.SerialPort(this.components);
            this.timer_alarm = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_uploadInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslbl_ver = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerConfiguration)).BeginInit();
            this.splitContainerConfiguration.Panel1.SuspendLayout();
            this.splitContainerConfiguration.Panel2.SuspendLayout();
            this.splitContainerConfiguration.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_capture)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel_camSetting111.SuspendLayout();
            this.panel_info_Check.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel17.SuspendLayout();
            this.panel13.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage_parameters.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox_markPointSet.SuspendLayout();
            this.groupBox_refpointSetting.SuspendLayout();
            this.groupBox_LightControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ProxScan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ShtSNScan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ledRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ledWhite)).BeginInit();
            this.panel_testinfo.SuspendLayout();
            this.panel_status.SuspendLayout();
            this.panel_LodeType.SuspendLayout();
            this.panel5.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageListForDeviceList
            // 
            this.imageListForDeviceList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListForDeviceList.ImageSize = new System.Drawing.Size(32, 32);
            this.imageListForDeviceList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "*.bmp|*.jpg";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.ToolStripMenuItem_Setting,
            this.ToolStripMenuItem_admin,
            this.testToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1034, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_exit,
            this.ToolStripMenuItem_reboot});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // ToolStripMenuItem_exit
            // 
            this.ToolStripMenuItem_exit.Name = "ToolStripMenuItem_exit";
            this.ToolStripMenuItem_exit.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_exit.Text = "退出";
            this.ToolStripMenuItem_exit.Click += new System.EventHandler(this.ToolStripMenuItem_exit_Click);
            // 
            // ToolStripMenuItem_reboot
            // 
            this.ToolStripMenuItem_reboot.Name = "ToolStripMenuItem_reboot";
            this.ToolStripMenuItem_reboot.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_reboot.Text = "退出重啟";
            this.ToolStripMenuItem_reboot.Click += new System.EventHandler(this.ToolStripMenuItem_reboot_Click);
            // 
            // ToolStripMenuItem_Setting
            // 
            this.ToolStripMenuItem_Setting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_paraSetting,
            this.ToolStripMenuItem_AxisYAutoCalibrate,
            this.ToolStripMenuItem_railWorkMode});
            this.ToolStripMenuItem_Setting.Name = "ToolStripMenuItem_Setting";
            this.ToolStripMenuItem_Setting.Size = new System.Drawing.Size(44, 21);
            this.ToolStripMenuItem_Setting.Text = "设置";
            // 
            // ToolStripMenuItem_paraSetting
            // 
            this.ToolStripMenuItem_paraSetting.Name = "ToolStripMenuItem_paraSetting";
            this.ToolStripMenuItem_paraSetting.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_paraSetting.Text = "參數設置";
            this.ToolStripMenuItem_paraSetting.Click += new System.EventHandler(this.ToolStripMenuItem_paraSetting_Click);
            // 
            // ToolStripMenuItem_AxisYAutoCalibrate
            // 
            this.ToolStripMenuItem_AxisYAutoCalibrate.Enabled = false;
            this.ToolStripMenuItem_AxisYAutoCalibrate.Name = "ToolStripMenuItem_AxisYAutoCalibrate";
            this.ToolStripMenuItem_AxisYAutoCalibrate.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_AxisYAutoCalibrate.Text = "Y軸偏移校準";
            // 
            // ToolStripMenuItem_railWorkMode
            // 
            this.ToolStripMenuItem_railWorkMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_normalWorkMode,
            this.ToolStripMenuItem_passOnlyMode});
            this.ToolStripMenuItem_railWorkMode.Name = "ToolStripMenuItem_railWorkMode";
            this.ToolStripMenuItem_railWorkMode.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_railWorkMode.Text = "軌道工作模式";
            // 
            // ToolStripMenuItem_normalWorkMode
            // 
            this.ToolStripMenuItem_normalWorkMode.Name = "ToolStripMenuItem_normalWorkMode";
            this.ToolStripMenuItem_normalWorkMode.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_normalWorkMode.Text = "關聯工作模式";
            this.ToolStripMenuItem_normalWorkMode.Click += new System.EventHandler(this.ToolStripMenuItem_normalWorkMode_Click);
            // 
            // ToolStripMenuItem_passOnlyMode
            // 
            this.ToolStripMenuItem_passOnlyMode.Name = "ToolStripMenuItem_passOnlyMode";
            this.ToolStripMenuItem_passOnlyMode.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_passOnlyMode.Text = "滑軌通行模式";
            this.ToolStripMenuItem_passOnlyMode.Click += new System.EventHandler(this.ToolStripMenuItem_passOnlyMode_Click);
            // 
            // ToolStripMenuItem_admin
            // 
            this.ToolStripMenuItem_admin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_adminLogin,
            this.ToolStripMenuItem_deleteHisPicture,
            this.ToolStripMenuItem_RunTest});
            this.ToolStripMenuItem_admin.Name = "ToolStripMenuItem_admin";
            this.ToolStripMenuItem_admin.Size = new System.Drawing.Size(44, 21);
            this.ToolStripMenuItem_admin.Text = "高级";
            // 
            // ToolStripMenuItem_adminLogin
            // 
            this.ToolStripMenuItem_adminLogin.Name = "ToolStripMenuItem_adminLogin";
            this.ToolStripMenuItem_adminLogin.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_adminLogin.Text = "管理登入";
            this.ToolStripMenuItem_adminLogin.Click += new System.EventHandler(this.ToolStripMenuItem_adminLogin_Click);
            // 
            // ToolStripMenuItem_deleteHisPicture
            // 
            this.ToolStripMenuItem_deleteHisPicture.Name = "ToolStripMenuItem_deleteHisPicture";
            this.ToolStripMenuItem_deleteHisPicture.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_deleteHisPicture.Text = "歷史圖片刪除";
            this.ToolStripMenuItem_deleteHisPicture.Click += new System.EventHandler(this.ToolStripMenuItem_deleteHisPicture_Click);
            // 
            // ToolStripMenuItem_RunTest
            // 
            this.ToolStripMenuItem_RunTest.Enabled = false;
            this.ToolStripMenuItem_RunTest.Name = "ToolStripMenuItem_RunTest";
            this.ToolStripMenuItem_RunTest.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItem_RunTest.Text = "試運行";
            this.ToolStripMenuItem_RunTest.Click += new System.EventHandler(this.ToolStripMenuItem_RunTest_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Visible = false;
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainerConfiguration);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(771, 662);
            this.panel1.TabIndex = 3;
            // 
            // splitContainerConfiguration
            // 
            this.splitContainerConfiguration.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainerConfiguration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerConfiguration.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerConfiguration.Location = new System.Drawing.Point(0, 0);
            this.splitContainerConfiguration.Name = "splitContainerConfiguration";
            // 
            // splitContainerConfiguration.Panel1
            // 
            this.splitContainerConfiguration.Panel1.Controls.Add(this.panel6);
            this.splitContainerConfiguration.Panel1.Controls.Add(this.panel3);
            this.splitContainerConfiguration.Panel1.Controls.Add(this.baslerCCD1);
            // 
            // splitContainerConfiguration.Panel2
            // 
            this.splitContainerConfiguration.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.splitContainerConfiguration.Panel2.Controls.Add(this.TipPointShow);
            this.splitContainerConfiguration.Panel2.Controls.Add(this.pictureBox_capture);
            this.splitContainerConfiguration.Panel2.Controls.Add(this.label_visioDecodeString);
            this.splitContainerConfiguration.Panel2.Controls.Add(this.panel_camSetting111);
            this.splitContainerConfiguration.Size = new System.Drawing.Size(771, 662);
            this.splitContainerConfiguration.SplitterDistance = 246;
            this.splitContainerConfiguration.TabIndex = 2;
            this.splitContainerConfiguration.TabStop = false;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.richTextBox_SingleShow);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 508);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(242, 150);
            this.panel6.TabIndex = 59;
            // 
            // richTextBox_SingleShow
            // 
            this.richTextBox_SingleShow.BackColor = System.Drawing.Color.DarkSlateGray;
            this.richTextBox_SingleShow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_SingleShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_SingleShow.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox_SingleShow.Location = new System.Drawing.Point(0, 0);
            this.richTextBox_SingleShow.Name = "richTextBox_SingleShow";
            this.richTextBox_SingleShow.Size = new System.Drawing.Size(242, 150);
            this.richTextBox_SingleShow.TabIndex = 0;
            this.richTextBox_SingleShow.Text = "";
            this.richTextBox_SingleShow.TextChanged += new System.EventHandler(this.richTextBox_SingleShow_TextChanged);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.SteelBlue;
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label21);
            this.panel3.Controls.Add(this.label18);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label_deviation_Slopy_Y);
            this.panel3.Controls.Add(this.label_deviation_VY);
            this.panel3.Controls.Add(this.label_deviation_Slopy_X);
            this.panel3.Controls.Add(this.label_deviation_VX);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(0, 379);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(230, 100);
            this.panel3.TabIndex = 58;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.SteelBlue;
            this.label8.Location = new System.Drawing.Point(4, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 47;
            this.label8.Text = "製品斜率補正：";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.SteelBlue;
            this.label21.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.Location = new System.Drawing.Point(60, 80);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(19, 12);
            this.label21.TabIndex = 47;
            this.label21.Text = "Y:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.SteelBlue;
            this.label18.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(60, 34);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(19, 12);
            this.label18.TabIndex = 47;
            this.label18.Text = "Y:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.SteelBlue;
            this.label13.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(149, 80);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 12);
            this.label13.TabIndex = 47;
            this.label13.Text = "/10000";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.SteelBlue;
            this.label9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(149, 61);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 47;
            this.label9.Text = "/10000";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.SteelBlue;
            this.label17.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(151, 34);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(19, 12);
            this.label17.TabIndex = 47;
            this.label17.Text = "mm";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.SteelBlue;
            this.label20.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(60, 61);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(19, 12);
            this.label20.TabIndex = 47;
            this.label20.Text = "X:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.SteelBlue;
            this.label15.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(60, 15);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(19, 12);
            this.label15.TabIndex = 47;
            this.label15.Text = "X:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.SteelBlue;
            this.label12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(151, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 12);
            this.label12.TabIndex = 47;
            this.label12.Text = "mm";
            // 
            // label_deviation_Slopy_Y
            // 
            this.label_deviation_Slopy_Y.AutoSize = true;
            this.label_deviation_Slopy_Y.BackColor = System.Drawing.Color.SteelBlue;
            this.label_deviation_Slopy_Y.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_deviation_Slopy_Y.ForeColor = System.Drawing.Color.Crimson;
            this.label_deviation_Slopy_Y.Location = new System.Drawing.Point(82, 81);
            this.label_deviation_Slopy_Y.Name = "label_deviation_Slopy_Y";
            this.label_deviation_Slopy_Y.Size = new System.Drawing.Size(47, 12);
            this.label_deviation_Slopy_Y.TabIndex = 47;
            this.label_deviation_Slopy_Y.Text = "------";
            // 
            // label_deviation_VY
            // 
            this.label_deviation_VY.AutoSize = true;
            this.label_deviation_VY.BackColor = System.Drawing.Color.SteelBlue;
            this.label_deviation_VY.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_deviation_VY.ForeColor = System.Drawing.Color.Crimson;
            this.label_deviation_VY.Location = new System.Drawing.Point(81, 35);
            this.label_deviation_VY.Name = "label_deviation_VY";
            this.label_deviation_VY.Size = new System.Drawing.Size(47, 12);
            this.label_deviation_VY.TabIndex = 47;
            this.label_deviation_VY.Text = "------";
            // 
            // label_deviation_Slopy_X
            // 
            this.label_deviation_Slopy_X.AutoSize = true;
            this.label_deviation_Slopy_X.BackColor = System.Drawing.Color.SteelBlue;
            this.label_deviation_Slopy_X.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_deviation_Slopy_X.ForeColor = System.Drawing.Color.Crimson;
            this.label_deviation_Slopy_X.Location = new System.Drawing.Point(82, 62);
            this.label_deviation_Slopy_X.Name = "label_deviation_Slopy_X";
            this.label_deviation_Slopy_X.Size = new System.Drawing.Size(47, 12);
            this.label_deviation_Slopy_X.TabIndex = 47;
            this.label_deviation_Slopy_X.Text = "------";
            // 
            // label_deviation_VX
            // 
            this.label_deviation_VX.AutoSize = true;
            this.label_deviation_VX.BackColor = System.Drawing.Color.SteelBlue;
            this.label_deviation_VX.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_deviation_VX.ForeColor = System.Drawing.Color.Crimson;
            this.label_deviation_VX.Location = new System.Drawing.Point(81, 16);
            this.label_deviation_VX.Name = "label_deviation_VX";
            this.label_deviation_VX.Size = new System.Drawing.Size(47, 12);
            this.label_deviation_VX.TabIndex = 47;
            this.label_deviation_VX.Text = "------";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.SteelBlue;
            this.label7.Location = new System.Drawing.Point(4, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 47;
            this.label7.Text = "平面誤差補正";
            // 
            // baslerCCD1
            // 
            this.baslerCCD1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baslerCCD1.Enabled = false;
            this.baslerCCD1.Location = new System.Drawing.Point(0, 0);
            this.baslerCCD1.Margin = new System.Windows.Forms.Padding(4);
            this.baslerCCD1.Name = "baslerCCD1";
            this.baslerCCD1.Size = new System.Drawing.Size(242, 658);
            this.baslerCCD1.TabIndex = 15;
            // 
            // TipPointShow
            // 
            this.TipPointShow.AutoScroll = true;
            this.TipPointShow.Dock = System.Windows.Forms.DockStyle.Right;
            this.TipPointShow.Location = new System.Drawing.Point(355, 160);
            this.TipPointShow.Name = "TipPointShow";
            this.TipPointShow.Size = new System.Drawing.Size(162, 498);
            this.TipPointShow.TabIndex = 11;
            // 
            // pictureBox_capture
            // 
            this.pictureBox_capture.BackColor = System.Drawing.Color.DimGray;
            this.pictureBox_capture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_capture.ContextMenuStrip = this.contextMenuStrip1;
            this.pictureBox_capture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_capture.Location = new System.Drawing.Point(0, 160);
            this.pictureBox_capture.Name = "pictureBox_capture";
            this.pictureBox_capture.Size = new System.Drawing.Size(517, 498);
            this.pictureBox_capture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_capture.TabIndex = 8;
            this.pictureBox_capture.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_save});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 26);
            // 
            // ToolStripMenuItem_save
            // 
            this.ToolStripMenuItem_save.Name = "ToolStripMenuItem_save";
            this.ToolStripMenuItem_save.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_save.Text = "圖片存儲";
            this.ToolStripMenuItem_save.Click += new System.EventHandler(this.ToolStripMenuItem_save_Click);
            // 
            // label_visioDecodeString
            // 
            this.label_visioDecodeString.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label_visioDecodeString.AutoSize = true;
            this.label_visioDecodeString.BackColor = System.Drawing.Color.Silver;
            this.label_visioDecodeString.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_visioDecodeString.ForeColor = System.Drawing.Color.Green;
            this.label_visioDecodeString.Location = new System.Drawing.Point(244, 610);
            this.label_visioDecodeString.Name = "label_visioDecodeString";
            this.label_visioDecodeString.Size = new System.Drawing.Size(0, 16);
            this.label_visioDecodeString.TabIndex = 10;
            // 
            // panel_camSetting111
            // 
            this.panel_camSetting111.BackColor = System.Drawing.Color.SteelBlue;
            this.panel_camSetting111.Controls.Add(this.button_RunMsg);
            this.panel_camSetting111.Controls.Add(this.button_sheetSNInfo);
            this.panel_camSetting111.Controls.Add(this.button_partName);
            this.panel_camSetting111.Controls.Add(this.panel_info_Check);
            this.panel_camSetting111.Controls.Add(this.button_alarm);
            this.panel_camSetting111.Controls.Add(this.label_test);
            this.panel_camSetting111.Controls.Add(this.label5);
            this.panel_camSetting111.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_camSetting111.Location = new System.Drawing.Point(0, 0);
            this.panel_camSetting111.Name = "panel_camSetting111";
            this.panel_camSetting111.Size = new System.Drawing.Size(517, 160);
            this.panel_camSetting111.TabIndex = 9;
            // 
            // button_RunMsg
            // 
            this.button_RunMsg.BackColor = System.Drawing.Color.Gray;
            this.button_RunMsg.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_RunMsg.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_RunMsg.ForeColor = System.Drawing.Color.LightGreen;
            this.button_RunMsg.Location = new System.Drawing.Point(255, 104);
            this.button_RunMsg.Name = "button_RunMsg";
            this.button_RunMsg.Size = new System.Drawing.Size(100, 56);
            this.button_RunMsg.TabIndex = 16;
            this.button_RunMsg.UseVisualStyleBackColor = false;
            // 
            // button_sheetSNInfo
            // 
            this.button_sheetSNInfo.BackColor = System.Drawing.Color.DarkGray;
            this.button_sheetSNInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_sheetSNInfo.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_sheetSNInfo.Location = new System.Drawing.Point(255, 51);
            this.button_sheetSNInfo.Name = "button_sheetSNInfo";
            this.button_sheetSNInfo.Size = new System.Drawing.Size(100, 53);
            this.button_sheetSNInfo.TabIndex = 14;
            this.button_sheetSNInfo.UseVisualStyleBackColor = false;
            // 
            // button_partName
            // 
            this.button_partName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(202)))), ((int)(((byte)(240)))));
            this.button_partName.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_partName.Font = new System.Drawing.Font("Arial Black", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_partName.Location = new System.Drawing.Point(255, 0);
            this.button_partName.Name = "button_partName";
            this.button_partName.Size = new System.Drawing.Size(100, 51);
            this.button_partName.TabIndex = 15;
            this.button_partName.UseVisualStyleBackColor = false;
            // 
            // panel_info_Check
            // 
            this.panel_info_Check.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(202)))), ((int)(((byte)(240)))));
            this.panel_info_Check.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_info_Check.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_info_Check.Controls.Add(this.txtbox_DeviceID);
            this.panel_info_Check.Controls.Add(this.lbl_DeviceID);
            this.panel_info_Check.Controls.Add(this.panel10);
            this.panel_info_Check.Controls.Add(this.btn_softReset);
            this.panel_info_Check.Controls.Add(this.textBox_LotNo);
            this.panel_info_Check.Controls.Add(this.button_workProhabit);
            this.panel_info_Check.Controls.Add(this.textBox_MPN);
            this.panel_info_Check.Controls.Add(this.button_workpermitted);
            this.panel_info_Check.Controls.Add(this.panel17);
            this.panel_info_Check.Controls.Add(this.textBox_operator);
            this.panel_info_Check.Controls.Add(this.button_OK_FT);
            this.panel_info_Check.Controls.Add(this.panel13);
            this.panel_info_Check.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_info_Check.Location = new System.Drawing.Point(355, 0);
            this.panel_info_Check.Name = "panel_info_Check";
            this.panel_info_Check.Size = new System.Drawing.Size(162, 160);
            this.panel_info_Check.TabIndex = 47;
            // 
            // txtbox_DeviceID
            // 
            this.txtbox_DeviceID.Enabled = false;
            this.txtbox_DeviceID.Location = new System.Drawing.Point(61, 74);
            this.txtbox_DeviceID.Name = "txtbox_DeviceID";
            this.txtbox_DeviceID.Size = new System.Drawing.Size(93, 21);
            this.txtbox_DeviceID.TabIndex = 11;
            // 
            // lbl_DeviceID
            // 
            this.lbl_DeviceID.BackColor = System.Drawing.Color.Transparent;
            this.lbl_DeviceID.Font = new System.Drawing.Font("宋体", 9F);
            this.lbl_DeviceID.Location = new System.Drawing.Point(5, 78);
            this.lbl_DeviceID.Name = "lbl_DeviceID";
            this.lbl_DeviceID.Size = new System.Drawing.Size(66, 18);
            this.lbl_DeviceID.TabIndex = 11;
            this.lbl_DeviceID.Text = "设备编号:";
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(202)))), ((int)(((byte)(240)))));
            this.panel10.Controls.Add(this.label10);
            this.panel10.Location = new System.Drawing.Point(4, 3);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(55, 21);
            this.panel10.TabIndex = 38;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "作業員:";
            // 
            // btn_softReset
            // 
            this.btn_softReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(202)))), ((int)(((byte)(240)))));
            this.btn_softReset.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_softReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_softReset.Location = new System.Drawing.Point(82, 104);
            this.btn_softReset.Name = "btn_softReset";
            this.btn_softReset.Size = new System.Drawing.Size(75, 25);
            this.btn_softReset.TabIndex = 16;
            this.btn_softReset.Text = "軟件復位";
            this.btn_softReset.UseVisualStyleBackColor = false;
            this.btn_softReset.Click += new System.EventHandler(this.btn_softReset_Click);
            // 
            // textBox_LotNo
            // 
            this.textBox_LotNo.Location = new System.Drawing.Point(61, 27);
            this.textBox_LotNo.Name = "textBox_LotNo";
            this.textBox_LotNo.Size = new System.Drawing.Size(93, 21);
            this.textBox_LotNo.TabIndex = 1;
            // 
            // button_workProhabit
            // 
            this.button_workProhabit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(202)))), ((int)(((byte)(240)))));
            this.button_workProhabit.Enabled = false;
            this.button_workProhabit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_workProhabit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_workProhabit.Location = new System.Drawing.Point(82, 132);
            this.button_workProhabit.Name = "button_workProhabit";
            this.button_workProhabit.Size = new System.Drawing.Size(75, 25);
            this.button_workProhabit.TabIndex = 16;
            this.button_workProhabit.Text = "作業禁止";
            this.button_workProhabit.UseVisualStyleBackColor = false;
            this.button_workProhabit.Click += new System.EventHandler(this.button_workProhabit_Click);
            // 
            // textBox_MPN
            // 
            this.textBox_MPN.BackColor = System.Drawing.Color.Gainsboro;
            this.textBox_MPN.Enabled = false;
            this.textBox_MPN.Location = new System.Drawing.Point(61, 51);
            this.textBox_MPN.Name = "textBox_MPN";
            this.textBox_MPN.ReadOnly = true;
            this.textBox_MPN.Size = new System.Drawing.Size(93, 21);
            this.textBox_MPN.TabIndex = 43;
            // 
            // button_workpermitted
            // 
            this.button_workpermitted.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(202)))), ((int)(((byte)(240)))));
            this.button_workpermitted.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_workpermitted.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_workpermitted.Location = new System.Drawing.Point(1, 132);
            this.button_workpermitted.Name = "button_workpermitted";
            this.button_workpermitted.Size = new System.Drawing.Size(75, 25);
            this.button_workpermitted.TabIndex = 16;
            this.button_workpermitted.Text = "作業允許";
            this.button_workpermitted.UseVisualStyleBackColor = false;
            this.button_workpermitted.Click += new System.EventHandler(this.button_workpermitted_Click);
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(202)))), ((int)(((byte)(240)))));
            this.panel17.Controls.Add(this.label11);
            this.panel17.Location = new System.Drawing.Point(4, 26);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(55, 21);
            this.panel17.TabIndex = 39;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "LOT號:";
            // 
            // textBox_operator
            // 
            this.textBox_operator.Location = new System.Drawing.Point(61, 3);
            this.textBox_operator.Name = "textBox_operator";
            this.textBox_operator.Size = new System.Drawing.Size(93, 21);
            this.textBox_operator.TabIndex = 0;
            // 
            // button_OK_FT
            // 
            this.button_OK_FT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(202)))), ((int)(((byte)(240)))));
            this.button_OK_FT.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_OK_FT.Location = new System.Drawing.Point(1, 104);
            this.button_OK_FT.Name = "button_OK_FT";
            this.button_OK_FT.Size = new System.Drawing.Size(75, 25);
            this.button_OK_FT.TabIndex = 2;
            this.button_OK_FT.Text = "確定";
            this.button_OK_FT.UseVisualStyleBackColor = false;
            this.button_OK_FT.Click += new System.EventHandler(this.button_OK_FT_Click);
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(202)))), ((int)(((byte)(240)))));
            this.panel13.Controls.Add(this.label14);
            this.panel13.Controls.Add(this.label23);
            this.panel13.Location = new System.Drawing.Point(5, 53);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(55, 21);
            this.panel13.TabIndex = 40;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(11, -17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(35, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "品目:";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Font = new System.Drawing.Font("宋体", 9F);
            this.label23.Location = new System.Drawing.Point(9, 4);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(43, 18);
            this.label23.TabIndex = 12;
            this.label23.Text = "品目:";
            // 
            // button_alarm
            // 
            this.button_alarm.BackColor = System.Drawing.Color.Gray;
            this.button_alarm.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_alarm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_alarm.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_alarm.Location = new System.Drawing.Point(0, 0);
            this.button_alarm.Name = "button_alarm";
            this.button_alarm.Size = new System.Drawing.Size(255, 160);
            this.button_alarm.TabIndex = 12;
            this.button_alarm.Text = "緊急停止";
            this.button_alarm.UseVisualStyleBackColor = true;
            this.button_alarm.Click += new System.EventHandler(this.button_alarm_Click);
            // 
            // label_test
            // 
            this.label_test.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_test.AutoSize = true;
            this.label_test.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_test.Location = new System.Drawing.Point(98, 145);
            this.label_test.Name = "label_test";
            this.label_test.Size = new System.Drawing.Size(65, 12);
            this.label_test.TabIndex = 10;
            this.label_test.Text = "**********";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(3, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "到位拍照時間：";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_CADView);
            this.tabControl1.Controls.Add(this.tabPage_parameters);
            this.tabControl1.Controls.Add(this.tabPage_mainview);
            this.tabControl1.Controls.Add(this.tabPage_1020);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 64);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1034, 694);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage_CADView
            // 
            this.tabPage_CADView.Location = new System.Drawing.Point(4, 22);
            this.tabPage_CADView.Name = "tabPage_CADView";
            this.tabPage_CADView.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_CADView.Size = new System.Drawing.Size(1026, 668);
            this.tabPage_CADView.TabIndex = 1;
            this.tabPage_CADView.Text = "CAD视图";
            this.tabPage_CADView.UseVisualStyleBackColor = true;
            // 
            // tabPage_parameters
            // 
            this.tabPage_parameters.Controls.Add(this.panel1);
            this.tabPage_parameters.Controls.Add(this.panel2);
            this.tabPage_parameters.Location = new System.Drawing.Point(4, 22);
            this.tabPage_parameters.Name = "tabPage_parameters";
            this.tabPage_parameters.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_parameters.Size = new System.Drawing.Size(1026, 668);
            this.tabPage_parameters.TabIndex = 0;
            this.tabPage_parameters.Text = "选项设置";
            this.tabPage_parameters.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.panel2.Controls.Add(this.groupBox_markPointSet);
            this.panel2.Controls.Add(this.groupBox_refpointSetting);
            this.panel2.Controls.Add(this.groupBox_LightControl);
            this.panel2.Controls.Add(this.panel_testinfo);
            this.panel2.Controls.Add(this.richTextBox_record);
            this.panel2.Controls.Add(this.panel_status);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(774, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(249, 662);
            this.panel2.TabIndex = 4;
            // 
            // groupBox_markPointSet
            // 
            this.groupBox_markPointSet.BackColor = System.Drawing.Color.SteelBlue;
            this.groupBox_markPointSet.Controls.Add(this.lbl_refMarkY);
            this.groupBox_markPointSet.Controls.Add(this.lbl_refMarkX);
            this.groupBox_markPointSet.Controls.Add(this.textBox_CalPos_Z);
            this.groupBox_markPointSet.Controls.Add(this.textBox_CalPos_Y);
            this.groupBox_markPointSet.Controls.Add(this.textBox_CalPos_X);
            this.groupBox_markPointSet.Controls.Add(this.button_CalPosMove);
            this.groupBox_markPointSet.Controls.Add(this.label16);
            this.groupBox_markPointSet.Controls.Add(this.label19);
            this.groupBox_markPointSet.Controls.Add(this.label22);
            this.groupBox_markPointSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_markPointSet.Enabled = false;
            this.groupBox_markPointSet.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox_markPointSet.Location = new System.Drawing.Point(0, 521);
            this.groupBox_markPointSet.Name = "groupBox_markPointSet";
            this.groupBox_markPointSet.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox_markPointSet.Size = new System.Drawing.Size(249, 125);
            this.groupBox_markPointSet.TabIndex = 55;
            this.groupBox_markPointSet.TabStop = false;
            this.groupBox_markPointSet.Text = "校準坐標定點運動";
            // 
            // lbl_refMarkY
            // 
            this.lbl_refMarkY.AutoSize = true;
            this.lbl_refMarkY.Location = new System.Drawing.Point(180, 48);
            this.lbl_refMarkY.Name = "lbl_refMarkY";
            this.lbl_refMarkY.Size = new System.Drawing.Size(0, 14);
            this.lbl_refMarkY.TabIndex = 85;
            // 
            // lbl_refMarkX
            // 
            this.lbl_refMarkX.AutoSize = true;
            this.lbl_refMarkX.Location = new System.Drawing.Point(180, 23);
            this.lbl_refMarkX.Name = "lbl_refMarkX";
            this.lbl_refMarkX.Size = new System.Drawing.Size(0, 14);
            this.lbl_refMarkX.TabIndex = 84;
            // 
            // textBox_CalPos_Z
            // 
            this.textBox_CalPos_Z.Enabled = false;
            this.textBox_CalPos_Z.Location = new System.Drawing.Point(91, 67);
            this.textBox_CalPos_Z.Name = "textBox_CalPos_Z";
            this.textBox_CalPos_Z.Size = new System.Drawing.Size(85, 23);
            this.textBox_CalPos_Z.TabIndex = 83;
            this.textBox_CalPos_Z.Text = "0";
            // 
            // textBox_CalPos_Y
            // 
            this.textBox_CalPos_Y.Location = new System.Drawing.Point(91, 43);
            this.textBox_CalPos_Y.Name = "textBox_CalPos_Y";
            this.textBox_CalPos_Y.Size = new System.Drawing.Size(85, 23);
            this.textBox_CalPos_Y.TabIndex = 82;
            this.textBox_CalPos_Y.Text = "0";
            // 
            // textBox_CalPos_X
            // 
            this.textBox_CalPos_X.Location = new System.Drawing.Point(91, 19);
            this.textBox_CalPos_X.Name = "textBox_CalPos_X";
            this.textBox_CalPos_X.Size = new System.Drawing.Size(85, 23);
            this.textBox_CalPos_X.TabIndex = 81;
            this.textBox_CalPos_X.Text = "0";
            // 
            // button_CalPosMove
            // 
            this.button_CalPosMove.BackColor = System.Drawing.Color.SteelBlue;
            this.button_CalPosMove.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button_CalPosMove.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_CalPosMove.ForeColor = System.Drawing.Color.Black;
            this.button_CalPosMove.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_CalPosMove.Location = new System.Drawing.Point(3, 92);
            this.button_CalPosMove.Name = "button_CalPosMove";
            this.button_CalPosMove.Size = new System.Drawing.Size(243, 30);
            this.button_CalPosMove.TabIndex = 63;
            this.button_CalPosMove.Text = "發送指令";
            this.button_CalPosMove.UseVisualStyleBackColor = false;
            this.button_CalPosMove.Click += new System.EventHandler(this.button_CalPosMove_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Enabled = false;
            this.label16.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.ForeColor = System.Drawing.Color.Sienna;
            this.label16.Location = new System.Drawing.Point(30, 72);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(47, 12);
            this.label16.TabIndex = 66;
            this.label16.Text = "Pox_Z:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.ForeColor = System.Drawing.Color.Sienna;
            this.label19.Location = new System.Drawing.Point(30, 47);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(47, 12);
            this.label19.TabIndex = 63;
            this.label19.Text = "Pox_Y:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label22.ForeColor = System.Drawing.Color.Sienna;
            this.label22.Location = new System.Drawing.Point(30, 24);
            this.label22.Name = "label22";
            this.label22.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label22.Size = new System.Drawing.Size(47, 12);
            this.label22.TabIndex = 64;
            this.label22.Text = "Pox_X:";
            // 
            // groupBox_refpointSetting
            // 
            this.groupBox_refpointSetting.BackColor = System.Drawing.Color.SteelBlue;
            this.groupBox_refpointSetting.Controls.Add(this.button_RefOrgPoint);
            this.groupBox_refpointSetting.Controls.Add(this.button_setRefPoint);
            this.groupBox_refpointSetting.Controls.Add(this.textBox_fixPoint_z);
            this.groupBox_refpointSetting.Controls.Add(this.textBox_fixPoint_y);
            this.groupBox_refpointSetting.Controls.Add(this.label_fixPoint_x);
            this.groupBox_refpointSetting.Controls.Add(this.textBox_fixPoint_x);
            this.groupBox_refpointSetting.Controls.Add(this.label_fixPoint_y);
            this.groupBox_refpointSetting.Controls.Add(this.label_fixPoint_z);
            this.groupBox_refpointSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_refpointSetting.Enabled = false;
            this.groupBox_refpointSetting.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox_refpointSetting.Location = new System.Drawing.Point(0, 394);
            this.groupBox_refpointSetting.Name = "groupBox_refpointSetting";
            this.groupBox_refpointSetting.Size = new System.Drawing.Size(249, 127);
            this.groupBox_refpointSetting.TabIndex = 58;
            this.groupBox_refpointSetting.TabStop = false;
            this.groupBox_refpointSetting.Text = "參考原點設定:";
            // 
            // button_RefOrgPoint
            // 
            this.button_RefOrgPoint.BackColor = System.Drawing.Color.SteelBlue;
            this.button_RefOrgPoint.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_RefOrgPoint.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_RefOrgPoint.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_RefOrgPoint.Location = new System.Drawing.Point(149, 98);
            this.button_RefOrgPoint.Name = "button_RefOrgPoint";
            this.button_RefOrgPoint.Size = new System.Drawing.Size(90, 27);
            this.button_RefOrgPoint.TabIndex = 86;
            this.button_RefOrgPoint.Text = "回参考原点";
            this.button_RefOrgPoint.UseVisualStyleBackColor = false;
            this.button_RefOrgPoint.Click += new System.EventHandler(this.button_RtnRefOrgPoint_Click);
            // 
            // button_setRefPoint
            // 
            this.button_setRefPoint.BackColor = System.Drawing.Color.SteelBlue;
            this.button_setRefPoint.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_setRefPoint.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_setRefPoint.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_setRefPoint.Location = new System.Drawing.Point(13, 98);
            this.button_setRefPoint.Name = "button_setRefPoint";
            this.button_setRefPoint.Size = new System.Drawing.Size(90, 27);
            this.button_setRefPoint.TabIndex = 81;
            this.button_setRefPoint.Text = "發送指令";
            this.button_setRefPoint.UseVisualStyleBackColor = false;
            this.button_setRefPoint.Click += new System.EventHandler(this.button_setRefPoint_Click);
            // 
            // textBox_fixPoint_z
            // 
            this.textBox_fixPoint_z.Location = new System.Drawing.Point(90, 70);
            this.textBox_fixPoint_z.Name = "textBox_fixPoint_z";
            this.textBox_fixPoint_z.Size = new System.Drawing.Size(85, 26);
            this.textBox_fixPoint_z.TabIndex = 80;
            this.textBox_fixPoint_z.Text = "0";
            // 
            // textBox_fixPoint_y
            // 
            this.textBox_fixPoint_y.Location = new System.Drawing.Point(90, 44);
            this.textBox_fixPoint_y.Name = "textBox_fixPoint_y";
            this.textBox_fixPoint_y.Size = new System.Drawing.Size(85, 26);
            this.textBox_fixPoint_y.TabIndex = 78;
            this.textBox_fixPoint_y.Text = "0";
            // 
            // label_fixPoint_x
            // 
            this.label_fixPoint_x.AutoSize = true;
            this.label_fixPoint_x.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_fixPoint_x.ForeColor = System.Drawing.Color.Black;
            this.label_fixPoint_x.Location = new System.Drawing.Point(30, 23);
            this.label_fixPoint_x.Name = "label_fixPoint_x";
            this.label_fixPoint_x.Size = new System.Drawing.Size(53, 14);
            this.label_fixPoint_x.TabIndex = 84;
            this.label_fixPoint_x.Text = "X座標:";
            // 
            // textBox_fixPoint_x
            // 
            this.textBox_fixPoint_x.Location = new System.Drawing.Point(90, 18);
            this.textBox_fixPoint_x.Name = "textBox_fixPoint_x";
            this.textBox_fixPoint_x.Size = new System.Drawing.Size(85, 26);
            this.textBox_fixPoint_x.TabIndex = 76;
            this.textBox_fixPoint_x.Text = "0";
            // 
            // label_fixPoint_y
            // 
            this.label_fixPoint_y.AutoSize = true;
            this.label_fixPoint_y.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_fixPoint_y.ForeColor = System.Drawing.Color.Black;
            this.label_fixPoint_y.Location = new System.Drawing.Point(30, 48);
            this.label_fixPoint_y.Name = "label_fixPoint_y";
            this.label_fixPoint_y.Size = new System.Drawing.Size(53, 14);
            this.label_fixPoint_y.TabIndex = 83;
            this.label_fixPoint_y.Text = "Y座標:";
            // 
            // label_fixPoint_z
            // 
            this.label_fixPoint_z.AutoSize = true;
            this.label_fixPoint_z.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_fixPoint_z.ForeColor = System.Drawing.Color.Black;
            this.label_fixPoint_z.Location = new System.Drawing.Point(30, 73);
            this.label_fixPoint_z.Name = "label_fixPoint_z";
            this.label_fixPoint_z.Size = new System.Drawing.Size(53, 14);
            this.label_fixPoint_z.TabIndex = 85;
            this.label_fixPoint_z.Text = "Z座標:";
            // 
            // groupBox_LightControl
            // 
            this.groupBox_LightControl.BackColor = System.Drawing.Color.SteelBlue;
            this.groupBox_LightControl.Controls.Add(this.button_LedRed);
            this.groupBox_LightControl.Controls.Add(this.button_LedWhite);
            this.groupBox_LightControl.Controls.Add(this.button_LedWhiteOff);
            this.groupBox_LightControl.Controls.Add(this.button_LedRedOff);
            this.groupBox_LightControl.Controls.Add(this.pictureBox_ProxScan);
            this.groupBox_LightControl.Controls.Add(this.pictureBox_ShtSNScan);
            this.groupBox_LightControl.Controls.Add(this.label36);
            this.groupBox_LightControl.Controls.Add(this.label35);
            this.groupBox_LightControl.Controls.Add(this.label37);
            this.groupBox_LightControl.Controls.Add(this.label34);
            this.groupBox_LightControl.Controls.Add(this.button_CCDTrigger);
            this.groupBox_LightControl.Controls.Add(this.label33);
            this.groupBox_LightControl.Controls.Add(this.pictureBox_ledRed);
            this.groupBox_LightControl.Controls.Add(this.pictureBox_ledWhite);
            this.groupBox_LightControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_LightControl.Enabled = false;
            this.groupBox_LightControl.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox_LightControl.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox_LightControl.Location = new System.Drawing.Point(0, 265);
            this.groupBox_LightControl.Name = "groupBox_LightControl";
            this.groupBox_LightControl.Size = new System.Drawing.Size(249, 129);
            this.groupBox_LightControl.TabIndex = 56;
            this.groupBox_LightControl.TabStop = false;
            this.groupBox_LightControl.Text = "光源控制";
            // 
            // button_LedRed
            // 
            this.button_LedRed.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_LedRed.Location = new System.Drawing.Point(99, 15);
            this.button_LedRed.Name = "button_LedRed";
            this.button_LedRed.Size = new System.Drawing.Size(40, 20);
            this.button_LedRed.TabIndex = 26;
            this.button_LedRed.Text = "ON";
            this.button_LedRed.UseVisualStyleBackColor = true;
            this.button_LedRed.Click += new System.EventHandler(this.button_LedRed_Click);
            // 
            // button_LedWhite
            // 
            this.button_LedWhite.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_LedWhite.Location = new System.Drawing.Point(99, 41);
            this.button_LedWhite.Name = "button_LedWhite";
            this.button_LedWhite.Size = new System.Drawing.Size(40, 20);
            this.button_LedWhite.TabIndex = 27;
            this.button_LedWhite.Text = "ON";
            this.button_LedWhite.UseVisualStyleBackColor = true;
            this.button_LedWhite.Click += new System.EventHandler(this.button_LedWhite_Click);
            // 
            // button_LedWhiteOff
            // 
            this.button_LedWhiteOff.BackColor = System.Drawing.Color.Gray;
            this.button_LedWhiteOff.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_LedWhiteOff.Location = new System.Drawing.Point(155, 39);
            this.button_LedWhiteOff.Name = "button_LedWhiteOff";
            this.button_LedWhiteOff.Size = new System.Drawing.Size(40, 20);
            this.button_LedWhiteOff.TabIndex = 29;
            this.button_LedWhiteOff.Text = " ";
            this.button_LedWhiteOff.UseVisualStyleBackColor = false;
            this.button_LedWhiteOff.Click += new System.EventHandler(this.button_WhiteRedOff_Click);
            // 
            // button_LedRedOff
            // 
            this.button_LedRedOff.BackColor = System.Drawing.Color.Gray;
            this.button_LedRedOff.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_LedRedOff.Location = new System.Drawing.Point(155, 15);
            this.button_LedRedOff.Name = "button_LedRedOff";
            this.button_LedRedOff.Size = new System.Drawing.Size(40, 20);
            this.button_LedRedOff.TabIndex = 28;
            this.button_LedRedOff.Text = " ";
            this.button_LedRedOff.UseVisualStyleBackColor = false;
            this.button_LedRedOff.Click += new System.EventHandler(this.button_LedRedOff_Click);
            // 
            // pictureBox_ProxScan
            // 
            this.pictureBox_ProxScan.BackColor = System.Drawing.Color.Gray;
            this.pictureBox_ProxScan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_ProxScan.Location = new System.Drawing.Point(154, 82);
            this.pictureBox_ProxScan.Name = "pictureBox_ProxScan";
            this.pictureBox_ProxScan.Size = new System.Drawing.Size(41, 15);
            this.pictureBox_ProxScan.TabIndex = 7;
            this.pictureBox_ProxScan.TabStop = false;
            // 
            // pictureBox_ShtSNScan
            // 
            this.pictureBox_ShtSNScan.BackColor = System.Drawing.Color.Gray;
            this.pictureBox_ShtSNScan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_ShtSNScan.Location = new System.Drawing.Point(154, 62);
            this.pictureBox_ShtSNScan.Name = "pictureBox_ShtSNScan";
            this.pictureBox_ShtSNScan.Size = new System.Drawing.Size(41, 15);
            this.pictureBox_ShtSNScan.TabIndex = 7;
            this.pictureBox_ShtSNScan.TabStop = false;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label36.Location = new System.Drawing.Point(28, 85);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(107, 14);
            this.label36.TabIndex = 13;
            this.label36.Text = "开始PROX照合:";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label35.Location = new System.Drawing.Point(28, 64);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(130, 14);
            this.label35.TabIndex = 13;
            this.label35.Text = "开始扫Sheet条码:";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label37.Location = new System.Drawing.Point(30, 106);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(76, 14);
            this.label37.TabIndex = 13;
            this.label37.Text = "CCD触发：";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label34.Location = new System.Drawing.Point(28, 43);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(75, 14);
            this.label34.TabIndex = 13;
            this.label34.Text = "白色光源:";
            // 
            // button_CCDTrigger
            // 
            this.button_CCDTrigger.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_CCDTrigger.Location = new System.Drawing.Point(122, 100);
            this.button_CCDTrigger.Name = "button_CCDTrigger";
            this.button_CCDTrigger.Size = new System.Drawing.Size(76, 25);
            this.button_CCDTrigger.TabIndex = 27;
            this.button_CCDTrigger.Text = "Trigger";
            this.button_CCDTrigger.UseVisualStyleBackColor = true;
            this.button_CCDTrigger.Click += new System.EventHandler(this.button_CCDTrigger_Click);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label33.Location = new System.Drawing.Point(27, 20);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(75, 14);
            this.label33.TabIndex = 11;
            this.label33.Text = "红色光源:";
            // 
            // pictureBox_ledRed
            // 
            this.pictureBox_ledRed.BackColor = System.Drawing.Color.Gray;
            this.pictureBox_ledRed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_ledRed.Location = new System.Drawing.Point(98, 20);
            this.pictureBox_ledRed.Name = "pictureBox_ledRed";
            this.pictureBox_ledRed.Size = new System.Drawing.Size(41, 15);
            this.pictureBox_ledRed.TabIndex = 4;
            this.pictureBox_ledRed.TabStop = false;
            this.pictureBox_ledRed.Visible = false;
            // 
            // pictureBox_ledWhite
            // 
            this.pictureBox_ledWhite.BackColor = System.Drawing.Color.Gray;
            this.pictureBox_ledWhite.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_ledWhite.Location = new System.Drawing.Point(97, 41);
            this.pictureBox_ledWhite.Name = "pictureBox_ledWhite";
            this.pictureBox_ledWhite.Size = new System.Drawing.Size(42, 15);
            this.pictureBox_ledWhite.TabIndex = 7;
            this.pictureBox_ledWhite.TabStop = false;
            this.pictureBox_ledWhite.Visible = false;
            // 
            // panel_testinfo
            // 
            this.panel_testinfo.BackColor = System.Drawing.Color.SteelBlue;
            this.panel_testinfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_testinfo.Controls.Add(this.button_clearTestinfo);
            this.panel_testinfo.Controls.Add(this.textBox_decoderate);
            this.panel_testinfo.Controls.Add(this.textBox_totalpcs);
            this.panel_testinfo.Controls.Add(this.textBox_decodeNG);
            this.panel_testinfo.Controls.Add(this.textBox_totalsheets);
            this.panel_testinfo.Controls.Add(this.label6);
            this.panel_testinfo.Controls.Add(this.label4);
            this.panel_testinfo.Controls.Add(this.label3);
            this.panel_testinfo.Controls.Add(this.label2);
            this.panel_testinfo.Controls.Add(this.label1);
            this.panel_testinfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_testinfo.Location = new System.Drawing.Point(0, 161);
            this.panel_testinfo.Name = "panel_testinfo";
            this.panel_testinfo.Size = new System.Drawing.Size(249, 104);
            this.panel_testinfo.TabIndex = 19;
            // 
            // button_clearTestinfo
            // 
            this.button_clearTestinfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button_clearTestinfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(202)))), ((int)(((byte)(240)))));
            this.button_clearTestinfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.button_clearTestinfo.Location = new System.Drawing.Point(164, 73);
            this.button_clearTestinfo.Name = "button_clearTestinfo";
            this.button_clearTestinfo.Size = new System.Drawing.Size(80, 24);
            this.button_clearTestinfo.TabIndex = 16;
            this.button_clearTestinfo.Text = "計數清除";
            this.button_clearTestinfo.UseVisualStyleBackColor = false;
            this.button_clearTestinfo.Click += new System.EventHandler(this.button_clearTestinfo_Click);
            // 
            // textBox_decoderate
            // 
            this.textBox_decoderate.Location = new System.Drawing.Point(86, 73);
            this.textBox_decoderate.Name = "textBox_decoderate";
            this.textBox_decoderate.Size = new System.Drawing.Size(65, 21);
            this.textBox_decoderate.TabIndex = 1;
            // 
            // textBox_totalpcs
            // 
            this.textBox_totalpcs.Location = new System.Drawing.Point(86, 50);
            this.textBox_totalpcs.Name = "textBox_totalpcs";
            this.textBox_totalpcs.Size = new System.Drawing.Size(96, 21);
            this.textBox_totalpcs.TabIndex = 1;
            // 
            // textBox_decodeNG
            // 
            this.textBox_decodeNG.Location = new System.Drawing.Point(86, 27);
            this.textBox_decodeNG.Name = "textBox_decodeNG";
            this.textBox_decodeNG.Size = new System.Drawing.Size(96, 21);
            this.textBox_decodeNG.TabIndex = 1;
            // 
            // textBox_totalsheets
            // 
            this.textBox_totalsheets.Location = new System.Drawing.Point(86, 4);
            this.textBox_totalsheets.Name = "textBox_totalsheets";
            this.textBox_totalsheets.Size = new System.Drawing.Size(96, 21);
            this.textBox_totalsheets.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(153, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "%";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(4, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "解析成功率:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(4, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "測試總PCS:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(4, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "解析NG總數:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "測試總張數:";
            // 
            // richTextBox_record
            // 
            this.richTextBox_record.BackColor = System.Drawing.Color.DarkGray;
            this.richTextBox_record.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_record.Location = new System.Drawing.Point(0, 161);
            this.richTextBox_record.Name = "richTextBox_record";
            this.richTextBox_record.Size = new System.Drawing.Size(249, 501);
            this.richTextBox_record.TabIndex = 17;
            this.richTextBox_record.Text = "";
            // 
            // panel_status
            // 
            this.panel_status.BackColor = System.Drawing.Color.SteelBlue;
            this.panel_status.Controls.Add(this.panel_LodeType);
            this.panel_status.Controls.Add(this.panel5);
            this.panel_status.Controls.Add(this.button_status);
            this.panel_status.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_status.Location = new System.Drawing.Point(0, 0);
            this.panel_status.Name = "panel_status";
            this.panel_status.Size = new System.Drawing.Size(249, 161);
            this.panel_status.TabIndex = 18;
            // 
            // panel_LodeType
            // 
            this.panel_LodeType.BackColor = System.Drawing.Color.Lavender;
            this.panel_LodeType.Controls.Add(this.rdb_NetLoading);
            this.panel_LodeType.Controls.Add(this.rdb_LocalLoading);
            this.panel_LodeType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_LodeType.Enabled = false;
            this.panel_LodeType.Location = new System.Drawing.Point(175, 83);
            this.panel_LodeType.Margin = new System.Windows.Forms.Padding(2);
            this.panel_LodeType.Name = "panel_LodeType";
            this.panel_LodeType.Size = new System.Drawing.Size(74, 78);
            this.panel_LodeType.TabIndex = 19;
            // 
            // rdb_NetLoading
            // 
            this.rdb_NetLoading.AutoSize = true;
            this.rdb_NetLoading.Checked = true;
            this.rdb_NetLoading.Location = new System.Drawing.Point(2, 32);
            this.rdb_NetLoading.Margin = new System.Windows.Forms.Padding(2);
            this.rdb_NetLoading.Name = "rdb_NetLoading";
            this.rdb_NetLoading.Size = new System.Drawing.Size(71, 16);
            this.rdb_NetLoading.TabIndex = 12;
            this.rdb_NetLoading.TabStop = true;
            this.rdb_NetLoading.Text = "网络读取";
            this.rdb_NetLoading.UseVisualStyleBackColor = true;
            this.rdb_NetLoading.CheckedChanged += new System.EventHandler(this.rdb_NetLoading_CheckedChanged);
            // 
            // rdb_LocalLoading
            // 
            this.rdb_LocalLoading.AutoSize = true;
            this.rdb_LocalLoading.Location = new System.Drawing.Point(2, 13);
            this.rdb_LocalLoading.Margin = new System.Windows.Forms.Padding(2);
            this.rdb_LocalLoading.Name = "rdb_LocalLoading";
            this.rdb_LocalLoading.Size = new System.Drawing.Size(71, 16);
            this.rdb_LocalLoading.TabIndex = 11;
            this.rdb_LocalLoading.TabStop = true;
            this.rdb_LocalLoading.Text = "特殊读取";
            this.rdb_LocalLoading.UseVisualStyleBackColor = true;
            this.rdb_LocalLoading.Visible = false;
            this.rdb_LocalLoading.CheckedChanged += new System.EventHandler(this.rdb_LocalLoading_CheckedChanged);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btn_safetyDoorEnable);
            this.panel5.Controls.Add(this.btn_safetyDoorDispose);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 83);
            this.panel5.Margin = new System.Windows.Forms.Padding(2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(175, 78);
            this.panel5.TabIndex = 18;
            // 
            // btn_safetyDoorEnable
            // 
            this.btn_safetyDoorEnable.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btn_safetyDoorEnable.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_safetyDoorEnable.Enabled = false;
            this.btn_safetyDoorEnable.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_safetyDoorEnable.Location = new System.Drawing.Point(0, 40);
            this.btn_safetyDoorEnable.Name = "btn_safetyDoorEnable";
            this.btn_safetyDoorEnable.Size = new System.Drawing.Size(175, 38);
            this.btn_safetyDoorEnable.TabIndex = 37;
            this.btn_safetyDoorEnable.Text = "安全门上锁";
            this.btn_safetyDoorEnable.UseVisualStyleBackColor = false;
            this.btn_safetyDoorEnable.Click += new System.EventHandler(this.btn_safetyDoorEnable_Click);
            // 
            // btn_safetyDoorDispose
            // 
            this.btn_safetyDoorDispose.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btn_safetyDoorDispose.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_safetyDoorDispose.Enabled = false;
            this.btn_safetyDoorDispose.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_safetyDoorDispose.Location = new System.Drawing.Point(0, 0);
            this.btn_safetyDoorDispose.Name = "btn_safetyDoorDispose";
            this.btn_safetyDoorDispose.Size = new System.Drawing.Size(175, 42);
            this.btn_safetyDoorDispose.TabIndex = 36;
            this.btn_safetyDoorDispose.Text = "安全门释放";
            this.btn_safetyDoorDispose.UseVisualStyleBackColor = false;
            this.btn_safetyDoorDispose.Click += new System.EventHandler(this.btn_safetyDoorDispose_Click);
            // 
            // button_status
            // 
            this.button_status.BackColor = System.Drawing.Color.Gray;
            this.button_status.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_status.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Bold);
            this.button_status.Location = new System.Drawing.Point(0, 0);
            this.button_status.Name = "button_status";
            this.button_status.Size = new System.Drawing.Size(249, 83);
            this.button_status.TabIndex = 17;
            this.button_status.Text = "待機中";
            this.button_status.UseVisualStyleBackColor = false;
            // 
            // tabPage_mainview
            // 
            this.tabPage_mainview.Location = new System.Drawing.Point(4, 22);
            this.tabPage_mainview.Name = "tabPage_mainview";
            this.tabPage_mainview.Size = new System.Drawing.Size(1026, 668);
            this.tabPage_mainview.TabIndex = 2;
            this.tabPage_mainview.Text = "全局视图";
            this.tabPage_mainview.UseVisualStyleBackColor = true;
            // 
            // tabPage_1020
            // 
            this.tabPage_1020.Location = new System.Drawing.Point(4, 22);
            this.tabPage_1020.Name = "tabPage_1020";
            this.tabPage_1020.Size = new System.Drawing.Size(1026, 668);
            this.tabPage_1020.TabIndex = 3;
            this.tabPage_1020.Text = "控制卡狀態";
            this.tabPage_1020.UseVisualStyleBackColor = true;
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.Color.SteelBlue;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_LoadCADFile,
            this.toolStripButton_LoadPinmuConfigFile,
            this.toolStripButton_SelfDefinePosition,
            this.toolStripSeparator2,
            this.toolStripButton_saveCADAs,
            this.toolStripButton_saveConfig,
            this.toolStripSeparator3,
            this.toolStripButton_sendTPsMessage,
            this.toolStripButton_practice,
            this.toolStripSeparator4,
            this.toolStripButton_setSize,
            this.toolStripButton_Exit,
            this.toolStripSeparator1,
            this.toolStripButton_manualCapture,
            this.toolStripSeparator5,
            this.toolStripLabel2,
            this.toolStripButton_LinkType,
            this.tsslB_productType,
            this.toolStripButton_autoDetectMicType,
            this.toolStripLabel_opreatorType});
            this.toolStrip.Location = new System.Drawing.Point(0, 25);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1034, 39);
            this.toolStrip.TabIndex = 5;
            this.toolStrip.Text = "toolStrip";
            // 
            // toolStripButton_LoadCADFile
            // 
            this.toolStripButton_LoadCADFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.toolStripButton_LoadCADFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_LoadCADFile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_LoadCADFile.Image")));
            this.toolStripButton_LoadCADFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_LoadCADFile.Name = "toolStripButton_LoadCADFile";
            this.toolStripButton_LoadCADFile.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton_LoadCADFile.Text = "toolStripButton1";
            this.toolStripButton_LoadCADFile.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.toolStripButton_LoadCADFile.ToolTipText = "導入CAD圖紙文檔";
            this.toolStripButton_LoadCADFile.Click += new System.EventHandler(this.toolStripButton_LoadCADFile_Click);
            // 
            // toolStripButton_LoadPinmuConfigFile
            // 
            this.toolStripButton_LoadPinmuConfigFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_LoadPinmuConfigFile.Enabled = false;
            this.toolStripButton_LoadPinmuConfigFile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_LoadPinmuConfigFile.Image")));
            this.toolStripButton_LoadPinmuConfigFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_LoadPinmuConfigFile.Name = "toolStripButton_LoadPinmuConfigFile";
            this.toolStripButton_LoadPinmuConfigFile.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton_LoadPinmuConfigFile.Text = "toolStripButton2";
            this.toolStripButton_LoadPinmuConfigFile.ToolTipText = "導入品目配置文檔";
            // 
            // toolStripButton_SelfDefinePosition
            // 
            this.toolStripButton_SelfDefinePosition.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_SelfDefinePosition.Enabled = false;
            this.toolStripButton_SelfDefinePosition.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_SelfDefinePosition.Image")));
            this.toolStripButton_SelfDefinePosition.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_SelfDefinePosition.Name = "toolStripButton_SelfDefinePosition";
            this.toolStripButton_SelfDefinePosition.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton_SelfDefinePosition.Text = "toolStripButton3";
            this.toolStripButton_SelfDefinePosition.ToolTipText = "自定義標籤座標";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.BackColor = System.Drawing.Color.OrangeRed;
            this.toolStripSeparator2.ForeColor = System.Drawing.Color.Azure;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripButton_saveCADAs
            // 
            this.toolStripButton_saveCADAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_saveCADAs.Enabled = false;
            this.toolStripButton_saveCADAs.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_saveCADAs.Image")));
            this.toolStripButton_saveCADAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_saveCADAs.Name = "toolStripButton_saveCADAs";
            this.toolStripButton_saveCADAs.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton_saveCADAs.Text = "toolStripButton1";
            this.toolStripButton_saveCADAs.ToolTipText = "另存為CAD文檔";
            // 
            // toolStripButton_saveConfig
            // 
            this.toolStripButton_saveConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_saveConfig.Enabled = false;
            this.toolStripButton_saveConfig.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_saveConfig.Image")));
            this.toolStripButton_saveConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_saveConfig.Name = "toolStripButton_saveConfig";
            this.toolStripButton_saveConfig.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton_saveConfig.Text = "toolStripButton1";
            this.toolStripButton_saveConfig.ToolTipText = "配置信息存儲";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripButton_sendTPsMessage
            // 
            this.toolStripButton_sendTPsMessage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_sendTPsMessage.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_sendTPsMessage.Image")));
            this.toolStripButton_sendTPsMessage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_sendTPsMessage.Name = "toolStripButton_sendTPsMessage";
            this.toolStripButton_sendTPsMessage.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton_sendTPsMessage.Text = "toolStripButton1";
            this.toolStripButton_sendTPsMessage.ToolTipText = "發送座標信息";
            this.toolStripButton_sendTPsMessage.Click += new System.EventHandler(this.toolStripButton_sendTPsMessage_Click);
            // 
            // toolStripButton_practice
            // 
            this.toolStripButton_practice.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_practice.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_practice.Image")));
            this.toolStripButton_practice.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_practice.Name = "toolStripButton_practice";
            this.toolStripButton_practice.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton_practice.Text = "toolStripButton2";
            this.toolStripButton_practice.ToolTipText = "動作演示";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripButton_setSize
            // 
            this.toolStripButton_setSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_setSize.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_setSize.Image")));
            this.toolStripButton_setSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_setSize.Name = "toolStripButton_setSize";
            this.toolStripButton_setSize.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton_setSize.Text = "toolStripButton1";
            this.toolStripButton_setSize.ToolTipText = "設定面板尺寸";
            // 
            // toolStripButton_Exit
            // 
            this.toolStripButton_Exit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_Exit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Exit.Image")));
            this.toolStripButton_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Exit.Name = "toolStripButton_Exit";
            this.toolStripButton_Exit.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton_Exit.Text = "toolStripButton1";
            this.toolStripButton_Exit.ToolTipText = "系統退出";
            this.toolStripButton_Exit.Click += new System.EventHandler(this.toolStripButton_Exit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripButton_manualCapture
            // 
            this.toolStripButton_manualCapture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_manualCapture.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_manualCapture.Image")));
            this.toolStripButton_manualCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_manualCapture.Name = "toolStripButton_manualCapture";
            this.toolStripButton_manualCapture.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton_manualCapture.Text = "toolStripButton1";
            this.toolStripButton_manualCapture.Click += new System.EventHandler(this.toolStripButton_manualCapture_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(29, 36);
            this.toolStripLabel2.Text = "test";
            this.toolStripLabel2.Visible = false;
            this.toolStripLabel2.Click += new System.EventHandler(this.toolStripLabel2_Click);
            // 
            // toolStripButton_LinkType
            // 
            this.toolStripButton_LinkType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_LinkType.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripButton_LinkType.ForeColor = System.Drawing.Color.MediumSpringGreen;
            this.toolStripButton_LinkType.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_LinkType.Image")));
            this.toolStripButton_LinkType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_LinkType.Name = "toolStripButton_LinkType";
            this.toolStripButton_LinkType.Size = new System.Drawing.Size(226, 36);
            this.toolStripButton_LinkType.Text = "关联作业类型:[MIC关联]";
            // 
            // tsslB_productType
            // 
            this.tsslB_productType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsslB_productType.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsslB_productType.Image = ((System.Drawing.Image)(resources.GetObject("tsslB_productType.Image")));
            this.tsslB_productType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsslB_productType.Name = "tsslB_productType";
            this.tsslB_productType.Size = new System.Drawing.Size(111, 36);
            this.tsslB_productType.Text = "部品类型:[]";
            // 
            // toolStripButton_autoDetectMicType
            // 
            this.toolStripButton_autoDetectMicType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_autoDetectMicType.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold);
            this.toolStripButton_autoDetectMicType.ForeColor = System.Drawing.Color.White;
            this.toolStripButton_autoDetectMicType.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_autoDetectMicType.Image")));
            this.toolStripButton_autoDetectMicType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_autoDetectMicType.Name = "toolStripButton_autoDetectMicType";
            this.toolStripButton_autoDetectMicType.Size = new System.Drawing.Size(30, 36);
            this.toolStripButton_autoDetectMicType.Text = "[]";
            this.toolStripButton_autoDetectMicType.Click += new System.EventHandler(this.toolStripButton_autoDetectMicType_Click);
            // 
            // toolStripLabel_opreatorType
            // 
            this.toolStripLabel_opreatorType.Font = new System.Drawing.Font("微软雅黑", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripLabel_opreatorType.Name = "toolStripLabel_opreatorType";
            this.toolStripLabel_opreatorType.Size = new System.Drawing.Size(149, 36);
            this.toolStripLabel_opreatorType.Text = "当前模式：[OP]";
            // 
            // serialPort_scan
            // 
            this.serialPort_scan.BaudRate = 115200;
            this.serialPort_scan.DtrEnable = true;
            this.serialPort_scan.ReadTimeout = 1000;
            this.serialPort_scan.RtsEnable = true;
            // 
            // timer_alarm
            // 
            this.timer_alarm.Interval = 500;
            this.timer_alarm.Tick += new System.EventHandler(this.timer_alarm_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel_uploadInfo,
            this.toolStripStatusLabel3,
            this.tsslbl_ver});
            this.statusStrip1.Location = new System.Drawing.Point(0, 758);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1034, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.IndianRed;
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(116, 17);
            this.toolStripStatusLabel1.Text = "关联结果数据上传：";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(116, 17);
            this.toolStripStatusLabel2.Text = "                           ";
            // 
            // toolStripStatusLabel_uploadInfo
            // 
            this.toolStripStatusLabel_uploadInfo.Name = "toolStripStatusLabel_uploadInfo";
            this.toolStripStatusLabel_uploadInfo.Size = new System.Drawing.Size(101, 17);
            this.toolStripStatusLabel_uploadInfo.Text = "结果上传等待中...";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(618, 17);
            this.toolStripStatusLabel3.Spring = true;
            // 
            // tsslbl_ver
            // 
            this.tsslbl_ver.Name = "tsslbl_ver";
            this.tsslbl_ver.Size = new System.Drawing.Size(68, 17);
            this.tsslbl_ver.Text = "软件版本：";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(109, 760);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 20);
            this.button1.TabIndex = 6;
            this.button1.Text = "开始上传";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button_startUpload_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 780);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "PYLON";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainerConfiguration.Panel1.ResumeLayout(false);
            this.splitContainerConfiguration.Panel2.ResumeLayout(false);
            this.splitContainerConfiguration.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerConfiguration)).EndInit();
            this.splitContainerConfiguration.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_capture)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel_camSetting111.ResumeLayout(false);
            this.panel_camSetting111.PerformLayout();
            this.panel_info_Check.ResumeLayout(false);
            this.panel_info_Check.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel17.ResumeLayout(false);
            this.panel17.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage_parameters.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox_markPointSet.ResumeLayout(false);
            this.groupBox_markPointSet.PerformLayout();
            this.groupBox_refpointSetting.ResumeLayout(false);
            this.groupBox_refpointSetting.PerformLayout();
            this.groupBox_LightControl.ResumeLayout(false);
            this.groupBox_LightControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ProxScan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ShtSNScan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ledRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ledWhite)).EndInit();
            this.panel_testinfo.ResumeLayout(false);
            this.panel_testinfo.PerformLayout();
            this.panel_status.ResumeLayout(false);
            this.panel_LodeType.ResumeLayout(false);
            this.panel_LodeType.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageListForDeviceList;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_exit;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Setting;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainerConfiguration;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_parameters;
        private System.Windows.Forms.TabPage tabPage_CADView;
        private System.Windows.Forms.TabPage tabPage_mainview;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_admin;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel_status;
        private System.Windows.Forms.RichTextBox richTextBox_record;
        private System.Windows.Forms.PictureBox pictureBox_capture;
        private System.Windows.Forms.Panel panel_camSetting111;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton_LoadCADFile;
        private System.Windows.Forms.ToolStripButton toolStripButton_LoadPinmuConfigFile;
        private System.Windows.Forms.ToolStripButton toolStripButton_SelfDefinePosition;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton_saveCADAs;
        private System.Windows.Forms.ToolStripButton toolStripButton_saveConfig;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton_sendTPsMessage;
        private System.Windows.Forms.ToolStripButton toolStripButton_practice;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButton_setSize;
        private System.Windows.Forms.ToolStripButton toolStripButton_Exit;
        private System.IO.Ports.SerialPort serialPort_scan;
        private System.Windows.Forms.Label label_visioDecodeString;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_save;
        private System.Windows.Forms.Label label_test;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_alarm;
        private System.Windows.Forms.Timer timer_alarm;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_paraSetting;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_AxisYAutoCalibrate;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_adminLogin;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_railWorkMode;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_normalWorkMode;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_passOnlyMode;
        private System.Windows.Forms.TabPage tabPage_1020;
        private System.Windows.Forms.Panel panel_testinfo;
        private System.Windows.Forms.Button button_clearTestinfo;
        private System.Windows.Forms.TextBox textBox_decoderate;
        private System.Windows.Forms.TextBox textBox_totalpcs;
        private System.Windows.Forms.TextBox textBox_decodeNG;
        private System.Windows.Forms.TextBox textBox_totalsheets;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_status;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_reboot;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_deleteHisPicture;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_RunTest;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton toolStripButton_autoDetectMicType;
        private System.Windows.Forms.ToolStripButton toolStripButton_LinkType;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_uploadInfo;
        private System.Windows.Forms.ToolStripButton toolStripButton_manualCapture;
        private System.Windows.Forms.ToolStripButton tsslB_productType;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel tsslbl_ver;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_opreatorType;
        private System.Windows.Forms.Panel panel_LodeType;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btn_safetyDoorEnable;
        private System.Windows.Forms.Button btn_safetyDoorDispose;
        private System.Windows.Forms.RadioButton rdb_NetLoading;
        private System.Windows.Forms.RadioButton rdb_LocalLoading;
        private Basler.BaslerCCD baslerCCD1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel_info_Check;
        private System.Windows.Forms.TextBox txtbox_DeviceID;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_softReset;
        private System.Windows.Forms.TextBox textBox_LotNo;
        private System.Windows.Forms.Button button_workProhabit;
        private System.Windows.Forms.TextBox textBox_MPN;
        private System.Windows.Forms.Button button_workpermitted;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_operator;
        private System.Windows.Forms.Button button_OK_FT;
        private System.Windows.Forms.Button button_partName;
        private System.Windows.Forms.Button button_sheetSNInfo;
        private System.Windows.Forms.Button button_RunMsg;
        private System.Windows.Forms.Label lbl_DeviceID;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.GroupBox groupBox_refpointSetting;
        private System.Windows.Forms.Button button_RefOrgPoint;
        private System.Windows.Forms.Button button_setRefPoint;
        private System.Windows.Forms.TextBox textBox_fixPoint_z;
        private System.Windows.Forms.TextBox textBox_fixPoint_y;
        private System.Windows.Forms.Label label_fixPoint_x;
        private System.Windows.Forms.TextBox textBox_fixPoint_x;
        private System.Windows.Forms.Label label_fixPoint_y;
        private System.Windows.Forms.Label label_fixPoint_z;
        private System.Windows.Forms.GroupBox groupBox_LightControl;
        private System.Windows.Forms.PictureBox pictureBox_ProxScan;
        private System.Windows.Forms.PictureBox pictureBox_ShtSNScan;
        private System.Windows.Forms.PictureBox pictureBox_ledWhite;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.PictureBox pictureBox_ledRed;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Button button_CCDTrigger;
        private System.Windows.Forms.Button button_LedRed;
        private System.Windows.Forms.Button button_LedWhite;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.GroupBox groupBox_markPointSet;
        private System.Windows.Forms.Label lbl_refMarkY;
        private System.Windows.Forms.Label lbl_refMarkX;
        private System.Windows.Forms.TextBox textBox_CalPos_Z;
        private System.Windows.Forms.TextBox textBox_CalPos_Y;
        private System.Windows.Forms.TextBox textBox_CalPos_X;
        private System.Windows.Forms.Button button_CalPosMove;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label_deviation_Slopy_Y;
        private System.Windows.Forms.Label label_deviation_VY;
        private System.Windows.Forms.Label label_deviation_Slopy_X;
        private System.Windows.Forms.Label label_deviation_VX;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_LedRedOff;
        private System.Windows.Forms.Button button_LedWhiteOff;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RichTextBox richTextBox_SingleShow;
        private System.Windows.Forms.Panel TipPointShow;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;

    }
}

