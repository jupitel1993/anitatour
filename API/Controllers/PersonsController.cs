using System.Threading.Tasks;
using API.Common.Extensions;
using Application.Common;
using Application.Countries.Queries;
using Application.Persons;
using Application.Persons.Commands;
using Application.Persons.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace API.Controllers
{
    [Authorize]
    public class PersonsController : BaseController
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PersonDto>> Get([FromRoute] int id)
        {
            var person = await Mediator.Send(new GetPersonByIdQuery(){Id = id});

            return Ok(person);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Pageable<PersonDto>>> GetAll([FromQuery] SieveModel sieveModel)
        {
            var persons = await Mediator.Send(new GetAllCountriesQuery() { SieveModel = sieveModel });

            return Ok(persons);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PersonDto>> Create([FromBody] PersonDto personDto)
        {
            personDto.UserId = User.GetId();
            var person = await Mediator.Send(new CreatePersonCommand() { PersonDto = personDto });

            return Ok(person);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PersonDto>> Update([FromRoute] int id, [FromBody] PersonDto personDto)
        {
            personDto.Id = id;

            var existing = await Mediator.Send(new GetPersonByIdQuery() {Id = id});
            if (existing.UserId != User.GetId())
            {
                return Forbid();
            }

            var person = await Mediator.Send(new UpdatePersonCommand() { PersonDto = personDto });

            return Ok(person);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var existing = await Mediator.Send(new GetPersonByIdQuery() { Id = id });
            if (existing.UserId != User.GetId())
            {
                return Forbid();
            }
            return Ok(await Mediator.Send(new DeletePersonCommand() { PersonId = id }));
        }
        
    }
}