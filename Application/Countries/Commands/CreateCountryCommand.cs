using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Countries.Commands
{
    public class CreateCountryCommand : IRequest<CountryDto>
    {
        public CountryDto CountryDto { get; set; }
    }

    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, CountryDto>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHasherService _hasher;

        public CreateCountryCommandHandler(IDbContext context, IMapper mapper, IHasherService hasher)
        {
            _context = context;
            _mapper = mapper;
            _hasher = hasher;
        }
        public async Task<CountryDto> Handle(CreateCountryCommand command, CancellationToken cancellationToken)
        {
            var dto = command.CountryDto;
            var country = new Country()
            {
                Name = dto.Name,
            };

            _context.Countries.Add(country);
            await _context.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<CountryDto>(country);
        }
    }
}