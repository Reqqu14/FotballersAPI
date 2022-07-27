using AutoMapper;
using FotballersAPI.Application.Interfaces;
using FotballersAPI.Domain.Data;
using MediatR;

namespace FotballersAPI.Application.Functions.Users.Commands.CreateUserCommand
{
    public class CreateUserCommand
    {
        public class ModelValidator : CreateUserCommandModelValidator<CreateUserCommandRequest>
        {

        }

        public class BusinessValidator : CreateUserCommandBusinessValidator<CreateUserCommandRequest>
        {
            public BusinessValidator(IUserRepository userRepository) : base(userRepository)
            {

            }
        }

        public class Handler : IRequestHandler<CreateUserCommandRequest, Unit>
        {
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;

            public Handler(IMapper mapper, IUserRepository userRepository)
            {
                _mapper = mapper;
                _userRepository = userRepository;
            }
            public async Task<Unit> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
            {
                var dto = _mapper.Map<User>(request);

                await _userRepository.AddAsync(dto, cancellationToken);

                return Unit.Value;
            }
        }
    }
}
