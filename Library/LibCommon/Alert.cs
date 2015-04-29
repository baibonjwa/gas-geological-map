// ******************************************************************
// 概  述：提示对话框共通类
// 作  者：伍鑫
// 创建日期：2013/11/26
// 版本号：V1.2
// 版本信息：
// V1.0 新建
// V1.1 添加确认对话框方法 2014/02/25 By 伍鑫
// V1.2 完善提示对话框，确定对话框方法重载 2014/03/17 By 伍鑫
// ******************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibCommon
{
    public class Alert
    {
        public static void noteMsg(string message)
        {
            MessageBox.Show(message, Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 提示对话框
        /// </summary>
        /// <param name="message">提示内容</param>
        public static void alert(string message)
        {
            // update at 2014/03/17 By 伍鑫 -- Start

            //MessageBox.Show(message, "提示");
            MessageBox.Show(message, Const.NOTES, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            // update at 2014/02/17 By 伍鑫 -- End
        }

        // add at 2014/03/17 By 伍鑫 -- Start
        /// <summary>
        /// 提示对话框(重载)
        /// </summary>
        /// <param name="message">提示内容</param>
        /// <param name="title">窗口标题</param>
        public static void alert(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 提示对话框(重载)
        /// </summary>
        /// <param name="message">提示内容</param>
        /// <param name="title">窗口标题</param>
        /// <param name="messageBoxButton">按钮样式</param>
        public static void alert(string message, string title, MessageBoxButtons messageBoxButton)
        {
            MessageBox.Show(message, title, messageBoxButton, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 提示对话框(重载)
        /// </summary>
        /// <param name="message">提示内容</param>
        /// <param name="title">窗口标题</param>
        /// <param name="messageBoxButton">按钮样式</param>
        /// <param name="messageBoxIcon">图标样式</param>
        public static void alert(string message, string title, MessageBoxButtons messageBoxButton, MessageBoxIcon messageBoxIcon)
        {
            MessageBox.Show(message, title, messageBoxButton, messageBoxIcon);
        }
        // add at 2014/02/17 By 伍鑫 -- End

        // add at 2014/02/25 By 伍鑫 -- Start
        /// <summary>
        /// 确定对话框
        /// </summary>
        /// <param name="message">提示内容</param>
        /// <returns></returns>
        public static bool confirm(string message)
        {
            return MessageBox.Show(message, Const.NOTES, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
        }
        // add at 2014/02/25 By 伍鑫 -- End

        // add at 2014/03/17 By 伍鑫 -- Start
        /// <summary>
        /// 确定对话框(重载)
        /// </summary>
        /// <param name="message">提示内容</param>
        /// <param name="title">窗口标题</param>
        /// <returns></returns>
        public static bool confirm(string message, string title)
        {
            return MessageBox.Show(message, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
        }

        /// <summary>
        /// 确定对话框(重载)
        /// </summary>
        /// <param name="message">提示内容</param>
        /// <param name="title">窗口标题</param>
        /// <param name="messageBoxButton">按钮样式</param>
        /// <returns></returns>
        public static bool confirm(string message, string title, MessageBoxButtons messageBoxButton)
        {
            bool Result = false;
            if (messageBoxButton == MessageBoxButtons.OKCancel)
            {
                 Result = MessageBox.Show(message, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
            }
            else if (messageBoxButton == MessageBoxButtons.YesNo)
            {
                Result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
            }

            return Result;
        }

        /// <summary>
        /// 确定对话框(重载)
        /// </summary>
        /// <param name="message">提示内容</param>
        /// <param name="title">窗口标题</param>
        /// <param name="messageBoxButton">按钮样式</param>
        /// <param name="messageBoxIcon">图标样式</param>
        /// <returns></returns>
        public static bool confirm(string message, string title, MessageBoxButtons messageBoxButton, MessageBoxIcon messageBoxIcon)
        {
            bool Result = false;
            if (messageBoxButton == MessageBoxButtons.OKCancel)
            {
                Result = MessageBox.Show(message, title, MessageBoxButtons.OKCancel, messageBoxIcon) == DialogResult.OK;
            }
            else if (messageBoxButton == MessageBoxButtons.YesNo)
            {
                Result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, messageBoxIcon) == DialogResult.Yes;
            }

            return Result;
        }
        // add at 2014/02/17 By 伍鑫 -- End
    }
}
