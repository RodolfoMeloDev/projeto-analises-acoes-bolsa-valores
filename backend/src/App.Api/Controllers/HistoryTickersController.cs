using System.Net;
using App.Domain.Dtos.HistoryTicker;
using App.Domain.Interfaces.Services.HistoryTicker;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryTickersController : ControllerBase
    {
        private readonly IHistoryTickerService _service;

        public HistoryTickersController(IHistoryTickerService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("FileImport/{fileImportId}", Name = "GetHistoryTickerAllFileImport")]
        public async Task<IActionResult> GetAllHistoryTickerByFileImport(int fileImportId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.GetAllByFileImport(fileImportId));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("Ticker/{ticker}")]
        public async Task<IActionResult> GetAllHistoryTickerByTicker(string ticker)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.GetAllByTicker(ticker));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
