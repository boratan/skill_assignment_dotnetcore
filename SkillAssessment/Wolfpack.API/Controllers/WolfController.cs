using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Wolfpack.DomainModel;
using Wolfpack.DomainServices;

namespace Wolfpack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class WolfController : ControllerBase
    {

        private readonly IWolfRepository _wolfRepository;

        public WolfController(IWolfRepository wolfRepository)
        {
            _wolfRepository = wolfRepository;
        }

        // GET: api/wolf
        [HttpGet]
        public IActionResult Get()
        {
            var wolfs = _wolfRepository.GetAllWolves();
            if (!wolfs.Any())
                return NotFound();
            return Ok(wolfs);
        }

        // GET: api/wolf/5
        [HttpGet("{id}", Name = "GetWolf")]
        public IActionResult Get(int id)
        {
            try
            {
                var wolf = _wolfRepository.GetWolfById(id);
                return Ok(wolf);
            }
            catch (KeyNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        // POST: api/wolf
        [HttpPost]
        public IActionResult Post([FromBody] Wolf wolf)
        {
            try
            {
                using var scope = new TransactionScope();
                var newWolf = _wolfRepository.AddWolf(wolf);
                scope.Complete();
                return CreatedAtAction(nameof(Get), 
                    new {id = newWolf.WolfId}, newWolf);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // PUT: api/wolf/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Wolf wolf)
        {
            if (wolf == null) return BadRequest("No wolf was send");
            try
            {
                using var scope = new TransactionScope();
                _wolfRepository.UpdateWolf(id, wolf);
                scope.Complete();
                return NoContent();
            }
            catch (KeyNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // DELETE: api/wolf/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                using var scope = new TransactionScope();
                _wolfRepository.DeleteWolf(id);
                scope.Complete();
                return new NoContentResult();
            }
            catch (KeyNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
