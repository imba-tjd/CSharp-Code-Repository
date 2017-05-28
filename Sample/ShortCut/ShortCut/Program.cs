// 《Learning Hard 学习笔记》代码
// http://www.ituring.com.cn/book/1604

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SnippingTool
{
    static class Program
    {
        [DllImport("user32.dll")]
        private static extern void SetProcessDPIAware();
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new 聊天窗体());
        }
    }
}
