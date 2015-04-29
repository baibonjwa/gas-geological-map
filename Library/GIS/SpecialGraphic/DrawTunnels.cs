using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;
using GIS.Common;
using LibEntity;

namespace GIS.SpecialGraphic
{
    /// <summary>
    /// 根据导线点绘制导线点和巷道
    /// </summary>
    [Guid("ad63d139-288c-48b5-9637-c5b87e1f8df1")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.SpecialGraphic.DrawTunnels")]
    public class DrawTunnels
    {
        /// <summary>
        /// 根据点坐标绘制点要素
        /// </summary>
        /// <param name="featureLayer"></param>
        /// <param name="point"></param>
        /// <param name="id"></param>
        /// <param name="wirePoint"></param>
        public void CreatePoint(IFeatureLayer featureLayer, IPoint point, string id, WirePoint wirePoint)
        {
            try
            {
                IFeatureClass featureClass = featureLayer.FeatureClass;
                IGeometry geometry = point;

                if (featureClass.ShapeType == esriGeometryType.esriGeometryPoint)
                {
                    IDataset dataset = (IDataset)featureClass;
                    IWorkspace workspace = dataset.Workspace;
                    IWorkspaceEdit workspaceEdit = workspace as IWorkspaceEdit;
                    DataEditCommon.CheckEditState();
                    workspaceEdit.StartEditOperation();
                    IFeature feature = featureClass.CreateFeature();

                    DrawCommon.HandleZMValue(feature, geometry);//几何图形Z值处理

                    feature.Shape = point;
                    feature.Value[feature.Fields.FindField(GIS_Const.FIELD_BID)] = id;
                    feature.Value[feature.Fields.FindField(GIS_Const.FIELD_HDID)] = wirePoint.Wire.Tunnel.TunnelId;
                    feature.Value[feature.Fields.FindField(GIS_Const.FIELD_NAME)] = wirePoint.WirePointName;
                    feature.Value[feature.Fields.FindField(GIS_Const.FIELD_ID)] = wirePoint.WirePointId;
                    feature.Store();
                    workspaceEdit.StopEditOperation();

                    IEnvelope envelop = point.Envelope;
                    DataEditCommon.g_pMyMapCtrl.ActiveView.Extent = envelop;
                    DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
                }
                else
                {
                    MessageBox.Show(@"请选择点图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                return;
            }
        }


        /// <summary>
        /// 根据点集坐标绘制线要素
        /// </summary>
        /// <param name="featureLayer"></param>
        /// <param name="lstPoint"></param>
        public void CreateLine(IFeatureLayer featureLayer, List<IPoint> lstPoint, int ID)
        {
            //try
            //{
            IFeatureClass featureClass = featureLayer.FeatureClass;
            if (featureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
            {
                IPointCollection multipoint = new MultipointClass();
                if (lstPoint.Count < 2)
                {
                    MessageBox.Show(@"请选择两个及两个以上点数。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                ISegmentCollection pPath = new PathClass();
                ILine pLine;
                ISegment pSegment;
                object o = Type.Missing;
                for (int i = 0; i < lstPoint.Count - 1; i++)
                {
                    pLine = new LineClass();
                    pLine.PutCoords(lstPoint[i], lstPoint[i + 1]);
                    pSegment = pLine as ISegment;
                    pPath.AddSegment(pSegment, ref o, ref o);
                }
                IGeometryCollection pPolyline = new PolylineClass();
                pPolyline.AddGeometry(pPath as IGeometry, ref o, ref o);

                IDataset dataset = (IDataset)featureClass;
                IWorkspace workspace = dataset.Workspace;
                IWorkspaceEdit workspaceEdit = workspace as IWorkspaceEdit;

                workspaceEdit.StartEditing(true);
                workspaceEdit.StartEditOperation();

                IFeature feature = featureClass.CreateFeature();

                IGeometry geometry = pPolyline as IGeometry;
                DrawCommon.HandleZMValue(feature, geometry);//几何图形Z值处理

                feature.Shape = pPolyline as PolylineClass;
                int iFieldID = feature.Fields.FindField(GIS_Const.FIELD_BID);
                feature.Value[iFieldID] = ID.ToString();
                feature.Store();
                workspaceEdit.StopEditOperation();
                workspaceEdit.StopEditing(false);

                IEnvelope envelop = feature.Shape.Envelope;
                DataEditCommon.g_pMyMapCtrl.ActiveView.Extent = envelop;
                DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
            }
            else
            {
                MessageBox.Show(@"请选择线图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //}
            //catch
            //{
            //    return;
            //}
        }


        /// <summary>
        /// 根据要素ID查找对应要素
        /// </summary>
        /// <param name="feaLayer"></param>
        /// <param name="featureID"></param>
        /// <returns></returns>
        public IFeature FindFeatureByID(IFeatureLayer feaLayer, string featureID)
        {
            try
            {
                //遍历图层找到对应要素
                IFeature pFeature = null;
                IFeatureCursor feaCursor = null;
                feaCursor = feaLayer.FeatureClass.Search(null, true);
                pFeature = feaCursor.NextFeature();
                while (pFeature != null)
                {
                    int iFieldID = pFeature.Fields.FindField("ID");//图层中对应绑定ID字段
                    string sFieldIDValue = pFeature.get_Value(iFieldID).ToString();

                    //若存在该要素，则返回此要素
                    if (sFieldIDValue == featureID)
                    {
                        return pFeature;
                    }

                    pFeature = feaCursor.NextFeature();
                }

                System.Runtime.InteropServices.Marshal.ReleaseComObject(feaCursor);
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 删除指定要素
        /// </summary>
        /// <param name="feaLayer"></param>
        /// <param name="featureID"></param>
        public void DeleteFeature(IFeatureLayer feaLayer, string featureID)
        {
            //方法1：删除要素
            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.WhereClause = "ID" + "='" + featureID + "'";
            //Get table and row
            ITable esriTable = (ITable)feaLayer.FeatureClass;
            esriTable.DeleteSearchedRows(queryFilter);

            //方法2：删除要素
            ////获得编辑工作空间
            //IDataset pDataset = null;
            //IWorkspace pWorkspace = null;
            //IWorkspaceEdit pWorkspaceEdit = null;
            //pDataset = (IDataset)feaLayer.FeatureClass;
            //pWorkspace = pDataset.Workspace;
            //pWorkspaceEdit = pWorkspace as IWorkspaceEdit;

            ////遍历图层找到对应要素
            //IFeature pFeature = null;
            //IFeatureCursor feaCursor = null;
            //IQueryFilter queryFilter = new QueryFilterClass();
            //queryFilter.WhereClause = "ID" + "='" + featureID + "'";
            //feaCursor = feaLayer.FeatureClass.Search(queryFilter, true);
            //pFeature = feaCursor.NextFeature();
            //while (pFeature != null)
            //{
            //    //int iFieldID = pFeature.Fields.FindField("ID");//图层中对应绑定ID字段
            //    //string sFieldIDValue = pFeature.get_Value(iFieldID).ToString();
            //    pFeature.Delete();
            //    ////若存在该要素，则删除此要素
            //    //if (sFieldIDValue == featureID)
            //    //{
            //    //    pWorkspaceEdit.StartEditing(false);
            //    //    pWorkspaceEdit.StartEditOperation();
            //    //    pFeature.Delete();
            //    //    pWorkspaceEdit.StopEditOperation();
            //    //    pWorkspaceEdit.StopEditing(true);

            //    //    break;
            //    //}

            //    pFeature = feaCursor.NextFeature();
            //}

            //System.Runtime.InteropServices.Marshal.ReleaseComObject(feaCursor);           

        }
    }
}
