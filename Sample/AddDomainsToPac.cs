// 用于给ss的pac.txt添加域名

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
        File.Copy("pac.txt", "pac.bak", true);
        TextReader tr = new StreamReader("pac.txt");
        StringBuilder sb = new StringBuilder();

        if (args.Length == 0)
        {
            args = new string[1];
            args[0] = Console.ReadLine();
        }

        sb.AppendLine(tr.ReadLine());
        foreach (var arg in args)
            sb.AppendLine(string.Format("  \"{0}\": 1,", arg.Trim()));
        sb.Append(tr.ReadToEnd());

        tr.Dispose();
        TextWriter tw = new StreamWriter("pac.txt", false);
        tw.Write(sb);
        tw.Flush();
        tw.Dispose();
        System.Diagnostics.Process.Start("pac.txt");
    }
}
