using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using GIS;
using GIS.Common;
using LibCommon;
using LibEntity;

namespace geoInput
{
    public partial class ProspectingLineInfoEntering : Form
    {
        private int _iPK;
        /** 主键  **/
        /** 业务逻辑类型：添加/修改  **/
        private readonly string _bllType = "add";
        //public event EventHandler<ItemClickEventArgs> ListBoxItemClick;

        //public class ItemClickEventArgs : EventArgs
        //{
        //    public int Index { get; set; }
        //}

        /// <summary>
        ///     构造方法
        /// </summary>
        public ProspectingLineInfoEntering()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     带参数的构造方法
        /// </summary>
        /// <params name="strPrimaryKey">主键</params>
        public ProspectingLineInfoEntering(ProspectingLine prospectingLine)
        {
            InitializeComponent();

            // 设置业务类型
            _bllType = "update";
        }

        /// <summary>
        ///     选择（小）断层 【→】
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < lstProspectingBoreholeAll.SelectedItems.Count;)
            {
                // 将左侧ListBox中选择的数据添加到右侧ListBox中
                lstProspectingBoreholeSelected.Items.Add(lstProspectingBoreholeAll.SelectedItems[i].ToString());
                // 移除左侧ListBox中选择添加的数据
                lstProspectingBoreholeAll.Items.RemoveAt(lstProspectingBoreholeAll.SelectedIndices[0]);
            }
        }

        /// <summary>
        ///     将已经选择的（小）断层移除 【←】
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void btnDeltete_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < lstProspectingBoreholeSelected.SelectedItems.Count;)
            {
                // 将右侧ListBox中选择移除的数据恢复到左侧ListBox中
                lstProspectingBoreholeAll.Items.Add(lstProspectingBoreholeSelected.SelectedItems[i].ToString());
                // 移除右侧ListBox中选择的数据
                lstProspectingBoreholeSelected.Items.RemoveAt(lstProspectingBoreholeSelected.SelectedIndices[0]);
            }
        }

        /// <summary>
        ///     提交
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            // 创建勘探线实体
            var prospectingLineEntity = new ProspectingLine();
            // 勘探线名称
            prospectingLineEntity.prospecting_line_name = txtProspectingLineName.Text.Trim();
            // 勘探线钻孔
            var cnt = lstProspectingBoreholeSelected.Items.Count;
            var lstProspectingBoreholePts = new List<IPoint>(); //20140505 lyf 存储选择的钻孔点要素
            for (var i = 0; i < cnt; i++)
            {
                var strDisplayName = lstProspectingBoreholeSelected.Items[i].ToString();
                if (String.IsNullOrWhiteSpace(prospectingLineEntity.prospecting_borehole))
                {
                    prospectingLineEntity.prospecting_borehole = strDisplayName;
                }
                else
                {
                    prospectingLineEntity.prospecting_borehole = prospectingLineEntity.prospecting_borehole + "," +
                                                                strDisplayName;
                }

                IPoint pt = new PointClass();
                pt = GetProspectingBoreholePointSelected(strDisplayName);
                if (pt != null && !lstProspectingBoreholePts.Contains(pt))
                {
                    lstProspectingBoreholePts.Add(pt);
                }
            }

            var bResult = false;
            if (_bllType == "add")
            {
                // BIDID
                prospectingLineEntity.binding_id = IdGenerator.NewBindingId();

                // 勘探线信息登录
                prospectingLineEntity.Save();

                ///20140505 lyf
                ///绘制勘探线图形
                DrawProspectingLine(prospectingLineEntity, lstProspectingBoreholePts);
            }
            else
            {
                // 主键
                prospectingLineEntity.prospecting_line_id = _iPK;
                // 勘探线信息修改
                prospectingLineEntity.Save();
                //20140506 lyf 
                //获取勘探线的BID
                var sBid = ProspectingLine.Find(_iPK).binding_id;
                if (sBid != "")
                {
                    prospectingLineEntity.binding_id = sBid;
                    ModifyProspectingLine(prospectingLineEntity, lstProspectingBoreholePts); //修改图元
                }
            }

            // 添加/修改成功的场合
            if (bResult)
            {
            }
        }

        /// <summary>
        ///     取消
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 窗口关闭
            Close();
        }

        /// <summary>
        ///     实现点击鼠标右键，将点击处的Item设为选中
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void lstProspectingBoreholeSelected_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) //判断是否右键点击
            {
                var p = e.Location; //获取点击的位置

                var index = lstProspectingBoreholeSelected.IndexFromPoint(p); //根据位置获取右键点击项的索引

                lstProspectingBoreholeSelected.ClearSelected();

                lstProspectingBoreholeSelected.SelectedIndex = index; //设置该索引值对应的项为选定状态
            }
        }

        /// <summary>
        ///     上移
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 当前下标
            var iNowIndex = lstProspectingBoreholeSelected.SelectedIndex;

            if (iNowIndex == 0)
            {
                Alert.AlertMsg("无法上移");
                return;
            }

            var strTemp = lstProspectingBoreholeSelected.SelectedItem.ToString();

            lstProspectingBoreholeSelected.Items[iNowIndex] = lstProspectingBoreholeSelected.Items[iNowIndex - 1];

            lstProspectingBoreholeSelected.Items[iNowIndex - 1] = strTemp;

            lstProspectingBoreholeSelected.ClearSelected();

            lstProspectingBoreholeSelected.SelectedIndex = iNowIndex - 1; // 设置该索引值对应的项为选定状态
        }

        /// <summary>
        ///     下移
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 当前下标
            var iNowIndex = lstProspectingBoreholeSelected.SelectedIndex;

            if (iNowIndex == lstProspectingBoreholeSelected.Items.Count - 1)
            {
                Alert.AlertMsg("无法下移");
                return;
            }

            var strTemp = lstProspectingBoreholeSelected.SelectedItem.ToString();

            lstProspectingBoreholeSelected.Items[iNowIndex] = lstProspectingBoreholeSelected.Items[iNowIndex + 1];

            lstProspectingBoreholeSelected.Items[iNowIndex + 1] = strTemp;

            lstProspectingBoreholeSelected.ClearSelected();

            lstProspectingBoreholeSelected.SelectedIndex = iNowIndex + 1; // 设置该索引值对应的项为选定状态
        }

        #region 绘制勘探线

        /// <summary>
        ///     修改勘探线图元
        /// </summary>
        /// <params name="prospectingLineEntity"></params>
        /// <params name="lstProspectingBoreholePts"></params>
        private void ModifyProspectingLine(ProspectingLine prospectingLineEntity, List<IPoint> lstProspectingBoreholePts)
        {
            //1.获得当前编辑图层
            var drawspecial = new DrawSpecialCommon();
            var sLayerAliasName = LayerNames.DEFALUT_KANTANXIAN; //“勘探线”图层
            var featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            if (featureLayer == null)
            {
                MessageBox.Show("未找到" + sLayerAliasName + "图层,无法修改勘探线图元。");
                return;
            }

            //2.删除原来图元，重新绘制新图元
            var bIsDeleteOldFeature = DataEditCommon.DeleteFeatureByBId(featureLayer, prospectingLineEntity.binding_id);
            if (bIsDeleteOldFeature)
            {
                //绘制图元
                DrawProspectingLine(prospectingLineEntity, lstProspectingBoreholePts);
            }
        }

        /// <summary>
        ///     根据钻孔点名查找钻孔点信息
        /// </summary>
        /// <params name="strDisplayName"></params>
        /// <returns></returns>
        private IPoint GetProspectingBoreholePointSelected(String strDisplayName)
        {
            try
            {
                var brehole = Borehole.FindAllByProperty("name", strDisplayName).FirstOrDefault();

                IPoint pt = new PointClass();
                if (brehole != null)
                {
                    pt.X = brehole.coordinate_x;
                    pt.Y = brehole.coordinate_x;
                    pt.Z = brehole.coordinate_z;
                }

                return pt;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///     根据所选钻孔点绘制勘探线
        /// </summary>
        /// <params name="prospectingLineEntity"></params>
        /// <params name="lstProspectingBoreholePts"></params>
        private void DrawProspectingLine(ProspectingLine prospectingLineEntity, List<IPoint> lstProspectingBoreholePts)
        {
            //1.获得当前编辑图层
            var drawspecial = new DrawSpecialCommon();
            var sLayerAliasName = LayerNames.DEFALUT_KANTANXIAN; //“勘探线”图层
            var featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            if (featureLayer == null)
            {
                MessageBox.Show("未找到" + sLayerAliasName + "图层,无法绘制勘探线图元。");
                return;
            }

            //2.绘制图元
            if (lstProspectingBoreholePts.Count == 0) return;

            var prospectingLineID = prospectingLineEntity.binding_id;
            //绘制推断断层
            PointsFit2Polyline.CreateLine(featureLayer, lstProspectingBoreholePts, prospectingLineID);
        }

        #endregion
    }
}