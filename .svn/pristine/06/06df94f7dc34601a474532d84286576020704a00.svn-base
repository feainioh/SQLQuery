using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace PylonLiveView
{
    class PCI1020_new
    {

        /**************************************************************************
        // 硬件设置参数
        **************************************************************************/


        // 公用参数 
        public struct PCI1020_PARA_DataList
        {	 
	        public Int32 Multiple;				// 倍率 (1~500)
	        public Int32 StartSpeed;			// 初始速度(1~8000)
	        public Int32 DriveSpeed;			// 驱动速度(1~8000)
	        public Int32 Acceleration;			// 加速度(125~1000000)
	        public Int32 Deceleration;			// 减速度(125~1000000)
	        public Int32 AccIncRate;			// 加速度变化率(954~62500000)
	        public Int32 DecIncRate;			// 减速度变化率(954~62500000)
        };

        // 直线和S曲线参数
        public struct PCI1020_PARA_LCData
        {
	        public Int32 AxisNum;				// 轴号 (X轴 | Y轴 | X、Y轴)
	        public Int32 LV_DV;					// 驱动方式  (连续 | 定长 )
	        public Int32 DecMode;				// 减速方式  (自动减速 | 手动减速)	
	        public Int32 PulseMode;				// 脉冲方式 (CW/CCW方式 | CP/DIR方式)
	        public Int32 Line_Curve;			// 运动方式	(直线 | 曲线)
	        public Int32 Direction;				// 运动方向 (正方向 | 反方向)
	        public Int32 nPulseNum;		    	// 定量输出脉冲数(0~268435455)
        } ;

        // 插补轴
        public struct PCI1020_PARA_InterpolationAxis
        {	
	        public Int32 Axis1;					// 主轴
	        public Int32 Axis2;					// 第二轴
	        public Int32 Axis3;					// 第三轴
        };

        // 直线插补和固定线速度直线插补参数
        public struct PCI1020_PARA_LineData	
        {	
	        public Int32 Line_Curve;			// 运动方式	(直线 | 曲线)
	        public Int32 ConstantSpeed;			// 固定线速度 (不固定线速度 | 固定线速度)
	        public Int32 n1AxisPulseNum;		// 主轴终点脉冲数 (-8388608~8388607)
	        public Int32 n2AxisPulseNum;		// 第二轴轴终点脉冲数 (-8388608~8388607)
	        public Int32 n3AxisPulseNum;		// 第三轴轴终点脉冲数 (-8388608~8388607)		
        };

        // 正反方向圆弧插补参数
        public struct PCI1020_PARA_CircleData	
        {
	        public Int32 ConstantSpeed;			// 固定线速度 (不固定线速度 | 固定线速度)
	        public Int32 Direction;				// 运动方向 (正方向 | 反方向)
	        public Int32 Center1;				// 主轴圆心坐标(脉冲数-8388608~8388607)
            public Int32 Center2;				// 第二轴轴圆心坐标(脉冲数-8388608~8388607)
	        public Int32 Pulse1;				// 主轴终点坐标(脉冲数-8388608~8388607)	
	        public Int32 Pulse2;				// 第二轴轴终点坐标(脉冲数-8388608~8388607)
        };

        /***************************************************************/
        // 轴号
        public const Int32 PCI1020_XAXIS			= 0X0;				// X轴
        public const Int32 PCI1020_YAXIS			= 0X1;				// Y轴
        public const Int32 PCI1020_ZAXIS			= 0X2;				// X、Y轴
        public const Int32 PCI1020_UAXIS			= 0X3;				// X、Y轴
        public const Int32 PCI1020_ALLAXIS			= 0XF;				// X、Y、Z、U轴

        /***************************************************************/
        // 驱动方式
        public const Int32 PCI1020_DV				= 0X0;				// 定长驱动
        public const Int32 PCI1020_LV				= 0X1;				// 连续驱动

        /***************************************************************/
        // 减速方式
        public const Int32 PCI1020_AUTO			= 0X0;				// 自动减速
        public const Int32 PCI1020_HAND			= 0X1;				// 手动减速

        /***************************************************************/
        // 脉冲方式
        public const Int32 PCI1020_CWCCW			= 0X0;				// CW/CCW方式 
        public const Int32 PCI1020_CPDIR 			= 0X1;				// CP/DIR方式

        /***************************************************************/
        // 运动方式
        public const Int32 PCI1020_LINE			= 0X0;				// 直线运动
        public const Int32 PCI1020_CURVE			= 0X1;				// S曲线运动

        /***************************************************************/
        // 运动方向
        public const Int32 PCI1020_MDIRECTION		= 0X0;				// 反方向
        public const Int32 PCI1020_PDIRECTION		= 0X1;				// 正方向

        /***************************************************************/
        //固定线速度
        public const Int32 PCI1020_NOCONSTAND		= 0X0;				// 不固定线速度
        public const Int32 PCI1020_CONSTAND		= 0X1;				// 固定线速度

        /***************************************************************/
        // 软件限位的逻辑实位计数器选择和设置外部越限信号的停止方式和设置外部停止信号的停止号选择
        /***************************************************************/
        // 计数器类别
        public const Int32 PCI1020_LOGIC			= 0X0;				// 逻辑位计数器
        public const Int32 PCI1020_FACT			= 0X1;				// 实位计数器

        /***************************************************************/
        // 外部停止信号
        public const Int32 PCI1020_IN0				= 0X0;				// 停止信号0
        public const Int32 PCI1020_IN1				= 0X1;				// 停止信号1
        public const Int32 PCI1020_IN2				= 0X2;				// 停止信号2
        public const Int32 PCI1020_IN3				= 0X3;				// 停止信号3

        /***************************************************************/
        // 停止方式
        public const Int32 PCI1020_SUDDENSTOP		= 0X0;				// 立即停止
        public const Int32 PCI1020_DECSTOP			= 0X1;				// 减速停止

        /********************************************************************/
        // 输出切换
        public const Int32 PCI1020_GENERALOUT		= 0X0;				// 通用输出
        public const Int32 PCI1020_STATUSOUT		= 0X1;				// 状态输出

        /********************************************************************/
        public const Int32 PCI1020_ERROR			= 0XFF;			// 错误

        /****************************************************************/
        // 设置中断位使能
        public struct PCI1020_PARA_Interrupt      
        {
	        public UInt32 PULSE;			// 1：中断使能，中断信号由各输出的脉冲上升沿触发 0：禁止中断
	        public UInt32 PBCM;			// 1：中断使能，当逻辑/实际位置计数器的值大于等于COMP-寄存器的值时发中断信号 0：禁止中断
	        public UInt32 PSCM;			// 1：中断使能，当逻辑/实际位置计数器的值小于COMP-寄存器的值时发中断信号 0：禁止中断
	        public UInt32 PSCP;			// 1：中断使能，当逻辑/实际位置计数器的值小于COMP+寄存器的值时发中断信号 0：禁止中断
	        public UInt32 PBCP;			// 1：中断使能，当逻辑/实际位置计数器的值大于等于COMP+寄存器的值发中断信号 0：禁止中断
	        public UInt32 CDEC;			// 1：中断使能，在加/减速驱动中，当开始减速时发中断信号 0：禁止中断
	        public UInt32 CSTA;			// 1：中断使能，在加/减速驱动中，当开始定速时发中断信号 0：禁止中断
	        public UInt32 DEND;			// 1：中断使能，在驱动结束时发中断信号 0：禁止中断
	        public UInt32 CIINT;			// 1：中断使能，当允许写入下一个节点命令时产生中断 0：禁止中断
	        public UInt32 BPINT;			// 1：中断使能，当位插补堆栈计数器的值由2变为1时产生中断 0：禁止中断

        };

        // 设置同步参数
        public struct PCI1020_PARA_SynchronActionOwnAxis    
        {
	        public UInt32 PBCP;			// 1：当逻辑/实位计数器的值大于等于COMP+寄存器时，启动同步动作 0：无效
	        public UInt32 PSCP;			// 1：当逻辑/实位计数器的值小于COMP+寄存器时，启动同步动作 0：无效
	        public UInt32 PSCM;			// 1：当逻辑/实位计数器的值小于COMP-寄存器时，启动同步动作 0：无效
	        public UInt32 PBCM;			// 1：当逻辑/实位计数器的值大于等于COMP-寄存器时，启动同步动作 0：无效
	        public UInt32 DSTA;			// 1：当驱动开始时，启动同步动作 0：无效
	        public UInt32 DEND;			// 1：当驱动结束时，启动同步动作 0：无效
	        public UInt32 IN3LH;			// 1：当IN3出现上升沿时，启动同步动作 0：无效
	        public UInt32 IN3HL;			// 1：当IN3出现下降沿时，启动同步动作 0：无效
	        public UInt32 LPRD;			// 1：当读逻辑位置计数器时，启动同步动作 0：无效
	        public UInt32 CMD;			// 1：当写入同步操作命令时，启动同步轴的同步动作 0：无效
	        public UInt32 AXIS1;			// 1：指定与自己轴同步的轴  0：没有指定
	        public UInt32 AXIS2;			// 1：指定与自己轴同步的轴  0：没有指定
	        public UInt32 AXIS3;			// 1：指定与自己轴同步的轴  0：没有指定
						        // 当前轴	AXIS3		AXIS2		AXIS1  
						        // X轴		 U轴		 Z轴		 Y轴
						        // Y轴		 X轴		 U轴		 Z轴
						        // Z轴		 Y轴		 X轴		 U轴
						        // U轴		 Z轴		 Y轴		 X轴
        };

        // 设置同步参数
        public struct PCI1020_PARA_SynchronActionOtherAxis    
        {
	        public UInt32 FDRVP;			// 1：启动正方向定长驱动 0：无效
	        public UInt32 FDRVM;			// 1：启动反方向定长驱动 0：无效
	        public UInt32 CDRVP;			// 1：启动正方向连续驱动 0：无效
	        public UInt32 CDRVM;			// 1：启动反方向连续驱动 0：无效
	        public UInt32 SSTOP;			// 1：减速停止 0：无效
	        public UInt32 ISTOP;			// 1：立即停止 0：无效
	        public UInt32 LPSAV;			// 1：把当前逻辑寄存器LP值保存到同步缓冲寄存器BR 0：无效
	        public UInt32 EPSAV;			// 1：把当前实位寄存器EP值保存到同步缓冲寄存器BR 0：无效
	        public UInt32 LPSET;			// 1：把WR6和WR7的值设定到逻辑寄存器LP中 0：无效
	        public UInt32 EPSET;			// 1：把WR6和WR7的值设定到逻辑寄存器EP中 0：无效 
	        public UInt32 OPSET;			// 1：把WR6和WR7的值设定到逻辑寄存器LP中 0：无效
	        public UInt32 VLSET;			// 1：把WR6的值设定为驱动速度V 0：无效
	        public UInt32 OUTN;			// 1：用nDCC引脚输出同步脉冲  0：nDCC输出同步脉冲无效？？？
	        public UInt32 INTN;			// 1：产生中断  0：不产生中断
        };

        // 设置其他参数
        public struct PCI1020_PARA_ExpMode
        {
	        public UInt32 EPCLR;			// 1：当IN2触发有效时清除实位计数器 0：无效
	        public UInt32 FE0;			// 1：外部输入信号EMGN、nLMTP、nLMTM、nIN0、nIN1滤波器有效 0：无效
	        public UInt32 FE1;			// 1：外部输入信号nIN2滤波器有效 0：无效
	        public UInt32 FE2;			// 1：外部输入信号nALARM、nINPOS滤波器有效 0：无效
	        public UInt32 FE3;			// 1：外部输入信号nEXPP、nEXPM、EXPLS滤波器有效 0：无效
	        public UInt32 FE4;			// 1：外部输入信号nIN3滤波器有效 0：无效
	        public UInt32 FL0;			// 滤波器的时间常数 
						        //	FL2 FL1 FL0	 滤波器时间常数	 信号延迟
	        public UInt32 FL1;			//		0：			1.75μS			2μS
	        public UInt32 FL2;			//		1：			224μS			256μS
						        //		2：			448μS			512μS
						        //		3：			896μS			1.024mμS
						        //		4：			1.792mS			2.048mS
						        //		5：			3.584mS			4.096mS
						        //		6：			7.168mS			8.012mS
						        //		7：			14.336mS		16.384mS
        };

        // 偏离计数器清除设置
        public struct PCI1020_PARA_DCC
        {
	        public UInt32 DCCE;			// 1：使能偏离计数器清除输出 0：无效
	        public UInt32 DCCL;			// 1：偏离计数器清除输出的逻辑电平为低电平  0：偏离计数器清除输出的逻辑电平为高电平
	        public UInt32 DCCW0;			// 用来指定偏离计数器清除输出的脉冲宽度
	        public UInt32 DCCW1;			//  DCCW2 DCCW1 DCCW0 清除的脉冲宽度(μS)
	        public UInt32 DCCW2;			// 	  0		0	  0		  10         	  0	 0  0		1000
						        // 	  0		0	  1		  20			  0	 0  0		2000
						        // 	  0		1	  0		  100			  0	 0  0		10000
						        // 	  0		1	  1		  200			  0	 0  0		20000
        };

        // 自动原点搜寻设置
        public struct PCI1020_PARA_AutoHomeSearch
        {
	        public UInt32 ST1E;			// 1：第一步使能 0：无效
	        public UInt32 ST1D;			// 第一步的搜寻运转方向 0：正方向  1：负方向
	        public UInt32 ST2E;			// 1：第二步使能 0：无效
	        public UInt32 ST2D;			// 第二步的搜寻运转方向 0：正方向  1：负方向
	        public UInt32 ST3E;			// 1：第三步使能 0：无效
	        public UInt32 ST3D;			// 第三步的搜寻运转方向 0：正方向  1：负方向
	        public UInt32 ST4E;			// 1：第四步使能 0：无效
	        public UInt32 ST4D;			// 第四步的搜寻运转方向 0：正方向  1：负方向
	        public UInt32 PCLR;			// 1：第四步结束时清除逻辑计数器和实位计数器 0：无效
	        public UInt32 SAND;			// 1：原点信号和Z相信号有效时停止第三步操作 0：无效 
	        public UInt32 LIMIT;			// 1：利用硬件限位信号(nLMTP或nLMPM)进行原点搜寻 0：无效
	        public UInt32 HMINT;			// 1：当自动原点搜索结束时产生中断 0：无效
        } ;

        // IO输出
        public struct PCI1020_PARA_DO      
        {
	        public UInt32 OUT0;			// 输出0
	        public UInt32 OUT1;			// 输出1
	        public UInt32 OUT2;			// 输出2
	        public UInt32 OUT3;			// 输出3
	        public UInt32 OUT4;			// 输出4
	        public UInt32 OUT5;			// 输出5
	        public UInt32 OUT6;			// 输出6
	        public UInt32 OUT7;			// 输出7
        };

        // 状态寄存器
        public struct PCI1020_PARA_RR0      
        {
	        public UInt32 XDRV;			// X轴的驱动状态  1：正在输出脉冲 0：停止驱动
	        public UInt32 YDRV;			// Y轴的驱动状态  1：正在输出脉冲 0：停止驱动
	        public UInt32 ZDRV;			// Z轴的驱动状态  1：正在输出脉冲 0：停止驱动
	        public UInt32 UDRV;			// U轴的驱动状态  1：正在输出脉冲 0：停止驱动
	        public UInt32 XERROR;		// X轴的出错状态  X轴的RR2寄存器的任何一位为1，此位就为1
	        public UInt32 YERROR;		// Y轴的出错状态  Y轴的RR2寄存器的任何一位为1，此位就为1
	        public UInt32 ZERROR;		// Z轴的出错状态  Z轴的RR2寄存器的任何一位为1，此位就为1
	        public UInt32 UERROR;		// U轴的出错状态  U轴的RR2寄存器的任何一位为1，此位就为1
	        public UInt32 IDRV;			// 插补驱动状态   1：正处于插补模式  0：未处于插补模式
	        public UInt32 CNEXT;			// 表示可以写入连续插补的下一个数据  1：可以写入 0：不可以写入
	                            // 当设置连续插补中断使能后，CNEXT为1表示产生了中断，在中断程序写入下一个插补命令后，该位清零并且中断信号回到高电平
	        public UInt32 ZONE0;			// ZONE2、ZONE1、ZONE0表示在圆弧插补驱动中所在的象限
	        public UInt32 ZONE1;			// 000 ：第0象限   001：第1象限  010：第2象限  011：第3象限
	        public UInt32 ZONE2;			// 100 ：第4象限   101：第5象限	 110：第6象限  111：第7象限
	        public UInt32 BPSC0;			// BPSC1、BPSC0表示在位插补驱动中堆栈计数器(SC)的数值
	        public UInt32 BPSC1;			// 00： 0   01：1   10： 2   11：3
						        // 设置位插补中断使能后，当SC的值由2变为1时，产生中断，
	                            // 当向位插补堆栈写入新的数据或调用PCI1020_ClearInterruptStatus，中断解除。
        };


        // 状态寄存器RR1，每一个轴都有RR1寄存器，读哪个要指定轴号
        public struct PCI1020_PARA_RR1    
        {
	        public UInt32 CMPP;			// 表示逻辑/实位计数器和COMP+寄存器的大小关系 1：逻辑/实位计数器≥COMP+ 0：逻辑/实位计数器＜COMP+
	        public UInt32 CMPM;			// 表示逻辑/实位计数器和COMP-寄存器的大小关系 1：逻辑/实位计数器＜COMP- 0：逻辑/实位计数器≥COMP-
	        public UInt32 ASND;			// 在加/减速驱动中加速时，为1
	        public UInt32 CNST;			// 在加/减速驱动中定速时，为1
	        public UInt32 DSND;			// 在加/减速驱动中减速时，为1
	        public UInt32 AASND;			// 在S曲线加/减速驱动中，加速度/减速度增加时，为1 
	        public UInt32 ACNST;			// 在S曲线加/减速驱动中，加速度/减速度不变时，为1 
	        public UInt32 ADSND;			// 在S曲线加/减速驱动中，加速度/减速度减少时，为1 
	        public UInt32 IN0;			// 外部停止信号IN0有效使驱动停止时，为1
	        public UInt32 IN1;			// 外部停止信号IN1有效使驱动停止时，为1
	        public UInt32 IN2;			// 外部停止信号IN2有效使驱动停止时，为1
	        public UInt32 IN3;			// 外部停止信号IN3有效使驱动停止时，为1
	        public UInt32 LMTP;			// 外部正方向限制信号(nLMTP)有效使驱动停止时，为1
	        public UInt32 LMTM;			// 外部反方向限制信号(nLMTM)有效使驱动停止时，为1
	        public UInt32 ALARM;			// 外部伺服马达报警信号(nALARM)有效使驱动停止时，为1
	        public UInt32 EMG;			// 外部紧急停止信号(EMGN)使驱动停止时，为1
        };


        // 状态寄存器RR2，每一个轴都有RR2寄存器，读哪个要指定轴号
        public struct PCI1020_PARA_RR2     
        {
	        public UInt32 SLMTP;			// 设置正方向软件限位后，在正方向驱动中，逻辑/实位计数器大于COMP+寄存器值时，为1
	        public UInt32 SLMTM;			// 设置反方向软件限位后，在反方向驱动中，逻辑/实位计数器小于COMP-寄存器值时，为1
	        public UInt32 HLMTP;			// 外部正方向限制信号(nLMTP)处于有效电平时，为1
	        public UInt32 HLMTM;			// 外部反方向限制信号(nLMTM)处于有效电平时，为1
	        public UInt32 ALARM;			// 外部伺服马达报警信号(nALARM)设置为有效并处于有效状态时，为1
	        public UInt32 EMG;			// 外部紧急停止信号处于低电平时，为1
	        public UInt32 HOME;			// 当Z相编码信号在自动搜寻原点出错时为1
	        public UInt32 HMST0;			// HMST0-4(HMST4-0)表示自动原点搜寻中执行的步数
	        public UInt32 HMST1;			//	 0：等待自动原点搜寻命令
	        public UInt32 HMST2;			//	 3：等待IN0信号在指定方向上有效	
	        public UInt32 HMST3;			//	 8、12、15：等待IN1信号在指定方向上有效
	        public UInt32 HMST4;			//	 20：IN2信号在指定方向上有效
						        //	 25：第四步
        };


        // 状态寄存器RR3
        public struct PCI1020_PARA_RR3      
        {
	        public UInt32 XIN0;			// 外部停止信号XIN0的电平状态 1：高电平 0：低电平
	        public UInt32 XIN1;			// 外部停止信号XIN1的电平状态 1：高电平 0：低电平
	        public UInt32 XIN2;			// 外部停止信号XIN2的电平状态 1：高电平 0：低电平
	        public UInt32 XIN3;			// 外部停止信号XIN3的电平状态 1：高电平 0：低电平
	        public UInt32 XEXPP;			// 外部正方向点动输入信号XEXPP的电平状态 1：高电平 0：低电平
	        public UInt32 XEXPM;			// 外部反方向点动输入信号XEXPM的电平状态 1：高电平 0：低电平
	        public UInt32 XINPOS;		// 外部伺服电机到位信号XINPOS的电平状态  1：高电平 0：低电平
	        public UInt32 XALARM;		// 外部伺服马达报警信号XALARM的电平状态  1：高电平 0：低电平
	        public UInt32 YIN0;			// 外部输入信号YIN0的电平状态  1：高电平 0：低电平
	        public UInt32 YIN1;			// 外部输入信号YIN1的电平状态  1：高电平 0：低电平
	        public UInt32 YIN2;			// 外部输入信号YIN2的电平状态  1：高电平 0：低电平
	        public UInt32 YIN3;			// 外部输入信号YIN3的电平状态  1：高电平 0：低电平
	        public UInt32 YEXPP;			// 外部正方向点动输入信号YEXPP的电平状态 1：高电平 0：低电平
	        public UInt32 YEXPM;			// 外部反方向点动输入信号YEXPM的电平状态 1：高电平 0：低电平
	        public UInt32 YINPOS;		// 外部伺服电机到位信号YINPOS的电平状态  1：高电平 0：低电平
	        public UInt32 YALARM;		// 外部伺服马达报警信号YALARM的电平状态  1：高电平 0：低电平
        };


        // 状态寄存器RR4 
        public struct PCI1020_PARA_RR4     
        {
	        public UInt32 ZIN0;			// 外部停止信号YIN0的电平状态 1：高电平 0：低电平
	        public UInt32 ZIN1;			// 外部停止信号YIN1的电平状态 1：高电平 0：低电平
	        public UInt32 ZIN2;			// 外部停止信号YIN2的电平状态 1：高电平 0：低电平
	        public UInt32 ZIN3;			// 外部停止信号YIN3的电平状态 1：高电平 0：低电平
	        public UInt32 ZEXPP;			// 外部正方向点动输入信号ZEXPP的电平状态 1：高电平 0：低电平
	        public UInt32 ZEXPM;			// 外部反方向点动输入信号ZEXPM的电平状态 1：高电平 0：低电平
	        public UInt32 ZINPOS;		// 外部伺服电机到位信号ZINPOS的电平状态  1：高电平 0：低电平
	        public UInt32 ZALARM;		// 外部伺服马达报警信号ZALARM的电平状态  1：高电平 0：低电平
	        public UInt32 UIN0;			// 外部停止信号UIN0的电平状态 1：高电平 0：低电平
	        public UInt32 UIN1;			// 外部停止信号UIN1的电平状态 1：高电平 0：低电平
	        public UInt32 UIN2;			// 外部停止信号UIN2的电平状态 1：高电平 0：低电平
	        public UInt32 UIN3;			// 外部停止信号UIN3的电平状态 1：高电平 0：低电平
	        public UInt32 UEXPP;			// 外部正方向点动输入信号UEXPP的电平状态 1：高电平 0：低电平
	        public UInt32 UEXPM;			// 外部反方向点动输入信号UEXPM的电平状态 1：高电平 0：低电平
	        public UInt32 UINPOS;		// 外部伺服电机到位信号UINPOS的电平状态  1：高电平 0：低电平
	        public UInt32 UALARM;		// 外部伺服马达报警信号UALARM的电平状态  1：高电平 0：低电平
        };

        // 状态寄存器RR5  当有中断产生时，相应的中断标志为1，中断输出信号为低电平
        // 当主CPU读了RR5寄存器的中断标志后，RR5的标志就为0，中断信号恢复到高电平
        public struct PCI1020_PARA_RR5     
        {
	        public UInt32 PULSE;			// 产生一个增量脉冲时为1
	        public UInt32 PBCM;			// 逻辑/实际位置计数器的值大于等于COMP-寄存器的值时为1
	        public UInt32 PSCM;			// 逻辑/实际位置计数器的值小于COMP-寄存器的值时为1
	        public UInt32 PSCP;			// 逻辑/实际位置计数器的值小于COMP+寄存器的值时为1
	        public UInt32 PBCP;			// 逻辑/实际位置计数器的值大于等于COMP+寄存器的值为1
	        public UInt32 CDEC;			// 在加/减速时，脉冲开始减速时为1
	        public UInt32 CSTA;			// 在加/减速时，开始定速时为1
	        public UInt32 DEND;			// 驱动结束时为1
	        public UInt32 HMEND;			// 自动原点搜索结束时为1
	        public UInt32 SYNC;			// 同步产生的中断
        };



        //######################## 设备对象管理函数 #################################
        // 适用于本设备的最基本操作	
                [DllImport("PCI1020_32.DLL")]
        public static extern IntPtr PCI1020_CreateDevice(       // 创建句柄
							        int DeviceID);           // 设备ID号
                
                [DllImport("PCI1020_32.DLL")]
        public static extern Int32 PCI1020_GetDeviceCount(        // 获得设备总数
							        IntPtr hDevice);		 // 设备句柄
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_ReleaseDevice(		 // 释放设备
							        IntPtr hDevice);		 // 设备句柄
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_Reset(				 // 软件复位
							        IntPtr hDevice);		 // 设备句柄
        //*******************************************************************
        // 设置电机的逻辑计数器、实际位置计数器、加速计数器偏移
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_PulseOutMode(         // 设置脉冲输出模式
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)  
							        Int32 Mode);				 // 模式
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetR(				 // 设置倍率(1-500)	
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)  
							        Int32 Data);				 // 倍率值(1-500)
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetA(				 // 设置加速度
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)     
							        Int32 Data);				 // 加速度 (125-1000000)
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetDec(				 // 设置减速度
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
							        Int32 Data);				 // 减速度值(125-1000,000)
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetAccIncRate(		 // 加速度变化率  
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)   
							        Int32 Data);				 // 数据 (954-62500000) 
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetDecIncRate(		 // 加速度变化率  
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
							        Int32 Data);				 // 数据 (954-62500000)
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetSV(				 // 设置初始速度(1-8000)
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)   
							        Int32 Data);				 // 速度值(1-8000)
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetV(				 // 设置驱动速度
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)     
							        Int32 Data);				 // 驱动速度值(1-8000)
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetHV(				 // 设置原点搜寻速度
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)	
							        Int32 Data);				 // 原点搜寻速度值(1-8000)
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetP(				 // 设置定长脉冲数
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
							        Int32 Data);			     // 定长脉冲数(0-268435455)
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetIP(				 // 设置插补终点脉冲数(-8388608-+8388607)
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
							        Int32 Data);				 // 插补终点脉冲数(-8388608-+8388607)
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetC(                 // 设置圆心坐标(脉冲数)  
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
							        Int32 Data);				 // 圆心坐标脉冲数范围(-2147483648-+2147483647)
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetLP(				 // 设置逻辑位置计数器
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
							        Int32 Data);				 // 逻辑位置计数器值(-2147483648-+2147483647)

                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetEP(				 // 设置实位计数器 
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
							        Int32 Data);				 // 实位计数器值(-2147483648-+2147483647)
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetAccofst(			 // 设置加速计数器偏移
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
							        Int32 Data);				 // 偏移范围(0-65535)
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SelectLPEP(			 // 选择逻辑计数器或实位计数器
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
							        Int32 LogicFact);		 // 选择逻辑位置计数器或实位计数器 PCI1020_LOGIC：逻辑位置计数器 PCI1020_FACT：实位计数器	
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetCOMPP(			 // 设置COMP+寄存器
							        IntPtr hDevice,			 // 设备号
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)  
							        UInt16 LogicFact,		 // 选择逻辑位置计数器或实位计数器 PCI1020_LOGIC：逻辑位置计数器 PCI1020_FACT：实位计数器	
							        Int32 Data);
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetCOMPM(			 // 设置COMP-寄存器
							        IntPtr hDevice,			 // 设备号
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
							        UInt16 LogicFact,		 // 选择逻辑位置计数器或实位计数器 PCI1020_LOGIC：逻辑位置计数器 PCI1020_FACT：实位计数器	
							        Int32 Data);
        //*******************************************************************
        // 设置同步位
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetSynchronAction(	 // 设置同步位
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
							        ref PCI1020_PARA_SynchronActionOwnAxis pPara1,// 自己轴的参数设置
						            ref PCI1020_PARA_SynchronActionOtherAxis pPara2);// 其它轴的参数设置
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SynchronActionDisable(// 设置同步位无效
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)
                                    ref  PCI1020_PARA_SynchronActionOwnAxis pPara1,// 自己轴的参数设置
                                    ref  PCI1020_PARA_SynchronActionOtherAxis pPara2);// 其它轴的参数设置
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_WriteSynchronActionCom(// 写同步操作命令
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum);			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 

        //*******************************************************************
        //  设置DCC和其他模式
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetDCC(				 // 设置输出信号nDCC的输出电平和电平宽度
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
                                    ref PCI1020_PARA_DCC pPara);// DCC信号参数结构体指针
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_StartDCC(			   // 启动偏离计数器清除输出命令
							        IntPtr hDevice,			   // 设备句柄
							        Int32 AxisNum);			   // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_ExtMode(				   // 设置其他模式
							        IntPtr hDevice,			   // 设备句柄
							        Int32 AxisNum,			   // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
                                    ref PCI1020_PARA_ExpMode pPara);// 其他参数结构体指针
        //*******************************************************************
        // 设置自动原点搜寻
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetInEnable(			// 设置自动原点搜寻第一、第二、第三步外部触发信号IN0-2的有效电平
							        IntPtr hDevice,			// 设备号
							        Int32 AxisNum,			// 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)	
							        Int32 InNum,				// 停止号
							        Int32 LogLever);			// 有效电平
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetAutoHomeSearch(   // 设置自动原点搜寻参数
							        IntPtr hDevice,			// 设备句柄
							        Int32 AxisNum,			// 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)
							        ref PCI1020_PARA_AutoHomeSearch pPara);// 自动搜寻原点参数结构体指针
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_StartAutoHomeSearch( // 启动自动原点搜寻
							        IntPtr hDevice,			// 设备句柄		
							        Int32 AxisNum);			// 轴号(1:X轴; 2:Y轴)	

        //*******************************************************************
        // 设置编码器输入信号类型
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetEncoderSignalType(// 设置编码器输入信号类型
							        IntPtr hDevice,			// 设备句柄
							        Int32 AxisNum,			// 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)	
							        Int32 Type);				// 输入信号类型 0：2相脉冲输入 1：上/下脉冲输入

        //*******************************************************************
        // 直线S曲线初始化、启动函数
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_InitLVDV(				// 初始化连续,定长脉冲驱动
							        IntPtr hDevice,				// 设备句柄
							        ref PCI1020_PARA_DataList pDL, // 公共参数结构体指针
							        ref PCI1020_PARA_LCData pLC);	// 直线S曲线参数结构体指针
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_StartLVDV(				// 启动连续,定长脉冲驱动
							        IntPtr hDevice,				// 设备句柄
							        Int32 AxisNum);				// 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool	PCI1020_Start4D(IntPtr hDevice);// 4轴同时启动						           
        //*******************************************************************
        // 任意2轴直线插补初始化、启动函数
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_InitLineInterpolation_2D(// 初始化任意2轴直线插补运动 
							        IntPtr hDevice,				// 设备句柄
							        ref PCI1020_PARA_DataList pDL, // 公共参数结构体指针
							        ref PCI1020_PARA_InterpolationAxis pIA,// 插补轴结构体指针
							        ref PCI1020_PARA_LineData pLD);// 直线插补结构体指针
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_StartLineInterpolation_2D(// 启动任意2轴直线插补运动 
							        IntPtr hDevice);			 // 设备句柄
        							
        //*******************************************************************
        // 任意3轴直线插补初始化、启动函数
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_InitLineInterpolation_3D(// 初始化任意3轴直线插补运动	
							        IntPtr hDevice,				// 设备句柄
							        ref PCI1020_PARA_DataList pDL, // 公共参数结构体指针
							        ref PCI1020_PARA_InterpolationAxis pIA,// 插补轴结构体指针
							        ref PCI1020_PARA_LineData pLD);// 直线插补结构体指针
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_StartLineInterpolation_3D(// 启动任意3轴直线插补运动 				
							        IntPtr hDevice);			 // 设备句柄
        	
        //*******************************************************************
        // 任意2轴正反方向圆弧插补初始化、启动函数
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_InitCWInterpolation_2D(	// 初始化任意2轴正反方向圆弧插补运动 
							        IntPtr hDevice,				// 设备句柄
							        ref PCI1020_PARA_DataList pDL, // 公共参数结构体指针
							        ref PCI1020_PARA_InterpolationAxis pIA,// 插补轴结构体指针
							        ref PCI1020_PARA_CircleData pCD);// 圆弧插补结构体指针
                                 
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_StartCWInterpolation_2D( // 启动任意2轴正、反方向圆弧插补运动 
							        IntPtr hDevice,				// 设备句柄
	                                Int32 Direction);			// 方向 正转：PCI1020_PDIRECTION 反转：PCI1020_MDIRECTION                     
        //*************************************************************************
        // 位插补相关函数
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_InitBitInterpolation_2D(	// 初始化任意2轴位插补参数
							        IntPtr hDevice,				// 设备句柄
							        ref PCI1020_PARA_InterpolationAxis pIA,// 插补轴结构体指针
							        ref PCI1020_PARA_DataList pDL);// 公共参数结构体指针
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_InitBitInterpolation_3D(// 初始化任意2轴位插补参数
							        IntPtr hDevice,			   // 设备句柄
							        ref PCI1020_PARA_InterpolationAxis pIA,// 插补轴结构体指针
						            ref PCI1020_PARA_DataList pDL);// 公共参数结构体指针
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_AutoBitInterpolation_2D( // 启动任意2轴位插补子线程
							        IntPtr hDevice,				// 设备句柄
							        UInt16[] pBuffer,				// 位插补数据指针	
							        UInt32 nCount);				// 数据组数
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_AutoBitInterpolation_3D( // 启动任意3轴位插补子线程
							        IntPtr hDevice,				// 设备句柄
							        UInt16[] pBuffer,				// 位插补数据指针	
							        UInt32 nCount);				// 数据组数
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_ReleaseBitInterpolation(	// 释放BP寄存器
							        IntPtr hDevice);			// 设备句柄
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetBP_2D(                // 设置任意2轴位插补数据
							        IntPtr hDevice,				// 设备句柄 
							        UInt16 BP1PData,				// 1轴正方向驱动数据
							        UInt16 BP1MData,				// 1轴反方向驱动数据
							        UInt16 BP2PData,				// 2轴正方向驱动数据
							        UInt16 BP2MData);				// 2轴反方向驱动数据
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetBP_3D(				// 设置任意3轴位插补数据	
							        IntPtr hDevice,				// 设备句柄
							        UInt16 BP1PData,			// 1轴正方向驱动数据
							        UInt16 BP1MData,			// 1轴反方向驱动数据
							        UInt16 BP2PData,			// 2轴正方向驱动数据
							        UInt16 BP2MData,			// 2轴反方向驱动数据
							        UInt16 BP3PData,			// 3轴正方向驱动数据
							        UInt16 BP3MData);			// 3轴反方向驱动数据
                
                [DllImport("PCI1020_32.DLL")]
        public static extern Int32 PCI1020_BPRegisterStack(			// BP位数据堆栈返回值
							        IntPtr hDevice);			// 设备句柄
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_StartBitInterpolation_2D(// 启动任意2轴位插补
							        IntPtr hDevice);			// 设备句柄
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_StartBitInterpolation_3D(// 启动任意3轴位插补
							        IntPtr hDevice);			// 设备句柄
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool  PCI1020_BPWait(					// 等待位插补的下一个数据设定
							        IntPtr hDevice);			// 设备句柄
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_ClearBPData(				// 清除BP寄存器数据
							        IntPtr hDevice);			// 设备句柄
        //*******************************************************************
        // 连续插补相关函数
        		
                [DllImport("PCI1020_32.DLL")]					
        public static extern bool  PCI1020_NextWait(				// 等待连续插补下一个节点命令设定
							        IntPtr hDevice);			// 设备句柄

        //*******************************************************************
        // 单步插补函数
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SingleStepInterpolationCom(// 设置命令控制单步插补运动
							        IntPtr hDevice);			// 设备句柄	
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_StartSingleStepInterpolation(// 发单步命令
							        IntPtr hDevice);
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SingleStepInterpolationExt(// 设置外部控制单步插补运动
							        IntPtr hDevice);			// 设备句柄
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_ClearSingleStepInterpolation(// 清除单步插补设置
							        IntPtr hDevice);			// 设备句柄
        //*******************************************************************
        // 中断位设置、插补中断状态清除
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetInterruptBit(			// 设置中断位
							        IntPtr hDevice,				// 设备句柄
							        Int32 AxisNum,				// 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
							        ref PCI1020_PARA_Interrupt pPara);// 中断位结构体指针
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_ClearInterruptStatus(	// 清除插补中断状态 
							        IntPtr hDevice);			// 设备句柄

        //*******************************************************************
        // 外部信号启动电机定长驱动、连续驱动
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetOutEnableDV(		 // 设置外部使能定量驱动(下降沿有效)
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum);			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
        		    
                [DllImport("PCI1020_32.DLL")]                
        public static extern bool PCI1020_SetOutEnableLV(		 // 设置外部使能连续驱动(保持低电平有效)
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum);			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 

        //*******************************************************************
        // 设置软件限位有效和无效
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetPDirSoftwareLimit( // 设置正方向软件限位
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
							        Int32 LogicFact,			 // 逻辑/实位计数器选择 PCI1020_LOGIC：逻辑位置计数器 PCI1020_FACT：实位计数器	
							        Int32 Data);				 // 软件限位数据
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetMDirSoftwareLimit( // 设置反方向软件限位
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
							        Int32 LogicFact,			 // 逻辑/实位计数器选择 PCI1020_LOGIC：逻辑位置计数器 PCI1020_FACT：实位计数器	
							        Int32 Data);				 
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_ClearSoftwareLimit(	 // 清除软件限位
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum);			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)

        //******************************************************************* 
        // 设置外部输入信号的有效和无效	
                [DllImport("PCI1020_32.DLL")]	
        public static extern bool PCI1020_SetLMTEnable(		 // 设置外部越限信号的有效及停止方式	
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
							        Int32 StopMode);          // PCI1020_DECSTOP：减速停止，PCI1020_SUDDENSTOP：立即停止
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetStopEnable(		 // 设置外部停止信号有效
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
							        Int32 StopNum);			 // 停止号
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetStopDisable(		 // 设置外部停止信号无效
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)
							        Int32 StopNum);			 // 停止号
        		
                [DllImport("PCI1020_32.DLL")]									
        public static extern bool PCI1020_SetALARMEnable(       // 设置伺服报警信号有效 
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum);			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)  
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetALARMDisable(      // 设置伺服报警信号无效  
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum);			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)  
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetINPOSEnable(		 // 设置伺服马达定位完毕输入信号有效 
							        IntPtr hDevice,			 // 设备句柄	
							        Int32 AxisNum);			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetINPOSDisable(		 // 设置伺服马达定位完毕输入信号无效
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum);			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 

        //*******************************************************************
        // 减速函数设置
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_DecValid(			 // 减速有效
							        IntPtr hDevice);		 // 设备句柄
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_DecInvalid(			 // 减速无效
							        IntPtr hDevice);		 // 设备句柄
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_DecStop(				 // 减速停止
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum);			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)  
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_InstStop(			 // 立即停止
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum);			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_AutoDec(				 // 自动减速
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum);			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_HanDec(				 // 手动减速点设定
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
							        Int32 Data);				 // 手动减速点数据，范围(0 - 4294967295)

        //*************************************************************************
        // 读电机状态：逻辑计数器、实际位置计数器、当前速度、加/减速度
                [DllImport("PCI1020_32.DLL")]
        public static extern Int32 PCI1020_ReadLP(				 // 读逻辑计数器
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum);			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
                
                [DllImport("PCI1020_32.DLL")]
        public static extern Int32 PCI1020_ReadEP(				 // 读实位计数器
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum);			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
                
                [DllImport("PCI1020_32.DLL")]
        public static extern Int32 PCI1020_ReadBR(				 // 读同步缓冲寄存器
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum);			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴)
                
                [DllImport("PCI1020_32.DLL")]
        public static extern Int32 PCI1020_ReadCV(				 // 读当前速度
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum);			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
                
                [DllImport("PCI1020_32.DLL")]
        public static extern Int32 PCI1020_ReadCA(				 // 读当前加速度
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum);			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 

        //*******************************************************************
        // 设置输出切换和通用输出
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_OutSwitch(			 // 设置输出切换
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
							        Int32 StatusGeneralOut);	 // 状态输出和通用输出选择 PCI1020_STATUS:状态输出 PCI1020_GENERAL:通用输出
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_SetDeviceDO(
							         IntPtr hDevice,	 	 // 设备号
							         Int32 AxisNum,			 // 轴号
							         ref PCI1020_PARA_DO pPara);		
        //*******************************************************************
        // 读状态寄存器的位状态
                [DllImport("PCI1020_32.DLL")]
        public static extern Int32 PCI1020_ReadRR(				 // 读RR寄存器
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
							        Int32 Num);				 // 寄存器号
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_GetRR0Status(		 // 获得主状态寄存器RR0的位状态
							        IntPtr hDevice,			 // 设备句柄
							        ref PCI1020_PARA_RR0 pPara);// RR0寄存器状态
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_GetRR1Status(		 // 获得状态寄存器RR1的位状态
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
							        ref PCI1020_PARA_RR1 pPara);// RR1寄存器状态			
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_GetRR2Status(		 // 获得状态寄存器RR2的位状态
							        IntPtr hDevice,			 // 设备句柄
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
							        ref PCI1020_PARA_RR2 pPara);// RR2寄存器状态			
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_GetRR3Status(		 // 获得状态寄存器RR3的位状态
							        IntPtr hDevice,			 // 设备句柄
							        ref PCI1020_PARA_RR3 pPara);// RR3寄存器状态			
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_GetRR4Status(		 // 获得状态寄存器RR4的位状态
							        IntPtr hDevice,			 // 设备句柄
							        ref PCI1020_PARA_RR4 pPara);// RR4寄存器状态
                
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_GetRR5Status(
							        IntPtr hDevice,			 // 设备号
							        Int32 AxisNum,			 // 轴号(PCI1020_XAXIS:X轴,PCI1020_YAXIS:Y轴, PCI1020_ZAXIS:Z轴,PCI1020_UAXIS:U轴) 
							        ref PCI1020_PARA_RR5 pPara);// RR5寄存器状态
                
        //####################### 中断函数 #################################
        // 它由硬件信号的状态变化引起CPU产生中断事件hEventInt。
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_InitDeviceInt(IntPtr hDevice, IntPtr hEventInt); // 初始化中断      
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_ReleaseDeviceInt(IntPtr hDevice); // 释放中断资源
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_GetDeviceIntSrc(IntPtr hDevice, Byte[] IntSrc); // 获取设备中断位
                [DllImport("PCI1020_32.DLL")]
        public static extern bool PCI1020_ResetDeviceIntSrc(IntPtr hDevice, Byte IntSrcID); // 释放中断位
        //*******************************************************************

    }
}
