using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Infrastructure
{
    public class EventMetadata
    {
        public string Value { get; private set; }
        private EventMetadata(string value)
        {
            Value = value;
        }

        public static EventMetadata TypeHeader { get { return new EventMetadata("EventClrType"); } }
    }
}
