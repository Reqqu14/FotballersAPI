using FotballersAPI.Application.Interfaces;
using FotballersAPI.Domain.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FotballersAPI.Application.Functions.Users.Commands.LoginCommand
{
    public class LoginUserCommand
    {
        public class ModelValidator : LoginUserCommandModelValidator<LoginUserCommandRequest>
        {

        }

        public class BusinessValidator : LoginUserCommandBusinessValidator<LoginUserCommandRequest>
        {
            public BusinessValidator(IUserRepository userRepository, IPasswordHasher<User> passwordHasher) : base(userRepository, passwordHasher)
            {

            }
        }

        public class Handler : IRequestHandler<LoginUserCommandRequest, Unit>
        {
            public async Task<Unit> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
            {
                // Token Generation future implementation
                return Unit.Value;
            }
        }
    }
}
