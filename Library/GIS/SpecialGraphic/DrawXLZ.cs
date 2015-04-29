using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using GIS.Properties;
using ESRI.ArcGIS.Geodatabase;
using GIS.Common;
using ESRI.ArcGIS.Geometry;

namespace GIS.SpecialGraphic
{
    /// <summary>
    /// 画陷落柱
    /// </summary>
    [Guid("7d203e2c-1b68-43df-a0ca-8853884cf979")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.SpecialGraphic.DrawXLZ")]
    public sealed class DrawXLZ : BaseTool
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
        private IMap m_pMap;
        private IPoint fPoint;//第一点
        
        public DrawXLZ()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text 
            base.m_caption = "画陷落柱";  //localizable text 
            base.m_message = "画陷落柱";  //localizable text
            base.m_toolTip = "";  //localizable text
            base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            try
            {
                //
                // TODO: change resource name if necessary
                //
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
        public override void OnKeyDown(int keyCode, int Shift)
        {
            if (keyCode == (int)Keys.Escape)
            {
                m_newBezierCurveFeedback = null;
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
        }
        /// <summary>
        /// 点击事件
        /// </summary>
        public override void OnClick()
        {
            DataEditCommon.InitEditEnvironment();
            DataEditCommon.CheckEditState();

            m_pCurrentLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap,LayerNames.LAYER_ALIAS_MR_XianLuoZhu1);
            IFeatureLayer featureLayer = m_pCurrentLayer as IFeatureLayer;
            if (featureLayer == null)
            {
                MessageBox.Show(@"陷落柱图层丢失！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataEditCommon.g_pMyMapCtrl.CurrentTool = null;
                return;
            }
            else
            {
                if (featureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolygon)
                {
                    MessageBox.Show(@"陷落柱图层丢失！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataEditCommon.g_pMyMapCtrl.CurrentTool = null;
                    return;
                }
            }

            m_pMap = m_hookHelper.FocusMap;
           
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
                fPoint = pt;
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
            }
            DataEditCommon.g_pAxMapControl.Focus();
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {

        }

        public override void OnDblClick()
        {
            IGeometry pGeometry;
            if (m_newBezierCurveFeedback != null)
            {
                m_newBezierCurveFeedback.AddPoint(fPoint);
            }
            pGeometry = m_newBezierCurveFeedback.Stop();
            IActiveView pActiveView = m_hookHelper.ActiveView;
            m_newBezierCurveFeedback = null;
            IPolyline polyline = new PolylineClass();
            polyline = (IPolyline)pGeometry;
                //polyline = DataEditCommon.PDFX(polyline, "Bezier");
            IPointCollection pointCollection = (IPointCollection)polyline;
            if (pointCollection.PointCount < 4)
            {
                MessageBox.Show("关键点不能小于3个！");
                return;
            }
            //DrawFeatureByShape(m_pCurrentLayer, pGeometry);
            CollapsePillarsEntering form = new CollapsePillarsEntering(pointCollection);
            form.ShowDialog();
        }

        /// <summary>
        /// 根据图形绘制要素
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="pGeometry"></param>
        private void DrawFeatureByShape(ILayer pLayer, IGeometry pGeometry)
        {
            if (pLayer != null)
            {
                if (pLayer is IFeatureLayer)
                {
                    IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                    IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                    if (pFeatureClass != null)
                    {
                        DataEditCommon.g_CurWorkspaceEdit.StartEditOperation();
                        IFeature pCircleFeature = pFeatureClass.CreateFeature();
                        IPolyline polyline = new PolylineClass();
                        polyline = (IPolyline)pGeometry;

                        pCircleFeature.Shape = polyline;
                        pCircleFeature.Store();
                        DataEditCommon.g_CurWorkspaceEdit.StopEditOperation();
                        m_pMap.SelectFeature(m_pCurrentLayer, pCircleFeature);

                        IActiveView pActiveView = (IActiveView)m_pMap;
                        m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);

                        ///要素闪烁1.2：需要符号
                        ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbolClass();
                        pSimpleLineSymbol.Color = GetRGBColor(255, 0, 0);
                        pSimpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
                        pSimpleLineSymbol.Width = 8;
                        DataEditCommon.g_pAxMapControl.FlashShape(polyline, 40, 300, pSimpleLineSymbol);
                    }
                }
            }
        }

        /// <summary>
        /// 颜色转换
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <returns></returns>
        private IRgbColor GetRGBColor(int red, int green, int blue)
        {
            IRgbColor rGBColor = new RgbColorClass();
            rGBColor.Red = red;
            rGBColor.Green = green;
            rGBColor.Blue = blue;
            return rGBColor;
        }
        #endregion
    }
}
