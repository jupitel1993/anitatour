using System.Threading.Tasks;
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
    public class UsersController : BaseController
    {
        [Authorize(Policy = "Manager")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDto>> Get([FromQuery] int id)
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

        [AllowAnonymous]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDto>> Update([FromQuery] int id, [FromBody] UserDto userDto)
        {
            userDto.Id = id;
            var user = await Mediator.Send(new UpdateUserCommand() { UserDto = userDto });

            return Ok(user);
        }


        [Authorize(Policy = "Manager")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDto>> Delete([FromQuery] int id)
        {
            return Ok(await Mediator.Send(new DeleteUserCommand(){UserId = id}));
        }

    }
}