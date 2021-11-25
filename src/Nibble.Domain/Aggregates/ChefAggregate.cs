using Nibble.Domain.ValueObjects;
using Nibble.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Domain.Aggregates
{
    public class ChefAggregate : BaseAggregate
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Address PickupAddress { get; private set; }

        internal static IAggregate Create()
        {
            return new ChefAggregate();
        }

        public ChefAggregate()
        {
            //AddEventHandler<ChefCreated>
        }
    }
}
