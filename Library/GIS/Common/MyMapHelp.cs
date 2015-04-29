using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using GIS.Common;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.SystemUI;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;

namespace GIS
{
    public class MyMapHelp
    {
        /// <summary>
        /// 点集合转成范围
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IGeometry GetGeoFromPoint(List<IPoint> list)
        {
            ITopologicalOperator ptopo=null;
            IGeometry objGeo = null;
            for (int i = 0; i < list.Count; i++)
            {
                IGeometry pGeo = (IGeometry)list[i];
                if (ptopo == null)
                {
                    ptopo = (ITopologicalOperator)pGeo;
                    objGeo = pGeo;
                }
                else
                {
                    objGeo = ptopo.Union(pGeo);
                    ptopo = (ITopologicalOperator)objGeo;
                }
            }
            return objGeo;
        }
        public static IGeometry GetGeoFromGeos(List<IGeometry> list)
        {
            ITopologicalOperator ptopo = null;
            IGeometry objGeo = null;
            for (int i = 0; i < list.Count; i++)
            {
                IGeometry pGeo = list[i];
                if (ptopo == null)
                {
                    ptopo = (ITopologicalOperator)pGeo;
                    objGeo = pGeo;
                }
                else
                {
                    objGeo = ptopo.Union(pGeo);
                    ptopo = (ITopologicalOperator)objGeo;
                }
            }
            return objGeo;
        }
        public static IGeometry GetGeoFromFeature(List<IFeature> list)
        {
            ITopologicalOperator ptopo = null;
            IGeometry objGeo = null;
            for (int i = 0; i < list.Count; i++)
            {
                IGeometry pGeo = list[i].ShapeCopy;
                if (ptopo == null)
                {
                    ptopo = (ITopologicalOperator)pGeo;
                    objGeo = pGeo;
                }
                else
                {
                    objGeo = ptopo.Union(pGeo);
                    ptopo = (ITopologicalOperator)objGeo;
                }
            }
            return objGeo;
        }
        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="pGeo"></param>
        public static void Jump(IGeometry pGeo)
        {
            if (pGeo == null)
                return;
            if (pGeo.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                ITopologicalOperator pTopo = (ITopologicalOperator)pGeo;
                pGeo = pTopo.Buffer(10);
            }
            else
            {
                double ct = 10;
                if (pGeo.Envelope.Height > pGeo.Envelope.Width)
                {
                    ct = pGeo.Envelope.Height / 4;
                }
                else
                {
                    ct = pGeo.Envelope.Width /4;
                }
                ITopologicalOperator pTopo = (ITopologicalOperator)pGeo;
                pGeo = pTopo.Buffer(ct);
            }
            GIS.Common.DataEditCommon.g_pAxMapControl.Extent = pGeo.Envelope;
            GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, GIS.Common.DataEditCommon.g_pAxMapControl.Extent);
        }
        /// <summary>
        /// 地图单位转中文
        /// </summary>
        /// <param name="inUnit"></param>
        /// <returns></returns>
        public static string mapUnit(string inUnit)
        {
            string outUnitCH = inUnit;
            switch (inUnit)
            {
                case "Kilometers":
                    outUnitCH = "千米";
                    break;
                case "Meters":
                    outUnitCH = "米";
                    break;
                case "Decimeters":
                    outUnitCH = "分米";
                    break;
                case "Centimeters":
                    outUnitCH = "厘米";
                    break;
                case "Millimeters":
                    outUnitCH = "毫米";
                    break;
                case "Miles":
                    outUnitCH = "英里";
                    break;
                case "NauticalMiles":
                    outUnitCH = "海里";
                    break;
                case "Yards":
                    outUnitCH = "码";
                    break;
                case "Feet":
                    outUnitCH = "英尺";
                    break;
                case "Inches":
                    outUnitCH = "英寸";
                    break;
                case "DecimalDegrees":
                    outUnitCH = "度";
                    break;
                case "Hectares":
                    outUnitCH = "公顷";
                    break;
                case "Acres":
                    outUnitCH = "英亩";
                    break;
            }
            return outUnitCH;
        }
        /// <summary>
        /// 判断是否包含
        /// </summary>
        /// <param name="Poly">参考范围图形</param>
        /// <param name="list">要判断的图形集合</param>
        /// <returns>所有不包含在范围内的图形</returns>
        public static List<IGeometry> withIn(IGeometry Poly, List<IGeometry> list)
        {
            var polygon = Poly;

            List<IGeometry> listnoIn = new List<IGeometry>();
            var geometryList = list;
            bool isWithin = false;

            foreach (var geometry in geometryList)
            {
                var point = geometry as IRelationalOperator;

                if (point != null)
                {
                    isWithin = point.Within(polygon);

                    if (!isWithin)
                    {
                        listnoIn.Add(geometry);
                        //break;
                    }
                }
            }
            return listnoIn;
        }
        /// <summary>
        /// 设定当前工具
        /// </summary>
        /// <param name="tool">ICommand</param>
        public static void SetCurrentTool(ICommand tool)
        {
            tool.OnCreate(DataEditCommon.g_pAxMapControl.Object);
            if (tool.Enabled)
            {
                DataEditCommon.g_pAxMapControl.CurrentTool = (ITool)tool;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("当前工具未激活！");
            }
        }

        /// <summary>
        /// 面转线
        /// </summary>
        /// <param name="pPolyline"></param>
        /// <returns></returns>
        public static List<ILine> PolygonToListline(IPolygon Polygon)
        {
            List<ILine> listline = new List<ILine>();
            if (Polygon != null && Polygon.IsEmpty == false)
            {
                IGeometryCollection pPolygonGeoCol = Polygon as IGeometryCollection;
                ISegment pSegment = null;
                ILine pline = new LineClass();
                for (int i = 0; i < pPolygonGeoCol.GeometryCount; i++)
                {
                    ISegmentCollection pPolygonSegCol = pPolygonGeoCol.get_Geometry(i) as ISegmentCollection;
                    for (int j = 0; j < pPolygonSegCol.SegmentCount; j++)
                    {
                        pSegment = pPolygonSegCol.get_Segment(j);
                        pline = pSegment as ILine;
                        listline.Add(pline);
                    }
                }
            }
            return listline;
        }
        /// <summary>
        /// 面转线
        /// </summary>
        /// <param name="pPolyline"></param>
        /// <returns></returns>
        public static IPolyline PolygonToPolyline(IPolygon Polygon)
        {
            IPolyline pPolyline = null;
            if (Polygon != null && Polygon.IsEmpty == false)
            {
                IGeometryCollection pPolygonGeoCol = Polygon as IGeometryCollection;
                ISegment pSegment = null;
                ILine pline = new LineClass();
                ISegmentCollection pLineSegment = new PolylineClass();
                for (int i = 0; i < pPolygonGeoCol.GeometryCount; i++)
                {
                    ISegmentCollection pPolygonSegCol = pPolygonGeoCol.get_Geometry(i) as ISegmentCollection;
                    //for (int j = 0; j < pPolygonSegCol.SegmentCount; j++)
                    //{
                    //    pSegment = pPolygonSegCol.get_Segment(j);
                    //    pline = pSegment as ILine;
                    //}
                    pLineSegment.AddSegmentCollection(pPolygonSegCol);
                }
                pPolyline = (IPolyline)pLineSegment;
            }
            return pPolyline;
        }
        #region 点到直线的距离
        public static double PointDistanceLine(IPoint pt, IPoint pt1, IPoint pt2)
        {
            double l = 0;
            if (pt1.Y == pt2.Y)
            {
                l = Math.Abs(pt.Y - pt1.Y);
            }
            else if (pt1.X == pt2.X)
            {
                l = Math.Abs(pt.X - pt1.X);
            }
            else
            {
                double k = (pt2.Y - pt1.Y) / (pt2.X - pt1.X);
                double c = (pt2.X * pt1.Y - pt1.X * pt2.Y) / (pt2.X - pt1.X);
                l = Math.Abs(k * pt.X - pt.Y + c) / Math.Sqrt(k * k + 1);
            }
            return l;
        }
        #endregion
        /// <summary>
        /// 查询条件要素
        /// </summary>
        /// <param name="feaLayer">IFeatureLayer</param>
        /// <param name="WhereClause">自定义查询条件</param>
        public static IFeature FindFeatureByWhereClause(IFeatureLayer feaLayer, string WhereClause)
        {
            try
            {
                IFeature pFeature = null;
                IQueryFilter queryFilter = new QueryFilterClass();
                queryFilter.WhereClause = WhereClause;
                //Get table and row
                IFeatureCursor pCursor = feaLayer.FeatureClass.Search(queryFilter, false);
                pFeature = pCursor.NextFeature();                
                System.Runtime.InteropServices.Marshal.ReleaseComObject(queryFilter);
                return pFeature;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 查询条件要素
        /// </summary>
        /// <param name="feaLayer">IFeatureLayer</param>
        /// <param name="WhereClause">自定义查询条件</param>
        public static List<IFeature> FindFeatureListByWhereClause(IFeatureLayer feaLayer, string WhereClause)
        {
            try
            {
                List<IFeature> list = new List<IFeature>();
                IFeature pFeature = null;
                IQueryFilter queryFilter = new QueryFilterClass();
                queryFilter.WhereClause = WhereClause;
                //Get table and row
                IFeatureCursor pCursor = feaLayer.FeatureClass.Search(queryFilter, false);
                pFeature = pCursor.NextFeature();
                while (pFeature != null)
                {
                    list.Add(pFeature);
                    pFeature = pCursor.NextFeature();
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(queryFilter);
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //获得已知点在直线上的投影点(新添加的过程)
        public static IPoint GetProjectionPoint(IPoint pPoint, ILine pLine)
        {
            if (pPoint == null || pLine == null)
                return null;

            double k = GetSlope(pLine.FromPoint, pLine.ToPoint);
            IPoint pPt = new PointClass();
            if (k == -9999)
                pPt.PutCoords(pLine.FromPoint.X, pPoint.Y);
            else if (k == 0)
                pPt.PutCoords(pPoint.X, pLine.FromPoint.Y);
            else
            {
                if ((pPoint.Y - pLine.FromPoint.Y) - (pPoint.X - pLine.FromPoint.X) * k == 0)  //点在直线上
                    pPt.PutCoords(pPoint.X, pPoint.Y);
                else
                {
                    pPt.X = (pPoint.X + k * k * pLine.FromPoint.X + k * (pPoint.Y - pLine.FromPoint.Y)) / (1 + k * k);
                    pPt.Y = k * (pPt.X - pLine.FromPoint.X) + pLine.FromPoint.Y;
                }
            }

            return pPt;
        }

        //求直线的斜率
        public static double GetSlope(IPoint pPt1, IPoint pPt2)
        {
            if (pPt1 == null || pPt2 == null)
                return -8888;

            if (pPt1.X == pPt2.X)        //垂直于X轴
                return -9999;
            else if (pPt1.Y == pPt2.Y)   //平行于X轴
                return 0;
            else   //其他情况
                return (pPt2.Y - pPt1.Y) / (pPt2.X - pPt1.X);
        }
        /// <summary>
        /// 计算两点之间的距离
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <returns></returns>
        public static double GetTwoPointDistance(IPoint pt1, IPoint pt2)
        {
            double dDist = -1;

            if (pt1 == null || pt2 == null)
                return dDist;
            else
            {
                dDist = Math.Sqrt(Math.Pow(pt2.X - pt1.X, 2) + Math.Pow(pt2.Y - pt1.Y, 2));
                return dDist;
            }
        }
        #region *******************显示符合条件的纪录********************
        /// <summary>
        /// 给定过滤条件，显示符合条件的纪录
        /// </summary>
        /// <param name="pMap">IMap</param>
        /// <param name="p2DLayer">ILayer</param>
        /// <param name="strFilter">sql语句where 后面的条件</param>
        public static void Show_IsVisiable(IMap pMap, ILayer p2DLayer, string strFilter)
        {
            try
            {
                IFeatureLayer p2DFeatureLayer = (IFeatureLayer)p2DLayer;
                IFeatureLayerDefinition p2DFeatureLayerDefinition = (IFeatureLayerDefinition)p2DFeatureLayer;
                p2DFeatureLayerDefinition.DefinitionExpression = strFilter;
                IActiveView pActive = (IActiveView)pMap;
                pActive.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
            }
            catch (Exception ex)
            {
            }
        }

        #endregion
        #region 空间特征查询排序 IQueryFilterDefinition 用法
        /// <summary>
        /// 空间特征查询排序
        /// </summary>
        /// <param name="pfeatureclass">操作的 pfeatureclass</param>
        /// <param name="strSQL">查询条件sql 语句的where 后面的</param>
        /// <param name="PostfixClause">order by  xxxx</param>
        ///<returns>查询结果集的游标</returns>
        public static IFeatureCursor FeatureSorting(IFeatureClass pfeatureclass, string strSQL, string PostfixClause)
        {
            try
            {
                IQueryFilter pQ = new QueryFilterClass();
                pQ.WhereClause = strSQL;
                IQueryFilterDefinition pQueryDef = (IQueryFilterDefinition)pQ;
                pQueryDef.PostfixClause = PostfixClause;
                IFeatureCursor Res = pfeatureclass.Search(pQ, false);
                return Res;
            }
            catch
            {
                return null;
            }

        }
        #endregion
        /// <summary>
        /// 导出为图片或PDF
        /// </summary>
        public static void ExportPicPdf(IMapControl3 m_MapControl)
        {
            SaveFileDialog savePrinterFileDialog = new SaveFileDialog();
            savePrinterFileDialog.Title = "打印成图片";
            savePrinterFileDialog.Filter = "BMP图片(*.bmp)|*.bmp|JPG图片(*.jpg)|*.jpg|PDF 文件(*.pdf)|*.pdf|PNG 图片(*.png)|*.png";
            if (savePrinterFileDialog.ShowDialog() == DialogResult.OK)
            {
                IActiveView activeView = m_MapControl.ActiveView;
                if (activeView == null)
                {
                    return;
                }

                IExport export = null;

                switch (savePrinterFileDialog.FilterIndex)
                {
                    case 1://bmp
                        export = new ExportBMP() as IExport;//输出BMP格式图片
                        export.ExportFileName = savePrinterFileDialog.FileName;
                        break;
                    case 2://jpg
                        export = new ExportJPEG() as IExport;//输出JPG格式图片
                        export.ExportFileName = savePrinterFileDialog.FileName;
                        break;
                    case 3://pdf
                        export = new ExportPDF() as IExport;//输出PDF格式文件
                        ((IExportPDF2)export).ExportMeasureInfo = true;
                        ((IExportPDF2)export).ExportPDFLayersAndFeatureAttributes = esriExportPDFLayerOptions.esriExportPDFLayerOptionsLayersAndFeatureAttributes;
                        export.ExportFileName = savePrinterFileDialog.FileName;
                        break;
                    case 4://png
                        export = new ExportPNG() as IExport;//输出PNG格式图片
                        export.ExportFileName = savePrinterFileDialog.FileName;
                        break;
                }

                if (export == null)
                {
                    MessageBox.Show(@"请选择输出路径。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int ScreenResolution = 96;
                int OutputResolution = 300;
                export.Resolution = OutputResolution;

                tagRECT exportRECT;
                exportRECT.left = 0;
                exportRECT.top = 0;
                exportRECT.right = activeView.ExportFrame.right * (OutputResolution / ScreenResolution);
                exportRECT.bottom = activeView.ExportFrame.bottom * (OutputResolution / ScreenResolution);

                IEnvelope pixelBoundsEnv = new Envelope() as IEnvelope;
                pixelBoundsEnv.PutCoords(exportRECT.left, exportRECT.top, exportRECT.right, exportRECT.bottom);
                export.PixelBounds = pixelBoundsEnv;
                int hDC = export.StartExporting();
                activeView.Output(hDC, (int)export.Resolution, ref exportRECT, null, null);
                export.FinishExporting();
                export.Cleanup();
            }
        }
        //修改符号角度
        public static void Angle_Symbol(ILayer layer, double angle)
        {
            try
            {
                IGeoFeatureLayer geoFeatureLayer;
                geoFeatureLayer = layer as IGeoFeatureLayer;
                ISimpleRenderer simpleRenderer = (ISimpleRenderer)geoFeatureLayer.Renderer;
                IMarkerSymbol mark = simpleRenderer.Symbol as IMarkerSymbol;
                mark.Angle = angle;
                geoFeatureLayer.Renderer = (IFeatureRenderer)simpleRenderer;
            }
            catch { }
        }
    }
}
