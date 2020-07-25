using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tours.Queries
{
    public class GetTourByIdQuery : IRequest<TourDto>
    {
        public int Id { get; set; }
    }

    public class GetTourByIdQueryHandler : IRequestHandler<GetTourByIdQuery, TourDto>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public GetTourByIdQueryHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TourDto> Handle(GetTourByIdQuery query, CancellationToken cancellationToken)
        {
            var tour = await _context.Tours
                .AsNoTracking()
                .Where(x => x.Id == query.Id)
                .FirstAsync(cancellationToken);
            
            return _mapper.Map<TourDto>(tour);
        }
    }
}