using Nibble.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Contracts.Events.Chef
{
    public class MealRemoved : IEvent
    {
        public Guid Id { get; set; }
        //better way to do this
        public Guid ChefId { get; set; }
        public MealRemoved(Guid id, Guid chefId)
        {
            Id = id;
            ChefId = chefId;
        }

        public int CompareTo(object obj)
        {
            if (obj is MealRemoved removed && removed.Id == Id)
                return 0;
            else return -1;
        }
    }
}
