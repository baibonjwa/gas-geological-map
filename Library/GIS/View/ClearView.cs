using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using GIS.Common;
using GIS.Properties;

namespace GIS.View
{
    /// <summary>
    /// 还原局部视图
    /// </summary>
    [Guid("0d0c2dfc-a99a-4cc9-862d-5de38b4caf7d")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.View.ClearView")]
    public sealed class ClearView : BaseCommand
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);
            ControlsCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);
            ControlsCommands.Unregister(regKey);
        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper = null;
        public ClearView()
        {
            //公共属性定义
            base.m_category = "视图"; 
            base.m_caption = "还原视图";  
            base.m_message = "清除局部视图";
            base.m_toolTip = "还原视图";
            base.m_name = "ClearView";   

            try
            {
                base.m_bitmap = Resources.ClearView;  
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// 创建工具
        /// </summary>
        /// <param name="hook">程序实例</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                if (m_hookHelper.ActiveView == null)
                    m_hookHelper = null;
            }
            catch
            {
                m_hookHelper = null;
            }

            if (m_hookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        public override void OnClick()
        {
            DataEditCommon.g_pAxMapControl.CurrentTool = null;

            IMap pMap = m_hookHelper.FocusMap;
            UID puid = new UID();
            puid.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";//IFeatureLayer
            IEnumLayer enumLayer = pMap.get_Layers(puid, true);
            enumLayer.Reset();
            ILayer player;
            player = enumLayer.Next();
            IFeatureLayer featureLayer = null;            
            while (player != null)
            {
                featureLayer = player as IFeatureLayer;             
                IFeatureLayerDefinition featureLayerDef = featureLayer as IFeatureLayerDefinition;
                string sWhereClause = "";//定义筛选条件
                featureLayerDef.DefinitionExpression = sWhereClause;

                player = enumLayer.Next();
            }

            m_hookHelper.ActiveView.Extent = m_hookHelper.ActiveView.FullExtent;
            m_hookHelper.ActiveView.Refresh();
        }

        #endregion
    }
}
