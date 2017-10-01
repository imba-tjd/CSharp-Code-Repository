// 《Learning Hard 学习笔记》代码
// http://www.ituring.com.cn/book/1604

using System;
using System.Threading;

namespace ThreadPool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("主线程运行");
            CancellationTokenSource cts = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(callback, cts.Token);
            Console.WriteLine("按下回车键来取消操作");
            Console.Read();
            cts.Cancel(); // 取消请求


            Console.WriteLine("主线程ID = {0}", Thread.CurrentThread.ManagedThreadId);
            ThreadPool.QueueUserWorkItem(CallBackWorkItem);
            ThreadPool.QueueUserWorkItem(CallBackWorkItem, "work");
            Thread.Sleep(3000);
            Console.WriteLine("主线程退出");

            Console.ReadKey();
        }

        private static void callback(object state)
        {
            CancellationToken token = (CancellationToken) state;
            Console.WriteLine("开始计数");
            Count(token, 1000); // 开始计数
        }
        private static void Count(CancellationToken token, int countto)
        {
            for (int i = 0; i < countto; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("计数取消");
                    return;
                }

                Console.WriteLine("计数为: " + i);
                Thread.Sleep(300);
            }

            Console.WriteLine("计数完成");
        }

        private static void CallBackWorkItem(object state)
        {
            Console.WriteLine("线程池线程开始执行");
            if (state != null)
            {
                Console.WriteLine("线程池线程ID = {0} 传入的参数为 {1}", Thread.CurrentThread.ManagedThreadId, state.ToString());
            }
            else
            {
                Console.WriteLine("线程池线程ID = {0}", Thread.CurrentThread.ManagedThreadId);
            }
        }
    }
}