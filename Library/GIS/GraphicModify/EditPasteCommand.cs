using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using GIS.Properties;
using ESRI.ArcGIS.SystemUI;
using GIS.Common;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace GIS.GraphicModify
{
    /// <summary>
    /// 粘贴
    /// </summary>
    [Guid("f0c33556-26e2-431d-ba45-e39b2c7b381e")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.GraphicModify.EditPasteCommand")]
    public sealed class EditPasteCommand : BaseTool
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
        private ICommand m_command = null;

        public EditPasteCommand()
        {
            //公共属性定义
            base.m_category = "修改"; 
            base.m_caption = "粘贴"; 
            base.m_message = "粘贴剪贴板上的内容,左键在地图上确定位置";  
            base.m_toolTip = "粘贴";  
            base.m_name = "EditPasteCommand";   

            try
            {
                base.m_bitmap = Resources.EditPaste16;
                m_command = new ESRI.ArcGIS.Controls.ControlsEditingPasteCommandClass();
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
        /// <param name="hook">创建工具</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                m_command.OnCreate(hook);
                if (m_hookHelper.ActiveView == null)
                    m_hookHelper = null;
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
                if (GIS.Common.DataEditCommon.copypaste == 0)
                    return false;
                if (GIS.Common.DataEditCommon.copypasteLayer == null)
                    return false;
                return true; 
            }
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        public override void OnClick()
        {
            //if (DataEditCommon.g_pMyMapCtrl.CurrentTool != null)
            //    DataEditCommon.g_pMyMapCtrl.CurrentTool = null;
            //m_command.OnClick();

        }
        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {

            m_command.OnClick();
            m_hookHelper.ActiveView.Refresh();
            IPoint pPoint = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            pPoint = GIS.GraphicEdit.SnapSetting.getSnapPoint(pPoint);
            IFeatureLayer featureLayer = GIS.Common.DataEditCommon.copypasteLayer;
            if (featureLayer == null) return;
            IFeatureSelection m_featureSelection = featureLayer as IFeatureSelection;
            ESRI.ArcGIS.Geodatabase.ISelectionSet m_selectionSet = m_featureSelection.SelectionSet;
            if (m_selectionSet.Count == 0)
                return;
            IFeatureClass pFeatureClass = featureLayer.FeatureClass;
            ICursor pCursor = null;
            m_selectionSet.Search(null, false, out pCursor);
            IFeatureCursor pFeatureCursor = pCursor as IFeatureCursor;
            IFeature m_pFeature = pFeatureCursor.NextFeature();
            double dx = 0;
            double dy = 0;
            if (m_pFeature == null)
                return;
                DataEditCommon.InitEditEnvironment();
                DataEditCommon.CheckEditState();
                while(m_pFeature!=null)
                {
                    
                    ITransform2D pTrans2D = m_pFeature.ShapeCopy as ITransform2D;
                    if (m_pFeature.Shape.Dimension == esriGeometryDimension.esriGeometry0Dimension)
                    {
                        IPoint pt = m_pFeature.Shape as IPoint;
                        if (dx == 0)
                            dx = pPoint.X - pt.X;
                        if (dy == 0)
                            dy = pPoint.Y - pt.Y;
                        pTrans2D.Move(dx, dy);
                        m_pFeature.Shape = pTrans2D as IGeometry;
                        m_pFeature.Store();
                    }
                    else if (m_pFeature.Shape.Dimension == esriGeometryDimension.esriGeometry1Dimension)
                    {
                        IPolyline lPolyline = m_pFeature.Shape as IPolyline;
                        if (dx == 0)
                            dx = pPoint.X - lPolyline.FromPoint.X;
                        if(dy==0)
                            dy=pPoint.Y - lPolyline.FromPoint.Y;
                        pTrans2D.Move(dx, dy);
                        m_pFeature.Shape = pTrans2D as IGeometry;
                        m_pFeature.Store();
                    }
                    else if (m_pFeature.Shape.Dimension == esriGeometryDimension.esriGeometry2Dimension)
                    {
                        if (m_pFeature.FeatureType == esriFeatureType.esriFTAnnotation)
                        {
                            IAnnotationFeature annoFeature = m_pFeature as IAnnotationFeature;

                            IElement element = (IElement)annoFeature.Annotation;
                            ITextElement textElement = new TextElementClass();
                            IPoint mPoint=element.Geometry as IPoint;
                            pTrans2D = mPoint as ITransform2D;
                            if (dx == 0)
                                dx = pPoint.X - mPoint.X;
                            if (dy == 0)
                                dy = pPoint.Y - mPoint.Y;
                            pTrans2D.Move(dx, dy);
                            element.Geometry = pTrans2D as IGeometry;
                            annoFeature.Annotation = element;
                            m_pFeature.Store();
                        }
                        else
                        {
                            IPolygon lPolyline = m_pFeature.Shape as IPolygon;
                            if (dx == 0)
                                dx = pPoint.X - lPolyline.FromPoint.X;
                            if (dy == 0)
                                dy = pPoint.Y - lPolyline.FromPoint.Y;
                            pTrans2D.Move(dx, dy);
                            m_pFeature.Shape = pTrans2D as IGeometry;
                            m_pFeature.Store();
                        }
                    }
                    else
                    { }
                    m_pFeature = pFeatureCursor.NextFeature();
                }
                DataEditCommon.g_engineEditor.StopOperation("editpaste");
                GIS.Common.DataEditCommon.copypaste = 0;
                DataEditCommon.copypasteLayer = null;
                m_hookHelper.ActiveView.Refresh();
            //IPoint pPoint = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            //pPoint = GIS.GraphicEdit.SnapSetting.getSnapPoint(pPoint);
            //IFeatureLayer featureLayer = DataEditCommon.g_pLayer as IFeatureLayer;
            //if (featureLayer == null) return;
            //if (DataEditCommon.MyCopy.m_featureLayer.FeatureClass.ShapeType == featureLayer.FeatureClass.ShapeType && DataEditCommon.MyCopy.m_featureLayer.FeatureClass.FeatureType == featureLayer.FeatureClass.FeatureType)
            //{
            //    ESRI.ArcGIS.Geodatabase.ISelectionSet m_selectionSet = DataEditCommon.MyCopy.m_selectionSet;
            //    for (int i = 0; i < m_selectionSet.Count; i++)
            //    {
                    
            //    }
            //}
            //else if (DataEditCommon.MyCopy.m_featureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline && featureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon && featureLayer.FeatureClass.FeatureType != esriFeatureType.esriFTAnnotation)
            //{ 

            //}
            //else if (DataEditCommon.MyCopy.m_featureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon && DataEditCommon.MyCopy.m_featureLayer.FeatureClass.FeatureType!=esriFeatureType.esriFTAnnotation&& featureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
            //{

            //}
            //DataEditCommon.InitEditEnvironment();
            //DataEditCommon.CheckEditState();
        }
        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            IPoint pPoint = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            pPoint = GIS.GraphicEdit.SnapSetting.getSnapPoint(pPoint);
            DataEditCommon.g_pAxMapControl.Focus();
        }
        #endregion
    }
}
