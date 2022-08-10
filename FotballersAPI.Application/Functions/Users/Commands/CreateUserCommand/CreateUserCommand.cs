using AutoMapper;
using FotballersAPI.Application.Interfaces;
using FotballersAPI.Domain.Data;
using FotballersAPI.Domain.Data.Constants.RabbitMqQueues;
using FotballersAPI.Domain.Data.RabbitMqConfiguration;
using FotballersAPI.Domain.Events.Users;
using HashidsNet;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;

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
            private readonly IPasswordHasher<User> _passwordHasher;
            private readonly IHashids _hashids;
            private readonly IBus _bus;
            private readonly RabbitMqConfiguration _rabbitMqConfiguration;
            private const string _activateUserLink = @"https://localhost:7128/api/Users/Activate/{0}";

            public Handler(
                IMapper mapper, 
                IUserRepository userRepository, 
                IPasswordHasher<User> passwordHasher,
                IHashids hashids,
                IBus bus,
                RabbitMqConfiguration rabbitMqConfiguration)
            {
                _mapper = mapper;
                _userRepository = userRepository;
                _passwordHasher = passwordHasher;
                _hashids = hashids;
                _bus = bus;
                _rabbitMqConfiguration = rabbitMqConfiguration;
            }
            public async Task<Unit> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
            {
                var user = _mapper.Map<User>(request);
                user.Password = _passwordHasher.HashPassword(user, user.Password);

                var newUser = await _userRepository.AddAsync(user, cancellationToken);

                var createdUser = _mapper.Map<UserCreated>(newUser);

                var hashedUserId = _hashids.Encode(newUser.Id);

                createdUser.Id = hashedUserId;
                createdUser.ActivateAccountLink = string.Format(_activateUserLink, hashedUserId);

                Uri uri = new Uri(_rabbitMqConfiguration.QueueUrl + RabbitMqQueuesConstants.ActivateUserQueue);
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(createdUser);

                return Unit.Value;
            }
        }
    }
}
