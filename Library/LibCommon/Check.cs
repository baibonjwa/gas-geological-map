// ******************************************************************
// 概  述：输入验证类
// 作  者：伍鑫
// 创建日期：2014/01/21
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace LibCommon
{
    public class Check
    {
        /** 必须录入Check Message **/
        private static string MESSAGE_EMPTY = "不能为空！";
        /** 是否存在Check Message **/
        private static string MESSAGE_EXIST = "已存在，请重新录入！";
        /** 是否是数字Check Message **/
        private static string MESSAGE_NUMERIC = "应为数字！";
        /** 是否选择Check Message **/
        private static string MESSAGE_SELECT = "请选择";
        /** 是否是特殊字符Check Message **/
        private static string MESSAGE_SPECIAL_CHARACTERS = "包含特殊字符！";
        /** 感叹号“！” **/
        private static string MESSAGE_EXCLAMATION = "！";


        /// <summary>
        /// 判断是否录入
        /// </summary>
        /// <param name="controlName">控件名</param>
        /// <param name="messageSub">提示信息主题</param>
        /// <returns></returns>
        public static bool isEmpty(Control controlName, String messageSub)
        {
            if (controlName.GetType() == typeof(TextBox))
            {
                TextBox tb = controlName as TextBox;
                if (Validator.IsEmpty(controlName.Text.Trim()))
                {
                    controlName.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(messageSub + MESSAGE_EMPTY);
                    controlName.Focus();
                    return false;
                }
                else
                {
                    controlName.BackColor = Const.NO_ERROR_FIELD_COLOR;
                    return true;
                }
            }

            if (controlName.GetType() == typeof(ComboBox))
            {
                ComboBox cb = controlName as ComboBox;
                if (Validator.IsEmpty(controlName.Text.Trim()))
                {
                    Alert.alert(MESSAGE_SELECT + messageSub + MESSAGE_EXCLAMATION);
                    return false;
                }
            }

            // 验证通过
            return true;
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="controlName">控件名</param>
        /// <param name="messageSub">提示信息主题</param>
        /// <param name="checkResult">是否存在</param>
        /// <returns></returns>
        public static bool isExist(Control controlName, String messageSub, bool checkResult)
        {
            if (controlName.GetType() == typeof(TextBox))
            {
                TextBox tb = controlName as TextBox;
                if (checkResult)
                {
                    controlName.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(messageSub + MESSAGE_EXIST);
                    controlName.Focus();
                    return false;
                }
                else
                {
                    controlName.BackColor = Const.NO_ERROR_FIELD_COLOR;
                    return true;
                }
            }

            // 验证通过
            return true;
        }

        /// <summary>
        /// 判断是否是数字
        /// </summary>
        /// <param name="controlName">控件名</param>
        /// <param name="messageSub">提示信息主题</param>
        /// <returns></returns>
        public static bool IsNumeric(Control controlName, String messageSub)
        {
            if (controlName.GetType() == typeof(TextBox))
            {
                TextBox tb = controlName as TextBox;
                if (!Validator.IsNumeric(controlName.Text.Trim()))
                {
                    controlName.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(messageSub + MESSAGE_NUMERIC);
                    controlName.Focus();
                    return false;
                }
                else
                {
                    controlName.BackColor = Const.NO_ERROR_FIELD_COLOR;
                    return true;
                }
            }

            // 验证通过
            return true;
        }

        /// <summary>
        /// 检查特殊字符
        /// </summary>
        /// <param name="controlName">控件名</param>
        /// <param name="messageSub">提示信息主题</param>
        /// <returns></returns>
        public static bool checkSpecialCharacters(Control controlName, String messageSub)
        {
            //Regex regExp = new Regex("[~!@#$%^&*()=+[\\]{}''\";:/?.,><`|！·￥…—（）\\、；：。，》《]");
           Regex regExp = new Regex("[~!@#$%^&*()=+[\\]{}''\";:?.,><`|！·￥…—（）\\、；：。，》《]"); // 过滤掉'/'

           if (controlName.GetType() == typeof(TextBox))
           {
               TextBox tb = controlName as TextBox;
               if (Validator.checkSpecialCharacters(controlName.Text.Trim()))
               {
                   controlName.BackColor = Const.ERROR_FIELD_COLOR;
                   Alert.alert(messageSub + MESSAGE_SPECIAL_CHARACTERS);
                   controlName.Focus();
                   return false;
               }
               else
               {
                   controlName.BackColor = Const.NO_ERROR_FIELD_COLOR;
                   return true;
               }
           }
           // 验证通过
           return true;
        }
    }
}
