using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Directions.Commands
{
    public class UpdateDirectionCommand : IRequest<DirectionDto>
    {
        public DirectionDto DirectionDto { get; set; }
    }

    public class UpdateDirectionCommandHandler : IRequestHandler<UpdateDirectionCommand, DirectionDto>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public UpdateDirectionCommandHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DirectionDto> Handle(UpdateDirectionCommand command, CancellationToken cancellationToken)
        {
            var direction = await _context.Directions
                .FirstAsync(x => x.Id == command.DirectionDto.Id, cancellationToken);

            var dto = command.DirectionDto;
            direction.Name = dto.Name;
            direction.CountryId = dto.CountryId;
            direction.Description = dto.Description;

            await _context.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<DirectionDto>(direction);
        }
    }
}