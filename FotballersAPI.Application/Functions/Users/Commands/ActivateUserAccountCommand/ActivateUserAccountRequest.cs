using MediatR;

namespace FotballersAPI.Application.Functions.Users.Commands.ActivateUserAccountCommand
{
    public class ActivateUserAccountRequest : IRequest<Unit>
    {
        public string UserId { get; set; }
    }
}
