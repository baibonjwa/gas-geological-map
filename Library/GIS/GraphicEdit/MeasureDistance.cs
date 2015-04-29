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
using GIS.Properties;
using ESRI.ArcGIS.esriSystem;

namespace GIS
{
    /// <summary>
    /// 距离测量
    /// </summary>
    [Guid("010190b6-f6f7-44d9-acd9-4c7d3ccf5ee9")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.GraphicEdit.MeasureDistance")]
    public sealed class MeasureDistance : BaseTool
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
        private INewLineFeedback m_NewLineFeedback = null;
        private IPointCollection m_ptColl; //记录节点
        private MeasureResult _MsgInfo = null;
        private IPolyline m_TraceLine = null; //完整的轨迹线        //
        private IGroupElement m_Elements = null; //用于保存包含此功能产生的所有Element
        private IGroupElement m_TraceElement = null; //测距轨迹线
        private IGroupElement m_VertexElement = null; //结点
        private IGroupElement m_LabelElement = null; // 距离标记

        public MeasureDistance()
        {
            //公共属性定义
            base.m_category = "编辑";
            base.m_caption = "测量距离";
            base.m_message = "测量距离";
            base.m_toolTip = "测量距离";
            base.m_name = "MeasureDistance";
            try
            {
                base.m_bitmap = Resources.MeasureTool16;
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), "Resources." + GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        public MeasureResult MsgInfo
        {
            set
            {
                _MsgInfo = value;
                _MsgInfo.FormClosing += msgInfo_FromClosing;
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

        void Init()
        {
            //初始化
            m_Elements = new GroupElementClass();
            m_TraceElement = new GroupElementClass();
            m_VertexElement = new GroupElementClass();
            m_LabelElement = new GroupElementClass();

            //初始化,并添加到GraphicsContainer
            IGraphicsContainer g = m_hookHelper.ActiveView as IGraphicsContainer;
            g.AddElement(m_Elements as IElement, 0);
            g.AddElement(m_TraceElement as IElement, 0);
            g.AddElement(m_VertexElement as IElement, 0);
            g.AddElement(m_LabelElement as IElement, 0);

            //添加到m_Elements中
            g.MoveElementToGroup(m_VertexElement as IElement, m_Elements);
            g.MoveElementToGroup(m_LabelElement as IElement, m_Elements);
            g.MoveElementToGroup(m_TraceElement as IElement, m_Elements);
        }

        public override void OnClick()
        {
            //初始化
            foreach (System.Windows.Forms.Form form in Application.OpenForms)
            {
                if (form.Name.Equals("MeasureResult"))
                {
                    MeasureResult m_form = (MeasureResult)form;
                    m_form.closeauto = true;
                    form.Close();
                    break;
                }
            }
            Init();

            ///20140218 lyf 弹出结果显示窗体            
            if (_MsgInfo == null || _MsgInfo.IsDisposed)
                _MsgInfo = new MeasureResult(m_hookHelper);
            _MsgInfo.Show();
            _MsgInfo.TopMost = true;
            _MsgInfo.tsmiAreaUnit.Visible = false;
        }

        void msgInfo_FromClosing(object sender, FormClosingEventArgs e)
        {
            //DeleteAllElements(m_hookHelper);
            _MsgInfo = null;
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            if (Button == 2)
                return;
            IPoint pt = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            pt = GIS.GraphicEdit.SnapSetting.getSnapPoint(pt);
            IGraphicsContainer g = m_hookHelper.ActiveView.GraphicsContainer;
            IEnvelope pEnvBounds = null;

            //获取上一次轨迹线的范围,以便确定刷新范围
            try
            {
                if (m_TraceLine != null)
                {
                    m_TraceLine.QueryEnvelope(pEnvBounds);
                    pEnvBounds.Expand(4, 4, true); //矩形框向四周扩大4倍(大于2倍就行),目的是为了保证有充足的刷新区域
                }
                else
                    pEnvBounds = m_hookHelper.ActiveView.Extent;
            }
            catch
            {
                pEnvBounds = m_hookHelper.ActiveView.Extent;
            }

            #region 启动画线
            if (m_NewLineFeedback == null)
            {
                //移除element
                RemoveElements();
                //刷新
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                Application.DoEvents();

                m_NewLineFeedback = new NewLineFeedbackClass();
                m_NewLineFeedback.Display = m_hookHelper.ActiveView.ScreenDisplay;
                //必须先得到symbol,后设置symbol
                ISimpleLineSymbol simpleLineSymbol = m_NewLineFeedback.Symbol as ISimpleLineSymbol;
                simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
                simpleLineSymbol.Width = 2;
                simpleLineSymbol.Color = TransColorToAEColor(Color.Blue);

                m_NewLineFeedback.Start(pt);
            }
            else
            {
                m_NewLineFeedback.AddPoint(pt);
            }

            if (m_ptColl == null)
            {
                m_ptColl = new PolylineClass();
            }
            //记录节点
            object obj = Type.Missing;
            m_ptColl.AddPoint(pt, ref obj, ref obj);

            #endregion

            #region 绘制结点

            try
            {
                IElement vertexElement = createElement_x(pt);
                //
                g = m_hookHelper.ActiveView as IGraphicsContainer;

                //g.AddElement(vertexElement, 0);
                //g.MoveElementToGroup(vertexElement, m_VertexElement);

                m_VertexElement.AddElement(vertexElement);
                //刷新
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, vertexElement, pEnvBounds);

            }
            catch
            { }
            #endregion

            try
            {
                if (m_ptColl.PointCount >= 2)
                {
                    IPoint fromPt = m_ptColl.get_Point(m_ptColl.PointCount - 2); //倒数第二个点
                    IPoint toPt = m_ptColl.get_Point(m_ptColl.PointCount - 1); //最后第一个点
                    ILine line = new LineClass();
                    line.PutCoords(fromPt, toPt);

                    #region 绘制轨迹线

                    try
                    {
                        object missing = Type.Missing;
                        ISegmentCollection segColl = new PolylineClass();
                        segColl.AddSegment(line as ISegment, ref missing, ref missing);
                        IElement traceElement = createElement_x(segColl as IPolyline);
                        //
                        g = m_hookHelper.ActiveView as IGraphicsContainer;

                        //g.AddElement(traceElement, 0);
                        //g.MoveElementToGroup(traceElement, m_TraceElement);

                        m_TraceElement.AddElement(traceElement);

                        m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, traceElement, pEnvBounds);

                    }
                    catch
                    { }
                    #endregion

                    #region 计算单线的长度,并将结果显示在单线中点偏上上面
                    try
                    {
                        double angle = line.Angle;
                        if ((angle > (Math.PI / 2) && angle < (Math.PI)) || (angle > -Math.PI && angle < -(Math.PI / 2))) // 大于90度小于等于180
                            angle += Math.PI;

                        //标注点Y值偏移量
                        double d_OffsetY = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.FromPoints(9);

                        //标注点

                        double d_CenterX = (fromPt.X + toPt.X) / 2;
                        double d_CenterY = (fromPt.Y + toPt.Y) / 2 + d_OffsetY; //向上偏移

                        IPoint labelPt = new PointClass();
                        labelPt.PutCoords(d_CenterX, d_CenterY);
                        IUnitConverter unitConverter = new UnitConverterClass();
                        double segmentLength = unitConverter.ConvertUnits(line.Length, _MsgInfo.inUnit.pUnit, _MsgInfo.outUnit.pUnit);
                        ITextElement txtElement = CreateTextElement(segmentLength.ToString("0.00"));

                        IElement labelelement = txtElement as IElement;
                        labelelement.Geometry = labelPt;
                        object oElement = (object)labelelement;

                        //根据角度旋转
                        TransformByRotate(ref oElement, labelPt, angle);

                        ////添加到GraphicsContainer
                        //g.AddElement(labelelement, 0);

                        ////移到m_LabelElement组中
                        //g.MoveElementToGroup(labelelement, m_LabelElement);

                        //添加到组
                        m_LabelElement.AddElement(labelelement);

                        //刷新

                        m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, labelelement, pEnvBounds);
                    }
                    catch
                    { }
                    #endregion
                }


            }
            catch
            { }

            _MsgInfo.LineResultChange();
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            IPoint pt = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            pt = GIS.GraphicEdit.SnapSetting.getSnapPoint(pt);
            if (m_NewLineFeedback == null)
                return;
            m_NewLineFeedback.MoveTo(pt);

            if (m_ptColl.PointCount == 0)
                return;
            double d_Total = 0;
            double d_segment = 0;

            IPoint lastPt = m_ptColl.get_Point(m_ptColl.PointCount - 1);
            ILine line = new LineClass();
            line.PutCoords(lastPt, pt);
            //节距离
            d_segment = line.Length;
            _MsgInfo.Segment = d_segment;
            try
            {
                IPolyline polyline = m_ptColl as IPolyline;
                if (polyline.IsEmpty)
                {

                    d_Total = d_segment;
                }
                else
                {
                    d_Total = polyline.Length + d_segment;
                }

            }
            catch
            {

            }
            //赋值给总长度
            _MsgInfo.Total = d_Total;

            _MsgInfo.LineResultChange();
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
        }

        public override void OnDblClick()
        {
            if (m_NewLineFeedback == null)
                return;


            //绘制线与多边形几何图形时,双击结束绘制
            try
            {
                m_TraceLine = m_NewLineFeedback.Stop();
                if (m_TraceLine == null)
                    return;
            }
            catch
            { }
            finally
            {
                Recycle();
            }
        }
        #endregion

        //回收
        public void Recycle()
        {
            m_NewLineFeedback = null;
            m_ptColl.RemovePoints(0, m_ptColl.PointCount);
            m_ptColl = null;
            m_TraceLine = null;//20140218 lyf
            m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, m_hookHelper.ActiveView.Extent);
        }

        /// <summary>
        /// 从组中删除所有元素
        /// </summary>
        /// <param name="groupElement"></param>
        void RemoveElementFromGroupElement(IGroupElement groupElement)
        {
            if (groupElement == null || groupElement.ElementCount == 0)
                return;
            try
            {
                IGraphicsContainer g = m_hookHelper.ActiveView.GraphicsContainer;
                for (int index = 0; index < groupElement.ElementCount; index++)
                {
                    IElement tmp_Ele = groupElement.get_Element(index);
                    if (tmp_Ele is IGroupElement)
                        RemoveElementFromGroupElement(tmp_Ele as IGroupElement);
                    else
                    {
                        try
                        {
                            groupElement.DeleteElement(tmp_Ele);
                        }
                        catch
                        {

                        }
                        finally
                        {
                            tmp_Ele = null;
                        }
                    }
                }
                //groupElement.ClearElements();
            }
            catch
            { }
            finally
            {
                //刷新
                IEnvelope pEnvBounds = null;

                //获取上一次轨迹线的范围,以便确定刷新范围
                try
                {
                    if (m_TraceLine != null)
                    {
                        m_TraceLine.QueryEnvelope(pEnvBounds);
                        pEnvBounds.Expand(4, 4, true); //矩形框向四周扩大4倍(大于2倍就行),目的是为了保证有充足的刷新区域
                    }
                    else
                        pEnvBounds = m_hookHelper.ActiveView.Extent;
                }
                catch
                {
                    pEnvBounds = m_hookHelper.ActiveView.Extent;
                }
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, pEnvBounds);
            }
        }
        /// <summary>
        /// 移除节点,标注和轨迹线Element
        /// </summary>
        void RemoveElements()
        {
            try
            {

                //刷新一次
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, m_hookHelper.ActiveView.Extent);
                IGraphicsContainer g = m_hookHelper.ActiveView.GraphicsContainer;
                #region 1-new
                //RemoveElementFromGroupElement(m_Elements);
                #endregion
                #region 2
                m_LabelElement.ClearElements();
                m_VertexElement.ClearElements();
                m_TraceElement.ClearElements();
                #endregion
            }
            catch
            {

            }
            finally
            {

                //刷新一次
                m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, m_hookHelper.ActiveView.Extent);
            }
        }
        /// <summary>
        /// 删除所有与此相关的元素
        /// </summary>
        public void DeleteAllElements(IHookHelper hookHelper)
        {
            //m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            IGraphicsContainer g = hookHelper.ActiveView.GraphicsContainer;

            //RemoveElementFromGroupElement(m_Elements);
            try
            {
                //g.DeleteElement(m_Elements as IElement);
                g.DeleteAllElements();
            }
            catch
            { }
            finally
            {
                m_TraceElement = null;
                m_LabelElement = null;
                m_VertexElement = null;
                m_Elements = null;
                //最后再刷新一次
                hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                hookHelper.ActiveView.Refresh();
            }
        }

        /// <summary>
        /// 将系统颜色转换为IColor
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        ESRI.ArcGIS.Display.IColor TransColorToAEColor(Color color)
        {
            IRgbColor rgb = new RgbColorClass();
            rgb.RGB = color.R + color.G * 256 + color.B * 65536;
            return rgb as IColor;
        }

        /// <summary>
        /// 按指定的角度旋转
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="originPt"></param>
        /// <param name="rotate"></param>
        void TransformByRotate(ref object obj, IPoint originPt, double rotate)
        {
            if (obj == null && originPt == null)
                return;
            try
            {
                ITransform2D transform2D = obj as ITransform2D;
                if (transform2D == null)
                    return;
                transform2D.Rotate(originPt, rotate);

            }
            catch
            { }
        }

        #region 创建Element
        /// <summary>
        /// 创建一个TextElement
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        ITextElement CreateTextElement(string text)
        {
            //创建一个TextSymbol
            ITextSymbol txtSymbol = new TextSymbolClass();

            //设置字体
            Font dispFont = new Font("Arial", 10, FontStyle.Regular);
            txtSymbol.Font = (stdole.IFontDisp)ESRI.ArcGIS.ADF.COMSupport.OLE.GetIFontDispFromFont(dispFont);

            //设置属性
            txtSymbol.Color = TransColorToAEColor(Color.Red); //颜色

            //创建一个TextElement
            ITextElement txtElement = new TextElementClass();
            txtElement.Symbol = txtSymbol;
            txtElement.Text = text;

            return txtElement;
        }

        /// <summary>
        /// 绘制几何图形
        /// </summary>
        /// <param name="geoType"></param>
        /// <param name="geometry"></param>
        /// <returns></returns>
        ESRI.ArcGIS.Carto.IElement createElement_x(ESRI.ArcGIS.Geometry.IGeometry geometry)
        {
            IElement element = null;
            try
            {
                switch (geometry.GeometryType)
                {
                    case esriGeometryType.esriGeometryPolyline://Polyline线
                        ISimpleLineSymbol simpleLineSymbol = m_NewLineFeedback.Symbol as ISimpleLineSymbol;

                        ILineElement lineElement = new LineElementClass();
                        lineElement.Symbol = simpleLineSymbol as ILineSymbol;
                        element = lineElement as IElement;
                        element.Geometry = geometry;
                        break;
                    case esriGeometryType.esriGeometryPoint:
                        //设置结点符号
                        IRgbColor pRGB = new RgbColorClass();
                        pRGB.Red = 255;
                        pRGB.Green = 0;
                        pRGB.Blue = 0;

                        ISimpleMarkerSymbol pSimpleMarkSymbol = new SimpleMarkerSymbolClass();
                        pSimpleMarkSymbol.Color = pRGB as IColor;
                        pSimpleMarkSymbol.Size = 2;
                        pSimpleMarkSymbol.Style = esriSimpleMarkerStyle.esriSMSSquare;

                        IMarkerElement pMarkerElement = new MarkerElementClass();
                        pMarkerElement.Symbol = pSimpleMarkSymbol as IMarkerSymbol;
                        element = pMarkerElement as IElement;
                        element.Geometry = geometry as IGeometry;
                        break;
                }
            }
            catch
            { }
            return element;
        }
        #endregion

    }
}
