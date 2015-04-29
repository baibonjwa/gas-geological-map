using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;
using GIS;
using GIS.Common;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace GIS
{
    /// <summary>
    /// 修改工具条
    /// </summary>
    [Guid("bc56c8ef-06d5-4dd3-8b03-3cfc9065e523")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.GIS_ToolbarModify")]
    public sealed class GIS_ToolbarModify : BaseToolbar
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

        public GIS_ToolbarModify()
        {
            BeginGroup(); //分隔条
            AddItem("GIS.GraphicModify.RotateTool");
            AddItem("GIS.GraphicModify.FeatureMoveEdit");
            AddItem("GIS.GraphicModify.MirrorFeature");
            AddItem("GIS.GraphicModify.EditCopyCommand");
            AddItem("GIS.GraphicModify.EditCutCommand");
            AddItem("GIS.GraphicModify.EditPasteCommand");
            AddItem("GIS.GraphicModify.DeleteFeature");
            AddItem("GIS.GraphicModify.ExtendTool");
            AddItem("GIS.GraphicModify.TrimLineTool");
        }

        public override string Caption
        {
            get
            {                
                return "修改工具条";
            }
        }
        public override string Name
        {
            get
            {                
                return "GIS_ToolbarModify";
            }
        }
    }
}