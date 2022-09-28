using FotballersAPI.Application.Functions.Users.Commands.ActivateUserAccountCommand;
using FotballersAPI.Application.Functions.Users.Commands.CreateUserCommand;
using FotballersAPI.Application.Functions.Users.Commands.LoginCommand;
using FotballersAPI.Application.Functions.Users.Commands.ResendActivateLinkCommand;
using FotballersAPI.Application.Functions.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FotballersAPI.WebHost.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        [HttpGet]
        [Route("Users")]
        [ProducesResponseType(typeof(GetUsersQueryResponse), 200)]
        public async Task<IActionResult> GetUsers()
        {
            var response = await Mediator.Send(new GetUsersQueryRequest());
            return Ok(response);
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(Unit), 201)]

        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommandRequest request)
        {
            var response = await Mediator.Send(request);
            return Created("test", response);
        }

        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(Unit), 201)]
        public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest request)
        {
            var token = await Mediator.Send(request);
            return Ok(token);
        }

        [HttpPost]
        [Route("Activate/{hashedId}")]
        [ProducesResponseType(typeof(Unit), 201)]
        public async Task<IActionResult> ActivateUser([FromRoute] string hashedId)
        {            
            await Mediator.Send(new ActivateUserAccountRequest { UserId = hashedId});
            return Ok();
        }        

        [HttpPost]
        [Route("ResendActivateLink/{email}")]
        [ProducesResponseType(typeof(Unit), 201)]
        public async Task<IActionResult> ResendActivateEmail([FromRoute] string email)
        {
            await Mediator.Send(new ResendActivateLinkCommandRequest { Email = email });
            return Ok();
        }

    }
}
