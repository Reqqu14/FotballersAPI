using MediatR;

namespace FotballersAPI.Application.Functions.Users.Commands.ResendActivateLinkCommand
{
    public class ResendActivateLinkCommandRequest : IRequest<Unit>
    {
        public string Email { get; set; }
    }
}
