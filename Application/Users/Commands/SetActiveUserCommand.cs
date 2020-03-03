using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Users.Commands
{
    public class SetActiveUserCommand : IRequest
    {
        public int UserId { get; set; }

        public bool State { get; set; }
    }

    public class SetActiveUserCommandHandler : AsyncRequestHandler<SetActiveUserCommand>
    {
        private readonly IDbContext _context;

        public SetActiveUserCommandHandler(IDbContext context)
        {
            _context = context;
        }
        protected override async Task Handle(SetActiveUserCommand command, CancellationToken cancellationToken)
        {
            var user = _context.Users.First(x => x.Id == command.UserId);
            user.Active = command.State;
            await _context.SaveChangesAsync(cancellationToken);
            
        }
    }
}