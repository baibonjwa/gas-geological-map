using System;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using GIS;
using GIS.Common;
using LibCommon;
using LibEntity;

namespace sys4
{
    public partial class GasGushQuantityInfoManagement : Form
    {
        /// <summary>
        ///     构造方法
        /// </summary>
        public GasGushQuantityInfoManagement()
        {
            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_OP.MANAGE_GASGUSHQUANTITY_INFO);
        }

        private void RefreshData()
        {
            gcGasGushQuantity.DataSource = GasGushQuantity.FindAll();
        }

        /// <summary>
        ///     添加（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!Alert.confirm(Const_OP.DEL_CONFIRM_MSG_GASGUSHQUANTITY)) return;
            var selectedIndex = gridView1.GetSelectedRows();
            foreach (var gasContent in selectedIndex.Select(i => (GasGushQuantity) gridView1.GetRow(i)))
            {
                DelGasGushQuantityPt(new[] {gasContent.BindingId});
                gasContent.Delete();
            }
        }

        /// <summary>
        ///     删除瓦斯信息
        /// </summary>
        /// <param name="bid">绑定ID</param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcGasGushQuantity, "瓦斯涌出量点信息报表");
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

        private void btnMap_Click(object sender, EventArgs e)
        {
            var selectedIndex = gridView1.GetSelectedRows();
            var list =
                selectedIndex.Select(i => (GasGushQuantity) gridView1.GetRow(i))
                    .Select(gasGushQuantity => new PointClass
                    {
                        X = gasGushQuantity.CoordinateX,
                        Y = gasGushQuantity.CoordinateY
                    }).Cast<IPoint>().ToList();
            MyMapHelp.Jump(MyMapHelp.GetGeoFromPoint(list));
        }
    }
}