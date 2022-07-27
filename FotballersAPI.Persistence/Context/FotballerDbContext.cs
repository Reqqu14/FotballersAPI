using FotballersAPI.Domain.Data;
using FotballersAPI.Domain.Data.Abstraction;
using FotballersAPI.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FotballersAPI.Persistence.Context
{
    public class FotballerDbContext : DbContext
    {
        public FotballerDbContext(DbContextOptions<FotballerDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
            ConfigureAuditEntities(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ConfigureAuditEntities(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(AuditableEntity).IsAssignableFrom(entity.ClrType))
                {
                    modelBuilder.Entity(entity.ClrType).Property<DateTimeOffset>(nameof(AuditableEntity.ModifiedOn));

                    modelBuilder.Entity(entity.ClrType).Property<DateTimeOffset>(nameof(AuditableEntity.CreatedOn));

                    modelBuilder.Entity(entity.ClrType).Property<string>(nameof(AuditableEntity.CreatedBy))
                        .HasMaxLength(50)
                        .IsRequired(false);

                    modelBuilder.Entity(entity.ClrType).Property<string>(nameof(AuditableEntity.ModifiedBy))
                        .HasMaxLength(50)
                        .IsRequired(false);
                }
            }
        }

        private void UpdateAuditEntities()
        {
            var entires = ChangeTracker
                .Entries()
                .Where(x => x.Entity is AuditableEntity &&
                    x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var entity in entires)
            {
                var auditEntity = (AuditableEntity)entity.Entity;
                auditEntity.ModifiedOn = DateTimeOffset.UtcNow;

                if (entity.State == EntityState.Added)
                {
                    auditEntity.CreatedOn = DateTimeOffset.UtcNow;
                }

                if (entity.State == EntityState.Modified)
                {
                    auditEntity.ModifiedOn = DateTimeOffset.UtcNow;
                }
            }
        }
    }
}
