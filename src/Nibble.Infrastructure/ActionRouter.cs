using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Infrastructure
{
    public class ActionRouter
    {
        private Dictionary<Type, Action<object>> _routes = new Dictionary<Type, Action<object>>();

        public void Add<TAction>(Action<TAction> handle)
        {
            _routes.Add(typeof(TAction), action => handle((TAction)action));
        }

        public bool Execute<TAction>(TAction action)
        {
            var actionType = action.GetType();
            if (_routes.ContainsKey(actionType))
            {
                _routes[actionType](action);
                return true;
            }
            return false;
        }
    }
}
