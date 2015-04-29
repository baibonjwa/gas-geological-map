// ******************************************************************
// 概  述：设置Farpoint通用默认属性类
// 作  者：杨小颖
// 创建日期：2014/03/08
// 版本号：V1.0
// 版本信息:
// V1.0 新建
// ******************************************************************
using System;
using System.Windows.Forms;

namespace LibCommon
{
    public static class FormDefaultPropertiesSetter
    {
        /// <summary>
        /// 设置管理界面窗体默认属性
        /// </summary>
        /// <param name="fm">需要设置的窗体</param>
        /// <param name="title">窗体标题（使用在LibFormTitles中定义的常量）</param>
        static public void SetManagementFormDefaultProperties(Form fm, string title)
        {
            fm.FormBorderStyle = FormBorderStyle.Sizable;
            fm.MinimizeBox = true;
            fm.MaximizeBox = true;
            fm.ShowIcon = true;
            fm.ShowInTaskbar = true;
            fm.StartPosition = FormStartPosition.CenterParent;
            fm.WindowState = FormWindowState.Maximized;
            fm.Text = title;
        }

        /// <summary>
        /// 设置管理界面子窗体的格式
        /// </summary>
        /// <param name="fm"></param>
        /// <param name="title"></param>
        static public void SetMdiChildrenManagementFormDefaultProperties(Form fm, string title)
        {
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.MinimizeBox = true;
            fm.MaximizeBox = true;
            fm.ShowIcon = true;
            fm.ShowInTaskbar = true;
            fm.StartPosition = FormStartPosition.CenterParent;
            fm.WindowState = FormWindowState.Maximized;
            fm.Text = title;
        }

        /// <summary>
        /// 设置录入界面窗体默认属性
        /// </summary>
        /// <param name="fm"></param>
        /// <param name="title">窗体标题（使用在LibFormTitles中定义的常量）</param>
        static public void SetEnteringFormDefaultProperties(Form fm, string title)
        {
            fm.FormBorderStyle = FormBorderStyle.FixedDialog;
            fm.MaximizeBox = false;
            fm.MinimizeBox = false;
            fm.StartPosition = FormStartPosition.CenterParent;
            fm.WindowState = FormWindowState.Normal;
            fm.Text = title;
        }
    }
}
