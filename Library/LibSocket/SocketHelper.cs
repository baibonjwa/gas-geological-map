// ******************************************************************
// 概  述：Socket帮助类
// 作  者：杨小颖
// 创建日期：2014/03/17
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System.Net;
using System.Net.Sockets;

namespace LibSocket
{
    public class SocketHelper
    {
        public static IPAddress GetLocalMachineIPv4Address()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = null;
            int numIP = ipHostInfo.AddressList.Length;
            for (int i = 0; i < numIP; i++)
            {
                if (ipHostInfo.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ipHostInfo.AddressList[i];
                    break;
                }
            }
            return ipAddress;
        }

        public static bool IsIpAddress(string ip)
        {
            try
            {
                IPAddress IpAddress = IPAddress.Parse(ip);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsPortNumber(string portNumber, out int port)
        {
            try
            {
                if (!int.TryParse(portNumber, out port))
                {
                    return false;
                }
                if (port >= 1024 && port <= 65535)
                    return true;
                else
                    return false;
            }
            catch
            {
                port = -1;
                return false;
            }
        }

        public static string InitClientSocket(string Ip, int Port, out ClientSocket clientSocket)
        {
            string errorMsg = "";

            clientSocket = new ClientSocket(Ip, Port);
            //连接至服务器
            errorMsg = clientSocket.Connect2Server();

            return errorMsg;
        }
    } 
}

