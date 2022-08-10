using AutoMapper;
using FotballersAPI.Application.Interfaces;
using FotballersAPI.Domain.Data.Constants.RabbitMqQueues;
using FotballersAPI.Domain.Data.RabbitMqConfiguration;
using FotballersAPI.Domain.Events.Users;
using HashidsNet;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotballersAPI.Application.Functions.Users.Commands.ResendActivateLinkCommand
{
    public class ResendActivateLinkCommand
    {
        public class ModelValidator : ResendActivateLinkCommandModelValidator<ResendActivateLinkCommandRequest>
        {

        }

        public class BusinessValidator : ResendActivateLinkCommandBusinessValidator<ResendActivateLinkCommandRequest>
        {
            public BusinessValidator(IUserRepository userRepository) : base(userRepository)
            {

            }
        }

        public class Handler : IRequestHandler<ResendActivateLinkCommandRequest, Unit>
        {
            private const string _activateUserLink = @"https://localhost:7128/api/Users/Activate/{0}";
            private readonly IHashids _hashids;
            private readonly IBus _bus;
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly RabbitMqConfiguration _rabbitMqConfiguration;

            public Handler(
                IHashids hashids,
                IBus bus,
                IUserRepository userRepository,
                IMapper mapper,
                RabbitMqConfiguration rabbitMqConfiguration)
            {
                _hashids = hashids;
                _bus = bus;
                _userRepository = userRepository;
                _mapper = mapper;
                _rabbitMqConfiguration = rabbitMqConfiguration;
            }

            public async Task<Unit> Handle(ResendActivateLinkCommandRequest request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);

                var createdUser = _mapper.Map<UserCreated>(user);

                var hashedUserId = _hashids.Encode(user.Id);
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
