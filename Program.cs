using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConvertXmlObjectWithLinq
{
    /// <summary>
    /// This is small demo of converting XML -> object and object -> XML with Linq
    /// We assume that XML is validated (see my demo of XML validation) and do not have major errors
    /// Serge Klokov 2015
    /// 
    /// Note: XML file have property "Copy to output directory" set to "Copy if newer"
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // convert XML to Object
            var phoneBook = ConvertXmlToObject();

            // print it
            PrintPhoneBook(phoneBook);

            ConvertObjectToXml(phoneBook);

            Console.ReadKey();
        }

        private static PhoneBook ConvertXmlToObject()
        {
            XDocument xdoc = XDocument.Load("XML/PhoneBook.xml");

            // first way of getting root attribute
            var phoneBook = new PhoneBook
            {
                Owner = xdoc.Root.Attribute("owner").Value,
                People = xdoc.Root.Elements("Person").Select(
                    per => new Person
                    {
                        ID = Convert.ToInt16(per.Attribute("id").Value),
                        Name = per.Attribute("name").Value,

                        // bool is false by default (or if not exists)
                        Active = per.Attribute("active") == null ? false : Convert.ToBoolean(per.Attribute("active").Value),

                        Phones = per.Elements("Phone").Select(
                            ph => new Phone
                            {
                                Number = ph.Value,

                                // types that not in the enum will be converted to "unknown" type
                                // so result XML will differ from original
                                Type =
                                    ph.Attribute("type") != null
                                    ? SergeConversions.GetPhoneType(ph.Attribute("type").Value) // convert string to enum
                                    : PhoneType.unknown
                            }
                        ).ToList()
                    }
                ).ToList(),
            };

            // other options to get root just for reference
            // var rootElement2 = xdoc.Element("PhoneBook");
            // var rootElement3 = xdoc.Elements("PhoneBook").First();

            return phoneBook;
        }

        private static void ConvertObjectToXml(PhoneBook phoneBook)
        {
            XDocument xdoc = new XDocument(
                new XElement("PhoneBook", // root
                    //new XElement("Person",
                        phoneBook.People.Select(p =>
                            new XElement("Person",
                                new XAttribute("id", p.ID),
                                new XAttribute("name", p.Name),
                                new XAttribute("active", p.Active),
                                    p.Phones.Select(i =>
                                        new XElement("Phone",

                                            // it will be some "unknown" because of conversion before
                                            new XAttribute("type", i.Type),

                                            // actual content value
                                            i.Number 
                                    ))))));

            string s = xdoc.ToString();
            // save to file on the hard drive
            xdoc.Save("XML/FromObjectPhoneBook.xml");
        }

        private static void PrintPhoneBook(PhoneBook phoneBook)
        {
            foreach (var person in phoneBook.People)
            {
                Console.WriteLine("{0} , active = {1}", person.Name, person.Active);

                foreach (var phone in person.Phones)
                {
                    Console.Write("    {0}:{1}", phone.Type.ToString(), phone.Number);
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
