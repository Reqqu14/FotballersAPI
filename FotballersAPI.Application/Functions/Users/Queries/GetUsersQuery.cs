using AutoMapper;
using FotballersAPI.Application.Functions.Users.Dto;
using FotballersAPI.Application.Interfaces;
using FotballersAPI.Application.Interfaces.QueryOptions;
using MediatR;

namespace FotballersAPI.Application.Functions.Users.Queries
{
    public class GetUsersQuery
    {

        public class Handler : IRequestHandler<GetUsersQueryRequest, GetUsersQueryResponse>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public Handler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<GetUsersQueryResponse> Handle(GetUsersQueryRequest request, CancellationToken cancellationToken)
            {
                var options = new GetUserOptions()
                {
                    Skip = 0,
                    Take = 20
                };

                var users = await _userRepository.GetAllAsync(options, cancellationToken);

                var usersDto = _mapper.Map<List<GetUserDto>>(users);

                return new GetUsersQueryResponse
                {
                    Users = usersDto,
                };
            }
        }
    }
}
