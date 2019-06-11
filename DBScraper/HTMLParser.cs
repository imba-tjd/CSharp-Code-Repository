// 试图写一个HTML的解析器，但是失败了。不能用LL(1)的解法，见64行。
using System;
using System.Collections.Generic;
using System.Text;

class HTMLParser
{
    // HTMLItem Parse(Uri uri);
    const string HTMLIdentifier = "<!DOCTYPE html>";
    readonly HTMLContext con;
    private HTMLParser(HTMLContext context) => con = context;
    public static HTMLItem Parse(string content)
    {
        HTMLContext con = new HTMLContext(content);
        con.Trim();
        if (!con.StartsWith(HTMLIdentifier))
            throw new NotSupportedException("Content is not HTML5!");
        con.Next(HTMLIdentifier);
        return new HTMLParser(con).Parse();
    }
    HTMLItem Parse()
    {
        HTMLItem item = new HTMLItem()
        {
            Name = ParseName(),
            Properties = ParseProperties(),
        };
        item.Children = ParseChildren(item.Name);
        con.Trim();
        if (item.Children != null)
            con.Next($"</{item.Name}>");
        return item;
    }
    string ParseName()
    {
        con.Trim();
        if (con.Peek != '<') // Text
            return null;
        con.Next('<');
        if (con.PeekIsWS)
            throw new NotSupportedException("Invalid HTML Name.");
        return con.GetToken();
    }
    Dictionary<string, string> ParseProperties() // 错的，不能按空格分隔，因为空格可能在字符串里
    {
        var dic = new Dictionary<string, string>();
        con.Trim();
        while (!(con.StartsWith("/>") || con.PeekIsAngle))
        {
            string pro = con.GetToken();
            var pair = pro.Split('=');
            if (pair.Length != 2 ||
                pair[0][0] == '\"' ||
                pair[1][0] != '\"' || pair[1][pair[1].Length - 1] != '\"')
                throw new NotSupportedException("HTML Property Error.");
            dic.Add(pair[0], pair[1].Substring(1, pair[1].Length - 2));
            con.Trim();
        }
        if (con.Peek == '<')
            throw new NotSupportedException("HTML Property Error.");
        return dic.Count == 0 ? null : dic;
    }

    // <div>
    //     <img>
    //     asdf
    //     <img>
    // </div>
    List<HTMLItem> ParseChildren(string outer) // 写不下去了
    {
        List<HTMLItem> children = new List<HTMLItem>();
        con.Trim();
        while (true)
        {
            if (con.Peek == '<')
                break;
        }
        while (con.Peek == '<' && !con.StartsWith($"</{outer}>"))
            children.Add(Parse());
        con.Next($"</{outer}>");
        return children.Count == 0 ? null : children;
    }
    string ParseText()
    {
        con.Trim();
        if (con.PeekIsAngle)
            throw new NotSupportedException("HTML Error.");
        int count = 0;
        while (!con.PeekIsAngle)
            count++;
        string text = con.GetToken(count);
        con.Next(text);
        return text;
    }
}

class HTMLContext
{
    readonly string Content;
    int Position;
    public char Peek => Content[Position];
    public bool PeekIsWS => Peek == ' ' || Peek == '\n';
    public bool PeekIsAngle => Peek == '<' || Peek == '>';
    int RestCount => Content.Length - Position;
    public HTMLContext(string HTMLContent) => Content = HTMLContent;
    public void Trim()
    {
        TrimWS();
        TrimComment();
        TrimWS();
    }
    void TrimWS()
    {
        while (PeekIsWS)
            Position++;
    }
    void TrimComment()
    {
        if (StartsWith("<!--"))
        {
            while (!StartsWith("-->"))
                Position++;
            Position += 3;
        }
    }
    public void Next(int count) => Position += count;
    public void Next(string s) => Position += s.Length;
    public string GetToken()
    {
        Trim();
        int count = 0;
        while (!PeekIsWS && !PeekIsAngle)
            count++;
        return GetToken(count);
    }
    public string GetToken(int count) => Content.Substring(Position, count);
    public bool StartsWith(char c) => Peek == c;
    public bool StartsWith(string s)
    {
        if (s.Length > RestCount)
            return false;
        for (int i = 0; i < s.Length; i++)
            if (Content[i] != s[i])
                return false;
        return true;
    }
}

class HTMLItem
{
    public string Name;
    public Dictionary<string, string> Properties;
    public List<HTMLItem> Children;
    public string Text;
    public override string ToString()
    {
        if (Name == null)
            return Text;
        StringBuilder sb = new StringBuilder();
        sb.Append("<" + Name);
        if (Properties != null)
            foreach (var prop in Properties)
                sb.Append($" {prop.Key}=\"{prop.Value}\"");
        if (Children == null)
        {
            sb.Append("/>");
            return sb.ToString();
        }
        sb.Append(">");
        sb.AppendJoin(Environment.NewLine, ChildrenToString(Children));
        sb.Append($"</{Name}>");
        return sb.ToString();
    }
    IEnumerable<string> ChildrenToString(List<HTMLItem> children)
    {
        foreach (var child in children)
            yield return child.ToString();
    }
}
