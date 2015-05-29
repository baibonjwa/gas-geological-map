using System;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ESRI.ArcGIS;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using GIS;
using GIS.Common;
using GIS.GraphicEdit;
using GIS.LayersManager;
using GIS.SpecialGraphic;
using LibAbout;
using LibBusiness;

namespace ggm
{
    public partial class MainForm_OP : Form
    {
        //public const string m_SYSTEMNAME = "工作面动态防突管理系统";
        private string m_mapDocumentName = string.Empty;
        private string strPath = Application.StartupPath;
        private readonly GIS_FileMenu m_FileMenu = new GIS_FileMenu();

        public MainForm_OP()
        {
            RuntimeManager.Bind(ProductCode.EngineOrDesktop);
            IAoInitialize aoini = new AoInitializeClass();
            var licenseStatus = aoini.IsProductCodeAvailable(esriLicenseProductCode.esriLicenseProductCodeStandard);
            if (licenseStatus == esriLicenseStatus.esriLicenseAvailable)
            {
                licenseStatus = aoini.Initialize(esriLicenseProductCode.esriLicenseProductCodeStandard);
            }
            //LicenseInitializer license = new LicenseInitializer();
            //bool islicense=license.InitializeApplication();
            InitializeComponent();
        }

        private void bbiCheckUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        #region 窗体事件

        private void MainForm_OP_Load(object sender, EventArgs e)
        {

            IMapDocument pMapDocument = new MapDocumentClass();
            pMapDocument.Open(ConfigHelper.get_attribute("mxd_path"));
            mapControl_OP.LoadMxFile(ConfigHelper.get_attribute("mxd_path"));



            statusStrip1.AxMap = mapControl_OP;
            m_FileMenu.AxMapControl = mapControl_OP; //传入MapControl控件    
            var mapControl = (IMapControl3)mapControl_OP.Object;
            var toolbarControl = (IToolbarControl)toolbar_OP.Object;
            //绑定控件
            toolbar_OP.SetBuddyControl(mapControl);
            tocControl_OP.SetBuddyControl(mapControl);
            //给全局变量赋值
            DataEditCommon.g_tbCtlEdit = toolbarControl;
            DataEditCommon.g_pAxMapControl = mapControl_OP;
            DataEditCommon.g_axTocControl = tocControl_OP;
            DataEditCommon.load(ConfigHelper.current_seam.gis_name);

            IEnumDataset pEnumDataSet =
             DataEditCommon.g_pCurrentWorkSpace.Datasets[esriDatasetType.esriDTFeatureDataset];
            IDataset pDataSet = pEnumDataSet.Next();
            ISpatialReference pRef = (pDataSet as IGeoDataset).SpatialReference;
            string sDistrictCode = string.Empty;
            string sScale = string.Empty;

            //if (pDataSet != null)
            //{
            //    UID uid = new UIDClass();
            //    uid.Value = "{" + typeof(IFeatureLayer).GUID.ToString() + "}";
            //    IEnumLayer pEnumLayer = mapControl_OP.Map.Layers[uid];
            //    IFeatureLayer pFeaLyr = pEnumLayer.Next() as IFeatureLayer;
            //    IFeatureWorkspace pFeaClsWks = DataEditCommon.g_pCurrentWorkSpace as IFeatureWorkspace;
            //    while (pFeaLyr != null)
            //    {
            //        string sDsName = ((pFeaLyr as IDataLayer).DataSourceName as IDatasetName).Name;
            //        if ((DataEditCommon.g_pCurrentWorkSpace as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass, sDsName))
            //        {
            //            pFeaLyr.FeatureClass = pFeaClsWks.OpenFeatureClass(sDsName);
            //            pFeaLyr.Name = pFeaLyr.Name;
            //        }

            //        pFeaLyr = pEnumLayer.Next() as IFeatureLayer;
            //    }
            //    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(DataEditCommon.g_pCurrentWorkSpace);
            //    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(pFeaClsWks);
            //    mapControl_OP.Map.SpatialReference = pRef;
            //    IMxdContents pMxdC;
            //    pMxdC = mapControl_OP.Map as IMxdContents;
            //    pMapDocument.Open(ConfigHelper.GetAttribute("mxd_path"));
            //    pMapDocument.ReplaceContents(pMxdC);
            //    pMapDocument.Save(true, true);
            //}


            AddToolBar.Addtool(mapControl_OP, mapControl, toolbarControl, DataEditCommon.g_pCurrentWorkSpace);
        }


        private void MainForm_OP_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        private void mniDCShape_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_FileMenu.ExportShape();
        }


        private int m_currentButton;

        private void tsBtnWSYLD_Click(object sender, EventArgs e)
        {
            m_currentButton = 1;
            mapControl_OP.CurrentTool = null;
        }

        private void tsBtnWSHLD_Click(object sender, EventArgs e)
        {
            m_currentButton = 2;
            mapControl_OP.CurrentTool = null;
        }

        private void tsBtnWSYCLD_Click(object sender, EventArgs e)
        {
            m_currentButton = 3;
            mapControl_OP.CurrentTool = null;
        }

        private void tsBtnWSYLDZX_Click(object sender, EventArgs e)
        {
            m_currentButton = 0; //点击其他菜单或按钮时，设置该值为0，避免点击MapControl响应mapControl_OP_OnMouseDown事件

            var frmMakeContours = new MakeContours { m_layerName = "GAS_PRESSURE_CONTOUR", m_layerAliasName = "瓦斯压力等值线" };
            frmMakeContours.Show();
        }

        private void tsBtnWSHLDZX_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;

            var frmMakeContours = new MakeContours { m_layerName = "GAS_CONTENT_CONTOUR", m_layerAliasName = "瓦斯含量等值线" };
            frmMakeContours.Show();
        }

        private void tsBtnWSYCLDZX_Click(object sender, EventArgs e)
        {
            m_currentButton = 0;

            var frmMakeContours = new MakeContours
            {
                m_layerName = "GUSH_QUANTITY_CONTOUR",
                m_layerAliasName = "瓦斯涌出量等值线"
            };
            frmMakeContours.Show();
        }

        #endregion

        #region MapControls点击事件

        private void mapControl_OP_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (e.button == 1 && m_currentButton > 0)
            {
                //弹出右键菜单
                //DataEditCommon.contextMenu.PopupMenu(e.x, e.y, this.mapControl_OP.hWnd);
                var pt = mapControl_OP.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                pt = SnapSetting.getSnapPoint(pt);
                switch (m_currentButton)
                {
                    case 1:
                        var gasPressureInfoEnteringForm = new GasPressureInfoEntering { GasPressurePoint = pt };
                        gasPressureInfoEnteringForm.ShowDialog(); //绘制瓦斯压力点
                        m_currentButton = 0; //解除当前按钮
                        break;
                    case 2:
                        var gasContentInfoEnteringForm = new GasContentInfoEntering { GasContentPoint = pt };
                        gasContentInfoEnteringForm.ShowDialog(); //绘制瓦斯含量点
                        m_currentButton = 0; //解除当前按钮
                        break;
                    case 3:
                        var gasGushQuantityInfoEnteringForm = new GasGushQuantityInfoEntering
                        {
                            GasGushQuantityPoint = pt
                        };
                        gasGushQuantityInfoEnteringForm.ShowDialog(); //绘制瓦斯涌出量点
                        m_currentButton = 0; //解除当前按钮
                        break;
                }
            }
        }

        private void mapControl_OP_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (m_currentButton == 1 || m_currentButton == 2 || m_currentButton == 3)
            {
                var pt = mapControl_OP.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                SnapSetting.getSnapPoint(pt);
            }
        }

        private void mapControl_OP_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            //获得当前地图文档
            m_mapDocumentName = mapControl_OP.DocumentFilename;

            //如果没有地图文档，保存按钮不可用并清除状态栏
            //if (m_mapDocumentName == string.Empty)
            //{
            //    mniSave.Enabled = false;
            //    statusBarXY.Text = string.Empty;
            //}
            //else
            //{
            //    //保存菜单可用并设置状态栏内容
            //    mniSave.Enabled = true;
            //    statusBarXY.Text = "当前文件：" + System.IO.Path.GetFileName(m_mapDocumentName);
            //}
        }

        #endregion

        #region TOCControls点击事件

        /// <summary>
        ///     右键弹出图层管理菜单，进行图层管理
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void tocControl_OP_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            if (e.button != 2)
                return; //左键则跳出

            var item = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap map = null;
            ILayer layer = null;
            object unk = null;
            object data = null;

            //判断选择类型，确定右键菜单的位置
            tocControl_OP.HitTest(e.x, e.y, ref item, ref map, ref layer, ref unk, ref data);
            if (layer == null)
                return;
            //确认所选项 
            if (item == esriTOCControlItem.esriTOCControlItemMap)
                tocControl_OP.SelectItem(map, null);
            else
                tocControl_OP.SelectItem(layer, null); //20140224 有bug

            //设置图层的CustomProperty（在自定义图层命令中使用）
            mapControl_OP.CustomProperty = layer;

            //弹出菜单
            var menuMap = new LayersManagerMap();
            menuMap.SetHook(mapControl_OP);
            var menuLayer = new LayersManagerLayer();
            menuLayer.SetHook(mapControl_OP);
            if (item == esriTOCControlItem.esriTOCControlItemMap)
                //选中的为地图
                menuMap.PopupMenu(e.x, e.y, tocControl_OP.hWnd);
            if (item == esriTOCControlItem.esriTOCControlItemLayer)
                //选中的为图层
                menuLayer.PopupMenu(e.x, e.y, tocControl_OP.hWnd);
        }

        /// <summary>
        ///     双击图层符号，修改整个图层符号类型
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void tocControl_OP_OnDoubleClick(object sender, ITOCControlEvents_OnDoubleClickEvent e)
        {
            //esriTOCControlItem tocControlItem = esriTOCControlItem.esriTOCControlItemNone;
            //ILayer pLayer = null;
            //IBasicMap pBasicMap = null;
            //object unk = null;
            //object data = null;
            //if (e.button == 1)
            //{
            //    //判断点击的为哪种Item
            //    tocControl_OP.HitTest(e.x, e.y, ref tocControlItem, ref pBasicMap, ref pLayer, ref unk,
            //    ref data);
            //    //只有图层可设置符号
            //    if (tocControlItem == esriTOCControlItem.esriTOCControlItemLegendClass)
            //    {
            //        ESRI.ArcGIS.Carto.ILegendClass pLegendClass = new LegendClassClass();
            //        ESRI.ArcGIS.Carto.ILegendGroup pLegendGroup = new LegendGroupClass();
            //        if (unk is ILegendGroup)
            //        {
            //            pLegendGroup = (ILegendGroup)unk;
            //        }
            //        pLegendClass = pLegendGroup.get_Class((int)data);
            //        ISymbol pSymbol = pLegendClass.Symbol;
            //        ISymbolSelector pSymbolSelector = new SymbolSelectorClass();
            //        pSymbolSelector.AddSymbol(pSymbol);
            //        bool bOK = pSymbolSelector.SelectSymbol(0);
            //        if (bOK)
            //        {
            //            pLegendClass.Symbol = pSymbolSelector.GetSymbolAt(0);
            //        }

            //        this.mapControl_OP.ActiveView.Refresh();
            //        this.tocControl_OP.Refresh();
            //    }
            //}
        }

        #endregion

        #region ******文件******

        //打开矿图
        private void mniOpenMineMap_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;
            m_FileMenu.OpenMapDocument();
        }

        //保存
        private void mniSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;
            m_FileMenu.Save();
        }

        //另存为
        private void mniSaveAs_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;
            m_FileMenu.SaveAs();
        }

        private void mniExport_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;
        }

        //导出为CAD
        private void mniDCCAD_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;
            m_FileMenu.ExportCAD();
        }

        //导出为Pdf或图片
        private void mniDCTPPDF_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;
            m_FileMenu.ExportPicPdf();
        }

        //打印
        private void mniPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;
            m_FileMenu.Print();
        }

        //退出
        private void mniQuit_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;
            m_FileMenu.Exit();
        }

        #endregion

        #region ******数据管理******

        //瓦斯含量点
        private void mniWSHLD_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;

            var gasContentInfoManagementForm = new GasContentInfoManagement();
            gasContentInfoManagementForm.Show();
        }

        //瓦斯压力点
        private void mniWSYLD_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;

            var gasPressureInfoManagement = new GasPressureInfoManagement();
            gasPressureInfoManagement.Show();
        }

        //瓦斯涌出量点
        private void mniWSYCLD_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;

            var gasGushQuantityInfoManagementForm = new GasGushQuantityInfoManagement();
            gasGushQuantityInfoManagementForm.Show();
        }

        #endregion

        #region ******绘图******

        //瓦斯压力等值线绘制
        private void mniWSYLDZXHZ_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;

            var frmMakeContours = new MakeContours { m_layerName = "GAS_PRESSURE_CONTOUR", m_layerAliasName = "瓦斯压力等值线" };
            frmMakeContours.Show();
        }

        //瓦斯含量等值线绘制
        private void mniWSHLDZXHZ_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;

            var frmMakeContours = new MakeContours { m_layerName = "GAS_CONTENT_CONTOUR", m_layerAliasName = "瓦斯含量等值线" };
            frmMakeContours.Show();
        }

        //瓦斯涌出量等值线绘制
        private void mniWSYCLDZXHZ_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;

            var frmMakeContours = new MakeContours
            {
                m_layerName = "GUSH_QUANTITY_CONTOUR",
                m_layerAliasName = "瓦斯涌出量等值线"
            };
            frmMakeContours.Show();
        }

        //瓦斯地质图绘制
        private void mniWSDZTHZ_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;
        }

        //瓦斯压力点绘制
        private void mniWSYLDHZ_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 1;
            mapControl_OP.CurrentTool = null;
            //GIS.BasicGraphic.DrawWSD wsd = new GIS.BasicGraphic.DrawWSD();
            //wsd.OnCreate(DataEditCommon.g_pAxMapControl.Object);
            //ICommand command=null;
            //command =new GIS.BasicGraphic.DrawWSD();
            //command.OnCreate(Global.MainMap);
            //    if (command.Enabled)
            //    {

            //        DataEditCommon.g_pAxMapControl.CurrentTool = (ITool)command;
            //    }
        }

        //瓦斯含量点绘制
        private void mniWSHLDHZ_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 2;
            mapControl_OP.CurrentTool = null;
        }

        //瓦斯涌出量点绘制
        private void mniWSYCLDHZ_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 3;
            mapControl_OP.CurrentTool = null;
        }

        #endregion

        #region ******防突措施******

        //区域措施
        private void mniAreaMeasures_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;
        }

        //局部措施
        private void mniLocalMeasures_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;
        }

        //防突钻孔设计
        private void mniFTZKSJ_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;
        }

        #endregion

        #region ******参数计算******

        //瓦斯压力点绘制
        private void mniWSYL_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 1;
        }

        //瓦斯含量点绘制
        private void mniWSHL_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 2;
        }

        //瓦斯涌出量点绘制
        private void mniWSYCL_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 3;
        }

        //反算瓦斯压力/含量
        private void mniFSWSYLHHL_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;
        }

        //煤层透气性系数
        private void mniMCTQXXS_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;
        }

        //突出指标(D、K)
        private void mniTCZB_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;
        }

        //抽放管路阻力计算
        private void mniCFGLZLJS_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;
        }

        //瓦斯抽放管径
        private void mniWSCFGJ_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;
        }

        //抽放泵选型
        private void mniCFBXX_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;
        }

        #endregion

        #region ******帮助******

        //帮助文件
        private void mniHelpFile_ItemClick(object sender, ItemClickEventArgs e)
        {
            var strHelpFilePath = Application.StartupPath + "动态瓦斯地质图绘制软件帮助文件.chm";
            Process.Start(strHelpFilePath);
        }

        //关于
        private void mniAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
            var strAboutFilePath = Application.StartupPath + "动态瓦斯地质图绘制软件关于图片.jpg";
            var libabout = new About(ProductName, ProductVersion);
            libabout.ShowDialog();
        }

        #endregion

        #region ******文件浮动工具条******

        //打开矿图浮动工具条
        private void mniOpenMineMapFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniOpenMineMap_ItemClick(null, null);
        }

        //保存浮动工具条
        private void mniSaveFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniSave_ItemClick(null, null);
        }

        //另存为浮动工具条
        private void mniSaveAsFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniSaveAs_ItemClick(null, null);
        }

        //导出为CAD浮动工具条
        private void mniDCCADFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniDCCAD_ItemClick(null, null);
        }

        //导出为PDF或图片浮动工具条
        private void mniDCTPPDFFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniDCTPPDF_ItemClick(null, null);
        }

        //打印浮动工具条
        private void mniPrintFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniPrint_ItemClick(null, null);
        }

        //退出浮动工具条
        private void mniQuitFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniQuit_ItemClick(null, null);
        }

        #endregion

        #region ******数据管理浮动工具条******

        //瓦斯含量点
        private void mniWSHLDFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniWSHLD_ItemClick(null, null);
        }

        //瓦斯压力点
        private void mniWSYLDFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniWSYLD_ItemClick(null, null);
        }

        //瓦斯涌出量点
        private void mniWSYCLDFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniWSYCLD_ItemClick(null, null);
        }

        #endregion

        #region ******绘图浮动工具条******

        //瓦斯压力等值线绘制浮动工具条
        private void mniWSYLDZXHZFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniWSYLDZXHZ_ItemClick(null, null);
        }

        //瓦斯含量等值线绘制浮动工具条
        private void mniWSHLDZXHZFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniWSHLDZXHZ_ItemClick(null, null);
        }

        //瓦斯涌出量等值线绘制浮动工具条
        private void mniWSYCLDZXHZFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniWSYCLDZXHZ_ItemClick(null, null);
        }

        //瓦斯地质图绘制浮动工具条
        private void mniWSDZTHZFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniWSDZTHZ_ItemClick(null, null);
        }

        //瓦斯压力点绘制浮动工具条
        private void mniWSYLDHZFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniWSYLDHZ_ItemClick(null, null);
        }

        //瓦斯含量点绘制浮动工具条
        private void mniWSHLDHZFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniWSHLDHZ_ItemClick(null, null);
        }

        //瓦斯涌出量点绘制浮动工具条   
        private void mniWSYCLDHZFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniWSYCLDHZ_ItemClick(null, null);
        }

        #endregion

        #region ******防突措施浮动工具条******

        //区域措施浮动工具条
        private void mniAreaMeasuresFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniAreaMeasures_ItemClick(null, null);
        }

        //局部措施浮动工具条
        private void mniLocalMeasuresFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniLocalMeasures_ItemClick(null, null);
        }

        //防突钻孔设计
        private void mniFTZKSJFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniFTZKSJ_ItemClick(null, null);
        }

        #endregion

        #region ******参数计算浮动工具条******

        //瓦斯压力点绘制浮动工具条
        private void mniWSYLFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniWSYL_ItemClick(null, null);
        }

        //瓦斯含量点绘制浮动工具条
        private void mniWSHLFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniWSHL_ItemClick(null, null);
        }

        //瓦斯涌出量点绘制浮动工具条
        private void mniWSYCLFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniWSYCL_ItemClick(null, null);
        }

        //反算瓦斯压力/含量浮动工具条
        private void mniFSWSYLHHLFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniFSWSYLHHL_ItemClick(null, null);
        }

        //煤层透气性系数浮动工具条
        private void mniMCTQXXSFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniMCTQXXS_ItemClick(null, null);
        }

        //突出指标(D、K)浮动工具条
        private void mniTCZBFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniTCZB_ItemClick(null, null);
        }

        //抽放管路阻力计算浮动工具条
        private void mniCFGLZLJSFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniCFGLZLJS_ItemClick(null, null);
        }

        //瓦斯抽放管径浮动工具条
        private void mniWSCFGJFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniWSCFGJ_ItemClick(null, null);
        }

        //抽放泵选型浮动工具条
        private void mniCFBXXFloat_ItemClick(object sender, ItemClickEventArgs e)
        {
            mniCFBXX_ItemClick(null, null);
        }

        #endregion

        private void bbiFloorEvevationContour_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;

            var frmMakeContours = new MakeContours
            {
                m_layerName = "FLOOR_ELEVATION_CONTOUR",
                m_layerAliasName = "煤层底板等值线"
            };
            frmMakeContours.Show();
        }

        private void bbiGroundLevelContour_ItemClick(object sender, ItemClickEventArgs e)
        {
            m_currentButton = 0;

            var frmMakeContours = new MakeContours
            {
                m_layerName = "GROUND_LEVLE_CONTOUR",
                m_layerAliasName = "地面标高等值线"
            };
            frmMakeContours.Show();
        }

    }
}