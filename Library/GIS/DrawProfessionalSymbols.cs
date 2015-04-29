using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using System.Data;
using System.Drawing;
using ESRI.ArcGIS.ADF;
using LibEntity;
using LibGeometry;
using System.Windows.Forms;
namespace GIS
{
   public class DrawProfessionalSymbols
    {
     

       /// <summary>
       /// 设置瓦斯压力点图层标注样式
       /// </summary>
       public void SettingGasPressureLabel(IFeatureLayer pFeatureLayer)
       {
            IGeoFeatureLayer pGeoFeatureLayer = pFeatureLayer as IGeoFeatureLayer;
	        pGeoFeatureLayer.AnnotationProperties.Clear();//必须执行，因为里面有一个默认的
	        IBasicOverposterLayerProperties pBasic = new BasicOverposterLayerPropertiesClass();
	        ILabelEngineLayerProperties pLableEngine = new LabelEngineLayerPropertiesClass();
            //ITextSymbol pTextSymbol = new TextSymbolClass();
            //pTextSymbol = (ITextSymbol)pStyleGalleryItem.Item;    
	        string pLable = "\"<UND>\"+ [GAS_PRESSURE_VALUE]  + \"</UND>\"+ vbNewLine + [COORDINATE_Z]+\"|\"+ [DEPTH]";
	        pLableEngine.Expression = pLable;
	        pLableEngine.IsExpressionSimple = true;
	        pBasic.NumLabelsOption = esriBasicNumLabelsOption.esriOneLabelPerShape;
	        pLableEngine.BasicOverposterLayerProperties = pBasic;
            //pLableEngine.Symbol = pTextSymbol;
	        pGeoFeatureLayer.AnnotationProperties.Add(pLableEngine as IAnnotateLayerProperties);
	        pGeoFeatureLayer.DisplayAnnotation = true;
            
       }
       /// <summary>
       /// 设置瓦斯含量点图层标注样式
       /// </summary>
       /// <param name="pFeatureLayer"></param>
       public void SettingGasContentPointLabel(IFeatureLayer pFeatureLayer)
        {
            IGeoFeatureLayer pGeoFeatureLayer = pFeatureLayer as IGeoFeatureLayer;
            pGeoFeatureLayer.AnnotationProperties.Clear();//必须执行，因为里面有一个默认的
            IBasicOverposterLayerProperties pBasic = new BasicOverposterLayerPropertiesClass();
            ILabelEngineLayerProperties pLableEngine = new LabelEngineLayerPropertiesClass();
            //ITextSymbol pTextSymbol = new TextSymbolClass();
            //pTextSymbol = (ITextSymbol)pStyleGalleryItem.Item;    
            string pLable = "\"<UND>\"+ [GAS_CONTENT_VALUE]  + \"</UND>\"+ vbNewLine + [COORDINATE_Z]+\"|\"+ [DEPTH]";
            pLableEngine.Expression = pLable;
            pLableEngine.IsExpressionSimple = true;
            pBasic.NumLabelsOption = esriBasicNumLabelsOption.esriOneLabelPerShape;
            pLableEngine.BasicOverposterLayerProperties = pBasic;
            //pLableEngine.Symbol = pTextSymbol;
            pGeoFeatureLayer.AnnotationProperties.Add(pLableEngine as IAnnotateLayerProperties);
            pGeoFeatureLayer.DisplayAnnotation = true;
        }
       /// <summary>
       /// 设置瓦斯突出点标注样式
       /// </summary>
       /// <param name="pFeatureLayer"></param>
       public void SettingGasOutstandingLabel(IFeatureLayer pFeatureLayer)
       {
           IGeoFeatureLayer pGeoFeatureLayer = pFeatureLayer as IGeoFeatureLayer;
           pGeoFeatureLayer.AnnotationProperties.Clear();//必须执行，因为里面有一个默认的
           IBasicOverposterLayerProperties pBasic = new BasicOverposterLayerPropertiesClass();
           ILabelEngineLayerProperties pLableEngine = new LabelEngineLayerPropertiesClass();
           //ITextSymbol pTextSymbol = new TextSymbolClass();
           //pTextSymbol = (ITextSymbol)pStyleGalleryItem.Item;    
           string pLable = "\"<UND>\"+ [OUTBURST_COAL_QUANTITY]+\"|\"+ [GAS_TOTAL]  + \"</UND>\"+ vbNewLine + [OUTBURST_TIME]";
           pLableEngine.Expression = pLable;
           pLableEngine.IsExpressionSimple = true;
           pBasic.NumLabelsOption = esriBasicNumLabelsOption.esriOneLabelPerShape;
           pLableEngine.BasicOverposterLayerProperties = pBasic;
           //pLableEngine.Symbol = pTextSymbol;
           pGeoFeatureLayer.AnnotationProperties.Add(pLableEngine as IAnnotateLayerProperties);
           pGeoFeatureLayer.DisplayAnnotation = true;
       }
       /// <summary>
       /// 设置瓦斯动力现象点标注样式
       /// </summary>
       /// <param name="pFeatureLayer"></param>
       public void SettingPowerPhenomenonLabel(IFeatureLayer pFeatureLayer)
       {
           IGeoFeatureLayer pGeoFeatureLayer = pFeatureLayer as IGeoFeatureLayer;
           pGeoFeatureLayer.AnnotationProperties.Clear();//必须执行，因为里面有一个默认的
           IBasicOverposterLayerProperties pBasic = new BasicOverposterLayerPropertiesClass();
           ILabelEngineLayerProperties pLableEngine = new LabelEngineLayerPropertiesClass();
           //ITextSymbol pTextSymbol = new TextSymbolClass();
           //pTextSymbol = (ITextSymbol)pStyleGalleryItem.Item;    
           string pLable = "\"<UND>\"+ [OUTBURST_ROCK_QUANTITY]+\"|\"+ [GUSH_GAS_QUANTITY]  + \"</UND>\"+ vbNewLine + [COORDINATE_Z]+\"|\"+ [OCCURRENCE_TIME]";
           pLableEngine.Expression = pLable;
           pLableEngine.IsExpressionSimple = true;
           pBasic.NumLabelsOption = esriBasicNumLabelsOption.esriOneLabelPerShape;
           pLableEngine.BasicOverposterLayerProperties = pBasic;
           //pLableEngine.Symbol = pTextSymbol;
           pGeoFeatureLayer.AnnotationProperties.Add(pLableEngine as IAnnotateLayerProperties);
           pGeoFeatureLayer.DisplayAnnotation = true;
       }
       /// <summary>
       /// 设置回采工作面瓦斯涌出量标注样式
       /// </summary>
       /// <param name="pFeatureLayer"></param>
       public void SettingStopeWorkingFaceGasGushQuantityLabel(IFeatureLayer pFeatureLayer)
       {
           IGeoFeatureLayer pGeoFeatureLayer = pFeatureLayer as IGeoFeatureLayer;
           pGeoFeatureLayer.AnnotationProperties.Clear();//必须执行，因为里面有一个默认的
           IBasicOverposterLayerProperties pBasic = new BasicOverposterLayerPropertiesClass();
           ILabelEngineLayerProperties pLableEngine = new LabelEngineLayerPropertiesClass();
           //ITextSymbol pTextSymbol = new TextSymbolClass();
           //pTextSymbol = (ITextSymbol)pStyleGalleryItem.Item;    
           string pLable = "\"<UND>\"+ [ABSOLUTE_GAS_GUSH_QUANTITY]+\"|\"+ [RELATIVE_GAS_GUSH_QUANTITY]  + \"</UND>\"+ vbNewLine + [WORKING_FACE_DAY_OUTPUT]+\"|\"+ [STOPE_DATE]";
           pLableEngine.Expression = pLable;
           pLableEngine.IsExpressionSimple = true;
           pBasic.NumLabelsOption = esriBasicNumLabelsOption.esriOneLabelPerShape;
           pLableEngine.BasicOverposterLayerProperties = pBasic;
           //pLableEngine.Symbol = pTextSymbol;
           pGeoFeatureLayer.AnnotationProperties.Add(pLableEngine as IAnnotateLayerProperties);
           pGeoFeatureLayer.DisplayAnnotation = true;
       }
   //   public void DrawPowerPhenomenonGE(IFeatureLayer editlayer, PowerPhenomenonEntity pe )
   //{
   //    IFeature f;
   //    IPoint p;
   //    //定义一个地物类,把要编辑的图层转化为定义的地物类
   //    IFeatureClass fc = editlayer.FeatureClass;
   //    //先定义一个编辑的工作空间,然后把转化为数据集,最后转化为编辑工作空间,
   //    IWorkspaceEdit w = (fc as IDataset).Workspace as IWorkspaceEdit;
   //    //开始事务操作
   //    w.StartEditing(false);
   //    //开始编辑
   //    w.StartEditOperation();
   //    //创建一个地物
   //    f = fc.CreateFeature();
   //    p = new PointClass();
   //    //设置点的坐标
   //    p.PutCoords(Convert.ToDouble(pe.CoordinateX), Convert.ToDouble(pe.CoordinateY));
   //    //确定图形类型
   //    f.Shape = p;
   //    int num3;
   //    //num3 = editlayer.FeatureClass.Fields.FindField("PRIMARY_KEY");
   //    //f.set_Value(num3, pe.PrimaryKey);
   //    num3 = editlayer.FeatureClass.Fields.FindField("COORDINATE_X");
   //    f.set_Value(num3, pe.CoordinateX);
   //    num3 = editlayer.FeatureClass.Fields.FindField("COORDINATE_Y");
   //    f.set_Value(num3, pe.CoordinateY);
   //    num3 = editlayer.FeatureClass.Fields.FindField("COORDINATE_Z");
   //    f.set_Value(num3, pe.CoordinateZ);
   //    num3 = editlayer.FeatureClass.Fields.FindField("OUTBURST_ROCK_QUANTITY");
   //    f.set_Value(num3, pe.OutburstRockQuantity);
   //    num3 = editlayer.FeatureClass.Fields.FindField("GUSH_GAS_QUANTITY");
   //    f.set_Value(num3, pe.GushGasQuantity);
   //    num3 = editlayer.FeatureClass.Fields.FindField("OCCURRENCE_TIME");
   //    f.set_Value(num3, pe.OccurrenceTime);
   //    //保存地物
   //    f.Store();
   //    ICharacterMarkerSymbol pMarkerSymbol;
   //    pMarkerSymbol = new CharacterMarkerSymbolClass();
   //    stdole.IFontDisp stdFont = new stdole.StdFontClass() as stdole.IFontDisp;
   //    stdFont.Name = "mySymbols";
   //    stdFont.Size = 100;
   //    pMarkerSymbol.Font = stdFont;
   //    pMarkerSymbol.CharacterIndex = 2;
   //    //Symbol颜色
   //    pMarkerSymbol.Color = getRGB(0, 0, 0);
   //    //Symbol旋转角度            
   //    pMarkerSymbol.Angle = 0;
   //    //Symbol大小
   //    pMarkerSymbol.Size = 48;
   //    RenderfeatureLayer(editlayer, pMarkerSymbol as ISymbol);
   //    //结束编辑
   //    w.StopEditOperation();
   //    //结束事务操作
   //    w.StopEditing(true);
   //}
       ///// <summary>
       ///// 绘制瓦斯突出点图层
       ///// </summary>
       ///// <param name="editlayer"></param>
       ///// <param name="ge"></param>
       //public void DrawGasOutstandingGE(IFeatureLayer editlayer, GasOutburstEntity ge)
       //{
       //    IFeature f;
       //    IPoint p;
       //    //定义一个地物类,把要编辑的图层转化为定义的地物类
       //    IFeatureClass fc = editlayer.FeatureClass;
       //    //先定义一个编辑的工作空间,然后把转化为数据集,最后转化为编辑工作空间,
       //    IWorkspaceEdit w = (fc as IDataset).Workspace as IWorkspaceEdit;
       //    //开始事务操作
       //    w.StartEditing(false);
       //    //开始编辑
       //    w.StartEditOperation();
       //    //创建一个地物
       //    f = fc.CreateFeature();
       //    p = new PointClass();
       //    //设置点的坐标
       //    p.PutCoords(Convert.ToDouble(ge.CoordinateX), Convert.ToDouble(ge.CoordinateY));
       //    try
       //    {
       //        //确定图形类型
       //        f.Shape = p;
       //        int num3;
       //        //num3 = editlayer.FeatureClass.Fields.FindField("PRIMARY_KEY");
       //        //f.set_Value(num3, ge.PrimaryKey);
       //        num3 = editlayer.FeatureClass.Fields.FindField("COORDINATE_X");
       //        f.set_Value(num3, ge.CoordinateX);
       //        num3 = editlayer.FeatureClass.Fields.FindField("COORDINATE_Y");
       //        f.set_Value(num3, ge.CoordinateY);
       //        num3 = editlayer.FeatureClass.Fields.FindField("COORDINATE_Z");
       //        f.set_Value(num3, ge.CoordinateZ);
       //        num3 = editlayer.FeatureClass.Fields.FindField("OUTBURST_COAL_QUANTITY");
       //        f.set_Value(num3, ge.OutburstCoalQuantity);
       //        num3 = editlayer.FeatureClass.Fields.FindField("GAS_TOTAL");
       //        f.set_Value(num3, ge.GasTotal);
       //        num3 = editlayer.FeatureClass.Fields.FindField("OUTBURST_TIME");
       //        f.set_Value(num3, ge.OutburstTime);
       //        //保存地物
       //        f.Store();
       //    }
       //    catch (Exception e)
       //    {
       //        MessageBox.Show(e.Message);
       //    }
       //    ICharacterMarkerSymbol pMarkerSymbol;
       //    pMarkerSymbol = new CharacterMarkerSymbolClass();
       //    stdole.IFontDisp stdFont = new stdole.StdFontClass() as stdole.IFontDisp;
       //    stdFont.Name = "mySymbols";
       //    stdFont.Size = 100;
       //    pMarkerSymbol.Font = stdFont;
       //    pMarkerSymbol.CharacterIndex = 1;
       //    //Symbol颜色
       //    pMarkerSymbol.Color = getRGB(0, 0, 0);
       //    //Symbol旋转角度            
       //    pMarkerSymbol.Angle = 0;
       //    //Symbol大小
       //    pMarkerSymbol.Size = 48;
       //    RenderfeatureLayer(editlayer, pMarkerSymbol as ISymbol);
       //    //结束编辑
       //    w.StopEditOperation();
       //    //结束事务操作
       //    w.StopEditing(true);
       //}

       public void DrawStopeWorkingFaceGasGushQuantity(IFeatureLayer editlayer, GasGushQuantityEntity ge)
       {
           IFeature f;
           IPoint p;
           //定义一个地物类,把要编辑的图层转化为定义的地物类
           IFeatureClass fc = editlayer.FeatureClass;
           //先定义一个编辑的工作空间,然后把转化为数据集,最后转化为编辑工作空间,
           IWorkspaceEdit w = (fc as IDataset).Workspace as IWorkspaceEdit;
           //开始事务操作
           w.StartEditing(false);
           //开始编辑
           w.StartEditOperation();
           //创建一个地物
           f = fc.CreateFeature();
           p = new PointClass();
           //设置点的坐标
           p.PutCoords(Convert.ToDouble(ge.CoordinateX), Convert.ToDouble(ge.CoordinateY));
           try
           {
               //确定图形类型
               f.Shape = p;
               int num3;
               //num3 = editlayer.FeatureClass.Fields.FindField("PRIMARY_KEY");
               //f.set_Value(num3, ge.PrimaryKey);
               num3 = editlayer.FeatureClass.Fields.FindField("COORDINATE_X");
               f.set_Value(num3, ge.CoordinateX);
               num3 = editlayer.FeatureClass.Fields.FindField("COORDINATE_Y");
               f.set_Value(num3, ge.CoordinateY);
               num3 = editlayer.FeatureClass.Fields.FindField("COORDINATE_Z");
               f.set_Value(num3, ge.CoordinateZ);
               num3 = editlayer.FeatureClass.Fields.FindField("ABSOLUTE_GAS_GUSH_QUANTITY");
               f.set_Value(num3, ge.AbsoluteGasGushQuantity);
               num3 = editlayer.FeatureClass.Fields.FindField("RELATIVE_GAS_GUSH_QUANTITY");
               f.set_Value(num3, ge.RelativeGasGushQuantity);
               num3 = editlayer.FeatureClass.Fields.FindField("WORKING_FACE_DAY_OUTPUT");
               f.set_Value(num3, ge.WorkingFaceDayOutput);
               num3 = editlayer.FeatureClass.Fields.FindField("STOPE_DATE");
               f.set_Value(num3, ge.StopeDate);
               //保存地物
               f.Store();
           }
           catch (Exception e)
           {
               MessageBox.Show(e.Message);
           }
           //ICharacterMarkerSymbol pMarkerSymbol;
           //pMarkerSymbol = new CharacterMarkerSymbolClass();
           //stdole.IFontDisp stdFont = new stdole.StdFontClass() as stdole.IFontDisp;
           //stdFont.Name = "mySymbols";
           //stdFont.Size = 100;
           //pMarkerSymbol.Font = stdFont;
           //pMarkerSymbol.CharacterIndex = 1;
           ////Symbol颜色
           //pMarkerSymbol.Color = getRGB(0, 0, 0);
           ////Symbol旋转角度            
           //pMarkerSymbol.Angle = 0;
           ////Symbol大小
           //pMarkerSymbol.Size = 48;
           //RenderfeatureLayer(editlayer, pMarkerSymbol as ISymbol);
           //结束编辑
           w.StopEditOperation();
           //结束事务操作
           w.StopEditing(true);
       }
      /// <summary>
      /// 绘制瓦斯含量点
      /// </summary>
      /// <param name="editlayer"></param>
      /// <param name="ge"></param>
       public void DrawGasContentPointGE(IFeatureLayer editlayer, GasContentEntity ge)
       {
           IFeature f;
           IPoint p;
           //定义一个地物类,把要编辑的图层转化为定义的地物类
           IFeatureClass fc = editlayer.FeatureClass;
           //先定义一个编辑的工作空间,然后把转化为数据集,最后转化为编辑工作空间,
           IWorkspaceEdit w = (fc as IDataset).Workspace as IWorkspaceEdit;
           //开始事务操作
           w.StartEditing(false);
           //开始编辑
           w.StartEditOperation();
           //创建一个地物
           f = fc.CreateFeature();
           p = new PointClass();
           //设置点的坐标
           p.PutCoords(Convert.ToDouble(ge.CoordinateX), Convert.ToDouble(ge.CoordinateY));
           try
           {
               //确定图形类型
               f.Shape = p;
               int num3;
               //num3 = editlayer.FeatureClass.Fields.FindField("PRIMARY_KEY");
               //f.set_Value(num3, ge.PrimaryKey);
               num3 = editlayer.FeatureClass.Fields.FindField("COORDINATE_X");
               f.set_Value(num3, ge.CoordinateX);
               num3 = editlayer.FeatureClass.Fields.FindField("COORDINATE_Y");
               f.set_Value(num3, ge.CoordinateY);
               num3 = editlayer.FeatureClass.Fields.FindField("COORDINATE_Z");
               f.set_Value(num3, ge.CoordinateZ);
               num3 = editlayer.FeatureClass.Fields.FindField("DEPTH");
               f.set_Value(num3, ge.Depth);
               num3 = editlayer.FeatureClass.Fields.FindField("GAS_CONTENT_VALUE");
               f.set_Value(num3, ge.GasContentValue);
               num3 = editlayer.FeatureClass.Fields.FindField("MEASURE_DATE_TIME");
               f.set_Value(num3, ge.MeasureDateTime);
               //保存地物
               f.Store();
           }
           catch (Exception e)
           {
               MessageBox.Show(e.Message);
           }
           ICharacterMarkerSymbol pMarkerSymbol;
           pMarkerSymbol = new CharacterMarkerSymbolClass();
           stdole.IFontDisp stdFont = new stdole.StdFontClass() as stdole.IFontDisp;
           stdFont.Name = "ESRI Hazardous Materials";
           stdFont.Size = 48;
           pMarkerSymbol.Font = stdFont;
           pMarkerSymbol.CharacterIndex = 216;
           //Symbol颜色
           pMarkerSymbol.Color = getRGB(0, 0, 0);
           //Symbol旋转角度            
           pMarkerSymbol.Angle = 0;
           //Symbol大小
           pMarkerSymbol.Size = 48;
           RenderfeatureLayer(editlayer, pMarkerSymbol as ISymbol);
           //结束编辑
           w.StopEditOperation();
           //结束事务操作
           w.StopEditing(true);
       }
       /// <summary>
       /// 绘制瓦斯压力点符号
       /// ge绘制瓦斯压力点的数据结构
        /// editlayer指定需要在哪个图层添加该瓦斯压力点
        /// 2013-12-14
        /// lntu_GISer1
       /// </summary>
       /// <param name="ge"></param>
       /// <param name="pfeaLayer"></param>
       public void DrawGasPressureGE(IFeatureLayer editlayer,GasPressureEntity ge)
       {
           IFeature f;
           IPoint p;
           //定义一个地物类,把要编辑的图层转化为定义的地物类
           IFeatureClass fc = editlayer.FeatureClass;
           //先定义一个编辑的工作空间,然后把转化为数据集,最后转化为编辑工作空间,
           IWorkspaceEdit w = (fc as IDataset).Workspace as IWorkspaceEdit;
           //开始事务操作
           w.StartEditing(false);
           //开始编辑
           w.StartEditOperation();
           //创建一个地物
           f = fc.CreateFeature();
           p = new PointClass();
           //设置点的坐标
           p.PutCoords(Convert.ToDouble(ge.CoordinateX), Convert.ToDouble(ge.CoordinateY));
           try
           {
               //确定图形类型
               f.Shape = p;
               int num3;
               //num3 = editlayer.FeatureClass.Fields.FindField("PRIMARY_KEY");
               //f.set_Value(num3, ge.PrimaryKey);
               num3 = editlayer.FeatureClass.Fields.FindField("COORDINATE_X");
               f.set_Value(num3, ge.CoordinateX);
               num3 = editlayer.FeatureClass.Fields.FindField("COORDINATE_Y");
               f.set_Value(num3, ge.CoordinateY);
               num3 = editlayer.FeatureClass.Fields.FindField("COORDINATE_Z");
               f.set_Value(num3, ge.CoordinateZ);
               num3 = editlayer.FeatureClass.Fields.FindField("DEPTH");
               f.set_Value(num3, ge.Depth);
               num3 = editlayer.FeatureClass.Fields.FindField("GAS_PRESSURE_VALUE");
               f.set_Value(num3, ge.GasPressureValue);
               num3 = editlayer.FeatureClass.Fields.FindField("MEASURE_DATE_TIME");
               f.set_Value(num3, ge.MeasureDateTime);
               //保存地物
               f.Store();
               ICharacterMarkerSymbol pMarkerSymbol;
               pMarkerSymbol = new CharacterMarkerSymbolClass();
               stdole.IFontDisp stdFont = new stdole.StdFontClass() as stdole.IFontDisp;
               stdFont.Name = "ESRI Caves 3";
               stdFont.Size = 48;
               pMarkerSymbol.Font = stdFont;
               pMarkerSymbol.CharacterIndex = 107;
               //Symbol颜色
               pMarkerSymbol.Color = getRGB(0, 0, 0);
               //Symbol旋转角度            
               pMarkerSymbol.Angle = 0;
               //Symbol大小
               pMarkerSymbol.Size = 48;
               RenderfeatureLayer(editlayer, pMarkerSymbol as ISymbol);
           }
           catch (Exception e)
           {
               MessageBox.Show(e.Message);
           }
           //结束编辑
           w.StopEditOperation();
           //结束事务操作
           w.StopEditing(true);
       }

       private IRgbColor getRGB(int r, int g, int b)
       {
           IRgbColor pColor;
           pColor = new RgbColorClass();
           pColor.Red = r;
           pColor.Green = g;
           pColor.Blue = b;
           return pColor;
       }
       /// <summary>
       /// 渲染符号的样式为瓦斯压力点
       /// </summary>
       /// <param name="pfeature"></param>
       /// <param name="ge"></param>
       private void RenderfeatureLayer(IFeatureLayer featureLayer, ISymbol layerSymbols)
       {
           ISimpleRenderer pRenderer=new SimpleRendererClass(); 
           pRenderer.Symbol=layerSymbols;
           (featureLayer as IGeoFeatureLayer).Renderer = pRenderer as IFeatureRenderer;//渲染图层;  
       }
       /// <summary>
       /// 根据传入的左邦点数组和右邦点数组生成巷道
       /// </summary>
       /// <param name="tunnel"></param>
       /// <param name="pts"></param>
       /// <param name="editlayer"></param>
       public void DrawHuDong(TunnelEntity tunnel, WirePointInfoEntity[] pts, IFeatureLayer editlayer)
       {
           IPoint point = new PointClass();
           IPointCollection4 pointCollection = new PolygonClass();
           TunnelPointsCalculation TPC = new TunnelPointsCalculation();
           Vector3_DW[] lstLeftBtmVertices = null;
           Vector3_DW[] lstRightBtmVertices = null;
           TPC.CalcLeftAndRightVertics(pts, ref lstLeftBtmVertices,ref lstRightBtmVertices);
           for (int intI = 0; intI < lstLeftBtmVertices.Length; intI++)
           {
               point.X = lstLeftBtmVertices[intI].X;
               point.Y = lstLeftBtmVertices[intI].Y;
               point.Z = lstLeftBtmVertices[intI].Z;
               pointCollection.AddPoint(point);
           }
           for (int intI = lstRightBtmVertices.Length; intI >=0; intI--)
           {
               point.X = lstRightBtmVertices[intI].X;
               point.Y = lstRightBtmVertices[intI].Y;
               point.Z = lstRightBtmVertices[intI].Z;
               pointCollection.AddPoint(point);
           }
          
           IFeature f;
           IGeometryCollection polygon;
           //定义一个地物类,把要编辑的图层转化为定义的地物类
           IFeatureClass fc = editlayer.FeatureClass;
           //先定义一个编辑的工作空间,然后把转化为数据集,最后转化为编辑工作空间,
           IWorkspaceEdit w = (fc as IDataset).Workspace as IWorkspaceEdit;
           //开始事务操作
           w.StartEditing(false);
           //开始编辑
           w.StartEditOperation();
           //创建一个地物
          
           //绘制巷道


           polygon = new PolygonClass();
           IGeometryCollection pGeoColl = pointCollection as IGeometryCollection;
           ISegmentCollection pRing = new RingClass();
           pRing.AddSegmentCollection(pGeoColl as ISegmentCollection);

           polygon.AddGeometry(pRing as IGeometry);
           IPolygon polyGonGeo = polygon as IPolygon;
           polyGonGeo.SimplifyPreserveFromTo();  
           f = fc.CreateFeature();
           //polygon.PutCoords(Convert.ToDouble(ge.CoordinateX), Convert.ToDouble(ge.CoordinateY));
           //确定图形类型
           f.Shape = (IGeometry)polyGonGeo;

           //给巷道赋属性值
           int num3;
           //num3 = editlayer.FeatureClass.Fields.FindField("OBJECTID");
           //f.set_Value(num3, tunnel.TunnelID);
           num3 = editlayer.FeatureClass.Fields.FindField("MINE_NAME");
           f.set_Value(num3, tunnel.MineName);
           num3 = editlayer.FeatureClass.Fields.FindField("HORIZONTAL");
           f.set_Value(num3, tunnel.HorizontalName);
           num3 = editlayer.FeatureClass.Fields.FindField("MINING_AREA");
           f.set_Value(num3, tunnel.MiningAreaName);
           num3 = editlayer.FeatureClass.Fields.FindField("WORKING_FACE");
           f.set_Value(num3, tunnel.WorkingFaceName);
           num3 = editlayer.FeatureClass.Fields.FindField("TUNNEL_NAME");
           f.set_Value(num3, tunnel.TunnelName);
           num3 = editlayer.FeatureClass.Fields.FindField("SUPPORT_PATTERN");
           f.set_Value(num3, tunnel.TunnelSupportPattern);
           num3 = editlayer.FeatureClass.Fields.FindField("LITHOLOGY_ID");
           f.set_Value(num3, tunnel.TunnelLithologyID);
           num3 = editlayer.FeatureClass.Fields.FindField("FAULTAGETYPE");
           f.set_Value(num3, tunnel.TunnelSectionType);
           num3 = editlayer.FeatureClass.Fields.FindField("PARAM");
           f.set_Value(num3, tunnel.TunnelParam);
           num3 = editlayer.FeatureClass.Fields.FindField("DESIGNLENGTH");
           f.set_Value(num3, tunnel.TunnelDesignLength);
           //num3 = editlayer.FeatureClass.Fields.FindField("RULE_CODE");
           //f.set_Value(num3, tunnel.r);
           //保存地物
           f.Store();
          //渲染巷道样式
           ISimpleFillSymbol pFillSymbol;
           pFillSymbol = new SimpleFillSymbolClass();
           pFillSymbol.Color = getRGB(60, 100, 50);
           pFillSymbol.Outline.Color = getRGB(60, 100, 50);
           pFillSymbol.Outline.Width = 1;
           pFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;  
           RenderfeatureLayer(editlayer, pFillSymbol as ISymbol);
           //结束编辑
           w.StopEditOperation();
           //结束事务操作
           w.StopEditing(true);
       }
       
    }
}
