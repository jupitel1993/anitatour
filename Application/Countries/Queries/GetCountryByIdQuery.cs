using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Countries.Queries
{
    public class GetCountryByIdQuery : IRequest<CountryDto>
    {
        public int Id { get; set; }
    }

    public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, CountryDto>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public GetCountryByIdQueryHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CountryDto> Handle(GetCountryByIdQuery query, CancellationToken cancellationToken)
        {
            var country = await _context.Countries
                .AsNoTracking()
                .Where(x => x.Id == query.Id)
                .FirstAsync(cancellationToken);
            
            return _mapper.Map<CountryDto>(country);
        }
    }
}