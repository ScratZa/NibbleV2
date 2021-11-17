using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibble.Infrastructure
{
    public class CommandDispatcher
    {
        private IDomainRepository _domainRepository;
        private readonly IEnumerable<Action<object>> _postExecutionPipe;
        private readonly IEnumerable<Action<IRequest>> _preExecutionPipe;
        private ActionRouter _actionRouter;

        public CommandDispatcher(IDomainRepository domainRepository, IEnumerable<Action<IRequest>> preExecutionPipe, IEnumerable<Action<object>> postExecutionPipe)
        {
            _domainRepository = domainRepository;
            _postExecutionPipe = postExecutionPipe;
            _preExecutionPipe = preExecutionPipe ?? Enumerable.Empty<Action<IRequest>>();
            _actionRouter = new ActionRouter();
        }

        public async void RegisterHandler<TCommand>(IHandle<TCommand> handler) where TCommand : IRequest
        {
            _actionRouter.Add<TCommand>(command => ExecuteCommand(command, handler.HandleAsync));
        }

        public void ExecuteCommand<TCommand>(TCommand command)
        {
            if (!_actionRouter.Execute<TCommand>(command))
            {
                var commandType = typeof(TCommand);
                throw new ApplicationException("Missing handler for " + commandType.Name);
            }
        }

        public async void ExecuteCommand<TCommand>(TCommand command, Func<TCommand, Task<IAggregate>> handler) where TCommand : IRequest
        {
            RunPreExecutionPipe(command);
            var aggregate = await handler(command);
            var savedEvents = await _domainRepository.SaveAsync(aggregate);
            RunPostExecutionPipe(savedEvents);
        }

        private void RunPostExecutionPipe(IEnumerable<object> savedEvents)
        {
            foreach (var savedEvent in savedEvents)
            {
                foreach (var action in _postExecutionPipe)
                {
                    action(savedEvent);
                }
            }
        }

        private void RunPreExecutionPipe(IRequest command)
        {
            foreach (var action in _preExecutionPipe)
            {
                action(command);
            }
        }
    }
}
