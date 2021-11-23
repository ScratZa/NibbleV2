using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Infrastructure
{
    public class StreamType
    {
        public string Value { get; private set; }
        private StreamType(string value)
        {
            Value = value;
        }

        public static StreamType Default { get { return new StreamType("Nibble"); } }
        public static StreamType Customer { get { return new StreamType("Customer"); } }
    }
}
