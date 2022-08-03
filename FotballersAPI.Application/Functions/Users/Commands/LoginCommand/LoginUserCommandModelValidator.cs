using FluentValidation;

namespace FotballersAPI.Application.Functions.Users.Commands.LoginCommand
{
    public abstract class LoginUserCommandModelValidator<T> : AbstractValidator<T>
        where T : LoginUserCommandRequest
    {
        protected LoginUserCommandModelValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage($"{nameof(LoginUserCommandRequest.Login)} cannot be empty");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage($"{nameof(LoginUserCommandRequest.Password)} cannot be empty");
        }
    }
}
