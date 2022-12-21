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
    public class ordersController : ControllerBase
    {
        private readonly Iorders_repository _Repository;
        public ordersController(Iorders_repository repository)
        {
            _Repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            try
            {
                var result = await _Repository.GetOrders();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving records");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<orders>> GetOrder(int id)
        {
            try
            {
                var result = await _Repository.GetOrder(id);
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
        public async Task<ActionResult<orders>> PostOrder(orders order)
        {
            try
            {
                if (order == null)
                    return BadRequest();
                var created_order = await _Repository.AddOrder(order);
                return CreatedAtAction(nameof(GetOrder), new { id = created_order.order_id }, created_order);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<orders>> PutBrand(int id, orders order)
        {
            try
            {
                var order_to_update = await _Repository.GetOrder(id);
                if (order_to_update == null)
                    return NotFound();
                return await _Repository.UpdateOrder(order);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<orders>> DeleteOrder(int id)
        {
            try
            {
                var order_to_delete = await _Repository.GetOrder(id);
                if (order_to_delete == null)
                {
                    return NotFound();
                }
                var result = await _Repository.DeleteOrder(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting record");
            }
        }
    }
}
