// ******************************************************************
// 概  述：
// 作  者：
// 创建日期：
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LibGeometry
{
    #region Vector2_DW
    public struct Vector2_DW
    {
        #region Fields

        private double _x;
        private double _y;

        #endregion

        #region Properties

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return _x;
                    case 1:
                        return _y;
                }
                throw new IndexOutOfRangeException("Invalid Vector2_DW index!");
            }
            set
            {
                switch (index)
                {
                    case 0:
                        _x = value;
                        break;
                    case 1:
                        _y = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2_DW index!");
                }
            }
        }

        #endregion

        #region Functions

        public Vector2_DW(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public Vector2_DW(Vector2_DW vecSrc)
        {
            _x = vecSrc.X;
            _y = vecSrc.Y;
        }

        public double SqrtMagnitude()
        {
            return (_x * _x + _y * _y);
        }

        public double Magnitude()
        {
            return Math.Sqrt(_x * _x + _y * _y);
        }

        public Vector2_DW Normalize()
        {
            double maganitude = Magnitude();
            return new Vector2_DW(_x / maganitude, _y / maganitude);
        }

        public Vector2_DW Move(Vector2_DW basePt, Vector2_DW desPt)
        {
            Vector2_DW ptRet = new Vector2_DW();
            Vector2_DW vecOffset = desPt - basePt;
            ptRet = this + vecOffset;
            return ptRet;
        }

        public Vector2_DW Rotate(Vector2_DW ptBase, double angleDegree)
        {
            Vector2_DW ptRet = new Vector2_DW();
            Vector2_DW vec = this - ptBase;
            double angleRadium = ToolsMath_DW.Angle2Radium(angleDegree);
            double xOffset = (vec.X * Math.Cos(angleRadium) - vec.Y * Math.Sin(angleRadium));
            double yOffset = (vec.X * Math.Sin(angleRadium) + vec.Y * Math.Cos(angleRadium));

            ptRet = ptBase + new Vector2_DW(xOffset, yOffset);
            return ptRet;
        }

        public Vector2_DW Mirror(Vector2_DW lineSt, Vector2_DW lineEnd)
        {
            Vector2_DW ptRet = new Vector2_DW();
            Vector2_DW vecPer = (lineSt - lineEnd);
            Vector2_DW vec = new Vector2_DW(-vecPer.Y, vecPer.X);
            Vector2_DW ptTmp = new Vector2_DW(this + vec);
            Vector2_DW ptIntersect = new Vector2_DW();
            if (ToolsMath_DW.LineXLine(this, ptTmp, lineSt, lineEnd, ref ptIntersect) != LineIntersectType.None)
            {
                ptRet = ptIntersect + ptIntersect - this;
            }
            return ptRet;
        }

        public Vector2_DW Scale(Vector2_DW size)
        {
            Vector2_DW ptRet = new Vector2_DW();
            this.X *= size.X;
            this.Y *= size.Y;
            return ptRet;
        }

      

        #region Static Functions


        static public Vector2_DW Min(Vector2_DW lhs, Vector2_DW rhs)
        {
            double x = Math.Min(lhs.X, rhs.X);
            double y = Math.Min(lhs.Y, rhs.Y);
            return new Vector2_DW(x, y);
        }

        static public Vector2_DW Max(Vector2_DW lhs, Vector2_DW rhs)
        {
            double x = Math.Max(lhs.X, rhs.X);
            double y = Math.Max(lhs.Y, rhs.Y);
            return new Vector2_DW(x, y);
        }

        //返回a,b两点之间的距离
        static public double Distance(Vector2_DW a, Vector2_DW b)
        {
            Vector2_DW vec = b - a;
            return vec.Magnitude();
        }

        //返回两个向量之间的夹角(角度制)(0-180)
        static public double Angle(Vector2_DW from, Vector2_DW to)
        {
            return Math.Acos(ToolsMath_DW.Clamp(Vector2_DW.Dot(from.Normalize(), to.Normalize()), -1f, 1f)) * 57.29578f;
        }

        static public double Dot(Vector2_DW lhs, Vector2_DW rhs)
        {
            return (lhs.X * rhs.X + lhs.Y * rhs.Y);
        }

        //计算由a,b,c三点组成的向量 abxac的叉积,注:此处只是返回标量(平行四边形面积)并非实际意义上的叉积
        static public double Cross(Vector2_DW ptA, Vector2_DW ptB, Vector2_DW ptC)
        {
            Vector2_DW ab = ptB - ptA;// new Vector2_DW(b.X - a.X, b.Y - a.Y);
            Vector2_DW ac = ptC - ptA;// new Vector2_DW(c.X - a.X, c.Y - a.Y);
            return (ab.X * ac.Y - ac.X * ab.Y);
        }

        //计算abxac的叉积,注:此处只是返回标量(平行四边形面积)并非实际意义上的叉积
        static public double Cross(Vector2_DW vecL, Vector2_DW vecR)
        {
            return (vecL.X * vecR.Y - vecR.X * vecL.Y);
        }

        static public Vector2_DW operator +(Vector2_DW lhs, Vector2_DW rhs)
        {
            Vector2_DW result = lhs;
            result.X += rhs.X;
            result.Y += rhs.Y;
            return result;
        }

        static public Vector2_DW operator -(Vector2_DW lhs, Vector2_DW rhs)
        {
            Vector2_DW result = new Vector2_DW();
            result.X = lhs.X - rhs.X;
            result.Y = lhs.Y - rhs.Y;
            return result;
        }

        static public Vector2_DW operator -(Vector2_DW rhs)
        {
            Vector2_DW result = new Vector2_DW();
            result.X = -rhs.X;
            result.Y = -rhs.Y;
            return result;
        }

        static public Vector2_DW operator *(double d, Vector2_DW rhs)
        {
            Vector2_DW result = new Vector2_DW();
            result.X = d * rhs.X;
            result.Y = d * rhs.Y;
            return result;
        }

        static public Vector2_DW operator *(Vector2_DW lhs, double d)
        {
            Vector2_DW result = new Vector2_DW();
            result.X = d * lhs.X;
            result.Y = d * lhs.Y;
            return result;
        }

        //将Vector2_DW隐式转换为Vector3_DW,Z(标高)被设置为0
        static public implicit operator Vector3_DW(Vector2_DW vec)
        {
            Vector3_DW vecRet = new Vector3_DW();
            vecRet.X = vec.X;
            vecRet.Y = vec.Y;
            vecRet.Z = 0;
            return vecRet;
        }

        //将Vector3_DW隐式转换为Vector2_DW,Z(标高)被丢弃
        static public implicit operator Vector2_DW(Vector3_DW vec)
        {
            Vector2_DW vecRet = new Vector2_DW();
            vecRet.X = vec.X;
            vecRet.Y = vec.Y;
            return vecRet;
        }

        /*Point PointF操作
        public Point ToPoint()
        {
            return new Point((int)_x, (int)_y);
        }
        //将Vector2_DW隐式转换为PointF
        static public implicit operator PointF(Vector2_DW vec)
        {
            //return new Point((int)Math.Round(vec.X), (int)Math.Round(vec.Y));
            return new PointF((float)vec.X, (float)vec.Y);
        }

        //将Point隐式转换为Vector2_DW
        static public implicit operator Vector2_DW(Point pt)
        {
            return new Vector2_DW(pt.X, pt.Y);
        }

        //将PointF隐式转换为Vector2_DW
        static public implicit operator Vector2_DW(PointF pt)
        {
            return new Vector2_DW(pt.X, pt.Y);
        }

        //将Vector2_DW数组转换为Point数组
        static public Point[] Convert2PointArray(Vector2_DW[] vecs)
        {
            int n = vecs.Length;
            Point[] ptsRet = new Point[n];
            for (int i = 0; i < n; i++)
            {
                ptsRet[i].X = (int)Math.Round(vecs[i].X);
                ptsRet[i].Y = (int)Math.Round(vecs[i].Y);
            }
            return ptsRet;
        }
        */


        /*Unity
        #region 此处为了和Unity交互比较方便,因此将属性写成小写!

        public double x
        {
            get { return _x; }
            set { _x = value; }
        }

        public double y
        {
            get { return y; }
            set { _y = value; }
        }
        static public Vector2_DW right
        {
            get
            {
                return new Vector2_DW(1, 0);
            }
        }

        #endregion
         * */
        #endregion

        public override string ToString()
        {
            return ("(" +
                _x.ToString() +//x
                "," +
                _y.ToString() +//y
                ")");
        }

        #endregion
    }
    #endregion

    #region Vector3_DW

    public struct Vector3_DW
    {
        static public Vector3_DW zero = new Vector3_DW(0, 0, 0);
        #region Fields

        private double _x;
        private double _y;
        private double _z;

        #endregion

        #region Properties
        public double X
        {
            get { return _x; }
            set { _x = value; }
        }
        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }
        public double Z
        {
            get { return _z; }
            set { _z = value; }
        }

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return _x;
                    case 1:
                        return _y;
                    case 2:
                        return _z;
                }
                throw new IndexOutOfRangeException("Invalid Vector3_DW index!");
            }
            set
            {
                switch (index)
                {
                    case 0:
                        _x = value;
                        break;
                    case 1:
                        _y = value;
                        break;
                    case 2:
                        _z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3_DW index!");
                }
            }
        }

        #endregion

        #region Functions

        public Vector3_DW(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
        public Vector3_DW(Vector3_DW vecSrc)
        {
            _x = vecSrc.X;
            _y = vecSrc.Y;
            _z = vecSrc.Z;
        }
        public double SqrtMagnitude()
        {
            return (_x * _x + _y * _y + _z * _z);
        }

        public double Magnitude()
        {
            return Math.Sqrt(_x * _x + _y * _y + _z * _z);
        }

        public Vector3_DW Normalize()
        {
            double maganitude = Magnitude();
            return new Vector3_DW(_x / maganitude, _y / maganitude, _z / maganitude);
        }
      

        #region Static Functions

        static public Vector3_DW Min(Vector3_DW lhs, Vector3_DW rhs)
        {
            double x = Math.Min(lhs.X, rhs.X);
            double y = Math.Min(lhs.Y, rhs.Y);
            double z = Math.Min(lhs.Z, rhs.Z);
            return new Vector3_DW(x, y, z);
        }
        static public Vector3_DW Max(Vector3_DW lhs, Vector3_DW rhs)
        {
            double x = Math.Max(lhs.X, rhs.X);
            double y = Math.Max(lhs.Y, rhs.Y);
            double z = Math.Max(lhs.Z, rhs.Z);
            return new Vector3_DW(x, y, z);
        }

        static public double Distance(Vector3_DW a, Vector3_DW b)
        {
            Vector3_DW vec = b - a;
            return vec.Magnitude();
        }

        //返回两向量的夹角(角度制)
        static public double Angle(Vector3_DW lhs, Vector3_DW rhs)
        {
            double ll = lhs.Magnitude();
            double rl = rhs.Magnitude();
            if (ToolsMath_DW.IsZero(ll) || ToolsMath_DW.IsZero(rl))
            {
                return 0;
            }
            double angle = Math.Acos((lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z) / (ll * rl));
            angle *= (180 / Math.PI);
            return angle;

        }

        static public double Dot(Vector3_DW lhs, Vector3_DW rhs)
        {
            return (lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z);
        }

        static public Vector3_DW Cross(Vector3_DW lhs, Vector3_DW rhs)
        {
            Vector3_DW result = new Vector3_DW();
            result.X = lhs.Y * rhs.Z - lhs.Z * rhs.Y;
            result.Y = lhs.Z * rhs.X - rhs.Z * lhs.X;
            result.Z = lhs.X * rhs.Y - lhs.Y * rhs.X;
            return result;
        }
        static public Vector3_DW operator +(Vector3_DW lhs, Vector3_DW rhs)
        {
            Vector3_DW result = lhs;
            result.X += rhs.X;
            result.Y += rhs.Y;
            result.Z += rhs.Z;
            return result;
        }

        static public Vector3_DW operator -(Vector3_DW lhs, Vector3_DW rhs)
        {
            Vector3_DW result = new Vector3_DW();
            result.X = lhs.X - rhs.X;
            result.Y = rhs.Y - rhs.Y;
            result.Z = lhs.Z - rhs.Z;
            return result;
        }

        static public Vector3_DW operator -(Vector3_DW rhs)
        {
            Vector3_DW result = new Vector3_DW();
            result.X = -rhs.X;
            result.Y = -rhs.Y;
            result.Z = -rhs.Z;
            return result;
        }

        static public Vector3_DW operator *(double d, Vector3_DW rhs)
        {
            Vector3_DW result = new Vector3_DW();
            result.X = d * rhs.X;
            result.Y = d * rhs.Y;
            result.Z = d * rhs.Z;
            return result;
        }

        static public Vector3_DW operator *(Vector3_DW lhs, double d)
        {
            Vector3_DW result = new Vector3_DW();
            result.X = d * lhs.X;
            result.Y = d * lhs.Y;
            result.Z = d * lhs.Z;
            return result;
        }

        /*Point、PointF转换
        //注意:Z值被舍弃！
        public Point Convert2Point()
        {
            return (new Point((int)this.X, (int)this.Y));
        }
        //注意:Z值被舍弃！
        static public Point[] Convert2PointArray(Vector3_DW[] vecs)
        {
            int n = vecs.Length;
            Point[] ptsRet = new Point[n];
            for (int i = 0; i < n; i++)
            {
                ptsRet[i].X = (int)Math.Round(vecs[i].X);
                ptsRet[i].Y = (int)Math.Round(vecs[i].Y);
            }
            return ptsRet;
        }

        //将Vector3_DW隐式转换为PointF
        static public implicit operator PointF(Vector3_DW vec)
        {
            return new PointF((float)vec.X, (float)vec.Y);//注意:Z表示高程
        }
        //将Point隐式转换为Vector3_DW
        static public implicit operator Vector3_DW(Point pt)
        {
            return new Vector3_DW(pt.X, 0, pt.Y);
        }
        //将PointF隐式转换为Vector3_DW
        static public implicit operator Vector3_DW(PointF pt)
        {
            return new Vector3_DW(pt.X, 0, pt.Y);
        }
         * */
        #endregion

        public override string ToString()
        {
            return ("(" +
                _x.ToString() +//x
                "," +
                _y.ToString() +//y
                "," +
                _z.ToString() +//z
                ")");
        }

        #endregion
    }

    #endregion

    #region Box2D

    public struct Box2D
    {
        public Vector2_DW _min;
        public Vector2_DW _max;

        public double Height
        {
            get { return _max.Y - _min.Y; }
        }

        public double Width
        {
            get { return _max.X - _min.X; }
        }

        public Box2D(double xMin, double yMin, double xMax, double yMax)
            : this()
        {
            _min.X = xMin;
            _min.Y = yMin;
            _max.X = xMax;
            _max.Y = yMax;
        }

        public Box2D ExtendBox(double dis)//扩展Box
        {
            return new Box2D(
                _min.X - dis,
                _min.Y - dis,
                _max.X + dis,
                _max.Y + dis);
        }


        //将屏幕Box转化为屏幕Rect
        public static Rectangle Box2Rect(Box2D bx)
        {
            Rectangle rectRet = new Rectangle();
            rectRet.X = (int)bx._min.X;
            rectRet.Y = (int)bx._min.Y;
            rectRet.Width = (int)bx.Width;
            rectRet.Height = (int)bx.Height;
            return rectRet;
        }

        //将屏幕Rect转化为屏幕Box
        public static Box2D Rect2Box(Rectangle rectScreen)
        {
            Box2D bxRet = new Box2D();
            bxRet._min.X = rectScreen.X;
            bxRet._min.Y = rectScreen.Y;
            bxRet._max.X = rectScreen.Right;
            bxRet._max.Y = rectScreen.Bottom;
            return bxRet;
        }

    }

    #endregion

    #region ToolsMath_DW
    public enum LineIntersectType
    {
        None = -1,//没有交点
        OnBothExtend = 0,//在两条直线的延长线上
        OnFirstLine = 1,//在第一条直线上 p0-p1
        OnSecondLine = 2,//在第二条直线上 p2-p3
        OnBothLine = 3//在第两条直线上
    }

    public static class ToolsMath_DW
    {
        public const double _realTolerance = 1.0e-11f;
        /// <summary>
        /// 判断两个浮点数字是否相等
        /// 注意,不能用该函数判断浮点数是否为0.应用IsZero
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <param name="tolerance">精度</param>
        /// <returns></returns>
        public static bool IsRealEqual(double r1, double r2, double tolerance)
        {
            if (Math.Abs(r2) <= tolerance)
            {
                if (r1 > tolerance)
                {
                    return false;
                }
            }
            else
            {
                if (Math.Abs(r1 / r2) - 1.0 > tolerance)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsRealEqual(double r1, double r2)
        {
            double tolerance = _realTolerance;
            if (Math.Abs(r2) <= tolerance)
            {
                if (r1 > tolerance)
                {
                    return false;
                }
            }
            else
            {
                if (Math.Abs(r1 / r2) - 1.0 > tolerance)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsZero(double r, double tolerance)
        {
            return (r <= tolerance && r >= -tolerance) ? true : false;
        }

        public static bool IsZero(double r)
        {
            double tolerance = _realTolerance;
            return (r <= tolerance && r >= -tolerance) ? true : false;
        }

        public static LineIntersectType LineXLine(Vector2_DW p0, Vector2_DW p1, Vector2_DW p2, Vector2_DW p3, ref Vector2_DW xpt)
        {
            /*
            **  Get the intersection of the line defined by p0 and p1 with the
            **  line defined by p2 and p3.  Sets xpt to the intersection point
            **  if return value is non-negative. 
            **
            **  Returns:
            **       None = Lines are paralell or the points are coincide[eg:p0(0, 0), p1(2, 0),p2(1, 0), p3(4, 0)].
            **       OnBothExtend = Intersection is not on either segment.
            **       OnFirstLine = Intersection is on the 1st segment (p0-p1) but not the 2nd.
            **       OnSecondLine = Intersection is on the 2nd segment (p2-p3) but not the 1st.
            **       OnBothLine = Intersection is on both line segments.
            */
            double a1 = p1.Y - p0.Y;
            double b1 = p0.X - p1.X;
            double c1 = a1 * p0.X + b1 * p0.Y;

            double a2 = p3.Y - p2.Y;
            double b2 = p2.X - p3.X;
            double c2 = a2 * p2.X + b2 * p2.Y;

            double denominator = a1 * b2 - a2 * b1;
            if (IsZero(denominator))
            {
                //paralell
                return LineIntersectType.None;
            }
            else
            {
                int on1 = 1;
                int on2 = 1;

                xpt.X = (b2 * c1 - b1 * c2) / denominator;
                xpt.Y = (a1 * c2 - a2 * c1) / denominator;
                Vector2_DW vecMin = Vector2_DW.Min(p0, p1);
                Vector2_DW vecMax = Vector2_DW.Max(p0, p1);
                on1 = (vecMin.X <= xpt.X && xpt.X <= vecMax.X &&
                    vecMin.Y <= xpt.Y && xpt.Y <= vecMax.Y) ? 1 : 0;
                vecMin = Vector2_DW.Min(p2, p3);
                vecMax = Vector2_DW.Max(p2, p3);
                on2 = (vecMin.X <= xpt.X && xpt.X <= vecMax.X &&
                    vecMin.Y <= xpt.Y && xpt.Y <= vecMax.Y) ? 1 : 0;
                int result = on1 + 2 * on2;
                if (0 == result)
                {
                    return LineIntersectType.OnBothExtend;
                }
                else if (1 == result)
                {
                    return LineIntersectType.OnFirstLine;
                }
                else if (2 == result)
                {
                    return LineIntersectType.OnSecondLine;
                }
                else// 3==result
                {
                    return LineIntersectType.OnBothLine;
                }
            }
        }

        /// <summary>
        /// ///计算pos到直线(lineSt, lineEnd)的最短距离
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="lineSt"></param>
        /// <param name="lineEnd"></param>
        /// <param name="lineIsSegment">是否为线段</param>
        /// <returns></returns>
        public static double LinePointDistance(Vector2_DW lineSt, Vector2_DW lineEnd, Vector2_DW pos, bool lineIsSegment)
        {
            double disRet = 0;
            if (lineIsSegment == false)
            {
                disRet = Vector2_DW.Cross(lineSt, lineEnd, pos) / Vector2_DW.Distance(lineSt, lineEnd);
            }
            else
            {
                Vector2_DW ab = lineEnd - lineSt;
                Vector2_DW bc = pos - lineEnd;
                Vector2_DW ac = pos - lineSt;

                if (Vector2_DW.Dot(ab, bc) > 0)//pos在lineEnd外侧
                {
                    disRet = Vector2_DW.Distance(pos, lineEnd);
                }
                else if (Vector2_DW.Dot(ab, ac) > 0)//pos在lineEnd外侧
                {
                    disRet = Vector2_DW.Distance(pos, lineSt);
                }
                else//pos在lineSt与lineEnd之间
                {
                    disRet = Vector2_DW.Cross(lineSt, lineEnd, pos) / Vector2_DW.Distance(lineSt, lineEnd);
                }
            }
            return Math.Abs(disRet);
        }

        static public bool PtInBox(Box2D bx, Vector2_DW posScreen)//判断某一点是否在包围盒内
        {
            if (posScreen.X > bx._min.X
                && posScreen.X < bx._max.X
                && posScreen.Y > bx._min.Y
                && posScreen.Y < bx._max.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public bool PtInBox(Box2D bx, Point posScreen)//判断某一点是否在包围盒内
        {
            if (posScreen.X > bx._min.X
                && posScreen.X < bx._max.X
                && posScreen.Y > bx._min.Y
                && posScreen.Y < bx._max.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public bool BoxInRect(Rectangle rect, Box2D bx)
        {
            if (rect.Contains(new Point((int)bx._min.X, (int)bx._min.Y)) &&
            rect.Contains(new Point((int)bx._max.X, (int)bx._max.Y)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //判断两个矩形是否相交(注:当一个矩形包围另一个矩形时,也认为是相交)
        public static bool BoxXBox(Box2D bx1, Box2D bx2)
        {
            Box2D intersectBx = new Box2D();//如果两个矩形相交,其相交区域必为矩形
            intersectBx._min = Vector2_DW.Max(bx1._min, bx2._min);
            intersectBx._max = Vector2_DW.Min(bx1._max, bx2._max);
            if (intersectBx._min.X > intersectBx._max.X
                || intersectBx._min.Y > intersectBx._max.Y)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //获取直线的最小包围盒(传入的点的坐标为屏幕坐标)
        public static Box2D GetLineMinBox(Vector2_DW screenPtSt, Vector2_DW screenPtEnd)
        {
            Box2D bxRet = new Box2D();
            bxRet._min = Vector2_DW.Min(screenPtSt, screenPtEnd);
            bxRet._max = Vector2_DW.Max(screenPtSt, screenPtEnd);
            return bxRet;
        }

        /// <summary>
        /// 判断直线段与矩形是否相交(注:当线段全部在矩形内,也认为相交)
        /// 1,线段端点是否在矩形内,在,true
        /// 2,线段包围框是否和矩形相交,否,false
        /// 3,矩形四个顶点是否在线段两侧,在true,不在,false
        /// </summary>
        /// <param name="lStScreen"></param>
        /// <param name="lEndScreen"></param>
        /// <param name="bx"></param>
        /// <returns></returns>
        public static bool LineSegmenXBox(Vector2_DW lStScreen, Vector2_DW lEndScreen, Box2D bx)
        {
            if (ToolsMath_DW.PtInBox(bx, lStScreen)
                && ToolsMath_DW.PtInBox(bx, lEndScreen))
            {
                return true;
            }

            Box2D bxLine = ToolsMath_DW.GetLineMinBox(lStScreen, lEndScreen);
            if (!ToolsMath_DW.BoxXBox(bx, bxLine))
            {
                return false;
            }

            Vector2_DW vecLn = lStScreen - lEndScreen;
            Vector2_DW vBxLeftUpper = bx._min - lEndScreen;
            Vector2_DW vBxRightTop = (new Vector2_DW(bx._max.X, bx._min.Y)) - lEndScreen;
            Vector2_DW vBxRightBtm = bx._max - lEndScreen;
            Vector2_DW vBxLeftBtm = (new Vector2_DW(bx._min.X, bx._max.Y)) - lEndScreen;

            double f1 = Vector2_DW.Cross(vecLn, vBxLeftUpper);
            double f2 = Vector2_DW.Cross(vecLn, vBxRightTop);
            double f3 = Vector2_DW.Cross(vecLn, vBxRightBtm);
            double f4 = Vector2_DW.Cross(vecLn, vBxLeftBtm);
            if ((f1 >= 0 && f2 >= 0 && f3 >= 0 && f4 >= 0)
                ||
                (f1 <= 0 && f2 <= 0 && f3 <= 0 && f4 <= 0))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// 判断点pt是否在直线上
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="lnSt"></param>
        /// <param name="lnEnd"></param>
        /// <param name="isSegment">是否为直线段</param>
        /// <returns></returns>
        public static bool IsPointOnLine(Vector2_DW pt, Vector2_DW lnSt, Vector2_DW lnEnd, bool isSegment)
        {
            //设点为Q，线段为P1P2 ，判断点Q在该线段上的依据是：( Q - P1 ) × ( P2 - P1 ) = 0 且 Q 在以 P1，P2为对角顶点的矩形内。前者保证Q点在直线P1P2上，后者是保证Q点不在线段P1P2的延长线或反向延长线上
            //My Method:设点为Q，线段为P1P2 ，判断点Q在该线段上的依据是：( Q - P1 ) × ( P2 - P1 ) = 0 且 Q 到p1p2的距离==0。前者保证Q点在直线P1P2上，后者是保证Q点不在线段P1P2的延长线或反向延长线上
            Vector2_DW vec1 = pt - lnSt;
            Vector2_DW vec2 = pt - lnEnd;
            if (IsZero(Vector2_DW.Cross(vec1, vec2)) == true)
            {
                if (IsZero(LinePointDistance(lnSt, lnEnd, pt, isSegment)))
                {
                    return true;
                }
            }
            return false;
        }

        public static double Clamp(double value, double min, double max)
        {
            if (value < min)
            {
                value = min;
                return value;
            }
            if (value > max)
            {
                value = max;
            }
            return value;
        }

        //将角度制转换为弧度制
        public static double Angle2Radium(double angleDegree)
        {
            return (angleDegree * Math.PI / 180);
        }

        //将弧度制转换为角度制
        public static double Radium2Angle(double angleRadium)
        {
            return (angleRadium * 180 / Math.PI);
        }

        //获取points的最小包围盒
        static public Box2D GetMinBox(Vector2_DW[] points)
        {
            Box2D bxRet = new Box2D();
            int n = points.Length;
            double minX = 0;
            double minY = 0;
            double maxX = 0;
            double maxY = 0;
            for (int i = 0; i < n; i++)
            {
                if (0 == i)
                {
                    minX = maxX = points[i].X;
                    minY = maxY = points[i].Y;
                }
                else
                {
                    if (points[i].X < minX)
                    {
                        minX = points[i].X;
                    }
                    if (points[i].Y < minY)
                    {
                        minY = points[i].Y;
                    }
                    if (points[i].X > maxX)
                    {
                        maxX = points[i].X;
                    }
                    if (points[i].Y > minY)
                    {
                        maxY = points[i].Y;
                    }
                }
            }
            bxRet._min = new Vector2_DW(minX, minY);
            bxRet._max = new Vector2_DW(maxX, maxY);
            return bxRet;
        }

        //获取points的最小包围盒和最左端的点
        static public Box2D GetMinBox(Vector2_DW[] points, ref Vector2_DW leftMostPt)
        {
            Box2D bxRet = new Box2D();
            int n = points.Length;
            double minX = 0;
            double minY = 0;
            double maxX = 0;
            double maxY = 0;
            int nLeftMostIndex = 0;
            for (int i = 0; i < n; i++)
            {
                if (0 == i)
                {
                    minX = maxX = points[i].X;
                    minY = maxY = points[i].Y;
                }
                else
                {
                    if (points[i].X < minX)
                    {
                        minX = points[i].X;
                        nLeftMostIndex = i;
                    }
                    if (points[i].Y < minY)
                    {
                        minY = points[i].Y;
                    }
                    if (points[i].X > maxX)
                    {
                        maxX = points[i].X;
                    }
                    if (points[i].Y > minY)
                    {
                        maxY = points[i].Y;
                    }
                }
            }
            bxRet._min = new Vector2_DW(minX, minY);
            bxRet._max = new Vector2_DW(maxX, maxY);
            leftMostPt = new Vector2_DW(points[nLeftMostIndex].X, points[nLeftMostIndex].Y);
            return bxRet;
        }

    }


    #endregion
}
