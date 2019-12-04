// 检测是否存在SNI RST的程序，接受单个IP、域名或整个URL
using System;
using System.Diagnostics;

class SNI
{
    const string TargetIP = "104.131.212.184"; // 用于域前置的IP；community.notepad-plus-plus.org
    const string NormalNCNHost = "usa.baidu.com"; // 用于检测TargetIP是否被封，必须用正常的域名
    const string NormalCNHost = "www.baidu.com"; // 用于检测是否联网，可换成其它普通国内域名/IP

    static int Main(string[] args)
    {
#if DEBUG
        args = new[] { "www.bbc.com" };
#endif
        try
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: sni.exe [url]");
                if (args.Length == 0)
                    return 0;
                throw new ArgumentException("Expect one and only one url.");
            }

            string host = new UriBuilder(args[0]).Host;
            return new SNI().Run(host);
        }
        catch (Exception ex)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex);
            Console.ForegroundColor = color;
            return -1;
        }
    }

    int Run(string requestedHost) =>
        PingFailed(NormalCNHost) ? Log("Check your internet connection.", 3) :
        CheckTimeout(NormalNCNHost) ? Log("Cannot connect to target IP.", 2) :
        HasSniRst(requestedHost) ? Log("Has SNI RST.", 1) :
        Log("No SNI RST.", 0);

    string Curl(string host)
    {
        var psi = new ProcessStartInfo("curl", $"-I -sS --connect-timeout 1 --resolve {host}:443:{TargetIP} https://{host}")
        { UseShellExecute = false, RedirectStandardError = true };

        var p = Process.Start(psi);
        p.WaitForExit();

        return p.StandardError.ReadToEnd();
    }

    bool HasSniRst(string host) => Curl(host).Contains("failed to receive handshake"); // 充分不必要
    bool CheckTimeout(string host) => Curl(host).Contains("Connection timed out"); // 自然超时用的是 port 443: Timed out

    bool PingFailed(string host)
    {
        var p = Process.Start(
            new ProcessStartInfo("ping", $"-{GetParam()} 1 {host}")
            { UseShellExecute = false, RedirectStandardError = true, RedirectStandardOutput = true }
        );
        p.WaitForExit();
        return p.ExitCode != 0;

        string GetParam() => // Win下是-n，Linux下是-c
            Environment.OSVersion.Platform == PlatformID.Win32NT ? "n" : "c";
    }

    // 为了能在Run用三元表达式
    int Log(string message, int returncode)
    {
        var color = Console.ForegroundColor;
        if (returncode == 1)
            Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ForegroundColor = color;
        return returncode;
    }
    // 未来加上命令行选项可以在Debug下用这个记录要运行的外部程序，它们的输出也要处理但不是在这个函数里
    string Log(string message)
    {
        Console.WriteLine(message);
        return message;
    }
}
