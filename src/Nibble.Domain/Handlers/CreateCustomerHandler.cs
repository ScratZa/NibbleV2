using Nibble.Contracts.Commands;
using Nibble.Domain.Aggregates;
using Nibble.Domain.Exceptions;
using Nibble.Infrastructure;
using Nibble.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Domain.Handlers
{
    public class CreateCustomerHandler : IHandle<CreateCustomer>
    {
        private readonly IDomainRepository _repository;

        public CreateCustomerHandler(IDomainRepository repository)
        {
            _repository = repository;
        }
        public async Task<IAggregate> HandleAsync(CreateCustomer command)
        {
            try
            {
                await _repository.GetByIdAsync<CustomerAggregate>(command.Id);
                throw new CustomerAlreadyExistsException(command.Id, "Customer Already Exists");
            }
            catch(AggregateNotFoundException)
            {
                //is this the best approach - it seems good
            }

            var customer =  CustomerAggregate.Create(command.Id, command.FirstName, command.LastName);
            await _repository.SaveAsync(customer);
            return customer;
        }
    }
}
