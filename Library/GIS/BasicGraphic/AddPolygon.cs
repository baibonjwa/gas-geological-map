using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using GIS.Properties;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Carto;
using GIS.Common;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using System.Reflection;
using ESRI.ArcGIS.esriSystem;

namespace GIS.BasicGraphic
{
    /// <summary>
    /// 绘制面（填充图案）
    /// </summary>
    [Guid("aa623cf7-34fc-48f6-bd9a-1c0a4ec158c4")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.BasicGraphic.AddPolygon")]
    public sealed class AddPolygon : BaseTool
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
        private IMapControl3 m_pMapControl;
        private ILayer m_pCurrentLayer;
        private IFeatureLayer pFeatureLayer;
        private IMap m_pMap;
        private IDisplayFeedback m_pFeedback;
        private bool m_bInUse;
        private IPointCollection m_pPointCollection;
        private IGeometryCollection m_GeometryCollection = null;
        private IArray m_ElementArray = new ArrayClass();
        private bool m_IsFirstPoint;

        public AddPolygon()
        {
            //公共属性定义
            base.m_category = "基础图元绘制"; 
            base.m_caption = "绘制多边形";
            base.m_message = "绘制多边形";
            base.m_toolTip = "绘制多边形";  
            base.m_name = "AddPolygon";   
            try
            {
                base.m_bitmap = Resources.EditingPolygonTool16;
                base.m_cursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream(GetType(), "Resources.AddPolygon.cur"));
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

        public override void OnKeyDown(int keyCode, int Shift)
        {
            if (keyCode == (int)Keys.Escape)
            {
                m_IsFirstPoint = true;
                m_pFeedback = null;
                m_bInUse = false;
                m_GeometryCollection = null;
                m_pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
        }
        /// <summary>
        /// 点击事件
        /// </summary>
        public override void OnClick()
        {
            m_pMapControl = DataEditCommon.g_pMyMapCtrl;

            DataEditCommon.InitEditEnvironment();
            DataEditCommon.CheckEditState();
            ///获得编辑目标图层            
            m_pCurrentLayer = DataEditCommon.g_pLayer;
            pFeatureLayer = m_pCurrentLayer as IFeatureLayer;
            if (pFeatureLayer == null)
            {
                MessageBox.Show(@"请选择绘制图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataEditCommon.g_pMyMapCtrl.CurrentTool = null;
                return;
            }
            else
            {
                if (pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolyline && pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolygon || pFeatureLayer.FeatureClass.FeatureType == esriFeatureType.esriFTAnnotation)
                {
                    MessageBox.Show(@"请选择线状或面状图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataEditCommon.g_pMyMapCtrl.CurrentTool = null;
                    return;
                }
            }

            m_pMap = m_hookHelper.FocusMap;
            m_IsFirstPoint = true;
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            IPoint pMovePt = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            pMovePt = GIS.GraphicEdit.SnapSetting.getSnapPoint(pMovePt);
            //左键点击绘制
            if (Button == 1)
                if (m_IsFirstPoint == true)
                {
                    NewFeatureMouseDown(pMovePt);
                    m_IsFirstPoint = false;

                }
                else
                {
                    NewFeatureMouseDown(pMovePt);
                }
        }
        public override void OnDblClick()
        {
            m_IsFirstPoint = true;
            NewFeatureEnd();
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            IPoint pMovePt = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            pMovePt = GIS.GraphicEdit.SnapSetting.getSnapPoint(pMovePt);
            NewFeatureMouseMove(pMovePt);
            DataEditCommon.g_pAxMapControl.Focus();
        }

        /// <summary>
        /// 新建对象，添加点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void NewFeatureMouseDown(IPoint pPoint)
        {
            INewPolygonFeedback pPolyFeed;
            INewLineFeedback pLineFeed;
            try
            {
                /// 如果是新开始创建的对象，则相应的创建一个新的Feedback对象；
                /// 否则，向已存在的Feedback对象中加点
                if (!m_bInUse)
                {
                    m_pCurrentLayer = DataEditCommon.g_pLayer;
                    pFeatureLayer = (IFeatureLayer)DataEditCommon.g_pLayer;
                    if (pFeatureLayer.FeatureClass == null) return;
                    IActiveView pActiveView = (IActiveView)m_pMap;
                    m_pMap.ClearSelection();  //清除地图选中对象
                    m_bInUse = true;
                    //if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)   //创建线要素
                    //{
                    //    m_pFeedback = new NewLineFeedbackClass();
                    //    pLineFeed = (INewLineFeedback)m_pFeedback;
                    //    pLineFeed.Start(pPoint);
                        
                    //}
                    //else
                    //{
                        m_pFeedback = new NewPolygonFeedbackClass();
                        pPolyFeed = (INewPolygonFeedback)m_pFeedback;
                        pPolyFeed.Start(pPoint);
                    //}
                    if (m_pFeedback != null)
                        m_pFeedback.Display = pActiveView.ScreenDisplay;
                }
                else
                {
                    if (m_pFeedback is INewMultiPointFeedback)
                    {
                        object obj = Missing.Value;
                        m_pPointCollection.AddPoint(pPoint, ref obj, ref obj);
                    }
                    else if (m_pFeedback is INewLineFeedback)
                    {
                        pLineFeed = (INewLineFeedback)m_pFeedback;
                        pLineFeed.AddPoint(pPoint);
                    }
                    else if (m_pFeedback is INewPolygonFeedback)
                    {
                        pPolyFeed = (INewPolygonFeedback)m_pFeedback;
                        pPolyFeed.AddPoint(pPoint);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }

        /// <summary>
        /// 新建对象过程中鼠标移动方法,产生Track效果
        /// 在Map.MouseMove事件中调用本方法
        /// </summary>
        /// <param name="x">鼠标X坐标，屏幕坐标</param>
        /// <param name="y">鼠标Y坐标，屏幕坐标</param>
        public void NewFeatureMouseMove(IPoint pt)
        {
            if ((!m_bInUse) || (m_pFeedback == null)) return;

            m_pFeedback.MoveTo(pt);
        }

        /// <summary>
        /// 完成新建对象，取得绘制的对象，并添加到图层中
        /// 建议在Map.DblClick或Map.MouseDown(Button = 2)事件中调用本方法
        /// </summary>
        public void NewFeatureEnd()
        {
            IGeometry pGeom = null;
            IPointCollection pPointCollection;
            object obj = Type.Missing;
            try
            {
                if (m_pFeedback is INewMultiPointFeedback)
                {
                    INewMultiPointFeedback pMPFeed = (INewMultiPointFeedback)m_pFeedback;
                    pMPFeed.Stop();
                    pGeom = (IGeometry)m_pPointCollection;

                    if (m_GeometryCollection == null)
                    {
                        m_GeometryCollection = new PointClass() as IGeometryCollection;
                    }

                    m_GeometryCollection.AddGeometryCollection(pGeom as IGeometryCollection);
                }
                else if (m_pFeedback is INewLineFeedback)
                {
                    INewLineFeedback pLineFeed = (INewLineFeedback)m_pFeedback;

                    if (m_GeometryCollection == null)
                    {
                        m_GeometryCollection = new PolylineClass() as IGeometryCollection;
                    }

                    IPolyline pPolyLine = pLineFeed.Stop();

                    pPointCollection = (IPointCollection)pPolyLine;
                    if (pPointCollection.PointCount < 2)
                        MessageBox.Show("至少输入两个节点");
                    else
                        pGeom = (IGeometry)pPointCollection;

                    m_GeometryCollection.AddGeometryCollection(pGeom as IGeometryCollection);
                }
                else if (m_pFeedback is INewPolygonFeedback)
                {
                    INewPolygonFeedback pPolyFeed = (INewPolygonFeedback)m_pFeedback;

                    if (m_GeometryCollection == null)
                    {
                        m_GeometryCollection = new PolygonClass() as IGeometryCollection;
                    }

                    IPolygon pPolygon;
                    pPolygon = pPolyFeed.Stop();
                    if (pPolygon != null)
                    {
                        pPointCollection = (IPointCollection)pPolygon;
                        if (pPointCollection.PointCount < 3)
                            MessageBox.Show("至少输入三个节点");
                        else
                            pGeom = (IGeometry)pPointCollection;

                        m_GeometryCollection.AddGeometryCollection(pGeom as IGeometryCollection);
                    }
                }
                if (pFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    IPolyline pline = MyMapHelp.PolygonToPolyline(m_GeometryCollection as IPolygon);
                    CreateFeature(pline as IGeometry);
                }
                else
                {
                    CreateFeature(m_GeometryCollection as IGeometry);
                }
                m_pFeedback = null;
                m_bInUse = false;
                m_GeometryCollection = null;
            }
            catch (Exception e)
            {
                m_pFeedback = null;
                m_bInUse = false;
                m_GeometryCollection = null;
                Console.WriteLine(e.Message.ToString());
            }
        }

        /// <summary>
        /// 根据点创建要素
        /// </summary>
        /// <param name="pGeom"></param>
        private void CreateFeature(IGeometry pGeom)
        {
            try
            {
                if (pGeom == null) return;
                if (m_pCurrentLayer == null) return;

                IWorkspaceEdit pWorkspaceEdit = DataEditCommon.g_CurWorkspaceEdit;// GetWorkspaceEdit();
                IFeatureLayer pFeatureLayer = (IFeatureLayer)m_pCurrentLayer;
                IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;

                pWorkspaceEdit.StartEditOperation();
                IFeature pFeature = pFeatureClass.CreateFeature();

                // 处理Z/M值
                DrawCommon.HandleZMValue(pFeature, pGeom, 0);

                pFeature.Shape = pGeom;
                pFeature.Store();
                pWorkspaceEdit.StopEditOperation();
                m_GeometryCollection = null;
                m_pMap.SelectFeature(m_pCurrentLayer, pFeature);
                m_pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics | esriViewDrawPhase.esriViewGeoSelection, null, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }
        #endregion
    }
}
