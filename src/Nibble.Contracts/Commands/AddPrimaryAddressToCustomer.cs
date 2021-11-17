using MediatR;
using Nibble.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Contracts.Commands
{
    public class AddPrimaryAddressToCustomer : IRequest<IAggregate>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }

        public AddPrimaryAddressToCustomer(Guid id , string name, double lat, double lon)
        {
            Id = id;
            Name = name;
            Lat = lat;
            Lon = lon;
        }
    }
}
