// 《Learning Hard 学习笔记》代码
// http://www.ituring.com.cn/book/1604

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LambdaExpression1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Lambda表达式的演变过程
            // 下面是C# 1中创建委托实例的代码
            Func<string, int> delegatetest1 = new Func<string, int>(Callbackmethod);

            // C# 2中用匿名方法来创建委托实例，此时就不需要额外定义回调方法Callbackmethod
            Func<string, int> delegatetest2 = delegate(string text)
            {
                return text.Length;
            };
            // C# 3中使用Lambda表达式来创建委托实例
            Func<string, int> delegatetest3 = (string text) => text.Length;

            // 可以省略参数类型string,把上面代码再简化为：
            Func<string, int> delegatetest4 = (text) => text.Length;

            // 此时可以把圆括号也省略,简化为：
            Func<string, int> delegatetest = text => text.Length;

            // 调用委托
            Console.WriteLine("使用Lambda表达式返回字符串的长度为： " + delegatetest("learning hard"));
            Console.Read();
        }

        // 回调方法
        private static int Callbackmethod(string text)
        {
            return text.Length;
        }
    }
}

namespace LambdaExpression2
{
    class Program
    {
        static void Main(string[] args)
        {
            // 新建一个button实例
            Button button1 = new Button() {Text = "点击我"};

            // C# 2中使用匿名方法来订阅事件
            button1.Click += delegate(object sender, EventArgs e)
            {
                ReportEvent("Click事件", sender, e);
            };
            button1.KeyPress += delegate(object sender, KeyPressEventArgs e)
            {
                ReportEvent("KeyPress事件，即键盘按下事件", sender, e);
            };

            // C# 3Lambda表达式方式来订阅事件
            button1.Click += (sender, e) => ReportEvent("Click事件", sender, e);
            button1.KeyPress += (sender, e) => ReportEvent("KeyPress事件，即键盘按下事件", sender, e);

            // C# 3中使用对象初始化器
            Form form = new Form { Name = "在控制台中创建的窗体", AutoSize = true, Controls = { button1 } };

            // 运行窗体
            Application.Run(form);
        }

        // 记录事件的回调方法
        private static void ReportEvent(string title, object sender, EventArgs e)
        {
            Console.WriteLine("发生的事件为：{0}", title);
            Console.WriteLine("发生事件的对象为：{0}", sender);
            Console.WriteLine("发生事件参数为： {0}", e.GetType());
            Console.WriteLine();
            Console.WriteLine();
        }
}