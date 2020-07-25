using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Directions.Commands
{
    public class DeleteDirectionCommand : IRequest
    {
        public int DirectionId { get; set; }
    }

    public class DeleteDirectionCommandHandler : AsyncRequestHandler<DeleteDirectionCommand>
    {
        private readonly IDbContext _context;

        public DeleteDirectionCommandHandler(IDbContext context)
        {
            _context = context;
        }
        protected override async Task Handle(DeleteDirectionCommand command, CancellationToken cancellationToken)
        {
            var direction = _context.Directions.Where(x => x.Id == command.DirectionId);
            _context.Directions.RemoveRange(direction);
            await _context.SaveChangesAsync(cancellationToken);
            
        }
    }
}