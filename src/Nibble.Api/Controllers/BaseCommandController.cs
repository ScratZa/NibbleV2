using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nibble.Domain;
using Nibble.Infrastructure;
using System.Threading;

namespace Nibble.Api.Controllers
{
    [Route("command/api/[controller]")]
    [ApiController]
    public class BaseCommandController : ControllerBase
    {
        protected IDomainRepository _repository;

        protected BaseCommandController(IDomainRepository repository)
        {
            _repository = repository;
        }

        protected DomainEntry BuildDomainEntry()
        {
            return new DomainEntry(_repository);
        }
    }
}
