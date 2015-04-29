using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
namespace GIS
{
    public class FileMenu
    {
      
        /// <summary>
        /// 弹出新建文件对话框
        /// 参数path为新建对话框的初始路径
        /// 日期2013-12-13
        /// lntu_GISer1
        /// </summary>
        private string  newfileDialog(string path)
        {
            string fileName="";
            SaveFileDialog nfd = new SaveFileDialog();                //new一个方法
            nfd.Title = "新建地图文档";
            nfd.InitialDirectory = path; //定义打开的默认文件夹位置
            nfd.Filter = "地图文档(*.mxd)|*.mxd";
            if (nfd.ShowDialog() == DialogResult.OK)          //显示打开文件的窗口
            {
                fileName = nfd.FileName;               //获得选择的文件路径
               
            }
           
            return fileName;
        }
        /// <summary>
        /// 弹出打开文件对话框
        /// 参数path为打开对话框的初始路径
        /// 日期2013-12-13
        /// lntu_GISer1
        /// </summary>
        private string openfileDialog(string path)
        {
            string fileName = "";
            OpenFileDialog ofd = new OpenFileDialog();                //new一个方法
            ofd.Title = "打开地图文档";
            ofd.InitialDirectory = path; //定义打开的默认文件夹位置
            ofd.Filter = "地图文档(*.mxd)|*.mxd";
            if (ofd.ShowDialog() == DialogResult.OK)          //显示打开文件的窗口
            {
                fileName = ofd.FileName;               //获得选择的文件路径
            
            }
            return fileName;
          
        }
        /// <summary>
        /// 弹出保存文件对话框
        /// 参数path为保存对话框的初始路径
        /// 日期2013-12-13
        /// lntu_GISer1
        /// </summary>
        private string SavefileDialog(string path)
        {
            string fileName = "";
            SaveFileDialog sfd = new SaveFileDialog();                //new一个方法
            sfd.Title = "另存地图文档";
            sfd.InitialDirectory = path; //定义打开的默认文件夹位置
            sfd.Filter = "地图文档(*.mxd)|*.mxd";
            if (sfd.ShowDialog() == DialogResult.OK)         //显示打开文件的窗口
            {
                fileName = sfd.FileName;               //获得选择的文件路径
            }
            return fileName;
        }
        /// <summary>
        /// 打开mxd文档
        /// path为打开mxd文件的路径，mapControl加载该地图文档的地图控件名称
        /// 日期2013-12-13
        /// lntu_GISer1
        /// </summary>
        public  string  OpenMxdFile(string path,AxMapControl mapControl)
        { 
            string filename = openfileDialog(path);
            try
            {
                if (filename != "")
                {
                        mapControl.LoadMxFile(filename);
                        mapControl.Extent = mapControl.FullExtent;
                   
                }
                  return filename;

            }
            catch(Exception e)
            {
              
                MessageBox.Show(e.Message);
                return "";
            }

        }
        /// <summary>
        /// 新建mxd文档
        /// 参数path为创建mxd文件的路径+名称
        /// 参数mapControl为调用该dll项目的地图控件
        /// 日期2013-12-13
        /// lntu_GISer1
        /// </summary>
        public  string NewMxdFile(string path,AxMapControl mapControl)
        {
            try
            {
                string filename =newfileDialog(path);
                if (filename != "")
                {
                        MapDocument pMapDocument = new MapDocumentClass();
                        pMapDocument.New(filename);
                        pMapDocument.Open(filename, "");
                  
                }
                return filename;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return "";
            }

        }
        /// <summary>
        /// 保存mxd文档
        /// mapControl为地图控件的名称
        /// 日期2013-12-13
        /// lntu_GISer1
        /// </summary>
        public  void SaveMxdFile(AxMapControl mapControl)
        {

             IMxdContents pMxdC;
             pMxdC = mapControl.ActiveView.FocusMap as IMxdContents;
             IMapDocument pMapDocument = new MapDocumentClass();
             if (mapControl.DocumentFilename != null)
             {
                 pMapDocument.Open(mapControl.DocumentFilename, "");
                 pMapDocument.ReplaceContents(pMxdC);
                 if (pMapDocument.get_IsReadOnly(pMapDocument.DocumentFilename))
                 {

                     MessageBox.Show("当前地图文档为只读文档!");
                     return;
                 }
                 try
                 {
                     pMapDocument.Save(true, true);
                     MessageBox.Show("地图文档保存成功!");
                 }
                 catch (Exception e)
                 {
                     MessageBox.Show(e.Message);
                 }
             }
           
             //用当前的文件路径设置保存文件
          

        }
        /// <summary>
        ///导入dxf，shp等文件
        /// 日期2013-12-13
        /// lntu_GISer1
        /// </summary>
        public  void ImportFile(string path,AxMapControl mapControl)
        {
            string fileName = "";
            IWorkspaceFactory pWorkspaceFactory;
            IFeatureWorkspace pFeatureWorkspace;
            IFeatureLayer pFeatureLayer;
            IFeatureDataset pFeatureDataset;
            OpenFileDialog ofd = new OpenFileDialog();                //new一个方法
            ofd.Title = "导入地图数据";
            ofd.InitialDirectory = path; //定义打开的默认文件夹位置
            ofd.Filter = "shp文件(*.shp)|*.shp|CAD文件(*.dwg)|*.dwg";
            if (ofd.ShowDialog() == DialogResult.OK)          //显示打开文件的窗口
            {
                fileName = ofd.FileName;               //获得选择的文件路径
                if (fileName.Substring(fileName.Length - 4, 4).ToLower() == ".shp")
                {
                    try
                    {
                        pWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
                        pFeatureWorkspace = (IFeatureWorkspace)pWorkspaceFactory.OpenFromFile(path, 0);
                        pFeatureLayer = new FeatureLayerClass();
                        pFeatureLayer.FeatureClass = pFeatureWorkspace.OpenFeatureClass((System.IO.Path.GetFileName(ofd.FileName)).Replace(".shp", ""));
                        pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName;
                        mapControl.Map.AddLayer(pFeatureLayer);
                        mapControl.ActiveView.Refresh();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
                else
                {
                    if (fileName.Substring(fileName.Length -4, 4).ToLower() == ".dwg")
                    {
                        try
                        {
                         //打开CAD数据集
                        pWorkspaceFactory = new CadWorkspaceFactoryClass();
                        pFeatureWorkspace = (IFeatureWorkspace)pWorkspaceFactory.OpenFromFile(path, 0);
                        //打开一个要素集
                        pFeatureDataset = pFeatureWorkspace.OpenFeatureDataset((System.IO.Path.GetFileName(ofd.FileName)).Replace(".dwg", ""));
                        //IFeaturClassContainer可以管理IFeatureDataset中的每个要素类   
                        IFeatureClassContainer pFeatClassContainer = (IFeatureClassContainer)pFeatureDataset;
                        //对CAD文件中的要素进行遍历处理 
                        for (int i = 0; i < pFeatClassContainer.ClassCount - 1; i++)
                        {
                           
                            IFeatureClass pFeatClass = pFeatClassContainer.get_Class(i);
                            if (pFeatClass.FeatureType == esriFeatureType.esriFTCoverageAnnotation)
                            {
                                //如果是注记，则添加注记层
                                pFeatureLayer = new CadAnnotationLayerClass();
                            }
                            else//如果是点、线、面，则添加要素层
                            {
                                pFeatureLayer = new FeatureLayerClass();
                                pFeatureLayer.Name = pFeatClass.AliasName;
                                pFeatureLayer.FeatureClass = pFeatClass;
                                mapControl.Map.AddLayer(pFeatureLayer);
                                mapControl.ActiveView.Refresh();
                            }
                        }
                        }
                            catch(Exception e)
                        {
                                MessageBox.Show(e.Message);
                            }
                    
            }
           }
            }
        }
      //
    }
}
