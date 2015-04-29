using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using ESRI.ArcGIS.Carto;
using GIS;
using GIS.Common;
using LibCommon;
using LibCommonForm;
using LibEntity;

namespace sys3
{
    public partial class TunnelInfoManagement : Form
    {
        /// <summary>
        ///     构造方法
        /// </summary>
        public TunnelInfoManagement()
        {
            InitializeComponent();
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_GM.TUNNEL_INFO_MANAGEMENT);
        }

        private void RefreshData()
        {
            gcTunnel.DataSource = Tunnel.FindAll();
        }

        /// <summary>
        ///     初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TunnelInfoManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
            btnNoWire.BackColor = Const.NO_WIRE_TUNNEL_COLOR;
            btnWired.BackColor = Const.WIRED_TUNNEL_COLOR;
            btnTunnelJJ.BackColor = Const.JJ_TUNNEL_COLOR;
            btnTunnelHC.BackColor = Const.HC_TUNNEL_COLOR;
        }

        /// <summary>
        ///     添加按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            var d = new TunnelInfoEntering();

            if (DialogResult.OK == d.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        ///     修改按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnModify_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRow() == null)
            {
                Alert.alert("请选择要修改的信息");
                return;
            }
            var d = new TunnelInfoEntering((Tunnel) gridView1.GetFocusedRow());
            if (DialogResult.OK == d.ShowDialog())
            {
                RefreshData();
            }
        }

        /// <summary>
        ///     删除按钮响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnDel_Click(object sender, EventArgs e)
        {
            if (!Alert.confirm(Const_GM.TUNNEL_INFO_MSG_DEL)) return;
            //掘进ID
            var selectedIndex = gridView1.GetSelectedRows();
            foreach (var tunnel in selectedIndex.Select(i => (Tunnel) gridView1.GetRow(i)))
            {
                GisHelper.DelHdByHdId(tunnel.TunnelId.ToString(CultureInfo.InvariantCulture));
                tunnel.Delete();
            }
            RefreshData();
        }

        /// <summary>
        ///     退出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        ///     导出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                gcTunnel.ExportToXls(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        ///     打印按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcTunnel, "巷道信息报表");
        }

        /// <summary>
        ///     刷新按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            // 获取已选择明细行的索引
            var pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_TUNNEL);
            if (pLayer == null)
            {
                MessageBox.Show(@"未发现巷道全图层！");
                return;
            }
            var pFeatureLayer = (IFeatureLayer) pLayer;
            //for (int i = 0; i < iSelIdxsArr.Length; i++)
            //{
            var tunnel = (Tunnel) gridView1.GetFocusedRow();
            //if (bid != "")
            //{
            //if (true)
            var str = "HdId='" + tunnel.TunnelId + "'";
            //else
            //    str += " or HdId='" + bid + "'";
            //}
            //}
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
                Alert.alert("图元丢失");
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "TunnelType")
            {
                switch (e.DisplayText)
                {
                    case "OTHER":
                        e.DisplayText = "其他";
                        break;
                    case "TUNNELLING":
                        e.DisplayText = "掘进巷道";
                        break;
                    case "STOPING_OTHER":
                        e.DisplayText = "回采面其他关联巷道";
                        break;
                    case "STOPING_QY":
                        e.DisplayText = "切眼";
                        break;
                    case "STOPING_FY":
                        e.DisplayText = "辅运顺槽";
                        break;
                    case "STOPING_ZY":
                        e.DisplayText = "主运顺槽";
                        break;
                    case "HENGCHUAN":
                        e.DisplayText = "横川";
                        break;
                }
            }
        }
    }
}