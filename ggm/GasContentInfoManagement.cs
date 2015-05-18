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
    public partial class GasContentInfoManagement : Form
    {
        /// <summary>
        ///     构造方法
        /// </summary>
        public GasContentInfoManagement()
        {
            InitializeComponent();
        }

        private void RefreshData()
        {
            var gasContent = GasContent.FindAll();
            gcGasContent.DataSource = gasContent;
        }

        /// <summary>
        ///     添加（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var gasContentInfoEnteringForm = new GasContentInfoEntering();
            if (DialogResult.OK == gasContentInfoEnteringForm.ShowDialog())
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
            var gasContentInfoEnteringForm = new GasContentInfoEntering((GasContent)gridView1.GetFocusedRow());
            if (DialogResult.OK == gasContentInfoEnteringForm.ShowDialog())
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
            if (!Alert.Confirm("确定要删除瓦斯含量数据吗？")) return;
            var selectedIndex = gridView1.GetSelectedRows();
            foreach (var gasContent in selectedIndex.Select(i => (GasContent)gridView1.GetRow(i)))
            {
                DelGasGushQuantityPt(new[] { gasContent.binding_id });
                gasContent.Delete();
            }
            RefreshData();
        }

        /// <summary>
        ///     删除瓦斯信息
        /// </summary>
        /// <param name="bid">绑定ID</param>
        private void DelGasGushQuantityPt(string[] bid)
        {
            var pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_WSHLD);
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
        ///     导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                gcGasContent.ExportToXls(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        ///     打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcGasContent, "瓦斯含量点信息报表");
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
        ///     刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        ///     跳转到地图上所在的位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMap_Click(object sender, EventArgs e)
        {
            var selectedIndex = gridView1.GetSelectedRows();
            var list = selectedIndex.Select(i => (GasContent)gridView1.GetRow(i)).Select(gasContent => new PointClass
            {
                X = gasContent.coordinate_x,
                Y = gasContent.coordinate_y
            }).Cast<IPoint>().ToList();
            MyMapHelp.Jump(MyMapHelp.GetGeoFromPoint(list));
        }

        private void GasContentInfoManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}