using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIS
{
    public static class GIS_Const
    {
        public const string FIELD_OBJECTID = "OBJECTID"; //
        public const string FIELD_BID      = "BID";      // Binding WirePointName.
        public const string FIELD_SHAPE    = "SHAPE";    // 形状
        public const string FIELD_DCBZ     = "DCBZ";     // 断层标注
        public const string FIELD_BS       = "BS";       // 标示（巷道）是否被矫正过
        public const string FIELD_NAME = "NAME";         //导线点名称
        public const string FIELD_HDNAME = "HDNAME";//巷道名称
        public const string FIELD_HDID     = "HDID";     // 巷道id
        public const string FIELD_HdId = "HdId";//
        public const string FIELD_ID       = "ID";       // ID
        public const string FIELD_XH       = "XH";
        public const string FILE_DCBZ      = "DCBZ";
        public const string FIELD_COORDINATE_X = "COORDINATE_X";       //COORDINATEX
        public const string FIELD_COORDINATE_Y = "COORDINATE_Y";       //COORDINATEX
        public const string FIELD_COORDINATE_Z = "COORDINATE_Z";       //COORDINATEX

        public const string FIELD_BOREHOLE_NUMBER        = "BOREHOLE_NUMBER"; // 钻孔号
        public const string FIELD_ADD_TIME               = "addtime"; // 添加时间
        public const string FIELD_GROUND_ELEVATION       = "GROUND_ELEVATION"; // 顶板标高
        public const string FIELD_GROUND_FLOOR_ELEVATION = "GROUND_FLOOR_ELEVATION"; // 底板标高
        public const string FIELD_THICKNESS              = "THICKNESS"; // 厚度
        public const string FIELD_TYPE                   = "type"; // 类型
        
        // 
        public const string STR_IDataLayer               = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"; //IDataLayer
        public const string STR_IFeatureLayer            = "{40A9E885-5533-11d0-98BE-00805F7CED21}"; //IFeatureLayer
        public const string STR_IGeoFeatureLayer         = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"; //IGeoFeatureLayer
        public const string STR_IGraphicsLayer           = "{34B2EF81-F4AC-11D1-A245-080009B6F22B}"; //IGraphicsLayer
        public const string STR_IFDOGraphicsLayer        = "{5CEAE408-4C0A-437F-9DB3-054D83919850}"; //IFDOGraphicsLayer
        public const string STR_ICoverageAnnotationLayer = "{0C22A4C7-DAFD-11D2-9F46-00C04F6BC78E}"; //ICoverageAnnotationLayer
        public const string STR_IGroupLayer              = "{EDAD6644-1810-11D1-86AE-0000F8751720}"; //IGroupLayer
        public const string STR_IRasterLayer             = "{D02371C7-35F7-11D2-B1F2-00C04F8EDEFF}"; //IRasterLayer
    }
}
