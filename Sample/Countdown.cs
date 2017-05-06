using System;
using System.Threading;

namespace Countdown
{
    class Program
    {
        static void Main()
        {
            Console.Write("请输入倒计时秒数：");
            Countdown myCountdown = new Countdown(int.Parse(Console.ReadLine()));
            Console.ReadKey();
        }
    }
    class Countdown
    {
        public delegate void MyEventHandler();
        public event MyEventHandler MyEvent;
        private int second;
        public bool Enabled { get; set; }
        private void OnMyEvent()
        {
            while (Enabled)
            {
                Thread.Sleep(1000);
                MyEvent();
            }
        }
        public Countdown(int second)
        {
            this.second = second;
            MyEvent += new Countdown.MyEventHandler(Countdown_Event);
            Thread thread = new Thread(new ThreadStart(OnMyEvent))
            {
                IsBackground = true
            };
            Enabled = true;
            thread.Start();
        }
        private void Countdown_Event()
        {
            Console.WriteLine(--second);
            if (second == 0)
            {
                Console.WriteLine("时间到了！");
                Console.Beep();
                Enabled = false;
            }
        }
    }
}