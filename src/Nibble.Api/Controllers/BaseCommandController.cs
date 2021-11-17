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
        protected IMediator _mediator;

        protected BaseCommandController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
