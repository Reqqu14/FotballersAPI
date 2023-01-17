using FluentValidation;
using FotballerAPI.Helpers.Exceptions;
using FotballersAPI.Application.Interfaces;
using HashidsNet;

namespace FotballersAPI.Application.Functions.Users.Commands.ActivateUserAccountCommand
{
    public abstract class ActivateUserAccountBusinessValidator<T> : AbstractValidator<T>
        where T : ActivateUserAccountRequest
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashids _hashIds;

        protected ActivateUserAccountBusinessValidator(
            IUserRepository userRepository,
            IHashids hashids)
        {
            _userRepository = userRepository;
            _hashIds = hashids;

            RuleFor(x => x.UserId)
                .MustAsync(async (userId, cancellationToken) =>
                {
                    var decodecId = 0;

                    try
                    {
                        decodecId = _hashIds.Decode(userId).First();
                    }
                    catch (Exception ex)
                    {
                        throw new NotFoundException("user", decodecId);
                    }

                    return !await _userRepository.CheckUserExistsInDatabaseAsync(
                        decodecId,
                        cancellationToken);
                })
                .When(x => !string.IsNullOrEmpty(x.UserId));
        }
    }
}