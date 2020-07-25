using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ProgramConfiguration : IEntityTypeConfiguration<Program>
    {
        public void Configure(EntityTypeBuilder<Program> builder)
        {
            builder
                .HasMany(x => x.Tours)
                .WithOne(x => x.Program)
                .HasForeignKey(x => x.ProgramId);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Link).IsRequired();
            builder.Property(x => x.Status).IsRequired();
        }
    }
}
