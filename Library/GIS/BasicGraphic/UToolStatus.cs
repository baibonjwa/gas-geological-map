using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

namespace GIS.BasicGraphic
{
    public partial class UToolStatus : UserControl
    {
        AxMapControl m_axmap = null;
        double angle = 0;
        public UToolStatus()
        {
            InitializeComponent();
        }

        public AxMapControl AxMap
        {
            get { return m_axmap; }
            set
            {
                m_axmap = value;
                if (m_axmap == null) return;
                m_axmap.OnMouseMove += m_axmap_OnMouseMove;
                m_axmap.OnExtentUpdated += m_axmap_OnExtentUpdated;
                m_axmap.OnMouseDown += m_axmap_OnMouseDown;
                m_axmap.OnViewRefreshed += m_axmap_OnViewRefreshed;
                getsnaptext();
                angle = Math.Round(m_axmap.Rotation, 0);
                txtRoate.Text = Math.Round(m_axmap.Rotation, 0).ToString();
            }
        }

        void m_axmap_OnViewRefreshed(object sender, IMapControlEvents2_OnViewRefreshedEvent e)
        {
            if (Math.Round(m_axmap.Rotation,0) != angle)
            {
                angle = Math.Round(m_axmap.Rotation, 0);
                txtRoate.Text = angle.ToString();
                ILayer player = GIS.Common.DataEditCommon.GetLayerByName(m_axmap.Map, LayerNames.LAYER_ALIAS_MR_HCGZMWSYCLD);
                MyMapHelp.Angle_Symbol(player, -angle);
                player = GIS.Common.DataEditCommon.GetLayerByName(m_axmap.Map, LayerNames.LAYER_ALIAS_MR_WSHLD);
                MyMapHelp.Angle_Symbol(player, -angle);
                player = GIS.Common.DataEditCommon.GetLayerByName(m_axmap.Map, LayerNames.LAYER_ALIAS_MR_WSYLD);
                MyMapHelp.Angle_Symbol(player, -angle);
            }
        }
        #region 地图事件
        void m_axmap_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
            if (e.button == 2)
            {
                //弹出右键菜单
                GIS.Common.DataEditCommon.contextMenu.PopupMenu(e.x, e.y, m_axmap.hWnd);
                return;
            }
        }

        void m_axmap_OnExtentUpdated(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            toolLabelBLC.Text = "1:" + Math.Round(m_axmap.MapScale, 0).ToString();
        }

        void m_axmap_OnMouseMove(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEvent e)
        {
            statusBarXY.Text = "当前坐标：" + string.Format("{0}, {1}  {2}",
            e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"),
                //this.mapControl_OP.MapUnits.ToString().Substring(4));
            MyMapHelp.mapUnit(m_axmap.MapUnits.ToString().Substring(4)));
        }
        #endregion
        #region 比例尺
        private void toolLabelBLC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    int blc = 0;
                    string mblc = toolLabelBLC.Text.Trim().Replace("：",":");
                    if(mblc.Contains(":"))
                        mblc=mblc.Split(':')[1];
                    if (int.TryParse(mblc, out blc))
                    {
                        m_axmap.MapScale = blc;
                        m_axmap.ActiveView.Refresh();
                    }
                }
                catch { }
            }
        }

        private void toolLabelBLC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int blc = 0;
                string mblc = toolLabelBLC.Text.Trim().Replace("：", ":");
                if (mblc.Contains(":"))
                    mblc = mblc.Split(':')[1];
                if (int.TryParse(mblc, out blc))
                {
                    m_axmap.MapScale = blc;
                    m_axmap.ActiveView.Refresh();
                }
            }
            catch { }
        }
        #endregion
        #region 捕捉设置
        private void getsnaptext()
        {
            
            string sfile = Application.StartupPath + @"\snap.text";
            if (System.IO.File.Exists(sfile))
            {
                string stype = System.IO.File.ReadAllText(sfile);
                string ck = stype.Split('$')[0];
                string type = stype.Split('$')[1];
                string[] cktype = type.Split('|');
                DevExpress.XtraEditors.CheckButton ckbtn = new DevExpress.XtraEditors.CheckButton();
                for (int i = 0; i < panel1.Controls.Count; i++)
                {
                    try
                    {
                        ckbtn = (DevExpress.XtraEditors.CheckButton)panel1.Controls[i];
                        for (int j = 0; j < cktype.Length; j++)
                        {
                            if (ckbtn.Tag.ToString().Equals(cktype[j]))
                            {
                                ckbtn.Checked = true;
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                if (ck.Equals("true"))
                    ckBtnOpenBZ.Checked = true;
            }
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Application.StartupPath+@"\gis.xml");
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        string name = dt.Rows[i]["name"].ToString();
                        string value = dt.Rows[i]["value"].ToString();
                        string param=dt.Rows[i]["params"].ToString();
                        if (name.Equals("Scale"))
                        {
                            m_axmap.MapScale = Convert.ToDouble(value);
                            toolLabelBLC.Text = value;
                        }
                        if (name.Equals("ckScale"))
                        {
                            m_axmap.ReferenceScale = Convert.ToDouble(value);
                            if (value.Equals("0"))
                                cbBoxCKBLC.Text = "无";
                            else
                                cbBoxCKBLC.Text = value;
                        }
                        if (name.Equals("Rotation"))
                        {
                            m_axmap.Rotation = Convert.ToDouble(value);
                        }
                        if (name.Equals("extent"))
                        {
                            string[] extent = value.Split(',');
                            ESRI.ArcGIS.Geometry.IEnvelope pEnvelop = new ESRI.ArcGIS.Geometry.EnvelopeClass();
                            pEnvelop.XMax = Convert.ToDouble(extent[0]);
                            pEnvelop.XMin = Convert.ToDouble(extent[1]);
                            pEnvelop.YMax = Convert.ToDouble(extent[2]);
                            pEnvelop.YMin = Convert.ToDouble(extent[3]);
                            m_axmap.Extent = pEnvelop;
                        }
                        if (name.Equals("layer"))
                        {
                            ILayer pLayer = GetLayerByName(m_axmap.Map, value);
                            if (param.ToLower().Equals("true"))
                                pLayer.Visible = true;
                            else
                                pLayer.Visible = false;
                        }
                    }
                    catch { }
                }
            }
            catch { }
            m_axmap.Refresh();
        }
        private ILayer GetLayerByName(IMap pMap, string layerName)
        {
            ILayer pLayer;
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                pLayer = pMap.get_Layer(i);
                if (pLayer.Name == layerName)
                    return pLayer;
                if (pLayer is IGroupLayer)
                {
                    pLayer = GetLayerByName((IGroupLayer)pLayer, layerName);
                    if (pLayer != null)
                        return pLayer;
                }
            }
            return null;
        }
        private ILayer GetLayerByName(IGroupLayer pGroupLayer, string layerName)
        {
            ICompositeLayer comLayer = pGroupLayer as ICompositeLayer;
            ILayer tmpLayer;
            for (int j = 0; j <= comLayer.Count - 1; j++)
            {
                tmpLayer = comLayer.get_Layer(j);
                if (tmpLayer.Name == layerName)
                    return tmpLayer;
                if (tmpLayer is IGroupLayer)
                {
                    tmpLayer = GetLayerByName((IGroupLayer)tmpLayer, layerName);
                    if (tmpLayer != null)
                        return tmpLayer;
                }
            }
            return null;
        }
        private void ckBtnZD_CheckedChanged(object sender, EventArgs e)
        {
            setsnap();
        }
        private void setsnap()
        {
            esriSnappingType snaptype = new esriSnappingType();
            //snaptype=(esriSnappingType)((int)esriSnappingType.esriSnappingTypePoint + (int)esriSnappingType.esriSnappingTypeEdge + (int)esriSnappingType.esriSnappingTypeEndpoint + (int)esriSnappingType.esriSnappingTypeMidpoint + (int)esriSnappingType.esriSnappingTypeIntersection + (int)esriSnappingType.esriSnappingTypeVertex + (int)esriSnappingType.esriSnappingTypeTangent);
            //1点  8边线  2端点   16中点    32交点    4折点   64切线
            if (ckBtnOpenBZ.Checked == false)
                return;
            int type = 0;
            DevExpress.XtraEditors.CheckButton ckbtn = new DevExpress.XtraEditors.CheckButton();
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                try
                {
                    ckbtn = (DevExpress.XtraEditors.CheckButton)panel1.Controls[i];
                    if (ckbtn.Checked)
                    {
                        type += Convert.ToInt32(ckbtn.Tag);
                    }
                }
                catch
                {
                    continue;
                }
            }
            setSnaptext();
            snaptype = (esriSnappingType)type;
            GraphicEdit.SnapSetting.m_hookHelper = new HookHelperClass();
            GraphicEdit.SnapSetting.m_hookHelper.Hook = m_axmap.Object;
            GraphicEdit.SnapSetting.snappingType = snaptype;
            GraphicEdit.SnapSetting.m_bStartSnap = true;
            GraphicEdit.SnapSetting.StartSnappingEnv();
        }
        private void setSnaptext()
        {
            string sfile = Application.StartupPath + @"\snap.text";
            DevExpress.XtraEditors.CheckButton ckbtn = new DevExpress.XtraEditors.CheckButton();
            string stype = "";
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                try
                {
                    ckbtn = (DevExpress.XtraEditors.CheckButton)panel1.Controls[i];
                    if (ckbtn.Checked)
                    {
                        if (stype == "")
                            stype = ckbtn.Tag.ToString();
                        else
                            stype += "|" + ckbtn.Tag.ToString();
                    }
                }
                catch
                {
                    continue;
                }
            }
            stype = ckBtnOpenBZ.Checked.ToString().ToLower()+"$"+stype;
            System.IO.File.WriteAllText(sfile, stype);
        }
        private void ckBtnOpenBZ_CheckedChanged(object sender, EventArgs e)
        {
            if (ckBtnOpenBZ.Checked)
            {
                panel1.Enabled = true;
                ckBtnOpenBZ.Text = "捕捉开启";
                setsnap();
            }
            else
            {
                panel1.Enabled = false;
                ckBtnOpenBZ.Text = "捕捉关闭";
                GraphicEdit.SnapSetting.m_bStartSnap = false;
                Common.DataEditCommon.CloseAllSnapAgent();
                setSnaptext();
            }
        }
        #endregion
        #region 参考比例尺
        private void cbBoxCKBLC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int blc = 0;
                string mblc = cbBoxCKBLC.Text.Trim().Replace("：", ":");
                if (mblc.Contains("无"))
                {
                    m_axmap.Map.ReferenceScale = 0;
                    m_axmap.ActiveView.Refresh();
                }
                else
                {
                    if (mblc.Contains(":"))
                        mblc = mblc.Split(':')[1];
                    if (int.TryParse(mblc, out blc))
                    {
                        m_axmap.Map.ReferenceScale = blc;
                        m_axmap.ActiveView.Refresh();
                    }
                }
            }
            catch { }
        }

        private void cbBoxCKBLC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    int blc = 0;
                    string mblc = cbBoxCKBLC.Text.Trim().Replace("：", ":");
                    if (mblc.Contains("无"))
                    { 
                        m_axmap.Map.ReferenceScale = 0;
                        m_axmap.ActiveView.Refresh();
                    }
                    else
                    {
                        if (mblc.Contains(":"))
                            mblc = mblc.Split(':')[1];
                        if (int.TryParse(mblc, out blc))
                        {
                            m_axmap.Map.ReferenceScale = blc;
                            m_axmap.ActiveView.Refresh();
                        }
                    }
                }
                catch { }
            }
        }
        #endregion

        private void txtRoate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double mangle = 0;
                double.TryParse(txtRoate.Text, out mangle);
                m_axmap.Rotation = mangle;
                m_axmap.ActiveView.Refresh();
            }
        }
    }
}
