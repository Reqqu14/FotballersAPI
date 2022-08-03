using FluentValidation;

namespace FotballersAPI.Application.Functions.Users.Commands.ActivateUserAccountCommand
{
    public class ActivateUserAccountModelValidator<T> : AbstractValidator<T>
        where T : ActivateUserAccountRequest
    {
        protected ActivateUserAccountModelValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage($"{nameof(ActivateUserAccountRequest.UserId)} cannot be empty");
        }
    }
}
