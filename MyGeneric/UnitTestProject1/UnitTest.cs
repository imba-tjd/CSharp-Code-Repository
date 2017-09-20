using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImbaTJD.MyGeneric.Tests
{
    /*
    class Program
    {
        static void Main()
        {
            var t = new UnitTest();
            t.TestStack();
            Console.ReadLine();
        }
    }
    */

    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestStack()
        {
            MyStack<int> myStack = new MyStack<int>();

            Console.WriteLine("Stack Test Start");

            myStack.Push(1);
            myStack.Push(6);
            myStack.Push(3);

            foreach (var e in myStack)
                Console.WriteLine(e);

            Console.WriteLine(myStack.Pop());
            Console.WriteLine(myStack.Pop());
            Console.WriteLine(myStack.Pop());

            Console.WriteLine("Stack Test End");
        }

        [TestMethod]
        public void TestSortedSet()
        {
            MySortedSet<int> mySortedSet = new MySortedSet<int>();

            Console.WriteLine("SortedSet Test Start");

            mySortedSet.Add(1);
            mySortedSet.Add(6);
            mySortedSet.Add(3);

            foreach (var e in mySortedSet)
                Console.WriteLine(e);

            Console.WriteLine(mySortedSet[2]);
            Console.WriteLine(mySortedSet.Count);
            mySortedSet.Remove(1);
            Console.WriteLine(mySortedSet.Count);
            //mySortedSet.Clear();

            Console.WriteLine("SortedSet Test End");
        }

        [TestMethod]
        public void TestList()
        {
            var myList = new MyList<int?> { 1, 6, 3 };

            foreach (var e in myList)
                Console.WriteLine(e);

            myList.Add(null);
            myList.Add(null);

            Console.WriteLine(myList.Remove(null));
            Console.WriteLine(myList.Remove(null));
            Console.WriteLine(myList.Remove(null));

            var a = new MyList<int?>();
            myList.AddRange(a);

            MyList<int> b = null;
            var c = new MyList<int>(b);
        }
    }

    [TestClass]
    public class MyTestClass
    {
        [TestMethod]
        public void MyTestMethod()
        {
            var set = new SortedSet<int>() { 1, 6, 3 };
            var stack = new Stack<int>();
            var list = new List<int>();
            
        }
    }
}
