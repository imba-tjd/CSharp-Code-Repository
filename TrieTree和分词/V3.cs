using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Segmentor
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser("./segmented");
            parser.Run();
            Console.ReadKey();
        }
    }

    class Parser
    {
        string path;
        PathType pathType;
        bool recursive = false;
        List<string> files;
        public Parser(string path)
        {
            if (path is null)
                throw new ArgumentNullException(path);

            this.path = path;

            if (File.Exists(path))
            {
                this.pathType = PathType.File;
                files = new List<string>() { path };
            }
            else if (Directory.Exists(path))
            {
                this.pathType = PathType.Directory;
                Console.WriteLine("Reading files...");
                files = new List<string>(Directory.GetFiles(path, "*.seg", recursive == true ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly));
            }
            else
                throw new ArgumentException("The path is not exist.", path);
        }

        public Parser(string path, bool recursive) : this(path) => this.recursive = recursive;

        enum PathType
        {
            File,
            Directory
        }

        public void Run()
        {
            List<Task> tasks = new List<Task>();

            Parallel.ForEach(files, (file) =>
                tasks.Add(Task.Run(() => new Core(file).ParseFile()))
            );

            Task.WhenAll(tasks);
            Console.WriteLine($"Finished, total {files.Count} files in {path}.");
        }
    }

    // 对单个文件进行处理
    class Core
    {
        string file;

        public Core(string file)
        {
            if (!File.Exists(file))
                throw new FileNotFoundException("File not exist.", file);

            this.file = file;
        }

        // 进行词性划分
        public void ParseFile()
        {
            StringBuilder sb = new StringBuilder();

            using (TextReader reader = new StreamReader(file))
            {
                string line;

                Console.WriteLine("Reading: " + file);
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("<") | line == string.Empty)
                    {
                        //sb.AppendLine(line);
                        continue;
                    }

                    sb.AppendJoin(' ',
                    line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(
                        (x) => new Word(x).ToString())
                    );
                    sb.Append(Environment.NewLine);
                }
            }

            Console.WriteLine("Writing: " + file);
            using (TextWriter writer = new StreamWriter(file))
            {
                writer.Write(sb.ToString());
            }
        }

        // 对词语进行处理
        class Word
        {
            WordType type;
            string word;
            public Word(string word)
            {
                this.word = word;
                this.type = GenerateType(word);
            }
            override public string ToString()
            {
                string formatted = "";
                switch (type)
                {
                    case WordType.Empty: formatted = " "; break;
                    case WordType.Punctuation: formatted = word + "/P"; break;
                    case WordType.Single: formatted = word + "/S"; break;
                    case WordType.Double: formatted = word[0] + "/B" + word[1] + "/E"; break;
                    case WordType.TripleAndMore: formatted = FormatTripleAndMore(word); break;
                }
                return formatted;
            }
            string FormatTripleAndMore(string word)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(word[0] + "/B");
                for (int i = 1; i < word.Length - 1; i++)
                    sb.Append(word[i] + "/M");
                sb.Append(word[word.Length - 1] + "/E");

                return sb.ToString();
            }
            WordType GenerateType(string word)
            {
                WordType type = WordType.Empty;
                switch (word.Length)
                {
                    case 0: break;
                    case 1: type = char.IsPunctuation(word[0]) ? WordType.Punctuation : WordType.Single; break;
                    case 2: type = WordType.Double; break;
                    case 3: type = WordType.TripleAndMore; break;
                }
                return type;
            }
        }

        enum WordType
        {
            Empty,
            Punctuation,
            Single,
            Double,
            TripleAndMore
        }
    }
}
