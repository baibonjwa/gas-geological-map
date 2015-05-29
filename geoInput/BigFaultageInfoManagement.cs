using System;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using GIS;
using GIS.Common;
using GIS.HdProc;
using LibCommon;
using LibEntity;

namespace geoInput
{
    public partial class BigFaultageInfoManagement : Form
    {
        // 构造方法
        public BigFaultageInfoManagement()
        {
            InitializeComponent();
        }

        private void RefreshData()
        {
            var bigFaultages = InferFaultage.FindAll();
            gcBigFaultage.DataSource = bigFaultages;
        }

        /// <summary>
        ///     添加（必须实装）
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var m = new BigFaultageInfoEntering();

            if (DialogResult.OK == m.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        ///     修改（必须实装）
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRow() == null)
            {
                Alert.AlertMsg("请选择要修改的信息");
                return;
            }
            var bigFaultageInfoEntering = new BigFaultageInfoEntering(((InferFaultage)gridView1.GetFocusedRow()));
            if (DialogResult.OK == bigFaultageInfoEntering.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        ///     删除按钮（必须实装）
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!Alert.Confirm("确认删除数据吗？")) return;
            var selectedIndex = gridView1.GetSelectedRows();
            foreach (var bigFaultage in selectedIndex.Select(i => (InferFaultage)gridView1.GetRow(i)))
            {
                Global.tdclass.DelTdLyr(new[] { bigFaultage.bid });
                bigFaultage.Delete();
            }
            RefreshData();
        }

        /// <summary>
        ///     退出
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     导出
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                gcBigFaultage.ExportToXls(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        ///     打印
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcBigFaultage, "推断断层信息报表");
        }

        /// <summary>
        ///     刷新
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        ///     图显按钮事件
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void btnMap_Click(object sender, EventArgs e)
        {
            // 获取已选择明细行的索引
            int[] iSelIdxsArr = { ((InferFaultage)gridView1.GetFocusedRow()).id };

            var pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.DEFALUT_INFERRED_FAULTAGE);
            if (pLayer == null)
            {
                MessageBox.Show(@"未发现推断断层图层！");
                return;
            }
            var pFeatureLayer = (IFeatureLayer)pLayer;
            var str = "";
            for (var i = 0; i < iSelIdxsArr.Length; i++)
            {
                var bid = ((InferFaultage)gridView1.GetFocusedRow()).bid;
                if (bid == "") continue;
                if (i == 0)
                    str = "bid='" + bid + "'";
                else
                    str += " or bid='" + bid + "'";
            }
            var list = MyMapHelp.FindFeatureListByWhereClause(pFeatureLayer, str);
            if (list.Count > 0)
            {
                MyMapHelp.Jump(MyMapHelp.GetGeoFromFeature(list));
                DataEditCommon.g_pMap.ClearSelection();
                for (var i = 0; i < list.Count; i++)
                {
                    DataEditCommon.g_pMap.SelectFeature(pLayer, list[i]);
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

        private void BigFaultageInfoManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}