using Nibble.EventWorker.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.EventWorker.ReadModelStores
{
    public interface IReadModelStore
    {
        public void Add<T>(T model) where T: IReadModel;

    }
}
