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
    public partial class GasGushQuantityInfoManagement : Form
    {
        /// <summary>
        ///     构造方法
        /// </summary>
        public GasGushQuantityInfoManagement()
        {
            InitializeComponent();
        }

        private void RefreshData()
        {
            gcGasGushQuantity.DataSource = GasGushQuantity.FindAll();
        }

        /// <summary>
        ///     添加（必须实装）
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var gasGushQuantityInfoEnteringForm = new GasGushQuantityInfoEntering();
            if (DialogResult.OK == gasGushQuantityInfoEnteringForm.ShowDialog())
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
            var gasGushQuantityInfoEnteringForm =
                new GasGushQuantityInfoEntering((GasGushQuantity) gridView1.GetFocusedRow());
            if (DialogResult.OK == gasGushQuantityInfoEnteringForm.ShowDialog())
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
            if (!Alert.Confirm("确定要删除瓦斯涌出量数据吗？")) return;
            var selectedIndex = gridView1.GetSelectedRows();
            foreach (var gasContent in selectedIndex.Select(i => (GasGushQuantity) gridView1.GetRow(i)))
            {
                DelGasGushQuantityPt(new[] {gasContent.binding_id});
                gasContent.Delete();
            }
        }

        /// <summary>
        ///     删除瓦斯信息
        /// </summary>
        /// <params name="bid">绑定ID</params>
        private void DelGasGushQuantityPt(string[] bid)
        {
            var pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_WSYLD);
            var pFeatureLayer = (IFeatureLayer) pLayer;
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
        ///     导出
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                gcGasGushQuantity.ExportToXls(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        ///     打印
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcGasGushQuantity, "瓦斯涌出量点信息报表");
        }

        /// <summary>
        ///     退出
        /// </summary>
        /// <params name="sender"></params>
        /// <param name="e"></params>
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
            var list =
                selectedIndex.Select(i => (GasGushQuantity) gridView1.GetRow(i))
                    .Select(gasGushQuantity => new PointClass
                    {
                        X = gasGushQuantity.coordinate_x,
                        Y = gasGushQuantity.coordinate_y
                    }).Cast<IPoint>().ToList();
            MyMapHelp.Jump(MyMapHelp.GetGeoFromPoint(list));
        }
    }
}