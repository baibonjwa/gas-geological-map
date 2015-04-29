using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace GIS.HdProc
{
    public static class Global
    {
        public static ConstructParallel cons = new ConstructParallel();//巷道计算类
        public static CommonClass commonclss = new CommonClass();//GIS通用代码类
        public static QYJSClass hcjsclass = new QYJSClass();//回采计算类
        public static TddcClass tdclass = new TddcClass();//推断断层类

        public static IActiveView pActiveView;//当前视图变量
        public static double linespace=5.0f;//巷道宽度
        public static double searchlen = 60.0;//查询地质构造的距离
        public static double sxjl = 5;//上下层关系判断阈值
        public static double radius = 2;//导线点线图层符号的半径
        public static IFeatureLayer geolyr;//地质构造图层名
        public static IFeatureLayer dslyr;//峒室图层
        public static IFeatureLayer jllyr;//揭露断层d
        public static IFeatureLayer tdlyr;//推断断层
        public static IFeatureLayer zklyr;//钻孔层
        public static IFeatureLayer xlzlyr;//陷落柱层
        public static IFeatureLayer xlzlyr1;//默认陷落柱1层
        public static IFeatureLayer jtlyr;//井筒

        public static IFeatureLayer pntlyr;//导线点图层
        public static IFeatureLayer centerfdlyr;//中心线分段图层
        public static IFeatureLayer centerlyr;//中心线图层
        public static IFeatureLayer hdfdlyr;//巷道分段图层
        public static IFeatureLayer hdfdfulllyr;//巷道面全图层
        public static IFeatureLayer hcqlyr;//采掘区图层
        public static IFeatureLayer pntlinlyr;//导线点线图层

        public static ISpatialReference spatialref = null;//空间参考信息

        public static void SetInitialParams(IActiveView activeView)
        {
            if (activeView != null)
            {
                pActiveView = activeView;
                pntlyr      = commonclss.GetFeatureLayerFromLayerName(activeView, LayerNames.LAYER_ALIAS_MR_DX_POINT);//导线点图层
                centerfdlyr = commonclss.GetFeatureLayerFromLayerName(activeView, LayerNames.LAYER_ALIAS_MR_CENTER_LINE_FD);//中心线分段图层
                centerlyr   = commonclss.GetFeatureLayerFromLayerName(activeView, LayerNames.LAYER_ALIAS_MR_CENTER_LINE);//中心线图层
                hdfdlyr     = commonclss.GetFeatureLayerFromLayerName(activeView, LayerNames.LAYER_ALIAS_MR_TUNNEL_FD);//巷道分段图层
                hdfdfulllyr = commonclss.GetFeatureLayerFromLayerName(activeView, LayerNames.LAYER_ALIAS_MR_TUNNEL);//巷道面全图层
                geolyr      = commonclss.GetFeatureLayerFromLayerName(activeView, LayerNames.geo);//地质构造图层
                hcqlyr      = commonclss.GetFeatureLayerFromLayerName(activeView, LayerNames.LAYER_ALIAS_MR_STOPING_AREA);//采掘区图层
                pntlinlyr = commonclss.GetFeatureLayerFromLayerName(activeView, LayerNames.LAYER_ALIAS_MR_DXDLINE);//导线点线图层
                spatialref  = activeView.FocusMap.SpatialReference;//空间参考信息

                dslyr = commonclss.GetFeatureLayerFromLayerName(activeView, LayerNames.LAYER_NAME_MR_DS);//峒室层
                jllyr = commonclss.GetFeatureLayerFromLayerName(activeView, LayerNames.LAYER_NAME_MR_JLDC);//峒室层
                tdlyr = commonclss.GetFeatureLayerFromLayerName(activeView, LayerNames.LAYER_NAME_MR_TDDC);//峒室层
                zklyr = commonclss.GetFeatureLayerFromLayerName(activeView, LayerNames.LAYER_NAME_MR_ZK);//峒室层
                xlzlyr = commonclss.GetFeatureLayerFromLayerName(activeView, LayerNames.LAYER_NAME_MR_XLZ);//峒室层
                xlzlyr1 = commonclss.GetFeatureLayerFromLayerName(activeView, LayerNames.LAYER_NAME_MR_XLZ1);//峒室层
                jtlyr = commonclss.GetFeatureLayerFromLayerName(activeView, LayerNames.LAYER_NAME_MR_JT);//井筒
            }
        }
    }
}
