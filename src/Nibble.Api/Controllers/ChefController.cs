using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using Nibble.Contracts.Commands;
using Nibble.Contracts.Commands.Chef;
using System.Threading.Tasks;

namespace Nibble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefController : BaseController
    {
        public ChefController(IMediator mediator, IGraphClientFactory queryFactory) : base(mediator, queryFactory)
        {

        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateChef(CreateChef command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
