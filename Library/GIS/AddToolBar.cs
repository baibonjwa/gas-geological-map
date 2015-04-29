using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GIS.Common;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace GIS
{
    public class AddToolBar
    {
        public static void Addtool(AxMapControl axMapControl, IMapControl3 mapControl, IToolbarControl toolbarControl, IWorkspace workSpace)
        {
            //全局变量赋值
            DataEditCommon.g_tbCtlEdit = toolbarControl;
            DataEditCommon.g_pMyMapCtrl = mapControl;
            DataEditCommon.g_pCurrentWorkSpace = workSpace;
            DataEditCommon.g_pAxMapControl = axMapControl;
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.GraphicEdit.UndoEdit(), 0, -1, true, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.GraphicEdit.RedoEdit(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.AttributeQueryEdit(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.MeasureDistance(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.MeasureArea(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.GraphicEdit.FeatureSelect(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.GraphicEdit.FeatureClearSelect(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.SaveEdit(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);

            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.View.FixedZoomInCommand(), 0, -1, true, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.View.FixedZoomOutCommand(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.View.ZoomInTool(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.View.ZoomOutTool(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.View.ExtentBackCommand(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.View.ExtentForwardCommand(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.View.PanTool(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.View.ZoomToTool(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.View.RefreshViewCommand(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.View.FullExtentTool(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.View.MapRotateTool(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            //Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.View.ToolbarLocalView(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.View.LocalView(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.View.ClearView(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);

            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.RotateTool(), 0, -1, true, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.GraphicModify.FeatureMoveEdit(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.MirrorFeature(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.GraphicModify.EditCopyCommand(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.GraphicModify.EditCutCommand(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.GraphicModify.EditPasteCommand(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.GraphicModify.DeleteFeature(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.ExtendTool(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.TrimLineTool(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);


            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.BasicGraphic.LayersList(), 0, -1, true, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.BasicGraphic.AddPoint(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.AddStraightFeatureLine(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.BasicGraphic.AddFeatureLine(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.AddBezierCurve(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.AddRectangle(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.AddText(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.AddCircle(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.AddArc(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.AddEllipse(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            Common.DataEditCommon.g_tbCtlEdit.AddItem(new GIS.BasicGraphic.AddPolygon(), 0, -1, false, 0, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
        }
    }
}
