using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands
{
    public class UpdateUserCommand : IRequest<UserDto>
    {
        public UserDto UserDto { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHasherService _hasher;
        private readonly ITokenService _tokenService;

        public UpdateUserCommandHandler(IDbContext context, IMapper mapper, IHasherService hasher, ITokenService tokenService)
        {
            _context = context;
            _mapper = mapper;
            _hasher = hasher;
            _tokenService = tokenService;
        }
        public async Task<UserDto> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Where(x => x.Role < _tokenService.GetUserRole() || x.Id == _tokenService.GetUserId())
                .FirstAsync(x => x.Id == command.UserDto.Id, cancellationToken);
            var dto = command.UserDto;
            user.Role = dto.Role;
            user.Active = dto.Active;
            user.Username = dto.Username;
            user.Login = dto.Login;

            if (dto.Password != null)
            {
                user.Password = _hasher.GetHash(dto.Password);
            }
            await _context.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<UserDto>(user);
        }
    }
}