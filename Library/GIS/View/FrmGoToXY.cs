using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;

namespace GIS.View
{
    public partial class FrmGoToXY : Form
    {
        public FrmGoToXY()
        {
            InitializeComponent();
        }

        private void tooltemPan_Click(object sender, EventArgs e)
        {
            IPoint pt = getPT();
            if (pt != null)
            {
                Common.DataEditCommon.g_pMyMapCtrl.CenterAt(pt);
                Common.DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewBackground, null, null);
            }
        }

        private void toolItemZoom_Click(object sender, EventArgs e)
        {
            IPoint pt = getPT();
            if (pt != null)
            {
                MyMapHelp.Jump(pt);
            }
        }
        private IPoint getPT()
        {
            IPoint pt = new PointClass();
            string strx = txtX.Text.Trim();
            string stry = txtY.Text.Trim();
            double x = 0;
            double y = 0;
            if (!double.TryParse(strx, out x))
            {
                MessageBox.Show("坐标X非法！");
                return null;
            }
            if (!double.TryParse(stry, out y))
            {
                MessageBox.Show("坐标Y非法！");
                return null;
            }
            pt.X = x;
            pt.Y = y;
            return pt;
        }
    }
}
