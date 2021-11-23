using Nibble.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Nibble.EventWorker
{
    public interface IRouter
    {
        void Add<TAction>(Func<TAction,Task> @event);
        Task<bool> Execute<TAction>(TAction type);
    }
}