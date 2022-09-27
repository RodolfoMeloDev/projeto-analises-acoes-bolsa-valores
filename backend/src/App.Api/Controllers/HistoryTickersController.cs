using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

        [HttpPost]
        public async Task<IActionResult> InserHistoryTicker([FromBody] HistoryTickerDtoCreate historyTicker)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = await _service.InsertHistoryTicker(historyTicker);

                if (result == null)
                    return BadRequest();

                return Created(new Uri(Url.Link("GetHistoryTickerAllFileImport", new { })), result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
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

        [HttpDelete]
        [Route("FileImport/{fileImportId}")]
        public async Task<IActionResult> DeleteHistotyTickerByFileImport(int fileImportId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.DeleteHistoryTickerByFileImport(fileImportId));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
