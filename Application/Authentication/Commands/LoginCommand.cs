using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Commands
{
    public class LoginCommand : IRequest<(int, ERole)>
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, (int, ERole)>
    {
        private readonly IDbContext _context;
        private readonly IHasherService _hasherService;

        public LoginCommandHandler(IDbContext context, IHasherService hasherService)
        {
            _context = context;
            _hasherService = hasherService;
        }
        public async Task<(int, ERole)> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            User user;
            try
            {
                user = await _context.Users
                    .AsNoTracking()
                    .Where(x => x.Active && x.Login == command.Login)
                    .FirstAsync(cancellationToken);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidCredentialException();
            }

            var passwordHash = user.Password;
            var passwordIsValid = passwordHash == _hasherService.GetHash(command.Password);

            if (!passwordIsValid)
            {
                throw new InvalidCredentialException();
            }

            return (user.Id, user.Role);
        }
    }
}