// 《Learning Hard 学习笔记》代码
// http://www.ituring.com.cn/book/1604

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// 隐式实现接口
namespace UseInterface
{
    interface ICustomCompare
    {
        // 定义比较方法,继承该接口的类都要实现该方法
        int CompareTo(object other);
    }

    // 定义类继承接口
    public class Person : ICustomCompare
    {
        // 类字段
        int age;

        // 类属性
        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        // 实现接口方法
        public int CompareTo(object value)
        {
            // 先判断参数对象是否为null
            if (value == null)
                return 1;

            // 将object类型强制转换为Person类型
            Person otherp = (Person) value;

            // 把当前对象的Age属性与需要比较对象的Age属性对比
            if (this.Age < otherp.Age)
                return -1;
            if (this.Age > otherp.Age)
                return 1;
            return 0;
        }
    }

    // 接口中方法的调用
    class Program
    {
        static void Main(string[] args)
        {
            // 创建两个Person对象
            Person p1 = new Person();
            p1.Age = 18;
            Person p2 = new Person();
            p2.Age = 19;

            // 调用接口中方法把p1与p2比较
            ICustomCompare iCustomCompare = (ICustomCompare) p1;
            if (iCustomCompare.CompareTo(p2) > 0)
                Console.WriteLine("p1比p2大");
            else if (iCustomCompare.CompareTo(p2) < 0)
                Console.WriteLine("p1比p2小");
            else
                Console.WriteLine("p1和p2一样大");
            Console.Read();
        }
    }
}

// 显式实现接口
namespace InterfaceExplicitImp
{
    // 中国人打招呼接口
    interface IChineseGreeting
    {
        // 接口方法声明
        void SayHello();
    }

    // 美国人打招呼接口
    interface IAmericanGreeting
    {
        // 接口方法声明
        void SayHello();
    }

    // Speaker类实现了两个接口
    public class Speaker : IChineseGreeting, IAmericanGreeting
    {
        // 不能有访问修饰符修饰，因为显式实现的接口，成员默认为私有的
        void IChineseGreeting.SayHello()
        {
            Console.WriteLine("你好");
        }

        void IAmericanGreeting.SayHello()
        {
            Console.WriteLine("Hello");
        }
    }

    class Program
    {
        // 显式接口实现演示
        static void Main(string[] args)
        {
            // 初始化类实例
            Speaker speaker = new Speaker();

            // 调用中国人打招呼方法
            // 显式转化为IChineseGreeting接口来调用SayHello方法。
            IChineseGreeting iChineseG = (IChineseGreeting) speaker;
            iChineseG.SayHello();

            // 调用美国人打招呼方法
            // 显式转化为IAmericanGreeting接口来调用SayHello方法。
            IAmericanGreeting iAmericanG = (IAmericanGreeting) speaker;
            iAmericanG.SayHello();
            Console.Read();
        }
    }
}