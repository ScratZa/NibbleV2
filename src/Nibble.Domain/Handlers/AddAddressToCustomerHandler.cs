using MediatR;
using Nibble.Contracts.Commands;
using Nibble.Domain.Aggregates;
using Nibble.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nibble.Domain.Handlers
{
    public class AddAddressToCustomerHandler : IRequestHandler<AddPrimaryAddressToCustomer, IAggregate>
    {
        private readonly IDomainRepository _repository;

        public AddAddressToCustomerHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public async Task<IAggregate> Handle(AddPrimaryAddressToCustomer command, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync<CustomerAggregate>(command.Id);
            customer.AddAddress(command.Name, command.Lat, command.Lon);
            await _repository.SaveAsync(customer);
            return customer;
        }

        public async Task<IAggregate> HandleAsync(AddPrimaryAddressToCustomer command)
        {
            var customer = await _repository.GetByIdAsync<CustomerAggregate>(command.Id);
            customer.AddAddress(command.Name, command.Lat, command.Lon);
            await _repository.SaveAsync(customer);
            return customer;
        }
    }
}
