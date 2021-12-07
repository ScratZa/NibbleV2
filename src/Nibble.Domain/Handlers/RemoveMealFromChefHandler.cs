using MediatR;
using Nibble.Contracts.Commands.Chef;
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
    internal class RemoveMealFromChefHandler : IRequestHandler<RemoveMealFromChef, IAggregate>
    {
        private IDomainRepository _domainRepository;
        public RemoveMealFromChefHandler(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }
        public async Task<IAggregate> Handle(RemoveMealFromChef request, CancellationToken cancellationToken)
        {
            try
            {
                var chefAggregate = await _domainRepository.GetByIdAsync<ChefAggregate>(request.ChefId);
                var chef = chefAggregate.RemoveMeal(request.Id);
                await _domainRepository.SaveAsync(chefAggregate);
                return chef;
            }
            catch (Exception ex)
            {
                //Do something here;
                throw;
            }
        }
    }
}
