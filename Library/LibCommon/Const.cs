// ******************************************************************
// 概  述：全局常量类
// 作  者：伍鑫
// 创建日期：2013/11/28
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LibCommon
{
    public class Const
    {
        #region Socket相关
        public const string SEND_MSG_FAILED = "发送Socket数据失败！";
        public const string CONNECT_ARROW = "-->";
        public const string CLIENT_SOCKET_IS_NULL = "客户端Socket未正常初始化！";
        #endregion

        #region 提示信息
        public const string CONNECT_SOCKET_ERROR = "连接服务器失败,数据可以正常录入,但预警结果将无法及时更新！";
        public const string READ_SOCKET_CONFIG_FAILED = "读取客户端配置信息失败！";
        public const string LOG_MSG_CONNECT_SUCCEED = "连接服务器成功！";
        #endregion

        /** 区域颜色 Error**/
        public static Color ERROR_FIELD_COLOR = Color.Red;

        /** 区域颜色 No_Error **/
        public static Color NO_ERROR_FIELD_COLOR = Color.White;

        /** 未绑定导线巷道背景颜色 **/
        public static Color NO_WIRE_TUNNEL_COLOR = Color.Silver;

        /**绑定导线巷道背景颜色**/
        public static Color WIRED_TUNNEL_COLOR = Color.White;

        /** 定义为掘进巷道的巷道背景颜色**/
        public static Color JJ_TUNNEL_COLOR = Color.LightSalmon;

        /** 定义为回采巷道的巷道背景颜色 **/
        public static Color HC_TUNNEL_COLOR = Color.LightGreen;

        public const string MSG_YES = "是";

        public const string MSG_NO = "否";

        /** 提交成功MSG **/
        public const String SUCCESS_MSG = "提交成功！";

        /** 提交失败MSG **/
        public const String FAILURE_MSG = "提交失败！";

        /** 提交工作面同名MSG **/
        public const String WORKINGFACENAMESAME_MSG = "工作面已存在！";

        /** 预警值 **/
        public const Double WARN_VALUE = 4.5;

        /** 删除成功MSG **/
        public const String DEL_SUCCESS_MSG = "删除成功！";

        /** 删除失败MSG **/
        public const String DEL_FAILURE_MSG = "删除失败！";

        /** 日期格式 **/
        public const String DATE_FORMART_YYYY_MM_DD = "yyyy/MM/dd HH:mm:ss";

        /** 日期格式 **/
        public const String DATE_FORMART_YYYY_MM = "yyyy年MM月";

        /** 日期格式 **/
        public const String DATE_FORMART_H_MM_SS = "yyyy-MM-dd H:mm:ss";

        /** 导出成功MSG **/
        public const String EXPORT_SUCCESS_MSG = "导出成功！";

        /** 导出失败MSG **/
        public const String EXPORT_FAILURE_MSG = "导出失败！";

        /** Double值空值时的默认值 **/
        public const String DOUBLE_DEFAULT_VALUE = "0.000";

        /** 单行颜色 **/
        public static Color SINGLE_LINE = Color.DarkCyan;

        /** 双行颜色 **/
        public static Color DOUBLE_LINE = Color.LightCyan;

        /** 删除确认信息 **/
        public const String DEL_CONFIRM_MSG = "确认删除？";

        /**非空**/
        public const string MSG_NOT_NULL = "不能为空";

        /**数字验证**/
        public const string MSG_MUST_NUMBER = "应为数字";

        /**特殊字符验证**/
        public const string MSG_SP_CHAR = "不能包含特殊字符";

        /**已存在**/
        public const string MSG_ALREADY_HAVE = "已存在";

        /**重复**/
        public const string MSG_DOUBLE_EXISTS = "重复";

        /**请选择**/
        public const string MSG_PLEASE_CHOOSE = "请选择";

        /**请输入**/
        public const string MSG_PLEASE_TYPE_IN = "请输入";

        /**！**/
        public const char SIGN_EXCLAMATION_MARK = '！';

        /**数据获取失败**/
        public const string DATA_GET_FAILURE = "";

        /**空值**/
        public const string DATA_NULL = "";

        //Farpoint默认行数
        public const int FARPOINT_DEFAULT_ROW_COUNT = 50;

        //Farpoint默认列数
        public const int FARPOINT_DEFAULT_COLUMN_COUNT = 25;

        /** 处理标识位 **/
        public const int DISPOSE_FLAG_ZERO = 0;
        public const int DISPOSE_FLAG_ONE = 1;

        /** combox默认不选index **/
        public const int UN_SELECT_INDEX = -1;

        /** number **/
        public const int NUM_ZERO = 0;

        public const string NOTES = "提示";

        /** 所属煤层必须选择提示信息 **/
        public const string COALSEAMS_MUST_SELECT = "请选择所在煤层！";

        /// <summary>
        /// 第N行，X列不能为空
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        /// <param name="columnName">列索引</param>
        /// <returns>提示信息</returns>
        public static string rowNotNull(int rowIndex, string columnName)
        {
            string msg = "第" + (rowIndex + 1) + "行，" + columnName + "不能为空！";
            return msg;
        }

        /// <summary>
        /// 第N行，X列必须为数字
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        /// <param name="columnName">列索引</param>
        /// <returns>提示信息</returns>
        public static string rowMustBeNumber(int rowIndex, string columnName)
        {
            string msg = "第" + (rowIndex + 1) + "行, " + columnName + " 必须为数字！";
            return msg;
        }

        #region 登录界面相关提示信息
        //定义登录界面背景
        public const string LOGIN_BACKGROUND_BMP_PATH = "\\LoginBackground.bmp";

        public const string USER_NAME_OR_PWD_ERROR_MSG = "用户名或密码错误！";

        public const string LOGIN_FAILED_TITLE = "登录失败";

        public const string ADD_USER_INFO = "尚未设置权限信息，请先创建用于登录的用户信息！";

        public const string DELETE_USER_INFO = "确认要删除用户信息吗？";

        //定义是否初次登陆
        public static bool FIRST_TIME_LOGIN = false;
        //定义初次登陆用户名
        public static string FIRST_LOGIN_NAME = "";
        //定义初次登陆密码
        public static string FIRST_LOGIN_PASSWORD = "";
        //定义初次登陆用户权限
        public static string FIRST_LOGIN_PERMISSION = LibCommon.Permission.普通用户.ToString();
        //定义是与否枚举
        public enum TrueOrFalse
        {
            True,
            False
        }
        #endregion

        #region 用户登录信息界面相关信息
        public const string LOGIN_NAME_IS_WRONG = "用户名不能为空,且不能包含特殊字符！";

        public const string PASS_WORD_IS_WRONG = "密码不能为空,且不能包含特殊字符！";

        public const string CONFIRM_PASSWORD_IS_WRONG = "确认密码与输入密码不相同！";

        public const string PERMISSION_IS_WRONG = "用户权限不能为空！";

        public const string LOGIN_NAME_EXIST = "用户登录名已存在！";

        public const char PASSWORD_CHAR = '*';

        public const string YES = "√";

        public const string NO = "×";
        #endregion

        #region 用户详细信息界面相关提示信息
        public const string NAME_IS_WRONG = "姓名不能为空,且不能包含特殊字符！";

        public const string TEL_IS_WRONG = "          电话号码格式不正确！\n区号（3-4位） - 电话号码（6-8位）";

        public const string PHONE_IS_WRONG = "     手机号码格式不正确！\n首数字为 1 的，十一位数字";

        public const string EMAIL_IS_WRONG = "邮箱格式不正确！";

        public const char CHAR_IN_PHONENUMBER = '-';

        public const string DELETE_STUFF_INFO = "确认要删除人员信息吗？";

        #endregion

        #region 部门信息界面相关信息
        public const string DEPT_NAME_IS_WRONG = "部门名称不能为空、不能包含特殊字符，且不能重复！";

        public const string DEPT_EMAIL_IS_WRONG = "部门邮箱格式错误！";

        public const string DEPT_TEL_IS_WRONG = "部门电话格式错误！";

        public const string DEPT_STAFF_COUNT_IS_WRONG = "部门人数类型为数字！";
        #endregion

        #region 用户组管理界面相关信息
        public const string USER_GROUP_NAME_IS_WRONG = "用户组名称不能为空、不能包含特殊字符且不能重复，请重新输入！";
        public const string USER_GROUP_NAME_LOGIN_NAME = "登录名";
        public const string USER_GROUP_NAME_LOGIN_NAME_USER_GROUP = "所属用户组";
        public const string USER_GROUP_NAME_PERMISSION = "权限";
        #endregion

        #region Farpoint Filter相关信息
        public const string ALL_STRING = "(取消过滤)";
        public const string BLANK_STRING = "(空白行)";
        public const string NONBLANK_STRING = "(非空白行)";

        //符合过滤条件的默认颜色
        public static Color FIT_COLOR = Color.LightGreen;
        //不符合过滤条件的颜色
        public static Color NOT_FIT_COLOR = Color.White;
        #endregion

        public const string TXT_IS_WRONG1 = "字符串长度不能超过";
        public const string TXT_IS_WRONG2 = "个字节";

        //关于图片路径
        public static string strPicturepath = "";

        // 无效id
        public const int INVALID_ID = -1;
        public const int FINISHED = 1;
        public const int NOT_FINISHED = 0;
        public const double DOUBLE_ZERO = 0.0;

        public const string JSON_MSG = "jsonmsg";
    }
}
