using FluentValidation;
using FotballersAPI.Application.Interfaces;
using FotballersAPI.Domain.Data;
using Microsoft.AspNetCore.Identity;

namespace FotballersAPI.Application.Functions.Users.Commands.LoginCommand
{
    public abstract class LoginUserCommandBusinessValidator<T> : AbstractValidator<T>
        where T : LoginUserCommandRequest
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        protected LoginUserCommandBusinessValidator(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;

            RuleFor(x => x.Login)
                .MustAsync(async (username, cancellationToken) =>
                    await _userRepository.CheckLoginExistsInDatabaseAsync(username, cancellationToken))
                .When(x => !string.IsNullOrEmpty(x.Login))
                .WithMessage($"Provided login is incorrect");

            RuleFor(x => x.Password)
                .MustAsync(async (request, _, cancellationToken) =>
                {
                    var user = await _userRepository.GetUserByLoginAsync(request.Login, cancellationToken);

                    if(user is null)
                    {
                        return false;
                    }

                    var correctPassword = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

                    return correctPassword == PasswordVerificationResult.Success
                        ? true
                        : false;
                })
                .WithMessage($"Provided password is incorrect or user does not exists");
        }
    }
}
