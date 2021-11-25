using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using Nibble.Contracts.Commands;
using Nibble.EventWorker.ReadModels;
using Nibble.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Nibble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {
        public CustomerController(IMediator mediator, IGraphClientFactory queryFactory ) : base(mediator, queryFactory)
        {

        }
        #region Queries
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCustomer([FromRoute]Guid Id)
        {
            using(var client = await _queryFactory.CreateAsync())
            {
                var response = await client.Cypher
                    .OptionalMatch("(customer:Customer)-[:HAS_ADDRESS]-(address:Address)")
                    .Where((Customer customer) => customer.Id == Id)
                    .Return((customer, address) => new
                    {
                        Customer = customer.As<Customer>(),
                        Address = address.As<Address>()
                    })
                    .ResultsAsync;

                return Ok(response);  
            }
        }
        #endregion
        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomer(CreateCustomer command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("address")]
        public async Task<IActionResult> AddAddress(AddPrimaryAddressToCustomer command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
