using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;
using GIS.Common;
using ESRI.ArcGIS.SystemUI;

namespace GIS.View
{
    /// <summary>
    /// 局部视图和清除局部视图工具条
    /// </summary>
    [Guid("4f0a29e8-4cbc-4dae-bec7-cc44d279d270")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.View.ToolbarLocalView")]
    public sealed class ToolbarLocalView : BaseToolbar
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

        public ToolbarLocalView()
        {
            AddItem("GIS.View.LocalView");
            AddItem("GIS.View.ClearView");
        }

        public override string Caption
        {
            get
            {                
                return "局部视图";
            }
        }
        public override string Name
        {
            get
            {                
                return "ToolbarLocalView";
            }
        }
    }
}