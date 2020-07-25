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

namespace Application.Countries.Queries
{
    public class GetAllCountriesQuery : IRequest<Pageable<CountryOverviewDto>>
    {
        public SieveModel SieveModel { get; set; }
    }

    public class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, Pageable<CountryOverviewDto>>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISieveProcessor _sieveProcessor;

        public GetAllCountriesQueryHandler(IDbContext context, IMapper mapper, ISieveProcessor sieveProcessor)
        {
            _context = context;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }
        public async Task<Pageable<CountryOverviewDto>> Handle(GetAllCountriesQuery query, CancellationToken cancellationToken)
        {
            var results = _context.Countries.AsNoTracking();
            results = _sieveProcessor.Apply(query.SieveModel, results, out var totalCount);
            var items = await _mapper.ProjectTo<CountryOverviewDto>(results).ToListAsync(cancellationToken);
            var result = new Pageable<CountryOverviewDto>()
            {
                Items = items,
                TotalCount = totalCount,
            };
            return result;
        }
    }
}