using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using System.Collections;
using ESRI.ArcGIS.esriSystem;
using GIS.Properties;

namespace GIS
{
    /// <summary>
    /// 属性查询
    /// </summary>
    [Guid("dff5f6e0-3f5a-42a2-ac5b-d85eb26fb33a")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.GraphicEdit.AttributeQueryEdit")]
    public sealed class AttributeQueryEdit : BaseTool
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);
            ControlsCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);
            ControlsCommands.Unregister(regKey);
        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper = null;
        private IActiveView m_pActiveView;
        private FeatureAttribute m_pfrmFeatureAttribute;
        private IMap m_map;

        public AttributeQueryEdit()
        {
            //公共属性值定义
            base.m_category = "编辑"; 
            base.m_caption = "属性查询";  
            base.m_message = "选择要素查询编辑属性";  
            base.m_toolTip = "属性查询";  
            base.m_name = "AttributeQueryEdit";   //唯一id
            try
            {
                base.m_bitmap = Resources.GenericInformation16;
                base.m_cursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream(GetType(), "Resources.AttributeQueryEdit.cur"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// 创建工具
        /// </summary>
        /// <param name="hook">程序实例</param>
        public override void OnCreate(object hook)
        {
            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                if (m_hookHelper.ActiveView == null)
                {
                    m_hookHelper = null;
                }
            }
            catch
            {
                m_hookHelper = null;
            }

            if (m_hookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;

            //初始化变量
            m_map = m_hookHelper.FocusMap;
            m_pActiveView = m_hookHelper.ActiveView;
        }

        public override void OnClick()
        {

        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {            
            if (m_pActiveView == null || Button != 1)
            {
                return;
            }
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
            pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            pPoint = GIS.GraphicEdit.SnapSetting.getSnapPoint(pPoint);
            pGeometry = pPoint;
            dLength = Common.DataEditCommon.ConvertPixelDistanceToMapDistance(m_pActiveView, dLength);
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

            //for (int i = 0; i < pMap.LayerCount; i++)
            //{
                //if (pMap.get_Layer(i).Name !=Common.DataEditCommon.g_pLayer.Name) continue;//20140216 lyf 只对当前选择图层进行

                //if (pMap.get_Layer(i).Visible == false || !(pMap.get_Layer(i) is IFeatureLayer))
                //{
                //    continue;
                //}
                if (Common.DataEditCommon.g_pLayer == null) return;
                pFeatureLayer = Common.DataEditCommon.g_pLayer as IFeatureLayer;
                pFeatureClass = pFeatureLayer.FeatureClass;
                if (pFeatureClass == null)
                {
                    return;
                }
                IIdentify2 pID = pFeatureLayer as IIdentify2;
                IArray pArray = pID.Identify(pSrchEnv, null);

                //20140216 lyf 没有选中图元情况处理
                if (pArray == null)
                {
                    MessageBox.Show(@"未选中当前图层的图元！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //continue;
                    return;
                }

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
                            pSelected.Add(0);
                        }
                    }
                    pArray.RemoveAll();
                }
            //}
               
            GetClosestFeatureInCollection(dLength, pSelected, pPoint, ref pFeat);
            if (pFeat != null)
            {
                if (m_pfrmFeatureAttribute == null)
                {
                    m_pfrmFeatureAttribute = new FeatureAttribute(pFeat);
                    m_pfrmFeatureAttribute.StartPosition = FormStartPosition.CenterParent;
                    m_pfrmFeatureAttribute.Show();
                }
                else
                {
                    m_pfrmFeatureAttribute.Close();
                    m_pfrmFeatureAttribute.Dispose();
                    m_pfrmFeatureAttribute = new FeatureAttribute(pFeat);
                    m_pfrmFeatureAttribute.StartPosition = FormStartPosition.CenterParent;
                    m_pfrmFeatureAttribute.Show();
                }

            }
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            IPoint pPoint = m_pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            pPoint = GIS.GraphicEdit.SnapSetting.getSnapPoint(pPoint);
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
        }

        /// <summary>
        /// 获得最近的要素几何 
        /// </summary>
        /// <param name="SearchDist">搜索距离</param>
        /// <param name="searchCollection">搜索集</param>
        /// <param name="pPoint">点击的点</param>
        /// <param name="pFeature">获得要素</param>
        private void GetClosestFeatureInCollection(double SearchDist, ArrayList searchCollection, IPoint pPoint, ref IFeature pFeature)
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

                m_map.ClearSelection();
                //IFeatureLayer pFeatureLayer = m_map.get_Layer(Convert.ToInt32(searchCollection[k + 2])) as IFeatureLayer;
                IFeatureLayer pFeatureLayer = GIS.Common.DataEditCommon.g_pLayer as IFeatureLayer;
                IFeatureSelection pFeatureSelection = pFeatureLayer as IFeatureSelection;
                IQueryFilter pQueryFilter = new QueryFilterClass();
                pQueryFilter.WhereClause = "OBJECTID " + "= " + pFeature.OID.ToString();
                pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, false);
                m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// 图形闪烁
        /// </summary>
        /// <param name="pDisplay">显示设备</param>
        /// <param name="pGeometry">显示图形</param>
        /// <param name="nTimer">闪烁次数</param>
        /// <param name="time">系统线程暂停时间</param>
        private void FlashPolygon(IScreenDisplay pDisplay, IGeometry pGeometry, int nTimer, int time)
        {
            ISimpleFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            IRgbColor pRGBColor = new RgbColorClass();
            pRGBColor.Red = 255;
            pRGBColor.Green = 69;
            pRGBColor.Blue = 0;
            pFillSymbol.Outline = null;
            pFillSymbol.Color = pRGBColor;
            ISymbol pSymbol = (ISymbol)pFillSymbol;
            pSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            pDisplay.StartDrawing(0, (short)esriScreenCache.esriNoScreenCache);
            pDisplay.SetSymbol(pSymbol);
            for (int i = 0; i <= nTimer; i++)
            {
                pDisplay.DrawPolygon(pGeometry);
                System.Threading.Thread.Sleep(time);
            }
            pDisplay.FinishDrawing();
        }
        #endregion
    }
}
