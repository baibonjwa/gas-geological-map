using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace LibLoginForm
{
    class SkinReader
    {
        public string ReadCurSkin()
        {
            string ret = "MP10";

            try
            {
                string path = Application.StartupPath + "\\" + "_CurSkin.ini";

                StreamReader reader = new StreamReader(path, Encoding.Default);
                ret = reader.ReadLine();
                reader.Close();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ret;
        }
    }
}