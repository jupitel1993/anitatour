using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Persons.Commands
{
    public class DeletePersonCommand : IRequest
    {
        public int PersonId { get; set; }
    }

    public class DeletePersonCommandHandler : AsyncRequestHandler<DeletePersonCommand>
    {
        private readonly IDbContext _context;

        public DeletePersonCommandHandler(IDbContext context)
        {
            _context = context;
        }
        protected override async Task Handle(DeletePersonCommand command, CancellationToken cancellationToken)
        {
            var person = _context.Persons.First(x => x.Id == command.PersonId);
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}