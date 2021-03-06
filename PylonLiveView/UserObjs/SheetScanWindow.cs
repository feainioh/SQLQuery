﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DirectShowLib;
using PylonLiveView;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using Euresys.Open_eVision_1_2;
using System.Threading;

namespace PylonLiveView
{
    public partial class SheetScanWindow : UserControl
    {
        public SheetScanWindow()
        {
            InitializeComponent();
            //InitialCamAutomatic();
        }

        private void SheetScanWindow_Load(object sender, EventArgs e)
        {

        }
        public int m_value_Focus;
        public int m_value_Zoom;
        public int m_value_Tilt;
        public int m_value_Pan;
        public int m_value_Exposure;

        IAMCameraControl ctl = null;   //lifeCam摄像头控制接口
        Capture cam_capture = new Capture();
        public int m_dev_index = 0; //摄像头资源索引
        string m_devicePath;  //视频设备地址
        public Bitmap m_bitmap;
        MyFunctions myfunc = new MyFunctions();
        private void InitialCamAutomatic()
        {
            try
            {
                //自动查找HD-5000设备
                DsDevice[] capDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
                for (int i = 0; i < capDevices.Length; i++)
                {
                    if (capDevices[i].DevicePath.IndexOf(GlobalVar.gl_LifeCam_DeviceID) >= 0)
                    {
                        object obj;
                        m_dev_index = i;
                        cam_capture = new Capture(m_dev_index, 20, 800, 600);
                        cam_capture.m_capGraph.FindInterface(PinCategory.Capture, MediaType.Audio, cam_capture.m_capFilter, typeof(IAMCameraControl).GUID, out obj);
                        ctl = obj as IAMCameraControl;
                        //ReadSettingConfig();
                        //ManualSetCameraConfig();
                        Capture();
                    }
                }
            }
            catch { }
        }

        public void Capture()
        {
            Thread threadCapture = new Thread(new ThreadStart(
                delegate
                {
                    for (; ; )
                    {
                        try
                        {
                            int errCount = 0;  //累计抓取失败次数，如果超过5次，重新实例化
                            Bitmap _bitmap = new Bitmap(640, 480);
                            if (this.IsDisposed || (cam_capture == null)) { return; }
                            bool _getBitmapSuccess = true;

                            IntPtr ip = IntPtr.Zero;
                            ip = cam_capture.GetBitMap();
                            if (ip == (IntPtr)0)  //--抓取返回（INTPTR)0,视频采集出错，重新采集
                            {
                                _bitmap = new Bitmap(640, 480);
                                if (errCount > 10)
                                {
                                    resetCaputre();
                                    Marshal.FreeCoTaskMem(ip);
                                    GC.Collect();
                                    _getBitmapSuccess = false;
                                }
                                errCount++;
                            }
                            else
                            {
                                errCount = 0;
                            }

                            Bitmap _bmp = new Bitmap(640, 480);
                            if ((ip != (IntPtr)0) && _getBitmapSuccess)
                            {
                                _bmp = new Bitmap(cam_capture.Width, cam_capture.Height, cam_capture.Stride, PixelFormat.Format24bppRgb, ip);
                            }
                            _bitmap = new Bitmap(_bmp);
                            Bitmap bmp_fordis = (Bitmap)_bitmap.Clone();
                            m_bitmap = (Bitmap)_bmp.Clone();
                            //释放指针ip
                            Marshal.FreeCoTaskMem(ip);
                            //回收垃圾
                            GC.Collect();
                            BeginInvoke(new Action(() =>
                                {
                                    pictureBox1.Image = bmp_fordis;
                                }));
                        }
                        catch { }
                        Thread.Sleep(50);
                    }
                }));
            threadCapture.IsBackground = true;
            threadCapture.Start();
        }

        public string decodeResult()
        {
            string result = "";
            //for (int i = 0; i < 30; i++)
            {
                try
                {
                    MatrixDecode decoder = new MatrixDecode();
                    EImageBW8 BW8IMAGE = decoder.ConvertBitmapToEImageBW8((Bitmap)m_bitmap.Clone());
                    result = decoder.GetDecodeStrbyEImageBW8(BW8IMAGE);

                    //if ((result.Length == GlobalVar.gl_length_sheetBarcodeLength)
                    //if (myfunc.checkStringIsLegal(result, 3))
                    //{
                    //    break;  //成功
                    //}
                }
                catch { }
                Thread.Sleep(50);
            }
            return result;
        }

        //摄像头自动调准
        public void setCamAutoAdjust()
        {
            try
            {
                ctl.Set(CameraControlProperty.Focus, 0, CameraControlFlags.Auto);
                ctl.Set(CameraControlProperty.Zoom, 0, CameraControlFlags.Auto);
                ctl.Set(CameraControlProperty.Tilt, 0, CameraControlFlags.Auto);
                ctl.Set(CameraControlProperty.Pan, 0, CameraControlFlags.Auto);
                ReadCamParaConfig();
            }
            catch { }
        }
        
        //摄像头手动调准
        public void ManualSetCameraConfig()
        {
            try
            {
                ctl.Set(CameraControlProperty.Focus, m_value_Focus, CameraControlFlags.Manual);
                ctl.Set(CameraControlProperty.Zoom, m_value_Zoom, CameraControlFlags.Manual);
                ctl.Set(CameraControlProperty.Pan, m_value_Pan, CameraControlFlags.Manual);
                ctl.Set(CameraControlProperty.Tilt, m_value_Tilt, CameraControlFlags.Manual);
                ctl.Set(CameraControlProperty.Exposure, m_value_Exposure, CameraControlFlags.Manual);
            }
            catch { }
        }

        public void ReadCamParaConfig()
        {
            try
            {
                if ((ctl == null) || (m_devicePath.IndexOf(GlobalVar.gl_LifeCam_DeviceID) < 0))
                { return; }
                CameraControlFlags s = new CameraControlFlags();
                ctl.Get(CameraControlProperty.Focus, out m_value_Focus, out s);
                ctl.Get(CameraControlProperty.Zoom, out m_value_Zoom, out s);
                ctl.Get(CameraControlProperty.Pan, out m_value_Pan, out s);
                ctl.Get(CameraControlProperty.Tilt, out m_value_Tilt, out s);
                ctl.Get(CameraControlProperty.Exposure, out m_value_Exposure, out s);
            }
            catch { }
        }

        private void saveSettingConfig()
        {
            try
            {
                string iniFilePath = Application.StartupPath + "\\" + GlobalVar.gl_ProductModel + "\\" + GlobalVar.gl_LinkType + "\\" + GlobalVar.gl_iniTBS_FileName;
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_iniSection_cam, GlobalVar.gl_inikey_Focus, m_value_Focus.ToString(), iniFilePath);
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_iniSection_cam, GlobalVar.gl_inikey_Pan, m_value_Pan.ToString(), iniFilePath);
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_iniSection_cam, GlobalVar.gl_inikey_Tilt, m_value_Tilt.ToString(), iniFilePath);
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_iniSection_cam, GlobalVar.gl_inikey_Zoom, m_value_Zoom.ToString(), iniFilePath);
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_iniSection_cam, GlobalVar.gl_inikey_Exposure, m_value_Exposure.ToString(), iniFilePath);
            }
            catch { }
        }

        private void ReadSettingConfig()
        {
            StringBuilder str_tmp = new StringBuilder(50);
            string iniFilePath = Application.StartupPath + "\\" + GlobalVar.gl_ProductModel + "\\" + GlobalVar.gl_LinkType + "\\" + GlobalVar.gl_iniTBS_FileName;
            try
            {
                MyFunctions.GetPrivateProfileString(GlobalVar.gl_iniSection_cam, GlobalVar.gl_inikey_Focus, "", str_tmp, 50, iniFilePath);
                m_value_Focus = (str_tmp.ToString().Trim() == "" ? 40 : Convert.ToInt32(str_tmp.ToString()));
                MyFunctions.GetPrivateProfileString(GlobalVar.gl_iniSection_cam, GlobalVar.gl_inikey_Pan, "", str_tmp, 50, iniFilePath);
                m_value_Pan = (str_tmp.ToString().Trim() == "" ? 0 : Convert.ToInt32(str_tmp.ToString()));
                MyFunctions.GetPrivateProfileString(GlobalVar.gl_iniSection_cam, GlobalVar.gl_inikey_Tilt, "", str_tmp, 50, iniFilePath);
                m_value_Tilt = (str_tmp.ToString().Trim() == "" ? 0 : Convert.ToInt32(str_tmp.ToString()));
                MyFunctions.GetPrivateProfileString(GlobalVar.gl_iniSection_cam, GlobalVar.gl_inikey_Zoom, "", str_tmp, 50, iniFilePath);
                m_value_Zoom = (str_tmp.ToString().Trim() == "" ? 0 : Convert.ToInt32(str_tmp.ToString()));
                MyFunctions.GetPrivateProfileString(GlobalVar.gl_iniSection_cam, GlobalVar.gl_inikey_Exposure, "", str_tmp, 50, iniFilePath);
                m_value_Exposure = (str_tmp.ToString().Trim() == "" ? 0 : Convert.ToInt32(str_tmp.ToString()));
            }
            catch { }
        }

        //重新初始化camCapture
        private void resetCaputre()
        {
            try
            {
                if (cam_capture != null)
                {
                    cam_capture.Dispose();
                    //回收垃圾
                    GC.Collect();
                }
            }
            catch { }
            Thread.Sleep(100);
            cam_capture = new Capture(m_dev_index, 30, 640, 480);
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("請將製品(載板)放置于軌道上，等待圖像穩定后點擊[校準完畢]!")
                == DialogResult.OK)
            {
                setCamAutoAdjust();
            }
            button_start.Enabled = false;
            button_finish.Enabled = true;
        }

        private void button_finish_Click(object sender, EventArgs e)
        {
            ReadCamParaConfig();
            saveSettingConfig();
            ManualSetCameraConfig();
            button_start.Enabled = true;
            button_finish.Enabled = false;
        }
    }
}
