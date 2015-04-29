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
using LibCommon;

namespace GIS.Common
{
    [Guid("e34c22c8-8699-4333-b9d9-f1034a7a2c2d")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.Common.DrawSpecialCommon")]
    public class DrawSpecialCommon
    {
        /// <summary>
        /// 查询地图中的FeatureLayer(图层名为汉化图层名，一般为汉语名）
        /// </summary>
        /// <param name="strLayerName">图层名</param>
        /// <returns>矢量图层</returns>
        /// <remarks></remarks>
        public IFeatureLayer GetFeatureLayerByName(string strLayerName)
        {
            try
            {
                //取得Map中所有的FeatureLayer
                IMap map = DataEditCommon.g_pMap;
                UID uid = new UID();
                uid.Value = GIS_Const.STR_IFeatureLayer;
                IEnumLayer enumLayer = map.get_Layers(uid, true);
                if (enumLayer == null)
                    return null;
                enumLayer.Reset();
                ILayer layer;
                layer = enumLayer.Next();
                IFeatureLayer featureLayer = null;
                while ((layer != null))
                {
                    featureLayer = layer as IFeatureLayer;
                    if (featureLayer.Valid && featureLayer.FeatureClass != null)
                    {
                        if (featureLayer.Name == strLayerName)
                        {
                            return featureLayer;
                        }
                    }
                    layer = enumLayer.Next();
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// 移除非本系统所需图层
        /// </summary>
        /// <param name="strLayerName">当前系统图层组名称</param>
        public void RemoveLayerByName(string strLayerName)
        {
            try
            {
                //取得Map中所有的图层
                IMap map = DataEditCommon.g_pMap;
                IEnumLayer enumLayer = map.get_Layers(null, true);
                if (enumLayer == null)
                    return;
                enumLayer.Reset();
                ILayer layer;
                layer = enumLayer.Next();
                while ((layer != null))
                {
                    if (layer is IGroupLayer)
                    {
                        if (layer.Name != strLayerName)//不为当前系统所需图层，移除
                        {
                            map.DeleteLayer(layer);
                        }
                        else
                            layer.Visible = true;
                    }
                    else
                    {
                        layer = enumLayer.Next();
                        continue;
                    }
                    layer = enumLayer.Next();
                }
            }
            catch (Exception ex)
            {
                Log.Debug(ex.ToString());
            }

        }

        /// <summary>
        /// 图层是否展开控制
        /// </summary>
        public void LayersExpand()
        {
            ILayer pLayer;
            IGroupLayer pGroupLayer;
            IFeatureLayer pFeatureLayer;
            ILegendInfo pLegendInfo;
            ILegendGroup pLegendGroup;
            for (int i = 0; i <DataEditCommon.g_pAxMapControl.LayerCount; i++)
            {
                pLayer = DataEditCommon.g_pAxMapControl.Map.get_Layer(i);
                if (pLayer is IGroupLayer)
                {
                    pGroupLayer = pLayer as IGroupLayer;
                    pGroupLayer.Expanded = true;
                }
                else
                {
                    pFeatureLayer = pLayer as IFeatureLayer;
                    pLegendInfo = pFeatureLayer as ILegendInfo;
                    pLegendGroup = pLegendInfo.get_LegendGroup(0);
                    pLegendGroup.Visible = true;
                }
            }
            DataEditCommon.g_axTocControl.Update();
        }


        #region 创建要素图层

        /// <summary>
        /// 创建要素图层
        /// 也可以利用现有要素图层创建：IFeatureLayer featurelayer,
        /// </summary>
        /// <param name="map"></param>
        /// <param name="workspace"></param>
        /// <param name="layername"></param>
        /// <param name="aliasname"></param>
        /// <returns></returns>
        public IFeatureLayer CreateFeatureLayer(IMap map, IWorkspace workspace,
            string layername, string aliasname)
        {
            try
            {
                IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;
                //IFields fields = featurelayer.FeatureClass.Fields;

                // 创建要素类的字段集
                IFields fields = new FieldsClass();
                IFieldsEdit fieldsEdit = (IFieldsEdit)fields;

                // 添加要素类必须字段：object ID 字段
                IField oidField = new FieldClass();
                IFieldEdit oidFieldEdit = (IFieldEdit)oidField;
                oidFieldEdit.Name_2 = "OBJECTID";
                oidFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
                fieldsEdit.AddField(oidField);

                // 创建几何定义（和空间参考）
                IGeometryDef geometryDef = new GeometryDefClass();
                IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
                geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
                //ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                //ISpatialReference spatialReference =
                //spatialReferenceFactory.CreateProjectedCoordinateSystem((int)esriSRProjCSType.esriSRProjCS_NAD1983UTM_20N);
                //ISpatialReferenceResolution spatialReferenceResolution = (ISpatialReferenceResolution)spatialReference;
                //spatialReferenceResolution.ConstructFromHorizon();
                //ISpatialReferenceTolerance spatialReferenceTolerance = (ISpatialReferenceTolerance)spatialReference;
                //spatialReferenceTolerance.SetDefaultXYTolerance();
                geometryDefEdit.SpatialReference_2 = map.SpatialReference;

                //添加几何字段
                IField geometryField = new FieldClass();
                IFieldEdit geometryFieldEdit = (IFieldEdit)geometryField;
                geometryFieldEdit.Name_2 = "Shape";
                geometryFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                geometryFieldEdit.GeometryDef_2 = geometryDef;
                fieldsEdit.AddField(geometryField);

                //新建字段
                IField pField = new FieldClass();
                IFieldEdit pFieldEdit = pField as IFieldEdit;
                pFieldEdit.Length_2 = 50;
                pFieldEdit.Name_2 = "ID";
                pFieldEdit.AliasName_2 = "ID";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                fieldsEdit.AddField(pField);

                //检查字段的有效性
                IFieldChecker fieldChecker = new FieldCheckerClass();
                IEnumFieldError enumFieldError = null;
                IFields validatedFields = null;
                fieldChecker.ValidateWorkspace = (IWorkspace)featureWorkspace;
                fieldChecker.Validate(fields, out enumFieldError, out validatedFields);

                IFeatureClass featureClass = featureWorkspace.CreateFeatureClass(layername, validatedFields, null, null,
                    esriFeatureType.esriFTSimple, "Shape", "");

                //用创建的要素类生成要素图层
                IFeatureLayer newFeaLayer = new FeatureLayerClass();
                newFeaLayer.FeatureClass = featureClass;
                newFeaLayer.Name = aliasname;

                //添加新建图层到相应图层组中
                IGroupLayer groupLayer = new GroupLayerClass();
                string groupLayerName = aliasname.Split("-".ToCharArray())[1].Trim();
                groupLayer = GetGroupLayerByName(groupLayerName);
                groupLayer.Add(newFeaLayer as ILayer);
                DataEditCommon.g_axTocControl.Update();
                DataEditCommon.g_pAxMapControl.Refresh();

                return newFeaLayer;
            }
            catch (Exception ex)
            {
                Log.Debug(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 根据名称，查找GroupLayer图层
        /// </summary>
        /// <param name="strLayerName">当前系统图层组名称</param>
        public IGroupLayer GetGroupLayerByName(string strLayerName)
        {
            try
            {
                //取得Map中所有的图层
                IMap map = DataEditCommon.g_pMap;
                IEnumLayer enumLayer = map.get_Layers(null, true);
                if (enumLayer == null)
                    return null;
                enumLayer.Reset();
                ILayer layer;
                layer = enumLayer.Next();
                while ((layer != null))
                {
                    if (layer is IGroupLayer)
                    {
                        if (layer.Name == strLayerName)//找到要插入的图层组
                        {
                            layer.Visible = true;
                            return layer as IGroupLayer;
                        }                       
                    }
                    layer = enumLayer.Next();
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
       

        /// <summary>
        /// 根据名称获取数据库中对应图层（要素集）
        /// </summary>
        /// <param name="workspace"></param>
        /// <param name="dsName"></param>
        /// <returns></returns>
        public IDataset GetDatasetByName(IWorkspace workspace ,string dsName)
        {
            try
            {
                IEnumDataset enumDataset = workspace.get_Datasets(esriDatasetType.esriDTAny);
                IDataset dataset = enumDataset.Next();
                while (dataset != null)
                {
                    if (dataset.Name == dsName)
                    {
                        return dataset;
                    }
                    dataset = enumDataset.Next();
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion 创建要素图层


        #region 图层入库

        /// <summary>
        /// shape图层入库
        /// </summary>
        /// <param name="sourceworkspace"></param>
        /// <param name="targetworkspace"></param>
        /// <param name="nameOfsourceFeatureClass"></param>
        /// <param name="nameOftargetFeatureClass"></param>
        /// <returns></returns>
        public IFeatureClass ShapeFileIntoGDB(IWorkspace sourceworkspace,IWorkspace targetworkspace,
            string nameOfsourceFeatureClass, string nameOftargetFeatureClass)
        {
            try
            {
                //创建源工作空间
                IDataset sourceWorkspaceDataset = (IDataset)sourceworkspace;
                IWorkspaceName sourceWorkspaceName = (IWorkspaceName)sourceWorkspaceDataset.FullName;

                //创建源数据集
                IFeatureClassName sourceFeatureClassName = new FeatureClassNameClass();
                IDatasetName sourceDatasetName = (IDatasetName)sourceFeatureClassName;
                sourceDatasetName.WorkspaceName = sourceWorkspaceName;
                sourceDatasetName.Name = nameOfsourceFeatureClass;

                //创建目标工作空间
                IDataset targetWorkspaceDataset = (IDataset)targetworkspace;
                IWorkspaceName targetWorkspaceName = (IWorkspaceName)targetWorkspaceDataset.FullName;

                //创建目标数据集
                IFeatureClassName targetFeatureClassName = new FeatureClassNameClass();
                IDatasetName targetDatasetName = (IDatasetName)targetFeatureClassName;
                targetDatasetName.WorkspaceName = targetWorkspaceName;
                targetDatasetName.Name = nameOftargetFeatureClass;

                //源数据集的字段集
                IName sourceName = (IName)sourceFeatureClassName;
                IFeatureClass sourceFeatureClass = (IFeatureClass)sourceName.Open();

                //验证字段
                IFieldChecker fieldChecker = new FieldCheckerClass();
                IFields targetFeatureClassFields;
                IFields sourceFeatureClassFields = sourceFeatureClass.Fields;
                IEnumFieldError enumFieldError;

                //设置验证的对象
                fieldChecker.InputWorkspace = sourceworkspace;
                fieldChecker.ValidateWorkspace = targetworkspace;
                fieldChecker.Validate(sourceFeatureClassFields, out enumFieldError, out targetFeatureClassFields);

                //找到空间对象字段
                IField geometryField;
                for (int i = 0; i < targetFeatureClassFields.FieldCount; i++)
                {
                    if (targetFeatureClassFields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        geometryField = targetFeatureClassFields.get_Field(i);

                        //得到空间字段的定义
                        IGeometryDef geometryDef = geometryField.GeometryDef;

                        //得到空间字段的索引
                        IGeometryDefEdit targetFCGeometryDefEdit = (IGeometryDefEdit)geometryDef;
                        targetFCGeometryDefEdit.GridCount_2 = 1;
                        targetFCGeometryDefEdit.set_GridSize(0, 0);
                        //targetFCGeometryDefEdit.SpatialReference_2 = geometryField.GeometryDef.SpatialReference;

                        ISpatialReferenceFactory spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                        ISpatialReference spatialReference = DataEditCommon.g_pMap.SpatialReference;
                        //spatialReferenceFactory.CreateProjectedCoordinateSystem((int)esriSRProjCSType.esriSRProjCS_NAD1983UTM_20N);
                        ISpatialReferenceResolution spatialReferenceResolution = (ISpatialReferenceResolution)spatialReference;
                        spatialReferenceResolution.ConstructFromHorizon();
                        spatialReferenceResolution.SetDefaultXYResolution();
                        spatialReferenceResolution.SetDefaultZResolution();
                        ISpatialReferenceTolerance spatialReferenceTolerance = (ISpatialReferenceTolerance)spatialReference;
                        spatialReferenceTolerance.SetMinimumXYTolerance();
                        spatialReferenceTolerance.SetMinimumZTolerance();

                        double XMin, XMax, YMin, YMax, ZMin, ZMax, MinXYTolerance, MinZTolerance;
                        XMin = 4054.3603997438;
                        XMax = 78088.6926632544;
                        YMin = 14424.8510028409;
                        YMax = 59609.4812606697;
                        ZMin = 30330.1483519995;
                        ZMax = 38389.3283520005;
                        //spatialReference.SetDomain(XMin, XMax, YMin, YMax);
                        //spatialReference.SetZDomain(ZMin, ZMax);

                        MinXYTolerance = 0.000000000008219;
                        MinZTolerance = 0.000000000007629;
                        //spatialReferenceTolerance.SetMinimumZTolerance( MinXYTolerance);

                        //spatialReference.GetDomain(out XMin, out XMax, out YMin, out YMax);
                        //spatialReference.GetZDomain(out ZMin, out ZMax);


                        targetFCGeometryDefEdit.SpatialReference_2 = spatialReference;


                        //开始导入
                        IQueryFilter queryFilter = new QueryFilterClass();
                        queryFilter.WhereClause = "";

                        //导入所有的输入对象
                        IFeatureDataConverter featureDataConverter = new FeatureDataConverterClass();
                        IEnumInvalidObject enumInvalidObject = featureDataConverter.ConvertFeatureClass(sourceFeatureClassName,
                            queryFilter, null, targetFeatureClassName, geometryDef, targetFeatureClassFields, "", 1000, 0);
                        break;
                    }
                }

                //导入后数据集的字段集
                IName targetName = (IName)targetFeatureClassName;
                IFeatureClass targetFeatureClass = (IFeatureClass)targetName.Open();

                return targetFeatureClass;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        /// <summary>
        /// Shape数据导入SDE现有数据源
        /// </summary>
        /// <param name="sourceworkspace"></param>
        /// <param name="targetworkspace"></param>
        /// <param name="nameOfsourceFeatureClass"></param>
        /// <param name="nameOftargetFeatureClass"></param>
        /// <returns></returns>
        public bool ShapeImportGDB(IWorkspace sourceworkspace, IWorkspace targetworkspace,
            string nameOfsourceFeatureClass, string nameOftargetFeatureClass,List<ziduan> list)
        {
            try
            {
                //创建源工作空间
                IDataset sourceWorkspaceDataset = (IDataset)sourceworkspace;
                IWorkspaceName sourceWorkspaceName = (IWorkspaceName)sourceWorkspaceDataset.FullName;

                //创建源数据集
                IFeatureClassName sourceFeatureClassName = new FeatureClassNameClass();
                IDatasetName sourceDatasetName = (IDatasetName)sourceFeatureClassName;
                sourceDatasetName.WorkspaceName = sourceWorkspaceName;
                sourceDatasetName.Name = nameOfsourceFeatureClass;

                //源数据集的字段集
                IName sourceName = (IName)sourceFeatureClassName;
                IFeatureClass sourceFeatureClass = (IFeatureClass)sourceName.Open();

                //创建目标工作空间
                IDataset targetWorkspaceDataset = (IDataset)targetworkspace;
                IWorkspaceName targetWorkspaceName = (IWorkspaceName)targetWorkspaceDataset.FullName;

                //创建目标数据集
                IFeatureClassName targetFeatureClassName = new FeatureClassNameClass();
                IDatasetName targetDatasetName = (IDatasetName)targetFeatureClassName;
                targetDatasetName.WorkspaceName = targetWorkspaceName;
                targetDatasetName.Name = nameOftargetFeatureClass;

                //目标数据集的字段集
                IName targetName = (IName)targetFeatureClassName;
                IFeatureClass targetFeatureClass = (IFeatureClass)targetName.Open();

                IFeatureCursor pCursor = sourceFeatureClass.Search(null, false);
                IFeature pFeature = pCursor.NextFeature();

                if (pFeature != null)
                {
                    if (DataEditCommon.g_CurWorkspaceEdit==null)
                        DataEditCommon.g_CurWorkspaceEdit = (IWorkspaceEdit)DataEditCommon.g_pCurrentWorkSpace;
                    DataEditCommon.g_CurWorkspaceEdit.StartEditing(true);
                    DataEditCommon.g_CurWorkspaceEdit.StartEditOperation();
                }
                IField pField=null;
                object pValue = null;
                while (pFeature != null)
                {
                    IFeature tFeature=targetFeatureClass.CreateFeature();
                    tFeature.Shape = pFeature.ShapeCopy;
                    for (int i = 0; i < pFeature.Fields.FieldCount; i++)
                    {
                        pField = pFeature.Fields.get_Field(i);
                        if (pField.Type == esriFieldType.esriFieldTypeGeometry || pField.Type == esriFieldType.esriFieldTypeGlobalID || pField.Type == esriFieldType.esriFieldTypeOID)
                            continue;
                        pValue = pFeature.get_Value(i);
                        if (tFeature.Fields.FindField(pField.Name) > 0)
                        {
                            tFeature.set_Value(tFeature.Fields.FindField(pField.Name), pValue);
                        }
                    }
                    if (list != null)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (tFeature.Fields.FindField(list[i].name) > 0)
                            {
                                tFeature.set_Value(tFeature.Fields.FindField(list[i].name), list[i].value);
                            }
                        }
                    }
                    tFeature.Store();

                    pFeature = pCursor.NextFeature();
                }
                if (DataEditCommon.g_CurWorkspaceEdit.IsBeingEdited())
                {
                    DataEditCommon.g_CurWorkspaceEdit.StopEditOperation();
                    DataEditCommon.g_CurWorkspaceEdit.StopEditing(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

    
        /// <summary>
        /// Raster图层导入到数据库
        /// </summary>
        /// <param name="dbWorkspace">数据库</param>
        /// <param name="strRasterFileDir">本地栅格图层路径</param>
        /// <param name="strRasterFileName">本地栅格图层名称</param>
        /// <param name="strOutName">数据库栅格图层名称</param>
        /// <returns></returns>
        public IRasterDataset RasterFileIntoGDB(IWorkspace dbWorkspace,string strRasterFileDir, 
            string strRasterFileName,string strOutName)
        {
            IWorkspace pSdeWorkSpace = dbWorkspace;
            try
            {
                //判断是否有重名现象
                IWorkspace2 pWS2 = pSdeWorkSpace as IWorkspace2;

                //如果名称已存在
                if (pWS2.get_NameExists(esriDatasetType.esriDTRasterDataset, strOutName))
                {
                    DialogResult result;
                    result = MessageBox.Show("栅格文件名  " + strOutName + "  在数据库中已存在!" +
                        "\r是否覆盖?", "相同文件名", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    //覆盖原栅格要素
                    if (result == DialogResult.Yes)
                    {
                        IRasterWorkspaceEx pRWs = pSdeWorkSpace as IRasterWorkspaceEx;
                        IDataset pDataset = pRWs.OpenRasterDataset(strOutName) as IDataset;
                        pDataset.Delete();
                        pDataset = null;
                    }
                    else if (result == DialogResult.No)
                    {
                        //不覆盖,则退出for循环,忽略这个要素,转入下一个要素的导入
                        return null;
                    }
                }

                IWorkspaceFactory pRasterWsFac = new RasterWorkspaceFactoryClass();
                IWorkspace pWs = pRasterWsFac.OpenFromFile(strRasterFileDir, 0);
                IRasterDataset pRasterDs = null;
                IRasterWorkspace pRasterWs;
                if (!(pWs is IRasterWorkspace))
                {
                    MessageBox.Show("错误信息：" + strRasterFileDir + "不是栅格工作空间。");   
                    return null;
                }
                pRasterWs = pWs as IRasterWorkspace;
                pRasterDs = pRasterWs.OpenRasterDataset(strRasterFileName);
                ISaveAs2 saveAs2 = (ISaveAs2)pRasterDs;
                IRasterStorageDef rasterStorageDef = new RasterStorageDefClass();
                IRasterStorageDef2 rasterStorageDef2 = (IRasterStorageDef2)rasterStorageDef;
                rasterStorageDef2.CompressionType =
                    esriRasterCompressionType.esriRasterCompressionJPEG2000;

                rasterStorageDef2.CompressionQuality = 50;
                rasterStorageDef2.Tiled = true;
                rasterStorageDef2.TileHeight = 128;
                rasterStorageDef2.TileWidth = 128;
                
                saveAs2.SaveAsRasterDataset(strOutName, pSdeWorkSpace, "JP2", rasterStorageDef2);
                
                return pRasterDs;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误信息：" + ex.Message);                
                return null;
            }
        }
        #endregion
    }

    public class ziduan
    {
        public ziduan()
        { }
        public ziduan(string _name,string _value)
        {
            name = _name;
            value = _value;
        }
        public string name;
        public string value;
    }
}
