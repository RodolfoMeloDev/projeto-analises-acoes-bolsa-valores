using System.Net;
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
        public async Task<IActionResult> ReturnDataGreenblattOptions([FromBody] ParametersFilter parametersFilter)
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

        [HttpGet]
        [Route("PriceAndProfit")]
        public async Task<IActionResult> ReturnDataPriceAndProfitOptions([FromBody] ParametersFilter parametersFilter)
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

        [HttpGet]
        [Route("EvEbit")]
        public async Task<IActionResult> ReturnDataEvEbitOptions([FromBody] ParametersFilter parametersFilter)
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

        [HttpGet]
        [Route("ValuetionByBazin")]
        public async Task<IActionResult> ReturnDataValuetionByBazinOptions([FromBody] ParametersFilter parametersFilter)
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

        [HttpGet]
        [Route("ValuetionByGraham")]
        public async Task<IActionResult> ReturnDataValuetionByGrahamOptions([FromBody] ParametersFilter parametersFilter)
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

        [HttpGet]
        [Route("ValuetionByGordon")]
        public async Task<IActionResult> ReturnDataValuetionByGordonOptions([FromBody] ParametersFilter parametersFilter)
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
    }
}
