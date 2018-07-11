// https://www.zhihu.com/question/284015230
using System;
// using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Dotnet1
{
    class Program
    {
        static void Main()
        {
            Directory.GetFiles(
                Environment.CurrentDirectory, "*.txt", SearchOption.AllDirectories
                ).GroupBy(
                    x => Path.GetFileName(x)
                    ).AsParallel().ForAll(
                        x =>
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (var item in x)
                                using (TextReader tr = new StreamReader(item))
                                {
                                    sb.AppendLine(tr.ReadToEnd() + Environment.NewLine);
                                }
                            using (TextWriter tw = new StreamWriter(x.Key))
                            {
                                tw.Write(sb.ToString());
                            }
                        }
                        );
        }
    }
}
