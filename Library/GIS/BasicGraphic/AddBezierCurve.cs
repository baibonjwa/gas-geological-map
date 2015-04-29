using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using GIS.Properties;
using GIS.Common;

namespace GIS
{
    /// <summary>
    /// 绘制样条线
    /// </summary>
    [Guid("16b39064-852a-4dad-8138-db44f36da01c")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.BasicGraphic.AddBezierCurve")]
    public sealed class AddBezierCurve : BaseTool
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
        private INewBezierCurveFeedback m_newBezierCurveFeedback = null;
        private ILayer m_pCurrentLayer;

        public AddBezierCurve()
        {
            //公共属性定义
            base.m_category = "基础图元绘制";
            base.m_caption = "绘制样条线";
            base.m_message = "绘制贝塞尔样条曲线";
            base.m_toolTip = "绘制样条线";
            base.m_name = "AddBezierCurve";
            try
            {
                base.m_bitmap = Resources.EditingFreehandTool16;
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), "Resources." + GetType().Name + ".cur");
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
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            IPoint pt = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            pt = GIS.GraphicEdit.SnapSetting.getSnapPoint(pt);
            if (m_newBezierCurveFeedback == null)
            {
                m_newBezierCurveFeedback = new NewBezierCurveFeedbackClass();
                m_newBezierCurveFeedback.Display = m_hookHelper.ActiveView.ScreenDisplay;
                m_newBezierCurveFeedback.Start(pt);
            }
            else
            {
                m_newBezierCurveFeedback.AddPoint(pt);
            }
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            IPoint pt = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            pt = GIS.GraphicEdit.SnapSetting.getSnapPoint(pt);
            if (m_newBezierCurveFeedback != null)
            {
                m_newBezierCurveFeedback.MoveTo(pt);
                DataEditCommon.g_pAxMapControl.Focus();
            }
            
        }
        public override bool Checked
        {
            get
            {
                return base.Checked;
            }
        }
        public override void OnDblClick()
        {
            IGeometry pGeometry;
            pGeometry = m_newBezierCurveFeedback.Stop();
            IActiveView pActiveView = m_hookHelper.ActiveView;
            m_newBezierCurveFeedback = null;
            IFeatureLayer pFeatureLayer = m_pCurrentLayer as IFeatureLayer;
            IPolyline polyline = new PolylineClass();
            polyline = (IPolyline)pGeometry;
            IFeature pFeature= DataEditCommon.CreateUndoRedoFeature(pFeatureLayer, polyline);
            m_hookHelper.FocusMap.SelectFeature(pFeatureLayer, pFeature);
            m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection | esriViewDrawPhase.esriViewBackground, null, null);
        }
        public override void OnKeyDown(int keyCode, int Shift)
        {
            if (keyCode == (int)Keys.Escape)
            {
                m_newBezierCurveFeedback = null;
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
        }
        #endregion
    }
}
