using Nibble.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.EventWorker
{
    public class EventRouter : IRouter
    {
        private readonly Dictionary<Type, Func<object, Task>> _eventHandlers;

        public EventRouter()
        {
            _eventHandlers = new Dictionary<Type, Func<object, Task>>();
        }

        public void Add<TAction>(Func<TAction,Task> handle)
        {
            _eventHandlers.Add(typeof(TAction), action => handle((TAction)action));
        }

        public async Task<bool> Execute<TAction>(TAction action)
        {
            var type = action.GetType();
            if(_eventHandlers.ContainsKey(type))
            {
                await _eventHandlers[type](action);
                return true;
            }
            return false;
        }
    }
}
