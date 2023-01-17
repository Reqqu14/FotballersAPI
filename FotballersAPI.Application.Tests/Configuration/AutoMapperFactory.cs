using AutoMapper;
using FotballersAPI.Application.Functions.Users;

namespace FotballersAPI.Application.Tests.Configuration
{
    public static class AutoMapperFactory
    {
        public static IMapper Create()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddMaps(typeof(UsersMapperProfile));
            });

            return mappingConfig.CreateMapper();
        }
    }
}
