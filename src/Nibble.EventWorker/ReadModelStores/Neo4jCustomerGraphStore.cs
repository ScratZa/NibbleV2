using Nibble.Contracts.Events;
using Nibble.EventWorker.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;

namespace Nibble.EventWorker.ReadModelStores
{
    public class Neo4jCustomerGraphStore : ICustomerGraphStore
    {
        private readonly IGraphClientFactory _factory;
        public Neo4jCustomerGraphStore(IGraphClientFactory facotry)
        {
            _factory = facotry;
        }

        public async Task InitializeStore()
        {
            using (var _graphClient = await _factory.CreateAsync())
            {
                await _graphClient.Cypher.Match("(n)")
                             .OptionalMatch("(n)-[r]-()")
                             .Delete("n,r")
                             .ExecuteWithoutResultsAsync();
            }
        }

        public async Task AddCustomer(Customer customer)
        {
            using (var _graphClient = await _factory.CreateAsync())
            {
                await _graphClient.Cypher
                    .Create("(customer:Customer $newCustomer)")
                    .WithParam("newCustomer", customer)
                    .ExecuteWithoutResultsAsync();
            }
        }

        public async Task AddCustomerAddress(Guid Id, Address address)
        {
            using (var _graphClient = await _factory.CreateAsync())
            {
                await  _graphClient.Cypher
                       .Match("(customer:Customer)")
                       .Where((Customer customer) => customer.Id == Id)
                       .Create("(customer)-[:HAS_ADDRESS]->(address: Address $address)")
                       .WithParam("address", address)
                       .ExecuteWithoutResultsAsync();
            }
        }

        public Task<Customer> GetCustomer(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
