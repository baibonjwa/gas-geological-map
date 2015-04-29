// ******************************************************************
// 概  述：GUID生成类
// 作  者：杨小颖
// 创建日期：2014/03/04
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;

namespace LibCommon
{
    public class IDGenerator
    {
        /// <summary>
        /// 生成新的绑定ID（GUID）
        /// </summary>
        /// <returns>返回生成的绑定ID（全球唯一)</returns>
        static public string NewBindingID()
        {
            return System.Guid.NewGuid().ToString();
        }
    }
}
