using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.GeoAnalyst;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.Geoprocessing;
namespace GIS
{
  public  class AlarmAnalysis
    {
     
      /// <summary>
      /// 绘制工作面预警
      /// </summary>
      public void taskAlarm(LibGeometry.Vector3_DW[] vectors,ESRI.ArcGIS.Controls.AxMapControl map)
      {
          //1、提取散点
          //2、先由散点创建FeatureClass
          //3、由FeatureClass创建TIN
          DirectoryInfo dir = new DirectoryInfo(Application.StartupPath + "\\tempRaster");
          if (dir.Exists)
          {
              try
              {
                  DirectoryInfo[] childs = dir.GetDirectories();
                  foreach (DirectoryInfo child in childs)
                  {
                      child.Delete(true);
                  }
                  dir.Delete(true);
                  Directory.CreateDirectory(Application.StartupPath + "\\tempRaster");
              }
              catch (Exception e)
              {
                  MessageBox.Show(e.Message);
              }

          }
          else
          {
              Directory.CreateDirectory(Application.StartupPath + "\\tempRaster");
          }
       
          IFeatureClass pFeatureClass = GetFeatureCLass(vectors,map);
          IField pField = pFeatureClass.Fields.get_Field(pFeatureClass.FindField(" CoordinateZ"));
           CreateRasterfromFeature( Application.StartupPath + "\\tempRaster");

          //map.AddLayer(pTinLayer as ILayer);


      }
      /// <summary>
      /// 根据生成的featureclass动态生成tin
      /// </summary>
      /// <param name="pFeatureClass"></param>
      /// <param name="pField"></param>
      /// <param name="pPath"></param>
      /// <returns></returns>
       private ITin CreateTin(IFeatureClass pFeatureClass, IField pField, string pPath)
         {
            IGeoDataset pGeoDataset = pFeatureClass as IGeoDataset;
            ITinEdit pTinEdit = new TinClass();
              pTinEdit.InitNew(pGeoDataset.Extent);
              object pObj = Type.Missing;
              pTinEdit.AddFromFeatureClass(pFeatureClass, null, pField, null, esriTinSurfaceType.esriTinMassPoint, ref pObj);
              pTinEdit.SaveAs(pPath, ref pObj);
              pTinEdit.Refresh();
             return pTinEdit as ITin;
         }
      /// <summary>
      /// 根据离散点数据动态生成featureclass点图层
      /// </summary>
      /// <param name="vectors"></param>
      /// <returns></returns>
       private static IFeatureClass GetFeatureCLass(LibGeometry.Vector3_DW[] vectors,ESRI.ArcGIS.Controls.AxMapControl map)
        {
            IWorkspaceFactory pWorkspaceFactory = new InMemoryWorkspaceFactoryClass();
            IWorkspaceName pWorkspaceName = pWorkspaceFactory.Create("", "pWorkspace", null, 0);
            IName pName = (IName)pWorkspaceName;
            IWorkspace pWorkspace = (IWorkspace)pName.Open();
            IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;
             IFields pFields = new FieldsClass();
             IFieldsEdit pFieldsEdit = pFields as IFieldsEdit;
             IField pField = new FieldClass();
             IFieldEdit pFieldEdit = pField as IFieldEdit;
             pFieldEdit.Name_2 = "SHAPE";
             pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
             IGeometryDef pGeometryDef = new GeometryDefClass();
             IGeometryDefEdit pGeometryDefEdit = pGeometryDef as IGeometryDefEdit;
             pGeometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
             //为FeatureClass赋参考系，不写会出错***************************************            
             
             pGeometryDefEdit.SpatialReference_2 = map.SpatialReference;
             //************************************************************************
             pFieldEdit.GeometryDef_2 = pGeometryDef;
             pFieldsEdit.AddField(pField);
             pField = new FieldClass();//不要省略写！容易出问题
             pFieldEdit = pField as IFieldEdit;
             pFieldEdit.AliasName_2 = "高程";
             pFieldEdit.Name_2 = "CoordinateZ";
             pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
             pFieldsEdit.AddField(pField);
             pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(Application.StartupPath+"\\tempRaster", 0) as IFeatureWorkspace;
            IFeatureClass pFeatureClass = pFeatureWorkspace.CreateFeatureClass("points", pFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");



            //IWorkspaceFactory pWorkSpaceFac = new ShapefileWorkspaceFactoryClass();
            //IFeatureWorkspace pFeatureWorkSpace = pWorkSpaceFac.OpenFromFile(shpfolder, 0) as IFeatureWorkspace;

            ////创建字段集2
            //IFeatureClassDescription fcDescription = new FeatureClassDescriptionClass();
            //IObjectClassDescription ocDescription = (IObjectClassDescription)fcDescription;//创建必要字段
            //IFields fields = ocDescription.RequiredFields;
            //int shapeFieldIndex = fields.FindField(fcDescription.ShapeFieldName);
            //IField field = fields.get_Field(shapeFieldIndex);
            //IGeometryDef geometryDef = field.GeometryDef;
            //IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
            ////geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            ////geometryDefEdit.SpatialReference_2 = spatialReference;

            //geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
            //ISpatialReferenceFactory pSpatialRefFac = new SpatialReferenceEnvironmentClass();
            //IProjectedCoordinateSystem pcsSys = pSpatialRefFac.CreateProjectedCoordinateSystem((int)esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_39);
            //geometryDefEdit.SpatialReference_2 = pcsSys;

            //IFieldChecker fieldChecker = new FieldCheckerClass();
            //IEnumFieldError enumFieldError = null;
            //IFields validatedFields = null; //将传入字段 转成 validatedFields
            //fieldChecker.ValidateWorkspace = (IWorkspace)pFeatureWorkSpace;
            //fieldChecker.Validate(fields, out enumFieldError, out validatedFields);

            //pFeatureWorkSpace.CreateFeatureClass(shpname, validatedFields, ocDescription.InstanceCLSID, ocDescription.ClassExtensionCLSID, esriFeatureType.esriFTSimple, fcDescription.ShapeFieldName, "");







             //从vectors中获取散点值
            Dictionary<int, IPoint> pointDictionary = new Dictionary<int, IPoint>();
            for (int i = 0; i < vectors.Length; i++)
            {
                 IPoint pPoint=new PointClass();
                 pPoint.X = vectors[i].X;
                 pPoint.Y = vectors[i].Y;
                 pPoint.Z =vectors[i].Z;
                 if (pPoint.Z.ToString() != "非数字")
                 {
                     pointDictionary.Add(i, pPoint);
                 }
             }
         
             //插入到新建的FeatureClass中
             IWorkspaceEdit pWorkspaceEdit = pWorkspace as IWorkspaceEdit;
             pWorkspaceEdit.StartEditing(true);
             pWorkspaceEdit.StartEditOperation();
             IFeatureBuffer pFeatureBuffer = pFeatureClass.CreateFeatureBuffer();
             IFeatureCursor pFeatureCursor = pFeatureClass.Insert(true);
             for (int featureNum = 4; featureNum < pointDictionary.Count;featureNum++ )
             {
                 pFeatureBuffer.Shape = pointDictionary[featureNum] as IPoint;//出错点，在于新建字段的错误
                 pFeatureBuffer.set_Value(pFeatureClass.Fields.FindField("CoordinateZ"), pointDictionary[featureNum].Z);
                 pFeatureCursor.InsertFeature(pFeatureBuffer);
             }
             pFeatureCursor.Flush();
           
             pWorkspaceEdit.StopEditOperation();
             pWorkspaceEdit.StopEditing(true); 
             return pFeatureClass;
         }
       private void CreateRasterfromFeature(string strPath)
       {

           Geoprocessor GP = new Geoprocessor();
           ESRI.ArcGIS.Analyst3DTools.Idw gGYH = new ESRI.ArcGIS.Analyst3DTools.Idw();
           gGYH.in_point_features = strPath;
           gGYH.z_field = "CoordinateZ";
           gGYH.out_raster = Application.StartupPath+"\\tempRaster\\Raster";
           IGeoProcessorResult pGeoProcessorR = GP.Execute(gGYH, null) as IGeoProcessorResult;
           
       }
    }
}
