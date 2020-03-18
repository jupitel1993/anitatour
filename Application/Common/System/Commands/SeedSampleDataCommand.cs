using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Common.System.Commands
{
    public class SeedSampleDataCommand : IRequest
    {
    }

    public class SeedSampleDataCommandHandler : IRequestHandler<SeedSampleDataCommand>
    {
        private readonly IDbContext _context;
        private readonly IHasherService _hasher;

        public SeedSampleDataCommandHandler(IDbContext context, IHasherService hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        public async Task<Unit> Handle(SeedSampleDataCommand request, CancellationToken cancellationToken)
        {
            await SeedPredefinedUsers(cancellationToken);
            await SeedCountriesWithDestinations(cancellationToken);


            return await Task.FromResult(Unit.Value);
        }

        private Task SeedPredefinedUsers(CancellationToken cancellationToken)
        {
            if (_context.Users.Any()) return Task.CompletedTask;
            _context.Users.Add(new User()
            {
                Login = "admin",
                Password = _hasher.GetHash("admin"),
                Role = ERole.Admin,
                Active = true,
                Username = "admin",
            });
            _context.Users.Add(new User()
            {
                Login = "managerlead",
                Password = _hasher.GetHash("managerlead"),
                Role = ERole.ManagerLead,
                Active = true,
                Username = "managerlead",
            });
            _context.Users.Add(new User()
            {
                Login = "manager",
                Password = _hasher.GetHash("manager"),
                Role = ERole.Manager,
                Active = true,
                Username = "manager",
            });
            _context.Users.Add(new User()
            {
                Login = "agent",
                Password = _hasher.GetHash("agent"),
                Role = ERole.Agent,
                Active = false,
                Username = "agent",
                Company = new Company(),
        });
            return _context.SaveChangesAsync(cancellationToken);
        }

        private Task SeedCountriesWithDestinations(CancellationToken cancellationToken)
        {
            if (_context.Countries.Any()) return Task.CompletedTask;
            _context.Countries.Add(new Country()
            {
                Name = "Russia",
                Directions = new List<Direction>()
                {
                    new Direction()
                    {
                        Name = "St.Petersburg",
                        Description = "Some sample description",
                    },
                    new Direction()
                    {
                        Name = "Karelia",
                        Description = "Some sample description",
                    },
                }
            });
            _context.Countries.Add(new Country()
            {
                Name = "Belarus",
                Directions = new List<Direction>()
                {
                    new Direction()
                    {
                        Name = "Mir",
                        Description = "Some sample description",
                    },
                    new Direction()
                    {
                        Name = "Nesvizh",
                        Description = "Some sample description",
                    },
                }
            });
            _context.Countries.Add(new Country()
            {
                Name = "Ukraine",
                Directions = new List<Direction>()
                {
                    new Direction()
                    {
                        Name = "Kiev",
                        Description = "Some sample description",
                    },
                }
            });
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
