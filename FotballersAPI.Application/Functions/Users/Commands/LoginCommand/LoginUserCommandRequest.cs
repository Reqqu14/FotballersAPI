using MediatR;

namespace FotballersAPI.Application.Functions.Users.Commands.LoginCommand
{
    public class LoginUserCommandRequest : IRequest<Unit>
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}
