using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Castle.ActiveRecord;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using GIS.Common;
using LibCommon;
using LibEntity;

namespace GIS.SpecialGraphic
{
    public partial class CollapsePillarsEntering : Form
    {
        private string _errorMsg;


        /// <summary>
        ///     构造方法
        /// </summary>
        public CollapsePillarsEntering()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     构造方法
        /// </summary>
        public CollapsePillarsEntering(IPointCollection pointCollection)
        {
            InitializeComponent();

            dgrdvCoordinate.RowCount = pointCollection.PointCount;
            for (int i = 0; i < pointCollection.PointCount - 1; i++)
            {
                dgrdvCoordinate[0, i].Value = pointCollection.Point[i].X;
                dgrdvCoordinate[1, i].Value = pointCollection.Point[i].Y;
                if (pointCollection.Point[i].Z.ToString(CultureInfo.InvariantCulture) == "非数字" ||
                    pointCollection.Point[i].Z.ToString(CultureInfo.InvariantCulture) == "NaN")
                    dgrdvCoordinate[2, i].Value = 0;
                else
                    dgrdvCoordinate[2, i].Value = pointCollection.Point[i].Z;
            }
        }

        /// <summary>
        ///     构造方法
        /// </summary>
        /// <param name="collapsePillars"></param>
        public CollapsePillarsEntering(CollapsePillars collapsePillars)
        {
            InitializeComponent();
            using (new SessionScope())
            {
                collapsePillars = CollapsePillars.Find(collapsePillars.CollapsePillarsId);
                txtCollapsePillarsName.Text = collapsePillars.CollapsePillarsName;
                if (collapsePillars.Xtype == "1")
                    radioBtnS.Checked = true;
                txtDescribe.Text = collapsePillars.Discribe;
                foreach (var t in collapsePillars.CollapsePillarsPoints)
                {
                    dgrdvCoordinate.Rows.Add(t.CoordinateX,
                        t.CoordinateY,
                        t.CoordinateZ);
                }
            }
        }

        /// <summary>
        ///     取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //关闭窗体
            Close();
        }

        /// <summary>
        ///     提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            CollapsePillars collapsePillars = CollapsePillars.FindOneByCollapsePillarsName(txtCollapsePillarsName.Text);
            if (collapsePillars == null)
            {
                collapsePillars = new CollapsePillars
                {
                    CollapsePillarsName = txtCollapsePillarsName.Text,
                    Discribe = txtDescribe.Text,
                    Xtype = radioBtnX.Checked ? "0" : "1",
                    BindingId = IdGenerator.NewBindingId()
                };
            }
            else
            {
                collapsePillars.CollapsePillarsName = txtCollapsePillarsName.Text;
                collapsePillars.Discribe = txtDescribe.Text;
                collapsePillars.Xtype = radioBtnX.Checked ? "0" : "1";
            }

            //实体赋值
            //去除无用空行
            for (int i = 0; i < dgrdvCoordinate.RowCount - 1; i++)
            {
                if (dgrdvCoordinate.Rows[i].Cells[0].Value == null &&
                    dgrdvCoordinate.Rows[i].Cells[1].Value == null &&
                    dgrdvCoordinate.Rows[i].Cells[2].Value == null)
                {
                    dgrdvCoordinate.Rows.RemoveAt(i);
                }
            }

            var collapsePillarsPoints = new List<CollapsePillarsPoint>();
            //添加关键点
            for (int i = 0; i < dgrdvCoordinate.RowCount - 1; i++)
            {

                var collapsePillarsPoint = new CollapsePillarsPoint
                {
                    CoordinateX = Convert.ToDouble(dgrdvCoordinate[0, i].Value),
                    CoordinateY = Convert.ToDouble(dgrdvCoordinate[1, i].Value),
                    CoordinateZ = Convert.ToDouble(dgrdvCoordinate[2, i].Value),
                    BindingId = IdGenerator.NewBindingId(),
                    CollapsePillars = collapsePillars
                };
                collapsePillarsPoints.Add(collapsePillarsPoint);
            }
            collapsePillars.CollapsePillarsPoints = collapsePillarsPoints;
            collapsePillars.Save();
            ModifyXlz(collapsePillarsPoints, collapsePillars.CollapsePillarsId.ToString());
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        ///     显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgrdvCoordinate_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y, dgrdvCoordinate.RowHeadersWidth - 4, e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgrdvCoordinate.RowHeadersDefaultCellStyle.Font, rectangle,
                dgrdvCoordinate.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dgrdvCoordinate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != dgrdvCoordinate.Rows.Count - 1 && e.ColumnIndex == 3 &&
                Alert.Confirm("确认要删除吗？"))
            {
                if (e.ColumnIndex == 3)
                {
                    dgrdvCoordinate.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                var open = new OpenFileDialog { Filter = @"陷落柱数据(*.txt)|*.txt" };
                if (open.ShowDialog(this) == DialogResult.Cancel)
                    return;
                string filename = open.FileName;
                string[] file = File.ReadAllLines(filename);
                dgrdvCoordinate.RowCount = file.Length;
                if (open.SafeFileName != null) txtCollapsePillarsName.Text = open.SafeFileName.Split('.')[0];
                for (int i = 0; i < file.Length; i++)
                {
                    dgrdvCoordinate[0, i].Value = file[i].Split(',')[0];
                    dgrdvCoordinate[1, i].Value = file[i].Split(',')[1];
                    //dgrdvCoordinate[2, i].Value = file[i].Split(',')[2];
                    dgrdvCoordinate[2, i].Value = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region 根据关键点绘制陷落柱

        /// <summary>
        ///     修改陷落柱图元
        /// </summary>
        /// <param name="lstCollapsePillarsEntKeyPts"></param>
        /// <param name="sCollapseId"></param>
        private void ModifyXlz(List<CollapsePillarsPoint> lstCollapsePillarsEntKeyPts, string sCollapseId)
        {
            //1.获得当前编辑图层
            var drawspecial = new DrawSpecialCommon();
            const string sLayerAliasName = LayerNames.DEFALUT_COLLAPSE_PILLAR_1; //“陷落柱_1”图层
            IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            if (featureLayer == null)
            {
                MessageBox.Show(@"未找到" + sLayerAliasName + @"图层,无法修改陷落柱图元。");
                return;
            }

            //2.删除原来图元，重新绘制新图元
            bool bIsDeleteOldFeature = DataEditCommon.DeleteFeatureByBId(featureLayer, sCollapseId);
            if (bIsDeleteOldFeature)
            {
                //绘制图元
                DrawXlz(lstCollapsePillarsEntKeyPts, sCollapseId);
            }
        }


        private void DrawXlz(List<CollapsePillarsPoint> lstCollapsePillarsEntKeyPts, string sCollapseId)
        {
            ILayer mPCurrentLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap,
                LayerNames.LAYER_ALIAS_MR_XianLuoZhu1);
            var pFeatureLayer = mPCurrentLayer as IFeatureLayer;
            INewBezierCurveFeedback pBezier = new NewBezierCurveFeedbackClass();
            IPoint pt;
            IPolyline polyline = new PolylineClass();
            for (int i = 0; i < lstCollapsePillarsEntKeyPts.Count; i++)
            {
                pt = new PointClass();
                var mZAware = (IZAware)pt;
                mZAware.ZAware = true;

                pt.X = lstCollapsePillarsEntKeyPts[i].CoordinateX;
                pt.Y = lstCollapsePillarsEntKeyPts[i].CoordinateY;
                pt.Z = lstCollapsePillarsEntKeyPts[i].CoordinateZ;
                if (i == 0)
                {
                    pBezier.Start(pt);
                }
                else if (i == lstCollapsePillarsEntKeyPts.Count - 1)
                {
                    pBezier.AddPoint(pt);
                    pt = new PointClass();
                    var zZAware = (IZAware)pt;
                    zZAware.ZAware = true;
                    pt.X = lstCollapsePillarsEntKeyPts[0].CoordinateX;
                    pt.Y = lstCollapsePillarsEntKeyPts[0].CoordinateY;
                    pt.Z = lstCollapsePillarsEntKeyPts[0].CoordinateZ;
                    pBezier.AddPoint(pt);
                    polyline = pBezier.Stop();
                }
                else
                    pBezier.AddPoint(pt);
            }
            //polyline = (IPolyline)geo;
            var pSegmentCollection = polyline as ISegmentCollection;
            if (pSegmentCollection != null)
            {
                for (int i = 0; i < pSegmentCollection.SegmentCount; i++)
                {
                    pt = new PointClass();
                    var mZAware = (IZAware)pt;
                    mZAware.ZAware = true;

                    pt.X = lstCollapsePillarsEntKeyPts[i].CoordinateX;
                    pt.Y = lstCollapsePillarsEntKeyPts[i].CoordinateY;
                    pt.Z = lstCollapsePillarsEntKeyPts[i].CoordinateZ;


                    IPoint pt1 = new PointClass();
                    mZAware = (IZAware)pt1;
                    mZAware.ZAware = true;
                    if (i == pSegmentCollection.SegmentCount - 1)
                    {
                        pt1.X = lstCollapsePillarsEntKeyPts[0].CoordinateX;
                        pt1.Y = lstCollapsePillarsEntKeyPts[0].CoordinateY;
                        pt1.Z = lstCollapsePillarsEntKeyPts[0].CoordinateZ;

                        pSegmentCollection.Segment[i].FromPoint = pt;
                        pSegmentCollection.Segment[i].ToPoint = pt1;
                    }
                    else
                    {
                        pt1.X = lstCollapsePillarsEntKeyPts[i + 1].CoordinateX;
                        pt1.Y = lstCollapsePillarsEntKeyPts[i + 1].CoordinateY;
                        pt1.Z = lstCollapsePillarsEntKeyPts[i + 1].CoordinateZ;

                        pSegmentCollection.Segment[i].FromPoint = pt;
                        pSegmentCollection.Segment[i].ToPoint = pt1;
                    }
                }
            }
            polyline = pSegmentCollection as IPolyline;
            //polyline = DataEditCommon.PDFX(polyline, "Bezier");

            IPolygon pPolygon = DataEditCommon.PolylineToPolygon(polyline);
            var list = new List<ziduan>
            {
                new ziduan("COLLAPSE_PILLAR_NAME", lstCollapsePillarsEntKeyPts.First().CollapsePillars.CollapsePillarsName),
                new ziduan("BID", sCollapseId),
                radioBtnX.Checked ? new ziduan("XTYPE", "0") : new ziduan("XTYPE", "1")
            };
            IFeature pFeature = DataEditCommon.CreateNewFeature(pFeatureLayer, pPolygon, list);
            if (pFeature != null)
            {
                MyMapHelp.Jump(pFeature.Shape);
                DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
            }

            #region 暂时无用

            //string sTempFolderPath = System.Windows.Forms.Application.StartupPath + "\\TempFolder";

            /////1.将关键点坐标存储到临时文件中
            //string sPtsCoordinateTxtPath = sTempFolderPath + "\\PtsCoordinate.txt";
            //bool bIsWrite = WritePtsInfo2Txt(lstCollapsePillarsEntKeyPts, sPtsCoordinateTxtPath);
            //if (!bIsWrite) return;

            /////2.读取点坐标文件拟合生成陷落柱，仿照等值线
            /////步骤：点文件生成点要素层→转为Raster→提取等值线
            //Geoprocessor GP = new Geoprocessor();
            //string featureOut = sTempFolderPath + "\\KeyPts.shp";
            //DrawContours.ConvertASCIIDescretePoint2FeatureClass(GP, sPtsCoordinateTxtPath, featureOut);//点文件生成点要素层

            //string sRasterOut = sTempFolderPath + "\\Raster";
            //DrawContours.ConvertFeatureCls2Raster(GP, featureOut, sRasterOut);//要素层→Raster

            //string sR2Contour = sTempFolderPath + "\\Contour.shp";
            //double douElevation = 0.5;//等高距0.5
            //DrawContours.SplineRasterToContour(GP, sRasterOut, sR2Contour, douElevation);//提取等值线（即为拟合的陷落柱）

            /////3.复制生成的等值线（即为拟合的陷落柱）要素到陷落柱图层
            /////3.1 获得源图层
            //IFeatureLayer sourceFeaLayer = new FeatureLayerClass();
            //string sourcefeatureClassName = "Contour.shp";
            //IFeatureClass featureClass =PointsFit2Polyline.GetFeatureClassFromShapefileOnDisk(sTempFolderPath, sourcefeatureClassName);//获得等值线（即为拟合的陷落柱）图层

            //if (featureClass == null) return;
            //sourceFeaLayer.FeatureClass = featureClass;


            /////3.2 获得当前编辑图层(目标图层)
            //DrawSpecialCommon drawspecial = new DrawSpecialCommon();
            //string sLayerAliasName = LibCommon.LibLayerNames.DEFALUT_COLLAPSE_PILLAR;//“陷落柱_1”图层
            //IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            //if (featureLayer == null)
            //{
            //    MessageBox.Show("未找到" + sLayerAliasName + "图层,无法绘制陷落柱图元。");
            //    return;
            //}

            /////3.3 复制要素
            //PointsFit2Polyline.CopyFeature(sourceFeaLayer, featureLayer, sCollapseID);

            #endregion
        }

        #endregion

        private void btnMultImport_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                RestoreDirectory = true,
                Filter = @"文本文件(*.txt)|*.txt|所有文件(*.*)|*.*",
                Multiselect = true
            };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            _errorMsg = @"失败文件名：";
            pbCount.Maximum = ofd.FileNames.Length;
            pbCount.Value = 0;
            lblTotal.Text = ofd.FileNames.Length.ToString(CultureInfo.InvariantCulture);
            foreach (var fileName in ofd.FileNames)
            {
                try
                {
                    string[] file = File.ReadAllLines(fileName);
                    var collapsePillarsName =
                        fileName.Substring(fileName.LastIndexOf(@"\", StringComparison.Ordinal) + 1).Split('.')[0];
                    CollapsePillars collapsePillars = CollapsePillars.FindOneByCollapsePillarsName(collapsePillarsName);
                    if (collapsePillars == null)
                    {
                        collapsePillars = new CollapsePillars
                        {
                            Xtype = "0",
                            BindingId = IdGenerator.NewBindingId(),
                            CollapsePillarsName = collapsePillarsName
                        };
                    }
                    else
                    {
                        collapsePillars.CollapsePillarsName = collapsePillarsName;
                    }

                    var collapsePillarsPoints = new List<CollapsePillarsPoint>();
                    //添加关键点
                    for (int i = 0; i < file.Length - 1; i++)
                    {
                        var collapsePillarsPoint = new CollapsePillarsPoint
                        {
                            CoordinateX = Convert.ToDouble(file[i].Split(',')[0]),
                            CoordinateY = Convert.ToDouble(file[i].Split(',')[1]),
                            CoordinateZ = 0.0,
                            BindingId = IdGenerator.NewBindingId(),
                            CollapsePillars = collapsePillars
                        };
                        collapsePillarsPoints.Add(collapsePillarsPoint);
                    }
                    collapsePillars.CollapsePillarsPoints = collapsePillarsPoints;
                    collapsePillars.Save();
                    ModifyXlz(collapsePillarsPoints, collapsePillars.CollapsePillarsId.ToString());
                    lblSuccessed.Text = lblSuccessed.Text =
                        (Convert.ToInt32(lblSuccessed.Text) + 1).ToString(CultureInfo.InvariantCulture);
                    pbCount.Value++;
                }
                catch (Exception)
                {
                    lblError.Text =
                      (Convert.ToInt32(lblError.Text) + 1).ToString(CultureInfo.InvariantCulture);
                    lblSuccessed.Text =
                        (Convert.ToInt32(lblSuccessed.Text) - 1).ToString(CultureInfo.InvariantCulture);
                    _errorMsg += fileName.Substring(fileName.LastIndexOf(@"\", StringComparison.Ordinal) + 1) + "\n";
                    btnDetails.Enabled = true;
                }

            }
            Alert.AlertMsg("导入成功！");
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            Alert.AlertMsg(_errorMsg);
        }
    }
}