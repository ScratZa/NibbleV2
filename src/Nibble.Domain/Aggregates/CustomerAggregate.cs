using Nibble.Contracts.Events;
using Nibble.Domain.ValueObjects;
using Nibble.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Domain.Aggregates
{
    public class CustomerAggregate: BaseAggregate
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Address PrimaryAddress { get; private set; }
        public Address SecondaryAddress { get; private set; }
        internal static IAggregate Create(Guid id, string firstName, string lastName)
        {
            return new CustomerAggregate(id, firstName, lastName); 
        }
        public CustomerAggregate()
        {
            AddEventHandler<CustomerCreated>(ApplyCreateCustomer);
            AddEventHandler<PrimaryAddressAdded>(AddPrimaryAddress);
        }

        public CustomerAggregate AddAddress(string friendyName, double lat, double lon)
        {
            RaiseEvent(new PrimaryAddressAdded(Id, friendyName, lat, lon));
            return this;
        }

        private CustomerAggregate(Guid id, string FirstName, string LastName): this()
        {
            RaiseEvent(new CustomerCreated(id,FirstName,LastName));
        }

        private void ApplyCreateCustomer(CustomerCreated @event)
        {
            Id = @event.Id;
            FirstName = @event.FirstName;
            LastName = @event.LastName;
        }

        private void AddPrimaryAddress(PrimaryAddressAdded @event)
        {
            PrimaryAddress = new Address
            {
                FriendlyName = @event.Name,
                Latitude = @event.Lat,
                Longitude = @event.Lon
            };
        }
    }
}
