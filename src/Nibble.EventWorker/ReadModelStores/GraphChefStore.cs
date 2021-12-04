using Nibble.Contracts.Events;
using Nibble.EventWorker.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;
using Nibble.ReadModels;

namespace Nibble.EventWorker.ReadModelStores
{
    public class GraphChefStore : IChefStore
    {
        private readonly IGraphClientFactory _factory;
        public GraphChefStore(IGraphClientFactory facotry)
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

        public async Task<Chef> GetChef(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task AddChef(Chef chef)
        {
            using (var _graphClient = await _factory.CreateAsync())
            {
                await _graphClient.Cypher
                    .Create("(chef:Chef $newChef)")
                    .WithParam("newChef", chef)
                    .ExecuteWithoutResultsAsync();
            }
        }
    }
}
