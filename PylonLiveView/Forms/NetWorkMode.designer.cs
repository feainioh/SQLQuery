namespace PylonLiveView
{
    partial class NetWorkMode
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox_master = new System.Windows.Forms.GroupBox();
            this.panel_master = new System.Windows.Forms.Panel();
            this.button_scan = new System.Windows.Forms.Button();
            this.dataGridView_IPSearch = new System.Windows.Forms.DataGridView();
            this.Column_IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_MAC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_workPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_AsMaster = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_addnew = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker_scan = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.groupBox_master.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_IPSearch)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_master
            // 
            this.groupBox_master.BackColor = System.Drawing.Color.Transparent;
            this.groupBox_master.Controls.Add(this.panel_master);
            this.groupBox_master.Location = new System.Drawing.Point(3, 63);
            this.groupBox_master.Name = "groupBox_master";
            this.groupBox_master.Size = new System.Drawing.Size(264, 54);
            this.groupBox_master.TabIndex = 9;
            this.groupBox_master.TabStop = false;
            this.groupBox_master.Text = "主機連接";
            // 
            // panel_master
            // 
            this.panel_master.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_master.Location = new System.Drawing.Point(3, 17);
            this.panel_master.Name = "panel_master";
            this.panel_master.Size = new System.Drawing.Size(258, 34);
            this.panel_master.TabIndex = 0;
            // 
            // button_scan
            // 
            this.button_scan.Location = new System.Drawing.Point(3, 5);
            this.button_scan.Name = "button_scan";
            this.button_scan.Size = new System.Drawing.Size(72, 26);
            this.button_scan.TabIndex = 10;
            this.button_scan.Text = "開始掃描";
            this.button_scan.UseVisualStyleBackColor = true;
            this.button_scan.Click += new System.EventHandler(this.button_scan_Click);
            // 
            // dataGridView_IPSearch
            // 
            this.dataGridView_IPSearch.AllowUserToAddRows = false;
            this.dataGridView_IPSearch.AllowUserToDeleteRows = false;
            this.dataGridView_IPSearch.AllowUserToOrderColumns = true;
            this.dataGridView_IPSearch.AllowUserToResizeColumns = false;
            this.dataGridView_IPSearch.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView_IPSearch.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_IPSearch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_IPSearch.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_IPSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_IPSearch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_IP,
            this.Column_MAC,
            this.Column_workPort});
            this.dataGridView_IPSearch.ContextMenuStrip = this.contextMenuStrip;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_IPSearch.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView_IPSearch.Location = new System.Drawing.Point(273, 1);
            this.dataGridView_IPSearch.MultiSelect = false;
            this.dataGridView_IPSearch.Name = "dataGridView_IPSearch";
            this.dataGridView_IPSearch.RowHeadersWidth = 20;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView_IPSearch.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView_IPSearch.RowTemplate.Height = 23;
            this.dataGridView_IPSearch.Size = new System.Drawing.Size(376, 194);
            this.dataGridView_IPSearch.TabIndex = 13;
            this.dataGridView_IPSearch.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_IPSearch_CellMouseDown);
            // 
            // Column_IP
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column_IP.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column_IP.HeaderText = "IP地址";
            this.Column_IP.Name = "Column_IP";
            this.Column_IP.Width = 135;
            // 
            // Column_MAC
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column_MAC.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column_MAC.HeaderText = "MAC地址";
            this.Column_MAC.Name = "Column_MAC";
            this.Column_MAC.Width = 135;
            // 
            // Column_workPort
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column_workPort.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column_workPort.HeaderText = "工作端口";
            this.Column_workPort.Name = "Column_workPort";
            this.Column_workPort.Width = 80;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_AsMaster,
            this.ToolStripMenuItem_addnew});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(137, 48);
            // 
            // toolStripMenuItem_AsMaster
            // 
            this.toolStripMenuItem_AsMaster.Name = "toolStripMenuItem_AsMaster";
            this.toolStripMenuItem_AsMaster.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItem_AsMaster.Text = "連接為主機";
            this.toolStripMenuItem_AsMaster.Click += new System.EventHandler(this.toolStripMenuItem_AsMaster_Click);
            // 
            // ToolStripMenuItem_addnew
            // 
            this.ToolStripMenuItem_addnew.Name = "ToolStripMenuItem_addnew";
            this.ToolStripMenuItem_addnew.Size = new System.Drawing.Size(136, 22);
            this.ToolStripMenuItem_addnew.Text = "新增自定義";
            this.ToolStripMenuItem_addnew.Click += new System.EventHandler(this.ToolStripMenuItem_addnew_Click);
            // 
            // backgroundWorker_scan
            // 
            this.backgroundWorker_scan.WorkerReportsProgress = true;
            this.backgroundWorker_scan.WorkerSupportsCancellation = true;
            this.backgroundWorker_scan.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_scan_DoWork);
            this.backgroundWorker_scan.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_scan_ProgressChanged);
            this.backgroundWorker_scan.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_scan_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 36);
            this.progressBar1.Maximum = 10000;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(261, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 14;
            // 
            // button_OK
            // 
            this.button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_OK.Location = new System.Drawing.Point(88, 120);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(89, 24);
            this.button_OK.TabIndex = 15;
            this.button_OK.Text = "確認";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(178, 120);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(89, 24);
            this.button_Cancel.TabIndex = 15;
            this.button_Cancel.Text = "關閉";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // NetWorkMode
            // 
            this.AcceptButton = this.button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 202);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button_scan);
            this.Controls.Add(this.dataGridView_IPSearch);
            this.Controls.Add(this.groupBox_master);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(662, 230);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(662, 230);
            this.Name = "NetWorkMode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "工作模式與網絡設定";
            this.TopMost = true;
            this.groupBox_master.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_IPSearch)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_master;
        private System.Windows.Forms.Button button_scan;
        private System.Windows.Forms.DataGridView dataGridView_IPSearch;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_AsMaster;
        private System.Windows.Forms.Panel panel_master;
        private System.ComponentModel.BackgroundWorker backgroundWorker_scan;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_MAC;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_workPort;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_addnew;


    }
}