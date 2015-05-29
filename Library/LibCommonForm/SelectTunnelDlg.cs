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

        public Tunnel selected_tunnel { get; set; }
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
        /// <params name="tunnel"></params>
        public SelectTunnelDlg(Tunnel tunnel)
        {
            InitializeComponent();
            selected_tunnel = tunnel;
        }

        public SelectTunnelDlg(Workingface workingface)
        {
            InitializeComponent();
            selectTunnelUserControl1.LoadData(workingface);
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            selected_tunnel = selectTunnelUserControl1.selected_tunnel;
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Cancel the option
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            // 关闭窗口
            this.Close();
        }
    }
}
