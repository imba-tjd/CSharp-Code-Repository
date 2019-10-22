// 然而需要输入有多少列才能一行完成（Range的第二个参数）
using System.Linq;

class ReverseSquare
{
    static void Main()
    {
        string s = "1,2,3\n4,5,6\n7,8,9";
        var lines = s.Split('\n').Select(line => line.Split(',')).ToList();
        var data = lines.Aggregate(Enumerable.Range(0, 3).Select(x=>Enumerable.Empty<string>()),
            (cols, line) => cols.Zip(line, (col, word) => col.Append(word))).ToList();
        foreach (var item in data.Select(x => string.Join(", ", x)))
            System.Console.WriteLine(item);
    }
}
