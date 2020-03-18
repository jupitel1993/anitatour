using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(x => x.Login).IsUnique();
            
            builder
                .Property(x => x.Login)
                .IsRequired();

            builder
                .Property(x => x.Password)
                .IsRequired();

            builder
                .Property(x => x.Username)
                .IsRequired();

            builder
                .Property(x => x.Active)
                .HasDefaultValue(true)
                .IsRequired();

            builder
                .Property(x => x.Role)
                .IsRequired();

            builder
                .HasOne(x => x.Company);

        }
    }
}
