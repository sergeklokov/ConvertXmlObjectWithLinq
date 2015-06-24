using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertXmlObjectWithLinq
{
    /// <summary>
    /// all kinds of service methods
    /// </summary>
    public static class SergeConversions
    {
        /// <summary>
        /// convert string which could be null to enum
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static PhoneType GetPhoneType(string Type)
        {
            var t = PhoneType.unknown;
            Enum.TryParse(Type, true, out t);
            return t;
        }
    }
}
