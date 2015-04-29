using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GIS.SpecialGraphic
{
    public class DrawZK2
    {
        public Bitmap m_Bitmap = new Bitmap(Properties.Resources.图片);
        public string m_strMC = "ZZFT-023";
        public string m_strDMBG = "1128.64";

        public Pen m_pPen = new Pen(Color.Black);
        //写文字的字体,画刷和位置
        public Font m_pFontB = new Font("宋体", 15);
        public Font m_pFontS = new Font("宋体", 10);
        public Brush m_pBrushRed = Brushes.Red;
        public Brush m_pBrushBlack = Brushes.Black;

        //构造函数
        public DrawZK2(string strMC, string strDMBG)
        {
            m_strMC = strMC;
            m_strDMBG = strDMBG;

            DrawSymbol();
        }
        //public DrawZK2()
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
            g.DrawEllipse(m_pPen, m_pPoint.X, m_pPoint.Y, iWidth, iHeight);
            g.DrawEllipse(m_pPen, m_pPoint.X - 3, m_pPoint.Y - 3, iWidth + 6, iHeight + 6);

            //钻孔名称
            Point mcPoint = new Point();
            mcPoint.X = 10;
            mcPoint.Y = 0;
            g.DrawString(m_strMC, m_pFontB, m_pBrushBlack, mcPoint);

            //地面标高  m_strDMBG
            Point dmbgPoint = new Point();
            dmbgPoint.X = 0;
            dmbgPoint.Y = 24;
            g.DrawString(m_strDMBG, m_pFontS, m_pBrushRed, dmbgPoint);
        }
    }
}
