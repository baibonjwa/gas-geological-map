using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using GIS.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace GIS.BasicGraphic
{
    public partial class InputText : Form
    {
        private Font m_pFont;
        IColor m_color;
        private ESRI.ArcGIS.Geometry.IPoint m_Point;

        public InputText(ESRI.ArcGIS.Geometry.IPoint pPoint)
        {
            InitializeComponent();
            m_Point = pPoint;
            m_color = GetRGBColor(0, 0, 0);
        }

        /// <summary>
        /// 选择（修改）字体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFont_Click(object sender, EventArgs e)
        {
            FontDialog pFontDialog = new FontDialog();
            pFontDialog.ShowDialog();
            if (pFontDialog.Font != null)
                m_pFont = pFontDialog.Font;          
        }

        /// <summary>
        /// 绘制文字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (m_Point == null)
                return;
            string strText = this.textBox1.Text;
                DrawTextToMap(strText, m_Point);
            this.Close();
        }

        /// <summary>
        /// 绘制文字到地图中
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pPoint"></param>
        /// <param name="drawFont"></param>
        void DrawTextToMap(string text, ESRI.ArcGIS.Geometry.IPoint pPoint)
        {
            IElement element;

            ITextElement textElement = new TextElementClass();
            element = textElement as IElement;

            ITextSymbol textSymbol = new TextSymbolClass();
            textSymbol.Color = m_color;
            if (m_pFont != null)
            {
                textSymbol.Font = (stdole.IFontDisp)ESRI.ArcGIS.ADF.COMSupport.OLE.GetIFontDispFromFont(m_pFont);
                textSymbol.Size = Convert.ToDouble(m_pFont.Size);
            }
            element.Geometry = pPoint;

            textElement.Symbol = textSymbol;
            textElement.Text = text;
            IFeature pFeature = DataEditCommon.SaveAnno(element, text);
            GIS.Common.DataEditCommon.g_pMap.SelectFeature(GIS.Common.DataEditCommon.g_pLayer, pFeature);
                        GIS.Common.DataEditCommon.g_pAxMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics | esriViewDrawPhase.esriViewGeoSelection | esriViewDrawPhase.esriViewBackground, null, null);
        }

        /// <summary>
        /// 文字颜色
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <returns></returns>
        private IRgbColor GetRGBColor(int red, int green, int blue)
        {
            //创建rgb颜色和实例化IRGBColor接口
            IRgbColor rGB = new RgbColor();
            //设置RGB属性
            rGB.Red = red;
            rGB.Green = green;
            rGB.Blue = blue;
            rGB.UseWindowsDithering = true;
            return rGB;
        }

        private void btncolor_Click(object sender, EventArgs e)
        {
            ColorDialog colorD = new ColorDialog();
            colorD.ShowDialog();
            if (colorD.Color != null)
            {
                m_color = GetRGBColor(colorD.Color.R, colorD.Color.G, colorD.Color.B);
            }
        }
        private esriTextHorizontalAlignment GetHorizontalAlignment()
        {
            const esriTextHorizontalAlignment HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;

            return HorizontalAlignment;
        }

        private esriTextVerticalAlignment GetVerticalAlignment()
        {
            const esriTextVerticalAlignment VerticalAlignment = esriTextVerticalAlignment.esriTVABaseline;

            return VerticalAlignment;
        }
    }
}
