using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GIS
{
    public partial class FormCaveSizeInput : Form
    {
        public double CaveHeight = 0.0;
        public double CaveWidth = 0.0;

        public FormCaveSizeInput()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                double dHeight = 0;
                double dWidth = 0;

                if (double.TryParse(this.txtGD.Text, out dHeight))
                    CaveHeight = dHeight;
                else
                {
                    MessageBox.Show(@"输入的高度不是有效数值!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (double.TryParse(this.txtKD.Text, out dWidth))
                    CaveWidth = dWidth;
                else
                {
                    MessageBox.Show(@"输入的宽度不是有效数值!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
