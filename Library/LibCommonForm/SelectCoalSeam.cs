using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Framework.Config;
using LibBusiness;

namespace LibLoginForm
{
    public partial class SelectCoalSeam : Form
    {
        public Form Form { get; set; }

        public SelectCoalSeam(Form form)
        {
            InitializeComponent();
            Form = form;
        }

        private void SelectCoalSeam_Load(object sender, EventArgs e)
        {
            ConfigHelper.load();
            foreach (CoalSeam t in ConfigHelper.coal_seams)
            {
                cboCoalSeam.Items.Add(t);
            }
            cboCoalSeam.SelectedIndex = 0;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            ConfigHelper.current_seam = (CoalSeam)cboCoalSeam.SelectedItem;
            Hide();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Application.StartupPath + "\\" + "ARConfig.xml");
            XmlElement root = xmldoc.DocumentElement;
            var a = root.SelectNodes("/activerecord/config/add");
            var sqlcons = a[3].Attributes["value"].InnerText.Split(';');
            string str = "";
            for (int i = 0; i < sqlcons.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        str += "data Source=" + ConfigHelper.current_seam.db_name;
                        break;
                    default:
                        str += sqlcons[i];
                        break;
                }
                str += ";";
            }
            a[3].Attributes["value"].InnerText = str;
            xmldoc.Save(Application.StartupPath + "\\" + "ARConfig.xml");


            Thread.CurrentThread.CurrentUICulture =
                new CultureInfo("zh-Hans");
            Thread.CurrentThread.CurrentCulture =
                new CultureInfo("zh-Hans");
            IConfigurationSource config = new XmlConfigurationSource("ARConfig.xml");
            var asm = Assembly.Load("LibEntity");
            ActiveRecordStarter.Initialize(asm, config);
            Form.ShowDialog();
        }
    }
}
