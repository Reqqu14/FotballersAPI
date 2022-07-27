using FotballersAPI.Application.Interfaces;
using FotballersAPI.Persistence.Context;
using FotballersAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FotballersAPI.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FotballerDbContext>(options =>
                options.UseSqlServer(
                    configuration["ConnectionStrings:FotballerCS"],
                    b => b.MigrationsAssembly(typeof(FotballerDbContext).Assembly.FullName)));

            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
