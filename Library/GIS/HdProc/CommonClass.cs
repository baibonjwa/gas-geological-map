///GIS 常用查询 更新操作类
///2014-8-1 zlj
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCommon;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace GIS.HdProc
{
    public class CommonClass
    {
        /// <summary>
        /// 根据图层索引获得图层
        /// </summary>
        /// <param name="activeView">当前视图窗口对象</param>
        /// <param name="layerIndex">图层序号</param>
        /// <returns></returns>
        public ESRI.ArcGIS.Carto.IFeatureLayer GetFeatureLayerFromLayerName(ESRI.ArcGIS.Carto.IActiveView activeView, System.String name)
        {
            if (activeView == null || name == "")
            {
                return null;
            }
            IFeatureLayer fealyr = null;
            ESRI.ArcGIS.Carto.IMap map = activeView.FocusMap;
            for (int i = 0; i < map.LayerCount; i++)
            {
                ICompositeLayer lyrs = map.get_Layer(i) as ICompositeLayer;
                if (lyrs == null) continue;
                for (int j = 0; j < lyrs.Count; j++)
                {
                    ILayer lyr = lyrs.get_Layer(j);
                    ICompositeLayer compositrlyr = lyr as ICompositeLayer;
                    if (compositrlyr != null)
                    {
                        for (int k = 0; k < compositrlyr.Count; k++)
                        {
                            ILayer lyr1 = compositrlyr.get_Layer(k);
                            if (lyr1.Name == name)
                            {
                                fealyr = lyr1 as IFeatureLayer;
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (lyr.Name == name)
                        {
                            fealyr = lyr as IFeatureLayer;
                            break;
                        }
                    }
                }
            }
            return fealyr;
        }

        /// <summary>
        /// 根据图层索引获得图层
        /// </summary>
        /// <param name="activeView">当前视图窗口对象</param>
        /// <param name="layerIndex">图层序号</param>
        /// <returns></returns>
        public ESRI.ArcGIS.Carto.IFeatureLayer GetFeatureLayerFromLayerIndexNumber(ESRI.ArcGIS.Carto.IActiveView activeView, System.Int32 layerIndex)
        {
            if (activeView == null || layerIndex < 0)
            {
                return null;
            }
            ESRI.ArcGIS.Carto.IMap map = activeView.FocusMap;
            if (layerIndex < map.LayerCount && map.get_Layer(layerIndex) is ESRI.ArcGIS.Carto.IFeatureLayer)
            {
                return (ESRI.ArcGIS.Carto.IFeatureLayer)activeView.FocusMap.get_Layer(layerIndex); // Explicit Cast
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据传入的字段与值的字典信息，返回sql查询语句
        /// </summary>
        /// <param name="fldvals">键值字典</param>
        /// <returns>sql查询语句</returns>
        public string CreateSqlexpression(IFields flds, Dictionary<string, string> fldvals)
        {
            string sql = "";
            int k = 0;
            foreach (string key in fldvals.Keys)
            {
                int fldindex = flds.FindField(key);
                IField fld = flds.get_Field(fldindex);
                esriFieldType type = fld.Type;
                if (k == 0)
                {
                    if (type == esriFieldType.esriFieldTypeString)
                    {
                        sql = "\"" + key + "\"='" + fldvals[key] + "'";
                    }
                    else if (type == esriFieldType.esriFieldTypeInteger)
                    {
                        sql = "\"" + key + "\"=" + fldvals[key];
                    }
                }
                else
                {
                    if (type == esriFieldType.esriFieldTypeString)
                    {
                        sql += " AND \"" + key + "\"='" + fldvals[key] + "'";
                    }
                    else if (type == esriFieldType.esriFieldTypeInteger)
                    {
                        sql += " AND \"" + key + "\"=" + fldvals[key];
                    }
                }
                k++;
            }
            return sql;
        }

        /// <summary>
        /// 查找要素
        /// </summary>
        /// <param name="layer">图层</param>
        /// <param name="fieldName">字段</param>
        /// <param name="attributeValue">查询条件</param>
        /// <returns>查询结果(图层名称，几何图形，属性信息)</returns>
        public List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> SearchFeaturesByGeoAndText(ILayer layer, Dictionary<string, string> fldvals)
        {
            IFeatureLayer featureLayer = (IFeatureLayer)layer;

            IFeatureClass feaclass = featureLayer.FeatureClass;
            if (feaclass == null)
            {
                Log.Error("[GIS]....The FeatureClass of the Feature layer " + featureLayer.Name + " is null, please check the geodatabase if the feature layer exists.");
                return null;
            }

            var featureList = new List<Tuple<IFeature, IGeometry, Dictionary<string, string>>>();
            var filter0 = new QueryFilterClass();
            IFields flds = feaclass.Fields;
            string sql = CreateSqlexpression(flds, fldvals);
            filter0.WhereClause = sql;
            IFeatureCursor featureCursor = feaclass.Search(filter0, false);
            if (feaclass.Fields.FindField(GIS.GIS_Const.FIELD_XH) != -1)
            {

                ITableSort tableSort = new TableSortClass();
                tableSort.Table = featureLayer.FeatureClass as ITable;
                tableSort.QueryFilter = filter0;
                //tableSort.Fields = GIS.GIS_Const.FIELD_XH;
                //tableSort.set_Ascending(GIS.GIS_Const.FIELD_XH, false);
                tableSort.Fields = GIS.GIS_Const.FIELD_OBJECTID;
                tableSort.set_Ascending(GIS.GIS_Const.FIELD_OBJECTID, false);
                tableSort.Sort(null);
                featureCursor = tableSort.Rows as IFeatureCursor;
            }
            if (featureCursor != null)
            {
                var feature = featureCursor.NextFeature();
                IGeometry featureGeometry = null;
                while (feature != null)
                {
                    var fieldDict = new Dictionary<string, string>();
                    for (int i = 0; i < feature.Fields.FieldCount; i++)
                    {
                        var field = feature.Fields.Field[i];
                        if (field.Type != esriFieldType.esriFieldTypeGeometry)
                        {
                            fieldDict.Add(field.Name, feature.Value[i].ToString());
                        }
                        else
                        {
                            featureGeometry = feature.Shape;
                        }
                    }
                    var featureTuple = new Tuple<IFeature, IGeometry, Dictionary<string, string>>(feature, featureGeometry, fieldDict);
                    featureList.Add(featureTuple);
                    feature = featureCursor.NextFeature();
                }
            }
            return featureList;
        }

        /// <summary>
        /// 由点坐标创建面对象
        /// </summary>
        /// <param name="pnts">点集合</param>
        /// <returns>返回的面对象</returns>
        public IPolygon CreatePolygonFromPnts(IList<IPoint> pnts, ISpatialReference spr)
        {
            IPolygon polygon = new PolygonClass();
            INewPolygonFeedback fac = new NewPolygonFeedbackClass();

            polygon.SpatialReference = spr;
            IPointCollection pntcols = polygon as IPointCollection;
            for (int i = 0; i < pnts.Count; i++)
            {
                pntcols.AddPoint(pnts[i]);
            }
            if (!polygon.IsClosed)
                polygon.Close();
            //polygon.Close();
            return polygon;
        }
        /// <summary>
        /// 查询地质构造
        /// </summary>
        /// <param name="pGeomety"></param>
        /// <param name="searchregion"></param>
        /// <returns></returns>
        public Dictionary<string, List<GeoStruct>> GetStructsInfos(IPoint pGeomety, List<int> hdids = null)
        {
            Dictionary<string, List<GeoStruct>> results = new Dictionary<string, List<GeoStruct>>();
            //峒室 1
            List<GeoStruct> dss = BufferAnalyse(pGeomety, Global.searchlen, Global.dslyr);
            results.Add("1", dss);
            //揭露断层 2
            List<GeoStruct> jls = BufferAnalyse(pGeomety, Global.searchlen, Global.jllyr);
            results.Add("2", jls);
            //推断断层 3
            List<GeoStruct> tds = BufferAnalyse(pGeomety, Global.searchlen, Global.tdlyr);
            results.Add("3", tds);
            //钻孔 4
            List<GeoStruct> zks = BufferAnalyse(pGeomety, Global.searchlen, Global.zklyr);
            results.Add("4", zks);
            //陷落柱 5
            List<GeoStruct> xlzs = BufferAnalyse(pGeomety, Global.searchlen, Global.xlzlyr);
            results.Add("5", xlzs);
            //陷落柱1 6
            List<GeoStruct> xlz1s = BufferAnalyse(pGeomety, Global.searchlen, Global.xlzlyr1);
            results.Add("6", xlz1s);
            //井筒 7
            List<GeoStruct> jts = BufferAnalyse(pGeomety, Global.searchlen, Global.jtlyr);
            results.Add("7", jts);
            //巷道层 8
            //List<GeoStruct> hds = BufferAnalyse(pGeomety, Global.searchlen, Global.hdfdfulllyr);
            //for (int i = 0; i < hds.Count; i++)
            //{
            //    GeoStruct geostruct = hds[i];
            //    if (hdids != null)
            //    {
            //        for (int j = 0; j < hdids.Count; j++)
            //        {
            //            if (geostruct.geoinfos[GIS_Const.FIELD_HdId].ToString() == hdids[j].ToString())
            //            {
            //                hds.Remove(geostruct);
            //                i--;
            //            }
            //        }
            //    }
            //}
            //results.Add("8", hds);
            return results;
        }

        /// <summary>
        /// 获得查询结果信息
        /// </summary>
        /// <param name="pGeometry">点</param>
        /// <param name="hdids">巷道ID</param>
        /// <param name="kind">表示巷道掘进还是回采掘进</param>
        /// <returns></returns>
        public Dictionary<string, List<GeoStruct>> GetStructsInfosNew(IPoint pGeometry, List<int> hdids = null, int kind = 1)
        {
            #region
            /*
             * 根据工作面中点坐标，查询对应的掘进面或者回采面的信息
             */
            List<IPoint> pgeompts = CalculateCirclePnts(pGeometry as IPoint, 1);
            IPolygon pgeom = Global.commonclss.CreatePolygonFromPnts(pgeompts, Global.spatialref);
            IFeatureCursor feahds = null;
            IFeature feahd = null;
            if (kind == 1)
            {
                feahds = SpatialSearch(pgeom, "1=1", Global.hdfdfulllyr, esriSpatialRelEnum.esriSpatialRelIntersects);//查询巷道分段
                feahd = feahds.NextFeature();
            }
            else
            {
                feahds = SpatialSearch(pgeom, "1=1", Global.hcqlyr, esriSpatialRelEnum.esriSpatialRelIntersects);//查询回采工作面
                feahd = feahds.NextFeature();
            }
            IGeometry geomsearch = feahd.Shape;
            IHitTest hitest = geomsearch as IHitTest;
            ESRI.ArcGIS.Geometry.IPoint hitPoint = new ESRI.ArcGIS.Geometry.PointClass();
            System.Double hitDistance = 0;
            System.Int32 hitPartIndex = 0;
            System.Int32 hitSegmentIndex = 0;
            System.Boolean rightSide = false;
            bool findgeom = hitest.HitTest(pGeometry, 1, esriGeometryHitPartType.esriGeometryPartBoundary, hitPoint, ref hitDistance, ref hitPartIndex, ref hitSegmentIndex, ref rightSide);
            IPolyline plingeom = new PolylineClass();
            if (findgeom == true)
            {
                IPolygon polygon = geomsearch as IPolygon;
                IPointCollection pnts = polygon as IPointCollection;

                IPoint pntstart = pnts.get_Point(hitSegmentIndex);
                IPoint pntend = pnts.get_Point(hitSegmentIndex + 1);
                plingeom.FromPoint = pntstart;
                plingeom.ToPoint = pntend;
            }
            #endregion
            Dictionary<string, List<GeoStruct>> results = new Dictionary<string, List<GeoStruct>>();
            //峒室 1
            List<GeoStruct> dss = BufferAnalyseNew(plingeom, Global.searchlen, Global.dslyr, geomsearch);
            results.Add("1", dss);
            //揭露断层 2
            List<GeoStruct> jls = BufferAnalyseNew(plingeom, Global.searchlen, Global.jllyr, geomsearch);
            results.Add("2", jls);
            //推断断层 3
            List<GeoStruct> tds = BufferAnalyseNew(plingeom, Global.searchlen, Global.tdlyr, geomsearch);
            results.Add("3", tds);
            //钻孔 4
            List<GeoStruct> zks = BufferAnalyseNew(plingeom, Global.searchlen, Global.zklyr, geomsearch);
            results.Add("4", zks);
            //陷落柱 5
            List<GeoStruct> xlzs = BufferAnalyseNew(plingeom, Global.searchlen, Global.xlzlyr, geomsearch);
            results.Add("5", xlzs);
            //陷落柱1 6
            List<GeoStruct> xlz1s = BufferAnalyseNew(plingeom, Global.searchlen, Global.xlzlyr1, geomsearch);
            results.Add("6", xlz1s);
            //井筒 7
            List<GeoStruct> jts = BufferAnalyseNew(plingeom, Global.searchlen, Global.jtlyr, geomsearch);
            results.Add("7", jts);
            //巷道层 8
            //List<GeoStruct> hds = BufferAnalyseNew(plingeom, Global.searchlen, Global.hdfdfulllyr, geomsearch);
            //for (int i = 0; i < hds.Count; i++)
            //{
            //    GeoStruct geostruct = hds[i];
            //    if (hdids != null)
            //    {
            //        for (int j = 0; j < hdids.Count; j++)
            //        {
            //            if (geostruct.geoinfos[GIS_Const.FIELD_HdId].ToString() == hdids[j].ToString())
            //            {
            //                hds.Remove(geostruct);
            //                i--;
            //            }
            //        }
            //    }
            //}
            //results.Add("8", hds);
            return results;
        }

        public Dictionary<string, List<IPoint>> getCoordinates(IPolygon polygon, IPolyline plin1, IPolyline plin2, IPolyline plin3, double hdwid1, double hdwid2)
        {
            Dictionary<string, List<IPoint>> res = new Dictionary<string, List<IPoint>>();
            IPointCollection pnts = polygon as IPointCollection;
            int index = 1;
            for (int i = 0; i < pnts.PointCount - 1; i++)
            {
                IPolyline plinnew = new PolylineClass();
                plinnew.SpatialReference = Global.spatialref;
                plinnew.FromPoint = new PointClass() { X = pnts.get_Point(i).X, Y = pnts.get_Point(i).Y };
                plinnew.ToPoint = new PointClass() { X = pnts.get_Point(i + 1).X, Y = pnts.get_Point(i + 1).Y };

                ITopologicalOperator top1 = plin1 as ITopologicalOperator;
                IGeometry geom1 = top1.Buffer(hdwid1);
                IRelationalOperator relationOp1 = geom1 as IRelationalOperator;
                bool bres1 = relationOp1.Contains(plinnew);
                if (bres1)
                    continue;

                ITopologicalOperator top2 = plin2 as ITopologicalOperator;
                IGeometry geom2 = top2.Buffer(hdwid2);
                IRelationalOperator relationOp2 = geom2 as IRelationalOperator;
                bool bres2 = relationOp2.Contains(plinnew);
                if (bres2)
                    continue;
                List<IPoint> curpnts = new List<IPoint>();
                curpnts.Add(pnts.get_Point(i));
                curpnts.Add(pnts.get_Point(i + 1));
                res.Add(index.ToString(), curpnts);

                index++;
            }
            //处理顺序
            IPoint pntCenter = new PointClass() { X = (plin3.Envelope.XMax + plin3.Envelope.XMin) / 2, Y = (plin3.Envelope.YMax + plin3.Envelope.YMin) / 2 };
            IPolyline plin0 = new PolylineClass();
            plin0.SpatialReference = Global.spatialref;
            plin0.FromPoint = res["1"][0];
            plin0.ToPoint = res["1"][1];
            double dist0 = 0.0, dist1 = 0.0;
            bool isrightside = false;
            IPoint pntquery = null;
            plin0.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pntCenter, false, pntquery, ref dist0, ref dist1, ref isrightside);

            IPolyline plin00 = new PolylineClass();
            plin00.SpatialReference = Global.spatialref;
            plin00.FromPoint = res["2"][0];
            plin00.ToPoint = res["2"][1];
            double dist00 = 0.0, dist10 = 0.0;
            bool isrightside0 = false;
            IPoint pntquery0 = null;
            plin00.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pntCenter, false, pntquery0, ref dist00, ref dist10, ref isrightside0);

            if (dist10 > dist1)
            {
                List<IPoint> tmppnts = res["2"];
                res["2"] = res["1"];
                res["1"] = tmppnts;
            }
            return res;
        }
        /// <summary>
        /// 创建点缓冲区查询 查询对应的地质构造信息
        /// </summary>
        /// <param name="pGeom">点</param>
        /// <param name="dist">缓冲距离</param>
        /// <param name="fealyr">缓冲图层</param>
        /// <returns></returns>
        public List<GeoStruct> BufferAnalyse(IPoint pGeom, double dist, IFeatureLayer fealyr)
        {
            List<GeoStruct> geoList = new List<GeoStruct>();

            ITopologicalOperator top = pGeom as ITopologicalOperator;
            IGeometry geom = top.Buffer(dist);
            IFeatureCursor searchCursor = SpatialSearch(geom, "1=1", fealyr);
            if (searchCursor != null)
            {
                IFeature fea = searchCursor.NextFeature();
                while (fea != null)
                {
                    GeoStruct geoTmp = new GeoStruct();
                    while (fea != null)
                    {
                        var fieldDict = new Dictionary<string, string>();
                        for (var i = 0; i < fea.Fields.FieldCount; i++)
                        {
                            var field = fea.Fields.Field[i];
                            if (field.Type != esriFieldType.esriFieldTypeGeometry)
                            {
                                fieldDict.Add(field.Name, fea.Value[i].ToString());
                            }
                            else
                            {
                                geoTmp.geo = fea.Shape;
                            }
                        }
                        geoTmp.geoinfos = fieldDict;

                        double distance = CalculateDistance(pGeom, geoTmp.geo);
                        geoTmp.dist = distance;

                        geoList.Add(geoTmp);
                        fea = searchCursor.NextFeature();
                    }
                }
            }
            return geoList;
        }

        public List<GeoStruct> BufferAnalyseNew(IPolyline pGeom, double dist, IFeatureLayer fealyr, IGeometry Reg = null)
        {
            List<GeoStruct> geoList = new List<GeoStruct>();

            ITopologicalOperator top = pGeom as ITopologicalOperator;
            IGeometry geom = top.Buffer(dist);
            IFeatureCursor searchCursor = SpatialSearch(geom, "1=1", fealyr);
            if (searchCursor != null)
            {
                IFeature fea = searchCursor.NextFeature();


                while (fea != null)
                {
                    GeoStruct geoTmp = new GeoStruct();

                    var fieldDict = new Dictionary<string, string>();
                    for (var i = 0; i < fea.Fields.FieldCount; i++)
                    {
                        var field = fea.Fields.Field[i];
                        if (field.Type != esriFieldType.esriFieldTypeGeometry)
                        {
                            fieldDict.Add(field.Name, fea.Value[i].ToString());
                        }
                        else
                        {
                            geoTmp.geo = fea.Shape;
                        }
                    }
                    geoTmp.geoinfos = fieldDict;
                    IRelationalOperator relation = Reg as IRelationalOperator;
                    bool bin = (relation.Overlaps(fea.Shape) || relation.Touches(fea.Shape) || relation.Contains(fea.Shape));
                    double distance = CalculateDistanceNew(pGeom, geoTmp.geo);
                    if (bin)
                        distance = 0.0;
                    if (distance < Global.searchlen)
                    {
                        geoTmp.dist = distance;
                        geoList.Add(geoTmp);
                    }
                    //geoTmp.dist = distance;
                    //geoList.Add(geoTmp);

                    fea = searchCursor.NextFeature();
                }

            }
            return geoList;
        }
        /// <summary>
        /// 空间属性查询
        /// </summary>
        /// <param name="geom">查询对象</param>
        /// <param name="sql">sql语句</param>
        /// <param name="featurelyr">图层</param>
        /// <returns>返回查询结果集合</returns>
        public IFeatureCursor SpatialSearch(IGeometry geom, string sql, IFeatureLayer featurelyr, esriSpatialRelEnum rel = esriSpatialRelEnum.esriSpatialRelEnvelopeIntersects)
        {
            IFeatureClass featureclass = featurelyr.FeatureClass;
            IFeatureCursor feacursor = null;
            if (featureclass != null)
            {
                ISpatialFilter filter = new SpatialFilter();
                filter.Geometry = geom;
                filter.WhereClause = sql;
                filter.SpatialRel = rel;
                feacursor = featureclass.Search(filter, false);
            }
            return feacursor;
        }

        /// <summary>
        /// 空间属性查询
        /// </summary>
        /// <param name="geom">查询对象</param>
        /// <param name="dics">属性值字典</param>
        /// <param name="featurelyr">图层</param>
        /// <returns>返回查询结果集合</returns>
        public IFeatureCursor PropertySearch(string sql, IFeatureLayer featurelyr)
        {
            IFeatureClass featureclass = featurelyr.FeatureClass;
            IFeatureCursor feacursor = null;
            if (featureclass != null)
            {
                IQueryFilter filter = new QueryFilterClass();
                filter.WhereClause = sql;
                feacursor = featureclass.Search(filter, false);
            }
            return feacursor;
        }


        /// <summary>
        /// 计算两点之间的空间距离
        /// </summary>
        /// <param name="pnt0">起点</param>
        /// <param name="pnt1">终点</param>
        /// <returns>返回距离</returns>
        public double CalculateDistance(IPoint pnt0, IGeometry geom)
        {
            double dist0 = 0.0, dist1 = 0.0;
            double distance = 0.0;
            IPoint outPoint = new PointClass();
            IPoint pnt1 = new PointClass();
            if (geom.GeometryType == esriGeometryType.esriGeometryPolygon)
            {
                IPolygon polygon = (IPolygon)geom;
                polygon.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pnt0, false, outPoint, ref dist0, ref dist1, false);
                distance = dist1;
            }
            else if (geom.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                IPolyline polyline = (IPolyline)geom;
                polyline.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pnt0, false, outPoint, ref dist0, ref dist1, false);
                distance = dist1;
            }
            else if (geom.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                pnt1 = (IPoint)geom;
                double xstep = pnt1.X - pnt0.X;
                double ystep = pnt1.Y - pnt0.Y;
                distance = Math.Sqrt(xstep * xstep + ystep * ystep);
            }
            return distance;
        }

        /// <summary>
        /// 计算两点之间的空间距离
        /// </summary>
        /// <param name="pnt0">起点</param>
        /// <param name="pnt1">终点</param>
        /// <returns>返回距离</returns>
        public double CalculateDistanceNew(IPolyline plin, IGeometry geom)
        {
            double dist0 = 0.0, dist1 = 0.0;
            double distance = 0.0;
            //IPoint outPoint = new PointClass();
            //IPoint pnt1 = new PointClass();
            //if (geom.GeometryType == esriGeometryType.esriGeometryPolygon)
            //{
            //    IPolygon polygon = (IPolygon)geom;
            //    polygon.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pnt0, false, outPoint, ref dist0, ref dist1, false);
            //    distance = dist1;
            //}
            //else if (geom.GeometryType == esriGeometryType.esriGeometryPolyline)
            //{
            //    IPolyline polyline = (IPolyline)geom;
            //    polyline.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pnt0, false, outPoint, ref dist0, ref dist1, false);
            //    distance = dist1;
            //}
            //else if (geom.GeometryType == esriGeometryType.esriGeometryPoint)
            //{

            //    pnt1 = (IPoint)geom;
            //    double xstep = pnt1.X - pnt0.X;
            //    double ystep = pnt1.Y - pnt0.Y;
            //    distance = Math.Sqrt(xstep * xstep + ystep * ystep);
            //}
            IProximityOperator proxi = plin as IProximityOperator;
            distance = proxi.ReturnDistance(geom);
            return distance;
        }
        /// <summary>
        /// 根据当前绘制的线段判断在中心线分段图层中与之相交的线段，
        /// 根据z值判断上下层关系，返回对应的Id属性信息
        /// </summary>
        /// <param name="plin">要绘制的中心线</param>
        /// <param name="centlyr">中心线图层</param>
        /// <param name="HdId">巷道Id</param>
        /// <returns>返回线对象的Id值，决定显示效果</returns>
        public int SearchHdByLine(IPolyline plin, string HdId, IFeatureLayer centlyr)
        {
            int ires = 0;
            string sql = "\"" + GIS_Const.FIELD_HDID + "\"<>'" + HdId + "'";
            IFeatureCursor crosslines = SpatialSearch(plin, sql, centlyr);
            if (crosslines != null)
            {
                int fldindex = centlyr.FeatureClass.FindField(GIS_Const.FIELD_ID);
                IFeature fea = crosslines.NextFeature();
                while (fea != null)
                {
                    int IdVal = Convert.ToInt16(fea.get_Value(fldindex));
                    IPolyline polyline = fea.Shape as IPolyline;

                    ITopologicalOperator toplogical = (ITopologicalOperator)plin;
                    IGeometry geo = toplogical.Intersect(polyline, esriGeometryDimension.esriGeometry0Dimension);
                    IMultipoint mulpoint = geo as IMultipoint;
                    IPointCollection pntcol = mulpoint as IPointCollection;
                    if (pntcol != null && pntcol.PointCount > 0)
                    {
                        // 交点
                        IPoint pntjd = pntcol.get_Point(0);
                        if (pntjd != null)
                        {
                            // 巷道高度判断
                            int res = CheckHdGd(plin, polyline, pntjd);
                            if (res == 0)//同一层
                                ires = 0;
                            else if (res == -1)//下一层
                                ires = -1;
                            else//在上一层
                                ires = 1;
                        }
                    }
                    fea = crosslines.NextFeature();
                }
            }
            return ires;
        }

        /// <summary>
        /// 根据巷道高度判断两条巷道的上下级关系
        /// </summary>
        /// <param name="pl0">线0</param>
        /// <param name="pl1">线1</param>
        /// <param name="jdpnt">交点</param>
        /// <returns>z值大小的比较 0相同 1在上 -1在下</returns>
        public int CheckHdGd(IPolyline pl0, IPolyline pl1, IPoint jdpnt)
        {
            int ires = 0;
            double xstep0 = Math.Abs(pl0.ToPoint.X - pl0.FromPoint.X);
            double zstep0 = Math.Abs(pl0.ToPoint.Z - pl0.FromPoint.Z);
            double hd0 = zstep0 / xstep0 * Math.Abs((jdpnt.X - pl0.FromPoint.X));

            double xstep1 = Math.Abs(pl1.ToPoint.X - pl1.FromPoint.X);
            double zstep1 = Math.Abs(pl1.ToPoint.Z - pl1.FromPoint.Z);
            double hd1 = zstep1 / xstep1 * Math.Abs((jdpnt.X - pl1.FromPoint.X));

            double hddeta = hd1 - hd0;
            double detah = Math.Abs(hddeta);
            if (hddeta > 0)//pl1在上，pl0在下
            {
                if (detah > Global.sxjl)
                    ires = 1;
                else
                    ires = 0;
            }
            else//pl在下，pl0在上
            {
                if (detah > Global.sxjl)
                    ires = -1;
                else
                    ires = 0;
            }
            return ires;
        }

        /// <summary>
        /// 删除几何对象
        /// </summary>
        /// <param name="lyr">图层</param>
        /// <param name="fea">要素对象</param>
        /// <param name="attributes">属性集合</param>
        /// <param name="pGeo">空间对象</param>
        /// <returns>成功失败标志</returns>
        public void DelFeature(IFeatureLayer lyr, IFeature fea)
        {
            try
            {
                IFeatureClass Featureclass = lyr.FeatureClass;
                if (Featureclass != null)
                {
                    IWorkspaceEdit workspace = (IWorkspaceEdit)(Featureclass as IDataset).Workspace;
                    if (fea != null)
                    {
                        workspace.StartEditing(false);
                        workspace.StartEditOperation();

                        fea.Delete();
                        workspace.StopEditOperation();
                        workspace.StopEditing(true);
                    }
                }
            }
            catch (Exception ei)
            {
                Log.Debug("[GIS] Del Feature1: " + ei.ToString());
            }
        }
        /// <summary>
        /// 根据查询语句从ITable中删除对应的记录信息
        /// </summary>
        /// <param name="lyr">图层</param>
        /// <param name="sql">查询语句</param>
        public void DelFeaturesByQueryFilter(IFeatureLayer lyr, string sql)
        {
            IFeatureClass Featureclass = lyr.FeatureClass;
            IWorkspaceEdit workspace = (IWorkspaceEdit)(Featureclass as IDataset).Workspace;
            workspace.StartEditing(false);
            workspace.StartEditOperation();

            //删除要素
            IQueryFilter qfilter = new QueryFilterClass();
            qfilter.WhereClause = sql;
            ITable table = Featureclass as ITable;
            table.DeleteSearchedRows(qfilter);

            //停止编辑

            try
            {
                workspace.StopEditOperation();
                workspace.StopEditing(true);

            }
            catch (Exception ei)
            {
                throw;
            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(qfilter);
            Global.pActiveView.Refresh();
        }
        /// <summary>
        /// 删除多个几何对象
        /// </summary>
        /// <param name="lyr">图层</param>
        /// <param name="fea">要素对象</param>
        /// <param name="attributes">属性集合</param>
        /// <param name="pGeo">空间对象</param>
        /// <returns>成功失败标志</returns>
        public void DelFeatures(IFeatureLayer lyr, string sql)
        {
            try
            {
                IFeatureClass Featureclass = lyr.FeatureClass;
                if (Featureclass != null)
                {

                    IFeatureCursor featurecursor = PropertySearch(sql, lyr);
                    IWorkspaceEdit workspace = (IWorkspaceEdit)(Featureclass as IDataset).Workspace;
                    workspace.StartEditing(false);
                    workspace.StartEditOperation();
                    IFeature fea = featurecursor.NextFeature();
                    while (fea != null)
                    {
                        fea.Delete();
                        fea = featurecursor.NextFeature();
                    }
                    workspace.StopEditOperation();
                    workspace.StopEditing(true);
                }
            }
            catch (Exception ei)
            {
                Log.Debug("[GIS] Del Feature: " + ei.ToString());
            }
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="lyr">图层</param>
        /// <param name="fea">要素</param>
        /// <param name="pGeo">几何对象</param>
        /// <returns></returns>
        public bool UpdateFeature(IFeatureLayer lyr, IFeature fea, IGeometry pGeo)
        {
            bool bres = false;
            try
            {
                IFeatureClass Featureclass = lyr.FeatureClass;
                IWorkspaceEdit workspace = (IWorkspaceEdit)(Featureclass as IDataset).Workspace;
                workspace.StartEditing(false);
                workspace.StartEditOperation();
                fea.Shape = pGeo;
                workspace.StopEditOperation();
                workspace.StopEditing(true);
                bres = true;
            }
            catch (Exception ei)
            {
                bres = false;
            }
            return bres;
        }

        /// <summary>
        /// 更新导线点图层
        /// </summary>
        /// <param name="sql">更新查询语句</param>
        /// <param name="pgeo">几何对象</param>
        /// <param name="flyr">要查询更新的要素图层对象</param>
        /// <returns></returns>
        public bool UpdateFeature(string sql, IGeometry pgeo, IFeatureLayer flyr)
        {
            IFeatureClass fclss = flyr.FeatureClass;
            IWorkspaceEdit wks = (fclss as IDataset).Workspace as IWorkspaceEdit;
            wks.StartEditing(false);
            wks.StartEditOperation();
            IQueryFilter queryfilter = new QueryFilterClass();
            queryfilter.WhereClause = sql;
            //更新中心线分段信息
            IFeatureCursor dxdptcursor = fclss.Update(queryfilter, true);
            IFeature currentdxd = dxdptcursor.NextFeature();
            if (currentdxd != null)
            {
                currentdxd.Shape = pgeo;
                dxdptcursor.UpdateFeature(currentdxd);
            }
            wks.StopEditOperation();
            wks.StopEditing(true);
            return true;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="lyr">图层</param>
        /// <param name="fea">要素对象</param>
        /// <param name="attributes">属性集合</param>
        /// <param name="pGeo">空间对象</param>
        /// <returns>成功失败标志</returns>
        public bool UpdateFeature(IFeatureLayer lyr, IFeature fea, Dictionary<string, string> attributes, IGeometry pGeo)
        {
            bool bres = false;
            try
            {
                IFeatureClass Featureclass = lyr.FeatureClass;
                IWorkspaceEdit workspace = (IWorkspaceEdit)(Featureclass as IDataset).Workspace;
                workspace.StartEditing(false);
                workspace.StartEditOperation();
                int index = Featureclass.Fields.FindField(GIS_Const.FIELD_SHAPE);
                IGeometryDef geometryDef = fea.Fields.get_Field(index).GeometryDef as IGeometryDef;
                if (geometryDef.HasZ)
                {
                    IZAware pZAware = (IZAware)pGeo;
                    pZAware.ZAware = true;
                    fea.Shape = pGeo;
                    foreach (string key in attributes.Keys)
                    {
                        int findex = fea.Fields.FindField(key);
                        if (findex != -1)
                        {
                            fea.set_Value(findex, attributes[key]);
                        }
                    }
                    fea.Store();
                }
                else
                {
                    fea.Shape = pGeo;
                    foreach (string key in attributes.Keys)
                    {
                        int findex = fea.Fields.FindField(key);
                        if (findex != -1)
                        {
                            fea.set_Value(findex, attributes[key]);
                        }
                    }
                    fea.Store();
                }
                workspace.StopEditOperation();
                workspace.StopEditing(true);
                bres = true;
            }
            catch (Exception ei)
            {
                Log.Debug("[GIS] Update Feature: " + ei.ToString());
            }
            return bres;
        }

        /// 合并列表中所有多边形
        /// </summary>
        /// <param name="orifeature">保留源对象</param>
        /// <param name="geolist">图形列表</param>
        /// <returns></returns>
        public void CreatePolygonFromExistingGeometries(IFeatureLayer lyr, IFeature orifeature, List<IGeometry> geolist)
        {
            int i = 0;
            IGeometry geometry = null;
            IFeatureClass Featureclass = lyr.FeatureClass;
            IWorkspaceEdit workspace = (IWorkspaceEdit)(Featureclass as IDataset).Workspace;
            workspace.StartEditing(false);
            workspace.StartEditOperation();
            //合并图形
            ITopologicalOperator2 topologicalOperator2 = orifeature.ShapeCopy as ITopologicalOperator2;
            for (i = 0; i < geolist.Count; i++)
            {
                IGeometry geo = geolist[i];
                if (geometry != null)
                {
                    topologicalOperator2 = geometry as ITopologicalOperator2;
                }
                ITopologicalOperator opertor = geo as ITopologicalOperator;
                opertor.Simplify();
                topologicalOperator2.IsKnownSimple_2 = false;
                topologicalOperator2.Simplify();
                geometry = topologicalOperator2.Union(geo);
            }
            //更新图形对象
            IGeometry geoCombined = (IGeometry)geometry;
            orifeature.Shape = geoCombined as IGeometry;
            orifeature.Store();

            workspace.StopEditOperation();
            workspace.StopEditing(true);
        }
        /// <summary>
        /// 地图跳转到指定对象
        /// </summary>
        /// <param name="geom"></param>
        public void JumpToGeometry(IGeometry geom)
        {
            if (geom != null)
            {
                IEnvelope env = geom.Envelope;
                Global.pActiveView.Extent = env;
            }
        }
        //计算圆周上的点集合
        public List<IPoint> CalculateCirclePnts(IPoint center, double rad)
        {
            List<IPoint> pts = new List<IPoint>();
            for (int i = 0; i < 360; i++)
            {
                double deta = 2 * Math.PI / 360.0;
                double curang = deta * i;
                double x = center.X + rad * Math.Cos(curang);
                double y = center.Y + rad * Math.Sin(curang);
                IPoint ptemp = new PointClass();
                ptemp.PutCoords(x, y);
                pts.Add(ptemp);
            }
            pts.Add(new PointClass() { X = center.X + rad, Y = center.Y });
            return pts;
        }
        /// <summary>
        /// 构造圆形对象
        /// </summary>
        /// <param name="point"></param>
        /// <param name="radius"></param>
        /// <param name="isCCW"></param>
        /// <returns></returns>
        public ICircularArc CreateCircleArc(IPoint point, double radius, bool isCCW)
        {
            ICircularArc circularArc = new CircularArcClass();
            IConstructCircularArc construtionCircularArc = circularArc as IConstructCircularArc;
            construtionCircularArc.ConstructCircle(point, radius, isCCW);
            return circularArc;
        }
        /// <summary>
        /// 判断点在切眼巷道的哪侧
        /// </summary>
        /// <param name="qyhd">切眼巷道</param>
        /// <param name="pntcenter">回采面中点坐标</param>
        /// <returns></returns>
        public int GetDirectionByPnt(IPolyline qyhd, IPoint pntcenter)
        {
            int flag = 0;
            double ax = qyhd.ToPoint.X - qyhd.FromPoint.X;
            double ay = qyhd.ToPoint.Y - qyhd.FromPoint.Y;
            double bx = pntcenter.X - qyhd.FromPoint.X;
            double by = pntcenter.Y - qyhd.FromPoint.Y;
            if (ax * by - ay * bx > 0)
                flag = -1;
            if (ax * by - ay * bx < 0)
                flag = 1;
            return flag;
        }
    }

    /// <summary>
    /// 返回的地质构造类
    /// </summary>
    public class GeoStruct
    {
        public Dictionary<string, string> geoinfos;//地质构造的属性结构
        public double dist;//距离判断点的距离
        public IGeometry geo;//地质构造的空间信息
    }
}
