using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using GIS.Common;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace GIS.Warning
{
    public partial class FrmWarningPolygon : Form
    {
        public FrmWarningPolygon()
        {
            InitializeComponent();
        }
        IFeatureLayer pFeatureLayer_QYYJY;
        double hongse = 30;//红色预警距离
        double huangse = 100;//黄色预警距离
        private void FrmWarningPolygon_Load(object sender, EventArgs e)
        {
            pFeatureLayer_QYYJY = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_QYYJT) as IFeatureLayer;
            if (pFeatureLayer_QYYJY == null)
            {
                MessageBox.Show("区域预警图层丢失！");
                this.Close();
            }
            ListViewItem view = new ListViewItem("陷落柱");
            view.SubItems.Add(LayerNames.LAYER_ALIAS_MR_XianLuoZhu1);
            listView1.Items.Add(view);
            view = new ListViewItem("揭露断层");
            view.SubItems.Add(LayerNames.LAYER_NAME_MR_JLDC);
            listView1.Items.Add(view);
            view = new ListViewItem("推断断层");
            view.SubItems.Add(LayerNames.LAYER_NAME_MR_TDDC);
            listView1.Items.Add(view);
            view = new ListViewItem("钻孔");
            view.SubItems.Add(LayerNames.LAYER_NAME_MR_ZK);
            listView1.Items.Add(view);
        }
        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnup_Click(object sender, EventArgs e)
        {
            //设置一个最小值
            progressBarControl1.Properties.Minimum = 0;

            //设置步长，即每次增加的数
            progressBarControl1.Properties.Step = 1;
            //设置进度条的样式
            progressBarControl1.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            progressBarControl1.Position = 0;

            //设置一个最小值
            progressBarControl2.Properties.Minimum = 0;
            //设置一个最大值
            progressBarControl2.Properties.Maximum = listView1.CheckedItems.Count * 2;
            //设置步长，即每次增加的数
            progressBarControl2.Properties.Step = 1;
            //设置进度条的样式
            progressBarControl2.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            progressBarControl2.Position = 0;

            ////设置一个最小值
            //progressBar2.Minimum = 0;
            ////设置一个最大值
            //progressBar2.Maximum = listView1.CheckedItems.Count * 2;
            ////设置步长，即每次增加的数
            //progressBar2.Step = 1;
            //progressBar2.Value = 0;


            IFeatureClass tFeatureClass = pFeatureLayer_QYYJY.FeatureClass;
            DataEditCommon.DeleteFeatureByWhereClause(tFeatureClass, "");
            IGeometryArray pgeoArrayHong = new GeometryArrayClass();
            IGeometryArray pgeoArrayHang = new GeometryArrayClass();
            for (int i = 0; i < listView1.CheckedItems.Count; i++)
            {
                string layer = listView1.CheckedItems[i].SubItems[1].Text.ToString();
                ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, layer);
                IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                if (pFeatureLayer == null)
                    continue;
                IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                //设置一个最大值
                progressBarControl1.Properties.Maximum = pFeatureClass.FeatureCount(null);
                IFeatureCursor pCursor = pFeatureClass.Search(null, false);
                IFeature pFeature = pCursor.NextFeature();

                while (pFeature != null)
                {
                    ITopologicalOperator pTopo = (ITopologicalOperator)pFeature.Shape;
                    IGeometry pGeo = pTopo.Buffer(hongse);
                    pgeoArrayHong.Add(pGeo);
                    IGeometry pGeoH = pTopo.Buffer(huangse);
                    if (pFeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        pTopo = (ITopologicalOperator)pGeoH;
                        pGeoH = pTopo.Difference(pGeo);
                    }
                    pgeoArrayHang.Add(pGeoH);
                    pFeature = pCursor.NextFeature();
                    this.progressBarControl1.PerformStep();
                    Application.DoEvents();
                }
                List<ziduan> list = new List<ziduan>();
                list.Add(new ziduan("dengji", "1"));
                list.Add(new ziduan("layername", layer));
                list.Add(new ziduan("BID", "0"));
                DataEditCommon.CreateFeature(tFeatureClass, pgeoArrayHong, list);
                this.progressBarControl2.PerformStep();
                //progressBar2.Value += 1;
                Application.DoEvents();
                List<ziduan> listH = new List<ziduan>();
                listH.Add(new ziduan("dengji", "2"));
                listH.Add(new ziduan("layername", layer));
                listH.Add(new ziduan("BID", "0"));
                DataEditCommon.CreateFeature(tFeatureClass, pgeoArrayHang, listH);
                this.progressBarControl2.PerformStep();
                //progressBar2.Value += 1;
                Application.DoEvents();
            }
            if (pFeatureLayer_QYYJY.Visible == false)
                pFeatureLayer_QYYJY.Visible = true;
            DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            MessageBox.Show("完成！");
        }
        private void checkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAll.Checked)
            {
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    listView1.Items[i].Checked = true;
                }
            }
            else
            {
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    listView1.Items[i].Checked = false;
                }
            }
        }
    }
}
