using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tours.Commands
{
    public class UpdateTourCommand : IRequest<TourDto>
    {
        public TourDto TourDto { get; set; }
    }

    public class UpdateTourCommandHandler : IRequestHandler<UpdateTourCommand, TourDto>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public UpdateTourCommandHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TourDto> Handle(UpdateTourCommand command, CancellationToken cancellationToken)
        {
            var dto = command.TourDto;
            var tour = await _context.Tours
                .FirstAsync(x => x.Id == command.TourDto.Id, cancellationToken);

            tour.Start = dto.Start;
            tour.End = dto.End;

            await _context.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<TourDto>(tour);
        }
    }
}