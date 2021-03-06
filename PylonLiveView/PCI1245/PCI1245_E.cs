﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Advantech.Motion;
using PylonLiveView;
using System.Runtime.InteropServices;
using System.Threading;
using Advantech.MotionComponent;

namespace MainSpace.PCI1245
{
    public delegate void dele_MotionMsg(string msg);
    public partial class PCI1245_E : UserControl
    {
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);
        public event dele_MotionMsg eve_MotionMsg;
        Thread m_monitorthread = null; //后台监控线程
        public event EventHandler
            eve_BoardArrived,
            eve_SheetBarcodeScan,
            eve_EmergeceStop,
            eve_EmergenceRelease,
            eve_SofetyDoor;
        public uint m_GPValue_VelHigh_move = 10000;  //群组运动最快速度 --用于正常作业
        public uint m_GPValue_VelLow_move = 10000;  //群组运动最慢速度 --用于正常作业
        public uint m_GPValue_Acc_move = 10000;  //群组运动最快加速度 --用于正常作业
        public uint m_GPValue_Dec_move = 10000;  //群组运动最快减速度 --用于正常作业
        public uint m_GPValue_VelHigh_low { get { return m_GPValue_VelHigh_move / 2; } }  //群组运动最快速度  --用于原点返回或者其他测试模式
        public uint m_GPValue_VelLow_low { get { return m_GPValue_VelLow_move / 2; } }  //群组运动最慢速度   --用于原点返回或者其他测试模式
        public uint m_GPValue_Acc_low { get { return m_GPValue_Acc_move / 2; } }  //群组运动最快加速度 --用于原点返回或者其他测试模式
        public uint m_GPValue_Dec_low { get { return m_GPValue_Dec_move / 2; } }  //群组运动最快减速度 --用于原点返回或者其他测试模式


        private bool m_tag_SheetScan = false;     //载板到达扫描位置标识，到位后设置一次，并通知开始测试，如果已经为true则不再通知。  BitIn_Z==0 时复位
        private bool m_tag_boardArrived = false;  //载板到达测试位置标识，到位后设置一次，并通知开始测试，如果已经为true则不再通知。  ZIN0==0 时复位
        public uint m_AxisNum_X = 0;
        public uint m_AxisNum_Y = 1;
        public uint m_AxisNum_Z = 2;
        public uint m_AxisNum_U = 3;
        public string m_AxisName_X = "Axis-X";
        public string m_AxisName_Y = "Axis-Y";
        DEV_LIST[] CurAvailableDevs = new DEV_LIST[Motion.MAX_DEVICES];
        uint deviceCount = 0;
        uint DeviceNum = 0;
        public IntPtr m_DeviceHandle = IntPtr.Zero;
        IntPtr[] m_Axishand = new IntPtr[32];   
        IntPtr m_GpHand = IntPtr.Zero;  //  
        uint AxCountInGp = 0;    //群组中轴的数量
        uint m_ulAxisCount = 0;  //板卡轴数量
        bool m_bInit = false;  //设备是否被初始化
        bool m_bServoOn_X = false;   //X轴ServoON标志
        bool m_bServoOn_Y = false;   //Y轴ServoON标志
        public ushort m_GpAxisStatus = 0;   //当前轴群组状态
        public double m_CmdPosition_X = 0;   //X轴逻辑计数器位置
        public double m_CmdPosition_Y = 0;   //Y轴逻辑计数器位置
        public double m_CmdPosition_Z = 0;   //Z轴逻辑计数器位置 2017.12.18

        //群组中轴的距离阵列，阵列元素的每个值都表示轴的相对位置。
        double[] EndArray = new double[32];
        //每个轴的终点值,与EndArray组合
        uint EndPointNum = 0;

        public PCI1245_E()
        {
            InitializeComponent();
            try
            {
                int Result = Motion.mAcm_GetAvailableDevs(CurAvailableDevs, Motion.MAX_DEVICES, ref deviceCount);
                if (Result != (int)ErrorCode.SUCCESS)
                {
                    MessageBox.Show("Can Not Get Available Device", "Line", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch { return; }
            if (deviceCount > 0)
            {
                DeviceNum = CurAvailableDevs[0].DeviceNum;
            }
            comboBox_MoveDirect_X.SelectedIndex = 0;
            comboBox_MoveDirect_Y.SelectedIndex = 0; 
            ReadMotionSpeed();
            timer1.Start();
        }

        #region 轴群组运动方法函数
        //PCI1245初始化
        public bool OpenBoardAndInit()
        {
            try
            {
                uint Result = Motion.mAcm_DevOpen(DeviceNum, ref m_DeviceHandle);
                if (Result != (uint)ErrorCode.SUCCESS)
                {
                    MessageBox.Show("运动控制卡连接失败，请检查设备连接！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                uint buffLen = 4;
                uint AxesPerDev = 0;
                //通过分配的 PropertyID 获取属性 （特性属性、配置属性或参数属性）值------获取能控制轴的数量
                Result = Motion.mAcm_GetProperty(m_DeviceHandle, (uint)PropertyID.FT_DevAxesCount, ref AxesPerDev, ref buffLen);
                if (Result != (uint)ErrorCode.SUCCESS)
                {
                    MessageBox.Show("运动控制卡信息读取失败，请检查设备连接！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                m_ulAxisCount = AxesPerDev;
                for (int i = 0; i < m_ulAxisCount; i++)
                {
                    //打开指定轴，获取该轴的对象句柄
                    //And Initial property for each Axis 		
                    Result = Motion.mAcm_AxOpen(m_DeviceHandle, (UInt16)i, ref m_Axishand[i]);
                    if (Result != (uint)ErrorCode.SUCCESS)
                    {
                        MessageBox.Show("Open Axis Failed", "Line", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    double cmdPosition = new double();
                    cmdPosition = 0.0;
                    //设置指定轴的理论位置    
                    //(m_Axishand[i]:    Acm_AxOpen 的轴句柄--cmdPosition:  新的理论位置。（单位：PPU）)
                    Result = Motion.mAcm_AxSetCmdPosition(m_Axishand[i], cmdPosition);
                }
                for (int j = 0; j < 32; j++)
                {
                    //群组中轴的相对位置全部清零。
                    EndArray[j] = 0;
                }
                //加载配置文档，否则设备不能初始化运动
                Result = Motion.mAcm_DevLoadConfig(m_DeviceHandle, Application.StartupPath + "\\PCI1245E.cfg");
                if (Result != (uint)ErrorCode.SUCCESS)
                {
                    MessageBox.Show("运动控制卡加载配置文档出错，请检查！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                m_bInit = true;
                m_monitorthread = new Thread(new ThreadStart(Backthread_BoardMonitorContinous));
                m_monitorthread.IsBackground = true;
                m_monitorthread.Start();
                Button_OpenBoard.Enabled = false;
                Button_CloseBoard.Enabled = true;
                //获取Z轴信息 2017.12.19
                uint result = GetSpeecZ(); //初始化
            }
            catch (Exception ex)
            {
                logWR.appendNewLogMessage("打开/初始化PCI1245E出错:\r\n" + ex.ToString());
                return false;
            }
            return true;
        }

        //关闭PCI1245E
        public bool CloseDevice()
        {
            try
            {
                UInt16[] usAxisState = new UInt16[32];
                uint AxisNum;
                //Stop Every Axes
                if (m_bInit == true)
                {
                    for (AxisNum = 0; AxisNum < m_ulAxisCount; AxisNum++)
                    {
                        //获取轴的当前状态
                        Motion.mAcm_AxGetState(m_Axishand[AxisNum], ref usAxisState[AxisNum]);
                        if (usAxisState[AxisNum] == (uint)AxisState.STA_AX_ERROR_STOP)
                        {
                            Motion.mAcm_AxResetError(m_Axishand[AxisNum]);
                        }
                        //命令轴减速停止
                        Motion.mAcm_AxStopDec(m_Axishand[AxisNum]);
                    }
                    //移除群组中的所有轴并关闭群组句柄
                    Motion.mAcm_GpClose(ref m_GpHand);
                    m_GpHand = IntPtr.Zero;
                    //Close Axes
                    for (AxisNum = 0; AxisNum < m_ulAxisCount; AxisNum++)
                    {
                        uint Result = Motion.mAcm_AxSetSvOn(m_Axishand[AxisNum], 0);
                        Result = Motion.mAcm_AxClose(ref m_Axishand[AxisNum]);
                    }
                    m_ulAxisCount = 0;
                    AxCountInGp = 0;
                    //Close Device
                    Motion.mAcm_DevClose(ref m_DeviceHandle);
                    m_DeviceHandle = IntPtr.Zero;
                    //timer1.Enabled = false;
                    m_bInit = false;
                    textBoxGpState.Clear();
                    listBoxEndPoint.Items.Clear();
                    listBoxAxesInGp.Items.Clear();
                    textBoxMasterID.Text = "";
                    textBox_LoginCount_X.Text = textBox_LoginCount_Y.Text = "";
                    label_status_AxisX.Text = label_status_AxisY.Text = "";
                    EndPointNum = 0;

                    Button_OpenBoard.Enabled = true;
                    Button_CloseBoard.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                logWR.appendNewLogMessage("关闭PCI1245E出错:\r\n" + ex.ToString());
                return false;
            }
            return true;
        }

        /// <param name="Num"> 0-3</param>
        /// <param name="OnOff">1:ON   0:OFF</param>
        public bool ServerOn(uint Num, uint OnOff)
        {
            try
            {
                for (int AxisNum = 0; AxisNum < m_ulAxisCount; AxisNum++)
                {
                    if (AxisNum == Num)
                    {
                        uint Result = Motion.mAcm_AxSetSvOn(m_Axishand[AxisNum], OnOff);
                        if (Result != (uint)ErrorCode.SUCCESS)
                        {
                            MessageBox.Show("伺服打开/关闭出错[0x" + Convert.ToString(Result, 16) + "]", "Line", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                logWR.appendNewLogMessage("伺服打开/关闭出错:\r\n" + ex.ToString());
            }
            return false;
        }
        
            
        //软复位异常
        public void ResetAlarmError()
        {
            UInt16 State = new UInt16();
            if (m_bInit == true)
            {
                Motion.mAcm_AxResetError(m_Axishand[0]);
                Motion.mAcm_AxResetError(m_Axishand[1]);
                Motion.mAcm_GpGetState(m_GpHand, ref State);
                if (State == (UInt16)GroupState.STA_Gp_ErrorStop)
                { 
                    //当群组处于发生错误停止时，复位错误
                    Motion.mAcm_GpResetError(m_GpHand);
                }
            }
        }

        //添加轴至群组
        public void AddAxisIntoGroup(uint AxisNum)
        {
            uint AxesInfoInGp = new uint();
            if (m_bInit != true){   return;   }
            uint Result = Motion.mAcm_GpAddAxis(ref m_GpHand, m_Axishand[AxisNum]);
            if (Result != (uint)ErrorCode.SUCCESS)
            {
                MessageBox.Show("添加轴至群组失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                //添加成功
                AxCountInGp++;
                listBoxAxesInGp.Items.Add(AxisNum == 0 ? m_AxisName_X : m_AxisName_Y);
                uint buffLen = 4;
                Result = Motion.mAcm_GetProperty(m_GpHand, (uint)PropertyID.CFG_GpAxesInGroup, ref AxesInfoInGp, ref buffLen);
                if (Result == (uint)ErrorCode.SUCCESS)
                {
                    for (int i = 0; i < 32; i++)
                    {
                        if ((AxesInfoInGp & (0x1 << i)) > 0)
                        {
                            textBoxMasterID.Text = String.Format("{0:d}", i);
                            break;
                        }
                    }
                }
            }
        }

        public string GetGpStatusStr(uint GpState)
        {
            string strTemp = "";
            switch (GpState)
            {
                case 0:
                    strTemp = "STA_GP_DISABLE";
                    break;
                case 1:
                    strTemp = "STA_GP_READY";
                    break;
                case 2:
                    strTemp = "STA_GP_STOPPING";
                    break;
                case 3:
                    strTemp = "STA_GP_ERROR_STOP";
                    break;
                case 4:
                    strTemp = "STA_GP_MOTION";
                    break;
                case 5:
                    strTemp = "STA_GP_AX_MOTION";
                    break;
                case 6:
                    strTemp = "STA_GP_MOTION_PATH";
                    break;
                default:
                    break;
            }
            return strTemp;
        }

        //获取轴的当前状态
        public string GetAxisStatusStr(uint status)
        {
            switch (status)
            {
                case 0:
                    return "轴被禁用，请打开并激活";
                case 1:
                    return "轴已准备就绪，等待新的命令";
                case 2:
                    return "轴停止";
                case 3:
                    return "出现错误，轴停止";
                case 4:
                    return "轴正在执行返回原点运动";
                case 5:
                    return "轴正在执行 PTP 运动";
                case 6:
                    return "轴正在执行连续运动";
                case 7:
                    return "轴在群组中，正在执行插补运动";
                case 8:
                    return "轴由外部信号控制。当外部信号激活时，轴将执行 JOG 模式运动";
                case 9:
                    return "轴由外部信号控制。当外部信号激活时，轴将执行 MPG 模式运动";
            }
            return "";
        }

        //群组运动
        public void AxisGroup_Move(bool isAutoMotion)
        {
            try
            {
                uint AxisNum = new uint();
                UInt16 State = new UInt16();
                if (m_bInit != true) { return; }
                uint Result = Motion.mAcm_GpGetState(m_GpHand, ref State);
                if (Result != (uint)ErrorCode.SUCCESS)
                {
                    if (isAutoMotion) { eve_MotionMsg("轴群组状态异常，运动禁止"); }
                    else
                    {
                        MessageBox.Show("轴群组状态异常，运动禁止！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                } 
                if (State != (UInt16)GroupState.STA_Gp_Ready)
                {
                    if (isAutoMotion) { eve_MotionMsg("轴群组为非Ready状态，运动禁止"); }
                    else
                    {
                        MessageBox.Show("设备(轴)为Not Ready状态，请停止后检查再作業！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return;
                }
                AxisNum = (uint)listBoxAxesInGp.Items.Count;
                if (radioButtonRel.Checked)
                {
                    Result = Motion.mAcm_GpMoveLinearRel(m_GpHand, EndArray, ref AxisNum);
                }
                else
                    Result = Motion.mAcm_GpMoveLinearAbs(m_GpHand, EndArray, ref AxisNum);

                if (Result != (uint)ErrorCode.SUCCESS)
                {
                    if (isAutoMotion) { eve_MotionMsg("轴群组运动异常：[0x" + Convert.ToString(Result, 16) + ""); }
                    else
                    {
                        MessageBox.Show("轴群组运动异常，错误代码：[0x" + Convert.ToString(Result, 16) + "]", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } return;
                }
            }
            catch { }
        }
        #endregion

        #region 回原点
        private bool Home(int AxisNum)
        {
            try
            {
                UInt32 PropertyVal = new UInt32();
                double CrossDistance = new double();
                if (!m_bInit) { return false; }
                double Vel = 3000.0; //m_GPValue_VelLow_low; //1000.0;
                UInt32 Result = Motion.mAcm_SetProperty(m_Axishand[AxisNum], (uint)PropertyID.PAR_AxVelLow, ref Vel, (uint)Marshal.SizeOf(typeof(double)));
                if (Result != (uint)ErrorCode.SUCCESS)
                {
                    //MessageBox.Show("Set Property-PAR_AxVelLow Failed With Error Code[0x" + Convert.ToString(Result, 16) + "]", "Home", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                Vel = 3000; //m_GPValue_VelHigh_low; //3000.0;
                Result = Motion.mAcm_SetProperty(m_Axishand[AxisNum], (uint)PropertyID.PAR_AxVelHigh, ref Vel, (uint)Marshal.SizeOf(typeof(double)));
                if (Result != (uint)ErrorCode.SUCCESS)
                {
                    //MessageBox.Show("Set Property-PAR_AxVelHigh Failed With Error Code[0x" + Convert.ToString(Result, 16) + "]", "Home", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                PropertyVal = 2; //Edge on
                Result = Motion.mAcm_SetProperty(m_Axishand[AxisNum], (uint)PropertyID.PAR_AxHomeExSwitchMode, ref PropertyVal, (uint)Marshal.SizeOf(typeof(UInt32)));
                if (Result != (uint)ErrorCode.SUCCESS)
                {
                    //MessageBox.Show("Set Property-PAR_AxHomeExSwitchMode Failed With Error Code[0x" + Convert.ToString(Result, 16) + "]", "Home", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                CrossDistance = 500; //找到信号后，返回距离，初始值100
                Result = Motion.mAcm_SetProperty(m_Axishand[AxisNum], (uint)PropertyID.PAR_AxHomeCrossDistance, ref CrossDistance, (uint)Marshal.SizeOf(typeof(double)));
                if (Result != (uint)ErrorCode.SUCCESS)
                {
                    //MessageBox.Show("Set Property-AxHomeCrossDistance Failed With Error Code[0x" + Convert.ToString(Result, 16) + "]", "Home", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //Result = Motion.mAcm_AxHome(m_Axishand[AxisNum], (UInt32)comboBoxMode.SelectedIndex, (UInt32)comboBoxDir.SelectedIndex);
                Result = Motion.mAcm_AxHome(m_Axishand[AxisNum], 11, 1);  //MODE12_AbsSearchReFind  |  Nagative Direction
                if (Result != (uint)ErrorCode.SUCCESS)
                {
                    //MessageBox.Show("AxHome Failed With Error Code[0x" + Convert.ToString(Result, 16) + "]", "Home", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 按钮设置/控制事件
        private void Button_OpenBoard_Click(object sender, EventArgs e)
        {
            OpenBoardAndInit();
        }

        private void BtnCloseBoard_Click(object sender, EventArgs e)
        {
            CloseDevice();
        }

        private void button_ServerON_X_Click(object sender, EventArgs e)
        {
            ServerON_Axis_X();
        }

        public void ServerON_Axis_X()
        {
            if (m_bInit != true) { return; }
            if (m_bServoOn_X == false)
            {
                ServerOn(m_AxisNum_X, 1);
                m_bServoOn_X = true;
                button_ServerON_X.Text = "伺服 OFF";
                button_ServerON_X.BackColor = Color.Red;
            }
            else
            {
                ServerOn(m_AxisNum_X, 0);
                m_bServoOn_X = true;
                button_ServerON_X.Text = "伺服 ON";
                button_ServerON_X.BackColor = Color.Gray;
            }
        }

        private void button_ServerOn_Y_Click(object sender, EventArgs e)
        {
            ServerON_Axis_Y();
        }

        public void ServerON_Axis_Y()
        {
            if (m_bInit != true) { return; }
            if (m_bServoOn_Y == false)
            {
                ServerOn(m_AxisNum_Y, 1);
                m_bServoOn_Y = true;
                button_ServerOn_Y.Text = "伺服 OFF";
                button_ServerOn_Y.BackColor = Color.Red;
            }
            else
            {
                ServerOn(m_AxisNum_Y, 0);
                m_bServoOn_Y = true;
                button_ServerOn_Y.Text = "伺服 ON";
                button_ServerOn_Y.BackColor = Color.Gray;
            }
        }


        //复位异常按钮
        private void BtnResetErr_Click(object sender, EventArgs e)
        {
            ResetAlarmError();
        }

        //添加X轴至群组
        private void button_AddToGp_X_Click(object sender, EventArgs e)
        {
            AddAxisIntoGroup(m_AxisNum_X);
        }

        //添加Y轴至群组
        private void button_AddToGp_Y_Click(object sender, EventArgs e)
        {
            AddAxisIntoGroup(m_AxisNum_Y);
        }

        //X轴设置运动终点
        private void Button_SetEnd_X_Click(object sender, EventArgs e)
        {
            try
            {
                double value = Convert.ToDouble(numericUpDown_MovDis_X.Value)
                    * (comboBox_MoveDirect_X.SelectedIndex == 0 ? 1 : -1);
                SetPoxEnd_X(value / GlobalVar.gl_PixelDistance, false);
            }
            catch { }
        }

        //Y轴设置运动终点
        private void Button_SetEnd_Y_Click(object sender, EventArgs e)
        {
            try
            {
                double value = Convert.ToDouble(numericUpDown_MovDis_Y.Value)
                    * (comboBox_MoveDirect_Y.SelectedIndex == 0 ? 1 : -1);
                SetPoxEnd_Y(value / GlobalVar.gl_PixelDistance, false);
            }
            catch { }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            UInt16 State = new UInt16();
            if (m_bInit != true)
            {
                return;
            }
            Motion.mAcm_GpGetState(m_GpHand, ref State);
            if (State == (UInt16)GroupState.STA_Gp_ErrorStop)
            { Motion.mAcm_GpResetError(m_GpHand); }
            Motion.mAcm_GpStopEmg(m_GpHand);
        }

        private void Button_Move_Click(object sender, EventArgs e)
        {
            SetProp_GPSpeed(m_GPValue_VelHigh_move, m_GPValue_VelLow_move, m_GPValue_Acc_move, m_GPValue_Dec_move);
            AxisGroup_Move(false);
        }

        private void button_Home_X_Click(object sender, EventArgs e)
        {
            Home(0);
        }

        private void button_Home_Y_Click(object sender, EventArgs e)
        {
            Home(1);
        }

        //逻辑计数器复位_X
        private void Button_CountReset_X_Click(object sender, EventArgs e)
        {
            double cmdPosition = 0;
            if (m_bInit == true)
            {
                Motion.mAcm_AxSetCmdPosition(m_Axishand[m_AxisNum_X], cmdPosition);
            }	
        }

        //逻辑计数器复位_Y
        private void Button_CountReset_Y_Click(object sender, EventArgs e)
        {
            double cmdPosition = 0;
            if (m_bInit == true)
            {
                Motion.mAcm_AxSetCmdPosition(m_Axishand[m_AxisNum_Y], cmdPosition);
            }
        }

        private void Button_ClearEnd_Click(object sender, EventArgs e)
        {
            if (m_bInit != true){ return; }
            for (uint i = 0; i < 32; i++)
            {
                EndArray[i] = 0;
            }
            listBoxEndPoint.Items.Clear();
            EndPointNum = 0;
        }
        
        private void button_LedRed_Click(object sender, EventArgs e)
        {
            byte value =(button_LedRed.Text.ToUpper() == "ON") ? (byte)1 : (byte)0;
            uint result = SetDO(m_AxisNum_Z, 6, value);
        }

        private void button_LedWhite_Click(object sender, EventArgs e)
        {
            byte value = (button_LedWhite.Text.ToUpper() == "ON") ? (byte)1 : (byte)0;
            uint result = SetDO(m_AxisNum_U, 6, value);
        }

        #endregion

        #region DIO
        public void ALLIOInit()
        {
            try
            {
                SetDO(m_AxisNum_Y, 4, 1);
                SetDO(m_AxisNum_Y, 5, 1);
            }
            catch { }
        }

        public uint SetDO(uint AxisNum, ushort DOChannel, byte DoValue)
        {
            return Motion.mAcm_AxDoSetBit(m_Axishand[AxisNum], DOChannel, DoValue);
        }

        //拍照
        private void button_CCDTrigger_Click(object sender, EventArgs e)
        {
            //OneShot();
            CaptureTrigger();
        }

        //载板第一段到位扫描完毕
        public void Stage1ZaibanPass()
        {
            //m_inParaSetting = true;
            try
            {
                SetDO(m_AxisNum_Y, 0 + 4, 0);
                Thread.Sleep(500);
                SetDO(m_AxisNum_Y, 0 + 4, 1);
            }
            catch { }
        }

        public void Stage2ZaibanPass()
        {
            try
            {
                Thread.Sleep(200);
                SetDO(m_AxisNum_Y, 1 + 4, 0);
                Thread.Sleep(500);
                SetDO(m_AxisNum_Y, 1 + 4, 1);
            }
            catch { }
        }

        //value 0:OFF 1:ON
        public void LED_Red_OnOff(byte value)
        {
            SetDO(m_AxisNum_Z, 2, value);
        }

        //value 0:OFF 1:ON
        public void LED_Blue_OnOff(byte value)
        {
            SetDO(m_AxisNum_U, 2, value);
        }

        //0: 灭  1：亮
        public void LedLight_Red(byte light)
        {
            SetDO(m_AxisNum_Z, 4, light);
        }

        //0: 灭  1：亮
        public void LedLight_Yellow(byte light)
        {
            SetDO(m_AxisNum_Z, 5, light);
        }

        //0: 灭  1：亮
        public void LedLight_Green(byte light)
        {
            SetDO(m_AxisNum_U, 4, light);
        }

        //0: 灭  1：亮
        public void LedLight_Beep(byte beep)
        {
            SetDO(m_AxisNum_U, 5, beep);
        }

        //三色灯总开关  0: 有效  1：无效
        public void LedLight_Switch(byte valid)
        {
        }

        public void CaptureTrigger()
        {
            Thread.Sleep(60); //等待到位后稳定
            SetDO(m_AxisNum_X, 4, 1);
            Thread.Sleep(2);
            SetDO(m_AxisNum_X, 4, 0);
        }

        /// <summary>
        /// 0: ALL OFF  1：WHITE ON,RED OFF  2：WHITE OFF ,RED ON  3：ALL ON
        /// </summary>
        /// <param name="value"></param>
        public void CCDLight(int value)
        {
            switch (value)
            { 
                case 0:
                default:
                    SetDO(m_AxisNum_U, 6, 0);
                    SetDO(m_AxisNum_Z, 6, 0);
                    break;
                case 1:
                    SetDO(m_AxisNum_U, 6, 1);
                    SetDO(m_AxisNum_Z, 6, 0);
                    break;
                case 2:
                    SetDO(m_AxisNum_U, 6, 0);
                    SetDO(m_AxisNum_Z, 6, 1);
                    break;
                case 3:
                    SetDO(m_AxisNum_U, 6, 1);
                    SetDO(m_AxisNum_Z, 6, 1);
                    break;
            }
        }

        #endregion

        #region  轴参数设置
        //相对运动点距离设定  automotion:自动状态下，不更新设置界面值
        public void SetPoxEnd_X(double value, bool automotion)
        {
            try
            {
                //if (InvokeRequired)
                //{
                //    Invoke(new Action(() => { SetPoxEnd_X(value, automotion); }));
                //    return;
                //}
                if (m_bInit != true) { return; }
                EndArray[m_AxisNum_X] = value * GlobalVar.gl_PixelDistance;
                if (!automotion)
                {
                    this.Invoke(new Action(() =>
                    {
                        if (listBoxEndPoint.Items.Count > m_AxisNum_X)
                        {
                            listBoxEndPoint.Items[(int)m_AxisNum_X] = value * GlobalVar.gl_PixelDistance;
                        }
                        else
                        {
                            listBoxEndPoint.Items.Add(value * GlobalVar.gl_PixelDistance);
                        }
                    }));
                }
            }
            catch { }
        }

        //相对运动点距离设定 automotion:自动状态下，不更新设置界面值
        public void SetPoxEnd_Y(double value, bool automotion)
        {
            try
            {
                //if (InvokeRequired)
                //{
                //    Invoke(new Action(() => { SetPoxEnd_Y(value, automotion); }));
                //    return;
                //}
                if (m_bInit != true) { return; }
                EndArray[m_AxisNum_Y] = value * GlobalVar.gl_PixelDistance;
                if (!automotion)
                {
                    this.Invoke(new Action(() =>
                    {
                        if (listBoxEndPoint.Items.Count > m_AxisNum_Y)
                        {
                            listBoxEndPoint.Items[(int)m_AxisNum_Y] = value * GlobalVar.gl_PixelDistance;
                        }
                        else
                        {
                            listBoxEndPoint.Items.Add(value * GlobalVar.gl_PixelDistance);
                        }
                    }));
                }
            }
            catch { }
        }

        #region 轴速度设置
        //单轴最高速度设置-- type{true:群组，false:单轴}
        public uint SetProp_VelHigh(uint AxisNum, double VelHigh, bool isGpSetting)
        {
            uint result = Motion.mAcm_SetProperty(isGpSetting ? m_GpHand : m_Axishand[AxisNum],
                (uint)PropertyID.PAR_GpVelHigh,
                ref VelHigh, (uint)Marshal.SizeOf(typeof(double)));
            return result;
        }

        //单轴最低速度设置-- type{true:群组，false:单轴, bool type}
        public uint SetProp_VelLow(uint AxisNum, double VelLow, bool isGpSetting)
        {
            uint result = Motion.mAcm_SetProperty(isGpSetting ? m_GpHand : m_Axishand[AxisNum],
                isGpSetting ? (uint)PropertyID.PAR_GpVelLow : (uint)PropertyID.PAR_AxVelLow,
                ref VelLow, (uint)Marshal.SizeOf(typeof(double)));
            return result;
        }

        //单轴减速度设置-- type{true:群组，false:单轴, bool type}
        public uint SetProp_Dec(uint AxisNum, double Dec, bool isGpSetting)
        {
            uint result = Motion.mAcm_SetProperty(isGpSetting ? m_GpHand : m_Axishand[AxisNum],
                isGpSetting ? (uint)PropertyID.PAR_GpDec : (uint)PropertyID.PAR_AxDec,
                ref Dec, (uint)Marshal.SizeOf(typeof(double)));
            return result;
        }

        //单轴加速度设置-- type{true:群组，false:单轴, bool type}
        public uint SetProp_Acc(uint AxisNum, double Acc, bool isGpSetting)
        {
            uint result = Motion.mAcm_SetProperty(isGpSetting ? m_GpHand : m_Axishand[AxisNum],
                isGpSetting ? (uint)PropertyID.PAR_GpAcc : (uint)PropertyID.PAR_AxAcc,
                ref Acc, (uint)Marshal.SizeOf(typeof(double)));
            return result;
        }

        //速度曲线类型设置-- type{true:群组，false:单轴, bool type}, Jerk(0: T 形曲线（默认）)
        public uint SetProp_Jerk(uint AxisNum, double Jerk, bool isGpSetting)
        {
            uint result = Motion.mAcm_SetProperty(isGpSetting ? m_GpHand : m_Axishand[AxisNum],
                isGpSetting ? (uint)PropertyID.PAR_GpJerk : (uint)PropertyID.PAR_AxJerk,
                ref Jerk, (uint)Marshal.SizeOf(typeof(double)));
            return result;
        }
        #endregion

        #region 轴速度获取 //2017.12.19
        //单轴最高速度获取-- type{true:群组，false:单轴}
        public uint GetProp_VelHigh(uint AxisNum, ref double VelHigh)
        {
            uint buffLen = 4;
            uint result = Motion.mAcm_GetProperty(m_Axishand[AxisNum],
                (uint)PropertyID.PAR_AxVelHigh,
                ref VelHigh, ref buffLen);
            return result;
        }
        //单轴最低速度获取-- type{true:群组，false:单轴}
        public uint GetProp_VelLow(uint AxisNum, ref double VelLow)
        {
            uint buffLen = 4;
            uint result = Motion.mAcm_GetProperty(m_Axishand[AxisNum],
                (uint)PropertyID.PAR_AxVelLow,
                ref VelLow, ref buffLen);
            return result;
        }
        //单轴加速度获取-- type{true:群组，false:单轴}
        public uint GetProp_Acc(uint AxisNum, ref double VelAcc)
        {
            uint buffLen = 4;
            uint result = Motion.mAcm_GetProperty(m_Axishand[AxisNum],
                (uint)PropertyID.PAR_AxAcc,
                ref VelAcc, ref buffLen);
            return result;
        }
        //单轴减速度获取-- type{true:群组，false:单轴}
        public uint GetProp_Dec(uint AxisNum, ref double VelDec)
        {
            uint buffLen = 4;
            uint result = Motion.mAcm_GetProperty(m_Axishand[AxisNum],
                (uint)PropertyID.PAR_AxDec,
                ref VelDec, ref buffLen);
            return result;
        }
        //单轴速度曲线获取-- type{true:群组，false:单轴}
        public uint GetProp_Jerk(uint AxisNum, ref double Jerk)
        {
            uint buffLen = 4;
            uint result = Motion.mAcm_GetProperty(m_Axishand[AxisNum],
                (uint)PropertyID.PAR_AxJerk,
                ref Jerk, ref buffLen);
            return result;
        }
        #endregion

        //更新当前X轴速度参数
        private void button_UpdateSpdValue_X_Click(object sender, EventArgs e)
        {
        }

        private void button_SpeedSet_X_Click(object sender, EventArgs e)
        {
            uint result = SetProp_VelHigh(m_AxisNum_X, (double)numericUpDown_VelHigh_X.Value, false);
            result += SetProp_VelLow(m_AxisNum_X, (double)numericUpDown_VelLow_X.Value, false);
            result += SetProp_Jerk(m_AxisNum_X, (double)numericUpDown_Jerk_X.Value, false);
            result += SetProp_Dec(m_AxisNum_X, (double)numericUpDown_Dec_X.Value, false);
            if (result != 0)
            {
                MessageBox.Show("速度参数设置失败，请检查", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //更新当前Y轴速度参数
        private void button_UpdateSpdValue_Y_Click(object sender, EventArgs e)
        {
        }

        private void button_SpeedSet_Y_Click(object sender, EventArgs e)
        {
            uint result = SetProp_VelHigh(m_AxisNum_Y, (double)numericUpDown_VelHigh_Y.Value, false);
            result += SetProp_VelLow(m_AxisNum_Y, (double)numericUpDown_VelLow_Y.Value, false);
            result += SetProp_Jerk(m_AxisNum_Y, (double)numericUpDown_Jerk_Y.Value, false);
            result += SetProp_Dec(m_AxisNum_Y, (double)numericUpDown_Dec_Y.Value, false);
            if (result != 0)
            {
                MessageBox.Show("速度参数设置失败，请检查", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// 设置群组速度
        /// </summary>
        /// <param name="VelHigh">最高速度</param>
        /// <param name="VelLow">初速度</param>
        /// <param name="Acc">加速度</param>
        /// <param name="Dec">减速度</param>
        public void SetProp_GPSpeed(double VelHigh, double VelLow, double Acc, double Dec)
        {
            uint result = SetProp_VelHigh(0, VelHigh, true);
            result += SetProp_VelLow(0, VelLow, true);
            result += SetProp_Jerk(0, (double)0, true);  //写入默认值
            result += SetProp_Acc(0, Acc, true);
            result += SetProp_Dec(0, Dec, true);
            if (result != 0)
            {
                logWR.appendNewLogMessage("群组速度参数写入失败: \r\n");
            }
        }

        //设置群组速度
        private void button_SpeedSet_gp_Click(object sender, EventArgs e)
        {
            uint result = SetProp_VelHigh(0, (double)numericUpDown_VelHigh_gp.Value, true);
            result += SetProp_VelLow(0, (double)numericUpDown_VelLow_gp.Value, true);
            result += SetProp_Jerk(0, (double)0, true);  //写入默认值
            result += SetProp_Acc(0, (double)numericUpDown_Acc_gp.Value, true);            
            result += SetProp_Dec(0, (double)numericUpDown_Dec_gp.Value, true);

            if ((result == 0) && SaveMotionSpeed())
            {
                MessageBox.Show("速度参数设置成功。", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("速度参数设置失败，请检查", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        //更新显示群组速度
        private void button_UpdateGPSpeedValue_Click(object sender, EventArgs e)
        {
            try
            {
                double Vel = 0.0;
                uint bufLength = (uint)Marshal.SizeOf(typeof(double));
                uint result = Motion.mAcm_GetProperty(m_GpHand, (uint)PropertyID.PAR_GpVelHigh, ref Vel, ref bufLength);
                if (result == (uint)ErrorCode.SUCCESS) { numericUpDown_VelHigh_gp.Value = Convert.ToInt32(Vel); }
                result = Motion.mAcm_GetProperty(m_GpHand, (uint)PropertyID.PAR_GpVelLow, ref Vel, ref bufLength);
                if (result == (uint)ErrorCode.SUCCESS) { numericUpDown_VelLow_gp.Value = Convert.ToInt32(Vel); }
                result = Motion.mAcm_GetProperty(m_GpHand, (uint)PropertyID.PAR_GpDec, ref Vel, ref bufLength);
                if (result == (uint)ErrorCode.SUCCESS) { numericUpDown_Dec_gp.Value = Convert.ToInt32(Vel); }
                result = Motion.mAcm_GetProperty(m_GpHand, (uint)PropertyID.PAR_GpAcc, ref Vel, ref bufLength);
                if (result == (uint)ErrorCode.SUCCESS) { numericUpDown_Acc_gp.Value = Convert.ToInt32(Vel); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("群组速度参数读取失败：" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logWR.appendNewLogMessage("群组速度参数读取失败：\r\n" + ex.ToString());
            }
        }

        public uint SetGpSpeed(double VelHigh, double VelLow, double Jerk, double Dec)
        {
            uint result = 0;
            try
            {
                result = SetProp_VelHigh(0, VelHigh, true);
                result += SetProp_VelLow(0, VelLow, true);
                result += SetProp_Jerk(0, Jerk, true);
                result += SetProp_Dec(0, Dec, true);
            }
            catch(Exception ex)
            {
                logWR.appendNewLogMessage("速度参数设置失败:\r\n" + ex.ToString());
            }
            return result;
        }

        public void ReadMotionSpeed()
        {
            try
            {
                StringBuilder str_tmp = new StringBuilder(100);
                string iniFilePath = GlobalVar.gl_strTargetPath + "\\" + GlobalVar.gl_iniTBS_FileName;
                GetPrivateProfileString(GlobalVar.gl_iniSection_AdvMotionSpeed, GlobalVar.gl_iniKey_AdvMotionSpd_VelHigh, "", str_tmp, 50, iniFilePath);
                m_GPValue_VelHigh_move = (str_tmp.ToString().Trim() == "") ? m_GPValue_VelHigh_move : Convert.ToUInt32(str_tmp.ToString().Trim());
                GetPrivateProfileString(GlobalVar.gl_iniSection_AdvMotionSpeed, GlobalVar.gl_iniKey_AdvMotionSpd_VelLow, "", str_tmp, 50, iniFilePath);
                m_GPValue_VelLow_move = (str_tmp.ToString().Trim() == "") ? m_GPValue_VelLow_move : Convert.ToUInt32(str_tmp.ToString().Trim());
                GetPrivateProfileString(GlobalVar.gl_iniSection_AdvMotionSpeed, GlobalVar.gl_iniKey_AdvMotionSpd_Acc, "", str_tmp, 50, iniFilePath);
                m_GPValue_Acc_move = (str_tmp.ToString().Trim() == "") ? m_GPValue_Acc_move : Convert.ToUInt32(str_tmp.ToString().Trim());
                GetPrivateProfileString(GlobalVar.gl_iniSection_AdvMotionSpeed, GlobalVar.gl_iniKey_AdvMotionSpd_Dec, "", str_tmp, 50, iniFilePath);
                m_GPValue_Dec_move = (str_tmp.ToString().Trim() == "") ? m_GPValue_Dec_move : Convert.ToUInt32(str_tmp.ToString().Trim());
                numericUpDown_VelHigh_gp.Value = m_GPValue_VelHigh_move;
                numericUpDown_VelLow_gp.Value = m_GPValue_VelLow_move;
                numericUpDown_Acc_gp.Value = m_GPValue_Acc_move;
                numericUpDown_Dec_gp.Value = m_GPValue_Dec_move;
            }
            catch {  }
        }

        public bool SaveMotionSpeed()
        {
            try
            {
                m_GPValue_VelHigh_move = (uint)numericUpDown_VelHigh_gp.Value;  //群组运动最快速度 --用于正常作业
                m_GPValue_VelLow_move = (uint)numericUpDown_VelLow_gp.Value;  //群组运动最慢速度 --用于正常作业
                m_GPValue_Acc_move = (uint)numericUpDown_Acc_gp.Value;  //群组运动最快加速度 --用于正常作业
                m_GPValue_Dec_move = (uint)numericUpDown_Dec_gp.Value;  //群组运动最快减速度 --用于正常作业
                string iniFilePath = GlobalVar.gl_strTargetPath + "\\" + GlobalVar.gl_iniTBS_FileName;
                WritePrivateProfileString(GlobalVar.gl_iniSection_AdvMotionSpeed, GlobalVar.gl_iniKey_AdvMotionSpd_VelHigh, m_GPValue_VelHigh_move.ToString(), iniFilePath);
                WritePrivateProfileString(GlobalVar.gl_iniSection_AdvMotionSpeed, GlobalVar.gl_iniKey_AdvMotionSpd_VelLow, m_GPValue_VelLow_move.ToString(), iniFilePath);
                WritePrivateProfileString(GlobalVar.gl_iniSection_AdvMotionSpeed, GlobalVar.gl_iniKey_AdvMotionSpd_Acc, m_GPValue_Acc_move.ToString(), iniFilePath);
                WritePrivateProfileString(GlobalVar.gl_iniSection_AdvMotionSpeed, GlobalVar.gl_iniKey_AdvMotionSpd_Dec, m_GPValue_Dec_move.ToString(), iniFilePath);
            }
            catch { return false; }
            return true;
        }
        #endregion

        //需要显示，但是不重要的放在Timer中定时显示。
        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox_LoginCount_X.Text = Convert.ToString(m_CmdPosition_X);
            textBox_LoginCount_Y.Text = Convert.ToString(m_CmdPosition_Y);
            textBox_LoginCount_Z.Text = Convert.ToString(m_CmdPosition_Z); //2017.12.18
            label_status_AxisX.Text = GetAxisStatusStr(AxState_X);
            label_status_AxisY.Text = GetAxisStatusStr(AxState_Y);

            label_status_AxisZ.Text = GetAxisStatusStr(AxState_Z);
            UInt16 GpState = new UInt16();
            uint result = Motion.mAcm_GpGetState(m_GpHand, ref GpState);
            m_GpAxisStatus = GpState;
            textBoxGpState.Text = GetGpStatusStr(m_GpAxisStatus);
            #region X轴DO
            byte bitDo_X = 0;
            uint Result_X = Motion.mAcm_AxDoGetBit(m_Axishand[m_AxisNum_X], 4, ref bitDo_X);
            Picturebox_D04_X.BackColor = (bitDo_X == 1) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            Result_X = Motion.mAcm_AxDoGetBit(m_Axishand[m_AxisNum_X], 5, ref bitDo_X);
            Picturebox_D05_X.BackColor = (bitDo_X == 1) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            Result_X = Motion.mAcm_AxDoGetBit(m_Axishand[m_AxisNum_X], 6, ref bitDo_X);
            m_bServoOn_X = Convert.ToBoolean(bitDo_X);
            Picturebox_D06_X.BackColor = (bitDo_X == 1) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            Result_X = Motion.mAcm_AxDoGetBit(m_Axishand[m_AxisNum_X], 7, ref bitDo_X);
            Picturebox_D07_X.BackColor = (bitDo_X == 1) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            #endregion

            #region Y轴DO
            byte bitDo_Y = 0;
            uint Result_Y = Motion.mAcm_AxDoGetBit(m_Axishand[m_AxisNum_Y], 4, ref bitDo_Y);
            Picturebox_D04_Y.BackColor = (bitDo_Y == 1) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            Result_Y = Motion.mAcm_AxDoGetBit(m_Axishand[m_AxisNum_Y], 5, ref bitDo_Y);
            Picturebox_D05_Y.BackColor = (bitDo_Y == 1) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            Result_Y = Motion.mAcm_AxDoGetBit(m_Axishand[m_AxisNum_Y], 6, ref bitDo_Y);
            m_bServoOn_Y = Convert.ToBoolean(bitDo_Y);
            Picturebox_D06_Y.BackColor = (bitDo_Y == 1) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            Result_Y = Motion.mAcm_AxDoGetBit(m_Axishand[m_AxisNum_Y], 7, ref bitDo_Y);
            Picturebox_D07_Y.BackColor = (bitDo_Y == 1) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            #endregion

            #region Z/U轴DO
            byte byte_Z = 0;
            byte byte_U = 0;
            Motion.mAcm_AxDoGetBit(m_Axishand[m_AxisNum_Z], 6, ref byte_Z);
            pictureBox_ledRed.BackColor = (byte_Z == 0) ? Color.Gray : Color.Red;
            button_LedRed.Text = (byte_Z == 0) ? "ON" : "OFF";
            Motion.mAcm_AxDoGetBit(m_Axishand[m_AxisNum_U], 6, ref byte_U);
            pictureBox_ledWhite.BackColor = (byte_U == 0) ? Color.Gray : Color.Red;
            button_LedWhite.Text = (byte_U == 0) ? "ON" : "OFF";
            #endregion

            #region X轴DI
            byte BitIn_X = 0;
            UInt32 Result_XI = Motion.mAcm_AxDiGetBit(m_Axishand[m_AxisNum_X], 0, ref BitIn_X);
            pictureBox_DI0_X.BackColor = (BitIn_X == 1) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            Result_XI = Motion.mAcm_AxDiGetBit(m_Axishand[m_AxisNum_X], 1, ref BitIn_X);
            pictureBox_DI1_X.BackColor = (BitIn_X == 1) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            Result_XI = Motion.mAcm_AxDiGetBit(m_Axishand[m_AxisNum_X], 2, ref BitIn_X);
            pictureBox_DI2_X.BackColor = (BitIn_X == 1) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            Result_XI = Motion.mAcm_AxDiGetBit(m_Axishand[m_AxisNum_X], 3, ref BitIn_X);
            pictureBox_DI3_X.BackColor = (BitIn_X == 1) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            #endregion

            #region Y轴DI
            byte BitIn_Y = 0;
            UInt32 Result_YI = Motion.mAcm_AxDiGetBit(m_Axishand[m_AxisNum_Y], 0, ref BitIn_Y);
            pictureBox_DI0_Y.BackColor = (BitIn_Y == 1) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            Result_YI = Motion.mAcm_AxDiGetBit(m_Axishand[m_AxisNum_Y], 1, ref BitIn_Y);
            pictureBox_DI1_Y.BackColor = (BitIn_Y == 1) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            Result_YI = Motion.mAcm_AxDiGetBit(m_Axishand[m_AxisNum_Y], 2, ref BitIn_Y);
            pictureBox_DI2_Y.BackColor = (BitIn_Y == 1) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            Result_YI = Motion.mAcm_AxDiGetBit(m_Axishand[m_AxisNum_Y], 3, ref BitIn_Y);
            pictureBox_DI3_Y.BackColor = (BitIn_Y == 1) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
            #endregion

            #region U/Z轴DI
            byte BitIn_Z = 0;
            //Z轴IN0
            UInt32 Result_ZU = Motion.mAcm_AxDiGetBit(m_Axishand[m_AxisNum_Z], 0, ref BitIn_Z);
            if (Result_ZU == (uint)ErrorCode.SUCCESS)
            {
                if (BitIn_Z == 1)
                {
                    //通知到达条码扫描位置
                    pictureBox_ShtSNScan.BackColor = Color.Red;
                }
                else
                {
                    pictureBox_ShtSNScan.BackColor = Color.Gray;
                }
            }
            #endregion

            #region 读取LED I/O光源信息
            byte byte_Power = 0;
            uint Result_red = Motion.mAcm_AxDoGetBit(m_Axishand[m_AxisNum_Z], (ushort)2, ref byte_Power);
            if (Result_red == (uint)ErrorCode.SUCCESS)
            {
                if (byte_Power == 1)
                {
                    pictureBox_ledRed.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    pictureBox_ledRed.BackColor = System.Drawing.Color.Gray;
                }
            }
            uint Result_blue = Motion.mAcm_AxDoGetBit(m_Axishand[m_AxisNum_U], (ushort)2, ref byte_Power);
            if (Result_blue == (uint)ErrorCode.SUCCESS)
            {
                if (byte_Power == 1)
                {
                    pictureBox_ledWhite.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    pictureBox_ledWhite.BackColor = System.Drawing.Color.Gray;
                }
            }
            #endregion
        }

        UInt16 AxState_X = new UInt16();
        UInt16 AxState_Y = new UInt16();
        UInt16 AxState_Z = new UInt16();
        uint AxState_IO_X = 0;
        uint AxState_IO_Y = 0;
        private void Backthread_BoardMonitorContinous()
        {
            for (; ; )
            {
                try
                {
                    if (m_bInit)
                    {
                        double CurPos = new double();
                        //获取指定轴的当前实际位置
                        //Motion.AxGetActualPosition(m_Axishand[CmbAxes.SelectedIndex], ref CurPos);
                        //获取X轴的当前理论位置
                        Motion.mAcm_AxGetCmdPosition(m_Axishand[m_AxisNum_X], ref CurPos);
                        m_CmdPosition_X = CurPos;
                        //获取Y轴的当前理论位置
                        Motion.mAcm_AxGetCmdPosition(m_Axishand[m_AxisNum_Y], ref CurPos);
                        m_CmdPosition_Y = CurPos;

                        #region 读取状态 -- 紧急信号获取
                        ////获取X轴的当前状态
                        //Motion.mAcm_AxGetState(m_Axishand[m_AxisNum_X], ref AxState_X);
                        ////获取Y轴的当前状态
                        //Motion.mAcm_AxGetState(m_Axishand[m_AxisNum_Y], ref AxState_Y);
                        
                        Motion.mAcm_AxGetMotionIO(m_Axishand[m_AxisNum_X], ref AxState_IO_X);
                        Motion.mAcm_AxGetMotionIO(m_Axishand[m_AxisNum_Y], ref AxState_IO_Y);
                        //int ALM_X = Convert.ToInt32(AxState_IO_X & 0x02);  //获得第2bit的状态值 //ltt
                        //int ALM_Y = Convert.ToInt32(AxState_IO_X & 0x02);  //获得第2bit的状态值
                        int ALM_X = Convert.ToInt32(AxState_IO_X & 0x40);  //获得第7bit的状态值
                        int ALM_Y = Convert.ToInt32(AxState_IO_X & 0x40);  //获得第7bit的状态值
                        //紧急状态
                        //if ((ALM_X == 2) || (ALM_Y == 3))
                        if ((ALM_X == 64) || (ALM_Y == 64))
                        {
                            if (!GlobalVar.gl_inEmergence)
                            {
                                GlobalVar.gl_inEmergence = true;
                                eve_EmergeceStop(null, null);
                            }
                        }
                        //紧急停止解除
                        if (GlobalVar.gl_inEmergence)
                        {
                            if ((ALM_X == 0) || (ALM_Y == 0))
                            {
                                GlobalVar.gl_inEmergence = false;
                                //读取之前清除一下异常信号
                                ResetAlarmError();
                                eve_EmergenceRelease(null, null);
                            }
                        }
                        #endregion

                        #region 通知到达条码扫描位置
                        byte BitIn_Z = 0;
                        //Z轴IN0
                        UInt32 Result_ZU = Motion.mAcm_AxDiGetBit(m_Axishand[m_AxisNum_Z], 0, ref BitIn_Z);
                        if (Result_ZU == (uint)ErrorCode.SUCCESS)
                        {
                            if (BitIn_Z == 1)
                            {
                                if (!m_tag_SheetScan)
                                {
                                    eve_SheetBarcodeScan(null, null); //通知到达条码扫描位置
                                    m_tag_SheetScan = true;
                                }
                            }
                            else
                            {
                                if (m_tag_SheetScan)
                                {
                                    m_tag_SheetScan = false;
                                }
                            }
                        }
                        #endregion

                        #region  到板,开始测试信号
                        byte BitIn_X = 0;
                        UInt32 Result_XI = Motion.mAcm_AxDiGetBit(m_Axishand[m_AxisNum_X], 0, ref BitIn_X);
                        if (Result_XI == (uint)ErrorCode.SUCCESS)
                        {
                            if (BitIn_X == 1)
                            {                               
                                if (!m_tag_boardArrived)
                                {
                                    eve_BoardArrived(null, null);  //通知到板,开始测试
                                    m_tag_boardArrived = true;
                                }
                            }
                            else
                            {
                                if (m_tag_boardArrived)
                                {
                                    m_tag_boardArrived = false;
                                }
                            }
                        }
                        #endregion

                        #region 安全锁信号
                        byte BitIn_Front = 0;
                        UInt32 Result_Front = Motion.mAcm_AxDiGetBit(m_Axishand[m_AxisNum_U], 1, ref BitIn_Front);
                        if (Result_Front == (uint)ErrorCode.SUCCESS)
                        {
                            if (BitIn_Front == 1)
                            {
                                if (!GlobalVar.gl_safetyDoor_Front && GlobalVar.gl_usermode == 0)
                                {
                                    GlobalVar.gl_safetyDoor_Front = true; //前安全门关闭
                                }
                            }
                            else
                            {
                                if (GlobalVar.gl_safetyDoor_Front)
                                {
                                    GlobalVar.gl_safetyDoor_Front = false; //前安全门打开
                                }
                            }
                        }
                        if (eve_SofetyDoor != null)
                            eve_SofetyDoor(null, null); //只做显示用
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    logWR.appendNewLogMessage("板卡后台监测线程异常！\r\n" + ex.ToString());
                }
                Thread.Sleep(100);
            }
        }
        
        /// <summary>
        /// 定点运动---x轴距离-m_LPValue_AxisX    y轴距离-m_LPValue_AxisY
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="multiple">正常扫描=6，MARK扫描=10</param>
        public void FixPointMotion(float x, float y, int multiple)
        {
            double CurPos = new double();
            //获取指定轴的当前实际位置      Motion.AxGetActualPosition(m_Axishand[CmbAxes.SelectedIndex], ref CurPos);
            //获取X轴的当前理论位置
            Motion.mAcm_AxGetCmdPosition(m_Axishand[m_AxisNum_X], ref CurPos);
            m_CmdPosition_X = CurPos;
            double dis_x = x * GlobalVar.gl_PixelDistance - m_CmdPosition_X;
            Motion.mAcm_AxGetCmdPosition(m_Axishand[m_AxisNum_Y], ref CurPos);
            m_CmdPosition_Y = CurPos;
            double dis_y = y * GlobalVar.gl_PixelDistance - m_CmdPosition_Y;

            SetPoxEnd_X(dis_x / GlobalVar.gl_PixelDistance, true);
            SetPoxEnd_Y(dis_y / GlobalVar.gl_PixelDistance, true);
            AxisGroup_Move(true);
        }

        ///判断所有轴运动完毕        
        /// <returns>true:运动中   false:静止</returns>
        public bool CheckAxisInMoving()
        {
            //Acm_GpGetState 返回ready表示运动完毕
            UInt16 GpState = new UInt16();
            uint result = Motion.mAcm_GpGetState(m_GpHand, ref GpState);
            if (result == 0)
            {
                m_GpAxisStatus = GpState;
                return GpState != 1;
            }
            else
                return true;
        }

        public void WaitAllMoveFinished()
        {
            if (!GlobalVar.gl_Board1245EInit) return;
            Thread.Sleep(10);
            //Thread thd = new Thread(new ThreadStart(delegate //ltt
            //{
                UInt16 AxState_X = new UInt16();
                UInt16 AxState_Y = new UInt16();
                UInt16 GpState = new UInt16();
                for (; ; )
                {
                    uint result = Motion.mAcm_GpGetState(m_GpHand, ref GpState);
                    if ((result == (uint)ErrorCode.SUCCESS)
                        && (GpState == 1))
                    {
                        Motion.mAcm_AxGetState(m_Axishand[m_AxisNum_X], ref AxState_X);
                        Motion.mAcm_AxGetState(m_Axishand[m_AxisNum_Y], ref AxState_Y);
                        if ((AxState_X == 1) && (AxState_Y == 1))
                        { break; }
                        Thread.Sleep(5);
                    }
                }
            //}));
            //thd.IsBackground = true;
            //thd.Start();
        }

        //开机/恢复急停，复位，回原点
        public void AutoHomeSearch_Manual()
        {
            Thread thread = new Thread(new ThreadStart(delegate
            {
                if (!GlobalVar.gl_Board1245EInit) return;
                eve_MotionMsg("设备进行原点回归运动中...");
                while (!m_bInit) { Thread.Sleep(20); }
                WaitAllMoveFinished();
                if (AllAxisBackToMachanicalOrgPoint())  //回機械原點
                {
                    WaitAllMoveFinished();
                    MovetoRefPoint();     //运动到参考原点
                    eve_MotionMsg("设备运动到参考原点，关联Ready...");
                }
                else
                {
                    MessageBox.Show("设备回原点操作失败，请重新启动上位机!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    CloseDevice();
                    Application.Exit();
                }
            }));
            thread.IsBackground = true;
            thread.Start();
        }

        public bool AllAxisBackToMachanicalOrgPoint()
        {
            bool result = Home(0);
            result  &= Home(1);
            return result;
        }
        
        //只允许从机械原点开始运动向参考原点
        public void MovetoRefPoint()
        {
            try
            {
                //设置为较慢回归速度
                SetProp_GPSpeed(m_GPValue_VelHigh_low, m_GPValue_VelLow_low,
                    m_GPValue_Acc_low, m_GPValue_Dec_low);

                SetPoxEnd_X(Convert.ToDouble(GlobalVar.gl_Ref_Point_Axis.Pos_X), true);
                SetPoxEnd_Y(Convert.ToDouble(GlobalVar.gl_Ref_Point_Axis.Pos_Y), true);
                AxisGroup_Move(true);
                WaitAllMoveFinished();
                Thread.Sleep(200);
                //清除计数器
                double cmdPosition = 0;
                if (m_bInit == true)
                {
                    Motion.mAcm_AxSetCmdPosition(m_Axishand[m_AxisNum_X], cmdPosition);
                    Motion.mAcm_AxSetCmdPosition(m_Axishand[m_AxisNum_Y], cmdPosition);
                }
            }
            catch { }
        }        

        internal void SetControlEnable(bool enable)
        {
            this.Invoke(new Action(() => {
                btn_refAxisZ.Visible = enable;
            }));
        }

        //安全门释放
        private void btn_safetyDoor_Click(object sender, EventArgs e)
        {
            SetDO(m_AxisNum_U, 7, 0); //前门释放
        }

        //安全门上锁
        private void btn_safetyDoorEnable_Click(object sender, EventArgs e)
        {
            SetDO(m_AxisNum_U, 7, 1); //前门上锁
        }

        #region ============Z轴控制============ 2017.12.18
        //更新当前Z轴速度参数
        private void button_UpdateSpdValue_Z_Click(object sender, EventArgs e)
        {
            uint result = GetSpeecZ();
            if (result != 0)
            {
                MessageBox.Show("Z轴速度参数获取失败，请检查", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private uint GetSpeecZ()
        {
            double valued = 0.0;
            uint result = GetProp_VelHigh(m_AxisNum_Z, ref valued);
            if (result == 0) numericUpDown_VelHigh_Z.Value = Convert.ToDecimal(valued);
            result += GetProp_VelLow(m_AxisNum_Z, ref valued);
            if (result == 0) numericUpDown_VelLow_Z.Value = Convert.ToDecimal(valued);
            result += GetProp_Dec(m_AxisNum_Z, ref valued);
            if (result == 0) numericUpDown_Dec_Z.Value = Convert.ToDecimal(valued);
            result += GetProp_Acc(m_AxisNum_Z, ref valued);
            if (result == 0) numericUpDown_Acc_Z.Value = Convert.ToDecimal(valued);
            return result;
        }

        private void button_SpeedSet_Z_Click(object sender, EventArgs e)
        {
            uint result = SetSpeedZ();
            if (result != 0)
            {
                MessageBox.Show("速度参数设置失败，请检查", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private uint SetSpeedZ()
        {
            uint result = SetProp_VelHigh(m_AxisNum_Z, (double)numericUpDown_VelHigh_Z.Value, false);
            result += SetProp_VelLow(m_AxisNum_Z, (double)numericUpDown_VelLow_Z.Value, false);
            result += SetProp_Jerk(m_AxisNum_Z, (double)1000, false);
            result += SetProp_Dec(m_AxisNum_Z, (double)numericUpDown_Dec_Z.Value, false);
            result += SetProp_Acc(m_AxisNum_Z, (double)numericUpDown_Acc_Z.Value, false);
            return result;
        }

        //Z轴相对运动
        private void btn_goZ_Click(object sender, EventArgs e)
        {
            uint result = SetSpeedZ();
            if (result != 0)
            {
                MessageBox.Show("速度参数设置失败，请检查", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            WaitAxisZFinished();
            double value = Convert.ToDouble(numericUpDown_MovDis_Z.Value)
                    * (comboBox_MoveDirect_Z.SelectedIndex == 0 ? 1 : -1);
            uint res = Motion.mAcm_AxMoveRel(m_Axishand[m_AxisNum_Z], value);
            if (res != (uint)ErrorCode.SUCCESS)
            {
                MessageBox.Show("Z轴运动失败: " + res);
            }
        }

        public void WaitAxisZFinished()
        {
            if (!GlobalVar.gl_Board1245EInit) return;
            Thread.Sleep(10);
            UInt16 AxState_Z = new UInt16();
            for (; ; )
            {
                Thread.Sleep(10);
                Motion.mAcm_AxGetState(m_Axishand[m_AxisNum_Z], ref AxState_Z);                
                if (AxState_Z == 1)
                { break; }
            }
        }
        private string ShowAxisZState(UInt16 AxState_Z)
        {
            string strShow = "";
            switch (AxState_Z)
            {
                case 0:
                    strShow = "STA_AxDisable";
                    break;
                case 1:
                    strShow = "STA_AxReady";
                    break;
                case 2:
                    strShow = "STA_Stopping";
                    break;
                case 3:
                    strShow = "STA_AxErrorStop";
                    break;
                case 4:
                    strShow = "STA_AxHoming";
                    break;
                case 5:
                    strShow = "STA_AxPtpMotion";
                    break;
                case 6:
                    strShow = "STA_AxContiMotion";
                    break;
                case 7:
                    strShow = "STA_AxSyncMotion";
                    break;
                case 8:
                    strShow = "STA_AX_EXT_JOG";
                    break;
                case 9:
                    strShow = "STA_AX_EXT_MPG";
                    break;
            }
            return strShow;
        }

        //z轴回零
        private void button_Home_Z_Click(object sender, EventArgs e)
        {
            HomeZ();
        }
        //Z轴不在群组中，且运动速度较慢，单独控制
        private bool HomeZ()
        {
            int AxisNum = 2;
            try
            {
                UInt32 PropertyVal = new UInt32();
                double CrossDistance = 500; //找到信号后，返回距离，初始值100
                if (!m_bInit) { return false; }
                double Vel = (double)numericUpDown_VelLow_Z.Value; //m_GPValue_VelLow_low; //1000.0;
                UInt32 Result = Motion.mAcm_SetProperty(m_Axishand[AxisNum], (uint)PropertyID.PAR_AxVelLow, ref Vel, (uint)Marshal.SizeOf(typeof(double)));
                if (Result != (uint)ErrorCode.SUCCESS)
                {
                    //MessageBox.Show("Set Property-PAR_AxVelLow Failed With Error Code[0x" + Convert.ToString(Result, 16) + "]", "Home", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                Vel = (double)numericUpDown_VelHigh_Z.Value; //m_GPValue_VelHigh_low; //3000.0;
                Result = Motion.mAcm_SetProperty(m_Axishand[AxisNum], (uint)PropertyID.PAR_AxVelHigh, ref Vel, (uint)Marshal.SizeOf(typeof(double)));
                if (Result != (uint)ErrorCode.SUCCESS)
                {
                    //MessageBox.Show("Set Property-PAR_AxVelHigh Failed With Error Code[0x" + Convert.ToString(Result, 16) + "]", "Home", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                PropertyVal = 2; //Edge on
                Result = Motion.mAcm_SetProperty(m_Axishand[AxisNum], (uint)PropertyID.PAR_AxHomeExSwitchMode, ref PropertyVal, (uint)Marshal.SizeOf(typeof(UInt32)));
                if (Result != (uint)ErrorCode.SUCCESS)
                {
                    //MessageBox.Show("Set Property-PAR_AxHomeExSwitchMode Failed With Error Code[0x" + Convert.ToString(Result, 16) + "]", "Home", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                Result = Motion.mAcm_SetProperty(m_Axishand[AxisNum], (uint)PropertyID.PAR_AxHomeCrossDistance, ref CrossDistance, (uint)Marshal.SizeOf(typeof(double)));
                if (Result != (uint)ErrorCode.SUCCESS)
                {
                    //MessageBox.Show("Set Property-AxHomeCrossDistance Failed With Error Code[0x" + Convert.ToString(Result, 16) + "]", "Home", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //Result = Motion.mAcm_AxHome(m_Axishand[AxisNum], (UInt32)comboBoxMode.SelectedIndex, (UInt32)comboBoxDir.SelectedIndex);
                Result = Motion.mAcm_AxHome(m_Axishand[AxisNum], 11, 1);  //MODE12_AbsSearchReFind  |  Nagative Direction
                if (Result != (uint)ErrorCode.SUCCESS)
                {
                    //MessageBox.Show("AxHome Failed With Error Code[0x" + Convert.ToString(Result, 16) + "]", "Home", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void btn_refAxisZ_Click(object sender, EventArgs e)
        {
            double value = Convert.ToDouble(numericUpDown_refAxisZ.Value)
                    * (comboBox_op.SelectedIndex == 1 ? -1 : 0);
            GlobalVar.gl_dAxisZRef = value;
            MoveToAxisZRef(GlobalVar.gl_dAxisZRef);
        }
        public void MoveToAxisZRef(double value)
        {
            HomeZ();
            WaitAxisZFinished();
            uint res = Motion.mAcm_AxMoveRel(m_Axishand[m_AxisNum_Z], value * GlobalVar.gl_PixelDistanceZ);
            if (res != (uint)ErrorCode.SUCCESS)
            {
                MessageBox.Show("Z轴参考点设定失败: " + res);
            }
            else
            {
                string iniFilePath = GlobalVar.gl_strTargetPath + "\\" + GlobalVar.gl_iniTBS_FileName;
                MyFunctions.WritePrivateProfileString(GlobalVar.gl_iniSection_AxisZRef, GlobalVar.gl_LinkType.ToString(), value.ToString(), iniFilePath);
            }
        }
        public void ShowAixsZRef(double value)
        {
            this.Invoke(new Action(() => {
                comboBox_op.SelectedIndex = value < 0 ? 1 : 0;
                numericUpDown_refAxisZ.Value = Convert.ToDecimal(Math.Abs(value));
            }));            
        }
        #endregion

    }


    public enum AxisStatus : int
    {
        STA_AX_DISABLE = 0,
        STA_AX_READY = 1,
        STA_AX_STOPPING = 2,    
        STA_AX_ERROR_STOP = 3,
        STA_AX_HOMING = 4,
        STA_AX_PTP_MOT = 5,
        STA_AX_CONTI_MOT = 6,
        STA_AX_SYNC_MOT = 7
    }
}
