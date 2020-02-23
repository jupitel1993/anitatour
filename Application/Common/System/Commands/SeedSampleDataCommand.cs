using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
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
            // todo: remove (temp solution for avoiding warnings)
            await SeedSuperadmin(cancellationToken);
            
            return await Task.FromResult(Unit.Value);
        }

        private Task SeedSuperadmin(CancellationToken cancellationToken)
        {
            if (_context.Users.Any()) return Task.CompletedTask;
            _context.Users.Add(new User()
            {
                Login = "admin",
                Password = _hasher.GetHash("admin"),
                Super = true,
                Active = true,
                Username = "Super Admin",
            });
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
