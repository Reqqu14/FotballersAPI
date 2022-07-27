using FotballersAPI.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FotballersAPI.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public const int UsernameMaxLength = 50;

        public const int PasswordMaxLength = 150;

        public const int EmailMaxLength = 50;

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Username)
                .IsRequired()
                .HasMaxLength(UsernameMaxLength);

            builder
                .Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(PasswordMaxLength);

            builder
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(EmailMaxLength);
        }
    }
}
