using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using GIS.Properties;
using ESRI.ArcGIS.SystemUI;
using GIS.Common;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace GIS.MapPrint
{
    /// <summary>
    /// 添加文字信息
    /// </summary>
    [Guid("7ff31944-e34f-4281-bc63-cac27cc31be6")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.MapPrint.AddTextElement")]
    public sealed class AddTextElement : BaseTool
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

        private IPoint m_point = null;
        //private ICommand m_command = null;
        public AddTextElement()
        {
            //
            // 公共属性定义
            //
            base.m_category = "MapPrint";
            base.m_caption = "添加文字信息";
            base.m_message = "通过在地图点击，进行文字信息添加";
            base.m_toolTip = "添加文字信息";
            base.m_name = "MapPrint_AddTextElement";
            try
            {
                base.m_bitmap = Resources.ElementText16;
                //base.m_cursor = new System.Windows.Forms.Cursor(GetType(), "ViewCursors." + GetType().Name + ".cur");
                //m_command = new ESRI.ArcGIS.Controls.ControlsMapZoomOutTool();
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
                //m_command.OnCreate(hook);
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

        /// <summary>
        /// 点击事件
        /// </summary>
        public override void OnClick()
        {
            //if (Common.DataEditCommon.g_printAxPageLayoutControl.CurrentTool != null)
            //    Common.DataEditCommon.g_printAxPageLayoutControl.CurrentTool = null;
            //Common.MapPrintCommon.g_axPageLayoutControl.ActiveView.FocusMap.ClearSelection();
            //DataEditCommon.g_pMyMapCtrl.CurrentTool = (ITool)m_command;//设为当前操作命令 
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            Common.MapPrintCommon.GraphicsContainClearSelection();
            IPoint point = new PointClass();
            //m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            point = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            
            TextSetUp textSetUp = new TextSetUp();
            textSetUp.NewTextElement(point);
            if (textSetUp.ShowDialog() == DialogResult.OK)
            {
                Common.MapPrintCommon.g_axPageLayoutControl.CurrentTool = Common.MapPrintCommon.SetControlsSelectCommand();
            }
            
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {

        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {

        }

     

        #endregion
    }
}
