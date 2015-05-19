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
    public partial class GasContentInfoEntering : Form
    {
        /// <summary>
        ///     构造方法
        /// </summary>
        public GasContentInfoEntering()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     带参数的构造方法
        /// </summary>
        public GasContentInfoEntering(GasContent gasContent)
        {
            InitializeComponent();
            GasContent = gasContent;
        }

        private GasContent GasContent { get; set; }

        /// <summary>
        ///     20140311 lyf 加载窗体时传入拾取点的坐标值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GasContentInfoEntering_Load(object sender, EventArgs e)
        {
            dtpMeasureDateTime.Format = DateTimePickerFormat.Custom;
            dtpMeasureDateTime.CustomFormat = @"yyyy/MM/dd HH:mm:ss";
            if (GasContent != null)
            {
                txtCoordinateX.Text = GasContent.coordinate_x.ToString(CultureInfo.InvariantCulture);
                txtCoordinateY.Text = GasContent.coordinate_y.ToString(CultureInfo.InvariantCulture);
                txtCoordinateZ.Text = GasContent.coordinate_z.ToString(CultureInfo.InvariantCulture);
                txtDepth.Text = GasContent.depth.ToString(CultureInfo.InvariantCulture);
                txtGasContentValue.Text = GasContent.gas_content_value.ToString(CultureInfo.InvariantCulture);
                dtpMeasureDateTime.Value = GasContent.measure_datetime;
                selectTunnelSimple1.SetTunnel(GasContent.tunnel);
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

            // 创建一个瓦斯含量点实体
            if (GasContent == null)
            {
                var gasContent = new GasContent
                {
                    coordinate_x = Convert.ToDouble(txtCoordinateX.Text),
                    coordinate_y = Convert.ToDouble(txtCoordinateY.Text),
                    coordinate_z = Convert.ToDouble(txtCoordinateZ.Text),
                    depth = Convert.ToDouble(txtDepth.Text),
                    gas_content_value = Convert.ToDouble(txtGasContentValue.Text),
                    measure_datetime = dtpMeasureDateTime.Value,
                    tunnel = selectTunnelSimple1.SelectedTunnel,
                    bid = IdGenerator.NewBindingId()
                };
                // 坐标X
                gasContent.Save();
                DrawGasGushQuantityPt(gasContent);
            }
            else
            {
                GasContent.coordinate_x = Convert.ToDouble(txtCoordinateX.Text);
                GasContent.coordinate_y = Convert.ToDouble(txtCoordinateY.Text);
                GasContent.coordinate_z = Convert.ToDouble(txtCoordinateZ.Text);
                GasContent.depth = Convert.ToDouble(txtDepth.Text);
                GasContent.gas_content_value = Convert.ToDouble(txtGasContentValue.Text);
                GasContent.measure_datetime = dtpMeasureDateTime.Value;
                GasContent.tunnel = selectTunnelSimple1.SelectedTunnel;
                GasContent.Save();
                DelGasGushQuantityPt(GasContent.bid);
                DrawGasGushQuantityPt(GasContent);
            }
        }

        /// <summary>
        ///     取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }


        public const string GasContentPt = "瓦斯含量点";
        public IPoint GasContentPoint { get; set; }

        /// <summary>
        ///     20140801SDE中添加瓦斯含量点
        /// </summary>
        private void DrawGasGushQuantityPt(GasContent gasGushQuantityEntity)
        {
            var dCoordinateX = Convert.ToDouble(txtCoordinateX.Text);
            var dCoordinateY = Convert.ToDouble(txtCoordinateY.Text);
            var dCoordinateZ = Convert.ToDouble(txtCoordinateZ.Text);
            IPoint pt = new PointClass();
            pt.X = dCoordinateX;
            pt.Y = dCoordinateY;
            pt.Z = dCoordinateZ;
            var pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_WSHLD);
            if (pLayer == null)
            {
                MessageBox.Show(@"未找到瓦斯含量点图层,无法绘制瓦斯含量点图元。");
                return;
            }
            var pFeatureLayer = (IFeatureLayer) pLayer;
            IGeometry geometry = pt;
            var list = new List<ziduan>
            {
                new ziduan("bid", gasGushQuantityEntity.bid),
                new ziduan("mc", ""),
                new ziduan("addtime", DateTime.Now.ToString(CultureInfo.InvariantCulture))
            };
            var wshl = gasGushQuantityEntity.gas_content_value.ToString(CultureInfo.InvariantCulture); // 瓦斯含量
            var cdbg = gasGushQuantityEntity.coordinate_z.ToString(CultureInfo.InvariantCulture);
            var ms = gasGushQuantityEntity.depth.ToString(CultureInfo.InvariantCulture); // 埋深
            if (DataEditCommon.strLen(cdbg) < DataEditCommon.strLen(ms))
            {
                var count = DataEditCommon.strLen(ms) - DataEditCommon.strLen(cdbg);
                for (var i = 0; i < count; i++)
                {
                    cdbg = " " + cdbg;
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

            list.Add(new ziduan("wshl", wshl));
            list.Add(new ziduan("cdbg", cdbg));
            list.Add(new ziduan("ms", ms));

            var pfeature = DataEditCommon.CreateNewFeature(pFeatureLayer, geometry, list);
            if (pfeature != null)
            {
                MyMapHelp.Jump(pt);
                DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh((esriViewDrawPhase) 34, null, null);
            }
        }

        /// <summary>
        ///     删除瓦斯信息
        /// </summary>
        /// <param name="bid">绑定ID</param>
        /// <param name="mc">煤层</param>
        private void DelGasGushQuantityPt(string bid)
        {
            var pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_WSHLD);
            var pFeatureLayer = (IFeatureLayer) pLayer;
            DataEditCommon.DeleteFeatureByWhereClause(pFeatureLayer, "bid='" + bid + "'");
        }
    }
}