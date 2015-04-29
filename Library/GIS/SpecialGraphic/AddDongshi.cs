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
using GIS.Common;
using ESRI.ArcGIS.Geodatabase;
using System.Collections;
using ESRI.ArcGIS.esriSystem;

namespace GIS
{
    /// <summary>
    /// 绘制 硐室
    /// </summary>
    [Guid("0b3b0018-797d-41f1-a977-4a3dd1e2b1d6")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.SpecialGraphic.AddDongshi")]
    public sealed class AddDongshi : BaseTool
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
            ControlsCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper = null;
        private IPoint hightPoint;
        private IPoint mousePoint;
        private IGeometry hightGeometry;
        private IGeometry nearestGeometry;
        private double angle;
        private IPoint m_pPoint;
        private IPoint m_wirePoint = null;  //选择的第一点
        private GIS.Common.DrawSpecialCommon drawSpecialCom = null;
        IFeatureLayer m_pDongshiFeatureLayer = null;    //硐室图层
        IFeatureLayer pFeatLayer = null;//巷道图层
       

        public AddDongshi()
        {
            base.m_category = ""; //localizable text 
            base.m_caption = "绘制硐室";  //localizable text 
            base.m_message = "绘制硐室";  //localizable text
            base.m_toolTip = "绘制硐室";  //localizable text
            base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            try
            {
                base.m_bitmap = Properties.Resources.AddSpecialPoint;
                base.m_cursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream(GetType(), "Resources.AddCircularArc.cur"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (m_hookHelper == null)
                m_hookHelper = new HookHelperClass();

            m_hookHelper.Hook = hook;

            // TODO:  Add AddDongshi.OnCreate implementation
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            drawSpecialCom = new GIS.Common.DrawSpecialCommon();
            nearestGeometry = new PolylineClass();
            hightPoint = new PointClass();
            hightPoint.PutCoords(0, 0);
            string layerName = GIS.LayerNames.DEFALUT_DONGSHI;
            m_pDongshiFeatureLayer = drawSpecialCom.GetFeatureLayerByName(layerName);
            if (m_pDongshiFeatureLayer == null)
            {
                MessageBox.Show(@"当前地图上没有找到硐室图层", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            // 右键返回
            if (Button == 2) return;
            if (m_pDongshiFeatureLayer == null)
                return;
            m_pPoint = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            m_pPoint = GIS.GraphicEdit.SnapSetting.getSnapPoint(m_pPoint);

            double angle = -1;
            string layerName = LayerNames.LAYER_ALIAS_MR_TUNNEL_FD;      //巷道
            pFeatLayer = drawSpecialCom.GetFeatureLayerByName(layerName);

            IFeature pFeature = null;
            TestExistPointFeature(m_hookHelper, m_pPoint, pFeatLayer, ref pFeature);
            if (pFeature == null)
            {
                MessageBox.Show(@"鼠标点击处没有巷道，请先选择一条巷道边线", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //IPoint hightPoint= Snapping(m_pPoint, pFeatLayer, pFeature);
            //if (hightPoint.IsEmpty)
            //{
            //    MessageBox.Show("获取巷道边界失败，请重新选择", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            IProximityOperator proximityOperator = (IProximityOperator)pFeature.Shape;
            IPoint mousePoint = proximityOperator.ReturnNearestPoint(m_pPoint, esriSegmentExtension.esriNoExtension);

            IProximityOperator proximityOperator2 = (IProximityOperator)mousePoint;
            ISegmentCollection segmentCollection = (ISegmentCollection)pFeature.Shape;
            for (int i = 0; i < segmentCollection.SegmentCount; i++)
            {

                ISegmentCollection geometryCollection = new PolylineClass();
                ISegment segment = segmentCollection.get_Segment(i);
                geometryCollection.AddSegment(segment);
                geometryCollection.SegmentsChanged();
                var distance = proximityOperator2.ReturnDistance((IGeometry)geometryCollection);

                if (distance < 0.0001)
                {
                    angle = ((ILine)segment).Angle;
                    break;
                }
            }
            if (angle == -1)
            {
                MessageBox.Show(@"获取巷道边界失败，请重新选择！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //得到相应的中线
            string hdid = pFeature.get_Value(pFeature.Fields.FindField(GIS_Const.FIELD_HDID)).ToString();
            string bid = pFeature.get_Value(pFeature.Fields.FindField(GIS_Const.FIELD_BID)).ToString();

            FormCaveSizeInput frmCaveSize = new FormCaveSizeInput();
            frmCaveSize.ShowDialog();
            if (frmCaveSize.DialogResult == DialogResult.OK)
            {
                //输入正确，获得硐室的长和宽
                double Width = frmCaveSize.CaveWidth;
                double Height = frmCaveSize.CaveHeight;

                PointClass p0 = RectanglePoint(mousePoint, angle, Width);
                PointClass p1 = RectanglePoint(mousePoint, angle + Math.PI, Width);
                PointClass p2 = RectanglePoint2(p0, angle + 90 * Math.PI / 180, Height);
                PointClass p3 = RectanglePoint2(p1, angle + 90 * Math.PI / 180, Height);
                //绘制硐室
                CreateDongShi(p0, p2, p3, p1, hdid, bid);
            }
        }
        private PointClass RectanglePoint(IPoint point, double angle,double width)
        {
            PointClass p = new PointClass();
            p.SpatialReference = m_hookHelper.FocusMap.SpatialReference;
            p.ConstructAngleDistance(point, angle, width);
            return p;
        }
        private PointClass RectanglePoint2(IPoint point, double angle,double height)
        {
            PointClass p = new PointClass();
            p.SpatialReference = m_hookHelper.FocusMap.SpatialReference;
            p.ConstructAngleDistance(point, angle, height/2);
            return p;
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            IPoint pPnt = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            //进行捕捉
            pPnt = GIS.GraphicEdit.SnapSetting.getSnapPoint(pPnt);
        }
       
        //转换像素到地图单位
        public double ConvertPixelsToMapUnits(IActiveView activeView, double pixelUnits)
        {
            double realDisplayExtent;
            int pixelExtent;
            double sizeOfOnePixel;
            pixelExtent = activeView.ScreenDisplay.DisplayTransformation.get_DeviceFrame().right - activeView.ScreenDisplay.DisplayTransformation.get_DeviceFrame().left;
            realDisplayExtent = activeView.ScreenDisplay.DisplayTransformation.VisibleBounds.Width;
            sizeOfOnePixel = realDisplayExtent / pixelExtent;
            return pixelUnits * sizeOfOnePixel;
        }
        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add AddDongshi.OnMouseUp implementation
        }

        public override void OnKeyDown(int keyCode, int Shift)
        {
            if (keyCode == (int)Keys.Escape)
            { }
        }
        #endregion

        private void CreateDongShi(IPoint P0, IPoint P1, IPoint P2, IPoint P3, string hdid, string bid)
        {
            ISegmentCollection pPath = new PathClass();
            //第一条线段
            ILine pLine = new LineClass();
            pLine.PutCoords(P0, P1);
            //QI到ISegment
            ISegment pSegment = pLine as ISegment;
            //创建一个Path对象
            System.Object o = Type.Missing;
            //通过ISegmentCollection接口为Path对象添加Segment对象
            pPath.AddSegment(pSegment, ref o, ref o);
            //第二条线段
            ILine pLine2 = new LineClass();
            pLine2.PutCoords(P1, P2);
            ISegment pSegment2 = pLine2 as ISegment;
            //创建一个Path对象
            //通过ISegmentCollection接口为Path对象添加Segment对象
            pPath.AddSegment(pSegment2, ref o, ref o);
            //第三条线段
            ILine pLine3 = new LineClass();
            pLine3.PutCoords(P2, P3);
            ISegment pSegment3 = pLine3 as ISegment;
            //创建一个Path对象
            //通过ISegmentCollection接口为Path对象添加Segment对象
            pPath.AddSegment(pSegment3, ref o, ref o);

            IGeometryCollection pPolyline = new PolylineClass();
            //通过IGeometryCollection为Polyline对象添加Path对象
            pPolyline.AddGeometry(pPath as IGeometry, ref o, ref o);

            if (m_pDongshiFeatureLayer != null)
            {
                System.Collections.Generic.List<ziduan> list = new System.Collections.Generic.List<ziduan>();
                list.Add(new ziduan(GIS_Const.FIELD_HDID, hdid));
                list.Add(new ziduan(GIS_Const.FIELD_BID, bid));
                DataEditCommon.CreateNewFeature(m_pDongshiFeatureLayer, pPolyline as IGeometry,list);
                m_hookHelper.ActiveView.Refresh();
            }
        }
        /// <summary>
        /// 判断鼠标点击处有无巷道要素
        /// </summary>
        /// <param name="m_hookHelper"></param>
        /// <param name="pMousePoint">鼠标点</param>
        /// <param name="pFeatureLayer">导线点图层</param>
        /// <param name="theFeature">返回离鼠标最近的导线点</param>
        public void TestExistPointFeature(IHookHelper m_hookHelper, IPoint pMousePoint, IFeatureLayer pFeatureLayer, ref IFeature theFeature)
        {
            ArrayList pSelected = new ArrayList();
            IFeatureClass pFeatureClass;
            ISelectionEnvironment pSelectionEnvironment;
            IFeature pFeature=null;
            IGeometry pGeometry;
            ITopologicalOperator pTopolagicalOperator;
            double dLength;

            IEnvelope pSrchEnv;
            pSelectionEnvironment = new SelectionEnvironmentClass();
            dLength = pSelectionEnvironment.SearchTolerance;
            pGeometry = pMousePoint;
            dLength = DataEditCommon.ConvertPixelDistanceToMapDistance(m_hookHelper.ActiveView, dLength);
            pSrchEnv = pMousePoint.Envelope;
            pSrchEnv.Width = dLength;
            pSrchEnv.Height = dLength;
            pSrchEnv.CenterAt(pMousePoint);

            pTopolagicalOperator = (ITopologicalOperator)pGeometry;
            IGeometry pBuffer = pTopolagicalOperator.Buffer(dLength);
            pGeometry = pBuffer;

            IFeature pFeat = null;
            IMap pMap = m_hookHelper.FocusMap;

            pFeatureClass = pFeatureLayer.FeatureClass;
            IIdentify2 pID = pFeatureLayer as IIdentify2;
            //IArray pArray = pID.Identify(pSrchEnv, null);
            IArray pArray = pID.Identify(pGeometry, null);
            IFeatureIdentifyObj pFeatIdObj;
            IRowIdentifyObject pRowObj;

            if (pArray != null)
            {
                for (int j = 0; j < pArray.Count; j++)
                {
                    if (pArray.Element[j] is IFeatureIdentifyObj)
                    {
                        pFeatIdObj = pArray.Element[j] as IFeatureIdentifyObj;
                        pRowObj = pFeatIdObj as IRowIdentifyObject;
                        pFeature = pRowObj.Row as IFeature;
                        pSelected.Add(pFeature);
                    }
                }
                pArray.RemoveAll();
            }
            theFeature = pFeature;

            return;

            //GetClosestFeatureInCollection(m_hookHelper, dLength, pSelected, pMousePoint, ref pFeat);
            //if (pFeat != null)
            //    theFeature = pFeat;
            //else
            //    theFeature = null;

        }

        /// <summary>
        /// 获取离鼠标点最近的要素
        /// </summary>
        /// <param name="m_hookHelper"></param>
        /// <param name="SearchDist">查询距离</param>
        /// <param name="searchCollection">搜索到的要素集合</param>
        /// <param name="pPoint">鼠标点</param>
        /// <param name="pFeature">返回的最近要素</param>
        public void GetClosestFeatureInCollection(IHookHelper m_hookHelper, double SearchDist, ArrayList searchCollection, IPoint pPoint, ref IFeature pFeature)
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

                if (searchCollection.Count == 0) return;    //20140216 lyf 没有选中图元情况处理

                for (int i = 0; i < searchCollection.Count; i++)
                {
                    pTestFeature = searchCollection[i] as IFeature;
                    pGeometry = pTestFeature.Shape;
                    switch (pGeometry.GeometryType)
                    {
                        case esriGeometryType.esriGeometryPoint:
                            pointList.Add(pTestFeature);
                            break;
                        case esriGeometryType.esriGeometryPolyline:
                            lineList.Add(pTestFeature);
                            break;
                        case esriGeometryType.esriGeometryPolygon:
                            areaList.Add(pTestFeature);
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
                for (int i = 0; i < NewList.Count; i++)
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
            }
            catch
            {
                return;
            }
        }
    }
}
