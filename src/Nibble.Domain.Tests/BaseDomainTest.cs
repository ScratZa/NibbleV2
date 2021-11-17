using Nibble.Infrastructure;
using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Moq;
using System.Threading;
using Nibble.Contracts.Commands;
using Nibble.Domain.Handlers;
using System.Threading.Tasks;

namespace Nibble.Domain.Tests
{
    public class BaseDomainTest : IDisposable
    {
        InMemoryDomainRepository _repository;
        private Dictionary<Type, Type> _handlers; 
        private Dictionary<Guid, IEnumerable<IEvent>> _preEvents = new Dictionary<Guid, IEnumerable<IEvent>>();
        private DomainEntry _domainEntry;
        public BaseDomainTest()
        {
            BuildHandlers();
        }

        private void BuildHandlers()
        {
            _handlers = new Dictionary<Type, Type>();
            _handlers.Add(typeof(AddPrimaryAddressToCustomer), typeof(AddAddressToCustomerHandler));
            _handlers.Add(typeof(CreateCustomer), typeof(CreateCustomerHandler));
        }

        private IHandle<T> GetHandler<T>(T command, params object[] constructurArguments) where T : Infrastructure.IRequest
        {
            var handlerType = _handlers[typeof(T)];
            var handler =  (IHandle<T>)Activator.CreateInstance(handlerType, constructurArguments);
            return handler;
        }

        public IDomainRepository GetRepository()
        {
            return _repository;
        }
        public void Dispose()
        {
            IdGenerator.GenerateId = null;
            _preEvents = new Dictionary<Guid, IEnumerable<IEvent>>();
        }
        protected IMediator BuildMediator<TCommand, THandler>(TCommand command, THandler handler) 
            where TCommand : Infrastructure.IRequest
            where THandler : IHandle<TCommand>
        {
            var mediatR = new Mock<IMediator>();
            
            mediatR.Setup(m => m.Send(It.IsAny<TCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(handler.HandleAsync(command));

            return mediatR.Object;
        }

        public async Task When<TCommand>(TCommand command) 
            where TCommand : Infrastructure.IRequest
        {
            BuildRepository();
            var handler = GetHandler(command, _repository);
            var mediatR = BuildMediator(command, handler);
            await mediatR.Send(command);
        }

        protected void Then(params IEvent[] expectedEvents)
        {
            var latestEvents = _repository.GetRecentEvents().ToList();
            var expectedEventList = expectedEvents.ToList();
            Assert.Equal(expectedEventList.Count, latestEvents.Count);

            for (int i = 0; i < latestEvents.Count; i++)
            {
                Assert.Equal(expectedEvents[i], latestEvents[i]);
            }
        }

        public IDomainRepository BuildRepository()
        {
            _repository = new InMemoryDomainRepository();
            _repository.AddEvents(_preEvents);
            return _repository;
        }
        protected void WhenThrows<TException, TCommand>(TCommand command)
                where TException : Exception
                where TCommand : Infrastructure.IRequest
        {
            Assert.ThrowsAsync<TException>( async () =>  await When(command));
        }

        protected void Given(params IEvent[] existingEvents)
        {
            _preEvents = existingEvents
                .GroupBy(y => y.Id)
                .ToDictionary(y => y.Key, y => y.AsEnumerable());
        }
    }
}
