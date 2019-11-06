// 编译：csc ./UpdateHosts.cs /r:system.net.http.dll /o+
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class UH
{
    const string ADDR = "https://raw.githubusercontent.com/googlehosts/hosts/master/hosts-files/hosts-compact-no-sni-rst";
    const string ADDR_MIRROR = "https://coding.net/u/scaffrey/p/hosts/git/raw/master/hosts-files/hosts-compact-no-sni-rst";
    const string HOSTSPATH = @"C:\Windows\System32\drivers\etc\hosts";

    bool DEBUG => true; // 未来如果支持了命令行选项，可以改成可修改的
    bool UseMirror { get; set; } = false;
    static HttpClient HC { get; set; }

    // 以静态构造函数做兼容Win7的配置
    static UH()
    {
        System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
        var hch = new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip };
        HC = new HttpClient(hch) { Timeout = TimeSpan.FromSeconds(15) };
        HC.DefaultRequestHeaders.UserAgent.ParseAdd("curl");
    }
    static async Task Main() => await new UH().Run();

    async Task Run()
    {
        if (Environment.OSVersion.Platform != PlatformID.Win32NT)
            throw new PlatformNotSupportedException("This program only supports Windows.");

        if (!CanConnect(ADDR))
        {  // 其实更好的方式是手动指定IP，然而C#不支持；要不就域前置，用IP连，改HOST，但又会有安全问题
            Log("Cannot connect to GitHub src, trying mirror...");
            UseMirror = true;
        }

        // 开始下载
        Log("Start downloading.");
        string addr = UseMirror ? ADDR_MIRROR : ADDR;
        var newHOSTS = HC.GetStreamAsync(addr);
        // var newHOSTS = HC.GetStringAsync(addr);

        FileInfo HOSTS = new FileInfo(HOSTSPATH);
        if (!HOSTS.Exists)
            HOSTS.Create(); // 如果权限不够，现在会直接失败

        // 读取自定义的HOSTS，只会读取开头的部分
        Log("Start reading current custom hosts.");
        var customContent = new System.Text.StringBuilder();
        using (var reader = new StreamReader(HOSTS.OpenRead())) // reader关闭时也会关闭流
            while (true)
            {
                string line = reader.ReadLine();
                if (line.StartsWith("# Copyright (c) 2017"))
                    break;
                customContent.AppendLine(line);
            }
        Log("End reading current custom hosts.");

        // 如果下载失败，不应该写文件，所以放在任何写入的前面
        var downloaded = await newHOSTS;
        // Log($"Content length: {downloaded.Length}"); // 未来可以加个计时的功能；GZipStream不支持获取长度，之前的string是可以的
        Log("End downloading.");

        // 备份
        Log("Start backupping.");
        BackUp(HOSTS);
        Log("End backupping.");

        // 写入自定义和新的HOSTS
        Log("Start writing new hosts.");
        using (var writer = HOSTS.CreateText())
        {
            await writer.WriteAsync(customContent.ToString());
            // await writer.WriteAsync(downloaded); // GetStringAsync时用的代码
            await writer.FlushAsync();
            await downloaded.CopyToAsync(writer.BaseStream);
            await writer.BaseStream.FlushAsync();
        }
        Log("End writing new hosts.");
    }

    void BackUp(FileInfo f)
    {
        if (!f.Exists)
            throw new FileNotFoundException("can't find target file", f.Name);

        string target = f.FullName + ".bak";
        try
        {
            f.OpenWrite().Close(); // 防止hosts.bak可写但hosts本身不可写的情况
            f.CopyTo(target, true); // 直接覆盖；如果要全保留，要么追加，要么在别的地方保存。否则要么每次都弹UAC，要么修改父文件夹的权限，这两种都不可接受
        }
        catch (UnauthorizedAccessException)
        {
            // 其实3.0之后FileInfo支持Get/SetAccessControl()且无需package了，但看起来有点复杂
            Log("Applying UAC to modify the privilage.");
            RunCMD($"copy /Y \"{f.FullName}\" \"{target}\" &&" +
                    $"cacls \"{f.FullName}\" /E /G Users:W &&" +
                    $"cacls \"{target}\" /E /G Users:W");
        }

        // 一个流程最多调用一次，否则多次弹UAC不可接受；后者要不就改成申请UAC启动自己
        void RunCMD(string command) =>
            System.Diagnostics.Process.Start(
                new System.Diagnostics.ProcessStartInfo("cmd.exe", "/C " + command)
                { Verb = "runas", UseShellExecute = true, CreateNoWindow = true }
            ).WaitForExit();
    }

    void Log(string message)
    {
        if (this.DEBUG)
            Console.WriteLine(message);
    }

    // 然而raw.githubusercontent.comb不让ping；而且现在Windows不让往Raw Socket发TCP包了
    bool Ping(string host) => new System.Net.NetworkInformation.Ping()
        .SendPingAsync(host, 2).GetAwaiter().GetResult() // 默认超时是5秒
        .Status == System.Net.NetworkInformation.IPStatus.Success;

    // 试一下老的连接方式
    static bool CanConnect(string url)
    {
        if (!url.StartsWith("http"))
            url = "http://" + url;
        var req = System.Net.HttpWebRequest.CreateHttp(url);
        req.Method = "HEAD";
        req.Proxy = System.Net.GlobalProxySelection.GetEmptyWebProxy(); // Deprecated了，但推荐的方式是个全局的属性
        req.Timeout = 5000;
        req.KeepAlive = false;
        System.Net.HttpWebResponse rsp;
        try
        {
            rsp = req.GetResponse() as System.Net.HttpWebResponse; // 有async方法但已经这样了就无所谓了
        }
        catch (System.Net.WebException)
        {
            return false;
        }
        var sc = rsp.StatusCode;
        rsp.Close();
        return sc == System.Net.HttpStatusCode.OK;
    }
}
