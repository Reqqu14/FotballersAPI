using FluentValidation;

namespace FotballersAPI.Application.Functions.Users.Commands.CreateUserCommand
{
    public abstract class CreateUserCommandModelValidator<T> : AbstractValidator<T>
        where T : CreateUserCommandRequest
    {
        private const string EmailPattern = "^\\S+@\\S+\\.\\S+$";

        protected CreateUserCommandModelValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage($"{nameof(CreateUserCommandRequest.Username)} cannot be empty");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage($"{nameof(CreateUserCommandRequest.Password)} cannot be empty");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage($"{nameof(CreateUserCommandRequest.ConfirmPassword)} cannot be empty");

            RuleFor(x => x)
                .Must(x => x.ConfirmPassword == x.Password)
                .WithMessage("Password and confirm password must be the same");

            RuleFor(x => x.Email)
                .NotEmpty()
                .Matches(EmailPattern)
                .WithMessage($"{nameof(CreateUserCommandRequest.Email)} cannot be empty");

            RuleFor(x => x.DateOfBirth)
                .Must(BeAValidDate)
                .WithMessage("Date must be valid");

            RuleFor(x => x.Gender)
                .IsInEnum()
                .NotEmpty()
                .WithMessage("Gender is invalid");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
