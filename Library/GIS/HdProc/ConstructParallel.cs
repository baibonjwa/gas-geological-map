using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using LibCommon;
using LibEntity;

namespace GIS.HdProc
{
    public class ConstructParallel
    {
        /// <summary>
        /// 计算直线的倾角
        /// </summary>
        /// <param name="AOrigin">起始点</param>
        /// <param name="APoint">终点</param>
        /// <returns>返回倾角弧度值</returns>
        private double PointToAngle(IPoint AOrigin, IPoint APoint)
        {
            if (APoint.X == AOrigin.X)
            {
                if (APoint.Y > AOrigin.Y)
                    return Math.PI * 0.5f;
                else
                    return Math.PI * 1.5f;
            }
            else if (APoint.Y == AOrigin.Y)
            {
                if (APoint.X > AOrigin.X)
                    return 0;
                else
                    return Math.PI;
            }
            else
            {
                double Result = Math.Atan((double)(AOrigin.Y - APoint.Y) / (AOrigin.X - APoint.X));
                if ((APoint.X < AOrigin.X) && (APoint.Y > AOrigin.Y))
                    return Result + Math.PI;
                else if ((APoint.X < AOrigin.X) && (APoint.Y < AOrigin.Y))
                    return Result + Math.PI;
                else if ((APoint.X > AOrigin.X) && (APoint.Y < AOrigin.Y))
                    return Result + 2.0f * Math.PI;
                else return Result;
            }
        }

        /// <summary>
        /// 平行线
        /// </summary>
        /// <param name="inPolyline"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        private IPolyline ConstructOffset(IPolyline inPolyline, double offset)
        {
            if (inPolyline == null || inPolyline.IsEmpty)
            {
                return null;
            }
            object Missing = Type.Missing;
            IConstructCurve constructCurve = new PolylineClass();
            constructCurve.ConstructOffset(inPolyline, offset, ref Missing, ref Missing);
            return constructCurve as IPolyline;
        }

        /// <summary>
        /// 构造平行线的边点坐标串
        /// </summary>
        /// <param name="centerPts">中心线的坐标集合</param>
        /// <param name="lineSpace">巷道宽度的一半</param>
        /// <param name="flag">方向</param>
        /// <returns>返回平行线的坐标串</returns>
        public List<IPoint> GetLRParallelPnts(List<IPoint> centerPts, double lineSpace, int flag)
        {
            List<IPoint> results = new List<IPoint>();
            IPolyline plin = new PolylineClass();//获得的平行线

            IPolyline centerline = new PolylineClass();
            centerline.SpatialReference = Global.spatialref;
            IPointCollection centerlinecols = centerline as IPointCollection;
            for (int i = 0; i < centerPts.Count; i++)
            {
                centerlinecols.AddPoint(centerPts[i]);
            }
            //ITopologicalOperator4 top4 = centerline as ITopologicalOperator4;
            //if (!top4.IsSimple)
            //    top4.Simplify();
            if (flag == 1)//右侧
                plin = ConstructOffset(centerline, lineSpace / 2);
            else//左侧
            {
                plin = ConstructOffset(centerline, -lineSpace / 2);
                plin.ReverseOrientation();//将左侧平行线的点方向逆转
            }
            IPointCollection plincols = plin as IPointCollection;
            for (int i = 0; i < plincols.PointCount; i++)
            {
                IPoint pnttmp = new PointClass();
                pnttmp.PutCoords(plincols.get_Point(i).X, plincols.get_Point(i).Y);
                pnttmp.Z = 0;
                results.Add(pnttmp);
            }

            //for (int i = 0; i < centerPts.Count - 1; i++)
            //{
            //    IPoint offsetPoint = new PointClass();//偏移坐标A
            //    IPoint movePoint = new PointClass();//移动点的坐标
            //    IPoint downPoint = new PointClass();
            //    downPoint = centerPts[i];
            //    movePoint = centerPts[i + 1];
            //    double angle = PointToAngle(downPoint, movePoint);
            //    if (centerPts.Count >= 1)
            //    {
            //        if (flag == 1)//右平行线
            //        {
            //            offsetPoint.X = (float)(Math.Cos(angle + 0.5f * Math.PI) * lineSpace + downPoint.X);
            //            offsetPoint.Y = (float)(Math.Sin(angle + 0.5f * Math.PI) * lineSpace + downPoint.Y);
            //            offsetPoint.Z = downPoint.Z;
            //        }
            //        if (flag == 0)//左平行线
            //        {
            //            offsetPoint.X = (float)(Math.Cos(angle - 0.5f * Math.PI) * lineSpace + downPoint.X);
            //            offsetPoint.Y = (float)(Math.Sin(angle - 0.5f * Math.PI) * lineSpace + downPoint.Y);
            //            offsetPoint.Z = downPoint.Z;
            //        }
            //        Point pt = new PointClass();
            //        pt.X = movePoint.X + offsetPoint.X - downPoint.X;
            //        pt.Y = movePoint.Y + offsetPoint.Y - downPoint.Y;
            //        pt.Z = downPoint.Z;
            //        results.Add(offsetPoint);
            //        results.Add(pt);
            //    }
            //}
            return results;
        }

        /// <summary>
        /// 计算两条直线的交点坐标
        /// </summary>
        /// <param name="pline0Start"></param>
        /// <param name="pline0End"></param>
        /// <param name="pline1Start"></param>
        /// <param name="plineEnd"></param>
        /// <returns></returns>
        private IPoint GetIntersection(IPoint pline0Start, IPoint pline0End, IPoint pline1Start, IPoint pline1End)
        {
            double a = 0, b = 0;
            int state = 0;
            IPoint pntIntersect = new PointClass();
            if (pline0Start.X != pline0End.X)
            {
                a = (pline0End.Y - pline0Start.Y) / (pline0End.X - pline0Start.X);
                state |= 1;
            }
            if (pline1Start.X != pline1End.X)
            {
                b = (pline1End.Y - pline1Start.Y) / (pline1End.X - pline1Start.X);
                state |= 2;
            }
            switch (state)
            {
                case 0://L1 L2都平行于Y轴
                    {
                        if (pline0Start.X == pline1Start.X)//两条直线互相重合，且平行于Y轴，无法计算交点坐标
                        {
                            pntIntersect.X = 0;
                            pntIntersect.Y = 0;
                        }
                        else
                        {
                            pntIntersect.X = 0;
                            pntIntersect.Y = 0;
                        }
                    }
                    break;
                case 1://L1存在斜率，L2平行于Y轴
                    {
                        double x = pline1Start.X;
                        double y = (pline0Start.X - x) * (-a) + pline0Start.Y;
                        pntIntersect.X = x;
                        pntIntersect.Y = y;
                    }
                    break;
                case 2://L1平行Y轴，L2存在斜率
                    {
                        double x = pline0Start.X;
                        double y = (pline1Start.X - x) * (-b) + pline1Start.Y;
                        pntIntersect.X = x;
                        pntIntersect.Y = y;
                    }
                    break;
                case 3:
                    {
                        if (a == b)
                        {
                            pntIntersect.X = 0;
                            pntIntersect.Y = 0;
                        }
                        double x = (a + pline0Start.X - b * pline1Start.X - pline0Start.Y + pline1Start.Y) / (a - b);
                        double y = a * x - a * pline0Start.X + pline0Start.Y;
                        pntIntersect.X = x;
                        pntIntersect.Y = y;
                    }
                    break;
            }
            return pntIntersect;
        }

        /// <summary>
        /// 计算同侧相邻两条直线的交点坐标
        /// </summary>
        /// <param name="LinePnts">中心线 同侧 每段线段的平行线的点串</param>
        /// <returns>同侧平行线的点串</returns>
        public List<IPoint> CalculateRegPnts(List<IPoint> LinePnts)
        {
            List<IPoint> results = new List<IPoint>();
            if (LinePnts.Count > 2)
            {
                //LinePnts.RemoveRange(LinePnts.Count - 2, 2);
                results.Add(LinePnts[0]);
                for (int i = 1; i < LinePnts.Count - 2; i += 2)
                {
                    IPoint pntintersect = GetIntersection(LinePnts[i - 1], LinePnts[i], LinePnts[i + 1], LinePnts[i + 1]);
                    pntintersect.Z = 0.0;
                    //double k1 = 0.0, b1 = 0.0, k2 = 0.0, b2 = 0.0, x = 0.0, y = 0.0;
                    //if (LinePnts[i].X != LinePnts[i - 1].X && LinePnts[i + 2].X != LinePnts[i + 1].X)
                    //{
                    //    k1 = (LinePnts[i].Y - LinePnts[i - 1].Y) * 1.0 / (LinePnts[i].X - LinePnts[i - 1].X);
                    //    b1 = LinePnts[i].Y - k1 * LinePnts[i].X;
                    //    k2 = (LinePnts[i + 2].Y - LinePnts[i + 1].Y) * 1.0 / (LinePnts[i + 2].X - LinePnts[i + 1].X);
                    //    b2 = LinePnts[i + 2].Y - k2 * LinePnts[i + 2].X;
                    //    if (k1 != k2)
                    //    {
                    //        x = Math.Abs((b2 - b1) / (k2 - k1));
                    //        x = (b2 - b1) / (k2 - k1);
                    //        y = k1 * x + b1;
                    //    }
                    //    else
                    //    {
                    //        x = LinePnts[i].X;
                    //        y = LinePnts[i].Y;
                    //    }
                    //}
                    //else if (LinePnts[i].X == LinePnts[i - 1].X && LinePnts[i + 2].X != LinePnts[i + 1].X)
                    //{
                    //    k2 = (LinePnts[i + 2].Y - LinePnts[i + 1].Y) * 1.0 / (LinePnts[i + 2].X - LinePnts[i + 1].X);
                    //    b2 = LinePnts[i + 2].Y - k2 * LinePnts[i + 2].X;
                    //    x = LinePnts[i].X;
                    //    y = k2 * x + b2;
                    //}
                    //else if (LinePnts[i].X != LinePnts[i - 1].X && LinePnts[i + 2].X == LinePnts[i + 1].X)
                    //{
                    //    k1 = (LinePnts[i].Y - LinePnts[i - 1].Y) * 1.0 / (LinePnts[i].X - LinePnts[i - 1].X);
                    //    b1 = LinePnts[i].Y - k1 * LinePnts[i].X;
                    //    x = LinePnts[i + 2].X;
                    //    y = k1 * x + b1;
                    //}
                    //IPoint pt = new PointClass();
                    //pt.X = x;
                    //pt.Y = y;
                    //pt.Z = LinePnts[i].Z;
                    //results.Add(pt);
                    if (pntintersect != null)
                    {
                        results.Add(pntintersect);
                    }
                }
                results.Add(LinePnts[LinePnts.Count - 1]);
            }
            else
            {
                results.AddRange(LinePnts);
            }
            return results;
        }

        /// <summary>
        /// 构造多边形的边点坐标组
        /// </summary>
        /// <param name="pntsl">左侧平行线的点串</param>
        /// <param name="pntsr">右侧平行线的点串</param>
        /// <returns>返回多边形点的坐标串</returns>
        public List<IPoint> ConstructPnts(List<IPoint> pntsr, List<IPoint> pntsl)
        {
            List<IPoint> results = new List<IPoint>();
            results.AddRange(pntsr);
            //pntsl.Reverse();
            results.AddRange(pntsl);
            //results.Add(pntsr[0]);
            return results;
        }

        /// <summary>
        /// 添加导线点线图层元素
        /// </summary>
        /// <param name="pnts"></param>
        /// <param name="layer"></param>
        public void AddDxdLines(List<IPoint> pnts, Dictionary<string, string> dics, IFeatureLayer layer, List<WirePoint> cols = null)
        {
            try
            {
                IFeatureClass Featureclass = layer.FeatureClass;
                IWorkspaceEdit workspace = (IWorkspaceEdit)(Featureclass as IDataset).Workspace;
                workspace.StartEditing(false);
                workspace.StartEditOperation();
                for (int i = 0; i < pnts.Count; i++)
                {
                    IFeature fealin = Featureclass.CreateFeature();
                    IPolyline plin = new PolylineClass();
                    ISegmentCollection segcols = plin as ISegmentCollection;
                    ICurve circle = Global.commonclss.CreateCircleArc(pnts[i], Global.radius, true);
                    segcols.AddSegment(circle as ISegment);
                    fealin.Shape = plin;
                    if (cols != null)
                    {
                        string name = cols[i].wire_point_name;
                        int NamePos = fealin.Fields.FindField(GIS_Const.FIELD_NAME);
                        fealin.set_Value(NamePos, name);
                    }
                    foreach (string key in dics.Keys)
                    {
                        int findex = fealin.Fields.FindField(key);
                        if (findex != -1)
                        {
                            fealin.set_Value(findex, dics[key]);
                        }
                    }

                    fealin.Store();
                }
                workspace.StopEditOperation();
                workspace.StopEditing(true);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 添加对象到指定的图层
        /// </summary>
        /// <param name="pnts">对象的点串</param>
        /// <param name="dics">属性字段</param>
        /// <param name="hdlayer">添加对象图层</param>
        public void AddHangdaoToLayer(List<IPoint> pnts, Dictionary<string, string> dics, IFeatureLayer layer, List<WirePoint> pntinfos = null)
        {
            //try
            //{
            IFeatureClass Featureclass = layer.FeatureClass;
            IWorkspaceEdit workspace = (IWorkspaceEdit)(Featureclass as IDataset).Workspace;
            workspace.StartEditing(false);
            workspace.StartEditOperation();
            esriGeometryType type = layer.FeatureClass.ShapeType;
            int index = -1;
            IGeometryDef geometryDef = null;

            switch (type)
            {
                case esriGeometryType.esriGeometryPolygon://添加面到相应的图层
                    IPolygon polygon = new PolygonClass();

                    //添加
                    polygon.SpatialReference = Global.spatialref;
                    IPointCollection regpntcols = (IPointCollection)polygon;
                    for (int i = 0; i < pnts.Count; i++)
                    {
                        regpntcols.AddPoint(pnts[i]);
                    }
                    polygon.Close();
                    //拓扑检查一下
                    ITopologicalOperator4 tops = polygon as ITopologicalOperator4;
                    if (!tops.IsSimple)
                        tops.Simplify();
                    IFeature fea = Featureclass.CreateFeature();
                    index = fea.Fields.FindField(GIS_Const.FIELD_SHAPE);
                    geometryDef = fea.Fields.get_Field(index).GeometryDef as IGeometryDef;
                    if (geometryDef.HasZ)
                    {
                        IZAware pZAware = (IZAware)polygon;
                        pZAware.ZAware = true;

                    }

                    fea.Shape = polygon;
                    foreach (string key in dics.Keys)
                    {
                        int fIndex = fea.Fields.FindField(key);
                        if (fIndex != -1)
                        {
                            fea.set_Value(fIndex, dics[key]);
                        }
                    }
                    fea.Store();
                    //地图定位跳转
                    Global.commonclss.JumpToGeometry(polygon);
                    break;

                case esriGeometryType.esriGeometryPolyline://中心线
                    IPolyline polyline = new PolylineClass();
                    IPointCollection plinecols = (IPointCollection)polyline;
                    for (int i = 0; i < pnts.Count; i++)
                    {
                        plinecols.AddPoint(pnts[i]);
                    }
                    polyline.SpatialReference = Global.spatialref;
                    //拓扑检查一下
                    ITopologicalOperator4 tops1 = polyline as ITopologicalOperator4;
                    if (!tops1.IsSimple)
                        tops1.Simplify();
                    //添加
                    IFeature fealin = Featureclass.CreateFeature();

                    IPointCollection polylinenew = new PolylineClass();
                    for (int i = 0; i < plinecols.PointCount; i++)
                    {
                        IPoint pnt = plinecols.get_Point(i);
                        IPoint pntnew = new PointClass();
                        pntnew.X = pnt.X;
                        pntnew.Y = pnt.Y;
                        pntnew.Z = 0.0;
                        polylinenew.AddPoint(pntnew);
                    }
                    polyline = polylinenew as IPolyline;
                    index = fealin.Fields.FindField(GIS_Const.FIELD_SHAPE);
                    geometryDef = fealin.Fields.get_Field(index).GeometryDef as IGeometryDef;
                    if (geometryDef.HasZ)
                    {
                        IZAware pZAware = (IZAware)polyline;
                        pZAware.ZAware = true;
                    }

                    fealin.Shape = polyline;
                    foreach (string key in dics.Keys)
                    {
                        int findex = fealin.Fields.FindField(key);
                        if (findex != -1)
                        {
                            fealin.set_Value(findex, dics[key]);
                        }
                    }
                    fealin.Store();
                    //地图定位跳转
                    Global.commonclss.JumpToGeometry(polyline);
                    break;

                case esriGeometryType.esriGeometryPoint://导线点
                    for (int i = 0; i < pnts.Count; i++)
                    {
                        IFeature feapnt = Featureclass.CreateFeature();
                        index = feapnt.Fields.FindField(GIS_Const.FIELD_SHAPE);
                        geometryDef = feapnt.Fields.get_Field(index).GeometryDef as IGeometryDef;
                        if (geometryDef.HasZ)
                        {
                            IZAware pZAware = (IZAware)pnts[i];
                            pZAware.ZAware = true;
                        }
                        feapnt.Shape = pnts[i];
                        if (pntinfos != null)
                        {
                            string name = pntinfos[i].wire_point_name;
                            int NamePos = feapnt.Fields.FindField(GIS_Const.FIELD_NAME);
                            feapnt.set_Value(NamePos, name);
                        }
                        foreach (string key in dics.Keys)
                        {
                            int findex = feapnt.Fields.FindField(key);
                            if (findex != -1)
                            {
                                feapnt.set_Value(findex, dics[key]);
                            }
                        }

                        feapnt.Store();
                    }
                    break;
            }
            workspace.StopEditOperation();
            workspace.StopEditing(true);
            //}
            //catch(Exception ei)
            //{s
            //    throw new Exception();
            //}
        }

        /// <summary>
        /// 添加(分段)对象到指定的图层
        /// </summary>
        /// <param name="pnts">对象的点串</param>
        /// <param name="sxzs">上下</param>
        /// <param name="hdlayer">添加对象图层</param>
        /// <param name="BS">对象类型（针对巷道）</param>
        public void AddFDLineToLayer(List<IPoint> pnts, Dictionary<string, string> sxzs, IFeatureLayer hdlayer, int BS)
        {
            try
            {
                IFeatureClass Featureclass = hdlayer.FeatureClass;
                IWorkspaceEdit workspace = (IWorkspaceEdit)(Featureclass as IDataset).Workspace;
                workspace.StartEditing(false);
                workspace.StartEditOperation();
                esriGeometryType type = hdlayer.FeatureClass.ShapeType;
                for (int i = 0; i < pnts.Count - 1; i++)
                {
                    IPolyline line = new PolylineClass();
                    line.FromPoint = pnts[i];
                    line.ToPoint = pnts[i + 1];
                    line.SpatialReference = Global.spatialref;
                    //拓扑检查一下
                    ITopologicalOperator4 tops = line as ITopologicalOperator4;
                    if (!tops.IsSimple)
                        tops.Simplify();
                    //查询和当前线相交的巷道
                    int Idbs = 0;
                    if (BS == 1)
                        Idbs = Global.commonclss.SearchHdByLine(line, sxzs[GIS_Const.FIELD_HDID], Global.centerfdlyr);
                    //
                    IFeature fealin = Featureclass.CreateFeature();
                    int index = fealin.Fields.FindField(GIS_Const.FIELD_SHAPE);
                    IGeometryDef geometryDef = fealin.Fields.get_Field(index).GeometryDef as IGeometryDef;
                    if (geometryDef.HasZ)
                    {
                        IZAware pZAware = (IZAware)line;
                        pZAware.ZAware = true;
                        fealin.Shape = line;
                        foreach (string key in sxzs.Keys)
                        {
                            int fldp = fealin.Fields.FindField(key);
                            if (fldp != -1)
                            {
                                if (key == GIS_Const.FIELD_XH)
                                {
                                    int xh = Convert.ToInt16(sxzs[key]);
                                    fealin.set_Value(fealin.Fields.FindField(key), xh + i);
                                }
                                else if (key == GIS_Const.FIELD_ID)
                                {
                                    fealin.set_Value(fealin.Fields.FindField(key), Idbs);
                                }
                                else
                                    fealin.set_Value(fealin.Fields.FindField(key), sxzs[key]);
                            }
                        }
                        fealin.Store();
                    }
                    else
                    {
                        fealin.Shape = line;
                        foreach (string key in sxzs.Keys)
                        {
                            if (key == GIS_Const.FIELD_XH)
                            {
                                int xh = Convert.ToInt16(sxzs[key]);
                                fealin.set_Value(fealin.Fields.FindField(key), xh + i);
                            }
                            else if (key == GIS_Const.FIELD_ID)
                            {
                                fealin.set_Value(fealin.Fields.FindField(key), Idbs);
                            }
                            else

                                fealin.set_Value(fealin.Fields.FindField(key), sxzs[key]);
                        }
                        fealin.Store();
                    }
                }
                workspace.StopEditOperation();
                workspace.StopEditing(true);
            }
            catch (Exception ei)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pnts0"></param>
        /// <param name="sxzs"></param>
        /// <param name="hdlayer"></param>
        /// <returns></returns>
        public IPolygon AddRegToLayer(List<IPoint> pnts0, Dictionary<string, string> sxzs, IFeatureLayer hdlayer)
        {
            IPolygon polygon = new PolygonClass();
            polygon.SpatialReference = Global.spatialref;
            try
            {
                IFeatureClass Featureclass = hdlayer.FeatureClass;
                IWorkspaceEdit workspace = (IWorkspaceEdit)(Featureclass as IDataset).Workspace;
                workspace.StartEditing(false);
                workspace.StartEditOperation();
                for (int i = 0; i < pnts0.Count; i++)
                {
                    IPointCollection regpntcols = (IPointCollection)polygon;
                    regpntcols.AddPoint(pnts0[i]);
                }
                polygon.Close();
                IFeature fea = Featureclass.CreateFeature();
                int index = fea.Fields.FindField(GIS_Const.FIELD_SHAPE);
                IGeometryDef geometryDef = fea.Fields.get_Field(index).GeometryDef as IGeometryDef;
                if (geometryDef.HasZ)
                {
                    IZAware pZAware = (IZAware)polygon;
                    pZAware.ZAware = true;
                    fea.Shape = polygon;
                    foreach (string key in sxzs.Keys)
                    {
                        fea.set_Value(fea.Fields.FindField(key), sxzs[key]);
                    }
                    fea.Store();
                }
                else
                {
                    fea.Shape = polygon;
                    if (sxzs != null)
                    {
                        foreach (string key in sxzs.Keys)
                        {
                            if (fea.Fields.FindField(key) != -1)
                            {
                                fea.set_Value(fea.Fields.FindField(key), sxzs[key]);
                            }
                        }
                    }
                    fea.Store();
                }
                workspace.StopEditOperation();
                workspace.StopEditing(true);
            }
            catch (Exception ei)
            {

            }
            return polygon;
        }

        /// <summary>
        /// 将分段添加对象到指定的图层
        /// </summary>
        /// <param name="hdpnts">对象的点串</param>
        /// <param name="hdid">对象Id</param>
        /// <param name="hdtype">对象类型（针对巷道）</param>
        /// <param name="hdlayer">添加对象图层</param>
        /// <param name="centpnts">中心线上的点串</param>
        //public void AddFDRegToLayer(List<IPoint> pnts0, List<IPoint> pnts1, List<IPoint> centpnts, Dictionary<string, string> sxzs, IFeatureLayer hdlayer, double hdwid)
        //{
        //    try
        //    {
        //        //int count = 0;
        //        //int num0 = pnts0.Count;
        //        //int num1 = pnts1.Count;
        //        //if (num0 == num1)
        //        //    count = num0;
        //        //else if (num0 > num1)
        //        //    count = num1;
        //        //else
        //        //    count = num0;
        //        //if (count > centpnts.Count)
        //        //    count = centpnts.Count;
        //        int count = pnts0.Count;
        //        //pnts1.Reverse();

        //        IFeatureClass Featureclass = hdlayer.FeatureClass;
        //        IWorkspaceEdit workspace = (IWorkspaceEdit)(Featureclass as IDataset).Workspace;
        //        workspace.StartEditing(false);
        //        workspace.StartEditOperation();
        //        //for (int i = 0; i < pnts0.Count-1; i++)
        //        for (int i = 0; i < count - 1; i++)
        //        {
        //            IPolygon polygon = new PolygonClass();
        //            polygon.SpatialReference = Global.spatialref;
        //            IPointCollection regpntcols = (IPointCollection)polygon;
        //            regpntcols.AddPoint(pnts0[i]);
        //            regpntcols.AddPoint(pnts0[i + 1]);
        //            regpntcols.AddPoint(pnts1[i + 1]);
        //            regpntcols.AddPoint(pnts1[i]);

        //            polygon.Close();
        //            //拓扑检查一下
        //            ITopologicalOperator4 tops = polygon as ITopologicalOperator4;
        //            if (!tops.IsSimple)
        //                tops.Simplify();
        //            //查询附近的巷道，确定Id的值，确定对应的符号
        //            IPolyline plincenter = new PolylineClass();
        //            plincenter.FromPoint = centpnts[i];
        //            plincenter.ToPoint = centpnts[i + 1];
        //            plincenter.SpatialReference = Global.spatialref;
        //            //int Idbs = Global.commonclss.SearchHdByLine(plincenter,sxzs["HdId"], Global.centerfdlyr);
        //            //获取巷道分段所包含中心线的xh值
        //            string sql = "\"" + GIS_Const.FIELD_HDID + "\"='" + sxzs[GIS_Const.FIELD_HDID] + "'";
        //            //int[] getAttr = GetCenterLineXH(polygon as IGeometry, sql, Global.centerfdlyr,hdwid);
        //            int[] getAttr = GetCenterLineXHNew(polygon as IGeometry, sql, Global.centerfdlyr, hdwid);
        //            int xh = getAttr[0];
        //            int Idbs = getAttr[1];
        //            //创建Feature
        //            IFeature fea = Featureclass.CreateFeature();
        //            int index = fea.Fields.FindField(GIS_Const.FIELD_SHAPE);
        //            if (index != -1)
        //            {
        //                IGeometryDef geometryDef = fea.Fields.get_Field(index).GeometryDef as IGeometryDef;
        //                if (geometryDef.HasZ)
        //                {
        //                    IZAware pZAware = (IZAware)polygon;
        //                    pZAware.ZAware = true;
        //                    fea.Shape = polygon;
        //                    foreach (string key in sxzs.Keys)
        //                    {
        //                        if (key == GIS_Const.FIELD_XH)
        //                        {
        //                            //int xh = Convert.ToInt16(sxzs[key]);
        //                            fea.set_Value(fea.Fields.FindField(key), xh);
        //                        }
        //                        else if (key == GIS_Const.FIELD_ID)
        //                        {
        //                            fea.set_Value(fea.Fields.FindField(key), Idbs);
        //                        }
        //                        else

        //                            fea.set_Value(fea.Fields.FindField(key), sxzs[key]);
        //                    }
        //                    fea.Store();
        //                }
        //                else
        //                {
        //                    fea.Shape = polygon;
        //                    foreach (string key in sxzs.Keys)
        //                    {
        //                        int fp = fea.Fields.FindField(key);
        //                        if (fp != -1)
        //                        {
        //                            if (key == GIS_Const.FIELD_XH)
        //                            {
        //                                //int xh = Convert.ToInt16(sxzs[key]);
        //                                fea.set_Value(fea.Fields.FindField(key), xh);
        //                            }
        //                            else if (key == GIS_Const.FIELD_ID)
        //                            {
        //                                fea.set_Value(fea.Fields.FindField(key), Idbs);
        //                            }
        //                            else

        //                                fea.set_Value(fea.Fields.FindField(key), sxzs[key]);
        //                        }
        //                    }
        //                    fea.Store();
        //                }
        //            }
        //        }
        //        workspace.StopEditOperation();
        //        workspace.StopEditing(true);
        //    }
        //    catch (Exception ei)
        //    {
        //        throw;
        //    }

        //    //}
        //    //catch (Exception ei)
        //    //{
        //    //    throw new Exception();
        //    //}
        //}
        public void AddFDRegToLayer_JC(List<IPoint> pnts0, List<IPoint> pnts1, List<IPoint> centpnts, Dictionary<string, string> sxzs, IFeatureLayer hdlayer, double hdwid)
        {
            try
            {
                //int count = 0;
                //int num0 = pnts0.Count;
                //int num1 = pnts1.Count;
                //if (num0 == num1)
                //    count = num0;
                //else if (num0 > num1)
                //    count = num1;
                //else
                //    count = num0;
                //if (count > centpnts.Count)
                //    count = centpnts.Count;
                int count = pnts0.Count;
                //pnts1.Reverse();

                IFeatureClass Featureclass = hdlayer.FeatureClass;
                IWorkspaceEdit workspace = (IWorkspaceEdit)(Featureclass as IDataset).Workspace;
                workspace.StartEditing(false);
                workspace.StartEditOperation();
                //for (int i = 0; i < pnts0.Count-1; i++)
                for (int i = 0; i < count - 1; i++)
                {
                    IPolygon polygon = new PolygonClass();
                    polygon.SpatialReference = Global.spatialref;
                    IPointCollection regpntcols = (IPointCollection)polygon;
                    regpntcols.AddPoint(pnts0[i]);
                    regpntcols.AddPoint(pnts0[i + 1]);
                    regpntcols.AddPoint(pnts1[i + 1]);
                    regpntcols.AddPoint(pnts1[i]);

                    polygon.Close();
                    //拓扑检查一下
                    ITopologicalOperator4 tops = polygon as ITopologicalOperator4;
                    if (!tops.IsSimple)
                        tops.Simplify();
                    //查询附近的巷道，确定Id的值，确定对应的符号
                    IPolyline plincenter = new PolylineClass();
                    plincenter.FromPoint = centpnts[i];
                    plincenter.ToPoint = centpnts[i + 1];
                    plincenter.SpatialReference = Global.spatialref;
                    //int Idbs = Global.commonclss.SearchHdByLine(plincenter,sxzs["HdId"], Global.centerfdlyr);
                    //获取巷道分段所包含中心线的xh值
                    string sql = "\"" + GIS.GIS_Const.FIELD_HDID + "\"='" + sxzs[GIS.GIS_Const.FIELD_HDID] + "'";
                    //int[] getAttr = GetCenterLineXH(polygon as IGeometry, sql, Global.centerfdlyr,hdwid);
                    int[] getAttr = GetCenterLineXHNew(polygon as IGeometry, sql, Global.centerfdlyr, hdwid);
                    int xh = getAttr[0];
                    int Idbs = getAttr[1];
                    //创建Feature
                    IFeature fea = Featureclass.CreateFeature();
                    int index = fea.Fields.FindField(GIS_Const.FIELD_SHAPE);
                    if (index != -1)
                    {
                        IGeometryDef geometryDef = fea.Fields.get_Field(index).GeometryDef as IGeometryDef;
                        if (geometryDef.HasZ)
                        {
                            IZAware pZAware = (IZAware)polygon;
                            pZAware.ZAware = true;
                            fea.Shape = polygon;
                            foreach (string key in sxzs.Keys)
                            {
                                if (key == GIS.GIS_Const.FIELD_XH)
                                {
                                    //int xh = Convert.ToInt16(sxzs[key]);
                                    fea.set_Value(fea.Fields.FindField(key), xh);
                                }
                                else if (key == GIS.GIS_Const.FIELD_ID)
                                {
                                    fea.set_Value(fea.Fields.FindField(key), Idbs);
                                }
                                else

                                    fea.set_Value(fea.Fields.FindField(key), sxzs[key]);
                            }
                            fea.Store();
                        }
                        else
                        {
                            fea.Shape = polygon;
                            foreach (string key in sxzs.Keys)
                            {
                                int fp = fea.Fields.FindField(key);
                                if (fp != -1)
                                {
                                    if (key == GIS.GIS_Const.FIELD_XH)
                                    {
                                        //int xh = Convert.ToInt16(sxzs[key]);
                                        fea.set_Value(fea.Fields.FindField(key), xh);
                                    }
                                    else if (key == GIS.GIS_Const.FIELD_ID)
                                    {
                                        fea.set_Value(fea.Fields.FindField(key), Idbs);
                                    }
                                    else

                                        fea.set_Value(fea.Fields.FindField(key), sxzs[key]);
                                }
                            }
                            fea.Store();
                        }
                    }
                }
                workspace.StopEditOperation();
                workspace.StopEditing(true);
            }
            catch (Exception ei)
            {
                throw;
            }

            //}
            //catch (Exception ei)
            //{
            //    throw new Exception();
            //}
        }


        public void AddFDRegToLayer(List<IPoint> pnts0, List<IPoint> pnts1, List<IPoint> centpnts, Dictionary<string, string> sxzs, IFeatureLayer hdlayer, double hdwid)
        {
            try
            {
                //int count = 0;
                //int num0 = pnts0.Count;
                //int num1 = pnts1.Count;
                //if (num0 == num1)
                //    count = num0;
                //else if (num0 > num1)
                //    count = num1;
                //else
                //    count = num0;
                //if (count > centpnts.Count)
                //    count = centpnts.Count;
                int count = pnts0.Count;
                pnts1.Reverse();

                IFeatureClass Featureclass = hdlayer.FeatureClass;
                IWorkspaceEdit workspace = (IWorkspaceEdit)(Featureclass as IDataset).Workspace;
                workspace.StartEditing(false);
                workspace.StartEditOperation();
                //for (int i = 0; i < pnts0.Count-1; i++)
                for (int i = 0; i < count - 1; i++)
                {
                    IPolygon polygon = new PolygonClass();
                    polygon.SpatialReference = Global.spatialref;
                    IPointCollection regpntcols = (IPointCollection)polygon;
                    regpntcols.AddPoint(pnts0[i]);
                    regpntcols.AddPoint(pnts0[i + 1]);
                    regpntcols.AddPoint(pnts1[i + 1]);
                    regpntcols.AddPoint(pnts1[i]);

                    polygon.Close();
                    //拓扑检查一下
                    ITopologicalOperator4 tops = polygon as ITopologicalOperator4;
                    if (!tops.IsSimple)
                        tops.Simplify();
                    //查询附近的巷道，确定Id的值，确定对应的符号
                    IPolyline plincenter = new PolylineClass();
                    plincenter.FromPoint = centpnts[i];
                    plincenter.ToPoint = centpnts[i + 1];
                    plincenter.SpatialReference = Global.spatialref;
                    //int Idbs = Global.commonclss.SearchHdByLine(plincenter,sxzs["HdId"], Global.centerfdlyr);
                    //获取巷道分段所包含中心线的xh值
                    string sql = "\"" + GIS.GIS_Const.FIELD_HDID + "\"='" + sxzs[GIS.GIS_Const.FIELD_HDID] + "'";
                    //int[] getAttr = GetCenterLineXH(polygon as IGeometry, sql, Global.centerfdlyr,hdwid);
                    int[] getAttr = GetCenterLineXHNew(polygon as IGeometry, sql, Global.centerfdlyr, hdwid);
                    int xh = getAttr[0];
                    int Idbs = getAttr[1];
                    //创建Feature
                    IFeature fea = Featureclass.CreateFeature();
                    int index = fea.Fields.FindField(GIS_Const.FIELD_SHAPE);
                    if (index != -1)
                    {
                        IGeometryDef geometryDef = fea.Fields.get_Field(index).GeometryDef as IGeometryDef;
                        if (geometryDef.HasZ)
                        {
                            IZAware pZAware = (IZAware)polygon;
                            pZAware.ZAware = true;
                            fea.Shape = polygon;
                            foreach (string key in sxzs.Keys)
                            {
                                if (key == GIS.GIS_Const.FIELD_XH)
                                {
                                    //int xh = Convert.ToInt16(sxzs[key]);
                                    fea.set_Value(fea.Fields.FindField(key), xh);
                                }
                                else if (key == GIS.GIS_Const.FIELD_ID)
                                {
                                    fea.set_Value(fea.Fields.FindField(key), Idbs);
                                }
                                else

                                    fea.set_Value(fea.Fields.FindField(key), sxzs[key]);
                            }
                            fea.Store();
                        }
                        else
                        {
                            fea.Shape = polygon;
                            foreach (string key in sxzs.Keys)
                            {
                                int fp = fea.Fields.FindField(key);
                                if (fp != -1)
                                {
                                    if (key == GIS.GIS_Const.FIELD_XH)
                                    {
                                        //int xh = Convert.ToInt16(sxzs[key]);
                                        fea.set_Value(fea.Fields.FindField(key), xh);
                                    }
                                    else if (key == GIS.GIS_Const.FIELD_ID)
                                    {
                                        fea.set_Value(fea.Fields.FindField(key), Idbs);
                                    }
                                    else

                                        fea.set_Value(fea.Fields.FindField(key), sxzs[key]);
                                }
                            }
                            fea.Store();
                        }
                    }
                }
                workspace.StopEditOperation();
                workspace.StopEditing(true);
            }
            catch (Exception ei)
            {
                throw;
            }

            //}
            //catch (Exception ei)
            //{
            //    throw new Exception();
            //}
        }


        /// <summary>
        /// 获得巷道分段面下的中心线的xh和id值
        /// </summary>
        /// <param name="geom">查询对象</param>
        /// <param name="sql">查询条件</param>
        /// <param name="featurelyr">图层</param>
        /// <returns>返回xh,id数组</returns>
        public int[] GetCenterLineXH(IGeometry geom, string sql, IFeatureLayer featurelyr, double hdwid)
        {
            IFeature feature = null;
            IFeatureClass featureclass = featurelyr.FeatureClass;
            IFeatureCursor feacursor = null;
            if (featureclass != null)
            {
                //获得与面相交的巷道分段中心线
                ISpatialFilter filter = new SpatialFilter();
                filter.Geometry = geom;
                filter.WhereClause = sql;
                filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                feacursor = featureclass.Search(filter, false);
                IFeature newfea = feacursor.NextFeature();
                IFeature afea = null;
                List<IFeature> listfea = new List<IFeature>();
                while (newfea != null)
                {
                    listfea.Add(newfea);
                    newfea = feacursor.NextFeature();
                }
                if (listfea.Count > 1)
                {   //对中心线做buffer,取buffer与巷道面交集大的为当前巷道下的中心线
                    for (int i = 0; i < listfea.Count - 1; i++)
                    {
                        newfea = listfea[i];
                        IPolyline polyline = newfea.ShapeCopy as IPolyline;
                        double interarea = GetIntersectGeoArea(geom, polyline, hdwid);
                        afea = listfea[i + 1];
                        IPolyline apolyline = afea.ShapeCopy as IPolyline;
                        double ainterarea = GetIntersectGeoArea(geom, apolyline, hdwid);
                        if (ainterarea > interarea)
                        { feature = afea; }
                        else
                        { feature = newfea; }
                    }
                }
                else
                { feature = listfea[0]; }
            }
            int m = feature.Fields.FindField(GIS_Const.FIELD_XH);
            int xh = Convert.ToInt16(feature.get_Value(m).ToString());
            m = feature.Fields.FindField(GIS_Const.FIELD_ID);
            int id = Convert.ToInt16(feature.get_Value(m).ToString());
            int[] result = new int[2] { xh, id };
            return result;
        }
        /// <summary>
        /// 查询分段巷道覆盖的线的序号
        /// </summary>
        /// <param name="geom"></param>
        /// <param name="sql"></param>
        /// <param name="featurelyr"></param>
        /// <param name="hdwid"></param>
        /// <returns></returns>
        public int[] GetCenterLineXHNew(IGeometry geom, string sql, IFeatureLayer featurelyr, double hdwid)
        {
            IFeature feature = null;
            IFeatureClass featureclass = featurelyr.FeatureClass;
            IFeatureCursor feacursor = null;
            if (featureclass != null)
            {
                //获得与面相交的巷道分段中心线
                ISpatialFilter filter = new SpatialFilter();
                filter.Geometry = geom;
                filter.WhereClause = sql;
                filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                feacursor = featureclass.Search(filter, false);
                IFeature newfea = feacursor.NextFeature();
                IFeature afea = null;
                List<IFeature> listfea = new List<IFeature>();
                while (newfea != null)
                {
                    listfea.Add(newfea);
                    newfea = feacursor.NextFeature();
                }
                //if (listfea.Count > 1)
                //{   //对中心线做buffer,取buffer与巷道面交集大的为当前巷道下的中心线
                //    for (int i = 0; i < listfea.Count - 1; i++)
                //    {
                //        newfea = listfea[i];
                //        IPolyline polyline = newfea.ShapeCopy as IPolyline;
                //        double interarea = GetIntersectGeoArea(geom, polyline, hdwid);
                //        afea = listfea[i + 1];
                //        IPolyline apolyline = afea.ShapeCopy as IPolyline;
                //        double ainterarea = GetIntersectGeoArea(geom, apolyline, hdwid);
                //        if (ainterarea > interarea)
                //        { feature = afea; }
                //        else
                //        { feature = newfea; }
                //    }
                //}
                //else
                //{ feature = listfea[0]; }
                feature = listfea[0];
            }
            int m = feature.Fields.FindField(GIS_Const.FIELD_XH);
            int xh = Convert.ToInt16(feature.get_Value(m).ToString());
            m = feature.Fields.FindField(GIS_Const.FIELD_ID);
            int id = Convert.ToInt16(feature.get_Value(m).ToString());
            int[] result = new int[2] { xh, id };
            return result;
        }
        /// <summary>
        /// 对线做buffer,求得buffer与geom相交部分的面积
        /// </summary>
        /// <param name="geom">查询对象-面</param>
        /// <param name="polyline">线</param>
        /// <param name="hdwid">巷道宽度</param>
        private double GetIntersectGeoArea(IGeometry geom, IPolyline polyline, double hdwid)
        {
            ITopologicalOperator topo = polyline as ITopologicalOperator;
            IGeometry geobuffer = topo.Buffer(hdwid / 2);
            topo = geom as ITopologicalOperator;
            IGeometry intergeo = topo.Intersect(geobuffer, esriGeometryDimension.esriGeometry2Dimension);
            IArea area = intergeo as IArea;
            return area.Area;
        }

        /// <summary>
        /// 添加几何对象到图层中
        /// </summary>
        /// <param name="ptcols">几何形状的点坐标集合</param>
        /// <param name="lyr">图层</param>
        /// <param name="dics">属性</param>
        public void AddRegToLayerByPoints(IGeometry geo, IFeatureLayer lyr, Dictionary<string, string> sxzs)
        {
            IFeatureClass Featureclass = lyr.FeatureClass;
            IWorkspaceEdit workspace = (IWorkspaceEdit)(Featureclass as IDataset).Workspace;
            workspace.StartEditing(false);
            workspace.StartEditOperation();
            //查询附近的巷道，确定Id的值，确定对应的符号
            IPolyline plincenter = new PolylineClass();
            plincenter.FromPoint = (geo as IPointCollection).get_Point(0);
            plincenter.ToPoint = (geo as IPointCollection).get_Point(1);
            plincenter.SpatialReference = Global.spatialref;
            int Idbs = Global.commonclss.SearchHdByLine(plincenter, sxzs[GIS_Const.FIELD_HDID], Global.centerfdlyr);
            //创建Feature
            IFeature fea = Featureclass.CreateFeature();
            int index = fea.Fields.FindField(GIS_Const.FIELD_SHAPE);
            if (index != -1)
            {
                IGeometryDef geometryDef = fea.Fields.get_Field(index).GeometryDef as IGeometryDef;
                if (geometryDef.HasZ)
                {
                    IZAware pZAware = (IZAware)geo;
                    pZAware.ZAware = true;
                    fea.Shape = geo;
                    foreach (string key in sxzs.Keys)
                    {
                        if (key == GIS_Const.FIELD_XH)
                        {
                            int xh = Convert.ToInt16(sxzs[key]);
                            fea.set_Value(fea.Fields.FindField(key), xh + 1);
                        }
                        else if (key == GIS_Const.FIELD_ID)
                        {
                            fea.set_Value(fea.Fields.FindField(key), Idbs);
                        }
                        else

                            fea.set_Value(fea.Fields.FindField(key), sxzs[key]);
                    }
                    fea.Store();
                }
                else
                {
                    fea.Shape = geo;
                    foreach (string key in sxzs.Keys)
                    {
                        if (key == GIS_Const.FIELD_XH)
                        {
                            int xh = Convert.ToInt16(sxzs[key]);
                            fea.set_Value(fea.Fields.FindField(key), xh + 1);
                        }
                        else if (key == GIS_Const.FIELD_ID)
                        {
                            fea.set_Value(fea.Fields.FindField(key), Idbs);
                        }
                        else

                            fea.set_Value(fea.Fields.FindField(key), sxzs[key]);
                    }
                    fea.Store();
                }
            }
            workspace.StopEditOperation();
            workspace.StopEditing(true);
        }

        /// <summary>
        /// 根据传入的起止点计算延长点的坐标
        /// </summary>
        /// <param name="startP"></param>
        /// <param name="endP"></param>
        /// <param name="jjcd"></param>
        /// <returns></returns>
        public IPoint CalculateExtendPoint(IPoint startP, IPoint endP, double jjcd)
        {
            IPoint pnt = new PointClass();
            double x = 0, y = 0, z = 0;
            if (endP.X == startP.X)
            {
                if (endP.Y > startP.Y)
                {
                    x = endP.X;
                    y = endP.Y + jjcd;
                }
                else
                {
                    x = endP.X;
                    y = endP.Y - jjcd;
                }
            }
            else
            {
                double k = (endP.Y - startP.Y) / (endP.X - startP.X);
                double b = endP.Y - k * endP.X;
                double fz0 = 2 * endP.X - 2 * k * (b - endP.Y);
                double fz1 = Math.Pow(2 * k * (b - endP.Y) - 2 * endP.X, 2);
                double fz2 = 4 * (Math.Pow(k, 2) + 1) * (Math.Pow(endP.X, 2) + Math.Pow(b - endP.Y, 2) - Math.Pow(b, 2));
                double fm = 2 * (Math.Pow(k, 2) + 1);
                double x0 = (fz0 + Math.Sqrt(fz1 - fz2)) / fm;
                double y0 = k * x0 + b;
                double x1 = (fz0 - Math.Sqrt(fz1 - fz2)) / fm;
                double y1 = k * x1 + b;
                if (endP.X > startP.X)
                {
                    if (x0 > endP.X)
                    {
                        x = x1;
                        y = y1;
                    }
                    else
                    {
                        x = x1;
                        y = y1;
                    }
                }
                if (endP.X < startP.X)
                {
                    if (x0 < endP.X)
                    {
                        x = x0;
                        y = y0;
                    }
                    else
                    {
                        x = x1;
                        y = y1;
                    }
                }
            }
            pnt.X = x;
            pnt.Y = y;
            pnt.Z = z;
            return pnt;
        }

        /// <summary>
        /// 计算延长线上的距离
        /// </summary>
        /// <param name="startP"></param>
        /// <param name="endP"></param>
        /// <param name="jjcd"></param>
        /// <returns></returns>
        public IPoint CalculateExtendPointNew(IPolyline plin, IPoint pntTo, double jjcd, out bool bres)
        {
            bres = false;
            IPoint pntOut = new PointClass();
            double distfrom = 0.0, distto = 0.0;
            double outdistfrom = 0.0, outdistto = 0.0;
            bool boolfrom = false;
            plin.QueryPointAndDistance(esriSegmentExtension.esriExtendAtFrom, pntTo, false, pntOut, ref distfrom, ref outdistfrom, ref boolfrom);
            plin.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pntTo, false, pntOut, ref distto, ref outdistto, ref boolfrom);
            if (distfrom < distto)//反向的
            {
                bres = true;
                plin.ReverseOrientation();
                plin.QueryPoint(esriSegmentExtension.esriExtendAtTo, jjcd + plin.Length, false, pntOut);
            }
            else//正向
            {
                bres = false;
                plin.QueryPoint(esriSegmentExtension.esriExtendAtTo, jjcd + plin.Length, false, pntOut);
            }
            return pntOut;
        }
        /// <summary>
        /// 延长面对象
        /// 延长中心线
        /// </summary>
        /// <param name="HdId">鼠标点击获得的点</param>
        /// <param name="jjcd">掘进尺度</param>
        /// <param name="centerlinlyr">掘进绘制图层</param>
        /// <param name="checkval">上下层关系判断阈值</param>
        /// <param name="jjfx">掘进方向（0,1,2,3）</param>
        /// <param name="search">查询地质构造的值</param>
        /// <param name="jjbs">掘进标识 （0标识不精确的， 1代表由导线点精确定位的） </param>
        ///<param name="xzpnts">校正点坐标串</param>
        /// <returns></returns>
        private void CreateFeature(List<IPoint> pntlist, IFeatureLayer layer)
        {
            IFeatureClass Featureclass = layer.FeatureClass;
            IWorkspaceEdit workspace = (IWorkspaceEdit)(Featureclass as IDataset).Workspace;
            workspace.StartEditing(false);
            workspace.StartEditOperation();

            for (int i = 0; i < pntlist.Count; i++)
            {
                IPoint pnt = pntlist[i];
                IFeature feature = Featureclass.CreateFeature();
                IZAware pZAware = (IZAware)pnt;
                pZAware.ZAware = true;
                feature.Shape = pnt as IGeometry;
                feature.Store();
            }
            workspace.StopEditOperation();
            workspace.StopEditing(true);
        }

        private void CreateFeature(IGeometry geo, IFeatureLayer layer)
        {
            IFeatureClass Featureclass = layer.FeatureClass;
            IWorkspaceEdit workspace = (IWorkspaceEdit)(Featureclass as IDataset).Workspace;
            workspace.StartEditing(false);
            workspace.StartEditOperation();
            IFeature feature = Featureclass.CreateFeature();
            feature.Shape = geo;
            feature.Store();
            workspace.StopEditOperation();
            workspace.StopEditing(true);
        }

        /// <summary>
        /// 掘进巷道矫正
        /// </summary>
        /// <param name="HdId"></param>
        /// <param name="xzpnts"></param>
        /// <param name="jjcd"></param>
        /// <param name="jjfx"></param>
        /// <param name="search"></param>
        /// <param name="checkval"></param>
        /// <param name="jjbs">掘进标识</param>
        public void DrawJJJZ(string HdId, List<IPoint> xzpnts, double hdwid, double jjcd = 0, int jjfx = 0, double search = 0, double checkval = 0, int jjbs = 0)
        {
            List<IPoint> rightpts = null;
            List<IPoint> leftpts = null;
            List<IPoint> rightresults = null;
            List<IPoint> leftresults = null;
            List<IPoint> results = null;
            //int xh = 0;
            Dictionary<string, string> fldvals = new Dictionary<string, string>();
            fldvals.Add(GIS_Const.FIELD_HDID, HdId);
            //查询指定id的巷道对应的中心线（用于计算的分段显示的中心线）
            List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selobjs = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerfdlyr, fldvals);
            if (selobjs.Count == 0)
            {
                MessageBox.Show("没有找到相应的巷道信息！", "系统提示");
                return;
            }
            int xh = Convert.ToInt16(selobjs[0].Item3[GIS_Const.FIELD_XH]);
            xh = xh + 1;
            Dictionary<string, string> fdlin_dics = new Dictionary<string, string>();
            fdlin_dics.Add(GIS_Const.FIELD_HDID, HdId);
            fdlin_dics.Add(GIS_Const.FIELD_XH, (xh).ToString());
            fdlin_dics.Add(GIS_Const.FIELD_ID, "0");
            fdlin_dics.Add(GIS_Const.FIELD_BS, "0");
            //计算延长点的坐标
            IGeometry geosel = selobjs[0].Item2;
            IPolyline plin = (IPolyline)geosel;
            if (jjbs == 1)//根据导线点精确定位的
            {
                //清除掘进信息
                string sql = "\"" + GIS_Const.FIELD_HDID + "\"='" + HdId + "' AND \"" + GIS_Const.FIELD_BS + "\"=0";
                Global.commonclss.DelFeatures(Global.pntlyr, sql);
                Global.commonclss.DelFeatures(Global.centerlyr, sql);
                Global.commonclss.DelFeatures(Global.centerfdlyr, sql);
                Global.commonclss.DelFeatures(Global.hdfdfulllyr, sql);
                Global.commonclss.DelFeatures(Global.hdfdlyr, sql);
                Global.pActiveView.Refresh();
                //构造点集合，用于构造线 面等
                selobjs = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerfdlyr, fldvals);
                List<IPoint> dpts = new List<IPoint>();
                plin = (IPolyline)selobjs[0].Item2;
                dpts.Add(plin.ToPoint);
                dpts.AddRange(xzpnts);
                //
                Dictionary<string, string> fdlin_dics1 = new Dictionary<string, string>();
                fdlin_dics1.Add(GIS_Const.FIELD_HDID, HdId);
                fdlin_dics1.Add(GIS_Const.FIELD_XH, xh.ToString());
                fdlin_dics1.Add(GIS_Const.FIELD_ID, "0");
                fdlin_dics1.Add(GIS_Const.FIELD_BS, "1");
                AddHangdaoToLayer(dpts, fdlin_dics1, Global.pntlyr);//将导线点写到导线图层中
                AddDxdLines(xzpnts, fdlin_dics1, Global.pntlinlyr);
                AddFDLineToLayer(dpts, fdlin_dics1, Global.centerfdlyr, 1);//添加分段中心线到中心线图层
                //将线段添加到中心线全图层中
                List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selcenterlin = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerlyr, fldvals);
                if (selcenterlin.Count > 0)
                {
                    IPolyline centerlin = selcenterlin[0].Item2 as IPolyline;
                    IFeature centerlinfea = selcenterlin[0].Item1;
                    IPointCollection centerlinpnts = centerlin as IPointCollection;

                    IPolyline centerlinnew = new PolylineClass();
                    centerlinnew.SpatialReference = Global.spatialref;
                    IPointCollection centerpnts = centerlinnew as IPointCollection;
                    centerpnts.AddPoint(centerlinpnts.get_Point(centerlinpnts.PointCount - 1));
                    for (int i = 0; i < xzpnts.Count; i++)
                    {
                        centerpnts.AddPoint(xzpnts[i]);
                    }
                    ITopologicalOperator4 toplogical = (ITopologicalOperator4)centerlinnew;
                    if (!toplogical.IsSimple)
                        toplogical.Simplify();
                    List<IGeometry> geos = new List<IGeometry>();
                    geos.Add(centerlinnew);
                    Global.commonclss.CreatePolygonFromExistingGeometries(Global.centerlyr, centerlinfea, geos);
                }
                //将校正的巷道断面添加到巷道分段面中
                List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selhdfd = Global.commonclss.SearchFeaturesByGeoAndText(Global.hdfdlyr, fldvals);
                if (selhdfd.Count > 0)
                {
                    IFeature hdfd_fea = selhdfd[0].Item1;
                    IPolygon hdfd_polygon = (IPolygon)selhdfd[0].Item2;
                    IPointCollection hdfd_cols = hdfd_polygon as IPointCollection;
                    //计算中心线的平行线这点坐标
                    List<IPoint> fddpts = new List<IPoint>();
                    fddpts.Add(plin.FromPoint);
                    fddpts.Add(plin.ToPoint);
                    for (int i = 0; i < xzpnts.Count; i++)
                    {
                        fddpts.Add(xzpnts[i]);
                    }
                    rightpts = Global.cons.GetLRParallelPnts(fddpts, hdwid, 1);//右侧平行线上的端点串
                    leftpts = Global.cons.GetLRParallelPnts(fddpts, hdwid, 0); //左侧平行线上的端点串
                    //rightresults = Global.cons.CalculateRegPnts(rightpts);
                    //leftresults = Global.cons.CalculateRegPnts(leftpts);
                    //results = Global.cons.ConstructPnts(rightresults, leftresults);
                    rightpts.Reverse();
                    leftpts.Reverse();
                    results = Global.cons.ConstructPnts(rightpts, leftpts);

                    IFeatureClass Featureclass = Global.hdfdlyr.FeatureClass;
                    IWorkspaceEdit workspace = (IWorkspaceEdit)(Featureclass as IDataset).Workspace;
                    workspace.StartEditing(false);
                    workspace.StartEditOperation();
                    //leftpts.Reverse();

                    IPolygon polygon = new PolygonClass();
                    polygon.SpatialReference = Global.spatialref;
                    IPointCollection regpntcols = (IPointCollection)polygon;
                    regpntcols.AddPoint(rightpts[0]);
                    regpntcols.AddPoint(rightpts[1]);
                    regpntcols.AddPoint(leftpts[0]);
                    regpntcols.AddPoint(leftpts[1]);
                    polygon.Close();

                    //拓扑检查一下
                    ITopologicalOperator4 tops = polygon as ITopologicalOperator4;
                    if (!tops.IsSimple)
                        tops.Simplify();
                    hdfd_fea.Shape = polygon as IGeometry;
                    hdfd_fea.Store();
                    workspace.StopEditOperation();
                    workspace.StopEditing(true);
                    //AddRegToLayer(results, fdlin_dics1, Global.hdfdlyr);
                    //rightpts.RemoveAt(0);
                    //leftpts.RemoveAt(0);
                    //leftpts.Reverse();
                    AddFDRegToLayer(rightpts, leftpts, fddpts, fdlin_dics1, Global.hdfdlyr, hdwid);
                }
                //更新巷道面全图层
                List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selhdfull = Global.commonclss.SearchFeaturesByGeoAndText(Global.hdfdfulllyr, fldvals);
                if (selhdfull.Count > 0)
                {
                    IFeature hdfull_fea = selhdfull[0].Item1;
                    //IPolygon hdfull_polygon = (IPolygon)selhdfull[0].Item2;
                    //IPointCollection hdfull_cols = hdfull_polygon as IPointCollection;
                    //List<IPoint> hdfull_colsnew = new List<IPoint>();
                    //int pos = hdfull_cols.PointCount / 2;
                    //hdfull_cols.UpdatePoint(pos - 1, rightresults[1]);
                    //hdfull_cols.UpdatePoint(pos, leftresults[1]);
                    //Global.commonclss.UpdateFeature(Global.hdfdfulllyr, hdfull_fea, fdlin_dics1, hdfull_polygon);
                    IPolygon polygonsurplus = new PolygonClass();
                    polygonsurplus.SpatialReference = Global.spatialref;
                    polygonsurplus = Global.commonclss.CreatePolygonFromPnts(results, Global.spatialref);
                    //IGeometry polygonres = new PolygonClass();
                    //polygonres.SpatialReference = Global.spatialref;
                    //ITopologicalOperator topsurplus = hdfull_polygon as ITopologicalOperator;
                    //if (!topsurplus.IsSimple)
                    //    topsurplus.Simplify();
                    //ITopologicalOperator4 tops = polygonsurplus as ITopologicalOperator4;
                    //if (!tops.IsSimple)
                    //    tops.Simplify();
                    List<IGeometry> geos1 = new List<IGeometry>();
                    geos1.Add(polygonsurplus);
                    Global.commonclss.CreatePolygonFromExistingGeometries(Global.hdfdfulllyr, hdfull_fea, geos1);

                    //IPolygon polygonsurplus = new PolygonClass();
                    //polygonsurplus.SpatialReference = Global.spatialref;
                    //polygonsurplus = Global.commonclss.CreatePolygonFromPnts(results, Global.spatialref);
                    //geos1.Add(polygonsurplus);

                    //ITopologicalOperator4 tops = polygonsurplus as ITopologicalOperator4;
                    //if (!tops.IsSimple)
                    //    tops.Simplify();
                    //Global.commonclss.UpdateFeature(Global.hdfdfulllyr, hdfull_fea, fdlin_dics1, polygonsurplus);
                }
            }
        }

        /// <summary>
        /// 绘制巷道掘进
        /// </summary>
        /// <param name="HdId">巷道ID</param>
        /// <param name="xzpnts">校正点</param>
        /// <param name="jjcd">掘进长度</param>
        /// <param name="jjfx">掘进方向</param>
        /// <param name="search">查询地质资料的范围</param>
        /// <param name="checkval">上下层关系判断阈值</param>
        /// <param name="jjbs">掘进与校正标识</param>
        public Dictionary<string, List<GeoStruct>> DrawJJCD(string HdId, string bid, double hdwid, List<IPoint> xzpnts, double jjcd = 0, int jjfx = 0, double search = 0, double checkval = 0, int jjbs = 0)
        {
            Dictionary<string, List<GeoStruct>> dzxlist = null;

            List<IPoint> rightpts = null;
            List<IPoint> leftpts = null;
            List<IPoint> rightresults = null;
            List<IPoint> leftresults = null;
            List<IPoint> results = null;
            Dictionary<string, string> fldvals = new Dictionary<string, string>();
            fldvals.Add(GIS_Const.FIELD_HDID, HdId);
            //查询指定id的巷道对应的中心线（用于计算的分段显示的中心线）
            List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selobjs = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerfdlyr, fldvals);
            if (selobjs.Count == 0)
            {
                MessageBox.Show("没有找到相应的巷道信息！", "系统提示");
                return null;
            }
            int xh = Convert.ToInt32(selobjs[0].Item3[GIS_Const.FIELD_XH]) + 1;
            Dictionary<string, string> fdlin_dics = new Dictionary<string, string>();
            fdlin_dics.Add(GIS_Const.FIELD_HDID, HdId);
            fdlin_dics.Add(GIS_Const.FIELD_XH, xh.ToString());
            fdlin_dics.Add(GIS_Const.FIELD_ID, "0");
            fdlin_dics.Add(GIS_Const.FIELD_BS, "0");
            fdlin_dics.Add(GIS_Const.FIELD_BID, bid);
            //计算延长点的坐标
            IGeometry geosel = selobjs[0].Item2;
            IPolyline plin = (IPolyline)geosel;
            if (jjbs == 0)//掘进
            {
                IPoint outP = new PointClass();
                outP.SpatialReference = Global.spatialref;
                plin.QueryPoint(esriSegmentExtension.esriExtendAtTo, jjcd + plin.Length, false, outP);
                //根据点查询60米范围内的地质构造的信息
                List<int> hd_ids = new List<int>();
                hd_ids.Add(Convert.ToInt16(HdId));

                //将延长点添加到导线点图层上
                List<IPoint> dpts = new List<IPoint>();
                dpts.Add(outP);
                Dictionary<string, string> dics = new Dictionary<string, string>();
                dics.Add(GIS_Const.FIELD_HDID, HdId);
                dics.Add(GIS_Const.FIELD_BS, "0");
                dics.Add(GIS_Const.FIELD_BID, bid);
                //AddHangdaoToLayer(dpts, dics, Global.pntlyr);
                //将延长线添加到中线分段图层上
                List<IPoint> fdlin_pts = new List<IPoint>();
                fdlin_pts.Add(plin.ToPoint);
                fdlin_pts.Add(outP);
                AddFDLineToLayer(fdlin_pts, fdlin_dics, Global.centerfdlyr, 0);
                //将延长线添加到中线全图层上
                List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selcenterlin = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerlyr, fldvals);
                IPolyline centerlin = selcenterlin[0].Item2 as IPolyline;
                IPointCollection centerpnts = centerlin as IPointCollection;
                centerpnts.AddPoint(outP);
                IPolyline centerlinnew = centerpnts as IPolyline;
                IFeature centerlinfea = selcenterlin[0].Item1;
                AddHangdaoToLayer(fdlin_pts, fdlin_dics, Global.centerlyr);
                //将延长分段面添加到巷道面分段图层中
                List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selhdfd = Global.commonclss.SearchFeaturesByGeoAndText(Global.hdfdlyr, fldvals);
                IPolygon hdfd_polygon = (IPolygon)selhdfd[0].Item2;
                IPointCollection hdfd_cols = hdfd_polygon as IPointCollection;

                IPolyline center_line = new PolylineClass();
                center_line.FromPoint = plin.ToPoint;
                center_line.ToPoint = outP;
                IConstructCurve constructCurve = new PolylineClass();
                constructCurve.ConstructOffset(center_line, -hdwid / 2);//左侧的线
                IPolyline plyleft = constructCurve as IPolyline;

                IConstructCurve constructCurveright = new PolylineClass();
                constructCurveright.ConstructOffset(center_line, hdwid / 2);//左侧的线
                IPolyline plyright = constructCurveright as IPolyline;

                List<IPoint> hdfdjj_leftcols = new List<IPoint>();
                hdfdjj_leftcols.Add(plyleft.FromPoint);
                hdfdjj_leftcols.Add(plyleft.ToPoint);

                List<IPoint> hdfdjj_rightcols = new List<IPoint>();
                hdfdjj_rightcols.Add(plyright.FromPoint);
                hdfdjj_rightcols.Add(plyright.ToPoint);

                //IPolyline plyleft = new PolylineClass();
                //plyleft.SpatialReference = Global.spatialref;
                //IPointCollection plinleftcols = plyleft as IPointCollection;
                //if (plin.FromPoint.X - plin.ToPoint.X > 0)
                //{
                //    plinleftcols.AddPoint(hdfd_cols.get_Point(0));
                //    plinleftcols.AddPoint(hdfd_cols.get_Point(1));
                //}
                //else
                //{
                //    plinleftcols.AddPoint(hdfd_cols.get_Point(1));
                //    plinleftcols.AddPoint(hdfd_cols.get_Point(0));
                //}
                //IPoint pntleft = new PointClass();
                //plyleft.QueryPoint(esriSegmentExtension.esriExtendAtTo, jjcd + plyleft.Length, false, pntleft);


                //IPolyline plyright = new PolylineClass();
                //plyright.SpatialReference = Global.spatialref;
                //IPointCollection plinrightcols = plyright as IPointCollection;
                //if (plin.FromPoint.X - plin.ToPoint.X > 0)
                //{
                //    plinrightcols.AddPoint(hdfd_cols.get_Point(3));
                //    plinrightcols.AddPoint(hdfd_cols.get_Point(2));
                //}
                //else
                //{
                //    plinrightcols.AddPoint(hdfd_cols.get_Point(2));
                //    plinrightcols.AddPoint(hdfd_cols.get_Point(3));
                //}
                //IPoint pntright = new PointClass();
                //plyright.QueryPoint(esriSegmentExtension.esriExtendAtTo, jjcd + plyright.Length, false, pntright);

                //List<IPoint> hdfdjj_leftcols = new List<IPoint>();
                //if (plin.FromPoint.X - plin.ToPoint.X > 0)
                //{
                //    hdfdjj_leftcols.Add(pntleft);
                //    hdfdjj_leftcols.Add(hdfd_cols.get_Point(1));
                //}
                //else
                //{
                //    hdfdjj_leftcols.Add(pntleft);
                //    hdfdjj_leftcols.Add(hdfd_cols.get_Point(0));
                //}
                //List<IPoint> hdfdjj_rightcols = new List<IPoint>();
                //if (plin.FromPoint.X - plin.ToPoint.X > 0)
                //{
                //    hdfdjj_rightcols.Add(hdfd_cols.get_Point(2));
                //    hdfdjj_rightcols.Add(pntright);
                //}
                //else
                //{
                //    hdfdjj_rightcols.Add(hdfd_cols.get_Point(3));
                //    hdfdjj_rightcols.Add(pntright);
                //}
                AddFDRegToLayer_JC(hdfdjj_leftcols, hdfdjj_rightcols, fdlin_pts, fdlin_dics, Global.hdfdlyr, hdwid);
                //更新巷道全面
                List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selhdfull = Global.commonclss.SearchFeaturesByGeoAndText(Global.hdfdfulllyr, fldvals);
                IPolygon hdfull_polygon = selhdfull[0].Item2 as IPolygon;
                ITopologicalOperator topo = (ITopologicalOperator)hdfull_polygon;
                hdfdjj_rightcols.Reverse();
                List<IPoint> newhdcols = ConstructPnts(hdfdjj_leftcols, hdfdjj_rightcols);
                IPolygon hdadd = AddRegToLayer(newhdcols, fdlin_dics, Global.hdfdfulllyr);
                //查询地质构造
                //dzxlist = Global.commonclss.GetStructsInfos(outP,hd_ids);
                dzxlist = Global.commonclss.GetStructsInfosNew(outP, hd_ids);
                //将掘进点保存
                List<GeoStruct> Last = new List<GeoStruct>();
                GeoStruct LastGeo = new GeoStruct();
                LastGeo.geo = outP;
                Last.Add(LastGeo);
                dzxlist.Add("LAST", Last);
            }
            Global.pActiveView.Refresh();
            return dzxlist;
        }

        /***************************掘进尺度的删除操作***************************/
        /// <summary>
        ///  删除掘进进尺操作
        /// </summary>
        /// <param name="HdId">巷道ID</param>
        /// <param name="Bid">进尺对象</param>
        /// <param name="jjfx">掘进方向</param>
        /// <param name="search">地质结构查询范围</param>
        /// <param name="checkval">上下距离判断阈值</param>
        /// <param name="jjbs">掘进标识 掘进还是校正</param>
        public void DelJJCD(string HdId, string Bid, int jjfx = 0, double search = 0, double checkval = 0, int jjbs = 0)
        {
            IPolyline centerlin = DelCenterlin_fd(HdId, Bid);
            Dictionary<string, string> dxdpnts = DelCenterlin_full(HdId, Bid);
            DelHdFd(HdId, Bid, centerlin);
            DelHdFull(HdId, Bid, centerlin);
            //DelDxdS(dxdpnts);
            //删除峒室层对应当前巷道和BID的峒室要素
            string sql = "\"" + GIS_Const.FIELD_HDID + "\"='" + HdId + "' AND BID='" + Bid + "'";
            Global.commonclss.DelFeatures(Global.dslyr, sql);
            Global.pActiveView.Refresh();
        }

        /// <summary>
        /// 更新导线点图层中的掘进点
        /// </summary>
        /// <param name="dxdpnts"></param>
        private void DelDxdS(Dictionary<string, string> dxdpnts)
        {
            try
            {
                IFeatureClass feaclss = Global.pntlyr.FeatureClass;
                IWorkspaceEdit wks = (feaclss as IDataset).Workspace as IWorkspaceEdit;
                wks.StartEditing(false);
                wks.StartEditOperation();
                int first = 1;
                foreach (string key in dxdpnts.Keys)
                {

                    //查找所有的序号大于当前对象序号的巷道分段图层记录，进行平移
                    string sql = "\"" + GIS_Const.FIELD_BS + "\"=0 AND " + GIS_Const.FIELD_BID + "='" + key + "'";
                    IQueryFilter queryfilter = new QueryFilterClass();
                    queryfilter.WhereClause = sql;
                    IFeatureCursor dxdpnts_cursors = feaclss.Update(queryfilter, true);
                    IFeature currentpnts = dxdpnts_cursors.NextFeature();
                    if (currentpnts == null)
                    {
                        MessageBox.Show("没有找到对应的导线点信息，请检查数据库和地图！", "系统提示");
                        return;
                    }
                    else
                    {
                        if (first == 1)
                        {
                            currentpnts.Delete();
                        }
                        else
                        {
                            IPoint pnt = new PointClass();
                            string deta = dxdpnts[key].ToString();
                            double xdeta = Convert.ToDouble(deta.Split('|')[0]);
                            double ydeta = Convert.ToDouble(deta.Split('|')[1]);
                            pnt.X = (currentpnts.Shape as IPoint).X + xdeta;
                            pnt.Y = (currentpnts.Shape as IPoint).Y + ydeta;
                            pnt.Z = (currentpnts.Shape as IPoint).Z;
                            int index = currentpnts.Fields.FindField(GIS_Const.FIELD_SHAPE);
                            IGeometryDef geometryDef = currentpnts.Fields.get_Field(index).GeometryDef as IGeometryDef;
                            if (geometryDef.HasZ)
                            {
                                IZAware pZAware = (IZAware)pnt;
                                pZAware.ZAware = true;
                                currentpnts.Shape = pnt;
                            }
                            else
                            {
                                currentpnts.Shape = pnt;
                            }
                            dxdpnts_cursors.UpdateFeature(currentpnts);
                        }
                        currentpnts = dxdpnts_cursors.NextFeature();
                        first += 1;
                    }
                    Marshal.ReleaseComObject(dxdpnts_cursors);
                }
                wks.StartEditOperation();
                wks.StopEditing(true);
            }
            catch (Exception ei)
            {

            }
        }

        /// <summary>
        /// 删除巷道分段图层中的掘进记录
        /// </summary>
        /// <param name="HdId">巷道ID</param>
        /// <param name="Bid">掘进记录对象</param>
        /// <param name="centerlin">中心线分段中对应的线对象</param>
        private void DelHdFd(string HdId, string Bid, IPolyline centerlin)
        {
            try
            {
                IFeatureClass feaclss = Global.hdfdlyr.FeatureClass;
                IWorkspaceEdit wks = (feaclss as IDataset).Workspace as IWorkspaceEdit;
                wks.StartEditing(false);
                wks.StartEditOperation();
                //查找所有的序号大于当前对象序号的巷道分段图层记录，进行平移
                string sql = "\"" + GIS_Const.FIELD_HDID + "\"='" + HdId + "' AND \"" + GIS_Const.FIELD_BS + "\"=0 AND " + GIS_Const.FIELD_BID + "='" + Bid + "'";
                IQueryFilter queryfilter = new QueryFilterClass();
                queryfilter.WhereClause = sql;
                //更新中心线分段信息
                IFeatureCursor hdfd_cursors = feaclss.Update(queryfilter, true);
                IFeature currenthdfd = hdfd_cursors.NextFeature();
                if (currenthdfd == null)
                {
                    MessageBox.Show("没有找到对应的巷道分段空间信息，请检查数据库和地图！", "系统提示");
                    return;
                }
                else
                {
                    IPolygon currenthdfdreg = currenthdfd.Shape as IPolygon;
                    IPointCollection pnts = currenthdfdreg as IPointCollection;
                    int xhpos = currenthdfd.Fields.FindField(GIS_Const.FIELD_XH);
                    int xh = Convert.ToInt32(currenthdfd.get_Value(xhpos));

                    currenthdfd.Delete();

                    double xdeta0 = pnts.get_Point(1).X - pnts.get_Point(0).X;
                    double xdeta1 = centerlin.FromPoint.X - centerlin.ToPoint.X;
                    double ydeta0 = pnts.get_Point(1).Y - pnts.get_Point(0).Y;
                    double ydeta1 = centerlin.FromPoint.Y - centerlin.ToPoint.Y;
                    /************************平移后续对象**************************/

                    string sql_centerlin = "\"" + GIS_Const.FIELD_HDID + "\"='" + HdId + "' AND \"" + GIS_Const.FIELD_BS + "\"=0 AND " + GIS_Const.FIELD_XH + ">" + xh.ToString();
                    IQueryFilter queryfilter1 = new QueryFilterClass();
                    queryfilter1.WhereClause = sql_centerlin;
                    //查找所有的序号大于当前对象序号的中心线图层上的记录，进行平移
                    IFeatureCursor hdfdcursors = feaclss.Update(queryfilter1, true);
                    IFeature fea_hdfd = hdfdcursors.NextFeature();
                    while (fea_hdfd != null)
                    {
                        IPolygon reg = fea_hdfd.Shape as IPolygon;
                        ITransform2D trans = reg as ITransform2D;
                        //trans.Move(xdeta0, ydeta0);
                        trans.Move(xdeta1, ydeta1);
                        fea_hdfd.Shape = reg;
                        hdfdcursors.UpdateFeature(fea_hdfd);

                        fea_hdfd = hdfdcursors.NextFeature();
                    }
                    Marshal.ReleaseComObject(hdfdcursors);

                }

                Marshal.ReleaseComObject(hdfd_cursors);
                wks.StopEditOperation();
                wks.StopEditing(true);
            }
            catch (Exception ei)
            {
                Log.Debug("[Del HDFD]" + ei.ToString());
            }
        }

        /// <summary>
        /// 删除巷道全图层中的掘进记录
        /// </summary>
        /// <param name="HdId">巷道ID</param>
        /// <param name="Bid">掘进记录对象</param>
        /// <param name="centerlin">中心线分段中对应的线对象</param>
        private void DelHdFull(string HdId, string Bid, IPolyline centerlin)
        {
            try
            {
                IFeatureClass feaclss = Global.hdfdfulllyr.FeatureClass;
                IWorkspaceEdit wks = (feaclss as IDataset).Workspace as IWorkspaceEdit;
                wks.StartEditing(false);
                wks.StartEditOperation();
                //查找所有的序号大于当前对象序号的巷道分段图层记录，进行平移
                string sql = "\"" + GIS_Const.FIELD_HDID + "\"='" + HdId + "' AND \"" + GIS_Const.FIELD_BS + "\"=0 AND " + GIS_Const.FIELD_BID + "='" + Bid + "'";
                IQueryFilter queryfilter = new QueryFilterClass();
                queryfilter.WhereClause = sql;
                //更新中心线分段信息
                IFeatureCursor hdfull_cursors = feaclss.Update(queryfilter, true);
                IFeature currenthdfull = hdfull_cursors.NextFeature();
                if (currenthdfull == null)
                {
                    MessageBox.Show("没有找到对应的巷道全空间信息，请检查数据库和地图！", "系统提示");
                    return;
                }
                else
                {
                    IPolygon currenthdfdreg = currenthdfull.Shape as IPolygon;
                    IPointCollection pnts = currenthdfdreg as IPointCollection;
                    int xhpos = currenthdfull.Fields.FindField(GIS_Const.FIELD_XH);
                    int xh = Convert.ToInt32(currenthdfull.get_Value(xhpos));
                    currenthdfull.Delete();

                    double xdeta0 = pnts.get_Point(1).X - pnts.get_Point(0).X;
                    double xdeta1 = centerlin.FromPoint.X - centerlin.ToPoint.X;
                    double ydeta0 = pnts.get_Point(1).Y - pnts.get_Point(0).Y;
                    double ydeta1 = centerlin.FromPoint.Y - centerlin.ToPoint.Y;
                    //查找所有的序号大于当前对象序号的中心线图层上的记录，进行平移
                    /************************平移后续对象**************************/

                    string sql_hdfull = "\"" + GIS_Const.FIELD_HDID + "\"='" + HdId + "' AND \"" + GIS_Const.FIELD_BS + "\"=0 AND " + GIS_Const.FIELD_XH + ">" + xh.ToString();
                    IQueryFilter queryfilter1 = new QueryFilterClass();
                    queryfilter1.WhereClause = sql_hdfull;
                    IFeatureCursor hdfulls = feaclss.Update(queryfilter1, true);
                    IFeature fea_hdfull = hdfulls.NextFeature();
                    while (fea_hdfull != null)
                    {
                        IPolygon reg = fea_hdfull.Shape as IPolygon;
                        ITransform2D trans = reg as ITransform2D;
                        //trans.Move(xdeta0, ydeta0);
                        trans.Move(xdeta1, ydeta1);
                        fea_hdfull.Shape = reg;
                        hdfulls.UpdateFeature(fea_hdfull);

                        fea_hdfull = hdfulls.NextFeature();
                    }
                    Marshal.ReleaseComObject(hdfulls);
                }
                Marshal.ReleaseComObject(hdfull_cursors);
                wks.StartEditOperation();
                wks.StopEditing(true);
            }
            catch (Exception ei)
            {

            }
        }

        /// <summary>
        /// 删除中心线全图层中的掘进记录
        /// </summary>
        /// <param name="HdId">巷道ID</param>
        /// <param name="Bid">掘进记录对象</param>
        private Dictionary<string, string> DelCenterlin_full(string HdId, string Bid)
        {
            Dictionary<string, string> results = new Dictionary<string, string>();
            try
            {
                //更新中心线全图层信息
                IFeatureClass feaclss = Global.centerlyr.FeatureClass;
                IWorkspaceEdit wks = (feaclss as IDataset).Workspace as IWorkspaceEdit;
                wks.StartEditing(false);
                wks.StartEditOperation();

                string sql = "\"" + GIS_Const.FIELD_HDID + "\"='" + HdId + "' AND \"" + GIS_Const.FIELD_BS + "\"=0 AND " + GIS_Const.FIELD_BID + "='" + Bid + "'";
                IQueryFilter queryfilter = new QueryFilterClass();
                queryfilter.WhereClause = sql;
                IFeatureCursor centerlin_cursors = feaclss.Update(queryfilter, true);
                IFeature currentlin = centerlin_cursors.NextFeature();
                if (currentlin == null)
                {
                    MessageBox.Show("没有找到对应的中心线全空间信息，请检查数据库和地图！", "系统提示");
                    return null;
                }
                else//找到对应的信息
                {
                    IPolyline lin_full = currentlin.Shape as IPolyline;
                    IPoint start_p = lin_full.FromPoint;
                    IPoint end_p = lin_full.ToPoint;
                    int xhpos = currentlin.Fields.FindField(GIS_Const.FIELD_XH);
                    int xh = Convert.ToInt32(currentlin.get_Value(xhpos));
                    int bidpos = centerlin_cursors.Fields.FindField(GIS_Const.FIELD_BID);

                    double xdeta = start_p.X - end_p.X;
                    double ydeta = start_p.Y - end_p.Y;

                    results.Add(currentlin.get_Value(bidpos).ToString(), xdeta.ToString() + "|" + ydeta.ToString());
                    /************************修改对应当前序号的中心线分段的对象********************/
                    //修改当前的中心线分段对象
                    currentlin.Delete();
                    //centerlin_cursors.UpdateFeature(currentlin);

                    /************************平移后续对象**************************/

                    //查找所有的序号大于当前对象序号的中心线图层上的记录，进行平移
                    string sql_centerlin = "\"" + GIS_Const.FIELD_HDID + "\"='" + HdId + "' AND \"" + GIS_Const.FIELD_BS + "\"=0 AND " + GIS_Const.FIELD_XH + ">" + xh.ToString();
                    IQueryFilter queryfilter1 = new QueryFilterClass();
                    queryfilter1.WhereClause = sql_centerlin;
                    IFeatureCursor centerlin = feaclss.Update(queryfilter1, true);
                    IFeature fea_centerlin = centerlin.NextFeature();
                    while (fea_centerlin != null)
                    {
                        IPolyline plin = fea_centerlin.Shape as IPolyline;
                        ITransform2D trans = plin as ITransform2D;
                        trans.Move(xdeta, ydeta);
                        fea_centerlin.Shape = plin;
                        centerlin.UpdateFeature(fea_centerlin);

                        results.Add(fea_centerlin.get_Value(bidpos).ToString(), xdeta.ToString() + "|" + ydeta.ToString());

                        fea_centerlin = centerlin.NextFeature();
                    }
                    Marshal.ReleaseComObject(centerlin);
                }
                Marshal.ReleaseComObject(centerlin_cursors);
                wks.StopEditOperation();
                wks.StopEditing(true);
            }
            catch (Exception ei)
            {
                Log.Debug("[Construct Parallel]" + ei.ToString());
            }
            return results;
        }

        /// <summary>
        /// 删除中心线分段图层中的掘进记录
        /// </summary>
        /// <param name="HdId">巷道ID</param>
        /// <param name="Bid">掘进记录对象</param>
        private IPolyline DelCenterlin_fd(string HdId, string Bid)
        {
            IPolyline lin_fd = null;
            try
            {
                IWorkspaceEdit workspace = (IWorkspaceEdit)(Global.centerfdlyr.FeatureClass as IDataset).Workspace;
                workspace.StartEditing(false);
                workspace.StartEditOperation();

                IFeatureClass Featureclass = Global.centerfdlyr.FeatureClass;
                string sql = "\"" + GIS_Const.FIELD_HDID + "\"='" + HdId + "' AND \"" + GIS_Const.FIELD_BS + "\"=0 AND " + GIS_Const.FIELD_BID + "='" + Bid + "'";
                IQueryFilter queryfilter = new QueryFilterClass();
                queryfilter.WhereClause = sql;
                //更新中心线分段信息
                IFeatureCursor centerlin_cursors = Featureclass.Update(queryfilter, true);
                IFeature currentlin = centerlin_cursors.NextFeature();
                if (currentlin == null)
                {
                    MessageBox.Show("没有找到对应的中心线分段空间信息，请检查数据库和地图！", "系统提示");
                    return null;
                }
                else//找到对应的信息
                {
                    lin_fd = currentlin.Shape as IPolyline;
                    IPointCollection pts = lin_fd as IPointCollection;
                    IPoint start_p = lin_fd.FromPoint;
                    IPoint end_p = lin_fd.ToPoint;
                    int xhpos = currentlin.Fields.FindField(GIS_Const.FIELD_XH);
                    int xh = Convert.ToInt32(currentlin.get_Value(xhpos));
                    /************************修改对应当前序号的中心线分段的对象********************/
                    //修改当前的中心线分段对象
                    currentlin.Delete();
                    //centerlin_cursors.UpdateFeature(currentlin);

                    double xdeta0 = start_p.X - end_p.X;
                    double ydeta0 = start_p.Y - end_p.Y;
                    /************************平移后续对象**************************/
                    //查找所有的序号大于当前对象序号的中心线分段图层的记录，进行平移

                    string sql_centerlin_fd = "\"" + GIS_Const.FIELD_HDID + "\"='" + HdId + "' AND \"" + GIS_Const.FIELD_BS + "\"=0 AND " + GIS_Const.FIELD_XH + ">" + xh.ToString();
                    IQueryFilter queryfilter1 = new QueryFilterClass();
                    queryfilter1.WhereClause = sql_centerlin_fd;
                    IFeatureCursor centerlin_fds = Featureclass.Update(queryfilter1, true);
                    IFeature fea_centerlin_fd = centerlin_fds.NextFeature();
                    while (fea_centerlin_fd != null)
                    {
                        IPolyline plin = fea_centerlin_fd.Shape as IPolyline;
                        ITransform2D trans = plin as ITransform2D;
                        trans.Move(xdeta0, ydeta0);
                        fea_centerlin_fd.Shape = plin;
                        centerlin_fds.UpdateFeature(fea_centerlin_fd);

                        fea_centerlin_fd = centerlin_fds.NextFeature();
                    }
                    Marshal.ReleaseComObject(centerlin_fds);
                }

                Marshal.ReleaseComObject(centerlin_cursors);
                workspace.StopEditOperation();
                workspace.StopEditing(true);
            }
            catch (Exception ei)
            {
                Log.Debug("[Construct Parallel]" + ei.ToString());
            }

            return lin_fd;
        }

        /// <summary>
        /// 更新掘进尺度
        /// </summary>
        /// <param name="HdId">巷道ID</param>
        /// <param name="Jjxh">要修改的对象</param>
        /// <param name="Bid">当前要修改的对象</param>
        /// <param name="jjcd">掘进尺度修改</param>
        /// <param name="jjfx">方向</param>
        /// <param name="search">查询地质构造距离</param>
        /// <param name="checkval">判断距离</param>
        /// <param name="jjbs">掘进和掘进校正标识</param>
        /// <param name="hdwid">掘进巷道宽度</param>
        public Dictionary<string, string> UpdateJJCD(string HdId, string Bid, double hdwid, double jjcd = 0, int jjfx = 0, double search = 0, double checkval = 0, int jjbs = 0)
        {
            IPolyline centerlin = UpdateCenterlin_fd(HdId, Bid, jjcd);
            Dictionary<string, string> dxdpnts = UpdateCenterlin_full(HdId, Bid, jjcd);
            UpdateHdFd(HdId, Bid, jjcd, centerlin, hdwid);
            UpdateHdFull(HdId, Bid, jjcd, centerlin, hdwid);
            //UpdateDxdS(dxdpnts);
            Global.pActiveView.Refresh();
            return dxdpnts;
        }

        /// <summary>
        /// 更新导线点图层中的掘进点
        /// </summary>
        /// <param name="dxdpnts"></param>
        private void UpdateDxdS(Dictionary<string, string> dxdpnts)
        {
            try
            {
                IFeatureClass feaclss = Global.pntlyr.FeatureClass;
                IWorkspaceEdit wks = (feaclss as IDataset).Workspace as IWorkspaceEdit;
                wks.StartEditing(false);
                wks.StartEditOperation();
                foreach (string key in dxdpnts.Keys)
                {
                    //查找所有的序号大于当前对象序号的巷道分段图层记录，进行平移
                    string sql = "\"" + GIS_Const.FIELD_BS + "\"=0 AND " + GIS_Const.FIELD_BID + "='" + key + "'";
                    IQueryFilter queryfilter = new QueryFilterClass();
                    queryfilter.WhereClause = sql;
                    IFeatureCursor dxdpnts_cursors = feaclss.Update(queryfilter, true);
                    IFeature currentpnts = dxdpnts_cursors.NextFeature();
                    if (currentpnts == null)
                    {
                        MessageBox.Show("没有找到对应的导线点空间信息，请检查数据库和地图！", "系统提示");
                        return;
                    }
                    else
                    {
                        IPoint pnt = new PointClass();
                        string deta = dxdpnts[key].ToString();
                        double xdeta = Convert.ToDouble(deta.Split('|')[0]);
                        double ydeta = Convert.ToDouble(deta.Split('|')[1]);
                        pnt.X = (currentpnts.Shape as IPoint).X + xdeta;
                        pnt.Y = (currentpnts.Shape as IPoint).Y + ydeta;
                        pnt.Z = (currentpnts.Shape as IPoint).Z;
                        int index = currentpnts.Fields.FindField(GIS_Const.FIELD_SHAPE);
                        IGeometryDef geometryDef = currentpnts.Fields.get_Field(index).GeometryDef as IGeometryDef;
                        if (geometryDef.HasZ)
                        {
                            IZAware pZAware = (IZAware)pnt;
                            pZAware.ZAware = true;
                            currentpnts.Shape = pnt;
                        }
                        else
                        {
                            currentpnts.Shape = pnt;
                        }
                        dxdpnts_cursors.UpdateFeature(currentpnts);

                        currentpnts = dxdpnts_cursors.NextFeature();
                    }
                    Marshal.ReleaseComObject(dxdpnts_cursors);
                }
                wks.StartEditOperation();
                wks.StopEditing(true);
            }
            catch (Exception ei)
            {
                Log.Debug("[Construct Parallel]" + ei.ToString());
            }
        }

        /// <summary>
        /// 更新巷道全图层
        /// </summary>
        /// <param name="HdId">巷道ID</param>
        /// <param name="Bid">BID</param>
        /// <param name="jjcd">掘进尺度</param>
        /// <param name="plin">中心线</param>
        /// <param name="hdwid">巷道宽度</param>
        private void UpdateHdFull(string HdId, string Bid, double jjcd, IPolyline plin, double hdwid)
        {
            try
            {
                IFeatureClass feaclss = Global.hdfdfulllyr.FeatureClass;
                IWorkspaceEdit wks = (feaclss as IDataset).Workspace as IWorkspaceEdit;
                wks.StartEditing(false);
                wks.StartEditOperation();
                //查找所有的序号大于当前对象序号的巷道分段图层记录，进行平移
                string sql = "\"" + GIS_Const.FIELD_HDID + "\"='" + HdId + "' AND \"" + GIS_Const.FIELD_BS + "\"=0 AND " + GIS_Const.FIELD_BID + "='" + Bid + "'";
                IQueryFilter queryfilter = new QueryFilterClass();
                queryfilter.WhereClause = sql;
                //更新中心线分段信息
                IFeatureCursor hdfull_cursors = feaclss.Update(queryfilter, true);
                IFeature currenthdfull = hdfull_cursors.NextFeature();
                if (currenthdfull == null)
                {
                    MessageBox.Show("没有找到对应的巷道全空间信息，请检查数据库和地图！", "系统提示");
                    return;
                }
                else
                {
                    IPointCollection pnts = currenthdfull.Shape as IPointCollection;
                    IPolyline lin = plin;
                    IPoint start_p = lin.FromPoint;
                    IPoint end_p = lin.ToPoint;
                    IPoint ptstart = pnts.get_Point(0);
                    IPoint ptend = pnts.get_Point(1);
                    IPoint ptstart1 = pnts.get_Point(3);
                    IPoint ptend1 = pnts.get_Point(2);
                    double len_delta = jjcd - lin.Length;
                    double angle_lean = Math.Atan((end_p.Y - start_p.Y) / (end_p.X - start_p.X));
                    double offset_x = len_delta * Math.Cos(angle_lean);//X增量
                    double offset_y = len_delta * Math.Sin(angle_lean);//Y增量
                    //修改当前的巷道分段对象
                    IConstructCurve constructCurve = new PolylineClass();
                    constructCurve.ConstructOffset(plin, -hdwid / 2);//左侧的线
                    IPolyline plyleft = constructCurve as IPolyline;

                    IConstructCurve constructCurveright = new PolylineClass();
                    constructCurveright.ConstructOffset(plin, hdwid / 2);//左侧的线
                    IPolyline plyright = constructCurveright as IPolyline;

                    List<IPoint> hdfdjj_leftcols = new List<IPoint>();
                    hdfdjj_leftcols.Add(plyleft.FromPoint);
                    hdfdjj_leftcols.Add(plyleft.ToPoint);

                    List<IPoint> hdfdjj_rightcols = new List<IPoint>();
                    hdfdjj_rightcols.Add(plyright.FromPoint);
                    hdfdjj_rightcols.Add(plyright.ToPoint);
                    ////修改当前的巷道分段对象
                    //IPoint pnt_left = new PointClass();
                    //IPolyline plinleft = new PolylineClass();//左侧线求点
                    //plinleft.SpatialReference = Global.spatialref;
                    //if (plin.FromPoint.X - plin.ToPoint.X > 0)
                    //{
                    //    plinleft.FromPoint = pnts.get_Point(0);
                    //    plinleft.ToPoint = pnts.get_Point(1);
                    //    plinleft.QueryPoint(esriSegmentExtension.esriExtendAtTo, jjcd, false, pnt_left);
                    //}
                    //else
                    //{
                    //    plinleft.FromPoint = pnts.get_Point(1);
                    //    plinleft.ToPoint = pnts.get_Point(0);
                    //    plinleft.QueryPoint(esriSegmentExtension.esriExtendAtTo, jjcd, false, pnt_left);
                    //}
                    //IPoint pnt_right = new PointClass();
                    //IPolyline plinright = new PolylineClass();//右侧线求点
                    //plinright.SpatialReference = Global.spatialref;
                    //if (plin.FromPoint.X - plin.ToPoint.X > 0)
                    //{
                    //    plinright.FromPoint = pnts.get_Point(3);
                    //    plinright.ToPoint = pnts.get_Point(2);
                    //    plinright.QueryPoint(esriSegmentExtension.esriExtendAtTo, jjcd, false, pnt_right);
                    //}
                    //else
                    //{
                    //    plinright.FromPoint = pnts.get_Point(2);
                    //    plinright.ToPoint = pnts.get_Point(3);
                    //    plinright.QueryPoint(esriSegmentExtension.esriExtendAtTo, jjcd, false, pnt_right);
                    //}
                    List<IPoint> respnts = new List<IPoint>();
                    respnts.Add(hdfdjj_leftcols[0]);
                    respnts.Add(hdfdjj_leftcols[1]);
                    respnts.Add(hdfdjj_rightcols[1]);
                    respnts.Add(hdfdjj_rightcols[0]);
                    //List<IPoint> respnts = new List<IPoint>();
                    //if (plin.FromPoint.X - plin.ToPoint.X > 0)
                    //{
                    //    respnts.Add(pnts.get_Point(0));
                    //    respnts.Add(pnt_left);
                    //    respnts.Add(pnt_right);
                    //    respnts.Add(pnts.get_Point(3));
                    //}
                    //else
                    //{
                    //    respnts.Add(pnt_left);
                    //    respnts.Add(pnts.get_Point(1));
                    //    respnts.Add(pnts.get_Point(2));
                    //    respnts.Add(pnt_right);
                    //}
                    IPolygon polygon = Global.commonclss.CreatePolygonFromPnts(respnts, Global.spatialref);
                    currenthdfull.Shape = polygon;
                    hdfull_cursors.UpdateFeature(currenthdfull);
                    double xdeta0 = 0.0, ydeta0 = 0.0;
                    xdeta0 = hdfdjj_leftcols[1].X - pnts.get_Point(1).X;
                    ydeta0 = hdfdjj_leftcols[1].Y - pnts.get_Point(1).Y;
                    //if (plin.FromPoint.X - plin.ToPoint.X > 0)
                    //{
                    //    xdeta0 = pnt_left.X - pnts.get_Point(1).X;
                    //    ydeta0 = pnt_left.Y - pnts.get_Point(1).Y;
                    //}
                    //else
                    //{
                    //    xdeta0 = pnt_left.X - pnts.get_Point(0).X;
                    //    ydeta0 = pnt_left.Y - pnts.get_Point(0).Y;
                    //}
                    //double xdeta0 = pnt_left.X - ptend.X;
                    //double xdeta1 = pnt_right.X - ptend1.X;
                    //double ydeta0 = pnt_left.Y - ptend.Y;
                    //double ydeta1 = pnt_right.Y - ptend1.Y;
                    //查找所有的序号大于当前对象序号的中心线图层上的记录，进行平移
                    /************************平移后续对象**************************/
                    int xhpos = currenthdfull.Fields.FindField(GIS_Const.FIELD_XH);
                    int xh = Convert.ToInt32(currenthdfull.get_Value(xhpos));
                    string sql_hdfull = "\"" + GIS_Const.FIELD_HDID + "\"='" + HdId + "' AND \"" + GIS_Const.FIELD_BS + "\"=0 AND " + GIS_Const.FIELD_XH + ">" + xh.ToString();
                    IQueryFilter queryfilter1 = new QueryFilterClass();
                    queryfilter1.WhereClause = sql_hdfull;
                    IFeatureCursor hdfulls = feaclss.Update(queryfilter1, true);
                    IFeature fea_hdfull = hdfulls.NextFeature();
                    while (fea_hdfull != null)
                    {
                        IPolygon reg = fea_hdfull.Shape as IPolygon;
                        ITransform2D trans = reg as ITransform2D;
                        trans.Move(xdeta0, ydeta0);
                        fea_hdfull.Shape = reg;
                        hdfulls.UpdateFeature(fea_hdfull);

                        fea_hdfull = hdfulls.NextFeature();
                    }
                    Marshal.ReleaseComObject(hdfulls);
                }
                Marshal.ReleaseComObject(hdfull_cursors);
                wks.StartEditOperation();
                wks.StopEditing(true);
            }
            catch (Exception ei)
            {
                Log.Debug("[Construct Parallel]" + ei.ToString());
            }
        }

        /// <summary>
        /// 更新巷道分段图层
        /// </summary>
        /// <param name="HdId">巷道ID</param>
        /// <param name="Bid">bid</param>
        /// <param name="jjcd">掘进尺度</param>
        /// <param name="plin">中心线</param>
        /// <param name="hdwid">巷道宽度</param>
        private void UpdateHdFd(string HdId, string Bid, double jjcd, IPolyline plin, double hdwid)
        {
            try
            {
                IFeatureClass feaclss = Global.hdfdlyr.FeatureClass;
                IWorkspaceEdit wks = (feaclss as IDataset).Workspace as IWorkspaceEdit;
                wks.StartEditing(false);
                wks.StartEditOperation();
                //查找所有的序号大于当前对象序号的巷道分段图层记录，进行平移
                string sql = "\"" + GIS_Const.FIELD_HDID + "\"='" + HdId + "' AND \"" + GIS_Const.FIELD_BS + "\"=0 AND " + GIS_Const.FIELD_BID + "='" + Bid + "'";
                IQueryFilter queryfilter = new QueryFilterClass();
                queryfilter.WhereClause = sql;
                //更新中心线分段信息
                IFeatureCursor hdfd_cursors = feaclss.Update(queryfilter, true);
                IFeature currenthdfd = hdfd_cursors.NextFeature();
                if (currenthdfd == null)
                {
                    MessageBox.Show("没有找到对应的巷道分段空间信息，请检查数据库和地图！", "系统提示");
                    return;
                }
                else
                {
                    IPolygon currenthdfdreg = currenthdfd.Shape as IPolygon;
                    IPointCollection pnts = currenthdfdreg as IPointCollection;
                    //修改当前的巷道分段对象
                    //修改当前的巷道分段对象
                    IConstructCurve constructCurve = new PolylineClass();
                    constructCurve.ConstructOffset(plin, -hdwid / 2);//左侧的线
                    IPolyline plyleft = constructCurve as IPolyline;

                    IConstructCurve constructCurveright = new PolylineClass();
                    constructCurveright.ConstructOffset(plin, hdwid / 2);//左侧的线
                    IPolyline plyright = constructCurveright as IPolyline;

                    List<IPoint> hdfdjj_leftcols = new List<IPoint>();
                    hdfdjj_leftcols.Add(plyleft.FromPoint);
                    hdfdjj_leftcols.Add(plyleft.ToPoint);

                    List<IPoint> hdfdjj_rightcols = new List<IPoint>();
                    hdfdjj_rightcols.Add(plyright.FromPoint);
                    hdfdjj_rightcols.Add(plyright.ToPoint);
                    //IPoint pnt_left = new PointClass();
                    //IPolyline plinleft = new PolylineClass();//左侧线求点
                    //plinleft.SpatialReference = Global.spatialref;
                    //if (plin.FromPoint.X - plin.ToPoint.X > 0)
                    //{
                    //    plinleft.FromPoint = pnts.get_Point(0);
                    //    plinleft.ToPoint = pnts.get_Point(1);
                    //}
                    //else
                    //{
                    //    plinleft.FromPoint = pnts.get_Point(1);
                    //    plinleft.ToPoint = pnts.get_Point(0);
                    //}
                    //plinleft.QueryPoint(esriSegmentExtension.esriExtendAtTo, jjcd, false, pnt_left);

                    //IPoint pnt_right = new PointClass();
                    //IPolyline plinright = new PolylineClass();//左侧线求点
                    //plinright.SpatialReference = Global.spatialref;
                    //if (plin.FromPoint.X - plin.ToPoint.X > 0)
                    //{
                    //    plinright.FromPoint = pnts.get_Point(3);
                    //    plinright.ToPoint = pnts.get_Point(2);
                    //}
                    //else
                    //{
                    //    plinright.FromPoint = pnts.get_Point(2);
                    //    plinright.ToPoint = pnts.get_Point(3);
                    //}
                    //plinright.QueryPoint(esriSegmentExtension.esriExtendAtTo, jjcd, false, pnt_right);
                    List<IPoint> respnts = new List<IPoint>();
                    respnts.Add(hdfdjj_leftcols[0]);
                    respnts.Add(hdfdjj_leftcols[1]);
                    respnts.Add(hdfdjj_rightcols[1]);
                    respnts.Add(hdfdjj_rightcols[0]);
                    //List<IPoint> respnts = new List<IPoint>();
                    //if (plin.FromPoint.X - plin.ToPoint.X > 0)
                    //{
                    //    respnts.Add(pnts.get_Point(0));
                    //    respnts.Add(pnt_left);
                    //    respnts.Add(pnt_right);
                    //    respnts.Add(pnts.get_Point(3));
                    //}
                    //else
                    //{
                    //    respnts.Add(pnt_left);
                    //    respnts.Add(pnts.get_Point(1));
                    //    respnts.Add(pnts.get_Point(2));
                    //    respnts.Add(pnt_right);
                    //}
                    //double xdeta0 = 0.0, ydeta0 = 0.0;
                    double xdeta0 = 0.0, ydeta0 = 0.0;
                    xdeta0 = hdfdjj_leftcols[1].X - pnts.get_Point(1).X;
                    ydeta0 = hdfdjj_leftcols[1].Y - pnts.get_Point(1).Y;
                    //if (plin.FromPoint.X - plin.ToPoint.X > 0)
                    //{
                    //    xdeta0 = pnt_left.X - pnts.get_Point(1).X;
                    //    ydeta0 = pnt_left.Y - pnts.get_Point(1).Y;
                    //}
                    //else
                    //{
                    //    xdeta0 = pnt_left.X - pnts.get_Point(0).X;
                    //    ydeta0 = pnt_left.Y - pnts.get_Point(0).Y;
                    //}
                    IPolygon polygon = Global.commonclss.CreatePolygonFromPnts(respnts, Global.spatialref);
                    currenthdfd.Shape = polygon;
                    hdfd_cursors.UpdateFeature(currenthdfd);
                    /************************平移后续对象**************************/
                    IPolyline lin = plin;
                    IPoint start_p = lin.FromPoint;
                    IPoint end_p = lin.ToPoint;
                    double len_delta = jjcd - lin.Length;
                    double angle_lean = Math.Atan((end_p.Y - start_p.Y) / (end_p.X - start_p.X));
                    double offset_x = len_delta * Math.Cos(angle_lean);//X增量
                    double offset_y = len_delta * Math.Sin(angle_lean);//Y增量

                    int xhpos = currenthdfd.Fields.FindField(GIS_Const.FIELD_XH);
                    int xh = Convert.ToInt32(currenthdfd.get_Value(xhpos));
                    string sql_centerlin = "\"" + GIS_Const.FIELD_HDID + "\"='" + HdId + "' AND \"" + GIS_Const.FIELD_BS + "\"=0 AND " + GIS_Const.FIELD_XH + ">" + xh.ToString();
                    IQueryFilter queryfilter1 = new QueryFilterClass();
                    queryfilter1.WhereClause = sql_centerlin;
                    //查找所有的序号大于当前对象序号的中心线图层上的记录，进行平移
                    IFeatureCursor hdfdcursors = feaclss.Update(queryfilter1, true);
                    IFeature fea_hdfd = hdfdcursors.NextFeature();
                    while (fea_hdfd != null)
                    {
                        IPolygon reg = fea_hdfd.Shape as IPolygon;
                        ITransform2D trans = reg as ITransform2D;
                        trans.Move(xdeta0, ydeta0);
                        fea_hdfd.Shape = reg;
                        hdfdcursors.UpdateFeature(fea_hdfd);

                        fea_hdfd = hdfdcursors.NextFeature();
                    }
                    Marshal.ReleaseComObject(hdfdcursors);

                }

                Marshal.ReleaseComObject(hdfd_cursors);
                wks.StopEditOperation();
                wks.StopEditing(true);
            }
            catch (Exception ei)
            {
                Log.Debug("[Construct Parallel]" + ei.ToString());
            }
        }

        /// <summary>
        /// 更新中心线全图层
        /// </summary>
        /// <param name="HdId"></param>
        /// <param name="Bid"></param>
        /// <param name="jjcd"></param>
        private Dictionary<string, string> UpdateCenterlin_full(string HdId, string Bid, double jjcd)
        {
            Dictionary<string, string> results = new Dictionary<string, string>();
            try
            {
                //更新中心线全图层信息
                IFeatureClass feaclss = Global.centerlyr.FeatureClass;
                IWorkspaceEdit wks = (feaclss as IDataset).Workspace as IWorkspaceEdit;
                wks.StartEditing(false);
                wks.StartEditOperation();

                string sql = "\"" + GIS_Const.FIELD_HDID + "\"='" + HdId + "' AND \"" + GIS_Const.FIELD_BS + "\"=0 AND " + GIS_Const.FIELD_BID + "='" + Bid + "'";
                IQueryFilter queryfilter = new QueryFilterClass();
                queryfilter.WhereClause = sql;
                IFeatureCursor centerlin_cursors = feaclss.Update(queryfilter, true);
                IFeature currentlin = centerlin_cursors.NextFeature();
                if (currentlin == null)
                {
                    MessageBox.Show("没有找到对应的中心线全空间信息，请检查数据库和地图！", "系统提示");
                    return null;
                }
                else//找到对应的信息
                {
                    IPolyline lin_full = currentlin.Shape as IPolyline;
                    IPoint start_p = lin_full.FromPoint;
                    IPoint end_p = lin_full.ToPoint;
                    double len_delta = jjcd - lin_full.Length;
                    double angle_lean = Math.Atan((end_p.Y - start_p.Y) / (end_p.X - start_p.X));
                    double offset_x = len_delta * Math.Cos(angle_lean);//X增量
                    double offset_y = len_delta * Math.Sin(angle_lean);//Y增量
                    /************************修改对应当前序号的中心线分段的对象********************/
                    //修改当前的中心线分段对象
                    ITransform3D trans_linfd = lin_full as ITransform3D;
                    trans_linfd.Scale3D(start_p, jjcd / lin_full.Length, jjcd / lin_full.Length, 1);
                    currentlin.Shape = lin_full;
                    centerlin_cursors.UpdateFeature(currentlin);
                    double xdeta0 = lin_full.ToPoint.X - end_p.X;
                    double ydeta0 = lin_full.ToPoint.Y - end_p.Y;
                    int bidpos = centerlin_cursors.Fields.FindField(GIS_Const.FIELD_BID);

                    results.Add(currentlin.get_Value(bidpos).ToString(), xdeta0.ToString() + "|" + ydeta0.ToString());
                    /************************平移后续对象**************************/
                    int xhpos = currentlin.Fields.FindField(GIS_Const.FIELD_XH);
                    int xh = Convert.ToInt32(currentlin.get_Value(xhpos));
                    //查找所有的序号大于当前对象序号的中心线图层上的记录，进行平移
                    string sql_centerlin = "\"" + GIS_Const.FIELD_HDID + "\"='" + HdId + "' AND \"" + GIS_Const.FIELD_BS + "\"=0 AND " + GIS_Const.FIELD_XH + ">" + xh.ToString();
                    IQueryFilter queryfilter1 = new QueryFilterClass();
                    queryfilter1.WhereClause = sql_centerlin;
                    IFeatureCursor centerlin = feaclss.Update(queryfilter1, true);
                    IFeature fea_centerlin = centerlin.NextFeature();
                    while (fea_centerlin != null)
                    {
                        IPolyline plin = fea_centerlin.Shape as IPolyline;
                        ITransform2D trans = plin as ITransform2D;
                        trans.Move(xdeta0, ydeta0);
                        fea_centerlin.Shape = plin;
                        results.Add(fea_centerlin.get_Value(bidpos).ToString(), xdeta0.ToString() + "|" + ydeta0.ToString());

                        centerlin.UpdateFeature(fea_centerlin);
                        fea_centerlin = centerlin.NextFeature();
                    }
                    Marshal.ReleaseComObject(centerlin);
                }
                Marshal.ReleaseComObject(centerlin_cursors);
                wks.StopEditOperation();
                wks.StopEditing(true);
            }
            catch (Exception ei)
            {
                Log.Debug("[Construct Parallel]" + ei.ToString());
            }
            return results;
        }

        /// <summary>
        /// 中心线分段图层更新
        /// </summary>
        /// <param name="HdId"></param>
        /// <param name="Bid"></param>
        /// <param name="jjcd"></param>
        private IPolyline UpdateCenterlin_fd(string HdId, string Bid, double jjcd)
        {
            IPolyline lin_fd = null;
            try
            {
                IWorkspaceEdit workspace = (IWorkspaceEdit)(Global.centerfdlyr.FeatureClass as IDataset).Workspace;
                workspace.StartEditing(false);
                workspace.StartEditOperation();

                IFeatureClass Featureclass = Global.centerfdlyr.FeatureClass;
                string sql = "\"" + GIS_Const.FIELD_HDID + "\"='" + HdId + "' AND \"" + GIS_Const.FIELD_BS + "\"=0 AND " + GIS_Const.FIELD_BID + "='" + Bid + "'";
                IQueryFilter queryfilter = new QueryFilterClass();
                queryfilter.WhereClause = sql;
                //更新中心线分段信息
                IFeatureCursor centerlin_cursors = Featureclass.Update(queryfilter, true);
                IFeature currentlin = centerlin_cursors.NextFeature();
                if (currentlin == null)
                {
                    MessageBox.Show("没有找到对应的中心线分段空间信息，请检查数据库和地图！", "系统提示");
                    return null;
                }
                else//找到对应的信息
                {
                    lin_fd = currentlin.Shape as IPolyline;
                    IPointCollection pts = lin_fd as IPointCollection;
                    IPoint start_p = lin_fd.FromPoint;
                    IPoint end_p = lin_fd.ToPoint;
                    double len_delta = jjcd - lin_fd.Length;
                    double angle_lean = Math.Atan((end_p.Y - start_p.Y) / (end_p.X - start_p.X));
                    double offset_x = len_delta * Math.Cos(angle_lean);//X增量
                    double offset_y = len_delta * Math.Sin(angle_lean);//Y增量
                    /************************修改对应当前序号的中心线分段的对象********************/
                    //修改当前的中心线分段对象
                    ITransform3D trans_linfd = lin_fd as ITransform3D;
                    trans_linfd.Scale3D(start_p, jjcd / lin_fd.Length, jjcd / lin_fd.Length, 1);
                    currentlin.Shape = lin_fd;
                    centerlin_cursors.UpdateFeature(currentlin);
                    double xdeta0 = lin_fd.ToPoint.X - end_p.X;
                    double ydeta0 = lin_fd.ToPoint.Y - end_p.Y;
                    /************************平移后续对象**************************/
                    //查找所有的序号大于当前对象序号的中心线分段图层的记录，进行平移
                    int xhpos = currentlin.Fields.FindField(GIS_Const.FIELD_XH);
                    int xh = Convert.ToInt32(currentlin.get_Value(xhpos));
                    string sql_centerlin_fd = "\"" + GIS_Const.FIELD_HDID + "\"='" + HdId + "' AND \"" + GIS_Const.FIELD_BS + "\"=0 AND " + GIS_Const.FIELD_XH + ">" + xh.ToString();
                    IQueryFilter queryfilter1 = new QueryFilterClass();
                    queryfilter1.WhereClause = sql_centerlin_fd;
                    IFeatureCursor centerlin_fds = Featureclass.Update(queryfilter1, true);
                    IFeature fea_centerlin_fd = centerlin_fds.NextFeature();
                    while (fea_centerlin_fd != null)
                    {
                        IPolyline plin = fea_centerlin_fd.Shape as IPolyline;
                        ITransform2D trans = plin as ITransform2D;
                        trans.Move(xdeta0, ydeta0);
                        fea_centerlin_fd.Shape = plin;
                        centerlin_fds.UpdateFeature(fea_centerlin_fd);

                        fea_centerlin_fd = centerlin_fds.NextFeature();
                    }
                    Marshal.ReleaseComObject(centerlin_fds);
                }

                Marshal.ReleaseComObject(centerlin_cursors);
                workspace.StopEditOperation();
                workspace.StopEditing(true);
            }
            catch (Exception ei)
            {
                Log.Debug("[Construct Parallel]" + ei.ToString());
            }

            return lin_fd;
        }

        /// <summary>
        /// 绘制工作面回采
        /// </summary>
        /// <param name="hd1">巷道1 ID</param>
        /// <param name="hd2">巷道2 ID</param>
        /// <param name="hd3">切眼  ID</param>
        /// <param name="hccd">回采长度</param>
        /// <param name="hd1wid1">巷道1宽度</param>
        /// <param name="hd1wid2">巷道2宽度</param>
        /// <param name="hcbz">回采与回采校正标识</param>
        /// <returns></returns>
        public Dictionary<string, List<GeoStruct>> DrawHDHC(string hd1, string hd2, string hd3, double hccd, double hd1wid1, double hd1wid2, double qywid, int hcbz, double searchlen,
            Dictionary<string, string> dics, bool isAdd, IPoint prevPoint, out IPoint pos)
        {
            List<int> hd_ids = new List<int>();
            hd_ids.Add(Convert.ToInt16(hd1));
            hd_ids.Add(Convert.ToInt16(hd2));
            hd_ids.Add(Convert.ToInt16(hd3));
            //返回工作面坐标点
            IPoint prevHcPoint = new PointClass();
            pos = prevHcPoint;
            Dictionary<string, List<GeoStruct>> dzxlist = new Dictionary<string, List<GeoStruct>>();

            Dictionary<string, string> hdids = new Dictionary<string, string>();
            hdids.Add(GIS_Const.FIELD_HDID, hd1);
            List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selobjs1 = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerlyr, hdids);

            if (null == selobjs1 || selobjs1.Count < 0)
            {
                Log.Error("[GIS]....绘制回采巷道，“中心线图层”可能不存在，请检查GIS数据库。");
                return null;
            }

            IPolyline pline1 = selobjs1[0].Item2 as IPolyline;

            hdids[GIS_Const.FIELD_HDID] = hd2;
            List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selobjs2 = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerlyr, hdids);
            IPolyline pline2 = selobjs2[0].Item2 as IPolyline;

            hdids[GIS_Const.FIELD_HDID] = hd3;
            List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selobjs3 = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerlyr, hdids);
            IPolyline pline3 = selobjs3[0].Item2 as IPolyline;
            hdids[GIS_Const.FIELD_HDID] = hd1 + "_" + hd2;

            //设置回采移动的切眼
            int dirflag = 0;
            List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selhcregs = Global.commonclss.SearchFeaturesByGeoAndText(Global.hcqlyr, hdids);
            if (prevPoint != null && selhcregs != null && selhcregs.Count > 0)
            {
                //查询回采面中心点在切眼中的方向
                dirflag = Global.commonclss.GetDirectionByPnt(pline3, prevPoint);
                //构造新的切眼巷道
                Dictionary<string, List<IPoint>> regcoordinates = Global.commonclss.getCoordinates(selhcregs[0].Item2 as IPolygon, pline1, pline2, pline3, hd1wid1, hd1wid2);
                pline3 = new PolylineClass();
                pline3.FromPoint = regcoordinates["1"][1];
                pline3.ToPoint = regcoordinates["1"][0];
            }
            //查询采掘图层上是否包含了相应的回采区
            List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selcjqs = Global.commonclss.SearchFeaturesByGeoAndText(Global.hcqlyr, hdids);
            IPointCollection pntcol = new PolygonClass();
            if (hcbz == 1)//粗糙回采
            {
                pntcol = Global.hcjsclass.GetBackPolygonArea(pline1, pline2, pline3, hd1wid1, hd1wid1, qywid, hccd, dirflag);
                if (pntcol == null)
                {
                    pos = null;
                    return null;
                }
                List<IPoint> pnthccols = new List<IPoint>();
                for (int i = 0; i < pntcol.PointCount - 1; i++)
                {
                    pnthccols.Add(pntcol.get_Point(i));
                }
                if (isAdd)
                    Global.cons.AddHangdaoToLayer(pnthccols, dics, Global.hcqlyr);
                else
                {
                    IPolygon poly = Global.commonclss.CreatePolygonFromPnts(pnthccols, Global.spatialref);
                    if (selcjqs.Count > 0)
                        Global.commonclss.UpdateFeature(Global.hcqlyr, selcjqs[0].Item1, dics, poly);
                }
                prevHcPoint = pntcol.get_Point(pntcol.PointCount - 1);
                pos = pntcol.get_Point(pntcol.PointCount - 1);

                //根据点查询60米范围内的地质构造的信息

                //dzxlist = Global.commonclss.GetStructsInfos(prevHcPoint, hd_ids);
                dzxlist = Global.commonclss.GetStructsInfosNew(prevHcPoint, hd_ids, 2);
            }
            if (hcbz == 2)//回采校正
            {
                double x = 0.0;//Convert.ToDouble(this.txtX.Text);
                double y = 0.0;// Convert.ToDouble(this.txtY.Text);
                double hccd1 = Math.Sqrt(Math.Pow((x - prevHcPoint.X), 2) + Math.Pow((y - prevHcPoint.Y), 2));
                //将BS为0的回采去删除，创建新的回采区
                string sql = "\"" + GIS_Const.FIELD_HDID + "\"='" + hd1 + "_" + hd2 + "' AND \"" + GIS_Const.FIELD_BS + "\"=0";
                Global.commonclss.DelFeatures(Global.hcqlyr, sql);
                //查询对应的采掘
                pntcol = Global.hcjsclass.GetBackPolygonArea(pline1, pline2, pline3, hd1wid1, hd1wid1, hd1wid2, hccd1, dirflag);
                if (pntcol == null)
                    return null;
                //构造采掘区对象
                List<IPoint> pnthccols = new List<IPoint>();
                for (int i = 0; i < pntcol.PointCount - 1; i++)
                {
                    pnthccols.Add(pntcol.get_Point(i));
                }
                if (isAdd)
                    Global.cons.AddHangdaoToLayer(pnthccols, dics, Global.hcqlyr);
                else
                {
                    IPolygon poly = Global.commonclss.CreatePolygonFromPnts(pnthccols, Global.spatialref);
                    Global.commonclss.UpdateFeature(Global.hcqlyr, selcjqs[0].Item1, dics, poly);
                }
                //将当前点写入到对应的工作面表中
                prevHcPoint = pntcol.get_Point(pntcol.PointCount - 1);
                pos = prevHcPoint;
                //根据点查询60米范围内的地质构造的信息
                //dzxlist = Global.commonclss.GetStructsInfos(prevHcPoint, hd_ids);
                dzxlist = Global.commonclss.GetStructsInfosNew(prevHcPoint, hd_ids, 2);

            }
            Global.pActiveView.Refresh();
            return dzxlist;
        }

        /// <summary>
        /// 回采进尺更新
        /// </summary>
        /// <param name="hd1">主巷道ID</param>
        /// <param name="hd2">辅助巷道ID</param>
        /// <param name="hd3">切眼巷道ID</param>
        /// <param name="Bid">要修改的回采对象的BID</param>
        /// <param name="hccd">回采的尺度</param>
        /// <param name="search">查询附近地质构造的距离</param>
        /// <returns></returns>
        public Dictionary<string, IPoint> UpdateHCCD(string hd1, string hd2, string hd3, string Bid, double hccd, double zywid, double fywid, double qywid, double search = 0)
        {
            Dictionary<string, IPoint> respnts = new Dictionary<string, IPoint>();

            Dictionary<string, string> hdids = new Dictionary<string, string>();
            hdids.Add(GIS_Const.FIELD_HDID, hd1);
            List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selobjs1 = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerlyr, hdids);
            IPolyline pline1 = selobjs1[0].Item2 as IPolyline;

            hdids[GIS_Const.FIELD_HDID] = hd2;
            List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selobjs2 = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerlyr, hdids);
            IPolyline pline2 = selobjs2[0].Item2 as IPolyline;

            hdids[GIS_Const.FIELD_HDID] = hd3;
            List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selobjs3 = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerlyr, hdids);
            IPolyline pline3 = selobjs3[0].Item2 as IPolyline;
            hdids[GIS_Const.FIELD_HDID] = hd1 + "_" + hd2;
            IPointCollection pntcol = new PolygonClass();

            //查询对应的回采进尺的面
            IFeatureClass feaclss = Global.hcqlyr.FeatureClass;
            IWorkspaceEdit wks = (feaclss as IDataset).Workspace as IWorkspaceEdit;
            wks.StartEditing(false);
            wks.StartEditOperation();

            string sql = "\"" + GIS_Const.FIELD_HDID + "\"='" + hdids[GIS_Const.FIELD_HDID] + "' AND \"" + GIS_Const.FIELD_BS + "\"=0 AND " + GIS_Const.FIELD_BID + "='" + Bid + "'";
            IQueryFilter queryfilter = new QueryFilterClass();
            queryfilter.WhereClause = sql;
            IFeatureCursor hdhc_cursors = feaclss.Update(queryfilter, false);
            IFeature hdhc_fea = hdhc_cursors.NextFeature();
            if (hdhc_fea == null)
            {
                MessageBox.Show("没有找到对应的回采进尺空间信息，请检查数据库和地图！", "系统提示");
                return null;
            }
            else
            {
                //修改当前的回采进尺
                List<IPoint> pnthccols = new List<IPoint>();

                IPolygon hcpolygon = hdhc_fea.Shape as IPolygon;
                Dictionary<string, List<IPoint>> oldpnts = Global.commonclss.getCoordinates(hcpolygon, pline1, pline2, pline3, zywid, fywid);
                if (oldpnts.Count == 0)
                    return null;
                IPointCollection pts = hcpolygon as IPointCollection;
                //IPoint ptstart = pts.get_Point(0);
                //IPoint ptend = pts.get_Point(pts.PointCount-1);
                IPoint ptstart = oldpnts["2"][1];
                IPoint ptend = oldpnts["2"][0];
                IPoint ptcenter = new PointClass();
                ptcenter.X = (ptstart.X + ptend.X) / 2;
                ptcenter.Y = (ptstart.Y + ptend.Y) / 2;
                //原始中心点的坐标
                IPoint ptcenter1 = new PointClass();
                //ptcenter1.X = (pts.get_Point(1).X + pts.get_Point(2).X) / 2;
                //ptcenter1.Y = (pts.get_Point(1).Y + pts.get_Point(2).Y) / 2;
                ptcenter1.X = (oldpnts["1"][0].X + oldpnts["1"][1].X) / 2;
                ptcenter1.Y = (oldpnts["1"][0].Y + oldpnts["1"][1].Y) / 2;

                //设置回采移动的切眼
                int dirflag = Global.commonclss.GetDirectionByPnt(pline3, ptcenter);
                IPolyline pline3new = new PolylineClass();
                pline3new.FromPoint = oldpnts["2"][0];
                pline3new.ToPoint = oldpnts["2"][1];
                pntcol = Global.hcjsclass.GetBackPolygonArea(pline1, pline2, pline3new, zywid, fywid, qywid, hccd, dirflag);
                if (pntcol != null)
                {
                    Dictionary<string, string> dics = new Dictionary<string, string>();
                    for (int i = 0; i < pntcol.PointCount - 1; i++)
                    {
                        pnthccols.Add(pntcol.get_Point(i));
                    }
                    IPolygon polygon = Global.commonclss.CreatePolygonFromPnts(pnthccols, Global.spatialref);
                    hdhc_fea.Shape = polygon;
                    hdhc_cursors.UpdateFeature(hdhc_fea);

                    IPoint prevHcPoint = pntcol.get_Point(pntcol.PointCount - 1);
                    prevHcPoint.Z = 0.0;
                    //将当前的回采中心点添加到结果集中，以备更新数据库表中的workingface表
                    respnts.Add(Bid, prevHcPoint);
                    //更新导线点图层中的点
                    //Global.commonclss.UpdateFeature(sql, prevHcPoint, Global.pntlyr);
                    //平移或缩放后面的回采进尺面
                    double xdeta = prevHcPoint.X - ptcenter1.X;
                    double ydeta = prevHcPoint.Y - ptcenter1.Y;
                    int xhpos = hdhc_fea.Fields.FindField(GIS_Const.FIELD_XH);
                    int xh = Convert.ToInt32(hdhc_fea.get_Value(xhpos));
                    Marshal.ReleaseComObject(hdhc_cursors);
                    //string sql_hdfull = "\""+GIS.GIS_Const.FIELD_HDID+"\"='" + hdids[GIS.GIS_Const.FIELD_HDID] + "' AND \""+GIS.GIS_Const.FIELD_BS+"\"=0 AND "+GIS.GIS_Const.FIELD_XH+">" + xh.ToString();
                    string sql_hdfull = "\"HdId\"='" + hdids[GIS_Const.FIELD_HDID] + "' AND \"" + GIS_Const.FIELD_BS + "\"=0 AND " + GIS_Const.FIELD_XH + ">" + xh.ToString();
                    IQueryFilter queryfilter1 = new QueryFilterClass();
                    queryfilter1.WhereClause = sql_hdfull;
                    queryfilter1.SubFields = "*";
                    IFeatureCursor otherhcursor = feaclss.Update(queryfilter1, false);
                    IFeature fea_hc = otherhcursor.NextFeature();
                    int bidpos = otherhcursor.FindField(GIS_Const.FIELD_BID);
                    while (fea_hc != null)
                    {
                        IPolygon reg = fea_hc.Shape as IPolygon;
                        ITransform2D trans = reg as ITransform2D;
                        trans.Move(xdeta, ydeta);
                        fea_hc.Shape = reg;
                        otherhcursor.UpdateFeature(fea_hc);
                        //将工作面的中心点坐标保存到集合中
                        IPointCollection ptsothers = reg as IPointCollection;
                        IPoint ptothercenter = new PointClass();
                        Dictionary<string, List<IPoint>> otherpnts = Global.commonclss.getCoordinates(reg, pline1, pline2, pline3, zywid, fywid);
                        //ptothercenter.X = (ptsothers.get_Point(pts.PointCount - 3).X + ptsothers.get_Point(pts.PointCount - 2).X) / 2;
                        //ptothercenter.Y = (ptsothers.get_Point(pts.PointCount - 3).Y + ptsothers.get_Point(pts.PointCount - 2).Y) / 2;
                        ptothercenter.X = (otherpnts["1"][0].X + otherpnts["1"][1].X) / 2;
                        ptothercenter.Y = (otherpnts["1"][0].Y + otherpnts["1"][1].Y) / 2;
                        ptothercenter.Z = 0.0;
                        string bidval = fea_hc.get_Value(bidpos).ToString();
                        respnts.Add(bidval, ptothercenter);
                        //更新导线点图层
                        //Global.commonclss.UpdateFeature(sql_hdfull, ptothercenter, Global.pntlyr);
                        fea_hc = otherhcursor.NextFeature();
                    }
                    Marshal.ReleaseComObject(otherhcursor);
                }
            }
            Global.pActiveView.Refresh();
            return respnts;
            //将获得的点写入到工作面表中
        }

        /// <summary>
        /// 回采进尺更新
        /// </summary>
        /// <param name="hd1">主巷道ID</param>
        /// <param name="hd2">辅助巷道ID</param>
        /// <param name="hd3">切眼巷道ID</param>
        /// <param name="Bid">要修改的回采对象的BID</param>
        /// <param name="hccd">回采的尺度</param>
        /// <param name="search">查询附近地质构造的距离</param>
        /// <returns></returns>
        public Dictionary<string, IPoint> DelHCCD(string hd1, string hd2, string hd3, string Bid, double zywid, double fywid, double search = 0)
        {
            Dictionary<string, IPoint> respnts = new Dictionary<string, IPoint>();

            Dictionary<string, string> hdids = new Dictionary<string, string>();
            hdids.Add(GIS_Const.FIELD_HDID, hd1);
            List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selobjs1 = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerlyr, hdids);
            if (selobjs1.Count > 0)
            {
                IPolyline pline1 = selobjs1[0].Item2 as IPolyline;

                hdids[GIS_Const.FIELD_HDID] = hd2;
                List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selobjs2 = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerlyr, hdids);
                IPolyline pline2 = selobjs2[0].Item2 as IPolyline;

                hdids[GIS_Const.FIELD_HDID] = hd3;
                List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selobjs3 = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerlyr, hdids);
                IPolyline pline3 = selobjs3[0].Item2 as IPolyline;
                hdids[GIS_Const.FIELD_HDID] = hd1 + "_" + hd2;
                IPointCollection pntcol = new PolygonClass();

                //查询对应的回采进尺的面
                IFeatureClass feaclss = Global.hcqlyr.FeatureClass;
                IWorkspaceEdit wks = (feaclss as IDataset).Workspace as IWorkspaceEdit;
                wks.StartEditing(false);
                wks.StartEditOperation();

                string sql = "\"" + GIS_Const.FIELD_HDID + "\"='" + hdids[GIS_Const.FIELD_HDID] + "' AND \"" + GIS_Const.FIELD_BS + "\"=0 AND " + GIS_Const.FIELD_BID + "='" + Bid + "'";
                IQueryFilter queryfilter = new QueryFilterClass();
                queryfilter.WhereClause = sql;
                IFeatureCursor hdhc_cursors = feaclss.Update(queryfilter, true);
                IFeature hdhc_fea = hdhc_cursors.NextFeature();

                int bidpos = hdhc_cursors.FindField(GIS_Const.FIELD_BID);

                if (hdhc_fea == null)
                {
                    MessageBox.Show("没有找到对应的回采进尺空间信息，请检查数据库和地图！", "系统提示");
                    return null;
                }
                else
                {
                    //修改当前的回采进尺
                    List<IPoint> pnthccols = new List<IPoint>();
                    IPolygon hcpolygon = hdhc_fea.Shape as IPolygon;

                    Dictionary<string, List<IPoint>> oldpnts = Global.commonclss.getCoordinates(hcpolygon, pline1, pline2, pline3, zywid, fywid);
                    if (oldpnts.Count == 0)
                        return null;
                    IPoint ptstart = oldpnts["2"][1];
                    IPoint ptend = oldpnts["2"][0];
                    IPoint ptcenter = new PointClass();
                    ptcenter.X = (ptstart.X + ptend.X) / 2;
                    ptcenter.Y = (ptstart.Y + ptend.Y) / 2;

                    //原始中心点的坐标
                    IPoint ptcenter1 = new PointClass();
                    //ptcenter1.X = (pts.get_Point(1).X + pts.get_Point(2).X) / 2;
                    //ptcenter1.Y = (pts.get_Point(1).Y + pts.get_Point(2).Y) / 2;
                    ptcenter1.X = (oldpnts["1"][0].X + oldpnts["1"][1].X) / 2;
                    ptcenter1.Y = (oldpnts["1"][0].Y + oldpnts["1"][1].Y) / 2;

                    // 平移或缩放后的回采进尺
                    double xdeta = ptcenter.X - ptcenter1.X;
                    double ydeta = ptcenter.Y - ptcenter1.Y;
                    //将当前的回采中心点添加到结果集中，以备更新数据库表中的workingface表
                    string bidval0 = hdhc_fea.get_Value(bidpos).ToString();
                    IPoint centerpt = new PointClass();
                    centerpt.X = ptcenter1.X + xdeta;
                    centerpt.Y = ptcenter1.Y + ydeta;
                    centerpt.Z = 0.0;
                    respnts.Add(bidval0, centerpt);

                    //更新导线点图层中的点
                    int xhpos = hdhc_fea.Fields.FindField(GIS_Const.FIELD_XH);
                    int xh = Convert.ToInt32(hdhc_fea.get_Value(xhpos));
                    hdhc_fea.Delete();


                    string sql_hdfull = "\"" + GIS_Const.FIELD_HDID + "\"='" + hdids[GIS_Const.FIELD_HDID] + "' AND \"" + GIS_Const.FIELD_BS + "\"=0 AND " + GIS_Const.FIELD_XH + ">" + xh.ToString();
                    IQueryFilter queryfilter1 = new QueryFilterClass();
                    queryfilter1.WhereClause = sql_hdfull;
                    IFeatureCursor otherhcursor = feaclss.Update(queryfilter1, true);
                    IFeature fea_hc = otherhcursor.NextFeature();

                    while (fea_hc != null)
                    {
                        IPolygon reg = fea_hc.Shape as IPolygon;
                        ITransform2D trans = reg as ITransform2D;
                        trans.Move(xdeta, ydeta);
                        fea_hc.Shape = reg;
                        otherhcursor.UpdateFeature(fea_hc);
                        //将工作面的中心点坐标保存到集合中
                        IPointCollection ptsothers = reg as IPointCollection;
                        IPoint ptothercenter = new PointClass();
                        ptothercenter.X = (ptsothers.get_Point(1).X + ptsothers.get_Point(2).X) / 2;
                        ptothercenter.Y = (ptsothers.get_Point(1).Y + ptsothers.get_Point(2).Y) / 2;
                        ptothercenter.Z = 0.0;
                        string bidval = fea_hc.get_Value(bidpos).ToString();
                        respnts.Add(bidval, ptothercenter);
                        //更新导线点图层
                        //Global.commonclss.UpdateFeature(sql_hdfull, ptothercenter, Global.pntlyr);
                        fea_hc = otherhcursor.NextFeature();
                    }
                    Marshal.ReleaseComObject(otherhcursor);
                }
                Marshal.ReleaseComObject(hdhc_cursors);
                wks.StopEditOperation();
                wks.StopEditing(true);
            }
            Global.pActiveView.Refresh();
            return respnts;
            //将获得的点写入到工作面表中
        }

    }
}
