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
                                if (!ToRemove(item))
                                    if (dict.ContainsKey(item))
                                        dict[item] += 1;
                                    else
                                        dict.Add(item, 1);
                            }
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (var item in dict)
            {
                sb.AppendLine($"{item.Key}: {item.Value}");
            }

            using (TextWriter writer = new StreamWriter("dic.txt"))
            {
                writer.Write(sb.ToString());
            }
        }

        static bool ToRemove(string item)
        {
            HashSet<string> list = new HashSet<string>()
            {
                "，",
                "。",
                "“",
                "”",
                "（",
                "）",
                "、",
                "?",
                "？",
                ".",
                ",",
                "`",
                "*",
                "!",
                "！",
                "；",
                ";"
            };

            if (list.Contains(item))
                return true;
            else
                return false;
        }
    }
}
