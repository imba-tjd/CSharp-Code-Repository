// 用于给pac.txt添加域名
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        StringBuilder sb = new StringBuilder();
        TextReader tr = null;

        try
        {
            // if (File.Exists("pac.txt"))
            File.Copy("pac.txt", "pac.bak", true); // 反正如果没有pac.txt下面也会抛异常
            tr = new StreamReader("pac.txt");
        }
        catch (Exception ex)
        {
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

        foreach (string url in args)
        {
            // string turl = url; // 不需要trim
            // if (turl.StartsWith("http")) // 如果以 http:// 或 https:// 开头
            //     turl = turl.Substring(turl.IndexOf('/') + 2);
            // if (turl.IndexOf('/') is var end && end != -1) // 寻找去掉协议后的第一个 '/'
            //     turl = turl.Substring(0, end); // 第二个参数是个数而不是索引，而end是索引，根据不对称原理恰好为end个，turl[end]及之后内容会被去掉。

            int start = url.StartsWith("http") ? url.IndexOf('/') + 2 : 0;
            int end = url.IndexOf('/', start) is int i && i == -1 ? url.Length : i;
            string host = url.Substring(start, end - start);

            if (host != "")
                sb.AppendLine($"  '{host}': 1,");
        }

        sb.Append(await tr.ReadToEndAsync()); // 把剩下的内容读入缓冲区
        tr.Dispose();

        using TextWriter tw = new StreamWriter("pac.txt", false); // dispose时会自动flush
        try
        {
            tw.Write(sb); // 会覆盖原文件
        }
        catch (Exception ex)
        {
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
