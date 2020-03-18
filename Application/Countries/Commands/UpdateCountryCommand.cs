using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Countries.Commands
{
    public class UpdateCountryCommand : IRequest<CountryDto>
    {
        public CountryDto CountryDto { get; set; }
    }

    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, CountryDto>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCountryCommandHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CountryDto> Handle(UpdateCountryCommand command, CancellationToken cancellationToken)
        {
            var country = await _context.Countries
                .FirstAsync(x => x.Id == command.CountryDto.Id, cancellationToken);

            country.Name = command.CountryDto.Name;

            await _context.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<CountryDto>(country);
        }
    }
}