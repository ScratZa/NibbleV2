using Nibble.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Contracts.Events.Chef
{
    public class ChefCreated : IEvent
    {

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CookingStyle { get; set; }
        public string AddressName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ChefCreated(Guid id, string firstName, string lastName, string cookingStyle, string addressName, double latitude, double longitude)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            CookingStyle = cookingStyle;
            AddressName = addressName;
            Latitude = latitude;
            Longitude = longitude;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            var otherEvent = obj as ChefCreated;

            if (otherEvent == null)
                throw new InvalidCastException();

            if (Id == otherEvent.Id &&
                FirstName == otherEvent.FirstName &&
                LastName == otherEvent.LastName &&
                CookingStyle == otherEvent.CookingStyle &&
                AddressName == otherEvent.AddressName &&
                Latitude == otherEvent.Latitude &&
                Longitude == otherEvent.Longitude)
                return 0;

            return 1;
        }
    }
}
