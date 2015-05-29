using System;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using GIS;
using GIS.Common;
using LibCommon;
using LibEntity;

namespace ggm
{
    public partial class GasPressureInfoManagement : Form
    {
        /// <summary>
        ///     构造方法
        /// </summary>
        public GasPressureInfoManagement()
        {
            InitializeComponent();
        }

        private void RefreshData()
        {
            gcGasPressure.DataSource = GasPressure.FindAll();
        }

        /// <summary>
        ///     添加（必须实装）
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var gasPressureInfoEnteringForm = new GasPressureInfoEntering();
            if (DialogResult.OK == gasPressureInfoEnteringForm.ShowDialog())
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
            var gasPressureInfoEnteringForm = new GasPressureInfoEntering((GasPressure)gridView1.GetFocusedRow());
            if (DialogResult.OK == gasPressureInfoEnteringForm.ShowDialog())
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
            if (!Alert.Confirm("确定要删除瓦斯压力数据吗？")) return;
            // 瓦斯压力数据删除
            var selectedIndex = gridView1.GetSelectedRows();
            foreach (var gasPressure in selectedIndex.Select(i => (GasPressure)gridView1.GetRow(i)))
            {
                DelGasGushQuantityPt(new[] { gasPressure.bid });
                gasPressure.Delete();
            }
            RefreshData();
        }

        /// <summary>
        ///     删除瓦斯信息
        /// </summary>
        /// <params name="bid">绑定ID</params>
        private void DelGasGushQuantityPt(string[] bid)
        {
            var pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_WSYLD);
            var pFeatureLayer = (IFeatureLayer)pLayer;
            var strsql = "";
            for (var i = 0; i < bid.Length; i++)
            {
                if (i == 0)
                    strsql = "bid='" + bid[i] + "'";
                else
                    strsql += " or bid='" + bid[i] + "' ";
            }
            DataEditCommon.DeleteFeatureByWhereClause(pFeatureLayer, strsql);
        }

        /// <summary>
        ///     打印
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcGasPressure, "瓦斯压力点信息报表");
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
                gcGasPressure.ExportToXls(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        ///     退出
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void btnExit_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            Close();
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

        private void btnMap_Click(object sender, EventArgs e)
        {
            var selectedIndex = gridView1.GetSelectedRows();
            var list = selectedIndex.Select(i => (GasPressure)gridView1.GetRow(i)).Select(gasPressure => new PointClass
            {
                X = gasPressure.coordinate_x,
                Y = gasPressure.coordinate_y
            }).Cast<IPoint>().ToList();
            MyMapHelp.Jump(MyMapHelp.GetGeoFromPoint(list));
        }

        private void GasPressureInfoManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}