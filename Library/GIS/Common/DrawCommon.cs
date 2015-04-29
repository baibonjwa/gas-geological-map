using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesGDB;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesRaster;

namespace GIS.Common
{
    public class DrawCommon
    {
        /// <summary>
        /// Geometry中Z值和M值处理
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="geometry"></param>
        /// <param name="zValue"></param>
        public static void HandleZMValue(IFeature feature, IGeometry geometry)
        {
            //先判断图层要素是否有Z值
            int index;
            index = feature.Fields.FindField(GIS_Const.FIELD_SHAPE);
            IGeometryDef pGeometryDef;
            pGeometryDef = feature.Fields.get_Field(index).GeometryDef as IGeometryDef;

            IPointCollection pPointCollection = geometry as IPointCollection;
            if (pGeometryDef.HasZ)
            {
                IZAware pZAware = (IZAware)geometry;
                pZAware.ZAware = true;
                if (geometry.Envelope.ZMax.ToString() == "非数字" || geometry.Envelope.ZMax.ToString() == "NaN")
                {
                    IZ iz1 = (IZ)geometry;
                    iz1.SetConstantZ(0);  //将Z值设置为0
                }
                //IPoint point = (IPoint)geometry;
                //point.Z = 0;
            }
            else
            {
                IZAware pZAware = (IZAware)geometry;
                pZAware.ZAware = false;
            }

            if (pGeometryDef.HasM)
            {
                IMAware pMAware = (IMAware)geometry;
                pMAware.MAware = true;
            }
            else
            {
                IMAware pMAware = (IMAware)geometry;
                pMAware.MAware = false;
            }

        }

        /// <summary>
        /// Geometry中Z值和M值处理
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="geometry"></param>
        /// <param name="zValue"></param>
        public static void HandleZMValue(IFeature feature, IGeometry geometry, double zValue)
        {
            //先判断图层要素是否有Z值
            int index;
            index = feature.Fields.FindField(GIS_Const.FIELD_SHAPE);
            IGeometryDef pGeometryDef;
            pGeometryDef = feature.Fields.get_Field(index).GeometryDef as IGeometryDef;

            IPointCollection pPointCollection = geometry as IPointCollection;
            if (pGeometryDef.HasZ)
            {
                IZAware pZAware = (IZAware)geometry;
                pZAware.ZAware = true;

                if (geometry.GeometryType == esriGeometryType.esriGeometryPoint)
                {
                    IPoint point = (IPoint)geometry;
                    point.Z = zValue;
                }
                else
                {
                    IZ iz1 = (IZ)geometry;
                    iz1.SetConstantZ(zValue);  //将Z值设置为zValue
                }
            }
            else
            {
                IZAware pZAware = (IZAware)geometry;
                pZAware.ZAware = false;
            }

            if (pGeometryDef.HasM)
            {
                IMAware pMAware = (IMAware)geometry;
                pMAware.MAware = true;
            }
            else
            {
                IMAware pMAware = (IMAware)geometry;
                pMAware.MAware = false;
            }

        }
    }
}
