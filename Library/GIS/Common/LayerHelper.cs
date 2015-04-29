using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using ESRI.ArcGIS.Geometry;


namespace GIS.Common
{
    public class LayerHelper
    {
        public static string GetClassOwnerName(string pDSName)
        {
            int index = pDSName.IndexOf(".");
            if (index >= 0)
            {
                pDSName = pDSName.Substring(0, index);
            }
            else
            {
                pDSName = "";
            }
            pDSName = pDSName.ToUpper();
            return pDSName;
        }
        public static string GetClassShortName(IDataset paramDS)
        {
            if (paramDS == null)
            {
                return "";
            }
            return GetClassShortName(paramDS.Name.ToUpper());
        }

        public static string GetClassShortName(IFeatureClass fc)
        {
            try
            {
                string str = "";
                str = (fc as IDataset).Name;
                int num = str.LastIndexOf(".");
                if (num >= 0)
                {
                    str = str.Substring(num + 1);
                }
                return str;
            }
            catch
            {
                return "";
            }
        }
        public static string GetClassShortName(string paramName)
        {
            string str = paramName;
            int num = paramName.LastIndexOf(".");
            if (num >= 0)
            {
                str = paramName.Substring(num + 1);
            }
            return str;
        }
        public static ArrayList GetIntersectFeature(IGeoFeatureLayer pLayer, IGeometry pGeom)
        {
            ArrayList list = new ArrayList();
            if (pLayer != null)
            {
                if ((pGeom == null) || pGeom.IsEmpty)
                {
                    return list;
                }
                ISpatialReference reference = (pLayer.FeatureClass as IGeoDataset).SpatialReference;
                if (reference != null)
                {
                    pGeom.SpatialReference = reference;
                    pGeom.SnapToSpatialReference();
                }
                ISpatialFilter filter = new SpatialFilterClass();
                filter.Geometry = pGeom;
                filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                IFeatureCursor o = pLayer.FeatureClass.Search(filter, false);
                for (IFeature feature = o.NextFeature(); feature != null; feature = o.NextFeature())
                {
                    list.Add(feature);
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
            }
            return list;
        }

        public static ArrayList GetIntersectFeature(IGeoFeatureLayer pLayer, IPolygon pPoly)
        {
            return GetIntersectFeature(pLayer, (IGeometry)pPoly);
        }

        public static List<IFeature> GetIntersectFeature(IFeatureClass pClass, IGeometry pGeom)
        {
            List<IFeature> list = new List<IFeature>();
            if (pClass != null)
            {
                if ((pGeom == null) || pGeom.IsEmpty)
                {
                    return list;
                }
                ISpatialReference reference = (pClass as IGeoDataset).SpatialReference;
                if (reference != null)
                {
                    pGeom.SpatialReference = reference;
                    pGeom.SnapToSpatialReference();
                }
                ISpatialFilter filter = new SpatialFilterClass();
                filter.Geometry = pGeom;
                filter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                IFeatureCursor o = pClass.Search(filter, false);
                for (IFeature feature = o.NextFeature(); feature != null; feature = o.NextFeature())
                {
                    list.Add(feature);
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
            }
            return list;
        }
        public static string GetLayerAliasName(IMap pMap, string sLayerName)
        {
            return QueryLayerByModelName(pMap, sLayerName).FeatureClass.AliasName;
        }

        public static int IndexOfLayer(IMap paramMap, ILayer paramLayer)
        {
            int num2 = paramMap.LayerCount;
            for (int i = 0; i < num2; i++)
            {
                if (paramMap.get_Layer(i) == paramLayer)
                {
                    return i;
                }
            }
            return -1;
        }
        public static int IndexOfLayer(IMap pMap, string sAliasName)
        {
            if (pMap != null)
            {
                if (sAliasName == "")
                {
                    return -1;
                }
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    if (pMap.get_Layer(i).Name == sAliasName)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        public static string LayerTypeName(ILayer paramLayer)
        {
            string str = "";
            if (paramLayer is IGeoFeatureLayer)
            {
                IFeatureClass class2 = (paramLayer as IGeoFeatureLayer).FeatureClass;
                if (class2 != null)
                {
                    //str = GeometryHelper.ShapeTypeName(class2.ShapeType);
                }
                return str;
            }
            if (paramLayer is IRasterLayer)
            {
                return "影像";
            }
            if (paramLayer is ITopologyLayer)
            {
                return "拓扑";
            }
            if (paramLayer is IAnnotationLayer)
            {
                str = "注记";
            }
            return str;
        }
        public static void LoadLayerFromStream(ILayer paramLayer, byte[] paramLayerContent)
        {
            if (((paramLayer != null) && (paramLayerContent != null)) && (paramLayerContent.Length != 0))
            {
                IPersistStream stream = paramLayer as IPersistStream;
                XMLStreamClass class2 = new XMLStreamClass();
                class2.LoadFromBytes(ref paramLayerContent);
                stream.Load(class2);
            }
        }
        public static ILayer QueryLayerByDisplayName(IMap paramMap, string layerName)
        {
            int num = paramMap.LayerCount;
            layerName = layerName.ToUpper();
            for (int i = 0; i < num; i++)
            {
                ILayer layer = paramMap.get_Layer(i);
                if (layer.Name.ToUpper().Equals(layerName))
                {
                    return layer;
                }
            }
            return null;
        }
        public static IGeoFeatureLayer QueryLayerByModelName(IMap paramMap, string paramModelName)
        {
            if (paramMap != null)
            {
                if (paramModelName == null)
                {
                    return null;
                }
                int num = paramMap.LayerCount;
                paramModelName = paramModelName.ToUpper();
                for (int i = 0; i < num; i++)
                {
                    ILayer layer = paramMap.get_Layer(i);
                    if ((layer is IGeoFeatureLayer) && GetClassShortName((layer as IGeoFeatureLayer).FeatureClass as IDataset).ToUpper().Equals(paramModelName))
                    {
                        return (layer as IGeoFeatureLayer);
                    }
                }
            }
            return null;
        }
        public static string QueryLayerModelName(ILayer curLayer)
        {
            if (curLayer == null)
            {
                return "";
            }
            if (curLayer is IGeoFeatureLayer)
            {
                return GetClassShortName((curLayer as IGeoFeatureLayer).FeatureClass as IDataset);
            }
            return curLayer.Name;
        }
        public static double QueryXYUnit(IGeoFeatureLayer paramLayer)
        {
            if (paramLayer != null)
            {
                return QueryXYUnit(paramLayer.FeatureClass);
            }
            return double.NaN;
        }
        public static double QueryXYUnit(IFeatureClass paramClass)
        {
            IGeoDataset dataset = paramClass as IGeoDataset;
            if (dataset != null)
            {
                ISpatialReference reference = dataset.SpatialReference;
                if (reference != null)
                {
                    double num = 0.0;
                    double num2 = 0.0;
                    double num3 = 0.0;
                    reference.GetFalseOriginAndUnits(out  num, out num2, out num3);
                    if (num3 != 0.0)
                    {
                        return (1.0 / num3);
                    }
                }
            }
            return double.NaN;
        }
        public static byte[] SaveLayerToStream(ILayer paramLayer)
        {
            byte[] buffer = null;
            if (paramLayer is IPersistStream)
            {
                IPersistStream stream = paramLayer as IPersistStream;
                XMLStreamClass class2 = new XMLStreamClass();
                stream.Save(class2, 0);
                buffer = class2.SaveToBytes();
            }
            return buffer;
        }

        /// <summary>
        /// 获得一个shapefile文件的的要素类对象
        /// </summary>
        /// <param name="sFilePath"></param>
        /// <returns></returns>
        public static IFeatureClass GetShapefileWorkspaceFeatureClass(string sFilePath)
        {
            try
            {
                IWorkspaceFactory pWSF = new ESRI.ArcGIS.DataSourcesFile.ShapefileWorkspaceFactoryClass();

                string sPath = System.IO.Path.GetDirectoryName(sFilePath);
                IWorkspace pWS = pWSF.OpenFromFile(sPath, 0);

                string sFileName = System.IO.Path.GetFileNameWithoutExtension(sFilePath);

                IFeatureWorkspace pFWS = pWS as IFeatureWorkspace;

                IFeatureClass pFC = pFWS.OpenFeatureClass(sFileName);

                return pFC;
            }
            catch (Exception ex) { return null; }
        }


        public static IFeatureClassName GetFeatureClassName(IFeatureClass pfc)
        {
            IDataset pDS = pfc as IDataset;

            IWorkspaceName workspaceName = new WorkspaceNameClass();
            workspaceName = pDS.FullName as IWorkspaceName;

            IFeatureClassName featureClassName = new FeatureClassNameClass();
            IDatasetName datasetName = (IDatasetName)featureClassName;
            datasetName.Name = pDS.Name;
            datasetName.WorkspaceName = workspaceName;
            return featureClassName;
        }

        /// <summary>
        /// 根据图层名获得图层
        /// </summary>
        /// <param name="map">Map</param>
        /// <param name="lyrname">图层名</param>
        /// <returns>要素图层</returns>
        public static IFeatureLayer GetLayerByName(IMap map, string lyrname)
        {
            try
            {
                UID puid = new UID();
                puid.Value = GIS_Const.STR_IFeatureLayer; //"{40A9E885-5533-11d0-98BE-00805F7CED21}";//IFeatureLayer
                IEnumLayer enumLayer = map.get_Layers(puid, true);
                enumLayer.Reset();
                ILayer player;
                player = enumLayer.Next();
                IFeatureLayer featureLayer = new FeatureLayerClass();

                while (player != null)
                {
                    featureLayer = player as IFeatureLayer;
                    string layerName = featureLayer.Name;
                    if (layerName == lyrname)
                    {
                        return featureLayer;
                        //break;
                    }
                    player = enumLayer.Next();
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
