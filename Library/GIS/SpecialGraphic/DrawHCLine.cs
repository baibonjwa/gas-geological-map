using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;

namespace GIS.SpecialGraphic
{
    public class DrawHCLine
    {
        /// <summary>
        /// 创建回采进尺线（根据参考线创建平行线）
        /// </summary>
        /// <param name="featureLayer">回采进尺图层</param>
        /// <param name="pRefPolyline">参考开切眼巷道</param>
        /// <param name="dDistance">回采距离</param>
        /// <returns>成功创建返回True</returns>
        public static bool CreateParaLineFeature(IFeatureLayer featureLayer, IPolyline pRefPolyline,string bindID, double dDistance)
        {
            if (pRefPolyline == null || pRefPolyline.IsEmpty)
            {
                return false;
            }

            try
            {
                IFeatureClass featureClass = featureLayer.FeatureClass;
                IGeometry geometry = pRefPolyline;

                object Missing = Type.Missing;
                IConstructCurve constructCurve = new PolylineClass();
                constructCurve.ConstructOffset(pRefPolyline, dDistance, ref Missing, ref Missing);

                IPolyline hcLine = constructCurve as IPolyline;
                if (featureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    IDataset dataset = (IDataset)featureClass;
                    IWorkspace workspace = dataset.Workspace;
                    IWorkspaceEdit workspaceEdit = workspace as IWorkspaceEdit;
                    workspaceEdit.StartEditOperation();
                    IFeature feature = featureClass.CreateFeature();

                    Common.DataEditCommon.ZMValue(feature, hcLine);  //几何图形Z值处理

                    feature.Shape = hcLine;

                    //绑定唯一编号
                    int iFieldID = feature.Fields.FindField("ID");
                    if (iFieldID > -1)
                        feature.Value[iFieldID] = bindID;

                    //回采距离
                    iFieldID = feature.Fields.FindField("Distance");
                    if (iFieldID > -1)
                        feature.Value[iFieldID] = dDistance;
                   
                    feature.Store();
                    workspaceEdit.StopEditOperation();

                    //缩放到新增的线要素，并高亮该要素
                    GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.Extent = hcLine.Envelope;
                    GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.Extent.Expand(1.5, 1.5, true);
                    GIS.Common.DataEditCommon.g_pMyMapCtrl.Map.SelectFeature(featureLayer, feature);
                    GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewAll, null, null);
                    //GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
                    return true;
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show("请选择点图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///  删除满足条件的回采进尺线要素
        /// </summary>
        /// <param name="featureLayer">回采进尺图层</param>
        /// <param name="tunnelID">bindingID</param>
        /// <returns>成功删除返回true</returns>
        public static bool DeleteHCLineFeature(IFeatureLayer featureLayer, string bindingID)
        {
            try
            {
                IQueryFilter queryFilter = new QueryFilterClass();
                queryFilter.WhereClause = string.Format("ID='{0}'", bindingID);
                
                ITable esriTable = (ITable)featureLayer.FeatureClass;
                esriTable.DeleteSearchedRows(queryFilter);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("删除回采进尺线要素出错：" + ex.Message);
                return false;
            }
        }
    }
}
