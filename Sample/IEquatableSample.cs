// 《C# in Depth》
using System;

namespace CSharpInDepth3_7
{
    // public sealed class Pair<T1, T2> : IEquatable<Pair<T1, T2>>
    public sealed class Pair<T1, T2> : IEquatable<Pair<T1, T2>> where T1 : IEquatable<T1> where T2 : IEquatable<T2>
    {
        // static readonly IEqualityComparer<T1> FirstComparer = EqualityComparer<T1>.Default;
        // static readonly IEqualityComparer<T2> SecondComparer = EqualityComparer<T2>.Default;

        T1 First { get; }
        T2 Second { get; }
        public Pair(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }

        // public bool Equals(Pair<T1, T2> other)
        //     => other != null && FirstComparer.Equals(First, other.First) && SecondComparer.Equals(Second, other.Second);
        public bool Equals(Pair<T1, T2> other)
            => other != null && First.Equals(other.First) && Second.Equals(other.Second);

        public override bool Equals(object obj) => Equals(obj as Pair<T1, T2>);
        public override int GetHashCode() => First.GetHashCode() * 37 + Second.GetHashCode(); // 作者说比异或要好
    }

    public static class Pair
    {
        // public static Pair<T1, T2> Of<T1, T2>(T1 first, T2 second) => new Pair<T1, T2>(first, second);
        public static Pair<T1, T2> Of<T1, T2>(T1 first, T2 second) where T1 : IEquatable<T1> where T2 : IEquatable<T2> => new Pair<T1, T2>(first, second);
    }
    class Program
    {
        static void Main()
        {
            Pair<int, string> pair1 = new Pair<int, string>(10, "value");
            var pair2 = Pair.Of(10, "value"); // 使用在非泛型类中的泛型方法进行自动推断
            Console.WriteLine(pair1.Equals(pair2));
        }
    }
}
