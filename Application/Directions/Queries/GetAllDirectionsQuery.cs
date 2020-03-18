using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Sieve.Extensions;
using Application.Countries;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace Application.Directions.Queries
{
    public class GetAllDirectionsQuery : IRequest<Pageable<DirectionOverviewDto>>
    {
        public SieveModel SieveModel { get; set; }
    }

    public class GetAllDirectionsQueryHandler : IRequestHandler<GetAllDirectionsQuery, Pageable<DirectionOverviewDto>>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISieveProcessor _sieveProcessor;

        public GetAllDirectionsQueryHandler(IDbContext context, IMapper mapper, ISieveProcessor sieveProcessor)
        {
            _context = context;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }
        public async Task<Pageable<DirectionOverviewDto>> Handle(GetAllDirectionsQuery query, CancellationToken cancellationToken)
        {
            var results = _context.Directions.AsNoTracking();
            results = _sieveProcessor.Apply(query.SieveModel, results, out var totalCount);
            var items = await _mapper.ProjectTo<DirectionOverviewDto>(results).ToListAsync(cancellationToken);
            var result = new Pageable<DirectionOverviewDto>()
            {
                Items = items,
                TotalCount = totalCount,
            };
            return result;
        }
    }
}