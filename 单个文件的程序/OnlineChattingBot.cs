using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;

class OnlineChattingBot
{
    const string api = "http://api.qingyunke.com/api.php?key=free&appid=0&msg=";
    static async Task Main()
    {
        Console.WriteLine("你好！");
        string input;
        while ((input = Console.ReadLine().Trim()) != "再见")
        {
            string encoding = WebUtility.HtmlDecode(input);
            Task<string> request = new HttpClient().GetStringAsync(api + encoding);
            try
            {
                string response = await request;
                string text = response.Substring(23, response.LastIndexOf('\"') - 23);
                Console.WriteLine(text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
                Console.WriteLine("Oops... Some thing's wrong, try again.");
            }
        }
        Console.WriteLine("再见！");
    }
}
