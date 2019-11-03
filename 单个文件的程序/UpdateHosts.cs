using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class UH
{
    const string ADDR = "https://raw.githubusercontent.com/googlehosts/hosts/master/hosts-files/hosts-compact-no-sni-rst";
    const string HOSTSPATH = @"C:\Windows\System32\drivers\etc\hosts";
    bool DEBUG => true; // 未来如果支持了命令行选项，可以改成可修改的

    // 以静态构造函数做兼容Win7的配置
    static UH() => System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
    static async Task Main() => await new UH().Run();

    async Task Run()
    {
        if (Environment.OSVersion.Platform != PlatformID.Win32NT)
            throw new PlatformNotSupportedException("This program only supports Windows.");

        // 开始下载
        Log("Start downloading.");
        var newHOSTS = new HttpClient() { Timeout = TimeSpan.FromSeconds(15) }.GetStringAsync(ADDR);

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
            await writer.WriteAsync(downloaded);
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
}
