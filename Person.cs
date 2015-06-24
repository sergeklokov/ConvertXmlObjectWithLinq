using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertXmlObjectWithLinq
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set;}
        public bool Active { get; set; }  // it's active unless attribute active="false" exists

        public List<Phone> Phones { get; set; }
    }
}
