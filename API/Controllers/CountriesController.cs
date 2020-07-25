using System.Threading.Tasks;
using API.Common.Constants;
using Application.Common;
using Application.Countries;
using Application.Countries.Commands;
using Application.Countries.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace API.Controllers
{
    [Authorize]
    public class CountriesController : BaseController
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CountryDto>> Get([FromRoute] int id)
        {
            var country = await Mediator.Send(new GetCountryByIdQuery(){Id = id});

            return Ok(country);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Pageable<CountryOverviewDto>>> GetAll([FromQuery] SieveModel sieveModel)
        {
            var countries = await Mediator.Send(new GetAllCountriesQuery() { SieveModel = sieveModel });

            return Ok(countries);
        }

        [Authorize(Policy = Policy.Manager)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CountryDto>> Create([FromBody] CountryDto countryDto)
        {
            var company = await Mediator.Send(new CreateCountryCommand() { CountryDto = countryDto });

            return Ok(company);
        }

        [Authorize(Policy = Policy.Manager)]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CountryDto>> Update([FromRoute] int id, [FromBody] CountryDto countryDto)
        {
            countryDto.Id = id;
            var company = await Mediator.Send(new UpdateCountryCommand() { CountryDto = countryDto });

            return Ok(company);
        }

        [Authorize(Policy = Policy.Manager)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new DeleteCountryCommand() { CountryId = id }));
        }
        
    }
}