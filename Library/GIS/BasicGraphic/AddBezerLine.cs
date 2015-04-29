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
    /// 绘制样条线
    /// </summary>
    [Guid("de2f4006-34ce-4c1c-9a34-705ae466b63d")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.BasicGraphic.AddBezerLine")]
    public sealed class AddBezerLine : BaseTool
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
        private IDisplayFeedback m_pFeedback;
        private IPoint m_PT1;
        private IPoint m_PT2;
        private IPoint m_PT3;
        private IPoint m_PT4;

        //捕捉相关
        ISnappingEnvironment m_SnappingEnv;
        IPointSnapper m_Snapper;
        ISnappingFeedback m_SnappingFeedback;
        public AddBezerLine()
        {
            //公共属性定义
            base.m_category = "基础图元绘制";
            base.m_caption = "绘制样条线";
            base.m_message = "绘制贝塞尔样条曲线";
            base.m_toolTip = "绘制样条线";
            base.m_name = "AddBezerLine";
            try
            {
                base.m_bitmap = Resources.EditingFreehandTool16;
                base.m_cursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream(GetType(), "Resources.AddBezerLine.cur"));
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
            if (Button == 2)
                return;
            INewLineFeedback pLineFeed;

            if (m_PT1 == null)
            {
                m_PT1 = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                m_PT1 = GIS.GraphicEdit.SnapSetting.getSnapPoint(m_PT1);
                m_pFeedback = new NewLineFeedbackClass();
                pLineFeed = (INewLineFeedback)m_pFeedback;
                pLineFeed.Start(m_PT1);
                if (m_pFeedback != null)
                    m_pFeedback.Display = m_hookHelper.ActiveView.ScreenDisplay;

            }
            else if (m_PT2 == null)
            {
                m_PT2 = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                m_PT2 = GIS.GraphicEdit.SnapSetting.getSnapPoint(m_PT2);
                pLineFeed = (INewLineFeedback)m_pFeedback;
                pLineFeed.AddPoint(m_PT2);
            }
            else if (m_PT3 == null)
            {
                m_PT3 = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                m_PT3 = GIS.GraphicEdit.SnapSetting.getSnapPoint(m_PT3);
                pLineFeed = (INewLineFeedback)m_pFeedback;
                pLineFeed.AddPoint(m_PT3);
            }
            else if (m_PT4 == null)
            {
                m_PT4 = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                m_PT4 = GIS.GraphicEdit.SnapSetting.getSnapPoint(m_PT4);
                pLineFeed = (INewLineFeedback)m_pFeedback;
                pLineFeed.AddPoint(m_PT4);

                pLineFeed.Stop();
                IBezierCurveGEN bezier = CreateBazerCurve();
                DrawCircleByCenterAndRadius(m_pCurrentLayer, bezier, m_hookHelper.ActiveView.ScreenDisplay);

                m_PT1 = null;
                m_PT2 = null;
                m_PT3 = null;
                m_PT4 = null;
                m_pFeedback = null;
            }
        }

        private void DrawCircleByCenterAndRadius(ILayer pLayer, IBezierCurveGEN pCircularArc, IScreenDisplay pScreenDisplay)
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

                        m_pMap.ClearSelection();

                        IFeature pCircleFeature = pFeatureClass.CreateFeature();
                        pCircleFeature.Shape = pPolyline as PolylineClass;
                        pCircleFeature.Store();

                        m_pMap.SelectFeature(m_pCurrentLayer, pCircleFeature);

                        IActiveView pActiveView = (IActiveView)m_pMap;
                        m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
                        //局部刷新
                        IInvalidArea pInvalidArea = new InvalidAreaClass();
                        pInvalidArea.Add(pPolyline);

                        pInvalidArea.Display = m_hookHelper.ActiveView.ScreenDisplay;
                        pInvalidArea.Invalidate((short)esriScreenCache.esriAllScreenCaches);
                    }
                }
            }
        }
        private IBezierCurveGEN CreateBazerCurve()
        {
            IPoint[] controlPoints = new IPoint[4];
            //Bezier FromPoint / From Tangent FromPoint. 
            controlPoints[0] = m_PT1;
            //From Tangent ToPoint. 
            controlPoints[1] = m_PT2;
            //To Tangent FromPoint. 
            controlPoints[2] = m_PT3;
            //Bezier To Point / To Tangent ToPoint. 
            controlPoints[3] = m_PT4;
            //Define the Bezier control points. This is a simple s-curve. 
            //controlPoints[0].PutCoords(0, 100);
            //controlPoints[1].PutCoords(100, 100);
            //controlPoints[2].PutCoords(0, 0);
            //controlPoints[3].PutCoords(100, 0);
            //Create the Bezier curve.
            IBezierCurveGEN bezier = new BezierCurve();
            bezier.PutCoords(ref controlPoints);
            //Get all control points from the Bezier curve. 
            bezier.QueryCoords(ref controlPoints);
            //Get each control point individually. 
            bezier.QueryCoord(0, controlPoints[0]);
            bezier.QueryCoord(1, controlPoints[1]);
            bezier.QueryCoord(2, controlPoints[2]);
            bezier.QueryCoord(3, controlPoints[3]);
            //Change the control points to define a simple CircularArc-like curve. 
            //controlPoints[0].PutCoords(-100, 200);
            //controlPoints[1].PutCoords(-100, 100);
            //Replace individual control points. 
            bezier.PutCoord(0, controlPoints[0]);
            bezier.PutCoord(1, controlPoints[1]);
            return bezier;

        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            IPoint pMovePt = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            pMovePt = GIS.GraphicEdit.SnapSetting.getSnapPoint(pMovePt);
            if (m_pFeedback != null)
            {
                m_pFeedback.MoveTo(pMovePt);
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {

        }
        #endregion
    }
}
