using FotballersAPI.Application.Functions.Users.Commands.CreateUserCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FotballersAPI.WebHost.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        [HttpPost]
        [ProducesResponseType(typeof(Unit), 201)]

        public async Task<IActionResult> Create([FromBody] CreateUserCommandRequest request)
        {
            var response = await Mediator.Send(request);
            return Created("test",response);
        }
    }
}
