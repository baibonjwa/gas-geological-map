using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using GIS.Common;
using ESRI.ArcGIS.SystemUI;
using GIS.Properties;

namespace GIS.View
{
    /// <summary>
    /// Command that works in ArcGlobe or GlobeControl
    /// </summary>
    [Guid("9e6386d2-35d1-49d5-ae9e-cd20427e3cd1")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.View.ZoomToTool")]
    public sealed class ZoomToTool : BaseCommand
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
            GMxCommands.Register(regKey);
            ControlsCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            GMxCommands.Unregister(regKey);
            ControlsCommands.Unregister(regKey);
        }

        #endregion
        #endregion

        public ZoomToTool()
        {
            //
            // 公共属性定义
            //
            base.m_category = "";
            base.m_caption = "转到xy";
            base.m_message = "转到xy位置";
            base.m_toolTip = "转到xy";
            base.m_name = "ZoomToTool";
            try
            {
                base.m_bitmap = Resources.zoomto16;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            if (DataEditCommon.g_pMyMapCtrl.CurrentTool != null)
                DataEditCommon.g_pMyMapCtrl.CurrentTool = null;
            FrmGoToXY frm = new FrmGoToXY();
            frm.Show(DataEditCommon.g_pAxMapControl.FindForm());
        }

        #endregion
    }
}
