using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;

namespace GIS
{
    public partial class MeasureResult : Form
    {
        #region 变量
        public double Segment;
        public double Total;
        public double Area;
        //MeasureDistance measureDistance = new MeasureDistance();
        private IHookHelper m_HookHelper;

        public struct myUnit
        {
            public esriUnits pUnit;
            public string UnitName;
        }
        public myUnit inUnit, outUnit;
        public myUnit inAreaUnit, outAreaUnit;
        private myUnit PriMapUnit;

        private struct ConvertValue
        {
            public esriUnits units;
            public double value;
        }
        ConvertValue m_SegLen, m_TotLen, m_Area;
        public bool closeauto = false;
        private IUnitConverter unitConverter = new UnitConverterClass();

        #endregion

        public MeasureResult(IHookHelper myhookhelper)
        {
            InitializeComponent();
            //measureDistance.MsgInfo = this;
            m_HookHelper = myhookhelper;
            GetMapUnit();
            //esriAreaUnits.esriSquareMeters
        }

        /// <summary>
        /// 获得当前地图单位
        /// </summary>
        public void GetMapUnit()
        {
            string myMapUnit = m_HookHelper.FocusMap.MapUnits.ToString().Remove(0, 4);
            PriMapUnit.pUnit = m_HookHelper.FocusMap.MapUnits;
            PriMapUnit.UnitName = myMapUnit;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MeasureResult_Load(object sender, EventArgs e)
        {
            tsbtnClear.Visible = false;
            inUnit.pUnit = PriMapUnit.pUnit;
            inUnit.UnitName = PriMapUnit.UnitName;

            foreach (ToolStripMenuItem tool in this.tsmiAreaUnit.DropDownItems)
                tool.CheckOnClick = true;

            int CheckUnit = tsmiAreaUnit.DropDownItems.Count;

            if (PriMapUnit.pUnit == esriUnits.esriDecimalDegrees)
            {
                foreach (ToolStripMenuItem tool in tsmiAreaUnit.DropDownItems)
                    tool.Enabled = false;
            }

            else
            {
                foreach (ToolStripMenuItem tool in tsmiAreaUnit.DropDownItems)
                {
                    if (tool.Tag.ToString() == PriMapUnit.UnitName)
                    {
                        tool.Checked = true;
                        CheckUnit--;
                        break;
                    }
                }
            }

            if (CheckUnit < tsmiAreaUnit.DropDownItems.Count)
            {
                inAreaUnit.pUnit = PriMapUnit.pUnit;
                inAreaUnit.UnitName = PriMapUnit.UnitName;
            }
            else
            {
                inAreaUnit.pUnit = esriUnits.esriUnknownUnits;
                inAreaUnit.UnitName = "Unkown Units";
            }
            
            outUnit.pUnit = inUnit.pUnit;
            outUnit.UnitName = inUnit.UnitName;

            outAreaUnit.pUnit = inAreaUnit.pUnit;
            outAreaUnit.UnitName = inAreaUnit.UnitName;

            tsmiDisMeters.Checked = true;
        }


        /// <summary>
        /// 
        /// </summary>
        public void LineResultChange()
        {
            //double segLength = Math.Round(Segment, 6);
            //double totalLength = Math.Round(Total, 6);
            //txtResultInfo.Text = "当前长度：" + segLength + inUnit.UnitName +
            //    "\r\n总长度：" + totalLength + inUnit.UnitName;
            txtMessageChangelength();
        }

        /// <summary>
        /// 
        /// </summary>
        public void PolygonResultChange()
        {
            //double segLength = Math.Round(Segment, 6);
            //double totalLength = Math.Round(Total, 6); 
            //double area = ConvertToArea(Area, inAreaUnit.UnitName, outAreaUnit.UnitName);
            //area = Math.Round(Area, 6);

            //string areaUnit;
            //if (outAreaUnit.UnitName == "Unkown Units")
            //    areaUnit = outAreaUnit.UnitName;
            //else
            //    areaUnit = "Square " + outAreaUnit.UnitName;

            //txtResultInfo.Text = "当前长度：" + segLength + inUnit.UnitName +
            //    "\r\n周长：" + totalLength + inUnit.UnitName +
            //    "\r\n面积：" + area + areaUnit;
            txtMessageChange();
        }

        private void MeasureResult_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(closeauto==false)
                GIS.Common.DataEditCommon.g_pAxMapControl.CurrentTool = null;
            ClearLayer();
        }
              
        /// <summary>
        /// 清空视图
        /// </summary>
        public void ClearLayer()
        {
            if (m_HookHelper == null)
            {
                return;
            }
            IGraphicsContainer pGraphicsContainer = m_HookHelper.FocusMap as IGraphicsContainer;
            pGraphicsContainer.DeleteAllElements();
            m_HookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            m_HookHelper.ActiveView.Refresh();
        }

        private void tsmiDisKilometers_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Checked == false)
            {
                foreach (ToolStripMenuItem tool in this.tsmiDistanceUnit.DropDownItems)
                    tool.Checked = false;
                //tsmiDisKilometers.Checked = true;
                ((ToolStripMenuItem)sender).Checked = true;
                string danwei = ((ToolStripMenuItem)sender).Tag.ToString();
                switch(danwei)
                {
                    case "Kilometers":
                     outUnit.pUnit = esriUnits.esriKilometers;
                        break;
                    case "Meters":
                        outUnit.pUnit = esriUnits.esriMeters;
                        break;
                    case "Decimeters":
                        outUnit.pUnit = esriUnits.esriDecimeters;
                        break;
                    case "Centimeters":
                        outUnit.pUnit = esriUnits.esriCentimeters;
                        break;
                    case "Millimeters":
                        outUnit.pUnit = esriUnits.esriMillimeters;
                        break;
                    case "Miles":
                        outUnit.pUnit = esriUnits.esriMiles;
                        break;
                    case "NauticalMiles":
                        outUnit.pUnit = esriUnits.esriNauticalMiles;
                        break;
                    case "Yards":
                        outUnit.pUnit = esriUnits.esriYards;
                        break;
                    case "Feet":
                        outUnit.pUnit = esriUnits.esriFeet;
                        break;
                    case "Inches":
                        outUnit.pUnit = esriUnits.esriInches;
                        break;
                    case "DecimalDegrees":
                        outUnit.pUnit = esriUnits.esriDecimalDegrees;
                        break;
            }
                outUnit.UnitName = danwei;//"Kilometers";
                txtMessageChangelength();
            }
        }
        private void txtMessageChangelength()
        {
            double segmentLength = unitConverter.ConvertUnits(Segment, inUnit.pUnit, outUnit.pUnit);
            double perimeter = unitConverter.ConvertUnits(Total, inUnit.pUnit, outUnit.pUnit);
            //double area = ConvertToLength(Area, inAreaUnit.UnitName, outAreaUnit.UnitName);

            string areaUnit;
            if (outAreaUnit.UnitName == "Unkown Units")
                areaUnit = outAreaUnit.UnitName;
            else
                areaUnit = "Square " + outAreaUnit.UnitName;

            segmentLength = Math.Round(segmentLength, 6);
            perimeter = Math.Round(perimeter, 6);
            //area = Math.Round(area, 6);
            this.txtResultInfo.Text = "测量线" +
                "\r\n线段长是" + segmentLength + unitToCh(tsmiDistanceUnit) +
                "\r\n总长是：" + perimeter + unitToCh(tsmiDistanceUnit);
               //"\r\n面积是：" + area + areaUnit;

        }
        private void txtMessageChange()
        {
            double segmentLength = unitConverter.ConvertUnits(Segment, inUnit.pUnit, outUnit.pUnit);
            double perimeter = unitConverter.ConvertUnits(Total, inUnit.pUnit, outUnit.pUnit);
            double area = ConvertToArea(Area, inAreaUnit.UnitName, outAreaUnit.UnitName);

            string areaUnit;
            if (outAreaUnit.UnitName == "Unkown Units")
                areaUnit = outAreaUnit.UnitName;
            else
                areaUnit = "Square " + outAreaUnit.UnitName;

            segmentLength = Math.Round(segmentLength, 6);
            perimeter = Math.Round(perimeter, 6);
            area = Math.Round(area, 6);
            this.txtResultInfo.Text = "测量面" +
                "\r\n线段长是" + segmentLength + unitToCh(tsmiAreaUnit) +
                "\r\n周长是：" + perimeter + unitToCh(tsmiAreaUnit) +
                "\r\n面积是：" + area + unitToCh(tsmiAreaUnit);

        }

        private double ConvertToArea(double pArea, string oldUnit, string newUnit)
        {
            double convertarea = pArea;
            if (oldUnit == "Feet")
            {
                if (newUnit == "Kilometers")
                    convertarea = pArea * 0.09290304 / 1000000;
                else if (newUnit == "Meters")
                    convertarea = pArea * 0.09290304;
                else if (newUnit == "Miles")
                    convertarea = pArea * 0.00000003587;
                else if (newUnit == "Yards")
                    convertarea = pArea * 0.1111111;
                else if (newUnit == "Hectares")
                    convertarea = pArea * 0.0000093;
                else if (newUnit == "Acres")
                    convertarea = pArea * 0.000023;
            }
            else if (oldUnit == "Meters")
            {
                if (newUnit == "Feet")
                    convertarea = pArea * 10.76391042;
                else if (newUnit == "Kilometers")
                    convertarea = pArea * 1.0e-6;
                else if (newUnit == "Miles")
                    convertarea = pArea * 3.8610216e-7;
                else if (newUnit == "Yards")
                    convertarea = pArea * 1.1959900;
                else if (newUnit == "Hectares")
                    convertarea = pArea * 0.0001;
                else if (newUnit == "Acres")
                    convertarea = pArea * 0.0002471;
            }
            else if (oldUnit == "Miles")
            {
                if (newUnit == "Feet")
                    convertarea = pArea * 27878400;
                else if (newUnit == "Yards")
                    convertarea = pArea * 3097600;
                else if (newUnit == "Kilometers")
                    convertarea = pArea * 2.5899881;
                else if (newUnit == "Meters")
                    convertarea = pArea * 2589988.110336;
                else if (newUnit == "Hectares")
                    convertarea = pArea * 258.9988110;
                else if (newUnit == "Acres")
                    convertarea = pArea * 640;

            }
            else if (oldUnit == "Kilometers")
            {
                if (newUnit == "Feet")
                    convertarea = pArea * 10763910.4167097;
                else if (newUnit == "Yards")
                    convertarea = pArea * 1195990.0463011;
                else if (newUnit == "Miles")
                    convertarea = pArea * 0.3861022;
                else if (newUnit == "Meters")
                    convertarea = pArea * 1000000;
                else if (newUnit == "Hectares")
                    convertarea = pArea * 100;
                else if (newUnit == "Acres")
                    convertarea = pArea * 247.1053815;
            }

            return convertarea;

        }

        private void tsbtnClear_Click(object sender, EventArgs e)
        {
            ClearLayer();
            this.txtResultInfo.Text = "";
        }

        private void tsmiAreaKilo_Click(object sender, EventArgs e)
        {
            //if (((ToolStripMenuItem)sender).Checked == false)
            //{
                foreach (ToolStripMenuItem tool in this.tsmiAreaUnit.DropDownItems)
                    tool.Checked = false;
                //tsmiDisKilometers.Checked = true;
                ((ToolStripMenuItem)sender).Checked = true;
                string danwei = ((ToolStripMenuItem)sender).Tag.ToString();
                switch (danwei)
                {
                    case "Kilometers":
                        outUnit.pUnit = esriUnits.esriKilometers;
                        outAreaUnit.pUnit = esriUnits.esriKilometers;
                        break;
                    case "Meters":
                        outUnit.pUnit = esriUnits.esriMeters;
                        outAreaUnit.pUnit = esriUnits.esriMeters;
                        break;
                    case "Miles":
                        outUnit.pUnit = esriUnits.esriMiles;
                        outAreaUnit.pUnit = esriUnits.esriMiles;
                        break;
                    case "Yards":
                        outUnit.pUnit = esriUnits.esriYards;
                        outAreaUnit.pUnit = esriUnits.esriYards;
                        break;
                    case "Feet":
                        outUnit.pUnit = esriUnits.esriFeet;
                        outAreaUnit.pUnit = esriUnits.esriFeet;
                        break;
                    case "Hectares":
                        outUnit.pUnit = esriUnits.esriInches;
                        outAreaUnit.pUnit = esriUnits.esriInches;
                        break;
                    case "Acres":
                        outUnit.pUnit = esriUnits.esriDecimalDegrees;
                        outAreaUnit.pUnit = esriUnits.esriDecimalDegrees;
                        break;
                }
                outUnit.UnitName = danwei;//"Kilometers";
                outAreaUnit.UnitName = danwei;
                //outUnit.pUnit = esriUnits.esriKilometers;
                //outUnit.UnitName = "Kilometers";
                txtMessageChange();
            //}
        }
        private double ConvertToLength(double pArea, string oldUnit, string newUnit)
        {
            double convertarea = pArea;
            if (oldUnit == "Kilometers")
            {
                if (newUnit == "Meters")
                    convertarea = pArea * 1000;
            }
            else if (oldUnit == "Meters")
            {
                if (newUnit == "Kilometers")
                    convertarea = pArea / 1000;
            }
            return convertarea;

        }
        private string unitToCh(ToolStripMenuItem toolMenu)
        {
            string unit = "米";
            foreach (ToolStripMenuItem tool in toolMenu.DropDownItems)
            {
                if (tool.Checked)
                {
                    unit = tool.Text;
                    break;
                }
            }
            return unit;
        }
    }
}

