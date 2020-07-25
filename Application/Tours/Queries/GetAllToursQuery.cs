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

namespace Application.Tours.Queries
{
    public class GetAllToursQuery : IRequest<Pageable<TourDto>>
    {
        public SieveModel SieveModel { get; set; }
    }

    public class GetAllToursQueryHandler : IRequestHandler<GetAllToursQuery, Pageable<TourDto>>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISieveProcessor _sieveProcessor;

        public GetAllToursQueryHandler(IDbContext context, IMapper mapper, ISieveProcessor sieveProcessor)
        {
            _context = context;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }
        public async Task<Pageable<TourDto>> Handle(GetAllToursQuery query, CancellationToken cancellationToken)
        {
            var results = _context.Countries.AsNoTracking();
            results = _sieveProcessor.Apply(query.SieveModel, results, out var totalCount);
            var items = await _mapper.ProjectTo<TourDto>(results).ToListAsync(cancellationToken);
            var result = new Pageable<TourDto>()
            {
                Items = items,
                TotalCount = totalCount,
            };
            return result;
        }
    }
}