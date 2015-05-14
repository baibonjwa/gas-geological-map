using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibBusiness;
using LibCommon;
using LibEntity;
using LibBusiness.CommonBLL;
using System.Collections;
using LibDatabase;

namespace _3.GeologyMeasure
{
    public partial class Form_TunnelHCEntering : Form
    {
        #region ******变量声明******;
        TunnelHCEntity tunnelHCEntity = new TunnelHCEntity();
        TunnelHCEntity tmpTunnelHCEntity = new TunnelHCEntity();
        TunnelEntity tunnelEntity = new TunnelEntity();

        ArrayList listTunnelEntity = new ArrayList();
        #endregion ******变量声明******

        public Form_TunnelHCEntering()
        {
            InitializeComponent();

            //窗体属性设置
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_HC_ADD);

            //默认回采未完毕
            rbtnHCN.Checked = true;

            //默认工作制式选择
            if (WorkTimeBLL.getDefaultWorkTime() == Const_MS.WORK_TIME_38)
            {
                rbtn38.Checked = true;
            }
            else
            {
                rbtn46.Checked = true;
            }
            ////返回当前
            //cboWorkTime.Text = WorkTime.returnSysWorkTime(rbtn38.Checked ? Const_MS.WORK_TIME_38 : Const_MS.WORK_TIME_46);
            // 设置班次名称
            setWorkTimeName();
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="tunnelHCEntity">回采巷道实体</param>
        public Form_TunnelHCEntering(TunnelHCEntity tunnelHCEntity)
        {
            this.tunnelHCEntity = tunnelHCEntity;
            this.tmpTunnelHCEntity = tunnelHCEntity;
            this.Text = Const_GM.TUNNEL_JJ_CHANGE;
            InitializeComponent();
            
            LibCommon.FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.TUNNEL_HC_CHANGE);
        }

        private void Form_TunnelHCEntering_Load(object sender, EventArgs e)
        {
            //绑定队别名称
            bindTeamInfo();

            if (this.Text == Const_GM.TUNNEL_HC_CHANGE)
            {
                bindInfo();
            }
            if (rbtnHCN.Checked)
            {
                dtpStopDate.Enabled = false;
            }

            cboTeamName.DropDownStyle = ComboBoxStyle.DropDownList;

            cboWorkTime.DropDownStyle = ComboBoxStyle.DropDownList;

            button_ChooseAdd.Enabled = false;
            //listBox_Browse.Enabled = false;
        }

        /// <summary>
        /// 设置班次名称
        /// </summary>
        private void setWorkTimeName()
        {
            string strWorkTimeName = "";
            string sysDateTime = DateTime.Now.ToString("HH:mm:ss");
            if (this.rbtn38.Checked == true)
            {
                strWorkTimeName = MineDataSimpleBLL.selectWorkTimeNameByWorkTimeGroupIdAndSysTime(1, sysDateTime);
            }
            else
            {
                strWorkTimeName = MineDataSimpleBLL.selectWorkTimeNameByWorkTimeGroupIdAndSysTime(2, sysDateTime);
            }

            if (strWorkTimeName != null && strWorkTimeName != "")
            {
                cboWorkTime.Text = strWorkTimeName;
            }
        }

        /// <summary>
        /// 绑定修改信息
        /// </summary>
        private void bindInfo()
        {
            //主运顺槽
            btnTunnelChoose1.Text = TunnelInfoBLL.selectTunnelInfoByTunnelID(tunnelHCEntity.TunnelID_ZY).TunnelName;
            //辅运顺槽
            btnTunnelChoose2.Text = TunnelInfoBLL.selectTunnelInfoByTunnelID(tunnelHCEntity.TunnelID_FY).TunnelName;
            //开切眼
            btnTunnelChoose3.Text = TunnelInfoBLL.selectTunnelInfoByTunnelID(tunnelHCEntity.TunnelID_KQY).TunnelName;
            //其它巷道
            string[] sArray = new string[10];
            if (tunnelHCEntity.TunnelID != null)
            {
                sArray = tunnelHCEntity.TunnelID.Split(',');
            }
            foreach (string i in sArray)
            {
                if (i != "")
                {
                    int iTunnelID = Convert.ToInt16(i);
                    listBox_Browse.Items.Add(TunnelInfoBLL.selectTunnelInfoByTunnelID(iTunnelID).TunnelName);
                }
            }

            tunnelEntity.TunnelID = tunnelHCEntity.TunnelID_ZY;
            //队别名称
            cboTeamName.Text = TeamBLL.selectTeamInfoByID(tunnelHCEntity.TeamNameID).TeamName;

            //开始日期
            dtpStartDate.Value = tunnelHCEntity.StartDate;
            //是否回采完毕
            if (tunnelHCEntity.IsFinish == 1)
            {
                rbtnHCY.Checked = true;
            }
            else
            {
                rbtnHCN.Checked = true;
            }
            //停工日期
            if (tunnelHCEntity.IsFinish == 1)
            {
                dtpStopDate.Value = tunnelHCEntity.StopDate;
            }
            //工作制式
            if (tunnelHCEntity.WorkStyle == rbtn38.Text)
            {
                rbtn38.Checked = true;
            }
            else
            {
                rbtn46.Checked = true;
            }
            //班次
            cboWorkTime.Text = tunnelHCEntity.WorkTime;
        }

        /// <summary>
        /// 主运顺槽按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTunnelChoose1_Click(object sender, EventArgs e)
        {
            //巷道选择窗体
            TunnelChoose tunnelChoose;
            //第一次选择巷道时给巷道实体赋值，用于下条巷道选择时的控件选择定位
            if (tunnelHCEntity.TunnelID_ZY != 0)
            {
                tunnelEntity.TunnelID = tunnelHCEntity.TunnelID_ZY;
                tunnelEntity = TunnelInfoBLL.selectTunnelInfoByTunnelID(tunnelEntity.TunnelID);
            }
            //第一次选择巷道
            if (tunnelEntity.TunnelID == 0)
            {
                tunnelChoose = new TunnelChoose();
            }
            //非第一次选择巷道
            else
            {
                tunnelChoose = new TunnelChoose(tunnelEntity);
            }
            //巷道选择完毕
            if (DialogResult.OK == tunnelChoose.ShowDialog())
            {
                //巷道选择按钮Text改变
                btnTunnelChoose1.Text = tunnelChoose.returnTunnelInfo().TunnelName;
                //实体赋值
                if (this.Text == Const_GM.TUNNEL_HC_CHANGE)
                {
                    tmpTunnelHCEntity.TunnelID_ZY = tunnelChoose.returnTunnelInfo().TunnelID;
                }
                if (this.Text == Const_GM.TUNNEL_HC_ADD)
                {
                    tunnelHCEntity.TunnelID_ZY = tunnelChoose.returnTunnelInfo().TunnelID;
                }
                //巷道实体赋值，用于下次巷道选择
                tunnelEntity = tunnelChoose.returnTunnelInfo();
            }
        }

        /// <summary>
        /// 辅运顺槽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTunnelChoose2_Click(object sender, EventArgs e)
        {
            //巷道选择窗体
            TunnelChoose tunnelChoose;
            //第一次选择巷道时给巷道实体赋值，用于下条巷道选择时的控件选择定位
            if (tunnelHCEntity.TunnelID_FY != 0)
            {
                tunnelEntity.TunnelID = tunnelHCEntity.TunnelID_FY;
                tunnelEntity = TunnelInfoBLL.selectTunnelInfoByTunnelID(tunnelEntity.TunnelID);
            }
            //第一次选择巷道
            if (tunnelEntity.TunnelID == 0)
            {
                tunnelChoose = new TunnelChoose();
            }
            //非第一次选择巷道
            else
            {
                tunnelChoose = new TunnelChoose(tunnelEntity);
            }
            //巷道选择完毕
            if (DialogResult.OK == tunnelChoose.ShowDialog())
            {
                //巷道选择按钮Text改变
                btnTunnelChoose2.Text = tunnelChoose.returnTunnelInfo().TunnelName;
                //实体赋值
                if (this.Text == Const_GM.TUNNEL_HC_CHANGE)
                {
                    tmpTunnelHCEntity.TunnelID_FY = tunnelChoose.returnTunnelInfo().TunnelID;
                }
                if (this.Text == Const_GM.TUNNEL_HC_ADD)
                {
                    tunnelHCEntity.TunnelID_FY = tunnelChoose.returnTunnelInfo().TunnelID;
                }
                //巷道实体赋值，用于下次巷道选择
                tunnelEntity = tunnelChoose.returnTunnelInfo();
            }
        }

        /// <summary>
        /// 开切眼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTunnelChoose3_Click(object sender, EventArgs e)
        {
            //巷道选择窗体
            TunnelChoose tunnelChoose;
            //第一次选择巷道时给巷道实体赋值，用于下条巷道选择时的控件选择定位
            if (tunnelHCEntity.TunnelID_KQY != 0)
            {
                tunnelEntity.TunnelID = tunnelHCEntity.TunnelID_KQY;
                tunnelEntity = TunnelInfoBLL.selectTunnelInfoByTunnelID(tunnelEntity.TunnelID);
            }
            //第一次选择巷道
            if (tunnelEntity.TunnelID == 0)
            {
                tunnelChoose = new TunnelChoose();
            }
            //非第一次选择巷道
            else
            {
                tunnelChoose = new TunnelChoose(tunnelEntity);
            }
            //巷道选择完毕
            if (DialogResult.OK == tunnelChoose.ShowDialog())
            {
                //巷道选择按钮Text改变
                btnTunnelChoose3.Text = tunnelChoose.returnTunnelInfo().TunnelName;
                //实体赋值
                if (this.Text == Const_GM.TUNNEL_HC_CHANGE)
                {
                    tmpTunnelHCEntity.TunnelID_KQY = tunnelChoose.returnTunnelInfo().TunnelID;
                }
                if (this.Text == Const_GM.TUNNEL_HC_ADD)
                {
                    tunnelHCEntity.TunnelID_KQY = tunnelChoose.returnTunnelInfo().TunnelID;
                }
                //巷道实体赋值，用于下次巷道选择
                tunnelEntity = tunnelChoose.returnTunnelInfo();
            }
        }

        /// <summary>
        /// 回采巷道实体赋值
        /// </summary>
        private void bindTunnelHCEntity()
        {
            //队别
            tunnelHCEntity.TeamNameID = Convert.ToInt32(cboTeamName.SelectedValue);
            //开工日期
            tunnelHCEntity.StartDate = dtpStartDate.Value;
            //是否停工
            if (rbtnHCY.Checked)
            {
                tunnelHCEntity.IsFinish = 1;
            }
            if (rbtnHCN.Checked)
            {
                tunnelHCEntity.IsFinish = 0;
            }
            //停工日期
            if (rbtnHCY.Checked == true)
            {
                tunnelHCEntity.StopDate = dtpStopDate.Value;
            }
            //工作制式
            if (rbtn38.Checked)
            {
                tunnelHCEntity.WorkStyle = rbtn38.Text;
            }
            if (rbtn46.Checked)
            {
                tunnelHCEntity.WorkStyle = rbtn46.Text;
            }
            //班次
            tunnelHCEntity.WorkTime = cboWorkTime.Text;
        }

        /// <summary>
        /// 绑定队别名称
        /// </summary>
        private void bindTeamInfo()
        {
            cboTeamName.Items.Clear();
            DataSet ds = TeamBLL.selectTeamInfo();
            cboTeamName.DataSource = ds.Tables[0];
            cboTeamName.DisplayMember = TeamDbConstNames.TEAM_NAME;
            cboTeamName.ValueMember = TeamDbConstNames.ID;
        }

        /// <summary>
        /// 绑定班次
        /// </summary>
        private void bindWorkTimeFirstTime()
        {
            DataSet dsWorkTime;
            if (rbtn38.Checked)
            {
                dsWorkTime = WorkTimeBLL.returnWorkTime(rbtn38.Text);
            }
            else
            {
                dsWorkTime = WorkTimeBLL.returnWorkTime(rbtn46.Text);
            }
            for (int i = 0; i < dsWorkTime.Tables[0].Rows.Count; i++)
            {
                cboWorkTime.Items.Add(dsWorkTime.Tables[0].Rows[i][WorkTimeDbConstNames.WORK_TIME_NAME].ToString());
            }
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
            bindTunnelHCEntity();

            bool bResult = false;
            //添加
            if (this.Text == Const_GM.TUNNEL_HC_ADD)
            {
                bResult = TunnelHCBLL.insertTunnelHC(tunnelHCEntity);
            }
            //修改
            if (this.Text == Const_GM.TUNNEL_HC_CHANGE)
            {
                if (tmpTunnelHCEntity.TunnelID_ZY != 0)
                {
                    tunnelHCEntity.TunnelID_ZY = tmpTunnelHCEntity.TunnelID_ZY;
                }
                if (tmpTunnelHCEntity.TunnelID_FY != 0)
                {
                    tunnelHCEntity.TunnelID_FY = tmpTunnelHCEntity.TunnelID_FY;
                }
                if (tmpTunnelHCEntity.TunnelID_KQY != 0)
                {
                    tunnelHCEntity.TunnelID_KQY = tmpTunnelHCEntity.TunnelID_KQY;
                }
                if (tmpTunnelHCEntity.TunnelID != null)
                {
                    tunnelHCEntity.TunnelID = tmpTunnelHCEntity.TunnelID;
                }

                bResult = TunnelHCBLL.updateTunnelHC(tunnelHCEntity);
            }
            if (bResult)
            {
                //TODO:成功后事件
            }
            TunnelInfoBLL.setTunnelAsHC(tunnelHCEntity);
            return;
        }

        /// <summary>
        /// 工作制式选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn38_CheckedChanged(object sender, EventArgs e)
        {
            //三八制
            if (rbtn38.Checked)
            {
                cboWorkTime.DataSource = null;
                DataSet dsWorkTime = WorkTimeBLL.returnWorkTime(Const_MS.WORK_TIME_38);
                cboWorkTime.DataSource = dsWorkTime.Tables[0];
                cboWorkTime.DisplayMember = WorkTimeDbConstNames.WORK_TIME_NAME;
                cboWorkTime.ValueMember = WorkTimeDbConstNames.WORK_TIME_ID;
            }
            // 设置班次名称
            setWorkTimeName();
        }

        /// <summary>
        /// 工作制式选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn46_CheckedChanged(object sender, EventArgs e)
        {
            //四六制
            if (rbtn46.Checked)
            {
                cboWorkTime.DataSource = null;
                DataSet dsWorkTime = WorkTimeBLL.returnWorkTime(Const_MS.WORK_TIME_46);
                cboWorkTime.DataSource = dsWorkTime.Tables[0];
                cboWorkTime.DisplayMember = WorkTimeDbConstNames.WORK_TIME_NAME;
                cboWorkTime.ValueMember = WorkTimeDbConstNames.WORK_TIME_ID;
            }
            // 设置班次名称
            setWorkTimeName();
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns>是否验证通过</returns>
        private bool check()
        {
            //巷道选择
            if (tunnelHCEntity.TunnelID_ZY == 0 && tunnelHCEntity.TunnelID_FY == 0 && tunnelHCEntity.TunnelID_KQY == 0)
            {
                Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_GM.TUNNEL + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //主运顺槽选择
            if (tunnelHCEntity.TunnelID_ZY == 0)
            {
                Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_GM.MAIN_TUNNEL + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //辅运顺槽选择
            if (tunnelHCEntity.TunnelID_FY == 0)
            {
                Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_GM.SECOND_TUNNEL + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //开切眼选择
            if (tunnelHCEntity.TunnelID_KQY == 0)
            {
                Alert.alert(Const.MSG_PLEASE_CHOOSE + Const_GM.OOC_TUNNEL + Const.SIGN_EXCLAMATION_MARK);
                return false;
            }
            //巷道重复选择验证
            if (tunnelHCEntity.TunnelID_ZY == tunnelHCEntity.TunnelID_FY || tunnelHCEntity.TunnelID_FY == tunnelHCEntity.TunnelID_KQY || tunnelHCEntity.TunnelID_KQY == tunnelHCEntity.TunnelID_ZY)
            {
                Alert.alert(Const_GM.TUNNEL_HC_MSG_TUNNEL_DOUBLE_CHOOSE);
                return false;
            }
            //是否为掘进巷道验证
            //主运顺槽
            if (this.Text == Const_GM.TUNNEL_HC_ADD)
            {
                tunnelEntity.TunnelID = tunnelHCEntity.TunnelID_ZY;
            }
            if (this.Text == Const_GM.TUNNEL_HC_CHANGE)
            {
                tunnelEntity.TunnelID = tmpTunnelHCEntity.TunnelID_ZY;
            }
            if (tmpTunnelHCEntity.TunnelID_ZY != tunnelHCEntity.TunnelID_ZY)
            {
                //检查巷道是否为掘进巷道
                if (TunnelInfoBLL.isTunnelJJ(tunnelEntity))
                {
                    Alert.alert(btnTunnelChoose1.Text + Const_GM.TUNNEL_HC_MSG_TUNNEL_IS_JJ);
                    return false;
                }
                //检查巷道是否为回采巷道
                if (TunnelInfoBLL.isTunnelHC(tunnelEntity))
                {
                    Alert.alert(btnTunnelChoose1.Text + Const_GM.TUNNEL_HC_MSG_TUNNEL_IS_HC);
                    return false;
                }
            }
            //辅运顺槽
            if (this.Text == Const_GM.TUNNEL_HC_ADD)
            {
                tunnelEntity.TunnelID = tunnelHCEntity.TunnelID_FY;
            }
            if (this.Text == Const_GM.TUNNEL_HC_CHANGE)
            {
                tunnelEntity.TunnelID = tmpTunnelHCEntity.TunnelID_FY;
            }
            if (tmpTunnelHCEntity.TunnelID_FY != tunnelHCEntity.TunnelID_FY)
            {
                //检查巷道是否为掘进巷道
                if (TunnelInfoBLL.isTunnelJJ(tunnelEntity))
                {
                    Alert.alert(btnTunnelChoose2.Text + Const_GM.TUNNEL_HC_MSG_TUNNEL_IS_JJ);
                    return false;
                }
                //检查巷道是否为回采巷道
                if (TunnelInfoBLL.isTunnelHC(tunnelEntity))
                {
                    Alert.alert(btnTunnelChoose2.Text + Const_GM.TUNNEL_HC_MSG_TUNNEL_IS_HC);
                    return false;
                }
            }
            //开切眼
            if (this.Text == Const_GM.TUNNEL_HC_ADD)
            {
                tunnelEntity.TunnelID = tunnelHCEntity.TunnelID_KQY;
            }
            if (this.Text == Const_GM.TUNNEL_HC_CHANGE)
            {
                tunnelEntity.TunnelID = tmpTunnelHCEntity.TunnelID_KQY;
            }
            if (tmpTunnelHCEntity.TunnelID_KQY != tunnelHCEntity.TunnelID_KQY)
            {
                //检查巷道是否为掘进巷道
                if (TunnelInfoBLL.isTunnelJJ(tunnelEntity))
                {
                    Alert.alert(btnTunnelChoose3.Text + Const_GM.TUNNEL_HC_MSG_TUNNEL_IS_JJ);
                    return false;
                }
                //检查巷道是否为回采巷道
                if (TunnelInfoBLL.isTunnelHC(tunnelEntity))
                {
                    Alert.alert(btnTunnelChoose3.Text + Const_GM.TUNNEL_HC_MSG_TUNNEL_IS_HC);
                    return false;
                }
            }

            //其他关联巷道
            if (this.Text == Const_GM.TUNNEL_HC_ADD)
            {
                string[] sArray = new string[10];
                if (tunnelHCEntity.TunnelID != null)
                {
                    sArray = tunnelHCEntity.TunnelID.Split(',');
                }
                int iIndex = 0;
                foreach (string i in sArray)
                {
                    if (i != "")
                    {
                        int iTunnelID = Convert.ToInt16(i);
                        tunnelEntity.TunnelID = iTunnelID;

                        //检查巷道是否为掘进巷道
                        if (TunnelInfoBLL.isTunnelJJ(tunnelEntity))
                        {
                            string strText = listBox_Browse.Items[iIndex].ToString();
                            Alert.alert(strText + Const_GM.TUNNEL_HC_MSG_TUNNEL_IS_JJ);

 //                           listBox_Browse.Items.RemoveAt(iIndex);
                            return false;
                        }
                        //检查巷道是否为回采巷道
                        if (TunnelInfoBLL.isTunnelHC(tunnelEntity))
                        {
                            string strText = listBox_Browse.Items[iIndex].ToString();
                            Alert.alert(strText + Const_GM.TUNNEL_HC_MSG_TUNNEL_IS_HC);

  //                          listBox_Browse.Items.RemoveAt(iIndex);
                            return false;
                        }
                    }
                    iIndex += 1;
                }
            }
            if (this.Text == Const_GM.TUNNEL_HC_CHANGE)
            {
                string[] sArray = new string[10];
                if (tmpTunnelHCEntity.TunnelID != null)
                {
                    sArray = tmpTunnelHCEntity.TunnelID.Split(',');
                }
                int iIndex = 0;
                foreach (string i in sArray)
                {
                    if (i != "")
                    {
                        int iTunnelID = Convert.ToInt16(i);
                        tunnelEntity.TunnelID = iTunnelID;

                        //检查巷道是否为掘进巷道
                        if (TunnelInfoBLL.isTunnelJJ(tunnelEntity))
                        {
                            string strText = listBox_Browse.Items[iIndex].ToString();
                            Alert.alert(strText + Const_GM.TUNNEL_HC_MSG_TUNNEL_IS_JJ);

  //                          listBox_Browse.Items.RemoveAt(iIndex);
                            return false;
                        }
                        //检查巷道是否为回采巷道
                        if (TunnelInfoBLL.isTunnelHC(tunnelEntity))
                        {
                            string strText = listBox_Browse.Items[iIndex].ToString();
                            Alert.alert(strText + Const_GM.TUNNEL_HC_MSG_TUNNEL_IS_HC);

  //                          listBox_Browse.Items.RemoveAt(iIndex);
                            return false;
                        }
                    }

                    iIndex += 1;
                }
            }

            //队别为空
            if (!Check.isEmpty(cboTeamName, Const_MS.TEAM_NAME))
            {
                cboTeamName.BackColor = Const.ERROR_FIELD_COLOR;
                return false;
            }
            else
            {
                cboTeamName.BackColor = Const.NO_ERROR_FIELD_COLOR;
            }
            //验证成功
            return true;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            this.Close();
        }

        private void rbtnHCY_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnHCY.Checked)
            {
                dtpStopDate.Enabled = true;
            }
            else
            {
                dtpStopDate.Enabled = false;
            }
        }

        private void radioButton_Add_CheckedChanged(object sender, EventArgs e)
        {
            button_ChooseAdd.Enabled = true;
        }
        /// <summary>
        /// 添加其他巷道按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ChooseAdd_Click(object sender, EventArgs e)
        {
            //巷道选择窗体
            TunnelChoose tunnelChoose;
            //第一次选择巷道时给巷道实体赋值，用于下条巷道选择时的控件选择定位
            if (tunnelHCEntity.TunnelID_ZY != 0)
            {
                tunnelEntity.TunnelID = tunnelHCEntity.TunnelID_ZY;
                tunnelEntity = TunnelInfoBLL.selectTunnelInfoByTunnelID(tunnelEntity.TunnelID);
            }
            //第一次选择巷道
            if (tunnelEntity.TunnelID == 0)
            {
                tunnelChoose = new TunnelChoose();
            }
            //非第一次选择巷道
            else
            {
                tunnelChoose = new TunnelChoose(tunnelEntity);
            }
            //巷道选择完毕
            if (DialogResult.OK == tunnelChoose.ShowDialog())
            {
                //添加信息到listBox
                listBox_Browse.Items.Add(tunnelChoose.returnTunnelInfo().TunnelName);
                
                //实体赋值
                if (this.Text == Const_GM.TUNNEL_HC_CHANGE)
                {
                    if ((tmpTunnelHCEntity.TunnelID != "")&&(tmpTunnelHCEntity.TunnelID!=null))
                    {
                        tmpTunnelHCEntity.TunnelID += ",";
                    }
                    tmpTunnelHCEntity.TunnelID += Convert.ToString(tunnelChoose.returnTunnelInfo().TunnelID);
                }
                if (this.Text == Const_GM.TUNNEL_HC_ADD)
                {
                    if ((tunnelHCEntity.TunnelID!="")&&(tunnelHCEntity.TunnelID!=null))
                    {
                        tunnelHCEntity.TunnelID += ",";
                    }
                    tunnelHCEntity.TunnelID += Convert.ToString(tunnelChoose.returnTunnelInfo().TunnelID);
                }
                //巷道实体赋值，用于下次巷道选择
                tunnelEntity = tunnelChoose.returnTunnelInfo();
            }
        }

        private void listBox_Browse_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int index = listBox_Browse.IndexFromPoint(e.Location);
                if (index >= 0)
                {
                    listBox_Browse.SelectedIndex = index;
                    listBox_Browse.Items.RemoveAt(index);

                    //实体赋值
                    if (this.Text == Const_GM.TUNNEL_HC_CHANGE)
                    {
                        string[] sArray = new string[10];
                        string strTunnelID = "";
                        if ((tmpTunnelHCEntity.TunnelID != "")&&(tmpTunnelHCEntity.TunnelID != null))
                        {
                            sArray = tmpTunnelHCEntity.TunnelID.Split(',');
                        }
                        int iIndex = 0;
                        foreach (string i in sArray)
                        {
                            if (index != iIndex)
                            {
                                if ((strTunnelID != "") && (strTunnelID != null))
                                {
                                    strTunnelID += ",";
                                }
                                strTunnelID += i;

                                //巷道实体赋值，用于下次巷道选择
                                tunnelEntity.TunnelID = Convert.ToInt16(i);
                                tunnelEntity = TunnelInfoBLL.selectTunnelInfoByTunnelID(tunnelEntity.TunnelID);
                            }
                            else
                            {
                                int iTunnelID = Convert.ToInt16(i);
                                TunnelInfoBLL.clearTunnelType(iTunnelID);
                            }
                            iIndex += 1;
                        }

                        tmpTunnelHCEntity.TunnelID = strTunnelID;
                    }
                    if (this.Text == Const_GM.TUNNEL_HC_ADD)
                    {
                        string[] sArray = new string[10];
                        string strTunnelID = "";
                        if ((tunnelHCEntity.TunnelID != "")&&(tunnelHCEntity.TunnelID != null))
                        {
                            sArray = tunnelHCEntity.TunnelID.Split(',');
                        }
                        int iIndex = 0;
                        foreach (string i in sArray)
                        {
                            if (index != iIndex)
                            {
                                if ((strTunnelID != "") && (strTunnelID != null))
                                {
                                    strTunnelID += ",";
                                }
                                strTunnelID += i;

                                //巷道实体赋值，用于下次巷道选择
                                tunnelEntity.TunnelID = Convert.ToInt16(i);
                                tunnelEntity = TunnelInfoBLL.selectTunnelInfoByTunnelID(tunnelEntity.TunnelID);
                            }

                            iIndex += 1;
                        }

                        tunnelHCEntity.TunnelID = strTunnelID;
                    }
                }
            }
        }
    }
}
