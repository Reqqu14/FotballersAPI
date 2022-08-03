﻿using AutoMapper;
using FotballersAPI.Application.Interfaces;
using FotballersAPI.Domain.Data;
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

            public Handler(IMapper mapper, IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
            {
                _mapper = mapper;
                _userRepository = userRepository;
                _passwordHasher = passwordHasher;
            }
            public async Task<Unit> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
            {
                var user = _mapper.Map<User>(request);
                user.Password = _passwordHasher.HashPassword(user, user.Password);

                await _userRepository.AddAsync(user, cancellationToken);

                return Unit.Value;
            }
        }
    }
}
