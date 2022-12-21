using BikeStoresAPI.Interfaces;
using BikeStoresAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeStoresAPI.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    [Authorize]
    public class storesController : ControllerBase
    {
        private readonly Istores_repository _Repository;
        public storesController(Istores_repository repository)
        {
            _Repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetStores()
        {
            try
            {
                var result = await _Repository.GetStores();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving records");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<stores>> GetStore(int id)
        {
            try
            {
                var result = await _Repository.GetStore(id);
                if (result == null)
                    return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving record");
            }
        }

        [HttpPost]
        public async Task<ActionResult<stores>> PostStore(stores store)
        {
            try
            {
                if (store == null)
                    return BadRequest();
                var created_store = await _Repository.AddStore(store);
                return CreatedAtAction(nameof(GetStore), new { id = created_store.store_id }, created_store);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<stores>> PutStore(int id, stores store)
        {
            try
            {
                var store_to_update = await _Repository.GetStore(id);
                if (store_to_update == null)
                    return NotFound();
                return await _Repository.UpdateStore(store);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<stores>> DeleteStore(int id)
        {
            try
            {
                var store_to_delete = await _Repository.GetStore(id);
                if (store_to_delete == null)
                {
                    return NotFound();
                }
                var result = await _Repository.DeleteStore(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting record");
            }
        }
    }
}
