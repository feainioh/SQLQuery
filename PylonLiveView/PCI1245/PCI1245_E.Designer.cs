using System;
namespace MainSpace.PCI1245
{
    partial class PCI1245_E
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => 
                {
                    Dispose(disposing);
                    return;
                }
                ));
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Button_OpenBoard = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericUpDown_MovDis_X = new System.Windows.Forms.NumericUpDown();
            this.comboBox_MoveDirect_X = new System.Windows.Forms.ComboBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Button_ClearEnd_X = new System.Windows.Forms.Button();
            this.Button_SetEnd_X = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.Button_CountReset_X = new System.Windows.Forms.Button();
            this.Button_CloseBoard = new System.Windows.Forms.Button();
            this.groupBox_AxisX = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.pictureBox_DI3_X = new System.Windows.Forms.PictureBox();
            this.Button_D07_X = new System.Windows.Forms.Button();
            this.pictureBox_DI2_X = new System.Windows.Forms.PictureBox();
            this.Button_D06_X = new System.Windows.Forms.Button();
            this.pictureBox_DI0_X = new System.Windows.Forms.PictureBox();
            this.Button_D05_X = new System.Windows.Forms.Button();
            this.pictureBox_DI1_X = new System.Windows.Forms.PictureBox();
            this.Button_D04_X = new System.Windows.Forms.Button();
            this.Picturebox_D05_X = new System.Windows.Forms.PictureBox();
            this.Picturebox_D06_X = new System.Windows.Forms.PictureBox();
            this.Picturebox_D07_X = new System.Windows.Forms.PictureBox();
            this.Picturebox_D04_X = new System.Windows.Forms.PictureBox();
            this.button_Home_X = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.numericUpDown_VelLow_X = new System.Windows.Forms.NumericUpDown();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.numericUpDown_VelHigh_X = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.numericUpDown_Jerk_X = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Dec_X = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.button_UpdateSpdValue_X = new System.Windows.Forms.Button();
            this.button_SpeedSet_X = new System.Windows.Forms.Button();
            this.label_status_AxisX = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_LoginCount_X = new System.Windows.Forms.TextBox();
            this.button_ServerON_X = new System.Windows.Forms.Button();
            this.button_AddToGp_X = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.pictureBox_DI3_Y = new System.Windows.Forms.PictureBox();
            this.label19 = new System.Windows.Forms.Label();
            this.Button_D07_Y = new System.Windows.Forms.Button();
            this.pictureBox_DI2_Y = new System.Windows.Forms.PictureBox();
            this.Button_D06_Y = new System.Windows.Forms.Button();
            this.pictureBox_DI0_Y = new System.Windows.Forms.PictureBox();
            this.Button_D05_Y = new System.Windows.Forms.Button();
            this.pictureBox_DI1_Y = new System.Windows.Forms.PictureBox();
            this.Button_D04_Y = new System.Windows.Forms.Button();
            this.Picturebox_D05_Y = new System.Windows.Forms.PictureBox();
            this.Picturebox_D06_Y = new System.Windows.Forms.PictureBox();
            this.Picturebox_D07_Y = new System.Windows.Forms.PictureBox();
            this.Picturebox_D04_Y = new System.Windows.Forms.PictureBox();
            this.button_Home_Y = new System.Windows.Forms.Button();
            this.Button_CountReset_Y = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.numericUpDown_VelLow_Y = new System.Windows.Forms.NumericUpDown();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.numericUpDown_VelHigh_Y = new System.Windows.Forms.NumericUpDown();
            this.label27 = new System.Windows.Forms.Label();
            this.numericUpDown_Jerk_Y = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Dec_Y = new System.Windows.Forms.NumericUpDown();
            this.label28 = new System.Windows.Forms.Label();
            this.button_UpdateSpdValue_Y = new System.Windows.Forms.Button();
            this.button_SpeedSet_Y = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDown_MovDis_Y = new System.Windows.Forms.NumericUpDown();
            this.comboBox_MoveDirect_Y = new System.Windows.Forms.ComboBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.Button_ClearEnd_Y = new System.Windows.Forms.Button();
            this.Button_SetEnd_Y = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label_status_AxisY = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button_ServerOn_Y = new System.Windows.Forms.Button();
            this.textBox_LoginCount_Y = new System.Windows.Forms.TextBox();
            this.button_AddToGp_Y = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.listBoxAxesInGp = new System.Windows.Forms.ListBox();
            this.listBoxEndPoint = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnResetErr = new System.Windows.Forms.Button();
            this.textBoxMasterID = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxGpState = new System.Windows.Forms.TextBox();
            this.Button_Move = new System.Windows.Forms.Button();
            this.radioButtonAbs = new System.Windows.Forms.RadioButton();
            this.radioButtonRel = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Button_Stop = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.numericUpDown_VelLow_gp = new System.Windows.Forms.NumericUpDown();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.numericUpDown_VelHigh_gp = new System.Windows.Forms.NumericUpDown();
            this.label31 = new System.Windows.Forms.Label();
            this.numericUpDown_Acc_gp = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Dec_gp = new System.Windows.Forms.NumericUpDown();
            this.label32 = new System.Windows.Forms.Label();
            this.button_UpdateGpSpdValue = new System.Windows.Forms.Button();
            this.button_SpeedSet_gp = new System.Windows.Forms.Button();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.pictureBox_ProxScan = new System.Windows.Forms.PictureBox();
            this.pictureBox_ShtSNScan = new System.Windows.Forms.PictureBox();
            this.pictureBox_ledWhite = new System.Windows.Forms.PictureBox();
            this.label36 = new System.Windows.Forms.Label();
            this.pictureBox_ledRed = new System.Windows.Forms.PictureBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.button_CCDTrigger = new System.Windows.Forms.Button();
            this.button_LedRed = new System.Windows.Forms.Button();
            this.button_LedWhite = new System.Windows.Forms.Button();
            this.label33 = new System.Windows.Forms.Label();
            this.btn_refAxisZ = new System.Windows.Forms.Button();
            this.groupBox_z = new System.Windows.Forms.GroupBox();
            this.label_status_AxisZ = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.button_Home_Z = new System.Windows.Forms.Button();
            this.Button_CountReset_Z = new System.Windows.Forms.Button();
            this.label46 = new System.Windows.Forms.Label();
            this.button_ServerOn_Z = new System.Windows.Forms.Button();
            this.textBox_LoginCount_Z = new System.Windows.Forms.TextBox();
            this.button_AddToGp_Z = new System.Windows.Forms.Button();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.numericUpDown_VelLow_Z = new System.Windows.Forms.NumericUpDown();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.numericUpDown_VelHigh_Z = new System.Windows.Forms.NumericUpDown();
            this.label44 = new System.Windows.Forms.Label();
            this.numericUpDown_Acc_Z = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_Dec_Z = new System.Windows.Forms.NumericUpDown();
            this.label45 = new System.Windows.Forms.Label();
            this.button_UpdateSpdValue_Z = new System.Windows.Forms.Button();
            this.button_SpeedSet_Z = new System.Windows.Forms.Button();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.numericUpDown_MovDis_Z = new System.Windows.Forms.NumericUpDown();
            this.comboBox_MoveDirect_Z = new System.Windows.Forms.ComboBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.btn_goZ = new System.Windows.Forms.Button();
            this.label41 = new System.Windows.Forms.Label();
            this.comboBox_op = new System.Windows.Forms.ComboBox();
            this.numericUpDown_refAxisZ = new System.Windows.Forms.NumericUpDown();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.label47 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MovDis_X)).BeginInit();
            this.groupBox_AxisX.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DI3_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DI2_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DI0_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DI1_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox_D05_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox_D06_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox_D07_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox_D04_X)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_VelLow_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_VelHigh_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Jerk_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Dec_X)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DI3_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DI2_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DI0_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DI1_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox_D05_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox_D06_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox_D07_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox_D04_Y)).BeginInit();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_VelLow_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_VelHigh_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Jerk_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Dec_Y)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MovDis_Y)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_VelLow_gp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_VelHigh_gp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Acc_gp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Dec_gp)).BeginInit();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ProxScan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ShtSNScan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ledWhite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ledRed)).BeginInit();
            this.groupBox_z.SuspendLayout();
            this.groupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_VelLow_Z)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_VelHigh_Z)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Acc_Z)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Dec_Z)).BeginInit();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MovDis_Z)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_refAxisZ)).BeginInit();
            this.groupBox14.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button_OpenBoard
            // 
            this.Button_OpenBoard.Location = new System.Drawing.Point(32, 5);
            this.Button_OpenBoard.Name = "Button_OpenBoard";
            this.Button_OpenBoard.Size = new System.Drawing.Size(101, 23);
            this.Button_OpenBoard.TabIndex = 20;
            this.Button_OpenBoard.Text = "连接板卡";
            this.Button_OpenBoard.UseVisualStyleBackColor = true;
            this.Button_OpenBoard.Click += new System.EventHandler(this.Button_OpenBoard_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericUpDown_MovDis_X);
            this.groupBox3.Controls.Add(this.comboBox_MoveDirect_X);
            this.groupBox3.Controls.Add(this.label38);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.Button_ClearEnd_X);
            this.groupBox3.Controls.Add(this.Button_SetEnd_X);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.groupBox3.Location = new System.Drawing.Point(9, 72);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(221, 70);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "运动设置";
            // 
            // numericUpDown_MovDis_X
            // 
            this.numericUpDown_MovDis_X.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_MovDis_X.Location = new System.Drawing.Point(60, 43);
            this.numericUpDown_MovDis_X.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_MovDis_X.Name = "numericUpDown_MovDis_X";
            this.numericUpDown_MovDis_X.Size = new System.Drawing.Size(60, 21);
            this.numericUpDown_MovDis_X.TabIndex = 15;
            this.numericUpDown_MovDis_X.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // comboBox_MoveDirect_X
            // 
            this.comboBox_MoveDirect_X.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_MoveDirect_X.FormattingEnabled = true;
            this.comboBox_MoveDirect_X.Items.AddRange(new object[] {
            "正向",
            "反向"});
            this.comboBox_MoveDirect_X.Location = new System.Drawing.Point(60, 19);
            this.comboBox_MoveDirect_X.Name = "comboBox_MoveDirect_X";
            this.comboBox_MoveDirect_X.Size = new System.Drawing.Size(76, 20);
            this.comboBox_MoveDirect_X.TabIndex = 14;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(129, 46);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(23, 12);
            this.label38.TabIndex = 13;
            this.label38.Text = "pps";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "终点值:";
            // 
            // Button_ClearEnd_X
            // 
            this.Button_ClearEnd_X.Location = new System.Drawing.Point(150, 42);
            this.Button_ClearEnd_X.Name = "Button_ClearEnd_X";
            this.Button_ClearEnd_X.Size = new System.Drawing.Size(64, 23);
            this.Button_ClearEnd_X.TabIndex = 12;
            this.Button_ClearEnd_X.Text = "清除";
            this.Button_ClearEnd_X.UseVisualStyleBackColor = true;
            this.Button_ClearEnd_X.Click += new System.EventHandler(this.Button_ClearEnd_Click);
            // 
            // Button_SetEnd_X
            // 
            this.Button_SetEnd_X.Location = new System.Drawing.Point(150, 17);
            this.Button_SetEnd_X.Name = "Button_SetEnd_X";
            this.Button_SetEnd_X.Size = new System.Drawing.Size(64, 23);
            this.Button_SetEnd_X.TabIndex = 9;
            this.Button_SetEnd_X.Text = "设置终点";
            this.Button_SetEnd_X.UseVisualStyleBackColor = true;
            this.Button_SetEnd_X.Click += new System.EventHandler(this.Button_SetEnd_X_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "方向:";
            // 
            // Button_CountReset_X
            // 
            this.Button_CountReset_X.Location = new System.Drawing.Point(159, 45);
            this.Button_CountReset_X.Name = "Button_CountReset_X";
            this.Button_CountReset_X.Size = new System.Drawing.Size(64, 23);
            this.Button_CountReset_X.TabIndex = 15;
            this.Button_CountReset_X.Text = "重置";
            this.Button_CountReset_X.UseVisualStyleBackColor = true;
            this.Button_CountReset_X.Click += new System.EventHandler(this.Button_CountReset_X_Click);
            // 
            // Button_CloseBoard
            // 
            this.Button_CloseBoard.Enabled = false;
            this.Button_CloseBoard.Location = new System.Drawing.Point(148, 6);
            this.Button_CloseBoard.Name = "Button_CloseBoard";
            this.Button_CloseBoard.Size = new System.Drawing.Size(91, 23);
            this.Button_CloseBoard.TabIndex = 32;
            this.Button_CloseBoard.Text = "关闭板卡";
            this.Button_CloseBoard.UseVisualStyleBackColor = true;
            this.Button_CloseBoard.Click += new System.EventHandler(this.BtnCloseBoard_Click);
            // 
            // groupBox_AxisX
            // 
            this.groupBox_AxisX.Controls.Add(this.groupBox7);
            this.groupBox_AxisX.Controls.Add(this.button_Home_X);
            this.groupBox_AxisX.Controls.Add(this.label8);
            this.groupBox_AxisX.Controls.Add(this.groupBox8);
            this.groupBox_AxisX.Controls.Add(this.groupBox3);
            this.groupBox_AxisX.Controls.Add(this.Button_CountReset_X);
            this.groupBox_AxisX.Controls.Add(this.label_status_AxisX);
            this.groupBox_AxisX.Controls.Add(this.label1);
            this.groupBox_AxisX.Controls.Add(this.textBox_LoginCount_X);
            this.groupBox_AxisX.Controls.Add(this.button_ServerON_X);
            this.groupBox_AxisX.Controls.Add(this.button_AddToGp_X);
            this.groupBox_AxisX.Location = new System.Drawing.Point(292, 3);
            this.groupBox_AxisX.Name = "groupBox_AxisX";
            this.groupBox_AxisX.Size = new System.Drawing.Size(242, 373);
            this.groupBox_AxisX.TabIndex = 33;
            this.groupBox_AxisX.TabStop = false;
            this.groupBox_AxisX.Text = "X轴信息";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label18);
            this.groupBox7.Controls.Add(this.label17);
            this.groupBox7.Controls.Add(this.label16);
            this.groupBox7.Controls.Add(this.label15);
            this.groupBox7.Controls.Add(this.pictureBox_DI3_X);
            this.groupBox7.Controls.Add(this.Button_D07_X);
            this.groupBox7.Controls.Add(this.pictureBox_DI2_X);
            this.groupBox7.Controls.Add(this.Button_D06_X);
            this.groupBox7.Controls.Add(this.pictureBox_DI0_X);
            this.groupBox7.Controls.Add(this.Button_D05_X);
            this.groupBox7.Controls.Add(this.pictureBox_DI1_X);
            this.groupBox7.Controls.Add(this.Button_D04_X);
            this.groupBox7.Controls.Add(this.Picturebox_D05_X);
            this.groupBox7.Controls.Add(this.Picturebox_D06_X);
            this.groupBox7.Controls.Add(this.Picturebox_D07_X);
            this.groupBox7.Controls.Add(this.Picturebox_D04_X);
            this.groupBox7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox7.Location = new System.Drawing.Point(11, 238);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(219, 106);
            this.groupBox7.TabIndex = 29;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "DI/O";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label18.Location = new System.Drawing.Point(177, 79);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(23, 12);
            this.label18.TabIndex = 35;
            this.label18.Text = "DI3";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label17.Location = new System.Drawing.Point(124, 79);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(23, 12);
            this.label17.TabIndex = 35;
            this.label17.Text = "DI2";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label16.Location = new System.Drawing.Point(69, 79);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(23, 12);
            this.label16.TabIndex = 35;
            this.label16.Text = "DI1";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label15.Location = new System.Drawing.Point(16, 79);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(23, 12);
            this.label15.TabIndex = 35;
            this.label15.Text = "DI0";
            // 
            // pictureBox_DI3_X
            // 
            this.pictureBox_DI3_X.BackColor = System.Drawing.Color.Gray;
            this.pictureBox_DI3_X.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_DI3_X.Location = new System.Drawing.Point(176, 59);
            this.pictureBox_DI3_X.Name = "pictureBox_DI3_X";
            this.pictureBox_DI3_X.Size = new System.Drawing.Size(22, 15);
            this.pictureBox_DI3_X.TabIndex = 33;
            this.pictureBox_DI3_X.TabStop = false;
            // 
            // Button_D07_X
            // 
            this.Button_D07_X.Location = new System.Drawing.Point(166, 34);
            this.Button_D07_X.Name = "Button_D07_X";
            this.Button_D07_X.Size = new System.Drawing.Size(39, 20);
            this.Button_D07_X.TabIndex = 29;
            this.Button_D07_X.Text = "ERC";
            this.Button_D07_X.UseVisualStyleBackColor = true;
            // 
            // pictureBox_DI2_X
            // 
            this.pictureBox_DI2_X.BackColor = System.Drawing.Color.Gray;
            this.pictureBox_DI2_X.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_DI2_X.Location = new System.Drawing.Point(123, 59);
            this.pictureBox_DI2_X.Name = "pictureBox_DI2_X";
            this.pictureBox_DI2_X.Size = new System.Drawing.Size(22, 15);
            this.pictureBox_DI2_X.TabIndex = 32;
            this.pictureBox_DI2_X.TabStop = false;
            // 
            // Button_D06_X
            // 
            this.Button_D06_X.Location = new System.Drawing.Point(114, 34);
            this.Button_D06_X.Name = "Button_D06_X";
            this.Button_D06_X.Size = new System.Drawing.Size(39, 20);
            this.Button_D06_X.TabIndex = 28;
            this.Button_D06_X.Text = "SVON";
            this.Button_D06_X.UseVisualStyleBackColor = true;
            // 
            // pictureBox_DI0_X
            // 
            this.pictureBox_DI0_X.BackColor = System.Drawing.Color.Gray;
            this.pictureBox_DI0_X.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_DI0_X.Location = new System.Drawing.Point(16, 59);
            this.pictureBox_DI0_X.Name = "pictureBox_DI0_X";
            this.pictureBox_DI0_X.Size = new System.Drawing.Size(22, 15);
            this.pictureBox_DI0_X.TabIndex = 30;
            this.pictureBox_DI0_X.TabStop = false;
            // 
            // Button_D05_X
            // 
            this.Button_D05_X.Location = new System.Drawing.Point(62, 34);
            this.Button_D05_X.Name = "Button_D05_X";
            this.Button_D05_X.Size = new System.Drawing.Size(32, 20);
            this.Button_D05_X.TabIndex = 27;
            this.Button_D05_X.Text = "DO5";
            this.Button_D05_X.UseVisualStyleBackColor = true;
            // 
            // pictureBox_DI1_X
            // 
            this.pictureBox_DI1_X.BackColor = System.Drawing.Color.Gray;
            this.pictureBox_DI1_X.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_DI1_X.Location = new System.Drawing.Point(68, 59);
            this.pictureBox_DI1_X.Name = "pictureBox_DI1_X";
            this.pictureBox_DI1_X.Size = new System.Drawing.Size(22, 15);
            this.pictureBox_DI1_X.TabIndex = 31;
            this.pictureBox_DI1_X.TabStop = false;
            // 
            // Button_D04_X
            // 
            this.Button_D04_X.Location = new System.Drawing.Point(7, 33);
            this.Button_D04_X.Name = "Button_D04_X";
            this.Button_D04_X.Size = new System.Drawing.Size(39, 20);
            this.Button_D04_X.TabIndex = 26;
            this.Button_D04_X.Text = "拍照";
            this.Button_D04_X.UseVisualStyleBackColor = true;
            // 
            // Picturebox_D05_X
            // 
            this.Picturebox_D05_X.BackColor = System.Drawing.Color.Gray;
            this.Picturebox_D05_X.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Picturebox_D05_X.Location = new System.Drawing.Point(66, 19);
            this.Picturebox_D05_X.Name = "Picturebox_D05_X";
            this.Picturebox_D05_X.Size = new System.Drawing.Size(22, 15);
            this.Picturebox_D05_X.TabIndex = 7;
            this.Picturebox_D05_X.TabStop = false;
            // 
            // Picturebox_D06_X
            // 
            this.Picturebox_D06_X.BackColor = System.Drawing.Color.Gray;
            this.Picturebox_D06_X.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Picturebox_D06_X.Location = new System.Drawing.Point(122, 19);
            this.Picturebox_D06_X.Name = "Picturebox_D06_X";
            this.Picturebox_D06_X.Size = new System.Drawing.Size(22, 15);
            this.Picturebox_D06_X.TabIndex = 6;
            this.Picturebox_D06_X.TabStop = false;
            // 
            // Picturebox_D07_X
            // 
            this.Picturebox_D07_X.BackColor = System.Drawing.Color.Gray;
            this.Picturebox_D07_X.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Picturebox_D07_X.Location = new System.Drawing.Point(174, 19);
            this.Picturebox_D07_X.Name = "Picturebox_D07_X";
            this.Picturebox_D07_X.Size = new System.Drawing.Size(22, 15);
            this.Picturebox_D07_X.TabIndex = 5;
            this.Picturebox_D07_X.TabStop = false;
            // 
            // Picturebox_D04_X
            // 
            this.Picturebox_D04_X.BackColor = System.Drawing.Color.Gray;
            this.Picturebox_D04_X.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Picturebox_D04_X.Location = new System.Drawing.Point(15, 19);
            this.Picturebox_D04_X.Name = "Picturebox_D04_X";
            this.Picturebox_D04_X.Size = new System.Drawing.Size(22, 15);
            this.Picturebox_D04_X.TabIndex = 4;
            this.Picturebox_D04_X.TabStop = false;
            // 
            // button_Home_X
            // 
            this.button_Home_X.Location = new System.Drawing.Point(159, 19);
            this.button_Home_X.Name = "button_Home_X";
            this.button_Home_X.Size = new System.Drawing.Size(64, 23);
            this.button_Home_X.TabIndex = 28;
            this.button_Home_X.Text = "回原点";
            this.button_Home_X.UseVisualStyleBackColor = true;
            this.button_Home_X.Click += new System.EventHandler(this.button_Home_X_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "逻辑计数器:";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.numericUpDown_VelLow_X);
            this.groupBox8.Controls.Add(this.label24);
            this.groupBox8.Controls.Add(this.label23);
            this.groupBox8.Controls.Add(this.numericUpDown_VelHigh_X);
            this.groupBox8.Controls.Add(this.label14);
            this.groupBox8.Controls.Add(this.numericUpDown_Jerk_X);
            this.groupBox8.Controls.Add(this.numericUpDown_Dec_X);
            this.groupBox8.Controls.Add(this.label12);
            this.groupBox8.Controls.Add(this.button_UpdateSpdValue_X);
            this.groupBox8.Controls.Add(this.button_SpeedSet_X);
            this.groupBox8.ForeColor = System.Drawing.SystemColors.InfoText;
            this.groupBox8.Location = new System.Drawing.Point(8, 144);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(222, 89);
            this.groupBox8.TabIndex = 5;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "速度设定";
            // 
            // numericUpDown_VelLow_X
            // 
            this.numericUpDown_VelLow_X.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_VelLow_X.Location = new System.Drawing.Point(60, 41);
            this.numericUpDown_VelLow_X.Maximum = new decimal(new int[] {
            80000,
            0,
            0,
            0});
            this.numericUpDown_VelLow_X.Name = "numericUpDown_VelLow_X";
            this.numericUpDown_VelLow_X.Size = new System.Drawing.Size(56, 21);
            this.numericUpDown_VelLow_X.TabIndex = 15;
            this.numericUpDown_VelLow_X.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(127, 41);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(35, 12);
            this.label24.TabIndex = 13;
            this.label24.Text = "Jerk:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(131, 18);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(29, 12);
            this.label23.TabIndex = 13;
            this.label23.Text = "Dec:";
            // 
            // numericUpDown_VelHigh_X
            // 
            this.numericUpDown_VelHigh_X.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_VelHigh_X.Location = new System.Drawing.Point(60, 17);
            this.numericUpDown_VelHigh_X.Maximum = new decimal(new int[] {
            80000,
            0,
            0,
            0});
            this.numericUpDown_VelHigh_X.Name = "numericUpDown_VelHigh_X";
            this.numericUpDown_VelHigh_X.Size = new System.Drawing.Size(56, 21);
            this.numericUpDown_VelHigh_X.TabIndex = 15;
            this.numericUpDown_VelHigh_X.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(5, 19);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 13;
            this.label14.Text = "VelHigh:";
            // 
            // numericUpDown_Jerk_X
            // 
            this.numericUpDown_Jerk_X.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_Jerk_X.Location = new System.Drawing.Point(163, 39);
            this.numericUpDown_Jerk_X.Maximum = new decimal(new int[] {
            80000,
            0,
            0,
            0});
            this.numericUpDown_Jerk_X.Name = "numericUpDown_Jerk_X";
            this.numericUpDown_Jerk_X.Size = new System.Drawing.Size(56, 21);
            this.numericUpDown_Jerk_X.TabIndex = 15;
            this.numericUpDown_Jerk_X.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // numericUpDown_Dec_X
            // 
            this.numericUpDown_Dec_X.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_Dec_X.Location = new System.Drawing.Point(163, 15);
            this.numericUpDown_Dec_X.Maximum = new decimal(new int[] {
            80000,
            0,
            0,
            0});
            this.numericUpDown_Dec_X.Name = "numericUpDown_Dec_X";
            this.numericUpDown_Dec_X.Size = new System.Drawing.Size(56, 21);
            this.numericUpDown_Dec_X.TabIndex = 15;
            this.numericUpDown_Dec_X.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 12);
            this.label12.TabIndex = 13;
            this.label12.Text = "VelLow:";
            // 
            // button_UpdateSpdValue_X
            // 
            this.button_UpdateSpdValue_X.Location = new System.Drawing.Point(62, 64);
            this.button_UpdateSpdValue_X.Name = "button_UpdateSpdValue_X";
            this.button_UpdateSpdValue_X.Size = new System.Drawing.Size(75, 23);
            this.button_UpdateSpdValue_X.TabIndex = 12;
            this.button_UpdateSpdValue_X.Text = "更新参数";
            this.button_UpdateSpdValue_X.UseVisualStyleBackColor = true;
            this.button_UpdateSpdValue_X.Click += new System.EventHandler(this.button_UpdateSpdValue_X_Click);
            // 
            // button_SpeedSet_X
            // 
            this.button_SpeedSet_X.Location = new System.Drawing.Point(137, 64);
            this.button_SpeedSet_X.Name = "button_SpeedSet_X";
            this.button_SpeedSet_X.Size = new System.Drawing.Size(75, 23);
            this.button_SpeedSet_X.TabIndex = 12;
            this.button_SpeedSet_X.Text = "设置参数";
            this.button_SpeedSet_X.UseVisualStyleBackColor = true;
            this.button_SpeedSet_X.Click += new System.EventHandler(this.button_SpeedSet_X_Click);
            // 
            // label_status_AxisX
            // 
            this.label_status_AxisX.AutoSize = true;
            this.label_status_AxisX.BackColor = System.Drawing.Color.Orange;
            this.label_status_AxisX.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label_status_AxisX.Location = new System.Drawing.Point(50, 351);
            this.label_status_AxisX.Name = "label_status_AxisX";
            this.label_status_AxisX.Size = new System.Drawing.Size(29, 12);
            this.label_status_AxisX.TabIndex = 27;
            this.label_status_AxisX.Text = "----";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 351);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "状态:";
            // 
            // textBox_LoginCount_X
            // 
            this.textBox_LoginCount_X.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBox_LoginCount_X.Location = new System.Drawing.Point(79, 47);
            this.textBox_LoginCount_X.Name = "textBox_LoginCount_X";
            this.textBox_LoginCount_X.ReadOnly = true;
            this.textBox_LoginCount_X.Size = new System.Drawing.Size(74, 21);
            this.textBox_LoginCount_X.TabIndex = 10;
            // 
            // button_ServerON_X
            // 
            this.button_ServerON_X.Location = new System.Drawing.Point(6, 20);
            this.button_ServerON_X.Name = "button_ServerON_X";
            this.button_ServerON_X.Size = new System.Drawing.Size(64, 23);
            this.button_ServerON_X.TabIndex = 25;
            this.button_ServerON_X.Text = "伺服 ON";
            this.button_ServerON_X.UseVisualStyleBackColor = true;
            this.button_ServerON_X.Click += new System.EventHandler(this.button_ServerON_X_Click);
            // 
            // button_AddToGp_X
            // 
            this.button_AddToGp_X.Location = new System.Drawing.Point(78, 20);
            this.button_AddToGp_X.Name = "button_AddToGp_X";
            this.button_AddToGp_X.Size = new System.Drawing.Size(75, 23);
            this.button_AddToGp_X.TabIndex = 1;
            this.button_AddToGp_X.Text = "添加群组";
            this.button_AddToGp_X.UseVisualStyleBackColor = true;
            this.button_AddToGp_X.Click += new System.EventHandler(this.button_AddToGp_X_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Controls.Add(this.button_Home_Y);
            this.groupBox5.Controls.Add(this.Button_CountReset_Y);
            this.groupBox5.Controls.Add(this.groupBox9);
            this.groupBox5.Controls.Add(this.groupBox1);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label_status_AxisY);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.button_ServerOn_Y);
            this.groupBox5.Controls.Add(this.textBox_LoginCount_Y);
            this.groupBox5.Controls.Add(this.button_AddToGp_Y);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox5.Location = new System.Drawing.Point(541, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(257, 370);
            this.groupBox5.TabIndex = 33;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Y轴信息";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label22);
            this.groupBox6.Controls.Add(this.label21);
            this.groupBox6.Controls.Add(this.label20);
            this.groupBox6.Controls.Add(this.pictureBox_DI3_Y);
            this.groupBox6.Controls.Add(this.label19);
            this.groupBox6.Controls.Add(this.Button_D07_Y);
            this.groupBox6.Controls.Add(this.pictureBox_DI2_Y);
            this.groupBox6.Controls.Add(this.Button_D06_Y);
            this.groupBox6.Controls.Add(this.pictureBox_DI0_Y);
            this.groupBox6.Controls.Add(this.Button_D05_Y);
            this.groupBox6.Controls.Add(this.pictureBox_DI1_Y);
            this.groupBox6.Controls.Add(this.Button_D04_Y);
            this.groupBox6.Controls.Add(this.Picturebox_D05_Y);
            this.groupBox6.Controls.Add(this.Picturebox_D06_Y);
            this.groupBox6.Controls.Add(this.Picturebox_D07_Y);
            this.groupBox6.Controls.Add(this.Picturebox_D04_Y);
            this.groupBox6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox6.Location = new System.Drawing.Point(10, 234);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(219, 101);
            this.groupBox6.TabIndex = 30;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "DI/O";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label22.Location = new System.Drawing.Point(177, 78);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(23, 12);
            this.label22.TabIndex = 35;
            this.label22.Text = "DI3";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label21.Location = new System.Drawing.Point(124, 78);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(23, 12);
            this.label21.TabIndex = 35;
            this.label21.Text = "DI2";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label20.Location = new System.Drawing.Point(69, 78);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(23, 12);
            this.label20.TabIndex = 35;
            this.label20.Text = "DI1";
            // 
            // pictureBox_DI3_Y
            // 
            this.pictureBox_DI3_Y.BackColor = System.Drawing.Color.Gray;
            this.pictureBox_DI3_Y.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_DI3_Y.Location = new System.Drawing.Point(176, 59);
            this.pictureBox_DI3_Y.Name = "pictureBox_DI3_Y";
            this.pictureBox_DI3_Y.Size = new System.Drawing.Size(22, 15);
            this.pictureBox_DI3_Y.TabIndex = 33;
            this.pictureBox_DI3_Y.TabStop = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label19.Location = new System.Drawing.Point(16, 78);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(23, 12);
            this.label19.TabIndex = 35;
            this.label19.Text = "DI0";
            // 
            // Button_D07_Y
            // 
            this.Button_D07_Y.Location = new System.Drawing.Point(167, 34);
            this.Button_D07_Y.Name = "Button_D07_Y";
            this.Button_D07_Y.Size = new System.Drawing.Size(39, 20);
            this.Button_D07_Y.TabIndex = 29;
            this.Button_D07_Y.Text = "ERC";
            this.Button_D07_Y.UseVisualStyleBackColor = true;
            // 
            // pictureBox_DI2_Y
            // 
            this.pictureBox_DI2_Y.BackColor = System.Drawing.Color.Gray;
            this.pictureBox_DI2_Y.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_DI2_Y.Location = new System.Drawing.Point(123, 59);
            this.pictureBox_DI2_Y.Name = "pictureBox_DI2_Y";
            this.pictureBox_DI2_Y.Size = new System.Drawing.Size(22, 15);
            this.pictureBox_DI2_Y.TabIndex = 32;
            this.pictureBox_DI2_Y.TabStop = false;
            // 
            // Button_D06_Y
            // 
            this.Button_D06_Y.Location = new System.Drawing.Point(118, 34);
            this.Button_D06_Y.Name = "Button_D06_Y";
            this.Button_D06_Y.Size = new System.Drawing.Size(32, 20);
            this.Button_D06_Y.TabIndex = 28;
            this.Button_D06_Y.Text = "DO6";
            this.Button_D06_Y.UseVisualStyleBackColor = true;
            // 
            // pictureBox_DI0_Y
            // 
            this.pictureBox_DI0_Y.BackColor = System.Drawing.Color.Gray;
            this.pictureBox_DI0_Y.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_DI0_Y.Location = new System.Drawing.Point(16, 59);
            this.pictureBox_DI0_Y.Name = "pictureBox_DI0_Y";
            this.pictureBox_DI0_Y.Size = new System.Drawing.Size(22, 15);
            this.pictureBox_DI0_Y.TabIndex = 30;
            this.pictureBox_DI0_Y.TabStop = false;
            // 
            // Button_D05_Y
            // 
            this.Button_D05_Y.Location = new System.Drawing.Point(62, 34);
            this.Button_D05_Y.Name = "Button_D05_Y";
            this.Button_D05_Y.Size = new System.Drawing.Size(32, 20);
            this.Button_D05_Y.TabIndex = 27;
            this.Button_D05_Y.Text = "DO5";
            this.Button_D05_Y.UseVisualStyleBackColor = true;
            // 
            // pictureBox_DI1_Y
            // 
            this.pictureBox_DI1_Y.BackColor = System.Drawing.Color.Gray;
            this.pictureBox_DI1_Y.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_DI1_Y.Location = new System.Drawing.Point(68, 59);
            this.pictureBox_DI1_Y.Name = "pictureBox_DI1_Y";
            this.pictureBox_DI1_Y.Size = new System.Drawing.Size(22, 15);
            this.pictureBox_DI1_Y.TabIndex = 31;
            this.pictureBox_DI1_Y.TabStop = false;
            // 
            // Button_D04_Y
            // 
            this.Button_D04_Y.Location = new System.Drawing.Point(6, 34);
            this.Button_D04_Y.Name = "Button_D04_Y";
            this.Button_D04_Y.Size = new System.Drawing.Size(39, 20);
            this.Button_D04_Y.TabIndex = 26;
            this.Button_D04_Y.Text = "SCAN";
            this.Button_D04_Y.UseVisualStyleBackColor = true;
            // 
            // Picturebox_D05_Y
            // 
            this.Picturebox_D05_Y.BackColor = System.Drawing.Color.Gray;
            this.Picturebox_D05_Y.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Picturebox_D05_Y.Location = new System.Drawing.Point(66, 19);
            this.Picturebox_D05_Y.Name = "Picturebox_D05_Y";
            this.Picturebox_D05_Y.Size = new System.Drawing.Size(22, 15);
            this.Picturebox_D05_Y.TabIndex = 7;
            this.Picturebox_D05_Y.TabStop = false;
            // 
            // Picturebox_D06_Y
            // 
            this.Picturebox_D06_Y.BackColor = System.Drawing.Color.Gray;
            this.Picturebox_D06_Y.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Picturebox_D06_Y.Location = new System.Drawing.Point(122, 19);
            this.Picturebox_D06_Y.Name = "Picturebox_D06_Y";
            this.Picturebox_D06_Y.Size = new System.Drawing.Size(22, 15);
            this.Picturebox_D06_Y.TabIndex = 6;
            this.Picturebox_D06_Y.TabStop = false;
            // 
            // Picturebox_D07_Y
            // 
            this.Picturebox_D07_Y.BackColor = System.Drawing.Color.Gray;
            this.Picturebox_D07_Y.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Picturebox_D07_Y.Location = new System.Drawing.Point(174, 19);
            this.Picturebox_D07_Y.Name = "Picturebox_D07_Y";
            this.Picturebox_D07_Y.Size = new System.Drawing.Size(22, 15);
            this.Picturebox_D07_Y.TabIndex = 5;
            this.Picturebox_D07_Y.TabStop = false;
            // 
            // Picturebox_D04_Y
            // 
            this.Picturebox_D04_Y.BackColor = System.Drawing.Color.Gray;
            this.Picturebox_D04_Y.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Picturebox_D04_Y.Location = new System.Drawing.Point(15, 19);
            this.Picturebox_D04_Y.Name = "Picturebox_D04_Y";
            this.Picturebox_D04_Y.Size = new System.Drawing.Size(22, 15);
            this.Picturebox_D04_Y.TabIndex = 4;
            this.Picturebox_D04_Y.TabStop = false;
            // 
            // button_Home_Y
            // 
            this.button_Home_Y.Location = new System.Drawing.Point(159, 16);
            this.button_Home_Y.Name = "button_Home_Y";
            this.button_Home_Y.Size = new System.Drawing.Size(64, 23);
            this.button_Home_Y.TabIndex = 28;
            this.button_Home_Y.Text = "回原点";
            this.button_Home_Y.UseVisualStyleBackColor = true;
            this.button_Home_Y.Click += new System.EventHandler(this.button_Home_Y_Click);
            // 
            // Button_CountReset_Y
            // 
            this.Button_CountReset_Y.Location = new System.Drawing.Point(159, 41);
            this.Button_CountReset_Y.Name = "Button_CountReset_Y";
            this.Button_CountReset_Y.Size = new System.Drawing.Size(62, 23);
            this.Button_CountReset_Y.TabIndex = 28;
            this.Button_CountReset_Y.Text = "重置";
            this.Button_CountReset_Y.UseVisualStyleBackColor = true;
            this.Button_CountReset_Y.Click += new System.EventHandler(this.Button_CountReset_Y_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.numericUpDown_VelLow_Y);
            this.groupBox9.Controls.Add(this.label25);
            this.groupBox9.Controls.Add(this.label26);
            this.groupBox9.Controls.Add(this.numericUpDown_VelHigh_Y);
            this.groupBox9.Controls.Add(this.label27);
            this.groupBox9.Controls.Add(this.numericUpDown_Jerk_Y);
            this.groupBox9.Controls.Add(this.numericUpDown_Dec_Y);
            this.groupBox9.Controls.Add(this.label28);
            this.groupBox9.Controls.Add(this.button_UpdateSpdValue_Y);
            this.groupBox9.Controls.Add(this.button_SpeedSet_Y);
            this.groupBox9.ForeColor = System.Drawing.SystemColors.InfoText;
            this.groupBox9.Location = new System.Drawing.Point(10, 143);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(222, 89);
            this.groupBox9.TabIndex = 5;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "速度设定";
            // 
            // numericUpDown_VelLow_Y
            // 
            this.numericUpDown_VelLow_Y.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_VelLow_Y.Location = new System.Drawing.Point(60, 41);
            this.numericUpDown_VelLow_Y.Maximum = new decimal(new int[] {
            80000,
            0,
            0,
            0});
            this.numericUpDown_VelLow_Y.Name = "numericUpDown_VelLow_Y";
            this.numericUpDown_VelLow_Y.Size = new System.Drawing.Size(56, 21);
            this.numericUpDown_VelLow_Y.TabIndex = 15;
            this.numericUpDown_VelLow_Y.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(127, 41);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(35, 12);
            this.label25.TabIndex = 13;
            this.label25.Text = "Jerk:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(131, 18);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(29, 12);
            this.label26.TabIndex = 13;
            this.label26.Text = "Dec:";
            // 
            // numericUpDown_VelHigh_Y
            // 
            this.numericUpDown_VelHigh_Y.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_VelHigh_Y.Location = new System.Drawing.Point(60, 17);
            this.numericUpDown_VelHigh_Y.Maximum = new decimal(new int[] {
            80000,
            0,
            0,
            0});
            this.numericUpDown_VelHigh_Y.Name = "numericUpDown_VelHigh_Y";
            this.numericUpDown_VelHigh_Y.Size = new System.Drawing.Size(56, 21);
            this.numericUpDown_VelHigh_Y.TabIndex = 15;
            this.numericUpDown_VelHigh_Y.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(5, 19);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(53, 12);
            this.label27.TabIndex = 13;
            this.label27.Text = "VelHigh:";
            // 
            // numericUpDown_Jerk_Y
            // 
            this.numericUpDown_Jerk_Y.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_Jerk_Y.Location = new System.Drawing.Point(163, 39);
            this.numericUpDown_Jerk_Y.Maximum = new decimal(new int[] {
            80000,
            0,
            0,
            0});
            this.numericUpDown_Jerk_Y.Name = "numericUpDown_Jerk_Y";
            this.numericUpDown_Jerk_Y.Size = new System.Drawing.Size(56, 21);
            this.numericUpDown_Jerk_Y.TabIndex = 15;
            this.numericUpDown_Jerk_Y.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // numericUpDown_Dec_Y
            // 
            this.numericUpDown_Dec_Y.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_Dec_Y.Location = new System.Drawing.Point(163, 15);
            this.numericUpDown_Dec_Y.Maximum = new decimal(new int[] {
            80000,
            0,
            0,
            0});
            this.numericUpDown_Dec_Y.Name = "numericUpDown_Dec_Y";
            this.numericUpDown_Dec_Y.Size = new System.Drawing.Size(56, 21);
            this.numericUpDown_Dec_Y.TabIndex = 15;
            this.numericUpDown_Dec_Y.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(7, 42);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(47, 12);
            this.label28.TabIndex = 13;
            this.label28.Text = "VelLow:";
            // 
            // button_UpdateSpdValue_Y
            // 
            this.button_UpdateSpdValue_Y.Location = new System.Drawing.Point(62, 64);
            this.button_UpdateSpdValue_Y.Name = "button_UpdateSpdValue_Y";
            this.button_UpdateSpdValue_Y.Size = new System.Drawing.Size(75, 23);
            this.button_UpdateSpdValue_Y.TabIndex = 12;
            this.button_UpdateSpdValue_Y.Text = "更新参数";
            this.button_UpdateSpdValue_Y.UseVisualStyleBackColor = true;
            this.button_UpdateSpdValue_Y.Click += new System.EventHandler(this.button_UpdateSpdValue_Y_Click);
            // 
            // button_SpeedSet_Y
            // 
            this.button_SpeedSet_Y.Location = new System.Drawing.Point(137, 64);
            this.button_SpeedSet_Y.Name = "button_SpeedSet_Y";
            this.button_SpeedSet_Y.Size = new System.Drawing.Size(75, 23);
            this.button_SpeedSet_Y.TabIndex = 12;
            this.button_SpeedSet_Y.Text = "设置参数";
            this.button_SpeedSet_Y.UseVisualStyleBackColor = true;
            this.button_SpeedSet_Y.Click += new System.EventHandler(this.button_SpeedSet_Y_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDown_MovDis_Y);
            this.groupBox1.Controls.Add(this.comboBox_MoveDirect_Y);
            this.groupBox1.Controls.Add(this.label39);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.Button_ClearEnd_Y);
            this.groupBox1.Controls.Add(this.Button_SetEnd_Y);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.groupBox1.Location = new System.Drawing.Point(9, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 70);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "运动设置";
            // 
            // numericUpDown_MovDis_Y
            // 
            this.numericUpDown_MovDis_Y.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_MovDis_Y.Location = new System.Drawing.Point(67, 43);
            this.numericUpDown_MovDis_Y.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_MovDis_Y.Name = "numericUpDown_MovDis_Y";
            this.numericUpDown_MovDis_Y.Size = new System.Drawing.Size(57, 21);
            this.numericUpDown_MovDis_Y.TabIndex = 16;
            this.numericUpDown_MovDis_Y.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // comboBox_MoveDirect_Y
            // 
            this.comboBox_MoveDirect_Y.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_MoveDirect_Y.FormattingEnabled = true;
            this.comboBox_MoveDirect_Y.Items.AddRange(new object[] {
            "正向",
            "反向"});
            this.comboBox_MoveDirect_Y.Location = new System.Drawing.Point(67, 19);
            this.comboBox_MoveDirect_Y.Name = "comboBox_MoveDirect_Y";
            this.comboBox_MoveDirect_Y.Size = new System.Drawing.Size(81, 20);
            this.comboBox_MoveDirect_Y.TabIndex = 14;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(130, 47);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(23, 12);
            this.label39.TabIndex = 13;
            this.label39.Text = "pps";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "终点值:";
            // 
            // Button_ClearEnd_Y
            // 
            this.Button_ClearEnd_Y.Location = new System.Drawing.Point(154, 42);
            this.Button_ClearEnd_Y.Name = "Button_ClearEnd_Y";
            this.Button_ClearEnd_Y.Size = new System.Drawing.Size(64, 23);
            this.Button_ClearEnd_Y.TabIndex = 12;
            this.Button_ClearEnd_Y.Text = "清除";
            this.Button_ClearEnd_Y.UseVisualStyleBackColor = true;
            this.Button_ClearEnd_Y.Click += new System.EventHandler(this.Button_ClearEnd_Click);
            // 
            // Button_SetEnd_Y
            // 
            this.Button_SetEnd_Y.Location = new System.Drawing.Point(154, 17);
            this.Button_SetEnd_Y.Name = "Button_SetEnd_Y";
            this.Button_SetEnd_Y.Size = new System.Drawing.Size(64, 23);
            this.Button_SetEnd_Y.TabIndex = 9;
            this.Button_SetEnd_Y.Text = "设置终点";
            this.Button_SetEnd_Y.UseVisualStyleBackColor = true;
            this.Button_SetEnd_Y.Click += new System.EventHandler(this.Button_SetEnd_Y_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 12);
            this.label11.TabIndex = 11;
            this.label11.Text = "方向:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "逻辑计数器:";
            // 
            // label_status_AxisY
            // 
            this.label_status_AxisY.AutoSize = true;
            this.label_status_AxisY.BackColor = System.Drawing.Color.Orange;
            this.label_status_AxisY.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label_status_AxisY.Location = new System.Drawing.Point(49, 347);
            this.label_status_AxisY.Name = "label_status_AxisY";
            this.label_status_AxisY.Size = new System.Drawing.Size(29, 12);
            this.label_status_AxisY.TabIndex = 27;
            this.label_status_AxisY.Text = "----";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 347);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 26;
            this.label7.Text = "状态:";
            // 
            // button_ServerOn_Y
            // 
            this.button_ServerOn_Y.Location = new System.Drawing.Point(6, 17);
            this.button_ServerOn_Y.Name = "button_ServerOn_Y";
            this.button_ServerOn_Y.Size = new System.Drawing.Size(64, 23);
            this.button_ServerOn_Y.TabIndex = 26;
            this.button_ServerOn_Y.Text = "伺服 ON";
            this.button_ServerOn_Y.UseVisualStyleBackColor = true;
            this.button_ServerOn_Y.Click += new System.EventHandler(this.button_ServerOn_Y_Click);
            // 
            // textBox_LoginCount_Y
            // 
            this.textBox_LoginCount_Y.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBox_LoginCount_Y.Location = new System.Drawing.Point(79, 43);
            this.textBox_LoginCount_Y.Name = "textBox_LoginCount_Y";
            this.textBox_LoginCount_Y.ReadOnly = true;
            this.textBox_LoginCount_Y.Size = new System.Drawing.Size(74, 21);
            this.textBox_LoginCount_Y.TabIndex = 10;
            // 
            // button_AddToGp_Y
            // 
            this.button_AddToGp_Y.Location = new System.Drawing.Point(78, 16);
            this.button_AddToGp_Y.Name = "button_AddToGp_Y";
            this.button_AddToGp_Y.Size = new System.Drawing.Size(75, 23);
            this.button_AddToGp_Y.TabIndex = 1;
            this.button_AddToGp_Y.Text = "添加群组";
            this.button_AddToGp_Y.UseVisualStyleBackColor = true;
            this.button_AddToGp_Y.Click += new System.EventHandler(this.button_AddToGp_Y_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // listBoxAxesInGp
            // 
            this.listBoxAxesInGp.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxAxesInGp.FormattingEnabled = true;
            this.listBoxAxesInGp.ItemHeight = 12;
            this.listBoxAxesInGp.Location = new System.Drawing.Point(6, 30);
            this.listBoxAxesInGp.Name = "listBoxAxesInGp";
            this.listBoxAxesInGp.Size = new System.Drawing.Size(105, 76);
            this.listBoxAxesInGp.TabIndex = 30;
            // 
            // listBoxEndPoint
            // 
            this.listBoxEndPoint.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxEndPoint.FormattingEnabled = true;
            this.listBoxEndPoint.ItemHeight = 12;
            this.listBoxEndPoint.Location = new System.Drawing.Point(111, 30);
            this.listBoxEndPoint.Name = "listBoxEndPoint";
            this.listBoxEndPoint.Size = new System.Drawing.Size(105, 76);
            this.listBoxEndPoint.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "群组(轴):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(115, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 31;
            this.label5.Text = "标终点队列:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.listBoxEndPoint);
            this.groupBox2.Controls.Add(this.listBoxAxesInGp);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(41, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(222, 110);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "轴群组信息/目标终点队列";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "主轴ID:";
            // 
            // BtnResetErr
            // 
            this.BtnResetErr.Location = new System.Drawing.Point(104, 281);
            this.BtnResetErr.Name = "BtnResetErr";
            this.BtnResetErr.Size = new System.Drawing.Size(105, 23);
            this.BtnResetErr.TabIndex = 15;
            this.BtnResetErr.Text = "复位异常";
            this.BtnResetErr.UseVisualStyleBackColor = true;
            this.BtnResetErr.Click += new System.EventHandler(this.BtnResetErr_Click);
            // 
            // textBoxMasterID
            // 
            this.textBoxMasterID.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxMasterID.Location = new System.Drawing.Point(107, 237);
            this.textBoxMasterID.Name = "textBoxMasterID";
            this.textBoxMasterID.ReadOnly = true;
            this.textBoxMasterID.Size = new System.Drawing.Size(75, 21);
            this.textBoxMasterID.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(44, 264);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 12);
            this.label13.TabIndex = 16;
            this.label13.Text = "群组状态:";
            // 
            // textBoxGpState
            // 
            this.textBoxGpState.Location = new System.Drawing.Point(106, 259);
            this.textBoxGpState.Name = "textBoxGpState";
            this.textBoxGpState.ReadOnly = true;
            this.textBoxGpState.Size = new System.Drawing.Size(120, 21);
            this.textBoxGpState.TabIndex = 17;
            // 
            // Button_Move
            // 
            this.Button_Move.Location = new System.Drawing.Point(53, 346);
            this.Button_Move.Name = "Button_Move";
            this.Button_Move.Size = new System.Drawing.Size(75, 23);
            this.Button_Move.TabIndex = 24;
            this.Button_Move.Text = "运动";
            this.Button_Move.UseVisualStyleBackColor = true;
            this.Button_Move.Click += new System.EventHandler(this.Button_Move_Click);
            // 
            // radioButtonAbs
            // 
            this.radioButtonAbs.AutoSize = true;
            this.radioButtonAbs.Location = new System.Drawing.Point(12, 15);
            this.radioButtonAbs.Name = "radioButtonAbs";
            this.radioButtonAbs.Size = new System.Drawing.Size(71, 16);
            this.radioButtonAbs.TabIndex = 6;
            this.radioButtonAbs.Text = "Absolute";
            this.radioButtonAbs.UseVisualStyleBackColor = true;
            // 
            // radioButtonRel
            // 
            this.radioButtonRel.AutoSize = true;
            this.radioButtonRel.Checked = true;
            this.radioButtonRel.Location = new System.Drawing.Point(134, 15);
            this.radioButtonRel.Name = "radioButtonRel";
            this.radioButtonRel.Size = new System.Drawing.Size(71, 16);
            this.radioButtonRel.TabIndex = 7;
            this.radioButtonRel.TabStop = true;
            this.radioButtonRel.Text = "Relative";
            this.radioButtonRel.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButtonRel);
            this.groupBox4.Controls.Add(this.radioButtonAbs);
            this.groupBox4.ForeColor = System.Drawing.SystemColors.InfoText;
            this.groupBox4.Location = new System.Drawing.Point(42, 303);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(221, 37);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "运动模式";
            // 
            // Button_Stop
            // 
            this.Button_Stop.Location = new System.Drawing.Point(180, 346);
            this.Button_Stop.Name = "Button_Stop";
            this.Button_Stop.Size = new System.Drawing.Size(75, 23);
            this.Button_Stop.TabIndex = 25;
            this.Button_Stop.Text = "停止";
            this.Button_Stop.UseVisualStyleBackColor = true;
            this.Button_Stop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.numericUpDown_VelLow_gp);
            this.groupBox10.Controls.Add(this.label29);
            this.groupBox10.Controls.Add(this.label30);
            this.groupBox10.Controls.Add(this.numericUpDown_VelHigh_gp);
            this.groupBox10.Controls.Add(this.label31);
            this.groupBox10.Controls.Add(this.numericUpDown_Acc_gp);
            this.groupBox10.Controls.Add(this.numericUpDown_Dec_gp);
            this.groupBox10.Controls.Add(this.label32);
            this.groupBox10.Controls.Add(this.button_UpdateGpSpdValue);
            this.groupBox10.Controls.Add(this.button_SpeedSet_gp);
            this.groupBox10.ForeColor = System.Drawing.SystemColors.InfoText;
            this.groupBox10.Location = new System.Drawing.Point(41, 146);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(222, 89);
            this.groupBox10.TabIndex = 5;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "群组速度设定";
            // 
            // numericUpDown_VelLow_gp
            // 
            this.numericUpDown_VelLow_gp.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_VelLow_gp.Location = new System.Drawing.Point(57, 41);
            this.numericUpDown_VelLow_gp.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown_VelLow_gp.Name = "numericUpDown_VelLow_gp";
            this.numericUpDown_VelLow_gp.Size = new System.Drawing.Size(65, 21);
            this.numericUpDown_VelLow_gp.TabIndex = 15;
            this.numericUpDown_VelLow_gp.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(128, 41);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(29, 12);
            this.label29.TabIndex = 13;
            this.label29.Text = "Acc:";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(128, 18);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(29, 12);
            this.label30.TabIndex = 13;
            this.label30.Text = "Dec:";
            // 
            // numericUpDown_VelHigh_gp
            // 
            this.numericUpDown_VelHigh_gp.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_VelHigh_gp.Location = new System.Drawing.Point(57, 17);
            this.numericUpDown_VelHigh_gp.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown_VelHigh_gp.Name = "numericUpDown_VelHigh_gp";
            this.numericUpDown_VelHigh_gp.Size = new System.Drawing.Size(65, 21);
            this.numericUpDown_VelHigh_gp.TabIndex = 15;
            this.numericUpDown_VelHigh_gp.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(5, 19);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(53, 12);
            this.label31.TabIndex = 13;
            this.label31.Text = "VelHigh:";
            // 
            // numericUpDown_Acc_gp
            // 
            this.numericUpDown_Acc_gp.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_Acc_gp.Location = new System.Drawing.Point(158, 39);
            this.numericUpDown_Acc_gp.Maximum = new decimal(new int[] {
            80000,
            0,
            0,
            0});
            this.numericUpDown_Acc_gp.Name = "numericUpDown_Acc_gp";
            this.numericUpDown_Acc_gp.Size = new System.Drawing.Size(56, 21);
            this.numericUpDown_Acc_gp.TabIndex = 15;
            this.numericUpDown_Acc_gp.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // numericUpDown_Dec_gp
            // 
            this.numericUpDown_Dec_gp.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_Dec_gp.Location = new System.Drawing.Point(158, 15);
            this.numericUpDown_Dec_gp.Maximum = new decimal(new int[] {
            80000,
            0,
            0,
            0});
            this.numericUpDown_Dec_gp.Name = "numericUpDown_Dec_gp";
            this.numericUpDown_Dec_gp.Size = new System.Drawing.Size(56, 21);
            this.numericUpDown_Dec_gp.TabIndex = 15;
            this.numericUpDown_Dec_gp.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(7, 42);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(47, 12);
            this.label32.TabIndex = 13;
            this.label32.Text = "VelLow:";
            // 
            // button_UpdateGpSpdValue
            // 
            this.button_UpdateGpSpdValue.Location = new System.Drawing.Point(62, 64);
            this.button_UpdateGpSpdValue.Name = "button_UpdateGpSpdValue";
            this.button_UpdateGpSpdValue.Size = new System.Drawing.Size(75, 23);
            this.button_UpdateGpSpdValue.TabIndex = 12;
            this.button_UpdateGpSpdValue.Text = "更新参数";
            this.button_UpdateGpSpdValue.UseVisualStyleBackColor = true;
            this.button_UpdateGpSpdValue.Click += new System.EventHandler(this.button_UpdateGPSpeedValue_Click);
            // 
            // button_SpeedSet_gp
            // 
            this.button_SpeedSet_gp.Location = new System.Drawing.Point(137, 64);
            this.button_SpeedSet_gp.Name = "button_SpeedSet_gp";
            this.button_SpeedSet_gp.Size = new System.Drawing.Size(75, 23);
            this.button_SpeedSet_gp.TabIndex = 12;
            this.button_SpeedSet_gp.Text = "设置参数";
            this.button_SpeedSet_gp.UseVisualStyleBackColor = true;
            this.button_SpeedSet_gp.Click += new System.EventHandler(this.button_SpeedSet_gp_Click);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.pictureBox_ProxScan);
            this.groupBox11.Controls.Add(this.pictureBox_ShtSNScan);
            this.groupBox11.Controls.Add(this.pictureBox_ledWhite);
            this.groupBox11.Controls.Add(this.label36);
            this.groupBox11.Controls.Add(this.pictureBox_ledRed);
            this.groupBox11.Controls.Add(this.label35);
            this.groupBox11.Controls.Add(this.label34);
            this.groupBox11.Controls.Add(this.button_CCDTrigger);
            this.groupBox11.Controls.Add(this.button_LedRed);
            this.groupBox11.Controls.Add(this.button_LedWhite);
            this.groupBox11.Controls.Add(this.label33);
            this.groupBox11.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox11.Location = new System.Drawing.Point(13, 378);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(473, 82);
            this.groupBox11.TabIndex = 33;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "光源控制";
            this.groupBox11.Visible = false;
            // 
            // pictureBox_ProxScan
            // 
            this.pictureBox_ProxScan.BackColor = System.Drawing.Color.Gray;
            this.pictureBox_ProxScan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_ProxScan.Location = new System.Drawing.Point(260, 43);
            this.pictureBox_ProxScan.Name = "pictureBox_ProxScan";
            this.pictureBox_ProxScan.Size = new System.Drawing.Size(34, 15);
            this.pictureBox_ProxScan.TabIndex = 7;
            this.pictureBox_ProxScan.TabStop = false;
            // 
            // pictureBox_ShtSNScan
            // 
            this.pictureBox_ShtSNScan.BackColor = System.Drawing.Color.Gray;
            this.pictureBox_ShtSNScan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_ShtSNScan.Location = new System.Drawing.Point(260, 22);
            this.pictureBox_ShtSNScan.Name = "pictureBox_ShtSNScan";
            this.pictureBox_ShtSNScan.Size = new System.Drawing.Size(34, 15);
            this.pictureBox_ShtSNScan.TabIndex = 7;
            this.pictureBox_ShtSNScan.TabStop = false;
            // 
            // pictureBox_ledWhite
            // 
            this.pictureBox_ledWhite.BackColor = System.Drawing.Color.Gray;
            this.pictureBox_ledWhite.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_ledWhite.Location = new System.Drawing.Point(68, 44);
            this.pictureBox_ledWhite.Name = "pictureBox_ledWhite";
            this.pictureBox_ledWhite.Size = new System.Drawing.Size(34, 15);
            this.pictureBox_ledWhite.TabIndex = 7;
            this.pictureBox_ledWhite.TabStop = false;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(153, 45);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(83, 12);
            this.label36.TabIndex = 13;
            this.label36.Text = "开始PROX照合:";
            // 
            // pictureBox_ledRed
            // 
            this.pictureBox_ledRed.BackColor = System.Drawing.Color.Gray;
            this.pictureBox_ledRed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_ledRed.Location = new System.Drawing.Point(68, 24);
            this.pictureBox_ledRed.Name = "pictureBox_ledRed";
            this.pictureBox_ledRed.Size = new System.Drawing.Size(34, 15);
            this.pictureBox_ledRed.TabIndex = 4;
            this.pictureBox_ledRed.TabStop = false;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(153, 24);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(101, 12);
            this.label35.TabIndex = 13;
            this.label35.Text = "开始扫Sheet条码:";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(6, 47);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(59, 12);
            this.label34.TabIndex = 13;
            this.label34.Text = "白色光源:";
            // 
            // button_CCDTrigger
            // 
            this.button_CCDTrigger.Location = new System.Drawing.Point(317, 22);
            this.button_CCDTrigger.Name = "button_CCDTrigger";
            this.button_CCDTrigger.Size = new System.Drawing.Size(86, 35);
            this.button_CCDTrigger.TabIndex = 27;
            this.button_CCDTrigger.Text = "CCD触发";
            this.button_CCDTrigger.UseVisualStyleBackColor = true;
            this.button_CCDTrigger.Click += new System.EventHandler(this.button_CCDTrigger_Click);
            // 
            // button_LedRed
            // 
            this.button_LedRed.Location = new System.Drawing.Point(109, 21);
            this.button_LedRed.Name = "button_LedRed";
            this.button_LedRed.Size = new System.Drawing.Size(37, 20);
            this.button_LedRed.TabIndex = 26;
            this.button_LedRed.Text = "ON";
            this.button_LedRed.UseVisualStyleBackColor = true;
            this.button_LedRed.Click += new System.EventHandler(this.button_LedRed_Click);
            // 
            // button_LedWhite
            // 
            this.button_LedWhite.Location = new System.Drawing.Point(109, 42);
            this.button_LedWhite.Name = "button_LedWhite";
            this.button_LedWhite.Size = new System.Drawing.Size(37, 20);
            this.button_LedWhite.TabIndex = 27;
            this.button_LedWhite.Text = "ON";
            this.button_LedWhite.UseVisualStyleBackColor = true;
            this.button_LedWhite.Click += new System.EventHandler(this.button_LedWhite_Click);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(5, 24);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(59, 12);
            this.label33.TabIndex = 11;
            this.label33.Text = "红色光源:";
            // 
            // btn_refAxisZ
            // 
            this.btn_refAxisZ.Location = new System.Drawing.Point(122, 55);
            this.btn_refAxisZ.Name = "btn_refAxisZ";
            this.btn_refAxisZ.Size = new System.Drawing.Size(75, 23);
            this.btn_refAxisZ.TabIndex = 29;
            this.btn_refAxisZ.Text = "Z轴参考点";
            this.btn_refAxisZ.UseVisualStyleBackColor = true;
            this.btn_refAxisZ.Visible = false;
            this.btn_refAxisZ.Click += new System.EventHandler(this.btn_refAxisZ_Click);
            // 
            // groupBox_z
            // 
            this.groupBox_z.Controls.Add(this.groupBox14);
            this.groupBox_z.Controls.Add(this.label_status_AxisZ);
            this.groupBox_z.Controls.Add(this.label48);
            this.groupBox_z.Controls.Add(this.button_Home_Z);
            this.groupBox_z.Controls.Add(this.Button_CountReset_Z);
            this.groupBox_z.Controls.Add(this.label46);
            this.groupBox_z.Controls.Add(this.button_ServerOn_Z);
            this.groupBox_z.Controls.Add(this.textBox_LoginCount_Z);
            this.groupBox_z.Controls.Add(this.button_AddToGp_Z);
            this.groupBox_z.Controls.Add(this.groupBox13);
            this.groupBox_z.Controls.Add(this.groupBox12);
            this.groupBox_z.Location = new System.Drawing.Point(804, 3);
            this.groupBox_z.Name = "groupBox_z";
            this.groupBox_z.Size = new System.Drawing.Size(235, 373);
            this.groupBox_z.TabIndex = 34;
            this.groupBox_z.TabStop = false;
            this.groupBox_z.Text = "Z轴控制";
            // 
            // label_status_AxisZ
            // 
            this.label_status_AxisZ.AutoSize = true;
            this.label_status_AxisZ.BackColor = System.Drawing.Color.Orange;
            this.label_status_AxisZ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label_status_AxisZ.Location = new System.Drawing.Point(62, 349);
            this.label_status_AxisZ.Name = "label_status_AxisZ";
            this.label_status_AxisZ.Size = new System.Drawing.Size(29, 12);
            this.label_status_AxisZ.TabIndex = 39;
            this.label_status_AxisZ.Text = "----";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(27, 349);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(35, 12);
            this.label48.TabIndex = 38;
            this.label48.Text = "状态:";
            // 
            // button_Home_Z
            // 
            this.button_Home_Z.Location = new System.Drawing.Point(156, 20);
            this.button_Home_Z.Name = "button_Home_Z";
            this.button_Home_Z.Size = new System.Drawing.Size(64, 23);
            this.button_Home_Z.TabIndex = 36;
            this.button_Home_Z.Text = "回原点";
            this.button_Home_Z.UseVisualStyleBackColor = true;
            this.button_Home_Z.Click += new System.EventHandler(this.button_Home_Z_Click);
            // 
            // Button_CountReset_Z
            // 
            this.Button_CountReset_Z.Location = new System.Drawing.Point(156, 45);
            this.Button_CountReset_Z.Name = "Button_CountReset_Z";
            this.Button_CountReset_Z.Size = new System.Drawing.Size(62, 23);
            this.Button_CountReset_Z.TabIndex = 37;
            this.Button_CountReset_Z.Text = "重置";
            this.Button_CountReset_Z.UseVisualStyleBackColor = true;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(5, 52);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(71, 12);
            this.label46.TabIndex = 34;
            this.label46.Text = "逻辑计数器:";
            // 
            // button_ServerOn_Z
            // 
            this.button_ServerOn_Z.Location = new System.Drawing.Point(3, 21);
            this.button_ServerOn_Z.Name = "button_ServerOn_Z";
            this.button_ServerOn_Z.Size = new System.Drawing.Size(64, 23);
            this.button_ServerOn_Z.TabIndex = 35;
            this.button_ServerOn_Z.Text = "伺服 ON";
            this.button_ServerOn_Z.UseVisualStyleBackColor = true;
            // 
            // textBox_LoginCount_Z
            // 
            this.textBox_LoginCount_Z.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBox_LoginCount_Z.Location = new System.Drawing.Point(76, 47);
            this.textBox_LoginCount_Z.Name = "textBox_LoginCount_Z";
            this.textBox_LoginCount_Z.ReadOnly = true;
            this.textBox_LoginCount_Z.Size = new System.Drawing.Size(74, 21);
            this.textBox_LoginCount_Z.TabIndex = 33;
            // 
            // button_AddToGp_Z
            // 
            this.button_AddToGp_Z.Enabled = false;
            this.button_AddToGp_Z.Location = new System.Drawing.Point(75, 20);
            this.button_AddToGp_Z.Name = "button_AddToGp_Z";
            this.button_AddToGp_Z.Size = new System.Drawing.Size(75, 23);
            this.button_AddToGp_Z.TabIndex = 32;
            this.button_AddToGp_Z.Text = "添加群组";
            this.button_AddToGp_Z.UseVisualStyleBackColor = true;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.numericUpDown_VelLow_Z);
            this.groupBox13.Controls.Add(this.label42);
            this.groupBox13.Controls.Add(this.label43);
            this.groupBox13.Controls.Add(this.numericUpDown_VelHigh_Z);
            this.groupBox13.Controls.Add(this.label44);
            this.groupBox13.Controls.Add(this.numericUpDown_Acc_Z);
            this.groupBox13.Controls.Add(this.numericUpDown_Dec_Z);
            this.groupBox13.Controls.Add(this.label45);
            this.groupBox13.Controls.Add(this.button_UpdateSpdValue_Z);
            this.groupBox13.Controls.Add(this.button_SpeedSet_Z);
            this.groupBox13.ForeColor = System.Drawing.SystemColors.InfoText;
            this.groupBox13.Location = new System.Drawing.Point(6, 146);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(222, 89);
            this.groupBox13.TabIndex = 31;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "速度设定";
            // 
            // numericUpDown_VelLow_Z
            // 
            this.numericUpDown_VelLow_Z.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_VelLow_Z.Location = new System.Drawing.Point(60, 41);
            this.numericUpDown_VelLow_Z.Maximum = new decimal(new int[] {
            80000,
            0,
            0,
            0});
            this.numericUpDown_VelLow_Z.Name = "numericUpDown_VelLow_Z";
            this.numericUpDown_VelLow_Z.Size = new System.Drawing.Size(56, 21);
            this.numericUpDown_VelLow_Z.TabIndex = 15;
            this.numericUpDown_VelLow_Z.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(131, 41);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(29, 12);
            this.label42.TabIndex = 13;
            this.label42.Text = "Acc:";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(131, 18);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(29, 12);
            this.label43.TabIndex = 13;
            this.label43.Text = "Dec:";
            // 
            // numericUpDown_VelHigh_Z
            // 
            this.numericUpDown_VelHigh_Z.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_VelHigh_Z.Location = new System.Drawing.Point(60, 17);
            this.numericUpDown_VelHigh_Z.Maximum = new decimal(new int[] {
            80000,
            0,
            0,
            0});
            this.numericUpDown_VelHigh_Z.Name = "numericUpDown_VelHigh_Z";
            this.numericUpDown_VelHigh_Z.Size = new System.Drawing.Size(56, 21);
            this.numericUpDown_VelHigh_Z.TabIndex = 15;
            this.numericUpDown_VelHigh_Z.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(5, 19);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(53, 12);
            this.label44.TabIndex = 13;
            this.label44.Text = "VelHigh:";
            // 
            // numericUpDown_Acc_Z
            // 
            this.numericUpDown_Acc_Z.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_Acc_Z.Location = new System.Drawing.Point(163, 39);
            this.numericUpDown_Acc_Z.Maximum = new decimal(new int[] {
            80000,
            0,
            0,
            0});
            this.numericUpDown_Acc_Z.Name = "numericUpDown_Acc_Z";
            this.numericUpDown_Acc_Z.Size = new System.Drawing.Size(56, 21);
            this.numericUpDown_Acc_Z.TabIndex = 15;
            this.numericUpDown_Acc_Z.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // numericUpDown_Dec_Z
            // 
            this.numericUpDown_Dec_Z.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_Dec_Z.Location = new System.Drawing.Point(163, 15);
            this.numericUpDown_Dec_Z.Maximum = new decimal(new int[] {
            80000,
            0,
            0,
            0});
            this.numericUpDown_Dec_Z.Name = "numericUpDown_Dec_Z";
            this.numericUpDown_Dec_Z.Size = new System.Drawing.Size(56, 21);
            this.numericUpDown_Dec_Z.TabIndex = 15;
            this.numericUpDown_Dec_Z.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(7, 42);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(47, 12);
            this.label45.TabIndex = 13;
            this.label45.Text = "VelLow:";
            // 
            // button_UpdateSpdValue_Z
            // 
            this.button_UpdateSpdValue_Z.Location = new System.Drawing.Point(62, 64);
            this.button_UpdateSpdValue_Z.Name = "button_UpdateSpdValue_Z";
            this.button_UpdateSpdValue_Z.Size = new System.Drawing.Size(75, 23);
            this.button_UpdateSpdValue_Z.TabIndex = 12;
            this.button_UpdateSpdValue_Z.Text = "更新参数";
            this.button_UpdateSpdValue_Z.UseVisualStyleBackColor = true;
            this.button_UpdateSpdValue_Z.Click += new System.EventHandler(this.button_UpdateSpdValue_Z_Click);
            // 
            // button_SpeedSet_Z
            // 
            this.button_SpeedSet_Z.Location = new System.Drawing.Point(137, 64);
            this.button_SpeedSet_Z.Name = "button_SpeedSet_Z";
            this.button_SpeedSet_Z.Size = new System.Drawing.Size(75, 23);
            this.button_SpeedSet_Z.TabIndex = 12;
            this.button_SpeedSet_Z.Text = "设置参数";
            this.button_SpeedSet_Z.UseVisualStyleBackColor = true;
            this.button_SpeedSet_Z.Click += new System.EventHandler(this.button_SpeedSet_Z_Click);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.numericUpDown_MovDis_Z);
            this.groupBox12.Controls.Add(this.comboBox_MoveDirect_Z);
            this.groupBox12.Controls.Add(this.label37);
            this.groupBox12.Controls.Add(this.label40);
            this.groupBox12.Controls.Add(this.btn_goZ);
            this.groupBox12.Controls.Add(this.label41);
            this.groupBox12.Enabled = false;
            this.groupBox12.ForeColor = System.Drawing.SystemColors.InfoText;
            this.groupBox12.Location = new System.Drawing.Point(6, 75);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(222, 70);
            this.groupBox12.TabIndex = 30;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "运动设置";
            // 
            // numericUpDown_MovDis_Z
            // 
            this.numericUpDown_MovDis_Z.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown_MovDis_Z.Location = new System.Drawing.Point(58, 43);
            this.numericUpDown_MovDis_Z.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_MovDis_Z.Name = "numericUpDown_MovDis_Z";
            this.numericUpDown_MovDis_Z.Size = new System.Drawing.Size(57, 21);
            this.numericUpDown_MovDis_Z.TabIndex = 16;
            this.numericUpDown_MovDis_Z.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // comboBox_MoveDirect_Z
            // 
            this.comboBox_MoveDirect_Z.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_MoveDirect_Z.FormattingEnabled = true;
            this.comboBox_MoveDirect_Z.Items.AddRange(new object[] {
            "正向",
            "反向"});
            this.comboBox_MoveDirect_Z.Location = new System.Drawing.Point(58, 19);
            this.comboBox_MoveDirect_Z.Name = "comboBox_MoveDirect_Z";
            this.comboBox_MoveDirect_Z.Size = new System.Drawing.Size(81, 20);
            this.comboBox_MoveDirect_Z.TabIndex = 14;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(121, 47);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(23, 12);
            this.label37.TabIndex = 13;
            this.label37.Text = "pps";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(11, 48);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(47, 12);
            this.label40.TabIndex = 13;
            this.label40.Text = "终点值:";
            // 
            // btn_goZ
            // 
            this.btn_goZ.Location = new System.Drawing.Point(152, 41);
            this.btn_goZ.Name = "btn_goZ";
            this.btn_goZ.Size = new System.Drawing.Size(64, 23);
            this.btn_goZ.TabIndex = 12;
            this.btn_goZ.Text = "GO";
            this.btn_goZ.UseVisualStyleBackColor = true;
            this.btn_goZ.Click += new System.EventHandler(this.btn_goZ_Click);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(10, 25);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(35, 12);
            this.label41.TabIndex = 11;
            this.label41.Text = "方向:";
            // 
            // comboBox_op
            // 
            this.comboBox_op.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_op.FormattingEnabled = true;
            this.comboBox_op.Items.AddRange(new object[] {
            "+",
            "-"});
            this.comboBox_op.Location = new System.Drawing.Point(14, 28);
            this.comboBox_op.Name = "comboBox_op";
            this.comboBox_op.Size = new System.Drawing.Size(43, 20);
            this.comboBox_op.TabIndex = 40;
            // 
            // numericUpDown_refAxisZ
            // 
            this.numericUpDown_refAxisZ.DecimalPlaces = 2;
            this.numericUpDown_refAxisZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown_refAxisZ.Location = new System.Drawing.Point(63, 27);
            this.numericUpDown_refAxisZ.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown_refAxisZ.Name = "numericUpDown_refAxisZ";
            this.numericUpDown_refAxisZ.Size = new System.Drawing.Size(80, 21);
            this.numericUpDown_refAxisZ.TabIndex = 41;
            this.numericUpDown_refAxisZ.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.label47);
            this.groupBox14.Controls.Add(this.btn_refAxisZ);
            this.groupBox14.Controls.Add(this.numericUpDown_refAxisZ);
            this.groupBox14.Controls.Add(this.comboBox_op);
            this.groupBox14.Location = new System.Drawing.Point(7, 238);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(221, 100);
            this.groupBox14.TabIndex = 42;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Z轴参考点设定";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(146, 31);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(17, 12);
            this.label47.TabIndex = 42;
            this.label47.Text = "mm";
            // 
            // PCI1245_E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.groupBox_z);
            this.Controls.Add(this.Button_Stop);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.Button_Move);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.textBoxGpState);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textBoxMasterID);
            this.Controls.Add(this.groupBox11);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.BtnResetErr);
            this.Controls.Add(this.groupBox_AxisX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Button_CloseBoard);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Button_OpenBoard);
            this.Name = "PCI1245_E";
            this.Size = new System.Drawing.Size(1093, 467);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MovDis_X)).EndInit();
            this.groupBox_AxisX.ResumeLayout(false);
            this.groupBox_AxisX.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DI3_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DI2_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DI0_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DI1_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox_D05_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox_D06_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox_D07_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox_D04_X)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_VelLow_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_VelHigh_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Jerk_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Dec_X)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DI3_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DI2_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DI0_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DI1_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox_D05_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox_D06_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox_D07_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox_D04_Y)).EndInit();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_VelLow_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_VelHigh_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Jerk_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Dec_Y)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MovDis_Y)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_VelLow_gp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_VelHigh_gp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Acc_gp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Dec_gp)).EndInit();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ProxScan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ShtSNScan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ledWhite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ledRed)).EndInit();
            this.groupBox_z.ResumeLayout(false);
            this.groupBox_z.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_VelLow_Z)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_VelHigh_Z)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Acc_Z)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Dec_Z)).EndInit();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_MovDis_Z)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_refAxisZ)).EndInit();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_OpenBoard;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button Button_ClearEnd_X;
        private System.Windows.Forms.Button Button_SetEnd_X;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Button_CountReset_X;
        private System.Windows.Forms.Button Button_CloseBoard;
        private System.Windows.Forms.GroupBox groupBox_AxisX;
        private System.Windows.Forms.Button button_ServerON_X;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button_ServerOn_Y;
        private System.Windows.Forms.Label label_status_AxisX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_status_AxisY;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_LoginCount_X;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_LoginCount_Y;
        private System.Windows.Forms.Button Button_CountReset_Y;
        private System.Windows.Forms.Button button_AddToGp_X;
        private System.Windows.Forms.Button button_AddToGp_Y;
        private System.Windows.Forms.ListBox listBoxAxesInGp;
        private System.Windows.Forms.ListBox listBoxEndPoint;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnResetErr;
        private System.Windows.Forms.TextBox textBoxMasterID;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxGpState;
        private System.Windows.Forms.Button Button_Move;
        private System.Windows.Forms.RadioButton radioButtonAbs;
        private System.Windows.Forms.RadioButton radioButtonRel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button Button_Stop;
        private System.Windows.Forms.ComboBox comboBox_MoveDirect_X;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox_MoveDirect_Y;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button Button_ClearEnd_Y;
        private System.Windows.Forms.Button Button_SetEnd_Y;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numericUpDown_MovDis_X;
        private System.Windows.Forms.NumericUpDown numericUpDown_MovDis_Y;
        private System.Windows.Forms.Button button_Home_X;
        private System.Windows.Forms.Button button_Home_Y;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.PictureBox pictureBox_DI3_X;
        private System.Windows.Forms.Button Button_D07_X;
        private System.Windows.Forms.PictureBox pictureBox_DI2_X;
        private System.Windows.Forms.Button Button_D06_X;
        private System.Windows.Forms.PictureBox pictureBox_DI0_X;
        private System.Windows.Forms.Button Button_D05_X;
        private System.Windows.Forms.PictureBox pictureBox_DI1_X;
        private System.Windows.Forms.Button Button_D04_X;
        private System.Windows.Forms.PictureBox Picturebox_D05_X;
        private System.Windows.Forms.PictureBox Picturebox_D06_X;
        private System.Windows.Forms.PictureBox Picturebox_D07_X;
        private System.Windows.Forms.PictureBox Picturebox_D04_X;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.PictureBox pictureBox_DI3_Y;
        private System.Windows.Forms.Button Button_D07_Y;
        private System.Windows.Forms.PictureBox pictureBox_DI2_Y;
        private System.Windows.Forms.Button Button_D06_Y;
        private System.Windows.Forms.PictureBox pictureBox_DI0_Y;
        private System.Windows.Forms.Button Button_D05_Y;
        private System.Windows.Forms.PictureBox pictureBox_DI1_Y;
        private System.Windows.Forms.Button Button_D04_Y;
        private System.Windows.Forms.PictureBox Picturebox_D05_Y;
        private System.Windows.Forms.PictureBox Picturebox_D06_Y;
        private System.Windows.Forms.PictureBox Picturebox_D07_Y;
        private System.Windows.Forms.PictureBox Picturebox_D04_Y;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.NumericUpDown numericUpDown_VelHigh_X;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown numericUpDown_Dec_X;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button_SpeedSet_X;
        private System.Windows.Forms.NumericUpDown numericUpDown_VelLow_X;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.NumericUpDown numericUpDown_Jerk_X;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.NumericUpDown numericUpDown_VelLow_Y;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.NumericUpDown numericUpDown_VelHigh_Y;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.NumericUpDown numericUpDown_Jerk_Y;
        private System.Windows.Forms.NumericUpDown numericUpDown_Dec_Y;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button button_SpeedSet_Y;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.NumericUpDown numericUpDown_VelLow_gp;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.NumericUpDown numericUpDown_VelHigh_gp;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.NumericUpDown numericUpDown_Acc_gp;
        private System.Windows.Forms.NumericUpDown numericUpDown_Dec_gp;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Button button_SpeedSet_gp;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.PictureBox pictureBox_ledWhite;
        private System.Windows.Forms.PictureBox pictureBox_ledRed;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Button button_LedRed;
        private System.Windows.Forms.Button button_LedWhite;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.PictureBox pictureBox_ShtSNScan;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.PictureBox pictureBox_ProxScan;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Button button_CCDTrigger;
        private System.Windows.Forms.Button button_UpdateSpdValue_X;
        private System.Windows.Forms.Button button_UpdateSpdValue_Y;
        private System.Windows.Forms.Button button_UpdateGpSpdValue;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Button btn_refAxisZ;
        private System.Windows.Forms.GroupBox groupBox_z;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.NumericUpDown numericUpDown_VelLow_Z;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.NumericUpDown numericUpDown_VelHigh_Z;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.NumericUpDown numericUpDown_Acc_Z;
        private System.Windows.Forms.NumericUpDown numericUpDown_Dec_Z;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Button button_UpdateSpdValue_Z;
        private System.Windows.Forms.Button button_SpeedSet_Z;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.NumericUpDown numericUpDown_MovDis_Z;
        private System.Windows.Forms.ComboBox comboBox_MoveDirect_Z;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Button btn_goZ;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label_status_AxisZ;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Button button_Home_Z;
        private System.Windows.Forms.Button Button_CountReset_Z;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Button button_ServerOn_Z;
        private System.Windows.Forms.TextBox textBox_LoginCount_Z;
        private System.Windows.Forms.Button button_AddToGp_Z;
        private System.Windows.Forms.ComboBox comboBox_op;
        private System.Windows.Forms.NumericUpDown numericUpDown_refAxisZ;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.Label label47;
    }
}
