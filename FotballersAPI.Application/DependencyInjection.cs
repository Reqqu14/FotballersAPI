using FluentValidation;
using FotballersAPI.Application.Functions.Users;
using FotballersAPI.Application.Infrastructure.Pipelines;
using FotballersAPI.Domain.Data;
using HashidsNet;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FotballersAPI.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddAutoMapper(typeof(UsersMapperProfile));
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddSingleton<IHashids>
                (_ => new Hashids(configuration["Salt:HashSalt"], 11));
        }
    }
}
