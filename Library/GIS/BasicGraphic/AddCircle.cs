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
using GIS.Properties;
using GIS.Common;

namespace GIS
{
    /// <summary>
    /// 绘制圆
    /// </summary>
    [Guid("614eb525-2b88-4209-89e1-d61db0ea5186")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.BasicGraphic.AddCircle")]
    public sealed class AddCircle : BaseTool
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
        private ILayer m_pCurrentLayer;
        private IMap m_pMap;
        private bool m_IsFirstPoint;
        private IPoint m_FirstPoint;
        private IPoint m_SecondPoint;
        private IDisplayFeedback m_pFeedback;

        private INewCircleFeedback m_pNewCircleFeedback;
        private IScreenDisplay m_pScrD;
        private IPoint m_pPoint;


        public AddCircle()
        {
            //公共属性定义
            base.m_category = "基础图元绘制";
            base.m_caption = "绘制圆";
            base.m_message = "根据圆心和半径绘制圆";
            base.m_toolTip = "绘制圆";
            base.m_name = "AddCircle";
            try
            {
                base.m_bitmap = Resources.EditingCircleTool16;
                base.m_cursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream(GetType(), "Resources.Cross.cur"));
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
        }
        public override void OnKeyDown(int keyCode, int Shift)
        {
            if (keyCode == (int)Keys.Escape)
            {
                m_pNewCircleFeedback = null;
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
        }
        public override bool Enabled
        {
            get
            {
                IFeatureLayer featureLayer = DataEditCommon.g_pLayer as IFeatureLayer;
                if (featureLayer == null)
                {
                    return false;
                }
                else
                {
                    if (featureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolyline && featureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolygon || featureLayer.FeatureClass.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public override bool Checked
        {
            get
            {
                return base.Checked;
            }
        }
        /// <summary>
        /// 点击事件
        /// </summary>
        public override void OnClick()
        {
            DataEditCommon.InitEditEnvironment();
            DataEditCommon.CheckEditState();
            m_pCurrentLayer = DataEditCommon.g_pLayer;
            IFeatureLayer featureLayer = m_pCurrentLayer as IFeatureLayer;
            if (featureLayer == null)
            {
                MessageBox.Show(@"请选择绘制图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataEditCommon.g_pMyMapCtrl.CurrentTool = null;
                return;
            }
            else
            {
                if (featureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolyline &&
                    featureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolygon || featureLayer.FeatureClass.FeatureType == esriFeatureType.esriFTAnnotation)
                {
                    MessageBox.Show(@"请选择线状图层或面状图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataEditCommon.g_pMyMapCtrl.CurrentTool = null;
                    return;
                }
            }

            m_pMap = m_hookHelper.FocusMap;
            m_pScrD = m_hookHelper.ActiveView.ScreenDisplay;
            m_pNewCircleFeedback = null;
        }
        
        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            if (Button == 2) return;
            m_pPoint = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            m_pPoint = GIS.GraphicEdit.SnapSetting.getSnapPoint(m_pPoint);
            if (m_pNewCircleFeedback == null)
            {
                m_pNewCircleFeedback = new NewCircleFeedback();
                m_pNewCircleFeedback.Display = m_pScrD;
                m_pNewCircleFeedback.Start(m_pPoint);
            }
            else
            {
                ICircularArc pCircuArc;
                pCircuArc = m_pNewCircleFeedback.Stop();
                if (pCircuArc != null && pCircuArc.Radius > 0.001)
                {
                    m_pCurrentLayer = DataEditCommon.g_pLayer;
                    DrawCircleFeature(m_pCurrentLayer, pCircuArc, m_pScrD);
                }
                m_pNewCircleFeedback = null;
            }
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            IPoint pPnt = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            pPnt = GIS.GraphicEdit.SnapSetting.getSnapPoint(pPnt);
            if (m_pNewCircleFeedback != null)
            {
                m_pNewCircleFeedback.MoveTo(pPnt);
                DataEditCommon.g_pAxMapControl.Focus();
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            //if (Button != 1)
            //    return;
            //if (m_pNewCircleFeedback != null)
            //{
            //    ICircularArc pCircuArc;
            //    pCircuArc = m_pNewCircleFeedback.Stop();
            //    if (pCircuArc != null && pCircuArc.Radius > 0.001)
            //    {
            //        m_pCurrentLayer = DataEditCommon.g_pLayer;
            //        DrawCircleFeature(m_pCurrentLayer, pCircuArc, m_pScrD);
            //    }
            //}
            //m_pNewCircleFeedback = null;
        }

        /// <summary>
        /// 根据绘制的圆形创建要素
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="pCircuArc"></param>
        /// <param name="pScreenDisplay"></param>
        private void DrawCircleFeature(ILayer pLayer, ICircularArc pCircuArc, IScreenDisplay pScreenDisplay)
        {
            if (pLayer == null) return;
            ISegmentCollection pSegmentCollection = null;
            if (pLayer is IFeatureLayer)
            {
                IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                if (pFeatureClass != null)
                {
                    IPolyline pPolyline = null;
                    IPolygon pPolygon = null;

                    if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        pSegmentCollection = new PolylineClass();
                        pSegmentCollection.AddSegment(pCircuArc as ISegment);
                        pPolyline = pSegmentCollection as IPolyline;
                    }
                    else if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        pSegmentCollection = new PolygonClass();
                        pSegmentCollection.AddSegment(pCircuArc as ISegment);
                        pPolygon = pSegmentCollection as IPolygon;
                    }
                    else
                        return;
                    IFeature pFeature = null;
                    if (pPolyline != null)
                        pFeature = DataEditCommon.CreateUndoRedoFeature(pFeatureLayer, pPolyline);
                    else if (pPolygon != null)
                        pFeature = DataEditCommon.CreateUndoRedoFeature(pFeatureLayer, pPolygon);
                    else
                        return;
                    m_hookHelper.FocusMap.SelectFeature(m_pCurrentLayer, pFeature);
                    m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics | esriViewDrawPhase.esriViewGeoSelection | esriViewDrawPhase.esriViewBackground, null, null);
                }
            }
        }

        /// <summary>
        /// 根据圆心和半径绘制圆
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="pPoint"></param>
        /// <param name="circleRadius"></param>
        /// <param name="pScreenDisplay"></param>
        private void DrawCircleByCenterAndRadius(ILayer pLayer, IPoint pPoint, double circleRadius, IScreenDisplay pScreenDisplay)
        {
            if (pLayer != null)
            {
                ISegmentCollection pSegmentCollection = null;
                if (pLayer is IFeatureLayer)
                {
                    IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                    IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                    if (pFeatureClass != null)
                    {
                        if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                        {
                            pSegmentCollection = new PolylineClass();
                        }
                        else if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                        {
                            pSegmentCollection = new PolygonClass();
                        }
                        //开始画圆
                        pSegmentCollection.SetCircle(pPoint, circleRadius);

                        IFeature pCircleFeature = pFeatureClass.CreateFeature();
                        pCircleFeature.Shape = pSegmentCollection as IGeometry;
                        pCircleFeature.Store();
                        //局部刷新
                        IInvalidArea pInvalidArea = new InvalidAreaClass();
                        pInvalidArea.Add(pSegmentCollection);
                        pInvalidArea.Display = pScreenDisplay;
                        pInvalidArea.Invalidate((short)esriScreenCache.esriAllScreenCaches);
                    }
                }
            }
        }
        #endregion
    }
}
