// ******************************************************************
// 概  述：字符串拆分类
// 作  者：杨小颖
// 创建日期：2013/11/26
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LibCommon
{
    public class StringSplitor
    {      
        /// <summary>
        /// 正则表达式解析两个字符串间的内容，如：获取括号内的内容
        /// </summary>
        /// <param name="str"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        //static public string[] ParseRuleDescriptionParams(string str, string start, string end)
        //{
        //    Regex rg = new Regex("(?<=(" + start + "))[.\\s\\S]*?(?=(" + end + "))", RegexOptions.Multiline | RegexOptions.Singleline);
        //    MatchCollection mc = rg.Matches(str);
        //    if (mc.Count < 1)
        //    {
        //        return null;
        //    }
        //    string[] ret = new string[mc.Count];
        //    for (int i = 0; i < mc.Count; i++)
        //    {
        //        ret[i] = mc[i].Value;
        //    }
        //    return ret;
        //}


        /// <summary>
        /// 正则表达式解析两个字符串间的内容，如：获取括号内的内容
        /// </summary>
        /// <param name="str"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        static public string[] ParseRuleDescriptionParams(string str, string start, string end)
        {
            Regex rg = new Regex("(?<=(" + @"\" + start + "))[.\\s\\S]*?(?=(" + @"\" + end + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            MatchCollection mc = rg.Matches(str);
            if (mc.Count < 1)
            {
                return null;
            }
            string[] ret = new string[mc.Count];
            for (int i = 0; i < mc.Count; i++)
            {
                ret[i] = mc[i].Value;
            }
            return ret;
        }

    }
}
