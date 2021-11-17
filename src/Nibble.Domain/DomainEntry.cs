using Nibble.Domain.Handlers;
using Nibble.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Domain
{
    public class DomainEntry
    {
        private readonly CommandDispatcher _dispatcher;

        public DomainEntry(IDomainRepository repository, 
            IEnumerable<Action<IRequest>> preExecutions = null, 
            IEnumerable<Action<object>> postExecutions = null)
        {
            preExecutions = preExecutions ?? Enumerable.Empty<Action<IRequest>>();
            postExecutions = CreatePostExecutions(postExecutions);
            _dispatcher = CreateCommandDispatcher(repository, preExecutions, postExecutions);
        }

        public void ExecuteCommand<TCommand>(TCommand command) where TCommand : IRequest
        {
            _dispatcher.ExecuteCommand(command);
        }

        private CommandDispatcher CreateCommandDispatcher(IDomainRepository domainRepository, IEnumerable<Action<IRequest>> preExecutions, IEnumerable<Action<object>> postExecutions)
        {
            var commandDispatcher = new CommandDispatcher(domainRepository, preExecutions, postExecutions);

            var customerCommandHandler = new CreateCustomerHandler(domainRepository);
            commandDispatcher.RegisterHandler(customerCommandHandler);
            var addressCommandHandler = new AddAddressToCustomerHandler(domainRepository);
            commandDispatcher.RegisterHandler(addressCommandHandler);

            return commandDispatcher;
        }

        private IEnumerable<Action<object>> CreatePostExecutions(IEnumerable<Action<object>> postExecutions)
        {
           if(postExecutions!=null)
            {
                foreach(var execution in postExecutions)
                {
                    yield return execution;
                }
            }
        }
    }
}
