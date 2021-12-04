using Nibble.EventWorker.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.ReadModels
{
    public class ChefListItem: IReadModel
    {
        public Guid Id { get; set;}
        public string Name { get; set; }
        public string CookingStyle { get; set; }
    }
}
