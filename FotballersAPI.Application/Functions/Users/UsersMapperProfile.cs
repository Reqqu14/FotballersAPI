using AutoMapper;
using FotballerAPI.Helpers;
using FotballersAPI.Application.Functions.Users.Commands.CreateUserCommand;
using FotballersAPI.Application.Functions.Users.Dto;
using FotballersAPI.Domain.Data;
using FotballersAPI.Domain.Events.Users;

namespace FotballersAPI.Application.Functions.Users
{
    public class UsersMapperProfile : Profile
    {
        public UsersMapperProfile()
        {
            CreateMap<CreateUserCommandRequest, User>();

            CreateMap<User, UserCreated>();

            CreateMap<User, GetUserDto>()
                .ForMember(x => x.Gender, y => y.MapFrom(z => z.Gender.GetEnumDescription()));
        }
    }
}
