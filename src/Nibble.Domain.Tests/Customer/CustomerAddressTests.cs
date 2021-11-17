using Nibble.Contracts.Commands;
using Nibble.Contracts.Events;
using Nibble.Domain.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Nibble.Domain.Tests.Customer
{
    public class CustomerAddressTests : BaseDomainTest
    {
        [Fact]
        public async void GivenACustomer_WhenAddingAnAddress_AddressShouldBeAdded()
        {
            Guid id = Guid.NewGuid();
            Given(new CustomerCreated(id, "test", "name"));

            await When(new AddPrimaryAddressToCustomer(id, "primary", 22.1, 23.0));

            Then(new PrimaryAddressAdded(id, "primary", 22.1, 23.0));
        }
    }
}
