using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using GIS.Properties;
using GIS.Common;

namespace GIS
{
    /// <summary>
    /// 绘制矩形
    /// </summary>
    [Guid("b8566949-ed23-43b6-932a-d9a1936f1f21")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.BasicGraphic.AddRectangle")]
    public sealed class AddRectangle : BaseTool
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
        private INewEnvelopeFeedback m_pFeedback;
        public AddRectangle()
        {
            //公共属性定义
            base.m_category = "基础图元绘制"; 
            base.m_caption = "绘制矩形"; 
            base.m_message = "拉框绘制矩形"; 
            base.m_toolTip = "绘制矩形"; 
            base.m_name = "AddRectangule";  
            try
            {
                base.m_bitmap = Resources.EditingRectangleTool16;
                base.m_cursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream(GetType(), "Resources.AddRectangule.cur"));
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
                m_pFeedback = null;
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
        }
        public override bool Checked
        {
            get
            {
                return base.Checked;
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
                if (featureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolyline && featureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolygon||featureLayer.FeatureClass.FeatureType==esriFeatureType.esriFTAnnotation)
                {
                    MessageBox.Show(@"请选择线状或面状图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataEditCommon.g_pMyMapCtrl.CurrentTool = null;
                    return;
                }
            }
            m_pFeedback = null;
        }
        
        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            if (Button == 2)
                return;
           
                if (m_pFeedback == null)//点击左角点
                {
                    IPoint m_FirstPoint = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                    m_FirstPoint = GIS.GraphicEdit.SnapSetting.getSnapPoint(m_FirstPoint);
                    m_pFeedback = new NewEnvelopeFeedbackClass();
                    m_pFeedback.Display = m_hookHelper.ActiveView.ScreenDisplay;
                    m_pFeedback.Start(m_FirstPoint);
                    
                }
                else//点击右角点
                {
                    IPoint m_SecondPoint = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                    m_SecondPoint = GIS.GraphicEdit.SnapSetting.getSnapPoint(m_SecondPoint);
                    IGeometry pgeo=m_pFeedback.Stop();
                    DrawRectangular(m_pCurrentLayer, pgeo);
                    m_pFeedback = null;
                }
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            IPoint pt = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            pt = GIS.GraphicEdit.SnapSetting.getSnapPoint(pt);
            if (m_pFeedback != null)
            {
                m_pFeedback.MoveTo(pt);
                DataEditCommon.g_pAxMapControl.Focus();
            }
            
        }
        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            
        }
        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="pScreenDisplay"></param>
        private void DrawRectangular(ILayer pLayer, IGeometry pGeo)
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
                        pSegmentCollection.SetRectangle(pGeo.Envelope);
                        IFeature pFeature=DataEditCommon.CreateUndoRedoFeature(pFeatureLayer, (IGeometry)pSegmentCollection);
                        m_hookHelper.FocusMap.SelectFeature(m_pCurrentLayer, pFeature);
                        m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics | esriViewDrawPhase.esriViewGeoSelection|esriViewDrawPhase.esriViewBackground, null, null);
                    }
                }
            }
        }


        #endregion
    }
}
