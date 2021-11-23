using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Neo4jClient;
using Nibble.EventWorker.EventStore;
using Nibble.EventWorker.ReadModelStores;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nibble.EventWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var applicationConfig = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true)
                                .Build();

             var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<NeoServerConfiguration>(context => NeoServerConfiguration.GetConfigurationAsync(
                        applicationConfig.GetValue<Uri>("Neo4j:Uri"),
                        applicationConfig.GetValue<string>("Neo4j:Username"),
                        applicationConfig.GetValue<string>("Neo4j:Password")).Result);
                    services.AddSingleton<IGraphClientFactory, GraphClientFactory>();
                    services.AddTransient<ICustomerGraphStore, Neo4jCustomerGraphStore>();
                    services.Configure<EventStoreOptions>(applicationConfig.GetSection("EventStore"));
                    services.AddSingleton<IEventClientManager, EventStoreClientManager>();
                    services.AddTransient<IReadModelStore, ConsoleStore>();
                    services.AddTransient<IRouter, EventRouter>();
                    services.AddHostedService<Worker>();
                });
            return host;
        }
    }
}
