using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wolfpack.DomainModel;
using Wolfpack.DomainServices;

namespace Wolfpack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PackController : ControllerBase
    {
        private readonly IPackRepository _packRepository;

        public PackController(IPackRepository packRepository)
        {
            _packRepository = packRepository;
        }

        // GET: api/pack
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Pack>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            var packs = _packRepository.GetAllPacks();
            if (!packs.Any())
                return NotFound();
            return Ok(packs);
        }

        // GET: api/pack/5
        [HttpGet("{id}", Name = "GetPack")]
        [ProducesResponseType(typeof(IEnumerable<Pack>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            try
            {
                var pack = _packRepository.GetPackById(id);
                return Ok(pack);
            }
            catch (KeyNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        // POST: api/pack
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] Pack pack)
        {
            try
            {
                using var scope = new TransactionScope();
                _packRepository.CreatePack(pack);
                scope.Complete();
                return NoContent();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // PUT: api/pack/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] Pack pack)
        {
            if (pack == null) return BadRequest("No pack was send");
            try
            {
                using var scope = new TransactionScope();
                _packRepository.UpdatePack(id, pack);
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

        // DELETE: api/pack/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            try
            {
                using var scope = new TransactionScope();
                _packRepository.DeletePack(id);
                scope.Complete();
                return new NoContentResult();
            }
            catch (KeyNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        // PUT: api/pack/5/add
        [HttpPut("{id}/add")]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult AddWolfToThePack(int id, [FromQuery] int wolfId)
        {
            try
            {
                using var scope = new TransactionScope();
                _packRepository.AddWolfToPack(id, wolfId);
                scope.Complete();
                return NoContent();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // PUT: api/pack/5/remove
        [HttpPut("{id}/remove")]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult RemoveWolfFromThePack(int id, [FromQuery] int wolfId)
        {
            try
            {
                using var scope = new TransactionScope();
                _packRepository.RemoveWolfFromPack(id, wolfId);
                scope.Complete();
                return NoContent();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
