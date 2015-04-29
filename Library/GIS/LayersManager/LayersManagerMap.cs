using System;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Controls;

namespace GIS.LayersManager
{
    /// <summary>
    /// 地图管理
    ///</summary>
    [Guid("980475fe-2270-42ae-b967-fca7792bb10b")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("GIS.LayersManager.LayersManagerMap")]
    public class LayersManagerMap
    {
        private IToolbarMenu2 m_toolbarMenu = null;
        private bool m_beginGroupFlag = false;

        public LayersManagerMap()
        {
        }

        /// <summary>
        /// ToolbarMenu实例化和添加工具/命令按钮
        /// </summary>
        public void SetHook(object hook)
        {
            m_toolbarMenu = new ToolbarMenuClass();
            m_toolbarMenu.SetHook(hook);
            //添加工具/命令
            m_toolbarMenu.AddItem(new LayerVisibility(), 1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_toolbarMenu.AddItem(new LayerVisibility(), 2, 1, false, esriCommandStyles.esriCommandStyleTextOnly);
            AddItem("esriControls.ControlsAddDataCommand", -1);
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
