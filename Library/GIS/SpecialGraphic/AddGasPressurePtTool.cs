using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using GIS.Common;
using ESRI.ArcGIS.Carto;

namespace GIS.SpecialGraphic
{
    /// <summary>
    /// Summary description for AddGasPressurePtTool.
    /// </summary>
    [Guid("1f0811d1-207d-40b3-b068-67a2c31c1a13")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.SpecialGraphic.AddGasPressurePtTool")]
    public sealed class AddGasPressurePtTool : BaseTool
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
        private ILayer m_pCurrentLayer;

        public AddGasPressurePtTool()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "专业图元"; //localizable text 
            base.m_caption = "瓦斯压力点";  //localizable text 
            base.m_message = "输入相应参数，绘制瓦斯压力点";  //localizable text
            base.m_toolTip = "绘制瓦斯压力点";  //localizable text
            base.m_name = "AddGasPressurePtTool";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            try
            {
                //
                // TODO: change resource name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
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

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            DataEditCommon.InitEditEnvironment();
            DataEditCommon.CheckEditState();

            m_pCurrentLayer = DataEditCommon.g_pLayer;
            IFeatureLayer featureLayer = m_pCurrentLayer as IFeatureLayer;
            if (featureLayer == null)
            {
                MessageBox.Show("请选择绘制图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataEditCommon.g_pMyMapCtrl.CurrentTool = null;
                return;
            }
            else
            {
                //if (featureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPolyline)
                //{
                //    MessageBox.Show("请选择线状图层。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    DataEditCommon.g_pMyMapCtrl.CurrentTool = null;
                //    return;
                //}
            }
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            
            IPoint pt = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);



            //_4.OutburstPrevention.GasPressureInfoEntering gasPressureInfoEnteringForm = new GasPressureInfoEntering();
            //if (DialogResult.OK == gasPressureInfoEnteringForm.ShowDialog())
            //{
                //// 加载瓦斯压力数据
                //loadGasPressureInfo();
                //// 跳转到尾页
                //this.dataPager1.btnLastPage_Click(sender, e);
            //}


            //GISCommon.CreateSepcialSymbol.frmAddSpecialSymbol pfrmAddSpecialSymbol = new CreateSepcialSymbol.frmAddSpecialSymbol();
            //pfrmAddSpecialSymbol.ShowDialog();


        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add AddGasPressurePtTool.OnMouseMove implementation
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add AddGasPressurePtTool.OnMouseUp implementation
        }
        #endregion
    }
}
