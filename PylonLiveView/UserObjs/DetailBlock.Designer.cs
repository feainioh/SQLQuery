namespace PylonLiveView
{
    partial class DetailBlock
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_save = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_magnify = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_pos_x = new System.Windows.Forms.Label();
            this.lbl_pos_y = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_result = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(173, 175);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_save,
            this.ToolStripMenuItem_magnify});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            // 
            // ToolStripMenuItem_save
            // 
            this.ToolStripMenuItem_save.Name = "ToolStripMenuItem_save";
            this.ToolStripMenuItem_save.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_save.Text = "图片存储";
            this.ToolStripMenuItem_save.Click += new System.EventHandler(this.ToolStripMenuItem_save_Click);
            // 
            // ToolStripMenuItem_magnify
            // 
            this.ToolStripMenuItem_magnify.Name = "ToolStripMenuItem_magnify";
            this.ToolStripMenuItem_magnify.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItem_magnify.Text = "放大視圖";
            this.ToolStripMenuItem_magnify.Click += new System.EventHandler(this.ToolStripMenuItem_magnify_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "X:";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(1, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "Y:";
            this.label4.Visible = false;
            // 
            // lbl_pos_x
            // 
            this.lbl_pos_x.AutoSize = true;
            this.lbl_pos_x.Location = new System.Drawing.Point(12, 0);
            this.lbl_pos_x.Name = "lbl_pos_x";
            this.lbl_pos_x.Size = new System.Drawing.Size(41, 12);
            this.lbl_pos_x.TabIndex = 0;
            this.lbl_pos_x.Text = "000.00";
            this.lbl_pos_x.Visible = false;
            // 
            // lbl_pos_y
            // 
            this.lbl_pos_y.AutoSize = true;
            this.lbl_pos_y.Location = new System.Drawing.Point(12, 12);
            this.lbl_pos_y.Name = "lbl_pos_y";
            this.lbl_pos_y.Size = new System.Drawing.Size(41, 12);
            this.lbl_pos_y.TabIndex = 0;
            this.lbl_pos_y.Text = "000.00";
            this.lbl_pos_y.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(173, 24);
            this.panel1.TabIndex = 2;
            this.panel1.Visible = false;
            // 
            // button_result
            // 
            this.button_result.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.button_result.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_result.Location = new System.Drawing.Point(0, 116);
            this.button_result.MaximumSize = new System.Drawing.Size(0, 23);
            this.button_result.MinimumSize = new System.Drawing.Size(0, 23);
            this.button_result.Name = "button_result";
            this.button_result.Size = new System.Drawing.Size(173, 23);
            this.button_result.TabIndex = 3;
            this.button_result.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_result.UseVisualStyleBackColor = false;
            // 
            // DetailBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_result);
            this.Controls.Add(this.lbl_pos_y);
            this.Controls.Add(this.lbl_pos_x);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "DetailBlock";
            this.Size = new System.Drawing.Size(173, 175);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_pos_x;
        private System.Windows.Forms.Label lbl_pos_y;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_result;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_save;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_magnify;
    }
}
