// Illustrated C# 2012
namespace UserConversion
{
    class Person
    {
        public string Name;
        public int Age;

        public static implicit operator int(Person p) // Person到int显式转换
        {
            return p.Age;
        }
    }

    class Employee : Person { } // Employee到Person有隐式转换

    class Program
    {
        static void Main(string[] args)
        {
            Employee bill = new Employee();
            bill.Name = "William";
            bill.Age = 25;

            float fVar = bill; // int到float隐式转换
            System.Console.WriteLine($"Person Info: {bill.Name}, {fVar}");
        }
    }
}
