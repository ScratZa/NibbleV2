using EventStore.Client;
using Nibble.Infrastructure.EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Nibble.Infrastructure
{
    public class EventStoreDomainRepository : BaseDomainRepository
    {
        private IEventConnectionManager _connectionManager;
        
        public EventStoreDomainRepository(IEventConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        private string AggregateStreamName(Guid id, StreamType Category)
        {
            return string.Format("{0}-{1}", Category.Value, id);
        }

        public async override Task<TAggragate> GetByIdAsync<TAggragate>(Guid id)
        {
            var connection = _connectionManager.GetConnection();
            var streamName = AggregateStreamName(id, StreamType.Default);
            var eventsSlice = connection.ReadStreamAsync(Direction.Forwards, streamName, 0, int.MaxValue);
            List<IEvent> events = new List<IEvent>();
            await foreach(var e in eventsSlice)
            {
                var metaData = DeserializeObject<Dictionary<string, string>>(e.OriginalEvent.Metadata.Span);
                var eventData = DeserializeObject(e.OriginalEvent.Data.Span, metaData[EventMetadata.TypeHeader.Value]);
                events.Add(eventData as IEvent);
            }
            return BuildAggregate<TAggragate>(events);
        }
        private T DeserializeObject<T>(ReadOnlySpan<byte> data)
        {
            return (T)(DeserializeObject(data, typeof(T).AssemblyQualifiedName));
        }

        private object DeserializeObject(ReadOnlySpan<byte> data, string typeName)
        {
            var jsonString = Encoding.UTF8.GetString(data);
            return JsonSerializer.Deserialize(jsonString, Type.GetType(typeName));
        }
        public async override Task<IEnumerable<IEvent>> SaveAsync<TAggregate>(TAggregate aggregate)
        {
            var connection = _connectionManager.GetConnection();
            var events = aggregate.GetNewEvents().ToList();
            var expectedVersion = CalculateExpectedVersion(aggregate, events);
            var eventData = events.Select(CreateEventData);
            var streamName = AggregateStreamName(aggregate.Id,StreamType.Default);
            var resp = await connection.AppendToStreamAsync(streamName, StreamRevision.FromInt64(expectedVersion), eventData);
            return events.AsEnumerable();
        }

        public EventData CreateEventData(object @event)
        {
            var eventHeaders = new Dictionary<string, string>()
            {
                {
                    EventMetadata.TypeHeader.Value, @event.GetType().AssemblyQualifiedName
                }
            };
            var eventDataHeaders = SerializeObject(eventHeaders);
            var data = SerializeObject(@event);
            var eventData = new EventData(Uuid.NewUuid(), @event.GetType().Name, data, eventDataHeaders);
            return eventData;
        }

        private byte[] SerializeObject<T>(T eventHeaders)
        {
            return JsonSerializer.SerializeToUtf8Bytes(eventHeaders);
        }
    }
}

