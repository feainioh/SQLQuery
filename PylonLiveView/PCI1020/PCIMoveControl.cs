using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using PylonLiveView;
using HANDLE = System.IntPtr;  

namespace PylonLiveView
{
    public partial class PCIMoveControl : UserControl
    {
        public event EventHandler
            eve_BoardArrived,
            eve_SheetBarcodeScan,
            eve_EmergeceStop,
            eve_EmergenceRelease;
        
        //[DllImport("kernel32.dll")]
        //public static extern IntPtr CreateEvent(Int32 lpEventAttributes, Int32 bManualReset, Int32 bInitialState, Int32 lpName);
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateEvent(HANDLE lpEventAttributes, [In, MarshalAs(UnmanagedType.Bool)] bool bManualReset, [In, MarshalAs(UnmanagedType.Bool)] bool bIntialState, [In, MarshalAs(UnmanagedType.BStr)] string lpName);  
        //Public Declare Function WaitForSingleObject Lib "" (ByVal hEvent As Long, ByVal dwMilliseconds As Long) As Long
        [DllImport("kernel32.dll")]
        public static extern int WaitForSingleObject(IntPtr hEvent, Int32 dwMilliseconds);
        public EventWaitHandle m_waitMoveFinished_X = new EventWaitHandle(false, EventResetMode.AutoReset);
        public EventWaitHandle m_waitMoveFinished_Y = new EventWaitHandle(false, EventResetMode.AutoReset);
        private bool m_tag_boardArrived = false;  //载板到达测试位置标识，到位后设置一次，并通知开始测试，如果已经为true则不再通知。  ZIN0==0 时复位
        private bool m_tag_SheetScan = false;     //载板到达扫描位置标识，到位后设置一次，并通知开始测试，如果已经为true则不再通知。  ZIN1==0 时复位
        
        #region 輸入信號變量
        private PCI1020.PCI1020_PARA_RR0 RR0;         // 状态寄存器RR0
        private PCI1020.PCI1020_PARA_RR3 RR3;         // 状态寄存器RR3
        private PCI1020.PCI1020_PARA_RR4 RR4;         // 状态寄存器RR4

        private PCI1020.PCI1020_PARA_DataList DL_Axis_X;		// 公用参数
        private PCI1020.PCI1020_PARA_LCData LC_Axis_X;			// 直线和S曲线参数
        private PCI1020.PCI1020_PARA_RR1 RR1_Axis_X;         // 状态寄存器RR1
        private PCI1020.PCI1020_PARA_RR2 RR2_Axis_X;         // 状态寄存器RR2
        private PCI1020.PCI1020_PARA_RR5 RR5_Axis_X;         // 状态寄存器RR5

        private PCI1020.PCI1020_PARA_DataList DL_Axis_Y;
        private PCI1020.PCI1020_PARA_LCData LC_Axis_Y;
        private PCI1020.PCI1020_PARA_RR1 RR1_Axis_Y;         // 状态寄存器RR1
        private PCI1020.PCI1020_PARA_RR2 RR2_Axis_Y;         // 状态寄存器RR2
        private PCI1020.PCI1020_PARA_RR5 RR5_Axis_Y;         // 状态寄存器RR5

        private PCI1020.PCI1020_PARA_DataList DL_Axis_Z;
        private PCI1020.PCI1020_PARA_LCData LC_Axis_Z;
        private PCI1020.PCI1020_PARA_RR1 RR1_Axis_Z;         // 状态寄存器RR1
        private PCI1020.PCI1020_PARA_RR2 RR2_Axis_Z;         // 状态寄存器RR2
        #endregion

        #region 手动寻原点使用变量
        private bool m_tag_inAutoHomeSearch_Xstep1 = false;             //是否在自动原点搜寻第一步过程中
        private bool m_tag_inAutoHomeSearch_Xstep2_waitStart = false;   //是否在自动原点搜寻第二步等待灯亮 -> 灯灭过程中
        private bool m_tag_inAutoHomeSearch_Xstep2_waitEnd = false;   //是否在自动原点搜寻第二步等待灯灭 -> 灯亮过程中
        
        private bool m_tag_inAutoHomeSearch_Ystep1 = false;             //是否在自动原点搜寻第一步过程中
        private bool m_tag_inAutoHomeSearch_Ystep2_waitStart = false;   //是否在自动原点搜寻第二步等待灯亮 -> 灯灭过程中
        private bool m_tag_inAutoHomeSearch_Ystep2_waitEnd = false;   //是否在自动原点搜寻第二步等待灯灭 -> 灯亮过程中
        #endregion

        private bool m_initFinished = false;
        public bool m_deviceOpened = false;     //判断设备是否连接成功，否则程序不允许运行
        public IntPtr hDevice; // 创建设备对象
        public bool m_status_Axis_X = false;     //轴状态 true:运动中  false:停止运动 
        public bool m_status_Axis_Y = false;     //轴状态 true:运动中  false:停止运动 
        public bool m_status_Axis_Z = false;     //轴状态 true:运动中  false:停止运动 

        public int m_EPValue_AxisX = 0;      //X轴实位计数器
        public int m_EPValue_AxisY = 0;      //Y轴实位计数器
        public int m_LPValue_AxisX = 0;      //X轴实位计数器
        public int m_LPValue_AxisY = 0;      //Y轴实位计数器


        private PCI1020.PCI1020_PARA_ExpMode m_ExpMode_Para;						// 设置其他参数

        private PCI1020.PCI1020_PARA_Interrupt Para;		// 设置中断位使能	
        IntPtr hEventInterrupt;

        #region 輸出I/0信號變量
        private PCI1020.PCI1020_PARA_DO m_IO_Para_X, m_IO_Para_Y, m_IO_Para_Z, m_IO_Para_U;	
        #endregion

        #region  只读变量--从continous read中读取的控制卡输入状态
        private bool m_inParaSetting = false;  //是否在修改参数函数中，这段过程不要读取状态
        public uint m_ServerStatus_AxisX;
        public uint m_ServerStatus_AxisY;
        #endregion

        public PCIMoveControl()
        {
            InitializeComponent();
            OpenDevice();

            comboBox_AxisX_MoveType.SelectedIndex = 0;
            comboBox_AxisY_MoveType.SelectedIndex = 0;
        }

        EventWaitHandle waitBack = new EventWaitHandle(false, EventResetMode.AutoReset);
        public void OpenDevice()
        {
            try
            {
                hDevice = PCI1020.PCI1020_CreateDevice(0); // 创建设备对象
                if (hDevice == (IntPtr)(-1))
                {
                    m_deviceOpened = false;
                    return; // 如果创建设备对象失败，则返回
                }
                m_deviceOpened = true;
                LC_Axis_X.AxisNum = PCI1020.PCI1020_XAXIS;					// 轴号(PCI1020_XAXIS:X轴; PCI1020_YAXIS:Y轴；PCI1020_ZAXIS:Z轴; PCI1020_UAXIS:U轴)
                LC_Axis_Y.AxisNum = PCI1020.PCI1020_YAXIS;
                LC_Axis_X.LV_DV = PCI1020.PCI1020_DV;						// 驱动方式 PCI1020_LV:连续驱动；PCI1020_DV:定长驱动
                LC_Axis_Y.LV_DV = PCI1020.PCI1020_DV;
                LC_Axis_X.PulseMode = PCI1020.PCI1020_CPDIR;				// 模式0：CW/CCW方式，1：CP/DIR方式 
                LC_Axis_Y.PulseMode = PCI1020.PCI1020_CPDIR;
                LC_Axis_X.Line_Curve = PCI1020.PCI1020_CURVE;				// 直线曲线(0:直线加/减速; 1:S曲线加/减速)
                LC_Axis_Y.Line_Curve = PCI1020.PCI1020_CURVE;
                DL_Axis_X.Multiple = 1;								        // 倍数 (1~500) 
                DL_Axis_Y.Multiple = 1;
                PCI1020.PCI1020_Reset(hDevice);
                if (!backgroundWorker_continousMonitor.IsBusy)
                {
                    backgroundWorker_continousMonitor.RunWorkerAsync();
                }
                InitDevice();
                timer_refresh.Enabled = true;
                m_initFinished = true;
            }
            catch { }
        }

        public void CloseDevice()
        {
            try
            {
                if (hDevice != (IntPtr)(-1))
                {
                    PCI1020.PCI1020_Reset(hDevice);
                    PCI1020.PCI1020_ReleaseDeviceInt(hDevice);
                    PCI1020.PCI1020_ReleaseDevice(hDevice);
                }
                backgroundWorker_continousMonitor.CancelAsync();
                timer_refresh.Enabled = false;
            }
            catch { }
        }

        //初始化设备参数，用于刚连接设备或者复位恢复
        public void InitDevice()
        {
            try
            {
                //InitInterrupts();
                ALLIOInit();
                ServerX_ON();   //开启伺服
                ServerY_ON();
            }
            catch { }
        }

        public void WaitAllMoveFinished()
        {
            Thread.Sleep(10);
            //如果50个ms内，XY都没有在运动，判断为静止'
            for (; ; )
            {
                bool status = false;
                for (int i = 0; i < 12; i++)
                {
                    PCI1020.PCI1020_GetRR0Status(
                                               hDevice,			// 设备句柄
                                               ref RR0);				// RR0寄存器状态	
                    m_status_Axis_X = Convert.ToBoolean(RR0.XDRV);
                    m_status_Axis_Y = Convert.ToBoolean(RR0.YDRV);
                    m_status_Axis_Z = Convert.ToBoolean(RR0.ZDRV);
                    status |= m_status_Axis_X;
                    status |= m_status_Axis_Y;
                    Thread.Sleep(2);
                }
                if (!status) { break; }
            }
            //之前的检测判断XY轴是否在运动的方式，有漏洞，放弃
            //m_waitMoveFinished_X.Reset();
            //m_waitMoveFinished_Y.Reset();
            //m_waitMoveFinished_X.WaitOne();
            //m_waitMoveFinished_Y.WaitOne();
        }
                
        /// <returns>true:运动中   false:静止</returns>
        public bool CheckAxisInMoving()
        {
            Thread.Sleep(20);
            //如果30个ms内，XY都没有在运动，判断为静止'
            for (; ; )
            {
                PCI1020.PCI1020_GetRR0Status(
                                           hDevice,			// 设备句柄
                                           ref RR0);				// RR0寄存器状态	
                m_status_Axis_X = Convert.ToBoolean(RR0.XDRV);
                m_status_Axis_Y = Convert.ToBoolean(RR0.YDRV);
                m_status_Axis_Z = Convert.ToBoolean(RR0.ZDRV);
                bool status = false;
                for (int i = 0; i < 10; i++)
                {
                    status |= m_status_Axis_X;
                    status |= m_status_Axis_Y;
                    Thread.Sleep(3);
                }
                return status;
            }
        }

        //开机/恢复急停，复位，回原点
        public void AutoHomeSearch_Manual()
        {
            Thread thread = new Thread(new ThreadStart(delegate
            {
                while (!m_initFinished)
                { }
                WaitAllMoveFinished();
                if (AllAxisBackToMachanicalOrgPoint())  //回機械原點
                {
                    WaitAllMoveFinished();
                    MovetoRefPoint();     //运动到参考原点
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

        private void InitInterrupts()
        {
            try
            {
                hEventInterrupt = CreateEvent(IntPtr.Zero, false, false, "");
                Para.DEND = 1;
                PCI1020.PCI1020_SetInterruptBit(		// 设置中断位
                                hDevice,		// 设备句柄
                                PCI1020.PCI1020_XAXIS,		// 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
                                ref Para);			// 中断位结构体指针
                PCI1020.PCI1020_InitDeviceInt(hDevice, hEventInterrupt);		// 初始化中断
                ThreadPool.QueueUserWorkItem(Thread_waitInterrupt_AutoHomeSearch);
            }
            catch { }
        }

        #region 后台不停监测输入信号
        private void backgroundWorker_continousMonitor_DoWork(object sender, DoWorkEventArgs e)
        {
            try { CardStatusMonitor(e); }
            catch { }
        }

        private void CardStatusMonitor(object obj)
        {
            for (; ; )
            {
                if (m_inParaSetting) { continue; }
                if (backgroundWorker_continousMonitor.CancellationPending)
                {
                    (obj as DoWorkEventArgs).Cancel = true;
                    return;
                }
                try
                {
                    //Speed_Axis_X = (PCI1020.PCI1020_ReadCV(hDevice, LC_Axis_X.AxisNum)) * DL_Axis_X.Multiple;	// 读当前速度
                    //Accelerate_Axis_X = (PCI1020.PCI1020_ReadCA(hDevice, LC_Axis_X.AxisNum)) * DL_Axis_X.Multiple * 125;	// 读当前加速度
                    //LP_Axis_X = PCI1020.PCI1020_ReadLP(hDevice, LC_Axis_X.AxisNum);	// 读逻辑计数器

                    PCI1020.PCI1020_GetRR3Status(
                                        hDevice,			// 设备句柄
                                        ref RR3);				// RR3寄存器状态		
                    PCI1020.PCI1020_GetRR4Status(
                                        hDevice,			// 设备句柄
                                        ref RR4);				// RR4寄存器状态		

                    PCI1020.PCI1020_GetRR1Status(
                                         hDevice,			// 设备句柄
                                         PCI1020.PCI1020_XAXIS,
                                         ref RR1_Axis_X);				// RR1寄存器状态		
                    PCI1020.PCI1020_GetRR2Status(
                                         hDevice,			// 设备句柄
                                         PCI1020.PCI1020_XAXIS,
                                         ref RR2_Axis_X);				// RR2寄存器状态		
                    //PCI1020.PCI1020_GetRR5Status(
                    //                    hDevice,			// 设备句柄
                    //                     PCI1020.PCI1020_XAXIS,
                    //                    ref RR5_Axis_X);				// RR3寄存器状态		
                    PCI1020.PCI1020_GetRR1Status(
                                         hDevice,			// 设备句柄
                                         PCI1020.PCI1020_YAXIS,
                                         ref RR1_Axis_Y);				// RR1寄存器状态		
                    PCI1020.PCI1020_GetRR2Status(
                                         hDevice,			// 设备句柄
                                         PCI1020.PCI1020_YAXIS,
                                         ref RR2_Axis_Y);				// RR2寄存器状态		
                    //PCI1020.PCI1020_GetRR5Status(
                    //                    hDevice,			// 设备句柄
                    //                     PCI1020.PCI1020_YAXIS,
                    //                    ref RR5_Axis_Y);				// RR3寄存器状态		

                    //紧急停止
                    if ((RR2_Axis_X.EMG == 1) || (RR2_Axis_Y.EMG == 1) || (RR2_Axis_Z.EMG == 1))
                    {
                        if (!GlobalVar.gl_inEmergence)
                        {
                            GlobalVar.gl_inEmergence = true;
                            eve_EmergeceStop(null, null);
                        }
                    }

                    //通知到达条码扫描位置
                    if ((RR4.ZIN0 == 0) && (!m_tag_SheetScan))
                    {
                        m_tag_SheetScan = true;
                        eve_SheetBarcodeScan(null, null);
                    }
                    else if ((RR4.ZIN0 == 1) && m_tag_SheetScan)
                    {
                        m_tag_SheetScan = false;
                    }

                    //通知到板,开始测试
                    if ((RR4.ZIN1 == 0) && (!m_tag_boardArrived))
                    {
                        //ResetStage1Signal();
                        m_tag_boardArrived = true; 
                        eve_BoardArrived(null, null);
                    }
                    else if ((RR4.ZIN1 == 1) && m_tag_boardArrived)
                    {
                        m_tag_boardArrived = false;
                    }

                    if (!m_status_Axis_X)
                    { m_waitMoveFinished_X.Set(); }
                    if (!m_status_Axis_Y)
                    { m_waitMoveFinished_Y.Set(); }

                    #region 手动原点搜寻信号侦测     --废弃不用               
                    if ((RR3.XIN0 == 0) && m_tag_inAutoHomeSearch_Xstep1)
                    {
                        m_tag_inAutoHomeSearch_Xstep1 = false;  //X轴原点搜寻第一次正向搜寻回到参考原点，开始减速
                    }
                    if (m_tag_inAutoHomeSearch_Xstep2_waitStart && (RR3.XIN0 == 0))
                    {
                        m_tag_inAutoHomeSearch_Xstep2_waitStart = false;  //第二步反向运动灯亮 -> 灯灭标志
                    }
                    if (m_tag_inAutoHomeSearch_Xstep2_waitEnd && (RR3.XIN0 == 1))
                    {
                        m_tag_inAutoHomeSearch_Xstep2_waitEnd = false; //第二步反向运动灯灯灭 -> 再亮标志,找寻过程结束
                    }
                    
                    if ((RR3.YIN0 == 0) && m_tag_inAutoHomeSearch_Ystep1)
                    {
                        m_tag_inAutoHomeSearch_Ystep1 = false;  //Y轴原点搜寻回到参考原点，开始减速
                    }
                    if (m_tag_inAutoHomeSearch_Ystep2_waitStart && (RR3.YIN0 == 0))
                    {
                        m_tag_inAutoHomeSearch_Ystep2_waitStart = false;  //第二步反向运动灯亮 -> 灯灭标志,也可能直接就是灭的
                    }
                    if (m_tag_inAutoHomeSearch_Ystep2_waitEnd && (RR3.YIN0 == 1))
                    {
                        m_tag_inAutoHomeSearch_Ystep2_waitEnd = false;  //第二步反向运动灯灯灭 -> 再亮标志,找寻过程结束
                    }
                    #endregion

                    //紧急停止解除
                    if (GlobalVar.gl_inEmergence &&
                        (RR2_Axis_X.EMG == 0) && (RR2_Axis_Y.EMG == 0) && (RR2_Axis_Z.EMG == 0))
                    {
                        GlobalVar.gl_inEmergence =  false;
                        eve_EmergenceRelease(null, null);
                    }

                    m_LPValue_AxisX = PCI1020.PCI1020_ReadLP(hDevice, PCI1020.PCI1020_XAXIS);
                    m_LPValue_AxisY = PCI1020.PCI1020_ReadLP(hDevice, PCI1020.PCI1020_YAXIS);
                    m_EPValue_AxisX = PCI1020.PCI1020_ReadEP(hDevice, PCI1020.PCI1020_XAXIS);
                    m_EPValue_AxisY = PCI1020.PCI1020_ReadEP(hDevice, PCI1020.PCI1020_YAXIS);


                }
                catch(Exception e)
                {
                    logWR.appendNewLogMessage("板卡状态读取异常:" + e.ToString());
                }
                Thread.Sleep(20);
            }
        }

        private void updateStatusValue()
        {
            try
            {
                #region 实位计数器
                textBox_ReadEP_AxisX.Text = m_EPValue_AxisX.ToString();
                textBox_ReadEP_AxisY.Text = m_EPValue_AxisY.ToString();
                textBox_ReadLP_AxisX.Text = m_LPValue_AxisX.ToString();
                textBox_ReadLP_AxisY.Text = m_LPValue_AxisY.ToString();
                #endregion

                #region RR0
                button_status_AxisX_inMove.BackColor = m_status_Axis_X ? Color.DarkRed : Color.White;
                button_status_AxisX_inWaiting.BackColor = m_status_Axis_X ? Color.White : Color.DarkRed;
                button_status_AxisY_inMove.BackColor = m_status_Axis_Y ? Color.DarkRed : Color.White;
                button_status_AxisY_inWaiting.BackColor = m_status_Axis_Y ? Color.White : Color.DarkRed;
                #endregion

                #region RR3
                m_ServerStatus_AxisX = RR3.XIN1;
                m_ServerStatus_AxisY = RR3.YIN1;
                textBox_status_AxisX_XIN0.Text = RR3.XIN0.ToString();
                textBox_status_AxisX_XIN1.Text = RR3.XIN1.ToString();
                textBox_status_AxisX_XIN2.Text = RR3.XIN2.ToString();
                textBox_status_AxisX_XIN3.Text = RR3.XIN3.ToString();
                textBox_status_AxisX_XEXPP.Text = RR3.XEXPP.ToString();
                textBox_status_AxisX_XEXPM.Text = RR3.XEXPM.ToString();
                textBox_status_AxisX_XINPOS.Text = RR3.XINPOS.ToString();
                textBox_status_AxisX_XALARM.Text = RR3.XALARM.ToString();

                textBox_status_AxisY_YIN0.Text = RR3.YIN0.ToString();
                textBox_status_AxisY_YIN1.Text = RR3.YIN1.ToString();
                textBox_status_AxisY_YIN2.Text = RR3.YIN2.ToString();
                textBox_status_AxisY_YIN3.Text = RR3.YIN3.ToString();
                textBox_status_AxisY_YEXPP.Text = RR3.YEXPP.ToString();
                textBox_status_AxisY_YEXPM.Text = RR3.YEXPM.ToString();
                textBox_status_AxisY_YINPOS.Text = RR3.YINPOS.ToString();
                textBox_status_AxisY_YALARM.Text = RR3.YALARM.ToString();
                #endregion

                #region RR1
                textBox_RR1status_AxisX_IN0.Text = RR1_Axis_X.IN0.ToString();
                textBox_RR1status_AxisX_IN1.Text = RR1_Axis_X.IN1.ToString();
                textBox_RR1status_AxisX_IN2.Text = RR1_Axis_X.IN2.ToString();
                textBox_RR1status_AxisX_IN3.Text = RR1_Axis_X.IN3.ToString();
                textBox_RR1status_AxisX_LMTM.Text = RR1_Axis_X.LMTM.ToString();
                textBox_RR1status_AxisX_LMTP.Text = RR1_Axis_X.LMTP.ToString();
                textBox_RR1status_AxisX_ALARM.Text = RR1_Axis_X.ALARM.ToString();
                textBox_RR1status_AxisX_EMGN.Text = RR1_Axis_X.EMG.ToString();

                textBox_RR1status_AxisY_IN0.Text = RR1_Axis_Y.IN0.ToString();
                textBox_RR1status_AxisY_IN1.Text = RR1_Axis_Y.IN1.ToString();
                textBox_RR1status_AxisY_IN2.Text = RR1_Axis_Y.IN2.ToString();
                textBox_RR1status_AxisY_IN3.Text = RR1_Axis_Y.IN3.ToString();
                textBox_RR1status_AxisY_LMTM.Text = RR1_Axis_Y.LMTM.ToString();
                textBox_RR1status_AxisY_LMTP.Text = RR1_Axis_Y.LMTP.ToString();
                textBox_RR1status_AxisY_ALARM.Text = RR1_Axis_Y.ALARM.ToString();
                textBox_RR1status_AxisY_EMGN.Text = RR1_Axis_Y.EMG.ToString();
                #endregion
            }
            catch { }
        }
        #endregion 

        #region Server ON/OFF
        private void button_ServerX_ON_Click(object sender, EventArgs e)
        {
            ServerX_ON();
        }

        private void button_ServerX_OFF_Click(object sender, EventArgs e)
        {
            ServerX_OFF();
        }

        private void button_ServerY_ON_Click(object sender, EventArgs e)
        {
            ServerY_ON();
        }

        private void button_ServerY_OFF_Click(object sender, EventArgs e)
        {
            ServerY_OFF();
        }
        #endregion

        #region 运动控制
        /// <summary>
        /// X轴运动参数设定
        /// </summary>
        /// <param name="Direction">PCI1020.PCI1020_PDIRECTION / PCI1020.PCI1020_MDIRECTION</param>
        /// <param name="DriverMode">驱动方式 PCI1020_LV:连续驱动；PCI1020_DV:定长驱动</param>
        /// <param name="StartSpeed">起始速度</param>
        /// <param name="Acceleration"></param>
        /// <param name="Deceleration"></param>
        /// <param name="DriveSpeed">0:反向  1：正向</param>
        /// <param name="PulseCount"></param>
        public bool AxisMoveParasSetting_X(int Direction, int DriverMode, int StartSpeed, int Multiple, int Acceleration, int Deceleration, int DriveSpeed, int PulseCount,
            int AccIncRate, int DecIncRate)
        {
            try
            {
                LC_Axis_X.AxisNum = PCI1020.PCI1020_XAXIS;					// 轴号(PCI1020_XAXIS:X轴; PCI1020_YAXIS:Y轴；PCI1020_ZAXIS:Z轴; PCI1020_UAXIS:U轴)
                LC_Axis_X.LV_DV = DriverMode;						// 驱动方式 PCI1020_LV:连续驱动；PCI1020_DV:定长驱动
                LC_Axis_X.PulseMode = PCI1020.PCI1020_CPDIR;				// 模式0：CW/CCW方式，1：CP/DIR方式 
                LC_Axis_X.Line_Curve = PCI1020.PCI1020_CURVE;				// 直线曲线(0:直线加/减速; 1:S曲线加/减速)
                LC_Axis_X.Line_Curve = PCI1020.PCI1020_LINE;				// 直线曲线(0:直线加/减速; 1:S曲线加/减速)
                LC_Axis_X.Direction = Direction;			// 转动方向 PCI1020_PDirection: 正转  PCI1020_MDirection:反转	\
                LC_Axis_X.DecMode = PCI1020.PCI1020_AUTO;
                DL_Axis_X.StartSpeed = StartSpeed;
                DL_Axis_X.Multiple = Multiple;
                DL_Axis_X.Acceleration = Acceleration;
                DL_Axis_X.Deceleration = Deceleration;
                DL_Axis_X.DriveSpeed = DriveSpeed;
                DL_Axis_X.AccIncRate = AccIncRate;
                DL_Axis_X.DecIncRate = DecIncRate;
                LC_Axis_X.nPulseNum = Math.Abs(PulseCount);
                return PCI1020.PCI1020_InitLVDV(				//	初始化连续,定长脉冲驱动
                                hDevice,
                                ref DL_Axis_X,
                                ref LC_Axis_X);                 
            }
            catch { return false; }
        }

        /// <summary>
        /// Y轴运动参数设定
        /// </summary>
        /// <param name="Direction">PCI1020.PCI1020_PDIRECTION / PCI1020.PCI1020_MDIRECTION</param>
        /// <param name="DriverMode">驱动方式 PCI1020_LV:连续驱动；PCI1020_DV:定长驱动</param>
        /// <param name="StartSpeed"></param>
        /// <param name="Acceleration"></param>
        /// <param name="Deceleration"></param>
        /// <param name="DriveSpeed"></param>
        /// <param name="DriveSpeed">0:反向  1：正向</param>
        /// <param name="PulseCount"></param>
        public bool AxisMoveParasSetting_Y(int Direction, int DriverMode, int StartSpeed, int Multiple, int Acceleration, int Deceleration, int DriveSpeed, int PulseCount,
            int AccIncRate, int DecIncRate)
        {
            try
            {
                LC_Axis_Y.AxisNum = PCI1020.PCI1020_YAXIS;
                LC_Axis_Y.LV_DV = DriverMode;
                LC_Axis_Y.PulseMode = PCI1020.PCI1020_CPDIR;
                //LC_Axis_Y.Line_Curve = PCI1020.PCI1020_CURVE;
                LC_Axis_Y.Line_Curve = PCI1020.PCI1020_LINE;
                LC_Axis_Y.Direction = Direction;			// 转动方向 PCI1020_PDirection: 正转  PCI1020_MDirection:反转	
                LC_Axis_Y.DecMode = PCI1020.PCI1020_AUTO;
                DL_Axis_Y.StartSpeed = StartSpeed;
                DL_Axis_Y.Multiple = Multiple;
                DL_Axis_Y.Acceleration = Acceleration;
                DL_Axis_Y.Deceleration = Deceleration;
                DL_Axis_Y.DriveSpeed = DriveSpeed;
                DL_Axis_Y.AccIncRate = AccIncRate;
                DL_Axis_Y.DecIncRate = DecIncRate;
                LC_Axis_Y.nPulseNum = Math.Abs(PulseCount);
                return PCI1020.PCI1020_InitLVDV(				//	初始化连续,定长脉冲驱动
                                hDevice,
                                ref DL_Axis_Y,
                                ref LC_Axis_Y);
            }
            catch { return false; }
        }

        private void button_AxisX_start_Click(object sender, EventArgs e)
        {
            AxisMoveParasSetting_X(radioButton_AxisX_MoveDirc_M.Checked ? 0 : 1 
                , PCI1020.PCI1020_DV
                , Convert.ToInt32(numericUpDown_AxisX_StartSpeed.Value)
                , Convert.ToInt32(numericUpDown_AxisX_Multiple.Value), Convert.ToInt32(numericUpDown_AxisX_AccelerateSpeed.Value)
                , Convert.ToInt32(numericUpDown_AxisX_DecelerateSpeed.Value), Convert.ToInt32(numericUpDown_AxisX_DriveSpeed.Value)
                , Convert.ToInt32(numericUpDown_AxisX_PulseCount.Value), Convert.ToInt32(numericUpDown_AxisX_AccIncRate.Value)
                , Convert.ToInt32(numericUpDown_AxisX_DecIncRate.Value));
            PCI1020.PCI1020_StartLVDV(				// 启动定长脉冲驱动
                            hDevice,		// 设备句柄
                            LC_Axis_X.AxisNum);
        }

        private void button_AxisY_start_Click(object sender, EventArgs e)
        {
            AxisMoveParasSetting_Y(radioButton_AxisY_MoveDirc_M.Checked ? 0 : 1
                , PCI1020.PCI1020_DV
                , Convert.ToInt32(numericUpDown_AxisY_StartSpeed.Value)
                , Convert.ToInt32(numericUpDown_AxisY_Multiple.Value), Convert.ToInt32(numericUpDown_AxisY_AccelerateSpeed.Value)
                , Convert.ToInt32(numericUpDown_AxisY_DecelerateSpeed.Value), Convert.ToInt32(numericUpDown_AxisY_DriveSpeed.Value)
                , Convert.ToInt32(numericUpDown_AxisY_PulseCount.Value), Convert.ToInt32(numericUpDown_AxisY_AccIncRate.Value)
                , Convert.ToInt32(numericUpDown_AxisY_DecIncRate.Value));
            PCI1020.PCI1020_StartLVDV(				// 启动定长脉冲驱动
                            hDevice,		// 设备句柄
                            LC_Axis_Y.AxisNum);
        }

        public void AllAxisMoveStart()
        {
            PCI1020.PCI1020_Start4D(hDevice);
        }
        #endregion

        #region 中断获取
        private void Thread_waitInterrupt_AutoHomeSearch(object obj)
        {
            for (; ; )
            {
                WaitForSingleObject(hEventInterrupt, -1);
                //MessageBox.Show("aaaaaaaaaaaaaaaaaaaaaaa");
            }
        }
        #endregion

        #region 原点搜寻
        //只允许从机械原点开始运动向参考原点
        public void MovetoRefPoint()
        {
            try
            {
                AxisMoveParasSetting_X(PCI1020.PCI1020_PDIRECTION, PCI1020.PCI1020_DV
                    , 300, 3, 600, 600, 2500, Convert.ToInt32(GlobalVar.gl_Ref_Point_Axis.Pos_X * GlobalVar.gl_PixelDistance), 18000, 18000);
                AxisMoveParasSetting_Y(PCI1020.PCI1020_PDIRECTION, PCI1020.PCI1020_DV
                , 300, 3, 600, 600, 2500, Convert.ToInt32(GlobalVar.gl_Ref_Point_Axis.Pos_Y * GlobalVar.gl_PixelDistance), 18000, 18000);
                AllAxisMoveStart();
                WaitAllMoveFinished();
                Thread.Sleep(100);
                ClearEPLPValue();
            }
            catch { }
        }

        private void button_ReturnXRefPoint_Click(object sender, EventArgs e)
        {
            AxisMoveParasSetting_X(m_EPValue_AxisX > 0 ? 1 : 0
                , PCI1020.PCI1020_DV, 300, 3, 600, 600, 2500, m_EPValue_AxisX, 18000, 18000);
            PCI1020.PCI1020_StartLVDV(				// 启动定长脉冲驱动
                            hDevice,		// 设备句柄
                            PCI1020.PCI1020_XAXIS);
        }

        private void button_ReturnYRefPoint_Click(object sender, EventArgs e)
        {
            AxisMoveParasSetting_Y(m_EPValue_AxisY > 0 ? 1 : 0
                , PCI1020.PCI1020_DV
                , 300, 3, 600, 600, 2500, m_EPValue_AxisY, 18000, 18000);
            PCI1020.PCI1020_StartLVDV(				// 启动定长脉冲驱动
                            hDevice,		// 设备句柄
                            PCI1020.PCI1020_YAXIS);
        }

        bool m_Tag_ReturnFinished_X, m_Tag_ReturnFinished_Y;  //用作判断XY搜寻原点是否完毕
        bool m_Tag_ReturnSuccess_X, m_Tag_ReturnSuccess_Y;  //用作判断XY搜寻原点是否都执行正常
        public bool AllAxisBackToMachanicalOrgPoint()
        {
            bool result = true;
            for (int i = 0; i < 1; i++)
            {
                WaitAllMoveFinished();
                AutoHomeSearch_AxisX(null);
                AutoHomeSearch_AxisY(null);
                result &= PCI1020.PCI1020_StartAutoHomeSearch(		// 启动自动原点搜寻
                                 hDevice,			// 设备句柄		
                                 PCI1020.PCI1020_XAXIS);
                result &= PCI1020.PCI1020_StartAutoHomeSearch(		// 启动自动原点搜寻
                                 hDevice,			// 设备句柄		
                                 PCI1020.PCI1020_YAXIS);

                //出错后重复执行2次
                if (result)
                { break; }
            }
            return result;
        }


        /// <summary>
        /// 定点运动---x轴距离-m_LPValue_AxisX    y轴距离-m_LPValue_AxisY
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="multiple">正常扫描=6，MARK扫描=10</param>
        public void FixPointMotion(float x, float y, int multiple)
        {
            try
            {
                //保证精度，重复读取一次
                WaitAllMoveFinished();
                Thread.Sleep(80);
                m_LPValue_AxisX = PCI1020.PCI1020_ReadLP(hDevice, PCI1020.PCI1020_XAXIS);
                Thread.Sleep(80);
                m_LPValue_AxisY = PCI1020.PCI1020_ReadLP(hDevice, PCI1020.PCI1020_YAXIS);
                int dis_x = Convert.ToInt32(x * GlobalVar.gl_PixelDistance) - m_LPValue_AxisX;
                int dis_y = Convert.ToInt32(y * GlobalVar.gl_PixelDistance) - m_LPValue_AxisY;
                //PCI1020.PCI1020_Reset(hDevice);
                AxisMoveParasSetting_X(dis_x > 0 ? 1 : 0, PCI1020.PCI1020_DV, 2500, multiple, 5000, 10000, 6000, dis_x, 18000, 18000);
                AxisMoveParasSetting_Y(dis_y > 0 ? 1 : 0, PCI1020.PCI1020_DV, 2500, multiple, 5000, 10000, 6000, dis_y, 18000, 18000);
                //AxisMoveParasSetting_X(dis_x > 0 ? 1 : 0, PCI1020.PCI1020_DV, 1000, multiple, 1000, 1000, 2000, dis_x, 1000, 1000);
                //AxisMoveParasSetting_Y(dis_y > 0 ? 1 : 0, PCI1020.PCI1020_DV, 1000, multiple, 1000, 1000, 2000, dis_y, 1000, 1000);
                PCI1020.PCI1020_Start4D(hDevice);
            }
            catch { }
        }
        
        private void button_AxisX_AutoHomeSearch_Click(object sender, EventArgs e)
        {
            AutoHomeSearch_AxisX(null);
            PCI1020.PCI1020_StartAutoHomeSearch(		// 启动自动原点搜寻
                             hDevice,			// 设备句柄		
                             PCI1020.PCI1020_XAXIS);
        }

        private void button_AxisY_AutoHomeSearch_Click(object sender, EventArgs e)
        {
            AutoHomeSearch_AxisY(null);
            PCI1020.PCI1020_StartAutoHomeSearch(		// 启动自动原点搜寻
                             hDevice,			// 设备句柄		
                             PCI1020.PCI1020_YAXIS);
        }

        PCI1020.PCI1020_PARA_AutoHomeSearch m_Para_AutoHomeSearch_X;				// 自动原点搜寻设置
        PCI1020.PCI1020_PARA_AutoHomeSearch m_Para_AutoHomeSearch_Y;				// 自动原点搜寻设置
        public void AutoHomeSearch_AxisX(object obj)
        {
            try
            {
                PCI1020.PCI1020_SetInEnable(			// 设置自动原点搜寻第一、第二、第三步外部触发信号IN0-2的有效电平
                            hDevice,			// 设备号
                            PCI1020.PCI1020_XAXIS,			// 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)	
                            1,					// 停止号
                            0); 	
                m_ExpMode_Para.FE0 = 1;                  // 设置IN0，1滤波器有效
                m_ExpMode_Para.FE1 = 1;					// 设置IN2滤波器有效
                m_ExpMode_Para.FE4 = 1;					// 设置IN3滤波器有效
                PCI1020.PCI1020_ExtMode(				// 设置其他模式
                            hDevice,			// 设备句柄
                            PCI1020.PCI1020_XAXIS,		// 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
                            ref m_ExpMode_Para);			// 其他参数结构体指针
                m_Para_AutoHomeSearch_X.LIMIT = 0;				// 1：利用硬件限位信号(nLMTP或nLMPM)进行原点搜寻 0：无效
                m_Para_AutoHomeSearch_X.SAND = 0;				// 1：原点信号和Z相信号有效时停止第三步操作 0：无效
                m_Para_AutoHomeSearch_X.PCLR = 1;             //  回原地后计数器复位
                m_Para_AutoHomeSearch_X.ST4E = 0;					// 1：第四步使能 0：无效
                m_Para_AutoHomeSearch_X.ST4D = 0;					// 第四步的搜寻运转方向 0：正方向  1：负方向
                m_Para_AutoHomeSearch_X.ST3E = 0;					// 1：第三步使能 0：无效
                m_Para_AutoHomeSearch_X.ST3D = 0;					// 第三步的搜寻运转方向 0：正方向  1：负方向
                m_Para_AutoHomeSearch_X.ST2E = 1;					// 1：第二步使能 0：无效
                m_Para_AutoHomeSearch_X.ST2D = 1;					// 第二步的搜寻运转方向 0：正方向  1：负方向
                m_Para_AutoHomeSearch_X.ST1E = 1;					// 1：第一步使能 0：无效
                m_Para_AutoHomeSearch_X.ST1D = 1;			    	// 第一步的搜寻运转方向 0：正方向  1：负方向  

                //m_Para_AutoHomeSearch.HMINT = 1;
                PCI1020.PCI1020_SetAutoHomeSearch(		// 设置自动原点搜寻参数
                        hDevice,			// 设备句柄
                        PCI1020.PCI1020_XAXIS,		// 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)
                        ref m_Para_AutoHomeSearch_X);
                PCI1020.PCI1020_SetR(hDevice, PCI1020.PCI1020_XAXIS, 1 * 1);     // 倍率
                PCI1020.PCI1020_SetA(hDevice, PCI1020.PCI1020_XAXIS, 2000 );	  // 加速度		
                PCI1020.PCI1020_SetDec(hDevice, PCI1020.PCI1020_XAXIS, 4000 * 3);	  // 减速度	--- 当速度快的时候，需要增加减速度，否则会冲过限位点	
                PCI1020.PCI1020_SetSV(hDevice, PCI1020.PCI1020_XAXIS, 1000);	  // 初始速度							 
                PCI1020.PCI1020_SetV(hDevice, PCI1020.PCI1020_XAXIS, 1000);	  // 驱动速度(高速原点搜寻速度)				
                PCI1020.PCI1020_SetHV(hDevice, PCI1020.PCI1020_XAXIS, 200);    // 低速原点搜寻速度
                //PCI1020.PCI1020_SetP(hDevice, AxisNum, 800); // 定长脉冲数
                ////PCI1020.PCI1020_SetP(hDevice, PCI1020.PCI1020_XAXIS, 350000); // 定长脉冲数
                PCI1020.PCI1020_PulseOutMode(hDevice, PCI1020.PCI1020_XAXIS, PCI1020.PCI1020_CPDIR);  //设置脉冲方式
                //PCI1020.PCI1020_StartAutoHomeSearch(		// 启动自动原点搜寻
                //                 hDevice,			// 设备句柄		
                //                 PCI1020.PCI1020_XAXIS);
            }
            catch{}
        }


        public void AutoHomeSearch_AxisY(object obj)
        {
            try
            {

                PCI1020.PCI1020_SetInEnable(			// 设置自动原点搜寻第一、第二、第三步外部触发信号IN0-2的有效电平
                            hDevice,			// 设备号
                            PCI1020.PCI1020_YAXIS,			// 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)	
                            1,					// 停止号
                            0);
                //for (int i = 0; i < 4; i++)
                //{
                //    PCI1020.PCI1020_SetInEnable(			// 设置自动原点搜寻第一、第二、第三步外部触发信号IN0-2的有效电平
                //                hDevice,			// 设备号
                //                PCI1020.PCI1020_YAXIS,			// 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)	
                //                i,					// 停止号
                //                0);					// 有效电平
                //}
                m_ExpMode_Para.FE0 = 1;                  // 设置IN0，1滤波器有效
                m_ExpMode_Para.FE1 = 1;					// 设置IN2滤波器有效
                m_ExpMode_Para.FE4 = 1;					// 设置IN3滤波器有效
                PCI1020.PCI1020_ExtMode(				// 设置其他模式
                            hDevice,			// 设备句柄
                            PCI1020.PCI1020_YAXIS,		// 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
                            ref m_ExpMode_Para);			// 其他参数结构体指针
                m_Para_AutoHomeSearch_Y.LIMIT = 0;				// 1：利用硬件限位信号(nLMTP或nLMPM)进行原点搜寻 0：无效
                m_Para_AutoHomeSearch_Y.SAND = 0;				// 1：原点信号和Z相信号有效时停止第三步操作 0：无效
                //m_Para_AutoHomeSearch_Y.PCLR = 1;             //  回原地后计数器复位
                m_Para_AutoHomeSearch_Y.ST4E = 0;					// 1：第四步使能 0：无效
                m_Para_AutoHomeSearch_Y.ST4D = 0;					// 第四步的搜寻运转方向 0：正方向  1：负方向
                m_Para_AutoHomeSearch_Y.ST3E = 0;					// 1：第三步使能 0：无效
                m_Para_AutoHomeSearch_Y.ST3D = 0;					// 第三步的搜寻运转方向 0：正方向  1：负方向
                m_Para_AutoHomeSearch_Y.ST2E = 1;					// 1：第二步使能 0：无效
                m_Para_AutoHomeSearch_Y.ST2D = 1;					// 第二步的搜寻运转方向 0：正方向  1：负方向
                m_Para_AutoHomeSearch_Y.ST1E = 1;					// 1：第一步使能 0：无效
                m_Para_AutoHomeSearch_Y.ST1D = 1;			    	// 第一步的搜寻运转方向 0：正方向  1：负方向  

                //m_Para_AutoHomeSearch.HMINT = 1;
                PCI1020.PCI1020_SetAutoHomeSearch(		// 设置自动原点搜寻参数
                        hDevice,			// 设备句柄
                        PCI1020.PCI1020_YAXIS,		// 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)
                        ref m_Para_AutoHomeSearch_Y);
                PCI1020.PCI1020_SetR(hDevice, PCI1020.PCI1020_YAXIS, 1 * 1);     // 倍率
                PCI1020.PCI1020_SetA(hDevice, PCI1020.PCI1020_YAXIS, 2000);	  // 加速度		
                PCI1020.PCI1020_SetDec(hDevice, PCI1020.PCI1020_YAXIS, 4000 * 3);	  // 减速度	--- 当速度快的时候，需要增加减速度，否则会冲过限位点	
                PCI1020.PCI1020_SetSV(hDevice, PCI1020.PCI1020_YAXIS, 200);	  // 初始速度							 
                PCI1020.PCI1020_SetV(hDevice, PCI1020.PCI1020_YAXIS, 1000);	  // 驱动速度(高速原点搜寻速度)				
                PCI1020.PCI1020_SetHV(hDevice, PCI1020.PCI1020_YAXIS, 200);    // 低速原点搜寻速度
                //PCI1020.PCI1020_SetP(hDevice, AxisNum, 800); // 定长脉冲数
                //PCI1020.PCI1020_SetP(hDevice, PCI1020.PCI1020_XAXIS, 350000); // 定长脉冲数
                PCI1020.PCI1020_PulseOutMode(hDevice, PCI1020.PCI1020_YAXIS, PCI1020.PCI1020_CPDIR);  //设置脉冲方式
                //PCI1020.PCI1020_StartAutoHomeSearch(		// 启动自动原点搜寻
                //                 hDevice,			// 设备句柄		
                //                 PCI1020.PCI1020_YAXIS);
            }
            catch { }
        }

        /*  手动回原点方式，废弃
        public void ManualHomeSearch_AxisX(object obj)
        {
            m_tag_inAutoHomeSearch_Xstep1 = m_tag_inAutoHomeSearch_Xstep2_waitStart = 
                m_tag_inAutoHomeSearch_Xstep2_waitEnd = false; 
            try
            {
                Thread.Sleep(50);
                m_waitMoveFinished_X.Reset();
                m_waitMoveFinished_X.WaitOne();

                //----------------第一次正向寻原点，快速--------------
                if (!AxisMoveParasSetting_X(1, PCI1020.PCI1020_LV
                    , 100, 3, 200, 480, 500, 10000, 1000, 1000))
                {
                    logWR.appendNewLogMessage("X轴第一次正向寻原点设置异常_ManualHomeSearch_AxisX: \r\n "
                        + "AxisMoveParasSetting_X(1, PCI1020.PCI1020_LV, 100, 3, 200, 1200, 500, 10000, 1000, 1000)");
                    m_Tag_ReturnSuccess_X &= false;
                    return;
                }
                if (!PCI1020.PCI1020_StartLVDV(hDevice, PCI1020.PCI1020_XAXIS))
                {
                    logWR.appendNewLogMessage("X轴第一次正向寻原点设置异常_PCI1020_StartLVDV: \r\n "
                        + "PCI1020_StartLVDV(hDevice, PCI1020.PCI1020_XAXIS)"); 
                    m_Tag_ReturnSuccess_X &= false;
                    return;
                }
                Thread.Sleep(10);
                m_tag_inAutoHomeSearch_Xstep1 = true;
                //等待感应到位
                do { Thread.Sleep(2); } 
                    while (m_tag_inAutoHomeSearch_Xstep1);
                //减速停止
                if (!PCI1020.PCI1020_DecStop(hDevice, PCI1020.PCI1020_XAXIS))
                {
                    logWR.appendNewLogMessage("X轴第一次正向寻原点减速停止异常_PCI1020_DecStop(hDevice, PCI1020.PCI1020_XAXIS) ");
                    //m_Tag_ReturnSuccess_X &= false;
                    //return;
                }
                Thread.Sleep(20);
                m_waitMoveFinished_X.Reset();
                m_waitMoveFinished_X.WaitOne();
                //----------------第二次反向寻原点，慢速--------------
                if(!AxisMoveParasSetting_X(0, PCI1020.PCI1020_LV
                    , 100, 2, 200, 1200, 200, 10000, 1000, 1000))
                {
                    logWR.appendNewLogMessage("第二次反向寻原点设置异常_ManualHomeSearch_AxisX: \r\n "
                        + "AxisMoveParasSetting_X(1, PCI1020.PCI1020_LV, 100, 2, 200, 1200, 500, 10000, 1000, 1000)");
                    m_Tag_ReturnSuccess_X &= false;
                    return;
                }
                Thread.Sleep(20);
                //反向等待感应灯灭, 如果当时是灭的，就不需要这一步了
                if (RR3.XIN0 == 1)
                {
                    if (!PCI1020.PCI1020_StartLVDV(hDevice, PCI1020.PCI1020_XAXIS))
                    {
                        logWR.appendNewLogMessage("X轴第二次反向寻原点等待灯灭异常PCI1020_StartLVDV(hDevice, PCI1020.PCI1020_XAXIS)");
                        m_Tag_ReturnSuccess_X &= false;
                        return;
                    }
                    Thread.Sleep(10);
                    m_tag_inAutoHomeSearch_Xstep2_waitStart = true;
                    do { Thread.Sleep(2); }
                        while (m_tag_inAutoHomeSearch_Xstep2_waitStart);
                }
                else
                {
                    if (!PCI1020.PCI1020_StartLVDV(hDevice, PCI1020.PCI1020_XAXIS))
                    {
                        logWR.appendNewLogMessage("X轴第二次反向寻原点等待灯灭异常PCI1020_StartLVDV(hDevice, PCI1020.PCI1020_XAXIS)");
                        m_Tag_ReturnSuccess_X &= false;
                        return;
                    }
                    Thread.Sleep(10);
                }
                //反向开始等待感应灯亮(IN0 = 1)，表示第二步结束
                m_tag_inAutoHomeSearch_Xstep2_waitEnd = true;
                do { Thread.Sleep(2); }
                    while (m_tag_inAutoHomeSearch_Xstep2_waitEnd);
                //立即停止
                if (!PCI1020.PCI1020_InstStop(hDevice, PCI1020.PCI1020_XAXIS))
                {
                    logWR.appendNewLogMessage("X轴第二次反向寻原点执行立即停止异常：PCI1020_InstStop(hDevice, PCI1020.PCI1020_XAXIS)");
                    m_Tag_ReturnSuccess_X &= false;
                    return;
                }
                m_waitMoveFinished_X.Reset();
                m_waitMoveFinished_X.WaitOne();
                Thread.Sleep(200);
                m_Tag_ReturnFinished_X = true;
                m_Tag_ReturnSuccess_X = true;
            }
            catch { }
        }

        public void ManualHomeSearch_AxisY(object obj)
        {
            m_tag_inAutoHomeSearch_Ystep1 = m_tag_inAutoHomeSearch_Ystep2_waitStart
            = m_tag_inAutoHomeSearch_Ystep2_waitEnd = false;
            try
            {
                Thread.Sleep(50);
                m_waitMoveFinished_Y.Reset();
                m_waitMoveFinished_Y.WaitOne();

                //----------------第一次反向寻原点，快速--------------
                if (!AxisMoveParasSetting_Y(0, PCI1020.PCI1020_LV
                    , 100, 3, 200, 480, 500, 8000, 10000, 10000))
                {
                    logWR.appendNewLogMessage("Y轴第一次正向寻原点设置异常_ManualHomeSearch_AxisY: \r\n "
                        + "AxisMoveParasSetting_Y(1, PCI1020.PCI1020_LV, 100, 3, 200, 1200, 500, 10000, 1000, 1000)");
                    m_Tag_ReturnSuccess_Y &= false;
                    return;
                }
                if (!PCI1020.PCI1020_StartLVDV(hDevice, PCI1020.PCI1020_YAXIS))
                {
                    logWR.appendNewLogMessage("Y轴第一次正向寻原点设置异常_PCI1020_StartLVDV: \r\n "
                        + "PCI1020_StartLVDV(hDevice, PCI1020.PCI1020_YAXIS)");
                    m_Tag_ReturnSuccess_Y &= false;
                    return;
                }
                Thread.Sleep(10);
                //等待感应到位
                m_tag_inAutoHomeSearch_Ystep1 = true;
                do { Thread.Sleep(2); }
                while (m_tag_inAutoHomeSearch_Ystep1);
                //减速停止
                if (!PCI1020.PCI1020_DecStop(hDevice, PCI1020.PCI1020_YAXIS))
                {
                    logWR.appendNewLogMessage("Y轴第一次正向寻原点减速停止异常_PCI1020_DecStop(hDevice, PCI1020.PCI1020_YAXIS) ");
                    //m_Tag_ReturnSuccess_Y &= false;
                    //return;
                }
                Thread.Sleep(10);
                m_waitMoveFinished_Y.Reset();
                m_waitMoveFinished_Y.WaitOne();
                //----------------第二次反向寻原点，慢速--------------
                AxisMoveParasSetting_Y(1, PCI1020.PCI1020_LV
                    , 100, 2, 200, 200, 200, 10000, 1000, 1000);
                //反向等待感应灯灭, 如果当时是灭的，就不需要这一步了
                if (RR3.YIN0 == 1)
                {
                    if (!PCI1020.PCI1020_StartLVDV(hDevice, PCI1020.PCI1020_YAXIS))
                    {
                        logWR.appendNewLogMessage("Y轴第二次反向寻原点等待灯灭异常PCI1020_StartLVDV(hDevice, PCI1020.PCI1020_YAXIS)");
                        m_Tag_ReturnSuccess_Y &= false;
                        return;
                    }
                    Thread.Sleep(10);
                    m_tag_inAutoHomeSearch_Ystep2_waitStart = true;
                    do { Thread.Sleep(2); }
                     while (m_tag_inAutoHomeSearch_Ystep2_waitStart);
                }
                else
                {
                    if (!PCI1020.PCI1020_StartLVDV(hDevice, PCI1020.PCI1020_YAXIS))
                    {
                        logWR.appendNewLogMessage("Y轴第二次反向寻原点等待灯灭异常PCI1020_StartLVDV(hDevice, PCI1020.PCI1020_YAXIS)");
                        m_Tag_ReturnSuccess_Y &= false;
                        return;
                    }
                    Thread.Sleep(10);
                }
                //反向开始等待感应灯亮(IN0 = 1)，表示第二步结束
                m_tag_inAutoHomeSearch_Ystep2_waitEnd = true;
                do { Thread.Sleep(2); }
                    while (m_tag_inAutoHomeSearch_Ystep2_waitEnd);
                //立即停止
                if (!PCI1020.PCI1020_InstStop(hDevice, PCI1020.PCI1020_YAXIS))
                {
                    logWR.appendNewLogMessage("Y轴第二次反向寻原点执行立即停止异常：PCI1020_InstStop(hDevice, PCI1020.PCI1020_YAXIS)");
                    m_Tag_ReturnSuccess_Y &= false;
                    return;
                }
                m_waitMoveFinished_Y.Reset();
                m_waitMoveFinished_Y.WaitOne();
                Thread.Sleep(200);
                m_Tag_ReturnFinished_Y = true;
                m_Tag_ReturnSuccess_Y = true;
            }
            catch { }
        }
        */
        #endregion

        #region 软件限位
        #endregion

        #region I/0控制
        private void ALLIOInit()
        {
            try
            {
                m_IO_Para_X.OUT0 = m_IO_Para_X.OUT1 = m_IO_Para_X.OUT2 = m_IO_Para_X.OUT3 
                    = m_IO_Para_X.OUT4 = m_IO_Para_X.OUT5 = m_IO_Para_X.OUT6 = m_IO_Para_X.OUT7 = 1;   //三色灯控制，  0有效
                m_IO_Para_Y.OUT0 = m_IO_Para_Y.OUT1 = m_IO_Para_Y.OUT2 = m_IO_Para_Y.OUT3
                    = m_IO_Para_Y.OUT4 = m_IO_Para_Y.OUT5 = m_IO_Para_Y.OUT6 = m_IO_Para_Y.OUT7 = 1;   //用作伺服使能，0有效
                m_IO_Para_Z.OUT0 = m_IO_Para_Z.OUT1 = m_IO_Para_Z.OUT2 = m_IO_Para_Z.OUT3
                    = m_IO_Para_Z.OUT4 = m_IO_Para_Z.OUT5 = m_IO_Para_Z.OUT6 = m_IO_Para_Z.OUT7 = 0;   //用作导轨控制，0有效
                m_IO_Para_U.OUT0 = m_IO_Para_U.OUT1 = m_IO_Para_U.OUT2 = m_IO_Para_U.OUT3 
                    = m_IO_Para_U.OUT4 = m_IO_Para_U.OUT5 = m_IO_Para_U.OUT6 = m_IO_Para_U.OUT7 = 0;   //用作拍照(继电器)，1有效
                PCI1020.PCI1020_SetDeviceDO(			// 开关量输出
                                 hDevice,	 		// 设备号
                                 PCI1020.PCI1020_XAXIS,		// 轴号
                                 ref m_IO_Para_X);
                PCI1020.PCI1020_SetDeviceDO(			// 开关量输出
                                 hDevice,	 		// 设备号
                                 PCI1020.PCI1020_YAXIS,		// 轴号
                                 ref m_IO_Para_Y);
                PCI1020.PCI1020_SetDeviceDO(			// 开关量输出
                                 hDevice,	 		// 设备号
                                 PCI1020.PCI1020_ZAXIS,		// 轴号
                                 ref m_IO_Para_Z);
                PCI1020.PCI1020_SetDeviceDO(			// 开关量输出
                                 hDevice,	 		// 设备号
                                 PCI1020.PCI1020_UAXIS,		// 轴号
                                 ref m_IO_Para_U);
                LedLight_Switch(1);
            }
            catch(Exception e) 
            {
                logWR.appendNewLogMessage("ALLIOInit_Low Error: \r\n" + e.ToString());
            }
        }

        public void CaptureTrigger()
        {
            //m_inParaSetting = true;
            try
            {
                m_IO_Para_U.OUT1 = 1;
                PCI1020.PCI1020_SetDeviceDO(			// 开关量输出
                            hDevice,	 		// 设备号
                            PCI1020.PCI1020_UAXIS,		// 轴号
                            ref m_IO_Para_U);
                Thread.Sleep(1);
                m_IO_Para_U.OUT1 = 0;
                PCI1020.PCI1020_SetDeviceDO(			// 开关量输出
                            hDevice,	 		// 设备号
                            PCI1020.PCI1020_UAXIS,		// 轴号
                            ref m_IO_Para_U);
            }
            catch { }            
            //m_inParaSetting = false;
        }

        //载板第一段到位扫描完毕
        public void Stage1ZaibanPass()
        {
            //m_inParaSetting = true;
            try
            {
                m_IO_Para_Z.OUT1 = 1;
                PCI1020.PCI1020_SetDeviceDO(			// 开关量输出
                            hDevice,	 		// 设备号
                            PCI1020.PCI1020_ZAXIS,		// 轴号
                            ref m_IO_Para_Z);               
                Thread.Sleep(500);
                m_IO_Para_Z.OUT1 = 0;
                PCI1020.PCI1020_SetDeviceDO(			// 开关量输出
                            hDevice,	 		// 设备号
                            PCI1020.PCI1020_ZAXIS,		// 轴号
                            ref m_IO_Para_Z);
            }
            catch { }
        }
        
        public void Stage2ZaibanPass()
        {
            //m_inParaSetting = true;
            try
            {
                Thread.Sleep(200);
                m_IO_Para_Z.OUT2 = 1;
                PCI1020.PCI1020_SetDeviceDO(			// 开关量输出
                            hDevice,	 		// 设备号
                            PCI1020.PCI1020_ZAXIS,		// 轴号
                            ref m_IO_Para_Z);
                Thread.Sleep(500);
                m_IO_Para_Z.OUT2 = 0;
                PCI1020.PCI1020_SetDeviceDO(			// 开关量输出
                            hDevice,	 		// 设备号
                            PCI1020.PCI1020_ZAXIS,		// 轴号
                            ref m_IO_Para_Z);
            }
            catch { }
            //m_inParaSetting = false;
        }

        private void ServerX_ON()
        {
            m_inParaSetting = true;
            try
            {
                m_IO_Para_Y.OUT3 = 0;
                PCI1020.PCI1020_SetDeviceDO(			// 开关量输出
                            hDevice,	 		// 设备号
                            PCI1020.PCI1020_YAXIS,		// 轴号
                            ref m_IO_Para_Y);
            }
            catch { }
            m_inParaSetting = false;
        }

        private void ServerX_OFF()
        {
            m_inParaSetting = true;
            try
            {
                m_IO_Para_Y.OUT3 = 1;
                PCI1020.PCI1020_SetDeviceDO(			// 开关量输出
                            hDevice,	 		// 设备号
                            PCI1020.PCI1020_YAXIS,		// 轴号
                            ref m_IO_Para_Y);
            }
            catch { }
            m_inParaSetting = false;
        }

        private void ServerY_ON()
        {
            m_inParaSetting = true;
            try
            {
                m_IO_Para_Y.OUT2 = 0;
                PCI1020.PCI1020_SetDeviceDO(			// 开关量输出
                            hDevice,	 		// 设备号
                            PCI1020.PCI1020_YAXIS,		// 轴号
                            ref m_IO_Para_Y);
            }
            catch { }
            m_inParaSetting = false;
        }

        private void ServerY_OFF()
        {
            m_inParaSetting = true;
            try
            {
                m_IO_Para_Y.OUT2 = 1;
                PCI1020.PCI1020_SetDeviceDO(			// 开关量输出
                            hDevice,	 		// 设备号
                            PCI1020.PCI1020_YAXIS,		// 轴号
                            ref m_IO_Para_Y);
            }
            catch { }
            m_inParaSetting = false;
        }

        //0: 亮  1：灭
        public void LedLight_Red(uint light)
        {
            try
            {
                m_IO_Para_X.OUT3 = light;
                PCI1020.PCI1020_SetDeviceDO(			// 开关量输出
                            hDevice,	 		// 设备号
                            PCI1020.PCI1020_XAXIS,		// 轴号
                            ref m_IO_Para_X);
            }
            catch { }
        }

        //0: 亮  1：灭
        public void LedLight_Green(uint light)
        {
            try
            {
                m_IO_Para_X.OUT2 = light;
                PCI1020.PCI1020_SetDeviceDO(			// 开关量输出
                            hDevice,	 		// 设备号
                            PCI1020.PCI1020_XAXIS,		// 轴号
                            ref m_IO_Para_X);
            }
            catch { }
        }

        //0: 亮  1：灭
        public void LedLight_Yellow(uint light)
        {
            try
            {
                m_IO_Para_X.OUT1 = light;
                PCI1020.PCI1020_SetDeviceDO(			// 开关量输出
                            hDevice,	 		// 设备号
                            PCI1020.PCI1020_XAXIS,		// 轴号
                            ref m_IO_Para_X);
            }
            catch { }
        }

        //0: 亮  1：灭
        public void LedLight_Beep(uint beep)
        {
            try
            {
                m_IO_Para_Y.OUT1 = beep;
                PCI1020.PCI1020_SetDeviceDO(			// 开关量输出
                            hDevice,	 		// 设备号
                            PCI1020.PCI1020_YAXIS,		// 轴号
                            ref m_IO_Para_Y);
            }
            catch { }
        }

        //三色灯总开关  0: 有效  1：无效
        public void LedLight_Switch(uint valid)
        {
            try
            {
                m_IO_Para_U.OUT2 = valid;
                m_IO_Para_U.OUT3 = valid;
                PCI1020.PCI1020_SetDeviceDO(			// 开关量输出
                            hDevice,	 		// 设备号
                            PCI1020.PCI1020_UAXIS,		// 轴号
                            ref m_IO_Para_U);
            }
            catch { }
        }
        #endregion

        private void timer_refresh_Tick(object sender, EventArgs e)
        {
            updateStatusValue();
        }

        public void ClearEPLPValue()
        {
            PCI1020.PCI1020_SetLP(hDevice, PCI1020.PCI1020_XAXIS, 0);		// 设置逻辑位置计数器
            PCI1020.PCI1020_SetLP(hDevice, PCI1020.PCI1020_YAXIS, 0);		// 设置逻辑位置计数器
            PCI1020.PCI1020_SetLP(hDevice, PCI1020.PCI1020_ZAXIS, 0);		// 设置逻辑位置计数器
            PCI1020.PCI1020_SetEP(hDevice, PCI1020.PCI1020_XAXIS, 0);		// 设置实位计数器 	
            PCI1020.PCI1020_SetEP(hDevice, PCI1020.PCI1020_YAXIS, 0);		// 设置实位计数器 	
            PCI1020.PCI1020_SetEP(hDevice, PCI1020.PCI1020_ZAXIS, 0);		// 设置实位计数器 	
        }

        public void TagReset()
        {
            //m_tag_boardArrived = false;
            //m_tag_SheetScan = false;
        }
    }
}
