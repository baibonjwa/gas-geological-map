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
    public partial class GasPressureInfoManagement : Form
    {
        /// <summary>
        ///     构造方法
        /// </summary>
        public GasPressureInfoManagement()
        {
            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetManagementFormDefaultProperties(this, Const_OP.MANAGE_GASPRESSURE_INFO);
        }

        private void RefreshData()
        {
            gcGasPressure.DataSource = GasPressure.FindAll();
        }

        /// <summary>
        ///     添加（必须实装）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var gasPressureInfoEnteringForm = new GasPressureInfoEntering((GasPressure) gridView1.GetFocusedRow());
            if (DialogResult.OK == gasPressureInfoEnteringForm.ShowDialog())
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
            if (!Alert.confirm(Const_OP.DEL_CONFIRM_MSG_GASPRESSURE)) return;
            // 瓦斯压力数据删除
            var selectedIndex = gridView1.GetSelectedRows();
            foreach (var gasPressure in selectedIndex.Select(i => (GasPressure) gridView1.GetRow(i)))
            {
                DelGasGushQuantityPt(new[] {gasPressure.BindingId});
                gasPressure.Delete();
            }
            RefreshData();
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
        ///     打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsBtnPrint_Click(object sender, EventArgs e)
        {
            DevUtil.DevPrint(gcGasPressure, "瓦斯压力点信息报表");
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
                gcGasPressure.ExportToXls(saveFileDialog1.FileName);
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
            var list = selectedIndex.Select(i => (GasPressure) gridView1.GetRow(i)).Select(gasPressure => new PointClass
            {
                X = gasPressure.CoordinateX,
                Y = gasPressure.CoordinateY
            }).Cast<IPoint>().ToList();
            MyMapHelp.Jump(MyMapHelp.GetGeoFromPoint(list));
        }

        private void GasPressureInfoManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}