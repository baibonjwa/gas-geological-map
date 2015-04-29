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
    public partial class GasPressureInfoEntering : Form
    {
        /// <summary>
        ///     构造方法
        /// </summary>
        public GasPressureInfoEntering()
        {
            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_OP.INSERT_GASPRESSURE_INFO);
        }

        /// <summary>
        ///     带参数的构造方法
        /// </summary>
        public GasPressureInfoEntering(GasPressure gasPressure)
        {
            GasPressure = gasPressure;
            InitializeComponent();
        }

        private GasPressure GasPressure { get; set; }

        /// <summary>
        ///     20140308 lyf 加载窗体时传入拾取点的坐标值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GasPressureInfoEntering_Load(object sender, EventArgs e)
        {
            dtpMeasureDateTime.Format = DateTimePickerFormat.Custom;
            dtpMeasureDateTime.CustomFormat = Const.DATE_FORMART_YYYY_MM_DD;
            DataBindUtil.LoadCoalSeamsName(cboCoalSeams);
            // 坐标X
            if (GasPressure != null)
            {
                txtCoordinateX.Text = GasPressure.CoordinateX.ToString(CultureInfo.InvariantCulture);
                txtCoordinateY.Text = GasPressure.CoordinateY.ToString(CultureInfo.InvariantCulture);
                txtCoordinateZ.Text = GasPressure.CoordinateZ.ToString(CultureInfo.InvariantCulture);
                txtDepth.Text = GasPressure.Depth.ToString(CultureInfo.InvariantCulture);
                txtGasPressureValue.Text = GasPressure.GasPressureValue.ToString(CultureInfo.InvariantCulture);
                dtpMeasureDateTime.Value = GasPressure.MeasureDateTime;
                cboCoalSeams.SelectedValue = GasPressure.CoalSeams;
                selectTunnelSimple1.SetTunnel(GasPressure.Tunnel);
            }
        }

        /// <summary>
        ///     提  交
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

            // 创建一个瓦斯含量点实体
            if (GasPressure == null)
            {
                var gasPressure = new GasPressure
                {
                    CoordinateX = Convert.ToDouble(txtCoordinateX.Text),
                    CoordinateY = Convert.ToDouble(txtCoordinateY.Text),
                    CoordinateZ = Convert.ToDouble(txtCoordinateZ.Text),
                    Depth = Convert.ToDouble(txtDepth.Text),
                    GasPressureValue = Convert.ToDouble(txtGasPressureValue.Text),
                    MeasureDateTime = dtpMeasureDateTime.Value,
                    Tunnel = selectTunnelSimple1.SelectedTunnel,
                    CoalSeams = (CoalSeams) cboCoalSeams.SelectedItem,
                    BindingId = IDGenerator.NewBindingID()
                };
                // 坐标X
                gasPressure.Save();
                DrawGasGushQuantityPt(gasPressure);
            }
            else
            {
                GasPressure.CoordinateX = Convert.ToDouble(txtCoordinateX.Text);
                GasPressure.CoordinateY = Convert.ToDouble(txtCoordinateY.Text);
                GasPressure.CoordinateZ = Convert.ToDouble(txtCoordinateZ.Text);
                GasPressure.Depth = Convert.ToDouble(txtDepth.Text);
                GasPressure.GasPressureValue = Convert.ToDouble(txtGasPressureValue.Text);
                GasPressure.MeasureDateTime = dtpMeasureDateTime.Value;
                GasPressure.Tunnel = selectTunnelSimple1.SelectedTunnel;
                GasPressure.CoalSeams = (CoalSeams) cboCoalSeams.SelectedValue;
                GasPressure.Save();
                DelGasGushQuantityPt(GasPressure.BindingId, GasPressure.CoalSeams.CoalSeamsName);
                DrawGasGushQuantityPt(GasPressure);
            }
        }

        /// <summary>
        ///     取  消
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
            // 判断所在煤层是否选择
            if (cboCoalSeams.SelectedValue == null)
            {
                Alert.alert(Const.COALSEAMS_MUST_SELECT);
                return false;
            }

            // 判断坐标X是否录入
            if (!LibCommon.Check.isEmpty(txtCoordinateX, Const_OP.COORDINATE_X))
            {
                return false;
            }

            // 判断坐标X是否为数字
            if (!LibCommon.Check.IsNumeric(txtCoordinateX, Const_OP.COORDINATE_X))
            {
                return false;
            }

            // 判断坐标Y是否录入
            if (!LibCommon.Check.isEmpty(txtCoordinateY, Const_OP.COORDINATE_Y))
            {
                return false;
            }

            // 判断坐标Y是否为数字
            if (!LibCommon.Check.IsNumeric(txtCoordinateY, Const_OP.COORDINATE_Y))
            {
                return false;
            }

            // 判断测点标高是否录入
            if (!LibCommon.Check.isEmpty(txtCoordinateZ, Const_OP.GAS_PRESSURE_COORDINATE_Z))
            {
                return false;
            }

            // 判断测点标高是否为数字
            if (!LibCommon.Check.IsNumeric(txtCoordinateZ, Const_OP.GAS_PRESSURE_COORDINATE_Z))
            {
                return false;
            }

            // 判断埋深是否录入
            if (!LibCommon.Check.isEmpty(txtDepth, Const_OP.DEPTH))
            {
                return false;
            }

            // 判断埋深是否为数字
            if (!LibCommon.Check.IsNumeric(txtDepth, Const_OP.DEPTH))
            {
                return false;
            }

            // 判断瓦斯压力值是否录入
            if (!LibCommon.Check.isEmpty(txtGasPressureValue, Const_OP.GAS_PRESSURE_VALUE))
            {
                return false;
            }

            // 判断瓦斯压力值是否为数字
            if (!LibCommon.Check.IsNumeric(txtGasPressureValue, Const_OP.GAS_PRESSURE_VALUE))
            {
                return false;
            }

            // 验证通过
            return true;
        }

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

        /// <summary>
        ///     20140801SDE中添加瓦斯压力点
        /// </summary>
        private void DrawGasGushQuantityPt(GasPressure gasGushQuantityEntity)
        {
            var dCoordinateX = Convert.ToDouble(txtCoordinateX.Text);
            var dCoordinateY = Convert.ToDouble(txtCoordinateY.Text);
            var dCoordinateZ = Convert.ToDouble(txtCoordinateZ.Text);
            IPoint pt = new PointClass();
            pt.X = dCoordinateX;
            pt.Y = dCoordinateY;
            pt.Z = dCoordinateZ;
            var pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_WSYLD);
            if (pLayer == null)
            {
                MessageBox.Show(@"未找到瓦斯压力点图层,无法绘制瓦斯压力点图元。");
                return;
            }
            var pFeatureLayer = (IFeatureLayer) pLayer;
            IGeometry geometry = pt;
            var list = new List<ziduan>
            {
                new ziduan("bid", gasGushQuantityEntity.BindingId),
                new ziduan("mc", gasGushQuantityEntity.CoalSeams.ToString()),
                new ziduan("addtime", DateTime.Now.ToString(CultureInfo.InvariantCulture))
            };
            var wsyl = gasGushQuantityEntity.GasPressureValue.ToString(CultureInfo.InvariantCulture);
            var cdbg = gasGushQuantityEntity.CoordinateZ.ToString(CultureInfo.InvariantCulture);
            var ms = gasGushQuantityEntity.Depth.ToString(CultureInfo.InvariantCulture);
            if (DataEditCommon.strLen(cdbg) < DataEditCommon.strLen(ms))
            {
                var count = DataEditCommon.strLen(ms) - DataEditCommon.strLen(cdbg);
                for (var i = 0; i < count; i++)
                {
                    cdbg = " " + cdbg; // // 测点标高
                }
            }
            else if (DataEditCommon.strLen(cdbg) > DataEditCommon.strLen(ms))
            {
                var count = DataEditCommon.strLen(cdbg) - DataEditCommon.strLen(ms);
                for (var i = 0; i < count; i++)
                {
                    ms += " ";
                }
            }

            list.Add(new ziduan("wsyl", wsyl));
            list.Add(new ziduan("cdbg", cdbg));
            list.Add(new ziduan("ms", ms));

            var pfeature = DataEditCommon.CreateNewFeature(pFeatureLayer, geometry, list);
            if (pfeature != null)
            {
                MyMapHelp.Jump(pt);
                DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(
                    (esriViewDrawPhase) 34, null, null);
            }
        }

        /// <summary>
        ///     删除瓦斯信息
        /// </summary>
        /// <param name="bid">绑定ID</param>
        /// <param name="mc">煤层</param>
        private void DelGasGushQuantityPt(string bid, string mc)
        {
            var pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_WSYLD);
            var pFeatureLayer = (IFeatureLayer) pLayer;
            DataEditCommon.DeleteFeatureByWhereClause(pFeatureLayer, "bid='" + bid + "' and mc='" + mc + "'");
        }

        #region 绘制瓦斯压力点图元

        public const string GasPressurePt = "瓦斯压力点";

        public IPoint GasPressurePoint { get; set; }

        //private void DrawGasPressurePt(string coalseamNO)
        //{
        //    var drawspecial = new DrawSpecialCommon();
        //    ////获得当前编辑图层
        //    //IFeatureLayer featureLayer = (IFeatureLayer)DataEditCommon.g_pLayer;

        //    var sLayerAliasName = coalseamNO + "号煤层-" + GasPressurePt;
        //    //string sLayerAliasName = GAS_PRESSURE_PT;
        //    var featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);

        //    if (featureLayer == null)
        //    {
        //        //如果对应图层不存在，要自动创建图层
        //        //IFeatureLayer existFeaLayer = drawspecial.GetFeatureLayerByName(GAS_PRESSURE_PT);
        //        var workspace = DataEditCommon.g_pCurrentWorkSpace;
        //        var layerName = "GAS_PRESSURE_PT" + "_NO" + coalseamNO;
        //        //若MapControl不存在该图层，但数据库中存在该图层，则先删除之，再重新生成
        //        var dataset = drawspecial.GetDatasetByName(workspace, layerName);
        //        if (dataset != null) dataset.Delete();
        //        //自动创建图层
        //        var map = DataEditCommon.g_pMap;
        //        //drawspecial.CreateFeatureLayerByExistLayer(map, existFeaLayer, workspace, layerName, sLayerAliasName);
        //        featureLayer = drawspecial.CreateFeatureLayer(map, workspace, layerName, sLayerAliasName);
        //        if (featureLayer == null)
        //        {
        //            MessageBox.Show("未成功创建" + sLayerAliasName + "图层,无法绘制瓦斯压力点图元，请联系管理员。");
        //            return;
        //        }
        //    }

        //    ///2.绘制瓦斯压力点   
        //    var dCoordinateX = Convert.ToDouble(txtCoordinateX.Text);
        //    var dCoordinateY = Convert.ToDouble(txtCoordinateY.Text);
        //    var dCoordinateZ = Convert.ToDouble(txtCoordinateZ.Text);
        //    IPoint pt = new PointClass();
        //    pt.X = dCoordinateX;
        //    pt.Y = dCoordinateY;
        //    pt.Z = dCoordinateZ;
        //    var pDrawWSYLD = new DrawWSYLD("P", txtGasPressureValue.Text,
        //        txtCoordinateZ.Text, txtDepth.Text);
        //    var feature = featureLayer.FeatureClass.CreateFeature();

        //    IGeometry geometry = pt;
        //    DrawCommon.HandleZMValue(feature, geometry); //几何图形Z值处理
        //    feature.Shape = pt;
        //    feature.Store();

        //    var strValue = feature.get_Value(feature.Fields.FindField("OBJECTID")).ToString();
        //    DataEditCommon.SpecialPointRenderer(featureLayer, "OBJECTID", strValue, pDrawWSYLD.m_Bitmap);

        //    ///3.显示瓦斯压力点图层
        //    if (featureLayer.Visible == false)
        //        featureLayer.Visible = true;

        //    DataEditCommon.g_pMyMapCtrl.ActiveView.Refresh();
        //}

        #endregion 绘制瓦斯压力点图元
    }
}