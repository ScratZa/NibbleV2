using Nibble.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Contracts.Events
{
    public class PrimaryAddressAdded : IEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }

        public PrimaryAddressAdded(Guid id,string name, double lat, double lon)
        {
            Id = id;
            Name = name;
            Lat = lat;
            Lon = lon;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            var other = obj as PrimaryAddressAdded;
            if(Id == other.Id && Lat == other.Lat && Lon == other.Lon)
            {
                return 0;
            }
            return 1;
        }
    }
}
