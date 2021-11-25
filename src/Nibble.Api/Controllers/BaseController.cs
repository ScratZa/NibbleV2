using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Neo4jClient;
using Nibble.Domain;
using Nibble.Infrastructure;
using System.Threading;

namespace Nibble.Api.Controllers
{
    [Route("command/api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly IGraphClientFactory _queryFactory;
        protected BaseController(IMediator mediator, IGraphClientFactory factory)
        {
            _mediator = mediator;
            _queryFactory = factory;
        }
    }
}
