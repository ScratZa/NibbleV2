using Nibble.Contracts.Commands;
using Nibble.Contracts.Events;
using Nibble.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Nibble.Domain.Tests.Customer
{
    public class CustomerTests : BaseDomainTest
    {
        [Fact]
        public async void WhenCreatingTheCustomer_TheCustomerShouldBeCreatedWithTheRightName()
        {
            Guid id = Guid.NewGuid();

            await When(new CreateCustomer { Id = id, FirstName = "Vinni", LastName = "Jones" });

            Then(new CustomerCreated(id, "Vinni", "Jones"));
        }

        [Fact]
        public void WhenCreatingTheCustomerThatExists_ACustomerAlreadyExistsExceptionShouldBeRaised()
        {
            Guid id = Guid.NewGuid();

            Given(new CustomerCreated(id, "test", "name"));

            WhenThrows<CustomerAlreadyExistsException, CreateCustomer>(new CreateCustomer { Id = id, FirstName = "", LastName = "" });
        }

    }
}
