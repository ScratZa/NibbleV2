using EventStore.Client;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Infrastructure.EventStore
{

    public class EventStoreConnectionManager : IEventConnectionManager
    {
        private EventStoreClient _connection;

        public EventStoreConnectionManager(IOptions<EventStoreOptions> options)
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
