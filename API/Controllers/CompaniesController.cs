﻿using System.Threading.Tasks;
using Application.Companies;
using Application.Companies.Commands;
using Application.Companies.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class CompaniesController : BaseController
    {
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CompanyDto>> Get([FromRoute] int id)
        {
            var company = await Mediator.Send(new GetCompanyByIdQuery(){Id = id});

            return Ok(company);
        }
        

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CompanyDto>> Update([FromRoute] int id, [FromBody] CompanyDto companyDto)
        {
            companyDto.Id = id;
            var company = await Mediator.Send(new UpdateCompanyCommand() { CompanyDto = companyDto });

            return Ok(company);
        }


    }
}