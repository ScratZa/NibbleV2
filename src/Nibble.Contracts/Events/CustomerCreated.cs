using Newtonsoft.Json;
using Nibble.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Contracts.Events
{
    public class CustomerCreated : IEvent
    {
        [JsonProperty]
        public Guid Id { get; set; }
        [JsonProperty]
        public string FirstName { get; set; }
        [JsonProperty]
        public string LastName { get; set; }

        public CustomerCreated()
        {

        }
        public CustomerCreated(Guid id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            var otherEvent = obj as CustomerCreated;

            if (otherEvent == null)
                throw new InvalidCastException();

            if (Id == otherEvent.Id &&
                FirstName == otherEvent.FirstName &&
                LastName == otherEvent.LastName)
                return 0;

            return 1;

        }
    }
}
