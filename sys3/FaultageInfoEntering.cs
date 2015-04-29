using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
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

namespace sys3
{
    public partial class FaultageInfoEntering : Form
    {
        /// <summary>
        ///     构造方法
        /// </summary>
        public FaultageInfoEntering()
        {
            InitializeComponent();

            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.INSERT_FAULTAGE_INFO);
        }

        /// <summary>
        ///     带参数的构造方法
        /// </summary>
        public FaultageInfoEntering(Faultage faultage)
        {
            InitializeComponent();
            Faultage = faultage;
            // 设置窗体默认属性
            FormDefaultPropertiesSetter.SetEnteringFormDefaultProperties(this, Const_GM.UPDATE_FAULTAGE_INFO);

            // 设置断层信息
            // 断层名称
            txtFaultageName.Text = Faultage.FaultageName;
            // 落差
            txtGap.Text = Faultage.Gap;
            // 倾角
            txtAngle.Text = Faultage.Angle.ToString(CultureInfo.InvariantCulture);
            // 类型
            if (Const_GM.FRONT_FAULTAGE.Equals(Faultage.Type))
            {
                rbtnFrontFaultage.Checked = true;
            }
            else
            {
                rbtnOppositeFaultage.Checked = true;
            }
            // 走向
            txtTrend.Text = Faultage.Trend.ToString(CultureInfo.InvariantCulture);
            // 断距
            txtSeparation.Text = Faultage.Separation;
            // 坐标X
            txtCoordinateX.Text = Faultage.CoordinateX.ToString(CultureInfo.InvariantCulture);
            // 坐标Y
            txtCoordinateY.Text = Faultage.CoordinateY.ToString(CultureInfo.InvariantCulture);
            // 坐标Z
            txtCoordinateZ.Text = Faultage.CoordinateZ.ToString(CultureInfo.InvariantCulture);
            //长度
            var bid = Faultage.BindingId;
            var pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.DEFALUT_EXPOSE_FAULTAGE);
            var featureLayer = (IFeatureLayer) pLayer;
            if (pLayer == null)
            {
                txtLength.Text = @"0";
                return;
            }
            var pFeature = MyMapHelp.FindFeatureByWhereClause(featureLayer, "BID='" + bid + "'");
            if (pFeature != null)
            {
                var pline = (IPolyline) pFeature.Shape;
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // 验证
            if (!Check())
            {
                DialogResult = DialogResult.None;
                return;
            }
            DialogResult = DialogResult.OK;

            // 创建断层实体
            Faultage = Faultage.FindByFaultageName(txtFaultageName.Text.Trim());
            Faultage = Faultage ?? new Faultage {BindingId = IDGenerator.NewBindingID()};

            Faultage.FaultageName = txtFaultageName.Text.Trim();
            Faultage.Gap = txtGap.Text.Trim();
            Faultage.Type = rbtnFrontFaultage.Checked ? Const_GM.FRONT_FAULTAGE : Const_GM.OPPOSITE_FAULTAGE;
            Faultage.Trend = Convert.ToDouble(txtTrend.Text);
            Faultage.Separation = txtSeparation.Text.Trim();

            double dAngle;
            if (double.TryParse(txtAngle.Text.Trim(), out dAngle))
            {
                Faultage.Angle = dAngle;
            }

            // 坐标X
            double dCoordinateX;
            if (double.TryParse(txtCoordinateX.Text.Trim(), out dCoordinateX))
            {
                Faultage.CoordinateX = dCoordinateX;
            }

            // 坐标Y
            double dCoordinateY;
            if (double.TryParse(txtCoordinateY.Text.Trim(), out dCoordinateY))
            {
                Faultage.CoordinateY = dCoordinateY;
            }

            // 坐标Z
            double dCoordinateZ;
            if (double.TryParse(txtCoordinateZ.Text.Trim(), out dCoordinateZ))
            {
                Faultage.CoordinateZ = dCoordinateZ;
            }
            double dLength;
            if (double.TryParse(txtLength.Text.Trim(), out dLength))
            {
                Faultage.Length = dLength;
            }

            ModifyJldc(Faultage);

            Faultage.Save();
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        ///     取  消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭窗口
            Close();
        }

        /// <summary>
        ///     验证画面入力数据
        /// </summary>
        /// <returns>验证结果：true 通过验证, false未通过验证</returns>
        private bool Check()
        {
            // 判断断层名称是否录入
            if (!LibCommon.Check.isEmpty(txtFaultageName, Const_GM.FAULTAGE_NAME))
            {
                return false;
            }

            // 断层名称特殊字符判断
            if (!LibCommon.Check.checkSpecialCharacters(txtFaultageName, Const_GM.FAULTAGE_NAME))
            {
                return false;
            }

            // 判断落差是否录入
            if (!LibCommon.Check.isEmpty(txtGap, Const_GM.GAP))
            {
                return false;
            }
            // 判断长度是否录入
            if (!LibCommon.Check.isEmpty(txtLength, "长度"))
            {
                return false;
            }

            // 判断落差是否为数字
            if (!LibCommon.Check.IsNumeric(txtGap, Const_GM.GAP))
            {
                return false;
            }

            // 判断倾角是否录入
            if (!LibCommon.Check.isEmpty(txtAngle, Const_GM.ANGLE))
            {
                return false;
            }

            // 判断倾角是否为数字
            if (!LibCommon.Check.IsNumeric(txtAngle, Const_GM.ANGLE))
            {
                return false;
            }
            // 判断长度是否为数字
            if (!LibCommon.Check.IsNumeric(txtLength, "长度"))
            {
                return false;
            }
            // 判断走向是否录入
            if (!LibCommon.Check.isEmpty(txtTrend, Const_GM.TREND))
            {
                return false;
            }

            // 判断走向是否为数字
            if (!LibCommon.Check.IsNumeric(txtTrend, Const_GM.TREND))
            {
                return false;
            }

            // 判断断距是否录入
            if (!LibCommon.Check.isEmpty(txtSeparation, Const_GM.SEPARATION))
            {
                return false;
            }

            // 判断断距是否为数字
            if (!LibCommon.Check.IsNumeric(txtSeparation, Const_GM.SEPARATION))
            {
                return false;
            }

            //****************************************************
            // 判断坐标X是否录入
            if (!LibCommon.Check.isEmpty(txtCoordinateX, Const_GM.COORDINATE_X))
            {
                return false;
            }

            // 判断坐标X是否为数字
            if (!LibCommon.Check.IsNumeric(txtCoordinateX, Const_GM.COORDINATE_X))
            {
                return false;
            }

            // 判断坐标Y是否录入
            if (!LibCommon.Check.isEmpty(txtCoordinateY, Const_GM.COORDINATE_Y))
            {
                return false;
            }

            // 判断坐标Y是否为数字
            if (!LibCommon.Check.IsNumeric(txtCoordinateY, Const_GM.COORDINATE_Y))
            {
                return false;
            }

            // 判断坐标Z是否录入
            if (!LibCommon.Check.isEmpty(txtCoordinateZ, Const_GM.COORDINATE_Z))
            {
                return false;
            }

            // 判断坐标Z是否为数字
            if (!LibCommon.Check.IsNumeric(txtCoordinateZ, Const_GM.COORDINATE_Z))
            {
                return false;
            }
            //****************************************************

            // 验证通过
            return true;
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
                        var faultage = Faultage.FindByFaultageName(str[0]);
                        if (faultage == null)
                        {
                            faultage = new Faultage
                            {
                                FaultageName = str[0],
                                CoordinateX = Convert.ToDouble(str[1].Split(',')[0]),
                                CoordinateY = Convert.ToDouble(str[1].Split(',')[1]),
                                CoordinateZ = 0.0,
                                Separation = str[2],
                                Gap = str[2],
                                Trend = String.IsNullOrWhiteSpace(str[4]) ? 0.0 : Convert.ToDouble(str[4]),
                                Angle = String.IsNullOrWhiteSpace(str[5]) ? 0.0 : Convert.ToDouble(str[5]),
                                Length = String.IsNullOrWhiteSpace(str[6]) ? 0.0 : Convert.ToDouble(str[6]),
                                Type = str[3],
                                BindingId = IDGenerator.NewBindingID()
                            };
                            DrawJldc(faultage);
                        }
                        else
                        {
                            faultage.FaultageName = str[0];
                            faultage.CoordinateX = Convert.ToDouble(str[1].Split(',')[0]);
                            faultage.CoordinateY = Convert.ToDouble(str[1].Split(',')[1]);
                            faultage.CoordinateZ = 0.0;
                            faultage.Separation = str[2];
                            faultage.Gap = str[2];
                            faultage.Trend = String.IsNullOrWhiteSpace(str[4]) ? 0.0 : Convert.ToDouble(str[4]);
                            faultage.Angle = String.IsNullOrWhiteSpace(str[5]) ? 0.0 : Convert.ToDouble(str[5]);
                            faultage.Length = String.IsNullOrWhiteSpace(str[6]) ? 0.0 : Convert.ToDouble(str[6]);
                            faultage.Type = str[3];
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
            Alert.alert("导入完成");
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            Alert.alert(ErrorMsg);
        }

        #region 绘制图元

        /// <summary>
        ///     修改揭露断层图元
        /// </summary>
        /// <param name="faultageEntity"></param>
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
            DataEditCommon.DeleteFeatureByBId(featureLayer, faultageEntity.BindingId);
            DrawJldc(faultageEntity);
        }

        /// <summary>
        ///     绘制揭露断层图元
        /// </summary>
        /// <param name="faultage"></param>
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
            //pFeature.Value[iFieldID] = faultage.BindingId.ToString();

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
            var featureLayer = (IFeatureLayer) pLayer;
            if (pLayer == null)
            {
                MessageBox.Show(@"未找到揭露断层图层,无法绘制揭露断层图元。");
                return;
            }
            //2.生成要素（要根据中心点获取起止点）
            //中心点
            IPoint centrePt = new PointClass();
            centrePt.X = faultage.CoordinateX;
            centrePt.Y = faultage.CoordinateY;
            centrePt.Z = faultage.CoordinateZ;

            var trend = faultage.Trend; //走向
            var length = faultage.Length/2; //默认长度为20，左右各10

            //计算起止点
            IPoint fromPt = new PointClass();
            IPoint toPt = new PointClass();
            CalculateEndpoints(centrePt, trend, length, ref fromPt, ref toPt);

            ILine line = new LineClass();
            line.PutCoords(fromPt, toPt);
            var missing = Type.Missing;
            var segment = (ISegment) line;
            ISegmentCollection newLine = new PolylineClass();
            newLine.AddSegment(segment, ref missing, ref missing);
            var polyline = (IPolyline) newLine;

            var list = new List<ziduan>
            {
                new ziduan("bid", faultage.BindingId),
                new ziduan("FAULTAGE_NAME", faultage.FaultageName),
                new ziduan("addtime", DateTime.Now.ToString(CultureInfo.InvariantCulture)),
                new ziduan("GAP", faultage.Gap),
                new ziduan("ANGLE", faultage.Angle.ToString(CultureInfo.InvariantCulture)),
                new ziduan("TREND", faultage.Trend.ToString(CultureInfo.InvariantCulture)),
                new ziduan("SEPARATION", faultage.Separation),
                new ziduan("type", faultage.Type)
            };

            var pfeature = DataEditCommon.CreateNewFeature(featureLayer, polyline, list);
            if (pfeature == null) return;
            MyMapHelp.Jump(polyline);
            DataEditCommon.g_pMyMapCtrl.ActiveView.PartialRefresh(
                (esriViewDrawPhase) 34, null, null);
        }

        /// <summary>
        ///     根据中心点、倾角和长度计算起止点
        /// </summary>
        /// <param name="centrePt">中心点</param>
        /// <param name="angle">倾角</param>
        /// <param name="length">1/2长度</param>
        /// <param name="fromPt">起点</param>
        /// <param name="toPt">终点</param>
        private static void CalculateEndpoints(IPoint centrePt, double angle, double length, ref IPoint fromPt,
            ref IPoint toPt)
        {
            var radian = (Math.PI/180)*angle; //角度转为弧度

            fromPt.X = centrePt.X + length*Math.Cos(radian);
            fromPt.Y = centrePt.Y + length*Math.Sin(radian);

            toPt.X = centrePt.X - length*Math.Cos(radian);
            toPt.Y = centrePt.Y - length*Math.Sin(radian);

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
        /// <param name="sServerStylePath"></param>
        /// <param name="sGalleryClassName"></param>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public static ILineSymbol GetSymbol(string sServerStylePath, string sGalleryClassName, string symbolName)
        {
            IStyleGallery pStyleGallery = new ServerStyleGalleryClass();
            var pStyleGalleryStorage = (IStyleGalleryStorage) pStyleGallery;
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
                    lineSymbol = (ILineSymbol) pStyleGalleryItem.Item;
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

                var customSymbol = (ISymbol) lineSymbol;

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
            var customSymbol = (ISymbol) lineSymbol;

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
                var dispFont = (IFontDisp) OLE.GetIFontDispFromFont(font);

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
                var pAnnoLyrPros = (IAnnotateLayerTransformationProperties) pLableEngine;
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