using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace LibAbout
{
    public partial class About : Form
    {
        WavePanel umi = new WavePanel();
        String productName;
        String version;

        public About(String productName, String version)
        {
            this.productName = productName;
            this.version = version;
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            //版本号
            lbTitle.Text = productName;
            lbVersion.Text = version;
            //设置窗体能否接收子窗体
            this.IsMdiContainer = true;
            //设置窗体的子窗体
            umi.MdiParent = this;
            //添加窗体
            PanelFather.Controls.Add(umi);
            //设置显示格式
            umi.WindowState = FormWindowState.Maximized;
            umi.Dock = DockStyle.Fill;
            umi.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);
            umi.Show();
            umi.Activate();
        }

        private void _btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", "http://www.syccri.com");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", "http://www.fsccri.com");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", "http://www.ccms.net.cn   ");
        }
    }
}
