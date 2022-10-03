using System.Net;
using App.Domain.Dtos.Ticker;
using App.Domain.Interfaces.Services.Ticker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TickersController : ControllerBase
    {
        private readonly ITickerService _service;

        public TickersController(ITickerService service)
        {
            _service = service;
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("{id}", Name = "GetTickerWithId")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.GetById(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("Complete/{id}")]
        public async Task<IActionResult> GetByIdComplete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.GetByIdComplete(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.GetAll());
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("Complete/")]
        public async Task<IActionResult> GetAllComplete()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.GetAllComplete());
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("Complete/Ticker/{ticker}")]
        public async Task<IActionResult> GetByTickerComplete(string ticker)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.GetByTicker(ticker));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("Segment/{segment}")]
        public async Task<IActionResult> GetTickersBySegment(int segment)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.GetBySegmentId(segment));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("SubSector/{subSector}")]
        public async Task<IActionResult> GetTickersBySubSector(int subSector)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.GetBySubSectorId(subSector));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("Sector/{sector}")]
        public async Task<IActionResult> GetTickersBySector(int sector)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.GetBySubSectorId(sector));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TickerDtoUpdate ticker)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = await _service.Update(ticker);

                if (result != null)
                    return Ok(result);

                return BadRequest();
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
