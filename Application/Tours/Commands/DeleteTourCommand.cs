using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tours.Commands
{
    public class DeleteTourCommand : IRequest
    {
        public int TourId { get; set; }
    }

    public class DeleteTourCommandHandler : AsyncRequestHandler<DeleteTourCommand>
    {
        private readonly IDbContext _context;

        public DeleteTourCommandHandler(IDbContext context)
        {
            _context = context;
        }
        protected override async Task Handle(DeleteTourCommand command, CancellationToken cancellationToken)
        {
            var tour = _context.Tours
                .Include(x => x.Tourists)
                .First(x => x.Id == command.TourId);
            _context.Persons.RemoveRange(tour.Tourists);
            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}