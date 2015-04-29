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
    public class SpatialReferenceHelper
    {
        // Fields  
        private const double ParaC = 6399596.65198801;
        private const double ParaE1 = 0.00669438499958795;
        private const double Parak0 = 1.57048687472752E-07;
        private const double Parak1 = 0.00505250559291393;
        private const double Parak2 = 2.98473350966158E-05;
        private const double Parak3 = 2.41627215981336E-07;
        private const double Parak4 = 2.22241909461273E-09;

        // Methods  

        public static double DistanceOfTwoEarthPoints(double lng1, double lat1, double lng2, double lat2, GaussSphere gs)
        {
            double d = Rad(lat1);
            double num2 = Rad(lat2);
            double num3 = d - num2;
            double num4 = Rad(lng1) - Rad(lng2);
            double num5 = 2.0 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(num3 / 2.0), 2.0) + ((Math.Cos(d) * Math.Cos(num2)) * Math.Pow(Math.Sin(num4 / 2.0), 2.0))));
            num5 *= (gs == GaussSphere.WGS84) ? 6378137.0 : ((gs == GaussSphere.Xian80) ? 6378140.0 : 6378245.0);
            return (Math.Round((double)(num5 * 10000.0)) / 10000.0);
        }

        public static string FormatJWD(double pJWD)
        {
            int num = (int)pJWD;
            double num2 = (pJWD - num) * 60.0;
            int num3 = (int)num2;
            num2 = (num2 - num3) * 60.0;
            int num4 = (int)num2;
            return string.Concat(new object[] { num, "\x00b0", num3, "'", num4, "\"" });
        }

        public static string FormatJWDEx(double pJWD)
        {
            int num = (int)pJWD;
            double num2 = (pJWD - num) * 60.0;
            int num3 = (int)num2;
            num2 = (num2 - num3) * 60.0;
            int num4 = (int)num2;
            return string.Concat(new object[] { num, "度", num3, "分", num4, "秒" });
        }

        public static string FormatSpatialReference(ISpatialReference sr)
        {
            try
            {
                if (sr == null)
                {
                    return "";
                }
                StringBuilder builder = new StringBuilder();
                if (sr is UnknownCoordinateSystemClass)
                {
                    builder.Append("UnknownCoordinateSystem");
                }
                else
                {
                    if (sr.Name == "Unknown")
                    {
                        builder.Append("UnknownCoordinateSystem");
                    }
                    else
                    {
                        IGeographicCoordinateSystem system2;
                        if (sr is IProjectedCoordinateSystem)
                        {
                            Exception exception;
                            IProjectedCoordinateSystem system = sr as IProjectedCoordinateSystem;
                            builder.Append("投影坐标系:\n");
                            builder.Append("  Name:").Append(system.Name).Append("\n");
                            builder.Append("  Alias:").Append(system.Alias).Append("\n");
                            builder.Append("  Abbreviation:").Append(system.Abbreviation).Append("\n");
                            builder.Append("  Remarks:").Append(system.Remarks).Append("\n");
                            builder.Append("投影:").Append(system.Projection.Name).Append("\n");
                            builder.Append("投影参数:\n");
                            builder.Append("   False_Easting:").Append(system.FalseEasting).Append("\n");
                            builder.Append("   False_Northing:").Append(system.FalseNorthing).Append("\n");
                            builder.Append("   Central_Meridian:").Append(system.get_CentralMeridian(true)).Append("\n");
                            try
                            {
                                builder.Append("   Scale_Factor:").Append(system.ScaleFactor).Append("\n");
                            }
                            catch { }
                            builder.Append("   Latitude_Of_Origin:0\n");
                            builder.Append("Linear Unit:").Append(system.CoordinateUnit.Name).Append("(").Append(system.CoordinateUnit.MetersPerUnit).Append(")\n");
                            builder.Append("Geographic Coordinate System:\n");
                            system2 = system.GeographicCoordinateSystem;
                            builder.Append("  Name:").Append(system2.Name).Append("\n");
                            builder.Append("  Alias:").Append(system2.Alias).Append("\n");
                            builder.Append("  Abbreviation:").Append(system2.Abbreviation).Append("\n");
                            builder.Append("  Remarks:").Append(system2.Remarks).Append("\n");
                            builder.Append("  Angular Unit:").Append(system2.CoordinateUnit.Name).Append("(").Append(system2.CoordinateUnit.RadiansPerUnit).Append(")\n");
                            builder.Append("  Prime Meridian:").Append(system2.PrimeMeridian.Name).Append("(").Append(system2.PrimeMeridian.Longitude).Append(")\n");
                            builder.Append("  Datum:").Append(system2.Datum.Name).Append("\n");
                            builder.Append("    Spheroid:").Append(system2.Datum.Spheroid.Name).Append("\n");
                            builder.Append("      Semimajor Axis:").Append(system2.Datum.Spheroid.SemiMajorAxis).Append("\n");
                            builder.Append("      Semiminor Axis:").Append(system2.Datum.Spheroid.SemiMinorAxis).Append("\n");
                            builder.Append("      Inverse Flattening:").Append((double)(1.0 / system2.Datum.Spheroid.Flattening)).Append("\n");
                            builder.Append("X/Y Domain:\n");
                            try
                            {
                                double num = 0.0;
                                double num2 = 0.0;
                                double num3 = 0.0;
                                double num4 = 0.0;
                                double num5 = 0.0;
                                sr.GetDomain(out num, out num3, out num2, out num4);
                                sr.GetFalseOriginAndUnits(out num, out num2, out num5);
                                builder.Append(" Min X:").Append(num).Append("\n");
                                builder.Append(" Min Y:").Append(num2).Append("\n");
                                builder.Append(" Max X:").Append(num3).Append("\n");
                                builder.Append(" Max Y:").Append(num4).Append("\n");
                                builder.Append(" XYScale:").Append(num5).Append("\n");
                                builder.Append("\n");
                            }
                            catch (Exception exception1)
                            {
                                exception = exception1;
                            }
                            builder.Append("Z Domain:\n");
                            try
                            {
                                double num6;
                                double num7;
                                double num8 = 0.0;
                                sr.GetZDomain(out num6, out num7);
                                sr.GetZFalseOriginAndUnits(out num6, out num8);
                                builder.Append("  Min Z:").Append(num6).Append("\n");
                                builder.Append("  Max Z:").Append(num7).Append("\n");
                                builder.Append("  ZScale:").Append(num8).Append("\n");
                                builder.Append("\n");
                            }
                            catch (Exception exception2)
                            {
                                exception = exception2;
                            }
                            try
                            {
                                double num9;
                                double num10;
                                builder.Append("M Domain:\n");
                                double num11 = 0.0;
                                sr.GetMDomain(out num9, out num10);
                                sr.GetMFalseOriginAndUnits(out num9, out num11);
                                builder.Append("  Min M:").Append(num9).Append("\n");
                                builder.Append("  Max M:").Append(num10).Append("\n");
                                builder.Append("  MScale:").Append(num11).Append("\n");
                            }
                            catch (Exception exception3)
                            {
                                exception = exception3;
                            }
                        }
                        else if (sr is IGeographicCoordinateSystem)
                        {
                            builder.Append("Geographic Coordinate System:\n");
                            system2 = sr as IGeographicCoordinateSystem;
                            builder.Append("  Name:").Append(system2.Name).Append("\n");
                            builder.Append("  Alias:").Append(system2.Alias).Append("\n");
                            builder.Append("  Abbreviation:").Append(system2.Abbreviation).Append("\n");
                            builder.Append("  Remarks:").Append(system2.Remarks).Append("\n");
                            builder.Append("  Angular Unit:").Append(system2.CoordinateUnit.Name).Append("(").Append(system2.CoordinateUnit.RadiansPerUnit).Append(")\n");
                            builder.Append("  Prime Meridian:").Append(system2.PrimeMeridian.Name).Append("(").Append(system2.PrimeMeridian.Longitude).Append(")\n");
                            builder.Append("  Datum:").Append(system2.Datum.Name).Append("\n");
                            builder.Append("    Spheroid:").Append(system2.Datum.Spheroid.Name).Append("\n");
                            builder.Append("      Semimajor Axis:").Append(system2.Datum.Spheroid.SemiMajorAxis).Append("\n");
                            builder.Append("      Semiminor Axis:").Append(system2.Datum.Spheroid.SemiMinorAxis).Append("\n");
                            builder.Append("      Inverse Flattening:").Append((double)(1.0 / system2.Datum.Spheroid.Flattening)).Append("\n");
                        }
                    }
                }
                return builder.ToString();
            }
            catch (Exception ex)
            { return ""; }
        }

        public static void GaussToGeo(double y, double x, short DH, out double L, out double B, double LP)
        {
            double num9 = 3.14159265358979;
            double num10 = 0.0067385254147;
            double num11 = 6399698.90178271;
            double num8 = (x / 1000000.0) - 3.0;
            double num6 = (((((27.11115372595 + (9.02468257083 * num8)) - (0.00579740442 * Math.Pow(num8, 2.0))) - (0.00043532572 * Math.Pow(num8, 3.0))) + (4.857285E-05 * Math.Pow(num8, 4.0))) + (2.15727E-06 * Math.Pow(num8, 5.0))) - (1.9399E-07 * Math.Pow(num8, 6.0));
            double num2 = Math.Tan((num6 * num9) / 180.0);
            double num7 = num10 * Math.Pow(Math.Cos((num6 * num9) / 180.0), 2.0);
            double num3 = (y * Math.Sqrt(1.0 + num7)) / num11;
            double num5 = num6 - ((((1.0 + num7) * num2) / num9) * (((90.0 * Math.Pow(num3, 2.0)) - ((7.5 * (((5.0 + (3.0 * Math.Pow(num2, 2.0))) + num7) - ((9.0 * num7) * Math.Pow(num2, 2.0)))) * Math.Pow(num3, 4.0))) + ((0.25 * ((61.0 + (90.0 * Math.Pow(num2, 2.0))) + (45.0 * Math.Pow(num2, 4.0)))) * Math.Pow(num3, 6.0))));
            double num4 = (((180.0 * num3) - ((30.0 * ((1.0 + (2.0 * Math.Pow(num2, 2.0))) + num7)) * Math.Pow(num3, 3.0))) + ((1.5 * ((5.0 + (28.0 * Math.Pow(num2, 2.0))) + (24.0 * Math.Pow(num2, 4.0)))) * Math.Pow(num3, 5.0))) / (num9 * Math.Cos((num6 * num9) / 180.0));
            double num = num4 * 3600.0;
            if (LP == -1000.0)
            {
                L = (((DH * 6) - 3) * 3600.0) + num;
            }
            else
            {
                L = (LP * 3600.0) + num;
            }
            B = num5 * 3600.0;
        }

        public static void GeoToGauss(double jd, double wd, short DH, short DH_width, out double y, out double x, double LP)
        {
            double num2;
            double num12 = 3.14159265358979;
            double num13 = 0.0067385254147;
            double num14 = 6399698.90178271;
            double num4 = ((jd / 3600.0) * num12) / 180.0;
            double a = ((wd / 3600.0) * num12) / 180.0;
            if (LP == -1000.0)
            {
                num2 = (DH - 0.5) * DH_width;
            }
            else
            {
                num2 = LP;
            }
            double num3 = (jd / 3600.0) - num2;
            double num10 = Math.Sin(a);
            double num11 = Math.Cos(a);
            double num8 = (30.870794749999998 * wd) - (((((32005.7799 * num10) + (133.9238 * Math.Pow(num10, 3.0))) + (0.6976 * Math.Pow(num10, 5.0))) + (0.0039 * Math.Pow(num10, 7.0))) * num11);
            double num6 = num13 * Math.Pow(num11, 2.0);
            double num7 = num14 / Math.Sqrt(1.0 + num6);
            double num = Math.Tan(a);
            double num9 = ((num12 / 180.0) * num3) * num11;
            x = num8 + ((num7 * num) * (((0.5 * Math.Pow(num9, 2.0)) + (((((5.0 - Math.Pow(num, 2.0)) + (9.0 * num6)) + (4.0 * Math.Pow(num6, 2.0))) * Math.Pow(num9, 4.0)) / 24.0)) + ((((61.0 - (58.0 * Math.Pow(num, 2.0))) + Math.Pow(num, 4.0)) * Math.Pow(num9, 6.0)) / 720.0)));
            y = num7 * ((num9 + ((((1.0 - Math.Pow(num, 2.0)) + num6) * Math.Pow(num9, 3.0)) / 6.0)) + ((((((5.0 - (18.0 * Math.Pow(num, 2.0))) + Math.Pow(num, 4.0)) + (14.0 * num6)) - ((58.0 * num6) * Math.Pow(num, 2.0))) * Math.Pow(num9, 5.0)) / 120.0));
        }

        private void GKTransform(double x, double y, ref double B, ref double L, double center, int n)
        {
            double num = (y - 500000.0) - (0xf4240 * n);
            double a = 1.57048687472752E-07 * x;
            double num4 = Math.Sin(a);
            double num2 = a + (Math.Cos(a) * ((((0.00505250559291393 * num4) - (2.98473350966158E-05 * Math.Pow(num4, 3.0))) + (2.41627215981336E-07 * Math.Pow(num4, 5.0))) - (2.22241909461273E-09 * Math.Pow(num4, 7.0))));
            double num6 = Math.Tan(num2);
            double num8 = 0.00669438499958795 * Math.Pow(Math.Cos(num2), 2.0);
            double num5 = Math.Sqrt(1.0 + num8);
            double num7 = 6399596.65198801 / num5;
            double num10 = num / num7;
            double num9 = Math.Pow(num5, 2.0) * num6;
            double num11 = Math.Pow(num6, 2.0);
            B = ((num2 - ((num9 * Math.Pow(num10, 2.0)) / 2.0)) + ((((((5.0 + (3.0 * num11)) + num8) - ((9.0 * num8) * num11)) * num9) * Math.Pow(num10, 4.0)) / 24.0)) - (((((61.0 + (90.0 * num11)) + (45.0 * Math.Pow(num11, 2.0))) * num9) * Math.Pow(num10, 6.0)) / 720.0);
            double num13 = 1.0 / Math.Cos(num2);
            L = (((num13 * num10) - (((((1.0 + (2.0 * num11)) + num8) * num13) * Math.Pow(num10, 3.0)) / 6.0)) + (((((((5.0 + (28.0 * num11)) + (24.0 * Math.Pow(num11, 2.0))) + (6.0 * num8)) + ((8.0 * num8) * num11)) * num13) * Math.Pow(num10, 5.0)) / 120.0)) + center;
        }

        public static IPoint JWD2XY(IPoint pJWDPoint, int gcsType, int pcsType)
        {
            try
            {
                ISpatialReferenceFactory factory = new SpatialReferenceEnvironmentClass();
                pJWDPoint.SpatialReference = factory.CreateGeographicCoordinateSystem(gcsType);
                pJWDPoint.Project(factory.CreateProjectedCoordinateSystem(pcsType));
                return pJWDPoint;
            }
            catch
            {
                return null;
            }
        }

        private static double Rad(double d)
        {
            return ((d * 3.1415926535897931) / 180.0);
        }

        public static IPoint XY2JWD(double x, double y, int gcsType, int pcsType)
        {
            try
            {
                IPoint point = new PointClass();
                point.PutCoords(x, y);
                ISpatialReferenceFactory factory = new SpatialReferenceEnvironmentClass();
                point.SpatialReference = factory.CreateProjectedCoordinateSystem(pcsType);
                point.Project(factory.CreateGeographicCoordinateSystem(gcsType));
                return point;
            }
            catch
            {
                return null;
            }
        }

        public static IPoint XY2JYD(ISpatialReference aSR, IPoint pXY)
        {
            IPoint point = new PointClass();
            point = (pXY as IClone).Clone() as IPoint;
            if (aSR == null)
            {
                return point;
            }
            if (aSR.Name != "Unknown")
            {
                point.SpatialReference = aSR;
                point.SnapToSpatialReference();
                if (aSR is IProjectedCoordinateSystem)
                {
                    IGeographicCoordinateSystem system = (aSR as IProjectedCoordinateSystem).GeographicCoordinateSystem;
                    point.Project(system);
                }
                return point;
            }
            return null;
        }

        // Nested Types  
        public enum GaussSphere
        {
            Beijing54,
            Xian80,
            WGS84
        }

        public static ISpatialReference SelectSR(String pFolderPath)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "ESRI 投影信息文件(*.prj)|*.prj";
            ofd.Title = "读取ESRI投影定义文件";
            ofd.Multiselect = false;
            if (pFolderPath != null &&
                Directory.Exists(pFolderPath))
            {
                ofd.InitialDirectory = pFolderPath;
            }
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filePath = ofd.FileName;
                return ReadSR(filePath);
            }
            else return null;
        }
        public static ISpatialReference ReadSR(string pPrjFilePath)
        {
            if (File.Exists(pPrjFilePath))
            {
                StreamReader aReader = new StreamReader(pPrjFilePath);
                string aLine = aReader.ReadToEnd();
                aReader.Close();
                int byteCount = 0;
                if (aLine.StartsWith("GEOGCS"))
                {
                    GeographicCoordinateSystemClass sr = new GeographicCoordinateSystemClass();
                    (sr as IESRISpatialReference).ImportFromESRISpatialReference(aLine, out byteCount);
                    return sr;
                }
                else
                {
                    ProjectedCoordinateSystemClass sr = new ProjectedCoordinateSystemClass();
                    (sr as IESRISpatialReference).ImportFromESRISpatialReference(aLine, out byteCount);
                    return sr;
                }
            }
            else
            {
                return null;
            }
        }
    }  
}
