using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Countries.Commands
{
    public class DeleteCountryCommand : IRequest
    {
        public int CountryId { get; set; }
    }

    public class DeleteCountryCommandHandler : AsyncRequestHandler<DeleteCountryCommand>
    {
        private readonly IDbContext _context;

        public DeleteCountryCommandHandler(IDbContext context)
        {
            _context = context;
        }
        protected override async Task Handle(DeleteCountryCommand command, CancellationToken cancellationToken)
        {
            var country = _context.Countries.First(x => x.Id == command.CountryId);

            _context.Directions.RemoveRange(country.Directions);
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync(cancellationToken);
            
        }
    }
}