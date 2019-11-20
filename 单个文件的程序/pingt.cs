using System;
using System.Diagnostics;

// 对tcping的简单封装，自动使用443
class PingT
{
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("参数不正确");
            Environment.Exit(1);
        }

        // 无论输入主机还是网址都能运行，后者会自动提取出主机，；但为了使用Uri，又要当输入前者时加上协议
        string url = args[0].Trim();
        if (!url.StartsWith("http"))
            url = "http://" + url;
        string host = new Uri(url).Host;

        Process.Start(new ProcessStartInfo("tcping", $"-n 8 {host} 443") { UseShellExecute = false }).WaitForExit();
    }
}
