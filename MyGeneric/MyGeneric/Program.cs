﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace ImbaTJD.MyGeneric
{
    public class MyStack<T> : IEnumerable<T>, IReadOnlyCollection<T>
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

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = _top; i >= 0; i--)
                yield return _stack[i];
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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

    public class MyList<T> : IEnumerable<T>, IReadOnlyList<T>
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

        public MyList(IEnumerable<T> collection) : this() => AddRange(collection);

        /*
        public MyList(MyList<T> myList, int capacity) : this(capacity)
        {
            foreach (var e in myList)
                Add(e);
            Count = myList.Count;
        }

        public MyList(MyList<T> myList) : this(myList, myList.Capacity) { }
        */

        public int Count { get; private set; }
        public int Capacity { get; }
        public T this[int index]
        {
            get => _myList[index];
            set => _myList[index] = value;
        }

        // 泛型的GetEnumerator()如果返回(IEnumerator<T>)_myList.GetEnumerator()会报System.InvalidCastException：
        // 无法将类型为“SZArrayEnumerator”的对象强制转换为类型“System.Collections.Generic.IEnumerator`1[System.Int32]”。（或具体类型）
        // 如果返回(_myList as IEnumerable<T>).GetEnumerator()，会把空的也输出了。
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return _myList[i];
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Clear() => _myList = new T[0];

        public void Add(T item)
        {
            if (Count == Capacity)
                throw new InvalidOperationException("列表已满！");

            T[] tempList = _myList;

            if (Count == _myList.Length)
            {
                tempList = new T[Count + 8];
                _myList.CopyTo(tempList, 0);
            }

            tempList[Count++] = item;
            _myList = tempList;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            foreach (var item in collection)
                Add(item);
        }

        public bool Remove(T item)
        {
            T[] tempList = new T[Count - 1];

            int itemi = -1;
            for (int i = 0; i < Count; i++)
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

        public void RemoveAt(int index)
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
        }

        public void Reverse()
        {
            T temp;
            for (int i = 0; i < (Count - 1) / 2; i++)
            {
                temp = _myList[i];
                _myList[i] = _myList[Count - 1 - i];
                _myList[Count - 1 - i] = temp;
            }
        }
    }

    public class MySortedSet<T> : IEnumerable<T>, IReadOnlyCollection<T> where T : IComparable<T>
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

        public MySortedSet(IEnumerable<T> collection) : this()
        {
            foreach (var item in collection)
                Add(item);
        }

        public int Count { get; private set; }
        public MySortedSetSequence Sequence { get; private set; }
        public T Max => Sequence == MySortedSetSequence.SmallToBig ? this[Count - 1] : this[0];
        public T Min => Sequence == MySortedSetSequence.SmallToBig ? this[0] : this[Count - 1];
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

        public IEnumerator<T> GetEnumerator()
        {
            Node p = _head.next;

            while (p != null)
            {
                yield return p.data;
                p++;
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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

            Sequence = Sequence == MySortedSetSequence.SmallToBig ? MySortedSetSequence.BigToSmall : MySortedSetSequence.SmallToBig;
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

    public enum MySortedSetSequence
    {
        SmallToBig,
        BigToSmall
    }
}
