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
        if (args.Length != 1)
            throw new ArgumentException("Expect one and only one url.");

        string host = new UriBuilder(args[0]).Host;
        return new SNI().Run(host);
    }

    int Run(string requestedHost) =>
        PingFailed(NormalCNHost) ? Log("Check your internet connection.", 3) :
        HasSniRst(NormalNCNHost) ? Log("Cannot connect to target IP.", 2) :
        HasSniRst(requestedHost) ? Log("Has SNI RST.", 1) :
        Log("Not SNI RST.", 0);

    bool HasSniRst(string host)
    {
        var psi = new ProcessStartInfo("curl", $"-I -sS --resolve {host}:443:{TargetIP} https://{host}")
        { UseShellExecute = false, RedirectStandardError = true };

        var p = Process.Start(psi);
        p.WaitForExit();

        string result = p.StandardError.ReadToEnd();
        return result.Contains("failed to receive handshake"); // 为false时并不意味着就没有SNI RST了
    }

    bool PingFailed(string host)
    {
        var p = Process.Start(
            new ProcessStartInfo("ping", $"-{GetParam()} 2 {host}")
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
        Console.WriteLine(message);
        return returncode;
    }
}
