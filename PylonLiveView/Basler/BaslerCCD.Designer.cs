namespace Basler
{
    partial class BaslerCCD
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
                CCDClosing();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaslerCCD));
            this.deviceListView = new System.Windows.Forms.ListView();
            this.panel_camSetting = new System.Windows.Forms.Panel();
            this.comboBoxTriggerActivation = new PylonC.NETSupportLibrary.EnumerationComboBoxUserControl();
            this.comboBoxPixelFormat = new PylonC.NETSupportLibrary.EnumerationComboBoxUserControl();
            this.comboBoxTriggerMode = new PylonC.NETSupportLibrary.EnumerationComboBoxUserControl();
            this.sliderGain = new PylonC.NETSupportLibrary.MySliderUserControl();
            this.comboBoxTriggerSource = new PylonC.NETSupportLibrary.EnumerationComboBoxUserControl();
            this.sliderExposureTime = new PylonC.NETSupportLibrary.MySliderUserControl();
            this.mySlider_offset = new PylonC.NETSupportLibrary.MySlider();
            this.comboBoxExposureAuto = new PylonC.NETSupportLibrary.EnumerationComboBoxUserControl();
            this.sliderWidth = new PylonC.NETSupportLibrary.MySliderUserControl();
            this.comboBoxTestImage = new PylonC.NETSupportLibrary.EnumerationComboBoxUserControl();
            this.mySlider_gain = new PylonC.NETSupportLibrary.MySlider();
            this.sliderHeight = new PylonC.NETSupportLibrary.MySliderUserControl();
            this.updateDeviceListTimer = new System.Windows.Forms.Timer(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonOneShot = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonContinuousShot = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_manualCapture = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.panel_camSetting.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // deviceListView
            // 
            this.deviceListView.BackColor = System.Drawing.Color.CadetBlue;
            this.deviceListView.Dock = System.Windows.Forms.DockStyle.Top;
            this.deviceListView.Location = new System.Drawing.Point(0, 39);
            this.deviceListView.MultiSelect = false;
            this.deviceListView.Name = "deviceListView";
            this.deviceListView.ShowItemToolTips = true;
            this.deviceListView.Size = new System.Drawing.Size(243, 37);
            this.deviceListView.TabIndex = 1;
            this.deviceListView.UseCompatibleStateImageBehavior = false;
            this.deviceListView.View = System.Windows.Forms.View.Tile;
            this.deviceListView.SelectedIndexChanged += new System.EventHandler(this.deviceListView_SelectedIndexChanged);
            // 
            // panel_camSetting
            // 
            this.panel_camSetting.BackColor = System.Drawing.Color.SteelBlue;
            this.panel_camSetting.Controls.Add(this.comboBoxTriggerActivation);
            this.panel_camSetting.Controls.Add(this.comboBoxPixelFormat);
            this.panel_camSetting.Controls.Add(this.comboBoxTriggerMode);
            this.panel_camSetting.Controls.Add(this.sliderGain);
            this.panel_camSetting.Controls.Add(this.comboBoxTriggerSource);
            this.panel_camSetting.Controls.Add(this.sliderExposureTime);
            this.panel_camSetting.Controls.Add(this.mySlider_offset);
            this.panel_camSetting.Controls.Add(this.comboBoxExposureAuto);
            this.panel_camSetting.Controls.Add(this.sliderWidth);
            this.panel_camSetting.Controls.Add(this.comboBoxTestImage);
            this.panel_camSetting.Controls.Add(this.mySlider_gain);
            this.panel_camSetting.Controls.Add(this.sliderHeight);
            this.panel_camSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_camSetting.Location = new System.Drawing.Point(0, 76);
            this.panel_camSetting.Name = "panel_camSetting";
            this.panel_camSetting.Size = new System.Drawing.Size(243, 439);
            this.panel_camSetting.TabIndex = 2;
            // 
            // comboBoxTriggerActivation
            // 
            this.comboBoxTriggerActivation.DisplayName = "觸發邊沿";
            this.comboBoxTriggerActivation.Location = new System.Drawing.Point(3, 77);
            this.comboBoxTriggerActivation.Name = "comboBoxTriggerActivation";
            this.comboBoxTriggerActivation.NodeName = "TriggerActivation";
            this.comboBoxTriggerActivation.Size = new System.Drawing.Size(231, 22);
            this.comboBoxTriggerActivation.TabIndex = 14;
            // 
            // comboBoxPixelFormat
            // 
            this.comboBoxPixelFormat.DisplayName = "像素格式";
            this.comboBoxPixelFormat.Location = new System.Drawing.Point(3, 32);
            this.comboBoxPixelFormat.Name = "comboBoxPixelFormat";
            this.comboBoxPixelFormat.NodeName = "PixelFormat";
            this.comboBoxPixelFormat.Size = new System.Drawing.Size(231, 22);
            this.comboBoxPixelFormat.TabIndex = 13;
            // 
            // comboBoxTriggerMode
            // 
            this.comboBoxTriggerMode.DisplayName = "觸發模式";
            this.comboBoxTriggerMode.Location = new System.Drawing.Point(3, 55);
            this.comboBoxTriggerMode.Name = "comboBoxTriggerMode";
            this.comboBoxTriggerMode.NodeName = "TriggerMode";
            this.comboBoxTriggerMode.Size = new System.Drawing.Size(231, 22);
            this.comboBoxTriggerMode.TabIndex = 15;
            // 
            // sliderGain
            // 
            this.sliderGain.DisplayName = "相機增益";
            this.sliderGain.Location = new System.Drawing.Point(7, 163);
            this.sliderGain.MaximumSize = new System.Drawing.Size(875, 46);
            this.sliderGain.Name = "sliderGain";
            this.sliderGain.NodeName = "GainRaw";
            this.sliderGain.Size = new System.Drawing.Size(233, 24);
            this.sliderGain.TabIndex = 8;
            // 
            // comboBoxTriggerSource
            // 
            this.comboBoxTriggerSource.DisplayName = "觸發源";
            this.comboBoxTriggerSource.Location = new System.Drawing.Point(3, 99);
            this.comboBoxTriggerSource.Name = "comboBoxTriggerSource";
            this.comboBoxTriggerSource.NodeName = "TriggerSource";
            this.comboBoxTriggerSource.Size = new System.Drawing.Size(231, 22);
            this.comboBoxTriggerSource.TabIndex = 16;
            // 
            // sliderExposureTime
            // 
            this.sliderExposureTime.DisplayName = "曝光值";
            this.sliderExposureTime.Location = new System.Drawing.Point(7, 186);
            this.sliderExposureTime.MaximumSize = new System.Drawing.Size(875, 46);
            this.sliderExposureTime.Name = "sliderExposureTime";
            this.sliderExposureTime.NodeName = "ExposureTimeRaw";
            this.sliderExposureTime.Size = new System.Drawing.Size(233, 24);
            this.sliderExposureTime.TabIndex = 9;
            // 
            // mySlider_offset
            // 
            this.mySlider_offset._Maximum = 255;
            this.mySlider_offset._Minimum = -255;
            this.mySlider_offset._Name = "偏移(處理):";
            this.mySlider_offset._SmallChange = 1;
            this.mySlider_offset._Value = 0;
            this.mySlider_offset.Location = new System.Drawing.Point(9, 279);
            this.mySlider_offset.Name = "mySlider_offset";
            this.mySlider_offset.Size = new System.Drawing.Size(233, 24);
            this.mySlider_offset.TabIndex = 18;
            // 
            // comboBoxExposureAuto
            // 
            this.comboBoxExposureAuto.DisplayName = "曝光模式";
            this.comboBoxExposureAuto.Location = new System.Drawing.Point(3, 121);
            this.comboBoxExposureAuto.Name = "comboBoxExposureAuto";
            this.comboBoxExposureAuto.NodeName = "ExposureAuto";
            this.comboBoxExposureAuto.Size = new System.Drawing.Size(231, 22);
            this.comboBoxExposureAuto.TabIndex = 12;
            // 
            // sliderWidth
            // 
            this.sliderWidth.DisplayName = "圖片寬";
            this.sliderWidth.Location = new System.Drawing.Point(7, 209);
            this.sliderWidth.MaximumSize = new System.Drawing.Size(875, 46);
            this.sliderWidth.Name = "sliderWidth";
            this.sliderWidth.NodeName = "Width";
            this.sliderWidth.Size = new System.Drawing.Size(233, 24);
            this.sliderWidth.TabIndex = 10;
            // 
            // comboBoxTestImage
            // 
            this.comboBoxTestImage.DisplayName = "圖片選擇";
            this.comboBoxTestImage.Location = new System.Drawing.Point(3, 9);
            this.comboBoxTestImage.Name = "comboBoxTestImage";
            this.comboBoxTestImage.NodeName = "TestImageSelector";
            this.comboBoxTestImage.Size = new System.Drawing.Size(231, 22);
            this.comboBoxTestImage.TabIndex = 17;
            // 
            // mySlider_gain
            // 
            this.mySlider_gain._Maximum = 300;
            this.mySlider_gain._Minimum = 0;
            this.mySlider_gain._Name = "增益(處理):";
            this.mySlider_gain._SmallChange = 1;
            this.mySlider_gain._Value = 100;
            this.mySlider_gain.Location = new System.Drawing.Point(9, 257);
            this.mySlider_gain.Name = "mySlider_gain";
            this.mySlider_gain.Size = new System.Drawing.Size(233, 24);
            this.mySlider_gain.TabIndex = 19;
            // 
            // sliderHeight
            // 
            this.sliderHeight.DisplayName = "圖片高";
            this.sliderHeight.Location = new System.Drawing.Point(7, 232);
            this.sliderHeight.MaximumSize = new System.Drawing.Size(875, 46);
            this.sliderHeight.Name = "sliderHeight";
            this.sliderHeight.NodeName = "Height";
            this.sliderHeight.Size = new System.Drawing.Size(233, 24);
            this.sliderHeight.TabIndex = 11;
            // 
            // updateDeviceListTimer
            // 
            this.updateDeviceListTimer.Interval = 5000;
            this.updateDeviceListTimer.Tick += new System.EventHandler(this.updateDeviceListTimer_Tick);
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.Color.SteelBlue;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOneShot,
            this.toolStripButtonContinuousShot,
            this.toolStripButtonStop,
            this.toolStripButtonClose,
            this.toolStripButton_manualCapture,
            this.toolStripSeparator5});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(243, 39);
            this.toolStrip.TabIndex = 7;
            this.toolStrip.Text = "toolStrip";
            // 
            // toolStripButtonOneShot
            // 
            this.toolStripButtonOneShot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOneShot.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOneShot.Image")));
            this.toolStripButtonOneShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOneShot.Name = "toolStripButtonOneShot";
            this.toolStripButtonOneShot.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonOneShot.Text = "One Shot";
            this.toolStripButtonOneShot.ToolTipText = "One Shot";
            this.toolStripButtonOneShot.Click += new System.EventHandler(this.toolStripButtonOneShot_Click);
            // 
            // toolStripButtonContinuousShot
            // 
            this.toolStripButtonContinuousShot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonContinuousShot.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonContinuousShot.Image")));
            this.toolStripButtonContinuousShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonContinuousShot.Name = "toolStripButtonContinuousShot";
            this.toolStripButtonContinuousShot.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonContinuousShot.Text = "Continuous Shot";
            this.toolStripButtonContinuousShot.ToolTipText = "Continuous Shot";
            this.toolStripButtonContinuousShot.Click += new System.EventHandler(this.toolStripButtonContinuousShot_Click);
            // 
            // toolStripButtonStop
            // 
            this.toolStripButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStop.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStop.Image")));
            this.toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStop.Name = "toolStripButtonStop";
            this.toolStripButtonStop.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonStop.Text = "Stop Grab";
            this.toolStripButtonStop.ToolTipText = "Stop Grab";
            this.toolStripButtonStop.Click += new System.EventHandler(this.toolStripButtonStop_Click);
            // 
            // toolStripButtonClose
            // 
            this.toolStripButtonClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonClose.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonClose.Image")));
            this.toolStripButtonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClose.Name = "toolStripButtonClose";
            this.toolStripButtonClose.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonClose.Text = "toolStripButton1";
            this.toolStripButtonClose.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripButton_manualCapture
            // 
            this.toolStripButton_manualCapture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_manualCapture.Image = global::Basler.Properties.Resources.init1;
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
            // BaslerCCD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_camSetting);
            this.Controls.Add(this.deviceListView);
            this.Controls.Add(this.toolStrip);
            this.Name = "BaslerCCD";
            this.Size = new System.Drawing.Size(243, 515);
            this.panel_camSetting.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView deviceListView;
        private System.Windows.Forms.Panel panel_camSetting;
        private PylonC.NETSupportLibrary.EnumerationComboBoxUserControl comboBoxTriggerActivation;
        private PylonC.NETSupportLibrary.EnumerationComboBoxUserControl comboBoxPixelFormat;
        private PylonC.NETSupportLibrary.EnumerationComboBoxUserControl comboBoxTriggerMode;
        private PylonC.NETSupportLibrary.MySliderUserControl sliderGain;
        private PylonC.NETSupportLibrary.EnumerationComboBoxUserControl comboBoxTriggerSource;
        private PylonC.NETSupportLibrary.MySliderUserControl sliderExposureTime;
        private PylonC.NETSupportLibrary.MySlider mySlider_offset;
        private PylonC.NETSupportLibrary.EnumerationComboBoxUserControl comboBoxExposureAuto;
        private PylonC.NETSupportLibrary.MySliderUserControl sliderWidth;
        private PylonC.NETSupportLibrary.EnumerationComboBoxUserControl comboBoxTestImage;
        private PylonC.NETSupportLibrary.MySlider mySlider_gain;
        private PylonC.NETSupportLibrary.MySliderUserControl sliderHeight;
        private System.Windows.Forms.Timer updateDeviceListTimer;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonOneShot;
        private System.Windows.Forms.ToolStripButton toolStripButtonContinuousShot;
        private System.Windows.Forms.ToolStripButton toolStripButtonStop;
        private System.Windows.Forms.ToolStripButton toolStripButtonClose;
        private System.Windows.Forms.ToolStripButton toolStripButton_manualCapture;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}
