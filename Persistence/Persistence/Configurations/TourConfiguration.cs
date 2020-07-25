using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class TourConfiguration : IEntityTypeConfiguration<Tour>
    {
        public void Configure(EntityTypeBuilder<Tour> builder)
        {
            builder
                .HasOne(x => x.Program)
                .WithMany(x => x.Tours)
                .HasForeignKey(x => x.ProgramId);
            builder
                .HasMany(x => x.Tourists);

            builder.Property(x => x.Start).IsRequired();
            builder.Property(x => x.End).IsRequired();
        }
    }
}
