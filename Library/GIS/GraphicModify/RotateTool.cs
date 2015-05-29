using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using GIS.Common;
using GIS.GraphicEdit;
using GIS.Properties;
using Cursor = System.Windows.Forms.Cursor;

namespace GIS
{
    /// <summary>
    /// 旋转
    /// </summary>
    [Guid("0e3e8130-744a-48b3-b1fc-e23ec8907ae2")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.GraphicModify.RotateTool")]
    public sealed class RotateTool : BaseTool
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
        private IEngineEditor m_engineEditor = null;
        private IRotateTracker m_rotateTracker; //旋转轨迹
        private IMapControl3 m_mapControl;
        private IFeatureLayer m_featureLayer = null;

        public RotateTool()
        {
            //公共属性定义
            m_category = "旋转";
            m_caption = "旋转图元";
            m_message = "选择图元，进行旋转";
            m_toolTip = "旋转图元";
            m_name = "RotateTool";
            try
            {
                m_bitmap = Resources.RotateTool;
                m_cursor = new Cursor(GetType().Assembly.GetManifestResourceStream(GetType(), "Resources.RotateTool.cur"));

                m_engineEditor = Activator.CreateInstance<EngineEditorClass>();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods
        public override bool Enabled
        {
            get
            {
                IFeatureLayer m_featureLayer = DataEditCommon.g_pLayer as IFeatureLayer;
                if (m_featureLayer == null) return false;
                IFeatureSelection m_featureSelection = m_featureLayer as IFeatureSelection;
                ISelectionSet m_selectionSet = m_featureSelection.SelectionSet;//QI到ISelectionSet
                if (m_selectionSet.Count < 1)
                {
                    return false;
                }
                return true;
            }
        }
        /// <summary>
        /// 创建工具
        /// </summary>
        /// <params name="hook">程序实例</params>
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

            m_enabled = m_hookHelper != null;
            m_mapControl = DataEditCommon.g_pMyMapCtrl;
            m_rotateTracker = new EngineRotateTrackerClass();
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        public override void OnClick()
        {
            IFeatureLayer m_featureLayer = DataEditCommon.g_pLayer as IFeatureLayer;
            if (m_featureLayer == null) return;
            IFeatureSelection m_featureSelection = m_featureLayer as IFeatureSelection;
            ISelectionSet m_selectionSet = m_featureSelection.SelectionSet;//QI到ISelectionSet
            if (m_selectionSet.Count < 1)
            {
                MessageBox.Show(@"请选择要旋转的图元。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataEditCommon.g_pMyMapCtrl.CurrentTool = null;
            }
            //GIS.GraphicEdit.SnapSetting.StartSnappingEnv();
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            if (Button == 1)
            {
                //if (m_engineEditor.SelectionCount == 0) return;
                m_rotateTracker.Display = m_mapControl.ActiveView.ScreenDisplay;
                //以所有选择要素的的中心点为旋转支点
                var pEnumFeature = m_hookHelper.ActiveView.Selection as IEnumFeature;
                //pEnumFeature = m_engineEditor.EditSelection;
                pEnumFeature.Reset();
                var pFeature = pEnumFeature.Next();
                if (pFeature == null) return;
                IEnvelope pSelEnvelope = pFeature.Extent;   //选择要素集的Envelope
                pFeature = pEnumFeature.Next();
                while (pFeature != null)
                {
                    if (pFeature.Extent.IsEmpty == false)
                    {
                        pSelEnvelope.Union(pFeature.Extent);
                    }
                    pFeature = pEnumFeature.Next();
                }
                IPoint pPt = new PointClass();
                pPt.PutCoords((pSelEnvelope.XMin + pSelEnvelope.XMax) / 2, (pSelEnvelope.YMin + pSelEnvelope.YMax) / 2);
                //设置要旋转的几何要素
                SetTrackGeometry(pPt);
                IPoint pStartPoint = m_mapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                pStartPoint = SnapSetting.getSnapPoint(pStartPoint);
                if (m_rotateTracker != null)
                {
                    m_rotateTracker.OnMouseMove(pStartPoint);
                    m_rotateTracker.OnMouseDown();
                }
            }
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            IPoint pPoint = m_mapControl.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            pPoint = SnapSetting.getSnapPoint(pPoint);
            if (Button == 1)
            {
                if (m_rotateTracker != null)
                {
                    m_rotateTracker.OnMouseMove(pPoint);
                }
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            if (Button != 1) return;
            //if (m_engineEditor.SelectionCount < 1) return;
            if (m_rotateTracker == null) return;

            IEnumFeature pEnumFeature;
            IFeature pFeature;
            ITransform2D pTrans2D;
            bool bChanged = m_rotateTracker.OnMouseUp();
            if (bChanged == false) return;

            try
            {
                if (this.DoubleCheck(m_rotateTracker) == false) return;
                IEnvelope pSelEnvelope = null;
                pEnumFeature = m_hookHelper.ActiveView.Selection as IEnumFeature;
                pEnumFeature.Reset();
                pFeature = pEnumFeature.Next();
                m_engineEditor.StartOperation();
                while (pFeature != null)
                {
                    if (pSelEnvelope == null)
                        pSelEnvelope = pFeature.Extent;
                    else if (pFeature.Extent.IsEmpty == false)
                        pSelEnvelope.Union(pFeature.Extent);
                    //如果选中的是地物
                    if (pFeature.FeatureType == esriFeatureType.esriFTSimple || pFeature.FeatureType == esriFeatureType.esriFTDimension ||
                        pFeature.FeatureType == esriFeatureType.esriFTSimpleEdge || pFeature.FeatureType == esriFeatureType.esriFTSimpleJunction)
                    {
                        pTrans2D = pFeature.ShapeCopy as ITransform2D;
                        //旋转要素
                        pTrans2D.Rotate(m_rotateTracker.Origin, m_rotateTracker.Angle);
                        pFeature.Shape = pTrans2D as IGeometry;
                        pFeature.Store();
                    }
                    if (pFeature.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        double angle = m_rotateTracker.Angle / Math.PI * 180;
                        int i = pFeature.Fields.FindField("Angle");
                        double oldAngle = (double)pFeature.get_Value(i);
                        double newAngle = oldAngle + angle;
                        if (newAngle > 360) newAngle = angle - 360;
                        if (newAngle < -360) newAngle = angle + 360;
                        pFeature.set_Value(i, newAngle);
                        pFeature.Store();
                    }
                    pFeature = pEnumFeature.Next();
                }
                m_engineEditor.StopOperation("Rotate Features");
                m_mapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                m_mapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, pSelEnvelope);
            }
            catch (Exception ex)
            {
                m_engineEditor.AbortOperation();
            }
            finally
            {
                pEnumFeature = null;
                pFeature = null;
                pTrans2D = null;
            }
        }

        /// <summary>
        /// 设置旋转的原点并加载旋转的要素
        /// </summary>
        /// <params name="pPt"></params>
        private void SetTrackGeometry(IPoint pPt)
        {
            IEnumFeature pEnumFeature;
            IFeature pFeature;
            try
            {
                //m_RotateTracker.ClearGeometry();
                m_rotateTracker.Origin = pPt;
                pEnumFeature = m_hookHelper.ActiveView.Selection as IEnumFeature;
                if (pEnumFeature == null) return;
                pEnumFeature.Reset();
                pFeature = pEnumFeature.Next();
                while (pFeature != null)
                {
                    m_rotateTracker.AddGeometry(pFeature.Shape);
                    pFeature = pEnumFeature.Next();
                }
                //this.AddNewAnchorPt(pPt);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 是否双击
        /// </summary>
        /// <params name="pRotateTracker"></params>
        /// <returns></returns>
        private bool DoubleCheck(IRotateTracker pRotateTracker)
        {
            if (pRotateTracker.Angle > -3.15 || pRotateTracker.Angle < 3.15)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
