using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Infrastructure
{
    public class BaseAggregate : IAggregate
    {
        private int _version =-1;
        public int Version
        {
            get
            {
                return _version;
            }
            protected set
            {
                _version = value;
            }
        }

        public Guid Id { get; protected set; }

        private List<IEvent> _uncomittedEvents = new List<IEvent>();
        private Dictionary<Type, Action<IEvent>> _eventHandlers = new Dictionary<Type, Action<IEvent>>();

        //Todo: double check this it seems wonky
        public void RaiseEvent(IEvent @event)
        {
            ApplyEvent(@event);
            _uncomittedEvents.Add(@event);
        }

        protected void AddEventHandler<T>(Action<T> eventHandler) where T : class
        {
            _eventHandlers.Add(typeof(T), o => eventHandler(o as T));
        }
        public void ApplyEvent(IEvent @event)
        {
            var type = @event.GetType();
            if (_eventHandlers.ContainsKey(type))
            {
                _eventHandlers[type](@event);
            }
            Version++;
        }

        public void ClearEvents()
        {
            _uncomittedEvents.Clear();
        }

        public IEnumerable<IEvent> GetNewEvents()
        {
            return _uncomittedEvents;
        }
    }
}
