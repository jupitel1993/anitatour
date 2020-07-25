using System.Threading.Tasks;
using API.Common.Constants;
using Application.Common;
using Application.Users;
using Application.Users.Commands;
using Application.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace API.Controllers
{
    [Authorize(Policy = Policy.Manager)]
    public class UsersController : BaseController
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDto>> Get([FromRoute] int id)
        {
            var user = await Mediator.Send(new GetUserByIdQuery(){Id = id});

            return Ok(user);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Pageable<UserDto>>> GetAll([FromQuery] SieveModel sieveModel)
        {
            var users = await Mediator.Send(new GetAllUsersQuery() { SieveModel = sieveModel});

            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDto>> Create([FromBody] UserDto user)
        {
            return Ok(await Mediator.Send(new CreateUserCommand() {UserDto = user}));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDto>> Update([FromRoute] int id, [FromBody] UserDto userDto)
        {
            userDto.Id = id;
            var user = await Mediator.Send(new UpdateUserCommand() { UserDto = userDto });

            return Ok(user);
        }


        [Authorize(Policy = Policy.AdminOnly)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new DeleteUserCommand(){UserId = id}));
        }

        [HttpPost("{id}/activate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> SetActiveState([FromRoute] int id, [FromBody] bool state)
        {
            return Ok(await Mediator.Send(new SetActiveUserCommand() { UserId = id, State = state}));
        }

    }
}