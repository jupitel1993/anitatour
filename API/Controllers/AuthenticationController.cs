﻿using System.Threading.Tasks;
using Application.Authentication;
using Application.Authentication.Commands;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly ITokenService _tokenService;

        public AuthenticationController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginCommand command)
        {
            var id = await Mediator.Send(command);
            var (token, validTo) = _tokenService.GetSecurityToken(id, command.Login);

            return Ok(new UserDto(){ Token = token, ExpDate = validTo, Id = id, UserName = command.Login});
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {
            // make token invalid
            await _tokenService.DeactivateCurrentTokenAsync();
            return Ok();
        }

    }
}