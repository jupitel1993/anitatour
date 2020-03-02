using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public UserDto UserDto { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHasherService _hasher;

        public CreateUserCommandHandler(IDbContext context, IMapper mapper, IHasherService hasher)
        {
            _context = context;
            _mapper = mapper;
            _hasher = hasher;
        }
        public async Task<UserDto> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var dto = command.UserDto;
            var user = new User()
            {
                Active = dto.Active,
                Login = dto.Login,
                Username = dto.Username,
                Role = dto.Role,
                Password = _hasher.GetHash(dto.Password),
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<UserDto>(user);
        }
    }
}