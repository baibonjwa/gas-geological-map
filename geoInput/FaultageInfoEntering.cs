using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.ADF.COMSupport;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using GIS;
using GIS.Common;
using LibCommon;
using LibEntity;
using stdole;
using Font = System.Drawing.Font;

namespace geoInput
{
    public partial class FaultageInfoEntering : Form
    {
        /// <summary>
        ///     构造方法
        /// </summary>
        public FaultageInfoEntering()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     带参数的构造方法
        /// </summary>
        public FaultageInfoEntering(Faultage faultage)
        {
            InitializeComponent();
            Faultage = faultage;

            // 设置断层信息
            // 断层名称
            txtFaultageName.Text = Faultage.name;
            // 落差
            txtGap.Text = Faultage.gap;
            // 倾角
            txtAngle.Text = Faultage.angle.ToString(CultureInfo.InvariantCulture);
            // 类型
            if (Faultage.type == "正断层")
            {
                rbtnFrontFaultage.Checked = true;
            }
            else
            {
                rbtnOppositeFaultage.Checked = true;
            }
            // 走向
            txtTrend.Text = Faultage.trend.ToString(CultureInfo.InvariantCulture);
            // 断距
            txtSeparation.Text = Faultage.separation;
            // 坐标X
            txtCoordinateX.Text = Faultage.coordinate_x.ToString(CultureInfo.InvariantCulture);
            // 坐标Y
            txtCoordinateY.Text = Faultage.coordinate_y.ToString(CultureInfo.InvariantCulture);
            // 坐标Z
            txtCoordinateZ.Text = Faultage.coordinate_z.ToString(CultureInfo.InvariantCulture);
            //长度
            var bid = Faultage.bid;
            var pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.DEFALUT_EXPOSE_FAULTAGE);
            var featureLayer = (IFeatureLayer)pLayer;
            if (pLayer == null)
            {
                txtLength.Text = @"0";
                return;
            }
            var pFeature = MyMapHelp.FindFeatureByWhereClause(featureLayer, "BID='" + bid + "'");
            if (pFeature != null)
            {
                var pline = (IPolyline)pFeature.Shape;
                if (pline == null) return;
                txtLength.Text = Math.Round(pline.Length).ToString(CultureInfo.InvariantCulture);
            }
        }

        /** 主键  **/
        /** 业务逻辑类型：添加/修改  **/
        public string ErrorMsg { get; private set; }
        private Faultage Faultage { get; set; }

        /// <summary>
        ///     提  交
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            // 创建断层实体
            Faultage = Faultage.FindAllByProperty("name", txtFaultageName.Text.Trim()).FirstOrDefault();
            Faultage = Faultage ?? new Faultage { bid = IdGenerator.NewBindingId() };

            Faultage.name = txtFaultageName.Text.Trim();
            Faultage.gap = txtGap.Text.Trim();
            Faultage.type = rbtnFrontFaultage.Checked ? "正断层" : "逆断层";
            Faultage.trend = Convert.ToDouble(txtTrend.Text);
            Faultage.separation = txtSeparation.Text.Trim();

            double dAngle;
            if (double.TryParse(txtAngle.Text.Trim(), out dAngle))
            {
                Faultage.angle = dAngle;
            }

            // 坐标X
            double dCoordinateX;
            if (double.TryParse(txtCoordinateX.Text.Trim(), out dCoordinateX))
            {
                Faultage.coordinate_x = dCoordinateX;
            }

            // 坐标Y
            double dCoordinateY;
            if (double.TryParse(txtCoordinateY.Text.Trim(), out dCoordinateY))
            {
                Faultage.coordinate_y = dCoordinateY;
            }

            // 坐标Z
            double dCoordinateZ;
            if (double.TryParse(txtCoordinateZ.Text.Trim(), out dCoordinateZ))
            {
                Faultage.coordinate_z = dCoordinateZ;
            }
            double dLength;
            if (double.TryParse(txtLength.Text.Trim(), out dLength))
            {
                Faultage.length = dLength;
            }

            ModifyJldc(Faultage);

            Faultage.Save();
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        ///     取  消
        /// </summary>
        /// <params name="sender"></params>
        /// <params name="e"></params>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataEditCommon.PickUpPoint(txtCoordinateX, txtCoordinateY);
        }

        private void btnReadTxt_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                RestoreDirectory = true,
                Filter = @"文本文件(*.txt)|*.txt|所有文件(*.*)|*.*"
            };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            var aa = ofd.FileName;
            var sr = new StreamReader(@aa);
            string duqu;
            while ((duqu = sr.ReadLine()) != null)
            {
                var str = duqu.Split('|');
                txtFaultageName.Text = str[0];
                txtCoordinateX.Text = str[1].Split(',')[0];
                txtCoordinateY.Text = str[1].Split(',')[1];
                txtCoordinateZ.Text = @"0";
                txtSeparation.Text = str[2];
                txtGap.Text = str[2];
                txtTrend.Text = str[4];
                txtAngle.Text = str[5];
                txtLength.Text = str[6];
            }
        }

        private void btnMultTxt_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                RestoreDirectory = true,
                Filter = @"文本文件(*.txt)|*.txt|所有文件(*.*)|*.*",
                Multiselect = true
            };
            ErrorMsg = @"失败文件名：";


            if (ofd.ShowDialog() != DialogResult.OK) return;
            var fileCount = ofd.FileNames.Length;
            lblTotal.Text = fileCount.ToString();
            pbCount.Maximum = fileCount;
            pbCount.Value = 0;
            foreach (var fileName in ofd.FileNames)
            {
                try
                {
                    var sr = new StreamReader(fileName, Encoding.GetEncoding("GB2312"));
                    string duqu;
                    while ((duqu = sr.ReadLine()) != null)
                    {
                        var str = duqu.Split('|');
                        var faultage = Faultage.FindAllByProperty("name", str[0]).FirstOrDefault();
                        if (faultage == null)
                        {
                            faultage = new Faultage
                            {
                                name = str[0],
                                coordinate_x = Convert.ToDouble(str[1].Split(',')[0]),
                                coordinate_y = Convert.ToDouble(str[1].Split(',')[1]),
                                coordinate_z = 0.0,
                                separation = str[2],
                                gap = str[2],
                                trend = string.IsNullOrWhiteSpace(str[4]) ? 0.0 : Convert.ToDouble(str[4]),
                                angle = string.IsNullOrWhiteSpace(str[5]) ? 0.0 : Convert.ToDouble(str[5]),
                                length = string.IsNullOrWhiteSpace(str[6]) ? 0.0 : Convert.ToDouble(str[6]),
                                type = str[3],
                                bid = IdGenerator.NewBindingId()
                            };
                            DrawJldc(faultage);
                        }
                        else
                        {
                            faultage.name = str[0];
                            faultage.coordinate_x = Convert.ToDouble(str[1].Split(',')[0]);
                            faultage.coordinate_y = Convert.ToDouble(str[1].Split(',')[1]);
                            faultage.coordinate_z = 0.0;
                            faultage.separation = str[2];
                            faultage.gap = str[2];
                            faultage.trend = string.IsNullOrWhiteSpace(str[4]) ? 0.0 : Convert.ToDouble(str[4]);
                            faultage.angle = string.IsNullOrWhiteSpace(str[5]) ? 0.0 : Convert.ToDouble(str[5]);
                            faultage.length = string.IsNullOrWhiteSpace(str[6]) ? 0.0 : Convert.ToDouble(str[6]);
                            faultage.type = str[3];
                            ModifyJldc(faultage);
                        }
                        faultage.Save();
                        pbCount.Value++;
                        lblSuccessed.Text = lblSuccessed.Text =
                            (Convert.ToInt32(lblSuccessed.Text) + 1).ToString(CultureInfo.InvariantCulture);
                    }
                }
                catch (Exception)
                {
                    lblError.Text =
                        (Convert.ToInt32(lblError.Text) + 1).ToString(CultureInfo.InvariantCulture);
                    ErrorMsg += fileName.Substring(fileName.LastIndexOf(@"\", StringComparison.Ordinal) + 1) + "\n";
                    btnDetails.Enabled = true;
                }
            }
            Alert.AlertMsg("导入完成");
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            Alert.AlertMsg(ErrorMsg);
        }

        #region 绘制图元

        /// <summary>
        ///     修改揭露断层图元
        /// </summary>
        /// <params name="faultageEntity"></params>
        private void ModifyJldc(Faultage faultageEntity)
        {
            //1.获得当前编辑图层
            var drawspecial = new DrawSpecialCommon();
            const string sLayerAliasName = LayerNames.DEFALUT_EXPOSE_FAULTAGE; //“揭露断层”图层
            var featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            if (featureLayer == null)
            {
                MessageBox.Show(@"未找到" + sLayerAliasName + @"图层,无法修改揭露断层图元。");
                return;
            }

            //2.删除原来图元，重新绘制新图元
            DataEditCommon.DeleteFeatureByBId(featureLayer, faultageEntity.bid);
            DrawJldc(faultageEntity);
        }

        /// <summary>
        ///     绘制揭露断层图元
        /// </summary>
        /// <params name="faultage"></params>
        private void DrawJldc(Faultage faultage)
        {
            ////1.获得当前编辑图层
            //DrawSpecialCommon drawspecial = new DrawSpecialCommon();
            //string sLayerAliasName = LibCommon.LibLayerNames.DEFALUT_EXPOSE_FAULTAGE;//“揭露断层”图层
            //IFeatureLayer featureLayer = drawspecial.GetFeatureLayerByName(sLayerAliasName);
            //if (featureLayer == null)
            //{
            //    MessageBox.Show("未找到" + sLayerAliasName + "图层,无法绘制揭露断层图元。");
            //    return;
            //}

            ////2.生成要素（要根据中心点获取起止点）
            ////中心点
            //double centrePtX = Convert.ToDouble(this.txtCoordinateX.Text.ToString());
            //double centrePtY = Convert.ToDouble(this.txtCoordinateY.Text.ToString());
            //IPoint centrePt = new PointClass();
            //centrePt.X = centrePtX;
            //centrePt.Y = centrePtY;

            //// 图形坐标Z  //zwy 20140526 add
            //double dCoordinateZ = 0;
            //if (!double.TryParse(this.txtCoordinateZ.Text.Trim(), out dCoordinateZ))
            //{
            //    MessageBox.Show("输入的Z坐标不是有效数值，请检查！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //centrePt.Z = dCoordinateZ;

            //double angle = Convert.ToDouble(this.txtAngle.Text.ToString());//倾角
            //double length = 10;//默认长度为20，左右各10

            ////计算起止点
            //IPoint fromPt = new PointClass();
            //IPoint toPt = new PointClass();
            //CalculateEndpoints(centrePt, angle, length, ref fromPt, ref toPt);
            //DataEditCommon.g_CurWorkspaceEdit.StartEditing(false);
            //DataEditCommon.g_CurWorkspaceEdit.StartEditOperation();
            //IFeature pFeature = featureLayer.FeatureClass.CreateFeature();

            //ILine line = new LineClass();
            //line.PutCoords(fromPt, toPt);
            //object Missing = Type.Missing;
            //ISegment segment = line as ISegment;
            //ISegmentCollection newLine = new PolylineClass();
            //newLine.AddSegment(segment, ref Missing, ref Missing);
            //IPolyline polyline = newLine as IPolyline;

            //DataEditCommon.ZMValue(pFeature, polyline);  //zwy 20140526 add
            //pFeature.Shape = polyline;

            ////2.1断层标注(DCBZ)
            //string strMC = this.txtFaultageName.Text;//断层名称
            //string strLC = this.txtGap.Text;//落差
            //string strQJ = this.txtAngle.Text;//倾角
            //string strDCBZ = strMC + " " + "H=" + strLC + "m" + " " + "<" + strQJ + "°";

            ////断层标注字段赋值（该字段值保持在图层属性中）
            //int index = featureLayer.FeatureClass.Fields.FindField("FAULTAGE_NAME");
            //pFeature.set_Value(index, strDCBZ);

            ////要素ID字段赋值（对应属性表中BindingID）
            //int iFieldID = pFeature.Fields.FindField(GIS_Const.FIELD_BID);
            //pFeature.Value[iFieldID] = faultage.bid.ToString();

            //pFeature.Store();
            //DataEditCommon.g_CurWorkspaceEdit.StopEditOperation();
            //DataEditCommon.g_CurWorkspaceEdit.StopEditing(true);
            ////2.2给生成的断层赋符号
            //int ID = pFeature.OID;
            //string path = Application.StartupPath + @"\symbol.ServerStyle";//【这里用到了自己定义的符号库】
            ////默认为正断层（符号）
            //string sGalleryClassName = "123";
            //string symbolName = "123"; ;
            //if (this.rbtnFrontFaultage.Checked)//正断层
            //{

            //    sGalleryClassName = "123";
            //    symbolName = "123";
            //}
            //else if (this.rbtnOppositeFaultage.Checked)//逆断层
            //{

            //    sGalleryClassName = "1234";
            //    symbolName = "1234";
            //}

            //ILineSymbol lineSymbol = GetSymbol(path, sGalleryClassName, symbolName);
            //ILayer layer = featureLayer as ILayer;
            //SpecialLineRenderer(layer, ID, lineSymbol);
            //AddAnnotate(layer, GIS_Const.FILE_DCBZ);

            ////缩放到新增的线要素，并高亮该要素
            //GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.Extent = pFeature.Shape.Envelope;
            //GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.Extent.Expand(1.5, 1.5, true);
            //GIS.Common.DataEditCommon.g_pMyMapCtrl.Map.SelectFeature(featureLayer, pFeature);
            //GIS.Common.DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewAll, null, null);


            var pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.DEFALUT_EXPOSE_FAULTAGE);
            var featureLayer = (IFeatureLayer)pLayer;
            if (pLayer == null)
            {
                MessageBox.Show(@"未找到揭露断层图层,无法绘制揭露断层图元。");
                return;
            }
            //2.生成要素（要根据中心点获取起止点）
            //中心点
            IPoint centrePt = new PointClass();
            centrePt.X = faultage.coordinate_x;
            centrePt.Y = faultage.coordinate_y;
            centrePt.Z = faultage.coordinate_z;

            var trend = faultage.trend; //走向
            var length = faultage.length / 2; //默认长度为20，左右各10

            //计算起止点
            IPoint fromPt = new PointClass();
            IPoint toPt = new PointClass();
            CalculateEndpoints(centrePt, trend, length, ref fromPt, ref toPt);

            ILine line = new LineClass();
            line.PutCoords(fromPt, toPt);
            var missing = Type.Missing;
            var segment = (ISegment)line;
            ISegmentCollection newLine = new PolylineClass();
            newLine.AddSegment(segment, ref missing, ref missing);
            var polyline = (IPolyline)newLine;

            var list = new List<ziduan>
            {
                new ziduan("bid", faultage.bid),
                new ziduan("FAULTAGE_NAME", faultage.name),
                new ziduan("addtime", DateTime.Now.ToString(CultureInfo.InvariantCulture)),
                new ziduan("GAP", faultage.gap),
                new ziduan("ANGLE", faultage.angle.ToString(CultureInfo.InvariantCulture)),
                new ziduan("TREND", faultage.trend.ToString(CultureInfo.InvariantCulture)),
                new ziduan("SEPARATION", faultage.separation),
                new ziduan("type", faultage.type)
            };

            var pfeature = DataEditCommon.CreateNewFeature(featureLayer, polyline, list);
            if (pfeature == null) return;
            MyMapHelp.Jump(polyline);
            DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(
                (esriViewDrawPhase)34, null, null);
        }

        /// <summary>
        ///     根据中心点、倾角和长度计算起止点
        /// </summary>
        /// <params name="centrePt">中心点</params>
        /// <params name="angle">倾角</params>
        /// <params name="length">1/2长度</params>
        /// <params name="fromPt">起点</params>
        /// <params name="toPt">终点</params>
        private static void CalculateEndpoints(IPoint centrePt, double angle, double length, ref IPoint fromPt,
            ref IPoint toPt)
        {
            var radian = (Math.PI / 180) * angle; //角度转为弧度

            fromPt.X = centrePt.X + length * Math.Cos(radian);
            fromPt.Y = centrePt.Y + length * Math.Sin(radian);

            toPt.X = centrePt.X - length * Math.Cos(radian);
            toPt.Y = centrePt.Y - length * Math.Sin(radian);

            //给Z坐标赋值
            if (!double.IsNaN(centrePt.Z))
            {
                fromPt.Z = centrePt.Z;
                toPt.Z = centrePt.Z;
            }
            else
            {
                fromPt.Z = 0;
                toPt.Z = 0;
            }
        }

        /// <summary>
        ///     获得符号
        /// </summary>
        /// <params name="sServerStylePath"></params>
        /// <params name="sGalleryClassName"></params>
        /// <params name="symbolName"></params>
        /// <returns></returns>
        public static ILineSymbol GetSymbol(string sServerStylePath, string sGalleryClassName, string symbolName)
        {
            IStyleGallery pStyleGallery = new ServerStyleGalleryClass();
            var pStyleGalleryStorage = (IStyleGalleryStorage)pStyleGallery;
            ILineSymbol lineSymbol = new SimpleLineSymbolClass();

            //查找到符号
            pStyleGalleryStorage.TargetFile = sServerStylePath; //输入符号的地址
            var pEnumStyleGalleryItem = pStyleGallery.Items["Line Symbols", sServerStylePath, sGalleryClassName];
            //sGalleryClassName为输入符号库的名称

            pEnumStyleGalleryItem.Reset();
            var pStyleGalleryItem = pEnumStyleGalleryItem.Next();
            while (pStyleGalleryItem != null)
            {
                if (pStyleGalleryItem.Name == symbolName)
                {
                    lineSymbol = (ILineSymbol)pStyleGalleryItem.Item;
                    Marshal.ReleaseComObject(pEnumStyleGalleryItem);
                    break;
                }
                pStyleGalleryItem = pEnumStyleGalleryItem.Next();
            }
            //定义符号的大小,已经定义好了是size=30
            //lineSymbol.Width = 2;

            return lineSymbol;
        }

        public static void SpecialLineRenderer2(ILayer layer, string field, string value, ILineSymbol lineSymbol)
        {
            var geoFeaLayer = layer as IGeoFeatureLayer;
            IUniqueValueRenderer uniValueRender = new UniqueValueRenderer();

            IQueryFilter2 queryFilter = new QueryFilterClass();
            uniValueRender.FieldCount = 1;
            uniValueRender.Field[0] = field;
            queryFilter.AddField(field);
            if (geoFeaLayer != null)
            {
                var fieldIndex = geoFeaLayer.FeatureClass.Fields.FindField(field);

                var customSymbol = (ISymbol)lineSymbol;

                var featureCursor = geoFeaLayer.FeatureClass.Search(queryFilter, true);
                var feature = featureCursor.NextFeature();
                while (feature != null)
                {
                    var sValue = Convert.ToString(feature.Value[fieldIndex]);
                    if (sValue == value)
                    {
                        uniValueRender.AddValue(sValue, "", customSymbol);
                    }
                    else
                    {
                        var defaultSymbol = geoFeaLayer.Renderer.SymbolByFeature[feature];
                        uniValueRender.AddValue(sValue, "", defaultSymbol);
                    }

                    feature = featureCursor.NextFeature();
                }
            }

            ComReleaser.ReleaseCOMObject(null);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(featureCursor);  //释放指针                
            if (geoFeaLayer != null) geoFeaLayer.Renderer = uniValueRender as IFeatureRenderer;
        }

        public static void SpecialLineRenderer(ILayer layer, int id, ILineSymbol lineSymbol)
        {
            var geoFeaLayer = layer as IGeoFeatureLayer;
            IUniqueValueRenderer uniValueRender = new UniqueValueRenderer();

            uniValueRender.FieldCount = 1;
            uniValueRender.Field[0] = "OBJECTID";
            var customSymbol = (ISymbol)lineSymbol;

            //选择某个字段作为渲染符号值            
            if (geoFeaLayer != null)
            {
                var featureCursor = geoFeaLayer.FeatureClass.Search(null, true);
                var feature = featureCursor.NextFeature();
                while (feature != null)
                {
                    var nowId = feature.OID;

                    if (nowId == id)
                    {
                        uniValueRender.AddValue(feature.OID.ToString(), "", customSymbol);
                    }
                    else
                    {
                        var defaultSymbol = geoFeaLayer.Renderer.SymbolByFeature[feature];
                        uniValueRender.AddValue(feature.OID.ToString(), "", defaultSymbol);
                    }

                    feature = featureCursor.NextFeature();
                }
            }

            if (geoFeaLayer != null) geoFeaLayer.Renderer = uniValueRender as IFeatureRenderer;
        }

        public static void AddAnnotate(ILayer layer, string fieldName)
        {
            var pGeoLayer = layer as IGeoFeatureLayer;
            if (pGeoLayer != null)
            {
                var ipalpColl = pGeoLayer.AnnotationProperties;
                ipalpColl.Clear();
                IColor fontColor = new RgbColor();
                fontColor.RGB = 255; //字体颜色      
                var font = new Font("宋体", 10, FontStyle.Bold);
                var dispFont = (IFontDisp)OLE.GetIFontDispFromFont(font);

                ITextSymbol pTextSymbol = new TextSymbolClass
                {
                    Color = fontColor,
                    Font = dispFont,
                    Size = 12
                };
                ////用来控制标注和要素的相对位置关系  

                ILineLabelPosition pLineLpos = new LineLabelPositionClass
                {
                    Parallel = true, //修改标注的属性     
                    //Perpendicular = false,  
                    Below = true,
                    InLine = false,
                    Above = false
                };

                //用来控制标注冲突      
                ILineLabelPlacementPriorities pLinePlace = new LineLabelPlacementPrioritiesClass
                {
                    AboveStart = 5, //让above 和start的优先级为5 
                    BelowAfter = 4
                };

                //用来实现对ILineLabelPosition 和 ILineLabelPlacementPriorities以及更高级属性的控制

                IBasicOverposterLayerProperties pBolp = new BasicOverposterLayerPropertiesClass
                {
                    FeatureType = esriBasicOverposterFeatureType.esriOverposterPolygon,
                    LineLabelPlacementPriorities = pLinePlace,
                    LineLabelPosition = pLineLpos
                };
                //创建标注对象          
                ILabelEngineLayerProperties pLableEngine = new LabelEngineLayerPropertiesClass
                {
                    Symbol = pTextSymbol,
                    BasicOverposterLayerProperties = pBolp,
                    IsExpressionSimple = true,
                    Expression = "[" + fieldName + "]"
                };
                //设置标注的参考比例尺  
                var pAnnoLyrPros = (IAnnotateLayerTransformationProperties)pLableEngine;
                pAnnoLyrPros.ReferenceScale = 2500000;
                //设置标注可见的最大最小比例尺       
                var pAnnoPros = pLableEngine as IAnnotateLayerProperties;
                //pAnnoPros.AnnotationMaximumScale = 2500000;       
                //pAnnoPros.AnnotationMinimumScale = 25000000;  
                //pAnnoPros.WhereClause属性  设置过滤条件   
                ipalpColl.Add(pAnnoPros);
            }
            if (pGeoLayer != null) pGeoLayer.DisplayAnnotation = true;
        }

        #endregion
    }
}