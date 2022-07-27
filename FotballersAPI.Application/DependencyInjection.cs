using FluentValidation;
using FotballersAPI.Application.Functions.Users;
using FotballersAPI.Application.Infrastructure.Pipelines;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FotballersAPI.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddAutoMapper(typeof(UsersMapperProfile));
        }
    }
}
