using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.SystemUI;
using GIS.Common;

namespace GIS
{
    /// <summary>
    /// 视图工具条
    /// </summary>
    [Guid("7a9d1775-3ad3-4fdc-9ed2-b842e6e395c7")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.GIS_ToolbarView")]
    public sealed class GIS_ToolbarView : BaseToolbar
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

        public GIS_ToolbarView()
        {
            BeginGroup(); //分隔条            
            AddItem("GIS.View.FixedZoomInCommand");
            AddItem("GIS.View.FixedZoomOutCommand");
            AddItem("GIS.View.ZoomInTool");
            AddItem("GIS.View.ZoomOutTool");
            AddItem("GIS.View.ExtentBackCommand");
            AddItem("GIS.View.ExtentForwardCommand");
            AddItem("GIS.View.PanTool");
            AddItem("GIS.View.ZoomToTool");
            AddItem("GIS.View.RefreshViewCommand");
            AddItem("GIS.View.FullExtentCommand");
            AddItem("GIS.View.MapRotateTool");
            AddItem("GIS.View.ToolbarLocalView");            
        }

        public override string Caption
        {
            get
            {                
                return "视图";
            }
        }
        public override string Name
        {
            get
            {                
                return "GIS_ToolbarView";
            }
        }
    }
}