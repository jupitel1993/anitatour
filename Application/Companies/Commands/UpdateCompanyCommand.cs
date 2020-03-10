using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Companies.Commands
{
    public class UpdateCompanyCommand : IRequest<CompanyDto>
    {
        public CompanyDto CompanyDto { get; set; }
    }

    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, CompanyDto>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCompanyCommandHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CompanyDto> Handle(UpdateCompanyCommand command, CancellationToken cancellationToken)
        {
            var company = await _context.Companies
                .FirstAsync(x => x.Id == command.CompanyDto.Id, cancellationToken);

            _mapper.Map(command.CompanyDto, company);
            await _context.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<CompanyDto>(company);
        }
    }
}