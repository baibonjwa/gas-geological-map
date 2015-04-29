using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;

namespace GIS.LayersManager
{
    /// <summary>
    /// 放大到所选图层
    /// </summary>
    public sealed class LayerVisible :  BaseCommand, ICommandSubType
    {
        private IMapControl3 m_mapControl;
        private long m_subType;

        public override string Caption
        {
            get
            {
                if (m_subType == 1) return "显示图层";
                else return "隐藏图层";
            }
        }
        public override void OnClick()
        {
            ILayer layer = (ILayer)m_mapControl.CustomProperty;
            if (m_subType == 1)
            {
                layer.Visible = true;
            }
            else { 
                layer.Visible = false;
            }
            m_mapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }
        public int GetCount()
        {
            return 2;
        }
        public void SetSubType(int SubType)
        {
            m_subType = SubType;
        }
        public override void OnCreate(object hook)
        {
            m_mapControl = (IMapControl3)hook;
        }
        public override bool Enabled
        {
            get
            {
                ILayer layer = (ILayer)m_mapControl.CustomProperty;
                if (layer is IFeatureLayer||layer is IGroupLayer)
                {
                    if (m_subType == 1) return !layer.Visible;
                    else return layer.Visible;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
