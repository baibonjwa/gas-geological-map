using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using GIS;
using GIS.Common;
using LibBusiness;
using LibCommon;
using LibCommonForm;
using LibEntity;

namespace sys4
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
            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_OP.INSERT_GASGUSHQUANTITY_INFO);
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
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_OP.UPDATE_GASGUSHQUANTITY_INFO);
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
            dtpStopeDate.CustomFormat = Const.DATE_FORMART_YYYY_MM;
            DataBindUtil.LoadCoalSeamsName(cboCoalSeams);
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
                cboCoalSeams.SelectedValue = GasGushQuantity.CoalSeams;
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
            // 验证
            if (!Check())
            {
                DialogResult = DialogResult.None;
                return;
            }
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
                    CoalSeams = (CoalSeams)cboCoalSeams.SelectedValue,
                    BindingId = IDGenerator.NewBindingID()
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
                GasGushQuantity.CoalSeams = (CoalSeams)cboCoalSeams.SelectedValue;
                GasGushQuantity.BindingId = IDGenerator.NewBindingID();
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

        /// <summary>
        ///     验证画面入力数据
        /// </summary>
        /// <returns>验证结果：true 通过验证, false未通过验证</returns>
        private bool Check()
        {
            // 判断是否选择所属巷道
            if (selectTunnelSimple1.SelectedTunnel == null)
            {
                Alert.alert(Const_OP.TUNNEL_NAME_MUST_INPUT);
                return false;
            }

            // 判断所在煤层是否选择
            if (cboCoalSeams.SelectedValue == null)
            {
                Alert.alert(Const.COALSEAMS_MUST_SELECT);
                return false;
            }

            // 判断坐标X是否录入
            if (!LibCommon.Check.isEmpty(txtCoordinateX, Const_OP.COORDINATE_X))
                return false;

            // 判断坐标X是否为数字
            if (!LibCommon.Check.IsNumeric(txtCoordinateX, Const_OP.COORDINATE_X))
                return false;

            // 判断坐标Y是否录入
            if (!LibCommon.Check.isEmpty(txtCoordinateY, Const_OP.COORDINATE_Y))
                return false;

            // 判断坐标Y是否为数字
            if (!LibCommon.Check.IsNumeric(txtCoordinateY, Const_OP.COORDINATE_Y))
                return false;

            // 判断坐标Z是否录入
            if (!LibCommon.Check.isEmpty(txtCoordinateZ, Const_OP.STOPE_WORKING_FACE_GAS_GUSH_QUANTITY_COORDINATE_Z))
                return false;

            // 判断坐标Z是否为数字
            if (!LibCommon.Check.IsNumeric(txtCoordinateZ, Const_OP.STOPE_WORKING_FACE_GAS_GUSH_QUANTITY_COORDINATE_Z))
                return false;

            // 判断绝对瓦斯涌出量是否录入
            if (!LibCommon.Check.isEmpty(txtAbsoluteGasGushQuantity, Const_OP.ABSOLUTE_GAS_GUSH_QUANTITY))
                return false;

            // 判断绝对瓦斯涌出量是否为数字
            if (!LibCommon.Check.IsNumeric(txtAbsoluteGasGushQuantity, Const_OP.ABSOLUTE_GAS_GUSH_QUANTITY))
                return false;

            // 判断相对瓦斯涌出量是否录入
            if (!LibCommon.Check.isEmpty(txtRelativeGasGushQuantity, Const_OP.RELATIVE_GAS_GUSH_QUANTITY))
            {
                return false;
            }

            // 判断相对瓦斯涌出量是否为数字
            if (!LibCommon.Check.IsNumeric(txtRelativeGasGushQuantity, Const_OP.RELATIVE_GAS_GUSH_QUANTITY))
            {
                return false;
            }

            // 判断工作面日产量是否录入
            if (!LibCommon.Check.isEmpty(txtWorkingFaceDayOutput, Const_OP.WORKING_FACE_DAY_OUTPUT))
            {
                return false;
            }

            // 判断工作面日产量是否为数字
            if (!LibCommon.Check.IsNumeric(txtWorkingFaceDayOutput, Const_OP.WORKING_FACE_DAY_OUTPUT))
            {
                return false;
            }

            // 验证通过
            return true;
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

        /// <summary>
        ///     20140311 lyf 绘制瓦斯涌出量点图元
        /// </summary>
        /// <summary>
        ///     煤层信息添加管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCoalSeams_Click(object sender, EventArgs e)
        {
            var commonManagementForm = new CommonManagement(5, 0);
            if (DialogResult.OK == commonManagementForm.ShowDialog())
            {
                // 绑定煤层名称信息
                DataBindUtil.LoadCoalSeamsName(cboCoalSeams);
            }
        }
    }
}