using System;
using System.Windows.Forms;
using LibEntity;
using LibBusiness;
using LibCommon;

namespace LibCommonForm
{
    public partial class SelectTunnelUserControl : UserControl
    {
        public Tunnel SelectedTunnel { get; set; }

        public SelectTunnelUserControl()
        {
            InitializeComponent();
        }

        public void LoadData(Workingface workingface)
        {
            LoadData();
            lstMineName.SelectedValue = workingface.mining_area.horizontal.mine.id;
            lstHorizontalName.SelectedValue = workingface.mining_area.horizontal.id;
            lstMiningAreaName.SelectedValue = workingface.mining_area.id;
            lstWorkingFaceName.SelectedValue = workingface.id;
        }

        public void LoadData(Tunnel tunnel)
        {
            LoadData();
            lstMineName.SelectedValue = tunnel.workingface.mining_area.horizontal.mine.id;
            lstHorizontalName.SelectedValue = tunnel.workingface.mining_area.horizontal.id;
            lstMiningAreaName.SelectedValue = tunnel.workingface.mining_area.id;
            lstWorkingFaceName.SelectedValue = tunnel.workingface.id;
            lstTunnelName.SelectedValue = tunnel.id;
        }

        public void LoadData()
        {
            DataBindUtil.LoadMineName(lstMineName);
        }

        public void SetButtonEnable(bool enable)
        {
            btnMineName.Enabled = enable;
            btnHorizontalName.Enabled = enable;
            btnMiningAreaName.Enabled = enable;
            btnWorkingFaceName.Enabled = enable;
            btnTunnelName.Enabled = enable;
        }

        /// <summary>
        /// 矿井名称Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMineName_Click(object sender, EventArgs e)
        {
            var commonManagement = new CommonManagement(1, 999);
            if (DialogResult.OK == commonManagement.ShowDialog())
            {
            }
            DataBindUtil.LoadMineName(lstMineName);
        }

        /// <summary>
        /// 水平名称Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHorizontalName_Click(object sender, EventArgs e)
        {
            if (lstMineName.SelectedItems.Count > 0)
            {
                var commonManagement = new CommonManagement(2, Convert.ToInt32(lstMineName.SelectedValue));
                if (DialogResult.OK == commonManagement.ShowDialog())
                {

                }
                if (lstMineName.SelectedItems.Count <= 0) return;
                var mine = (Mine)lstMineName.SelectedItem;
                DataBindUtil.LoadHorizontalName(lstHorizontalName, mine.id);
            }
            else
            {
                Alert.AlertMsg("请先选择所在矿井名称！");
            }
        }

        /// <summary>
        /// 采区名称Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMiningAreaName_Click(object sender, EventArgs e)
        {
            if (lstHorizontalName.SelectedItems.Count > 0)
            {
                var commonManagement = new CommonManagement(3, Convert.ToInt32(lstHorizontalName.SelectedValue));
                if (DialogResult.OK == commonManagement.ShowDialog())
                {

                }
                if (lstHorizontalName.SelectedItems.Count <= 0) return;
                var horizontal = (Horizontal)lstHorizontalName.SelectedItem;
                DataBindUtil.LoadMiningAreaName(lstMiningAreaName, horizontal.id);
            }
            else
            {
                Alert.AlertMsg("请先选择所在水平名称！");
            }
        }

        /// <summary>
        /// 工作面名称Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWorkingFaceName_Click(object sender, EventArgs e)
        {
            if (lstMiningAreaName.SelectedItems.Count > 0)
            {
                var commonManagement = new CommonManagement(4, Convert.ToInt32(lstMiningAreaName.SelectedValue));
                if (DialogResult.OK == commonManagement.ShowDialog())
                {

                }
                if (lstMiningAreaName.SelectedItems.Count <= 0) return;
                var miningArea = (MiningArea)lstMiningAreaName.SelectedItem;
                DataBindUtil.LoadWorkingFaceName(lstWorkingFaceName, miningArea.id);
            }
            else
            {
                Alert.AlertMsg("请先选择所在采区名称！");
            }
        }

        /// <summary>
        /// 巷道名称Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTunnelName_Click(object sender, EventArgs e)
        {
            if (SelectedTunnel != null)
            {
                var tunnelInfoEntering = new TunnelInfoEntering(SelectedTunnel);
                if (DialogResult.OK == tunnelInfoEntering.ShowDialog())
                {
                    LoadData(SelectedTunnel);
                }
            }
            else
            {
                var tunnelInfoEntering = new TunnelInfoEntering();
                if (DialogResult.OK == tunnelInfoEntering.ShowDialog())
                {
                    LoadData();
                }
            }
        }

        private void lstMineName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstMineName.SelectedItems.Count <= 0) return;
            var mine = (Mine)lstMineName.SelectedItem;
            DataBindUtil.LoadHorizontalName(lstHorizontalName, mine.id);
        }

        private void lstHorizontalName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstHorizontalName.SelectedItems.Count <= 0) return;
            var horizontal = (Horizontal)lstHorizontalName.SelectedItem;
            DataBindUtil.LoadMiningAreaName(lstMiningAreaName, horizontal.id);
        }

        private void lstMiningAreaName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstMiningAreaName.SelectedItems.Count <= 0) return;
            var miningArea = (MiningArea)lstMiningAreaName.SelectedItem;
            DataBindUtil.LoadWorkingFaceName(lstWorkingFaceName, miningArea.id);
        }

        private void lstWorkingFaceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstWorkingFaceName.SelectedItems.Count <= 0) return;
            var workingFace = (Workingface)lstWorkingFaceName.SelectedItem;
            DataBindUtil.LoadTunnelName(lstTunnelName, workingFace.id);
        }

        private void lstTunnelName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTunnelName.SelectedItems.Count <= 0)
                SelectedTunnel = null;
            else
                SelectedTunnel = (Tunnel)lstTunnelName.SelectedItem;
        }
    }
}
