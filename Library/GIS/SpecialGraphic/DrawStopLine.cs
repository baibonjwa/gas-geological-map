using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;

namespace GIS.SpecialGraphic
{
    public class DrawStopLine
    {
        public static bool CreateLineFeature(IFeatureLayer featureLayer, IPolyline pStopLine, LibEntity.StopLine stopLineEntity)
        {
            if (pStopLine == null || pStopLine.IsEmpty)
            {
                return false;
            }

            try
            {
                IFeatureClass featureClass = featureLayer.FeatureClass;
                if (featureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    IDataset dataset = (IDataset)featureClass;
                    IWorkspace workspace = dataset.Workspace;
                    IWorkspaceEdit workspaceEdit = workspace as IWorkspaceEdit;
                    workspaceEdit.StartEditing(false);
                    workspaceEdit.StartEditOperation();
                    IFeature feature = featureClass.CreateFeature();

                    Common.DataEditCommon.ZMValue(feature, pStopLine);  //几何图形Z值处理

                    feature.Shape = pStopLine;

                    //编号
                    int iFieldID = feature.Fields.FindField("BID");
                    if (iFieldID > -1)
                        feature.Value[iFieldID] = stopLineEntity.BindingId;

                    //名称
                    iFieldID = feature.Fields.FindField("NAME");
                    if (iFieldID > -1)
                        feature.Value[iFieldID] = stopLineEntity.StopLineName;

                    feature.Store();
                    workspaceEdit.StopEditOperation();
                    workspaceEdit.StopEditing(true);
                    //GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
                    GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.Extent = pStopLine.Envelope;
                    GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.Extent.Expand(1.5, 1.5, true);
                    GIS.Common.DataEditCommon.g_pMyMapCtrl.Map.SelectFeature(featureLayer, feature);
                    GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewAll, null, null);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("创建停采线要素出错:" + ex.Message);
                return false;
            }
        }

        /// <summary>
        ///  删除满足条件的停采线要素
        /// </summary>
        /// <param name="featureLayer">停采线图层</param>
        /// <param name="stopLineID">停采线ID</param>
        /// <returns>成功删除返回true</returns>
        public static bool DeleteLineFeature(IFeatureLayer featureLayer, string stopLineID)
        {
            try
            {
                IFeatureClass featureClass = featureLayer.FeatureClass;
               
                    IDataset dataset = (IDataset)featureClass;
                    IWorkspace workspace = dataset.Workspace;
                    IWorkspaceEdit workspaceEdit = workspace as IWorkspaceEdit;
                    workspaceEdit.StartEditing(false);
                    workspaceEdit.StartEditOperation();
                IQueryFilter queryFilter = new QueryFilterClass();
                queryFilter.WhereClause = string.Format("BID='{0}'", stopLineID);
                //Get table and row
                ITable esriTable = (ITable)featureLayer.FeatureClass;
                esriTable.DeleteSearchedRows(queryFilter);
                workspaceEdit.StopEditOperation();
                workspaceEdit.StopEditing(true);
                GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewAll|esriViewDrawPhase.esriViewGeoSelection, null, null);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("删除停采线要素出错：" + ex.Message);
                return false;
            }
        }
    }
}
