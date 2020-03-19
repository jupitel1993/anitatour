using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Users.Commands
{
    public class DeleteUserCommand : IRequest
    {
        public int UserId { get; set; }
    }

    public class DeleteUserCommandHandler : AsyncRequestHandler<DeleteUserCommand>
    {
        private readonly IDbContext _context;
        private readonly ITokenService _tokenService;

        public DeleteUserCommandHandler(IDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        protected override async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var user = _context.Users
                .Where(x => x.Role < _tokenService.GetUserRole())
                .Where(x => x.Id == command.UserId);
            _context.Users.RemoveRange(user);
            await _context.SaveChangesAsync(cancellationToken);
            
        }
    }
}