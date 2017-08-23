using System;
using System.Collections;
using System.Collections.Generic;

namespace MyGeneric
{
    class Program
    {
        static int Main()
        {
            MyStack<int> a = new MyStack<int>(8 * 1024);
            MySortedList<int> b = new MySortedList<int>();
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

    class MyStack<T> : IEnumerable<T>, IReadOnlyCollection<T>
    {
        readonly T[] _stack;
        int _top = -1;
        bool IsFull => _top == Capacity - 1;
        bool IsEmpty => _top < 0;

        public MyStack(int capacity = 256)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "栈大小不能为负！");
            if (capacity * System.Runtime.InteropServices.Marshal.SizeOf<T>() > 8 * 1024 * 4)
                throw new ArgumentOutOfRangeException(nameof(capacity), "申请的栈太大！");

            Capacity = capacity;
            _stack = new T[capacity];
        }

        public int Capacity { get; } // 最大容量
        public int Count => _top + 1; // 当前个数
        public void Clear() => _top = -1;

        public IEnumerator<T> GetEnumerator() => _stack.GetEnumerator() as IEnumerator<T>;
        IEnumerator IEnumerable.GetEnumerator() => _stack.GetEnumerator();

        public T Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("栈为空！");

            return _stack[_top];
        }

        public T Pop()
        {
            if (IsEmpty)
                throw new InvalidOperationException("栈为空！");

            return _stack[_top--];
        }

        public void Push(T e)
        {
            if (IsFull)
                throw new StackOverflowException("栈已满！");

            _stack[++_top] = e;
        }

    }

    class MySortedList<T> : IEnumerable<T>, IReadOnlyList<T> where T : IComparable<T>
    {
        T[] _myList = new T[0]; // 数组使用时新建一个，不是创建对象时建立capacity大小的数组

        public MySortedList(int capacity = 256)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "列表大小不能为负！");
            if (capacity > 512)
                throw new ArgumentOutOfRangeException(nameof(capacity), "申请的列表太大！");

            Capacity = capacity;
        }

        public void Clear() => _myList = new T[0];


        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return _myList[i];
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return _myList[i];
        }

        public int Count => _myList.Length;
        public int Capacity { get; }
        public T this[int index] => _myList[index];

        public bool Contains(T item)
        {
            foreach (T e in _myList)
                if (e.Equals(item))
                    return true;
            return false;
        }

        public bool Add(T item)
        {
            if (Count == Capacity)
                throw new InvalidOperationException("列表已满！");

            T[] tempSet = new T[Count + 1];
            tempSet[Count] = item; // 直接把元素添加到最后一个
            for (int i = 0, j = 0; i < Count - 1; i++)
            {
                if (item.CompareTo(_myList[i]) < 0)
                {
                    tempSet[i] = item;
                    j = 1; //成功插入后，数组复制的下标相差1
                }
                tempSet[i + j] = _myList[i]; //如已成功插入，则会覆盖刚才添加到最后的e
            }
            _myList = tempSet;
            return true;
        }

        public bool AddRange(T[] collection)
        {
            foreach (var e in collection)
                if (!Add(e))
                    return false;
            return true;
        }

        public bool Remove(T item)
        {
            if (item == null)
                return true;
            if (Count == 0)
                throw new InvalidOperationException("集合为空！");

            int itemi = -1;
            for (int i = 0; i < Count - 1; i++)
                if (_myList[i].Equals(item)) { itemi = i; break; }
            if (itemi == -1)
                return false;

            T[] tempSet = new T[Count - 1];
            for (int i = 0, j = 0; i < Count - 1; i++)
            {
                if (i == itemi)
                    j = 1;
                tempSet[i] = _myList[i + j];
            }
            _myList = tempSet;
            return true;
        }
    }
}