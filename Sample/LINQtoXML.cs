// 《Illustrated C# 2012 (4th Edition)》

using System;
using System.Linq;
using System.Xml.Linq;

namespace LINQtoXML
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument employeeXDoc1 = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("This is a comment"),
                new XProcessingInstruction("xml-stylesheet", "href=\"stories.css\" type=\"text/css\""),

                new XElement("Employees",
                new XAttribute("root", true),
                    new XElement("employee",
                        new XElement("Name", "Bob Smith"),
                        new XElement("PhoneNumber", "123456")
                        ),
                    new XElement("employee",
                        new XElement("Name", "Sally Jones"),
                        new XElement("PhoneNumber", "654321")
                        )
                )
            );
            employeeXDoc1.Save("EmployeesFile.xml");

            XDocument employeeXDoc2 = XDocument.Load("EmployeesFile.xml");
            Console.WriteLine(employeeXDoc2);

            Console.WriteLine("root:{0}", employeeXDoc2.Element("Employees").Attribute("root").Value);

            var xyz = from e in employeeXDoc2.Element("Employees").Elements()
                      let f = e.Element("Name")
                      where f.Value.ToString().Length == 9
                      select new { f.Name, f.Value };

            foreach (var x in xyz)
                Console.WriteLine("Name:{0}", x.Value);

            Console.ReadKey();
        }
    }
}

/* Output:
<!--This is a comment-->
<?xml-stylesheet href="stories.css" type="text/css"?>
<Employees root="true">
  <employee>
    <Name>Bob Smith</Name>
    <PhoneNumber>123456</PhoneNumber>
  </employee>
  <employee>
    <Name>Sally Jones</Name>
    <PhoneNumber>654321</PhoneNumber>
  </employee>
</Employees>
root:true
Name:Bob Smith
*/