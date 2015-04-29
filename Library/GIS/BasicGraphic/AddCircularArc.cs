using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using GIS.Properties;
using GIS.Common;

namespace GIS.BasicGraphic
{
    /// <summary>
    /// 绘制圆弧
    /// </summary>
    [Guid("003c6628-855f-46ca-a4cf-c9ee63bc9462")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.BasicGraphic.AddCircularArc")]
    public sealed class AddCircularArc : BaseTool
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
        private IPoint m_point_Center;
        private IPoint m_point_From;
        private IPoint m_point_To;
        private ILayer m_pCurrentLayer;
        private IMap m_pMap;
        private IDisplayFeedback m_pFeedback;

        public AddCircularArc()
        {
            //公共属性定义
            base.m_category = "基础图元绘制"; 
            base.m_caption = "绘制圆弧";  
            base.m_message = "根据圆心和半径绘制圆弧";  
            base.m_toolTip = "绘制圆弧";  
            base.m_name = "AddCircularArc";   
            try
            {
                base.m_bitmap = Resources.EditingMidpointArcSegmentTool16;
                base.m_cursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream(GetType(), "Resources.AddCircularArc.cur"));
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
                    if (featureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolyline)
                    {
                        return false;
                    }
                }
                return true;
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
                if (featureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolyline)
                {
                    MessageBox.Show(@"请选择线状图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataEditCommon.g_pMyMapCtrl.CurrentTool = null;
                    return;
                }
            }

            m_pMap = m_hookHelper.FocusMap;
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            if (Button == 2) return;

            INewLineFeedback pLineFeed;
            if (m_point_From == null)
            { 
                m_point_From = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);

                //拖拽效果
                m_pFeedback = new NewLineFeedbackClass();
                pLineFeed = (INewLineFeedback)m_pFeedback;
                pLineFeed.Start(m_point_From);
                if (m_pFeedback != null)
                    m_pFeedback.Display = m_hookHelper.ActiveView.ScreenDisplay;
            }
            else if (m_point_To == null)
            { 
                m_point_To = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                //添加拖拽点
                pLineFeed = (INewLineFeedback)m_pFeedback;
                pLineFeed.AddPoint(m_point_To);
            }
            else if (m_point_Center == null)
            {
                m_point_Center = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                //添加拖拽点
                pLineFeed = (INewLineFeedback)m_pFeedback;
                pLineFeed.AddPoint(m_point_Center);
                pLineFeed.Stop();

                ESRI.ArcGIS.Geometry.ICircularArc circularArc = new ESRI.ArcGIS.Geometry.CircularArcClass();
                circularArc.PutCoords(m_point_Center, m_point_From, m_point_To, ESRI.ArcGIS.Geometry.esriArcOrientation.esriArcClockwise);
                DrawCircleByCenterAndRadius(m_pCurrentLayer, circularArc, m_hookHelper.ActiveView.ScreenDisplay);
                //画完后清空
                m_point_From = null;
                m_point_To = null;
                m_point_Center = null;
            }
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            if (m_pFeedback != null)
            {
                m_pFeedback.MoveTo(m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y));
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {

        }

        private void DrawCircleByCenterAndRadius(ILayer pLayer, ICircularArc pCircularArc, IScreenDisplay pScreenDisplay)
        {
            object o = Type.Missing;
            if (pLayer != null)
            {
                ISegmentCollection pSegmentCollection = null;
                pSegmentCollection = new PathClass();
                if (pLayer is IFeatureLayer)
                {
                    IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                    IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                    if (pFeatureClass != null)
                    {

                        ISegment pSegment = pCircularArc as ISegment;
                        pSegmentCollection.AddSegment(pSegment, ref o, ref o);
                        IGeometryCollection pPolyline = new PolylineClass();
                        //通过IGeometryCollection为Polyline对象添加Path对象
                        pPolyline.AddGeometry(pSegmentCollection as IGeometry, ref o, ref o);
                        IFeature pCircleFeature = pFeatureClass.CreateFeature();
                        pCircleFeature.Shape = pPolyline as PolylineClass;
                        pCircleFeature.Store();

                        //局部刷新
                        IInvalidArea pInvalidArea = new InvalidAreaClass();
                        pInvalidArea.Add(pCircularArc);
                        pInvalidArea.Display = pScreenDisplay;
                        pInvalidArea.Invalidate((short)esriScreenCache.esriAllScreenCaches);

                        //20140410 lyf
                        m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
                    }
                }
            }
        }

        #endregion
    }
}
