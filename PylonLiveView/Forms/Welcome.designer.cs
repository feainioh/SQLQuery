namespace PylonLiveView
{
    partial class Welcome
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Welcome));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_Barcode = new System.Windows.Forms.RadioButton();
            this.radioButton_prox = new System.Windows.Forms.RadioButton();
            this.radioButton_mic = new System.Windows.Forms.RadioButton();
            this.button_select = new System.Windows.Forms.Button();
            this.label_type = new System.Windows.Forms.Label();
            this.button_cancel = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.chooseLineName1 = new PylonLiveView.ChooseLineName();
            this.radioButton_IC = new System.Windows.Forms.RadioButton();
            this.panel8.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(145, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "系統初始化中......請稍候";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("方正姚体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.CadetBlue;
            this.label2.Location = new System.Drawing.Point(74, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(366, 54);
            this.label2.TabIndex = 0;
            this.label2.Text = "條碼關聯上位機";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.LightGray;
            this.panel8.Controls.Add(this.label2);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(509, 56);
            this.panel8.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_IC);
            this.groupBox1.Controls.Add(this.radioButton_Barcode);
            this.groupBox1.Controls.Add(this.radioButton_prox);
            this.groupBox1.Controls.Add(this.radioButton_mic);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(77, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(349, 56);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "關聯部品類型選擇";
            // 
            // radioButton_Barcode
            // 
            this.radioButton_Barcode.AutoSize = true;
            this.radioButton_Barcode.Location = new System.Drawing.Point(6, 26);
            this.radioButton_Barcode.Name = "radioButton_Barcode";
            this.radioButton_Barcode.Size = new System.Drawing.Size(102, 18);
            this.radioButton_Barcode.TabIndex = 2;
            this.radioButton_Barcode.Text = "BARCODE關聯";
            this.radioButton_Barcode.UseVisualStyleBackColor = true;
            // 
            // radioButton_prox
            // 
            this.radioButton_prox.AutoSize = true;
            this.radioButton_prox.Location = new System.Drawing.Point(110, 26);
            this.radioButton_prox.Name = "radioButton_prox";
            this.radioButton_prox.Size = new System.Drawing.Size(81, 18);
            this.radioButton_prox.TabIndex = 1;
            this.radioButton_prox.Text = "PROX關聯";
            this.radioButton_prox.UseVisualStyleBackColor = true;
            // 
            // radioButton_mic
            // 
            this.radioButton_mic.AutoSize = true;
            this.radioButton_mic.Checked = true;
            this.radioButton_mic.Location = new System.Drawing.Point(195, 26);
            this.radioButton_mic.Name = "radioButton_mic";
            this.radioButton_mic.Size = new System.Drawing.Size(74, 18);
            this.radioButton_mic.TabIndex = 0;
            this.radioButton_mic.TabStop = true;
            this.radioButton_mic.Text = "MIC關聯";
            this.radioButton_mic.UseVisualStyleBackColor = true;
            // 
            // button_select
            // 
            this.button_select.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_select.Location = new System.Drawing.Point(46, 247);
            this.button_select.Name = "button_select";
            this.button_select.Size = new System.Drawing.Size(174, 28);
            this.button_select.TabIndex = 6;
            this.button_select.Text = "程序啟動";
            this.button_select.UseVisualStyleBackColor = true;
            this.button_select.Click += new System.EventHandler(this.button_select_Click);
            // 
            // label_type
            // 
            this.label_type.AutoSize = true;
            this.label_type.Font = new System.Drawing.Font("幼圆", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_type.ForeColor = System.Drawing.Color.Red;
            this.label_type.Location = new System.Drawing.Point(208, 82);
            this.label_type.Name = "label_type";
            this.label_type.Size = new System.Drawing.Size(99, 24);
            this.label_type.TabIndex = 7;
            this.label_type.Text = "MIC關聯";
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(290, 247);
            this.button_cancel.Margin = new System.Windows.Forms.Padding(2);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(174, 28);
            this.button_cancel.TabIndex = 9;
            this.button_cancel.Text = "退出";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // chooseLineName1
            // 
            this.chooseLineName1.Location = new System.Drawing.Point(-1, 119);
            this.chooseLineName1.Margin = new System.Windows.Forms.Padding(2);
            this.chooseLineName1.Name = "chooseLineName1";
            this.chooseLineName1.Size = new System.Drawing.Size(510, 123);
            this.chooseLineName1.TabIndex = 10;
            // 
            // radioButton_IC
            // 
            this.radioButton_IC.AutoSize = true;
            this.radioButton_IC.Checked = true;
            this.radioButton_IC.Location = new System.Drawing.Point(273, 26);
            this.radioButton_IC.Name = "radioButton_IC";
            this.radioButton_IC.Size = new System.Drawing.Size(67, 18);
            this.radioButton_IC.TabIndex = 3;
            this.radioButton_IC.TabStop = true;
            this.radioButton_IC.Text = "IC關聯";
            this.radioButton_IC.UseVisualStyleBackColor = true;
            // 
            // Welcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(509, 278);
            this.Controls.Add(this.chooseLineName1);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label_type);
            this.Controls.Add(this.button_select);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel8);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Welcome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Welcome_FormClosing);
            this.Load += new System.EventHandler(this.Welcome_Load);
            this.Shown += new System.EventHandler(this.Welcome_Shown);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_prox;
        private System.Windows.Forms.RadioButton radioButton_mic;
        private System.Windows.Forms.Button button_select;
        private System.Windows.Forms.Label label_type;
        private System.Windows.Forms.Button button_cancel;
        private ChooseLineName chooseLineName1;
        private System.Windows.Forms.RadioButton radioButton_Barcode;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.RadioButton radioButton_IC;

    }
}