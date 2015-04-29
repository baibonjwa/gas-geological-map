using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using System.IO;

namespace GIS.FileMenu
{
    public partial class FrmExploreToShape : Form
    {
        Dictionary<int, IFeatureClass> m_dicPathAliasName = null;
        private AxMapControl m_AxMapControl;
        public AxMapControl AxMapControl
        {
            get
            {
                return m_AxMapControl;
            }
            set
            {
                m_AxMapControl = value;
            }
        }
        public FrmExploreToShape()
        {
            InitializeComponent();
        }

        private void FrmExploreToShape_Load(object sender, EventArgs e)
        {
            InitControls();
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            //////////////////////////////
            ///初始化当前图层控件
            IEnumLayer enumLayer = this.m_AxMapControl.ActiveView.FocusMap.get_Layers();
            ILayer layer = enumLayer.Next();
            int itemNo = 0;
            m_dicPathAliasName = new Dictionary<int, IFeatureClass>();
            string workspacePath = GIS.Common.SDEOperation.GetSDEWorkspace().PathName;
            while (layer != null)
            {
                if (layer is IGroupLayer)
                {
                    layer = enumLayer.Next();
                    continue;
                }

                IDataLayer dataLayer = layer as IDataLayer;
                if (dataLayer == null)
                {
                    layer = enumLayer.Next();
                    continue;
                }
                IDatasetName dsName = dataLayer.DataSourceName as IDatasetName;
                string strDSName = dsName.Name;
                //string workspacePath = dsName.WorkspaceName.PathName;
                string filePath = workspacePath + "\\" + strDSName;
                //this.cklstCurLayers.Items.Add(filePath);

                IFeatureLayer featureLayer = dataLayer as IFeatureLayer;
                IFeatureClass pFeatureclass = featureLayer.FeatureClass;
                string layerAliasName = featureLayer.Name;
                //显示图层别名，并保存图层源文件路径和Item序号
                if (featureLayer.FeatureClass != null)
                {
                    if (!m_dicPathAliasName.ContainsValue(pFeatureclass) && pFeatureclass.FeatureType != esriFeatureType.esriFTAnnotation)
                    {
                        m_dicPathAliasName.Add(itemNo, pFeatureclass);
                        this.cklstCurLayers.Items.Add(layerAliasName);
                        itemNo++;
                    }
                }

                layer = enumLayer.Next();
            }
        }
        /// <summary>
        /// 选择导出路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectedPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog saveExportCADFileDialog = new FolderBrowserDialog();
            if (saveExportCADFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.tbOutputFile.Text = saveExportCADFileDialog.SelectedPath;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.cklstCurLayers.Items.Count; i++)
                this.cklstCurLayers.SetItemChecked(i, checkBox1.Checked);//默认全选
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.cklstCurLayers.CheckedItems.Count == 0)
            {
                MessageBox.Show("请选择要导出的图层！");
                return;
            }
            if (this.tbOutputFile.Text.Trim().Equals(""))
            {
                MessageBox.Show("请选择导出路径！");
                return;
            }
            if (System.IO.Directory.Exists(this.tbOutputFile.Text) == false)
            {
                MessageBox.Show("导出路径不存在！");
                return;
            }
            progressBar1.Maximum = cklstCurLayers.CheckedItems.Count;
            string outpath = this.tbOutputFile.Text + "\\";

            List<IFeatureClass> list = new List<IFeatureClass>();
            int succount = 0;
            try
            {
                for (int i = 0; i < this.cklstCurLayers.Items.Count; i++)
                {
                    if (this.cklstCurLayers.GetItemChecked(i))
                    {
                        list.Add(m_dicPathAliasName[i]);
                    }
                }
                if (list.Count > 0)
                {
                    IDataset pDataSet = list[0] as IDataset;
                    IWorkspace pWorkspace = pDataSet.Workspace;

                    IWorkspaceFactory pWSF = new ShapefileWorkspaceFactoryClass();
                    IFeatureWorkspace pWS = (IFeatureWorkspace)pWSF.OpenFromFile(outpath, 0);
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].FeatureType == esriFeatureType.esriFTAnnotation)
                        {
                            MessageBox.Show("注记无法转换成shape格式！");
                            progressBar1.Value++;
                            continue;
                        }
                        string fileName = (list[i] as IDataset).Name;
                        string name = fileName.Substring(fileName.LastIndexOf('.') + 1);
                        string fullFileName = outpath + name + ".shp";
                        fullFileName = fullFileName.Replace(@"//", @"/");
                        if (File.Exists(fullFileName))
                        { //如果已经存在就先删除
                            DirectoryInfo di = new DirectoryInfo(outpath);
                            FileInfo[] fis = di.GetFiles(fileName + "*");
                            foreach (FileInfo fi in fis)
                            {
                                fi.Delete();
                            }
                        }
                        if (IFeatureDataConverter_ConvertFeatureClass(pWorkspace, (IWorkspace)pWS, fileName, name))
                            succount++;
                        progressBar1.Value++;
                    }
                }
                //GP.Execute(GPExportCAD, null);
                MessageBox.Show("成功转换" + succount.ToString() + "个图层！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 将一个要素类从一个工作空间转移到另外一个工作空间
        /// 注意目标工作空间不能有改要素类，必须先清除  
        /// </summary>
        /// <param name="sourceWorkspace">源工作空间</param>
        /// <param name="targetWorkspace">目标工作空间</param>
        /// <param name="nameOfSourceFeatureClass">源要素类名</param>
        /// <param name="nameOfTargetFeatureClass">目标要素类名</param>
        public bool IFeatureDataConverter_ConvertFeatureClass(IWorkspace sourceWorkspace, IWorkspace targetWorkspace, string nameOfSourceFeatureClass, string nameOfTargetFeatureClass)
        {
            bool change = false;
            //create source workspace name   
            IDataset sourceWorkspaceDataset = (IDataset)sourceWorkspace;
            IWorkspaceName sourceWorkspaceName = (IWorkspaceName)sourceWorkspaceDataset.FullName;
            //create source dataset name   
            IFeatureClassName sourceFeatureClassName = new FeatureClassNameClass();
            IDatasetName sourceDatasetName = (IDatasetName)sourceFeatureClassName;
            sourceDatasetName.WorkspaceName = sourceWorkspaceName;
            sourceDatasetName.Name = nameOfSourceFeatureClass;
            //create target workspace name   
            IDataset targetWorkspaceDataset = (IDataset)targetWorkspace;
            IWorkspaceName targetWorkspaceName = (IWorkspaceName)targetWorkspaceDataset.FullName;
            //create target dataset name   
            IFeatureClassName targetFeatureClassName = new FeatureClassNameClass();
            IDatasetName targetDatasetName = (IDatasetName)targetFeatureClassName;
            targetDatasetName.WorkspaceName = targetWorkspaceName;
            targetDatasetName.Name = nameOfTargetFeatureClass;
            //Open input Featureclass to get field definitions.   
            ESRI.ArcGIS.esriSystem.IName sourceName = (ESRI.ArcGIS.esriSystem.IName)sourceFeatureClassName;
            IFeatureClass sourceFeatureClass = (IFeatureClass)sourceName.Open();

            //Validate the field names because you are converting between different workspace types.   
            IFieldChecker fieldChecker = new FieldCheckerClass();
            IFields targetFeatureClassFields;
            IFields sourceFeatureClassFields = sourceFeatureClass.Fields;
            IEnumFieldError enumFieldError;
            // Most importantly set the input and validate workspaces!     
            fieldChecker.InputWorkspace = sourceWorkspace;
            fieldChecker.ValidateWorkspace = targetWorkspace;
            fieldChecker.Validate(sourceFeatureClassFields, out enumFieldError, out targetFeatureClassFields);
            // Loop through the output fields to find the geomerty field   
            IField geometryField;
            for (int i = 0; i < targetFeatureClassFields.FieldCount; i++)
            {
                if (targetFeatureClassFields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                {
                    geometryField = targetFeatureClassFields.get_Field(i);
                    // Get the geometry field's geometry defenition            
                    IGeometryDef geometryDef = geometryField.GeometryDef;
                    //Give the geometry definition a spatial index grid count and grid size        
                    IGeometryDefEdit targetFCGeoDefEdit = (IGeometryDefEdit)geometryDef;
                    targetFCGeoDefEdit.GridCount_2 = 1;
                    targetFCGeoDefEdit.set_GridSize(0, 0);
                    //Allow ArcGIS to determine a valid grid size for the data loaded      
                    targetFCGeoDefEdit.SpatialReference_2 = geometryField.GeometryDef.SpatialReference;
                    // we want to convert all of the features   
                    IQueryFilter queryFilter = new QueryFilterClass();
                    queryFilter.WhereClause = "";
                    // Load the feature class     
                    IFeatureDataConverter fctofc = new FeatureDataConverterClass();
                    IEnumInvalidObject enumErrors = fctofc.ConvertFeatureClass(sourceFeatureClassName, queryFilter, null, targetFeatureClassName, geometryDef, targetFeatureClassFields, "", 1000, 0);
                    if (enumErrors.Next() == null)
                        change = true;
                    break;
                }
            }
            return change;
        }
    }
}
