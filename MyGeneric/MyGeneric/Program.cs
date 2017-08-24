using System;
using System.Collections;
using System.Collections.Generic;

namespace MyGeneric
{
    class Program
    {
        static int Main()
        {
            MyStack<int> myStack = new MyStack<int>();
            MySortedSet<int> mySortedSet = new MySortedSet<int>();

            myStack.Push(1);
            myStack.Push(2);
            myStack.Push(1);
            foreach (var e in myStack)
                Console.WriteLine(e);
            Console.WriteLine(myStack.Pop());
            Console.WriteLine(myStack.Pop());
            Console.WriteLine(myStack.Pop());
            Console.WriteLine("Stack Test End");

            mySortedSet.Add(1);
            mySortedSet.Add(6);
            mySortedSet.Add(3);

            Console.WriteLine(mySortedSet[2]);
            Console.WriteLine(mySortedSet.Count);
            mySortedSet.Remove(1);
            Console.WriteLine(mySortedSet.Count);
            //mySortedSet.Clear();
            Console.WriteLine("SortedSet Test End");



            Console.ReadKey();
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

        // TODO : 输出顺序应该反过来
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

        public MyList(MyList<T> myList, int capacity):this(capacity)
        {
            foreach (var e in myList)
                Add(e);
            Count = myList.Count;
        }

        public MyList(MyList<T> myList) : this(myList, myList.Capacity) { }

        public int Count { get; private set; }
        public int Capacity { get; }
        public T this[int index]
        {
            get => _myList[index];
            set => _myList[index] = value;
        }

        public IEnumerator<T> GetEnumerator() => _myList.GetEnumerator() as IEnumerator<T>;
        IEnumerator IEnumerable.GetEnumerator() => _myList.GetEnumerator();

        public void Clear() => _myList = new T[0];

        public bool Add(T item)
        {
            if (Count == Capacity)
                throw new InvalidOperationException("列表已满！");

            T[] tempList = _myList;

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
            Count--;
            return true;
        }

        public bool Contains(T item)
        {
            foreach (T e in _myList)
                if (e.Equals(item))
                    return true;
            return false;
        }

        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= Capacity)
                throw new ArgumentOutOfRangeException(nameof(index), "索引超过范围！");

            T[] tempList = new T[Count - 1];
            for (int i = 0, j = 0; i < Count - 1; i++)
            {
                if (i == index)
                    j = 1;
                tempList[i] = _myList[i + j];
            }
            _myList = tempList;
            Count--;
            return true;
        }

        public void Reverse()
        {
            T temp;
            for (int i = 0; i < (Count - 1) / 2; i++)
            {
                temp = this[i];

                this[Count - 1 - i] = temp;
            }
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

            public static Node operator ++(Node node) => node = node.next;
        }

        public MySortedSet(MySortedSetSequence sequence = MySortedSetSequence.SmallToBig)
        {
            _head = new Node();
            Sequence = sequence;
        }

        public MySortedSet(MySortedSet<T> mySortedSet)
        {
            throw new NotImplementedException();
        }

        public int Count { get; private set; }
        public MySortedSetSequence Sequence { get; }
        public T Max => Sequence == MySortedSetSequence.SmallToBig ? this[Count-1] : this[0];
        public T Min => Sequence == MySortedSetSequence.SmallToBig ? this[0] : this[Count-1];
        public T this[int index]
        {
            get
            {
                Node p = _head.next;

                for (int i = 0; i < index; i++)
                    p++;

                return p.data;
            }
        }

        public void Clear() => _head.next = null;

        public IEnumerator<T> GetEnumerator() => (this as IEnumerable).GetEnumerator() as IEnumerator<T>;
        IEnumerator IEnumerable.GetEnumerator()
        {
            Node p = _head.next;

            while (p != null)
            {
                yield return p.data;
                p++;
            }
        }

        public bool Add(T item)
        {
            Node p = _head;

            while (p.next != null)
            {
                if (p.next.data.CompareTo(item) == 0)
                    return false;
                if (p.next.data.CompareTo(item) < 0 && Sequence == MySortedSetSequence.SmallToBig
                    || p.next.data.CompareTo(item) > 0 && Sequence == MySortedSetSequence.BigToSmall)
                    p++;
                else
                    break;
            }

            p.next = new Node(item, p.next);
            Count++;
            return true;
        }

        public bool Contains(T item)
        {
            foreach (var e in this)
                if (e.Equals(item))
                    return true;
            return false;
        }

        public bool Remove(T item)
        {
            Node p = _head;

            while (p.next != null && !p.next.data.Equals(item))
                p++;

            if (p.next == null)
                return false;

            p.next = p.next.next;
            return true;
        }

        public void Reverse()
        {
            Node p = _head.next;
            Node q = p.next;

            p.next = null;

            while (q != null)
            {
                p = q;
                q = q.next;
                p.next = _head.next;
                _head.next = p;
            }
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

    enum MySortedSetSequence
    {
        SmallToBig,
        BigToSmall
    }
}
