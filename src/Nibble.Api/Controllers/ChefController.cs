using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using Nibble.Contracts.Commands;
using Nibble.Contracts.Commands.Chef;
using Nibble.ReadModels;
using System;
using System.Threading.Tasks;
using Nibble.Mappers;
using System.Linq;

namespace Nibble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefController : BaseController
    {
        public ChefController(IMediator mediator, IGraphClientFactory queryFactory) : base(mediator, queryFactory)
        {

        }

        [HttpGet("list")]
        public async Task<IActionResult> AllChefs()
        {
            using (var client = await _queryFactory.CreateAsync())
            {
                var response = await client.Cypher
                    .Match("(chef:Chef)")
                    .Return(chef => chef.As<Chef>())
                    .ResultsAsync;

                return Ok(response.ToChefListItems());
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Chef([FromRoute]Guid id)
        {
            using (var client = await _queryFactory.CreateAsync())
            {
                var response = await client.Cypher
                    .Match("(chef:Chef)")
                    .Where((Chef chef) => chef.Id == id)
                    .ReturnDistinct((chef) => chef.As<Chef>())
                    .ResultsAsync;

                return Ok(response.Single());
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateChef(CreateChef command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("meal/add")]
        public async Task<IActionResult> AddMeal([FromBody] AddMealToChef command)
        {
            command.ChefId = GetChefId();
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("meal/remove")]
        public async Task<IActionResult> RemoveMeal([FromBody] RemoveMealFromChef command)
        {
            command.ChefId = GetChefId();
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
