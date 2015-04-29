using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using GIS.Common;
using ESRI.ArcGIS.Geodatabase;

namespace GIS.SpecialGraphic
{
    public class DrawXZZ
    {
        /// <summary>
        /// 绘制小柱状
        /// </summary>
        /// <param name="datasources">各地质层厚度(0.83,0.3,1.2,200)最后一个值为底板标高</param>
        /// <param name="pt">小柱状中心点</param>
        /// <param name="Angle">旋转角度</param>
        /// <param name="bili">比例</param>
        /// <param name="bid">BID</param>
        /// <param name="edit">函数外控制编辑状态为false，函数内自动控制编辑状态为true，当批量绘图时建议函数外控制编辑状态</param>
        public static bool drawXZZ(List<KeyValuePair<int, double>> datasources, IPoint pt,double Angle, string bid, double bili = 1, bool edit = true)
        {
            double angle = -Angle;
            Angle = -Angle * Math.PI / 180;
            DrawXZZMap draw = new DrawXZZMap(bili);
            var AnnoLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_AnnotationXZZ) as IFeatureLayer;//注记图层
            var lineLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_PolylineXZZ) as IFeatureLayer;//线源图层
            var topLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_PolygonXZZ) as IFeatureLayer; //外部图形图层
            if (AnnoLayer == null || lineLayer == null || topLayer == null)
            {
                System.Windows.Forms.MessageBox.Show("小柱状图层缺失！");
                return false;
            }
            IWorkspaceEdit workspaceEdit=null;
            if (edit)
            {
                var dataset = lineLayer.FeatureClass as IDataset;
                workspaceEdit = dataset.Workspace as IWorkspaceEdit;
                workspaceEdit.StartEditing(true);
                workspaceEdit.StartEditOperation();
            }
            try
            {
                //  准备添加数据
                var polygonTopCursor = topLayer.FeatureClass.Insert(true);
                var polylineCursor = lineLayer.FeatureClass.Insert(true);
                var AnnoCursor = AnnoLayer.FeatureClass.Insert(true);
                //  从数据源图层获得数据
                var num1 = datasources[datasources.Count - 1].Value;     //  最后一条横线数值
                datasources.RemoveAt(datasources.Count - 1);
                if (datasources.Count > 1)
                {
                    List<IPoint> txtPoints;
                    List<IPoint> lineStartPoints;

                    var polygons = new List<KeyValuePair<string, IPolygon>>();

                    //  构造图形
                    draw.ContructGeometry(pt, datasources, out polygons, out lineStartPoints, out txtPoints);


                    ITransform2D pTrans2D;

                    //  生成方框图形
                    foreach (var polygon in polygons)
                    {
                        var feature2 = topLayer.FeatureClass.CreateFeatureBuffer();
                        feature2.set_Value(feature2.Fields.FindField("BID"), bid);
                        if (polygon.Key == "top")
                        {
                            feature2.set_Value(feature2.Fields.FindField("type"), 0);
                        }
                        else if (polygon.Key == "white")
                        {
                            feature2.set_Value(feature2.Fields.FindField("type"), 1);
                        }
                        else if (polygon.Key == "black")
                        {
                            feature2.set_Value(feature2.Fields.FindField("type"), 2);
                        }
                        pTrans2D = polygon.Value as ITransform2D;
                        //旋转要素
                        pTrans2D.Rotate(pt, Angle);
                        IPolygon ppp = pTrans2D as IPolygon;

                        GIS.Common.DataEditCommon.ZMValue(feature2, ppp);
                        feature2.Shape = ppp;

                        polygonTopCursor.InsertFeature(feature2);
                    }

                    //  生成注记
                    var enveloplist = new List<IEnvelope>();

                    for (int i = 0; i < txtPoints.Count; i++)
                    {
                        var featureAnno = AnnoLayer.FeatureClass.CreateFeatureBuffer();
                        IAnnotationFeature AnnoFeature = (IAnnotationFeature)featureAnno;

                        pTrans2D = txtPoints[i] as ITransform2D;
                        //旋转要素
                        pTrans2D.Rotate(pt, Angle);

                        ITextSymbol pTextSymbol = new TextSymbolClass();
                        pTextSymbol.Angle = angle;
                        var elementTxt = new TextElementClass
                        {
                            Geometry = pTrans2D as IGeometry,
                            FontName = "微软雅黑",
                            Size = 12 * bili,
                            SymbolID = 0,
                            Symbol=pTextSymbol
                        };

                        if (i == txtPoints.Count - 1)
                        {
                            elementTxt.Text = num1.ToString();  //  最后一条横线的数值
                            elementTxt.VerticalAlignment = esriTextVerticalAlignment.esriTVATop; //  显示在线下边
                            featureAnno.set_Value(featureAnno.Fields.FindField("strType"), 2);
                        }
                        else
                        {
                            elementTxt.Text = datasources[i].Value.ToString();
                            elementTxt.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                            featureAnno.set_Value(featureAnno.Fields.FindField("strType"), datasources[i].Key);
                        }
                        AnnoFeature.Annotation = elementTxt;
                        featureAnno.set_Value(featureAnno.Fields.FindField("strAngle"), -angle);
                        featureAnno.set_Value(featureAnno.Fields.FindField("strX"), pt.X);
                        featureAnno.set_Value(featureAnno.Fields.FindField("strY"), pt.Y);
                        featureAnno.set_Value(featureAnno.Fields.FindField("strScale"), bili);
                        featureAnno.set_Value(featureAnno.Fields.FindField("strIndex"), i+1);
                        featureAnno.set_Value(featureAnno.Fields.FindField("BID"), bid);

                        pTrans2D = featureAnno.Shape as ITransform2D;
                        pTrans2D.Rotate(pt, -Angle);

                        enveloplist.Add(((IGeometry)pTrans2D).Envelope);
                        AnnoCursor.InsertFeature(featureAnno);
                    }
                    //  生成线
                    var polyline = new PolylineClass();
                    for (int i = 0; i < enveloplist.Count; i++)
                    {
                        //  计算注记的终点
                        IPoint toPoint = new PointClass();
                        if (i % 2 == 0)
                        {
                            toPoint.X = enveloplist[i].XMax;
                        }
                        else
                        {
                            toPoint.X = enveloplist[i].XMin;
                        }
                        
                        toPoint.Y = lineStartPoints[i].Y;

                        var line = new PathClass
                        {
                            FromPoint = lineStartPoints[i],
                            ToPoint = toPoint
                        };
                        
                        polyline.AddGeometry(line);
                    }
                    var featureLine = lineLayer.FeatureClass.CreateFeatureBuffer();

                    pTrans2D = polyline as ITransform2D;
                    pTrans2D.Rotate(pt, Angle);
                    IPolyline mline = pTrans2D as IPolyline;
                    GIS.Common.DataEditCommon.ZMValue(featureLine,mline);
                    featureLine.Shape = mline;
                    featureLine.set_Value(featureLine.Fields.FindField("BID"), bid);
                    polylineCursor.InsertFeature(featureLine);
                }

                AnnoCursor.Flush();
                polygonTopCursor.Flush();
                polylineCursor.Flush();
                if (edit)
                {
                    workspaceEdit.StopEditOperation();
                    workspaceEdit.StopEditing(true);

                    DataEditCommon.g_pMyMapCtrl.ActiveView.Refresh();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (edit)
                {
                    workspaceEdit.AbortEditOperation();
                    workspaceEdit.StopEditing(false);
                }
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
    /// <summary>
    /// 画小柱状
    /// </summary>
    class DrawXZZMap
    {
        /// <summary>
        /// 图形宽度
        /// </summary>
        private double GeometryWidth { get; set; }

        /// <summary>
        /// 第一个图形高度
        /// </summary>
        private double GeometryHeight0 { get; set; }

        /// <summary>
        /// 第二个图形高度
        /// </summary>
        private double GeometryHeight1 { get; set; }

        /// <summary>
        /// 文字偏移距离
        /// </summary>
        private double TextOffsetDistance { get; set; }

        /// <summary>
        /// 最后文字偏移距离
        /// </summary>
        private double LastTextOffsetDistance { get; set; }

        /// <summary>
        /// 文字偏移角度
        /// </summary>
        private double TextOffsetAngle { get; set; }

        private const double Deg2Rad = Math.PI / 180.0;
        private const double Angle0 = 0;
        private const double Angle1 = 90;
        private const double Angle2 = 180;
        private const double Angle3 = 30;
        //private IGeometryCollection _polyline;
        private List<IPoint> _points;
        private List<IPoint> _linepoints;
        private IPoint _centerPoint;
        private double _halfHeight;
        private double _height3;
        private double _height4;
        private double BL = 1;
        public DrawXZZMap(double bili)
        {
            BL = bili;
            GeometryWidth = 1 * bili;
            GeometryHeight0 = 0.5 * bili;
            GeometryHeight1 = 0.5 * bili;
            TextOffsetDistance = 0.5 * bili;
            LastTextOffsetDistance = 0.5 * bili;
            TextOffsetAngle = 0;
        }

        public void ContructGeometry(IPoint point, List<KeyValuePair<int, double>> datasources, out List<KeyValuePair<string, IPolygon>> polygons, out List<IPoint> lineStartPoints, out List<IPoint> txtPoints)
        {
            _centerPoint = new PointClass { X = point.X, Y = point.Y };
            polygons = new List<KeyValuePair<string, IPolygon>>();

            lineStartPoints = new List<IPoint>();
            txtPoints = new List<IPoint>();

            var polygonBlack = new PolygonClass();
            var polygonWhite = new PolygonClass();
            _linepoints = lineStartPoints;
            _points = txtPoints;

            var indexLine = 0;
            var length = datasources.Sum(s => s.Value) * BL;
            var totalLength = length / 2;
            _halfHeight = length / 2;
            _height3 = _halfHeight + GeometryHeight1;
            _height4 = _halfHeight + GeometryHeight0 + GeometryHeight1;
            IPoint lastFromPoint = null;
            //IPoint lastToPoint = null;

            foreach (var d in datasources)
            {
                double dd = d.Value * BL;
                var list30 = new List<IPoint>
                {
                    ConstructPoint(_centerPoint, 180, 90, totalLength),
                    ConstructPoint(_centerPoint, 0, 90, totalLength),
                    ConstructPoint(_centerPoint, 0, 90, totalLength - dd),
                    ConstructPoint(_centerPoint, 180, 90, totalLength - dd),
                    ConstructPoint(_centerPoint, 180, 90, totalLength)
                };

                //  生成面
                var geometry = ConstructRing(list30);

                if (d.Key == 0)
                {
                    polygonWhite.AddGeometry(geometry);
                }
                else
                {
                    polygonBlack.AddGeometry(geometry);
                }

                //int angleLine = indexLine % 2 == 0 ? 0 : 180;

                var fromPoint = list30[indexLine % 2 + 2];
                //var toPoint = ConstructPoint3(fromPoint, angleLine, LineWidth);

                //  生成线
                _linepoints.Add(fromPoint);

                lastFromPoint = list30[3];
                //lastToPoint = ConstructPoint3(lastFromPoint, Angle2, LineWidth * 2);

                var txtPoint = new PointClass();
                txtPoint.ConstructAngleDistance(fromPoint, (TextOffsetAngle + indexLine % 2 * 180) * Deg2Rad, TextOffsetDistance);

                //  生成点
                _points.Add(txtPoint);

                totalLength -= dd;
                indexLine++;
            }

            if (lastFromPoint != null)
            {
                var txtLastPoint = new PointClass();
                txtLastPoint.ConstructAngleDistance(lastFromPoint, 180 * Deg2Rad, LastTextOffsetDistance);

                //  生成点
                _points.Add(txtLastPoint);
                //  生成线
                _linepoints.Add(lastFromPoint);
            }


            polygonWhite.Close();
            polygonBlack.Close();
            var tPolygon = new PolygonClass();
            polygons.Add(new KeyValuePair<string, IPolygon>("top", ContructGeometry(out tPolygon)));
            polygons.Add(new KeyValuePair<string, IPolygon>("top", tPolygon));
            polygons.Add(new KeyValuePair<string, IPolygon>("white", polygonWhite));
            polygons.Add(new KeyValuePair<string, IPolygon>("black", polygonBlack));
        }

        private IPolygon ContructGeometry(out PolygonClass polygon1)
        {
            polygon1 = new PolygonClass();
            var polygon = new PolygonClass();
            var p00 = ConstructPoint(_centerPoint, Angle0, Angle1, _height4, 0);
            var p01 = ConstructPoint(_centerPoint, Angle2, Angle1, _height4);
            var p04 = ConstructPoint(_centerPoint, Angle0, Angle1, _height4);

            var list0 = new List<IPoint>
            {
                p01,
                ConstructPoint2(p01, Angle3, p00, Angle2 - Angle3),
                ConstructPoint2(p00, -Angle3, p04, Angle2 + Angle3),
                p04,
                ConstructPoint(_centerPoint, Angle0, Angle1, _height3),
                ConstructPoint(_centerPoint, Angle2, Angle1, _height3),
                p01
            };

            polygon1.AddGeometry(ConstructRing(list0));

            var list1 = new List<IPoint>
            {
                p00,
                ConstructPoint2(p00, Angle3, p04, Angle2 - Angle3),
                p04,
                ConstructPoint2(p00, -Angle3, p04, Angle2 + Angle3),
                p00
            };

            polygon.AddGeometry(ConstructRing(list1));

            var list2 = new List<IPoint>
            {
                ConstructPoint(_centerPoint, Angle2, Angle1, _height3),
                ConstructPoint(_centerPoint, Angle0, Angle1, _height3),
                ConstructPoint(_centerPoint, Angle0, Angle1, _halfHeight),
                ConstructPoint(_centerPoint, Angle2, Angle1, _halfHeight),
                ConstructPoint(_centerPoint, Angle2, Angle1, _height3)
            };

            polygon.AddGeometry(ConstructRing(list2));

            var list3 = new List<IPoint>
            {
                ConstructPoint(_centerPoint, Angle2, -Angle1, _height3),
                ConstructPoint(_centerPoint, Angle0, -Angle1, _height3),
                ConstructPoint(_centerPoint, Angle0, -Angle1, _halfHeight),
                ConstructPoint(_centerPoint, Angle2, -Angle1, _halfHeight),
                ConstructPoint(_centerPoint, Angle2, -Angle1, _height3)
            };

            polygon.AddGeometry(ConstructRing(list3));

            var p20 = ConstructPoint(_centerPoint, Angle0, -Angle1, _height4, 0);
            var p21 = ConstructPoint(_centerPoint, Angle2, -Angle1, _height4);
            var p24 = ConstructPoint(_centerPoint, Angle0, -Angle1, _height4);

            var list4 = new List<IPoint>
            {
                p21,
                ConstructPoint2(p21, Angle3, p20, Angle2 - Angle3),
                ConstructPoint2(p20, -Angle3, p24, Angle2 + Angle3),
                p24,
                ConstructPoint(_centerPoint, Angle0, -Angle1, _height3),
                ConstructPoint(_centerPoint, Angle2, -Angle1, _height3),
                p21
            };

            polygon1.AddGeometry(ConstructRing(list4));

            var list5 = new List<IPoint>
            {
                p20,
                ConstructPoint2(p20, Angle2 - Angle3, p21, Angle3),
                p21,
                ConstructPoint2(p20, Angle2 + Angle3, p21, -Angle3),
                p20
            };

            polygon.AddGeometry(ConstructRing(list5));
            polygon.Close();

            return polygon;
        }

        private IGeometry ConstructRing(IEnumerable<IPoint> points)
        {
            var r = new RingClass();

            foreach (var point in points)
            {
                r.AddPoint(point);
            }

            r.Close();

            return r;
        }

        private IPoint ConstructPoint(IPoint sourcePoint, double angle, double offsetAngle, double length)
        {
            return ConstructPoint(sourcePoint, angle, offsetAngle, length, GeometryWidth);
        }

        private IPoint ConstructPoint(IPoint sourcePoint, double angle, double offsetAngle, double length, double width)
        {
            var p = new PointClass();
            p.ConstructAngleDistance(sourcePoint, angle * Deg2Rad, width / 2);
            var p2 = new PointClass();
            p2.ConstructAngleDistance(p, offsetAngle * Deg2Rad, length);

            return p2;
        }

        private IPoint ConstructPoint2(IPoint p0, double angle0, IPoint p1, double angle1)
        {
            double angle1Rad = angle0 * Deg2Rad;
            double angle2Rad = angle1 * Deg2Rad;
            IConstructPoint constructionPoint = new PointClass();
            constructionPoint.ConstructAngleIntersection(p0, angle1Rad, p1, angle2Rad);

            return constructionPoint as IPoint;
        }

    }
}
