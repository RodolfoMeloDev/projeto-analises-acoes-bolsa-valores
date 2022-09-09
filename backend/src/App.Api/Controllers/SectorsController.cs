using System.Net;
using App.Domain.Dtos.Sector;
using App.Domain.Interfaces.Services.Sector;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectorsController : ControllerBase
    {
        private readonly ISectorService _service;

        public SectorsController(ISectorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSectors()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(await _service.GetAllSectors());

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("{id}", Name = "GetSectorWithId")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(await _service.GetSectorById(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertSector([FromBody] SectorDtoCreate sector)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = await _service.InsertSector(sector);

                if (result == null)
                    return BadRequest();

                return Created(new Uri(Url.Link("GetSectorWithId", new { id = result.Id })), result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSector([FromBody] SectorDtoUpdate sector)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = await _service.UpdateSector(sector);

                if (result != null)
                    return Ok(result);

                return BadRequest();
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteSector(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.DeleteSector(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
