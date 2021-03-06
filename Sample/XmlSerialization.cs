// 《写给大家看的面向对象编程书》
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Net;
using System.Net.Sockets;

namespace XmlSerialization
{
    [XmlRoot("person")]
    public class Person
    {
        [XmlAttribute("name")]
        public string Name { get; private set; }
        [XmlElement("age")]
        public int Age { get; private set; }
        [XmlIgnore]
        public int Score { get; private set; }

        public Person(String name = "John", int age = 25, int score = 60)
        {
            Name = name;
            Age = age;
            Score = score;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Serialize();
            DeSerialize();
            Console.ReadKey();
        }

        static void Serialize()
        {
            Person[] myPeople =
            {
                new Person(),
                new Person("Joe",32,67)
            };

            XmlSerializer mySerializer = new XmlSerializer(typeof(Person[]));
            TextWriter myWriter = new StreamWriter("person.xml");
            mySerializer.Serialize(myWriter, myPeople);
            myWriter.Close();
        }

        static void DeSerialize()
        {
            Person[] myRestoredPeople;
            XmlSerializer mySerializer = new XmlSerializer(typeof(Person[]));
            TextReader myReader = new StreamReader("Person.xml");
            myRestoredPeople = mySerializer.Deserialize(myReader) as Person[];
            foreach (var person in myRestoredPeople)
                Console.WriteLine($"{person.Name} is {person.Age} years old.");
        }
    }

    /*  客户代码
        获得用户信息
        创建一个对象
        设置属性
        （将对象串行化为xml）
        创建一个socket连接
        创建输入流
        写对象（将对象串行化至流）
        关闭流
    */
    public class Client
    {
        public static void Connect()
        {
            Person person = new Person();
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 11111);
                XmlSerializer mySerializer = new XmlSerializer(typeof(Person));
                NetworkStream stream = client.GetStream();
                mySerializer.Serialize(stream, person);
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    /*  服务器代码
        创建一个对象引用
        监听虚拟端口11111
        等待客户连接
        创建输入/输出流
        （）读取字节
        读取对象（解串行化）
        打印消息
    */
    public class Server
    {
        public static void Listen()
        {
            TcpListener server = null;
            TcpClient client = null;
            try
            {
                server = new TcpListener(IPAddress.Parse("127.0.0.1"), 11111);
                server.Start();

                Byte[] bytes = new byte[256];

                client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");

                NetworkStream stream = client.GetStream();

                if (stream.Read(bytes, 0, bytes.Length) != 0)
                {
                    MemoryStream ms = new MemoryStream(bytes);
                    XmlSerializer mySerializer = new XmlSerializer(typeof(Person));
                    Person person = mySerializer.Deserialize(ms) as Person;
                    Console.WriteLine($"{person.Name} is {person.Age} years old.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Close();
                server.Stop();
            }
        }
    }
}
