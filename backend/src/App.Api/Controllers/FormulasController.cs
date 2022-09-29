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
        [Route("Greenblatt")]
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

        [HttpGet]
        [Route("PriceAndProfit")]
        public async Task<IActionResult> ReturnDataPriceAndProfitOptions([FromBody] OptionsFormula optionsFormula)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.PriceAndProfit(optionsFormula));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("ValuetionByBazin")]
        public async Task<IActionResult> ReturnDataValuetionByBazinOptions([FromBody] OptionsFormula optionsFormula)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.ValuetionByBazin(optionsFormula));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("ValuetionByGraham")]
        public async Task<IActionResult> ReturnDataValuetionByGrahamOptions([FromBody] OptionsFormula optionsFormula)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.ValuetionByGraham(optionsFormula));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("ValuetionByGordon")]
        public async Task<IActionResult> ReturnDataValuetionByGordonOptions([FromBody] OptionsFormula optionsFormula)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.ValuetionByGordon(optionsFormula));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
