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
    internal class AddMealToChefHandler : IRequestHandler<AddMealToChef, IAggregate>
    {
        private IDomainRepository _domainRepository;
        public AddMealToChefHandler(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository; 
        }
        public async Task<IAggregate> Handle(AddMealToChef request, CancellationToken cancellationToken)
        {
            try
            {
                var chefAggregate = await _domainRepository.GetByIdAsync<ChefAggregate>(request.ChefId);
                var chef = chefAggregate.AddMeal(request.Name, request.Description, request.Price, request.Tags, request.Ingredients);
                await _domainRepository.SaveAsync(chefAggregate);
                return chef;
            }
            catch(Exception ex)
            {
                //Do something here;
                throw;
            }
        }
    }
}
