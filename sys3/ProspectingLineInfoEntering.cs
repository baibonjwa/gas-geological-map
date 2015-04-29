using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using GIS;
using GIS.Common;
using LibCommon;
using LibEntity;

namespace sys3
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

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.INSERT_PROSPECTING_LINE_INFO);

            //this.listBox1.Click += (ss, ee) =>
            //{
            //    ListBox lb = ss as ListBox;
            //    Point p = lb.PointToClient(Control.MousePosition);
            //    for (int i = 0; i < lb.Items.Count; i++)
            //    {
            //        if (ListBoxItemClick != null && lb.GetItemRectangle(i).Contains(p))
            //        {
            //            ListBoxItemClick.Invoke(lb, new ItemClickEventArgs() { Index = i });
            //            break;
            //        }
            //    }
            //};

            //this.ListBoxItemClick += (s, e) =>
            //{
            //    MessageBox.Show(e.Index.ToString());
            //};

            // 绑定钻孔信息
        }

        /// <summary>
        ///     带参数的构造方法
        /// </summary>
        /// <param name="strPrimaryKey">主键</param>
        public ProspectingLineInfoEntering(ProspectingLine prospectingLine)
        {
            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.UPDATE_PROSPECTING_LINE_INFO);

            // 设置业务类型
            _bllType = "update";
        }

        /// <summary>
        ///     选择（小）断层 【→】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // 验证
            if (!check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;

            // 创建勘探线实体
            var prospectingLineEntity = new ProspectingLine();
            // 勘探线名称
            prospectingLineEntity.ProspectingLineName = txtProspectingLineName.Text.Trim();
            // 勘探线钻孔
            var cnt = lstProspectingBoreholeSelected.Items.Count;
            var lstProspectingBoreholePts = new List<IPoint>(); //20140505 lyf 存储选择的钻孔点要素
            for (var i = 0; i < cnt; i++)
            {
                var strDisplayName = lstProspectingBoreholeSelected.Items[i].ToString();
                if (Validator.IsEmpty(prospectingLineEntity.ProspectingBorehole))
                {
                    prospectingLineEntity.ProspectingBorehole = strDisplayName;
                }
                else
                {
                    prospectingLineEntity.ProspectingBorehole = prospectingLineEntity.ProspectingBorehole + "," +
                                                                strDisplayName;
                }

                ///20140505 lyf
                ///根据钻孔点名查找钻孔点信息  
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
                prospectingLineEntity.BindingId = IDGenerator.NewBindingID();

                // 勘探线信息登录
                prospectingLineEntity.Save();

                ///20140505 lyf
                ///绘制勘探线图形
                DrawProspectingLine(prospectingLineEntity, lstProspectingBoreholePts);
            }
            else
            {
                // 主键
                prospectingLineEntity.ProspectingLineId = _iPK;
                // 勘探线信息修改
                prospectingLineEntity.Save();
                //20140506 lyf 
                //获取勘探线的BID
                var sBid = ProspectingLine.Find(_iPK).BindingId;
                if (sBid != "")
                {
                    prospectingLineEntity.BindingId = sBid;
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 窗口关闭
            Close();
        }

        /// <summary>
        ///     验证画面入力数据
        /// </summary>
        /// <returns>验证结果：true 通过验证, false未通过验证</returns>
        private bool check()
        {
            // 判断勘探线名称是否录入
            if (!Check.isEmpty(txtProspectingLineName, Const_GM.PROSPECTING_LINE_NAME))
            {
                return false;
            }

            // 勘探线名称特殊字符判断
            if (!Check.checkSpecialCharacters(txtProspectingLineName, Const_GM.PROSPECTING_LINE_NAME))
            {
                return false;
            }

            //// 判断勘探线名称是否存在
            //if (!Check.isExist(this.txtProspectingLineName, Const_GM.PROSPECTING_LINE_NAME, 
            //    ProspectingLineBLL.isProspectingLineNameExist(this.txtProspectingLineName.Text.Trim())))
            //{
            //    return false;
            //}

            // 只有当添加新勘探线信息的时候才去判断勘探线名称是否重复
            if (_bllType == "add")
            {
                // 判断孔号是否存在
                if (ProspectingLine.ExistsByProspectingLineName(txtProspectingLineName.Text.Trim()))
                {
                    return false;
                }
            }
            else
            {
                /* 修改的时候，首先要获取UI输入的钻孔名称到DB中去检索，
                如果检索件数 > 0 并且该断层ID还不是传过来的主键，那么视为输入了已存在的钻孔名称 */
                var boreholeId = -1;
                if (ProspectingLine.ExistsByProspectingLineName(txtProspectingLineName.Text.Trim()))
                {
                    txtProspectingLineName.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_GM.PROSPECTING_LINE_EXIST_MSG); // 勘探线名称已存在，请重新录入！
                    txtProspectingLineName.Focus();
                    return false;
                }
            }

            // 勘探线钻孔必须选择
            if (lstProspectingBoreholeSelected.Items.Count == 0)
            {
                Alert.alert(Const_GM.PROSPECTING_BOREHOLE_MUST_CHOOSE_MSG);
                lstProspectingBoreholeAll.Focus();
                return false;
            }

            // 验证通过
            return true;
        }

        /// <summary>
        ///     实现点击鼠标右键，将点击处的Item设为选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 当前下标
            var iNowIndex = lstProspectingBoreholeSelected.SelectedIndex;

            if (iNowIndex == 0)
            {
                Alert.alert("无法上移");
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 当前下标
            var iNowIndex = lstProspectingBoreholeSelected.SelectedIndex;

            if (iNowIndex == lstProspectingBoreholeSelected.Items.Count - 1)
            {
                Alert.alert("无法下移");
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
        /// <param name="prospectingLineEntity"></param>
        /// <param name="lstProspectingBoreholePts"></param>
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
            var bIsDeleteOldFeature = DataEditCommon.DeleteFeatureByBId(featureLayer, prospectingLineEntity.BindingId);
            if (bIsDeleteOldFeature)
            {
                //绘制图元
                DrawProspectingLine(prospectingLineEntity, lstProspectingBoreholePts);
            }
        }

        /// <summary>
        ///     根据钻孔点名查找钻孔点信息
        /// </summary>
        /// <param name="strDisplayName"></param>
        /// <returns></returns>
        private IPoint GetProspectingBoreholePointSelected(String strDisplayName)
        {
            try
            {
                var brehole = Borehole.FindOneByBoreholeNum(strDisplayName);

                IPoint pt = new PointClass();
                if (brehole != null)
                {
                    pt.X = brehole.CoordinateX;
                    pt.Y = brehole.CoordinateX;
                    pt.Z = brehole.CoordinateZ;
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
        /// <param name="prospectingLineEntity"></param>
        /// <param name="lstProspectingBoreholePts"></param>
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

            var prospectingLineID = prospectingLineEntity.BindingId;
            //绘制推断断层
            PointsFit2Polyline.CreateLine(featureLayer, lstProspectingBoreholePts, prospectingLineID);
        }

        #endregion
    }
}