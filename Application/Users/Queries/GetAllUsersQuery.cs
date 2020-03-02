using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Sieve.Extensions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace Application.Users.Queries
{
    public class GetAllUsersQuery : IRequest<Pageable<UserDto>>
    {
        public SieveModel SieveModel { get; set; }
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Pageable<UserDto>>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISieveProcessor _sieveProcessor;

        public GetAllUsersQueryHandler(IDbContext context, IMapper mapper, ISieveProcessor sieveProcessor)
        {
            _context = context;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }
        public async Task<Pageable<UserDto>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var results = _context.Users.AsNoTracking();
            results = _sieveProcessor.Apply(query.SieveModel, results, out var totalCount);
            var items = await _mapper.ProjectTo<UserDto>(results).ToListAsync(cancellationToken);
            //var items = _mapper.Map<List<UserDto>>(await results.ToListAsync(cancellationToken));
            var result = new Pageable<UserDto>()
            {
                Items = items,
                TotalCount = totalCount,
            };
            return result;
        }
    }
}