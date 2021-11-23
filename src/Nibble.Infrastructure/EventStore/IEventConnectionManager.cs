using EventStore.Client;

namespace Nibble.Infrastructure.EventStore
{
    public interface IEventConnectionManager
    {
        EventStoreClient GetConnection();
    }
}