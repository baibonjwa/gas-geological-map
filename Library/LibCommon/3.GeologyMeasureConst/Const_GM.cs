// ******************************************************************
// 概  述：系统三常量名
// 作  者：伍鑫
// 创建日期：2014/01/18
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
    public static class Const_GM
    {
        /** 断层 **/
        public const string FAULTAGE_NAME = "断层名称";
        public const string GAP           = "落差";
        public const string ANGLE         = "倾角";
        public const string TYPE          = "类型";
        public const string TREND         = "走向";
        public const string SEPARATION    = "断距";
        public const string COORDINATE_X  = "坐标X";
        public const string COORDINATE_Y  = "坐标Y";
        public const string COORDINATE_Z  = "坐标Z";

        /** 添加揭露断层窗口名 **/
        public const string INSERT_FAULTAGE_INFO = "添加揭露断层";
        /** 修改揭露断层窗口名 **/
        public const string UPDATE_FAULTAGE_INFO = "修改揭露断层";
        /** 管理揭露断层窗口名 **/
        public const string MANAGE_FAULTAGE_INFO = "揭露断层";

        /** 添加推断断层窗口名 **/
        public const string INSERT_BIG_FAULTAGE_INFO = "添加推断断层";
        /** 修改推断断层窗口名 **/
        public const string UPDATE_BIG_FAULTAGE_INFO = "修改推断断层";
        /** 管理推断断层窗口名 **/
        public const string MANAGE_BIG_FAULTAGE_INFO = "推断断层";

        /** 添加井筒窗口名 **/
        public const string INSERT_PITSHAFT_INFO = "添加井筒";
        /** 修改井筒窗口名 **/
        public const string UPDATE_PITSHAFT_INFO = "修改井筒";
        /** 管理井筒窗口名 **/
        public const string MANAGE_PITSHAFT_INFO = "井筒";

        /** 断层类型 **/
        public const string FRONT_FAULTAGE = "正断层";
        public const string OPPOSITE_FAULTAGE = "逆断层";
        /** 断层信息存在提示信息 **/
        public const string FAULTAGE_EXIST_MSG = "断层名称已存在，请重新录入！";
        /** 揭露点必须选择提示信息 **/
        public const string EXPOSEPOINT_MUST_CHOOSE_MSG = "请选择揭露点！";

        /** 钻孔信息添加窗口名 **/
        public const string INSERT_BOREHOLE_INFO = "添加勘探钻孔";
        /** 钻孔信息修改窗口名 **/
        public const string UPDATE_BOREHOLE_INFO = "修改勘探钻孔";
        /** 钻孔信息管理窗口名 **/
        public const string MANAGE_BOREHOLE_INFO = "勘探钻孔";

        /** 钻孔 **/
        public const string BOREHOLE_NUMBER = "孔号";
        public const string GROUND_ELEVATION = "地面标高";

        /** 孔号存在提示信息 **/
        public const string BOREHOLE_EXIST_MSG = "孔号已存在，请重新录入！";
        /** 煤层岩性必须录入提示信息 **/
        public const string LITHOLOGY_MUST_INPUT = "请录入煤层的岩性！";
        /** 无法获取钻孔ID提示信息 **/
        public const string CAN_NOT_GET_BOREHOLE_ID = "无法根据孔号获取钻孔ID！";
        /** 无法获取钻孔ID提示信息 **/
        public const string CAN_NOT_GET_BOREHOLE_BID = "无法根据孔号获取钻孔绑定ID！";
        
        // 巷道
        public const string TUNNEL = "巷道";
        public const string MINE_NAME = "矿井名称";
        public const string HORIZONTAL = "水平";
        public const string MINING_AREA = "采区";
        public const string WORKING_FACE = "工作面";
        public const string TUNNEL_NAME = "巷道名称";

        public const string TUNNEL_NOT_FOUND = "所查找的巷道不存在";

        public const string TUNNEL_TYPE_TUNNELING_CHN = "掘进";
        public const string TUNNEL_TYPE_STOPING_CHN   = "回采";
        public const string TUNNEL_TYPE_OTHER_CHN     = "设计巷道";

        public const string COAL_LAYER    = "煤层";
        public const string STONE_LAYER   = "岩层";
        public const string COAL_TUNNEL   = "煤巷";
        public const string STONE_TUNNEL  = "岩巷";
        public const string DESIGN_LENGTH = "设计长度";
        public const string DESIGN_AREA   = "设计面积";
        public const string TUNNEL_INFO_MSG_DEL = "确定要删除所选内容吗？(删除巷道会同时删除相关掘进回采进尺日报数据、及掘进巷道，回采巷道会重置为设计巷道）";//关联的导线信息不回被删除
        public const string TUNNEL_INFO_MSG_DEL_TUNNEL_JJHC = "要删除相关掘进回采巷道信息吗？";
        public const string TUNNEL_INFO_MSG_DEL_WIRE = "要删除巷道绑定的导线信息吗？";

        public const string TUNNEL_INFO_MSG_WORKING_FACE_NAME = "巷道所在工作面信息";
        public const string TUNNEL_INFO_MSG_DOUBLE_TUNNEL_NAME = "巷道名称重复，请重新输入巷道名称！";

        public const string TUNNEL_INFO_SQUARE = "矩形";
        public const string TUNNEL_INFO_LADDERSHAPE = "梯形";
        public const string TUNNEL_INFO_SEMICIRCLE = "半圆拱";
        public const string TUNNEL_INFO_THREEPOINT = "三心拱";
        public const string TUNNEL_INFO_ARC = "圆形";
        public const string TUNNEL_INFO_OTHER = "其他";

        /** 删除确认提示信息 **/
        public const string DEL_CONFIRM_MSG_FAULTAGE = "确认要删除所选揭露断层数据吗？";
        /** 删除确认提示信息 **/
        public const string DEL_CONFIRM_MSG_BIG_FAULTAGE = "确认要删除所选推断断层数据吗？";
        /** 删除确认提示信息 **/
        public const string DEL_CONFIRM_MSG_BOREHOLE = "确认要删除所选钻孔数据吗？";
        /** 删除确认提示信息 **/
        public const string DEL_CONFIRM_MSG_PITSHAFT = "确认要删除所选井筒数据吗？";

        /** 勘探线 **/
        public const string PROSPECTING_LINE_NAME = "勘探线名称";
        public const string PROSPECTING_BOREHOLE_MUST_CHOOSE_MSG = "请选择勘探线钻孔！";
        /** 删除确认提示信息 **/
        public const string DEL_CONFIRM_MSG_PROSPECTING_LINE = "确认要删除所选勘探线数据吗？";

        /** 添加勘探线窗口名 **/
        public const string INSERT_PROSPECTING_LINE_INFO = "添加勘探线";
        /** 修改勘探线窗口名 **/
        public const string UPDATE_PROSPECTING_LINE_INFO = "修改勘探线";
        /** 管理勘探线窗口名 **/
        public const string MANAGE_PROSPECTING_LINE_INFO = "勘探线";

        /** 勘探线名称存在提示信息 **/
        public const string PROSPECTING_LINE_EXIST_MSG = "勘探线名称已存在，请重新录入！";

        /** 推断断层名称存在提示信息 **/
        public const string BIGFAULTAGE_EXIST_MSG = "推断断层名称已存在，请重新录入！";

        /** 井筒 **/
        public const string PITSHAFT_NAME = "井筒名称";
        public const string PITSHAFT_TYPE = "井筒类型";
        public const string WELLHEAD_ELEVATION = "井口标高";
        public const string WELLBOTTOM_ELEVATION = "井底标高";
        public const string PITSHAFT_COORDINATE_X = "井筒坐标X";
        public const string PITSHAFT_COORDINATE_Y = "井筒坐标Y";
        public const string FIGURE_COORDINATE_X = "图形坐标X";
        public const string FIGURE_COORDINATE_Y = "图形坐标Y";
        public const string FIGURE_COORDINATE_Z = "图形坐标Z";
        /** 井筒名称存在提示信息 **/
        public const string PITSHAFT_NAME_EXIST_MSG = "井筒名称已存在，请重新录入！";

        /**陷落柱**/
        public const string COLLAPSEPILLARE_ADD = "添加陷落柱";
        public const string COLLAPSEPILLARE_CHANGE = "修改陷落柱";
        public const string COLLAPSEPILLARE_FARPOINT_TITLE = "陷落柱";
        public const string COLLAPSEPILLARE_MANAGEMENT = "陷落柱";

        public const string COLLAPSEPILLARE_NAME = "陷落柱名称";
        public const string COLLAPSEPILLARE_MSG_MUST_MORE_THAN_THREE = "关键点个数应大于3！";
        public const string COLLAPSEPILLARE_MSG_COORDINATE_X = "坐标X";
        public const string COLLAPSEPILLARE_MSG_COORDINATE_Y = "坐标Y";
        public const string COLLAPSEPILLARE_MSG_COORDINATE_Z = "坐标Z";
        public const string COLLAPSEPILLARE_DISCRIBE = "描述";

        /**巷道选择**/
        public const string TUNNEL_CHOOSE = "巷道选择";
        /**回采巷道**/
        public const string TUNNEL_HC_ADD = "添加回采面";
        public const string TUNNEL_HC_CHANGE = "修改回采面";
        public const string TUNNEL_HC_FARPOINT_TITLE = "回采面数据管理";
        public const string TUNNEL_HC_MANAGEMENT = "回采面管理";
        public const string MAIN_TUNNEL = "主运顺槽";
        public const string SECOND_TUNNEL = "辅运顺槽";
        public const string OOC_TUNNEL = "开切眼";

        public const string TUNNEL_HC_MSG_TUNNEL_DOUBLE_CHOOSE = "巷道选择重复，请重新选择！";
        public const string TUNNEL_HC_MSG_TUNNEL_IS_JJ = "已被选择为掘进巷道，请重新选择！";
        public const string TUNNEL_HC_MSG_TUNNEL_IS_HC = "已被选择为回采巷道，请重新选择！";

        /**掘进巷道**/
        public const string TUNNEL_JJ_ADD = "添加掘进面";
        public const string TUNNEL_JJ_CHANGE = "修改掘进面";
        public const string TUNNEL_JJ_FARPOINT_TITLE = "掘进面数据管理";
        public const string TUNNEL_JJ_MANAGEMENT = "掘进面管理";

        public const string TUNNEL_JJ_MSG_TUNNEL_IS_ALREADY_JJ = "该巷道已设置为掘进巷道，请重新选择巷道！";
        public const string TUNNEL_JJ_MSG_TUNNEL_IS_HC = "所选巷道为回采巷道，请重新选择！";

        /**横川**/
        public const string TUNNEL_HCHUAN_ADD = "添加横川";
        public const string TUNNEL_HCHUAN_CHANGE = "修改横川";
        public const string TUNNEL_HCHUAN_FARPOINT_TITLE = "横川数据管理";
        public const string TUNNEL_HCHUAN_MANAGEMENT = "横川管理";

        public const string TUNNEL_HCHUAN_MSG_TUNNEL_IS_ALREADY_JJ = "该巷道已设置为掘进巷道，请重新选择巷道！";
        public const string TUNNEL_HCHUAN_MSG_TUNNEL_IS_HC = "所选巷道为回采巷道，请重新选择！";
        /**设计巷道**/
        public const string TUNNEL_INFO_ADD = "添加设计巷道";
        public const string TUNNEL_INFO_CHANGE = "修改设计巷道";
        public const string TUNNEL_INFO_FARPOINT_TITLE = "设计巷道管理";
        public const string TUNNEL_INFO_MANAGEMENT = "设计巷道管理";
        /**导线**/
        public const string WIRE_INFO_ADD = "添加导线";
        public const string WIRE_INFO_CHANGE = "修改导线";
        public const string WIRE_INFO_FARPOINT_TITLE = "导线管理";
        public const string WIRE_INFO_MANAGEMENT = "导线管理";
        public const string WIRE_NAME = "导线名称";
        public const string WIRE_POINT_ID = "导线点编号";
        public const string X = "坐标X";
        public const string Y = "坐标Y";
        public const string Z = "坐标Z";
        public const string DISTANCE_TO_LEFT = "距左帮距离";
        public const string DISTANCE_TO_RIGHT = "距右帮距离";
        public const string DISTANCE_TO_TOP = "距顶板距离";
        public const string DISTANCE_TO_BOTTOM = "距底板距离";

        public const string TUNNEL_CHOOSE_FIRST = "该巷道已绑定导线[";
        public const string TUNNEL_CHOOSE_MIDDLE = "],是否想在[";
        public const string TUNNEL_CHOOSE_LAST = "]上继续添加导线点？";

        public const string WIRE_INFO_MSG_DEL_WIRE_INFO = "删除所有导线点将同时删除导线信息，您确定要删除么？";
        public const string WIRE_INFO_MSG_POINT_MUST_MORE_THAN_TWO = "导线点个数小于2,请继续添加导线点！";
        public const string WIRE_INFO_MSG_TUNNEL_ALREADY_BIND_WIRE = "所选巷道已绑定导线，请重新选择巷道！";
        public const string WIRE_INFO_MSG_WIRE_POINT_SPECIAL_SIGN = "导线点编号包含特殊字符！";
        public const string WIRE_INFO_MSG_WIRE_POINT_NAME_DOUBLE = "导线点名称重复！";

        /** 帮助文件 **/
        public const string System3_Help_File = "\\工作面地质测量管理系统帮助文件.chm";
        /** 关于图片 **/
        public const string Picture_Name = "\\系统三关于图片.jpg";
    }
}
