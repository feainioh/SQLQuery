using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO.Ports;
using System.IO;
using System.Threading;


namespace PylonLiveView
{
    public class Modbus
    {
        public enum emMsgFormat : int
        {
            Decimal = 10,
            Hex = 16,
        }
        public enum emFunction : byte
        {
            ReadCoil = 1,
            ReadDiscreteInputs = 2,
            ReadHoldReg = 3,
            ReadInputReg = 4,
            WriteSingleCoil = 5,
            WriteSingleHoldReg = 6,
            WriteMultiCoils = 15,
            WriteMultiHoldReg = 16,
        }
        public enum emMsgType:int
        {
            Send = 0,
            Recv,
            Info,
            Error,
        }
        public enum emCommMode : int
        {
            NoConnection = 0,
            TCP = 1,
            RTU = 2,
            ASCII = 3,
            
        }
        #region 事件定义
        //接收到数据的事件
        public delegate void delegate_DataReceive(OutputModule output);
        public event delegate_DataReceive event_DataReceive;
        //发送或接到通信消息时的事件，用于通信记录的显示
        /// <summary>
        ///  参数：nMsgType 0，发送的消息；1，接收的消息；3，通信报错；
        /// </summary>
        public delegate void dele_MessageText(string str, emMsgType nMsgType, int funcCode, int offset);
        public event dele_MessageText event_MessageText;
        #endregion
        #region 常量定义
        #region 功能码定义
        public const byte byREAD_COIL = 1;
        public const byte byREAD_DISCRETE_INPUTS = 2;
        public const byte byREAD_HOLDING_REG = 3;
        public const byte byREAD_INPUT_REG = 4;
        public const byte byWRITE_SINGLE_COIL = 5;
        public const byte byWRITE_SINGLE_HOLDING_REG = 6;
        public const byte byWRITE_MULTI_COILS = 15;
        public const byte byWRITE_MULTI_HOLDING_REG = 16;
        #endregion
        private const int nRECONNECT_TIMES = 4; //TCP重连次数
        private const int nRESEND_TIMES = 4; //TCP消息重发次数
        private const int nTIMEOUT_SEND = 2000;
        private const int nTIMEOUT_RECV = 2000;
        #endregion
        #region 属性
        public bool m_bConnected
        {
            get
            {
                switch (m_nRunMode)
                {
                    case emCommMode.RTU:
                        if (m_SerialPort != null)
                        {
                            return m_SerialPort.IsOpen;
                        }
                        else return false;
                    case emCommMode.TCP:
                        if (m_socketTcp != null)
                        {
                            return m_socketTcp.Connected;
                        }
                        else return false;
                    default:
                        return false;
                }
            }
        }
        public emCommMode CommMode
        {
            get
            {
                return m_nRunMode;
            }
        }
        public emMsgFormat MsgForm
        {
            set { m_nMsgFormat = value;}
            get { return m_nMsgFormat; }
        }
        #endregion
        #region 成员变量定义
        Thread m_thd_SendMsg = null;
        AutoResetEvent m_Event_SendMsg = new AutoResetEvent(false);
        private Queue<InputModule> m_InputQueue = new Queue<InputModule> { };
        private emMsgFormat m_nMsgFormat = emMsgFormat.Hex;
        OutputModule m_output = new OutputModule();
        private byte[] m_byTCPDataRecv = new byte[14];//null;
        private byte[] m_byRtuDataRecv = new byte[14];
        private IPAddress m_IP;
        private int m_nPort = 502;
        private Socket m_socketTcp;
        private SerialPort m_SerialPort = null;
        private emCommMode m_nRunMode = (int)emCommMode.NoConnection;
        private int m_nTCPCount = 0;//用于TCP消息计数
        Thread thd_TcpConnect = null;
        #endregion
        #region TCP
        #region Construction
        public Modbus(IPAddress _ip, int port)
        {
            m_nRunMode = emCommMode.TCP;
            m_IP = _ip;
            m_nPort = port;
            if (!TCPconnect(_ip, m_nPort))
            {
                throw new Exception("网络连接失败！");
            }
            m_thd_SendMsg = new Thread(new ThreadStart(ThdProcSendMessage));
            m_thd_SendMsg.IsBackground = true;
        }
        #endregion
        /// <summary>
        ///   TCP连接
        /// </summary>
        private bool TCPconnect(IPAddress ip, int nPort = 502)
        {
            AutoResetEvent eventConn = new AutoResetEvent(false);
            thd_TcpConnect = new Thread(new ThreadStart(
                delegate
                {
                    int nCount = 1;
                    do
                    {
                        try
                        {
                            if (event_MessageText != null)
                            {
                                event_MessageText(string.Format("第{0}次连接...", nCount), emMsgType.Info, 0, 0);
                            }
//                            IPAddress _ip;
//                             if (IPAddress.TryParse(strIP, out _ip) == false)
//                             {
//                                 IPHostEntry hst = Dns.GetHostEntry(strIP);
//                                 strIP = hst.AddressList[0].ToString();
//                             }
                            m_socketTcp = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                            m_socketTcp.Connect(new IPEndPoint(ip, nPort));
                            m_socketTcp.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, nTIMEOUT_SEND);
                            m_socketTcp.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, nTIMEOUT_RECV);
                            m_socketTcp.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, 1);
                            if (event_MessageText != null)
                            {
                                event_MessageText("连接成功！", emMsgType.Info, 0, 0);
                            }
                            eventConn.Set();
                            return;
                        }
                        catch (Exception)
                        {
                            if (m_socketTcp != null)
                            {
                                m_socketTcp.Dispose();
                                m_socketTcp = null;
                            }
                            continue;
                        }
                    }
                    while (++nCount <= nRECONNECT_TIMES);
                    if (event_MessageText != null)
                    {
                        event_MessageText("连接失败！", emMsgType.Error, 0, 0);
                    }
                    eventConn.Set();
                    return;
                }));
            thd_TcpConnect.IsBackground = true;
            thd_TcpConnect.Start();
            eventConn.WaitOne();
            return m_bConnected;
        }
        /// <summary>
        ///  TCP下读取数据组帧 
        /// </summary>
        private byte[] CreateReadMsg_TCP(InputModule cmdMsg)
        {
            byte[] byMsg = new byte[12];
            if (m_nTCPCount++ == 65535)
            {
                m_nTCPCount = 0;
            }
            //事务标识符
            byte[] byCount = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)m_nTCPCount));
            byMsg[0] = byCount[0];
            byMsg[1] = byCount[1];
            //协议标识符
            byMsg[2] = 0;
            byMsg[3] = 0;
            //长度
            byte[] byLength = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)6));
            byMsg[4] = byLength[0];
            byMsg[5] = byLength[1];
            //SlaveID
            byMsg[6] = cmdMsg.bySlaveID;
            //功能码 1 
            byMsg[7] = cmdMsg.byFuntion;
            //数据 N
            byte[] byAddr = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)cmdMsg.nStartAddr));
            byMsg[8] = byAddr[0];
            byMsg[9] = byAddr[1];
            byte[] byDataLength = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)cmdMsg.nDataLength));
            byMsg[10] = byDataLength[0];
            byMsg[11] = byDataLength[1];
            //定义接收buffer大小
            SetRecvBufSize(ref m_byTCPDataRecv, cmdMsg);
            return byMsg;
        }
        /// <summary>
        ///  TCP下写入数据组帧 
        /// </summary>
        private byte[] CreateWriteMsg_TCP(InputModule cmdMsg)
        {
            byte[] byMsg = null;
            int nWriteDataIndex = 0;
            if (cmdMsg.byFuntion >= byWRITE_MULTI_COILS)
            {
                byMsg = new byte[10 + cmdMsg.byWriteData.Length + 3];
            }
            else
            {
                byMsg = new byte[10 + cmdMsg.byWriteData.Length];
            }
            if (m_nTCPCount++ == 65535)
            {
                m_nTCPCount = 0;
            }
            //事务标识符
            byte[] byCount = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)m_nTCPCount));
            byMsg[0] = byCount[0];
            byMsg[1] = byCount[1];
            //协议标识符
            byMsg[2] = 0;
            byMsg[3] = 0;
            //长度
            byte[] byLength = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)(byMsg.Length - 6)));
            byMsg[4] = byLength[0];
            byMsg[5] = byLength[1];

            byMsg[6] = cmdMsg.bySlaveID;
            byMsg[7] = cmdMsg.byFuntion;
            byte[] byAddr = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)cmdMsg.nStartAddr));
            byMsg[8] = byAddr[0];
            byMsg[9] = byAddr[1];
            nWriteDataIndex = 9 + 1;
            if (cmdMsg.byFuntion >= byWRITE_MULTI_COILS)
            {
                byte[] _cnt = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)cmdMsg.nDataLength));
                byMsg[10] = _cnt[0];			// Number of bytes
                byMsg[11] = _cnt[1];			// Number of bytes
                byMsg[12] = Convert.ToByte(cmdMsg.byWriteData.Length);
                nWriteDataIndex = 12 + 1;
            }
            Array.Copy(cmdMsg.byWriteData, 0, byMsg, nWriteDataIndex, cmdMsg.byWriteData.Length);
            //定义接收buffer大小
            SetRecvBufSize(ref m_byTCPDataRecv, cmdMsg);
            return byMsg;
        }
        /// <summary>
        ///   TCP读写寄存器数据
        /// </summary>
        private string SendMessage_TCP_Async(byte[] bySend)
        {
            try
            {
                SocketError socketErr;
                m_socketTcp.BeginSend(bySend, 0, bySend.Length, SocketFlags.None, out socketErr, new AsyncCallback(OnSend), null);
                if (event_MessageText != null)
                {
                    //string str = BitConverter.ToString(bySend);
                    event_MessageText(BytesToStr(bySend), emMsgType.Send, 0, 0);
                }
                m_socketTcp.BeginReceive(m_byTCPDataRecv, 0, m_byTCPDataRecv.Length, SocketFlags.None, new AsyncCallback(OnReceive), m_socketTcp);
            }
            catch (Exception ex)
            {       
                return ex.Message;
            }
            return null;
        }
        private void OnSend(IAsyncResult result) 
        {
            if (result.IsCompleted == false)
            {
                if (event_MessageText != null)
                {
                    event_MessageText("发送失败!", emMsgType.Error, 0, 0);
                }
            }
        }
        private void OnReceive(IAsyncResult result)
        {
            if (result.IsCompleted == false)
            {
                if (event_MessageText != null)
                {
                    event_MessageText("接收失败!", emMsgType.Error, 0, 0);
                }
            }
            else
            {
                m_output.byRecvData = m_byTCPDataRecv;
                if (event_DataReceive != null)
                {
                    event_DataReceive(m_output);
                }
                if (event_MessageText != null)
                {
                    //string str = BitConverter.ToString(m_byTCPDataRecv);
                    event_MessageText(BytesToStr(m_byTCPDataRecv), emMsgType.Recv, m_output.byFuntion, m_output.nStartAddr);
                }
                m_Event_SendMsg.Set();
            }
        }
        private string SendMessage_TCP_Sync(byte[] bySend, int funccode, int offset)
        {
            if (m_socketTcp == null)
            {
                if (event_MessageText != null)
                {
                    event_MessageText("检测到套接字处于关闭状态,正在尝试重连...", emMsgType.Error, funccode, offset);
                }
                if (!TCPconnect(m_IP, m_nPort))
                {
                    return "重连失败！无法发送消息！";
                }
            }
            int nCount = 1;
            do 
            {
                try
                {
                    m_socketTcp.Send(bySend);
                    if (event_MessageText != null)
                    {
                        //string str = BitConverter.ToString(bySend);
                        event_MessageText(BytesToStr(bySend), emMsgType.Send, funccode, offset);
                    }
                    m_socketTcp.Receive(m_byTCPDataRecv);
                    if (event_DataReceive != null)
                    {
                        m_output.byRecvData = m_byTCPDataRecv;
                        event_DataReceive(m_output);
                    }
                    if (event_MessageText != null)
                    {
                        //string str = BitConverter.ToString(m_byTCPDataRecv);
                        event_MessageText(BytesToStr(m_byTCPDataRecv), emMsgType.Recv, m_output.byFuntion, m_output.nStartAddr);
                    }
                    m_Event_SendMsg.Set();
                    return null;
                }
                catch (ObjectDisposedException ex)//socket 关闭
                {
                    if (event_MessageText != null)
                    {
                        event_MessageText(ex.Message, emMsgType.Error, 0, 0);
                    }
                    if (m_socketTcp != null)
                    {
                        m_socketTcp.Dispose();
                        m_socketTcp = null;
                    }
                    if (TCPconnect(m_IP, m_nPort))
                    {
                        continue;
                    }
                    else
                    {
                        return "消息发送失败，已断开连接！";
                    }
                }
                catch (ArgumentNullException ex)//buffer为空
                {
                    return ex.Message;
                }
                catch(SocketException ex)//访问socket出错
                {
                    if(event_MessageText != null)
                    {
                        event_MessageText(ex.Message, emMsgType.Error, 0, 0);
                    }
                    if (m_socketTcp != null)
                    {
                        m_socketTcp.Dispose();
                        m_socketTcp = null;
                    }
                    if (TCPconnect(m_IP, m_nPort))
                    {
                        continue;
                    }
                    else
                    {
                        return "消息发送失败！已断开连接！";
                    }
                }
                catch (ArgumentOutOfRangeException ex)//越界
                {
                    return ex.Message;
                }
            } 
            while (++nCount <= nRESEND_TIMES);
            return "消息发送失败！";
        }
        #endregion
        #region SerialPort 
        #region Construction
        public Modbus(string strPortName, int nBaudRate, int nDatabits, Parity parity, StopBits stopBits)
        {
            if (m_SerialPort != null)
            {
                m_SerialPort.Dispose();
                m_SerialPort = null;
            }
            m_SerialPort = new SerialPort(strPortName, nBaudRate, parity, nDatabits, stopBits);
            m_nRunMode = emCommMode.RTU;
            try
            {
                if (!SPconnect())
                {
                    throw new Exception("串口打开失败！");
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            m_thd_SendMsg = new Thread(new ThreadStart(ThdProcSendMessage));
            m_thd_SendMsg.IsBackground = true;
        }
        #endregion
        private bool SPconnect()
        {
            m_SerialPort.Open();
            if (m_SerialPort.IsOpen)
            {
                m_SerialPort.ReadTimeout = nTIMEOUT_RECV;
                m_SerialPort.WriteTimeout = nTIMEOUT_SEND;
                m_SerialPort.DataReceived += new SerialDataReceivedEventHandler(m_SerialPort_DataReceived);
                return true;
            }
            else
            {
                return false;
            }      
        }
        /// <summary>
        ///   RTU方式读取数据组帧
        /// </summary>
        private byte[] CreateReadHeader_RTU(InputModule cmdMsg)
        {
            byte[] byMsg = new byte[8];
            byMsg[0] = cmdMsg.bySlaveID;
            byMsg[1] = cmdMsg.byFuntion;
            byte[] byAddr = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)cmdMsg.nStartAddr));
            byMsg[2] = byAddr[0];
            byMsg[3] = byAddr[1];
            byte[] byDataLength = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)cmdMsg.nDataLength));
            byMsg[4] = byDataLength[0];
            byMsg[5] = byDataLength[1];
            byte[] CRC = CRC16(byMsg);
            byMsg[byMsg.Length - 2] = CRC[0];
            byMsg[byMsg.Length - 1] = CRC[1];
            //定义接收buffer大小
            SetRecvBufSize(ref m_byRtuDataRecv, cmdMsg);
            return byMsg;
        }
        /// <summary>
        ///   RTU方式写入数据组帧
        /// </summary>
        private byte[] CreateWritrHeader_RTU(InputModule cmdMsg)
        {
            byte[] byMsg = null;
            int nWriteDataIndex = 0;
            if (cmdMsg.byFuntion >= byWRITE_MULTI_COILS)
            {
                byMsg = new byte[6 + cmdMsg.byWriteData.Length + 3];
            }
            else
            {
                byMsg = new byte[6 + cmdMsg.byWriteData.Length];
            }
            byMsg[0] = cmdMsg.bySlaveID;
            byMsg[1] = cmdMsg.byFuntion;
            byte[] byAddr = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)cmdMsg.nStartAddr));
            byMsg[2] = byAddr[0];
            byMsg[3] = byAddr[1];
            nWriteDataIndex = 3 + 1;
            if (cmdMsg.byFuntion >= byWRITE_MULTI_COILS)
            {
                byte[] _cnt = BitConverter.GetBytes((short)IPAddress.HostToNetworkOrder((short)cmdMsg.nDataLength));
                byMsg[4] = _cnt[0];			// Number of bytes
                byMsg[5] = _cnt[1];			// Number of bytes
                byMsg[6] = Convert.ToByte(cmdMsg.byWriteData.Length);
                nWriteDataIndex = 6 + 1;
            }
            Array.Copy(cmdMsg.byWriteData, 0, byMsg, nWriteDataIndex, cmdMsg.byWriteData.Length);
            byte[] CRC = CRC16(byMsg);
            byMsg[byMsg.Length - 2] = CRC[0];
            byMsg[byMsg.Length - 1] = CRC[1];
            //定义接收buffer大小
            SetRecvBufSize(ref m_byRtuDataRecv, cmdMsg);
            return byMsg;
        }
        private string SendMessage_SP(byte[] byWriteData)
        {
            try
            {
                m_SerialPort.DiscardOutBuffer();
                m_SerialPort.DiscardInBuffer();
                m_SerialPort.Write(byWriteData, 0, byWriteData.Length);
                if (event_MessageText != null)
                {
                    //string str = BitConverter.ToString(byWriteData);
                    event_MessageText(BytesToStr(byWriteData), emMsgType.Send, 0, 0);
                }
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
            return null;
        }
        private void m_SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
         {

             for (int i = 0; i < m_byRtuDataRecv.Length; i++)
             {
                 try
                 {
                     m_byRtuDataRecv[i] = (byte)m_SerialPort.ReadByte();
                 }
                 catch (TimeoutException)
                 {
                     break;
                 }
                 catch (InvalidOperationException ex)
                 {
                     if (event_MessageText != null)
                     {
                         event_MessageText(ex.Message, emMsgType.Error, 0, 0);
                     }
                     throw new Exception("端口未打开！");
                 }
             }
             try
             {
                 if (!CheckCRC(m_byRtuDataRecv))
                 {
                     if (event_MessageText != null)
                     {
                         event_MessageText("消息校验出错！", emMsgType.Error, 0, 0);
                     }
                     return;
                 }
                 if (event_DataReceive != null)
                 {
                     m_output.byRecvData = m_byRtuDataRecv;
                     event_DataReceive(m_output);
                 }
                 if (event_MessageText != null)
                 {
                     //string str = BitConverter.ToString(m_byRtuDataRecv);
                     event_MessageText(BytesToStr(m_byRtuDataRecv), emMsgType.Recv, 0, 0);
                 }
                 m_Event_SendMsg.Set();
             }
             catch(Exception ex)
             {
                 if (event_MessageText != null)
                 {
                     event_MessageText(ex.Message, emMsgType.Error, 0, 0);
                 }
                 return;
             } 
         }
        #endregion
        #region 内部调用
        private void ThdProcSendMessage()
        {
            while(m_InputQueue.Count > 0)
            {
                InputModule input = m_InputQueue.Dequeue();
                Send(input);
                m_Event_SendMsg.WaitOne();
                Thread.Sleep(200);
            }
        }

        private void Send(InputModule input)
        {
            Thread thd = new Thread(new ThreadStart(
                delegate
                {
                    m_output.nStartAddr = input.nStartAddr;
                    m_output.byFuntion = input.byFuntion;

                    bool bReadOrWriteReg;
                    string strException;
                    if ((input.byFuntion == byREAD_COIL)
                        || (input.byFuntion == byREAD_DISCRETE_INPUTS)
                        || (input.byFuntion == byREAD_HOLDING_REG)
                        || (input.byFuntion == byREAD_INPUT_REG))
                    {
                        bReadOrWriteReg = true;
                    }
                    else if ((input.byFuntion == byWRITE_SINGLE_COIL)
                        || (input.byFuntion == byWRITE_MULTI_HOLDING_REG)
                        || (input.byFuntion == byWRITE_MULTI_COILS)
                        || (input.byFuntion == byWRITE_SINGLE_HOLDING_REG))
                    {
                        bReadOrWriteReg = false;
                    }
                    else
                    {
                        if (event_MessageText != null)
                        {
                            event_MessageText("检测到不支持的功能码！", emMsgType.Error, 0, 0);
                        }
                        return;
                    }
                    try
                    {
                        switch (m_nRunMode)
                        {
                            case emCommMode.RTU:
                                if (bReadOrWriteReg)
                                {
                                    if ((strException = SendMessage_SP(CreateReadHeader_RTU(input))) != null)
                                    {
                                        if (event_MessageText != null)
                                        {
                                            event_MessageText(strException, emMsgType.Error, 0, 0);
                                        }
                                    }
                                }
                                else
                                {
                                    if ((strException = SendMessage_SP(CreateWritrHeader_RTU(input))) != null)
                                    {
                                        if (event_MessageText != null)
                                        {
                                            event_MessageText(strException, emMsgType.Error, 0, 0);
                                        }
                                    }
                                }
                                break;
                            case emCommMode.TCP:
                                if (bReadOrWriteReg)
                                {
                                    if ((strException = SendMessage_TCP_Sync(CreateReadMsg_TCP(input), Convert.ToInt32(input.byFuntion), input.nStartAddr)) != null)
                                    {
                                        if (event_MessageText != null)
                                        {
                                            event_MessageText(strException, emMsgType.Error, 0, 0);
                                        }
                                    }
                                }
                                else
                                {
                                    if ((strException = SendMessage_TCP_Sync(CreateWriteMsg_TCP(input), Convert.ToInt32(input.byFuntion), input.nStartAddr)) != null)
                                    {
                                        if (event_MessageText != null)
                                        {
                                            event_MessageText(strException, emMsgType.Error, 0, 0);
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    catch (System.Exception ex)
                    {
                        if (event_MessageText != null)
                        {
                            event_MessageText(ex.Message, emMsgType.Error, 0, 0);
                        }
                    }
                }
                ));
            thd.IsBackground = true;
            thd.Start();
        }
        private bool CheckCRC(byte[] byMsg)
        {
            int nEndIndex = byMsg.Length - 1;
            while (byMsg[nEndIndex] == 0 || (byMsg[nEndIndex] == '\0'))
            {
                nEndIndex--;
            }
            byte[] byCRC = new byte[2];
            byCRC[1] = byMsg[nEndIndex];
            byCRC[0] = byMsg[nEndIndex - 1];
            byte[] byTmp = new byte[nEndIndex+1];
            Array.Copy(byMsg, 0, byTmp, 0, byTmp.Length);
            byte[] byTmp2 = CRC16(byTmp);
            if ((byCRC[0] == byTmp2[0]) && (byCRC[1] == byTmp2[1]))
            {
                return true;
            }
            return false;
        }
        private void SetRecvBufSize(ref byte[] byArr, InputModule input)
        {
            if (byArr != null)
            {
                Array.Clear(byArr, 0, byArr.Length);
                byArr = null;
            }
            int nHead = 20, nCRC = 0;
            switch (m_nRunMode)
            {
                case emCommMode.TCP:
                        nHead = 8;
                        break;
                case emCommMode.RTU:
                        nHead = 2;
                        nCRC = 2;
                        break;
                case emCommMode.ASCII:
                        break;
                default:
                    break;

            }
            if ((input.byFuntion == byREAD_COIL)||(input.byFuntion == byREAD_DISCRETE_INPUTS))
            {
                int nCount, nTemp = input.nDataLength;
                nCount = ((nTemp % 8) == 0) ? (nTemp / 8) : ((nTemp - (nTemp%8))/8+1);
                byArr = new byte[nHead + 1 + nCount + nCRC];
            }
            else if ((input.byFuntion == byREAD_HOLDING_REG)
                || (input.byFuntion == byREAD_INPUT_REG))
            {
                byArr = new byte[nHead + 1 +input.nDataLength * 2 + nCRC];
            }
            else if ((input.byFuntion == byWRITE_SINGLE_COIL)
                ||(input.byFuntion == byWRITE_MULTI_HOLDING_REG)
                ||(input.byFuntion == byWRITE_MULTI_COILS)
                ||(input.byFuntion == byWRITE_SINGLE_HOLDING_REG))
            {
                byArr = new byte[nHead + 4 + nCRC];
            }
        }
        private string BytesToStr(byte[] by)
        {
            string strRet = null;
            switch(m_nMsgFormat)
            {
                case emMsgFormat.Hex:
                    strRet = BitConverter.ToString(by);
                    break;
                case emMsgFormat.Decimal:
                    for (int i = 0; i < by.Length; i++ )
                    {
                        strRet += Convert.ToString(by[i], 10) +" ";
                    }
                    break;
                default:
                    break;
            }
            return strRet;
        }
        #endregion
        #region 供外部调用
        //断开连接
        public void Disconnect()
        {
            switch (m_nRunMode)
            {
                case emCommMode.RTU:
                    if (m_SerialPort != null)
                    {
                        m_SerialPort.Close();
                    }
                    break;
                case emCommMode.TCP:
                    if (thd_TcpConnect != null && thd_TcpConnect.IsAlive)
                    {
                        thd_TcpConnect.Abort();
                    }
                    if(m_socketTcp != null)
                    {
                        m_socketTcp.Disconnect(false);
                    }
                    break;
                default:
                    break;
            }
        }
        //连接
        public bool Connect()
        {
            switch(m_nRunMode)
            {
                case emCommMode.RTU:
                    return SPconnect();
                case emCommMode.TCP:
                    return TCPconnect(m_IP, m_nPort);
                default:
                    return false;
            }
        }

        //发送消息
        public void SendMessage(InputModule input)
        {
            m_InputQueue.Enqueue(input);
            if (!m_thd_SendMsg.IsAlive)
            {
                m_thd_SendMsg = new Thread(new ThreadStart(ThdProcSendMessage));
                m_thd_SendMsg.IsBackground = true;
                m_thd_SendMsg.Start();
            }            
        }

        // CRC校验
        public byte[] CRC16(byte[] byBuffer)
        {
            ushort CRCFull = 0xFFFF;
            byte CRCHigh = 0xFF, CRCLow = 0xFF;
            char CRCLSB;
            byte[] CRC = new byte[2];
            for (int i = 0; i < (byBuffer.Length) - 2; i++)
            {
                CRCFull = (ushort)(CRCFull ^ byBuffer[i]);

                for (int j = 0; j < 8; j++)
                {
                    CRCLSB = (char)(CRCFull & 0x0001);
                    CRCFull = (ushort)((CRCFull >> 1) & 0x7FFF);

                    if (CRCLSB == 1)
                        CRCFull = (ushort)(CRCFull ^ 0xA001);
                }
            }
            CRC[1] = CRCHigh = (byte)((CRCFull >> 8) & 0xFF);
            CRC[0] = CRCLow = (byte)(CRCFull & 0xFF);
            return CRC;
        }
        public string Dispose()
        {
            try
            {
                switch (m_nRunMode)
                {
                    case emCommMode.TCP:
                        {
                            if (m_socketTcp != null)
                            {
                                if (m_socketTcp.Connected)
                                {
                                    try
                                    {
                                        m_socketTcp.Shutdown(SocketShutdown.Both);
                                        m_socketTcp.Disconnect(false);
                                    }
                                    catch (System.Exception ex)
                                    {
                                        throw ex;
                                    }

                                }
                                m_socketTcp.Dispose();
                                m_socketTcp = null;
                            }
                        }
                        break;
                    case emCommMode.RTU:
                        {
                            if ((m_SerialPort != null) && (m_SerialPort.IsOpen))
                            {
                                m_SerialPort.Close();
                            }
                            m_SerialPort.Dispose();
                            m_SerialPort = null;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
            return null;
        }
        #endregion
        ~Modbus()
        {
            Dispose();
        }
    }
    public class InputModule
    {
       public byte bySlaveID;
       public int nStartAddr;
       public byte byFuntion;
       public int nDataLength;
       public byte[] byWriteData;
    }
    public class OutputModule 
    {
        public byte bySlaveID;
        public int nStartAddr;
        public byte byFuntion;
        public int nDataLength;
        public byte[] byRecvData;
    }
    public class IEEEConverter
    {
        /// <summary>
        ///   将浮点数转化成协议规定格式的数据
        ///   参数1：f 需要转化的浮点数，
        ///   参数2：style为1时转化成16进制数据字符串，为2时转化成2进制数据字符串
        /// </summary>
        public static string FloatToStandard(double f, int style)
        {
            char[] Num = new char[32];//32为二进制结果加1结束符

            if (f < 0) //f为负数 ,符号位Num[0]赋1
            {
                Num[0] = '1';
                f = f * (-1); //转换为整数
            }
            else
                Num[0] = '0';

            //求指数
            int exponent = 0;
            if (f >= 1)//指数符号为正情况，即fabs(f)>=1 
            {
                while (f >= 2)
                {
                    exponent++;
                    f = f / 2;
                }
            }
            else//负指数情况
            {
                while (f < 1)
                {
                    exponent++;
                    f = f * 2;
                }
                exponent = (128 - exponent) + 128;//(128-exponent)为补码,+128是指数符号位置1
            }

            exponent += 127;//指数转换为阶码

            int i;
            for (i = 8; i >= 1; i--)//将指数转换为二进制数
            {
                if (exponent % 2 == 0)
                    Num[i] = '0';
                else
                    Num[i] = '1';
                exponent /= 2;
            }

            f = f - 1;//小数转换为标准格式
            //求小数部分
            double s = 1;
            for (i = 9; i < 32; i++)
            {
                s /= 2;
                if (f >= s)
                {
                    //MessageBox.Show(Convert.ToString(s));
                    f = f - s;
                    Num[i] = '1';
                }
                else
                    Num[i] = '0';
            }

            if (style == 2)//二进制输出
            {
                string tt = new string(Num);
                return tt;
            }
            else //二进制转换为十六进制
            {
                char sum;
                int j = 0;
                for (i = 0; i < 32; i = i + 4, j++)
                {
                    sum = Convert.ToChar((Num[i] - '0') * 8 + (Num[i + 1] - '0') * 4 + (Num[i + 2] - '0') * 2 + (Num[i + 3] - '0'));
                    if (sum > 9)
                        Num[j] = Convert.ToChar((sum - 10) + 'a');
                    else
                        Num[j] = Convert.ToChar(sum + '0');
                }
                string tt = new string(Num);
                tt = tt.Substring(0, 8);
                return tt;
            }
        }
        /// <summary>
        ///   将协议规定格式的数据转化成浮点型数据
        ///   参数1：Fnum需要转化的数据字符串
        ///   参数2：style为1表示是16进制表示的字符串，为2表示是2进制表示的字符串
        /// </summary>
        public static double StandardToFloat(string FNum, int style)
        {
            char[] Num = new char[32];
            char[] Hex = new char[8];

            int i, j, value;

            if (style == 2)//二进制
            {
                for (i = 0; i < 32; i++)
                    Num[i] = FNum[i];
            }
            else//十六进制转换为二进制
            {
                for (i = 0; i < 8; i++)
                {
                    Hex[i] = FNum[i];
                    if (Hex[i] >= '0' && Hex[i] <= '9')
                        value = Hex[i] - '0';
                    else
                    {
                        Hex[i] = Convert.ToChar(Hex[i] | 0x20); //统一转换为小写
                        value = Hex[i] - 'a' + 10;
                    }
                    for (j = 3; j >= 0; j--)
                    {
                        Num[i * 4 + j] = Convert.ToChar((value % 2) + '0');
                        value /= 2;
                    }
                }
            }

            double f = 1;//f为最终浮点数结果,标准浮点格式隐含小数点前的1
            double s = 1;//s为小数各个位代表的权值

            for (i = 9; i < 32; i++)//转换小数部分
            {
                s /= 2;
                if (Num[i] == '1')
                {
                    f += s;
                    //MessageBox.Show(Convert.ToString(s));
                }
            }

            int exponent = 0;//指数部分
            int d = 1;//d代表指数部分各位的权值
            for (i = 8; i >= 1; i--)//此时计算的是阶码
            {
                if (Num[i] == '1')
                    exponent += d;
                d *= 2;
            }
            if (exponent >= 127)
                exponent -= 127;//将阶码转换为指数
            else
            {
                exponent += 256;//补上指数变阶码溢出的最高位
                exponent -= 127;//阶码变指数,指数为补码
                exponent = 128 - (exponent - 128);//(exponent-128)去符号位,在讲补码变为指数绝对值
                exponent = exponent * (-1);//最终指数部分
            }

            if (Num[0] == '1')//浮点数符号位为1,说明为负数
                f = f * (-1);

            f = f * Math.Pow(2, exponent);//将小数部分和浮点部分组合

            return f;
        }
    }
}
