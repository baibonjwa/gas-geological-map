using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GIS.SpecialGraphic
{
    public class DrawJT
    {
        //////////////////////
        //图片
        public Bitmap m_Bitmap = new Bitmap(Properties.Resources.图片);
        //标题
        private string m_strH;
        private string m_strX;
        private string m_strY;
        private string m_strName;

        private Pen m_pPen = new Pen(Color.Black);
        //写文字的字体,画刷和位置
        private Font m_pFont = new Font("宋体", 10);
        private Brush m_pBrush = Brushes.Black;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DrawJT()
        {
            m_strH = "H";//标题       
            DrawSymbol();
        }
        /// <summary>
        /// 输入数值的构造函数
        /// </summary>
        public DrawJT(string strX, string strY, string strH,string strName)
        {
            m_strX = strX;
            m_strY=strY;
            m_strH = strH;
            m_strName = strName;

            DrawSymbol();

        }
        /// <summary>
        /// 开始画符号
        /// </summary>
        private void DrawSymbol()
        {
            //获得画布
            Graphics g = Graphics.FromImage(m_Bitmap);
            //画圆
            Point inCirclePoint = new Point();
            inCirclePoint.X = 5;
            inCirclePoint.Y = 40;
            int circleWidth = 10;
            //画圆形
            g.DrawEllipse(m_pPen, inCirclePoint.X, inCirclePoint.Y, circleWidth, circleWidth);
            g.DrawEllipse(m_pPen, inCirclePoint.X - 3, inCirclePoint.Y + -3, circleWidth + 6, circleWidth + 6);
            Rectangle reg = new Rectangle();
            reg.Width = circleWidth + 2;
            reg.Height = circleWidth + 2;
            reg.X = inCirclePoint.X;
            reg.Y = inCirclePoint.Y;
            g.FillPie(m_pBrush, reg, 90, 180);

            Point formLinePoint1 = new Point();
            formLinePoint1.X = inCirclePoint.X + 5;
            formLinePoint1.Y = inCirclePoint.Y + 5;

            Point toLinePoint1 = new Point();
            toLinePoint1.X = formLinePoint1.X + 45;
            toLinePoint1.Y = formLinePoint1.Y + 10;
            g.DrawLine(m_pPen, formLinePoint1, toLinePoint1);

            Point LinePoint2 = new Point();
            LinePoint2.X = toLinePoint1.X + 80;
            LinePoint2.Y = toLinePoint1.Y;
            g.DrawLine(m_pPen, toLinePoint1, LinePoint2);

            string txt1 = m_strH;
            Point txtPoint1 = new Point();
            txtPoint1.X = toLinePoint1.X + 1;
            txtPoint1.Y = toLinePoint1.Y - 12;
            g.DrawString(txt1, m_pFont, m_pBrush, txtPoint1);

            string txt2 = m_strY;
            Point txtPoint2 = new Point();
            txtPoint2.X = txtPoint1.X;
            txtPoint2.Y = txtPoint1.Y - 12;
            g.DrawString(txt2, m_pFont, m_pBrush, txtPoint2);

            string txt3 = m_strX;           
            Point txtPoint3 = new Point();
            txtPoint3.X = txtPoint2.X;
            txtPoint3.Y = txtPoint2.Y - 12;
            g.DrawString(txt3, m_pFont, m_pBrush, txtPoint3);

            string txt4 = m_strName;// "副井";
            Point txtPoint4 = new Point();
            txtPoint4.X = txtPoint3.X;
            txtPoint4.Y = txtPoint3.Y - 12;
            g.DrawString(txt4, m_pFont, m_pBrush, txtPoint4);


        }
    }
}
