using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using GIS.Properties;
using GIS.Common;

namespace GIS.GraphicEdit
{
    /// <summary>
    /// 对象捕捉工具
    /// </summary>
    //[Guid("31cb399c-c8cd-4b6d-aee0-a1b8dc6404ae")]
    //[ClassInterface(ClassInterfaceType.None)]
    //[ProgId("GIS.GraphicEdit.SnapSetting")]
    //public sealed class SnapSetting : BaseCommand
    public class SnapSetting
    {
        #region COM Registration Function(s)
        //[ComRegisterFunction()]
        //[ComVisible(false)]
        //static void RegisterFunction(Type registerType)
        //{
        //    // Required for ArcGIS Component Category Registrar support
        //    ArcGISCategoryRegistration(registerType);

        //    //
        //    // TODO: Add any COM registration code here
        //    //
        //}

        //[ComUnregisterFunction()]
        //[ComVisible(false)]
        //static void UnregisterFunction(Type registerType)
        //{
        //    // Required for ArcGIS Component Category Registrar support
        //    ArcGISCategoryUnregistration(registerType);

        //    //
        //    // TODO: Add any COM unregistration code here
        //    //
        //}

        //#region ArcGIS Component Category Registrar generated code
        ///// <summary>
        ///// Required method for ArcGIS Component Category registration -
        ///// Do not modify the contents of this method with the code editor.
        ///// </summary>
        //private static void ArcGISCategoryRegistration(Type registerType)
        //{
        //    string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
        //    MxCommands.Register(regKey);
        //    ControlsCommands.Register(regKey);
        //}
        ///// <summary>
        ///// Required method for ArcGIS Component Category unregistration -
        ///// Do not modify the contents of this method with the code editor.
        ///// </summary>
        //private static void ArcGISCategoryUnregistration(Type registerType)
        //{
        //    string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
        //    MxCommands.Unregister(regKey);
        //    ControlsCommands.Unregister(regKey);
        //}

        //#endregion
        #endregion

        public static IHookHelper m_hookHelper = null;
        public static bool m_bStartSnap = false;       // 是否启动捕捉        
        public SnapSetting()
        {
            ////公共属性定义
            //base.m_category = "编辑"; 
            //base.m_caption = "点捕捉开关"; 
            //base.m_message = "点捕捉开关";
            //base.m_toolTip = "捕捉点";  
            //base.m_name = "SnapSetting";  

            //try
            //{
            //    base.m_bitmap = Resources.SnappingPoint16;
            //}
            //catch (Exception ex)
            //{
            //    System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            //}
        }

        #region Overridden Class Methods

        ///// <summary>
        ///// 创建工具
        ///// </summary>
        ///// <param name="hook">程序实例</param>
        //public override void OnCreate(object hook)
        //{
        //    if (hook == null)
        //        return;

        //    try
        //    {
        //        m_hookHelper = new HookHelperClass();
        //        m_hookHelper.Hook = hook;
        //        if (m_hookHelper.ActiveView == null)
        //            m_hookHelper = null;
        //    }
        //    catch
        //    {
        //        m_hookHelper = null;
        //    }

        //    if (m_hookHelper == null)
        //        base.m_enabled = false;
        //    else
        //        base.m_enabled = true;
        //}
        
        //public override void OnClick()
        //{

        //    if (base.m_checked == false)
        //    {
        //        base.m_checked = true;
        //        m_bStartSnap = true;
        //        //StartSnappingEnv();
        //    }
        //    else
        //    {
        //        m_bStartSnap = false;
        //        base.m_checked = false;
        //    }
        //}
        #endregion

        //捕捉相关
        static ISnappingEnvironment m_SnappingEnv;
        static IPointSnapper m_Snapper;
        static ISnappingFeedback m_SnappingFeedback;
        public static esriSnappingType snappingType;
        //设置捕捉环境
        public static void StartSnappingEnv()
        {
            if (m_bStartSnap)
            {
                m_SnappingEnv = DataEditCommon.GetSnapEnvironment(m_hookHelper, snappingType);
                if (m_SnappingEnv != null)
                {
                    m_Snapper = m_SnappingEnv.PointSnapper;
                    m_SnappingFeedback = new SnappingFeedbackClass();
                    m_SnappingFeedback.Initialize(m_hookHelper.Hook, m_SnappingEnv);
                }
            }
        }
        public static IPoint getSnapPoint(IPoint ptIn)
        {
            IPoint SnapPt = ptIn;
            try
            {
                if (m_bStartSnap)
                {
                    if (m_Snapper == null)
                    {
                        StartSnappingEnv();
                    }
                    ISnappingResult snapResult = m_Snapper.Snap(ptIn);
                    m_SnappingFeedback.Update(snapResult, 0);
                    //更新当前点为捕捉到的点
                    if (snapResult != null)
                        SnapPt = snapResult.Location;
                }
                return SnapPt;
            }
            catch { return SnapPt; }
        }
    
    
    }
}
