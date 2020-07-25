using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Persons.Queries
{
    public class GetPersonByIdQuery : IRequest<PersonDto>
    {
        public int Id { get; set; }
    }

    public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, PersonDto>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public GetPersonByIdQueryHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PersonDto> Handle(GetPersonByIdQuery query, CancellationToken cancellationToken)
        {
            var person = await _context.Persons
                .AsNoTracking()
                .Where(x => x.Id == query.Id)
                .FirstAsync(cancellationToken);
            
            return _mapper.Map<PersonDto>(person);
        }
    }
}