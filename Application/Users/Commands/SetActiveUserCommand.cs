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
        private readonly ITokenService _tokenService;

        public SetActiveUserCommandHandler(IDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        protected override async Task Handle(SetActiveUserCommand command, CancellationToken cancellationToken)
        {
            var user = _context.Users
                .Where(x => x.Role < _tokenService.GetUserRole())
                .First(x => x.Id == command.UserId);
            user.Active = command.State;
            await _context.SaveChangesAsync(cancellationToken);
            
        }
    }
}