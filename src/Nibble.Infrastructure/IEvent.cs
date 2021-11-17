using System;

namespace Nibble.Infrastructure
{
    public interface IEvent : IComparable
    {
        public Guid Id { get; }
    }
}