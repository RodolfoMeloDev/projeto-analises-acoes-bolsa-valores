using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using App.Domain.Interfaces.Services.BaseTicker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseTickerController : ControllerBase
    {
        private readonly IBaseTickerService _service;

        public BaseTickerController(IBaseTickerService service)
        {
            _service = service;
        }

        [AllowAnonymous]
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
                throw;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("BySegment/{segment}")]
        public async Task<IActionResult> GetAll(int segment)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.GetAllBySegment(segment));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
                throw;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("BySubSector/{subSector}")]
        public async Task<IActionResult> GetAllBySubSector(int subSector)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.GetAllBySubSector(subSector));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
                throw;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("BySector/{sector}")]
        public async Task<IActionResult> GetAllBySector(int sector)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(await _service.GetAllBySector(sector));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
                throw;
            }
        }
    }
}