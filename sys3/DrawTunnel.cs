// ******************************************************************
// 概  述：绘制巷道
// 作成者：伍鑫
// 作成日：2013/12/26
// 版本号：1.0
// ******************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibBusiness;
using LibEntity;

namespace _3.GeologyMeasure
{
    public partial class DrawTunnel : Form
    {
        /** 存储导线点实体的数组 **/
        private WirePointInfoEntity[] _wirePointInfoEntityArr;

        public WirePointInfoEntity[] WirePointInfoEntityArr
        {
            get { return _wirePointInfoEntityArr; }
            set { _wirePointInfoEntityArr = value; }
        }

        /** 巷道实体 **/
        private TunnelEntity _tunnelEntity;

        public TunnelEntity TunnelEntity
        {
            get { return _tunnelEntity; }
            set { _tunnelEntity = value; }
        }

        public DrawTunnel()
        {
            InitializeComponent();
            // 加载矿井信息
            loadMineName();
        }

        /// <summary>
        /// 加载矿井信息
        /// </summary>
        private void loadMineName()
        {
            //// 获取矿井信息
            //DataSet ds = TunnelInfoBLL.cboAddMineName();
            //// 检索件数
            //int iSelCnt = ds.Tables[0].Rows.Count;
            //// 检索件数 > 0 的场合
            //if (iSelCnt > 0)
            //{
            //    // 绑定矿井信息
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        this.lstMineName.Items.Add(dr["MINE_NAME"].ToString());
            //    }
            //}
        }

        /// <summary>
        /// 矿井选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstMineName_MouseUp(object sender, MouseEventArgs e)
        {
            this.lstHorizontalName.Items.Clear();
            this.lstMiningAreaName.Items.Clear();
            this.lstWorkingFaceName.Items.Clear();
            this.lstTunnelName.Items.Clear();

            for (int i = 0; i < this.lstMineName.SelectedItems.Count; i++)
            {
                // 创建巷道实体
                TunnelEntity tunnelEntity = new TunnelEntity();
                // 矿井名称
                tunnelEntity.MineName = this.lstMineName.SelectedItems[i].ToString();

                //// 获取水平信息
                //DataSet ds = TunnelInfoBLL.cboAddHorizontal(tunnelEntity);

                //// 检索件数
                //int iSelCnt = ds.Tables[0].Rows.Count;
                //// 检索件数 > 0 的场合
                //if (iSelCnt > 0)
                //{
                //    // 绑定水平信息
                //    foreach (DataRow dr in ds.Tables[0].Rows)
                //    {
                //        this.lstHorizontalName.Items.Add(dr["HORIZONTAL"].ToString());
                //    }
                //}
            }
        }

        /// <summary>
        /// 水平选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstHorizontalName_MouseUp(object sender, MouseEventArgs e)
        {
            //this.lstMiningAreaName.Items.Clear();
            //this.lstWorkingFaceName.Items.Clear();
            //this.lstTunnelName.Items.Clear();

            //for (int i = 0; i < this.lstMineName.SelectedItems.Count; i++)
            //{
            //    for (int n = 0; n < this.lstHorizontalName.SelectedItems.Count; n++)
            //    {
            //        // 创建巷道实体
            //        TunnelEntity tunnelEntity = new TunnelEntity();
            //        // 矿井名称
            //        tunnelEntity.MineName = this.lstMineName.SelectedItems[i].ToString();
            //        // 水平
            //        tunnelEntity.HorizontalName = this.lstHorizontalName.SelectedItems[n].ToString();

            //        // 获取采区信息
            //        DataSet ds = TunnelInfoBLL.cboAddMiningArea(tunnelEntity);

            //        // 检索件数
            //        int iSelCnt = ds.Tables[0].Rows.Count;
            //        // 检索件数 > 0 的场合
            //        if (iSelCnt > 0)
            //        {
            //            // 绑定采区信息
            //            foreach (DataRow dr in ds.Tables[0].Rows)
            //            {
            //                this.lstMiningAreaName.Items.Add(dr["MINING_AREA"].ToString());
            //            }
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 采区选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstMiningAreaName_MouseUp(object sender, MouseEventArgs e)
        {
            //this.lstWorkingFaceName.Items.Clear();
            //this.lstTunnelName.Items.Clear();

            //for (int i = 0; i < this.lstMineName.SelectedItems.Count; i++)
            //{
            //    for (int n = 0; n < this.lstHorizontalName.SelectedItems.Count; n++)
            //    {
            //        for (int x = 0; x < this.lstMiningAreaName.SelectedItems.Count; x++)
            //        {
            //            // 创建巷道实体
            //            TunnelEntity tunnelEntity = new TunnelEntity();
            //            // 矿井名称
            //            tunnelEntity.MineName = this.lstMineName.SelectedItems[i].ToString();
            //            // 水平
            //            tunnelEntity.HorizontalName = this.lstHorizontalName.SelectedItems[n].ToString();
            //            // 采区
            //            tunnelEntity.MiningAreaName = this.lstMiningAreaName.SelectedItems[x].ToString();

            //            // 获取工作面信息
            //            DataSet ds = TunnelInfoBLL.cboAddWorkingFace(tunnelEntity);

            //            // 检索件数
            //            int iSelCnt = ds.Tables[0].Rows.Count;
            //            // 检索件数 > 0 的场合
            //            if (iSelCnt > 0)
            //            {
            //                // 绑定工作面信息
            //                foreach (DataRow dr in ds.Tables[0].Rows)
            //                {
            //                    this.lstWorkingFaceName.Items.Add(dr["WORKING_FACE"].ToString());
            //                }
            //            }
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 工作面选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstWorkingFaceName_MouseUp(object sender, MouseEventArgs e)
        {
            //this.lstTunnelName.Items.Clear();

            //for (int i = 0; i < this.lstMineName.SelectedItems.Count; i++)
            //{
            //    for (int n = 0; n < this.lstHorizontalName.SelectedItems.Count; n++)
            //    {
            //        for (int x = 0; x < this.lstMiningAreaName.SelectedItems.Count; x++)
            //        {
            //            for (int y = 0; y < this.lstWorkingFaceName.SelectedItems.Count; y++)
            //            {
            //                // 创建巷道实体
            //                TunnelEntity tunnelEntity = new TunnelEntity();
            //                // 矿井名称
            //                tunnelEntity.MineName = this.lstMineName.SelectedItems[i].ToString();
            //                // 水平
            //                tunnelEntity.HorizontalName = this.lstHorizontalName.SelectedItems[n].ToString();
            //                // 采区
            //                tunnelEntity.MiningAreaName = this.lstMiningAreaName.SelectedItems[x].ToString();
            //                // 工作面
            //                tunnelEntity.WorkingFaceName = this.lstWorkingFaceName.SelectedItems[y].ToString();

            //                // 获取巷道名称信息
            //                DataSet ds = TunnelInfoBLL.cboAddTunnelName(tunnelEntity);

            //                // 检索件数
            //                int iSelCnt = ds.Tables[0].Rows.Count;
            //                // 检索件数 > 0 的场合
            //                if (iSelCnt > 0)
            //                {
            //                    // 绑定巷道名称信息
            //                    foreach (DataRow dr in ds.Tables[0].Rows)
            //                    {
            //                        this.lstTunnelName.Items.Add(dr["TUNNEL_NAME"].ToString());
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 巷道选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstTunnelName_MouseUp(object sender, MouseEventArgs e)
        {
            //if (this.lstTunnelName.SelectedItems.Count > 0)
            //{
            //    // 创建巷道实体
            //    TunnelEntity tunnelEntity = new TunnelEntity();
            //    // 矿井名称
            //    tunnelEntity.MineName = this.lstMineName.Text;
            //    // 水平
            //    tunnelEntity.HorizontalName = this.lstHorizontalName.Text;
            //    // 采区
            //    tunnelEntity.MiningAreaName = this.lstMiningAreaName.Text;
            //    // 工作面
            //    tunnelEntity.WorkingFaceName = this.lstWorkingFaceName.Text;
            //    // 巷道名称
            //    tunnelEntity.TunnelName = this.lstTunnelName.Text;

            //    DataSet ds = TunnelInfoBLL.selectTunnelInfo(tunnelEntity);

            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        // 获取巷道ID
            //        int iTunnelId = Convert.ToInt32(ds.Tables[0].Rows[0]["OBJECTID"].ToString());

            //        // 获取巷道实体
            //        _tunnelEntity = new TunnelEntity();
            //        _tunnelEntity = TunnelInfoBLL.GetTunnelEntityByTunnelID(iTunnelId);

            //        //////////////////////////////////////////////////////////////////////////
            //        MessageBox.Show(iTunnelId.ToString());
            //        //////////////////////////////////////////////////////////////////////////

            //        //// 获取导线点信息
            //        //getWirePointInfo(iTunnelId);
            //    }
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }

        ///// <summary>
        ///// 获取导线点信息
        ///// </summary>
        ///// <param name="iTunnelId">巷道ID</param>
        //private void getWirePointInfo(int iTunnelId)
        //{
        //    // 获取该巷道ID下的所有导线点ID
        //    string[] pointIdArr = WireInfoBLL.wirePointID(iTunnelId);

        //    this._wirePointInfoEntityArr = new WirePointInfoEntity[pointIdArr.Length];

        //    // 循环所有导线点ID
        //    for (int i = 0; i < pointIdArr.Length; i++)
        //    {
        //        // 通过导线点ID获取导线点信息
        //        DataSet dsWirePointInfo = WirePointBLL.selectWirePointInfoByWirePointId(pointIdArr[i].ToString());
        //        // 检索件数
        //        int selCnt = dsWirePointInfo.Tables[0].Rows.Count;
        //        // 检索件数 > 0的场合
        //        if (selCnt > 0)
        //        {
        //            // 将查询出来的导线点信息存放到导线点实体中
        //            for (int n = 0; n < selCnt; n++)
        //            {
        //                WirePointInfoEntity wirePointInfoEntity = new WirePointInfoEntity();
        //                wirePointInfoEntity.WirePointID = dsWirePointInfo.Tables[0].Rows[n]["WIRE_POINT_ID"].ToString();
        //                wirePointInfoEntity.CoordinateX = Convert.ToInt32(dsWirePointInfo.Tables[0].Rows[n]["COORDINATE_X"]);
        //                wirePointInfoEntity.CoordinateY = Convert.ToInt32(dsWirePointInfo.Tables[0].Rows[n]["COORDINATE_Y"]);
        //                wirePointInfoEntity.CoordinateZ = Convert.ToInt32(dsWirePointInfo.Tables[0].Rows[n]["COORDINATE_Z"]);
        //                wirePointInfoEntity.LeftDis = Convert.ToInt32(dsWirePointInfo.Tables[0].Rows[n]["DISTANCE_FROM_THE_LEFT"]);
        //                wirePointInfoEntity.RightDis = Convert.ToInt32(dsWirePointInfo.Tables[0].Rows[n]["DISTANCE_FROM_THE_RIGHT"]);

        //                this._wirePointInfoEntityArr[n] = wirePointInfoEntity;

        //                //////////////////////////////////////////////////////////////////////////
        //                MessageBox.Show(wirePointInfoEntity.WirePointID);
        //                //////////////////////////////////////////////////////////////////////////
        //            }
        //        }

        //    }
        //}

    }

}
