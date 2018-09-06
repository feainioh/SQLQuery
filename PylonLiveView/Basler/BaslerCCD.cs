using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PylonC.NETSupportLibrary;
using System.Drawing.Imaging;
using System.Threading;

namespace Basler
{
    public partial class BaslerCCD : UserControl
    {
        public static PylonC.NETSupportLibrary.ImageProvider m_imageProvider
            = new PylonC.NETSupportLibrary.ImageProvider(); /* Create one image provider. */        
        private Bitmap m_bitmap = null; /* The bitmap is used for displaying the image. */

        private string m_CCDName = ""; //CCD名称
        private string m_BaslerFormName = ""; //区分上下CCD
        private bool m_ScanAuthorized = false;  //标志位
        private bool m_inScanFunction = false;  //busy标志
        public bool m_CCDInitOK = false;
        public bool m_bIsContinuePic = false; //连续拍照
        #region 委托
        /// <summary>
        /// 图片获取
        /// </summary>
        /// <param name="bmp"></param>
        public delegate void dele_bmpReceived(ImageProvider.Image bmp);
        private dele_bmpReceived m_event_bmpReceive;
        public event dele_bmpReceived event_bmpReceive
        {
            add
            {
                if (m_event_bmpReceive == null)
                {
                    m_event_bmpReceive += value;
                }
            }
            remove
            {
                m_event_bmpReceive -= value;
            }
        }
        /// <summary>
        /// 日志提示
        /// </summary>
        /// <param name="str"></param>
        public delegate void dele_StatusText(string str);
        private dele_StatusText m_eveInstagram_StatusText;
        public event dele_StatusText event_StatusText
        {
            add
            {
                if (m_eveInstagram_StatusText == null)
                {
                    m_eveInstagram_StatusText += value;
                }
            }
            remove
            {
                m_eveInstagram_StatusText -= value;
            }
        }
        #endregion

        public BaslerCCD()
        {
            InitializeComponent();
            //InitCCDCamera();
        }

        //初始化相机
        public void InitCCDCamera()
        {
            try
            {
                //this.Invoke(new Action(() =>
                //{
                    m_CCDInitOK = false;
                    SetStatusText("初始化相机中。。");
                    initImageProvider();
                    SetStatusText("初始化相机" + (m_CCDInitOK ? "成功" : "失败"));
                //}));
            }
            catch { }
        }

        //开始拍照
        public void StartOneShot()
        {
            OneShot();
        }

        //停止拍照
        public void StopCCD()
        {
            Stop();
        }

        //曝光值
        public void SetExposureValue(int value)
        {
            this.Invoke(new Action(() =>
            {
                sliderExposureTime.valueChanged(value);
            }));
        }

        //信息提示
        private void SetStatusText(string str)
        {
            if (m_eveInstagram_StatusText != null)
                m_eveInstagram_StatusText(str);
        }
        
        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            if (m_bitmap != null)
            {
                m_bitmap.Save("d:\\capture.bmp");
            }
        }

        private void toolStripButton_manualCapture_Click(object sender, EventArgs e)
        {
            InitCCDCamera();
        }

        #region 相机功能模块
        private void initImageProvider()
        {
            try
            {                
                /* Register for the events of the image provider needed for proper operation. */
                m_imageProvider.GrabErrorEvent += new ImageProvider.GrabErrorEventHandler(OnGrabErrorEventCallback);
                m_imageProvider.DeviceRemovedEvent += new ImageProvider.DeviceRemovedEventHandler(OnDeviceRemovedEventCallback);
                m_imageProvider.DeviceOpenedEvent += new ImageProvider.DeviceOpenedEventHandler(OnDeviceOpenedEventCallback);
                m_imageProvider.DeviceClosedEvent += new ImageProvider.DeviceClosedEventHandler(OnDeviceClosedEventCallback);
                m_imageProvider.GrabbingStartedEvent += new ImageProvider.GrabbingStartedEventHandler(OnGrabbingStartedEventCallback);
                m_imageProvider.ImageReadyEvent += new ImageProvider.ImageReadyEventHandler(OnImageReadyEventCallback);
                m_imageProvider.GrabbingStoppedEvent += new ImageProvider.GrabbingStoppedEventHandler(OnGrabbingStoppedEventCallback);

                /* Provide the controls in the lower left area with the image provider object. */
                sliderGain.MyImageProvider = m_imageProvider;
                sliderExposureTime.MyImageProvider = m_imageProvider;
                sliderHeight.MyImageProvider = m_imageProvider;
                sliderWidth.MyImageProvider = m_imageProvider;
                comboBoxTestImage.MyImageProvider = m_imageProvider;
                comboBoxPixelFormat.MyImageProvider = m_imageProvider;
                comboBoxTriggerActivation.MyImageProvider = m_imageProvider;
                comboBoxTriggerSource.MyImageProvider = m_imageProvider;
                comboBoxExposureAuto.MyImageProvider = m_imageProvider;
                comboBoxTriggerMode.MyImageProvider = m_imageProvider;

                /* Update the list of available devices in the upper left area. */
                UpdateDeviceList();

                /* Enable the tool strip buttons according to the state of the image provider. */
                EnableButtons(m_imageProvider.IsOpen, false);
            }
            catch (Exception e)
            { throw new Exception(e.ToString()); }
        }

        /* Handles the click on the single frame button. */
        private void toolStripButtonOneShot_Click(object sender, EventArgs e)
        {
            OneShot(); /* Starts the grabbing of one image. */
        }

        /* Handles the click on the continuous frame button. */
        private void toolStripButtonContinuousShot_Click(object sender, EventArgs e)
        {
            m_bIsContinuePic = true;
            ContinuousShot(); /* Start the grabbing of images until grabbing is stopped. */
        }

        /* Handles the click on the stop frame acquisition button. */
        private void toolStripButtonStop_Click(object sender, EventArgs e)
        {
            m_bIsContinuePic = false;
            Stop(); /* Stops the grabbing of images. */
        }

        /* Handles the event related to the occurrence of an error while grabbing proceeds. */
        private void OnGrabErrorEventCallback(Exception grabException, string additionalErrorMessage)
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the BeginInvoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.GrabErrorEventHandler(OnGrabErrorEventCallback), grabException, additionalErrorMessage);
                return;
            }
            ShowException(grabException, additionalErrorMessage);
        }

        /* Handles the event related to the removal of a currently open device. */
        private void OnDeviceRemovedEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the BeginInvoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.DeviceRemovedEventHandler(OnDeviceRemovedEventCallback));
                return;
            }
            /* Disable the buttons. */
            EnableButtons(false, false);
            /* Stops the grabbing of images. */
            Stop();
            /* Close the image provider. */
            CloseTheImageProvider();
            /* Since one device is gone, the list needs to be updated. */
            UpdateDeviceList();
        }

        /* Handles the event related to a device being open. */
        private void OnDeviceOpenedEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the BeginInvoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.DeviceOpenedEventHandler(OnDeviceOpenedEventCallback));
                return;
            }
            /* The image provider is ready to grab. Enable the grab buttons. */
            EnableButtons(true, false);
        }

        /* Handles the event related to a device being closed. */
        private void OnDeviceClosedEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the BeginInvoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.DeviceClosedEventHandler(OnDeviceClosedEventCallback));
                return;
            }
            /* The image provider is closed. Disable all buttons. */
            EnableButtons(false, false);
        }

        /* Handles the event related to the image provider executing grabbing. */
        private void OnGrabbingStartedEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the BeginInvoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.GrabbingStartedEventHandler(OnGrabbingStartedEventCallback));
                return;
            }

            ///* Do not update device list while grabbing to avoid jitter because the GUI-Thread is blocked for a short time when enumerating. */
            //updateDeviceListTimer.Stop();
            //updateDeviceListTimer.Enabled = false;
            //updateDeviceListTimer = null;

            /* The image provider is grabbing. Disable the grab buttons. Enable the stop button. */
            EnableButtons(false, true);
        }

        /* 获取照片  Handles the event related to an image having been taken and waiting for processing. */
        private void OnImageReadyEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the BeginInvoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.ImageReadyEventHandler(OnImageReadyEventCallback));
                return;
            }
            try
            {
                /* Acquire the image from the image provider. Only show the latest image. The camera may acquire images faster than images can be displayed*/
                ImageProvider.Image image = m_imageProvider.GetLatestImage();

                /* Check if the image has been removed in the meantime. */
                if (image != null)
                {
                    if (m_event_bmpReceive != null)
                        m_event_bmpReceive(image);
                    m_imageProvider.ReleaseImage();
                }
            }
            catch (Exception e)
            {
                //logWR.appendNewLogMessage("照片采集OnImageReadyEventCallback error : \r\n" + e.ToString());
            }
        }

        private Bitmap DrawDiagonalLines(Bitmap bmp)
        {
            try
            {
                Bitmap result = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(result);
                //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(bmp, 0, 0);
                Pen p = new Pen(Color.Green, 4);
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                g.DrawLine(p, new Point(0, bmp.Height / 2), new Point(bmp.Width, bmp.Height / 2));
                g.DrawLine(p, new Point(bmp.Width / 2, 0), new Point(bmp.Width / 2, bmp.Height));
                g.Save();
                return result;
            }
            catch
            {
                return new Bitmap(640, 480);
            }
        }

        /* Handles the event related to the image provider having stopped grabbing. */
        private void OnGrabbingStoppedEventCallback()
        {
            if (InvokeRequired)
            {
                /* If called from a different thread, we must use the BeginInvoke method to marshal the call to the proper thread. */
                BeginInvoke(new ImageProvider.GrabbingStoppedEventHandler(OnGrabbingStoppedEventCallback));
                return;
            }

            /* Enable device list update again */
            //updateDeviceListTimer.Start();

            /* The image provider stopped grabbing. Enable the grab buttons. Disable the stop button. */
            EnableButtons(m_imageProvider.IsOpen, false);
        }

        /* Helps to set the states of all buttons. */
        private void EnableButtons(bool canGrab, bool canStop)
        {
            return;
            toolStripButtonContinuousShot.Enabled = canGrab;
            toolStripButtonOneShot.Enabled = canGrab;
            toolStripButtonStop.Enabled = canStop;
        }

        /* Stops the image provider and handles exceptions. */
        private void Stop()
        {
            /* Stop the grabbing. */
            try
            {
                m_imageProvider.Stop();
            }
            catch (Exception e)
            {
                ShowException(e, m_imageProvider.GetLastErrorMessage());
            }
        }

        /* Closes the image provider and handles exceptions. */
        private void CloseTheImageProvider()
        {
            /* Close the image provider. */
            try
            {
                m_imageProvider.Close();
            }
            catch (Exception e)
            {
                ShowException(e, m_imageProvider.GetLastErrorMessage());
            }
        }

        /* Starts the grabbing of one image and handles exceptions. */
        private void OneShot()
        {
            try
            {
                m_bIsContinuePic = false;
                m_imageProvider.OneShot(); /* Starts the grabbing of one image. */
            }
            catch (Exception e)
            {
                ShowException(e, m_imageProvider.GetLastErrorMessage());
            }
        }

        /* Starts the grabbing of images until the grabbing is stopped and handles exceptions. */
        private void ContinuousShot()
        {
            try
            {
                m_imageProvider.ContinuousShot(); /* Start the grabbing of images until grabbing is stopped. */
            }
            catch (Exception e)
            {
                ShowException(e, m_imageProvider.GetLastErrorMessage());
            }
        }

        /* Updates the list of available devices in the upper left area. */
        private void UpdateDeviceList()
        {
            try
            {
                /* Ask the device enumerator for a list of devices. */
                List<DeviceEnumerator.Device> list = DeviceEnumerator.EnumerateDevices();

                ListView.ListViewItemCollection items = deviceListView.Items;

                /* Add each new device to the list. */
                foreach (DeviceEnumerator.Device device in list)
                {
                    //this.m_CCDName = device.Name.Substring(0, device.Name.IndexOf("(")).Trim(); //CCD名称
                    //if (m_BaslerFormName != m_CCDName)
                    //    continue;
                    //else
                        m_CCDInitOK = true;

                    bool newitem = true;
                    /* For each enumerated device check whether it is in the list view. */
                    foreach (ListViewItem item in items)
                    {
                        /* Retrieve the device data from the list view item. */
                        DeviceEnumerator.Device tag = item.Tag as DeviceEnumerator.Device;

                        if (tag.FullName == device.FullName)
                        {
                            /* Update the device index. The index is used for opening the camera. It may change when enumerating devices. */
                            tag.Index = device.Index;
                            /* No new item needs to be added to the list view */
                            newitem = false;
                            break;
                        }
                    }

                    /* If the device is not in the list view yet the add it to the list view. */
                    if (newitem)
                    {
                        ListViewItem item = new ListViewItem(device.Name);
                        if (device.Tooltip.Length > 0)
                        {
                            item.ToolTipText = device.Tooltip;
                        }
                        item.Tag = device;

                        /* Attach the device data. */
                        deviceListView.Items.Add(item);
                        camConnectAutomatic();
                    }
                }

                /* Delete old devices which are removed. */
                foreach (ListViewItem item in items)
                {
                    bool exists = false;

                    /* For each device in the list view check whether it has not been found by device enumeration. */
                    foreach (DeviceEnumerator.Device device in list)
                    {
                        if (((DeviceEnumerator.Device)item.Tag).FullName == device.FullName)
                        {
                            exists = true;
                            break;
                        }
                    }
                    /* If the device has not been found by enumeration then remove from the list view. */
                    if (!exists)
                    {
                        deviceListView.Items.Remove(item);
                    }
                }
            }
            catch (Exception e)
            {
                m_CCDInitOK = false;
                ShowException(e, m_imageProvider.GetLastErrorMessage());
            }
        }

        /* Shows exceptions in a message box. */
        private void ShowException(Exception e, string additionalErrorMessage)
        {
            string more = "\n\nLast error message (may not belong to the exception):\n" + additionalErrorMessage;
            MessageBox.Show("Exception caught:\n" + e.Message + (additionalErrorMessage.Length > 0 ? more : ""), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /* Closes the image provider when the window is closed. */
        public void CCDClosing()
        {
            /* Stops the grabbing of images. */
            Stop();
            /* Close the image provider. */
            CloseTheImageProvider();

            try
            {
            }
            catch { }
            //Application.Exit();
            ////System.GC.Collect();


            ////Thread.Sleep(50);
            ////Application.ExitThread();
            ////System.Environment.Exit(0);
        }

        /* Handles the selection of cameras from the list box. The currently open device is closed and the first 
         selected device is opened. */
        private void deviceListView_SelectedIndexChanged(object sender, EventArgs ev)
        {
            ConnectWebCam();
        }

        private void ConnectWebCam()
        {
            /* Close the currently open image provider. */
            /* Stops the grabbing of images. */
            Stop();
            /* Close the image provider. */
            CloseTheImageProvider();

            /* Open the selected image provider. */
            //if (deviceListView.SelectedItems.Count > 0)
            if (deviceListView.Items.Count > 0)
            {
                /* Get the first selected item. */
                //ListViewItem item = deviceListView.SelectedItems[0];
                ListViewItem item = deviceListView.Items[0];
                /* Get the attached device data. */
                DeviceEnumerator.Device device = item.Tag as DeviceEnumerator.Device;
                try
                {
                    /* Open the image provider using the index from the device data. */
                    m_imageProvider.Open(device.Index);
                }
                catch (Exception e)
                {
                    ShowException(e, m_imageProvider.GetLastErrorMessage());
                }
            }
        }

        /* If the F5 key has been pressed update the list of devices. */
        private void deviceListView_KeyDown(object sender, KeyEventArgs ev)
        {
            if (ev.KeyCode == Keys.F5)
            {
                ev.Handled = true;
                /* Update the list of available devices in the upper left area. */
                UpdateDeviceList();
            }
        }

        /* Timer callback used for periodically checking whether displayed devices are still attached to the PC. */
        private void updateDeviceListTimer_Tick(object sender, EventArgs e)
        {
            UpdateDeviceList(); //fotest
        }

        private void camConnectAutomatic()
        {
            try
            {
                if (deviceListView.Items.Count > 0)
                {
                    ConnectWebCam();
                    //ContinuousShot(); /* Start the grabbing of images until grabbing is stopped. */
                }
                /* Do not update device list while grabbing to avoid jitter because the GUI-Thread is blocked for a short time when enumerating. */
                updateDeviceListTimer.Stop();
                updateDeviceListTimer.Enabled = false;
                updateDeviceListTimer = null;
            }
            catch { }
        }
        #endregion

    }

    public class BitmapInfo
    {
        public int FlowID;
        public Bitmap bitmap = new Bitmap(640, 480);
        public int num;
        //public bool m_inProcessing = false;   //是否正在处理中
        public bool m_processed = false;  //是否已經被處理
    }
}
