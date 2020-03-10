using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Users;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Companies.Queries
{
    public class GetCompanyByIdQuery : IRequest<CompanyDto>
    {
        public int Id { get; set; }
    }

    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, CompanyDto>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public GetCompanyByIdQueryHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CompanyDto> Handle(GetCompanyByIdQuery query, CancellationToken cancellationToken)
        {
            var company = await _context.Companies
                .AsNoTracking()
                .Where(x => x.Id == query.Id)
                .FirstAsync(cancellationToken);
            
            return _mapper.Map<CompanyDto>(company);
        }
    }
}