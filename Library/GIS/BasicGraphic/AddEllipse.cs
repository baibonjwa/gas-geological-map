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

namespace GIS
{
    /// <summary>
    /// 绘制椭圆
    /// </summary>
    [Guid("41108967-7add-45e2-8d90-c91926fa9b58")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.BasicGraphic.AddEllipse")]
    public sealed class AddEllipse : BaseTool
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
        //椭圆外包框角点
        private IPoint m_PT1;
        private IPoint m_PT2;
        private IPoint m_PT3;
        private IPoint m_PT4;
        private ILayer m_pCurrentLayer;
        private IMap m_pMap;
        private IDisplayFeedback m_pFeedback;

        private ISimpleLineSymbol m_LineSym;
        private IEllipticArc m_pEllipticArc;
        private INewEnvelopeFeedback m_pEnvFeedback;
        private IPoint m_pFirstPoint = null;
        private IPoint m_pMovePoint = null;
        private int m_lMouseDownCount;
        private bool m_bIsLineFeat;
        private bool m_bCreated;
        
        public AddEllipse()
        {
            //公共属性定义
            base.m_category = "基础图元绘制";
            base.m_caption = "绘制椭圆";
            base.m_message = "根据外包框绘制椭圆";
            base.m_toolTip = "绘制椭圆";
            base.m_name = "AddEllipse";
            try
            {
                base.m_bitmap = Resources.EditingEllipseTool16;
                base.m_cursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream(GetType(), "Resources.AddEllipse.cur"));
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
                m_pEllipticArc = null;
                m_pEnvFeedback = null;
                m_lMouseDownCount = 0;
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

            if (featureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                m_bIsLineFeat = true;
            else
                m_bIsLineFeat = false;

            m_pMap = m_hookHelper.FocusMap;
            m_lMouseDownCount = 0;
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            if (Button != 1)
                return;

            m_lMouseDownCount = m_lMouseDownCount + 1;
            if (m_lMouseDownCount == 1)
            {
                if (m_pMovePoint != null)
                    m_pFirstPoint = m_pMovePoint;
                else
                {
                    IPoint mPoint=m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                    mPoint=GIS.GraphicEdit.SnapSetting.getSnapPoint(mPoint);
                    m_pFirstPoint = mPoint;
                }
                SetNewLineFeedBack();
                m_pEnvFeedback.Start(m_pFirstPoint);
            }
            OnMouseUp();
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            m_pMovePoint = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            m_pMovePoint = GIS.GraphicEdit.SnapSetting.getSnapPoint(m_pMovePoint);
            
            if (m_lMouseDownCount == 1)
            {
                m_bCreated = false;

                IEnvelope pEnv = new EnvelopeClass();
                pEnv.UpperLeft = m_pFirstPoint;
                pEnv.LowerRight = m_pMovePoint;

                IConstructEllipticArc pEllipArc = new EllipticArcClass();
                pEllipArc.ConstructEnvelope(pEnv);
                m_pEllipticArc = pEllipArc as IEllipticArc;
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
            }
            DataEditCommon.g_pAxMapControl.Focus();
        }

        private void OnMouseUp()
        {
            if (m_lMouseDownCount != 2 ) return;
            if (m_pEllipticArc == null) return;

            m_lMouseDownCount = 0;
            ISegmentCollection pSegColl=null;
            if (m_pEllipticArc.Envelope.Width > 0.001 && m_pEllipticArc.Envelope.Height > 0.001)
            {
                m_pCurrentLayer = DataEditCommon.g_pLayer;
                IFeatureLayer featureLayer = m_pCurrentLayer as IFeatureLayer;

                if (featureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)   //创建线要素
                {
                    IPolyline pPolyline;
                    pSegColl = new PolylineClass();
                    pSegColl.AddSegment(m_pEllipticArc as ISegment);
                    pPolyline = pSegColl as IPolyline;
                    IFeature pFeature = DataEditCommon.CreateUndoRedoFeature(featureLayer, pPolyline);
                    m_hookHelper.FocusMap.SelectFeature(m_pCurrentLayer, pFeature);
                    m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics | esriViewDrawPhase.esriViewGeoSelection | esriViewDrawPhase.esriViewBackground, null, null);
                }
                if (featureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)   //创建面要素
                {
                    IPolygon pPolygon;
                    pSegColl = new PolygonClass();
                    pSegColl.AddSegment(m_pEllipticArc as ISegment);
                    pPolygon = pSegColl as IPolygon;
                    IFeature pFeature = DataEditCommon.CreateUndoRedoFeature(featureLayer, pPolygon);
                    m_hookHelper.FocusMap.SelectFeature(m_pCurrentLayer, pFeature);
                    m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics | esriViewDrawPhase.esriViewGeoSelection | esriViewDrawPhase.esriViewBackground, null, null);
                }

                //局部刷新
                IInvalidArea pInvalidArea = new InvalidAreaClass();
                pInvalidArea.Add(pSegColl);
                pInvalidArea.Display = m_hookHelper.ActiveView.ScreenDisplay;
                pInvalidArea.Invalidate((short)esriScreenCache.esriAllScreenCaches);

                m_bCreated = true;
            }
        }

        public override void Refresh(int hDC)
        {
            //刷新
            if (!m_bCreated)
                DrawArc(m_pEllipticArc);
        }

        private void DrawArc(IEllipticArc pEllipArc)
        {
            if (pEllipArc == null) return;

            try
            {
                ISegmentCollection pSegColl;
                IPolyline pPolyline;
                pSegColl = new PolylineClass();
                pSegColl.AddSegment(pEllipArc as ISegment);
                pPolyline = pSegColl as IPolyline;

                IRgbColor pColor = new RgbColor();
                pColor.Red = 0;
                pColor.Green = 0;
                pColor.Blue = 0;

                m_LineSym = new SimpleLineSymbol();
                m_LineSym.Color = pColor;
                m_LineSym.Width = 1;
                m_LineSym.Style = esriSimpleLineStyle.esriSLSSolid;

                ISymbol pSym = m_LineSym as ISymbol;
                pSym.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
                m_pEnvFeedback.Symbol = pSym;

                m_hookHelper.ActiveView.ScreenDisplay.StartDrawing(m_hookHelper.ActiveView.ScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
                m_hookHelper.ActiveView.ScreenDisplay.SetSymbol(pSym);
                m_hookHelper.ActiveView.ScreenDisplay.DrawPolyline(pPolyline);
                m_hookHelper.ActiveView.ScreenDisplay.FinishDrawing();
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// 绘制椭圆
        /// </summary>
        /// <param name="pEnvelope"></param>
        public void DrawEllipse(IEnvelope pEnvelope)
        {
            IFeatureLayer pFeatureLayer = m_pCurrentLayer as IFeatureLayer;
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            IConstructEllipticArc constructEllipticArc = new EllipticArcClass();
            constructEllipticArc.ConstructEnvelope(pEnvelope);
            IEllipticArc ellipse = constructEllipticArc as IEllipticArc;

            ISegment segment = ellipse as ISegment;
            ISegmentCollection pPolyline = new Polyline() as ISegmentCollection;
            object Missing = Type.Missing;
            pPolyline.AddSegment(segment, ref Missing, ref Missing);
            IGeometry poly = pPolyline as IGeometry;
            IFeature pCircleFeature = pFeatureClass.CreateFeature();
            pCircleFeature.Shape = poly as PolylineClass;
            pCircleFeature.Store();

            //局部刷新
            IInvalidArea pInvalidArea = new InvalidAreaClass();
            pInvalidArea.Add(poly);

            pInvalidArea.Display = m_hookHelper.ActiveView.ScreenDisplay;
            pInvalidArea.Invalidate((short)esriScreenCache.esriAllScreenCaches);

        }
        #endregion

        private void SetNewLineFeedBack()
        {
            m_pEnvFeedback = new NewEnvelopeFeedback();
            IRgbColor pColor = new RgbColor();
            pColor.Red = 0;
            pColor.Green = 0;
            pColor.Blue = 0;

            m_LineSym = new SimpleLineSymbol();
            m_LineSym.Color = pColor;
            m_LineSym.Width = 1;
            m_LineSym.Style = esriSimpleLineStyle.esriSLSSolid;

            ISymbol pSym = m_LineSym as ISymbol;
            pSym.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            m_pEnvFeedback.Symbol = pSym;

            m_hookHelper.ActiveView.ScreenDisplay.StartDrawing(m_hookHelper.ActiveView.ScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
            m_hookHelper.ActiveView.ScreenDisplay.SetSymbol(pSym);
            m_hookHelper.ActiveView.ScreenDisplay.FinishDrawing();

            m_pEnvFeedback.Display = m_hookHelper.ActiveView.ScreenDisplay;
        }
    }
}
