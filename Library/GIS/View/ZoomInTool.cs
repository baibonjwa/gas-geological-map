using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using GIS.Properties;
using ESRI.ArcGIS.SystemUI;
using System.Reflection;
using System.Resources;
using GIS.Common;

namespace GIS.View
{
    /// <summary>
    ///放大
    /// </summary>
    [Guid("6a39506c-7c21-484c-9487-5a95cf1f3db6")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.View.ZoomInTool")]
    public sealed class ZoomInTool : BaseTool
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

        public ZoomInTool()
        {
            //
            // 公共属性定义
            //
            base.m_category = "";
            base.m_caption = "放大";
            base.m_message = " 通过在地图上点击一次或拖拽框的方式放大";
            base.m_toolTip = "";
            base.m_name = "ZoomInTool";
            try
            {
                base.m_bitmap = Resources.ZoomInTool16;
                //base.m_cursor = new Cursor(GetType().Assembly.GetManifestResourceStream(GetType(), "Resources.ZoomInTool.cur"));

                m_command = new ESRI.ArcGIS.Controls.ControlsMapZoomInTool();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }
       

        #region Overridden Class Methods

        /// <summary>
        ///创建工具
        /// </summary>
        /// <param name="hook">程序实例</param>
        public override void OnCreate(object hook)
        {
            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                m_command.OnCreate(hook);
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

        /// <summary>
        /// 点击事件
        /// </summary>
        public override void OnClick()
        {
            if (DataEditCommon.g_pMyMapCtrl.CurrentTool != null)
                DataEditCommon.g_pMyMapCtrl.CurrentTool = null;

            DataEditCommon.g_pMyMapCtrl.CurrentTool = (ITool)m_command;//设为当前操作命令  
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {

        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {

        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {

        }
        #endregion
    }
}
