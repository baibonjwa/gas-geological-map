using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using GIS.Properties;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Carto;
using GIS.Common;

namespace GIS.GraphicModify
{
    /// <summary>
    /// 移动
    /// </summary>
    [Guid("054fa11c-2247-49bc-8d44-e89ef32007f8")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.GraphicModify.FeatureMoveEdit")]
    public sealed class FeatureMoveEdit : BaseTool
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

        public FeatureMoveEdit()
        {
            //公共属性定义
            base.m_category = "修改"; 
            base.m_caption = "图元移动";  
            base.m_message = "选取图元移动双击修改节点";  
            base.m_toolTip = "图元移动"; 
            base.m_name = "FeatureMoveEdit";  
            try
            {
                base.m_bitmap = Resources.EditingEditTool16;
                //base.m_cursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream(GetType(), "Resources.FeatureMoveEdit.cur"));

                m_command = new ESRI.ArcGIS.Controls.ControlsEditingEditTool();
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
        public override bool Checked
        {
            get
            {
                //if (DataEditCommon.g_pMyMapCtrl.CurrentTool == (ITool)m_command)
                //    return true;
                //else
                //    return false;
                return base.Checked;
            }
        }
        public override bool Enabled
        {
            get
            {
                return base.Enabled;
            }
        }
        /// <summary>
        /// 点击事件
        /// </summary>
        public override void OnClick()
        {
            DataEditCommon.InitEditEnvironment();
            DataEditCommon.CheckEditState();
            m_featureLayer = DataEditCommon.g_pLayer as IFeatureLayer;
            if (m_featureLayer == null)
            {
                MessageBox.Show(@"请选择图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataEditCommon.g_pMyMapCtrl.CurrentTool = null;
                return;
            }
            DataEditCommon.g_engineEditLayers.SetTargetLayer(m_featureLayer, 0);

            DataEditCommon.g_pMyMapCtrl.CurrentTool = (ITool)m_command;
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
                      
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            DataEditCommon.g_pAxMapControl.Focus();
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            
        }

        #endregion
    }
}
