// ******************************************************************
// 概  述：Json字符串解析类
// 作  者：杨小颖
// 创建日期：2014/03/19
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.IO;
using LibSocket;
using Newtonsoft.Json;

namespace LibSocket
{
    public static class JsonParser
    {
        /// <summary>
        /// 将Json字符串转为预警结果信息
        /// </summary>
        /// <param name="jsonTxt">Socket传送过来的Json字符串</param>
        /// <returns></returns>
        static public UpdateWarningResultMessage Convert2WarningResultMsg(string jsonTxt)
        {
            UpdateWarningResultMessage warningResult = (UpdateWarningResultMessage)JsonConvert.DeserializeObject<UpdateWarningResultMessage>(jsonTxt);
            return warningResult;
        }

        /// <summary>
        /// 将Json字符串转为预警数据信息
        /// </summary>
        /// <param name="jsonTxt">Socket传送过来的Json字符串</param>
        /// <returns></returns>
        static public UpdateWarningDataMsg Convert2WarningDataMsg(string jsonTxt)
        {
            UpdateWarningDataMsg ret = (UpdateWarningDataMsg)JsonConvert.DeserializeObject<UpdateWarningDataMsg>(jsonTxt);
            return ret;
        }

    }
}
