using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;
using GIS.Properties;

namespace GIS.GraphicEdit
{
    /// <summary>
    /// 全部选择命令
    /// </summary>
    [Guid("b572ea09-487c-4e57-9f0d-47984698e1d7")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.GraphicEdit.FeatureSelectAll")]
    public sealed class FeatureSelectAll : BaseCommand
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
        private ICommand m_command = null;
        private IFeatureLayer m_featureLayer = null;

        public FeatureSelectAll()
        {
            //公共属性定义
            base.m_category = "编辑"; 
            base.m_caption = "图元全选";  
            base.m_message = "选择可选图层中的所有图元";  
            base.m_toolTip = "图元全选";  
            base.m_name = "FeatureSelectAll";   

            try
            {
                base.m_bitmap = Resources.SelectionSelectAll16;
                m_command = new ESRI.ArcGIS.Controls.ControlsSelectAllCommandClass();
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
        /// <params name="hook">程序实例</params>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                m_command.OnCreate(hook);
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
        
        public override void OnClick()
        {
            //实现FeatureSelectAll.OnClick
            m_command.OnClick();
        }

        #endregion
    }
}
