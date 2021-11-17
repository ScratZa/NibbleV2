using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nibble.Contracts.Commands;
using Nibble.Infrastructure;
using System.Threading.Tasks;

namespace Nibble.Api.Controllers
{
    [Route("command/api/[controller]")]
    [ApiController]
    public class CustomerController : BaseCommandController
    {
        public CustomerController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomer(CreateCustomer command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("address/create")]
        public async Task<IActionResult> CreateCustomer(AddPrimaryAddressToCustomer command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
