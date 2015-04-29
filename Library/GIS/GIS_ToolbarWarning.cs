using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace GIS
{
    /// <summary>
    /// 预警工具条
    /// </summary>
    [Guid("e1afd862-c354-48ae-acb6-b37b0cbd0f0d")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.GIS_ToolbarWarning")]
    public sealed class GIS_ToolbarWarning : BaseToolbar
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
            ControlsToolbars.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsToolbars.Unregister(regKey);
        }

        #endregion
        #endregion

        public GIS_ToolbarWarning()
        {
            BeginGroup();
            AddItem("GIS.Warning.FlashWarningPoints");
        }

        public override string Caption
        {
            get
            {
                //TODO: Replace bar caption
                return "预警工具条";
            }
        }
        public override string Name
        {
            get
            {
                //TODO: Replace bar ID
                return "GIS_ToolbarWarning";
            }
        }
    }
}