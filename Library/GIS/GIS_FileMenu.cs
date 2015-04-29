using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geoprocessor;
using System.Windows.Forms;
using ESRI.ArcGIS.ConversionTools;
using GIS.FileMenu;
namespace GIS
{
    public class GIS_FileMenu
    {
        public AxMapControl m_AxMapControl;
        private IMapControl3 m_MapControl;
        public AxMapControl AxMapControl
        {
            get
            {
                return m_AxMapControl;
            }
            set
            {
                m_AxMapControl = value;
                m_MapControl = m_AxMapControl.Object as IMapControl3;
            }
        }
        

        /// <summary>
        /// 新建地图文档
        /// </summary>
        public void NewMapDocumnet()
        {
            if (m_AxMapControl.LayerCount > 0)
            {
                DialogResult result = MessageBox.Show(@"是否保存当前地图？", "警告",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel) return;
                if (result == DialogResult.Yes) this.Save();
            }

            //创建新的地图实例
            IMap map = new Map() as IMap;
            map.Name = "Map";
            this.AxMapControl.DocumentFilename = string.Empty;
            //更新新建地图实例的共享地图文档
            this.AxMapControl.Map = map;
        }

        /// <summary>
        /// 打开地图
        /// </summary>
        public void OpenMapDocument()
        {
            if (this.AxMapControl.LayerCount > 0)
            {
                DialogResult result = MessageBox.Show(@"是否保存当前地图？", "警告",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel) return;
                if (result == DialogResult.Yes) this.Save();
            }

            ICommand pCommand = new ControlsOpenDocCommand();
            pCommand.OnCreate(this.AxMapControl.Object);
            pCommand.OnClick();
        }
        

        /// <summary>
        /// 保存地图文档
        /// </summary>
        public void Save()
        {
            //首先确认当前地图文档是否有效
            if (m_MapControl.CheckMxFile(m_MapControl.DocumentFilename))
            {
                ////创建一个新的地图文档实例
                //IMapDocument mapDoc = new MapDocument();
                ////打开当前地图文档
                //mapDoc.Open(m_MapControl.DocumentFilename, string.Empty);

                ////判断地图数据是否只读
                //if (mapDoc.get_IsReadOnly(m_MapControl.DocumentFilename))
                //{
                //    MessageBox.Show("地图文档只读，不能加载!");
                //    mapDoc.Close();
                //    return;
                //}

                ////替换地图目录
                //mapDoc.ReplaceContents((IMxdContents)m_MapControl.Map);

                ////保存地图文档
                //mapDoc.Save(mapDoc.UsesRelativePaths,true);
                //mapDoc.Close();

                
                System.Data.DataTable dt = new System.Data.DataTable("GIS");
                dt.Columns.Add("name");
                dt.Columns.Add("value");
                dt.Columns.Add("params");
                System.Data.DataRow dr = dt.NewRow();
                string extent = Math.Round(m_MapControl.Extent.XMax, 2).ToString() + "," + Math.Round(m_MapControl.Extent.XMin, 2).ToString() + "," + Math.Round(m_MapControl.Extent.YMax, 2).ToString() + "," + Math.Round(m_MapControl.Extent.YMin, 2).ToString();
                dr = dt.NewRow();
                dr[0] = "extent";
                dr[1] = extent;
                dt.Rows.Add(dr);
                string Scale = Math.Round(m_MapControl.MapScale,0).ToString();
                dr = dt.NewRow();
                dr[0] = "Scale";
                dr[1] = Scale;
                dt.Rows.Add(dr);
                string ckScale = Math.Round(m_MapControl.ReferenceScale,0).ToString();
                dr = dt.NewRow();
                dr[0] = "ckScale";
                dr[1] = ckScale;
                dt.Rows.Add(dr);
                string Rotation = Math.Round(m_MapControl.Rotation,0).ToString();
                dr = dt.NewRow();
                dr[0] = "Rotation";
                dr[1] = Rotation;
                dt.Rows.Add(dr);
                for (int i = 0; i < m_MapControl.LayerCount; i++)
                {
                    ILayer player = m_MapControl.get_Layer(i);
                    if (player is IGroupLayer)
                    {
                        dr = dt.NewRow();
                        dr[0] = "layer";
                        dr[1] = player.Name;
                        dr[2] = player.Visible.ToString();
                        dt.Rows.Add(dr);
                        setgisxml(ref dt,player as IGroupLayer);
                    }
                    else
                    {
                        dr = dt.NewRow();
                        dr[0] = "layer";
                        dr[1] = player.Name;
                        dr[2] = player.Visible.ToString();
                        dt.Rows.Add(dr);
                    }
                }
                dt.WriteXml(Application.StartupPath + @"\gis.xml");
            }
            //else
            //    this.SaveAs();//新建后m_MapControl.DocumentFilename为空，故调用另存为进行保存
        }
        private void setgisxml(ref System.Data.DataTable dt,IGroupLayer pGroupLayer)
        { 
            ICompositeLayer comLayer = pGroupLayer as ICompositeLayer;
            System.Data.DataRow dr;
            for (int i = 0; i < comLayer.Count; i++)
            {
                ILayer player = comLayer.get_Layer(i);
                if (player is IGroupLayer)
                {
                    dr = dt.NewRow();
                    dr[0] = "layer";
                    dr[1] = player.Name;
                    dr[2] = player.Visible.ToString();
                    dt.Rows.Add(dr);
                    setgisxml(ref dt, player as IGroupLayer);
                }
                else
                {
                    dr = dt.NewRow();
                    dr[0] = "layer";
                    dr[1] = player.Name;
                    dr[2] = player.Visible.ToString();
                    dt.Rows.Add(dr);
                }
            }
        }
        /// <summary>
        /// 另存为
        /// </summary>
        public void SaveAs()
        {
            //调用另存为命令
            ICommand command = new ControlsSaveAsDocCommand();
            command.OnCreate(this.AxMapControl.Object);
            command.OnClick();
        }
        
        /// <summary>
        /// 添加数据
        /// </summary>
        public void AddData()
        {
            int currentLayerCount = this.AxMapControl.LayerCount;

            ICommand pCommand = new ControlsAddDataCommand();
            pCommand.OnCreate(this.AxMapControl.Object);
            pCommand.OnClick();
        }

        /// <summary>
        /// 导入CAD文件
        /// </summary>
        public void Import()
        {
            OpenFileDialog openDwg = new OpenFileDialog();
            openDwg.Filter = "CAD file(*.DWG)|*.DWG|CAD DGN file(*.DGN)|*.DGN|CAD DXF file(*.DXF)|*.DXF";
            openDwg.Title = "DWG格式数据的输入";
            if (openDwg.ShowDialog() == DialogResult.OK)
            {
                string dwgfileName = openDwg.FileName;
                string fileName = dwgfileName.Substring(dwgfileName.LastIndexOf(@"\") + 1);
                string filePath = dwgfileName.Substring(0, dwgfileName.Length - fileName.Length - 1);
                IFeatureLayer pFeatureLayer;
                //打开CAD数据集
                IWorkspaceFactory2 pWorkspaceFactory = new CadWorkspaceFactory() as IWorkspaceFactory2;
                IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace)pWorkspaceFactory.OpenFromFile(filePath, 0);
                //打开一个要素集
                IFeatureDataset pFeatureDataset = pFeatureWorkspace.OpenFeatureDataset(fileName);
                //IFeaturClassContainer可以管理IFeatureDataset中的每个要素类
                IFeatureClassContainer pFeatureClassContainer = (IFeatureClassContainer)pFeatureDataset;
                //对CAD文件中的要素进行遍历处理
                IEnvelope envelope = new Envelope() as IEnvelope;
                for (int i = 0; i < pFeatureClassContainer.ClassCount; i++)
                {
                    try
                    {
                        IFeatureClass pFeatureClass = pFeatureClassContainer.get_Class(i);
                        if (pFeatureClass.FeatureType == esriFeatureType.esriFTAnnotation)
                        {
                            //如果是注记，则添加注记层
                            pFeatureLayer = new CadAnnotationLayer() as IFeatureLayer;
                        }
                        else//如果是点、线、面，则添加要素层
                        {
                            pFeatureLayer = new FeatureLayer() as IFeatureLayer;
                        }
                        pFeatureLayer.Name = pFeatureClass.AliasName;
                        pFeatureLayer.FeatureClass = pFeatureClass;
                        this.AxMapControl.Map.AddLayer(pFeatureLayer);

                        //

                        IQueryFilter queryFilter = new QueryFilter() as IQueryFilter;
                        queryFilter.WhereClause = "";
                        IFeatureCursor featureCursor = pFeatureLayer.FeatureClass.Search(queryFilter, true);
                        IFeature feature = featureCursor.NextFeature();
                        while (feature != null)
                        {
                            IGeometry geometry = feature.Shape;
                            IEnvelope featureExtent = geometry.Envelope;
                            envelope.Union(featureExtent);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(feature);
                            feature = featureCursor.NextFeature();
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }

                }
                this.AxMapControl.ActiveView.Refresh();
            }

        }
        
        /// <summary>
        /// 导出数据为CAD
        /// </summary>
        public void ExportCAD()
        {
            ExportCADSetup exportCADForm = new ExportCADSetup();
            exportCADForm.AxMapControl = this.m_AxMapControl;
            exportCADForm.Show();
        }

        /// <summary>
        /// 导出为图片或PDF
        /// </summary>
        public void ExportPicPdf()
        {
            try
            {
                SaveFileDialog savePrinterFileDialog = new SaveFileDialog();
                savePrinterFileDialog.Title = "打印成图片";
                savePrinterFileDialog.Filter = "BMP图片(*.bmp)|*.bmp|JPG图片(*.jpg)|*.jpg|PDF 文件(*.pdf)|*.pdf|PNG 图片(*.png)|*.png";
                if (savePrinterFileDialog.ShowDialog() == DialogResult.OK)
                {
                    IActiveView activeView = m_MapControl.ActiveView;
                    if (activeView == null)
                    {
                        return;
                    }

                    IExport export = null;

                    switch (savePrinterFileDialog.FilterIndex)
                    {
                        case 1://bmp
                            export = new ExportBMP() as IExport;//输出BMP格式图片
                            export.ExportFileName = savePrinterFileDialog.FileName;
                            break;
                        case 2://jpg
                            export = new ExportJPEG() as IExport;//输出JPG格式图片
                            export.ExportFileName = savePrinterFileDialog.FileName;
                            break;
                        case 3://pdf
                            export = new ExportPDF() as IExport;//输出PDF格式文件
                            ((IExportPDF2)export).ExportMeasureInfo = true;
                            ((IExportPDF2)export).ExportPDFLayersAndFeatureAttributes = esriExportPDFLayerOptions.esriExportPDFLayerOptionsLayersAndFeatureAttributes;
                            export.ExportFileName = savePrinterFileDialog.FileName;
                            break;
                        case 4://png
                            export = new ExportPNG() as IExport;//输出PNG格式图片
                            export.ExportFileName = savePrinterFileDialog.FileName;
                            break;
                    }

                    if (export == null)
                    {
                        MessageBox.Show(@"请选择输出路径。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int ScreenResolution = 96;
                    int OutputResolution = 300;
                    export.Resolution = OutputResolution;

                    tagRECT exportRECT;
                    exportRECT.left = 0;
                    exportRECT.top = 0;
                    exportRECT.right = activeView.ExportFrame.right * (OutputResolution / ScreenResolution);
                    exportRECT.bottom = activeView.ExportFrame.bottom * (OutputResolution / ScreenResolution);

                    IEnvelope pixelBoundsEnv = new Envelope() as IEnvelope;
                    pixelBoundsEnv.PutCoords(exportRECT.left, exportRECT.top, exportRECT.right, exportRECT.bottom);
                    export.PixelBounds = pixelBoundsEnv;
                    int hDC = export.StartExporting();
                    activeView.Output(hDC, (int)export.Resolution, ref exportRECT, null, null);
                    export.FinishExporting();
                    export.Cleanup();
                }
                MessageBox.Show("导出完成！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        public void PrintPreview()
        {

        }
        
        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            Print printForm = new Print(AxMapControl);
            printForm.FileName = this.m_AxMapControl.DocumentFilename;
            if (this.AxMapControl.DocumentFilename == null || this.AxMapControl.DocumentFilename == "")
            {
                MessageBox.Show(@"没有打印数据，请加载。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            printForm.Show(); 
        }

        /// <summary>
        /// 退出
        /// </summary>
        public void Exit()
        {
            Application.Exit();
        }
        /// <summary>
        /// 导出为Shape
        /// </summary>
        public void ExportShape()
        {
            FileMenu.FrmExploreToShape frm = new FrmExploreToShape();
            frm.AxMapControl = this.m_AxMapControl;
            frm.ShowDialog();
        }
    }
}
