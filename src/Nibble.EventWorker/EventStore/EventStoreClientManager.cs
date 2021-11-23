using EventStore.Client;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.EventWorker.EventStore
{
    internal class EventStoreClientManager : IEventClientManager
    {
        private EventStoreClient _connection;

        public EventStoreClientManager(IOptions<EventStoreOptions> options)
        {
            var settings = EventStoreClientSettings.Create(options.Value.Url);
            _connection = new EventStoreClient(settings);
        }

        public EventStoreClient GetConnection()
        {
            return _connection; 
        }
    }
}
