using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Contracts
{
    public class Address
    {
        public string AddressName { get; set; }
        public string AddressDisplayName { get; set; }
        public double[] Point { get; set; }
    }
}
