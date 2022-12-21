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
    public class stocksController : ControllerBase
    {
        private readonly Istocks_repository _Repository;
        public stocksController(Istocks_repository repository)
        {
            _Repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetStocks()
        {
            try
            {
                var result = await _Repository.GetStocks();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving records");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<stocks>> GetStock(int id)
        {
            try
            {
                var result = await _Repository.GetStock(id);
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
        public async Task<ActionResult<stocks>> PostStock(stocks stocks)
        {
            try
            {
                if (stocks == null)
                    return BadRequest();
                var created_stock = await _Repository.AddStock(stocks);
                return CreatedAtAction(nameof(GetStock), new { id = created_stock.store_id }, created_stock);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<stocks>> PutStock(int id, stocks stocks)
        {
            try
            {
                var stock_to_update = await _Repository.GetStock(id);
                if (stock_to_update == null)
                    return NotFound();
                return await _Repository.UpdateStock(stocks);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<stocks>> DeleteStock(int id)
        {
            try
            {
                var stock_to_delete = await _Repository.GetStock(id);
                if (stock_to_delete == null)
                {
                    return NotFound();
                }
                var result = await _Repository.DeleteStock(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting record");
            }
        }
    }
}
