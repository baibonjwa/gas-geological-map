
using System.Text;
using System.Text.RegularExpressions;
using System.Data;

namespace LibCommon
{
    public class Validator
    {
        /// <summary>
        /// 判断是否为空 (判断是否为NULL或者"")
        /// </summary>
        public static bool IsEmpty(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
              return true;
            }
            return false;
        }

        /// <summary>
        /// 判断是否为空(判断是否为NULL，""，" ")
        /// </summary>
        public static bool IsEmptyOrBlank(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断是不是数字
        /// </summary>
        public static bool IsNumeric(string expression)
        {
            if (expression == ".")
            {
                return false;
            }

            if (expression != null && expression != "")
            {
                string str = expression;
                if (str.Length > 0 && Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 检查特殊字符
        /// </summary>
        public static bool checkSpecialCharacters(string expression)
        {
            Regex regExp = new Regex("[~!@#$%^&*()=+[\\]{}''\";:/?.,><`|！·￥…—（）\\、；：。，》《]");
            return regExp.IsMatch(expression);
        }

        /// <summary>
        /// 检查字符串格式是否符合电话号码类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool checkIsIsTelePhone(string str)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str, @"^(\d{3,4}-)?\d{6,8}$");
        }

        /// <summary>
        /// 验证手机号是否正确
        /// </summary>
        /// <param name="str_handset">手机号码字符串</param>
        /// <returns>返回布尔值</returns>
        public static bool checkIsPhoneNumber(string str)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str, @"^[1]+[3,8]+\d{9}$");
        }

        /// <summary>
        /// 检查密码是否含有除数字、字母之外的字符
        /// </summary>
        /// <param name="str_password"></param>
        /// <returns>返回值为“true”则不包含</returns>
        public static bool checkPassWordIsWordsAndNumber(string str_password)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_password, @"[A-Za-z]+[0-9]");
        }

        /// <summary>
        /// 验证Email格式是否正确
        /// </summary>
        /// <param name="str_Email">Email地址字符串</param>
        /// <returns>方法返回布尔值</returns>
        public static bool checkIsEmailAddress(string str_Email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_Email,@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 验证密码长度是否正确
        /// </summary>
        /// <param name="str_Length">密码字符串</param>
        /// <returns>方法返回布尔值</returns>
        public static bool checkIsPasswordLength(string str_Length)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_Length, @"^\d{0,32}$");
        }

        /// <summary>
        /// 验证输入是否为正整数
        /// </summary>
        /// <param name="str_intNumber">用户输入的数值</param>
        /// <returns>方法返回布尔值</returns>
        public static bool IsIntPositiveNumber(string str_intNumber)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_intNumber, @"^\+?[0-9][0-9]*$");
        }
    }
}
