using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using GIS;
using GIS.Common;
using LibCommon;
using LibEntity;

namespace geoInput
{
    public partial class BoreholeInfoManagement : Form
    {
        /// <summary>
        ///     构造方法
        /// </summary>
        public BoreholeInfoManagement()
        {
            InitializeComponent();
        }

        private void RefreshData()
        {
            gcBorehole.DataSource = Borehole.FindAll();
        }

        /// <summary>
        ///     添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var m = new BoreholeInfoEntering();
            var result = m.ShowDialog();
            if (result == DialogResult.OK || result == DialogResult.Cancel)
            {
                RefreshData();
            }
        }

        /// <summary>
        ///     修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRow() == null)
            {
                Alert.AlertMsg("请选择要修改的信息");
                return;
            }
            var m = new BoreholeInfoEntering(((Borehole) gridView1.GetFocusedRow()));
            if (DialogResult.OK == m.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        ///     删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!Alert.Confirm("确认要删除该钻孔吗？")) return;
            var selectedIndex = gridView1.GetSelectedRows();
            foreach (var borehole in selectedIndex.Select(i => (Borehole) gridView1.GetRow(i)))
            {
                DeleteZuanKongByBid(new[] {borehole.bid});
                borehole.Delete();
            }
            RefreshData();
        }

        /// <summary>
        ///     根据钻孔绑定ID删除钻孔图元
        /// </summary>
        /// <param name="sBoreholeBidArray">要删除钻孔的绑定ID</param>
        private static void DeleteZuanKongByBid(ICollection<string> sBoreholeBidArray)
        {
            if (sBoreholeBidArray.Count == 0) return;

            //1.获得当前编辑图层
            var drawspecial = new DrawSpecialCommon();
            const string sLayerAliasName = LayerNames.DEFALUT_BOREHOLE; //“钻孔”图层
            var featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            if (featureLayer == null)
            {
                MessageBox.Show(@"未找到" + sLayerAliasName + @"图层,无法删除钻孔图元。");
                return;
            }

            //2.删除钻孔图元
            foreach (var sBoreholeBid in sBoreholeBidArray)
            {
                DataEditCommon.DeleteFeatureByBId(featureLayer, sBoreholeBid);
            }
        }

        /// <summary>
        ///     退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            // 关闭窗口
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
                gcBorehole.ExportToXls(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        ///     打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcBorehole, "钻孔信息报表");
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

            var pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.DEFALUT_BOREHOLE);
            if (pLayer == null)
            {
                MessageBox.Show(@"未发现钻孔图层！");
                return;
            }
            var pFeatureLayer = (IFeatureLayer) pLayer;
            var str = "";
            var bid = ((Borehole) gridView1.GetFocusedRow()).bid;
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

        private void BoreholeInfoManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}