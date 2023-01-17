using FotballersAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace FotballersAPI.Application.Tests.Configuration
{
    public static class FotballersDbContextFactory
    {
        public static FotballerDbContext Create()
        {
            var options = new DbContextOptionsBuilder<FotballerDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new FotballerDbContext(options);

            context.Database.EnsureCreated();

            return context;
        }

        public static void Destroy (FotballerDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
