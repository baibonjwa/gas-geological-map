using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using ESRI.ArcGIS.Geometry;

namespace GIS.Common
{
    public class ClsOpenClass
    {
        public static void OpenFeatureClass(AxMapControl MapControl,
            IFeatureClassName pFcName, ListView listview1)
        {
            try
            {
                MapControl.Map.ClearLayers();
                MapControl.SpatialReference = null;
                IName pName = pFcName as IName;
                IFeatureClass pFc = pName.Open() as IFeatureClass;

                listview1.Items.Clear();
                listview1.Columns.Clear();
                LoadListView(pFc, listview1);

                IFeatureCursor pCursor = pFc.Search(null, false);
                IFeature pfea = pCursor.NextFeature();
                int j = 0;
                while (pfea != null)
                {
                    ListViewItem lv = new ListViewItem();

                    for (int i = 0; i < pfea.Fields.FieldCount; i++)
                    {
                        string sFieldName = pfea.Fields.get_Field(i).Name;
                        lv.SubItems.Add(FeatureHelper.GetFeatureValue(pfea, sFieldName).ToString());
                    }

                    lv.Tag = pfea;
                    if (j % 2 == 0)
                    {
                        lv.BackColor = System.Drawing.Color.GreenYellow;
                    }
                    listview1.Items.Add(lv);
                    pfea = pCursor.NextFeature();
                    j++;
                }
                LSGISHelper.OtherHelper.ReleaseObject(pCursor);
                //最后加载图形数据


                if (pFcName.FeatureType == esriFeatureType.esriFTRasterCatalogItem)
                {
                    ESRI.ArcGIS.Carto.IGdbRasterCatalogLayer pGdbRCLayer = new ESRI.ArcGIS.Carto.GdbRasterCatalogLayerClass();
                    pGdbRCLayer.Setup(pFc as ITable);
                    MapControl.Map.AddLayer(pGdbRCLayer as ILayer);
                }
                else if ((pFcName.FeatureType == esriFeatureType.esriFTSimple) ||
                     (pFcName.FeatureType == esriFeatureType.esriFTComplexEdge) ||
                    (pFcName.FeatureType == esriFeatureType.esriFTComplexJunction) ||
                    (pFcName.FeatureType == esriFeatureType.esriFTSimpleEdge) ||
                     (pFcName.FeatureType == esriFeatureType.esriFTSimpleJunction))
                {

                    IFeatureLayer pLayer = new FeatureLayerClass();
                    pLayer.FeatureClass = pFc;
                    pLayer.Name = (pFc as IDataset).Name;
                    MapControl.Map.AddLayer(pLayer as ILayer);
                }
                else if (pFcName.FeatureType == esriFeatureType.esriFTAnnotation)
                {
                    ILayer pLayer = OpenAnnotationLayer(pFc);
                    pLayer.Name = (pFc as IDataset).Name;
                    MapControl.Map.AddLayer(pLayer as ILayer);
                }

                MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
            catch (Exception ex)
            { }
        }
        public static void OpenRasterDataset(AxMapControl MapControl,
            IRasterDatasetName pRdName, ListView listview1)
        {
            MapControl.ClearLayers();
            MapControl.SpatialReference = null;
            listview1.Items.Clear();
            listview1.Columns.Clear();
            IDatasetName pDsName = pRdName as IDatasetName;
            string sName = pDsName.Name;

            IName pName = pRdName as IName;

            IRasterDataset pRds = pName.Open() as IRasterDataset;
            IRasterLayer pRL = new RasterLayerClass();
            pRL.CreateFromDataset(pRds);
            pRL.Name = sName;
            MapControl.AddLayer(pRL as ILayer);
            MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

        }

        public static void OpenMosaicDataset(AxMapControl MapControl,
           IMosaicDatasetName pMdName, ListView listview1)
        {
            MapControl.ClearLayers();
            MapControl.SpatialReference = null;
            listview1.Items.Clear();
            listview1.Columns.Clear();
            IDatasetName pDsName = pMdName as IDatasetName;
            string sName = pDsName.Name;

            IName pName = pMdName as IName;

            IMosaicDataset pMds = pName.Open() as IMosaicDataset;
            IFeatureClass pFc = pMds.Catalog;
            listview1.Items.Clear();
            listview1.Columns.Clear();
            LoadListView(pFc, listview1);

            IFeatureCursor pCursor = pFc.Search(null, false);
            IFeature pfea = pCursor.NextFeature();
            int j = 0;
            while (pfea != null)
            {
                ListViewItem lv = new ListViewItem();

                for (int i = 0; i < pfea.Fields.FieldCount; i++)
                {
                    string sFieldName = pfea.Fields.get_Field(i).Name;
                    lv.SubItems.Add(FeatureHelper.GetFeatureValue(pfea, sFieldName).ToString());
                }

                lv.Tag = pfea;
                if (j % 2 == 0)
                {
                    lv.BackColor = System.Drawing.Color.GreenYellow;
                }
                listview1.Items.Add(lv);
                pfea = pCursor.NextFeature();
                j++;
            }
            LSGISHelper.OtherHelper.ReleaseObject(pCursor);
            IMosaicLayer pML = new MosaicLayerClass();
            pML.CreateFromMosaicDataset(pMds);

            MapControl.AddLayer(pML as ILayer);
            MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

        }

        public static void OpenTable(AxMapControl MapControl,
            ITableName pTName, ListView listview1)
        {
            try
            {
                MapControl.Map.ClearLayers();
                MapControl.SpatialReference = null;
                MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewAll, null, null);
                IName pName = pTName as IName;
                ITable pFc = pName.Open() as ITable;

                listview1.Items.Clear();
                listview1.Columns.Clear();
                LoadListView(pFc, listview1);

                ICursor pCursor = pFc.Search(null, false);
                IRow pfea = pCursor.NextRow();
                int j = 0;
                while (pfea != null)
                {
                    ListViewItem lv = new ListViewItem();

                    for (int i = 0; i < pfea.Fields.FieldCount; i++)
                    {
                        string sFieldName = pfea.Fields.get_Field(i).Name;
                        lv.SubItems.Add(FeatureHelper.GetRowValue(pfea, sFieldName).ToString());
                    }

                    lv.Tag = pfea;
                    if (j % 2 == 0)
                    {
                        lv.BackColor = System.Drawing.Color.GreenYellow;
                    }
                    listview1.Items.Add(lv);
                    pfea = pCursor.NextRow();
                    j++;
                }
                LSGISHelper.OtherHelper.ReleaseObject(pCursor);
            }
            catch { }
        }
        public static void LoadListView(IFeatureClass pFC, ListView listView1)
        {
            try
            {

                listView1.Columns.Clear();
                //添加一个空
                ColumnHeader columnHeader = new ColumnHeader();

                listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                columnHeader
                });
                columnHeader.Text = "";

                for (int i = 0; i < pFC.Fields.FieldCount; i++)
                {
                    ColumnHeader columnHeader1 = new ColumnHeader();

                    listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                columnHeader1
                });
                    IFields pFields = pFC.Fields;

                    IField pField = pFields.get_Field(i);


                    columnHeader1.Text = pField.AliasName;


                }

            }
            catch (Exception ex)
            { }
        }
        public static void LoadListView(ITable pFC, ListView listView1)
        {
            try
            {

                listView1.Columns.Clear();
                //添加一个空
                ColumnHeader columnHeader = new ColumnHeader();

                listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                columnHeader
                });
                columnHeader.Text = "";

                for (int i = 0; i < pFC.Fields.FieldCount; i++)
                {
                    ColumnHeader columnHeader1 = new ColumnHeader();

                    listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                columnHeader1
                });
                    IFields pFields = pFC.Fields;

                    IField pField = pFields.get_Field(i);


                    columnHeader1.Text = pField.AliasName;


                }

            }
            catch (Exception ex)
            { }
        }

        public static ILayer OpenAnnotationLayer(IFeatureClass pfc)
        {
            IFDOGraphicsLayerFactory pfdof = new FDOGraphicsLayerFactoryClass();
            IFeatureDataset pFDS = pfc.FeatureDataset;
            IWorkspace pWS = pFDS.Workspace;
            IFeatureWorkspace pFWS = pWS as IFeatureWorkspace;
            ILayer pLayer = pfdof.OpenGraphicsLayer(pFWS, pFDS, (pfc as IDataset).Name);
            return pLayer;
        }

    }
}
