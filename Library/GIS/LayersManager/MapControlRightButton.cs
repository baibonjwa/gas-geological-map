using System;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Controls;

namespace GIS.LayersManager
{
    /// <summary>
    /// MapControl右键菜单
    ///</summary>
    [Guid("18bef24f-c0ed-411e-b22b-318ac3e8e890")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.LayersManager.MapControlRightButton")]
    public class MapControlRightButton
    {
        private IToolbarMenu2 m_toolbarMenu = null;
        private bool m_beginGroupFlag = false;

        public MapControlRightButton()
        {
        }

        /// <summary>
        /// ToolbarMenu实例化和添加工具/命令按钮
        /// </summary>
        public void SetHook(object hook)
        {

            //#region 系统右键功能菜单
            //IToolbarMenu toolmenu = new ToolbarMenuClass();	 //右键菜单
            //toolmenu.AddItem(new MenuCommand.CommandZoomIn(), -1, toolmenu.Count, false, esriCommandStyles.esriCommandStyleTextOnly);
            //toolmenu.AddItem(new MenuCommand.CommandZoomOut(), -1, toolmenu.Count, false, esriCommandStyles.esriCommandStyleTextOnly);
            //toolmenu.AddItem(new MenuCommand.CommandPan(), -1, toolmenu.Count, true, esriCommandStyles.esriCommandStyleTextOnly);
            //toolmenu.AddItem(new MenuCommand.CommandExtent(), -1, toolmenu.Count, false, esriCommandStyles.esriCommandStyleTextOnly);
            //toolmenu.AddItem(new MenuCommand.CommandMouseSelect(), -1, toolmenu.Count, true, esriCommandStyles.esriCommandStyleTextOnly);
            ////toolmenu.AddItem(new MenuCommand.CommandRectanleSelect(), -1, toolmenu.Count, false, esriCommandStyles.esriCommandStyleTextOnly);
            //toolmenu.AddItem(new MenuCommand.CommandMeasureLength(), -1, toolmenu.Count, true, esriCommandStyles.esriCommandStyleTextOnly);
            //toolmenu.AddItem(new MenuCommand.CommandMeasureArea(), -1, toolmenu.Count, false, esriCommandStyles.esriCommandStyleTextOnly);
            //#endregion



            m_toolbarMenu = new ToolbarMenuClass();
            m_toolbarMenu.SetHook(hook);
            //添加工具/命令
            //AddItem("esriControls.ControlsMapZoomOutFixedCommand", -1);
            //AddItem("esriControls.ControlsMapZoomInFixedCommand", -1);            
            m_toolbarMenu.AddItem(new GIS.View.ZoomInTool(), -1, m_toolbarMenu.Count, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_toolbarMenu.AddItem(new GIS.View.ZoomOutTool(), -1, m_toolbarMenu.Count, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_toolbarMenu.AddItem(new GIS.View.PanTool(), -1, m_toolbarMenu.Count, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_toolbarMenu.AddItem(new GIS.View.RefreshViewCommand(), -1, m_toolbarMenu.Count, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_toolbarMenu.AddItem(new GIS.View.FullExtentTool(), -1, m_toolbarMenu.Count, false, esriCommandStyles.esriCommandStyleIconAndText);
            BeginGroup();
            m_toolbarMenu.AddItem(new GIS.GraphicEdit.FeatureSelect(), -1, m_toolbarMenu.Count, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_toolbarMenu.AddItem(new GIS.GraphicModify.EditCopyCommand(), -1, m_toolbarMenu.Count, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_toolbarMenu.AddItem(new GIS.GraphicModify.EditCutCommand(), -1, m_toolbarMenu.Count, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_toolbarMenu.AddItem(new GIS.GraphicModify.EditPasteCommand(), -1, m_toolbarMenu.Count, false, esriCommandStyles.esriCommandStyleIconAndText);
            //BeginGroup();
            //AddItem("GIS.View.ZoomInTool",-1);
            //AddItem("GIS.View.ZoomOutTool", -1);
            //AddItem("GIS.View.PanTool", -1);
            //AddItem("GIS.View.RefreshViewCommand", -1);
            //AddItem("GIS.View.FullExtentCommand", -1);
            //BeginGroup(); //分隔条
            ////AddItem("{380FB31E-6C24-4F5C-B1DF-47F33586B885}", -1); //撤销
            ////AddItem(new Guid("B0675372-0271-4680-9A2C-269B3F0C01E8"), -1); //重做
            //AddItem("GIS.GraphicEdit.UndoEdit",-1);
            //AddItem("GIS.GraphicEdit.RedoEdit",-1);
            //BeginGroup();             
            ////AddItem("esriControls.ControlsEditingCopyCommand", -1);
            ////AddItem("esriControls.ControlsEditingPasteCommand", -1);
            ////AddItem("esriControls.ControlsEditingCutCommand", -1);  
            //AddItem("GIS.GraphicModify.EditCopyCommand", -1);
            //AddItem("GIS.GraphicModify.EditCutCommand", -1);
            //AddItem("GIS.GraphicModify.EditPasteCommand", -1);
         
        }

        /// <summary>
        /// 在指定位置弹出菜单
        /// </summary>
        /// <param name="X">X坐标</param>
        /// <param name="Y">Y坐标</param>
        /// <param name="hWndParent">父窗口句柄</param>
        public void PopupMenu(int X, int Y, int hWndParent)
        {
            if (m_toolbarMenu != null)
                m_toolbarMenu.PopupMenu(X, Y, hWndParent);
        }

        /// <summary>
        /// 运行时根据需要重新获取ToolbarMenu
        /// </summary>
        public IToolbarMenu2 ContextMenu
        {
            get
            {
                return m_toolbarMenu;
            }
        }

        #region Helper methods to add items to the context menu
        /// <summary>
        /// 添加分隔条
        /// </summary>
        private void BeginGroup()
        {
            m_beginGroupFlag = true;
        }

        /// <summary>
        /// 根据UID添加对应命令
        /// </summary>
        private void AddItem(UID itemUID)
        {
            m_toolbarMenu.AddItem(itemUID.Value, itemUID.SubType, -1, m_beginGroupFlag, esriCommandStyles.esriCommandStyleIconAndText);
            m_beginGroupFlag = false; //重设分割标志
        }

        /// <summary>
        /// 根据识别字符串和子索引将命令添加到命令条上
        /// </summary>
        private void AddItem(string itemID, int subtype)
        {
            UID itemUID = new UIDClass();
            try
            {
                itemUID.Value = itemID;
            }
            catch
            {
                //错误时处理：添加空GUID
                itemUID.Value = Guid.Empty.ToString("B");
            }

            if (subtype > 0)
                itemUID.SubType = subtype;
            AddItem(itemUID);

        }

        /// <summary>
        /// 根据GUID和子索引将命令添加到命令条上
        /// </summary>
        private void AddItem(Guid itemGuid, int subtype)
        {
            AddItem(itemGuid.ToString("B"), subtype);
        }

        /// <summary>
        /// 根据类型和子索引将命令添加到命令条上
        /// </summary>
        private void AddItem(Type itemType, int subtype)
        {
            if (itemType != null)
                AddItem(itemType.GUID, subtype);
        }

        #endregion

    }
}
