using EventStore.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nibble.Contracts.Events;
using Nibble.Contracts.Events.Chef;
using Nibble.EventWorker.EventStore;
using Nibble.EventWorker.ReadModels;
using Nibble.EventWorker.ReadModelStores;
using Nibble.Infrastructure;
using Nibble.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Nibble.EventWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEventClientManager _eventClientManager;
        private readonly IRouter _router;
        private readonly ICustomerGraphStore _customerStore;
        private readonly IChefStore _chefStore;
        public Worker(ILogger<Worker> logger, IEventClientManager client, IRouter router, ICustomerGraphStore customerStore, IChefStore chefStore)
        {
            _eventClientManager = client;
            _logger = logger;
            _router = router;
            _customerStore = customerStore;
            _chefStore = chefStore;
            SetupRouteHandlers();
        }

        private void SetupRouteHandlers()
        {
            _router.Add<CustomerCreated>(HandleCustomerCreated);
            _router.Add<PrimaryAddressAdded>(HandleAddressAdded);
            _router.Add<ChefCreated>(HandleChefCreated);
        }
        private async Task HandleChefCreated(ChefCreated obj)
        {
            var chef = new Chef
            {
                Id = obj.Id,
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                Address = obj.AddressName,
                Latitude = obj.Latitude,
                Longitude = obj.Longitude,
                CookingStyle = obj.CookingStyle
            };

            try
            {
                await _chefStore.AddChef(chef);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private async Task HandleCustomerCreated(CustomerCreated obj)
        {
            var customer = new Customer
            {
                Id = obj.Id,
                Name = obj.FirstName,
                LastName = obj.LastName
            };

            try
            {
                await _customerStore.AddCustomer(customer);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task HandleAddressAdded(PrimaryAddressAdded obj)
        {
            var address = new Address
            {
                Name = obj.Name,
                Latitude = obj.Lat,
                Longitude = obj.Lon
            };

            try
            {
                await _customerStore.AddCustomerAddress(obj.Id,address);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _customerStore.InitializeStore();
            await ConnectToEventStreams();

            while (true)
            {
                Console.WriteLine("Healthy");
                await Task.Delay(10000);
            }
        }

        private async Task ConnectToEventStreams()
        {
            var connection = _eventClientManager.GetConnection();
            var prefixStreamFilter = new SubscriptionFilterOptions(StreamFilter.Prefix(StreamType.Default.Value+"-"));
            await connection.SubscribeToAllAsync(async (subscription, @event, CancellationToken) =>
            {
                Console.WriteLine($"Event Received from subscription {subscription.SubscriptionId} - event {@event.Event.EventType}");
                await HandleEvent(@event);
            },filterOptions: prefixStreamFilter);
        }

        private async Task HandleEvent(ResolvedEvent @event)
        {
            var evt = EventSerializer.DeserializeEvent(@event);
            await _router.Execute(evt);
        }
    }
}
