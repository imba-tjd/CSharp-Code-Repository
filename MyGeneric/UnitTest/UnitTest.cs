using System;
using MyGeneric;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    class UnitTest
    {
        [TestMethod]
        static void TestStack()
        {
            MyStack<int> myStack = new MyStack<int>();

            Console.WriteLine("Stack Test Start");

            myStack.Push(1);
            myStack.Push(2);
            myStack.Push(1);

            foreach (var e in myStack)
                Console.WriteLine(e);

            Console.WriteLine(myStack.Pop());
            Console.WriteLine(myStack.Pop());
            Console.WriteLine(myStack.Pop());

            Console.WriteLine("Stack Test End");
        }

        [TestMethod]
        static void TestSortedSet()
        {
            MySortedSet<int> mySortedSet = new MySortedSet<int>();

            Console.WriteLine("SortedSet Test Start");

            mySortedSet.Add(1);
            mySortedSet.Add(6);
            mySortedSet.Add(3);

            Console.WriteLine(mySortedSet[2]);
            Console.WriteLine(mySortedSet.Count);
            mySortedSet.Remove(1);
            Console.WriteLine(mySortedSet.Count);
            //mySortedSet.Clear();

            Console.WriteLine("SortedSet Test End");
        }
    }
}
