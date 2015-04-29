using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.SpatialAnalyst;
using GIS.Common;
using LibEntity;

namespace GIS.SpecialGraphic
{

    public partial class MakeContours : Form
    {
        struct XYValueStruct
        {
            public double X;
            public double Y;
            public double TheValue;
        };
        public static string m_MakeContoursFolder = "";
        string m_strDataFilePath;
        //传入图层名和图层别名
        public string m_layerName;
        public string m_layerAliasName;
        string EditLayerName;
        string SDELayerName;
        public MakeContours()
        {

            InitializeComponent();
        }

        private void MakeContours_Load(object sender, EventArgs e)
        {
            //不同插值法生成等值线
            m_MakeContoursFolder = Application.StartupPath + "\\MakeContours";
            if (Directory.Exists(m_MakeContoursFolder))
            {
                try
                {
                    Directory.Delete(m_MakeContoursFolder, true);
                }
                catch (Exception ex)
                { }
            }
            int d = 0;
            while (!Directory.Exists(m_MakeContoursFolder))
            {
                d++;
                Directory.CreateDirectory(m_MakeContoursFolder);
                if (d > 10)
                {
                    MessageBox.Show("初始化失败，请重试！");
                    this.Close();
                    return;
                }
            }
            CB_InterpolationMethod.SelectedIndex = 0;
            CB_searchRadiusProp.SelectedIndex = 0;
            CB_semiVariogramProp.SelectedIndex = 0;
            CB_SplineType.SelectedIndex = 0;
            CB_ZFiled.SelectedIndex = 0;
            groupBox1.Enabled = false;
            if (m_layerAliasName.Equals("瓦斯压力等值线"))
            {
                EditLayerName = LayerNames.LAYER_ALIAS_MR_YLDZX;
                SDELayerName = LayerNames.LAYER_NAME_MR_YLDZX;
            }
            if (m_layerAliasName.Equals("瓦斯含量等值线"))
            {
                EditLayerName = LayerNames.LAYER_ALIAS_MR_HLDZX;
                SDELayerName = LayerNames.LAYER_NAME_MR_HLDZX;
            }
            if (m_layerAliasName.Equals("瓦斯涌出量等值线"))
            {
                EditLayerName = LayerNames.LAYER_ALIAS_MR_YCLDZX;
                SDELayerName = LayerNames.LAYER_NAME_MR_YCLDZX;
            }
        }

        private void FileInput_Click(object sender, EventArgs e)
        {
            string pathBefore1 = "";
            OpenFileDialog Path1 = new OpenFileDialog();
            Path1.Title = "文件选择";
            Path1.Filter = "离散点数据文件|*.txt";
            Path1.ShowDialog();
            if (Path1.FileName == "")
            {
                return;
            }
            else
            {
                TB_DocumentPath.Text = Path1.FileName.ToString();
                m_strDataFilePath = Application.StartupPath + "\\tempdata";
            }

        }

        private void BT_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(m_MakeContoursFolder))
                {
                    Directory.CreateDirectory(m_MakeContoursFolder);
                }
                panel1.Enabled = false;
                panel2.Enabled = false;
                panel4.Enabled = false;
                lbshengc.Visible = true;
                Application.DoEvents();
                ArrayList TheValueList = new ArrayList();
                if (radioBtndata.Checked)
                {
                    if (m_layerAliasName.Equals("瓦斯压力等值线"))
                    {
                        var gasPressures = GasPressure.FindAll();
                        foreach (var i in gasPressures)
                        {
                            XYValueStruct temp;
                            temp.X = i.CoordinateX;
                            temp.Y = i.CoordinateY;
                            temp.TheValue = i.GasPressureValue;
                            TheValueList.Add(temp);
                        }
                    }
                    if (m_layerAliasName.Equals("瓦斯含量等值线"))
                    {
                        var gasContent = GasContent.FindAll();
                        foreach (var i in gasContent)
                        {
                            XYValueStruct temp;
                            temp.X = i.CoordinateX;
                            temp.Y = i.CoordinateY;
                            temp.TheValue = i.GasContentValue;
                            TheValueList.Add(temp);
                        }
                    }
                    if (m_layerAliasName.Equals("瓦斯涌出量等值线"))
                    {
                        var gasGushQuantity = GasGushQuantity.FindAll();
                        foreach (var i in gasGushQuantity)
                        {
                            XYValueStruct temp;
                            temp.X = i.CoordinateX;
                            temp.Y = i.CoordinateY;
                            temp.TheValue = i.AbsoluteGasGushQuantity;
                            TheValueList.Add(temp);
                        }
                    }

                    if (TheValueList.Count < 3)
                    {
                        MessageBox.Show("离散点数据为空或小于三个，无法生成等值线！");
                        return;
                    }
                    string[] strlsd = new string[TheValueList.Count];
                    for (int i = 0; i < TheValueList.Count; i++)
                    {
                        XYValueStruct tempxy = (XYValueStruct)TheValueList[i];
                        string temstr = tempxy.X + "," + tempxy.Y + "," + tempxy.TheValue;
                        strlsd[i] = temstr;
                    }
                    m_strDataFilePath = Application.StartupPath + "\\tempdata.txt";
                    File.WriteAllLines(m_strDataFilePath, strlsd);
                }
                else
                {
                    m_strDataFilePath = TB_DocumentPath.Text.Trim();
                    if (m_strDataFilePath.Equals(""))
                    {
                        MessageBox.Show("请选择一个离散点文件！");
                        return;
                    }
                }
                bool bIsSuccess = false;
                string failInfo = "";
                DrawSpecialCommon drawspecial = new DrawSpecialCommon();
                string sLayerAliasName = m_layerAliasName;//MapControl中图层名称
                string nameOftargetFeatureClass = SDELayerName;//m_layerName + "_NO" + sCoalseamNO;//数据库中图层名称
                string extent = "";
                if (radioBtnKJ.Checked)
                {
                    ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MINE_BOUNDARY);
                    IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
                    IEnvelope pEnvelope = pFeatureLayer.AreaOfInterest;
                    //右上左下  xmax  ymax  xmin  ymin
                    extent = pEnvelope.XMax.ToString() + " " + pEnvelope.YMax.ToString() + " " + pEnvelope.XMin.ToString() + " " + pEnvelope.YMin.ToString();
                }
                IWorkspace targetworkspace = DataEditCommon.g_pCurrentWorkSpace;
                //最后重新生成图层并添加到数据库及MapControl中
                bIsSuccess = CreateContours(targetworkspace, nameOftargetFeatureClass, sLayerAliasName, extent, ref failInfo);

                if (bIsSuccess)
                {
                    string layername = "";
                    if (m_layerAliasName.Equals("瓦斯压力等值线"))
                    {
                        layername = LayerNames.LAYER_ALIAS_MR_YLDZX;
                    }
                    if (m_layerAliasName.Equals("瓦斯含量等值线"))
                    {
                        layername = LayerNames.LAYER_ALIAS_MR_HLDZX;
                    }
                    if (m_layerAliasName.Equals("瓦斯涌出量等值线"))
                    {
                        layername = LayerNames.LAYER_ALIAS_MR_YCLDZX;
                    }
                    DataEditCommon.SetLayerVisibleByName(DataEditCommon.g_pMap, layername, true);

                    MessageBox.Show("等值线和渲染图生成完成。");
                    this.Close();
                }
                else
                {
                    MessageBox.Show(failInfo + "生成过程有误，请检查。");
                }
                //this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                panel1.Enabled = true;
                panel2.Enabled = true;
                panel4.Enabled = true;
                lbshengc.Visible = false;
                Application.DoEvents();
                progressBarControl1.Position = 0;
            }
        }




        /// <summary>
        /// 生成等值线
        /// </summary>
        /// <param name="targetworkspace"></param>
        /// <param name="nameOftargetFeatureClass"></param>
        /// <param name="sLayerAliasName"></param>
        private bool CreateContours(IWorkspace targetworkspace, string nameOftargetFeatureClass,
            string sLayerAliasName, string extent, ref string failInfo)
        {
            double douElevation;

            //设置一个最小值
            progressBarControl1.Properties.Minimum = 0;
            //设置一个最大值
            progressBarControl1.Properties.Maximum = 6;
            //设置步长，即每次增加的数
            progressBarControl1.Properties.Step = 1;
            //设置进度条的样式
            progressBarControl1.Properties.ProgressViewStyle = ProgressViewStyle.Solid;
            progressBarControl1.Position = 0;

            try
            {
                Geoprocessor GP = new Geoprocessor();

                if (withIn(m_strDataFilePath) == false)
                    return false;
                if (TB_Interval.Text == "")
                {
                    MessageBox.Show("请输入正确的等高距！", "提示！");
                    return false;
                }
                if (double.TryParse(TB_Interval.Text.Trim(), out douElevation))
                { }
                else
                {
                    TB_Interval.Text = null;
                    MessageBox.Show("请输入正确的等高距！", "提示!");
                    return false;
                }

                string featurOut = m_MakeContoursFolder + "\\Cont.shp";
                int k = 0;
                while (File.Exists(featurOut))
                {
                    k++;
                    featurOut = m_MakeContoursFolder + "\\Cont" + k.ToString() + ".shp";

                }
                int countCont = Directory.GetFiles(m_MakeContoursFolder, "Cont*").Length;
                if (countCont > 0)
                {
                    featurOut = m_MakeContoursFolder + "\\Cont" + countCont.ToString() + ".shp";
                }
                if (DrawContours.ConvertASCIIDescretePoint2FeatureClass(GP, m_strDataFilePath, featurOut) == false)
                    return false;
                //执行步长
                this.progressBarControl1.PerformStep();

                string ContourRaster = m_MakeContoursFolder + "\\Spline";
                //string ContourRaster = m_MakeContoursFolder + "\\" + nameOftargetFeatureClass + "_Render";
                if (extent != "")//右上左下  xmax  ymax  xmin  ymin
                    GP.SetEnvironmentValue("Extent", extent);
                bool suc = false;
                string sRasterLayerAliasName = sLayerAliasName + "渲染图";
                ILayer rsLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, sRasterLayerAliasName);
                if (rsLayer != null)
                {
                    DataEditCommon.g_pMap.DeleteLayer(rsLayer);
                }
                if (Directory.Exists(ContourRaster))
                {
                    try
                    {
                        Directory.Delete(ContourRaster, true);
                    }
                    catch
                    {
                        k = 1;
                        while (Directory.Exists(ContourRaster))
                        {
                            ContourRaster = m_MakeContoursFolder + "\\Spline" + k.ToString();
                            k++;
                        }
                    }
                }
                switch (CB_InterpolationMethod.Text)
                {
                    case "样条函数插值法":
                        suc = DrawContours.Interpolate2RasterSpline(GP, featurOut, ContourRaster, CB_SplineType.Text);
                        break;
                    case "自然邻域插值法":
                        suc = DrawContours.Interpolate2RasterNN(GP, featurOut, ContourRaster);
                        break;
                    case "克里格插值法":
                        suc = DrawContours.Interpolate2RasterKriging(GP, featurOut, ContourRaster,
                            CB_semiVariogramProp.Text, CB_searchRadiusProp.Text);
                        break;
                    case "反距离权重插值法":
                        suc = DrawContours.Interpolate2RasterIDW(GP, featurOut, ContourRaster);
                        break;
                    case "趋势面插值法":
                        suc = DrawContours.TrendToRaster(GP, featurOut, ContourRaster);
                        break;
                }
                if (suc == false)
                    return false;
                this.progressBarControl1.PerformStep();
                GP = new Geoprocessor();
                string R2Contour = m_MakeContoursFolder + "\\Contour.shp";
                k = 1;
                while (File.Exists(R2Contour))
                {
                    R2Contour = m_MakeContoursFolder + "\\Contour" + k.ToString() + ".shp";
                    k++;
                }
                int countContour = Directory.GetFiles(m_MakeContoursFolder, "Contour*").Length;
                if (countContour > 0)
                {
                    R2Contour = m_MakeContoursFolder + "\\Contour" + countContour.ToString() + ".shp";
                }

                if (DrawContours.SplineRasterToContour(GP, ContourRaster, R2Contour, douElevation) == false)
                    return false;
                this.progressBarControl1.PerformStep();

                string EvEContour = m_MakeContoursFolder + "\\EvEContour.shp";
                k = 1;
                while (File.Exists(EvEContour))
                {
                    EvEContour = m_MakeContoursFolder + "\\EvEContour" + k.ToString() + ".shp";
                    k++;
                }
                int countEvEContour = Directory.GetFiles(m_MakeContoursFolder, "EvEContour*").Length;
                if (countEvEContour > 0)
                {
                    EvEContour = m_MakeContoursFolder + "\\EvEContour" + countEvEContour.ToString() + ".shp";
                }
                if (DrawContours.FeaturesTo3D(GP, R2Contour, EvEContour) == false)
                    return false;
                this.progressBarControl1.PerformStep();

                //获得等值线Shp文件所在的工作空间 
                IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactory();
                IWorkspace sourceworkspace = workspaceFactory.OpenFromFile(m_MakeContoursFolder, 0);
                //裁切
                IPolygon bianjie = GetPolygon();

                //导入等值线Shp文件到数据库中
                DrawSpecialCommon drawspecial = new DrawSpecialCommon();
                string nameOfsourceFeatureClass = EvEContour.Substring(EvEContour.LastIndexOf("\\") + 1);
                nameOfsourceFeatureClass = nameOfsourceFeatureClass.Substring(0, nameOfsourceFeatureClass.LastIndexOf("."));
                bool Import = false;
                List<ziduan> list = new List<ziduan>();
                list.Add(new ziduan("BID", ""));
                list.Add(new ziduan("mingcheng", sLayerAliasName));
                list.Add(new ziduan("mcid", "0"));
                list.Add(new ziduan("date", DateTime.Now.ToString()));
                list.Add(new ziduan("type", CB_InterpolationMethod.Text));
                IFeatureLayer pFeatureLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, EditLayerName) as IFeatureLayer;
                string WhereClause = "mcid='" + "0" + "'";

                DataEditCommon.DeleteFeatureByWhereClause(pFeatureLayer, WhereClause);
                if (radioBtnKJ.Checked && bianjie != null)
                    Import = IntersectAll(DataEditCommon.GetFeatureClassByName(sourceworkspace, nameOfsourceFeatureClass), bianjie, list);
                else
                    Import = drawspecial.ShapeImportGDB(sourceworkspace, targetworkspace, nameOfsourceFeatureClass, nameOftargetFeatureClass, list);
                this.progressBarControl1.PerformStep();

                if (Import == false)
                {
                    MessageBox.Show(sLayerAliasName + "导入数据库失败！");
                    DataEditCommon.g_axTocControl.Update();
                    DataEditCommon.g_pAxMapControl.Refresh();
                    failInfo = sLayerAliasName;
                    return false;
                }

                //添加相应的渲染图
                IRasterLayer newRasterLayer = new RasterLayerClass();
                newRasterLayer.CreateFromFilePath(ContourRaster);
                newRasterLayer = IntersectRaster(newRasterLayer, bianjie);
                //newRasterLayer.CreateFromDataset(newRasterDs);
                newRasterLayer.Name = sRasterLayerAliasName;
                UsingRasterStretchColorRampRender(newRasterLayer);
                //groupLayer.Add(newRasterLayer as ILayer);
                int indexPosition = DataEditCommon.g_pAxMapControl.LayerCount;//GroupLayer序号

                //判断MapControl中是否存在该图层
                ILayer mLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, sRasterLayerAliasName);
                if (mLayer != null)
                {
                    DataEditCommon.g_pMyMapCtrl.Map.DeleteLayer(mLayer);
                    indexPosition--;
                }
                DataEditCommon.g_pAxMapControl.AddLayer(newRasterLayer as ILayer, indexPosition);//添加到最下面
                DataEditCommon.g_pAxMapControl.ActiveView.Extent = newRasterLayer.AreaOfInterest;
                DataEditCommon.g_axTocControl.Update();
                DataEditCommon.g_pAxMapControl.Refresh();
                this.progressBarControl1.PerformStep();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// 拉伸渲染raster图层
        /// </summary>
        /// <param name="pRLayer">raster图层</param>
        /// <remarks></remarks>
        public void UsingRasterStretchColorRampRender(IRasterLayer pRLayer)
        {
            //获得图层
            IRaster pRaster = default(IRaster);
            pRaster = pRLayer.Raster;

            //创建渲染并转换到栅格渲染
            IRasterStretchColorRampRenderer pStretchRen = default(IRasterStretchColorRampRenderer);
            pStretchRen = new RasterStretchColorRampRenderer();
            IRasterRenderer pRasRen = default(IRasterRenderer);
            pRasRen = (IRasterRenderer)pStretchRen;

            //栅格渲染赋值和更新
            pRasRen.Raster = pRaster;
            pRasRen.Update();

            //定义起止颜色
            IRgbColor pFromColor = new RgbColorClass();
            pFromColor.Red = 0;
            pFromColor.Green = 255;
            pFromColor.Blue = 0;

            IRgbColor pToColor = new RgbColorClass();
            pToColor.Red = 255;
            pToColor.Green = 0;
            pToColor.Blue = 0;

            //创建颜色条
            IAlgorithmicColorRamp pRamp = new AlgorithmicColorRamp();
            pRamp.Size = 255;
            pRamp.FromColor = pFromColor;
            pRamp.ToColor = pToColor;
            bool bOK;
            pRamp.CreateRamp(out bOK);

            //插入颜色条和选择渲染波段
            pStretchRen.BandIndex = 0;
            pStretchRen.ColorRamp = pRamp;

            //用新的设置更新渲染并赋值给图层
            pRasRen.Update();
            pRLayer.Renderer = (IRasterRenderer)pStretchRen;

            //释放内存
            pRLayer = null;
            pRaster = null;
            pStretchRen = null;
            pRasRen = null;
            pRamp = null;
            pToColor = null;
            pFromColor = null;
        }

        private void BT_Cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CB_InterpolationMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_InterpolationMethod.SelectedIndex == 1)
            {
                groupBox1.Enabled = true;

            }
            else
            {
                groupBox1.Enabled = false;
                CB_searchRadiusProp.SelectedIndex = 0;
                CB_semiVariogramProp.SelectedIndex = 0;
            }

            if (CB_InterpolationMethod.SelectedIndex == 0)
            {
                groupBox2.Enabled = true;

            }
            else
            {
                groupBox2.Enabled = false;
                CB_SplineType.SelectedIndex = 0;

            }


            if (CB_InterpolationMethod.SelectedIndex == 3)
            {
                groupBox3.Enabled = true;

            }
            else
            {
                groupBox3.Enabled = false;
                CB_ZFiled.SelectedIndex = 0;

            }

        }

        /// <summary>
        /// Check数据是否在矿界范围之内。
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool withIn(string file)
        {
            try
            {
                bool within = true;
                ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MINE_BOUNDARY);
                if (pLayer == null)
                {
                    MessageBox.Show("煤层矿界图层缺失！");
                    return false;
                }
                IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
                IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                IQueryFilter pFilter = new QueryFilterClass();
                pFilter.WhereClause = "layer='预警矿界'";
                IFeatureCursor pCursor = pFeatureClass.Search(pFilter, false);
                IFeature pFeature = pCursor.NextFeature();
                List<IGeometry> list = new List<IGeometry>();
                if (pFeature == null)
                    return false;
                ISegmentCollection pSegmentCollection = new PolygonClass();
                while (pFeature != null)
                {
                    list.Add(pFeature.Shape);
                    pFeature = pCursor.NextFeature();
                }
                IGeometry pgeoLine = MyMapHelp.GetGeoFromGeos(list);
                pSegmentCollection = pgeoLine as ISegmentCollection;
                IPolyline pPolyline = pSegmentCollection as IPolyline;

                IPolygon pPolygon = DataEditCommon.PolylineToPolygon(pPolyline);

                string[] liststr = File.ReadAllLines(file);
                list = new List<IGeometry>();
                IPoint pt = new PointClass();
                double x, y;
                for (int i = 0; i < liststr.Length; i++)
                {
                    pt = new PointClass();
                    if (!double.TryParse(liststr[i].Split(',')[0], out x) || !double.TryParse(liststr[i].Split(',')[1], out y))
                    {
                        MessageBox.Show("存在非数字的坐标，请检查！");
                        return false;
                    }
                    pt.X = x;
                    pt.Y = y;
                    list.Add(pt);
                }
                if (list.Count < 3)
                {
                    MessageBox.Show("离散点数据为空或小于三个，无法生成等值线！");
                    return false;
                }
                for (int i = 0; i < liststr.Length; i++)
                {
                    for (int j = 0; j < liststr.Length; j++)
                    {
                        if (liststr[i].Split(',')[0] == liststr[j].Split(',')[0] && liststr[i].Split(',')[1] == liststr[j].Split(',')[1] && liststr[i].Split(',')[2] == liststr[j].Split(',')[2] && i != j)
                        {
                            MessageBox.Show("存在重复点，请检查！");
                            return false;
                        }
                    }
                }
                List<IGeometry> listrt = MyMapHelp.withIn(pPolygon, list);
                if (listrt.Count > 0)
                {
                    within = false;
                    DialogResult dr = MessageBox.Show("存在超边界的坐标，是否立即查看？", "", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        string strlen = "";
                        for (int i = 0; i < listrt.Count; i++)
                        {
                            pt = listrt[i] as IPoint;
                            strlen += pt.X.ToString() + "," + pt.Y.ToString() + " \r\n";
                        }
                        string filename = Application.StartupPath + "\\ContentError.txt";
                        File.WriteAllText(filename, strlen);
                        Process.Start(filename);
                    }
                }
                return within;
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取矿界失败！" + ex.Message);
                return false;
            }
        }

        private IRasterLayer IntersectRaster(IRasterLayer rsterLayer, IPolygon polygon2)
        {
            //  获得与组合成的面相叠加的栅格
            if (radioBtnKJ.Checked && polygon2 != null)
            {
                IExtractionOp op = new RasterExtractionOpClass();
                IGeoDataset dataset = op.Polygon((IGeoDataset)rsterLayer.Raster, polygon2, true);
                IRasterLayer layer = new RasterLayerClass();
                layer.CreateFromRaster((IRaster)dataset);
                rsterLayer = layer;
            }
            return rsterLayer;
        }
        //获取矿界
        private IPolygon GetPolygon()
        {
            IPolygon polygon2 = null;
            ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MINE_BOUNDARY);
            IFeatureLayer polygonLayer = (IFeatureLayer)pLayer;

            IQueryFilter filter = new QueryFilterClass();
            filter.WhereClause = "layer = '预警矿界'";

            IFeatureLayer featureLayer = polygonLayer as IFeatureLayer;
            IFeatureCursor featureCursor = featureLayer.Search(filter, false);
            ITopologicalOperator2 topologicalOperator = null;

            //  获得矿界组合成面
            if (featureCursor != null)
            {
                ISegmentCollection polygon = new PolygonClass();

                IFeature feature = featureCursor.NextFeature();

                while (feature != null)
                {
                    polygon.AddSegmentCollection((ISegmentCollection)feature.Shape);
                    feature = featureCursor.NextFeature();
                }

                topologicalOperator = (ITopologicalOperator2)polygon;
                topologicalOperator.IsKnownSimple_2 = true;
                topologicalOperator.Simplify();

                polygon2 = (IPolygon)topologicalOperator;
            }
            return polygon2;
        }
        private bool IntersectAll(IFeatureClass lineLayer, IPolygon polygon2, List<ziduan> list)
        {
            try
            {
                if (radioBtnKJ.Checked && polygon2 != null)
                {
                    //  根据组合成的面裁剪压力等值线
                    SpatialFilterClass qfilter = new SpatialFilterClass();
                    qfilter.Geometry = polygon2;
                    qfilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

                    IFeatureCursor qfeatureCursor = lineLayer.Search(qfilter, false);

                    if (qfeatureCursor != null)
                    {
                        IFeature feature = qfeatureCursor.NextFeature();
                        IGeometryArray geometryArray = new GeometryArrayClass();
                        while (feature != null)
                        {
                            geometryArray.Add(feature.Shape);
                            feature = qfeatureCursor.NextFeature();
                        }

                        IGeometryServer2 geometryServer2 = new GeometryServerClass();
                        IGeometryArray geometryArray2 = geometryServer2.Intersect(polygon2.SpatialReference, geometryArray, polygon2);
                        //DataEditCommon.DeleteFeatureByWhereClause(lineLayer, "");
                        IFeatureLayer pFeatureLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, EditLayerName) as IFeatureLayer;
                        DataEditCommon.CreateFeature(pFeatureLayer.FeatureClass, geometryArray2, list);

                    }
                }
                return true;
            }
            catch
            { return false; }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtndata.Checked)
                panelLSD.Enabled = false;
            else
                panelLSD.Enabled = true;
        }
    }
}
