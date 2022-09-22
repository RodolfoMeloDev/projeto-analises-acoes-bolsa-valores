using System.Net;
using App.Domain.Dtos.FileImport;
using App.Domain.Interfaces.Services.FileImport;
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

        [HttpGet]
        [Route("{id}", Name = "GetFileImportWithId")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.GetFileImportById(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFileImport()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.GetAllFileImport());
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertFileImport([FromForm] FileImportDtoCreate fileImport)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (fileImport.File.Length > 0)
                {
                    var result = await _service.InsertFileImport(fileImport);

                    if (result == null)
                        return BadRequest();

                    return Created(new Uri(Url.Link("GetFileImportWithId", new { id = result.Id })), result);
                }
                else
                    return StatusCode((int)HttpStatusCode.InternalServerError, "Houve um problema ao carregar o arquivo.");
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteFileImport(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.DeleteFileImport(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
