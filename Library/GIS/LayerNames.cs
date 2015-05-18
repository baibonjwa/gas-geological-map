using System;

namespace GIS
{
    public class LayerNames
    {
        public const string LAYER_ALIAS_MINE_BOUNDARY = "矿界5000";
        public const string LAYER_ALIAS_TUNNEL = "巷道";
        public const string LAYER_ALIAS_MR_WSYLD = "瓦斯压力点";
        public const string LAYER_ALIAS_MR_WSHLD = "瓦斯含量点";
        public const string LAYER_ALIAS_MR_WSTCD = "瓦斯突出点";
        public const string LAYER_ALIAS_MR_DLXXD = "动力现象点";
        public const string LAYER_ALIAS_MR_HCGZMWSYCLD = "回采工作面瓦斯涌出量点";
        public const string LAYER_ALIAS_MR_YJD = "预警点";
        public const string LAYER_ALIAS_MR_K1 = "K1";
        public const string LAYER_ALIAS_MR_S = "S";
        public const string LAYER_ALIAS_MR_HLDZX = "含量等值线";
        public const string LAYER_ALIAS_MR_YLDZX = "压力等值线";
        public const string LAYER_ALIAS_MR_YCLDZX = "涌出量等值线";
        public const string LAYER_ALIAS_MR_XianLuoZhu1 = "陷落柱1";
        public const string LAYER_ALIAS_MR_QYYJT = "区域预警图";
        public const string LAYER_ALIAS_MR_Zhuzhuang = "柱状图";
        public const string LAYER_ALIAS_MR_AnnotationXZZ = "小柱状文字";
        public const string LAYER_ALIAS_MR_PolylineXZZ = "小柱状线";
        public const string LAYER_ALIAS_MR_PolygonXZZ = "小柱状面";

        public const string LAYER_ALIAS_MR_CENTER_LINE = "中心线全";
        public const string LAYER_ALIAS_MR_CENTER_LINE_FD = "中心线分段";
        public const string LAYER_ALIAS_MR_STOPING_AREA = "采掘区";
        public const string LAYER_ALIAS_MR_DX_POINT = "导线点";
        public const string LAYER_ALIAS_MR_TUNNEL = "巷道全";
        public const string LAYER_ALIAS_MR_TUNNEL_FD = "巷道分段";
        public const string LAYER_ALIAS_MR_DXDLINE = "导线点线";
        public const string LAYER_ALIAS_MR_HENGCHUAN = "横川";

        public const string geo = "地质构造";
        public const string LAYER_NAME_MR_DS = "硐室";
        public const string LAYER_NAME_MR_JLDC = "揭露断层";
        public const string LAYER_NAME_MR_TDDC = "推断断层";
        public const string LAYER_NAME_MR_ZK = "钻孔";
        public const string LAYER_NAME_MR_XLZ = "陷落柱";
        public const string LAYER_NAME_MR_XLZ1 = "陷落柱1";
        public const string LAYER_NAME_MR_JT = "井筒";
        public const string LAYER_NAME_MR_zhuzhuang = "GasEarlyWarningGIS.SDE.MR_zhuzhuang";



        public const string LAYER_NAME_MINE_BOUNDARY = "GasEarlyWarningGIS.SDE.Polyline_成庄矿界5000";
        public const string LAYER_NAME_TUNNEL = "巷道";
        public const string LAYER_NAME_MR_WSYLD = "瓦斯压力点";
        public const string LAYER_NAME_MR_WSHLD = "瓦斯含量点";
        public const string LAYER_NAME_MR_WSTCD = "瓦斯突出点";
        public const string LAYER_NAME_MR_DLXXD = "动力现象点";
        public const string LAYER_NAME_MR_HCGZMWSYCLD = "回采工作面瓦斯涌出量点";
        public const string LAYER_NAME_MR_YJD = "预警点";
        public const string LAYER_NAME_MR_K1 = "K1";
        public const string LAYER_NAME_MR_S = "S";
        public const string LAYER_NAME_MR_HLDZX = "MR_HLDZX";
        public const string LAYER_NAME_MR_YLDZX = "MR_YLDZX";
        public const string LAYER_NAME_MR_YCLDZX = "MR_YCLDZX";
        public const string LAYER_NAME_MR_DMBG = "MR_DMBGDZX";
        public const string LAYER_NAME_MR_MCDB = "MR_MCDBDZX";
        public const string LAYER_NAME_MR_MS = "MR_MSDZX";


        public const string LAYER_NAME_MR_CENTER_LINE = "GasEarlyWarningGIS.SDE.MR_centerline"; // "中心线全";
        public const string LAYER_NAME_MR_CENTER_LINE_FD = "GasEarlyWarningGIS.SDE.MR_centerlinfd";//"中心线分段";
        public const string LAYER_NAME_MR_STOPING_AREA = "GasEarlyWarningGIS.SDE.MR_cjqreg"; //"采掘区";
        public const string LAYER_NAME_MR_DX_POINT = "GasEarlyWarningGIS.SDE.MR_dxdpnt"; //"导线点";
        public const string LAYER_NAME_MR_TUNNEL = "GasEarlyWarningGIS.SDE.MR_hdjs";   // "巷道全";
        public const string LAYER_NAME_MR_TUNNEL_FD = "GasEarlyWarningGIS.SDE.MR_hdreg"; //"巷道分段";

        public const string DEFAULT = "默认";

        #region 系统一
        public const string TRANSDUCER = "传感器";

        private static string[] _sys1BuiltinLayerNames = null;

        static public string[] GetSys1BuiltinLayerNames()
        {
            if (_sys1BuiltinLayerNames == null)
            {
                _sys1BuiltinLayerNames = new string[]
                {
                    TRANSDUCER,
                };
            }
            return _sys1BuiltinLayerNames;
        }
        #endregion

        #region 系统二
        public const string DUG_FOOTAGE = "掘进进尺";

        public const string EXTRACTION_FOOTAGE = "回采进尺";

        public const string STOP_LINE = "停采线";

        private static string[] _sys2BuiltinLayers = null;

        static public string[] GetBuiltinSys2InLayerNames()
        {
            if (_sys2BuiltinLayers == null)
            {
                _sys2BuiltinLayers = new string[]
                {
                    DUG_FOOTAGE,
                    EXTRACTION_FOOTAGE,
                    STOP_LINE,
                };
            }
            return _sys2BuiltinLayers;
        }
        #endregion

        #region 系统三
        public const string DEFALUT_HENGCHUAN = "横川";

        public const string DEFALUT_BOREHOLE = "钻孔";

        public const string DEFALUT_EXPOSE_FAULTAGE = "揭露断层";

        public const string DEFALUT_INFERRED_FAULTAGE = "推断断层";

        public const string DEFALUT_COLLAPSE_PILLAR = "陷落柱";

        public const string DEFALUT_COLLAPSE_PILLAR_1 = "陷落柱1";

        public const string DEFALUT_TUNNEL = "巷道";

        public const string DEFALUT_WIRE_PT = "导线点";

        public const string DEFALUT_DONGSHI = "硐室";

        public const string DEFALUT_JINGTONG = "井筒";

        public const string DEFALUT_KANTANXIAN = "勘探线";

        public const string COAL_FLOOR_CONTOUR = "煤层底板等值线";

        public const string COAL_SEAM_BURIED_DEPTH_CONTOUR = "煤层埋深等值线";

        public const string SURFACE_CONTOUR = "地表等值线";

        private static string[] _sys3BuiltinLayerNames = null;

        static public string[] GetSys3BuiltinLayerNames()
        {
            if (_sys3BuiltinLayerNames == null)
            {
                _sys3BuiltinLayerNames = new string[]
                {
                    DEFALUT_BOREHOLE,
                    DEFALUT_EXPOSE_FAULTAGE,
                    DEFALUT_INFERRED_FAULTAGE,
                    DEFALUT_COLLAPSE_PILLAR,
                    DEFALUT_TUNNEL,
                    DEFALUT_WIRE_PT,
                    COAL_FLOOR_CONTOUR,
                    COAL_SEAM_BURIED_DEPTH_CONTOUR,
                    SURFACE_CONTOUR,
                };
            }
            return _sys3BuiltinLayerNames;
        }
        #endregion

        #region 系统四
        public const string GAS_PRESSURE_PT = "瓦斯压力点";

        public const string GAS_CONTENT_PT = "瓦斯含量点";

        public const string GAS_OUTBURST_PT = "瓦斯突出点";

        public const string GROUND_LEVLE_PT = "地面标高点";

        public const string FLOOR_ELEVATION_PT = "底板标高点";

        public const string EMBEDDED_DEPTH_PT = "埋深点";

        public const string POWER_PHENOMENON_PT = "动力现象点";

        public const string STOPE_WORKING_FACE_GAS_GUSH_QUANTITY_PT = "回采工作面瓦斯涌出量点";

        public const string GAS_PRESSURE_CONTOUR = "压力等值线";

        public const string GAS_CONTENT_CONTOUR = "含量等值线";

        public const string GUSH_QUANTITY_CONTOUR = "涌出量等值线";

        public const string GROUND_LEVLE_CONTOUR = "地面标高等值线";

        public const string FLOOR_ELEVATION_CONTOUR = "煤层底板等值线";

        public const string EMBEDDED_DEPTH_CONTOUR = "埋深等值线";

        private static string[] _sys4BuiltinLayerNames = null;

        static public string[] GetSys4BuiltinLayerNames()
        {
            if (_sys4BuiltinLayerNames == null)
            {
                _sys4BuiltinLayerNames = new string[]
                {
                    GAS_PRESSURE_PT,
                    GAS_CONTENT_PT,
                    GAS_OUTBURST_PT,
                    POWER_PHENOMENON_PT,
                    STOPE_WORKING_FACE_GAS_GUSH_QUANTITY_PT,

                    GAS_PRESSURE_CONTOUR,
                    GAS_CONTENT_CONTOUR,
                    GUSH_QUANTITY_CONTOUR,
                };
            }
            return _sys4BuiltinLayerNames;
        }

        #endregion

        #region 系统五
        public const string WORKING_FACE_WARNING_MAP = "工作面预警图";

        public const string MINE_WARNING_MAP = "矿井预警图";

        private static string[] _sys5BuiltinLayerNames = null;

        static public string[] GetSys5BuiltinLayerNames()
        {
            if (_sys5BuiltinLayerNames == null)
            {
                _sys5BuiltinLayerNames = new string[]
                {
                    WORKING_FACE_WARNING_MAP,
                    MINE_WARNING_MAP,
                };
            }
            return _sys5BuiltinLayerNames;
        }
        #endregion
    }
}
