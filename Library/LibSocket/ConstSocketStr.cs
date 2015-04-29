// ******************************************************************
// 概  述：Socket
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

namespace LibSocket
{
    /// <summary>
    /// Socket消息基类
    /// 每条Socket消息均包含Socket命令（SOCKET_COMMAND）、预警数据更新时间（注意：不一定是系统当前时间）
    /// </summary>
    public static class ConstSocketStr
    {
        //Json解析命令ID属性值
        public const string COMMADN_ID = "CommandId";

        /** 日期格式 **/
        public const string DATE_FORMART_YYYY_MM_DD = "yyyy-MM-dd HH:mm:ss";
    }
}

