// 豆瓣Top250爬虫；本来单纯的下载用HttpClient就够了，但XML与HTML不兼容，自己尝试写又失败了，只好用包了
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace DBScraper
{
    class Item
    {
        internal string Name;
        internal string Star;
        internal string Quote;
        internal string Link;
        public override string ToString() =>
            $"{Name},{Star},{Quote},{Link}";
    }

    class Program
    {
        const string BaseURL = "https://movie.douban.com/top250?start=";
        static void Main(string[] args)
        {
            using (TextWriter tw = new StreamWriter("top250.csv", false, System.Text.Encoding.UTF8))
            {
                tw.WriteLine("Name,Star,Quote,Link");

                foreach (var item in Loop(BaseURL))
                    tw.WriteLine(item);
            }
        }

        // 无法用 await Task<IEnumerable<string>> 因为这是C# 8的特性
        static IEnumerable<string> Loop(string baseurl)
        {
            for (int i = 0; i < 250; i += 25)
            {
                var doc = new HtmlWeb().Load(baseurl + i);
                var items = GetItems(doc);

                foreach (var content in items)
                    yield return content.ToString();

                System.Threading.Thread.CurrentThread.Join(200);
            }
        }

        static IEnumerable<Item> GetItems(HtmlDocument doc)
        {
            var ol = doc.DocumentNode.Descendants("ol").Single();
            var item = ol.ChildNodes.Where(e => e.Name == "li").Select(f => f.ChildNodes.ElementAt(1));

            // 有可能没有class属性，所以需要用?.
            // 216号电影《何以为家》没有Quote，而HtmlNode是引用类型，Default是null
            return item.Select(it => new Item()
            {
                Link = it.Descendants("a").First().Attributes["href"].Value,
                Name = it.Descendants("span").First().InnerText,
                Star = it.Descendants("span").Where(s => s.Attributes["class"]?.Value == "rating_num").Single().InnerText,
                Quote = it.Descendants("span").Where(s => s.Attributes["class"]?.Value == "inq").FirstOrDefault()?.InnerText
            });
        }
    }
}
