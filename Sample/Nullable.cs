// 《Learning Hard 学习笔记》代码
// http://www.ituring.com.cn/book/1604

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _12._1
{
    class Program
    {
        static void Main(string[] args)
        {
            // 下面代码也可以这样子定义int? value=1;
            Nullable<int> value = 1;

            Console.WriteLine("可空类型有值的输出情况：");
            Display(value);
            Console.WriteLine();
            Console.WriteLine();

            value = new Nullable<int>();
            Console.WriteLine("可空类型没有值的输出情况：");
            Display(value);
            Console.Read();
        }

        // 输出方法，演示可空类型中的方法和属性的使用
        private static void Display(int? nullable)
        {
            // HasValue 属性代表指示可空对象是否有值
            Console.WriteLine("可空类型是否有值：{0}", nullable.HasValue);
            if (nullable.HasValue)
            {
                Console.WriteLine("值为: {0}", nullable.Value);
            }

            // GetValueOrDefault代表如果可空对象有值,就用它的值返回,如果可空对象不包含值,返回GetValueOrDefault方法的参数值，默认值为0。
            Console.WriteLine("GetValueorDefault():{0}", nullable.GetValueOrDefault());

            // GetValueOrDefault(T)方法代表如果 HasValue 属性为 true，则为 Value 属性的值；否则为 ,即2。
            Console.WriteLine("GetValueorDefalut重载方法使用：{0}", nullable.GetValueOrDefault(2));

            // GetHashCode()代表如果 HasValue 属性为 true，则为 Value 属性返回的对象的哈希代码；如果 HasValue 属性为 false，则为零
            Console.WriteLine("GetHashCode()方法的使用：{0}", nullable.GetHashCode());
        }
    }
}

// 空合并运算符
namespace _12._2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("??运算符的使用如下：");
            NullcoalescingOperator();
            Console.Read();
        }
        // ??运算符的演示
        private static void NullcoalescingOperator()
        {
            int? nullable = null;
            int? nullhasvalue = 1;

            // ??和三目运算符的功能差不多的
            // 所以下面代码等价于：
            // x=nullable.HasValue?nullable.Value:12;
            int x = nullable ?? 12;

            // 此时nullhasvalue不能null,所以y的值为nullhasvalue.Value,即输出1
            int y = nullhasvalue ?? 123;
            Console.WriteLine("可空类型没有值的情况：{0}", x);
            Console.WriteLine("可空类型有值的情况：{0}", y);

            // 同时??运算符也可以用于引用类型， 下面是引用类型的例子
            Console.WriteLine();
            string stringnotnull = "123";
            string stringisnull = null;

            // 下面的代码等价于：
            // (stringnotnull ==null)? "456" :stringnotnull
            // 也也等价于：
            // if(stringnotnull==null)
            // {
            //      return "456";
            // }
            // else
            // {
            //      return stringnotnull;
            // }

            // 从上面的等价代码可以看出，有了??运算符之后可以省略大量的if—else语句，这样代码少了， 自然可读性就高了
            string result = stringnotnull ?? "456";
            string result2 = stringisnull ?? "12";
            Console.WriteLine("引用类型不为null的情况：{0}", result);
            Console.WriteLine("引用类型为null的情况：{0}", result2);
        }
    }
}

// 可空类型的装箱和拆箱
namespace _12._3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("可空类型的装箱和拆箱的使用如下：");
            BoxedandUnboxed();
            Console.Read();
        }

        // 可空类型装箱和拆箱的演示
        private static void BoxedandUnboxed()
        {
            // 定义一个可空类型对象nullable
            Nullable<int> nullable = 5;
            int? nullablewithoutvalue = null;

            Console.Write(nullablewithoutvalue.Value);
            // 获得可空对象的类型，此时返回的是System.Int32,而不是System.Nullable<System.Int32>,这点大家要特别注意下的
            Console.WriteLine("获取不为null的可空类型的类型为：{0}", nullable.GetType());

            // 对于一个为null的类型调用方法时出现异常，所以一般对于引用类型的调用方法前，最好养成习惯先检测下它是否为null
            //Console.WriteLine("获取为null的可空类型的类型为：{0}", nullablewithoutvalue.GetType());

            // 将可空类型对象赋给引用类型obj,此时会发生装箱操作，大家可以通过IL中的boxed 来证明
            object obj = nullable;

            // 获得装箱后引用类型的类型，此时输出的仍然是System.Int32,而不是System.Nullable<System.Int32>
            Console.WriteLine("获得装箱后obj 的类型：{0}", obj.GetType());

            // 拆箱成非可空变量
            int value = (int)obj;
            Console.WriteLine("拆箱成非可空变量的情况为：{0}", value);

            // 拆箱成可空变量
            nullable = (int?)obj;
            Console.WriteLine("拆箱成可空变量的情况为：{0}", nullable);

            // 装箱一个没有值的可空类型的对象
            obj = nullablewithoutvalue;
            Console.WriteLine("对null的可空类型装箱后obj 是否为null：{0}", obj == null);

            // 拆箱成非可空变量,此时会抛出NullReferenceException异常,因为没有值的可空类型装箱后obj等于null,引用一个空地址
            // 相当于拆箱后把null值赋给一个int 类型的变量,此时当然就会出现错误了
            //value = (int)obj;
            //Console.WriteLine("一个没有值的可空类型装箱后，拆箱成非可空变量的情况为：{0}", value);

            // 拆箱成可空变量
            nullable = (int?)obj;
            Console.WriteLine("一个没有值的可空类型装箱后，拆箱成可空变量是否为null：{0}", nullable == null);
        }
    }
}