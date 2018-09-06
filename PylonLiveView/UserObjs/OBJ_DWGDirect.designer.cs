namespace PylonLiveView
{
    partial class OBJ_DWGDirect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OBJ_DWGDirect));
            this.splitContainer_main = new System.Windows.Forms.SplitContainer();
            this.panel_tipMessage = new System.Windows.Forms.Panel();
            this.panel_Location = new System.Windows.Forms.Panel();
            this.label1_currentPos = new System.Windows.Forms.Label();
            this.label_pox_y = new System.Windows.Forms.Label();
            this.label_pox_x = new System.Windows.Forms.Label();
            this.button_Convert = new System.Windows.Forms.Button();
            this.button_openFile = new System.Windows.Forms.Button();
            this.splitContainer_mainRight = new System.Windows.Forms.SplitContainer();
            this.panel_graphics = new System.Windows.Forms.Panel();
            this.contextMenuStrip_shortcut = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_cancel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_searchPath = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ZoomWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_HidePath = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox_showPaperPoint = new System.Windows.Forms.PictureBox();
            this.panel_config = new System.Windows.Forms.Panel();
            this.button_RtnRefOrgPoint = new System.Windows.Forms.Button();
            this.button_RtnMachaOrgPoint = new System.Windows.Forms.Button();
            this.groupBox_fixPoint = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_fixMotion_Y = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_fixMotion_X = new System.Windows.Forms.NumericUpDown();
            this.button_sendMsg_fixMotion = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox_send = new System.Windows.Forms.GroupBox();
            this.button_practice = new System.Windows.Forms.Button();
            this.button_workPermitted = new System.Windows.Forms.Button();
            this.button_sendTPMessage = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_main)).BeginInit();
            this.splitContainer_main.Panel1.SuspendLayout();
            this.splitContainer_main.Panel2.SuspendLayout();
            this.splitContainer_main.SuspendLayout();
            this.panel_Location.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_mainRight)).BeginInit();
            this.splitContainer_mainRight.Panel1.SuspendLayout();
            this.splitContainer_mainRight.Panel2.SuspendLayout();
            this.splitContainer_mainRight.SuspendLayout();
            this.contextMenuStrip_shortcut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_showPaperPoint)).BeginInit();
            this.panel_config.SuspendLayout();
            this.groupBox_fixPoint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_fixMotion_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_fixMotion_X)).BeginInit();
            this.groupBox_send.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer_main
            // 
            this.splitContainer_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_main.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer_main.IsSplitterFixed = true;
            this.splitContainer_main.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_main.Name = "splitContainer_main";
            // 
            // splitContainer_main.Panel1
            // 
            this.splitContainer_main.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.splitContainer_main.Panel1.Controls.Add(this.panel_tipMessage);
            this.splitContainer_main.Panel1.Controls.Add(this.panel_Location);
            this.splitContainer_main.Panel1.Controls.Add(this.button_Convert);
            this.splitContainer_main.Panel1.Controls.Add(this.button_openFile);
            this.splitContainer_main.Panel1MinSize = 35;
            // 
            // splitContainer_main.Panel2
            // 
            this.splitContainer_main.Panel2.Controls.Add(this.splitContainer_mainRight);
            this.splitContainer_main.Size = new System.Drawing.Size(1138, 882);
            this.splitContainer_main.SplitterDistance = 160;
            this.splitContainer_main.SplitterWidth = 6;
            this.splitContainer_main.TabIndex = 0;
            // 
            // panel_tipMessage
            // 
            this.panel_tipMessage.AutoScroll = true;
            this.panel_tipMessage.BackColor = System.Drawing.Color.Transparent;
            this.panel_tipMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_tipMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_tipMessage.Location = new System.Drawing.Point(0, 163);
            this.panel_tipMessage.Name = "panel_tipMessage";
            this.panel_tipMessage.Size = new System.Drawing.Size(160, 719);
            this.panel_tipMessage.TabIndex = 77;
            // 
            // panel_Location
            // 
            this.panel_Location.Controls.Add(this.label1_currentPos);
            this.panel_Location.Controls.Add(this.label_pox_y);
            this.panel_Location.Controls.Add(this.label_pox_x);
            this.panel_Location.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Location.Location = new System.Drawing.Point(0, 124);
            this.panel_Location.Name = "panel_Location";
            this.panel_Location.Size = new System.Drawing.Size(160, 39);
            this.panel_Location.TabIndex = 76;
            // 
            // label1_currentPos
            // 
            this.label1_currentPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1_currentPos.AutoSize = true;
            this.label1_currentPos.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1_currentPos.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label1_currentPos.Location = new System.Drawing.Point(2, 25);
            this.label1_currentPos.Name = "label1_currentPos";
            this.label1_currentPos.Size = new System.Drawing.Size(128, 15);
            this.label1_currentPos.TabIndex = 10;
            this.label1_currentPos.Text = "目标点坐标集合:";
            // 
            // label_pox_y
            // 
            this.label_pox_y.AutoSize = true;
            this.label_pox_y.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_pox_y.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label_pox_y.Location = new System.Drawing.Point(103, 5);
            this.label_pox_y.Name = "label_pox_y";
            this.label_pox_y.Size = new System.Drawing.Size(43, 15);
            this.label_pox_y.TabIndex = 67;
            this.label_pox_y.Text = "0.00";
            // 
            // label_pox_x
            // 
            this.label_pox_x.AutoSize = true;
            this.label_pox_x.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_pox_x.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label_pox_x.Location = new System.Drawing.Point(22, 5);
            this.label_pox_x.Name = "label_pox_x";
            this.label_pox_x.Size = new System.Drawing.Size(43, 15);
            this.label_pox_x.TabIndex = 66;
            this.label_pox_x.Text = "0.00";
            // 
            // button_Convert
            // 
            this.button_Convert.BackColor = System.Drawing.Color.DarkSalmon;
            this.button_Convert.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_Convert.Enabled = false;
            this.button_Convert.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_Convert.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.button_Convert.Image = ((System.Drawing.Image)(resources.GetObject("button_Convert.Image")));
            this.button_Convert.Location = new System.Drawing.Point(0, 62);
            this.button_Convert.Name = "button_Convert";
            this.button_Convert.Size = new System.Drawing.Size(160, 62);
            this.button_Convert.TabIndex = 74;
            this.button_Convert.Text = "切換為相對座標";
            this.button_Convert.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button_Convert.UseVisualStyleBackColor = false;
            this.button_Convert.Click += new System.EventHandler(this.button_Convert_Click);
            // 
            // button_openFile
            // 
            this.button_openFile.BackColor = System.Drawing.Color.SteelBlue;
            this.button_openFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_openFile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.button_openFile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.button_openFile.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_openFile.ForeColor = System.Drawing.Color.Black;
            this.button_openFile.Image = ((System.Drawing.Image)(resources.GetObject("button_openFile.Image")));
            this.button_openFile.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_openFile.Location = new System.Drawing.Point(0, 0);
            this.button_openFile.Name = "button_openFile";
            this.button_openFile.Size = new System.Drawing.Size(160, 62);
            this.button_openFile.TabIndex = 75;
            this.button_openFile.Text = "檔案導入";
            this.button_openFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_openFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button_openFile.UseVisualStyleBackColor = false;
            this.button_openFile.Click += new System.EventHandler(this.button_openFile_Click);
            // 
            // splitContainer_mainRight
            // 
            this.splitContainer_mainRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_mainRight.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer_mainRight.IsSplitterFixed = true;
            this.splitContainer_mainRight.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_mainRight.Name = "splitContainer_mainRight";
            // 
            // splitContainer_mainRight.Panel1
            // 
            this.splitContainer_mainRight.Panel1.Controls.Add(this.panel_graphics);
            this.splitContainer_mainRight.Panel1.Controls.Add(this.pictureBox_showPaperPoint);
            // 
            // splitContainer_mainRight.Panel2
            // 
            this.splitContainer_mainRight.Panel2.Controls.Add(this.panel_config);
            this.splitContainer_mainRight.Size = new System.Drawing.Size(972, 882);
            this.splitContainer_mainRight.SplitterDistance = 604;
            this.splitContainer_mainRight.SplitterWidth = 6;
            this.splitContainer_mainRight.TabIndex = 1;
            // 
            // panel_graphics
            // 
            this.panel_graphics.BackColor = System.Drawing.Color.Black;
            this.panel_graphics.ContextMenuStrip = this.contextMenuStrip_shortcut;
            this.panel_graphics.Cursor = System.Windows.Forms.Cursors.Cross;
            this.panel_graphics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_graphics.Location = new System.Drawing.Point(0, 0);
            this.panel_graphics.Name = "panel_graphics";
            this.panel_graphics.Size = new System.Drawing.Size(604, 882);
            this.panel_graphics.TabIndex = 199;
            this.panel_graphics.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel_graphics_MouseClick);
            this.panel_graphics.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_graphics_MouseDown);
            this.panel_graphics.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_graphics_MouseUp);
            // 
            // contextMenuStrip_shortcut
            // 
            this.contextMenuStrip_shortcut.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_cancel,
            this.toolStripMenuItem_searchPath,
            this.toolStripMenuItem_ZoomWindow,
            this.toolStripMenuItem_HidePath});
            this.contextMenuStrip_shortcut.Name = "contextMenuStrip_shortcut";
            this.contextMenuStrip_shortcut.Size = new System.Drawing.Size(139, 100);
            // 
            // toolStripMenuItem_cancel
            // 
            this.toolStripMenuItem_cancel.Name = "toolStripMenuItem_cancel";
            this.toolStripMenuItem_cancel.Size = new System.Drawing.Size(138, 24);
            this.toolStripMenuItem_cancel.Text = "取消選擇";
            this.toolStripMenuItem_cancel.Click += new System.EventHandler(this.toolStripMenuItem_cancel_Click);
            // 
            // toolStripMenuItem_searchPath
            // 
            this.toolStripMenuItem_searchPath.Name = "toolStripMenuItem_searchPath";
            this.toolStripMenuItem_searchPath.Size = new System.Drawing.Size(138, 24);
            this.toolStripMenuItem_searchPath.Text = "路徑顯示";
            this.toolStripMenuItem_searchPath.Click += new System.EventHandler(this.toolStripMenuItem_searchPath_Click);
            // 
            // toolStripMenuItem_ZoomWindow
            // 
            this.toolStripMenuItem_ZoomWindow.Name = "toolStripMenuItem_ZoomWindow";
            this.toolStripMenuItem_ZoomWindow.Size = new System.Drawing.Size(138, 24);
            this.toolStripMenuItem_ZoomWindow.Text = "窗體視圖";
            this.toolStripMenuItem_ZoomWindow.Click += new System.EventHandler(this.toolStripMenuItem_ZoomWindow_Click);
            // 
            // toolStripMenuItem_HidePath
            // 
            this.toolStripMenuItem_HidePath.Name = "toolStripMenuItem_HidePath";
            this.toolStripMenuItem_HidePath.Size = new System.Drawing.Size(138, 24);
            this.toolStripMenuItem_HidePath.Text = "路徑隱藏";
            this.toolStripMenuItem_HidePath.Click += new System.EventHandler(this.toolStripMenuItem_HidePath_Click);
            // 
            // pictureBox_showPaperPoint
            // 
            this.pictureBox_showPaperPoint.BackColor = System.Drawing.SystemColors.ControlText;
            this.pictureBox_showPaperPoint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_showPaperPoint.ContextMenuStrip = this.contextMenuStrip_shortcut;
            this.pictureBox_showPaperPoint.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBox_showPaperPoint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_showPaperPoint.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_showPaperPoint.Name = "pictureBox_showPaperPoint";
            this.pictureBox_showPaperPoint.Size = new System.Drawing.Size(604, 882);
            this.pictureBox_showPaperPoint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_showPaperPoint.TabIndex = 1;
            this.pictureBox_showPaperPoint.TabStop = false;
            this.pictureBox_showPaperPoint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_showPaperPoint_MouseClick);
            // 
            // panel_config
            // 
            this.panel_config.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel_config.Controls.Add(this.button_RtnRefOrgPoint);
            this.panel_config.Controls.Add(this.button_RtnMachaOrgPoint);
            this.panel_config.Controls.Add(this.groupBox_fixPoint);
            this.panel_config.Controls.Add(this.groupBox_send);
            this.panel_config.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_config.Location = new System.Drawing.Point(0, 0);
            this.panel_config.Name = "panel_config";
            this.panel_config.Size = new System.Drawing.Size(362, 882);
            this.panel_config.TabIndex = 0;
            // 
            // button_RtnRefOrgPoint
            // 
            this.button_RtnRefOrgPoint.BackColor = System.Drawing.Color.SteelBlue;
            this.button_RtnRefOrgPoint.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_RtnRefOrgPoint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.button_RtnRefOrgPoint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.button_RtnRefOrgPoint.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_RtnRefOrgPoint.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_RtnRefOrgPoint.Location = new System.Drawing.Point(0, 430);
            this.button_RtnRefOrgPoint.Name = "button_RtnRefOrgPoint";
            this.button_RtnRefOrgPoint.Size = new System.Drawing.Size(362, 41);
            this.button_RtnRefOrgPoint.TabIndex = 55;
            this.button_RtnRefOrgPoint.Text = "回參考原點";
            this.button_RtnRefOrgPoint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button_RtnRefOrgPoint.UseVisualStyleBackColor = false;
            this.button_RtnRefOrgPoint.Visible = false;
            this.button_RtnRefOrgPoint.Click += new System.EventHandler(this.button_RtnRefOrgPoint_Click);
            // 
            // button_RtnMachaOrgPoint
            // 
            this.button_RtnMachaOrgPoint.BackColor = System.Drawing.Color.SteelBlue;
            this.button_RtnMachaOrgPoint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_RtnMachaOrgPoint.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_RtnMachaOrgPoint.Enabled = false;
            this.button_RtnMachaOrgPoint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.button_RtnMachaOrgPoint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.button_RtnMachaOrgPoint.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_RtnMachaOrgPoint.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_RtnMachaOrgPoint.Location = new System.Drawing.Point(0, 389);
            this.button_RtnMachaOrgPoint.Name = "button_RtnMachaOrgPoint";
            this.button_RtnMachaOrgPoint.Size = new System.Drawing.Size(362, 41);
            this.button_RtnMachaOrgPoint.TabIndex = 54;
            this.button_RtnMachaOrgPoint.Text = "回機械原點";
            this.button_RtnMachaOrgPoint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button_RtnMachaOrgPoint.UseVisualStyleBackColor = false;
            this.button_RtnMachaOrgPoint.Click += new System.EventHandler(this.button_RtnMachaOrgPoint_Click);
            // 
            // groupBox_fixPoint
            // 
            this.groupBox_fixPoint.Controls.Add(this.label7);
            this.groupBox_fixPoint.Controls.Add(this.label5);
            this.groupBox_fixPoint.Controls.Add(this.label6);
            this.groupBox_fixPoint.Controls.Add(this.numericUpDown1);
            this.groupBox_fixPoint.Controls.Add(this.numericUpDown_fixMotion_Y);
            this.groupBox_fixPoint.Controls.Add(this.numericUpDown_fixMotion_X);
            this.groupBox_fixPoint.Controls.Add(this.button_sendMsg_fixMotion);
            this.groupBox_fixPoint.Controls.Add(this.comboBox1);
            this.groupBox_fixPoint.Controls.Add(this.comboBox2);
            this.groupBox_fixPoint.Controls.Add(this.comboBox3);
            this.groupBox_fixPoint.Controls.Add(this.label1);
            this.groupBox_fixPoint.Controls.Add(this.label2);
            this.groupBox_fixPoint.Controls.Add(this.label4);
            this.groupBox_fixPoint.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_fixPoint.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox_fixPoint.Location = new System.Drawing.Point(0, 227);
            this.groupBox_fixPoint.Name = "groupBox_fixPoint";
            this.groupBox_fixPoint.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox_fixPoint.Size = new System.Drawing.Size(362, 162);
            this.groupBox_fixPoint.TabIndex = 51;
            this.groupBox_fixPoint.TabStop = false;
            this.groupBox_fixPoint.Text = "目標點運動控制";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(220, 87);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 15);
            this.label7.TabIndex = 71;
            this.label7.Text = "/100.0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(220, 55);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 15);
            this.label5.TabIndex = 71;
            this.label5.Text = "/100.0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(220, 31);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 15);
            this.label6.TabIndex = 72;
            this.label6.Text = "/100.0";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(128, 76);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(89, 30);
            this.numericUpDown1.TabIndex = 69;
            // 
            // numericUpDown_fixMotion_Y
            // 
            this.numericUpDown_fixMotion_Y.Location = new System.Drawing.Point(128, 49);
            this.numericUpDown_fixMotion_Y.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_fixMotion_Y.Name = "numericUpDown_fixMotion_Y";
            this.numericUpDown_fixMotion_Y.Size = new System.Drawing.Size(89, 30);
            this.numericUpDown_fixMotion_Y.TabIndex = 69;
            // 
            // numericUpDown_fixMotion_X
            // 
            this.numericUpDown_fixMotion_X.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown_fixMotion_X.Location = new System.Drawing.Point(128, 22);
            this.numericUpDown_fixMotion_X.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown_fixMotion_X.Name = "numericUpDown_fixMotion_X";
            this.numericUpDown_fixMotion_X.Size = new System.Drawing.Size(89, 30);
            this.numericUpDown_fixMotion_X.TabIndex = 70;
            // 
            // button_sendMsg_fixMotion
            // 
            this.button_sendMsg_fixMotion.BackColor = System.Drawing.Color.SteelBlue;
            this.button_sendMsg_fixMotion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button_sendMsg_fixMotion.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_sendMsg_fixMotion.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.button_sendMsg_fixMotion.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button_sendMsg_fixMotion.Location = new System.Drawing.Point(3, 109);
            this.button_sendMsg_fixMotion.Name = "button_sendMsg_fixMotion";
            this.button_sendMsg_fixMotion.Size = new System.Drawing.Size(356, 50);
            this.button_sendMsg_fixMotion.TabIndex = 63;
            this.button_sendMsg_fixMotion.Text = "發送指令";
            this.button_sendMsg_fixMotion.UseVisualStyleBackColor = false;
            this.button_sendMsg_fixMotion.Click += new System.EventHandler(this.button_sendMsg_fixMotion_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(82, 76);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(41, 28);
            this.comboBox1.TabIndex = 55;
            // 
            // comboBox2
            // 
            this.comboBox2.Enabled = false;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(82, 51);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(41, 28);
            this.comboBox2.TabIndex = 53;
            // 
            // comboBox3
            // 
            this.comboBox3.Enabled = false;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(82, 24);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(41, 28);
            this.comboBox3.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Sienna;
            this.label1.Location = new System.Drawing.Point(34, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 66;
            this.label1.Text = "Pox_Z:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Sienna;
            this.label2.Location = new System.Drawing.Point(34, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 63;
            this.label2.Text = "Pox_Y:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Sienna;
            this.label4.Location = new System.Drawing.Point(34, 30);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(61, 15);
            this.label4.TabIndex = 64;
            this.label4.Text = "Pox_X:";
            // 
            // groupBox_send
            // 
            this.groupBox_send.Controls.Add(this.button_practice);
            this.groupBox_send.Controls.Add(this.button_workPermitted);
            this.groupBox_send.Controls.Add(this.button_sendTPMessage);
            this.groupBox_send.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_send.Enabled = false;
            this.groupBox_send.Location = new System.Drawing.Point(0, 0);
            this.groupBox_send.Name = "groupBox_send";
            this.groupBox_send.Size = new System.Drawing.Size(362, 227);
            this.groupBox_send.TabIndex = 7;
            this.groupBox_send.TabStop = false;
            // 
            // button_practice
            // 
            this.button_practice.BackColor = System.Drawing.Color.SteelBlue;
            this.button_practice.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_practice.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_practice.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_practice.Image = ((System.Drawing.Image)(resources.GetObject("button_practice.Image")));
            this.button_practice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_practice.Location = new System.Drawing.Point(3, 157);
            this.button_practice.Name = "button_practice";
            this.button_practice.Size = new System.Drawing.Size(356, 64);
            this.button_practice.TabIndex = 2;
            this.button_practice.Text = "動作演示";
            this.button_practice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_practice.UseVisualStyleBackColor = false;
            // 
            // button_workPermitted
            // 
            this.button_workPermitted.BackColor = System.Drawing.Color.SteelBlue;
            this.button_workPermitted.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_workPermitted.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_workPermitted.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_workPermitted.Image = ((System.Drawing.Image)(resources.GetObject("button_workPermitted.Image")));
            this.button_workPermitted.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_workPermitted.Location = new System.Drawing.Point(3, 89);
            this.button_workPermitted.Name = "button_workPermitted";
            this.button_workPermitted.Size = new System.Drawing.Size(356, 68);
            this.button_workPermitted.TabIndex = 1;
            this.button_workPermitted.Text = "作業允許";
            this.button_workPermitted.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_workPermitted.UseVisualStyleBackColor = false;
            // 
            // button_sendTPMessage
            // 
            this.button_sendTPMessage.BackColor = System.Drawing.Color.SteelBlue;
            this.button_sendTPMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_sendTPMessage.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_sendTPMessage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_sendTPMessage.Image = ((System.Drawing.Image)(resources.GetObject("button_sendTPMessage.Image")));
            this.button_sendTPMessage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_sendTPMessage.Location = new System.Drawing.Point(3, 21);
            this.button_sendTPMessage.Name = "button_sendTPMessage";
            this.button_sendTPMessage.Size = new System.Drawing.Size(356, 68);
            this.button_sendTPMessage.TabIndex = 0;
            this.button_sendTPMessage.Text = "發送座標信息";
            this.button_sendTPMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_sendTPMessage.UseVisualStyleBackColor = false;
            this.button_sendTPMessage.Click += new System.EventHandler(this.button_sendTPMessage_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "DWG";
            this.openFileDialog.Filter = "DWG files|*.dwg";
            this.openFileDialog.Title = "開啟圖紙文檔/記錄檔";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "XML";
            this.saveFileDialog.FileName = "DataConfig";
            this.saveFileDialog.Filter = "XML files|*.xml";
            this.saveFileDialog.Title = "文檔存儲為";
            // 
            // OBJ_DWGDirect
            // 
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Controls.Add(this.splitContainer_main);
            this.Name = "OBJ_DWGDirect";
            this.Size = new System.Drawing.Size(1138, 882);
            this.Load += new System.EventHandler(this.OBJ_DWGDirect_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.on_control_Paint);
            this.Resize += new System.EventHandler(this.panel_graphics_Resize);
            this.splitContainer_main.Panel1.ResumeLayout(false);
            this.splitContainer_main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_main)).EndInit();
            this.splitContainer_main.ResumeLayout(false);
            this.panel_Location.ResumeLayout(false);
            this.panel_Location.PerformLayout();
            this.splitContainer_mainRight.Panel1.ResumeLayout(false);
            this.splitContainer_mainRight.Panel1.PerformLayout();
            this.splitContainer_mainRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_mainRight)).EndInit();
            this.splitContainer_mainRight.ResumeLayout(false);
            this.contextMenuStrip_shortcut.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_showPaperPoint)).EndInit();
            this.panel_config.ResumeLayout(false);
            this.groupBox_fixPoint.ResumeLayout(false);
            this.groupBox_fixPoint.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_fixMotion_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_fixMotion_X)).EndInit();
            this.groupBox_send.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer_main;
        private System.Windows.Forms.Panel panel_graphics;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_shortcut;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ZoomWindow;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_searchPath;
        private System.Windows.Forms.SplitContainer splitContainer_mainRight;
        private System.Windows.Forms.Panel panel_config;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox groupBox_send;
        private System.Windows.Forms.Button button_sendTPMessage;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_cancel;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_HidePath;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.PictureBox pictureBox_showPaperPoint;
        private System.Windows.Forms.Panel panel_Location;
        private System.Windows.Forms.Label label1_currentPos;
        private System.Windows.Forms.Label label_pox_y;
        private System.Windows.Forms.Label label_pox_x;
        private System.Windows.Forms.Button button_Convert;
        private System.Windows.Forms.Button button_openFile;
        private System.Windows.Forms.Panel panel_tipMessage;
        private System.Windows.Forms.Button button_workPermitted;
        private System.Windows.Forms.Button button_practice;
        private System.Windows.Forms.GroupBox groupBox_fixPoint;
        private System.Windows.Forms.Button button_sendMsg_fixMotion;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown_fixMotion_Y;
        private System.Windows.Forms.NumericUpDown numericUpDown_fixMotion_X;
        private System.Windows.Forms.Button button_RtnRefOrgPoint;
        private System.Windows.Forms.Button button_RtnMachaOrgPoint;
    }
}
