using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using App.Domain.Dtos.Formula;
using App.Domain.Interfaces.Services.Formula;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormulasController : ControllerBase
    {
        private readonly IFormulaService _service;

        public FormulasController(IFormulaService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("Greenblatt/{fileImportId}")]
        public async Task<IActionResult> ReturnDataGreenblatt(int fileImportId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.Greenblatt(fileImportId));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("Greenblatt/Parameters")]
        public async Task<IActionResult> ReturnDataGreenblattOptions([FromBody] OptionsFormula optionsFormula)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.Greenblatt(optionsFormula));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
