// ******************************************************************
// 概  述：系统四常量名
// 作  者：伍鑫
// 创建日期：2014/01/17
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
    public static class Const_OP
    {
        /** 瓦斯压力 **/
        public const string COORDINATE_X = "坐标X";
        public const string COORDINATE_Y = "坐标Y";
        public const string GAS_PRESSURE_COORDINATE_Z = "测点标高";
        public const string DEPTH = "埋深";
        public const string GAS_PRESSURE_VALUE = "瓦斯压力值";
        public const string MEASURE_DATE_TIME = "测定时间";

        /** 瓦斯含量 **/
        public const string GAS_CONTENT_VALUE = "瓦斯含量值";

        /** 瓦斯涌出量 **/
        public const string STOPE_WORKING_FACE_GAS_GUSH_QUANTITY_COORDINATE_Z = "坐标Z";
        public const string ABSOLUTE_GAS_GUSH_QUANTITY = "绝对瓦斯涌出量";
        public const string RELATIVE_GAS_GUSH_QUANTITY = "相对瓦斯涌出量";
        public const string WORKING_FACE_DAY_OUTPUT = "工作面日产量";
        public const string STOPE_DATE = "回采年月";

        /** 瓦斯含量点添加窗口名 **/
        public const string INSERT_GASCONTENT_INFO = "添加瓦斯含量点";
        /** 瓦斯含量点修改窗口名 **/
        public const string UPDATE_GASCONTENT_INFO = "修改瓦斯含量点";
        /** 瓦斯含量点管理窗口名 **/
        public const string MANAGE_GASCONTENT_INFO = "瓦斯含量点";

        /** 瓦斯压力点添加窗口名 **/
        public const string INSERT_GASPRESSURE_INFO = "添加瓦斯压力";
        /** 瓦斯压力点修改窗口名 **/
        public const string UPDATE_GASPRESSURE_INFO = "修改瓦斯压力";
        /** 瓦斯压力点管理窗口名 **/
        public const string MANAGE_GASPRESSURE_INFO = "瓦斯压力点";

        /** 瓦斯涌出量数据添加窗口名 **/
        public const string INSERT_GASGUSHQUANTITY_INFO = "添加瓦斯涌出量点";
        /** 瓦斯涌出量数据修改窗口名 **/
        public const string UPDATE_GASGUSHQUANTITY_INFO = "修改瓦斯涌出量点";
        /** 瓦斯涌出量数据管理窗口名 **/
        public const string MANAGE_GASGUSHQUANTITY_INFO = "瓦斯涌出量点";

        /** 所属巷道必须选择提示信息 **/
        public const string TUNNEL_NAME_MUST_INPUT = "请选择所在巷道！";

        /** 删除确认提示信息 **/
        public const string DEL_CONFIRM_MSG_GASCONTENT = "确认要删除所选瓦斯含量数据吗？";

        /** 删除确认提示信息 **/
        public const string DEL_CONFIRM_MSG_GASGUSHQUANTITY = "确认要删除所选瓦斯涌出量数据吗？";

        /** 删除确认提示信息 **/
        public const string DEL_CONFIRM_MSG_GASPRESSURE = "确认要删除所选瓦斯压力数据吗？";

        /**K1值**/
        public const string K1_VALUE_ADD = "添加K1值";
        public const string K1_VALUE_CHANGE = "修改K1值";
        public const string K1_VALUE_FARPOINT_TITLE = "K1值";
        public const string K1_VALUE_MANAGEMENT = "K1值";
        public const string K1_VALUE_MSG_DEL = "要删除此记录对应的所有信息吗？确定删除，取消只删除显示K1值信息";
        public const string K1_VALUE_MSG_ADD_MORE_THAN_ONE = "请至少输入一条信息！";
        public const string K1_VALUE_COORDINATE_X = "拾取点X";
        public const string K1_VALUE_COORDINATE_Y = "拾取点Y";
        public const string K1_VALUE_COORDINATE_Z = "拾取点Z";
        public const string K1_VALUE_DRY = "干煤K1值";
        public const string K1_VALUE_WET = "湿煤K1值";
        public const string K1_VALUE_BOREHOLE_DEEP = "孔深";

        /**S值**/
        public const string S_VALUE_ADD = "添加S值";
        public const string S_VALUE_CHANGE = "修改S值";
        public const string S_VALUE_FARPOINT_TITLE = "S值";
        public const string S_VALUE_MANAGEMENT = "S值";
        public const string S_VALUE_MSG_ADD_MORE_THAN_ONE = "请至少输入一个拾取点信息！";
        public const string S_VALUE_COORDINATE_X = "拾取点X";
        public const string S_VALUE_COORDINATE_Y = "拾取点Y";
        public const string S_VALUE_COORDINATE_Z = "拾取点Z";
        public const string S_VALUE_SG = "Sg值";
        public const string S_VALUE_SV = "Sv值";
        public const string S_VALUE_Q = "q值";
        public const string S_VALUE_BOREHOLE_DEEP = "孔深";

        /**井下数据**/
        //瓦斯
        public const string GAS_DATA = "瓦斯";
        public const string GAS_DATA_ADD = "添加瓦斯";
        public const string GAS_DATA_CHANGE = "修改瓦斯";
        public const string GAS_DATA_MANAGEMENT = "瓦斯信息";
        public const string GAS_DATA_FARPOINT = "瓦斯信息";

        public const string POWER_FAILURE = "瓦斯探头断电次数";
        public const string DRILL_TIMES = "吸钻预兆次数";
        public const string GAS_TIMES = "瓦斯忽大忽小预兆次数";
        public const string TEMP_DOWN_TIMES = "气温下降预兆次数";
        public const string COAL_BANG_TIMES = "煤炮频繁预兆次数";
        public const string CRATER_TIMES = "喷孔次数";
        public const string STOPER_TIMES = "顶钻次数";

        //管理
        public const string MANAGEMENT = "管理";
        public const string MANAGEMENT_ADD = "添加管理";
        public const string MANAGEMENT_CHANGE = "修改管理";
        public const string MANAGEMENT_MANAGEMENT = "管理信息";
        public const string MANAGEMENT_FARPOINT = "管理信息";

        //地质构造
        public const string GEOLOGIC_STRUCTURE = "地质构造";
        public const string GEOLOGIC_STRUCTURE_ADD = "添加地质构造";
        public const string GEOLOGIC_STRUCTURE_CHANGE = "修改地质构造";
        public const string GEOLOGIC_STRUCTURE_MANAGEMENT = "地质构造信息";
        public const string GEOLOGIC_STRUCTURE_FARPOINT = "地质构造信息";
        /** 帮助文件 **/
        public const string System4_Help_File = "\\工作面动态防突管理系统帮助文件.chm";
        /** 关于图片 **/
        public const string Picture_Name = "\\系统四关于图片.jpg";
    }
}