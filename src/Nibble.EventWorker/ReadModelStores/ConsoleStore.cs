using Nibble.EventWorker.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Nibble.EventWorker.ReadModelStores
{
    public class ConsoleStore : IReadModelStore
    {
        public void Add<T>(T model) where T : IReadModel
        {
            Console.WriteLine($"ReadModel Persisted {model.GetType()}");
            Console.WriteLine(JsonSerializer.Serialize(model));
        }
    }
}
