using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;
using System.IO;
using ESRI.ArcGIS.DataSourcesFile;

namespace GIS.Common
{
    public class PointsFit2Polyline
    {

        #region 点要素拟合为线

       ///方法1：根据点要素直接生产线要素，《参考绘制巷道》
        /// <summary>
        /// 根据点集坐标绘制线要素
        /// </summary>
        /// <param name="featureLayer"></param>
        /// <param name="lstPoint"></param>
        public static void CreateLine(IFeatureLayer featureLayer, List<IPoint> lstPoint, string ID)
        {
            try
            {
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
                    workspaceEdit.StartEditing(false);
                    workspaceEdit.StartEditOperation();

                    IFeature feature = featureClass.CreateFeature();

                    IGeometry geometry = pPolyline as IGeometry;
                    DrawCommon.HandleZMValue(feature, geometry);//几何图形Z值处理

                    feature.Shape = pPolyline as PolylineClass;
                    int iFieldID = feature.Fields.FindField("BID");
                    feature.Value[iFieldID] = ID.ToString();
                    feature.Store();
                    workspaceEdit.StopEditOperation();
                    workspaceEdit.StopEditing(true);
                    IEnvelope envelop = feature.Shape.Envelope;
                    DataEditCommon.g_pMyMapCtrl.ActiveView.Extent = envelop;
                    GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.Extent.Expand(1.5, 1.5, true);
                    GIS.Common.DataEditCommon.g_pMyMapCtrl.Map.SelectFeature(featureLayer, feature);
                    GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewAll, null, null);

                    
                    //DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
                }
                else
                {
                    MessageBox.Show(@"请选择线图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                return;
            }
        }



        ///方法2：用生成等值线方法进行拟合
        ///步骤：1）点生成txt文件，存于临时文件夹下；
        ///2）点文件生成点要素层→转为Raster→提取等值线；《参考生成等值线方法》  
        #region 复制要素

        /// <summary>
        /// 从本地获得要素类
        /// </summary>
        /// <param name="shapefileDirectory">文件路径（不含文件名）</param>
        /// <param name="shapefileName">文件名</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IFeatureClass GetFeatureClassFromShapefileOnDisk(String shapefileDirectory, String shapefileName)
        {
            DirectoryInfo directoryInfo_check = new DirectoryInfo(shapefileDirectory);
            if (directoryInfo_check.Exists)
            {
                //检查路径
                FileInfo fileInfo_check = new FileInfo(shapefileDirectory + "\\" + shapefileName);
                if (fileInfo_check.Exists)
                {
                    //获取要素类
                    IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactoryClass();
                    IWorkspace workspace = workspaceFactory.OpenFromFile(shapefileDirectory, 0);
                    IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;
                    IFeatureClass featureClass = featureWorkspace.OpenFeatureClass(shapefileName);

                    return featureClass;
                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 复制要素
        /// </summary>
        /// <param name="sourceFeaLayer"></param>
        /// <param name="targetFeaLayer"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool CopyFeature(IFeatureLayer sourceFeaLayer, IFeatureLayer targetFeaLayer, string ID)
        {
            //获得编辑工作空间
            IDataset pSourceDataset = null;
            IWorkspace pSourceWorkspace = null;
            IWorkspaceEdit pSourceWorkspaceEdit = null;
            IFeature pSourceFeature = null;
            IFeatureCursor feaCursor = null;

            try
            {
                pSourceDataset = (IDataset)sourceFeaLayer.FeatureClass;
                pSourceWorkspace = pSourceDataset.Workspace;
                pSourceWorkspaceEdit = pSourceWorkspace as IWorkspaceEdit;

                //遍历源图层找到对应要素

                IQueryFilter queryFilter = new QueryFilterClass();
                queryFilter.WhereClause = "";
                feaCursor = sourceFeaLayer.FeatureClass.Search(queryFilter, true);
                pSourceFeature = feaCursor.NextFeature();
                while (pSourceFeature != null)
                {
                    IGeometry geometry = pSourceFeature.Shape as IGeometry;
                    InsertNewFeature(targetFeaLayer, geometry, ID);//将源图层中要素插入到新图层中并赋值

                    pSourceFeature = feaCursor.NextFeature();
                }

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (feaCursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(feaCursor);  //释放指针
                }
            }
        }


        /// <summary>
        /// 插入新要素
        /// </summary>
        /// <param name="featureLayer">图层</param>
        /// <param name="geom">插入要素几何图形</param>
        /// <param name="ID">要素ID（绑定ID）</param>
        /// <returns></returns>
        public static bool InsertNewFeature(IFeatureLayer featureLayer, IGeometry geom, string ID)
        {
            IWorkspaceEdit workspaceEdit = null;
            IFeatureCursor featureCursor = null;

            try
            {
                IFeatureClass featureClass = featureLayer.FeatureClass;

                IDataset dataset = (IDataset)featureClass;
                IWorkspace workspace = dataset.Workspace;
                workspaceEdit = workspace as IWorkspaceEdit;

                workspaceEdit.StartEditing(true);
                workspaceEdit.StartEditOperation();

                IFeatureBuffer featureBuffer = featureClass.CreateFeatureBuffer();
                DataEditCommon.ZMValue(featureBuffer, geom);    //几何图形Z值处理
                featureBuffer.Shape = geom;

                int iFieldID = featureBuffer.Fields.FindField("ID");
                featureBuffer.Value[iFieldID] = ID.ToString();

                //开始插入要素
                featureCursor = featureClass.Insert(true);
                object featureOID = featureCursor.InsertFeature(featureBuffer);

                //保存要素
                featureCursor.Flush();

                IFeature feature = featureCursor.NextFeature();

                workspaceEdit.StopEditOperation();
                workspaceEdit.StopEditing(true);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                workspaceEdit.AbortEditOperation();
                workspaceEdit.StopEditing(false);

                return false;
            }
            finally
            {
                if (featureCursor != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(featureCursor);  //释放指针
                }
            }
        }

        #endregion
        #endregion
    }
}
