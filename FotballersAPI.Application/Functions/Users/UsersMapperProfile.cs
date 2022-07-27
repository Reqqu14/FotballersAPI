using AutoMapper;
using FotballersAPI.Application.Functions.Users.Commands.CreateUserCommand;
using FotballersAPI.Domain.Data;

namespace FotballersAPI.Application.Functions.Users
{
    public class UsersMapperProfile : Profile
    {
        public UsersMapperProfile()
        {
            CreateMap<CreateUserCommandRequest, User>();
        }
    }
}
