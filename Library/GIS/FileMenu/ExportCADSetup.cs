using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.ConversionTools;

namespace GIS.FileMenu
{
    public partial class ExportCADSetup : Form
    {
        #region 变常量定义
        Dictionary<int, string> m_dicPathAliasName = null;

        public AxMapControl m_AxMapControl;
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
        #endregion

        public ExportCADSetup()
        {
            InitializeComponent();
        }

        private void ExportCADSetup_Load(object sender, EventArgs e)
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
            m_dicPathAliasName = new Dictionary<int, string>();
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
                string layerAliasName = featureLayer.Name;
                //显示图层别名，并保存图层源文件路径和Item序号
                if (!m_dicPathAliasName.ContainsValue(filePath))
                {
                    m_dicPathAliasName.Add(itemNo, filePath);
                    this.cklstCurLayers.Items.Add(layerAliasName);
                    itemNo++;
                }
                
                layer = enumLayer.Next();
            }
            //for (int i = 0; i < this.cklstCurLayers.Items.Count; i++)
            //    this.cklstCurLayers.SetItemChecked(i, true);//默认全选

            //////////////////////
            ///初始化输出类型
            string[] outputType = new string[] {"DGN_V8","DWG_R14","DWG_R2000","DWG_R2004",
                "DWG_R2005","DWG_R2007","DWG_R2010","DXF_R14","DXF_R2000",
                "DXF_R2004","DXF_R2005","DXF_R2007 ","DXF_R2010"};
            this.cbOutputType.Items.AddRange(outputType);
            this.cbOutputType.SelectedItem = "DWG_R2010";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Geoprocessor GP = new Geoprocessor();
            ExportCAD GPExportCAD = new ExportCAD();
            
            //设置in_features属性
            if (this.cklstCurLayers.CheckedItems.Count == 0) return;
            progressBar1.Maximum = cklstCurLayers.CheckedItems.Count;
            string filePath = null;

            //for (int i = 0; i < this.cklstCurLayers.Items.Count; i++)
            //{
            //    if (this.cklstCurLayers.GetItemChecked(i))
            //    {
            //        filePath = filePath + m_dicPathAliasName[i] + ";";
            //    }
            //    //filePath = filePath + this.cklstCurLayers.CheckedItems[i].ToString() + ";";
            //}
            //filePath = filePath.TrimEnd(Convert.ToChar(";"));
            //GPExportCAD.in_features = filePath;

            //设置Output_Type属性
            GPExportCAD.Output_Type = this.cbOutputType.SelectedItem.ToString();

            //设置Output_File属性
            if (this.tbOutputFile.Text == null || this.tbOutputFile.Text == "")
            {
                MessageBox.Show(@"请选择输出路径。", "提示", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            if (System.IO.File.Exists(this.tbOutputFile.Text))
            {
                DialogResult dr = MessageBox.Show("当前文件已存在" + Environment.NewLine + "点击“是”将覆盖现有文件" + Environment.NewLine + "点击“否”将追加到现有文件" + Environment.NewLine + "点击“取消”退出本次操作！", "", MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Yes)
                {
                    System.IO.File.Delete(this.tbOutputFile.Text);
                }
                else if (dr == DialogResult.No)
                { }
                else
                    return;
            }
            GPExportCAD.Output_File = this.tbOutputFile.Text.Trim();

            ///设置Ignore_FileNames属性
            ///IGNORE_FILENAMES_IN_TABLES:忽略文档实体字段中的路径，
            ///并允许将所有实体输出到单个 CAD 文件。 
            ///USE_FILENAMES_IN_TABLES";//允许使用文档实体字段中的路径，
            ///并使用每个实体的路径，以使每个 CAD 部分写入到各自的文件。这是默认设置。 
            //if (this.cbIgnoreFileNames.Checked)
                GPExportCAD.Ignore_FileNames = "IGNORE_FILENAMES_IN_TABLES";
            //else
            //    GPExportCAD.Ignore_FileNames = "USE_FILENAMES_IN_TABLES";

            ///设置Append_To_Existing属性
            ///APPEND_TO_EXISTING_FILES:允许将输出文件内容添加到现有 CAD 输出文件中。
            ///现有 CAD 文件内容不会丢失。
            ///OVERWRITE_EXISTING_FILES";输出文件内容将覆盖现有 CAD 文件内容。这是默认设置。 
            //if (this.cbIgnoreFileNames.Checked)
                GPExportCAD.Append_To_Existing = "APPEND_TO_EXISTING_FILES";
            //else
            //    GPExportCAD.Append_To_Existing = "OVERWRITE_EXISTING_FILES";
            try
            {
                for (int i = 0; i < this.cklstCurLayers.Items.Count; i++)
                {
                    if (this.cklstCurLayers.GetItemChecked(i))
                    {
                        filePath = m_dicPathAliasName[i];
                        GPExportCAD.in_features = filePath;
                        GP.Execute(GPExportCAD, null);
                        progressBar1.Value++;
                    }
                }
                //GP.Execute(GPExportCAD, null);
                MessageBox.Show(@"转换完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {            
            this.Close();
        }

        /// <summary>
        /// 选择导出路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectedPath_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveExportCADFileDialog = new SaveFileDialog();
            saveExportCADFileDialog.OverwritePrompt = false;
            saveExportCADFileDialog.Title = "导出数据";
            saveExportCADFileDialog.Filter = "CAD工程数据集(*.dwg)|*.dwg";
            if (saveExportCADFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.tbOutputFile.Text = saveExportCADFileDialog.FileName;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.cklstCurLayers.Items.Count; i++)
                this.cklstCurLayers.SetItemChecked(i, checkBox1.Checked);//默认全选
        }

     
    }
}
