using FotballersAPI.Application.Functions.Users.Commands.CreateUserCommand;
using FotballersAPI.Application.Functions.Users.Commands.LoginCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FotballersAPI.WebHost.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(Unit), 201)]

        public async Task<IActionResult> Create([FromBody] CreateUserCommandRequest request)
        {
            var response = await Mediator.Send(request);
            return Created("test", response);
        }

        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(Unit), 201)]
        public async Task<IActionResult> Post([FromBody] LoginUserCommandRequest request)
        {
            var token = await Mediator.Send(request);
            return Ok(token);
        }
    }
}
