using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGeneric
{
    class Program
    {
        static int Main()
        {
            MyStack<int> a = new MyStack<int>(8 * 1024 + 1);
            MySortedSet<int> b = new MySortedSet<int>();
            a.Push(1);
            a.Push(2);
            a.Push(1);
            b.Add(1);
            b.Add(1);
            b.Add(6);
            b.Add(3);
            Console.WriteLine(a.Pop());
            Console.WriteLine(a.Pop());
            Console.WriteLine(a.Pop());
            Console.WriteLine(b[2]);
            Stack<int> d;
            d = null;
            //Console.ReadKey();
            return 0;
        }
    }

    class MyStack<T>
    {
        readonly T[] stack;
        int top = -1;

        public MyStack(int capacity = 256)
        {
            if (capacity < 0)
                throw new Exception("栈大小不能为负！");
            if (capacity * System.Runtime.InteropServices.Marshal.SizeOf<T>() > 8 * 1024 * 8)
                throw new Exception("申请的栈太大！");
            Capacity = capacity;
            stack = new T[capacity];
        }

        public int Capacity { get; private set; }
        public int Count { get => top + 1; }
        public void Clear()
        {
            top = -1;
        }
        public T Peek()
        {
            if (top == -1)
                throw new Exception("栈为空！");
            return stack[top];
        }
        public T Pop()
        {
            if (top < 0)
                throw new Exception("栈为空！");
            return stack[top--];
        }
        public void Push(T e)
        {
            if (top == Capacity - 1)
                throw new Exception("栈已满！");
            stack[++top] = e;
        }
    }
    class MySortedSet<T> where T : IComparable
    {
        T[] mySet = new T[0];
        public MySortedSet(int size = 256)
        {
            if (size < 0)
                throw new Exception("集合大小不能为负！");
            if (size > 4096)
                throw new Exception("申请的集合太大！");
            MaxSize = size;
        }


        public int Count { get => mySet.Length; }
        public int MaxSize { get; private set; }
        public T this[int index] { get => mySet[index]; }
        public void Add(T e)
        {
            if (mySet.Length == MaxSize)
                throw new Exception("集合已满！");
            if (mySet.Length == 0)
            {
                mySet = new T[1];
                mySet[0] = e;
                return;
            }
            for (int i = 0; i < mySet.Length; i++)
                if (mySet[i].Equals(e))
                    throw new Exception("给定元素已经存在！");
            T[] tempSet = new T[mySet.Length + 1];
            tempSet[mySet.Length] = e;
            for (int i = 0; i < mySet.Length; i++)
            {
                int j = 0;
                if (e.CompareTo(mySet[i]) < 0)
                {
                    tempSet[i] = e;
                    j = 1;
                }
                tempSet[i + j] = mySet[i];
            }
            mySet = tempSet;
        }
        public void Clear()
        {
            mySet = new T[0];
        }
        public void Remove(T e)
        {
            int ei = -1;
            if (mySet.Length == 0)
                throw new Exception("集合为空！");
            for (int i = 0; i < mySet.Length; i++)
                if (mySet[i].Equals(e))
                { ei = i; break; }
            if (ei == -1)
                throw new Exception("找不到给定的元素！");
            T[] tempSet = new T[mySet.Length - 1];
            for (int i = 0, j = 0; i < tempSet.Length; i++)
            {
                if (i == ei)
                    j = 1;
                tempSet[i] = mySet[i + j];
            }
            mySet = tempSet;
        }
        //public static bool operator >(T a, T b) => a.CompareTo(b) > 0;

        //public static bool operator <(T a, T b) => a.CompareTo(b) < 0;
    }
}
