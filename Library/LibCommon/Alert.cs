
using System.Windows.Forms;

namespace LibCommon
{
    public class Alert
    {
        private const string MSG_TITLE = "消息";
        public static void NoteMsg(string message)
        {
            MessageBox.Show(message, MSG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 提示对话框
        /// </summary>
        /// <param name="message">提示内容</param>
        public static void AlertMsg(string message)
        {
            MessageBox.Show(message, MSG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        /// <summary>
        /// 提示对话框(重载)
        /// </summary>
        /// <param name="message">提示内容</param>
        /// <param name="title">窗口标题</param>
        public static void AlertMsg(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 提示对话框(重载)
        /// </summary>
        /// <param name="message">提示内容</param>
        /// <param name="title">窗口标题</param>
        /// <param name="messageBoxButton">按钮样式</param>
        public static void AlertMsg(string message, string title, MessageBoxButtons messageBoxButton)
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
        public static void AlertMsg(string message, string title, MessageBoxButtons messageBoxButton, MessageBoxIcon messageBoxIcon)
        {
            MessageBox.Show(message, title, messageBoxButton, messageBoxIcon);
        }

        /// <summary>
        /// 确定对话框
        /// </summary>
        /// <param name="message">提示内容</param>
        /// <returns></returns>
        public static bool Confirm(string message)
        {
            return MessageBox.Show(message, MSG_TITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
        }
        /// <summary>
        /// 确定对话框(重载)
        /// </summary>
        /// <param name="message">提示内容</param>
        /// <param name="title">窗口标题</param>
        /// <returns></returns>
        public static bool Confirm(string message, string title)
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
        public static bool Confirm(string message, string title, MessageBoxButtons messageBoxButton)
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
        public static bool Confirm(string message, string title, MessageBoxButtons messageBoxButton, MessageBoxIcon messageBoxIcon)
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
    }
}
