using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertXmlObjectWithLinq
{
    /// <summary>
    /// really it's just root object, 
    /// usually we can ignore it 
    /// </summary>
    public class PhoneBook
    {
        public string Owner { get; set; }

        // Main data stored in People
        public List<Person> People { get; set; }
    }
}
