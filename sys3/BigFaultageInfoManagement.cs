using System;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using GIS;
using GIS.Common;
using GIS.HdProc;
using LibCommon;
using LibEntity;

namespace sys3
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
            var bigFaultages = BigFaultage.FindAll();
            gcBigFaultage.DataSource = bigFaultages;
        }

        /// <summary>
        ///     添加（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRow() == null)
            {
                Alert.alert("请选择要修改的信息");
                return;
            }
            var bigFaultageInfoEntering = new BigFaultageInfoEntering(((BigFaultage) gridView1.GetFocusedRow()));
            if (DialogResult.OK == bigFaultageInfoEntering.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        ///     删除按钮（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!Alert.confirm("确认删除数据吗？")) return;
            var selectedIndex = gridView1.GetSelectedRows();
            foreach (var bigFaultage in selectedIndex.Select(i => (BigFaultage) gridView1.GetRow(i)))
            {
                Global.tdclass.DelTdLyr(new[] {bigFaultage.BindingId});
                bigFaultage.Delete();
            }
            RefreshData();
        }

        /// <summary>
        ///     退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcBigFaultage, "推断断层信息报表");
        }

        /// <summary>
        ///     刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        ///     图显按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMap_Click(object sender, EventArgs e)
        {
            // 获取已选择明细行的索引
            int[] iSelIdxsArr = {((BigFaultage) gridView1.GetFocusedRow()).BigFaultageId};

            var pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.DEFALUT_INFERRED_FAULTAGE);
            if (pLayer == null)
            {
                MessageBox.Show(@"未发现推断断层图层！");
                return;
            }
            var pFeatureLayer = (IFeatureLayer) pLayer;
            var str = "";
            for (var i = 0; i < iSelIdxsArr.Length; i++)
            {
                var bid = ((BigFaultage) gridView1.GetFocusedRow()).BindingId;
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
                Alert.alert("图元丢失");
            }
        }

        private void BigFaultageInfoManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}