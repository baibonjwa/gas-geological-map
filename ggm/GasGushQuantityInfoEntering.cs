using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using GIS;
using GIS.Common;
using LibCommon;
using LibEntity;

namespace ggm
{
    public partial class GasGushQuantityInfoEntering : Form
    {
        /// <summary>
        ///     构造方法
        /// </summary>
        public GasGushQuantityInfoEntering()
        {
            GasGushQuantityPoint = null;
            InitializeComponent();
        }

        /// <summary>
        ///     带参数的构造方法
        /// </summary>
        /// <param name="gasGushQuantity"></param>
        public GasGushQuantityInfoEntering(GasGushQuantity gasGushQuantity)
        {
            GasGushQuantityPoint = null;
            InitializeComponent();
            // 设置窗体默认属性
            GasGushQuantity = gasGushQuantity;
        }

        private GasGushQuantity GasGushQuantity { get; set; }

        /// <summary>
        ///     20140311 lyf 加载窗体时传入拾取点的坐标值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GasGushQuantityInfoEntering_Load(object sender, EventArgs e)
        {
            // 设置日期控件格式
            dtpStopeDate.Format = DateTimePickerFormat.Custom;
            dtpStopeDate.CustomFormat = @"yyyy/MM/dd HH:mm:ss";
            if (GasGushQuantity != null)
            {
                txtCoordinateX.Text = GasGushQuantity.CoordinateX.ToString(CultureInfo.InvariantCulture);
                txtCoordinateY.Text = GasGushQuantity.CoordinateY.ToString(CultureInfo.InvariantCulture);
                txtCoordinateZ.Text = GasGushQuantity.CoordinateZ.ToString(CultureInfo.InvariantCulture);
                txtAbsoluteGasGushQuantity.Text =
                    GasGushQuantity.AbsoluteGasGushQuantity.ToString(CultureInfo.InvariantCulture);
                txtRelativeGasGushQuantity.Text =
                    GasGushQuantity.RelativeGasGushQuantity.ToString(CultureInfo.InvariantCulture);
                txtWorkingFaceDayOutput.Text =
                    GasGushQuantity.WorkingFaceDayOutput.ToString(CultureInfo.InvariantCulture);
                dtpStopeDate.Value = GasGushQuantity.StopeDate;
                selectTunnelSimple1.SetTunnel(GasGushQuantity.Tunnel);
            }
        }

        /// <summary>
        ///     提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            // 创建瓦斯涌出量点实体
            if (GasGushQuantity == null)
            {
                var gasGushQuantity = new GasGushQuantity
                {
                    CoordinateX = Convert.ToDouble(txtCoordinateX.Text),
                    CoordinateY = Convert.ToDouble(txtCoordinateY.Text),
                    CoordinateZ = Convert.ToDouble(txtCoordinateZ.Text),
                    AbsoluteGasGushQuantity = Convert.ToDouble(txtAbsoluteGasGushQuantity.Text),
                    RelativeGasGushQuantity = Convert.ToDouble(txtRelativeGasGushQuantity.Text),
                    WorkingFaceDayOutput = Convert.ToDouble(txtWorkingFaceDayOutput.Text),
                    StopeDate = dtpStopeDate.Value,
                    Tunnel = selectTunnelSimple1.SelectedTunnel,
                    BindingId = IdGenerator.NewBindingId()
                };
                // 坐标X
                gasGushQuantity.Save();
                DrawGasGushQuantityPt(gasGushQuantity);
            }
            else
            {
                GasGushQuantity.CoordinateX = Convert.ToDouble(txtCoordinateX.Text);
                GasGushQuantity.CoordinateY = Convert.ToDouble(txtCoordinateY.Text);
                GasGushQuantity.CoordinateZ = Convert.ToDouble(txtCoordinateZ.Text);
                GasGushQuantity.AbsoluteGasGushQuantity = Convert.ToDouble(txtAbsoluteGasGushQuantity.Text);
                GasGushQuantity.RelativeGasGushQuantity = Convert.ToDouble(txtRelativeGasGushQuantity.Text);
                GasGushQuantity.WorkingFaceDayOutput = Convert.ToDouble(txtWorkingFaceDayOutput.Text);
                GasGushQuantity.StopeDate = dtpStopeDate.Value;
                GasGushQuantity.Tunnel = selectTunnelSimple1.SelectedTunnel;
                GasGushQuantity.BindingId = IdGenerator.NewBindingId();
                GasGushQuantity.Save();
                DelGasGushQuantityPt(GasGushQuantity.BindingId, GasGushQuantity.CoalSeams.CoalSeamsName);
                DrawGasGushQuantityPt(GasGushQuantity);
            }
        }

        /// <summary>
        ///     取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            Close();
        }

        public const string STOPE_WORKING_FACE_GAS_GUSH_QUANTITY_PT = "瓦斯涌出量点";
        public IPoint GasGushQuantityPoint { get; set; }

        /// <summary>
        ///     20140801SDE中添加瓦斯涌出量点
        /// </summary>
        private void DrawGasGushQuantityPt(GasGushQuantity gasGushQuantityEntity)
        {
            var dCoordinateX = Convert.ToDouble(txtCoordinateX.Text);
            var dCoordinateY = Convert.ToDouble(txtCoordinateY.Text);
            var dCoordinateZ = Convert.ToDouble(txtCoordinateZ.Text);
            IPoint pt = new PointClass();
            pt.X = dCoordinateX;
            pt.Y = dCoordinateY;
            pt.Z = dCoordinateZ;
            var pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_HCGZMWSYCLD);
            if (pLayer == null)
            {
                MessageBox.Show(@"未找到瓦斯涌出量点图层,无法绘制瓦斯涌出量点图元。");
                return;
            }
            var pFeatureLayer = (IFeatureLayer)pLayer;
            IGeometry geometry = pt;
            var list = new List<ziduan>
            {
                new ziduan("bid", gasGushQuantityEntity.BindingId),
                new ziduan("mc", gasGushQuantityEntity.CoalSeams.ToString()),
                new ziduan("addtime", DateTime.Now.ToString(CultureInfo.InvariantCulture))
            };
            var hcny = gasGushQuantityEntity.StopeDate.ToLongDateString();
            var ydwsycl = gasGushQuantityEntity.AbsoluteGasGushQuantity.ToString(CultureInfo.InvariantCulture);
            var xdwsycl = gasGushQuantityEntity.RelativeGasGushQuantity.ToString(CultureInfo.InvariantCulture);
            var gzmrcl = gasGushQuantityEntity.WorkingFaceDayOutput.ToString(CultureInfo.InvariantCulture);
            if (DataEditCommon.strLen(ydwsycl) < DataEditCommon.strLen(xdwsycl))
            {
                var count = DataEditCommon.strLen(xdwsycl) - DataEditCommon.strLen(ydwsycl);
                for (var i = 0; i < count; i++)
                {
                    ydwsycl += " ";
                }
            }
            else if (DataEditCommon.strLen(ydwsycl) > DataEditCommon.strLen(xdwsycl))
            {
                var count = DataEditCommon.strLen(ydwsycl) - DataEditCommon.strLen(xdwsycl);
                for (var i = 0; i < count; i++)
                {
                    xdwsycl += " ";
                }
            }
            if (DataEditCommon.strLen(gzmrcl) < DataEditCommon.strLen(hcny))
            {
                var count = DataEditCommon.strLen(hcny) - DataEditCommon.strLen(gzmrcl);
                for (var i = 0; i < count; i++)
                {
                    gzmrcl = " " + gzmrcl;
                }
            }
            else if (DataEditCommon.strLen(gzmrcl) > DataEditCommon.strLen(hcny))
            {
                var count = DataEditCommon.strLen(gzmrcl) - DataEditCommon.strLen(hcny);
                for (var i = 0; i < count; i++)
                {
                    hcny += " ";
                }
            }
            list.Add(new ziduan("hcny", hcny));
            list.Add(new ziduan("jdwsycl", ydwsycl));
            list.Add(new ziduan("xdwsycl", xdwsycl));
            list.Add(new ziduan("gzmrcl", gzmrcl));

            var pfeature = DataEditCommon.CreateNewFeature(pFeatureLayer, geometry, list);
            if (pfeature != null)
            {
                MyMapHelp.Jump(pt);
                DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh((esriViewDrawPhase)34, null, null);
            }
        }

        /// <summary>
        ///     删除瓦斯信息
        /// </summary>
        /// <param name="bid">绑定ID</param>
        /// <param name="mc">煤层</param>
        private void DelGasGushQuantityPt(string bid, string mc)
        {
            var pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_HCGZMWSYCLD);
            var pFeatureLayer = (IFeatureLayer)pLayer;
            DataEditCommon.DeleteFeatureByWhereClause(pFeatureLayer, "bid='" + bid + "' and mc='" + mc + "'");
        }
    }
}