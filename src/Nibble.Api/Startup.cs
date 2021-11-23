using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Neo4jClient;
using Nibble.Contracts.Commands;
using Nibble.Domain.Handlers;
using Nibble.Infrastructure;
using Nibble.Infrastructure.EventStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<NeoServerConfiguration>(context => NeoServerConfiguration.GetConfigurationAsync(
                            Configuration.GetValue<Uri>("Neo4j:Uri"),
                            Configuration.GetValue<string>("Neo4j:Username"),
                            Configuration.GetValue<string>("Neo4j:Password")).Result);
            services.AddSingleton<IGraphClientFactory, GraphClientFactory>();
            services.AddMediatR(typeof(CreateCustomerHandler).Assembly,
                                typeof(CreateCustomer).Assembly);
            services.AddSingleton<IEventConnectionManager, EventStoreConnectionManager>();
            services.Configure<EventStoreOptions>(Configuration.GetSection("EventStore"));
            services.AddSingleton<IDomainRepository, EventStoreDomainRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
