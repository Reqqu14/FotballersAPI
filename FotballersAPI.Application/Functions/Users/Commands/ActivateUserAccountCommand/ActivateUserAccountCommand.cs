using FotballersAPI.Application.Interfaces;
using HashidsNet;
using MediatR;

namespace FotballersAPI.Application.Functions.Users.Commands.ActivateUserAccountCommand
{
    public class ActivateUserAccountCommand
    {
        public class ModelValidator : ActivateUserAccountModelValidator<ActivateUserAccountRequest>
        {

        }

        public class BusinessValidator : ActivateUserAccountBusinessValidator<ActivateUserAccountRequest>
        {
            public BusinessValidator(
                IUserRepository userRepository,
                IHashids hashids) : base(userRepository, hashids)
            {

            }
        }

        public class Handler : IRequestHandler<ActivateUserAccountRequest, Unit>
        {
            private readonly IUserRepository _userRepository;
            private readonly IHashids _hashids;

            public Handler(IUserRepository userRepository, IHashids hashids)
            {
                _userRepository = userRepository;
                _hashids = hashids;
            }

            public async Task<Unit> Handle(ActivateUserAccountRequest request, CancellationToken cancellationToken)
            {
                var userId = _hashids.Decode(request.UserId);

                var user = await _userRepository.GetByIdAsync(userId.First(), cancellationToken);

                user.Active = true;

                await _userRepository.UpdateAsync(user, cancellationToken);

                return Unit.Value;
            }
        }
    }
}
