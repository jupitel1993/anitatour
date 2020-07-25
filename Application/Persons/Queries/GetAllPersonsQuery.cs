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

namespace Application.Persons.Queries
{
    public class GetAllPersonsQuery : IRequest<Pageable<PersonDto>>
    {
        public SieveModel SieveModel { get; set; }
    }

    public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, Pageable<PersonDto>>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISieveProcessor _sieveProcessor;

        public GetAllPersonsQueryHandler(IDbContext context, IMapper mapper, ISieveProcessor sieveProcessor)
        {
            _context = context;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }
        public async Task<Pageable<PersonDto>> Handle(GetAllPersonsQuery query, CancellationToken cancellationToken)
        {
            var results = _context.Countries.AsNoTracking();
            results = _sieveProcessor.Apply(query.SieveModel, results, out var totalCount);
            var items = await _mapper.ProjectTo<PersonDto>(results).ToListAsync(cancellationToken);
            var result = new Pageable<PersonDto>()
            {
                Items = items,
                TotalCount = totalCount,
            };
            return result;
        }
    }
}