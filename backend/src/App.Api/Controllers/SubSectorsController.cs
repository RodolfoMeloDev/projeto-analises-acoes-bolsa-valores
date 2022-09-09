using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using App.Domain.Dtos.SubSector;
using App.Domain.Interfaces.Services.SubSector;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubSectorsController : ControllerBase
    {
        private readonly ISubSectorService _service;

        public SubSectorsController(ISubSectorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubSectors(){
            try{
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(await _service.GetAll());

            }catch (ArgumentException e) {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("Complete")]
        public async Task<IActionResult> GetAllCompleteSubSectors(){
            try{
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(await _service.GetAllComplete());

            }catch (ArgumentException e) {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("{id}", Name = "GetSubSectorWithId")]
        public async Task<IActionResult> GetById(int id){
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(await _service.GetById(id));
            }
            catch (ArgumentException e)
            {                
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("Complete/{id}")]
        public async Task<IActionResult> GetByIdComplete(int id){
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(await _service.GetByIdComplete(id));
            }
            catch (ArgumentException e)
            {                
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("Setor/{setorId}")]
        public async Task<IActionResult> GetBySetorId(int setorId){
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(await _service.GetBySectorId(setorId));
            }
            catch (ArgumentException e)
            {                
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertSubSector([FromBody] SubSectorDtoCreate subSector){
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = await _service.Insert(subSector);

                if (result == null)
                    return BadRequest();

                return Created(new Uri(Url.Link("GetSubSectorWithId", new { id = result.Id })), result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSubSector([FromBody] SubSectorDtoUpdate subSector){
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = await _service.Update(subSector);

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
        public async Task<IActionResult> DeleteSubSector(int id){
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