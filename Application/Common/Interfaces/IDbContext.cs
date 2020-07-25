using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<User> Users { get; set; }
        DbSet<Company> Companies { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Direction> Directions { get; set; }
        DbSet<Person> Persons { get; set; }
        DbSet<Tour> Tours { get; set; }
        DbSet<Program> Programs { get; set; }


    }
}
