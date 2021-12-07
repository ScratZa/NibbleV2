using MediatR;
using Nibble.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Contracts.Commands.Chef
{
    public class RemoveMealFromChef : BaseChefCommand, IRequest<IAggregate>
    {
        public Guid Id { get; set; }
    }
}
