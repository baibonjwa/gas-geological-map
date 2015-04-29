using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace GIS
{
    public partial class LayersListControl : UserControl
    {
        IMap m_map = null;

        public LayersListControl()
        {
            InitializeComponent();
        }

        public IMap Map
        {
            get { return m_map; }
            set
            {
                m_map = value;
                if (m_map == null) return;
               
                //填充Combox
                PopulateCombo();
            }
        }

        private void cmbLayersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_map == null)
                return;

 
            //遍历所有图层
            IEnumLayer layers = GetAllLayersList();
            if (layers == null)
                return;

            layers.Reset();
            ILayer layer = null;
            while ((layer = layers.Next()) != null)
            {
                //if (layer is IGroupLayer)
                //    continue;
                              
                if (this.cmbLayersList.SelectedItem.ToString() == layer.Name)
                {                    
                    DataEditCommon.g_pLayer = layer;
                }                
            }
        }

        /// <summary>
        /// 获得图层列表
        /// </summary>
        /// <returns></returns>
        private IEnumLayer GetAllLayersList()
        {
            IEnumLayer layers = null;
            if (null == m_map || 0 == m_map.LayerCount)
                return null;

            try
            {
                layers = m_map.get_Layers(null, true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return null;
            }

            return layers;
        }

        /// <summary>
        /// 列出Combox中所有图层和设置当前图层
        /// </summary>
        private void PopulateCombo()
        {
            if (null == m_map)
                return;

            //清空列表
            this.cmbLayersList.Items.Clear();
                         
            //获得所有图层
            IEnumLayer layers = GetAllLayersList();
            if (null != layers)
            {

                //添加图层到列表中
                layers.Reset();
                ILayer layer = null;
                while ((layer = layers.Next()) != null)
                {                    
                    this.cmbLayersList.Items.Add(layer.Name);
                }
            }
            //设置当前选中图层
            this.cmbLayersList.SelectedIndex = 0;
        }            


    }
}
