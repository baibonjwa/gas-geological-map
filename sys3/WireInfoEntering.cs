using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Castle.ActiveRecord;
using ESRI.ArcGIS.Geometry;
using GIS;
using GIS.Common;
using GIS.HdProc;
using GIS.SpecialGraphic;
using LibCommon;
using LibEntity;
using Point = ESRI.ArcGIS.Geometry.Point;

namespace sys3
{
    public partial class WireInfoEntering : Form
    {
        private String _errorMsg = "";
        /**********变量声明***********/
        private double _tmpDouble;
        private int _tmpRowIndex = -1;

        /// <summary>
        ///     构造方法
        /// </summary>
        public WireInfoEntering()
        {
            InitializeComponent();
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.WIRE_INFO_ADD);
            selectTunnelUserControl1.LoadData();
            // 注册委托事件
            //selectTunnelUserControl1.TunnelNameChanged +=
            //    InheritTunnelNameChanged;
            ////巷道信息赋值
            //Dictionary<string, string> flds = new Dictionary<string, string>();
            //flds.Add(GIS_Const.FIELD_HDID, _tunnelID.ToString());

            //List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selobjs = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerfdlyr, flds);

            //// 序号
            //int xh = 0;
            //if (selobjs.Count > 0)
            //    xh = Convert.ToInt16(selobjs[0].Item3[GIS_Const.FIELD_XH]) + 1;
            //string bid = "";
            //string hdname = "";
            //DataSet dst = LibBusiness.TunnelInfoBLL.selectOneTunnelInfoByTunnelID(_tunnelID);
            //if (dst.Tables[0].Rows.Count > 0)
            //{
            //    bid = dst.Tables[0].Rows[0][LibBusiness.TunnelInfoDbConstNames.BINDINGID].ToString();
            //    hdname = dst.Tables[0].Rows[0][LibBusiness.TunnelInfoDbConstNames.TUNNEL_NAME].ToString();
            //}

            //dics.Clear();
            //dics.Add(GIS_Const.FIELD_HDID, _tunnelID.ToString());
            //dics.Add(GIS_Const.FIELD_ID, "0");
            //dics.Add(GIS_Const.FIELD_BS, "1");
            //dics.Add(GIS_Const.FIELD_BID, bid);
            //dics.Add(GIS_Const.FIELD_NAME, hdname);
            //dics.Add(GIS_Const.FIELD_XH, xh.ToString());
        }

        /// <summary>
        ///     构造方法
        /// </summary>
        /// <param name="wire"></param>
        public WireInfoEntering(Wire wire)
        {
            // 初始化主窗体变量
            Wire = wire;
            InitializeComponent();
            // 加载需要修改的导线数据
            var wirePoints = WirePoint.FindAllByWireId(wire.WireId);
            if (wirePoints.Length > 0)
            {
                for (var i = 0; i < wirePoints.Length; i++)
                {
                    dgrdvWire.Rows.Add();
                    dgrdvWire[0, i].Value = wirePoints[i].WirePointName;
                    dgrdvWire[1, i].Value = wirePoints[i].CoordinateX;
                    dgrdvWire[2, i].Value = wirePoints[i].CoordinateY;
                    dgrdvWire[3, i].Value = wirePoints[i].CoordinateZ;
                    dgrdvWire[4, i].Value = wirePoints[i].LeftDis;
                    dgrdvWire[5, i].Value = wirePoints[i].RightDis;
                    dgrdvWire[6, i].Value = wirePoints[i].TopDis;
                    dgrdvWire[7, i].Value = wirePoints[i].BottomDis;
                }
            }

            txtWireName.Text = wire.WireName;
            txtWireLevel.Text = wire.WireLevel;
            dtpMeasureDate.Value = wire.MeasureDate;
            cboVobserver.Text = wire.Vobserver;
            cboVobserver.Text = wire.Vobserver;
            cboCounter.Text = wire.Counter;
            cboCounter.Text = wire.Counter;
            dtpCountDate.Value = wire.CountDate;
            cboChecker.Text = wire.Checker;
            cboChecker.Text = wire.Checker;
            dtpCheckDate.Value = wire.CheckDate;

            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.WIRE_INFO_CHANGE);
            //this.selectTunnelUserControl1.setCurSelectedID(_arr);

            // 注册委托事件
            //selectTunnelUserControl1.TunnelNameChanged +=
            //    InheritTunnelNameChanged;

            //巷道信息赋值
            //Dictionary<string, string> flds = new Dictionary<string, string>();
            //flds.Add(GIS_Const.FIELD_HDID, _tunnelID.ToString());
            //List<Tuple<IFeature, IGeometry, Dictionary<string, string>>> selobjs = Global.commonclss.SearchFeaturesByGeoAndText(Global.centerfdlyr, flds);
            //int xh = 0;
            //string bid = "";
            //string hdname = "";
            //DataSet dst=LibBusiness.TunnelInfoBLL.selectOneTunnelInfoByTunnelID(_tunnelID);
            //if (dst.Tables[0].Rows.Count > 0)
            //{
            //    bid = dst.Tables[0].Rows[0][LibBusiness.TunnelInfoDbConstNames.BINDINGID].ToString();
            //    hdname = dst.Tables[0].Rows[0][LibBusiness.TunnelInfoDbConstNames.TUNNEL_NAME].ToString();
            //}
            //if (selobjs.Count > 0)
            //    xh = Convert.ToInt16(selobjs[0].Item3[GIS_Const.FIELD_XH]) + 1;

            //dics.Clear();
            //dics.Add(GIS_Const.FIELD_HDID, _tunnelID.ToString());
            //dics.Add(GIS_Const.FIELD_ID, "0");
            //dics.Add(GIS_Const.FIELD_BS, "1");
            //dics.Add(GIS.GIS_Const.FIELD_BID, bid);
            //dics.Add(GIS_Const.FIELD_HDNAME, hdname);
            //dics.Add(GIS_Const.FIELD_XH, (xh + 1).ToString());
        }

        private Wire Wire { get; set; }

        /// <summary>
        ///     提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            var wirePoints = GetWirePointListFromDataGrid();
            using (new SessionScope())
            {
                var wire = Wire.FindOneByTunnelId(selectTunnelUserControl1.SelectedTunnel.TunnelId);
                if (wirePoints.Count < 2)
                {
                    Alert.alert(Const_GM.WIRE_INFO_MSG_POINT_MUST_MORE_THAN_TWO);
                    return;
                }
                if (wire != null)
                {
                    if (Alert.confirm("该巷道已绑定导线点，是否覆盖？"))
                    {
                        //foreach (var p in wire.WirePoints)
                        //{
                        //    p.Delete();
                        //}
                        foreach (var p in wirePoints)
                        {
                            p.Wire = wire;
                            p.Save();
                        }
                        wire.WireName = txtWireName.Text;
                        wire.WireLevel = txtWireLevel.Text;
                        wire.MeasureDate = dtpMeasureDate.Value;
                        wire.Vobserver = cboVobserver.Text;
                        wire.Counter = cboCounter.Text;
                        wire.CountDate = dtpCountDate.Value;
                        wire.Checker = cboChecker.Text;
                        wire.CheckDate = dtpCheckDate.Value;
                        wire.Save();
                        DrawWirePoint(wirePoints, "CHANGE");
                        double hdwid;
                        _dics = ConstructDics(selectTunnelUserControl1.SelectedTunnel, out hdwid);
                        if (selectTunnelUserControl1.SelectedTunnel != null)
                        {
                            UpdateHdbyPnts(selectTunnelUserControl1.SelectedTunnel.TunnelId, wirePoints, _dics, hdwid);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    wire = new Wire
                    {
                        Tunnel = selectTunnelUserControl1.SelectedTunnel,
                        WireName = txtWireName.Text,
                        WireLevel = txtWireLevel.Text,
                        MeasureDate = dtpMeasureDate.Value,
                        Vobserver = cboVobserver.Text,
                        Counter = cboCounter.Text,
                        CountDate = dtpCountDate.Value,
                        Checker = cboChecker.Text,
                        CheckDate = dtpCheckDate.Value
                    };
                    foreach (var p in wirePoints)
                    {
                        p.Wire = wire;
                        p.Save();
                    }
                    wire.Save();

                    DrawWirePoint(wirePoints, "ADD");

                    double hdwid;
                    _dics = ConstructDics(selectTunnelUserControl1.SelectedTunnel, out hdwid);
                    AddHdbyPnts(wirePoints, _dics, hdwid);
                }
            }
            DialogResult = DialogResult.OK;
        }

        private Dictionary<string, string> ConstructDics(Tunnel tunnel, out double hdwid)
        {
            //巷道信息赋值
            var flds = new Dictionary<string, string>
            {
                {
                    GIS_Const.FIELD_HDID,
                    tunnel.TunnelId.ToString(CultureInfo.InvariantCulture)
                }
            };
            var selobjs =
                Global.commonclss.SearchFeaturesByGeoAndText(Global.centerfdlyr, flds);

            var xh = 0;
            if (selobjs.Count > 0)
                xh = Convert.ToInt16(selobjs[0].Item3[GIS_Const.FIELD_XH]) + 1;
            var bid = tunnel.BindingId;
            var hdname = tunnel.TunnelName;
            hdwid = tunnel.TunnelWid;
            _dics.Clear();
            _dics.Add(GIS_Const.FIELD_HDID, tunnel.TunnelId.ToString(CultureInfo.InvariantCulture));
            _dics.Add(GIS_Const.FIELD_ID, "0");
            _dics.Add(GIS_Const.FIELD_BS, "1");
            _dics.Add(GIS_Const.FIELD_BID, bid);
            _dics.Add(GIS_Const.FIELD_HDNAME, hdname);
            _dics.Add(GIS_Const.FIELD_XH, (xh + 1).ToString(CultureInfo.InvariantCulture));
            return _dics;
        }

        /// <summary>
        ///     导线点实体赋值
        /// </summary>
        /// <param name="i">Datagridview行号</param>
        /// <returns>导线点实体</returns>
        private WirePoint SetWirePointEntity(int i)
        {
            // 最后一行为空行时，跳出循环
            if (i == dgrdvWire.RowCount - 1)
            {
                return null;
            }
            // 创建导线点实体
            var wirePointInfoEntity = new WirePoint();

            //导线点编号
            if (dgrdvWire.Rows[i].Cells[0] != null)
            {
                wirePointInfoEntity.WirePointName = dgrdvWire.Rows[i].Cells[0].Value.ToString();
            }
            //坐标X
            if (dgrdvWire.Rows[i].Cells[1].Value != null)
            {
                if (double.TryParse(dgrdvWire.Rows[i].Cells[1].Value.ToString(), out _tmpDouble))
                {
                    wirePointInfoEntity.CoordinateX = _tmpDouble;
                    _tmpDouble = 0;
                }
            }
            //坐标Y
            if (dgrdvWire.Rows[i].Cells[2].Value != null)
            {
                if (double.TryParse(dgrdvWire.Rows[i].Cells[2].Value.ToString(), out _tmpDouble))
                {
                    wirePointInfoEntity.CoordinateY = _tmpDouble;
                    _tmpDouble = 0;
                }
            }
            //坐标Z
            if (dgrdvWire.Rows[i].Cells[3].Value != null)
            {
                if (double.TryParse(dgrdvWire.Rows[i].Cells[3].Value.ToString(), out _tmpDouble))
                {
                    wirePointInfoEntity.CoordinateZ = _tmpDouble;
                    _tmpDouble = 0;
                }
            }
            //距左帮距离
            if (dgrdvWire.Rows[i].Cells[4].Value != null)
            {
                if (double.TryParse(dgrdvWire.Rows[i].Cells[4].Value.ToString(), out _tmpDouble))
                {
                    wirePointInfoEntity.LeftDis = _tmpDouble;
                    _tmpDouble = 0;
                }
            }
            //距右帮距离
            if (dgrdvWire.Rows[i].Cells[5].Value != null)
            {
                if (double.TryParse(dgrdvWire.Rows[i].Cells[5].Value.ToString(), out _tmpDouble))
                {
                    wirePointInfoEntity.RightDis = _tmpDouble;
                    _tmpDouble = 0;
                }
            }
            //距顶板距离
            if (dgrdvWire.Rows[i].Cells[6].Value != null)
            {
                if (double.TryParse(dgrdvWire.Rows[i].Cells[6].Value.ToString(), out _tmpDouble))
                {
                    wirePointInfoEntity.TopDis = _tmpDouble;
                    _tmpDouble = 0;
                }
            }
            //距底板距离
            if (dgrdvWire.Rows[i].Cells[7].Value != null)
            {
                if (double.TryParse(dgrdvWire.Rows[i].Cells[7].Value.ToString(), out _tmpDouble))
                {
                    wirePointInfoEntity.BottomDis = _tmpDouble;
                    _tmpDouble = 0;
                }
            }

            return wirePointInfoEntity;
        }

        /// <summary>
        ///     2014.2.26 lyf 修改函数，返回导线点List，为绘制导线点图形
        /// </summary>
        /// <returns>导线点List</returns>
        private List<WirePoint> GetWirePointListFromDataGrid()
        {
            var wirePoints = new List<WirePoint>();
            for (var i = 0; i < dgrdvWire.RowCount; i++)
            {
                var wirePoint = SetWirePointEntity(i);
                if (wirePoint == null) break;
                wirePoint.BindingId = IDGenerator.NewBindingID();
                wirePoints.Add(wirePoint);
            }

            return wirePoints;
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
            for (var i = 0; i < dgrdvWire.Rows.Count; i++)
            {
                dgrdvWire.BackgroundColor = Const.NO_ERROR_FIELD_COLOR;
            }
            //// 判断巷道信息是否选择
            if (selectTunnelUserControl1.SelectedTunnel == null)
            {
                Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_GM.TUNNEL + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            if (Validator.IsEmpty(txtWireName.Text))
            {
                txtWireName.BackColor = Const.ERROR_FIELD_COLOR;
                Alert.alert(Const.MSG_PLEASE_TYPE_IN + Const_GM.WIRE_NAME + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            txtWireName.BackColor = Const.NO_ERROR_FIELD_COLOR;
            // 判断导线点编号是否入力
            if (dgrdvWire.Rows.Count - 1 == 0)
            {
                Alert.alert(Const.MSG_PLEASE_TYPE_IN + Const_GM.WIRE_POINT_ID + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //dgrdvWire内部判断
            for (var i = 0; i < dgrdvWire.RowCount; i++)
            {
                // 最后一行为空行时，跳出循环
                if (i == dgrdvWire.RowCount - 1)
                {
                    break;
                }
                var cell = dgrdvWire.Rows[i].Cells[0] as DataGridViewTextBoxCell;
                // 判断导线点编号是否入力
                if (cell != null && cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const_GM.WIRE_POINT_ID + Const.MSG_NOT_NULL + Const.SIGN_EXCLAMATION_MARK);
                    return false;
                }
                if (cell != null) cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                ////判断导线点编号是否存在
                //if (Text == Const_GM.WIRE_INFO_ADD)
                //{
                //    //导线点是否存在
                //    if (WirePoint.ExistsByWirePointIdInWireInfo(wireEntity.WireId,
                //        dgrdvWire.Rows[i].Cells[0].Value.ToString()))
                //    {
                //        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                //        Alert.alert(Const_GM.WIRE_POINT_ID + Const.MSG_ALREADY_HAVE + Const.SIGN_EXCLAMATION_MARK);
                //        return false;
                //    }
                //    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //}
                ////判断导线点编号是否有输入重复
                //for (int j = 0; j < i; j++)
                //{
                //    if (dgrdvWire[0, j].Value.ToString() == dgrdvWire[0, i].Value.ToString())
                //    {
                //        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                //        dgrdvWire[0, j].Style.BackColor = Const.ERROR_FIELD_COLOR;
                //        Alert.alert(Const_GM.WIRE_POINT_ID + Const.MSG_DOUBLE_EXISTS + Const.SIGN_EXCLAMATION_MARK);
                //        return false;
                //    }
                //    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //    dgrdvWire[0, j].Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                //}

                //判断坐标X是否入力
                cell = dgrdvWire.Rows[i].Cells[1] as DataGridViewTextBoxCell;
                if (cell != null && cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.X));
                    return false;
                }
                if (cell != null)
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                    // 判断坐标X是否为数字
                    if (!Validator.IsNumeric(cell.Value.ToString()))
                    {
                        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        Alert.alert(Const.rowMustBeNumber(i, Const_GM.X));
                        return false;
                    }
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                //判断坐标Y是否入力
                cell = dgrdvWire.Rows[i].Cells[2] as DataGridViewTextBoxCell;
                if (cell != null && cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.Y));
                    return false;
                }
                if (cell != null)
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                    // 判断坐标Y是否为数字
                    if (!Validator.IsNumeric(cell.Value.ToString()))
                    {
                        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        Alert.alert(Const.rowMustBeNumber(i, Const_GM.Y));
                        return false;
                    }
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                //判断坐标Z是否入力
                cell = dgrdvWire.Rows[i].Cells[3] as DataGridViewTextBoxCell;
                if (cell != null && cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.Z));
                    return false;
                }
                if (cell != null)
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                    // 判断坐标Z是否为数字
                    if (!Validator.IsNumeric(cell.Value.ToString()))
                    {
                        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        Alert.alert(Const.rowMustBeNumber(i, Const_GM.Z));
                        return false;
                    }
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                //判断距左帮距离是否入力
                cell = dgrdvWire.Rows[i].Cells[4] as DataGridViewTextBoxCell;
                if (cell != null && cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.DISTANCE_TO_LEFT));
                    return false;
                }
                if (cell != null)
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                    // 判断距左帮距离是否为数字
                    if (!Validator.IsNumeric(cell.Value.ToString()))
                    {
                        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        Alert.alert(Const.rowMustBeNumber(i, Const_GM.DISTANCE_TO_LEFT));
                        return false;
                    }
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                //判断距右帮距离是否入力
                cell = dgrdvWire.Rows[i].Cells[5] as DataGridViewTextBoxCell;
                if (cell != null && cell.Value == null)
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowNotNull(i, Const_GM.DISTANCE_TO_RIGHT));
                    return false;
                }
                if (cell != null)
                {
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;

                    // 判断距右帮距离是否为数字
                    if (!Validator.IsNumeric(cell.Value.ToString()))
                    {
                        cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                        Alert.alert(Const.rowMustBeNumber(i, Const_GM.DISTANCE_TO_RIGHT));
                        return false;
                    }
                    cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                }
                cell = dgrdvWire.Rows[i].Cells[6] as DataGridViewTextBoxCell;
                // 判断距顶板距离是否为数字
                if (cell != null && (cell.Value != null && !Validator.IsNumeric(cell.Value.ToString())))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.DISTANCE_TO_TOP));
                    return false;
                }
                if (cell != null) cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
                cell = dgrdvWire.Rows[i].Cells[7] as DataGridViewTextBoxCell;
                // 判断距底板距离是否为数字
                if (cell != null && (cell.Value != null && !Validator.IsNumeric(cell.Value.ToString())))
                {
                    cell.Style.BackColor = Const.ERROR_FIELD_COLOR;
                    Alert.alert(Const.rowMustBeNumber(i, Const_GM.DISTANCE_TO_BOTTOM));
                    return false;
                }
                if (cell != null) cell.Style.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }
            //验证成功
            return true;
        }

        /// <summary>
        ///     显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvWire_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y, dgrdvWire.RowHeadersWidth - 4, e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(CultureInfo.InvariantCulture),
                dgrdvWire.RowHeadersDefaultCellStyle.Font, rectangle,
                dgrdvWire.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        /// <summary>
        ///     初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WireInfoEntering_Load(object sender, EventArgs e)
        {
            if (Text == Const_GM.WIRE_INFO_ADD)
            {
                selectTunnelUserControl1.LoadData();
                dtpMeasureDate.Value = DateTime.Now;
                dtpCountDate.Value = DateTime.Now;
                dtpCheckDate.Value = DateTime.Now;
            }
            else
            {
                selectTunnelUserControl1.LoadData(Wire.Tunnel);
            }
        }

        /// <summary>
        ///     datagridview进入行时，按钮可操作性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvWire_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _tmpRowIndex = e.RowIndex;
            btnAdd.Enabled = true;
            btnDel.Enabled = true;
            btnMoveUp.Enabled = e.RowIndex != 0;
            btnMoveDown.Enabled = e.RowIndex <= dgrdvWire.Rows.Count - 3;
            if (e.RowIndex != dgrdvWire.NewRowIndex)
            {
                btnDel.Enabled = true;
            }
            else
            {
                btnMoveUp.Enabled = false;
                btnDel.Enabled = false;
            }
        }

        /// <summary>
        ///     添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgrdvWire.CurrentRow != null)
            {
                dgrdvWire.Rows.Insert(dgrdvWire.CurrentRow.Index, 1);
                dgrdvWire.Focus();
                if (dgrdvWire.CurrentRow != null)
                    dgrdvWire.Rows[dgrdvWire.CurrentRow.Index - 1].Cells[0].Selected = true;
            }
        }

        /// <summary>
        ///     删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgrdvWire.CurrentRow != null &&
                (dgrdvWire.Rows.Count > 1 && dgrdvWire.CurrentRow.Index < dgrdvWire.Rows.Count - 1))
                dgrdvWire.Rows.RemoveAt(_tmpRowIndex);
        }

        /// <summary>
        ///     上移按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            var isLast = _tmpRowIndex == dgrdvWire.Rows.Count - 2;
            if (_tmpRowIndex == dgrdvWire.Rows.Count - 1)
            {
                _tmpRowIndex = dgrdvWire.SelectedRows[0].Index;
            }

            var dgvr = dgrdvWire.Rows[_tmpRowIndex];
            dgrdvWire.Rows.RemoveAt(_tmpRowIndex);

            if (_tmpRowIndex == 0)
                dgrdvWire.Rows.Insert(0, dgvr);
            else
                dgrdvWire.Rows.Insert(_tmpRowIndex - 1, dgvr);

            dgrdvWire.Rows[_tmpRowIndex].Selected = false;

            if (isLast && dgrdvWire.Rows.Count > 3)
            {
                dgrdvWire.Rows[_tmpRowIndex - 2].Selected = true;
                dgrdvWire.CurrentCell = dgrdvWire.Rows[_tmpRowIndex - 2].Cells[0];
                btnMoveDown_Click(sender, e);
            }
        }

        /// <summary>
        ///     下移按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            _tmpRowIndex = dgrdvWire.SelectedRows[0].Index;
            var dgvr = dgrdvWire.Rows[_tmpRowIndex];
            dgrdvWire.Rows.RemoveAt(_tmpRowIndex);
            dgrdvWire.Rows[_tmpRowIndex].Selected = true;
            dgrdvWire.Rows.Insert(_tmpRowIndex + 1, dgvr);
            dgrdvWire.Rows[_tmpRowIndex].Selected = false;
            dgrdvWire.Rows[_tmpRowIndex + 1].Selected = true;
            dgrdvWire.CurrentCell = dgrdvWire.Rows[_tmpRowIndex + 1].Cells[0];
        }

        private void btnTXT_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { RestoreDirectory = true, Filter = @"文本文件(*.txt)|*.txt|所有文件(*.*)|*.*" };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            var fileName = ofd.SafeFileName;
            if (fileName != null)
            {
                var strs = fileName.Split('-');
                var miningAreaName = strs[0];
                var workingFaceName = strs[1];
                var tunnelName = strs[2].Split('.')[0];
                using (new SessionScope())
                {
                    var miningArea = MiningArea.FindOneByMiningAreaName(miningAreaName);
                    if (miningArea == null)
                    {
                        Alert.confirm("该采区不存在，请先添加采区");
                        return;
                    }
                    var workingFace = WorkingFace.FindByWorkingFaceNameAndMiningAreaId(workingFaceName, miningArea.MiningAreaId);
                    if (workingFace == null)
                    {
                        if (Alert.confirm("该工作面不存在，是否创建该工作面？"))
                        {
                            workingFace = AddWorkingFace(miningArea, workingFaceName);
                        }
                    }
                    if (workingFace == null) return;
                    if (workingFace.Tunnels != null &&
                        workingFace.Tunnels.FirstOrDefault(u => u.TunnelName == tunnelName) != null)
                    {
                        var tunnel = workingFace.Tunnels.FirstOrDefault(u => u.TunnelName == tunnelName);
                        selectTunnelUserControl1.LoadData(tunnel);
                    }
                    else
                    {
                        if (Alert.confirm("该巷道不存在，是否创建该巷道？"))
                        {
                            if (Tunnel.ExistsByTunnelNameAndWorkingFaceId(tunnelName, workingFace.WorkingFaceId))
                            {
                                Alert.alert("该巷道已经存在");
                                return;
                            }
                            var tunnel = AddTunnel(workingFace, tunnelName);
                            selectTunnelUserControl1.LoadData(tunnel);
                        }
                    }
                }
                txtWireName.Text = tunnelName.Split('.').Length > 0
                    ? tunnelName.Split('.')[0] + "导线点"
                    : tunnelName + "导线点";
            }

            var sr = new StreamReader(ofd.FileName, Encoding.GetEncoding("GB2312"));
            string duqu;
            while ((duqu = sr.ReadLine()) != null)
            {
                var temp1 = duqu.Split('|');
                if (temp1.Length == 1) continue;
                var daoxianname = temp1[0];
                var daoxianx = temp1[1];
                var daoxiany = temp1[2];
                dgrdvWire.Rows.Add(1);
                dgrdvWire[0, dgrdvWire.Rows.Count - 2].Value = daoxianname;
                dgrdvWire[1, dgrdvWire.Rows.Count - 2].Value = daoxianx;
                dgrdvWire[2, dgrdvWire.Rows.Count - 2].Value = daoxiany;
                dgrdvWire[3, dgrdvWire.Rows.Count - 2].Value = "0";
                dgrdvWire[4, dgrdvWire.Rows.Count - 2].Value = "2.5";
                dgrdvWire[5, dgrdvWire.Rows.Count - 2].Value = "2.5";
            }
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            Alert.alert(_errorMsg);
        }

        #region 绘制导线点和巷道图形

        private Tunnel AddTunnel(WorkingFace workingFace, string tunnelName)
        {
            var type = tunnelName.Contains("横川") ? TunnelTypeEnum.HENGCHUAN : TunnelTypeEnum.OTHER;
            var tunnel = new Tunnel
            {
                TunnelName = tunnelName,
                WorkingFace = workingFace,
                TunnelWid = 5,
                BindingId = IDGenerator.NewBindingID(),
                TunnelType = type
            };
            tunnel.Save();
            return tunnel;
        }

        private WorkingFace AddWorkingFace(MiningArea miningArea, String workingFaceName)
        {
            var workingFace = new WorkingFace
            {
                WorkingFaceName = workingFaceName,
                MiningArea = miningArea,
                WorkingfaceTypeEnum = WorkingfaceTypeEnum.HC
            };
            workingFace.Save();
            return workingFace;
        }

        private Dictionary<string, string> _dics = new Dictionary<string, string>(); //属性字典
        private List<IPoint> _leftpts = new List<IPoint>(); //记录左侧平行线坐标
        private List<IPoint> _rightpts = new List<IPoint>(); //记录右侧平行线坐标

        /// <summary>
        ///     通过（关键/导线）点绘制巷道
        /// </summary>
        /// <param name="wirepntcols">导线信息列表</param>
        /// <param name="dics">巷道属性</param>
        /// <param name="hdwid">巷道宽度</param>
        private void AddHdbyPnts(List<WirePoint> wirepntcols, Dictionary<string, string> dics, double hdwid)
        {
            if (wirepntcols == null || wirepntcols.Count == 0)
                return;

            var pntcols = new List<IPoint>();
            foreach (var t in wirepntcols)
            {
                IPoint pnt = new PointClass();
                pnt.X = t.CoordinateX;
                pnt.Y = t.CoordinateY;
                pnt.Z = t.CoordinateZ;
                pntcols.Add(pnt);
            }

            Global.cons.AddHangdaoToLayer(pntcols, dics, Global.pntlyr, wirepntcols); //添加中线上的点到导线点图层中
            Global.cons.AddDxdLines(pntcols, dics, Global.pntlinlyr, wirepntcols); //添加导线点线图层符号
            Global.cons.AddHangdaoToLayer(pntcols, dics, Global.centerlyr); //添加中心线到线图层中
            Global.cons.AddFDLineToLayer(pntcols, dics, Global.centerfdlyr, 1); //添加分段中心线到中心线分段图层中

            //#################计算交点坐标######################
            _rightpts = Global.cons.GetLRParallelPnts(pntcols, hdwid, 1); //右侧平行线上的端点串
            _leftpts = Global.cons.GetLRParallelPnts(pntcols, hdwid, 0); //左侧平行线上的端点串

            //rightresults = Global.cons.CalculateRegPnts(rightpts);
            //leftresults = Global.cons.CalculateRegPnts(leftpts);
            //results = Global.cons.ConstructPnts(rightresults, leftresults);
            var results = Global.cons.ConstructPnts(_rightpts, _leftpts);

            ////在巷道面显示面中绘制巷道面  
            Global.cons.AddHangdaoToLayer(results, dics, Global.hdfdfulllyr); //添加巷道到巷道图层中
            //Global.cons.AddFDRegToLayer(rightresults, leftresults, pntcols, dics, Global.hdfdlyr);
            //Global.cons.AddHangdaoToLayer(rightpts, dics, Global.pntlyr);
            //Global.cons.AddHangdaoToLayer(leftpts, dics, Global.pntlyr);
            Global.cons.AddFDRegToLayer(_rightpts, _leftpts, pntcols, dics, Global.hdfdlyr, hdwid);
            Global.pActiveView.Refresh();
        }

        /// <summary>
        ///     更新巷道
        /// </summary>
        /// <param name="wirepntcols"></param>
        /// <param name="dics"></param>
        /// <param name="hdwid"></param>
        /// <param name="tunnelId"></param>
        private void UpdateHdbyPnts(int tunnelId, List<WirePoint> wirepntcols, Dictionary<string, string> dics,
            double hdwid)
        {
            if (wirepntcols == null || wirepntcols.Count == 0)
                return;

            var pntcols = new List<IPoint>();
            foreach (var t in wirepntcols)
            {
                IPoint pnt = new PointClass();
                pnt.X = t.CoordinateX;
                pnt.Y = t.CoordinateY;
                pnt.Z = t.CoordinateZ;
                pntcols.Add(pnt);
            }
            //清除图层上对应的信息
            var sql = "\"" + GIS_Const.FIELD_HDID + "\"='" + tunnelId + "'";
            Global.commonclss.DelFeatures(Global.pntlyr, sql);
            Global.commonclss.DelFeatures(Global.pntlinlyr, sql);
            Global.commonclss.DelFeatures(Global.centerlyr, sql);
            Global.commonclss.DelFeatures(Global.centerfdlyr, sql);
            Global.commonclss.DelFeatures(Global.hdfdfulllyr, sql);
            Global.commonclss.DelFeatures(Global.hdfdlyr, sql);
            Global.commonclss.DelFeatures(Global.dslyr, sql);
            //Global.commonclss.DelFeaturesByQueryFilter(Global.pntlyr, sql);
            //Global.commonclss.DelFeaturesByQueryFilter(Global.pntlinlyr, sql);
            //Global.commonclss.DelFeaturesByQueryFilter(Global.centerlyr, sql);
            //Global.commonclss.DelFeaturesByQueryFilter(Global.centerfdlyr, sql);
            //Global.commonclss.DelFeaturesByQueryFilter(Global.hdfdfulllyr, sql);
            //Global.commonclss.DelFeaturesByQueryFilter(Global.hdfdlyr, sql);
            //Global.commonclss.DelFeaturesByQueryFilter(Global.dslyr, sql);
            //重新添加
            Global.cons.AddHangdaoToLayer(pntcols, dics, Global.pntlyr, wirepntcols); //添加中线上的点到导线点图层中
            Global.cons.AddDxdLines(pntcols, dics, Global.pntlinlyr, wirepntcols); //添加导线点线
            Global.cons.AddHangdaoToLayer(pntcols, dics, Global.centerlyr); //添加中心线到线图层中
            Global.cons.AddFDLineToLayer(pntcols, dics, Global.centerfdlyr, 1); //添加分段中心线到中心线分段图层中
            //#################计算交点坐标######################
            _rightpts = Global.cons.GetLRParallelPnts(pntcols, hdwid, 1); //右侧平行线上的端点串
            _leftpts = Global.cons.GetLRParallelPnts(pntcols, hdwid, 0); //左侧平行线上的端点串
            //rightresults = Global.cons.CalculateRegPnts(rightpts);
            //leftresults = Global.cons.CalculateRegPnts(leftpts);
            //results = Global.cons.ConstructPnts(rightresults, leftresults);
            //Global.cons.AddHangdaoToLayer(rightpts, dics, Global.pntlyr);
            //Global.cons.AddHangdaoToLayer(leftpts, dics, Global.pntlyr);
            var results = Global.cons.ConstructPnts(_rightpts, _leftpts);
            //在巷道面显示面中绘制巷道面  
            //Global.cons.AddHangdaoToLayer(rightpts, dics, Global.pntlyr);
            //Global.cons.AddHangdaoToLayer(leftpts, dics, Global.pntlyr);
            Global.cons.AddHangdaoToLayer(results, dics, Global.hdfdfulllyr); //添加巷道到巷道图层中
            //Global.cons.AddFDRegToLayer(rightresults, leftresults, pntcols, dics, Global.hdfdlyr);
            Global.cons.AddFDRegToLayer(_rightpts, _leftpts, pntcols, dics, Global.hdfdlyr, hdwid);
            Global.pActiveView.Refresh();
        }

        /// <summary>
        ///     根据坐标绘制导线点
        /// </summary>
        /// <param name="lstWpie">导线坐标（List）</param>
        /// <param name="addOrChange"></param>
        private void DrawWirePoint(List<WirePoint> lstWpie, string addOrChange)
        {
            IPoint pt = new Point();

            //找到导线点图层
            var map = DataEditCommon.g_pMap;
            const string layerName = LayerNames.DEFALUT_WIRE_PT; //“导线点”图层
            var featureLayer = LayerHelper.GetLayerByName(map, layerName);

            if (featureLayer == null)
            {
                MessageBox.Show(@"没有找到" + layerName + @"图层，将不能绘制导线点。", @"提示", MessageBoxButtons.OK);
                return;
            }

            var drawWirePt = new DrawTunnels();
            //修改导线点操作，要先删除原有导线点要素
            if (addOrChange == "CHANGE")
            {
                foreach (var t in lstWpie)
                {
                    var wirePtInfo = t;
                    DataEditCommon.DeleteFeatureByBId(featureLayer, wirePtInfo.BindingId);
                }
            }

            foreach (var t in lstWpie)
            {
                pt.X = t.CoordinateX;
                pt.Y = t.CoordinateY;
                pt.Z = t.CoordinateZ;
                drawWirePt.CreatePoint(featureLayer, pt, t.BindingId, t);
            }
        }

        ///// <param name="verticesBtmRet">Vector3_DW数据</param>
        /// <summary>
        ///     根据导线点坐标绘制巷道
        /// </summary>
        /// <summary>
        ///     获得导线边线点坐标集
        /// </summary>
        /// <returns>导线边线点坐标集List</returns>
        //private List<IPoint> GetTunnelPts(Vector3_DW[] verticesBtmRet)
        //{
        //    var lstBtmRet = new List<IPoint>();
        //    try
        //    {
        //        Vector3_DW vector3dw;
        //        IPoint pt;
        //        for (int i = 0; i < verticesBtmRet.Length; i++)
        //        {
        //            vector3dw = new Vector3_DW();
        //            vector3dw = verticesBtmRet[i];
        //            pt = new PointClass();
        //            pt.X = vector3dw.X;
        //            pt.Y = vector3dw.Y;
        //            pt.Z = vector3dw.Z;
        //            if (!lstBtmRet.Contains(pt))
        //            {
        //                lstBtmRet.Add(pt);
        //            }
        //        }

        //        return lstBtmRet;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        #endregion 绘制导线点和巷道图形
        private void btnMultTxt_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                RestoreDirectory = true,
                Multiselect = true,
                Filter = @"文本文件(*.txt)|*.txt|所有文件(*.*)|*.*"
            };
            _errorMsg = @"失败文件名：";
            if (ofd.ShowDialog() != DialogResult.OK) return;
            var fileCount = ofd.FileNames.Length;
            pbCount.Maximum = fileCount * 2;
            pbCount.Value = 0;
            foreach (var fileName in ofd.FileNames)
            {
                lblTotal.Text = fileCount.ToString(CultureInfo.InvariantCulture);
                string safeFileName = null;
                try
                {
                    using (new SessionScope())
                    {
                        safeFileName = fileName.Substring(fileName.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
                        var strs = safeFileName.Split('-');
                        var miningAreaName = strs[0];
                        var workingFaceName = strs[1];
                        var tunnelName = strs[2].Split('.')[0];

                        var miningArea = MiningArea.FindOneByMiningAreaName(miningAreaName);
                        if (miningArea == null)
                        {
                            Alert.confirm("该采区不存在，请先添加采区");
                            return;
                        }
                        var workingFace = WorkingFace.FindByWorkingFaceNameAndMiningAreaId(workingFaceName, miningArea.MiningAreaId);
                        if (workingFace == null)
                        {
                            workingFace = AddWorkingFace(miningArea, workingFaceName);
                        }
                        if (workingFace == null) return;
                        Tunnel tunnel;
                        if (workingFace.Tunnels != null &&
                            workingFace.Tunnels.FirstOrDefault(u => u.TunnelName == tunnelName) != null)
                        {
                            tunnel = workingFace.Tunnels.FirstOrDefault(u => u.TunnelName == tunnelName);
                        }
                        else
                        {
                            tunnel = AddTunnel(workingFace, tunnelName);
                        }

                        var sr = new StreamReader(fileName, Encoding.GetEncoding("GB2312"));
                        string fileContent;
                        var wirePoints = new List<WirePoint>();
                        while ((fileContent = sr.ReadLine()) != null)
                        {
                            if (String.IsNullOrEmpty(fileContent)) continue;
                            var temp1 = fileContent.Split('|');
                            var pointName = temp1[0];
                            var pointX = temp1[1];
                            var pointY = temp1[2];

                            wirePoints.Add(new WirePoint
                            {
                                BindingId = IDGenerator.NewBindingID(),
                                WirePointName = pointName,
                                CoordinateX = Convert.ToDouble(pointX),
                                CoordinateY = Convert.ToDouble(pointY),
                                CoordinateZ = 0,
                                LeftDis = 2.5,
                                RightDis = 2.5,
                                TopDis = 0,
                                BottomDis = 0
                            });
                        }
                        if (wirePoints.Count < 2)
                        {
                            Alert.alert(Const_GM.WIRE_INFO_MSG_POINT_MUST_MORE_THAN_TWO);
                            throw new Exception();
                        }
                        var wire = Wire.FindOneByTunnelId(tunnel.TunnelId);

                        if (wire != null)
                        {
                            wire.WireName = tunnelName.Split('.').Length > 0
                                ? tunnelName.Split('.')[0] + "导线点"
                                : tunnelName + "导线点";
                            wire.WirePoints = wirePoints;
                        }
                        else
                        {
                            wire = new Wire
                            {
                                Tunnel = tunnel,
                                CheckDate = DateTime.Now,
                                MeasureDate = DateTime.Now,
                                CountDate = DateTime.Now,
                                WireName =
                                    tunnelName.Split('.').Length > 0
                                        ? tunnelName.Split('.')[0] + "导线点"
                                        : tunnelName + "导线点",
                                WirePoints = wirePoints
                            };
                        }
                        wire.Save();
                        pbCount.Value++;
                        DrawWirePoint(wirePoints, "CHANGE");
                        double hdwid;
                        _dics = ConstructDics(tunnel, out hdwid);
                        UpdateHdbyPnts(tunnel.TunnelId, wirePoints, _dics, hdwid);
                        pbCount.Value++;
                        lblSuccessed.Text =
                            (Convert.ToInt32(lblSuccessed.Text) + 1).ToString(CultureInfo.InvariantCulture);
                    }
                }
                catch
                {
                    pbCount.Value++;
                    lblError.Text =
                        (Convert.ToInt32(lblError.Text) + 1).ToString(CultureInfo.InvariantCulture);
                    _errorMsg += safeFileName + "\n";
                    btnDetails.Enabled = true;
                }
            }
            Alert.alert("导入完成");
        }
    }
}