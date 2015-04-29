using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using GIS.Common;
using ESRI.ArcGIS.Carto;

namespace GIS.SpecialGraphic
{
    /// <summary>
    /// Summary description for ToolUpdateXZZ.
    /// </summary>
    [Guid("505119fc-f62a-4025-9305-2d1144b4ad21")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.SpecialGraphic.ToolUpdateXZZ")]
    public sealed class ToolUpdateXZZ : BaseTool
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
        FrmNewXZZ Form;
        IPoint dPoint;
        IFeature pFeature;
        public ToolUpdateXZZ(FrmNewXZZ form)
        {
            Form = form;
            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text 
            base.m_caption = "";  //localizable text 
            base.m_message = "";  //localizable text
            base.m_toolTip = "";  //localizable text
            base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            try
            {
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
            // TODO: Add ToolUpdateXZZ.OnClick implementation
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            pFeature = null;
            dPoint = m_hookHelper.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            GIS.Common.DataEditCommon.TestExistFeature(m_hookHelper, X, Y, ref pFeature);
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolUpdateXZZ.OnMouseMove implementation
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add ToolUpdateXZZ.OnMouseUp implementation
        }
        public override void OnDblClick()
        {
            if (pFeature != null)
            {
                var AnnoLayer = DataEditCommon.GetLayerByName(DataEditCommon.g_pMap, LayerNames.LAYER_ALIAS_MR_AnnotationXZZ) as IFeatureLayer;//×¢¼ÇÍ¼²ã
                if (AnnoLayer == null)
                {
                    return;
                }
                string bid = pFeature.get_Value(pFeature.Fields.FindField("bid")).ToString();
                Form.Text="ÐÞ¸ÄÐ¡Öù×´";
                Form.Tag=bid;
                IFeatureClass pFeatureClass = AnnoLayer.FeatureClass;
                IQueryFilter pFilter = new QueryFilterClass();
                pFilter.WhereClause = "bid='"+bid+"'";
                IFeatureCursor pCursor = pFeatureClass.Search(pFilter, false);
                IFeature mFeature = pCursor.NextFeature();
                int k = 0;
                int count = pFeatureClass.FeatureCount(pFilter);
                System.Collections.Generic.KeyValuePair<int, string>[] listobj = new System.Collections.Generic.KeyValuePair<int, string>[count];
                while (mFeature != null)
                {
                    if (k == 0)
                    {
                        Form.txtAngle.Text = mFeature.get_Value(mFeature.Fields.FindField("strAngle")).ToString();
                        Form.txtBlc.Text = mFeature.get_Value(mFeature.Fields.FindField("strScale")).ToString();
                        Form.txtX.Text = mFeature.get_Value(mFeature.Fields.FindField("strX")).ToString();
                        Form.txtY.Text = mFeature.get_Value(mFeature.Fields.FindField("strY")).ToString();
                    }
                    int index=Convert.ToInt32(mFeature.get_Value(mFeature.Fields.FindField("strIndex")).ToString());
                    int type=Convert.ToInt32(mFeature.get_Value(mFeature.Fields.FindField("strType")).ToString());
                    string str=mFeature.get_Value(mFeature.Fields.FindField("TextString")).ToString();
                    listobj[index - 1] = new System.Collections.Generic.KeyValuePair<int, string>(type,str);
                    mFeature = pCursor.NextFeature();
                    k++;
                }
                if (listobj.Length > 0)
                {
                    Form.dgrdvZhzzt.RowCount = listobj.Length;
                    for (int i = 0; i < listobj.Length - 1; i++)
                    {
                        Form.dgrdvZhzzt.Rows[i].Cells[0].Value = listobj[i].Value;
                        DataGridViewComboBoxCell cell = Form.dgrdvZhzzt.Rows[i].Cells[1] as DataGridViewComboBoxCell;
                        if (listobj[i].Key == 0)
                            cell.Value = "ÑÒ²ã";
                        else
                            cell.Value = "Ãº²ã";
                    }
                    Form.txtDBBG.Text = listobj[listobj.Length - 1].Value;
                }
            }
        }
        #endregion
    }
}
