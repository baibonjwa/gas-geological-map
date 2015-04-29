using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace GIS
{
    public partial class FeatureAttribute : Form
    {
        IFeature m_pFeature;
        public FeatureAttribute(IFeature pFeature)
        {
            InitializeComponent();
            m_pFeature = pFeature;
            dgvBasicProperties.BackgroundColor = System.Drawing.Color.White;
            dgvBasicProperties.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            dgvBasicProperties.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.White;
            dgvBasicProperties.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(dgvBasicProperties.Font, System.Drawing.FontStyle.Bold);
            dgvBasicProperties.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dgvBasicProperties.GridColor = System.Drawing.Color.Black;
            dgvBasicProperties.RowHeadersVisible = false;
            dgvBasicProperties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvBasicProperties.MultiSelect = false;
            dgvBasicProperties.AllowUserToAddRows = false;
            //dgvBasicProperties.ReadOnly = true;
            dgvBasicProperties.RowCount = pFeature.Fields.FieldCount;
            dgvBasicProperties.ColumnCount = 2;
            dgvBasicProperties.Columns[0].HeaderText = "字段";
            dgvBasicProperties.Columns[0].ReadOnly = true;
            dgvBasicProperties.Columns[1].HeaderText = "字段值";
            dgvBasicProperties.Columns[1].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            for (int j = 0; j < pFeature.Fields.FieldCount; j++)
            {
                if (pFeature.Fields.get_Field(j).Type == esriFieldType.esriFieldTypeGeometry)
                {
                    dgvBasicProperties[0, j].Value = "要素类型";
                    dgvBasicProperties[1, j].Value = "Shape";
                }
                else
                {
                    dgvBasicProperties[0, j].Value = pFeature.Fields.get_Field(j).AliasName;
                    dgvBasicProperties[1, j].Value = pFeature.get_Value(j);
                }
            }
            //panel1.Visible = false;
            tabControl1.TabPages.RemoveAt(1);
        }
       
        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (m_pFeature == null)
                return;
            GIS.Common.DataEditCommon.g_CurWorkspaceEdit.StartEditing(false);
            GIS.Common.DataEditCommon.g_CurWorkspaceEdit.StartEditOperation();
            for (int i = 0; i < this.dgvBasicProperties.Rows.Count; i++)
            {
                try
                {
                    if (m_pFeature.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                        continue;
                    m_pFeature.Value[i] = this.dgvBasicProperties[1, i].Value;
                }
                catch (Exception)
                {
                    continue;
                }
            }
            m_pFeature.Store();
            GIS.Common.DataEditCommon.g_CurWorkspaceEdit.StopEditOperation();
            GIS.Common.DataEditCommon.g_CurWorkspaceEdit.StopEditing(true);
            GIS.Common.DataEditCommon.g_pAxMapControl.ActiveView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewBackground, null, null);
        }

    }
}
