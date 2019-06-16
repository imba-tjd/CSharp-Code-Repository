// 用来检查v2rayN的所有Forks是否有备份；只会检查默认分支
using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System.Net;

class Test
{
    static List<string> GetForks(HttpClient client)
    {
        const string network = "https://github.com/2dust/v2rayN/network/members";
        string content = client.GetStringAsync(network).GetAwaiter().GetResult();

        var matches = Regex.Matches(content, "href=\"(/.+/v2rayN(-1)?)\"");

        var list = new List<string>();
        foreach (Match item in matches)
            list.Add("https://github.com" + item.Groups[1].Value);
        return list;
    }

    static List<string> GetNotEven(HttpClient client, List<string> forks)
    {
        List<string> list = new List<string>();
        forks.AsParallel().ForAll(url =>
        {
            Console.WriteLine("Loading.");
            string content = client.GetStringAsync(url).GetAwaiter().GetResult();
            if (!content.Contains("This branch is even with"))
                list.Add(url);
        });
        return list;
    }

    static void Main()
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        var client = new HttpClient();
        var forks = GetForks(client);
        var notEven = GetNotEven(client, forks);
        client.Dispose();
        File.WriteAllLines("Result.txt", notEven);
    }
}
