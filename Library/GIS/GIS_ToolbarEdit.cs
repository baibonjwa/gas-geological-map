using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using GIS.Common;

namespace GIS
{
    /// <summary>
    /// 编辑工具条
    /// </summary>
    [Guid("4b89dc49-1c82-474c-8ff5-9fec3be39ac8")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.GIS_ToolbarEdit")]
    public sealed class GIS_ToolbarEdit : BaseToolbar
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

        public GIS_ToolbarEdit(AxMapControl axMapControl, IMapControl3 mapControl, IToolbarControl toolbarControl, IWorkspace workSpace)
        {           
            //全局变量赋值
            DataEditCommon.g_tbCtlEdit = toolbarControl;
            DataEditCommon.g_pMyMapCtrl = mapControl;
            DataEditCommon.g_pCurrentWorkSpace = workSpace;
            DataEditCommon.g_pAxMapControl = axMapControl;

            BeginGroup();
            //AddItem("GIS.GraphicModify.RotateTool");
            AddItem("GIS.GraphicEdit.UndoEdit");
            AddItem("GIS.GraphicEdit.RedoEdit");
            AddItem("GIS.GraphicEdit.AttributeQueryEdit");
            AddItem("GIS.GraphicEdit.MeasureDistance");
            AddItem("GIS.GraphicEdit.MeasureArea");
            //AddItem("GIS.GraphicEdit.MenuFeatureSelect");
            AddItem("GIS.GraphicEdit.FeatureSelect");
            AddItem("GIS.GraphicEdit.FeatureClearSelect");
            //AddItem("GIS.GraphicEdit.SnapSetting");
            AddItem("GIS.GraphicEdit.SaveEdit");            
        }

        public override string Caption
        {
            get
            {                
                return "编辑工具条";
            }
        }
        public override string Name
        {
            get
            {                
                return "GIS_ToolbarEdit";
            }
        }

        
    }
}