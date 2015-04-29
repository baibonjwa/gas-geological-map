// ******************************************************************
// 概  述：Socket客户端
// 作  者：杨小颖
// 创建日期：2014/03/16
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCommon;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace LibSocket
{
    public class ClientSocket
    {
        private const int MAX_RECV_LEN = 4096;//最大接收长度

        //服务器IP
        private string _serverIP = "127.0.0.1";
        //端口号
        private int _serverPort = 8888;
        //Socket
        private Socket _socketHandler = null;
        //是否启用重连线程
        private bool _enableReconnectThread = false;
        //重连线程
        Thread _threadReconnect = null;
        //处理服务端消息进程
        Thread _threadServerMsg = null;

        /// <summary>
        /// 获取客户端Socket
        /// </summary>
        /// <returns>客户端未连接至服务器时，该返回值可能为null</returns>
        public Socket GetClientSocket()
        {
            return _socketHandler;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serverIP">服务器IP</param>
        /// <param name="serverPort">服务器端口号</param>
        /// <param name="log">日志</param>
        public ClientSocket(string serverIP, int serverPort)
        {
            _serverIP = serverIP;
            _serverPort = serverPort;
        }

        public string ServerIP
        {
            get { return this._serverIP; }
        }

        /// <summary>
        /// 启动重连线程
        /// </summary>
        private void StartReconnectThread()
        {
            try
            {
                //是否需要启动线程
                bool startThread = false;

                if (_threadReconnect == null)
                {
                    startThread = true;
                }
                else
                {
                    //线程是否已经启动
                    if (_threadReconnect.IsAlive == false)
                    {
                        startThread = true;
                    }
                }

                if (startThread)
                {
                    _threadReconnect = new Thread(Reconnect2ServerThread);
                    _enableReconnectThread = true;
                    _threadReconnect.IsBackground = true;
                    Log.Debug("重连线程已启动，正在尝试连接服务器...");
                    _threadReconnect.Start();
                }
            }
            catch (System.Exception ex)
            {
                Log.Debug("启动重连线程失败：" + ex.Message);
            }
        }

        /// <summary>
        /// 重新连接服务器线程
        /// </summary>
        private void Reconnect2ServerThread()
        {
            while (true)
            {
                if (_enableReconnectThread == true)
                {
                    string exception = Connect2Server();
                    if (exception != "")
                    {
                        _enableReconnectThread = true;
                    }
                    else
                    {
                        _enableReconnectThread = false;

                        Log.Debug("重连服务器成功！");
                        return;
                    }
                }
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// 连接到服务器
        /// </summary>
        /// <returns>返回异常信息</returns>
        public string Connect2Server()
        {
            string retException = "";
            try
            {
                IPAddress ipAdd = null;
                if (!IPAddress.TryParse(_serverIP, out ipAdd))
                {
                    retException = "IP地址不正确！";
                    return retException;
                }
                _socketHandler = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //_socketHandler.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.KeepAlive, true);

                _socketHandler.Connect(_serverIP, _serverPort);

                //连接成功即启动处理服务器发送过来的消息线程
                if (_socketHandler.Connected == true)
                {
                    Log.Debug("连接服务器成功！");
                    StopServerMsgThread();
                    _threadServerMsg = new Thread(HandleReceivedData);
                    _threadServerMsg.IsBackground = true;
                    _threadServerMsg.Start();
                }
            }
            catch (System.Exception ex)
            {
                retException = ex.Message;
                Log.Error(ex.Message);
            }
            if (retException != "")
            {
                //启动重连线程（该函数内部会自动判断该线程是否已经启动或正在运行）
                StartReconnectThread();
            }
            return retException;
        }

        /// <summary>
        /// 向Server发送数据
        /// </summary>
        /// <param name="msg"></param>
        /// <returns>返回异常信息</returns>
        private string SendMsg2Server(string msg)
        {
            msg += "\r";//添加换行符，否则与java通信则不能正常发送消息。如果客户端发送的原始信息包含\r\n，发送后会自动连成一个字符串。
            string retException = "";
            if (_socketHandler == null)//尚未连接至服务器，应该首先调用Connect2Server进行连接
            {
                retException = "尚未连接至服务器！";
                return retException;
            }
            try
            {
                IPAddress ipAdd = null;
                if (!IPAddress.TryParse(_serverIP, out ipAdd))
                {
                    retException = "IP地址不正确！";
                    return retException;
                }
                if (_socketHandler.Connected == false)
                {
                    //如果客户端已经断开了连接
                    return "客户端已经与服务器断开连接！";
                }
                byte[] bytesSend = System.Text.Encoding.Default.GetBytes(msg);
                _socketHandler.Send(bytesSend);
            }
            catch (System.Exception ex)
            {
                retException = ex.Message;
            }
            return retException;
        }

        public string SendSocketMsg2Server(SocketMessage msg)
        {
            string jsonMsg = JsonConvert.SerializeObject(msg);
            return SendMsg2Server(jsonMsg);
        }

        /// <summary>
        /// 停止处理服务器消息线程
        /// </summary>
        private void StopServerMsgThread()
        {
            try
            {
                if (_threadServerMsg != null)
                {
                    _threadServerMsg.Abort();
                    _threadServerMsg = null;
                }
            }
            catch (Exception ex)
            {
                Log.Debug(ex.Message);
            }
        }

        private void StopReconnectThread()
        {
            try
            {
                if (_threadReconnect != null)
                {
                    _threadReconnect.Abort();
                    _threadReconnect = null;
                }
            }
            catch (Exception ex)
            {
                Log.Debug(ex.Message + " in function StopReconnectThread");
            }
        }
        /// <summary>
        /// 清理Socket
        /// </summary>
        public void CleanupSocket()
        {
            try
            {
                if (_socketHandler == null)
                {
                    return;
                }
                if (_socketHandler.Connected)
                {
                    Log.Debug("关闭客户端Socket：" + ((IPEndPoint)_socketHandler.LocalEndPoint).ToString());
                    _socketHandler.Disconnect(false);
                    _socketHandler.Shutdown(SocketShutdown.Both);
                }
                _socketHandler.Close();
                //停止处理服务器消息进程
                StopServerMsgThread();
                //停止重连线程
                StopReconnectThread();
            }
            catch (Exception ex)
            {
                Log.Debug("关闭客户端Socket失败：" + ex.Message);
            }
        }


        /// <summary>
        /// 处理来自服务端的数据
        /// </summary>
        /// <returns></returns>
        public void HandleReceivedData()
        {
            byte[] recvData = new byte[ClientSocket.MAX_RECV_LEN];
            while (true)
            {
                try
                {
                    if (_socketHandler.Connected == false)
                    {
                        StartReconnectThread();
                        return;
                    }
                    if (_socketHandler.Poll(-1, SelectMode.SelectRead))
                    {
                        int recvDataLen = _socketHandler.Receive(recvData);
                        string strDatas = System.Text.Encoding.Default.GetString(recvData, 0, recvDataLen);
                        //_log.Log("Received data: " + strDatas + " Length: " + recvDataLen.ToString());
                        if (strDatas == null || strDatas == "")
                        {
                            continue;
                        }
                        //通过Json先获取命令ID，然后根据不同ID将其转换为相应的实体类型
                        JObject jo = (JObject)JsonConvert.DeserializeObject(strDatas);
                        COMMAND_ID cmdId = COMMAND_ID.UNDEFINED;
                        if (jo[ConstSocketStr.COMMADN_ID] != null)
                        {
                            if (!Enum.TryParse<COMMAND_ID>(jo[ConstSocketStr.COMMADN_ID].ToString(), out cmdId))
                            {
                                Log.Error("无法解析Socket命令-->" + strDatas);
                                continue;
                            }
                        }
                        else
                        {
                            Log.Debug("无法解析Socket命令-->" + strDatas);
                            continue;
                        }
                        //更新预警结果
                        if (cmdId == COMMAND_ID.NTFY_WARNING_RESULT ||
                            cmdId == COMMAND_ID.WARNING_RESULT_SINGLE)
                        {
                            #region //test
                            //_log.Log("COMMAND 'NTFY_WARNING_RESULT ' RECEIVED!");
                            //UpdateWarningDataMsg warningResult = (UpdateWarningDataMsg)JsonConvert.DeserializeObject<UpdateWarningDataMsg>(strDatas);
                            //_log.Log("tunnelID: " + warningResult.TunnelId +
                            //    ", commandID: " + warningResult.CommandId.ToString() +
                            //    ", DateTime: " + warningResult.DTime);
                            #endregion

                            UpdateWarningResultMessage resultMsg = JsonParser.Convert2WarningResultMsg(strDatas);
                            //调用处理事件
                            OnMsgUpdateWarningResult(resultMsg);
                        }
                        else
                        {
                            Log.Debug("未处理命令：" + cmdId.ToString() + " --> " + strDatas);
                            //未定义命令类型
                            if (cmdId == COMMAND_ID.UNDEFINED)
                            {
                            }
                            //同步时间
                            if (cmdId == COMMAND_ID.SYNC_DATETIME)
                            {

                            }
                            //心跳检测
                            if (cmdId == COMMAND_ID.KEEP_ALIVE)
                            {
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Log.Error("Error in Function HandleReceivedData: " + ex.Message);
                }
            }
        }

        #region CommandId对应的处理函数
        public delegate void UpdateWarnigResultHandler(UpdateWarningResultMessage data);
        public event UpdateWarnigResultHandler OnMsgUpdateWarningResult;
        #endregion
    }
}

