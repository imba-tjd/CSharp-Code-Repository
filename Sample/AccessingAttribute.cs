// Illustrated C# 2012
// 对于特性，也可以使用Type对象来获取类型信息。
// IsDefined方法检测某个特性是否应用到了某个类上，第二个参数指示是否搜索类的继承树来查找这个特性。
// GetCustomAttributes方法返回应用到结构的特性的数组。每一个与目标相关联的特性的实例会被创建。非泛型版本返回的是object，需要进行转换。

using System;

namespace AccessingAttribute
{
    [AttributeUsage(AttributeTargets.Class)]
    sealed class MyAttributeAttribute : Attribute { public string Version { get; set; } }

    [MyAttribute(Version = "1.0")]
    class MyClass { }

    class Program
    {
        static void Main()
        {
            MyClass mc = new MyClass();
            Type t = mc.GetType();
            bool isDefined = t.IsDefined(typeof(MyAttributeAttribute), false);
            if (isDefined)
                Console.WriteLine($"MyAttribute is applied to {t.Name}");

            object[] AttArr = t.GetCustomAttributes(false);
            foreach (Attribute a in AttArr)
                if (a is MyAttributeAttribute attr)
                    Console.WriteLine("Version: " + attr.Version);

            Console.ReadKey();
        }
    }
}