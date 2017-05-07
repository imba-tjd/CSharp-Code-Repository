// 《Learning Hard 学习笔记》代码
// http://www.ituring.com.cn/book/1604

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _11._3_1
{
    // 声明开放泛型类型
    public class DictionaryStringKey<T> : Dictionary<string, T>
    {
    }
    class Program
    {
        static void Main(string[] args)
        {
            //  Dictionary<,>是一个开放类型，它有2个类型参数
            Type t = typeof(Dictionary<,>);
            Console.WriteLine("是否为开放类型: "+t.ContainsGenericParameters);
            // DictionaryStringKey<>也是一个开放类型，但它有1个类型参数
            t = typeof(DictionaryStringKey<>);
            Console.WriteLine("是否为开放类型: " + t.ContainsGenericParameters);
            // DictionaryStringKey<int>是一个封闭类型
            t = typeof(DictionaryStringKey<int>);
            Console.WriteLine("是否为开放类型: " + t.ContainsGenericParameters);
            // Dictionary<int, int>是封闭类型
            t = typeof(Dictionary<int, int>);
            Console.WriteLine("是否为开放类型: " + t.ContainsGenericParameters);
            Console.Read();
        }
    }
}

namespace _11._3_4
{
    class Program
    {
        static void Main(string[] args)
        {
            int n1 = 1;
            int n2 = 2;
            // 不使用类型推断的代码
            //genericMethod<int>(ref n1, ref n2);
            genericMethod(ref n1, ref n2);
            Console.WriteLine("n1的值现在为：" + n1);
            Console.WriteLine("n2的值现在为：" + n2);

            string str1 = "123";
            object obj = "456";
            // 使用类型推断出现编译错误
            //genericMethod(ref str1, ref obj);
            Console.Read();
        }

        // 泛型方法
        private static void genericMethod<T>(ref T t1, ref T t2)
        {
            T temp = t1;
            t1 = t2;
            t2 = temp;
        }
    }

    public class Test<T>
    {

    }
}
