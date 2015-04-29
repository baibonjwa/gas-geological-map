using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using System.Data;
using LibGeometry;

namespace GIS
{
  public  class GeneralFun
    {
        /// <summary>
        /// 删除feature图元
        /// </summary>
        /// <param name="intObjID"></param>
        /// <param name="strLayerName"></param>
        /// <param name="map"></param>
        public void DelFeature(int intObjID, string strLayerName, AxMapControl map)
        {
            IFeatureLayer pfeaLayer;
            for (int intI = 0; intI < map.LayerCount; intI++)
            {
                try
                {
                    pfeaLayer = map.get_Layer(intI) as IFeatureLayer;
                    if (pfeaLayer != null && pfeaLayer.FeatureClass.AliasName == strLayerName)
                    {

                        //定义一个地物类,把要编辑的图层转化为定义的地物类
                        IFeatureClass fc = pfeaLayer.FeatureClass;
                        //先定义一个编辑的工作空间,然后把转化为数据集,最后转化为编辑工作空间,
                        IWorkspaceEdit w = (fc as IDataset).Workspace as IWorkspaceEdit;
                        //开始事务操作
                        w.StartEditing(false);
                        //开始编辑
                        w.StartEditOperation();
                        IQueryFilter queryFilter = new QueryFilterClass();
                        queryFilter.WhereClause = "OBJECTID=" + intObjID;
                        IFeatureCursor updateCursor = pfeaLayer.FeatureClass.Update(queryFilter, false);
                        IFeature feature = updateCursor.NextFeature();
                        
                        int m = 0;
                        while (feature != null)
                        {
                            m++;
                            updateCursor.DeleteFeature();
                            feature = updateCursor.NextFeature();
                        }
                        //结束编辑
                        w.StopEditOperation();
                        //结束事务操作
                        w.StopEditing(true);
                        break;
                    }
                }
                catch
                {

                }
            }
        }
    }
}
