using FotballersAPI.Domain.Enums;
using MediatR;

namespace FotballersAPI.Application.Functions.Users.Commands.CreateUserCommand
{
    public class CreateUserCommandRequest : IRequest<Unit>
    {
        public string Username { get; set;}

        public string Password { get; set;}

        public string ConfirmPassword { get; set;}

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public GenderType Gender { get; set; }
    }
}
