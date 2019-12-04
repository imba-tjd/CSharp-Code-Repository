using System;
using System.IO;
using System.Collections.Generic;

class PacBuilder
{
    const string BLACKLISTPATH = "blacklist.txt";
    const string WHITELISTPATH = "whitelist.txt";
    const string FINDPROXY = @"
var proxy = '__PROXY__';
var direct = 'DIRECT;';
var hop = Object.hasOwnProperty;

function FindProxyForURL(url, host) {
    if(hop.call(whitelist, host))
        return direct;

    var suffix;
    var pos = host.lastIndexOf('.');
    pos = host.lastIndexOf('.', pos - 1);
    while(1) {
        if (pos <= 0) {
            if (hop.call(blacklist, host))
                return proxy;
            else
                return direct;
        }
        suffix = host.substring(pos + 1);
        if (hop.call(blacklist, suffix))
            return proxy;
        pos = host.lastIndexOf('.', pos - 1);
    }
}";
    const string FINDPROXYMINIFY = "var proxy='__PROXY__',direct='DIRECT;',hop=Object.hasOwnProperty;function FindProxyForURL(r,t){if(hop.call(whitelist,t))return direct;var l,i=t.lastIndexOf('.');for(i=t.lastIndexOf('.',i-1);;){if(i<=0)return hop.call(blacklist,t)?proxy:direct;if(l=t.substring(i+1),hop.call(blacklist,l))return proxy;i=t.lastIndexOf('.',i-1)}}";

    static void Main()
    {
        try
        {
            string[] userBlackList = File.ReadAllLines(BLACKLISTPATH);
            string[] userWhiteList = File.ReadAllLines(WHITELISTPATH);
            string blackList = "blacklist = " + GetStringiObj(userBlackList);
            string whiteList = "whitelist = " + GetStringiObj(userWhiteList);
            if (File.Exists("pac.txt"))
                File.Copy("pac.txt", "pac.txt.bak", true);
            File.WriteAllText("pac.txt", blackList + whiteList + FINDPROXYMINIFY);
        }
        catch (Exception ex)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex);
            Console.ForegroundColor = color;
            Console.ReadKey();
        }
    }

    static string GetStringiObj(IEnumerable<string> list) =>
        "JSON.parse('{" + string.Join(",", ObjItemfy(list)) + "}');\n";
    static string ObjItemfy(string str) =>
        string.Format("\"{0}\":null", str.IndexOf("//") is var i && i != -1 ? str.Substring(0, i).TrimEnd() : str);
    static IEnumerable<string> ObjItemfy(IEnumerable<string> strs)
    {
        foreach (var str in strs)
            if (str.Trim() is var s && s != string.Empty && !s.StartsWith("//"))
                yield return ObjItemfy(s);
    }
}
