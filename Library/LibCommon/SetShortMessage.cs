// ******************************************************************
// 概  述：
// 作  者：
// 创建日期：
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using LibCommon;
namespace LibCommon
{
   public static class SetShortMessage
    {

        //'短信收发二次开发接口(标准版)函数使用说明
        //'Sms_Connection函数说明如下：
        //'功能描述：用于初始化移动电话与串口的连接
        //'Com_Port：串口号
        //'Com_BaudRate：波特率
        //'Mobile_Type：返回移动电话型号
        //'Sms_Connection：返回值(0：连接移动电话失败；1：连接移动电话成功)
        [DllImport("sms.dll", EntryPoint = "Sms_Connection")]
        public static extern int Sms_Connection(string CopyRight, short Com_Port, short Com_BaudRate, string Mobile_Type, string CopyRightToCOM);

        //'Sms_Send函数说明如下：
        //'功能描述：发送短信
        //'Sms_TelNum：发送给的移动电话号码
        //'Sms_Text：发送的短信内容
        //'Sms_Connection：返回值(0：发送短信失败；1：发送短信成功)
        [DllImport("sms.dll", EntryPoint = "Sms_Send")]
        public static extern int Sms_Send(string Sms_TelNum, string Sms_Text);

        //'Sms_Receive函数说明如下：
        //'功能描述：接收指定类型的短信
        //'Sms_Type：短信类型(0：未读短信；1：已读短信；2：待发短信；3：已发短信；4：全部短信)
        //'Sms_Text：返回指定类型的短信内容字符串(短信内容字符串说明：短信与短信之前用"|"符号作为分隔符,每条短信中间的各字段用"#"符号作为分隔符)
        [DllImport("sms.dll", EntryPoint = "Sms_Receive")]
        public static extern int Sms_Receive(string Sms_Type, string Sms_Text);

        //'Sms_Delete函数说明如下：
        //'功能描述：删除指定的短信
        //'Sms_Index：短信的索引号
        [DllImport("sms.dll", EntryPoint = "Sms_Delete")]
        public static extern int Sms_Delete(string Sms_Index);

        //'Sms_AutoFlag函数说明如下：
        //'功能描述：检测连接的移动电话是否支持自动收发短信功能
        //'Sms_AutoFlag：返回值(0：不支持；1：支持)
        [DllImport("sms.dll", EntryPoint = "Sms_AutoFlag")]
        static extern int Sms_AutoFlag();

        //    'Sms_NewFlag函数说明如下：
        //'功能描述：查询是否收到新的短信息
        //'Sms_AutoFlag：返回值(0：未收到；1：收到)
        [DllImport("sms.dll", EntryPoint = "Sms_NewFlag")]
        public static extern int Sms_NewFlag();

        //    'Sms_Disconnection函数说明如下：
        //'功能描述：断开移动电话与串口的连接
        [DllImport("sms.dll", EntryPoint = "Sms_Disconnection")]
        public static extern int Sms_Disconnection();
    }
}
