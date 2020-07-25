using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Persons.Commands
{
    public class CreatePersonCommand : IRequest<PersonDto>
    {
        public PersonDto PersonDto { get; set; }
    }

    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, PersonDto>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public CreatePersonCommandHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PersonDto> Handle(CreatePersonCommand command, CancellationToken cancellationToken)
        {
            var dto = command.PersonDto;
            var person = new Person()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Document = dto.Document,
                TourId = dto.TourId,
                UserId = dto.UserId,
            };

            _context.Persons.Add(person);
            await _context.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<PersonDto>(person);
        }
    }
}