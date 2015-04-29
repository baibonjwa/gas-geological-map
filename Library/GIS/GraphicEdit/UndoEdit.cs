using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using GIS.Properties;

namespace GIS.GraphicEdit
{
    /// <summary>
    /// 重做命令
    /// </summary>
    [Guid("31d5a86b-76e9-4dba-bb62-9650b1ed0280")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.GraphicEdit.UndoEdit")]
    public sealed class UndoEdit : BaseCommand
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
        public UndoEdit()
        {
            //公共属性定义
            base.m_category = "编辑"; 
            base.m_caption = "撤销";  
            base.m_message = "撤销";  
            base.m_toolTip = "撤销";  
            base.m_name = "UndoEdit";   

            try
            {
                base.m_bitmap = Resources.EditUndo_B_16;
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
            if (Common.DataEditCommon.g_engineEditor == null) return;

            if (Common.DataEditCommon.g_engineEditor.EditState != esriEngineEditState.esriEngineStateEditing)
                return;
            bool hasundo = false;
            Common.DataEditCommon.g_CurWorkspaceEdit.HasUndos(ref hasundo);
            if (hasundo)
            {
                Common.DataEditCommon.g_CurWorkspaceEdit.UndoEditOperation();
                Common.DataEditCommon.g_pMyMapCtrl.Refresh();
            }
        }
        public override bool Enabled
        {
            get
            {
                if (Common.DataEditCommon.g_engineEditor == null) return false;

                if (Common.DataEditCommon.g_engineEditor.EditState != esriEngineEditState.esriEngineStateEditing)
                    return false;
                bool hasundo = false;
                Common.DataEditCommon.g_CurWorkspaceEdit.HasUndos(ref hasundo);
                if (hasundo)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion
    }
}
