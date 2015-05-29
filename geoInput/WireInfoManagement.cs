using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using GIS;
using GIS.Common;
using LibCommon;
using LibEntity;

namespace geoInput
{
    public partial class WireInfoManagement : Form
    {
        /// <summary>
        ///     构造方法
        /// </summary>
        public WireInfoManagement()
        {
            InitializeComponent();
        }

        private void RefreshData()
        {
            var wires = Wire.FindAll();
            gcWireInfo.DataSource = wires;
        }

        /// <summary>
        ///     初始化
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void WireInfoManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        ///     添加按钮响应
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            var wireInfoForm = new WireInfoEntering();
            if (DialogResult.OK == wireInfoForm.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        ///     修改按钮响应
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            var wire = (Wire)gridView1.GetFocusedRow();
            if (wire == null)
            {
                Alert.AlertMsg("请选择要修改的巷道");
                return;
            }
            var wireInfoForm = new WireInfoEntering(wire);
            if (DialogResult.OK == wireInfoForm.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        ///     删除按钮响应
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            //是否删除导线点
            if (!Alert.Confirm("确认要删除吗？")) return;
            var selectedIndex = gridView1.GetSelectedRows();
            foreach (var wire in selectedIndex.Select(index => (Wire)gridView1.GetRow(index)))
            {
                GisHelper.DelHdByHdId(wire.tunnel.id.ToString(CultureInfo.InvariantCulture));
                wire.Delete();
            }

            RefreshData();
        }

        /// <summary>
        ///     刷新按钮响应
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        ///     退出按钮响应
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     导出按钮
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                gcWireInfo.ExportToXls(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        ///     打印按钮
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcWireInfo, "巷道导线点信息报表");
        }

        /// <summary>
        ///     图显按钮事件
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void btnMap_Click(object sender, EventArgs e)
        {
            // 获取已选择明细行的索引
            var pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.DEFALUT_WIRE_PT);
            if (pLayer == null)
            {
                MessageBox.Show(@"未发现导线点图层！");
                return;
            }
            var pFeatureLayer = (IFeatureLayer)pLayer;
            var str = "";
            var bid = ((Wire)gridView1.GetFocusedRow()).tunnel.bid;
            if (bid != "")
            {
                if (true)
                    str = "bid='" + bid + "'";
                //else
                //    str += " or bid='" + bid + "'";
            }
            var list = MyMapHelp.FindFeatureListByWhereClause(pFeatureLayer, str);
            if (list.Count > 0)
            {
                MyMapHelp.Jump(MyMapHelp.GetGeoFromFeature(list));
                DataEditCommon.g_pMap.ClearSelection();
                foreach (var t in list)
                {
                    DataEditCommon.g_pMap.SelectFeature(pLayer, t);
                }
                WindowState = FormWindowState.Normal;
                Location = DataEditCommon.g_axTocControl.Location;
                Width = DataEditCommon.g_axTocControl.Width;
                Height = DataEditCommon.g_axTocControl.Height;
                DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null,
                    DataEditCommon.g_pAxMapControl.Extent);
            }
            else
            {
                Alert.AlertMsg("图元丢失");
            }
        }
    }
}