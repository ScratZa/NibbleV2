using EventStore.Client;
using Nibble.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Nibble.EventWorker
{
    public static class EventSerializer
    {
        public static string EventTypeKey = "EventType";

        public static byte[] Serialize(object metadata)
        {
            return JsonSerializer.SerializeToUtf8Bytes(metadata);
        }
        public static object DeserializeEvent(EventData @event)
        {
            var data = @event.Metadata.Span;
            var metaData = Encoding.UTF8.GetString(data);

            var metaDataList = JsonSerializer.Deserialize<Dictionary<string, string>>(metaData);

            if (metaDataList is not null && metaDataList.ContainsKey(EventTypeKey))
            {
                var eventType = metaDataList[EventTypeKey];
                return DeserializeEvent(@event.Data.Span, eventType);
            };
            return null;
        }

        public static object DeserializeEvent(ResolvedEvent @event)
        {
            var data = @event.OriginalEvent.Metadata.Span;
            var metaData = Encoding.UTF8.GetString(data);

            var metaDataList = JsonSerializer.Deserialize<Dictionary<string, string>>(metaData);

            if (metaDataList is not null && metaDataList.ContainsKey(EventMetadata.TypeHeader.Value))
            {
                var eventType = metaDataList[EventMetadata.TypeHeader.Value];
                return DeserializeEvent(@event.OriginalEvent.Data.Span, eventType);
            };
            return null;
        }
        private static object DeserializeEvent(ReadOnlySpan<byte> data, string eventType)
        {
            var dataString = Encoding.UTF8.GetString(data);
            try
            {
                return JsonSerializer.Deserialize(dataString, Type.GetType(eventType));
            }
            catch (JsonException)
            {
                return null;
            }
        }
    }
}
