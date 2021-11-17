using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Infrastructure
{
    public interface IHandle<in TCommand> where TCommand : IRequest
    {
        Task<IAggregate> HandleAsync(TCommand command);
    }
}
