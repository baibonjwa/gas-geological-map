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
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;

namespace GIS.SpecialGraphic
{
    /// <summary>
    /// 通用拾取点类
    /// </summary>
    [Guid("9ae31a4d-eda2-4f6d-aee1-419d226a9ab2")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.SpecialGraphic.DrawPoint")]
    public sealed class DrawPoint : BaseTool
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
        private System.Windows.Forms.TextBox txt_x;
        private System.Windows.Forms.TextBox txt_y;
        public DrawPoint(System.Windows.Forms.TextBox txtX, System.Windows.Forms.TextBox txtY)
        {
            txt_x = txtX;
            txt_y = txtY;
            base.m_category = ""; //localizable text 
            base.m_caption = "拾取点";  //localizable text 
            base.m_message = "拾取点";  //localizable text
            base.m_toolTip = "";  //localizable text
            base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            try
            {
                base.m_bitmap = Resources.EditingPointTool16;
                base.m_cursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream(GetType(), "Resources.AddPoint.cur"));
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

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            IPoint pMovePt = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            pMovePt = GIS.GraphicEdit.SnapSetting.getSnapPoint(pMovePt);
            txt_x.Text = Math.Round(pMovePt.X,3).ToString();
            txt_y.Text = Math.Round(pMovePt.Y,3).ToString();
            if (txt_x.FindForm()!=null&&txt_x.FindForm().Owner != null && txt_x.FindForm().Owner != GIS.Common.DataEditCommon.g_pAxMapControl.FindForm())
                txt_x.FindForm().Owner.WindowState = FormWindowState.Maximized;
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            IPoint pMovePt = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            GIS.GraphicEdit.SnapSetting.getSnapPoint(pMovePt);
        }
        #endregion
    }
}
