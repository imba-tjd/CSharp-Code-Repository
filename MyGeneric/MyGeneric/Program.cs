using System;
using System.Collections;
using System.Collections.Generic;

namespace MyGeneric
{
    class Program
    {
        static int Main()
        {
            MyStack<int> a = new MyStack<int>();
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

    class MyStack<T> : IEnumerable<T>, IReadOnlyCollection<T>
    {
        readonly T[] _stack;
        int _top = -1;
        bool _IsFull => _top == Capacity - 1;
        bool _IsEmpty => _top < 0;

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
            if (_IsEmpty)
                throw new InvalidOperationException("栈为空！");

            return _stack[_top];
        }

        public T Pop()
        {
            if (_IsEmpty)
                throw new InvalidOperationException("栈为空！");

            return _stack[_top--];
        }

        public void Push(T e)
        {
            if (_IsFull)
                throw new StackOverflowException("栈已满！");

            _stack[++_top] = e;
        }

    }

    class MyList<T> : IEnumerable<T>, IReadOnlyList<T>
    {
        T[] _myList = new T[0]; // 数组使用时新建一个，不是创建对象时建立capacity大小的数组

        public MyList(int capacity = 256)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "列表长度不能为负！");
            if (capacity > 512)
                throw new ArgumentOutOfRangeException(nameof(capacity), "申请的列表太长！");

            Capacity = capacity;
        }

        public int Count { get; private set; }
        public int Capacity { get; }
        public T this[int index] => _myList[index];

        public IEnumerator<T> GetEnumerator() => _myList.GetEnumerator() as IEnumerator<T>;
        IEnumerator IEnumerable.GetEnumerator() => _myList.GetEnumerator();

        public void Clear() => _myList = new T[0];

        public bool Add(T item)
        {
            T[] tempList = _myList;

            if (Count == Capacity)
                throw new InvalidOperationException("列表已满！");

            if (Contains(item))
                return false;

            if (Count == _myList.Length)
                tempList = new T[Count + 4];

            tempList[Count++] = item;
            return true;
        }

        public void AddRange(T[] collection)
        {
            foreach (var e in collection)
                Add(e);
        }

        public bool Remove(T item)
        {
            T[] tempList = new T[Count - 1];

            int itemi = -1;
            for (int i = 0; i < Count - 1; i++)
                if (_myList[i].Equals(item)) { itemi = i; break; }
            if (itemi == -1)
                return false;

            for (int i = 0, j = 0; i < Count - 1; i++)
            {
                if (i == itemi)
                    j = 1;
                tempList[i] = _myList[i + j];
            }
            _myList = tempList;
            return true;
        }

        public bool Contains(T item)
        {
            foreach (T e in _myList)
                if (e.Equals(item))
                    return true;
            return false;
        }
    }

    class MySortedSet<T> : IEnumerable<T>, IReadOnlyCollection<T> where T : IComparable<T>
    {
        Node _head;

        class Node
        {
            public T data;
            public Node next;

            internal Node() : this(default(T), null) { }
            internal Node(T data) : this(data, null) { }
            internal Node(Node next) : this(default(T), next) { }
            internal Node(T data, Node next)
            {
                this.data = data;
                this.next = next;
            }
        }

        public MySortedSet() => _head = new Node();

        public int Count => throw new NotImplementedException();
        public T Max => throw new NotImplementedException();
        public T Min => throw new NotImplementedException();
        public T this[int index] => throw new NotImplementedException();
        public void Clear() => _head.next = null;

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Add(T item)
        {
            throw new NotImplementedException();

        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public bool Reverse(T item)
        {
            throw new NotImplementedException();
        }

        /*
         * 使用数组储存有序集合时的Add方法
        public bool Add(T item)
        {
            if (Count == Capacity)
                throw new InvalidOperationException("集合已满！");

            T[] tempSet = new T[Count + 1];
            tempSet[Count] = item; // 直接把元素添加到最后一个
            for (int i = 0, j = 0; i < Count - 1; i++)
            {
                if (item.CompareTo(_mySet[i]) < 0)
                {
                    tempSet[i] = item;
                    j = 1; //成功插入后，数组复制的下标相差1
                }
                tempSet[i + j] = _mySet[i]; //如已成功插入，则会覆盖刚才添加到最后的e
            }
            _mySet = tempSet;
            return true;
        }
        */
    }
}
