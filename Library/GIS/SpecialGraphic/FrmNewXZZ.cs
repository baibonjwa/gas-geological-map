using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibCommon;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using GIS.Common;
using ESRI.ArcGIS.Geodatabase;

namespace GIS.SpecialGraphic
{
    public partial class FrmNewXZZ : Form
    {

        public FrmNewXZZ()
        {
            InitializeComponent();
        }
        
        #region 保存
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //去除无用空行
                for (int i = 0; i < dgrdvZhzzt.RowCount - 1; i++)
                {
                    if (this.dgrdvZhzzt.Rows[i].Cells[0].Value == null &&
                        this.dgrdvZhzzt.Rows[i].Cells[1].Value == null &&
                        this.dgrdvZhzzt.Rows[i].Cells[2].Value == null)
                    {
                        this.dgrdvZhzzt.Rows.RemoveAt(i);
                    }
                }
                this.DialogResult = DialogResult.OK;
                string bid = IdGenerator.NewBindingId();
                //实体赋值
                IPoint pt = new PointClass();
                pt.X = Convert.ToDouble(txtX.Text);
                pt.Y = Convert.ToDouble(txtY.Text);
                pt.Z = 0;
                double bili = Convert.ToDouble(txtBlc.Text);
                //List<double> list = new List<double>();
                var datasources = new List<KeyValuePair<int, double>>();
                for (int i = 0; i < dgrdvZhzzt.RowCount - 1; i++)
                {
                    DataGridViewTextBoxCell cell = dgrdvZhzzt.Rows[i].Cells[0] as DataGridViewTextBoxCell;
                    DataGridViewComboBoxCell cell1 = dgrdvZhzzt.Rows[i].Cells[1] as DataGridViewComboBoxCell;
                    //list.Add(Convert.ToDouble(cell.Value.ToString()));
                    int key=0;
                    if(cell1.Value.ToString()=="煤层")
                        key=1;
                    datasources.Add(new KeyValuePair<int, double>(key, Convert.ToDouble(cell.Value.ToString())));
                }
                //list.Add(Convert.ToDouble(txtDBBG.Text));
                datasources.Add(new KeyValuePair<int, double>(2, Convert.ToDouble(txtDBBG.Text)));
                if (this.Text.Equals("修改小柱状") && this.Tag != null)
                {
                    bid = this.Tag.ToString();
                    var AnnoLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_AnnotationXZZ) as IFeatureLayer;//注记图层
                    var lineLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_PolylineXZZ) as IFeatureLayer;//线源图层
                    var topLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_PolygonXZZ) as IFeatureLayer; //外部图形图层
                    if (AnnoLayer == null || lineLayer == null || topLayer == null)
                    {
                        System.Windows.Forms.MessageBox.Show("小柱状图层缺失！");
                        return;
                    }
                    DataEditCommon.DeleteFeatureByBId(AnnoLayer,bid);
                    DataEditCommon.DeleteFeatureByBId(lineLayer, bid);
                    DataEditCommon.DeleteFeatureByBId(topLayer, bid);
                }
                if (DrawXZZ.drawXZZ(datasources, pt, Convert.ToDouble(txtAngle.Text), bid, bili))
                {
                    FrmNewXZZ frm = new FrmNewXZZ();
                    frm.Show(this.Owner);
                    frm.Location = this.Location;
                    this.Close();
                    DataEditCommon.g_pAxMapControl.CurrentTool = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region datagrid事件
        private void dgrdvZhzzt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != dgrdvZhzzt.Rows.Count - 1 && e.ColumnIndex == 2 && Alert.Confirm("确认要删除吗？"))
            {
                if (e.ColumnIndex == 2)
                {
                    dgrdvZhzzt.Rows.RemoveAt(e.RowIndex);
                }
            }
        }
        /// <summary>
        /// 显示行号
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void dgrdvZhzzt_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y, dgrdvZhzzt.RowHeadersWidth - 4, e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgrdvZhzzt.RowHeadersDefaultCellStyle.Font, rectangle,
                dgrdvZhzzt.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        //右键菜单
        private void dgrdvZhzzt_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (this.dgrdvZhzzt.Rows[e.RowIndex].Selected == false)
                    {
                        this.dgrdvZhzzt.ClearSelection();
                        this.dgrdvZhzzt.Rows[e.RowIndex].Selected = true;
                        this.dgrdvZhzzt.CurrentCell = this.dgrdvZhzzt.Rows[e.RowIndex].Cells[0];
                    }
                }
            }
        }
        #endregion
        #region 菜单事件
        /// <summary>
        /// 上移
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iNowIndex = this.dgrdvZhzzt.CurrentRow.Index;

            if (iNowIndex == 0)
            {
                Alert.AlertMsg("无法上移");
                return;
            }

            object[] objArrRowData = new object[2];

            int index = -1;
            int n = -1;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;

            index = -1;
            this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value = this.dgrdvZhzzt.Rows[iNowIndex - 1].Cells[index].Value;
            this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value = this.dgrdvZhzzt.Rows[iNowIndex - 1].Cells[index].Value;

            index = -1;
            n = -1;
            this.dgrdvZhzzt.Rows[iNowIndex - 1].Cells[++index].Value = objArrRowData[++n];
            this.dgrdvZhzzt.Rows[iNowIndex - 1].Cells[++index].Value = objArrRowData[++n];

            this.dgrdvZhzzt.CurrentCell = this.dgrdvZhzzt.Rows[iNowIndex - 1].Cells[0];//设定当前行
            this.dgrdvZhzzt.Rows[iNowIndex - 1].Selected = true;

        }

        /// <summary>
        /// 下移
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iNowIndex = this.dgrdvZhzzt.CurrentRow.Index;

            if (iNowIndex == this.dgrdvZhzzt.Rows.Count - 2 || iNowIndex == this.dgrdvZhzzt.Rows.Count - 1)
            {
                Alert.AlertMsg("无法下移");
                return;
            }

            object[] objArrRowData = new object[2];

            int index = -1;
            int n = -1;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;

            index = -1;
            this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value = this.dgrdvZhzzt.Rows[iNowIndex + 1].Cells[index].Value;
            this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value = this.dgrdvZhzzt.Rows[iNowIndex + 1].Cells[index].Value;

            index = -1;
            n = -1;
            this.dgrdvZhzzt.Rows[iNowIndex + 1].Cells[++index].Value = objArrRowData[++n];
            this.dgrdvZhzzt.Rows[iNowIndex + 1].Cells[++index].Value = objArrRowData[++n];

            this.dgrdvZhzzt.CurrentCell = this.dgrdvZhzzt.Rows[iNowIndex + 1].Cells[0];//设定当前行
            this.dgrdvZhzzt.Rows[iNowIndex + 1].Selected = true;
        }

        object[] _objArrRowData = new object[4];
        /// <summary>
        /// 插入
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void 插入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iNowIndex = this.dgrdvZhzzt.CurrentRow.Index;

            DataGridViewRow newRow = new DataGridViewRow();//新建行
            this.dgrdvZhzzt.Rows.Insert(iNowIndex, newRow);//当前行的上面插入新行
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iNowIndex = this.dgrdvZhzzt.CurrentRow.Index;

            object[] objArrRowData = new object[2];

            int index = -1;
            int n = -1;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;

            _objArrRowData = objArrRowData;

            //MessageBox.Show("复制成功！");

            this.contextMenuStrip1.Items["粘贴ToolStripMenuItem"].Visible = true;
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iNowIndex = this.dgrdvZhzzt.CurrentRow.Index;

            if (iNowIndex == this.dgrdvZhzzt.Rows.Count - 1)
            {
                this.dgrdvZhzzt.Rows.Add();
            }

            int index = -1;
            int n = -1;
            this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value = _objArrRowData[++n];
            this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value = _objArrRowData[++n];

            this.contextMenuStrip1.Items["粘贴ToolStripMenuItem"].Visible = false;
        }

        /// <summary>
        /// 剪切
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iNowIndex = this.dgrdvZhzzt.CurrentRow.Index;

            object[] objArrRowData = new object[2];

            int index = -1;
            int n = -1;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = this.dgrdvZhzzt.Rows[iNowIndex].Cells[++index].Value;

            _objArrRowData = objArrRowData;

            this.dgrdvZhzzt.Rows.Remove(this.dgrdvZhzzt.CurrentRow);

            //MessageBox.Show("剪切成功！");

            this.contextMenuStrip1.Items["粘贴ToolStripMenuItem"].Visible = true;
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            DataEditCommon.g_pAxMapControl.CurrentTool = null;
        }

        private void btnQD_Click(object sender, EventArgs e)
        {
            GIS.Common.DataEditCommon.PickUpPoint(txtX, txtY);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ESRI.ArcGIS.SystemUI.ICommand command = new ToolUpdateXZZ(this);
            command.OnCreate(DataEditCommon.g_pAxMapControl.Object);
            if (command.Enabled)
            {
                DataEditCommon.g_pAxMapControl.CurrentTool = (ESRI.ArcGIS.SystemUI.ITool)command;
            }
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            if (this.Tag == null || this.Text != "修改小柱状")
            {
                MessageBox.Show("请先使用修改按钮选中要删除的小柱状");
                return;
            }
            string bid = this.Tag.ToString();
            var AnnoLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_AnnotationXZZ) as IFeatureLayer;//注记图层
            var lineLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_PolylineXZZ) as IFeatureLayer;//线源图层
            var topLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_PolygonXZZ) as IFeatureLayer; //外部图形图层
            if (AnnoLayer == null || lineLayer == null || topLayer == null)
            {
                return;
            }
            DataEditCommon.DeleteFeatureByBId(AnnoLayer, bid);
            DataEditCommon.DeleteFeatureByBId(lineLayer, bid);
            DataEditCommon.DeleteFeatureByBId(topLayer, bid);
            FrmNewXZZ frm = new FrmNewXZZ();
            frm.Show(this.Owner);
            frm.Location = this.Location;
            this.Close();
            DataEditCommon.g_pAxMapControl.CurrentTool = null;
        }
    }
}
