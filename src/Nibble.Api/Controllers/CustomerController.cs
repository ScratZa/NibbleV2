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
        public CustomerController(IDomainRepository repository) : base(repository)
        {

        }

        [HttpPost("create")]
        public IActionResult CreateCustomer(CreateCustomer command)
        {
            BuildDomainEntry()
            .ExecuteCommand<CreateCustomer>(command);

            return Ok();
        }

        [HttpPost("address/create")]
        public IActionResult CreateCustomer(AddPrimaryAddressToCustomer command)
        {
            BuildDomainEntry()
            .ExecuteCommand<AddPrimaryAddressToCustomer>(command);

            return Ok();
        }
    }
}
