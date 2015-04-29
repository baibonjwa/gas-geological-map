using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Analyst3DTools;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.esriSystem;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.GeoAnalyst;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;

namespace GIS.SpecialGraphic
{
    public class MyPt
    {
        public MyPt(double _x,double _y,double _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }
        public MyPt(){}
        public double x;
        public double y;
        public double z;
    }
   public class DrawContours
    {
       //离散点加密
       static public void DescretePointJM(string pathIn)
       {
           //27202 19104;27202 22947;31550 22947;31550 19104
           List <MyPt> listpt= new List<MyPt>();
           string[] line=System.IO.File.ReadAllLines(pathIn);
           for (int i = 0; i < line.Length; i++)
           {
               MyPt mypt= new MyPt();
               mypt.x = double.Parse(line[i].Split(',')[0]);
               mypt.y = double.Parse(line[i].Split(',')[1]);
               mypt.z = double.Parse(line[i].Split(',')[2]);
               listpt.Add(mypt);
           }
           for (int i = 0; i < listpt.Count; i++)
           {
               for (int j = 0; j < listpt.Count; j++)
               {
                   double x=(listpt[i].x + listpt[j].x)/2;
                   double y = (listpt[i].y + listpt[j].y)/2;
                   double z = (listpt[i].z + listpt[j].z) / 2;

                   
               }
           }
       }
       public static void Mlayer_Krige_Click()
       {
           // 用克里金Krige插值生成的栅格图像。如下：
           IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactory();
           string pPath = Application.StartupPath + @"\MakeContours\Cont.shp";
           string pFolder = System.IO.Path.GetDirectoryName(pPath);
           string pFileName = System.IO.Path.GetFileName(pPath);
           IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(pFolder, 0);
           IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;
           IFeatureClass oFeatureClass = pFeatureWorkspace.OpenFeatureClass(pFileName);
           IFeatureClassDescriptor pFCDescriptor = new FeatureClassDescriptorClass();
           pFCDescriptor.Create(oFeatureClass, null, "shape.z");

           IInterpolationOp pInterpolationOp = new RasterInterpolationOpClass();
           IRasterAnalysisEnvironment pEnv = pInterpolationOp as IRasterAnalysisEnvironment;

           object Cellsize = 0.004;//Cell size for output raster;0.004
           pEnv.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, ref Cellsize);
           //设置输出范围
           //27202 19104;27202 22947;31550 22947;31550 19104
           object snapRasterData = Type.Missing;
           IEnvelope pExtent;
           pExtent = new EnvelopeClass();
           Double xmin = 27202;
           Double xmax = 31550;

           Double ymin = 19104;
           Double ymax = 22947;
           pExtent.PutCoords(xmin, ymin, xmax, ymax);
           object extentProvider = pExtent;
           pEnv.SetExtent(esriRasterEnvSettingEnum.esriRasterEnvValue, ref extentProvider, ref snapRasterData);
           Double dSearchD = 10;
           object pSearchCount = 3;
           object missing = Type.Missing;
           IRasterRadius pRadius = new RasterRadius();
           pRadius.SetFixed(dSearchD, ref pSearchCount);
           //pRadius.SetVariable((int)pSearchCount, ref dSearchD);

           IGeoDataset poutGeoDataset = pInterpolationOp.Krige((IGeoDataset)pFCDescriptor, esriGeoAnalysisSemiVariogramEnum.esriGeoAnalysisGaussianSemiVariogram, pRadius, false, ref missing);

           IRaster pOutRaster = poutGeoDataset as IRaster;
           IRasterLayer pOutRasLayer = new RasterLayer();
           pOutRasLayer.CreateFromRaster(pOutRaster);

           IMap pMap = Common.DataEditCommon.g_pMap;
           pMap.AddLayer(pOutRasLayer);
           Common.DataEditCommon.g_axTocControl.Refresh();
           Common.DataEditCommon.g_pAxMapControl.ActiveView.Refresh();
       }

       public static void Mlayer_IDW_Click()
       {
           // 用反距离IDW插值生成的栅格图像。如下：
           IInterpolationOp pInterpolationOp = new RasterInterpolationOpClass();

           IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactory();
           string pPath = Application.StartupPath + @"\MakeContours\Cont.shp";
           string pFolder = System.IO.Path.GetDirectoryName(pPath);
           string pFileName = System.IO.Path.GetFileName(pPath);

           IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(pFolder, 0);

           IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;
           IFeatureClass oFeatureClass = pFeatureWorkspace.OpenFeatureClass(pFileName);
           IFeatureClassDescriptor pFCDescriptor = new FeatureClassDescriptorClass();
           pFCDescriptor.Create(oFeatureClass, null, "shape.z");
           IRasterRadius pRadius = new RasterRadiusClass();

           object objectMaxDistance = null;
           object objectbarrier = null;
           object missing = Type.Missing;
           pRadius.SetVariable(12, ref objectMaxDistance);

           object dCellSize =1;
           object snapRasterData = Type.Missing;
           IEnvelope pExtent;
           pExtent = new EnvelopeClass();
           Double xmin = 27202;
           Double xmax = 31550;

           Double ymin = 19104;
           Double ymax = 22947;
           pExtent.PutCoords(xmin, ymin, xmax, ymax);
           object extentProvider = pExtent;
           IRasterAnalysisEnvironment pEnv = pInterpolationOp as IRasterAnalysisEnvironment;
           pEnv.SetCellSize(esriRasterEnvSettingEnum.esriRasterEnvValue, ref dCellSize);
           pEnv.SetExtent(esriRasterEnvSettingEnum.esriRasterEnvValue, ref extentProvider, ref snapRasterData);
           IGeoDataset poutGeoDataset = pInterpolationOp.IDW((IGeoDataset)pFCDescriptor, 2, pRadius, ref objectbarrier);
           ISurfaceOp surOp = new RasterSurfaceOpClass();


           IRaster pOutRaster = poutGeoDataset as IRaster;

           IRasterLayer pOutRasLayer = new RasterLayer();
           pOutRasLayer.CreateFromRaster(pOutRaster);
           
           IMap pMap = Common.DataEditCommon.g_pMap;
           pMap.AddLayer(pOutRasLayer);
           Common.DataEditCommon.g_axTocControl.Refresh();
           Common.DataEditCommon.g_pAxMapControl.ActiveView.Refresh();

       }


























        //将离散点数据转换为FeatureClass
        static public bool ConvertASCIIDescretePoint2FeatureClass(Geoprocessor gp, string pathIn, string pathOut)
        {
            //Mlayer_Krige_Click();
            //Mlayer_IDW_Click();
            ASCII3DToFeatureClass asc2FeatureCls = new ASCII3DToFeatureClass();            
            asc2FeatureCls.input = pathIn;
            asc2FeatureCls.out_feature_class = pathOut;
            asc2FeatureCls.in_file_type = "XYZ";
            //asc2FeatureCls.out_geometry_type = "MULTIPOINT";
            //asc2FeatureCls.average_point_spacing = 10;
            asc2FeatureCls.out_geometry_type = "POINT";

            asc2FeatureCls.z_factor = 1;
            return RunTool(gp, asc2FeatureCls, null);
        }

        //将FeatureClass数据转换为Raster数据
        static public void ConvertFeatureCls2Raster(Geoprocessor gp, string pathIn, string pathOut)
        {
            ESRI.ArcGIS.ConversionTools.FeatureToRaster feature2raster = new ESRI.ArcGIS.ConversionTools.FeatureToRaster();
            feature2raster.in_features = pathIn;
            feature2raster.out_raster = pathOut;
            feature2raster.field = "Shape";
            RunTool(gp, feature2raster, null);
        }

        //用克里格插值到Raster默认方法
        static public void Interpolate2RasterKriging(Geoprocessor gp, string pathIn, string pathOut)
        {
            ESRI.ArcGIS.Analyst3DTools.Kriging raster = new ESRI.ArcGIS.Analyst3DTools.Kriging();
            raster.in_point_features = pathIn;
            raster.out_surface_raster = pathOut;
            raster.semiVariogram_props = "SPHERICAL";
            raster.z_field = "Shape";
            raster.cell_size = 1;
            RunTool(gp, raster, null);
        }


        //用克里格插值到Raster选择参数
        static public bool Interpolate2RasterKriging(Geoprocessor gp, string pathIn, string pathOut, string semiVariogramProp, string searchRadiusProp)
        {
            if (semiVariogramProp == "球模型")
            {
                semiVariogramProp = "Spherical";
            }
            if (semiVariogramProp == "圆模型")
            {
                semiVariogramProp = "Circular";
            }
            if (semiVariogramProp == "指数模型")
            {
                semiVariogramProp = "Exponential";
            }
            if (semiVariogramProp == "高斯模型")
            {
                semiVariogramProp = "Gaussian";
            }
            if (semiVariogramProp == "线模型")
            {
                semiVariogramProp = "Linear";
            }
            if (semiVariogramProp == "一次线性漂移模型")
            {
                semiVariogramProp = "Linear with Linear drift";
            }
            if (semiVariogramProp == "二次线性漂移模型")
            {
                semiVariogramProp = "Linear with Linear drift";
            }
            if (searchRadiusProp == "固定式")
            {
                searchRadiusProp = "Fixed";
            }
            else
            {
                searchRadiusProp = "Variable";
            }

            ESRI.ArcGIS.Analyst3DTools.Kriging raster = new ESRI.ArcGIS.Analyst3DTools.Kriging();
            raster.in_point_features = pathIn;
            raster.out_surface_raster = pathOut;
            raster.semiVariogram_props = semiVariogramProp;
            raster.search_radius = searchRadiusProp;
            raster.z_field = "Shape.Z";
            raster.cell_size = 1;
            return RunTool(gp, raster, null);
        }
        //用样条函数插值(Spline)到Raster默认方法
        static public void Interpolate2RasterSpline(Geoprocessor gp, string pathIn, string pathOut)
        {

            ESRI.ArcGIS.Analyst3DTools.Spline spline = new ESRI.ArcGIS.Analyst3DTools.Spline();
            spline.in_point_features = pathIn;
            spline.out_raster = pathOut;
            spline.z_field = "Shape.Z";
            spline.spline_type = "REGULARIZED";
            RunTool(gp, spline, null);
        }

        //用样条函数插值(Spline)到Raster选择参数
        static public bool Interpolate2RasterSpline(Geoprocessor gp, string pathIn, string pathOut, string SplineType)
        {


            ESRI.ArcGIS.Analyst3DTools.Spline spline = new ESRI.ArcGIS.Analyst3DTools.Spline();

            if (SplineType == "正规化")
            {
                spline.spline_type = "REGULARIZED";
            }
            else
            {
                spline.spline_type = "TENSION";
            }
            spline.in_point_features = pathIn;
            spline.out_raster = pathOut;
            spline.z_field = "Shape.Z";
            return RunTool(gp, spline, null);
        }

        //用自然邻域插值(NN)到Raster
        static public bool Interpolate2RasterNN(Geoprocessor gp, string pathIn, string pathOut)
        {
            ESRI.ArcGIS.Analyst3DTools.NaturalNeighbor nn = new ESRI.ArcGIS.Analyst3DTools.NaturalNeighbor();
            nn.cell_size = 1;
            nn.in_point_features = pathIn;
            nn.out_raster = pathOut;
            nn.z_field = "Shape.Z";
            return RunTool(gp, nn, null);
        }

        //用反距离权重插值(IDW)到Raster
        static public bool Interpolate2RasterIDW(Geoprocessor gp, string pathIn, string pathOut)
        {
            ESRI.ArcGIS.Analyst3DTools.Idw idw = new ESRI.ArcGIS.Analyst3DTools.Idw();
            idw.cell_size = 1;
            idw.z_field = "Shape";
            idw.in_point_features = pathIn;
            idw.out_raster = pathOut;
            
            return RunTool(gp, idw, null);
        }

        //趋势面法插值到Raster默认
        static public bool TrendToRaster(Geoprocessor gp, string pathIn, string pathOut)
        {
            ESRI.ArcGIS.Analyst3DTools.Trend ttr = new ESRI.ArcGIS.Analyst3DTools.Trend();
            ttr.in_point_features = pathIn;
            ttr.out_raster = pathOut;
            ttr.z_field = "Shape.Z";
            return RunTool(gp, ttr, null);
        }

        //SplineRaster To Contour
        static public bool SplineRasterToContour(Geoprocessor gp, string pathIn, string pathOut, double ContourInterval)
        {
            ESRI.ArcGIS.Analyst3DTools.Contour RasterContour = new ESRI.ArcGIS.Analyst3DTools.Contour();
            RasterContour.contour_interval = ContourInterval;
            RasterContour.in_raster = pathIn;
            RasterContour.z_factor = 1;
            RasterContour.base_contour = 0;
            RasterContour.out_polyline_features = pathOut;

            return RunTool(gp, RasterContour, null);
        }


        //features to 3D
        static public bool FeaturesTo3D(Geoprocessor gp, string pathIn, string pathOut)
        {
            ESRI.ArcGIS.Analyst3DTools.FeatureTo3DByAttribute featureTo3D = new ESRI.ArcGIS.Analyst3DTools.FeatureTo3DByAttribute();
            featureTo3D.in_features = pathIn;
            featureTo3D.out_feature_class = pathOut;
            featureTo3D.height_field = "Contour";
            return RunTool(gp, featureTo3D, null);
            //IGPProcess tools = null;
        }


        public static bool RunTool(Geoprocessor geoprocessor, IGPProcess process, ITrackCancel TC)
        {
            // Set the overwrite output option to true
            geoprocessor.OverwriteOutput = true;
            // Execute the tool            
            try
            {
                geoprocessor.Execute(process, null);
                ReturnMessages(geoprocessor);
                return true;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                ReturnMessages(geoprocessor);
                return false;
            }
        }


        private static string ReturnMessages(Geoprocessor gp)
        {
            string msgRet = "";
            if (gp.MessageCount > 0)
            {
                for (int Count = 0; Count <= gp.MessageCount - 1; Count++)
                {
                    msgRet += gp.GetMessage(Count);
                }
            }
            return msgRet;
        }
    }
}
