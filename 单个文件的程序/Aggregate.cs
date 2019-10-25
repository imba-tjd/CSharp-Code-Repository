// Aggregate的一些代码
using System;
using System.Linq;
using System.Collections.Generic;

// 然而需要输入有多少列才能一行完成（Range的第二个参数）
class ReverseSquare
{
    public static void Run()
    {
        // string s = "1,2,3\n4,5,6\n7,8,9";
        string s = "1,2,3,0\n4,5,6,0\n7,8,9,0";
        var lines = s.Split('\n').Select(line => line.Split(',')).ToList(); // [[1,2,3,0],[4,5,6,0],[7,8,9,0]]
        var data = lines.Aggregate(Enumerable.Range(0, 4).Select(x => Enumerable.Empty<string>()), // 生成待累加的4列
            (cols, line) => cols.Zip(line, (col, word) => col.Append(word))).ToList(); // 总列数与一行的项数相等，每次循环取一列与一行中的一项拼接
        foreach (var item in data)
            Console.WriteLine(string.Join(", ", item));
    }
}

// https://bbs.csdn.net/topics/392059014
class 笛卡尔积
{
    static IEnumerable<IEnumerable<T>> CartesianProduct<T>(IEnumerable<IEnumerable<T>> sequences)
    {
        return sequences.Aggregate(
          (new[] { Enumerable.Empty<T>() }).AsEnumerable(),
          (accumulator, sequence) =>
            from accseq in accumulator
            from item in sequence
            select accseq.Append(item));
    }
    public static void Run()
    {
        int count = 0;
        // 总共4位，每位都是1-3这三种选择
        foreach (var item in CartesianProduct(Enumerable.Repeat(Enumerable.Range(1, 3), 4)))
        {
            Console.WriteLine(string.Join(", ", item));
            count++;
        }
        Console.WriteLine(count);
    }
}

class Aggregate
{
    static void Main()
    {
        ReverseSquare.Run();
        Console.WriteLine();
        笛卡尔积.Run();
    }
}
