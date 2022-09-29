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

        [HttpGet]
        [Route("PriceAndProfit/{fileImportId}")]
        public async Task<IActionResult> ReturnDataPriceAndProfit(int fileImportId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.PriceAndProfit(fileImportId));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("PriceAndProfit/Parameters")]
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
        [Route("ValuetionByBazin/{fileImportId}")]
        public async Task<IActionResult> ReturnDataValuetionByBazin(int fileImportId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.ValuetionByBazin(fileImportId));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("ValuetionByBazin/Parameters")]
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
        [Route("ValuetionByGraham/{fileImportId}")]
        public async Task<IActionResult> ReturnDataValuetionByGraham(int fileImportId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.ValuetionByGraham(fileImportId));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("ValuetionByGraham/Parameters")]
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
        [Route("ValuetionByGordon/{fileImportId}/{riscoBolsa}")]
        public async Task<IActionResult> ReturnDataValuetionByGordon(int fileImportId, decimal riscoBolsa)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.ValuetionByGordon(fileImportId, riscoBolsa));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("ValuetionByGordon/Parameters")]
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
