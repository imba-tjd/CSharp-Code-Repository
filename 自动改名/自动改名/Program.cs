/*
 * 由SharpDevelop创建。
 * 用户： 谭九鼎
 * 日期: 2017/6/3
 * 时间: 21:57
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace 自动改名
{
    /// <summary>
    /// Class with program entry point.
    /// </summary>
    internal sealed class Program
    {
        [DllImport("user32.dll")]
        private static extern void SetProcessDPIAware();
        /// <summary>
        /// Program entry point.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            SetProcessDPIAware(); 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
        
    }
}
