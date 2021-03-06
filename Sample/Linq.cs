// 《Learning Hard 学习笔记》代码
// http://www.ituring.com.cn/book/1604

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqtoObject
{
    class MyClass
    {
        public string Group;
        public int Num;
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 初始化查询的数据
            List<int> inputArray = new List<int>();
            for (int i = 1; i < 10; i++)
            {
                inputArray.Add(i);
            }
            Console.WriteLine("使用Linq方法来对集合对象查询，查询结果为：");
            LinqQuery(inputArray);

            LinqGroup();

            Console.ReadKey();
        }

        // 使用Linq返回集合中为偶数的元素
        private static void LinqQuery(List<int> collection)
        {
            // 创建查询表达式来获得集合中为偶数的元素
            var queryResults = from item in collection
                               where item % 2 == 0
                               select item;
            // 输出查询结果
            foreach (var item in queryResults)
            {
                Console.Write(item+"  ");
            }
        }
        static void LinqGroup(void)
        {
            MyClass[] g = new MyClass[]
            {
                new MyClass() { Group = "A", Num = 0 },
                new MyClass() { Group = "A", Num = 1 },
                new MyClass() { Group = "B", Num = 0 },

            };

            var r = from e in g
                    group e by e.Group;

            foreach (var e in r)
            {
                Console.WriteLine(e.Key + ":");
                foreach (var f in e)
                    Console.WriteLine(f.Num);
            }
        }
    }
}

namespace LinqtoXML
{
    class Program
    {
         // 初始化XML数据
        private static string xmlString =
            "<Persons>"+
            "<Person Id='1'>"+
            "<Name>张三</Name>"+
            "<Age>18</Age>"+
            "</Person>" +
            "<Person Id='2'>"+
            "<Name>李四</Name>"+
            "<Age>19</Age>"+
            "</Person>"+
             "<Person Id='3'>" +
            "<Name>王五</Name>" +
            "<Age>22</Age>" +
            "</Person>"+
            "</Persons>";

        static void Main(string[] args)
        {
            Console.WriteLine("使用Linq方法来对XML文件查询，查询结果为：");
            UsingLinqLinqtoXMLQuery();
            Console.Read();
        }

        // 使用Linq 来对XML文件进行查询
        private static void UsingLinqLinqtoXMLQuery()
        {
            // 导入XML
            XElement xmlDoc = XElement.Parse(xmlString);

            // 创建查询，获取姓名为“李四”的元素
            var queryResults = from element in xmlDoc.Elements("Person")
                               where element.Element("Name").Value == "李四"
                               select element;

            // 输出查询结果
            foreach (var xele in queryResults)
            {
                Console.WriteLine("姓名为： " + xele.Element("Name").Value + "  Id 为：" + xele.Attribute("Id").Value);
            }
        }
    }
}