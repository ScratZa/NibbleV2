using EventStore.Client;
using Moq;
using Nibble.Contracts.Events;
using System;
using System.Collections.Generic;
using Xunit;

namespace Nibble.EventWorker.Tests
{
    public class EventSerializerTests
    {

        [Fact]
        public void WhenSerializingAnEvent_ThenShouldDeserializeToSameType()
        {
            var @event = new CustomerCreated()
            {
                Id = Guid.NewGuid(),
                FirstName = "Test",
                LastName = "Last"
            };
            var eventMetadata = new Dictionary<string, string>
            {
               {EventSerializer.EventTypeKey, @event.GetType().AssemblyQualifiedName.ToString()}
            };

            var eventMetaDataString = EventSerializer.Serialize(eventMetadata);
            var eventData = EventSerializer.Serialize(@event);

            var eventRecord = new EventData(Uuid.NewUuid(), "type", eventData, eventMetaDataString);
            var DeserializedEvent = EventSerializer.DeserializeEvent(eventRecord);
            var ExplicitDeserializedEvent = DeserializedEvent as CustomerCreated;

           
            Assert.Equal(@event.GetType(), DeserializedEvent.GetType());
            Assert.Equal(@event.Id, ExplicitDeserializedEvent.Id);
        }
    }
}
