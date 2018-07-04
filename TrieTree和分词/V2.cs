using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> files = new List<string>(Directory.GetFiles(Environment.CurrentDirectory + "\\segmented"));
            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (string file in files)
            {
                using (TextReader reader = new StreamReader(file))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!line.StartsWith("<"))
                            foreach (var item in line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                if (char.IsLetterOrDigit(item[0]))
                                {
                                    foreach (var Formatted in FormatWord(item))
                                    {
                                        if (dict.ContainsKey(Formatted))
                                            dict[Formatted] += 1;
                                        else
                                            dict.Add(Formatted, 1);
                                    }
                                }
                            }
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (var item in dict)
            {
                sb.AppendLine($"{item.Key}:{item.Value}");
            }

            using (TextWriter writer = new StreamWriter("result.txt"))
            {
                writer.Write(sb.ToString());
            }
        }

        static string[] FormatWord(string para)
        {
            if (para.Length <= 0)
                return new string[] { "" };
            else if (para.Length == 1)
                return new string[] { para + "_S" };

            List<string> Formatted = new List<string>();
            Formatted.Add(para[0] + "_B");
            for (int i = 1; i < para.Length - 1; i++)
                Formatted.Add(para[i] + "_M");
            Formatted.Add(para[para.Length - 1] + "_E");

            return Formatted.ToArray();
        }

    }
}
