using System;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using Microsoft.VisualBasic;
namespace _3.GeologyMeasure
{
    public partial class ImportLRData : Form
    {
        public ImportLRData()
        {
            InitializeComponent();
        }

        private void BTConnect_Click(object sender, EventArgs e)
        {

            if (TBUserName.Text.ToString() != "" && TBPassWord.Text.ToString() != "" && TBDataTableName.Text.ToString() != "")
            {
                string sInstall = ReadRegistry("SOFTWARE\\Microsoft\\Microsoft SQL Server\\MSSQL10.SQLEXPRESS\\Setup");
                System.Diagnostics.Process.Start(@sInstall + "\\100\\Tools\\Binn\\VSShell\\Common7\\IDE\\Ssms.exe");
                this.Close();
            }
            else
            {
                MessageBox.Show("请输入必填项！");
            }
        }

        /// <summary> 
            /// 从注册表中取得指定软件的路径
            /// </summary>
            /// <param name="sKey"></param>
            /// <returns></returns>
        private string ReadRegistry(string p)
        {
            RegistryKey rk = Registry.LocalMachine.OpenSubKey(p);
            if (rk == null) return "";
            return (string)rk.GetValue("SqlProgramDir");
        }

        private void CancleConnect_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
