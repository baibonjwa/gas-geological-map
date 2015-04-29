using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace LibPanels
{
    public enum MineDataPanelName
    {
        Ventilation,            //通风
        Ventilation_Change,     //通风修改
        CoalExistence,          //煤层赋存
        CoalExistence_Change,   //煤层赋存修改
        GasData,                //瓦斯
        GasData_Change,         //瓦斯修改
        UsualForecast,          //日常预测
        UsualForecast_Change,   //日常预测修改
        Management,             //管理
        Management_Change,      //管理修改
        GeologicStructure,       //地质构造
        GeologicStructure_Change,   //地质构造修改
    }

    public class LibPanels
    {
        [DllImport("kernel32")]
        private static extern bool GetPrivateProfileString(string lpApplicationName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        private const string PANEL_FORM_NAME = "PanelFormName.ini";
        public StringBuilder PanelFormName = new StringBuilder(256);
        public string title;
        public string configPath;

        
        

        private string GetConnectString(MineDataPanelName mdpn)
        {
            try
            {
                title = Enum.GetName(typeof(MineDataPanelName), mdpn);
                configPath = Application.StartupPath + "\\" + PANEL_FORM_NAME;
                if (!File.Exists(configPath))
                {
                    MessageBox.Show("无法找到数配置文件:" + PANEL_FORM_NAME);
                    return String.Empty;
                }
                GetPrivateProfileString(title, "PanelFormName", "NULL", PanelFormName, PanelFormName.Capacity, configPath);
                string panelFormName = PanelFormName.ToString();
                return panelFormName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return String.Empty;
        }
        public string panelFormName = "";
        public LibPanels(MineDataPanelName mdpn)
        {
            panelFormName = GetConnectString(mdpn);
        }

        /// <summary>
        /// 数字转化为汉字
        /// </summary>
        /// <param name="i">1：</param>
        /// <returns></returns>
        public static string DataChangeYesNo(object i)
        {
            int tmp = 0;
            if (int.TryParse(i.ToString(), out tmp))
            {
                if (tmp == 1)
                {
                    return "是";
                }
                else
                {
                    return "否";
                }
            }
            else
            {
                return "否";
            }
        }

        public static bool checkCoordinate(object x, object y, object z)
        {
            double tmpX = 0;
            double tmpY = 0;
            double tmpZ = 0;
            double.TryParse(x.ToString(), out tmpX);
            double.TryParse(y.ToString(), out tmpY);
            double.TryParse(z.ToString(), out tmpZ);
            if(tmpX==0&&tmpY==0&&tmpZ==0)
            {
                return false;
            }
            return true;
        }
    }
}
