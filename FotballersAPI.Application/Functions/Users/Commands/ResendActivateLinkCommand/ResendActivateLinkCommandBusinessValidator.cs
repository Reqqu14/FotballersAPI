using FluentValidation;
using FotballersAPI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotballersAPI.Application.Functions.Users.Commands.ResendActivateLinkCommand
{
    public class ResendActivateLinkCommandBusinessValidator<T> : AbstractValidator<T>
        where T : ResendActivateLinkCommandRequest
    {
        private readonly IUserRepository _userRepository;

        protected ResendActivateLinkCommandBusinessValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Email)
                .MustAsync(async (email, cancellationToken) =>
                    await _userRepository.CheckEmailExistsInDatabaseAsync(email, cancellationToken))
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage(x => $"Provided email '{x.Email}' doesn't exists");

            RuleFor(x => x.Email)
                .MustAsync(async (request, _, cancellationToken) =>
                {
                    var user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);

                    return user.Active
                        ? false
                        : true;
                })
                .WithMessage($"User is already active");
        }
    }
}
