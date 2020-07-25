using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Countries;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Directions.Commands
{
    public class CreateDirectionCommand : IRequest<DirectionDto>
    {
        public DirectionDto DirectionDto { get; set; }
    }

    public class CreateDirectionCommandHandler : IRequestHandler<CreateDirectionCommand, DirectionDto>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHasherService _hasher;

        public CreateDirectionCommandHandler(IDbContext context, IMapper mapper, IHasherService hasher)
        {
            _context = context;
            _mapper = mapper;
            _hasher = hasher;
        }
        public async Task<DirectionDto> Handle(CreateDirectionCommand command, CancellationToken cancellationToken)
        {
            var dto = command.DirectionDto;
            var direction = new Direction()
            {
                Name = dto.Name,
                CountryId = dto.CountryId,
                Description = dto.Description,
            };

            _context.Directions.Add(direction);
            await _context.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<DirectionDto>(direction);
        }
    }
}