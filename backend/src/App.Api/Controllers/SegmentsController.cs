using System.Net;
using App.Domain.Dtos.Segment;
using App.Domain.Interfaces.Services.Segment;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SegmentsController : ControllerBase
    {
        private readonly ISegmentService _service;

        public SegmentsController(ISegmentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> InsertSegment([FromBody] SegmentDtoCreate segment)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = await _service.Insert(segment);

                if (result == null)
                    return BadRequest();

                var _url = Url.Link("GetSegmentWithId", new { id = result.Id });
                
                if (string.IsNullOrEmpty(_url))
                    return StatusCode((int)HttpStatusCode.InternalServerError, "Não foi possível gerar a URL para retorno dos dados inseridos.");

                return Created(new Uri(_url), result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("{id}", Name = "GetSegmentWithId")]
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

        [HttpGet]
        public async Task<IActionResult> GetByAllSegments()
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

        [HttpGet]
        [Route("Setor/{setorId}")]
        public async Task<IActionResult> GetBySetorId(int setorId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.GetBySectorId(setorId));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("SubSetor/{subSetorId}")]
        public async Task<IActionResult> GetBySubSetorId(int subSetorId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.GetBySubSectorId(subSetorId));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("Complete")]
        public async Task<IActionResult> GetByAllSegmentsComplete()
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

        [HttpPut]
        public async Task<IActionResult> UpdateSegment([FromBody] SegmentDtoUpdate segment)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = await _service.Update(segment);

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
        public async Task<IActionResult> DeleteSegment(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.Delete(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
