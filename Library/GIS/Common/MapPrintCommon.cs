using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Controls;
using System.Drawing;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace GIS.Common
{
    [Guid("a38549fe-8c1a-4911-b2b8-2760ced7da05")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.Common.MapPrintCommon")]
    public class MapPrintCommon
    {
        public static AxPageLayoutControl g_axPageLayoutControl;
        public static AxToolbarControl g_axToolbarControl;


        public static void TextElementAdd(IPoint point, String content, System.Drawing.Font font, Color color)
        {
            ESRI.ArcGIS.Carto.ITextElement textElement = new TextElementClass();
            textElement.Text = content;

            ESRI.ArcGIS.Display.IRgbColor rgbColor = ColorToRgbColor(color);
            ITextSymbol textSymbol = SetUpTextSymbol(font, rgbColor);
            textElement.Symbol = textSymbol;
            IElement element = textElement as IElement;
            element.Geometry = point;

            IGraphicsContainer graphicsContainer = g_axPageLayoutControl.PageLayout as IGraphicsContainer;
            
            graphicsContainer.AddElement(element, 0);
            
            //IGraphicsContainerSelect pGraphicsContainerSelect = g_axPageLayoutControl.PageLayout as IGraphicsContainerSelect;
            //pGraphicsContainerSelect.UnselectAllElements();
            //pGraphicsContainerSelect.SelectElement(element);
            g_axPageLayoutControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, element, null);
        }
        public static void TextElementAdd(IPoint point, String content, System.Drawing.Font font, Color color, ref ITextElement textElementReturn)
        {
            ESRI.ArcGIS.Carto.ITextElement textElement = new TextElementClass();
            textElement.Text = content;

            ESRI.ArcGIS.Display.IRgbColor rgbColor = ColorToRgbColor(color);
            ITextSymbol textSymbol = SetUpTextSymbol(font, rgbColor);
            textElement.Symbol = textSymbol;
            IElement element = textElement as IElement;
            element.Geometry = point;

            IGraphicsContainer graphicsContainer = g_axPageLayoutControl.PageLayout as IGraphicsContainer;
            graphicsContainer.AddElement(element, 0);
            g_axPageLayoutControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            textElementReturn = textElement;


        }

        private static ITextSymbol SetUpTextSymbol(System.Drawing.Font font, IRgbColor rgbColor)
        {
            ITextSymbol textSymbol = new TextSymbolClass();
            stdole.IFontDisp fontDisp = ESRI.ArcGIS.ADF.COMSupport.OLE.GetIFontDispFromFont(font) as stdole.IFontDisp;
            textSymbol.Font = fontDisp;
            textSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
            textSymbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
            textSymbol.Angle = 0;
            textSymbol.Color = rgbColor;
            return textSymbol;
        }

        private static IRgbColor ColorToRgbColor(Color color)
        {
            IRgbColor rgbColor = new RgbColorClass();
            rgbColor.Blue = color.B;
            rgbColor.Green = color.G;
            rgbColor.Red = color.R;
            rgbColor.Transparency = color.A;
            return rgbColor;
        }

        internal static void TextElementUpdate(ITextElement textElement, String content, System.Drawing.Font font, Color color)
        {
            //ESRI.ArcGIS.Carto.ITextElement textElement = new TextElementClass();
            textElement.Text = content;

            ESRI.ArcGIS.Display.IRgbColor rgbColor = ColorToRgbColor(color);
            ITextSymbol textSymbol = SetUpTextSymbol(font, rgbColor);
            textElement.Symbol = textSymbol;
            IElement element = textElement as IElement;
            //element.Geometry = point;

            IGraphicsContainer graphicsContainer = g_axPageLayoutControl.PageLayout as IGraphicsContainer;
            graphicsContainer.UpdateElement(element);
            g_axPageLayoutControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }


        internal static ESRI.ArcGIS.SystemUI.ITool SetControlsSelectCommand()
        {
            for (int i = 0; i < g_axToolbarControl.Count; i++)
            {
                if (g_axToolbarControl.GetItem(i).Command.Name == "ControlToolsGraphicElement_SelectTool")
                {
                    return g_axToolbarControl.GetItem(i).Command as ESRI.ArcGIS.SystemUI.ITool;
                }
            }
            return null;
        }

        internal static void GraphicsContainClearSelection()
        {
            IGraphicsContainerSelect graphicContainerSelect = g_axPageLayoutControl.PageLayout as IGraphicsContainerSelect;
            graphicContainerSelect.UnselectAllElements();
            

        }

        internal static string GetMapUnits()
        {
            switch (g_axPageLayoutControl.ActiveView.FocusMap.MapUnits)
            { 
                case ESRI.ArcGIS.esriSystem.esriUnits.esriCentimeters:
                    return "厘米";
                case ESRI.ArcGIS.esriSystem.esriUnits.esriDecimalDegrees:
                    return "位";
                case ESRI.ArcGIS.esriSystem.esriUnits.esriDecimeters:
                    return "分米";
                case ESRI.ArcGIS.esriSystem.esriUnits.esriFeet:
                    return "英尺";
                case ESRI.ArcGIS.esriSystem.esriUnits.esriInches:
                    return "英寸";
                case ESRI.ArcGIS.esriSystem.esriUnits.esriKilometers:
                    return "千米";
                case ESRI.ArcGIS.esriSystem.esriUnits.esriMeters:
                    return "米";
                case ESRI.ArcGIS.esriSystem.esriUnits.esriMiles:
                    return "英里";
                case ESRI.ArcGIS.esriSystem.esriUnits.esriMillimeters:
                    return "毫米";
                case ESRI.ArcGIS.esriSystem.esriUnits.esriNauticalMiles:
                    return "海里";
                case ESRI.ArcGIS.esriSystem.esriUnits.esriPoints:
                    return "点";
                case ESRI.ArcGIS.esriSystem.esriUnits.esriUnitsLast:
                    return "内部用单位";
                case ESRI.ArcGIS.esriSystem.esriUnits.esriYards:
                    return "厘米";
                default:
                    return "未知单位";
                   
            }
            
        }
    }
}
