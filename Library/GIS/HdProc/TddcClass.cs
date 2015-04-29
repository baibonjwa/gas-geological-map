using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System.Runtime.InteropServices;
using GIS.HdProc;
using LibEntity;

namespace GIS.HdProc
{
    //推断断层绘制类
    public class TddcClass
    {
        private string BID = "";
        /// <summary>
        /// 删除BID对应的推断断层符号
        /// </summary>
        /// <param name="bid"></param>
        public void DelTdLyr(string[] bid)
        {
            //首先删除对应BID的推断断层信息
            for (int i = 0; i < bid.Count(); i++)
            {
                string sql = GIS.GIS_Const.FIELD_BID + " ='" + bid[i] + "'";
                Global.commonclss.DelFeatures(Global.tdlyr, sql);
            }
        }
        /// <summary>
        /// 修改推断断层的符号
        /// </summary>
        public void UpdateTdLyr(string collapsePoints, string bid)
        {
            //首先删除对应BID的推断断层信息
            string sql = GIS.GIS_Const.FIELD_BID + "='" + bid + "'";
            Global.commonclss.DelFeatures(Global.tdlyr, sql);
            //重新绘制推断断层符号
            AddTdLyr(collapsePoints, bid);
        }
        /// <summary>
        /// 添加推断断层符号
        /// </summary>
        /// <param name="collapsePoints"></param>
        public void AddTdLyr(string collapsePoints, string bid)
        {
            BID = bid;
            string param = ConstructStr(collapsePoints);
            string sql = BigFaultage.CFaultageName + " IN (" + param + ")";
            IFeatureCursor feacursors = Global.commonclss.PropertySearch(sql, Global.jllyr);
            IFeature fea = feacursors.NextFeature();
            List<IPolyline> plines = new List<IPolyline>();
            while (fea != null)
            {
                IPolyline plin = fea.Shape as IPolyline;
                plines.Add(plin);

                fea = feacursors.NextFeature();
            }
            //处理集合中线的顺序（从左到右）
            List<IPolyline> rightlines = ProcLines(plines);
            //绘制连接线上的曲线信息
            List<IPolyline> reslist = GetExtendPnts(rightlines);
            //绘制推断断层的曲线
            AddCurveToMap(Global.tdlyr, reslist, rightlines);
            Global.pActiveView.Refresh();
        }
        /// <summary>
        /// 构造sql语句
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private string ConstructStr(string param)
        {
            string sql = "";
            string[] chrs = param.Split(',').ToArray();
            for (int i = 0; i < chrs.Count(); i++)
            {
                string stmp = chrs[i];
                if (i == 0)
                    sql = "'" + stmp + "'";
                else
                    sql += ",'" + stmp + "'";
            }
            return sql;
        }
        /// <summary>
        /// 求每段延长线上的点坐标
        /// </summary>
        /// <returns></returns>
        private List<IPolyline> GetExtendPnts(List<IPolyline> polylines)
        {
            List<IPolyline> reslist = new List<IPolyline>();
            for (int i = 0; i < polylines.Count; i++)
            {
                IPolyline plin = polylines[i];
                //求延长线上指定长度的坐标
                IPoint outpto = new PointClass();
                outpto.SpatialReference = Global.spatialref;
                IPoint outpfrom = new PointClass();
                outpfrom.SpatialReference = Global.spatialref;
                if (i == 0)
                {
                    double distance = Math.Sqrt(Math.Pow(plin.ToPoint.X - polylines[i + 1].FromPoint.X, 2) + Math.Pow(plin.ToPoint.Y - polylines[i + 1].FromPoint.Y, 2));

                    plin.QueryPoint(esriSegmentExtension.esriExtendAtTo, distance / 3.0 + plin.Length, false, outpto);
                    plin.QueryPoint(esriSegmentExtension.esriExtendAtFrom, -distance / 3.0, false, outpfrom);
                }
                else if (i == polylines.Count - 1)
                {
                    double distance1 = Math.Sqrt(Math.Pow(plin.FromPoint.X - polylines[i - 1].ToPoint.X, 2) + Math.Pow(plin.FromPoint.Y - polylines[i - 1].ToPoint.Y, 2));

                    plin.QueryPoint(esriSegmentExtension.esriExtendAtTo, distance1 / 3.0 + plin.Length, false, outpto);
                    plin.QueryPoint(esriSegmentExtension.esriExtendAtFrom, -distance1 / 3.0, false, outpfrom);
                }
                else
                {
                    double distance2 = Math.Sqrt(Math.Pow(plin.ToPoint.X - polylines[i + 1].FromPoint.X, 2) + Math.Pow(plin.ToPoint.Y - polylines[i + 1].FromPoint.Y, 2));
                    double distance3 = Math.Sqrt(Math.Pow(plin.ToPoint.X - polylines[i - 1].FromPoint.X, 2) + Math.Pow(plin.ToPoint.Y - polylines[i - 1].FromPoint.Y, 2));

                    plin.QueryPoint(esriSegmentExtension.esriExtendAtTo, distance2 / 3.0 + plin.Length, false, outpto);
                    plin.QueryPoint(esriSegmentExtension.esriExtendAtFrom, -distance3 / 3.0, false, outpfrom);
                }
                IPolyline plinnew = new PolylineClass();
                plinnew.FromPoint = outpfrom;
                plinnew.ToPoint = outpto;
                reslist.Add(plinnew);
            }
            return reslist;
        }
        /// <summary>
        /// 处理线的顺序
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        private List<IPolyline> ProcLines(List<IPolyline> lines)
        {
            for (int i = 0; i < lines.Count - 1; i++)
            {
                for (int j = i + 1; j < lines.Count; j++)
                {
                    IPoint pntprev = new PointClass() { X = (lines[i].FromPoint.X + lines[i].ToPoint.X) / 2, Y = (lines[i].FromPoint.Y + lines[i].ToPoint.Y) / 2 };
                    IPoint pntnext = new PointClass() { X = (lines[j].FromPoint.X + lines[j].ToPoint.X) / 2, Y = (lines[j].FromPoint.Y + lines[j].ToPoint.Y) / 2 };
                    IPolyline plin = new PolylineClass();
                    if (pntprev.X > pntnext.X)
                    {
                        plin = lines[j];
                        lines[j] = lines[i];
                        lines[i] = plin;
                    }

                }

            }
            return lines;
        }
        /// <summary>
        /// 绘制曲线部分
        /// </summary>
        /// <param name="lyr">推断断层</param>
        /// <param name="newplines">延长点构造的线</param>
        /// <param name="originlines">原始点构造的线</param>
        private void AddCurveToMap(IFeatureLayer lyr, List<IPolyline> newplines, List<IPolyline> originlines)
        {
            IFeatureClass Featureclass = lyr.FeatureClass;
            IWorkspaceEdit workspace = (IWorkspaceEdit)(Featureclass as IDataset).Workspace;
            workspace.StartEditing(true);
            workspace.StartEditOperation();
            object missing = Type.Missing;
            //揭露断层上的曲线对象
            IGeometry geom = new PolylineClass();
            geom.SpatialReference = Global.spatialref;
            IGeometryCollection outgeomcols = geom as IGeometryCollection;
            IFeature fea = Featureclass.CreateFeature();
            ISegmentCollection newpath = new PathClass();
            int kindpos = Featureclass.Fields.FindField(GIS.GIS_Const.FIELD_TYPE);
            int bidpos = Featureclass.Fields.FindField(GIS.GIS_Const.FIELD_BID);
            for (int i = 0; i < newplines.Count; i++)
            {
                ILine lin = new LineClass();
                lin.SpatialReference = Global.spatialref;
                //直线段
                IPath path = new PathClass();
                if (i == 0)
                {
                    lin.FromPoint = newplines[i].FromPoint;
                    lin.ToPoint = originlines[i].ToPoint;
                }
                else if (i == originlines.Count - 1)
                {
                    lin.FromPoint = originlines[i].FromPoint;
                    lin.ToPoint = newplines[i].ToPoint;
                }
                else
                {
                    lin.FromPoint = originlines[i].FromPoint;
                    lin.ToPoint = originlines[i].ToPoint;
                }
                newpath.AddSegment(lin as ISegment, ref missing, ref missing);
                //曲线段
                if (i < newplines.Count - 1)
                {
                    IBezierCurveGEN bezier = new BezierCurveClass();
                    IPoint[] pntcontrols = new IPoint[4];
                    pntcontrols[0] = originlines[i].ToPoint;
                    pntcontrols[1] = newplines[i].ToPoint;
                    pntcontrols[2] = newplines[i + 1].FromPoint;
                    pntcontrols[3] = originlines[i + 1].FromPoint;
                    bezier.PutCoords(ref pntcontrols);

                    newpath.AddSegment(bezier as ISegment, ref missing, ref missing);
                }
            }
            outgeomcols.AddGeometry(newpath as IGeometry, ref missing, ref missing);
            int index = fea.Fields.FindField(GIS_Const.FIELD_SHAPE);
            IGeometryDef geometryDef = fea.Fields.get_Field(index).GeometryDef as IGeometryDef;
            if (geometryDef.HasZ)
            {
                IZAware pZAware = (IZAware)outgeomcols;
                pZAware.ZAware = true;
            }
            fea.Shape = outgeomcols as IPolyline;
            fea.set_Value(kindpos, 1);
            fea.set_Value(bidpos, BID);
            fea.Store();
            //外围曲线绘制
            ITopologicalOperator2 top = outgeomcols as ITopologicalOperator2;
            if (!top.IsSimple)
                top.Simplify();
            IConstructCurve curve = new PolylineClass();
            curve.ConstructOffset(outgeomcols as IPolyline, -3, ref missing, ref missing);
            IPolyline plinnew = curve as IPolyline;
            plinnew.SpatialReference = Global.spatialref;
            plinnew.FromPoint = newplines[0].FromPoint;
            plinnew.ToPoint = newplines[newplines.Count - 1].ToPoint;

            IFeature outcurve = Featureclass.CreateFeature();
            outcurve.set_Value(kindpos, 2);
            outcurve.set_Value(bidpos, BID);
            outcurve.Shape = plinnew;
            outcurve.Store();
            //结束编辑
            workspace.StopEditOperation();
            workspace.StopEditing(true);
            //定位跳转
            Global.commonclss.JumpToGeometry(plinnew);
        }
    }
}
