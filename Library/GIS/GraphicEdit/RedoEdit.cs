using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using GIS.Properties;
using GIS.Common;

namespace GIS.GraphicEdit
{
    /// <summary>
    /// 重做命令
    /// </summary>
    [Guid("c4c994f8-4ecb-4fc3-bc49-59200a2509b8")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.GraphicEdit.RedoEdit")]
    public sealed class RedoEdit : BaseCommand
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
        public RedoEdit()
        {
            //公共属性定义
            base.m_category = "编辑";
            base.m_caption = "重做";  
            base.m_message = "重做";  
            base.m_toolTip = "重做";  
            base.m_name = "RedoEdit";   

            try
            {
                base.m_bitmap = Resources.EditRedo_B_16;
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
        
        public override void OnClick()
        {
            if (DataEditCommon.g_engineEditor==null) return;

            if (DataEditCommon.g_engineEditor.EditState != esriEngineEditState.esriEngineStateEditing)
                return;
             bool hasredo = false;
            Common.DataEditCommon.g_CurWorkspaceEdit.HasRedos(ref hasredo);
            if (hasredo)
            {
                DataEditCommon.g_CurWorkspaceEdit.RedoEditOperation();
                DataEditCommon.g_pMyMapCtrl.Refresh();
            }
        }
        
        public override bool Enabled
        {
            get
            {
                if (DataEditCommon.g_engineEditor == null) return false;

                if (DataEditCommon.g_engineEditor.EditState != esriEngineEditState.esriEngineStateEditing)
                    return false;
                bool hasredo = false;
                Common.DataEditCommon.g_CurWorkspaceEdit.HasRedos(ref hasredo);
                if (hasredo)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion
    }
}
