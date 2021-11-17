using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Infrastructure
{
    public abstract class BaseDomainRepository : IDomainRepository
    {
        public abstract Task<TAggragate> GetByIdAsync<TAggragate>(Guid id) where TAggragate : IAggregate, new();

        public abstract Task<IEnumerable<IEvent>> SaveAsync<TAggregate>(TAggregate aggregate) where TAggregate : IAggregate;

        protected int CalculateExpectedVersion<T>(IAggregate aggregate, List<T> events)
        {
            var expected = aggregate.Version - events.Count;
            return expected;
        }

        protected TResult BuildAggregate<TResult>(IEnumerable<IEvent> events) where TResult : IAggregate, new()
        {
            var aggregate = new TResult();

            foreach(var @event in events)
            {
                aggregate.ApplyEvent(@event);
            }
            return aggregate;
        }
    }
}
