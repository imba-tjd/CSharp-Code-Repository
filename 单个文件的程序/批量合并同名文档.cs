// https://www.zhihu.com/question/284015230
using System;
// using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace DotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            Directory.GetFiles(
                args?[1] ?? Environment.CurrentDirectory, "*.txt", SearchOption.AllDirectories
                ).GroupBy(
                    x => Path.GetFileName(x)
                    ).AsParallel().ForAll(
                        x =>
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (var item in x)
                                sb.AppendLine(File.ReadAllText(item) + Environment.NewLine);
                            File.WriteAllText(x.Key, sb.ToString());

                            // foreach (var item in x)
                            //     using (TextReader tr = new StreamReader(item))
                            //     {
                            //         sb.AppendLine(tr.ReadToEnd());
                            //         sb.AppendLine();
                            //     }
                            // using (TextWriter tw = new StreamWriter(x.Key))
                            // {
                            //     tw.Write(sb.ToString());
                            // }
                        }
                        );
        }
    }
}
