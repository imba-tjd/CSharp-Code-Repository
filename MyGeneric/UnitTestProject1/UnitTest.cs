using System;
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
            myStack.Push(2);
            myStack.Push(1);
            Console.WriteLine("Push End");

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
            var myList = new MyList<int> { 1, 6, 3 };

            foreach (var e in myList)
                Console.WriteLine(e);

        }
    }

    [TestClass]
    public class MyTestClass
    {
        [TestMethod]
        public void MyTestMethod()
        {
            var stack = new System.Collections.Generic.Stack<int>();
            stack.Push(1);
            stack.Push(6);
            stack.Push(3);

            foreach (var e in stack)
            {
                Console.WriteLine(e);
            }
        }
    }
}
