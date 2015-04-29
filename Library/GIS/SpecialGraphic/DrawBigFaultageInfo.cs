using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using GIS.Common;
using LibEntity;

namespace GIS
{
    /// <summary>
    /// 绘制推断断层
    /// </summary>
    public class DrawBigFaultageInfo
    {
        /// <summary>
        /// 绘制推断断层
        /// </summary>
        /// <param name="title"></param>
        /// <param name="faultagePointList"></param>
        /// <param name="bId"></param>
        /// <returns></returns>
        public static bool DrawTddc(String title, List<BigFaultagePoint> faultagePointList, String bId)
        {
            List<IPoint> listptS = new List<IPoint>();
            List<IPoint> listptX = new List<IPoint>();

            foreach (var i in faultagePointList)
            {
                if (i.UpOrDown == "上盘")
                {
                    IPoint point = new PointClass();
                    point.X = i.CoordinateX;
                    point.Y = i.CoordinateY;
                    point.Z = i.CoordinateZ;

                    listptS.Add(point);
                }
                else if (i.UpOrDown == "下盘")
                {
                    IPoint point = new PointClass();
                    point.X = i.CoordinateX;
                    point.Y = i.CoordinateY;
                    point.Z = i.CoordinateZ;

                    listptX.Add(point);
                }
            }

            return DrawTDDC(title, bId, listptS, listptX);
        }


        /// <summary>
        /// 根据文本文件绘制推断断层
        /// </summary>
        /// <param name="filename">文本路径</param>
        /// <param name="bid"></param>
        /// <returns></returns>
        public static bool DrawTddcByFile(string filename, string bid)
        {
            try
            {
                string title = "";
                string[] strs = File.ReadAllLines(filename, Encoding.Default);
                List<IPoint> listptS = new List<IPoint>();
                List<IPoint> listptX = new List<IPoint>();
                string type = "";
                for (int i = 0; i < strs.Length; i++)
                {
                    if (i == 0)
                    {
                        title = strs[0];
                    }
                    if (strs[i] == "上盘")
                    {
                        type = "上盘";
                        continue;
                    }
                    if (strs[i] == "下盘")
                    {
                        type = "下盘";
                        continue;
                    }
                    if (strs[i].Equals(""))
                    {
                        continue;
                    }
                    string strx;
                    string stry;
                    string strz;
                    double x;
                    double y;
                    double z;
                    if (type == "上盘")
                    {
                        IPoint ptS = new PointClass();
                        strx = strs[i].Split(',')[0];
                        stry = strs[i].Split(',')[1];
                        strz = strs[i].Split(',')[2];
                        if (double.TryParse(strx, out x))
                        {
                            ptS.X = x;
                        }
                        else
                        {
                            MessageBox.Show(@"第" + (i + 1) + @"行非法X坐标！");
                            return false;
                        }
                        if (double.TryParse(stry, out y))
                        {
                            ptS.Y = y;
                        }
                        else
                        {
                            MessageBox.Show(@"第" + (i + 1) + @"行非法Y坐标！");
                            return false;
                        }
                        if (double.TryParse(strz, out z))
                        {
                            ptS.Z = z;
                        }
                        else
                        {
                            MessageBox.Show(@"第" + (i + 1) + @"行非法Z坐标！");
                            return false;
                        }
                        listptS.Add(ptS);
                    }
                    if (type == "下盘")
                    {
                        IPoint ptX = new PointClass();
                        strx = strs[i].Split(',')[0];
                        stry = strs[i].Split(',')[1];
                        strz = strs[i].Split(',')[2];
                        if (double.TryParse(strx, out x))
                        {
                            ptX.X = x;
                        }
                        else
                        {
                            MessageBox.Show(@"第" + (i + 1) + @"行非法X坐标！");
                            return false;
                        }
                        if (double.TryParse(stry, out y))
                        {
                            ptX.Y = y;
                        }
                        else
                        {
                            MessageBox.Show(@"第" + (i + 1) + @"行非法Y坐标！");
                            return false;
                        }
                        if (double.TryParse(strz, out z))
                        {
                            ptX.Z = z;
                        }
                        else
                        {
                            MessageBox.Show(@"第" + (i + 1) + @"行非法Z坐标！");
                            return false;
                        }
                        listptX.Add(ptX);
                    }
                }
                if (listptS.Count < 1)
                {
                    MessageBox.Show(@"上盘坐标读取失败！");
                    return false;
                }
                if (listptX.Count < 1)
                {
                    MessageBox.Show(@"下盘坐标读取失败！");
                    return false;
                }
                return DrawTDDC(title, bid, listptS, listptX);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 绘制推断断层
        /// </summary>
        /// <param name="title">文字</param>
        /// <param name="bid"></param>
        /// <param name="listptS">上盘坐标集合</param>
        /// <param name="listptX">下盘坐标集合</param>
        /// <returns></returns>
        public static bool DrawTDDC(string title, string bid, List<IPoint> listptS, List<IPoint> listptX)
        {
            try
            {
                const string sLayerAliasName = LayerNames.DEFALUT_INFERRED_FAULTAGE; //“推断断层”图层
                ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, sLayerAliasName);
                IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                if (pFeatureLayer == null)
                {
                    MessageBox.Show(@"推断断层图层缺失！");
                    return false;
                }

                INewBezierCurveFeedback pNewBezierCurveFeedback = new NewBezierCurveFeedbackClass();
                pNewBezierCurveFeedback.Display = DataEditCommon.g_pAxMapControl.ActiveView.ScreenDisplay;
                for (int i = 0; i < listptS.Count; i++)
                {
                    if (i == 0)
                        pNewBezierCurveFeedback.Start(listptS[0]);
                    else
                        pNewBezierCurveFeedback.AddPoint(listptS[i]);
                }
                IGeometry pGeometry = pNewBezierCurveFeedback.Stop();
                var polyline = (IPolyline)pGeometry;
                var pTopo = pGeometry as ITopologicalOperator;
                List<ziduan> ziduan = new List<ziduan>
                {
                    new ziduan("str", title),
                    new ziduan("type", "1"),
                    new ziduan("BID", bid)
                };
                IFeature pFeature = DataEditCommon.CreateNewFeature(pFeatureLayer, polyline, ziduan);
                DataEditCommon.g_pMap.SelectFeature(pFeatureLayer, pFeature);


                pNewBezierCurveFeedback = new NewBezierCurveFeedbackClass
                {
                    Display = DataEditCommon.g_pAxMapControl.ActiveView.ScreenDisplay
                };
                for (int i = 0; i < listptX.Count; i++)
                {
                    if (i == 0)
                        pNewBezierCurveFeedback.Start(listptX[0]);
                    else
                        pNewBezierCurveFeedback.AddPoint(listptX[i]);
                }
                IGeometry xGeometry = pNewBezierCurveFeedback.Stop();
                polyline = (IPolyline)xGeometry;
                ziduan = new List<ziduan> { new ziduan("str", ""), new ziduan("type", "2"), new ziduan("BID", bid) };
                pFeature = DataEditCommon.CreateNewFeature(pFeatureLayer, polyline, ziduan);
                if (pTopo != null)
                {
                    pTopo.Union(xGeometry);
                    DataEditCommon.g_pMap.SelectFeature(pFeatureLayer, pFeature);

                    MyMapHelp.Jump((IGeometry)pTopo);
                }
                DataEditCommon.g_pAxMapControl.ActiveView.PartialRefresh((esriViewDrawPhase)5, null, null);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static bool DelTddc(string bid)
        {
            try
            {
                const string sLayerAliasName = LayerNames.DEFALUT_INFERRED_FAULTAGE;
                ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap,
                    sLayerAliasName);
                IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                if (pFeatureLayer == null)
                {
                    MessageBox.Show(@"推断断层图层确实！");
                    return false;
                }
                return DataEditCommon.DeleteFeatureByBId(pFeatureLayer, bid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
    }

}
