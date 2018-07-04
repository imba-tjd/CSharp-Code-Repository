using System;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using IMBA_TJD.TrieTree;
using System.Collections.Concurrent;

namespace Segmentor
{
    class Program
    {
        static void Main()
        {
            Segmentor core = new Segmentor();

            while (true)
            {
                Console.Write("Input text to segment:");
                string text = Console.ReadLine();
                if (text == null)
                    break;

                Console.WriteLine("Prefix Match:");
                foreach (var item in core.PrefixResolve(text))
                {
                    Console.Write(item);
                    Console.Write(" ");
                }
                Console.WriteLine();

                Console.WriteLine("Suffix Match:");
                foreach (var item in core.SuffixResolve(text))
                {
                    Console.Write(item);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }

    class Segmentor
    {
        readonly string dicFile;
        TrieTree tree;
        Task buildTask;
        ConcurrentBag<string> entries; // 不能用HashSet，线程不安全
        List<string> sentences;

        public Segmentor(string dicFile = "./dic.txt")
        {
            this.dicFile = dicFile;
            buildTask = BuildAsync();
        }
        async Task BuildAsync() => await Task.Run(() =>
        {
            BuildEntries();
            BuildTrieTree();
        });
        void BuildEntries()
        {
            if (!File.Exists(dicFile))
                throw new FileNotFoundException("Dictionary file not found!", dicFile);

            entries = new ConcurrentBag<string>();

            string dicFileContent;
            using (TextReader reader = new StreamReader(dicFile))
            {
                dicFileContent = reader.ReadToEnd();
            }

            Parallel.ForEach(dicFileContent.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries), // 这一步就有可能分出空的，那下一步的索引就会越界
                (line) => entries.Add(line.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[0]) // 相同词语的会算存两份，但建树的时候没关系，相同的不会重复插入
            );
        }
        void BuildTrieTree() => tree = new TrieTree(entries);
        void BuildSentences(string text) => sentences = new List<string>(text.Split(new char[] { '.', '。' }, StringSplitOptions.RemoveEmptyEntries));
        public IEnumerable<string> PrefixResolve(string text)
        {
            if (!buildTask.IsCompleted)
                buildTask.Wait();
            BuildSentences(text);

            List<string> pieces = new List<string>();
            foreach (var sentence in sentences) // 对每个句子进行处理（以句号分隔）
            {
                int startIndex = 0;
                while (startIndex < sentence.Length)
                {
                    int nextEndMatch = tree.PrefixMatch(sentence.Substring(startIndex)); // 直接取得正向最大匹配的位置
                    if (nextEndMatch != 0)
                    {
                        pieces.Add(sentence.Substring(startIndex, nextEndMatch));
                        startIndex += nextEndMatch; // 子字符串起始位置向后移动
                    }
                    else // 树中没有匹配的词
                        pieces.Add(sentence[startIndex++].ToString()); // 子字符串起始位置向后移动
                }
            }
            // pieces.Add("。");

            return pieces;
        }
        public IEnumerable<string> SuffixResolve(string text)
        {
            if (!buildTask.IsCompleted)
                buildTask.Wait();
            BuildSentences(text);

            List<string> pieces = new List<string>();
            // pieces.Add("。");
            foreach (var sentence in sentences)
            {
                int endIndex = sentence.Length; // 两个变化方向：减减和变为startIndex的值，所以不能用for
                while (endIndex > 0) // 反向最大匹配
                {
                    int startIndex = 0; // 每次子字符串都从最前面开始匹配
                    while (startIndex < endIndex) // 确定endIndex下次移动的位置
                    {
                        string unMatched = sentence.Substring(startIndex, endIndex - startIndex);
                        if (tree.FullMatch(unMatched))
                        {
                            pieces.Add(unMatched);
                            endIndex = startIndex;
                            goto circle; // 位置确定完毕
                        }
                        else
                            startIndex++; // 子字符串起始位置向后移动
                    }

                    // 树中没有匹配的词
                    pieces.Add(sentence[--endIndex].ToString()); // 位置确定完毕

                    circle:;
                }
            }

            pieces.Reverse();
            return pieces;
        }

    }
}
