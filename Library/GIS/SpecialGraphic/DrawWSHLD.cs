using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GIS.SpecialGraphic
{
    //画瓦斯含量点
   public class DrawWSHLD
    {
       //////////////////////
        //图片
        public Bitmap m_Bitmap = new Bitmap(GIS.Properties.Resources.图片); //暂定使用程序内嵌图片，也可以自动生成图片，效果相同
        //标题
        public string m_strBT = "W";//标题
        public string m_strWSHLZ = "12.30";//分子
        public string m_strCDBG = "-600";//分母左侧
        public string m_strMS = "713.85";//分母右侧
        //画笔
        public Pen m_pPen = new Pen(Color.Red);
        //写文字的字体,画刷和位置
        public Font m_pFont = new Font("宋体", 15);
        public Font m_pFont2 = new Font("宋体", 9);
        public Brush m_pBrush = Brushes.Red;
        public Point m_pBTWZPoint = new Point();
        //////////////////////////////////////////////
        //画直线的两个点
        public Point m_pZXLeftPoint = new Point();
        public Point m_pZXRightPoint = new Point();
        //画竖线
        public Point m_pSXUpPoint = new Point();
        public Point m_pSXButtonPoint = new Point();

        //画圆形
        public Point m_pYXYDPoint = new Point();//圆形左上起点
        public int m_iWidth;
        public int m_iHeight;


        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DrawWSHLD()
        {
            m_strBT = "W";//标题
            m_strWSHLZ = "12.30";//分子
            m_strCDBG = "-600";//分母左侧
            m_strMS = "713.85";//分母右侧

            DrawSymbol();
        }
        /// <summary>
        /// 输入数值的构造函数
        /// </summary>
        /// <param name="strBT"></param>
        /// <param name="strWSYLZ"></param>
        /// <param name="strCDBG"></param>
        /// <param name="strMS"></param>
        public DrawWSHLD(string strBT, string strWSHLZ, string strCDBG, string strMS)
        {
            m_strBT = strBT;
            m_strWSHLZ = strWSHLZ;
            m_strCDBG = strCDBG;
            m_strMS = strMS;
            DrawSymbol();

        }
        /// <summary>
        /// 开始画符号
        /// </summary>
        private void DrawSymbol()
        {
            //获得画布
            Graphics g = Graphics.FromImage(m_Bitmap);
            //直线赋值
            m_pZXLeftPoint.X = 30;
            m_pZXLeftPoint.Y = 25;
            m_pZXRightPoint.X = 90;
            m_pZXRightPoint.Y = 25;
            //画直线
            g.DrawLine(m_pPen, m_pZXLeftPoint, m_pZXRightPoint);

            //竖线赋值
            m_pSXUpPoint.X = 60;
            m_pSXUpPoint.Y = 25;
            m_pSXButtonPoint.X = 60;
            m_pSXButtonPoint.Y = 38;
            //画直线
            g.DrawLine(m_pPen, m_pSXUpPoint, m_pSXButtonPoint);

            //圆形赋值
            m_pYXYDPoint.X=10;
            m_pYXYDPoint.Y=15;
            m_iWidth=18;
            m_iHeight=18;
            //画圆形
            g.DrawEllipse(m_pPen, m_pYXYDPoint.X, m_pYXYDPoint.Y, m_iWidth, m_iHeight);

            //写标题赋值(p)
            m_pBTWZPoint.X = 12;
            m_pBTWZPoint.Y = 15;
            g.DrawString(m_strBT, m_pFont, m_pBrush, m_pBTWZPoint);

            //2.3MPa
            int width_Shang = Convert.ToInt32(g.MeasureString(m_strWSHLZ, m_pFont2).Width / 2);
            m_pBTWZPoint.X = 60 - width_Shang;
            m_pBTWZPoint.Y = 12;
            g.DrawString(m_strWSHLZ, m_pFont2, m_pBrush, m_pBTWZPoint);

            //-600
            int width_ZuoXia = Convert.ToInt32(g.MeasureString(m_strCDBG, m_pFont2).Width);
            m_pBTWZPoint.X = 60 - 1 - width_ZuoXia;
            m_pBTWZPoint.Y = 25;
            g.DrawString(m_strCDBG, m_pFont2, m_pBrush, m_pBTWZPoint);

            //620
            m_pBTWZPoint.X = 63;
            m_pBTWZPoint.Y = 25;
            g.DrawString(m_strMS, m_pFont2, m_pBrush, m_pBTWZPoint);

        }
    }
}
