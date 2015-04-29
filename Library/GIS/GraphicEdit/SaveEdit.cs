using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using GIS.Properties;

namespace GIS
{
    /// <summary>
    /// 保存编辑
    /// </summary>
    [Guid("f15a49a9-0694-47fa-bfd4-dca47585d576")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.GraphicEdit.SaveEdit")]
    public sealed class SaveEdit : BaseCommand
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

        public SaveEdit()
        {
            //公共属性定义
            base.m_category = "编辑"; 
            base.m_caption = "保存"; 
            base.m_message = "保存编辑";  
            base.m_toolTip = "保存";  
            base.m_name = "保存";  
            try
            {
                base.m_bitmap = Resources.GenericSave_B_16;
                //base.m_cursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream(GetType(), "Resources.SaveEdit.cur"));
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
            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                if (m_hookHelper.ActiveView == null)
                {
                    m_hookHelper = null;
                }
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
            try
            {
                m_hookHelper.FocusMap.ClearSelection();
                if (Common.DataEditCommon.g_engineEditor != null)
                    Common.DataEditCommon.g_engineEditor.StopEditing(true);
                base.m_checked = false;
                m_hookHelper.ActiveView.Refresh();
            }
            catch 
            { }
        }
        public override bool Enabled
        {
            get
            {
                if (Common.DataEditCommon.g_engineEditor != null){
                    if (Common.DataEditCommon.g_engineEditor.HasEdits())
                        return true;
                }
                return false;
            }
        }
        //public override void OnMouseDown(int Button, int Shift, int X, int Y)
        //{
           
        //}

        //public override void OnMouseMove(int Button, int Shift, int X, int Y)
        //{
            
        //}

        //public override void OnMouseUp(int Button, int Shift, int X, int Y)
        //{
            
        //}
        #endregion
    }
}
