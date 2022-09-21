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
        public async Task<IActionResult> InserHistoryTicker([FromBody] HistoryTickerDtoCreate historyTicker){
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = await _service.InsertHistoryTicker(historyTicker);

                if (result == null)
                    return BadRequest();

                return Created(new Uri(Url.Link("GetHistoryTickerAll", new { } )), result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("", Name = "GetHistoryTickerAll")]
        public async Task<IActionResult> GetAllHistoryTicker(){
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.GetAllHistoryTicker());
            }
            catch (ArgumentException e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteHistotyTicker(int id){
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.DeleteHistoryTickertById(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpDelete]
        [Route("FileImport/{fileImportId}")]
        public async Task<IActionResult> DeleteHistotyTickerByFileImport(int fileImportId){
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.DeleteHistoryTickerByFileImport(fileImportId));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}