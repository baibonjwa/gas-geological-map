using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;

namespace GIS.HdProc
{
    /// <summary>
    /// 切眼计算类
    /// </summary>
    public class QYJSClass
    {
        /// <summary>
        /// 获得回采多边形点列表，最后点为切眼中心点
        /// </summary>
        /// <param name="pline1"></巷道1>
        /// <param name="pline2"></巷道2>
        /// <param name="pline3"></切眼>
        /// <param name="widthhd"></巷道宽度>
        /// <param name="widthqy"></切眼宽度>
        /// <param name="hcDis"></回采距离>
        /// <param name="direct"></回采方向，pline3 from-to方向,返回1-切眼右侧, 返回-1-切眼左侧（），只在中间回采时使用此参数,开始回采时值为0>
        /// <returns></returns>
        public IPointCollection GetBackPolygonArea(IPolyline pline1, IPolyline pline2, IPolyline pline3, double widthhd, double widthhd1, double widthqy, double hcDis, int direct)
        {
            object obj = Type.Missing;
            //ESRI.ArcGIS.Geometry.IPolyline polylineall = new ESRI.ArcGIS.Geometry.PolylineClass();
            IPolyline offsetqy = null;
            Boolean bright = false;
            //交点1（切眼与巷道1交点）
            IPoint point1 = GetIntersectPointExtend(pline3, pline1);

            //交点2（切眼与巷道2交点）
            IPoint point2 = GetIntersectPointExtend(pline3, pline2);

            //CreateFeature(pline3, GetLayerByName("中心线全"));
            int iDirect; //依据切眼与巷道的关系判断回采方向,切眼为巷道起点时使用
            if (direct != 0) //中心点回采
            {
                IPoint outpnt = new PointClass();
                iDirect = direct;
                offsetqy = GetExtentQy(pline1, pline2, pline3);
            }
            else
            {
                iDirect = GetBackDirection(pline3, 200, pline1, pline2);
                offsetqy = ConstructOffset(pline3, widthqy / 2.0 * iDirect);
            } //切眼平移后的线
            IPoint pointCent = ConstructMiddlePoint(offsetqy, offsetqy.Length / 2);//取平移后线的中点
            //平移两侧巷道
            //CreateFeature(pointCent, GetLayerByName("testpoint"));
            //CreateFeature(offsetqy, GetLayerByName("中心线全"));
            IPolyline offsethd1 = GetConstructOffsetHD(pline1, widthhd / 2, pointCent);
            IPolyline offsethd2 = GetConstructOffsetHD(pline2, widthhd1 / 2, pointCent);

            //取两条巷道与切眼交点
            IPoint pnt1 = GetIntersectPoint(offsethd1, offsetqy);
            IPoint pnt2 = GetIntersectPoint(offsethd2, offsetqy);

            ////理顺巷道线的方向
            IPoint outpoint = new PointClass();
            IPolyline offsethd1dir = GetPointPos(pline3.FromPoint, pline3.ToPoint, offsethd1, iDirect);
            IPolyline offsethd2dir = GetPointPos(pline3.FromPoint, pline3.ToPoint, offsethd2, iDirect);

            double dishd1 = 0; double alonghd1 = 0;
            offsethd1dir.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pnt1, false, outpoint, ref alonghd1, ref dishd1, ref bright);

            double dishd2 = 0; double alonghd2 = 0;
            offsethd2dir.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pnt2, false, outpoint, ref alonghd2, ref dishd2, ref bright);

            ICurve newhd1 = new PolylineClass();
            offsethd1dir.GetSubcurve(alonghd1, hcDis + alonghd1, false, out newhd1);

            ICurve newhd2 = new PolylineClass();
            offsethd2dir.GetSubcurve(alonghd2, hcDis + alonghd2, false, out newhd2);

            IPointCollection pntPolycol = new PolygonClass() as IPointCollection;
            IPointCollection pntlinecol = newhd1 as IPointCollection;
            int i = 0;
            IPoint point = null;
            for (i = 0; i < pntlinecol.PointCount; i++)
            {
                point = pntlinecol.get_Point(i);
                pntPolycol.AddPoint(point, ref obj, ref obj);
            }

            pntlinecol = newhd2 as IPointCollection;

            //取回采后切眼的中点
            IPolyline polylineendqy = new PolylineClass();

            if (point == null)
            {
                polylineendqy.FromPoint = point1;
                polylineendqy.ToPoint = point2;
            }
            else
            {
                polylineendqy.FromPoint = point;
                polylineendqy.ToPoint = pntlinecol.get_Point(pntlinecol.PointCount - 1);
            }
            IPoint pointendqyCent = ConstructMiddlePoint(polylineendqy, polylineendqy.Length / 2);//终点切眼的中点

            for (i = pntlinecol.PointCount - 1; i >= 0; i--)
            {
                point = pntlinecol.get_Point(i);
                pntPolycol.AddPoint(point, ref obj, ref obj);
            }
            pntPolycol.AddPoint(pointendqyCent, ref obj, ref obj);

            return pntPolycol;

        }
        /// <summary>
        /// 延长切眼线使之与两条巷道相交，
        /// </summary>
        /// <param name="ply1"></巷道1>
        /// <param name="ply2"></巷道2>
        /// <param name="plyqy"></切眼>
        /// <returns></延长的的切眼线>
        private IPolyline GetExtentQy(IPolyline ply1, IPolyline ply2, IPolyline plyqy)
        {
            IPolyline ply = null;
            IConstructCurve cons1 = new PolylineClass();
            bool isExtensionPerfomed = false;
            cons1.ConstructExtended(plyqy, ply1, (int)(esriCurveExtension.esriDefaultCurveExtension), ref isExtensionPerfomed);
            IPolyline qya = cons1 as IPolyline;
            IConstructCurve cons2 = new PolylineClass();
            cons2.ConstructExtended(qya, ply2, (int)esriCurveExtension.esriDefaultCurveExtension, ref isExtensionPerfomed);
            //IPoint pnthd1 = GetIntersectPoint(ply1, cons1 as IPolyline);
            //IPoint pnthd2 = GetIntersectPoint(ply2, cons2 as IPolyline);
            //plyqy.GetSubcurve();
            ply = cons2 as IPolyline;
            return ply;
        }

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

        private IPoint ConstructMiddlePoint(IPolyline pline, double distance)
        {
            IConstructPoint constructpnt = new PointClass(); ;
            constructpnt.ConstructAlong(pline, esriSegmentExtension.esriNoExtension, distance, false);
            return constructpnt as IPoint;
        }

        /// <summary>
        /// 向pline两侧指定距离做平行线，比较与point 的距离，返回距离近的偏移线做为偏移后的巷道
        /// </summary>
        /// <param name="pline"></param>
        /// <param name="distance"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private IPolyline GetConstructOffsetHD(IPolyline pline, double distance, IPoint point)
        {
            IPolyline line1 = ConstructOffset(pline, distance);
            IPolyline line2 = ConstructOffset(pline, distance * -1);
            //CreateFeature(line1, GetLayerByName("testline"));
            //CreateFeature(line2, GetLayerByName("testline"));
            object obj = Type.Missing;
            double dis1 = 0; double dis2 = 0;
            double alongdis1 = 0; double alongdis2 = 0;
            IPoint outPoint = new PointClass();
            Boolean bright1 = false; Boolean bright2 = false;
            line1.QueryPointAndDistance(esriSegmentExtension.esriExtendEmbedded, point, false, outPoint, ref alongdis1, ref dis1, ref bright1);
            IPoint outPoint2 = new PointClass();
            line2.QueryPointAndDistance(esriSegmentExtension.esriExtendEmbedded, point, false, outPoint2, ref alongdis2, ref dis2, ref bright2);
            if (dis1 < dis2) return line1;
            else return line2;
        }

        /// <summary>
        /// 判断切眼的回采方向
        /// </summary>
        /// <param name="pline"></切眼线>
        /// <param name="distance"></偏移距离>
        /// <param name="plyhd1"></巷道1>
        /// <param name="plyhd2"></巷道2>
        /// <returns></returns>
        private int GetBackDirection(IPolyline pline, double distance, IPolyline plyhd1, IPolyline plyhd2)
        {
            int direc = 0;
            IPolyline line1 = ConstructOffset(pline, distance);
            IPolyline line2 = ConstructOffset(pline, distance * -1);

            IPoint pnthd1 = GetIntersectPointExtend(line1, plyhd1);
            IPoint pnthd2 = GetIntersectPointExtend(line1, plyhd2);

            if (pnthd1 != null && pnthd2 != null)
            { direc = 1; }
            else
            { direc = -1; }
            return direc;
        }

        private IPoint GetIntersectPoint(IPolyline line1, IPolyline line2)
        {
            IPoint pnt = null;
            ITopologicalOperator topo = line1 as ITopologicalOperator;
            IGeometry geo = topo.Intersect(line2, esriGeometryDimension.esriGeometry0Dimension);
            IMultipoint mulpoint = geo as IMultipoint;
            IPointCollection pntcol = mulpoint as IPointCollection;
            if (pntcol != null)
                pnt = pntcol.get_Point(0);
            return pnt;
        }

        /// <summary>
        /// 判断pline的frompoint 在pnt1和 pnt2组成的线的哪一侧
        /// </summary>
        /// <param name="pnt1"></param>
        /// <param name="pnt2"></param>
        /// <param name="pline"></param>
        /// <returns>返回与回采方向相同的巷道线 </returns>
        private IPolyline GetPointPos(IPoint pnt1, IPoint pnt2, IPolyline pline, int iDirect)
        {
            IPoint pntstart = pline.FromPoint;
            if (pntstart == pnt1) //pline 起点与pnt1相同
            {
                return pline;
            }
            else if (pline.ToPoint == pnt1) //pline 终点与pnt1相同
            {
                pline.ReverseOrientation();
                //return pline;
            }
            else //非切眼状态
            {
                //(p2-p0)*(p1-p0)>0 则p0p1在p1点拐向右侧得到p1p2,(p2-p0)*(p1-p0)<0 则p0p1在p1点拐向左侧得到p1p2, p1为共同点
                IPoint p1p0 = new PointClass();
                //p1-p0
                p1p0.X = pnt1.X - pnt2.X;
                p1p0.Y = pnt1.Y - pnt2.Y;
                IPoint p2p0 = new PointClass();
                //p2-p0
                p2p0.X = pntstart.X - pnt2.X;
                p2p0.Y = pntstart.Y - pnt2.Y;
                //(p2-p0)*(p1-p0)
                double newrel = p2p0.X * p1p0.Y - p1p0.X * p2p0.Y;
                if (newrel > 0)  //frompoint 在切眼右侧
                {
                    if (iDirect == -1)
                        pline.ReverseOrientation();
                }
                else if (newrel < 0) //frompoint 在切眼左侧
                {
                    if (iDirect == 1)
                        pline.ReverseOrientation();
                    //return pline;
                }
            }
            return pline;
        }

        /// <summary>
        /// 将line1延长与line2相交后求交点
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <returns></returns>
        private IPoint GetIntersectPointExtend(IPolyline line1, IPolyline line2)
        {
            IPoint pnt = null;
            IConstructCurve constructCurve = new PolylineClass();
            bool isExtensionPerfomed = false;
            constructCurve.ConstructExtended(line1, line2, (int)esriCurveExtension.esriDefaultCurveExtension, ref isExtensionPerfomed);
            ITopologicalOperator topo = constructCurve as ITopologicalOperator;
            IGeometry geo = topo.Intersect(line2, esriGeometryDimension.esriGeometry0Dimension);
            IMultipoint mulpoint = geo as IMultipoint;
            IPointCollection pntcol = mulpoint as IPointCollection;
            if (pntcol != null && pntcol.PointCount > 0)
                pnt = pntcol.get_Point(0);
            return pnt;
        }
    }
}
