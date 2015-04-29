using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;


namespace GIS.SpecialGraphic
{
    public class DrawJJLine
    {
        /// <summary>
        /// 求导线（pRefPoint到pEndPoint）上的掘进点
        /// </summary>
        /// <param name="pRefPoint">参考导线点</param>
        /// <param name="pEndPoint">导线的终点</param>
        /// <param name="dDistance">距离参考导线点的距离</param>
        /// <returns>距参考导线点一定距离的点</returns>
        public static IPoint GetJJPoint(IPoint pRefPoint, IPoint pEndPoint, double dDistance)
        {
            if (pRefPoint.Z.Equals(Double.NaN))
            {
                pRefPoint.Z = 0;
            }

            if (pEndPoint.Z.Equals(Double.NaN))
            {
                pEndPoint.Z = 0;
            }

            IVector3D vector3D = new Vector3DClass();
            vector3D.ConstructDifference(pEndPoint, pRefPoint); //从参考点到导线终点的向量
            vector3D.Normalize();  //向量单位化

            IPoint pJjPoint = new PointClass();
            pJjPoint.X = pRefPoint.X + dDistance * vector3D.XComponent;
            pJjPoint.Y = pRefPoint.Y + dDistance * vector3D.YComponent;
            pJjPoint.Z = pRefPoint.Z + dDistance * vector3D.ZComponent;

            return pJjPoint;
        }

        /// <summary>
        /// 在掘进进尺图层创建掘进进尺线要素
        /// </summary>
        /// <param name="featureLayer">掘进进尺图层</param>
        /// <param name="pJjPolyline">掘进进尺线</param>
        /// <param name="tunnelID">对应的巷道ID</param>
        /// <returns>成功返回true</returns>
        public static bool CreateLineFeature(IFeatureLayer featureLayer, IPolyline pJjPolyline, string bindingID,  double distance)
        {
            if (pJjPolyline == null || pJjPolyline.IsEmpty)
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
                    workspaceEdit.StartEditOperation();
                    IFeature feature = featureClass.CreateFeature();

                    Common.DataEditCommon.ZMValue(feature, pJjPolyline);  //几何图形Z值处理

                    feature.Shape = pJjPolyline;

                    //绑定编号
                    int iFieldID = feature.Fields.FindField("ID");
                    if (iFieldID > -1)
                        feature.Value[iFieldID] = bindingID;

                    //掘进距离
                    iFieldID = feature.Fields.FindField("Distance");
                    if (iFieldID > -1)
                        feature.Value[iFieldID] = distance;

                    feature.Store();
                    workspaceEdit.StopEditOperation();

                    //缩放到新增的线要素，并高亮该要素
                    GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.Extent = pJjPolyline.Envelope;
                    GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.Extent.Expand(1.5, 1.5, false);
                    GIS.Common.DataEditCommon.g_pMyMapCtrl.Map.SelectFeature(featureLayer, feature);
                    GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewAll, null, null);
                    return true;
                }

                return false;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("创建掘进进尺线要素出错:" + ex.Message);
                return false;
            }
        }

        /// <summary>
        ///  删除满足条件的掘进进尺线要素
        /// </summary>
        /// <param name="featureLayer">掘进进尺图层</param>
        /// <param name="tunnelID">巷道ID</param>
        /// <param name="consultWirepointID">参考导线点编号</param>
        /// <param name="distance">掘进距离</param>
        /// <returns>成功删除返回true</returns>
        public static bool DeleteLineFeature(IFeatureLayer featureLayer, string bindingID)
        {
            if (string.IsNullOrEmpty(bindingID))
                return false;

            try
            {
                IQueryFilter queryFilter = new QueryFilterClass();
                queryFilter.WhereClause = string.Format("Tunnel='{0}'", bindingID);
                //Get table and row
                ITable esriTable = (ITable)featureLayer.FeatureClass;
                esriTable.DeleteSearchedRows(queryFilter);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("删除掘进进尺线要素出错：" + ex.Message);
                return false;
            }
        }
    }
}
