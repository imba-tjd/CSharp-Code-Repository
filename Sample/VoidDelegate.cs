// 《Learning Hard 学习笔记》代码
// http://www.ituring.com.cn/book/1604

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoteDelegate
{
    class Program
    {
        // 定义投票委托
        delegate void VoteDelegate(string name);
        static void Main(string[] args)
        {
            // 使用Vote方法来实例化委托对象
            //VoteDelegate votedelegate = new VoteDelegate(new Friend().Vote);
            // 下面方式为隐式实例化委托对象方式，把方法直接赋给委托对象
            VoteDelegate votedelegate = new Friend().Vote;

            // 通过调用托来回调Vote()方法，此为隐式调用方式
            votedelegate("SomeBody");

            // 使用匿名方法来实例化委托对象
            VoteDelegate votedelegate = delegate(string nickname)
            {
                Console.WriteLine("昵称为：{0} 来帮Learning Hard投票了", nickname);
            };
            votedelegate("SomeBody");

            // 定义闭包委托
            delegate void ClosureDelegate();

            ClosureDelegate test = CreateDelegateInstance();
            // 此时会回调匿名方法输出count的值
            test();

            Console.Read();
        }

        public class Friend
        {
            // 朋友的投票方法
            public void Vote(string nickname)
            {
                Console.WriteLine("昵称为：{0} 来帮Learning Hard投票了", nickname);
            }
        }

        // 闭包方法
        private static void closureMethod()
        {
            // outVariable和count对于匿名方法而言都是外部变量
            string outVariable = "外部变量";
            int count = 1;

            //  而capturedVariable是被匿名方法捕获的外部变量
            string capturedVariable = "被捕获的外部变量";
            ClosureDelegate closuredelegate = delegate
            {
                // localvariable是匿名方法中局部变量
                string localvariable = "匿名方法局部变量";

                // 引用capturedVariable变量
                Console.WriteLine(capturedVariable + " " + localvariable);

                Console.WriteLine(count);
                // 捕捉了外部变量
                count++;
            };

            // 调用委托
            closuredelegate();

            // 延长了变量的生命周期
            Console.WriteLine(++count);
        }
    }
}
