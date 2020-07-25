using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Document).IsRequired();
            builder.Property(x => x.Email).IsRequired();

            builder.HasOne(x => x.Tour)
                .WithMany(x => x.Tourists)
                .HasForeignKey(x => x.TourId);
        }
    }
}
