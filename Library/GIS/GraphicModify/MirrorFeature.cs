using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using System.Collections;
using ESRI.ArcGIS.esriSystem;
using GIS.Properties;
using GIS.Common;

namespace GIS
{
    /// <summary>
    /// 镜像
    /// </summary>
    [Guid("345f3dd6-361b-494d-baed-409371413bb1")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.GraphicModify.MirrorFeature")]
    public sealed class MirrorFeature : BaseTool
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
        //需要作为镜像的要素
        private IFeature m_pFeature;
        //镜像线的点
        private IPoint m_pPoint1;//一号点
        private IPoint m_pPoint2;//二号点
        private ILayer m_pCurrentLayer;
        private IDisplayFeedback m_pFeedback;
        ///20140216 lyf
        IFeatureLayer m_featureLayer = null;
        IFeatureSelection m_featureSelection = null;
        ISelectionSet m_selectionSet = null;

        public MirrorFeature()
        {
            //公共属性定义
            base.m_category = "修改";
            base.m_caption = "镜像";
            base.m_message = "根据镜像轴线生成镜像图元";
            base.m_toolTip = "镜像";
            base.m_name = "MirrorFeature";
            try
            {
                base.m_bitmap = Resources.EditingMirrorTool16;
                base.m_cursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream(GetType(), "Resources.MirrorFeature.cur"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
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
                ESRI.ArcGIS.Geodatabase.ISelectionSet m_selectionSet = m_featureSelection.SelectionSet;//QI到ISelectionSet
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
                m_pPoint1 = null;
                m_pPoint2 = null;
                m_pFeedback = null;
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

            m_pCurrentLayer = DataEditCommon.g_pLayer;

            ///20140216 lyf
            m_featureLayer = m_pCurrentLayer as IFeatureLayer;
            m_featureSelection = m_featureLayer as IFeatureSelection;
            m_selectionSet = m_featureSelection.SelectionSet;//QI到ISelectionSet
            if (m_selectionSet.Count != 1)
            {
                MessageBox.Show(@"请选择一个图元再进行镜像。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataEditCommon.g_pMyMapCtrl.CurrentTool = null;
                return;
            }
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            if (Button == 2)
                return;
            INewLineFeedback pLineFeed;

            if (m_pPoint1 == null)
            {
                m_pPoint1 = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                m_pPoint1 = GIS.GraphicEdit.SnapSetting.getSnapPoint(m_pPoint1);
                m_pFeedback = new NewLineFeedbackClass();
                pLineFeed = (INewLineFeedback)m_pFeedback;
                pLineFeed.Start(m_pPoint1);
                if (m_pFeedback != null)
                    m_pFeedback.Display = m_hookHelper.ActiveView.ScreenDisplay;

            }
            else if (m_pPoint2 == null)
            {
                ///20140216 lyf
                ICursor pCursor = null;
                m_selectionSet.Search(null, false, out pCursor);
                IFeatureCursor pFeatureCursor = pCursor as IFeatureCursor;
                IFeature m_pFeature = pFeatureCursor.NextFeature();

                if (pCursor != null)
                {
                    pCursor = null;
                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(pCursor);
                }

                if (m_pFeature == null)
                {
                    MessageBox.Show(@"所选要素为空，请重新选择要素。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                m_pPoint2 = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                m_pPoint2 = GIS.GraphicEdit.SnapSetting.getSnapPoint(m_pPoint2);
                //线和要素都齐全了，开始复制
                //IFeatureLayer pFeatureLayer = m_pCurrentLayer as IFeatureLayer;
                IFeatureClass pFeatureClass = m_featureLayer.FeatureClass;
                ILine nLine = new LineClass();//镜像轴线
                nLine.PutCoords(m_pPoint1, m_pPoint2);
                ITransformation nTransformation = new AffineTransformation2DClass();
                IAffineTransformation2D nAffineTransformation2D = nTransformation as IAffineTransformation2D;
                nAffineTransformation2D.DefineReflection(nLine);

                //启动编辑
                IWorkspaceEdit pWorkspaceEdit = DataEditCommon.g_CurWorkspaceEdit;// GetWorkspaceEdit();

                ITransform2D nTransform2D = m_pFeature.Shape as ITransform2D;//镜像目标

                nTransform2D.Transform(esriTransformDirection.esriTransformForward, nTransformation);

                if (m_pFeature.Shape.Dimension == esriGeometryDimension.esriGeometry0Dimension)
                {
                    pWorkspaceEdit.StartEditOperation();
                    IFeature pNewFeature = pFeatureClass.CreateFeature();
                    IPoint pNewPoint = nTransform2D as IPoint;
                    pNewFeature.Shape = pNewPoint;
                    pNewFeature.Store();
                    pWorkspaceEdit.StopEditOperation();
                    DataEditCommon.g_pMyMapCtrl.Map.ClearSelection();
                    DataEditCommon.g_pMyMapCtrl.Map.SelectFeature(m_pCurrentLayer, pNewFeature);
                }
                else if (m_pFeature.Shape.Dimension == esriGeometryDimension.esriGeometry1Dimension)
                {
                    pWorkspaceEdit.StartEditOperation();
                    IFeature pNewFeature = pFeatureClass.CreateFeature();
                    IPolyline pNewPolyline = nTransform2D as IPolyline;//镜像所得
                    pNewFeature.Shape = pNewPolyline;
                    pNewFeature.Store();
                    pWorkspaceEdit.StopEditOperation();
                    DataEditCommon.g_pMyMapCtrl.Map.ClearSelection();
                    DataEditCommon.g_pMyMapCtrl.Map.SelectFeature(m_pCurrentLayer, pNewFeature);
                }
                else if (m_pFeature.Shape.Dimension == esriGeometryDimension.esriGeometry2Dimension)
                {
                    pWorkspaceEdit.StartEditOperation();
                    IFeature pNewFeature = pFeatureClass.CreateFeature();
                    IPolygon pNewPolygon = nTransform2D as IPolygon;//镜像所得
                    pNewFeature.Shape = pNewPolygon;
                    pNewFeature.Store();
                    pWorkspaceEdit.StopEditOperation();
                    DataEditCommon.g_pMyMapCtrl.Map.ClearSelection();
                    DataEditCommon.g_pMyMapCtrl.Map.SelectFeature(m_pCurrentLayer, pNewFeature);
                }
                else
                { }
                pLineFeed = (INewLineFeedback)m_pFeedback;
                pLineFeed.AddPoint(m_pPoint2);

                pLineFeed.Stop();//拖拽停止
                m_pFeature = null;
                m_pPoint1 = null;
                m_pPoint2 = null;
            }

            m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
            m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            IPoint mPt = GIS.GraphicEdit.SnapSetting.getSnapPoint(m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y));
            if (m_pFeedback != null)
            {
                m_pFeedback.MoveTo(mPt);
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {

        }
        #endregion

        /// <summary>
        /// 获得选中要素集合
        /// </summary>
        /// <param name="SearchDist">搜索距离</param>
        /// <param name="searchCollection">搜索集合</param>
        /// <param name="pPoint">点</param>
        /// <param name="pFeature">选中要素</param>
        private void GetClosestFeatureInCollection(double SearchDist, ArrayList searchCollection, IPoint pPoint, ref IFeature pFeature)
        {
            IProximityOperator pProximity;
            IFeature pTestFeature;
            IFeature pFea;
            IFeature pPointFeature = null;
            IFeature pLineFeature = null;
            IFeature pAreaFeature = null;
            IGeometry pGeometry;
            double pointTestDistance;
            double lineTestDistance;
            double areaTestDistance;
            double testDistance;

            double tempDist;

            ArrayList pointList = new ArrayList();
            ArrayList lineList = new ArrayList();
            ArrayList areaList = new ArrayList();
            ArrayList NewList = new ArrayList();
            pointTestDistance = -1;
            lineTestDistance = -1;
            areaTestDistance = -1;
            testDistance = -1;

            try
            {
                pProximity = pPoint as IProximityOperator;

                if (searchCollection.Count == 0) return;//20140216 lyf 没有选中图元情况处理

                for (int i = 0; i < searchCollection.Count; i = i + 3)
                {
                    pTestFeature = searchCollection[i] as IFeature;
                    pGeometry = pTestFeature.Shape;
                    switch (pGeometry.GeometryType)
                    {
                        case esriGeometryType.esriGeometryPoint:
                            pointList.Add(pTestFeature);
                            pointList.Add(searchCollection[i + 1]);
                            pointList.Add(searchCollection[i + 2]);
                            break;
                        case esriGeometryType.esriGeometryPolyline:
                            lineList.Add(pTestFeature);
                            lineList.Add(searchCollection[i + 1]);
                            lineList.Add(searchCollection[i + 2]);
                            break;
                        case esriGeometryType.esriGeometryPolygon:
                            areaList.Add(pTestFeature);
                            areaList.Add(searchCollection[i + 1]);
                            areaList.Add(searchCollection[i + 2]);
                            break;
                    }
                }
                if (pointList.Count > 0)
                {
                    NewList = pointList;
                }
                else if (lineList.Count > 0)
                {
                    NewList = lineList;
                }
                else
                {
                    NewList = areaList;
                }
                int k = 0;
                for (int i = 0; i < NewList.Count; i = i + 3)
                {
                    pFea = NewList[i] as IFeature;
                    pGeometry = pFea.Shape;
                    tempDist = pProximity.ReturnDistance(pGeometry);

                    if (tempDist < SearchDist)
                    {
                        switch (pGeometry.GeometryType)
                        {
                            case esriGeometryType.esriGeometryPoint:
                                if (pointTestDistance < 0)
                                {
                                    pointTestDistance = tempDist + 1;
                                }
                                if (tempDist < pointTestDistance)
                                {
                                    pointTestDistance = tempDist;
                                    pPointFeature = pFea;
                                    k = i;
                                }
                                break;
                            case esriGeometryType.esriGeometryPolyline:
                                if (lineTestDistance < 0)
                                {
                                    lineTestDistance = tempDist + 1;
                                }
                                if (tempDist < lineTestDistance)
                                {
                                    lineTestDistance = tempDist;
                                    pLineFeature = pFea;
                                    k = i;
                                }
                                break;
                            case esriGeometryType.esriGeometryPolygon:
                                if (areaTestDistance < 0)
                                {
                                    areaTestDistance = tempDist + 1;
                                }
                                if (tempDist < areaTestDistance)
                                {
                                    areaTestDistance = tempDist;
                                    pAreaFeature = pFea;
                                    k = i;
                                }
                                break;
                        }
                    }
                    else
                    {
                        if (testDistance < 0) testDistance = tempDist + 1;
                        if (tempDist < testDistance)
                        {
                            testDistance = tempDist;
                            pFeature = pFea;
                            k = i;
                        }
                    }

                }
                if (pPointFeature != null)
                {
                    pFeature = pPointFeature;
                }
                if (pLineFeature != null)
                {
                    pFeature = pLineFeature;
                }
                if (pAreaFeature != null)
                {
                    pFeature = pAreaFeature;
                }
                m_hookHelper.FocusMap.ClearSelection();
                IFeatureLayer pFeatureLayer = m_hookHelper.FocusMap.get_Layer(Convert.ToInt32(searchCollection[k + 2])) as IFeatureLayer;
                IFeatureSelection pFeatureSelection = pFeatureLayer as IFeatureSelection;
                IQueryFilter pQueryFilter = new QueryFilterClass();
                pQueryFilter.WhereClause = "OBJECTID " + "= " + pFeature.OID.ToString();
                pFeatureSelection.SelectFeatures(pQueryFilter, esriSelectionResultEnum.esriSelectionResultNew, false);
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
            catch
            {
                return;
            }
        }
    }
}
