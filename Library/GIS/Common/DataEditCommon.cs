using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
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
    [Guid("8a062c2f-7dd2-4e9a-8668-50649beb3770")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.Common.DataEditCommon")]
    public class DataEditCommon
    {
        public static int X;
        public static int Y;
        public static IEngineEditEvents_Event g_EngineEditEvents;
        public static IEngineEditor g_engineEditor;
        public static IEngineEditLayers g_engineEditLayers;
        public static IOperationStack g_operationStack;
        public static IToolbarControl g_tbCtlEdit;
        public static IMap g_pMap;
        public static IWorkspace g_pCurrentWorkSpace;//当前编辑的工作空间
        public static IWorkspaceEdit g_CurWorkspaceEdit;  //当前编辑的工作空间
        public static IMapControl3 g_pMyMapCtrl;
        public static AxMapControl g_pAxMapControl;
        public static ILayer g_pLayer;//20140124 lyf 当前工作图层
        public static AxTOCControl g_axTocControl;
        public static IGraphicsContainer g_pGraph = null;//临时绘图容器
        public static GIS.LayersManager.MapControlRightButton contextMenu = new GIS.LayersManager.MapControlRightButton();
        public static int copypaste = 0;
        public static IFeatureLayer copypasteLayer = null;
        public static void load()
        {
            g_pMyMapCtrl = (IMapControl3)g_pAxMapControl.Object;
            g_pMap = g_pAxMapControl.Map;
            g_pGraph = (IGraphicsContainer)g_pMap;
            IWorkspace workspace = SDEOperation.connectSde();//dfjia
            g_pCurrentWorkSpace = workspace;
            g_CurWorkspaceEdit = (IWorkspaceEdit)workspace;
            contextMenu.SetHook(g_pAxMapControl.Object);
            //g_pAxMapControl.ActiveView.Refresh();
        }
        public static void InitEditEnvironment()
        {
            g_operationStack = new ControlsOperationStackClass();
            g_tbCtlEdit.OperationStack = g_operationStack;
            g_engineEditor = new EngineEditorClass();
            g_engineEditLayers = (IEngineEditLayers)g_engineEditor;

            System.Object tbr = g_tbCtlEdit.Object;
            IExtension engineEditorExt = (IExtension)g_engineEditor;
            engineEditorExt.Startup(tbr);
            g_EngineEditEvents = (IEngineEditEvents_Event)g_engineEditor;
        }

        public static void CheckEditState()
        {
            try
            {
                if (g_engineEditor.EditState == esriEngineEditState.esriEngineStateNotEditing)
                {
                    // 启动编辑代码如下：红色部分值得注意，如果不设置编辑类型，
                    // 则会报异常来自 HRESULT:0x80040356，这个设置类似用在Arcmap中Edit工具栏下来的Opetions选项的Versision选项卡。
                    g_engineEditor.EditSessionMode = esriEngineEditSessionMode.esriEngineEditSessionModeVersioned;
                    g_engineEditor.StartEditing(g_pCurrentWorkSpace, g_pMap);
                }
                if (g_CurWorkspaceEdit == null)
                    g_CurWorkspaceEdit = (IWorkspaceEdit)g_pCurrentWorkSpace;
                if (g_CurWorkspaceEdit.IsBeingEdited() == false)
                {
                    g_engineEditor.EditSessionMode = esriEngineEditSessionMode.esriEngineEditSessionModeVersioned;
                    g_engineEditor.StartEditing(g_pCurrentWorkSpace, g_pMap);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 是否存在未保存的编辑
        /// </summary>
        /// <returns></returns>
        public static bool hasEdit()
        {
            bool undo = false;
            bool redo = false;
            g_CurWorkspaceEdit.HasRedos(ref redo);
            g_CurWorkspaceEdit.HasUndos(ref undo);
            if (undo || redo)
                return true;
            return false;
        }

        /// <summary>
        /// 转换坐标点
        /// </summary>
        /// <param name="vActiveView"></param>
        /// <param name="vPixelDistance"></param>
        /// <returns></returns>
        public static double ConvertPixelDistanceToMapDistance(IActiveView vActiveView, double vPixelDistance)
        {
            tagPOINT tagP;
            WKSPoint WKSP = new WKSPoint();
            tagP.x = Convert.ToInt32(vPixelDistance);
            tagP.y = Convert.ToInt32(vPixelDistance);
            vActiveView.ScreenDisplay.DisplayTransformation.TransformCoords(ref WKSP, ref tagP, 1, 6);
            return WKSP.X;
        }

        /// <summary>
        /// 单个要素符号化
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="pBitmap"></param>
        public static void SpecialPointRenderer(ILayer layer, string field, string value, Bitmap pBitmap)
        {
            IGeoFeatureLayer geoFeaLayer;
            IFeatureRenderer featureRenderer;
            ISymbol defaultSymbol;
            IUniqueValueRenderer uniValueRender;

            geoFeaLayer = layer as IGeoFeatureLayer;
            featureRenderer = geoFeaLayer.Renderer;
            uniValueRender = new UniqueValueRenderer();

            ///选择某个字段作为渲染符号值
            IQueryFilter2 queryFilter = new QueryFilterClass();
            int fieldIndex;
            uniValueRender.FieldCount = 1;
            uniValueRender.Field[0] = field;
            queryFilter.AddField(field);
            fieldIndex = geoFeaLayer.FeatureClass.Fields.FindField(field);//获得字段的index  

            ///获取自定义符号
            ISymbol customSymbol;
            IPictureMarkerSymbol pictureMarkerSymbol = new PictureMarkerSymbolClass();
            pictureMarkerSymbol.Size = 55;
            string strFilePath = Application.StartupPath.ToString() + "\\temp.bmp";

            pBitmap.Save(strFilePath);
            pictureMarkerSymbol.CreateMarkerSymbolFromFile(esriIPictureType.esriIPicturePNG,
              strFilePath);
            customSymbol = (ISymbol)pictureMarkerSymbol;

            ///设置渲染符号进行渲染
            string sValue;
            IFeature feature = null;
            IFeatureCursor featureCursor;
            featureCursor = geoFeaLayer.FeatureClass.Search(queryFilter, true);
            feature = featureCursor.NextFeature();
            while (feature != null)
            {
                sValue = Convert.ToString(feature.get_Value(fieldIndex));
                if (sValue == value)
                {
                    uniValueRender.AddValue(sValue, "", customSymbol);
                }
                else
                {
                    ///非当前所选要素，其符号保持不变
                    defaultSymbol = geoFeaLayer.Renderer.get_SymbolByFeature(feature);
                    uniValueRender.AddValue(sValue, "", defaultSymbol);
                }

                feature = featureCursor.NextFeature();
            }

            if (featureCursor != null)
            {
                featureCursor = null;
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(featureCursor);
            }

            geoFeaLayer.Renderer = uniValueRender as IFeatureRenderer;
        }

        /// <summary>
        /// 测试点的位置是否存在Feature
        /// </summary>
        /// <param name="m_hookHelper"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="theFeature"></param>
        public static void TestExistFeature(IHookHelper m_hookHelper, int X, int Y, ref IFeature theFeature)
        {
            ArrayList pSelected = new ArrayList();
            IFeatureClass pFeatureClass;
            IPoint pPoint;
            ISelectionEnvironment pSelectionEnvironment;
            IFeature pFeature;
            IGeometry pGeometry;
            ITopologicalOperator pTopolagicalOperator;
            double dLength;

            IEnvelope pSrchEnv;
            pSelectionEnvironment = new SelectionEnvironmentClass();
            dLength = pSelectionEnvironment.SearchTolerance;
            pPoint = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            pGeometry = pPoint;
            dLength = ConvertPixelDistanceToMapDistance(m_hookHelper.ActiveView, dLength);
            pSrchEnv = pPoint.Envelope;
            pSrchEnv.Width = dLength;
            pSrchEnv.Height = dLength;
            pSrchEnv.CenterAt(pPoint);

            pTopolagicalOperator = (ITopologicalOperator)pGeometry;
            IGeometry pBuffer = pTopolagicalOperator.Buffer(dLength);
            pGeometry = pBuffer;
            IFeatureLayer pFeatureLayer;
            IFeature pFeat = null;
            IMap pMap = m_hookHelper.FocusMap;
            List<ILayer> list = GetLayerListByKey(DataEditCommon.g_pMap, "默认");
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Visible == false || list[i] is IFeatureLayer == false)
                {
                    continue;
                }
                pFeatureLayer = list[i] as IFeatureLayer;
                pFeatureClass = pFeatureLayer.FeatureClass;
                if (pFeatureClass == null)
                {
                    return;
                }
                IIdentify2 pID = pFeatureLayer as IIdentify2;
                IArray pArray = pID.Identify(pSrchEnv, null);
                IFeatureIdentifyObj pFeatIdObj;
                IRowIdentifyObject pRowObj;

                if (pArray != null)
                {
                    for (int j = 0; j < pArray.Count; j++)
                    {
                        if (pArray.Element[j] is IFeatureIdentifyObj)
                        {
                            pFeatIdObj = pArray.Element[j] as IFeatureIdentifyObj;
                            pRowObj = pFeatIdObj as IRowIdentifyObject;
                            pFeature = pRowObj.Row as IFeature;
                            pSelected.Add(pFeature);
                            pSelected.Add(pFeatureLayer.Name);
                            pSelected.Add(i);
                        }
                    }
                    pArray.RemoveAll();
                }
            }
            GetClosestFeatureInCollection(m_hookHelper, list, dLength, pSelected, pPoint, ref pFeat);
            if (pFeat != null)
                theFeature = pFeat;
            else
                theFeature = null;

        }


        public static void GetClosestFeatureInCollection(IHookHelper m_hookHelper, List<ILayer> list, double SearchDist, ArrayList searchCollection, IPoint pPoint, ref IFeature pFeature)
        {
            IProximityOperator pProximity;
            IFeature pTestFeature;
            IFeature pFea;
            IFeature pPointFeature = null;
            IFeature pLineFeature = null;
            IFeature pAreaFeature = null;
            IGeometry pGeometry;
            double pointTestDistance;
            double lineTestDistance;
            double areaTestDistance;
            double testDistance;

            double tempDist;

            ArrayList pointList = new ArrayList();
            ArrayList lineList = new ArrayList();
            ArrayList areaList = new ArrayList();
            ArrayList NewList = new ArrayList();
            pointTestDistance = -1;
            lineTestDistance = -1;
            areaTestDistance = -1;
            testDistance = -1;

            try
            {
                pProximity = pPoint as IProximityOperator;

                if (searchCollection.Count == 0) return;//20140216 lyf 没有选中图元情况处理

                for (int i = 0; i < searchCollection.Count; i = i + 3)
                {
                    pTestFeature = searchCollection[i] as IFeature;
                    pGeometry = pTestFeature.Shape;
                    switch (pGeometry.GeometryType)
                    {
                        case esriGeometryType.esriGeometryPoint:
                            pointList.Add(pTestFeature);
                            pointList.Add(searchCollection[i + 1]);
                            pointList.Add(searchCollection[i + 2]);
                            break;
                        case esriGeometryType.esriGeometryPolyline:
                            lineList.Add(pTestFeature);
                            lineList.Add(searchCollection[i + 1]);
                            lineList.Add(searchCollection[i + 2]);
                            break;
                        case esriGeometryType.esriGeometryPolygon:
                            areaList.Add(pTestFeature);
                            areaList.Add(searchCollection[i + 1]);
                            areaList.Add(searchCollection[i + 2]);
                            break;
                    }
                }
                if (pointList.Count > 0)
                {
                    NewList = pointList;
                }
                else if (lineList.Count > 0)
                {
                    NewList = lineList;
                }
                else
                {
                    NewList = areaList;
                }
                int k = 0;
                for (int i = 0; i < NewList.Count; i = i + 3)
                {
                    pFea = NewList[i] as IFeature;
                    pGeometry = pFea.Shape;
                    tempDist = pProximity.ReturnDistance(pGeometry);

                    if (tempDist < SearchDist)
                    {
                        switch (pGeometry.GeometryType)
                        {
                            case esriGeometryType.esriGeometryPoint:
                                if (pointTestDistance < 0)
                                {
                                    pointTestDistance = tempDist + 1;
                                }
                                if (tempDist < pointTestDistance)
                                {
                                    pointTestDistance = tempDist;
                                    pPointFeature = pFea;
                                    k = i;
                                }
                                break;
                            case esriGeometryType.esriGeometryPolyline:
                                if (lineTestDistance < 0)
                                {
                                    lineTestDistance = tempDist + 1;
                                }
                                if (tempDist < lineTestDistance)
                                {
                                    lineTestDistance = tempDist;
                                    pLineFeature = pFea;
                                    k = i;
                                }
                                break;
                            case esriGeometryType.esriGeometryPolygon:
                                if (areaTestDistance < 0)
                                {
                                    areaTestDistance = tempDist + 1;
                                }
                                if (tempDist < areaTestDistance)
                                {
                                    areaTestDistance = tempDist;
                                    pAreaFeature = pFea;
                                    k = i;
                                }
                                break;
                        }
                    }
                    else
                    {
                        if (testDistance < 0) testDistance = tempDist + 1;
                        if (tempDist < testDistance)
                        {
                            testDistance = tempDist;
                            pFeature = pFea;
                            k = i;
                        }
                    }

                }
                if (pPointFeature != null)
                {
                    pFeature = pPointFeature;
                }
                if (pLineFeature != null)
                {
                    pFeature = pLineFeature;
                }
                if (pAreaFeature != null)
                {
                    pFeature = pAreaFeature;
                }

                IFeatureLayer pFeatureLayer = list[Convert.ToInt32(searchCollection[k + 2])] as IFeatureLayer;
                IFeatureSelection pFeatureSelection = pFeatureLayer as IFeatureSelection;
                IQueryFilter pQueryFilter = new QueryFilterClass();
                pQueryFilter.WhereClause = GIS_Const.FIELD_OBJECTID + "= " + pFeature.OID.ToString();

            }
            catch
            {
                return;
            }
        }

        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int x, int y);
        [DllImport("User32.dll")]
        private static extern bool GetCursorPos(ref System.Drawing.Point p);


        public static void SnapFeaturePoint(IFeature pFeature, IHookHelper m_hookHelper, IPoint m_pSnapPoint, int X, int Y)
        {
            if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                IPoint pPoint = (IPoint)pFeature.Shape;
                int x = 0;
                int y = 0;
                m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.FromMapPoint(pPoint, out x, out y);
                System.Drawing.Point pPointScreen = new System.Drawing.Point();
                GetCursorPos(ref pPointScreen);
                int width = x - X;
                int height = y - Y;
                pPointScreen.X = pPointScreen.X + width;
                pPointScreen.Y = pPointScreen.Y + height;
                SetCursorPos(pPointScreen.X, pPointScreen.Y);
                m_pSnapPoint = pPoint;
            }
            IPointCollection m_pPointCol;
            if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                m_pPointCol = new Polyline();
            else if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)
                m_pPointCol = new Polygon();
            else
                return;

            IPointCollection pPntCol;
            IGeometryCollection pGeoCollection = (IGeometryCollection)pFeature.Shape;

            for (int i = 0; i < pGeoCollection.GeometryCount; i++)
            {
                IGeometry pGeom = pGeoCollection.get_Geometry(i);
                pPntCol = (IPointCollection)pGeom;
                m_pPointCol.AddPointCollection(pPntCol);
            }
            IPoint pProximity = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);

            List<double> pdistance = new List<double>();
            for (int i = 0; i < m_pPointCol.PointCount; i++)
            {
                double tempDist = (m_pPointCol.Point[i].X - pProximity.X) * (m_pPointCol.Point[i].X - pProximity.X) +
                    (m_pPointCol.Point[i].Y - pProximity.Y) * (m_pPointCol.Point[i].Y - pProximity.Y);
                pdistance.Add(tempDist);

            }
            double dbD = double.MaxValue;
            int index = -1;

            for (int i = 0; i < pdistance.Count; i++)
            {
                if (pdistance[i] < dbD)
                { dbD = pdistance[i]; index = i; }
            }

            ISelectionEnvironment pSelectionEnvironment;
            pSelectionEnvironment = new SelectionEnvironmentClass();
            double dLength = pSelectionEnvironment.SearchTolerance;
            dLength = DataEditCommon.ConvertPixelDistanceToMapDistance(m_hookHelper.ActiveView, dLength);
            dLength = dLength * dLength;
            if (dbD < dLength)
            {
                m_pSnapPoint = m_pPointCol.Point[index];
                System.Drawing.Point pPointScreen = new System.Drawing.Point();
                GetCursorPos(ref pPointScreen);
                int x = 0;
                int y = 0;
                m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.FromMapPoint(m_pSnapPoint, out x, out y);
                int width = x - X;
                int height = y - Y;
                pPointScreen.X = pPointScreen.X + width;
                pPointScreen.Y = pPointScreen.Y + height;
                SetCursorPos(pPointScreen.X, pPointScreen.Y);
            }
            else
                m_pSnapPoint = null;


        }

        /// <summary>
        /// 根据几何图形添加新要素
        /// </summary>
        /// <param name="featureLayer">当前编辑图层</param>
        /// <param name="geom">要素的几何形状</param>
        public static void CreateFeature(IFeatureLayer featureLayer, IGeometry geom)
        {
            IWorkspaceEdit workspaceEdit = null;
            IFeatureCursor featureCursor = null;

            try
            {
                IFeatureClass featureClass = featureLayer.FeatureClass;

                IDataset dataset = (IDataset)featureClass;
                IWorkspace workspace = dataset.Workspace;
                workspaceEdit = workspace as IWorkspaceEdit;

                workspaceEdit.StartEditing(true);
                workspaceEdit.StartEditOperation();

                //IFeatureBuffer featureBuffer = featureClass.CreateFeatureBuffer();
                //ZMValue(featureBuffer, geom);     //几何图形Z值处理
                //featureBuffer.Shape = geom;

                ////开始插入要素
                //featureCursor = featureClass.Insert(true);
                //object featureOID = featureCursor.InsertFeature(featureBuffer);

                ////保存要素
                //featureCursor.Flush();

                IFeature feature = featureClass.CreateFeature();

                ZMValue(feature, geom);     //几何图形Z值处理
                feature.Shape = geom;

                feature.Store();

                workspaceEdit.StopEditOperation();
                workspaceEdit.StopEditing(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                workspaceEdit.AbortEditOperation();
                workspaceEdit.StopEditing(false);
            }
            finally
            {
                if (featureCursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(featureCursor);  //释放指针
                }
            }
        }
        /// <summary>
        /// 根据几何图形添加新要素(可以撤销)
        /// </summary>
        /// <param name="featureLayer">当前编辑图层</param>
        /// <param name="geom">要素的几何形状</param>
        public static IFeature CreateUndoRedoFeature(IFeatureLayer featureLayer, IGeometry geom)
        {
            try
            {
                IFeatureClass featureClass = featureLayer.FeatureClass;
                g_CurWorkspaceEdit.StartEditOperation();
                IFeature feature = featureClass.CreateFeature();
                ZMValue(feature, geom);     //几何图形Z值处理
                feature.Shape = geom;
                feature.Store();
                g_CurWorkspaceEdit.StopEditOperation();
                return feature;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                g_CurWorkspaceEdit.AbortEditOperation();
                return null;
            }
        }
        /// <summary>
        /// 根据几何图形添加新要素
        /// </summary>
        /// <param name="featureLayer">当前编辑图层</param>
        /// <param name="geom">要素的几何形状</param>
        public static IFeature CreateNewFeature(IFeatureLayer featureLayer, IGeometry geom)
        {
            IWorkspaceEdit workspaceEdit = null;
            IFeatureCursor featureCursor = null;

            try
            {
                IFeatureClass featureClass = featureLayer.FeatureClass;

                IDataset dataset = (IDataset)featureClass;
                IWorkspace workspace = dataset.Workspace;
                workspaceEdit = workspace as IWorkspaceEdit;

                workspaceEdit.StartEditing(true);
                workspaceEdit.StartEditOperation();

                IFeatureBuffer featureBuffer = featureClass.CreateFeatureBuffer();
                ZMValue(featureBuffer, geom);     //几何图形Z值处理
                featureBuffer.Shape = geom;

                //开始插入要素
                featureCursor = featureClass.Insert(true);
                object featureOID = featureCursor.InsertFeature(featureBuffer);

                //保存要素
                featureCursor.Flush();

                IFeature feature = featureCursor.NextFeature();

                workspaceEdit.StopEditOperation();
                workspaceEdit.StopEditing(true);

                return feature;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                workspaceEdit.AbortEditOperation();
                workspaceEdit.StopEditing(false);

                return null;
            }
            finally
            {
                if (featureCursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(featureCursor);  //释放指针
                }
            }
        }
        public static void CreateFeature(IFeatureClass featureClass, IGeometry geometry, List<ziduan> list)
        {
            IGeometryArray pArry = new GeometryArrayClass();
            pArry.Add(geometry);
            CreateFeature(featureClass, pArry, list);
        }
        /// <summary>
        /// 根据几何图形添加新要素
        /// </summary>
        /// <param name="featureLayer">当前编辑图层</param>
        /// <param name="geom">要素集合</param>
        public static void CreateFeature(IFeatureClass featureClass, IGeometryArray geometryArray2, List<ziduan> list)
        {
            IWorkspaceEdit workspaceEdit = null;
            IFeatureCursor featureCursor = null;

            try
            {
                IDataset dataset = (IDataset)featureClass;
                IWorkspace workspace = dataset.Workspace;
                workspaceEdit = workspace as IWorkspaceEdit;

                workspaceEdit.StartEditing(true);
                workspaceEdit.StartEditOperation();
                for (int i = 0; i < geometryArray2.Count; i++)
                {
                    IFeature feature = featureClass.CreateFeature();
                    IGeometry mGeometry = geometryArray2.get_Element(i);
                    DataEditCommon.ZMValue(feature, mGeometry);     //几何图形Z值处理
                    feature.Shape = mGeometry;
                    if (list != null)
                    {
                        for (int j = 0; j < list.Count; j++)
                        {
                            if (feature.Fields.FindField(list[j].name) > 0)
                            {
                                feature.set_Value(feature.Fields.FindField(list[j].name), list[j].value);
                            }
                        }
                    }
                    if (feature.Fields.FindField("id") > 0)
                    {
                        feature.set_Value(feature.Fields.FindField("id"), (i + 1).ToString());
                    }
                    if (feature.Fields.FindField("Contour") > 0)
                    {
                        feature.set_Value(feature.Fields.FindField("Contour"), feature.Shape.Envelope.ZMax);
                    }
                    feature.Store();
                }
                workspaceEdit.StopEditOperation();
                workspaceEdit.StopEditing(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                workspaceEdit.AbortEditOperation();
                workspaceEdit.StopEditing(false);
            }
            finally
            {
                if (featureCursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(featureCursor);  //释放指针
                }
            }
        }
        /// <summary>
        /// 根据几何图形添加新要素
        /// </summary>
        /// <param name="featureLayer">当前编辑图层</param>
        /// <param name="geom">要素集合</param>
        public static void CreateFeatureNoEditor(IFeatureClass featureClass, IGeometry geometry, List<ziduan> list)
        {
            IFeature feature = featureClass.CreateFeature();
            DataEditCommon.ZMValue(feature, geometry);     //几何图形Z值处理
            feature.Shape = geometry;
            if (list != null)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    if (feature.Fields.FindField(list[j].name) > 0)
                    {
                        feature.set_Value(feature.Fields.FindField(list[j].name), list[j].value);
                    }
                }
            }
            feature.Store();
        }
        /// <summary>
        /// 根据几何图形添加新要素
        /// </summary>
        /// <param name="featureLayer">当前编辑图层</param>
        /// <param name="geom">要素的几何形状</param>
        public static IFeature CreateNewFeature(IFeatureLayer featureLayer, IGeometry geom, List<ziduan> list)
        {
            IWorkspaceEdit workspaceEdit = null;
            try
            {
                IFeatureClass featureClass = featureLayer.FeatureClass;

                IDataset dataset = (IDataset)featureClass;
                IWorkspace workspace = dataset.Workspace;
                workspaceEdit = workspace as IWorkspaceEdit;

                workspaceEdit.StartEditing(true);
                workspaceEdit.StartEditOperation();

                IFeature feature = featureClass.CreateFeature();

                DrawCommon.HandleZMValue(feature, geom);//几何图形Z值处理
                feature.Shape = geom;

                if (list != null)
                {
                    foreach (ziduan t in list)
                    {
                        if (feature.Fields.FindField(t.name) > 0)
                        {
                            feature.set_Value(feature.Fields.FindField(t.name), t.value);
                        }
                    }
                }
                feature.Store();
                workspaceEdit.StopEditOperation();
                workspaceEdit.StopEditing(true);

                return feature;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                workspaceEdit.AbortEditOperation();
                workspaceEdit.StopEditing(false);

                return null;
            }
        }
        /// <summary>
        /// 根据几何图形添加新要素
        /// </summary>
        /// <param name="featureLayer">当前编辑图层</param>
        /// <param name="geom">要素的几何形状</param>
        public static IFeature ModifyFeature(IFeatureLayer featureLayer, int id, IGeometry geom, List<ziduan> list)
        {
            IWorkspaceEdit workspaceEdit = null;
            try
            {
                IFeatureClass featureClass = featureLayer.FeatureClass;
                var featureCursor = featureClass.Search(new QueryFilterClass { WhereClause = "bid=" + id }, false);
                var feature = featureCursor.NextFeature();
                if (feature == null)
                {
                    return CreateNewFeature(featureLayer, geom, list);
                }
                IDataset dataset = (IDataset)featureClass;
                IWorkspace workspace = dataset.Workspace;
                workspaceEdit = workspace as IWorkspaceEdit;

                workspaceEdit.StartEditing(true);
                workspaceEdit.StartEditOperation();


                DrawCommon.HandleZMValue(feature, geom);//几何图形Z值处理
                feature.Shape = geom;

                if (list != null)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (feature.Fields.FindField(list[i].name) > 0)
                        {
                            feature.Value[feature.Fields.FindField(list[i].name)] = list[i].value;
                        }
                    }
                }

                feature.Store();

                workspaceEdit.StopEditOperation();
                workspaceEdit.StopEditing(true);


                return feature;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                workspaceEdit.AbortEditOperation();
                workspaceEdit.StopEditing(false);

                return null;
            }
        }
        /// <summary>
        /// Geometry中Z值和M值处理
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="geometry"></param>
        /// <param name="zValue"></param>
        public static void ZMValue(IFeatureBuffer feature, IGeometry geometry)
        {
            //先判断图层要素是否有Z值
            int index;
            index = feature.Fields.FindField(GIS_Const.FIELD_SHAPE);
            if (index < 0)
                return;

            IGeometryDef pGeometryDef;
            pGeometryDef = feature.Fields.get_Field(index).GeometryDef as IGeometryDef;
            MakeZMValue(pGeometryDef, geometry);
        }

        /// <summary>
        /// Geometry中Z值和M值处理
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="geometry"></param>
        /// <param name="zValue"></param>
        public static void ZMValue(IFeature feature, IGeometry geometry)
        {
            //先判断图层要素是否有Z值
            int index;
            index = feature.Fields.FindField(GIS_Const.FIELD_SHAPE);
            if (index < 0)
                return;
            IGeometryDef pGeometryDef;
            pGeometryDef = feature.Fields.get_Field(index).GeometryDef as IGeometryDef;
            MakeZMValue(pGeometryDef, geometry);
        }

        private static void MakeZMValue(IGeometryDef pGeometryDef, IGeometry geometry)
        {
            try
            {
                IZAware pZAware = (IZAware)geometry;
                IMAware pMAware = (IMAware)geometry;

                if (pGeometryDef.HasZ)
                {
                    pZAware.ZAware = true;
                    //if (geometry.Envelope.ZMax.ToString() == "非数字")
                    //{
                    //    geometry.Envelope.ZMax = 0;  //将Z值设置为0  
                    //    geometry.Envelope.ZMin = 0;  //将Z值设置为0  
                    //} 
                    if (geometry.GeometryType == esriGeometryType.esriGeometryPoint)
                    {
                        IPoint pt = (IPoint)geometry;
                        if (pt.Z.ToString() == "非数字")
                            pt.Z = 0;  //将Z值设置为0     
                    }
                    else
                    {
                        IZ iz1 = (IZ)geometry;
                        if (iz1.ZMax.ToString() == "非数字" || iz1.ZMax.ToString() == "NaN")
                            iz1.SetConstantZ(0);  //将Z值设置为0    
                    }
                }
                else
                {
                    pZAware.ZAware = false;
                }

                if (pGeometryDef.HasM)
                {
                    pMAware.MAware = true;
                }
                else
                {
                    pMAware.MAware = false;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 获取捕捉编辑环境
        /// </summary>
        /// <param name="hookHelper"></param>
        /// <returns></returns>
        public static ISnappingEnvironment GetSnapEnvironment(IHookHelper hookHelper, esriSnappingType snappingType)
        {
            IHookHelper2 m_hookHelper2 = (IHookHelper2)hookHelper;
            IExtensionManager extensionManager = m_hookHelper2.ExtensionManager;
            if (extensionManager != null)
            {
                UID guid = new UIDClass();
                guid.Value = "{E07B4C52-C894-4558-B8D4-D4050018D1DA}"; //Snapping extension.
                IExtension extension = extensionManager.FindExtension(guid);
                ISnappingEnvironment snappingEnv = extension as ISnappingEnvironment;
                //snappingEnv.SnappingType = (esriSnappingType)((int)esriSnappingType.esriSnappingTypePoint + (int)esriSnappingType.esriSnappingTypeEdge + (int)esriSnappingType.esriSnappingTypeEndpoint + (int)esriSnappingType.esriSnappingTypeMidpoint + (int)esriSnappingType.esriSnappingTypeIntersection + (int)esriSnappingType.esriSnappingTypeVertex + (int)esriSnappingType.esriSnappingTypeTangent);
                snappingEnv.SnappingType = snappingType;
                return snappingEnv;
            }
            return null;
        }

        /// <summary>
        /// 根据图层名称在地图中查找对应的图层
        /// </summary>
        /// <param name="pMap">地图</param>
        /// <param name="layerName">图层名称</param>
        /// <returns></returns>
        public static ILayer GetLayerByName(IMap pMap, string layerName)
        {
            ILayer pLayer;
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                pLayer = pMap.get_Layer(i);
                if (pLayer is IGroupLayer)
                {
                    pLayer = GetLayerByName((IGroupLayer)pLayer, layerName);
                    if (pLayer != null)
                        return pLayer;
                }
                else
                {
                    if (pLayer.Name == layerName)
                        return pLayer;
                }
            }
            return null;
        }
        private static ILayer GetLayerByName(IGroupLayer pGroupLayer, string layerName)
        {
            ICompositeLayer comLayer = pGroupLayer as ICompositeLayer;
            ILayer tmpLayer;
            for (int j = 0; j <= comLayer.Count - 1; j++)
            {
                tmpLayer = comLayer.get_Layer(j);
                if (tmpLayer is IGroupLayer)
                {
                    tmpLayer = GetLayerByName((IGroupLayer)tmpLayer, layerName);
                    if (tmpLayer != null)
                        return tmpLayer;
                }
                else
                {
                    if (tmpLayer.Name == layerName)
                        return tmpLayer;
                }
            }
            return null;
        }
        /// <summary>
        /// 返回图层集合
        /// </summary>
        /// <param name="pMap">IMap</param>
        /// <param name="key">图层字符串关键字</param>
        /// <returns></returns>
        public static List<ILayer> GetLayerListByKey(IMap pMap, string key = "")
        {
            ILayer pLayer;
            List<ILayer> listLayer = new List<ILayer>();
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                pLayer = pMap.get_Layer(i);
                if (pLayer is IGroupLayer)
                {
                    List<ILayer> list = GetLayerListByKey((IGroupLayer)pLayer, key);
                    if (list.Count > 0)
                        listLayer.AddRange(list);
                }
                else
                {
                    if (pLayer.Name.Contains(key) || key == "")
                        listLayer.Add(pLayer);
                }
            }
            return listLayer;
        }
        private static List<ILayer> GetLayerListByKey(IGroupLayer pGroupLayer, string key)
        {
            ICompositeLayer comLayer = pGroupLayer as ICompositeLayer;
            ILayer tmpLayer;
            List<ILayer> listtmpLayer = new List<ILayer>();
            for (int j = 0; j <= comLayer.Count - 1; j++)
            {
                tmpLayer = comLayer.get_Layer(j);
                if (tmpLayer is IGroupLayer)
                {
                    List<ILayer> list = GetLayerListByKey((IGroupLayer)tmpLayer, key);
                    if (list.Count > 0)
                        listtmpLayer.AddRange(list);
                }
                else
                {
                    if (tmpLayer.Name.Contains(key) || key == "")
                        listtmpLayer.Add(tmpLayer);
                }
            }
            return listtmpLayer;
        }
        /// <summary>
        /// 设定图层的Visible属性，若是true则所有父图层都将设为true，若为false父图层不变
        /// </summary>
        /// <param name="pMap"></param>
        /// <param name="layerName"></param>
        /// <param name="visible"></param>
        public static void SetLayerVisibleByName(IMap pMap, string layerName, bool visible)
        {
            ILayer pLayer;
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                pLayer = pMap.get_Layer(i);
                if (pLayer is IGroupLayer)
                {
                    pLayer = SetLayerVisibleByName((IGroupLayer)pLayer, layerName, visible);
                    if (pLayer != null && visible == true)
                    {
                        pMap.get_Layer(i).Visible = true;
                    }
                }
                else
                {
                    if (pLayer.Name == layerName)
                        pLayer.Visible = visible;
                }
            }
        }
        private static ILayer SetLayerVisibleByName(IGroupLayer pGroupLayer, string layerName, bool visible)
        {
            ICompositeLayer comLayer = pGroupLayer as ICompositeLayer;
            ILayer tmpLayer;
            for (int j = 0; j <= comLayer.Count - 1; j++)
            {
                tmpLayer = comLayer.get_Layer(j);
                if (tmpLayer is IGroupLayer)
                {
                    tmpLayer = SetLayerVisibleByName((IGroupLayer)tmpLayer, layerName, visible);
                    if (tmpLayer != null && visible == true)
                    {
                        comLayer.get_Layer(j).Visible = visible;
                        return tmpLayer;
                    }
                }
                else
                {
                    if (tmpLayer.Name == layerName)
                    {
                        tmpLayer.Visible = visible;
                        return tmpLayer;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 删除指定要素
        /// </summary>
        /// <param name="feaLayer"></param>
        /// <param name="featureID"></param>
        public static bool DeleteFeatureByBId(IFeatureLayer feaLayer, string featureID)
        {
            try
            {
                DataEditCommon.g_CurWorkspaceEdit.StartEditing(false);
                DataEditCommon.g_CurWorkspaceEdit.StartEditOperation();

                //方法1：删除要素
                IQueryFilter queryFilter = new QueryFilterClass();
                queryFilter.WhereClause = GIS_Const.FIELD_BID + "='" + featureID + "'";
                //Get table and row
                ITable esriTable = (ITable)feaLayer.FeatureClass;
                esriTable.DeleteSearchedRows(queryFilter);

                DataEditCommon.g_CurWorkspaceEdit.StopEditOperation();
                DataEditCommon.g_CurWorkspaceEdit.StopEditing(true);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(queryFilter);
                g_pMyMapCtrl.ActiveView.Refresh();//刷新

                return true;

                //方法2：删除要素
                ////获得编辑工作空间
                //IDataset pDataset = null;
                //IWorkspace pWorkspace = null;
                //IWorkspaceEdit pWorkspaceEdit = null;
                //pDataset = (IDataset)feaLayer.FeatureClass;
                //pWorkspace = pDataset.Workspace;
                //pWorkspaceEdit = pWorkspace as IWorkspaceEdit;

                ////遍历图层找到对应要素
                //IFeature pFeature = null;
                //IFeatureCursor feaCursor = null;
                //IQueryFilter queryFilter = new QueryFilterClass();
                //queryFilter.WhereClause = "ID" + "='" + featureID + "'";
                //feaCursor = feaLayer.FeatureClass.Search(queryFilter, true);
                //pFeature = feaCursor.NextFeature();
                //while (pFeature != null)
                //{
                //    //int iFieldID = pFeature.Fields.FindField("ID");//图层中对应绑定ID字段
                //    //string sFieldIDValue = pFeature.get_Value(iFieldID).ToString();
                //    pFeature.Delete();
                //    ////若存在该要素，则删除此要素
                //    //if (sFieldIDValue == featureID)
                //    //{
                //    //    pWorkspaceEdit.StartEditing(false);
                //    //    pWorkspaceEdit.StartEditOperation();
                //    //    pFeature.Delete();
                //    //    pWorkspaceEdit.StopEditOperation();
                //    //    pWorkspaceEdit.StopEditing(true);

                //    //    break;
                //    //}

                //    pFeature = feaCursor.NextFeature();
                //}

                //System.Runtime.InteropServices.Marshal.ReleaseComObject(feaCursor);   

            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除指定要素
        /// </summary>
        /// <param name="feaLayer"></param>
        /// <param name="featureID"></param>
        public static bool DeleteFeatureByObjectId(IFeatureLayer feaLayer, string objId)
        {
            try
            {
                IQueryFilter queryFilter = new QueryFilterClass();
                queryFilter.WhereClause = GIS_Const.FIELD_OBJECTID + "=" + objId;
                //Get table and row
                ITable esriTable = feaLayer.FeatureClass as ITable;
                esriTable.DeleteSearchedRows(queryFilter);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(queryFilter);

                //g_pMyMapCtrl.ActiveView.Refresh();//刷新

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 删除查询条件要素
        /// </summary>
        /// <param name="feaLayer">IFeatureLayer</param>
        /// <param name="WhereClause">自定义查询条件</param>
        public static bool DeleteFeatureByWhereClause(IFeatureLayer feaLayer, string WhereClause)
        {
            try
            {
                DataEditCommon.g_CurWorkspaceEdit.StartEditing(false);
                DataEditCommon.g_CurWorkspaceEdit.StartEditOperation();
                IQueryFilter queryFilter = new QueryFilterClass();
                queryFilter.WhereClause = WhereClause;
                //Get table and row
                ITable esriTable = feaLayer.FeatureClass as ITable;
                esriTable.DeleteSearchedRows(queryFilter);
                DataEditCommon.g_CurWorkspaceEdit.StopEditOperation();
                DataEditCommon.g_CurWorkspaceEdit.StopEditing(true);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(queryFilter);

                //g_pMyMapCtrl.ActiveView.Refresh();//刷新

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 删除查询条件要素
        /// </summary>
        /// <param name="feaLayer">IFeatureLayer</param>
        /// <param name="WhereClause">自定义查询条件</param>
        public static bool DeleteFeatureByWhereClause(IFeatureClass FeatureClass, string WhereClause)
        {
            try
            {
                IDataset ds = (IDataset)FeatureClass;
                IWorkspaceEdit mWorkspaceEdit = ds.Workspace as IWorkspaceEdit;
                mWorkspaceEdit.StartEditing(false);
                mWorkspaceEdit.StartEditOperation();
                IQueryFilter queryFilter = new QueryFilterClass();
                queryFilter.WhereClause = WhereClause;
                //Get table and row
                ITable esriTable = FeatureClass as ITable;
                esriTable.DeleteSearchedRows(queryFilter);
                mWorkspaceEdit.StopEditOperation();
                mWorkspaceEdit.StopEditing(true);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(queryFilter);

                //g_pMyMapCtrl.ActiveView.Refresh();//刷新

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 字符串长度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int strLen(string str)
        {
            if (str.Length == 0) return 0;
            ASCIIEncoding ascii = new ASCIIEncoding();
            int temLen = 0;
            byte[] s = ascii.GetBytes(str);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                    temLen += 2;
                else
                    temLen += 1;
            }
            return temLen;
        }
        /// <summary>
        /// 关闭所有捕捉
        /// </summary>
        public static void CloseAllSnapAgent()
        {
            InitEditEnvironment();
            CheckEditState();
            IEngineSnapEnvironment snapEnvironment = (IEngineSnapEnvironment)g_engineEditor;
            snapEnvironment.ClearSnapAgents();
        }
        /// <summary>
        /// 线转面
        /// </summary>
        /// <param name="pPolyline"></param>
        /// <returns></returns>
        public static IPolygon PolylineToPolygon(IPolyline pPolyline)
        {
            IPolygon pPolygon = null;
            IGeometryCollection pGeometryCollection = new PolygonClass();
            if (pPolyline != null && pPolyline.IsEmpty == false)
            {
                IGeometryCollection pPolylineGeoCol = pPolyline as IGeometryCollection;
                ISegmentCollection pSegCol = new RingClass();
                ISegment pSegment = null;
                object missing = Type.Missing;
                for (int i = 0; i < pPolylineGeoCol.GeometryCount; i++)
                {
                    ISegmentCollection pPolylineSegCol = pPolylineGeoCol.get_Geometry(i) as ISegmentCollection;
                    for (int j = 0; j < pPolylineSegCol.SegmentCount; j++)
                    {
                        pSegment = pPolylineSegCol.get_Segment(j);
                        pSegCol.AddSegment(pSegment, ref missing, ref missing);
                    }
                    pGeometryCollection.AddGeometry(pSegCol as IGeometry, ref missing, ref missing);
                }
            }
            pPolygon = pGeometryCollection as IPolygon;
            if (pPolygon.IsClosed == false)
                pPolygon.Close();
            pPolygon.SpatialReference = DataEditCommon.g_pMyMapCtrl.SpatialReference;
            return pPolygon;
        }
        /// <summary>
        /// 判断线的方向，如果是逆时针，转换成顺时针
        /// </summary>
        /// <param name="pPolyline"></param>
        /// <param name="type">"Bezier,Line"</param>
        /// <returns></returns>
        public static IPolyline PDFX(IPolyline pPolyline, string type)
        {
            IPolyline rPolyline = pPolyline;
            IGeometry geo = null;
            IPointCollection pPointCollection = (IPointCollection)pPolyline;
            double s = 0;
            for (int i = 0; i < pPointCollection.PointCount - 1; i++)
            {
                s += (pPointCollection.get_Point(i + 1).X - pPointCollection.get_Point(i).X) * (pPointCollection.get_Point(i + 1).Y - pPointCollection.get_Point(i).Y) * 0.5;
            }

            if (s < 0)
            {
                if (type == "Bezier")
                {
                    INewBezierCurveFeedback pBezier = new NewBezierCurveFeedbackClass();
                    for (int i = pPointCollection.PointCount - 1; i > -1; i--)
                    {
                        if (i == pPointCollection.PointCount - 1)
                        {
                            pBezier.Start(pPointCollection.get_Point(i));
                        }
                        else if (i == 0)
                        {
                            pBezier.AddPoint(pPointCollection.get_Point(i));
                            geo = pBezier.Stop();
                        }
                        else
                            pBezier.AddPoint(pPointCollection.get_Point(i));
                    }
                    rPolyline = (IPolyline)geo;
                }
                else
                {
                    IPointCollection rPointCollection = new PolylineClass();
                    for (int i = pPointCollection.PointCount - 1; i > -1; i--)
                    {
                        rPointCollection.AddPoint(pPointCollection.get_Point(i));
                    }
                    rPolyline = (IPolyline)rPointCollection;
                }
                rPolyline.SpatialReference = pPolyline.SpatialReference;

                ISegmentCollection pSegmentCollection = rPolyline as ISegmentCollection;
                for (int i = 0; i < pSegmentCollection.SegmentCount; i++)
                {
                    IPoint pt = new PointClass();
                    IZAware mZAware = (IZAware)pt;
                    mZAware.ZAware = true;

                    pt.X = pPointCollection.get_Point(pSegmentCollection.SegmentCount - i).X;
                    pt.Y = pPointCollection.get_Point(pSegmentCollection.SegmentCount - i).Y;
                    pt.Z = pPointCollection.get_Point(pSegmentCollection.SegmentCount - i).Z;


                    IPoint pt1 = new PointClass();
                    mZAware = (IZAware)pt1;
                    mZAware.ZAware = true;
                    if (i == pSegmentCollection.SegmentCount - 1)
                    {


                        pt1.X = pPointCollection.get_Point(0).X;
                        pt1.Y = pPointCollection.get_Point(0).Y;
                        pt1.Z = pPointCollection.get_Point(0).Z;

                        pSegmentCollection.get_Segment(i).FromPoint = pt;
                        pSegmentCollection.get_Segment(i).ToPoint = pt1;
                    }
                    else
                    {
                        pt1.X = pPointCollection.get_Point(pSegmentCollection.SegmentCount - 1 - i).X;
                        pt1.Y = pPointCollection.get_Point(pSegmentCollection.SegmentCount - 1 - i).Y;
                        pt1.Z = pPointCollection.get_Point(pSegmentCollection.SegmentCount - 1 - i).Z;

                        pSegmentCollection.get_Segment(i).FromPoint = pt;
                        pSegmentCollection.get_Segment(i).ToPoint = pt1;
                    }
                }
                rPolyline = pSegmentCollection as IPolyline;
            }

            return rPolyline;
        }
        /// <summary>
        /// 保标绘文字
        /// </summary>
        /// <param name="pElement">文字Element</param>
        /// <param name="TitleContent">内容</param>
        public static IFeature SaveAnno(IElement pElement, string TitleContent)
        {
            try
            {
                ILayer pLayer = g_pLayer;
                IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
                pFeatureLayer.ScaleSymbols = true;
                IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                if (pFeatureClass.FeatureType != esriFeatureType.esriFTAnnotation)
                {
                    System.Windows.Forms.MessageBox.Show("非文字注记层！");
                    return null;
                }
                g_CurWorkspaceEdit.StartEditOperation();
                IFeature pFeature = pFeatureClass.CreateFeature();
                IAnnotationFeature pAnnoFeature = (IAnnotationFeature)pFeature;
                //pFeature.set_Value(pFeature.Fields.FindField("judge"), bz);
                //pFeature.set_Value(pFeature.Fields.FindField("bz"), TitleContent);
                pFeature.set_Value(pFeature.Fields.FindField("FONTNAME"), ((ITextElement)pElement).Symbol.Font.Name);
                pFeature.set_Value(pFeature.Fields.FindField("BOLD"), ((ITextElement)pElement).Symbol.Font.Bold);
                pFeature.set_Value(pFeature.Fields.FindField("UNDERLINE"), ((ITextElement)pElement).Symbol.Font.Underline);
                pFeature.set_Value(pFeature.Fields.FindField("Italic"), ((ITextElement)pElement).Symbol.Font.Italic);
                pAnnoFeature.Annotation = pElement;
                pFeature.Store();
                g_CurWorkspaceEdit.StopEditOperation();
                return pFeature;
            }
            catch (Exception ess)
            {
                MessageBox.Show(ess.Message);
                return null;
            }
        }
        public static IFeatureClass GetFeatureClassByName(IWorkspace mWorkSpace, string name)
        {
            IFeatureClass pclass = null;
            IEnumDataset enumData = mWorkSpace.get_Datasets(esriDatasetType.esriDTFeatureClass);
            IDataset dataset = enumData.Next();
            while (dataset != null)
            {
                if (dataset.Name.Equals(name))
                {
                    pclass = (IFeatureClass)dataset;
                    break;
                }
                dataset = enumData.Next();
            }
            return pclass;
        }
        /// <summary>
        /// 删除当前可编辑图层所有选中元素
        /// </summary>
        public static void DeleteAllSelection()
        {
            IFeature pFeature;
            IEnumFeature pEnumFeature;
            pEnumFeature = g_pAxMapControl.Map.FeatureSelection as IEnumFeature;
            IFeatureLayer feaLayer = g_pLayer as IFeatureLayer;
            if (pEnumFeature == null)
            {
                return;
            }
            pEnumFeature.Reset();
            pFeature = pEnumFeature.Next();
            if (pFeature == null)
            {
                return;
            }
            InitEditEnvironment();
            CheckEditState();
            g_engineEditor.StartOperation();
            do
            {
                int iFieldBID = pFeature.Fields.FindField(GIS_Const.FIELD_OBJECTID);//图层中对应绑定ID字段
                string sObjId = pFeature.get_Value(iFieldBID).ToString();

                DeleteFeatureByObjectId(feaLayer, sObjId);
                RefreshModifyFeature((IObject)pFeature);
                pFeature = pEnumFeature.Next();
            }
            while (pFeature != null);
            g_engineEditor.StopOperation("Delete Feature");
            g_pMap.ClearSelection();
            g_pMyMapCtrl.ActiveView.Refresh();
        }

        /// <summary>
        /// 刷新修改要素
        /// </summary>
        /// <param name="pObject"></param>
        private static void RefreshModifyFeature(IObject pObject)
        {
            IInvalidArea pRefreshArea;
            pRefreshArea = new InvalidArea();
            pRefreshArea.Display = g_pAxMapControl.ActiveView.ScreenDisplay;
            pRefreshArea.Add(pObject);
            pRefreshArea.Invalidate(-2);

        }
        /// <summary>
        /// 在地图上拾取点，自动填充textbox
        /// </summary>
        /// <param name="txtX">X坐标TextBox控件</param>
        /// <param name="txtY">Y坐标TextBox控件</param>
        public static void PickUpPoint(TextBox txtX, TextBox txtY)
        {
            if (txtX.FindForm().Owner != null && txtX.FindForm().Owner != g_pAxMapControl.FindForm())
                txtX.FindForm().Owner.WindowState = FormWindowState.Minimized;
            ICommand command = new GIS.SpecialGraphic.DrawPoint(txtX, txtY);
            command.OnCreate(g_pMyMapCtrl);
            if (command.Enabled)
                g_pAxMapControl.CurrentTool = (ITool)command;
        }
    }
}
