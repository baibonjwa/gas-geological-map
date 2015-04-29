using System;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
//ESRI

// ArcGIS Engine引用
//using ESRI.ArcGIS.ToolbarControl;
//using ESRI.ArcGIS.TOCControl;
//using ESRI.ArcGIS.MapControl;
//using ESRI.ArcGIS.PageLayoutControl;
//using ESRI.ArcGIS.GlobeCore;

namespace GIS.Common
{
    /// <summary>
    /// SDE
    /// </summary>
    public class SDEOperation
    {
        public static IWorkspace connectSde()
        {
            try
            {
                //定义工作空间，工作空间的数据源来自SDE，IWorkspaceFactory是Geodatabase的入口
                //Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory");
                //IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);

                ////通过IPropertySet设置通过SDE连接数据库的各种参数
                //IPropertySet propertySet = new PropertySetClass();
                //propertySet.SetProperty("SERVER", "10.64.192.51");
                //propertySet.SetProperty("SERVICE", "esri_sde");
                //propertySet.SetProperty("INSTANCE", "sde:sqlserver:10.64.192.51");
                ////propertySet.SetProperty("Instance", "port:5151");
                //propertySet.SetProperty("DATABASE", "GasEarlyWarningGIS");
                //propertySet.SetProperty("USER", "sde");
                //propertySet.SetProperty("PASSWORD", "sde");   
                //propertySet.SetProperty("VERSION", "sde.DEFAULT");

                ////通过以上设置的参数将数据库的数据通过SDE读入工作空间
                //IWorkspace workspace = workspaceFactory.Open(propertySet, 0);
                IWorkspace workspace = null;

                ILayer pLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap,"点");
               
                IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
              
                IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                IDataset pDataset = (IDataset)pFeatureClass;
                workspace = pDataset.Workspace;
                //IWorkspaceFactory workspaceFactory = new SdeWorkspaceFactoryClass();
                //workspace = workspaceFactory.Open(ConnectSDE(true), 0);
                return workspace;


                
                //return GetSDEWorkspace();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接失败！" + ex.Message);
            }

            return null;
        }

        //--------------------------------------------------------------------------------

        public static IWorkspace GetSDEWorkspace()
        {
            try
            {
                //声明临时路径
                string path = @"d:\temp";
                // 声明临时.sde文件名称
                string sdeName = @"localhost.sde";
                string sdePath = path + "\\" + sdeName;
                // 如果已经存在了，则删除了重新创建
                if (File.Exists(sdePath))
                {
                    File.Delete(sdePath);
                }
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory");
                IWorkspaceFactory workspaceFactory = new SdeWorkspaceFactoryClass();
                // 创建.sde文件
                IWorkspaceName workspaceName = workspaceFactory.Create(path, sdeName, ConnectSDE(true), 0);
                // 使用.sde文件，通过.sde文件获取IWorkspace，之后就可以对数据库中的数据进行操作了
                IWorkspace pWorkspace = workspaceFactory.OpenFromFile(sdePath, 0);

                return pWorkspace;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 设置SDE连接属性
        /// </summary>
        /// <param name="ChkSdeLinkModle"></param>
        /// <returns>IPropertySet</returns>
        public static IPropertySet ConnectSDE(bool ChkSdeLinkModle)
        {
            //定义一个属性
            IPropertySet Propset = new PropertySetClass();
            if (ChkSdeLinkModle == true) // 采用SDE连接
            {
                string[] str=File.ReadAllLines(Application.StartupPath+@"\ConfigDatabaseGIS.ini");
                for (int i = 0; i < str.Length; i++)
                {
                    string key = str[i].Substring(0, str[i].IndexOf('='));
                    string value = str[i].Substring(str[i].IndexOf('=') + 1);
                    Propset.SetProperty(key,value);
                }
                ////设置数据库服务器名
                //Propset.SetProperty("SERVER", "10.64.192.51");
                ////设置SDE的端口，这是安装时指定的，默认安装时"port:5151"
                //Propset.SetProperty("INSTANCE", "sde:sqlserver:10.64.192.51");//
                ////DBCLIENT数据库平台
                //Propset.SetProperty("DBCLIENT", "sqlserver");
                ////DB_CONNECTION_PROPERTIES
                //Propset.SetProperty("DB_CONNECTION_PROPERTIES", "10.64.192.51");
                ////设置数据库的名字,只有SQL Server  Informix 数据库才需要设置
                //Propset.SetProperty("DATABASE", "GasEarlyWarningGIS");
                //////IS_GEODATABASE
                ////Propset.SetProperty("IS_GEODATABASE", "true");
                //////AUTHENTICATION_MODE
                ////Propset.SetProperty("AUTHENTICATION_MODE", "DBMS");
                ////SDE的用户名
                //Propset.SetProperty("USER", "sde");
                ////密码
                //Propset.SetProperty("PASSWORD", "sde");
                ////SDE的版本,在这为默认版本
                //Propset.SetProperty("VERSION", "SDE.DEFAULT");
            }
            else // 直接连接
            {
                //设置数据库服务器名,如果是本机可以用"sde:sqlserver:.",直接连接会弹出选择数据库对话框，要求填入用户名密码
                Propset.SetProperty("INSTANCE", "sde:sqlserver:xxsde");
            }
            return Propset;
        }
 
    }
}
