// 用于给ss/ssr的pac.txt添加域名

using System;
using System.IO;
using System.Text;

namespace AddDomainsToPac
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            TextReader tr = null;

            try {
                // if (File.Exists("pac.txt"))
                File.Copy("pac.txt", "pac.bak", true); // 反正如果没有pac.txt下面也会抛异常

                tr = new StreamReader("pac.txt");
            } catch (Exception ex) {
                Console.WriteLine("读文件时发生严重错误！");
                Console.WriteLine(ex.Message);
                Console.WriteLine("按任意键退出...");
                Console.ReadKey(true);
                Environment.Exit(ex.HResult);
            }

            if (args.Length == 0)
            {
                args = new string[1];
                Console.Write("请输入要添加进pac中的网址：");
                args[0] = Console.ReadLine();
            }

            sb.AppendLine(tr.ReadLine()); // 读取第一行的var domains = {

            foreach (string arg in args)
            {
                int start, end;
                string targ = arg; // 不需要trim

                if ((start = arg.IndexOf('/')) != -1) // 如果以 http:// 或 https:// 开头
                    targ = targ.Substring(start + 2);

                if ((end = targ.IndexOf('/')) != -1) // 寻找去掉协议后的第一个 '/'
                    targ = targ.Substring(0, end); // 第二个参数是个数而不是索引，而end是索引，根据不对称原理恰好为end个，targ[end]及之后内容会被去掉。

                sb.AppendLine(string.Format("  \"{0}\": 1,", targ));
            }

            sb.Append(tr.ReadToEnd()); // 把剩下的内容读入缓冲区
            tr.Dispose();

            using (TextWriter tw = new StreamWriter("pac.txt", false)) // dispose时会自动flush
                try{
                    tw.Write(sb); // 会覆盖原文件
                } catch(Exception ex) {
                    Console.WriteLine("写文件时发生严重错误！");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("请自行恢复备份的文件。");
                    Console.WriteLine("按任意键退出...");
                    Console.ReadKey(true);
                    Environment.Exit(ex.HResult);
                }

            System.Diagnostics.Process.Start("pac.txt");
        }
    }
}
