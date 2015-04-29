using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;

namespace GIS.BasicGraphic
{
    /// <summary>
    /// 图层列表
    /// </summary>
    [Guid("62a7a40f-29b4-441c-bb72-4ca0392eecc2")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.BasicGraphic.LayersList")]
    public sealed class LayersList : BaseCommand, IToolControl
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
        private LayersListControl m_layerListCtrl = null;
        public LayersList()
        {
            //公共属性定义
            base.m_category = "基础图元绘制"; 
            base.m_caption = "图层列表";  
            base.m_message = "显示当前所有图层";  
            base.m_toolTip = "图层列表";  
            base.m_name = "LayersList";  

            try
            {
                //string bitmapResourceName = GetType().Name + ".bmp";
                //base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
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

            //确保用户控件初始化
            if (null == m_layerListCtrl)
            {
                m_layerListCtrl = new LayersListControl();
                m_layerListCtrl.CreateControl();
            }
            //设置控件的Map属性
            m_layerListCtrl.Map = m_hookHelper.FocusMap;
        }

        public override void OnClick()
        {
           
        }

        #endregion

        public bool OnDrop(esriCmdBarType barType)
        {
            return true;
        }

        public void OnFocus(ICompletionNotify complete)
        {
            
        }

        public int hWnd
        {
            get
            {
                //传递用户空间句柄
                if (null == m_layerListCtrl)
                {
                    m_layerListCtrl = new LayersListControl();
                    m_layerListCtrl.CreateControl();
                }

                return m_layerListCtrl.Handle.ToInt32();

            }
        }
    }
}
