using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nibble.Infrastructure
{
    public interface IDomainRepository
    {
        Task<IEnumerable<IEvent>> SaveAsync<TAggragate>(TAggragate aggragate) where TAggragate : IAggregate;
        Task<TAggragate> GetByIdAsync<TAggragate>(Guid id) where TAggragate : IAggregate, new();
    }
}
