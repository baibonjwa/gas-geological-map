using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;

namespace GIS.BasicGraphic
{
    /// <summary>
    /// 旋转元素工具
    /// </summary>
    public sealed class BTGraphicsRotateElement : BaseTool
    {
        #region  成员变量

        private IHookHelper m_hookHelper = null;
        private IPoint m_point;
        private IElement m_element;
        private bool m_moving;
        private IPoint m_oldPoint;
        private IElement m_viewElement;
        /// <summary>
        /// 获取或设置标注图层
        /// </summary>
        public IFeatureLayer AnnotationLayer { get; set; }

        #endregion

        #region 构造函数

        public BTGraphicsRotateElement()
        {
            base.m_category = "";
            base.m_caption = "";
            base.m_message = "";
            base.m_toolTip = "旋转元素";
            base.m_name = "";
            //base.m_cursor = Cursors.Default;
            //try
            //{
            //    base.m_bitmap = Properties.Resources.bmp_rotate;
            //}
            //catch
            //{
            //    base.m_bitmap = null;
            //}
        }
        public BTGraphicsRotateElement(IFeatureLayer annotationLayer)
            : this()
        {
            AnnotationLayer = annotationLayer;
        }
        #endregion

        #region 方法覆写

        public override void OnCreate(object hook)
        {
            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                if (m_hookHelper.ActiveView == null)
                {
                    m_hookHelper = null;
                }
            }
            catch
            {
                m_hookHelper = null;
            }

            if (m_hookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;

            // TODO:  Add other initialization code

        }

        public override void OnClick()
        {
            // TODO: Add StationTool.OnClick implementation
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            if (Button == 1)
            {
                IGraphicsContainer pGraphicsContainer = m_hookHelper.ActiveView as IGraphicsContainer;
                m_point = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                IEnumElement pEnumElement = pGraphicsContainer.LocateElements(m_point, 1);
                if (pEnumElement == null) return;
                m_element = pEnumElement.Next();

                if (m_element is AnnotationElement)
                {
                    ITextElement pTextElement = new TextElementClass { Text = ((ITextElement)m_element).Text, Symbol = ((ITextElement)m_element).Symbol, ScaleText = true };
                    m_viewElement = pTextElement as IElement;
                    m_viewElement.Geometry = m_element.Geometry;
                    m_hookHelper.ActiveView.GraphicsContainer.AddElement(m_viewElement, 0);
                    m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, m_viewElement, null);
                }

                // 移动状态信息
                m_moving = true;
            }
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            if (Button == 1)
            {
                if (!m_moving) return;

                IPoint centerPoint = new PointClass
                {
                    X = (m_element.Geometry.Envelope.LowerLeft.X + m_element.Geometry.Envelope.LowerRight.X) / 2,
                    Y = (m_element.Geometry.Envelope.LowerLeft.Y + m_element.Geometry.Envelope.UpperRight.Y) / 2
                };
                if (m_element is AnnotationElement)
                {
                    // 如果旧点为空，则赋OnMouseDown事件所获取的点值
                    if (m_oldPoint == null)
                        m_oldPoint = m_point;
                    // 新点为当前的鼠标点坐标
                    IPoint newPoint = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                    // 获取旧点角度
                    IPointCollection pPointCollection = new PolylineClass();
                    object missing = Type.Missing;
                    pPointCollection.AddPoint(centerPoint, ref missing, ref missing);
                    pPointCollection.AddPoint(m_oldPoint, ref missing, ref missing);
                    double oldAngle = GetAngle(pPointCollection as IPolyline);
                    // 获取新点角度
                    IPointCollection pointCollection = new PolylineClass();
                    pointCollection.AddPoint(centerPoint, ref missing, ref missing);
                    pointCollection.AddPoint(newPoint, ref missing, ref missing);
                    double newAngle = GetAngle(pointCollection as IPolyline);
                    // 旋转Element,角度为新旧点之差
                    ITransform2D pTransform2D = m_viewElement as ITransform2D;
                    pTransform2D.Rotate(centerPoint, (newAngle - oldAngle));
                    m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, m_viewElement, m_hookHelper.ActiveView.Extent);

                    // 更新旧点变量
                    m_oldPoint = newPoint;
                }
                else
                {
                    object missing = Type.Missing;
                    IPoint newPoint = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
                    IPointCollection pPointCollection = new PolylineClass();
                    pPointCollection.AddPoint(centerPoint, ref missing, ref missing);
                    pPointCollection.AddPoint(m_point, ref missing, ref missing);
                    double oldAngle = GetAngle(pPointCollection as IPolyline);

                    IPointCollection pointCollection = new PolylineClass();
                    pointCollection.AddPoint(centerPoint, ref missing, ref missing);
                    pointCollection.AddPoint(newPoint, ref missing, ref missing);
                    double newAngle = GetAngle(pointCollection as IPolyline);

                    ITransform2D pTransform2D = m_element as ITransform2D;
                    pTransform2D.Rotate(centerPoint, (newAngle - oldAngle));
                    m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, m_element, m_hookHelper.ActiveView.Extent);
                    m_point = newPoint;
                }
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            if (Button == 1)
            {
                if (m_element is AnnotationElement)
                {
                    IPoint centerPoint = new PointClass
                    {
                        X = (m_element.Geometry.Envelope.LowerLeft.X + m_element.Geometry.Envelope.LowerRight.X) / 2,
                        Y = (m_element.Geometry.Envelope.LowerLeft.Y + m_element.Geometry.Envelope.UpperRight.Y) / 2
                    };
                    if (AnnotationLayer == null)
                    {
                        //ILayer pLayer = CommonFunctions.GetFeatureLayer("注记", m_hookHelper);
                        //AnnotationLayer = pLayer as IFeatureLayer;
                    }
                    //IFeatureClass pFeatureClass = AnnotationLayer.FeatureClass;
                    //IDataset pDataset = (IDataset)pFeatureClass;
                    //IWorkspaceEdit pWorkspaceEdit = (IWorkspaceEdit)pDataset.Workspace;
                    //pWorkspaceEdit.StartEditing(true);
                    //pWorkspaceEdit.StartEditOperation();
                    //pWorkspaceEdit.EnableUndoRedo();

                    double angle = ((ITextElement)m_viewElement).Symbol.Angle;
                    double oldAngle = ((ITextElement)m_element).Symbol.Angle;
                    ITransform2D pTransform2D = m_element as ITransform2D;
                    pTransform2D.Rotate(centerPoint, (angle - oldAngle) * Math.PI / 180);

                    IGraphicsContainer pGraphicsContainer = m_hookHelper.ActiveView as IGraphicsContainer;
                    pGraphicsContainer.UpdateElement(m_element);
                    //pWorkspaceEdit.DisableUndoRedo();
                    //pWorkspaceEdit.StopEditOperation();
                    //pWorkspaceEdit.StopEditing(true);
                    m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, m_hookHelper.ActiveView.Extent);
                    m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, m_element, m_hookHelper.ActiveView.Extent);

                    pGraphicsContainer.DeleteElement(m_viewElement);
                    m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, m_viewElement, m_hookHelper.ActiveView.Extent);
                }
                m_moving = false;
                m_element = null;
                m_point = null;
                m_viewElement = null;
                m_oldPoint = null;
            }
        }

        #endregion

        #region 方法函数

        /// <summary>
        /// 获取线的角度（弧度）
        /// </summary>
        /// <param name="pPolyline">线</param>
        /// <returns></returns>
        private static double GetAngle(IPolyline pPolyline)
        {
            //IPolycurve pPolycurve;
            ILine pTangentLine = new Line();
            pPolyline.QueryTangent(esriSegmentExtension.esriNoExtension, 0.5, true, pPolyline.Length, pTangentLine);
            Double radian = pTangentLine.Angle;
            //Double angle = radian * 180 / Math.PI;
            //// 如果要设置正角度执行以下方法
            //while (angle < 0)
            //{
            //    angle = angle + 360;
            //}
            //// 返回角度
            //return angle;

            // 返回弧度
            return radian;
        }

        #endregion
    }
}
