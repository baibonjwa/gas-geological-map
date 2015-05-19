using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using GIS;
using GIS.Common;
using LibCommon;
using LibEntity;

namespace geoInput
{
    public partial class PitshaftInfoEntering : Form
    {
        private Rectangle _PropertyName;
        /** 业务逻辑类型：添加/修改  **/
        private readonly string _bllType = "add";
        private readonly int _iPk;

        /// <summary>
        ///     构造方法
        /// </summary>
        public PitshaftInfoEntering()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     带参数的构造方法
        /// </summary>
        /// <param name="strPrimaryKey">主键</param>
        public PitshaftInfoEntering(string strPrimaryKey)
        {
            InitializeComponent();
            // 设置业务类型
            _bllType = "update";
            // 主键
            _iPk = Convert.ToInt32(strPrimaryKey);
            // 通过主键获取断层信息
            var pitshaft = Pitshaft.Find(_iPk);

            if (pitshaft != null)
            {
                // 井筒名称
                txtPitshaftName.Text = pitshaft.pitshaft_name;
                // 井筒类型
                cobPitshaftType.SelectedValue = pitshaft.pitshaft_type;
                // 井口标高
                txtWellheadElevation.Text = pitshaft.wellhead_elevation.ToString(CultureInfo.InvariantCulture);
                // 井底标高
                txtWellbottomElevation.Text = pitshaft.wellbottom_elevation.ToString(CultureInfo.InvariantCulture);
                // 井筒坐标X
                txtPitshaftCoordinateX.Text =
                    pitshaft.pitshaft_coordinate_x.ToString(CultureInfo.InvariantCulture);
                // 井筒坐标Y
                txtPitshaftCoordinateY.Text = pitshaft.pitshaft_coordinate_y.ToString(CultureInfo.InvariantCulture);
                // 图形坐标X
                txtFigureCoordinateX.Text = pitshaft.figure_coordinate_x.ToString(CultureInfo.InvariantCulture);
                // 图形坐标Y
                txtFigureCoordinateY.Text = pitshaft.figure_coordinate_y.ToString(CultureInfo.InvariantCulture);
                // 图形坐标Z
                txtFigureCoordinateZ.Text = pitshaft.figure_coordinate_z.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        ///     提  交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            // 创建井筒实体
            var pitshaftEntity = new Pitshaft { pitshaft_name = txtPitshaftName.Text.Trim() };
            // 井口标高
            double dWellheadElevation = 0;
            if (double.TryParse(txtWellheadElevation.Text.Trim(), out dWellheadElevation))
            {
                pitshaftEntity.wellhead_elevation = dWellheadElevation;
            }
            // 井底标高
            double dWellbottomElevation = 0;
            if (double.TryParse(txtWellbottomElevation.Text.Trim(), out dWellbottomElevation))
            {
                pitshaftEntity.wellbottom_elevation = dWellbottomElevation;
            }
            // 井筒坐标X
            double dPitshaftCoordinateX = 0;
            if (double.TryParse(txtPitshaftCoordinateX.Text.Trim(), out dPitshaftCoordinateX))
            {
                pitshaftEntity.pitshaft_coordinate_x = Math.Round(dPitshaftCoordinateX, 3);
            }
            // 井筒坐标Y
            double dPitshaftCoordinateY = 0;
            if (double.TryParse(txtPitshaftCoordinateY.Text.Trim(), out dPitshaftCoordinateY))
            {
                pitshaftEntity.pitshaft_coordinate_y = Math.Round(dPitshaftCoordinateY, 3);
            }
            // 图形坐标X
            double dFigureCoordinateX = 0;
            if (double.TryParse(txtFigureCoordinateX.Text.Trim(), out dFigureCoordinateX))
            {
                pitshaftEntity.figure_coordinate_x = Math.Round(dFigureCoordinateX, 3);
            }
            // 图形坐标Y
            double dFigureCoordinateY = 0;
            if (double.TryParse(txtFigureCoordinateY.Text.Trim(), out dFigureCoordinateY))
            {
                pitshaftEntity.figure_coordinate_y = Math.Round(dFigureCoordinateY, 3);
            }
            // 图形坐标Z
            double dFigureCoordinateZ = 0;
            if (double.TryParse(txtFigureCoordinateZ.Text.Trim(), out dFigureCoordinateZ))
            {
                pitshaftEntity.figure_coordinate_z = dFigureCoordinateZ;
            }

            var bResult = false;
            if (_bllType == "add")
            {
                // BID
                pitshaftEntity.bid = IdGenerator.NewBindingId();
                pitshaftEntity.Save();

                DrawJingTong(pitshaftEntity);
            }
            else
            {
                // 主键
                pitshaftEntity.id = _iPk;
                // 井筒信息修改
                pitshaftEntity.Save();


                //20140428 lyf 
                //获取井筒BID，为后面修改绘制井筒赋值所用
                var sBID = "";
                sBID = Pitshaft.Find(_iPk).bid;
                pitshaftEntity.bid = sBID;
                //修改图元
                ModifyJingTong(pitshaftEntity);
            }

            // 添加/修改成功的场合
            Close();
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

        private void btnQD_Click(object sender, EventArgs e)
        {
            DataEditCommon.PickUpPoint(txtPitshaftCoordinateX, txtPitshaftCoordinateY);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataEditCommon.PickUpPoint(txtFigureCoordinateX, txtFigureCoordinateY);
        }

        #region 绘制井筒图元

        /// <summary>
        ///     绘制井筒图元
        /// </summary>
        /// <param name="pitshaftEntity"></param>
        private void DrawJingTong(Pitshaft pitshaftEntity)
        {
            ////1.获得当前编辑图层
            //DrawSpecialCommon drawspecial = new DrawSpecialCommon();
            //string sLayerAliasName = LibCommon.LibLayerNames.DEFALUT_JINGTONG;//“井筒”图层
            //IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            //if (featureLayer == null)
            //{
            //    MessageBox.Show("未找到" + sLayerAliasName + "图层,无法绘制井筒图元。");
            //    return;
            //}

            ////2.绘制井筒
            //double X = Convert.ToDouble(this.txtFigureCoordinateX.Text.ToString());
            //double Y = Convert.ToDouble(this.txtFigureCoordinateY.Text.ToString());
            //IPoint pt = new PointClass();
            //pt.X = X;
            //pt.Y = Y;

            //double dZ = 0;
            //if (!string.IsNullOrEmpty(this.txtFigureCoordinateZ.Text))
            //{
            //    double.TryParse(this.txtFigureCoordinateZ.Text, out dZ);
            //}
            //pt.Z = dZ;

            ////标注内容
            //string strH =(Convert.ToDouble(this.txtWellheadElevation.Text.ToString())+
            //    Convert.ToDouble(this.txtWellbottomElevation.Text.ToString())).ToString();//井口标高+井底标高
            //string strX = SplitStr(this.txtPitshaftCoordinateX.Text.ToString());
            //string strY = SplitStr(this.txtPitshaftCoordinateY.Text.ToString());
            ////string strName =this.cobPitshaftType.SelectedValue.ToString()+"："+
            ////    this.txtPitshaftName.Text.ToString();
            //string strName = this.cobPitshaftType.SelectedValue.ToString() + "：" +
            //   this.txtPitshaftName.Text.ToString();

            //GIS.SpecialGraphic.DrawJT drawJT = new GIS.SpecialGraphic.DrawJT(strX, strY, strH, strName);
            ////dfs
            //DataEditCommon.InitEditEnvironment();
            //DataEditCommon.CheckEditState();
            //DataEditCommon.g_CurWorkspaceEdit.StartEditOperation();
            ////dfe
            //IFeature feature = featureLayer.FeatureClass.CreateFeature();         
            //IGeometry geometry = pt;
            //DataEditCommon.ZMValue(feature, geometry);   //几何图形Z值处理
            ////drawspecial.ZMValue(feature, geometry);//几何图形Z值处理
            //feature.Shape = pt;
            ////要素ID字段赋值（对应属性表中BindingID）
            //int iFieldID = feature.Fields.FindField(GIS_Const.FIELD_BID);
            //feature.Value[iFieldID] = pitshaftEntity.bid.ToString();
            //feature.Store();
            //DataEditCommon.g_CurWorkspaceEdit.StopEditOperation();
            //string strValue = feature.get_Value(feature.Fields.FindField(GIS_Const.FIELD_OBJECTID)).ToString();
            //DataEditCommon.SpecialPointRenderer(featureLayer, GIS_Const.FIELD_OBJECTID, strValue, drawJT.m_Bitmap);

            /////3.显示井筒图层
            //if (featureLayer.Visible == false)
            //    featureLayer.Visible = true;

            ////缩放到新增的线要素，并高亮该要素
            //GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.Extent = feature.Shape.Envelope;
            //GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.Extent.Expand(2.5, 2.5, true);
            //GIS.Common.DataEditCommon.g_pMyMapCtrl.Map.SelectFeature(featureLayer, feature);
            //GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewAll, null, null);

            ////DataEditCommon.g_pMyMapCtrl.ActiveView.Refresh();

            var X = Convert.ToDouble(txtFigureCoordinateX.Text);
            var Y = Convert.ToDouble(txtFigureCoordinateY.Text);
            IPoint pt = new PointClass();
            pt.X = X;
            pt.Y = Y;

            double dZ = 0;
            if (!string.IsNullOrEmpty(txtFigureCoordinateZ.Text))
            {
                double.TryParse(txtFigureCoordinateZ.Text, out dZ);
            }
            pt.Z = dZ;
            var pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.DEFALUT_JINGTONG);
            if (pLayer == null)
            {
                MessageBox.Show("未找到井筒图层,无法绘制井筒图元。");
                return;
            }
            var pFeatureLayer = (IFeatureLayer)pLayer;
            IGeometry geometry = pt;
            var list = new List<ziduan>();
            list.Add(new ziduan("bid", pitshaftEntity.bid));
            list.Add(new ziduan("mc", pitshaftEntity.pitshaft_name));
            list.Add(new ziduan("addtime", DateTime.Now.ToString()));
            list.Add(new ziduan("jkgc", pitshaftEntity.wellhead_elevation.ToString()));
            list.Add(new ziduan("jdgc", pitshaftEntity.wellbottom_elevation.ToString()));
            list.Add(new ziduan("yt", cobPitshaftType.Text));
            list.Add(new ziduan("x", pitshaftEntity.pitshaft_coordinate_x.ToString()));
            list.Add(new ziduan("y", pitshaftEntity.pitshaft_coordinate_y.ToString()));
            list.Add(new ziduan("h", (pitshaftEntity.wellbottom_elevation + pitshaftEntity.wellhead_elevation).ToString()));

            var pfeature = DataEditCommon.CreateNewFeature(pFeatureLayer, geometry, list);
            if (pfeature != null)
            {
                MyMapHelp.Jump(pt);
                DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(
                    esriViewDrawPhase.esriViewGeography | esriViewDrawPhase.esriViewForeground, null, null);
            }
        }

        /// <summary>
        ///     修改井筒
        /// </summary>
        /// <param name="pitshaftEntity"></param>
        private void ModifyJingTong(Pitshaft pitshaftEntity)
        {
            //1.获得当前编辑图层
            var drawspecial = new DrawSpecialCommon();
            var sLayerAliasName = LayerNames.DEFALUT_JINGTONG; //“井筒”图层
            var featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            if (featureLayer == null)
            {
                MessageBox.Show("未找到" + sLayerAliasName + "图层,无法修改井筒图元。");
                return;
            }

            //2.删除原来图元，重新绘制新图元
            var bIsDeleteOldFeature = DataEditCommon.DeleteFeatureByWhereClause(featureLayer,
                "BID='" + pitshaftEntity.bid + "'");
            //if (bIsDeleteOldFeature)
            {
                //绘制井筒
                DrawJingTong(pitshaftEntity);
            }
        }

        /// <summary>
        ///     坐标长度处理
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string SplitStr(string str)
        {
            string resultStr;

            if (!str.Contains('.')) return str;

            var strArr = str.Split('.');
            if (strArr[1].Length > 3)
            {
                resultStr = strArr[0] + "." + strArr[1].Substring(0, 3);
            }
            else
            {
                resultStr = str;
            }
            return resultStr;
        }

        #endregion
    }
}