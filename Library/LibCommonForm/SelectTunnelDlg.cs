// ******************************************************************
// 概  述：选择巷道
// 作  者：jhou
// 创建日期：2014/04/16
// 版本号：V1.0
// 版本信息：
// V1.0 新建
// ******************************************************************
using System;
using System.Windows.Forms;
using LibCommon;
using LibEntity;

namespace LibCommonForm
{
    public partial class SelectTunnelDlg : Form
    {

        public Tunnel SelectedTunnel { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public SelectTunnelDlg()
        {
            InitializeComponent();
            selectTunnelUserControl1.LoadData();
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="tunnel"></param>
        public SelectTunnelDlg(Tunnel tunnel)
        {
            InitializeComponent();
            SelectedTunnel = tunnel;
        }

        public SelectTunnelDlg(Workingface workingface)
        {
            InitializeComponent();
            selectTunnelUserControl1.LoadData(workingface);
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            SelectedTunnel = selectTunnelUserControl1.SelectedTunnel;
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Cancel the option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            // 关闭窗口
            this.Close();
        }
    }
}
