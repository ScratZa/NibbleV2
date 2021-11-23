using Nibble.Contracts.Events;
using Nibble.EventWorker.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.EventWorker.ReadModelStores
{
    public interface ICustomerGraphStore
    {
        public Task InitializeStore();
        public Task<Customer> GetCustomer(Guid id);
        public Task AddCustomer(Customer customer);
        public Task AddCustomerAddress(Guid Id, Address address);
    }
}
