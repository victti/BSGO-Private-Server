using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    internal class ConsumableAttribute
    {
        public string Attribute { get; set; }

        public ConsumableAttribute(string attribute)
        {
            Attribute = attribute;
        }
    }
}
