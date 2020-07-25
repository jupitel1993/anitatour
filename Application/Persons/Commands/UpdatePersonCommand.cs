using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Countries;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Persons.Commands
{
    public class UpdatePersonCommand : IRequest<PersonDto>
    {
        public PersonDto PersonDto { get; set; }
    }

    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, PersonDto>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public UpdatePersonCommandHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PersonDto> Handle(UpdatePersonCommand command, CancellationToken cancellationToken)
        {
            var dto = command.PersonDto;
            var person = await _context.Persons
                .FirstAsync(x => x.Id == command.PersonDto.Id, cancellationToken);

            person.FirstName = dto.FirstName;
            person.LastName = dto.LastName;
            person.Email = person.Email;
            person.Document = dto.Document;

            await _context.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<PersonDto>(person);
        }
    }
}