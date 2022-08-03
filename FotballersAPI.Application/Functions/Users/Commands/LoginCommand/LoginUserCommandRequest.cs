using MediatR;

namespace FotballersAPI.Application.Functions.Users.Commands.LoginCommand
{
    public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}
