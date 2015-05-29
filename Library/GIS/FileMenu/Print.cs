using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using System.Collections;
using System.Drawing.Printing;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;


namespace GIS.FileMenu
{
    public partial class Print : Form
    {

        public string m_FileName;
        public string FileName
        {
            get { return m_FileName; }
            set { m_FileName = value; }
        }

        //定义打印预览、打印、打印设置对话框
        internal PrintPreviewDialog m_printPreviewDialog;
        internal PrintDialog m_printDialog;
        internal PageSetupDialog m_pageSetupDialog;

        //定义打印文件，显示打印预览
        private PrintDocument m_PrintDocument = new PrintDocument();
        //打印预览时Cancel Tracker传递给输出函数
        private ITrackCancel m_TrackCancel = new CancelTracker() as ITrackCancel;
        //当前打印页
        private short m_CurrentPrintPage;
        //图饰位置
        private IList<MapSurroundEntity> m_mapSurroundList;

        //地图装饰设置
        private ESRI.ArcGIS.Carto.IGraphicsContainer m_graphicContainer = null;
        private ESRI.ArcGIS.Carto.ITextElement m_textElement = null;

        private ESRI.ArcGIS.Controls.AxMapControl axMap;

        public Print(ESRI.ArcGIS.Controls.AxMapControl AxMap)
        {
            axMap = AxMap;
            InitializeComponent();
            Common.MapPrintCommon.g_axPageLayoutControl = axPageLayoutControl1;
            Common.MapPrintCommon.g_axToolbarControl = axToolbarControl1;
        }

        private void Print_Load(object sender, EventArgs e)
        {
            //Mxd文件是否存在
            //******？？？？？？会打印两页
            if (axPageLayoutControl1.CheckMxFile(m_FileName))
            {
                //加载Mxd到PageLayout
                axPageLayoutControl1.LoadMxFile(m_FileName, "");

                IActiveView activeView = axPageLayoutControl1.ActiveView.FocusMap as IActiveView;
                IDisplayTransformation displayeTransformation = activeView.ScreenDisplay.DisplayTransformation;
                displayeTransformation.VisibleBounds = GIS.Common.DataEditCommon.g_pAxMapControl.Extent;
                displayeTransformation.Rotation = axMap.Rotation;
                double angle = Math.Round(displayeTransformation.Rotation, 0);
                ILayer player = GIS.Common.DataEditCommon.GetLayerByName(activeView.FocusMap, LayerNames.LAYER_ALIAS_MR_HCGZMWSYCLD);
                MyMapHelp.Angle_Symbol(player, -angle);
                player = GIS.Common.DataEditCommon.GetLayerByName(activeView.FocusMap, LayerNames.LAYER_ALIAS_MR_WSHLD);
                MyMapHelp.Angle_Symbol(player, -angle);
                player = GIS.Common.DataEditCommon.GetLayerByName(activeView.FocusMap, LayerNames.LAYER_ALIAS_MR_WSYLD);
                MyMapHelp.Angle_Symbol(player, -angle);
                //displayeTransformation.ReferenceScale = axMap.ReferenceScale;
                activeView.Refresh();

                m_graphicContainer = axPageLayoutControl1.PageLayout as IGraphicsContainer;
            }


            //MapCopy();

            InitToolBar();

            InitializePrintPreviewDialog(); //初始化打印预览对话框
            m_printDialog = new PrintDialog(); //打印对话框实例化
            InitializePageSetupDialog(); //初始化打印设置对话框   
        }

        private void MapCopy()
        {
            if (GIS.Common.DataEditCommon.g_pAxMapControl != null)
            {
                IObjectCopy objCopy = new ObjectCopyClass();
                object toCopyMap = GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.FocusMap;
                object copiedMap = objCopy.Copy(toCopyMap);
                object overwriteMap = axPageLayoutControl1.ActiveView.FocusMap;
                objCopy.Overwrite(toCopyMap, ref overwriteMap);
            }
        }

        private void InitToolBar()
        {


            axToolbarControl1.SetBuddyControl(axPageLayoutControl1);
            axToolbarControl1.RemoveAll();
            axToolbarControl1.AddItem(new GIS.pageLayout.LayoutZoomIn() as ESRI.ArcGIS.SystemUI.ICommand, -1, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem(new GIS.pageLayout.LayoutZoomOut() as ESRI.ArcGIS.SystemUI.ICommand, -1, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem(new GIS.pageLayout.LayoutPan() as ESRI.ArcGIS.SystemUI.ICommand, -1, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem(new GIS.pageLayout.LayoutFixedZoomIncs() as ESRI.ArcGIS.SystemUI.ICommand, -1, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem(new GIS.pageLayout.LayoutFixedZoomOut() as ESRI.ArcGIS.SystemUI.ICommand, -1, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem(new GIS.pageLayout.LayoutZoomWhole() as ESRI.ArcGIS.SystemUI.ICommand, -1, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem(new GIS.pageLayout.LayoutZoom100() as ESRI.ArcGIS.SystemUI.ICommand, -1, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem(new GIS.pageLayout.LayoutBackExtent() as ESRI.ArcGIS.SystemUI.ICommand, -1, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem(new GIS.pageLayout.LayoutNextExtent() as ESRI.ArcGIS.SystemUI.ICommand, -1, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("GIS.MapPrint.SelectElement", -1, -1, true, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem("GIS.MapPrint.AddTextElement", -1, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem(new MapPrint.CreateNorthArrow() as ESRI.ArcGIS.SystemUI.ICommand, -1, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            axToolbarControl1.AddItem(new MapPrint.CreateScaleBar() as ESRI.ArcGIS.SystemUI.ICommand, -1, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);

        }

        private void InitializePrintPreviewDialog()
        {
            //实例化打印预览对话框
            m_printPreviewDialog = new PrintPreviewDialog();
            //打印预览对话框相关属性设置
            m_printPreviewDialog.ClientSize = new System.Drawing.Size(800, 600);
            m_printPreviewDialog.Location = new System.Drawing.Point(29, 29);
            m_printPreviewDialog.Name = "打印预览";
            m_printPreviewDialog.Text = "打印预览";
            m_printPreviewDialog.MinimumSize = new System.Drawing.Size(375, 250);
            //允许系统平滑字形
            m_printPreviewDialog.UseAntiAlias = true;
            //打印预览中打印按钮偶尔有bug：打印空白，先去掉
            foreach (Control ctrl in m_printPreviewDialog.Controls)
            {
                if (ctrl.GetType() == typeof(ToolStrip))
                {
                    ToolStrip tools = ctrl as ToolStrip;
                    tools.Items.RemoveAt(0);
                }
            }

            //文档打印页事件
            this.m_PrintDocument.PrintPage += document_PrintPage;
        }

        private void InitializePageSetupDialog()
        {
            //实例化打印设置对话框
            m_pageSetupDialog = new PageSetupDialog();
            //初始化打印设置对话框属性
            m_pageSetupDialog.PageSettings = new System.Drawing.Printing.PageSettings();
            m_pageSetupDialog.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
            //不显示Network
            m_pageSetupDialog.ShowNetwork = false;
        }

        private void tsbPrintSetup_Click(object sender, EventArgs e)
        {
            try
            {
                GetSurroundPose();
                //显示打印设置页对话框
                DialogResult result = m_pageSetupDialog.ShowDialog();

                //设置预览文档属性
                m_PrintDocument.PrinterSettings = m_pageSetupDialog.PrinterSettings;
                m_PrintDocument.DefaultPageSettings = m_pageSetupDialog.PageSettings;

                //遍历查找所选择的页面大小
                int i;
                IEnumerator paperSizes = m_pageSetupDialog.PrinterSettings.PaperSizes.GetEnumerator();
                paperSizes.Reset();

                for (i = 0; i < m_pageSetupDialog.PrinterSettings.PaperSizes.Count; ++i)
                {
                    paperSizes.MoveNext();
                    if (((PaperSize)paperSizes.Current).Kind == m_PrintDocument.DefaultPageSettings.PaperSize.Kind)
                    {
                        m_PrintDocument.DefaultPageSettings.PaperSize = ((PaperSize)paperSizes.Current);
                    }
                }

                ///////////////////////////////////
                ///根据打印设置选项初始化当前打印机
                ///////////////////////////////////
                IPaper paper;
                paper = new Paper() as IPaper; //初始化页面

                IPrinter printer;
                printer = new EmfPrinter() as IPrinter; //初始化打印机

                paper.Attach(m_pageSetupDialog.PrinterSettings.GetHdevmode(m_pageSetupDialog.PageSettings).ToInt32(), m_pageSetupDialog.PrinterSettings.GetHdevnames().ToInt32());

                //将页面传给打印机
                printer.Paper = paper;

                //设置PageLayoutControl打印机
                axPageLayoutControl1.Printer = printer;
                if (axPageLayoutControl1.Page.Orientation != printer.Paper.Orientation)
                {
                    axPageLayoutControl1.Page.Orientation = printer.Paper.Orientation;
                    IMapFrame mapFram = axPageLayoutControl1.ActiveView.GraphicsContainer.FindFrame(axPageLayoutControl1.ActiveView.FocusMap) as IMapFrame;
                    IElement element = mapFram as IElement;
                    IEnvelope envelope = new EnvelopeClass();
                    envelope.XMax = element.Geometry.Envelope.YMax;
                    envelope.XMin = element.Geometry.Envelope.YMin;
                    envelope.YMax = element.Geometry.Envelope.XMax;
                    envelope.YMin = element.Geometry.Envelope.YMin;
                    element.Geometry = envelope;
                    UpdataSurroundPose();
                    //axPageLayoutControl1.ActiveView.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 更新图饰位置
        /// </summary>
        private void UpdataSurroundPose()
        {
            if (m_mapSurroundList == null && m_mapSurroundList.Count < 1) return;

            IGraphicsContainer graphicsContainer = axPageLayoutControl1.PageLayout as IGraphicsContainer;
            IEnvelope mapEnvelop = (graphicsContainer.FindFrame(axPageLayoutControl1.ActiveView.FocusMap) as IElement).Geometry.Envelope;
            MapSurroundEntity mapSurroundEntity;
            bool isUpdate = false;
            for (int i = 0; i < m_mapSurroundList.Count; i++)
            {
                mapSurroundEntity = m_mapSurroundList[i];
                IEnvelope elementEnv = mapSurroundEntity.element.Geometry.Envelope;
                isUpdate = false;
                if (mapSurroundEntity.element is IMapSurroundFrame)
                {
                    isUpdate = UpdateMapSurroundFrame(mapEnvelop, mapSurroundEntity);
                }
                else if (mapSurroundEntity.element is ITextElement)
                {
                    isUpdate = UpdateTextElement(mapEnvelop, mapSurroundEntity);
                }

                if (isUpdate)
                {
                    graphicsContainer.UpdateElement(mapSurroundEntity.element);
                }
            }
        }
        /// <summary>
        /// 更新文本位置
        /// </summary>
        /// <params name="mapEnvelop">地图范围</params>
        /// <params name="mapSurroundEntity">文本位置实体</params>
        /// <returns></returns>
        private bool UpdateTextElement(IEnvelope mapEnvelop, MapSurroundEntity mapSurroundEntity)
        {
            bool isUpdate = false;
            IPoint point = mapSurroundEntity.element.Geometry as IPoint;
            IEnvelope envelop = mapSurroundEntity.element.Geometry.Envelope;


            if (mapSurroundEntity.left > mapSurroundEntity.right)
            {
                if (mapSurroundEntity.left > mapEnvelop.Width || (mapSurroundEntity.left > mapSurroundEntity.right && (mapEnvelop.XMax - envelop.XMax) > mapSurroundEntity.right))
                {
                    point.X = mapEnvelop.XMax - mapSurroundEntity.right - mapSurroundEntity.width;
                    isUpdate = true;
                }
            }
            if (mapSurroundEntity.bottom > mapEnvelop.Height || (mapSurroundEntity.bottom > mapSurroundEntity.top && mapEnvelop.YMax - envelop.YMax > mapSurroundEntity.top))
            {
                point.Y = mapEnvelop.YMax - mapSurroundEntity.top - mapSurroundEntity.height;
                isUpdate = true;
            }

            if (isUpdate)
            {
                mapSurroundEntity.element.Geometry = point;
            }
            return isUpdate;
        }
        /// <summary>
        /// 更新MapSurroundFrame位置
        /// </summary>
        /// <params name="mapEnvelop">地图范围</params>
        /// <params name="mapSurroundEntity">MapSurroundFrame位置实体</params>
        /// <returns></returns>
        private bool UpdateMapSurroundFrame(IEnvelope mapEnvelop, MapSurroundEntity mapSurroundEntity)
        {
            bool isUpdate = false;
            IEnvelope envelop = mapSurroundEntity.element.Geometry.Envelope;

            if (mapSurroundEntity.left > mapSurroundEntity.right)
            {
                if (mapSurroundEntity.left > mapEnvelop.Width)
                {
                    envelop.XMin = mapEnvelop.XMax - mapSurroundEntity.right - mapSurroundEntity.width;
                    envelop.XMax = envelop.XMin + mapSurroundEntity.width;
                    isUpdate = true;
                }
                else if (mapSurroundEntity.left > mapSurroundEntity.right && (mapEnvelop.XMax - envelop.XMax) > mapSurroundEntity.right)
                {
                    envelop.XMax = mapEnvelop.XMax - mapSurroundEntity.right;
                    envelop.XMin = envelop.XMax - mapSurroundEntity.width;
                    isUpdate = true;
                }

            }
            if (mapSurroundEntity.bottom > mapEnvelop.Height)
            {
                envelop.YMin = mapEnvelop.YMax - mapSurroundEntity.top - mapSurroundEntity.height;
                envelop.YMax = envelop.YMin + mapSurroundEntity.height;
                isUpdate = true;
            }
            else if (mapSurroundEntity.bottom > mapSurroundEntity.top && mapEnvelop.YMax - envelop.YMax > mapSurroundEntity.top)
            {
                envelop.YMax = mapEnvelop.YMax - mapSurroundEntity.top;
                envelop.YMin = envelop.YMax - mapSurroundEntity.height;
                isUpdate = true;
            }
            if (isUpdate)
            {
                mapSurroundEntity.element.Geometry = envelop;
            }
            return isUpdate;
        }



        /// <summary>
        /// 获得图饰位置
        /// </summary>
        void GetSurroundPose()
        {
            m_mapSurroundList = new List<MapSurroundEntity>();
            IGraphicsContainer graphicsContainer = axPageLayoutControl1.PageLayout as IGraphicsContainer;
            IEnvelope mapEnvelop = (graphicsContainer.FindFrame(axPageLayoutControl1.ActiveView.FocusMap) as IElement).Geometry.Envelope;
            graphicsContainer.Reset();
            IElement element = graphicsContainer.Next();
            while (element != null)
            {
                if (element is IMapFrame)
                {
                    element = graphicsContainer.Next();
                    continue;
                }
                else
                {
                    IEnvelope envelop = element.Geometry.Envelope as IEnvelope;
                    MapSurroundEntity mapSurroundEntity = new MapSurroundEntity();
                    mapSurroundEntity.left = envelop.XMin - mapEnvelop.XMin;
                    mapSurroundEntity.right = mapEnvelop.XMax - envelop.XMax;
                    mapSurroundEntity.top = mapEnvelop.YMax - envelop.YMax;
                    mapSurroundEntity.bottom = envelop.YMin - mapEnvelop.YMin;
                    mapSurroundEntity.width = envelop.Width;
                    mapSurroundEntity.height = envelop.Height;
                    mapSurroundEntity.element = element;
                    m_mapSurroundList.Add(mapSurroundEntity);
                }
                element = graphicsContainer.Next();
            }

        }


        private void tsbPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                //初始化当前打印页码
                m_CurrentPrintPage = 0;

                //当前文档是否加载到PageLayoutControl
                if (axPageLayoutControl1.DocumentFilename == null) return;
                //设置打印预览文件为PageLayoutControl中的文档
                m_PrintDocument.DocumentName = axPageLayoutControl1.DocumentFilename;


                //设置打印预览对话框Document属性
                m_printPreviewDialog.Document = m_PrintDocument;

                //显示打印预览对话框
                m_printPreviewDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            //页面可选
            m_printDialog.AllowSomePages = true;
            //帮助可用
            m_printDialog.ShowHelp = true;

            //打印文档属性设置
            m_printDialog.Document = m_PrintDocument;

            //显示打印对话框，等待打印
            DialogResult result = m_printDialog.ShowDialog();

            //打印文档
            if (result == DialogResult.OK) m_PrintDocument.Print();
        }

        private void document_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            string sPageToPrinterMapping = "esriPageMappingScale";
            if (sPageToPrinterMapping == null)
                //默认是Tile
                axPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingTile;
            else if (sPageToPrinterMapping.Equals("esriPageMappingTile"))
                axPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingTile;
            else if (sPageToPrinterMapping.Equals("esriPageMappingCrop"))
                axPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingCrop;
            else if (sPageToPrinterMapping.Equals("esriPageMappingScale"))
                axPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingScale;
            else
                axPageLayoutControl1.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingTile;

            //获取图形设备分辨率
            short dpi = (short)e.Graphics.DpiX;
            //设备边框
            IEnvelope devBounds = new Envelope() as IEnvelope;
            //获得页面
            IPage page = axPageLayoutControl1.Page;

            //打印页数
            short printPageCount;
            printPageCount = axPageLayoutControl1.get_PrinterPageCount(0);
            m_CurrentPrintPage++;

            //当前打印机
            IPrinter printer = axPageLayoutControl1.Printer;
            //打印机设备边框
            page.GetDeviceBounds(printer, m_CurrentPrintPage, 0, dpi, devBounds);

            //设备边框矩形
            tagRECT deviceRect;
            //设备边框矩形坐标
            double xmin, ymin, xmax, ymax;
            devBounds.QueryCoords(out xmin, out ymin, out xmax, out ymax);
            //初始化边框矩形
            deviceRect.bottom = (int)ymax;
            deviceRect.left = (int)xmin;
            deviceRect.top = (int)ymin;
            deviceRect.right = (int)xmax;

            //边框是否可见
            IEnvelope visBounds = new Envelope() as IEnvelope;
            page.GetPageBounds(printer, m_CurrentPrintPage, 0, visBounds);

            //获得图形设备句柄以便打印预览
            IntPtr hdc = e.Graphics.GetHdc();

            //根据指定边框打印 
            axPageLayoutControl1.ActiveView.Output(hdc.ToInt32(), dpi, ref deviceRect, visBounds, m_TrackCancel);


            //检测是否还有打印任务
            if (m_CurrentPrintPage < printPageCount)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;

            //释放设备
            e.Graphics.ReleaseHdc(hdc);

        }


        private void axPageLayoutControl1_OnDoubleClick(object sender, ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnDoubleClickEvent e)
        {
            if (e.button == 1)
            {
                //标注的修改
                if (axPageLayoutControl1.CurrentTool == null) return;
                if (((axPageLayoutControl1.CurrentTool) as ICommand).Name == "ControlToolsGraphicElement_SelectTool")
                {
                    IPoint pPoint = new PointClass();
                    pPoint.PutCoords(e.pageX, e.pageY);


                    IGraphicsContainer pGraphicsContainer = axPageLayoutControl1.PageLayout as IGraphicsContainer;

                    IEnumElement pEnumElement = pGraphicsContainer.LocateElements(pPoint, 10);
                    if (pEnumElement != null)
                    {
                        IElement pElement = pEnumElement.Next();
                        if (pElement is ITextElement)
                        {
                            ITextElement ptextElement = pElement as ITextElement;
                            MapPrint.TextSetUp textSetUp = new MapPrint.TextSetUp();
                            textSetUp.UpdateTextElement(ptextElement);
                            textSetUp.Show();
                        }
                    }

                }
            }
        }

        /// <summary>
        /// 图饰位置
        /// </summary>
        public class MapSurroundEntity
        {
            public double left { get; set; }
            public double right { get; set; }
            public double top { get; set; }
            public double bottom { get; set; }
            public double width { get; set; }
            public double height { get; set; }
            public IElement element { get; set; }
        }

    }
}
