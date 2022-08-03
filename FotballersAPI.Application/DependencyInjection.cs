using FluentValidation;
using FotballersAPI.Application.Functions.Users;
using FotballersAPI.Application.Infrastructure.Authentication;
using FotballersAPI.Application.Infrastructure.Pipelines;
using FotballersAPI.Domain.Data;
using HashidsNet;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

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

            AddJwtToken(services, configuration);
        }

        private static void AddJwtToken(IServiceCollection services, IConfiguration configuration)
        {
            var authenticationSettings = new AuthenticationSettings();

            configuration.GetSection("Authentication").Bind(authenticationSettings);

            //Adding this object to DI to inject for generate token
            services.AddSingleton(authenticationSettings);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";

            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;

                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
                };
            });
        }
    }
}
