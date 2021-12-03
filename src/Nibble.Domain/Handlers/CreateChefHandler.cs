using EventStore.Client;
using MediatR;
using Nibble.Contracts.Commands;
using Nibble.Contracts.Commands.Chef;
using Nibble.Domain.Aggregates;
using Nibble.Domain.Exceptions;
using Nibble.Infrastructure;
using Nibble.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nibble.Domain.Handlers
{
    public class CreateChefHandler : IRequestHandler<CreateChef, IAggregate>
    {
        private readonly IDomainRepository _repository;

        public CreateChefHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public async Task<IAggregate> Handle(CreateChef command, CancellationToken cancellationToken)
        {
            try
            {
                //Need to make this idempotent - consider hashlookup
                if (command.Id == Guid.Empty)
                {
                    command.Id = Guid.NewGuid();
                }
                else
                {
                    await _repository.GetByIdAsync<ChefAggregate>(command.Id);
                    throw new CustomerAlreadyExistsException(command.Id, "Chef Already Exists");
                }
            }
            catch (AggregateNotFoundException)
            {
                //is this the best approach - it seems good
            }
            catch(StreamNotFoundException)
            {
                //is this the best approach - it seems good
            }

            var customer = ChefAggregate.Create(
                command.Id,
                command.FirstName,
                command.LastName,
                command.CookingStyle,
                command.Address.AddressName,
                command.Address.Point[0],
                command.Address.Point[1]);
            await _repository.SaveAsync(customer);
            return customer;
        }
    }
}
