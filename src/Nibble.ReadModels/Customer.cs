using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.EventWorker.ReadModels
{
    public class Customer : IReadModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public Guid Id { get; set; }
    }
}
