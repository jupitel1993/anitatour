using System.Threading.Tasks;
using Application.Authentication;
using Application.Authentication.Commands;
using Application.Common.Interfaces;
using Application.Users.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UsersController : BaseController
    {
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TokenDto>> Get([FromQuery] int id)
        {
            var user = await Mediator.Send(new GetUserByIdCommand(){Id = id});

            return Ok(user);
        }


    }
}