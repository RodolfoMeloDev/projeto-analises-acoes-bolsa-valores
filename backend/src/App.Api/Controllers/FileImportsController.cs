using System.Net;
using App.Domain.Dtos.FileImport;
using App.Domain.Interfaces.Services.FileImport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileImportsController : ControllerBase
    {
        private readonly IFileImportService _service;

        public FileImportsController(IFileImportService service)
        {
            _service = service;
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("{id}", Name = "GetFileImportWithId")]
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
                throw;
            }
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("User/{userId}")]
        public async Task<IActionResult> GetAll(int userId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.GetAll(userId));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
                throw;
            }
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("FilesByDate")]
        public async Task<IActionResult> GetByDate([FromQuery] int userId, DateTime dateFile)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.GetByDate(userId, dateFile));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
                throw;
            }
        }

        [Authorize("Bearer")]
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] FileImportDtoCreate fileImport)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (fileImport.File.Length > 0)
                {
                    var result = await _service.Insert(fileImport);

                    if (result == null)
                        return BadRequest();

                    var _url = Url.Link("GetFileImportWithId", new { id = result.Id });

                    if (string.IsNullOrEmpty(_url))
                        return StatusCode((int)HttpStatusCode.InternalServerError, "Não foi possível gerar a URL para retorno dos dados inseridos.");

                    return Created(new Uri(_url), result);
                }
                else
                    return StatusCode((int)HttpStatusCode.InternalServerError, "Houve um problema ao carregar o arquivo.");
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
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
