using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Nibble.Infrastructure.Exceptions;
using Newtonsoft.Json;

namespace Nibble.Infrastructure
{
    public class InMemoryDomainRepository : BaseDomainRepository
    {
        private List<IEvent> _recentEvents = new List<IEvent>();
        private Dictionary<Guid, List<string>> _eventStore = new Dictionary<Guid, List<string>>();
        private JsonSerializerSettings _serializationSettings;
        public InMemoryDomainRepository()
        {
            _serializationSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
        }
        public override Task<TResult> GetByIdAsync<TResult>(Guid id)
        {
            if(_eventStore.ContainsKey(id))
            {
                var eventList = _eventStore[id];
                var events = eventList.Select(x => JsonConvert.DeserializeObject(x, _serializationSettings) as IEvent);
                return Task.FromResult(BuildAggregate<TResult>(events));
            }
            //Todo implement domain exception
            throw new AggregateNotFoundException("Cannot find aggregate for id");
        }

        public override Task<IEnumerable<IEvent>> SaveAsync<TAggregate>(TAggregate aggregate)
        {
            var eventsToApply = aggregate.GetNewEvents();
            var eventsList = eventsToApply.Select(Serialize).ToList();
            var expectedVersion = CalculateExpectedVersion(aggregate, eventsList);

            if(expectedVersion <0)
            {
                _eventStore.Add(aggregate.Id, eventsList);
            }
            else
            {
                var existingEvents = _eventStore[aggregate.Id];
                var currentVersion = existingEvents.Count - 1; //accomodates for the -1 version

                if(currentVersion != expectedVersion)
                {
                    //Todo implement some domain exceptions here.
                    throw new Exception("Current version and expected version Missmatch");
                }

                existingEvents.AddRange(eventsList);
            }

            _recentEvents.AddRange(eventsToApply);
            aggregate.ClearEvents();
            return Task.FromResult(eventsToApply);
        }

        public IEnumerable<IEvent> GetRecentEvents()
        {
            return _recentEvents;
        }

        private string Serialize(IEvent @event)
        {
            return JsonConvert.SerializeObject(@event, _serializationSettings);
        }

        public void AddEvents(Dictionary<Guid,IEnumerable<IEvent>> eventsForAggregate)
        {
            foreach(var eventForAggregate in eventsForAggregate)
            {
                _eventStore.Add(eventForAggregate.Key, eventForAggregate.Value.Select(Serialize).ToList());
            }
        }

    }
}
