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

namespace geoInput
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
            DataBindUtil.LoadLithology(LITHOLOGY);
        }

        public BoreholeInfoEntering(IPoint pt)
        {
            InitializeComponent();

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
            using (new SessionScope())
            {
                borehole = Borehole.Find(borehole.borehole_id);
                // 孔号
                txtBoreholeNumber.Text = borehole.borehole_number;
                // 地面标高
                txtGroundElevation.Text = borehole.ground_elevation.ToString(CultureInfo.InvariantCulture);
                // X坐标
                txtCoordinateX.Text = borehole.coordinate_x.ToString(CultureInfo.InvariantCulture);
                // Y坐标
                txtCoordinateY.Text = borehole.coordinate_y.ToString(CultureInfo.InvariantCulture);
                // Z坐标
                txtCoordinateZ.Text = borehole.coordinate_z.ToString(CultureInfo.InvariantCulture);

                // 获取岩性信息

                DataBindUtil.LoadLithology(LITHOLOGY);

                // 明细


                gvCoalSeamsTexture.RowCount = borehole.borehole_lithologys.Count + 1;
                for (var i = 0; i < borehole.borehole_lithologys.Count; i++)
                {
                    // 岩性名称
                    var iLithologyId = borehole.borehole_lithologys[i].lithology.lithology_id;

                    var lithology = Lithology.Find(iLithologyId);

                    gvCoalSeamsTexture[0, i].Value = lithology.lithology_name;
                    // 底板标高
                    gvCoalSeamsTexture[1, i].Value = borehole.borehole_lithologys[i].floor_elevation;
                    // 厚度
                    gvCoalSeamsTexture[2, i].Value = borehole.borehole_lithologys[i].thickness;
                    // 煤层名称
                    gvCoalSeamsTexture[3, i].Value = borehole.borehole_lithologys[i].coal_seams_name;

                    // 坐标X
                    gvCoalSeamsTexture[4, i].Value =
                        borehole.borehole_lithologys[i].coordinate_x.ToString(CultureInfo.InvariantCulture);

                    // 坐标Y
                    gvCoalSeamsTexture[5, i].Value =
                        borehole.borehole_lithologys[i].coordinate_y.ToString(CultureInfo.InvariantCulture);

                    // 坐标Z
                    gvCoalSeamsTexture[6, i].Value =
                        borehole.borehole_lithologys[i].coordinate_x.ToString(CultureInfo.InvariantCulture);
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
            var borehole = Borehole.find_one_by_borehole_num(txtBoreholeNumber.Text) ??
                           new Borehole {binding_id = IdGenerator.NewBindingId()};
            borehole.borehole_number = txtBoreholeNumber.Text.Trim();
            borehole.ground_elevation = Convert.ToDouble(txtGroundElevation.Text.Trim());
            borehole.coordinate_x = Convert.ToDouble(txtCoordinateX.Text.Trim());
            borehole.coordinate_y = Convert.ToDouble(txtCoordinateY.Text.Trim());
            borehole.coordinate_z = Convert.ToDouble(txtCoordinateZ.Text.Trim());
            borehole.coal_seams_texture = string.Empty;

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
                    floor_elevation = Convert.ToDouble(gvCoalSeamsTexture.Rows[i].Cells[1].Value),
                    thickness = Convert.ToDouble(gvCoalSeamsTexture.Rows[i].Cells[2].Value),
                    coal_seams_name = gvCoalSeamsTexture.Rows[i].Cells[3].Value.ToString(),
                    coordinate_x = Convert.ToDouble(gvCoalSeamsTexture.Rows[i].Cells[4].Value),
                    coordinate_y = Convert.ToDouble(gvCoalSeamsTexture.Rows[i].Cells[5].Value),
                    coordinate_z = Convert.ToDouble(gvCoalSeamsTexture.Rows[i].Cells[6].Value),
                    lithology = Lithology.find_one_by_lithology_name(gvCoalSeamsTexture.Rows[i].Cells[0].Value.ToString()),
                    borehole = borehole
                };
                boreholeLithologys.Add(boreholeLithology);
            }
            borehole.borehole_lithologys = boreholeLithologys;
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

            if (borehole.borehole_id != 0)
            {
                DataEditCommon.DeleteFeatureByBId(featureLayer, borehole.binding_id);
            }

            var dlgResult = MessageBox.Show(@"是：见煤钻孔，否：未见煤钻孔，取消：不绘制钻孔",
                @"绘制钻孔", MessageBoxButtons.YesNoCancel);

            switch (dlgResult)
            {
                case DialogResult.Yes:
                    DrawZuanKong(borehole, borehole.borehole_lithologys.First());
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

            var lithology = Lithology.find_one_by_coal();

            // 当岩性名称选择为“煤”时，煤层名称可编辑，否则煤层名称设置为不可编辑，并清空
            if (cell0 != null && Convert.ToString(cell0.Value) ==
                lithology.lithology_name)
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


                var lithology = Lithology.find_one_by_coal();
                // 当岩性名称选择为“煤”时，煤层名称可编辑，否则煤层名称设置为不可编辑，并清空
                if (cell0 != null && Convert.ToString(cell0.Value) ==
                    lithology.lithology_name)
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
            if (Alert.Confirm("确认要删除吗？"))
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
                    Alert.AlertMsg("无法上移");
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
                Alert.AlertMsg("无法下移");
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
                        var borehole = Borehole.find_one_by_borehole_num(str[0]) ??
                                       new Borehole {binding_id = IdGenerator.NewBindingId()};

                        borehole.borehole_number = str[0];
                        borehole.ground_elevation = Convert.ToDouble(str[3]);
                        borehole.coordinate_x = Convert.ToDouble(str[1].Split(',')[0]);
                        borehole.coordinate_y = Convert.ToDouble(str[1].Split(',')[1]);
                        borehole.coordinate_z = 0;
                        borehole.coal_seams_texture = String.Empty;
                        // 创建钻孔岩性实体
                        var boreholeLithology = new BoreholeLithology
                        {
                            borehole = borehole,
                            lithology = Lithology.find_one_by_coal(),
                            floor_elevation = Convert.ToDouble(str[4]),
                            coal_seams_name = CoalSeams.FindAll().First().coal_seams_name,
                            thickness = Convert.ToDouble(str[2]),
                            coordinate_x = Convert.ToDouble(str[1].Split(',')[0]),
                            coordinate_y = Convert.ToDouble(str[1].Split(',')[1]),
                            coordinate_z = 0
                        };

                        borehole.borehole_lithologys = new[] {boreholeLithology};
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
            Alert.AlertMsg("导入成功！");
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
            Alert.AlertMsg(_errorMsg);
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
            //feature.Value[iFieldID] =breholeEntity.bid.ToString();
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
            pt.X = breholeEntity.coordinate_x;
            pt.Y = breholeEntity.coordinate_y;
            pt.Z = breholeEntity.coordinate_z;
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
                new ziduan("bid", breholeEntity.binding_id),
                new ziduan("BOREHOLE_NUMBER", breholeEntity.borehole_number),
                new ziduan("addtime", DateTime.Now.ToString(CultureInfo.InvariantCulture)),
                new ziduan("GROUND_ELEVATION", breholeEntity.ground_elevation.ToString(CultureInfo.InvariantCulture)),
                new ziduan("FLOOR_ELEVATION",
                    boreholeLithologyEntity.floor_elevation.ToString(CultureInfo.InvariantCulture)),
                new ziduan("THICKNESS", boreholeLithologyEntity.thickness.ToString(CultureInfo.InvariantCulture)),
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
            //feature.Value[iFieldID] = breholeEntity.bid.ToString();
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
            pt.X = breholeEntity.coordinate_x;
            pt.Y = breholeEntity.coordinate_y;
            pt.Z = breholeEntity.coordinate_z;
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
                new ziduan(GIS_Const.FIELD_BID, breholeEntity.binding_id),
                new ziduan(GIS_Const.FIELD_BOREHOLE_NUMBER, breholeEntity.borehole_number),
                new ziduan(GIS_Const.FIELD_ADD_TIME, DateTime.Now.ToString(CultureInfo.InvariantCulture)),
                new ziduan(GIS_Const.FIELD_GROUND_ELEVATION,
                    breholeEntity.ground_elevation.ToString(CultureInfo.InvariantCulture)),
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