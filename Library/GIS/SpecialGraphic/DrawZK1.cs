using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GIS.SpecialGraphic
{
    public class DrawZK1
    {
        public Bitmap m_Bitmap = new Bitmap(Properties.Resources.图片);
        public string m_strMC = "ZZ-239";
        public string m_strDMBG = "1097.51";
        public string m_strDBBG = "337.43";
        public string m_strMCHD = "4.89";

        public Pen m_pPen = new Pen(Color.Black);
        //写文字的字体,画刷和位置
        public Font m_pFontB = new Font("宋体", 15);
        public Font m_pFontS = new Font("宋体", 10);
        public Brush m_pBrushRed = Brushes.Red;
        public Brush m_pBrushBlack = Brushes.Black;

        //构造函数
        public DrawZK1(string strMC, string strDMBG, string strDBBG, string strMCHD)
        {
            m_strMC = strMC;
            m_strDMBG = strDMBG;
            m_strDBBG = strDBBG;
            m_strMCHD = strMCHD;

            DrawSymbol();
        }
        //public DrawZK1()
        //{
        //    DrawSymbol();
        //}

        private void DrawSymbol()//画出图标
        {
            Graphics g = Graphics.FromImage(m_Bitmap);
            //画圆
            Point m_pPoint = new Point();
            m_pPoint.X = 40;
            m_pPoint.Y = 20;
            int iWidth = 18;
            int iHeight = 18;
            g.DrawEllipse(m_pPen, m_pPoint.X - 3, m_pPoint.Y - 3, iWidth + 6, iHeight + 6);
            Brush myBrush = new SolidBrush(Color.Black);
            g.FillEllipse(myBrush, m_pPoint.X, m_pPoint.Y, iWidth, iHeight);

            //钻孔名称
            Point mcPoint = new Point();
            mcPoint.X = 20;
            mcPoint.Y = 0;
            g.DrawString(m_strMC, m_pFontB, m_pBrushBlack, mcPoint);

            //地面标高  m_strDMBG
            Point dmbgPoint = new Point();
            dmbgPoint.X = 0;
            dmbgPoint.Y = 18;
            g.DrawString(m_strDMBG, m_pFontS, m_pBrushRed, dmbgPoint);

            //地板标高 m_strDBBG
            Point dbbgPoint = new Point();
            dbbgPoint.X = 0;
            dbbgPoint.Y = 30;
            g.DrawString(m_strDBBG, m_pFontS, m_pBrushBlack, dbbgPoint);

            //煤层厚度 m_strMCHD
            Point mchdPoint = new Point();
            mchdPoint.X = 63;
            mchdPoint.Y = 24;
            g.DrawString(m_strMCHD, m_pFontS, m_pBrushBlack, mchdPoint);
        }
    }
}
