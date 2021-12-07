using Nibble.EventWorker.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.ReadModels
{
    public class Chef : IReadModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string CookingStyle { get; set; }
        public Guid Id { get; set; }
    }
}
