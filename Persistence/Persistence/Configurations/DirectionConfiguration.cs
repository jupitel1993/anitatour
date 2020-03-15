using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class DirectionConfiguration : IEntityTypeConfiguration<Direction>
    {
        public void Configure(EntityTypeBuilder<Direction> builder)
        {
            builder.HasIndex(x => x.Id);
            builder
                .HasOne(x => x.Country)
                .WithMany(x => x.Directions)
                .HasForeignKey(x => x.CountryId);

            builder.Property(x => x.Name).IsRequired();
        }
    }
}
