using FluentValidation;

namespace FotballersAPI.Application.Functions.Users.Commands.ResendActivateLinkCommand
{
    public class ResendActivateLinkCommandModelValidator<T> : AbstractValidator<T>
        where T : ResendActivateLinkCommandRequest
    {
        protected ResendActivateLinkCommandModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage($"{nameof(ResendActivateLinkCommandRequest.Email)} must be a valid email");
        }
    }
}
