using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using GIS.Properties;
using ESRI.ArcGIS.SystemUI;
using GIS.Common;
using ESRI.ArcGIS.Carto;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;


namespace GIS.GraphicModify
{
    /// <summary>
    /// 复制
    /// </summary>
    [Guid("257bd1f3-e76b-42c4-ab15-c5fce2da08bd")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.GraphicModify.EditCopyCommand")]
    public sealed class EditCopyCommand : BaseCommand
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
        public EditCopyCommand()
        {
            //公共属性定义
            base.m_category = "修改"; 
            base.m_caption = "复制";  
            base.m_message = "复制选中要素";  
            base.m_toolTip = "复制";  
            base.m_name = "EditCopyCommand";   

            try
            {
                base.m_bitmap = Resources.EditCopy16;
                m_command = new ESRI.ArcGIS.Controls.ControlsEditingCopyCommandClass();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }
        public override bool Enabled
        {
            get
            {
                IFeatureLayer m_featureLayer = DataEditCommon.g_pLayer as IFeatureLayer;
                if (m_featureLayer == null) return false;
                IFeatureSelection m_featureSelection = m_featureLayer as IFeatureSelection;
                ESRI.ArcGIS.Geodatabase.ISelectionSet m_selectionSet = m_featureSelection.SelectionSet;//QI到ISelectionSet
                if (m_selectionSet.Count < 1)
                {
                    return false;
                }
                return true;
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

        /// <summary>
        /// 点击事件
        /// </summary>
        public override void OnClick()
        {
            if (DataEditCommon.g_pMyMapCtrl.CurrentTool != null)
                DataEditCommon.g_pMyMapCtrl.CurrentTool = null;
            IFeatureLayer m_featureLayer = DataEditCommon.g_pLayer as IFeatureLayer;
            IFeatureSelection m_featureSelection = m_featureLayer as IFeatureSelection;
            ESRI.ArcGIS.Geodatabase.ISelectionSet m_selectionSet = m_featureSelection.SelectionSet;//QI到ISelectionSet
            if (m_selectionSet.Count < 1)
            {
                DataEditCommon.g_pMyMapCtrl.CurrentTool = null;
                MessageBox.Show("当前图层没有可复制的元素！");
                return;
            }
            GIS.Common.DataEditCommon.copypaste = 1;
            DataEditCommon.copypasteLayer = m_featureLayer;
            m_command.OnClick();
        }
        #endregion
    }
}
