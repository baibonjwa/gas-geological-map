using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace GIS
{
    /// <summary>
    /// 基本图元工具条
    /// </summary>
    [Guid("6ed1bf99-4dd9-4235-9a65-ca5dfa81dadc")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.GIS_ToolbarBasic")]
    public sealed class GIS_ToolbarBasic : BaseToolbar
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

        public GIS_ToolbarBasic()
        {
            BeginGroup(); //分隔条
            AddItem("GIS.BasicGraphic.LayersList");
            AddItem("GIS.BasicGraphic.AddPoint");
            AddItem("GIS.BasicGraphic.AddStraightFeatureLine");
            AddItem("GIS.BasicGraphic.AddFeatureLine");
            //AddItem(new GIS.BasicGraphic.AddFeatureLine(), -1, this.ItemCount, false, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleTextOnly);
            //AddItem("GIS.BasicGraphic.AddBezerLine");
            AddItem("GIS.BasicGraphic.AddBezierCurve");
            AddItem("GIS.BasicGraphic.AddRectangle");
            AddItem("GIS.BasicGraphic.AddText");
            AddItem("GIS.BasicGraphic.AddCircle");
            //AddItem("GIS.BasicGraphic.AddCircularArc");
            AddItem("GIS.BasicGraphic.AddArc");
            AddItem("GIS.BasicGraphic.AddEllipse");
            AddItem("GIS.BasicGraphic.AddPolygon");
        }

        public override string Caption
        {
            get
            {                
                return "基本图元工具条";
            }
        }
        public override string Name
        {
            get
            {                
                return "GIS_ToolbarBasic";
            }
        }
    }
}