using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Countries;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Directions.Queries
{
    public class GetDirectionByIdQuery : IRequest<DirectionDto>
    {
        public int Id { get; set; }
    }

    public class GetDirectionByIdQueryHandler : IRequestHandler<GetDirectionByIdQuery, DirectionDto>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public GetDirectionByIdQueryHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DirectionDto> Handle(GetDirectionByIdQuery query, CancellationToken cancellationToken)
        {
            var direction = await _context.Directions
                .AsNoTracking()
                .Where(x => x.Id == query.Id)
                .FirstAsync(cancellationToken);
            
            return _mapper.Map<DirectionDto>(direction);
        }
    }
}