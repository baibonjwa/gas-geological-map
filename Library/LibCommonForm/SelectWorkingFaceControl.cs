using System;
using System.Windows.Forms;
using LibBusiness;
using LibCommon;
using LibEntity;

namespace LibCommonForm
{
    public partial class SelectWorkingFaceControl : UserControl
    {

        public WorkingFace SelectedWorkingFace { get; set; }

        public SelectWorkingFaceControl()
        {
            InitializeComponent();
        }

        public SelectWorkingFaceControl(WorkingFace workingFace)
        {
            LoadData(workingFace);
        }

        public SelectWorkingFaceControl(MiningArea miningArea)
        {
            LoadData(miningArea);
        }

        public void LoadData(WorkingFace workingFace)
        {
            LoadData();
            lstMineName.SelectedValue = workingFace.MiningArea.Horizontal.Mine.MineId;
            lstHorizontalName.SelectedValue = workingFace.MiningArea.Horizontal.HorizontalId;
            lstMiningAreaName.SelectedValue = workingFace.MiningArea.MiningAreaId;
            lstWorkingFaceName.SelectedValue = workingFace.WorkingFaceId;
        }

        public void LoadData(MiningArea miningArea)
        {
            LoadData();
            lstMineName.SelectedValue = miningArea.Horizontal.Mine.MineId;
            lstHorizontalName.SelectedValue = miningArea.Horizontal.HorizontalId;
            lstMiningAreaName.SelectedValue = miningArea.MiningAreaId;
        }


        #region 加载矿井信息
        /// <summary>
        /// 加载矿井信息
        /// </summary>
        public void LoadData()
        {
            DataBindUtil.LoadMineName(lstMineName);
        }
        #endregion

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
                DataBindUtil.LoadHorizontalName(lstHorizontalName, mine.MineId);
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
                DataBindUtil.LoadMiningAreaName(lstMiningAreaName, horizontal.HorizontalId);
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
                DataBindUtil.LoadWorkingFaceName(lstWorkingFaceName, miningArea.MiningAreaId);
            }
            else
            {
                Alert.AlertMsg("请先选择所在采区名称！");
            }
        }

        /// <summary>
        /// 工作面过滤设置（规则过滤）
        /// </summary>
        //public void SetFilterOn(WorkingfaceTypeEnum workingfaceType)
        //{
        //    _isFilterOn = true;
        //    this.workingfaceTypes[0] = workingfaceType;
        //}

        //public void SetFilterOn(params WorkingfaceTypeEnum[] workingfaceTypes)
        //{
        //    _isFilterOn = true;
        //    this.workingfaceTypes = workingfaceTypes;
        //}

        private void lstMineName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstMineName.SelectedItems.Count <= 0) return;
            var mine = (Mine)lstMineName.SelectedItem;
            DataBindUtil.LoadHorizontalName(lstHorizontalName, mine.MineId);
        }

        private void lstHorizontalName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstHorizontalName.SelectedItems.Count <= 0) return;
            var horizontal = (Horizontal)lstHorizontalName.SelectedItem;
            DataBindUtil.LoadMiningAreaName(lstMiningAreaName, horizontal.HorizontalId);
        }

        private void lstMiningAreaName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstMiningAreaName.SelectedItems.Count <= 0) return;
            var miningArea = (MiningArea)lstMiningAreaName.SelectedItem;
            DataBindUtil.LoadWorkingFaceName(lstWorkingFaceName, miningArea.MiningAreaId);
        }

        private void lstWorkingFaceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstWorkingFaceName.SelectedItems.Count <= 0)
                SelectedWorkingFace = null;
            else
                SelectedWorkingFace = (WorkingFace)lstWorkingFaceName.SelectedItem;
        }
    }
}
