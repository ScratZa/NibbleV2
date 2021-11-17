using System;
using System.Collections.Generic;

namespace Nibble.Infrastructure
{
    public interface IAggregate
    { 
        int Version { get; }
        Guid Id { get; }

        void ClearEvents();
        IEnumerable<IEvent> GetNewEvents();
        void ApplyEvent(IEvent @event);
    }
}