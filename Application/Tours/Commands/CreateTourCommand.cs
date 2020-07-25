using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Tours.Commands
{
    public class CreateTourCommand : IRequest<TourDto>
    {
        public TourDto TourDto { get; set; }
    }

    public class CreateTourCommandHandler : IRequestHandler<CreateTourCommand, TourDto>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public CreateTourCommandHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TourDto> Handle(CreateTourCommand command, CancellationToken cancellationToken)
        {
            var dto = command.TourDto;
            var tour = new Tour()
            {
                ProgramId = dto.ProgramId,
                Start = dto.Start,
                End = dto.End,
                Tourists = new List<Person>(),
            };

            _context.Tours.Add(tour);
            await _context.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<TourDto>(tour);
        }
    }
}