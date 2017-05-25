using System;
using System.Collections;
using System.Collections.Generic;

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
            b.Add(6);
            b.Add(3);
            Console.WriteLine(a.Pop());
            Console.WriteLine(a.Pop());
            Console.WriteLine(a.Pop());
            Console.WriteLine(b[2]);
            Console.WriteLine(b.Count);
            b.Remove(1);
            Console.WriteLine(b.Count);
            b.Clear();
            int[] f = new int[0];
            Console.WriteLine(f.Length);
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

        public int Capacity { get; private set; } // 最大容量
        public int Count { get => top + 1; } // 当前个数
        public void Clear() => top = -1;
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
            if (e == null)
                throw new NullReferenceException();
            if (top == Capacity - 1)
                throw new Exception("栈已满！");

            stack[++top] = e;
        }
    }
    class MySortedSet<T> :IEnumerable<T> where T : IComparable // 有序不重复集合
    {
        T[] mySet = new T[0];
        public void Clear() => mySet = new T[0];
        public MySortedSet(int size = 256)
        {
            if (size < 0)
                throw new Exception("集合大小不能为负！");
            if (size > 4096)
                throw new Exception("申请的集合太大！");
            MaxSize = size;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return mySet[i];
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return mySet[i];
        }
        public int Count { get => mySet.Length; }
        public int MaxSize { get; private set; }
        public T this [int index] { get => mySet[index]; }

        public void Add(T item)
        {
            if (item == null)
                throw new NullReferenceException();
            if (Count == MaxSize)
                throw new Exception("集合已满！");
            foreach(T e in mySet)
            if (e.Equals(item))
                throw new Exception("给定的元素已经存在！");

            T[] tempSet = new T[Count + 1];
            tempSet[Count] = item; // 直接把元素添加到最后一个
            for (int i = 0, j = 0; i < Count - 1; i++)
            {
                if (item.CompareTo(mySet[i]) < 0)
                {
                    tempSet[i] = item;
                    j = 1; //成功插入后，数组复制的下标相差1
                }
                tempSet[i + j] = mySet[i]; //如已成功插入，则会覆盖刚才添加到最后的e
            }
            mySet = tempSet;
        }
        public void Remove(T item)
        {
            if (item == null)
                throw new NullReferenceException();
            if (Count == 0)
                throw new Exception("集合为空！");

            int itemi = -1;
            for (int i = 0; i < Count - 1; i++)
                if (mySet[i].Equals(item)) { itemi = i; break; }
            if (itemi == -1)
                throw new Exception("找不到给定的元素！");

            T[] tempSet = new T[Count - 1];
            for (int i = 0, j = 0; i < Count - 1; i++)
            {
                if (i == itemi)
                    j = 1;
                tempSet[i] = mySet[i + j];
            }
            mySet = tempSet;
        }
    }
}