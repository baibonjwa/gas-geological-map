using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using ESRI.ArcGIS.Geometry;

namespace GIS.Common
{
    public class SymbolHelper
    {
        // Methods
        public static IFillSymbol CreateFillSymbol(Color fillColor, Color outlineColor)
        {
            SimpleFillSymbolClass class2 = new SimpleFillSymbolClass();
            class2.Style = esriSimpleFillStyle.esriSFSSolid;
            class2.Color = ColorHelper.CreateColor(fillColor);
            ISimpleLineSymbol symbol = new SimpleLineSymbolClass();
            symbol.Style = esriSimpleLineStyle.esriSLSSolid;
            symbol.Color = ColorHelper.CreateColor(outlineColor);
            symbol.Width = 1.0;
            class2.Outline = symbol;
            return class2;
        }

        public static IFillSymbol CreateFillSymbol(Color fillColor, esriSimpleFillStyle eFillStyle, ISimpleLineSymbol aOutline)
        {
            SimpleFillSymbolClass class2 = new SimpleFillSymbolClass();
            class2.Style = eFillStyle;
            class2.Color = ColorHelper.CreateColor(fillColor);
            class2.Outline = aOutline;
            return class2;
        }

        public static IFillSymbol CreateFillSymbol(Color fillColor, esriSimpleFillStyle eFillStyle, Color outlineColor, double outlineWidth, esriSimpleLineStyle outlineStyle)
        {
            SimpleFillSymbolClass class2 = new SimpleFillSymbolClass();
            class2.Style = eFillStyle;
            class2.Color = ColorHelper.CreateColor(fillColor);
            ISimpleLineSymbol symbol = new SimpleLineSymbolClass();
            symbol.Style = outlineStyle;
            symbol.Color = ColorHelper.CreateColor(outlineColor);
            symbol.Width = outlineWidth;
            class2.Outline = symbol;
            return class2;
        }

        public static IFontDisp CreateFont(string pFontName, float pSize)
        {
            StdFontClass class2 = new StdFontClass();
            class2.Name = pFontName;
            class2.Size = Convert.ToDecimal(pSize);
            class2.Bold = false;
            class2.Italic = false;
            class2.Underline = false;
            class2.Strikethrough = false;
            return (class2 as IFontDisp);
        }

        public static IFontDisp CreateFont(string pFontName, float pSize, bool pBold, bool pItalic, bool pUnderline, bool pStroke)
        {
            StdFontClass class2 = new StdFontClass();
            class2.Name = pFontName;
            class2.Size = Convert.ToDecimal(pSize);
            class2.Bold = pBold;
            class2.Italic = pItalic;
            class2.Underline = pUnderline;
            class2.Strikethrough = pStroke;
            return (class2 as IFontDisp);
        }

        private static IGeometry CreateGeometryFromSymbol(ISymbol sym, IEnvelope env)
        {
            IPoint point;
            if (sym is IMarkerSymbol)
            {
                IArea area = (IArea)env;
                return area.Centroid;
            }
            if ((sym is ILineSymbol) || (sym is ITextSymbol))
            {
                IPolyline polyline = new PolylineClass();
                point = new PointClass();
                point.PutCoords(env.LowerLeft.X, (env.LowerLeft.Y + env.UpperRight.Y) / 2.0);
                polyline.FromPoint = point;
                point = new PointClass();
                point.PutCoords(env.UpperRight.X, (env.LowerLeft.Y + env.UpperRight.Y) / 2.0);
                polyline.ToPoint = point;
                if (sym is ITextSymbol)
                {
                    (sym as ITextSymbol).Text = "样本字符";
                }
                return polyline;
            }
            if (sym is IFillSymbol)
            {
                IPolygon polygon = new PolygonClass();
                IPointCollection points = (IPointCollection)polygon;
                point = new PointClass();
                point.PutCoords(env.LowerLeft.X, env.LowerLeft.Y);
                points.AddPoints(1, ref point);
                point.PutCoords(env.UpperLeft.X, env.UpperLeft.Y);
                points.AddPoints(1, ref point);
                point.PutCoords(env.UpperRight.X, env.UpperRight.Y);
                points.AddPoints(1, ref point);
                point.PutCoords(env.LowerRight.X, env.LowerRight.Y);
                points.AddPoints(1, ref point);
                point.PutCoords(env.LowerLeft.X, env.LowerLeft.Y);
                points.AddPoints(1, ref point);
                return polygon;
            }
            return null;
        }

        public static ILineSymbol CreateLineDirectionSymbol()
        {
            ILineSymbol symbol = new CartographicLineSymbolClass();
            symbol.Color = ColorHelper.CreateColor(0, 0, 200);
            LineDecorationClass class2 = new LineDecorationClass();
            SimpleLineDecorationElementClass lineDecorationElement = new SimpleLineDecorationElementClass();
            lineDecorationElement.AddPosition(0.3);
            lineDecorationElement.AddPosition(0.7);
            lineDecorationElement.PositionAsRatio = true;
            IMarkerSymbol symbol2 = (lineDecorationElement.MarkerSymbol as IClone).Clone() as IMarkerSymbol;
            symbol2.Size = 9.0;
            symbol2.Color = ColorHelper.CreateColor(0, 200, 0);
            lineDecorationElement.MarkerSymbol = symbol2;
            class2.AddElement(lineDecorationElement);
            (symbol as ILineProperties).LineDecoration = class2;
            return symbol;
        }

        public static ILineSymbol CreateSimpleLineSymbol(Color lineColor, double width)
        {
            SimpleLineSymbolClass class2 = new SimpleLineSymbolClass();
            class2.Color = ColorHelper.CreateColor(lineColor);
            class2.Style = esriSimpleLineStyle.esriSLSSolid;
            class2.Width = Math.Abs(width);
            return class2;
        }

        public static ILineSymbol CreateSimpleLineSymbol(Color lineColor, double width, esriSimpleLineStyle eStyle)
        {
            SimpleLineSymbolClass class2 = new SimpleLineSymbolClass();
            class2.Color = ColorHelper.CreateColor(lineColor);
            class2.Style = eStyle;
            class2.Width = Math.Abs(width);
            return class2;
        }

        public static IMarkerSymbol CreateSimpleMarkerSymbol(Color pColor, double pSize)
        {
            SimpleMarkerSymbolClass class2 = new SimpleMarkerSymbolClass();
            class2.Color = ColorHelper.CreateColor(pColor);
            class2.Size = pSize;
            return class2;
        }

        public static ITextSymbol CreateTextSymbol(Color pColor, IFontDisp pFont, double pSize, string sText)
        {
            ITextSymbol symbol = new TextSymbolClass();
            symbol.Color = ColorHelper.CreateColor(pColor);
            symbol.Font = pFont;
            symbol.Size = pSize;
            symbol.Text = sText;
            return symbol;
        }

        public static IFillSymbol CreateTransparentFillSymbol(ISimpleLineSymbol aOutline)
        {
            SimpleFillSymbolClass class2 = new SimpleFillSymbolClass();
            class2.Style = esriSimpleFillStyle.esriSFSNull;
            class2.Outline = aOutline;
            return class2;
        }

        public static IFillSymbol CreateTransparentFillSymbol(Color outlineColor)
        {
            SimpleFillSymbolClass class2 = new SimpleFillSymbolClass();
            class2.Style = esriSimpleFillStyle.esriSFSNull;
            ISimpleLineSymbol symbol = new SimpleLineSymbolClass();
            symbol.Style = esriSimpleLineStyle.esriSLSSolid;
            symbol.Color = ColorHelper.CreateColor(outlineColor);
            symbol.Width = 1.0;
            class2.Outline = symbol;
            return class2;
        }

        public static Image StyleToImage(ISymbol sym)
        {
            return StyleToImage(sym, 0x10, 0x10);
        }

        public static Image StyleToImage(ISymbol sym, int width, int height)
        {
            if (sym == null)
            {
                return null;
            }
            try
            {
                Image image = new Bitmap(width, height);
                Graphics graphics = Graphics.FromImage(image);
                IntPtr hdc = graphics.GetHdc();
                IEnvelope env = new EnvelopeClass();
                env.XMin = 1.0;
                env.XMax = width - 1;
                env.YMin = 1.0;
                env.YMax = height - 1;
                IGeometry geometry = CreateGeometryFromSymbol(sym, env);
                if (geometry != null)
                {
                    ITransformation transformation = DisplayHelper.CreateTransformationFromHDC(hdc, width, height);
                    sym.SetupDC((int)hdc, transformation);
                    sym.Draw(geometry);
                    sym.ResetDC();
                }
                graphics.ReleaseHdc(hdc);
                return image;
            }
            catch
            {
                return null;
            }
        }
    }
}
