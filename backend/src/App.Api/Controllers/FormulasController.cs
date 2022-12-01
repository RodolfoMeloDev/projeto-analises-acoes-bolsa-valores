using System.Net;
using App.Domain.Dtos.Formula;
using App.Domain.Interfaces.Services.Formula;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize("Bearer")]
        [HttpPost]
        [Route("Greenblatt")]
        public async Task<IActionResult> ReturnDataGreenblatt([FromBody] ParametersFilter parametersFilter)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.Greenblatt(parametersFilter));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPost]
        [Route("PriceAndProfit")]
        public async Task<IActionResult> ReturnDataPriceAndProfit([FromBody] ParametersFilter parametersFilter)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.PriceAndProfit(parametersFilter));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPost]
        [Route("EvEbit")]
        public async Task<IActionResult> ReturnDataEvEbit([FromBody] ParametersFilter parametersFilter)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.EvEbit(parametersFilter));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPost]
        [Route("ValuetionByBazin")]
        public async Task<IActionResult> ReturnDataValuetionByBazin([FromBody] ParametersFilter parametersFilter)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.ValuetionByBazin(parametersFilter));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPost]
        [Route("ValuetionByGraham")]
        public async Task<IActionResult> ReturnDataValuetionByGraham([FromBody] ParametersFilter parametersFilter)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.ValuetionByGraham(parametersFilter));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPost]
        [Route("ValuetionByGordon")]
        public async Task<IActionResult> ReturnDataValuetionByGordon([FromBody] ParametersFilter parametersFilter)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.ValuetionByGordon(parametersFilter));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPost]
        [Route("TickersAnalisys")]
        public async Task<IActionResult> TickersAnalisys([FromBody] ParametersFilter parametersFilter)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.TickersAnalisys(parametersFilter));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
