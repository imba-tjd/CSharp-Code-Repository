using System;
using System.Collections;
using System.Collections.Generic;

namespace ImbaTJD.MyGeneric
{
    /// <summary>
    /// 一种先进后出的数据结构
    /// </summary>
    /// <typeparam name="T">指定栈中的元素类型</typeparam>
    public class MyStack<T> : IEnumerable<T>, IReadOnlyCollection<T>
    {
        readonly T[] _stack;
        int _top = -1;
        bool _IsFull => _top == Capacity - 1;
        bool _IsEmpty => _top < 0;

        /// <summary>
        /// 创建栈的实例
        /// </summary>
        /// <param name="capacity">栈的大小</param>
        public MyStack(int capacity = 256)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "栈大小不能为负！");
            if (capacity * System.Runtime.InteropServices.Marshal.SizeOf<T>() > 8 * 1024 * 4)
                throw new ArgumentOutOfRangeException(nameof(capacity), "申请的栈太大！");

            Capacity = capacity;
            _stack = new T[capacity];
        }

        /// <summary>
        /// 从集合复制元素来创建栈的实例
        /// </summary>
        /// <param name="collection">要复制的集合</param>
        public MyStack(IEnumerable<T> collection):this()
        {
            foreach (var e in collection)
                Push(e);
        }

        /// <summary>
        /// 获取栈的最大容量
        /// </summary>
        public int Capacity { get; }

        /// <summary>
        /// 获取栈元素的个数
        /// </summary>
        public int Count => _top + 1;

        /// <summary>
        /// 清空栈
        /// </summary>
        public void Clear() => _top = -1;

        /// <summary>
        /// 返回枚举器
        /// </summary>
        /// <returns>Stack<![CDATA[<T>]]>的枚举器</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = _top; i >= 0; i--)
                yield return _stack[i];
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// 查看栈顶的元素
        /// </summary>
        /// <returns>顶部的元素</returns>
        public T Peek()
        {
            if (_IsEmpty)
                throw new InvalidOperationException("栈为空！");

            return _stack[_top];
        }

        /// <summary>
        /// 从栈顶弹出一个元素
        /// </summary>
        /// <returns>顶部的元素</returns>
        public T Pop()
        {
            if (_IsEmpty)
                throw new InvalidOperationException("栈为空！");

            return _stack[_top--];
        }

        /// <summary>
        /// 把元素压入栈顶
        /// </summary>
        /// <param name="e"></param>
        public void Push(T e)
        {
            if (_IsFull)
                throw new StackOverflowException("栈已满！");

            _stack[++_top] = e;
        }

    }

    /// <summary>
    /// 一种类似于加强型数组的数据结构
    /// </summary>
    /// <typeparam name="T">指定列表中的元素类型</typeparam>
    public class MyList<T> : IEnumerable<T>, IReadOnlyList<T>
    {
        T[] _myList = new T[0]; // 数组使用时新建一个，不是创建对象时建立capacity大小的数组

        /// <summary>
        /// 创建列表的实例
        /// </summary>
        /// <param name="capacity">列表的最大长度</param>
        public MyList(int capacity = 256)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "列表长度不能为负！");
            if (capacity > 512)
                throw new ArgumentOutOfRangeException(nameof(capacity), "申请的列表太长！");

            Capacity = capacity;
        }

        /// <summary>
        /// 从集合复制元素来创建列表的实例
        /// </summary>
        /// <param name="collection">要复制的集合</param>
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

        /// <summary>
        /// 获取列表元素的个数
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// 获取列表的最大长度
        /// </summary>
        public int Capacity { get; }

        /// <summary>
        /// 获取或设置指定索引处的元素
        /// </summary>
        /// <param name="index">从零开始的索引</param>
        /// <returns>指定索引处的元素</returns>
        public T this[int index]
        {
            get => _myList[index];
            set => _myList[index] = value;
        }

        // 泛型的GetEnumerator()如果返回(IEnumerator<T>)_myList.GetEnumerator()会报System.InvalidCastException：
        // 无法将类型为“SZArrayEnumerator”的对象强制转换为类型“System.Collections.Generic.IEnumerator`1[System.Int32]”。（或具体类型）
        // 如果返回(_myList as IEnumerable<T>).GetEnumerator()，会把空的也输出了。
        /// <summary>
        /// 返回枚举器
        /// </summary>
        /// <returns>List<![CDATA[<T>]]>的枚举器</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return _myList[i];
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// 清空列表
        /// </summary>
        public void Clear() => _myList = new T[0];

        /// <summary>
        /// 把元素添加到列表末尾
        /// </summary>
        /// <param name="item">要添加的元素</param>
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

        /// <summary>
        /// 把一个集合中的所有元素按顺序添加到列表末尾
        /// </summary>
        /// <param name="collection">要添加的元素来源的集合</param>
        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
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

        /// <summary>
        /// 将列表中的元素顺序反过来
        /// </summary>
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

        /// <summary>
        /// 返回只读的列表
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<T> AsReadOnly() => this as IReadOnlyList<T>;
    }

    /// <summary>
    /// 有序不重复集合
    /// </summary>
    /// <typeparam name="T">指定集合中的元素类型</typeparam>
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
