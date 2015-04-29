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
using System.IO;

namespace GIS.Common
{
    public class WorkspaceHelper
    {
        public static string GISConnectionString;
        public static IWorkspace GetAccessWorkspace(string sFilePath)
        {
            if (!File.Exists(sFilePath))
            {
                return null;
            }
            try
            {
                IWorkspaceFactory factory = new AccessWorkspaceFactoryClass();
                return factory.OpenFromFile(sFilePath, 0);
            }
            catch
            {
                return null;
            }
        }
        public static IWorkspace GetSDEWorkspace(string sServerName, string sInstancePort, string sUserName, string sPassword, string sVersionName)
        {
            IPropertySet set = new PropertySetClass();
            set.SetProperty("Server", sServerName);
            set.SetProperty("Instance", sInstancePort);
            set.SetProperty("User", sUserName);
            set.SetProperty("password", sPassword);
            set.SetProperty("version", sVersionName);
            SdeWorkspaceFactoryClass class2 = new SdeWorkspaceFactoryClass();
            try
            {
                return class2.Open(set, 0);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static IWorkspace GetFGDBWorkspace(string sFilePath)
        {
            if (!System.IO.Directory.Exists(sFilePath))
            {
                return null;
            }
            try
            {
                IWorkspaceFactory factory = new FileGDBWorkspaceFactoryClass();
                return factory.OpenFromFile(sFilePath, 0);
            }
            catch
            {
                return null;
            }
        }
        public static IWorkspace GetShapefileWorkspace(string sFilePath)
        {
            if (!File.Exists(sFilePath))
            {
                return null;
            }
            try
            {
                IWorkspaceFactory factory = new ShapefileWorkspaceFactoryClass();
                sFilePath = System.IO.Path.GetDirectoryName(sFilePath);
                return factory.OpenFromFile(sFilePath, 0);
            }
            catch
            {
                return null;
            }
        }
        public static string PGDBDataConnectionString(string sPath)
        {
            return ("Provider=ESRI.GeoDB.OLEDB.1;Data Source=" + sPath + ";Extended Properties=workspacetype=esriDataSourcesGDB.AccessWorkspaceFactory.1;Geometry=WKB");
        }
        public static string SDEDataConnectionString(string sServerName, string sDataSource, string sUserName, string sPW)
        {
            return ("Provider=ESRI.GeoDB.OLEDB.1;Location=" + sServerName + ";Data Source=" + sDataSource + "; User Id=" + sUserName + ";Password=" + sPW + "; Extended Properties=WorkspaceType= esriDataSourcesGDB.SDEWorkspaceFactory.1;Geometry=WKB|OBJECT;Instance=5151;Version=SDE.DEFAULT");
        }
        public static string ShapefileDataConnectionString(string sPath)
        {
            sPath = System.IO.Path.GetDirectoryName(sPath);
            return ("Provider=ESRI.GeoDB.OLEDB.1;Data Source=" + sPath + ";Extended Properties=WorkspaceType=esriDataSourcesFile.ShapefileWorkspaceFactory.1;Geometry=WKB|OBJECT");
        }
        public static bool HighPrecision(IWorkspace pWorkspace)
        {
            IGeodatabaseRelease geoVersion = pWorkspace as IGeodatabaseRelease;
            if (geoVersion == null) return false;
            if (geoVersion.MajorVersion == 2
                && geoVersion.MinorVersion == 2)
            {
                return true;
            }
            return false;
        }
        public static List<String> QueryFeatureClassName(IWorkspace pWorkspace)
        {
            return QueryFeatureClassName(pWorkspace, false, false);
        }
        public static List<String> QueryFeatureClassName(IWorkspace pWorkspace, bool pUpperCase)
        {
            return QueryFeatureClassName(pWorkspace, pUpperCase, false);
        }
        public static List<String> QueryFeatureClassName(IWorkspace pWorkspace, bool pUpperCase, bool pEscapeMetaTable)
        {
            try
            {
                String ownerName = "";
                if (pWorkspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                {
                    ownerName = pWorkspace.ConnectionProperties.GetProperty("user").ToString();
                    ownerName = ownerName.ToUpper();
                }
                List<String> sc = new List<String>();
                IEnumDatasetName edn = pWorkspace.get_DatasetNames(esriDatasetType.esriDTFeatureDataset);
                IDatasetName dn = edn.Next();
                while (dn != null)
                {
                    string dsName = dn.Name.ToUpper();
                    if (ownerName.Equals(LayerHelper.GetClassOwnerName(dsName)))
                    {
                        #region 添加数据集下面的FeatureClass
                        IEnumDatasetName fdn = dn.SubsetNames;

                        dn = fdn.Next();
                        while (dn != null)
                        {
                            dsName = dn.Name.ToUpper();
                            bool isTopology = dn is ITopologyName;
                            if (!isTopology)
                            {
                                string shortName = LayerHelper.GetClassShortName(dsName);
                                if (pUpperCase)
                                {
                                    shortName = shortName.ToUpper();
                                }
                                if (pEscapeMetaTable)
                                {

                                }
                                else
                                {
                                    sc.Add(shortName);
                                }
                            }
                            dn = fdn.Next();
                        }
                        #endregion
                    }
                    dn = edn.Next();
                }
                #region 获取直接的FeatureClass
                edn = pWorkspace.get_DatasetNames(esriDatasetType.esriDTFeatureClass);
                dn = edn.Next();
                while (dn != null)
                {
                    string dsName = dn.Name.ToUpper();
                    if (ownerName.Equals(LayerHelper.GetClassOwnerName(dsName)))
                    {
                        string shortName = LayerHelper.GetClassShortName(dsName);
                        if (pUpperCase)
                        {
                            shortName = shortName.ToUpper();
                        }
                        if (pEscapeMetaTable)
                        {

                        }
                        else
                        {
                            sc.Add(shortName);
                        }
                    }
                    dn = edn.Next();
                }
                #endregion
                return sc;
            }
            catch (Exception ex) { return null; }
        }
        public static List<IConfigurationKeyword> GetConfigurationKeywordList(IWorkspace pWS)
        {
            List<IConfigurationKeyword> pList = new List<IConfigurationKeyword>();
            IWorkspaceConfiguration pWConfig = pWS as IWorkspaceConfiguration;
            IEnumConfigurationKeyword pEnumConfig = pWConfig.ConfigurationKeywords;
            IConfigurationKeyword pConfig = pEnumConfig.Next();
            while (pConfig != null)
            {
                pList.Add(pConfig);
                pConfig = pEnumConfig.Next();
            }
            return pList;
        }
        public static List<IConfigurationParameter> GetConfigurationParameterList(IConfigurationKeyword pConfig)
        {
            List<IConfigurationParameter> pList = new List<IConfigurationParameter>();
            IEnumConfigurationParameter pEnumCP = pConfig.ConfigurationParameters;
            IConfigurationParameter pCP = pEnumCP.Next();
            while (pCP != null)
            {
                pList.Add(pCP);
                pCP = pEnumCP.Next();
            }
            return pList;
        }
    }
}
