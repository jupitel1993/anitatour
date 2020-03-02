using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Application.Authentication.Commands;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands
{
    public class GetUserByIdCommand : IRequest<UserDto>
    {
        public int Id { get; set; }
    }

    public class GetUserByIdCommandHandler : IRequestHandler<GetUserByIdCommand, UserDto>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public GetUserByIdCommandHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserDto> Handle(GetUserByIdCommand command, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Where(x => x.Id == command.Id)
                .FirstAsync(cancellationToken);
            
            return _mapper.Map<UserDto>(user);
        }
    }
}