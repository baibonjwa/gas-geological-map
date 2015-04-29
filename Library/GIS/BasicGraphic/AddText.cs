using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using GIS.Properties;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using GIS.Common;

namespace GIS
{
    /// <summary>
    /// 添加文本
    /// </summary>
    [Guid("e185ec83-f247-4fb3-bb89-5f2a41b735e9")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.BasicGraphic.AddText")]
    public sealed class AddText : BaseTool
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
        public AddText()
        {
            //公共属性定义
            base.m_category = "基础图元绘制"; 
            base.m_caption = "添加文字";  
            base.m_message = "输入文字，添加到图层，参考比例尺为1:10000"; 
            base.m_toolTip = "添加文字";  
            base.m_name = "AddText";   
            try
            {
                base.m_bitmap = Resources.ElementText16;
                base.m_cursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream(GetType(), "Resources.AddText.cur"));
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
        public override bool Enabled
        {
            get
            {
                ILayer pLayer = GIS.Common.DataEditCommon.g_pLayer;
                IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
                if (pFeatureLayer == null) return false;
                IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                if (pFeatureClass.FeatureType != esriFeatureType.esriFTAnnotation)
                {
                    return false;
                }
                return true;
            }
        }

        public override void OnClick()
        {
            DataEditCommon.InitEditEnvironment();
            DataEditCommon.CheckEditState();
            ILayer pLayer =GIS.Common.DataEditCommon.g_pLayer;
            IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            if (pFeatureClass.FeatureType != esriFeatureType.esriFTAnnotation)
            {
                MessageBox.Show(@"请选择文字注记(标注)图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataEditCommon.g_pMyMapCtrl.CurrentTool = null;
                return;
            }
        }
        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            IPoint m_Point = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            m_Point = GIS.GraphicEdit.SnapSetting.getSnapPoint(m_Point);
            GIS.BasicGraphic.InputText p = new GIS.BasicGraphic.InputText(m_Point);
            p.ShowDialog();          
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            IPoint m_Point = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            m_Point = GIS.GraphicEdit.SnapSetting.getSnapPoint(m_Point);
            DataEditCommon.g_pAxMapControl.Focus();
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            
        }

     
        #endregion
    }
}
