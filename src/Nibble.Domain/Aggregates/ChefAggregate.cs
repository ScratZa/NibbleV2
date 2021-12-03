using Nibble.Contracts.Events.Chef;
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
        public string CookingStyle { get; private set; }
        public Address PickupAddress { get; private set; }

        internal static IAggregate Create(Guid id, string firstName, string lastName, string cookingStyle, string addressName, double lat, double lon)
        {
            return new ChefAggregate(id, firstName,lastName,cookingStyle,addressName,lat,lon);
        }

        public ChefAggregate()
        {
            AddEventHandler<ChefCreated>(ApplyCreateChef);
        }

        private ChefAggregate(Guid id, string firstName, string lastName, string cookingStyle, string addressName, double lat, double lon) : this()
        {
            RaiseEvent(new ChefCreated(id, firstName, lastName, cookingStyle, addressName, lat, lon));
        }

        private void ApplyCreateChef(ChefCreated @event)
        {
            Id = @event.Id;
            FirstName = @event.FirstName;
            LastName = @event.LastName;
            PickupAddress = new Address
            {
                FriendlyName = "Default",
                AddressName = @event.AddressName,
                Latitude = @event.Latitude,
                Longitude = @event.Longitude,
            };
            CookingStyle = @event.CookingStyle;   
        }
    }
}
