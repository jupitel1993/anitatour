using System.Threading.Tasks;
using API.Common.Constants;
using Application.Common;
using Application.Directions;
using Application.Directions.Commands;
using Application.Directions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace API.Controllers
{
    [Authorize]
    public class DirectionsController : BaseController
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DirectionDto>> Get([FromRoute] int id)
        {
            var country = await Mediator.Send(new GetDirectionByIdQuery() { Id = id });

            return Ok(country);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Pageable<DirectionOverviewDto>>> GetAll([FromQuery] SieveModel sieveModel)
        {
            var directions = await Mediator.Send(new GetAllDirectionsQuery() { SieveModel = sieveModel });

            return Ok(directions);
        }

        [Authorize(Policy = Policy.Manager)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DirectionDto>> CreateDirection([FromBody] DirectionDto directionDto)
        {
            var direction = await Mediator.Send(new CreateDirectionCommand() { DirectionDto = directionDto });

            return Ok(direction);
        }

        [Authorize(Policy = Policy.Manager)]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DirectionDto>> Update([FromRoute] int id, [FromBody] DirectionDto directionDto)
        {
            directionDto.Id = id;
            var direction = await Mediator.Send(new UpdateDirectionCommand() { DirectionDto = directionDto });

            return Ok(direction);
        }

        [Authorize(Policy = Policy.Manager)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new DeleteDirectionCommand() { DirectionId = id }));
        }

    }
}