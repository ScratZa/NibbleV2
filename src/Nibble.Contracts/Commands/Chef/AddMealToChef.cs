using MediatR;
using Nibble.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Contracts.Commands.Chef
{
    public class AddMealToChef : BaseChefCommand, IRequest<IAggregate>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string[] Tags { get; set; }
        public string[] Ingredients { get; set; }
        public Guid Id { get; set; }

    }
}
