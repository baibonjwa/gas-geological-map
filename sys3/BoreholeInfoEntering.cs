using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Castle.ActiveRecord;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using GIS;
using GIS.Common;
using LibBusiness;
using LibCommon;
using LibEntity;

namespace sys3
{
    public partial class BoreholeInfoEntering : Form
    {
        private string _errorMsg;

        /// <summary>
        ///     构造方法
        /// </summary>
        public BoreholeInfoEntering()
        {
            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.INSERT_BOREHOLE_INFO);
            DataBindUtil.LoadLithology(LITHOLOGY);
        }

        public BoreholeInfoEntering(IPoint pt)
        {
            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.INSERT_BOREHOLE_INFO);
            txtCoordinateX.Text = pt.X.ToString(CultureInfo.InvariantCulture);
            txtCoordinateY.Text = pt.Y.ToString(CultureInfo.InvariantCulture);
            txtCoordinateZ.Text = pt.Z.ToString(CultureInfo.InvariantCulture).Equals("非数字")
                ? "0"
                : pt.Z.ToString(CultureInfo.InvariantCulture);
            DataBindUtil.LoadLithology(LITHOLOGY);
        }

        /// <summary>
        ///     带参数的构造方法
        /// </summary>
        /// <param name="borehole"></param>
        public BoreholeInfoEntering(Borehole borehole)
        {
            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.UPDATE_BOREHOLE_INFO);
            using (new SessionScope())
            {
                borehole = Borehole.Find(borehole.BoreholeId);
                // 孔号
                txtBoreholeNumber.Text = borehole.BoreholeNumber;
                // 地面标高
                txtGroundElevation.Text = borehole.GroundElevation.ToString(CultureInfo.InvariantCulture);
                // X坐标
                txtCoordinateX.Text = borehole.CoordinateX.ToString(CultureInfo.InvariantCulture);
                // Y坐标
                txtCoordinateY.Text = borehole.CoordinateY.ToString(CultureInfo.InvariantCulture);
                // Z坐标
                txtCoordinateZ.Text = borehole.CoordinateZ.ToString(CultureInfo.InvariantCulture);

                // 获取岩性信息

                DataBindUtil.LoadLithology(LITHOLOGY);

                // 明细


                gvCoalSeamsTexture.RowCount = borehole.BoreholeLithologys.Count + 1;
                for (var i = 0; i < borehole.BoreholeLithologys.Count; i++)
                {
                    // 岩性名称
                    var iLithologyId = borehole.BoreholeLithologys[i].Lithology.LithologyId;

                    var lithology = Lithology.Find(iLithologyId);

                    gvCoalSeamsTexture[0, i].Value = lithology.LithologyName;
                    // 底板标高
                    gvCoalSeamsTexture[1, i].Value = borehole.BoreholeLithologys[i].FloorElevation;
                    // 厚度
                    gvCoalSeamsTexture[2, i].Value = borehole.BoreholeLithologys[i].Thickness;
                    // 煤层名称
                    gvCoalSeamsTexture[3, i].Value = borehole.BoreholeLithologys[i].CoalSeamsName;

                    // 坐标X
                    gvCoalSeamsTexture[4, i].Value =
                        borehole.BoreholeLithologys[i].CoordinateX.ToString(CultureInfo.InvariantCulture);

                    // 坐标Y
                    gvCoalSeamsTexture[5, i].Value =
                        borehole.BoreholeLithologys[i].CoordinateY.ToString(CultureInfo.InvariantCulture);

                    // 坐标Z
                    gvCoalSeamsTexture[6, i].Value =
                        borehole.BoreholeLithologys[i].CoordinateX.ToString(CultureInfo.InvariantCulture);
                }
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


            var borehole = Borehole.FindOneByBoreholeNum(txtBoreholeNumber.Text) ??
                           new Borehole {BindingId = IDGenerator.NewBindingID()};
            borehole.BoreholeNumber = txtBoreholeNumber.Text.Trim();
            borehole.GroundElevation = Convert.ToDouble(txtGroundElevation.Text.Trim());
            borehole.CoordinateX = Convert.ToDouble(txtCoordinateX.Text.Trim());
            borehole.CoordinateY = Convert.ToDouble(txtCoordinateY.Text.Trim());
            borehole.CoordinateZ = Convert.ToDouble(txtCoordinateZ.Text.Trim());
            borehole.CoalSeamsTexture = string.Empty;

            var boreholeLithologys = new List<BoreholeLithology>();
            for (var i = 0; i < gvCoalSeamsTexture.RowCount; i++)
            {
                // 最后一行为空行时，跳出循环
                if (i == gvCoalSeamsTexture.RowCount - 1)
                {
                    break;
                }
                // 创建钻孔岩性实体
                var boreholeLithology = new BoreholeLithology
                {
                    FloorElevation = Convert.ToDouble(gvCoalSeamsTexture.Rows[i].Cells[1].Value),
                    Thickness = Convert.ToDouble(gvCoalSeamsTexture.Rows[i].Cells[2].Value),
                    CoalSeamsName = gvCoalSeamsTexture.Rows[i].Cells[3].Value.ToString(),
                    CoordinateX = Convert.ToDouble(gvCoalSeamsTexture.Rows[i].Cells[4].Value),
                    CoordinateY = Convert.ToDouble(gvCoalSeamsTexture.Rows[i].Cells[5].Value),
                    CoordinateZ = Convert.ToDouble(gvCoalSeamsTexture.Rows[i].Cells[6].Value),
                    Lithology = Lithology.FindOneByLithologyName(gvCoalSeamsTexture.Rows[i].Cells[0].Value.ToString()),
                    Borehole = borehole
                };
                boreholeLithologys.Add(boreholeLithology);
            }
            borehole.BoreholeLithologys = boreholeLithologys;
            borehole.Save();

            //    var dlgResult = MessageBox.Show(@"是：见煤钻孔，否：未见煤钻孔，取消：不绘制钻孔", @"绘制钻孔",
            //        MessageBoxButtons.YesNoCancel);

            //    if (dlgResult == DialogResult.Yes)
            //    {
            //        DrawZuanKong(borehole, boreholeLithologyEntityList[0]);
            //    }
            //    else if (dlgResult == DialogResult.No)
            //    {
            //        DrawZuanKong(borehole);
            //    }
            //    else if (dlgResult == DialogResult.Cancel)
            //    {
            //    }
            //}
            //else
            //{
            //1.获得当前编辑图层
            var drawspecial = new DrawSpecialCommon();
            const string sLayerAliasName = LayerNames.DEFALUT_BOREHOLE; //“钻孔”图层
            var featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            if (featureLayer == null)
            {
                MessageBox.Show(@"未找到" + sLayerAliasName + @"图层,无法删钻孔图元。");
                return;
            }

            if (borehole.BoreholeId != 0)
            {
                DataEditCommon.DeleteFeatureByBId(featureLayer, borehole.BindingId);
            }

            var dlgResult = MessageBox.Show(@"是：见煤钻孔，否：未见煤钻孔，取消：不绘制钻孔",
                @"绘制钻孔", MessageBoxButtons.YesNoCancel);

            switch (dlgResult)
            {
                case DialogResult.Yes:
                    DrawZuanKong(borehole, borehole.BoreholeLithologys.First());
                    break;
                case DialogResult.No:
                    DrawZuanKong(borehole);
                    break;
                case DialogResult.Cancel:
                    break;
            }
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        ///     取  消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭窗体
            Close();
        }

        /// <summary>
        ///     验证画面入力数据
        /// </summary>
        /// <returns>验证结果：true 通过验证, false未通过验证</returns>
        private bool Check()
        {
            // 判断孔号是否录入
            if (!LibCommon.Check.isEmpty(txtBoreholeNumber, Const_GM.BOREHOLE_NUMBER))
            {
                return false;
            }

            // 判断地面标高是否录入
            if (!LibCommon.Check.isEmpty(txtGroundElevation, Const_GM.GROUND_ELEVATION))
            {
                return false;
            }

            // 判断地面标高是否为数字
            if (!LibCommon.Check.IsNumeric(txtGroundElevation, Const_GM.GROUND_ELEVATION))
            {
                return false;
            }

            // 判断坐标X是否录入
            if (!LibCommon.Check.isEmpty(txtCoordinateX, Const_GM.COORDINATE_X))
            {
                return false;
            }

            // 判断坐标X是否为数字
            if (!LibCommon.Check.IsNumeric(txtCoordinateX, Const_GM.COORDINATE_X))
            {
                return false;
            }

            // 判断坐标Y是否录入
            if (!LibCommon.Check.isEmpty(txtCoordinateY, Const_GM.COORDINATE_Y))
            {
                return false;
            }

            // 判断坐标Y是否为数字
            if (!LibCommon.Check.IsNumeric(txtCoordinateY, Const_GM.COORDINATE_Y))
            {
                return false;
            }

            // 判断坐标Z是否录入
            if (!LibCommon.Check.isEmpty(txtCoordinateZ, Const_GM.COORDINATE_Z))
            {
                return false;
            }

            // 判断坐标Z是否为数字
            if (!LibCommon.Check.IsNumeric(txtCoordinateZ, Const_GM.COORDINATE_Z))
            {
                return false;
            }

            // 判断岩性是否入力
            if (gvCoalSeamsTexture.Rows.Count - 1 == 0)
            {
                Alert.alert(Const_GM.LITHOLOGY_MUST_INPUT); // 请录入煤层的岩性！
                return false;
            }

            // 临时存储煤层名称
            var arrCoalSeamsName = new List<String>();

            // 判断底板标高、厚度是否入力，以及入力的是否为数字
            for (var i = 0; i < gvCoalSeamsTexture.RowCount; i++)
            {
                // 最后一行为空行时，跳出循环
                if (i == gvCoalSeamsTexture.RowCount - 1)
                {
                    break;
                }

                // 岩性
                var cell0 = gvCoalSeamsTexture.Rows[i].Cells[0] as DataGridViewComboBoxCell;
                // 判断岩性是否选择
                if (cell0 != null && cell0.Value == null)
                {
                    Alert.alert("第" + (i + 1) + "行，请选择岩性！");
                    return false;
                }

                // 底板标高
                var cell1 = gvCoalSeamsTexture.Rows[i].Cells[1] as DataGridViewTextBoxCell;
                // 判断底板标高是否入力
                if (cell1 != null && cell1.Value == null)
                {
                    cell1.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert("第" + (i + 1) + "行，底板标高不能为空！");
                    return false;
                }
                if (cell1 != null)
                {
                    cell1.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                    // 判断底板标高是否为数字
                    if (!Validator.IsNumeric(cell1.Value.ToString()))
                    {
                        cell1.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        Alert.alert("第" + (i + 1) + "行，底板标高应为数字！");
                        return false;
                    }
                    cell1.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }

                // 厚度
                var cell2 = gvCoalSeamsTexture.Rows[i].Cells[2] as DataGridViewTextBoxCell;
                // 判断厚度是否入力
                if (cell2 != null && cell2.Value == null)
                {
                    cell2.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert("第" + (i + 1) + "行，厚度不能为空！");
                    return false;
                }
                if (cell2 != null)
                {
                    cell2.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                    // 判断厚度是否为数字
                    if (!Validator.IsNumeric(cell2.Value.ToString()))
                    {
                        cell2.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        Alert.alert("第" + (i + 1) + "行，厚度应为数字！");
                        return false;
                    }
                    cell2.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }

                // 当岩性选择为煤层时
                var lithology = Lithology.FindOneByCoal();
                if (cell0 != null && Convert.ToString(cell0.Value) == lithology.LithologyName)
                {
                    // 煤层名称
                    var cell3 = gvCoalSeamsTexture.Rows[i].Cells[3] as DataGridViewTextBoxCell;
                    if (cell3 != null)
                    {
                        cell3.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                        // 特殊符号判断
                        //if (Validator.checkSpecialCharacters(cell3.Value.ToString()))
                        //{
                        //    cell3.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        //    Alert.alert("第" + (i + 1) + "行，煤层结构名称包含特殊符号！");
                        //    return false;
                        //}
                        //else
                        //{
                        if (cell3.Value.ToString().Trim() != "")
                        {
                            cell3.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                            // 煤层名称不能重复
                            if (!arrCoalSeamsName.Contains(cell3.Value.ToString().Trim()))
                            {
                                arrCoalSeamsName.Add(cell3.Value.ToString().Trim());
                                cell3.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                            }
                            else
                            {
                                cell3.Style.BackColor = Const.ERROR_FIELD_COLOR;
                                Alert.alert("第" + (i + 1) + "行，煤层名称重复！（同一钻孔不能有相同的煤层名称）");
                                return false;
                            }
                        }
                    }
                }

                var cell40 = gvCoalSeamsTexture.Rows[i].Cells[4] as DataGridViewTextBoxCell;
                // 判断坐标X是否为数字
                if (cell40 != null && !Validator.IsNumeric(Convert.ToString(cell40.Value)))
                {
                    cell40.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert("第" + (i + 1) + "行，坐标X应为数字！");
                    return false;
                }
                if (cell40 != null) cell40.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                var cell50 = gvCoalSeamsTexture.Rows[i].Cells[5] as DataGridViewTextBoxCell;
                // 判断坐标Y是否为数字
                if (cell50 != null && !Validator.IsNumeric(Convert.ToString(cell50.Value)))
                {
                    cell50.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert("第" + (i + 1) + "行，坐标Y应为数字！");
                    return false;
                }
                if (cell50 != null) cell50.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                var cell60 = gvCoalSeamsTexture.Rows[i].Cells[6] as DataGridViewTextBoxCell;
                // 判断坐标Z是否为数字
                if (cell60 != null && !Validator.IsNumeric(Convert.ToString(cell60.Value)))
                {
                    cell60.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert("第" + (i + 1) + "行，坐标Z应为数字！");
                    return false;
                }
                if (cell60 != null) cell60.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }

            // 验证通过
            return true;
        }

        /// <summary>
        ///     岩性选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvCoalSeamsTexture_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0) return;
            // 岩性
            if (gvCoalSeamsTexture.CurrentRow == null) return;
            var cell0 =
                gvCoalSeamsTexture.Rows[gvCoalSeamsTexture.CurrentRow.Index].Cells[0] as
                    DataGridViewComboBoxCell;
            // 煤层名称
            var cell3 =
                gvCoalSeamsTexture.Rows[gvCoalSeamsTexture.CurrentRow.Index].Cells[3] as
                    DataGridViewTextBoxCell;

            var lithology = Lithology.FindOneByCoal();

            // 当岩性名称选择为“煤”时，煤层名称可编辑，否则煤层名称设置为不可编辑，并清空
            if (cell0 != null && Convert.ToString(cell0.Value) ==
                lithology.LithologyName)
            {
                if (cell3 != null) cell3.ReadOnly = false;
            }
            else
            {
                if (cell3 == null) return;
                cell3.Value = "";
                cell3.ReadOnly = true;
            }
        }

        /// <summary>
        ///     修改岩性时，当前记录为煤时，煤层名称设为可编辑，否则设为不可编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvCoalSeamsTexture_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            for (var i = 0; i < gvCoalSeamsTexture.RowCount - 1; i++)
            {
                // 岩性
                var cell0 = gvCoalSeamsTexture.Rows[i].Cells[0] as DataGridViewComboBoxCell;
                // 煤层名称
                var cell3 = gvCoalSeamsTexture.Rows[i].Cells[3] as DataGridViewTextBoxCell;


                var lithology = Lithology.FindOneByCoal();
                // 当岩性名称选择为“煤”时，煤层名称可编辑，否则煤层名称设置为不可编辑，并清空
                if (cell0 != null && Convert.ToString(cell0.Value) ==
                    lithology.LithologyName)
                {
                    if (cell3 != null) cell3.ReadOnly = false;
                }
                else
                {
                    if (cell3 == null) continue;
                    cell3.Value = "";
                    cell3.ReadOnly = true;
                }
            }
        }

        /// <summary>
        ///     行删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvCoalSeamsTexture_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 判断列索引是不是删除按钮
            if (e.ColumnIndex != 7) return;
            //// 最后一行为空行时，跳出循环
            // 最后一行删除按钮设为不可
            if (gvCoalSeamsTexture.CurrentRow == null ||
                gvCoalSeamsTexture.RowCount - 1 == gvCoalSeamsTexture.CurrentRow.Index) return;
            if (Alert.confirm(Const.DEL_CONFIRM_MSG))
            {
                gvCoalSeamsTexture.Rows.Remove(gvCoalSeamsTexture.CurrentRow);
            }
        }

        /// <summary>
        ///     右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvCoalSeamsTexture_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (gvCoalSeamsTexture.Rows[e.RowIndex].Selected == false)
                    {
                        gvCoalSeamsTexture.ClearSelection();
                        gvCoalSeamsTexture.Rows[e.RowIndex].Selected = true;
                        gvCoalSeamsTexture.CurrentCell = gvCoalSeamsTexture.Rows[e.RowIndex].Cells[0];
                    }
                }
            }
        }

        /// <summary>
        ///     上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gvCoalSeamsTexture.CurrentRow != null)
            {
                var iNowIndex = gvCoalSeamsTexture.CurrentRow.Index;

                if (iNowIndex == 0)
                {
                    Alert.alert("无法上移");
                    return;
                }

                var objArrRowData = new object[7];

                var index = -1;
                var n = -1;
                objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
                objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
                objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
                objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
                objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
                objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
                objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;

                index = -1;
                gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                    gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[index].Value;
                gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                    gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[index].Value;
                gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                    gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[index].Value;
                gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                    gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[index].Value;
                gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                    gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[index].Value;
                gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                    gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[index].Value;
                gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                    gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[index].Value;

                index = -1;
                n = -1;
                gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[++index].Value = objArrRowData[++n];
                gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[++index].Value = objArrRowData[++n];
                gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[++index].Value = objArrRowData[++n];
                gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[++index].Value = objArrRowData[++n];
                gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[++index].Value = objArrRowData[++n];
                gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[++index].Value = objArrRowData[++n];
                gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[++index].Value = objArrRowData[++n];

                gvCoalSeamsTexture.CurrentCell = gvCoalSeamsTexture.Rows[iNowIndex - 1].Cells[0]; //设定当前行
                gvCoalSeamsTexture.Rows[iNowIndex - 1].Selected = true;
            }
        }

        /// <summary>
        ///     下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gvCoalSeamsTexture.CurrentRow == null) return;
            var iNowIndex = gvCoalSeamsTexture.CurrentRow.Index;

            if (iNowIndex == gvCoalSeamsTexture.Rows.Count - 2 ||
                iNowIndex == gvCoalSeamsTexture.Rows.Count - 1)
            {
                Alert.alert("无法下移");
                return;
            }

            var objArrRowData = new object[7];

            var index = -1;
            var n = -1;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;
            objArrRowData[++n] = gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value;

            index = -1;
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[index].Value;
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[index].Value;
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[index].Value;
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[index].Value;
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[index].Value;
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[index].Value;
            gvCoalSeamsTexture.Rows[iNowIndex].Cells[++index].Value =
                gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[index].Value;

            index = -1;
            n = -1;
            gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[++index].Value = objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[++index].Value = objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[++index].Value = objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[++index].Value = objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[++index].Value = objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[++index].Value = objArrRowData[++n];
            gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[++index].Value = objArrRowData[++n];

            gvCoalSeamsTexture.CurrentCell = gvCoalSeamsTexture.Rows[iNowIndex + 1].Cells[0]; //设定当前行
            gvCoalSeamsTexture.Rows[iNowIndex + 1].Selected = true;
        }

        /// <summary>
        ///     插入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 插入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gvCoalSeamsTexture.CurrentRow == null) return;
            var iNowIndex = gvCoalSeamsTexture.CurrentRow.Index;

            var newRow = new DataGridViewRow(); //新建行
            gvCoalSeamsTexture.Rows.Insert(iNowIndex, newRow); //当前行的上面插入新行
        }

        /// <summary>
        ///     添加数据顺序编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvCoalSeamsTexture_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y, gvCoalSeamsTexture.RowHeadersWidth - 4, e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                gvCoalSeamsTexture.RowHeadersDefaultCellStyle.Font, rectangle,
                gvCoalSeamsTexture.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void btnQD_Click(object sender, EventArgs e)
        {
            DataEditCommon.PickUpPoint(txtCoordinateX, txtCoordinateY);
        }

        private void btnReadMultTxt_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                RestoreDirectory = true,
                Filter = @"文本文件(*.txt)|*.txt|所有文件(*.*)|*.*",
                Multiselect = true
            };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            _errorMsg = @"失败文件名：";
            pbCount.Maximum = ofd.FileNames.Length;
            pbCount.Value = 0;
            lblTotal.Text = ofd.FileNames.Length.ToString(CultureInfo.InvariantCulture);
            foreach (var fileName in ofd.FileNames)
            {
                var encoder = TxtFileEncoding.GetEncoding(fileName, Encoding.GetEncoding("GB2312"));

                var sr = new StreamReader(fileName, encoder);
                string duqu;
                while ((duqu = sr.ReadLine()) != null)
                {
                    try
                    {
                        var str = duqu.Split('|');
                        var borehole = Borehole.FindOneByBoreholeNum(str[0]) ??
                                       new Borehole {BindingId = IDGenerator.NewBindingID()};

                        borehole.BoreholeNumber = str[0];
                        borehole.GroundElevation = Convert.ToDouble(str[3]);
                        borehole.CoordinateX = Convert.ToDouble(str[1].Split(',')[0]);
                        borehole.CoordinateY = Convert.ToDouble(str[1].Split(',')[1]);
                        borehole.CoordinateZ = 0;
                        borehole.CoalSeamsTexture = String.Empty;
                        // 创建钻孔岩性实体
                        var boreholeLithology = new BoreholeLithology
                        {
                            Borehole = borehole,
                            Lithology = Lithology.FindOneByCoal(),
                            FloorElevation = Convert.ToDouble(str[4]),
                            CoalSeamsName = CoalSeams.FindAll().First().CoalSeamsName,
                            Thickness = Convert.ToDouble(str[2]),
                            CoordinateX = Convert.ToDouble(str[1].Split(',')[0]),
                            CoordinateY = Convert.ToDouble(str[1].Split(',')[1]),
                            CoordinateZ = 0
                        };

                        borehole.BoreholeLithologys = new[] {boreholeLithology};
                        DrawZuanKong(borehole, boreholeLithology);
                        borehole.Save();
                    }
                    catch (Exception)
                    {
                        lblError.Text =
                            (Convert.ToInt32(lblError.Text) + 1).ToString(CultureInfo.InvariantCulture);
                        lblSuccessed.Text =
                            (Convert.ToInt32(lblSuccessed.Text) - 1).ToString(CultureInfo.InvariantCulture);
                        _errorMsg += fileName.Substring(fileName.LastIndexOf(@"\", StringComparison.Ordinal) + 1) + "\n";
                        btnDetails.Enabled = true;
                    }
                }
                lblSuccessed.Text =
                    (Convert.ToInt32(lblSuccessed.Text) + 1).ToString(CultureInfo.InvariantCulture);
                pbCount.Value++;
            }
            Alert.alert("导入成功！");
        }

        private void btnReadTxt_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                RestoreDirectory = true,
                Filter = @"文本文件(*.txt)|*.txt|所有文件(*.*)|*.*"
            };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            foreach (var aa in ofd.FileNames)
            {
                var sr = new StreamReader(@aa, Encoding.GetEncoding("GB2312"));
                string duqu;
                while ((duqu = sr.ReadLine()) != null)
                {
                    var str = duqu.Split('|');
                    txtBoreholeNumber.Text = str[0];
                    txtCoordinateX.Text = str[1].Split(',')[0];
                    txtCoordinateY.Text = str[1].Split(',')[1];
                    txtCoordinateZ.Text = @"0";
                    gvCoalSeamsTexture.Rows.Add("煤层", str[4], str[2], "3#", str[1].Split(',')[0],
                        str[1].Split(',')[1],
                        "0");
                    txtGroundElevation.Text = str[3];
                }
            }
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            Alert.alert(_errorMsg);
        }

        #region 绘制钻孔

        /// <summary>
        ///     见煤钻孔
        /// </summary>
        /// <param name="breholeEntity">钻孔实体</param>
        /// <param name="boreholeLithologyEntity">钻孔岩性实体</param>
        private void DrawZuanKong(Borehole breholeEntity, BoreholeLithology boreholeLithologyEntity)
        {
            ////1.获得当前编辑图层
            //DrawSpecialCommon drawspecial = new DrawSpecialCommon();
            //string sLayerAliasName = LibCommon.LibLayerNames.DEFALUT_BOREHOLE;//“钻孔”图层
            //IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            //if (featureLayer == null)
            //{
            //    MessageBox.Show("未找到" + sLayerAliasName + "图层,无法绘制钻孔图元。");
            //    return;
            //}

            ////2.绘制图元
            //IPoint pt = new PointClass();
            //pt.X = breholeEntity.CoordinateX;
            //pt.Y = breholeEntity.CoordinateY;
            //pt.Z = breholeEntity.CoordinateZ;

            ////标注内容
            //string strH = breholeEntity.GroundElevation.ToString();//地面标高
            //string strName = breholeEntity.BoreholeNumber.ToString();//孔号（名称）
            //string strDBBG = boreholeLithologyEntity.FloorElevation.ToString();//底板标高
            //string strMCHD = boreholeLithologyEntity.Thickness.ToString();//煤层厚度

            //GIS.SpecialGraphic.DrawZK1 drawZK1 = new GIS.SpecialGraphic.DrawZK1(strName, strH, strDBBG, strMCHD);
            //DataEditCommon.g_CurWorkspaceEdit.StartEditing(false);
            //DataEditCommon.g_CurWorkspaceEdit.StartEditOperation();
            //IFeature feature = featureLayer.FeatureClass.CreateFeature();

            //IGeometry geometry = pt;
            //DataEditCommon.ZMValue(feature, geometry);   //几何图形Z值处理
            ////drawspecial.ZMValue(feature, geometry);//几何图形Z值处理
            //feature.Shape = pt;//要素形状
            ////要素ID字段赋值（对应属性表中BindingID）
            //int iFieldID = feature.Fields.FindField("BID");
            //feature.Value[iFieldID] =breholeEntity.BindingId.ToString();
            //feature.Store();//存储要素
            //DataEditCommon.g_CurWorkspaceEdit.StopEditOperation();
            //DataEditCommon.g_CurWorkspaceEdit.StopEditing(true);
            //string strValue = feature.get_Value(feature.Fields.FindField("OBJECTID")).ToString();
            //DataEditCommon.SpecialPointRenderer(featureLayer, "OBJECTID", strValue, drawZK1.m_Bitmap);

            /////3.显示钻孔图层
            //if (featureLayer.Visible == false)
            //    featureLayer.Visible = true;

            //IEnvelope envelop = feature.Shape.Envelope;
            //DataEditCommon.g_pMyMapCtrl.ActiveView.Extent = envelop;
            //DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
            //DataEditCommon.g_pMyMapCtrl.ActiveView.Refresh();

            IPoint pt = new PointClass();
            pt.X = breholeEntity.CoordinateX;
            pt.Y = breholeEntity.CoordinateY;
            pt.Z = breholeEntity.CoordinateZ;
            if (double.IsNaN(pt.Z))
                pt.Z = 0;
            var pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.DEFALUT_BOREHOLE);
            if (pLayer == null)
            {
                MessageBox.Show(@"未找到钻孔图层,无法绘制钻孔图元。");
                return;
            }
            var pFeatureLayer = (IFeatureLayer) pLayer;
            IGeometry geometry = pt;
            var list = new List<ziduan>
            {
                new ziduan("bid", breholeEntity.BindingId),
                new ziduan("BOREHOLE_NUMBER", breholeEntity.BoreholeNumber),
                new ziduan("addtime", DateTime.Now.ToString(CultureInfo.InvariantCulture)),
                new ziduan("GROUND_ELEVATION", breholeEntity.GroundElevation.ToString(CultureInfo.InvariantCulture)),
                new ziduan("FLOOR_ELEVATION",
                    boreholeLithologyEntity.FloorElevation.ToString(CultureInfo.InvariantCulture)),
                new ziduan("THICKNESS", boreholeLithologyEntity.Thickness.ToString(CultureInfo.InvariantCulture)),
                new ziduan("type", "2")
            };

            var pfeature = DataEditCommon.CreateNewFeature(pFeatureLayer, geometry, list);
            if (pfeature != null)
            {
                MyMapHelp.Jump(pt);
                DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(
                    (esriViewDrawPhase) 34, null, null);
            }
        }

        /// <summary>
        ///     未见煤钻孔
        /// </summary>
        /// <param name="breholeEntity">钻孔实体</param>
        private void DrawZuanKong(Borehole breholeEntity)
        {
            ////1.获得当前编辑图层
            //DrawSpecialCommon drawspecial = new DrawSpecialCommon();
            //string sLayerAliasName = LibCommon.LibLayerNames.DEFALUT_BOREHOLE;//“钻孔”图层
            //IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            //if (featureLayer == null)
            //{
            //    MessageBox.Show("未找到" + sLayerAliasName + "图层,无法绘制钻孔图元。");
            //    return;
            //}

            ////2.绘制图元
            //IPoint pt = new PointClass();
            //pt.X = breholeEntity.CoordinateX;
            //pt.Y = breholeEntity.CoordinateY;
            //pt.Z = breholeEntity.CoordinateZ;
            //if (pt.Z == double.NaN)
            //    pt.Z = 0;
            //GIS.SpecialGraphic.DrawZK2 drawZK2 = null;
            ////标注内容
            //string strH =breholeEntity.GroundElevation.ToString();//地面标高
            //string strName = breholeEntity.BoreholeNumber.ToString();//孔号（名称）

            //IFeature feature = featureLayer.FeatureClass.CreateFeature();
            //IGeometry geometry = pt;
            //DataEditCommon.ZMValue(feature, geometry);   //几何图形Z值处理
            ////drawspecial.ZMValue(feature, geometry);    //几何图形Z值处理
            //feature.Shape = pt;//要素形状
            ////要素ID字段赋值（对应属性表中BindingID）
            //int iFieldID = feature.Fields.FindField("ID");
            //feature.Value[iFieldID] = breholeEntity.BindingId.ToString();
            //feature.Store();//存储要素

            //string strValue = feature.get_Value(feature.Fields.FindField("OBJECTID")).ToString();
            //DataEditCommon.SpecialPointRenderer(featureLayer, "OBJECTID", strValue, drawZK2.m_Bitmap);

            /////3.显示钻孔图层
            //if (featureLayer.Visible == false)
            //    featureLayer.Visible = true;

            //IEnvelope envelop = feature.Shape.Envelope;
            //DataEditCommon.g_pMyMapCtrl.ActiveView.Extent = envelop;
            //DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
            //DataEditCommon.g_pMyMapCtrl.ActiveView.Refresh();

            IPoint pt = new PointClass();
            pt.X = breholeEntity.CoordinateX;
            pt.Y = breholeEntity.CoordinateY;
            pt.Z = breholeEntity.CoordinateZ;
            if (double.IsNaN(pt.Z))
                pt.Z = 0;
            var pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.DEFALUT_BOREHOLE);
            if (pLayer == null)
            {
                MessageBox.Show(@"未找到钻孔图层,无法绘制钻孔图元。");
                return;
            }
            var pFeatureLayer = (IFeatureLayer) pLayer;
            IGeometry geometry = pt;
            var list = new List<ziduan>
            {
                new ziduan(GIS_Const.FIELD_BID, breholeEntity.BindingId),
                new ziduan(GIS_Const.FIELD_BOREHOLE_NUMBER, breholeEntity.BoreholeNumber),
                new ziduan(GIS_Const.FIELD_ADD_TIME, DateTime.Now.ToString(CultureInfo.InvariantCulture)),
                new ziduan(GIS_Const.FIELD_GROUND_ELEVATION,
                    breholeEntity.GroundElevation.ToString(CultureInfo.InvariantCulture)),
                new ziduan(GIS_Const.FIELD_GROUND_FLOOR_ELEVATION, ""),
                new ziduan(GIS_Const.FIELD_THICKNESS, ""),
                new ziduan(GIS_Const.FIELD_TYPE, "1")
            };

            var pfeature = DataEditCommon.CreateNewFeature(pFeatureLayer, geometry, list);
            if (pfeature == null) return;
            MyMapHelp.Jump(pt);
            DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(
                (esriViewDrawPhase) 34, null, null);
        }

        #endregion
    }
}