using EventStore.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.EventWorker.EventStore
{
    public interface IEventClientManager
    {
        EventStoreClient GetConnection();
    }
}
