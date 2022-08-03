using FluentValidation;
using FotballersAPI.Application.Interfaces;

namespace FotballersAPI.Application.Functions.Users.Commands.CreateUserCommand
{
    public abstract class CreateUserCommandBusinessValidator<T> : AbstractValidator<T>
        where T : CreateUserCommandRequest
    {
        private readonly IUserRepository _userRepository;

        protected CreateUserCommandBusinessValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Email)
                .MustAsync(async (email, cancellationToken) =>
                    !await _userRepository.CheckEmailExistsInDatabaseAsync(email, cancellationToken))
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage(x => $"Provided email '{x.Email}' already exists");

            RuleFor(x => x.Username)
                .MustAsync(async (username, cancellationToken) =>
                    !await _userRepository.CheckLoginExistsInDatabaseAsync(username, cancellationToken))
                .When(x => !string.IsNullOrEmpty(x.Username))
                .WithMessage(x => $"Provided username '{x.Username}' already exists");
        }
    }
}
