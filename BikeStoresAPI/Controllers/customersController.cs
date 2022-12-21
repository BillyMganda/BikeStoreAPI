using BikeStoresAPI.FilterData;
using BikeStoresAPI.Interfaces;
using BikeStoresAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BikeStoresAPI.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    [Authorize]
    public class customersController : ControllerBase
    {
        private readonly Icustomer_repository _Repository;
        public customersController(Icustomer_repository repository)
        {
            _Repository = repository;
        }

        [HttpGet]
        public IActionResult GetCustomers([FromQuery] QueryStringParameters queryStringParameters)
        {
            try
            {
                var result = _Repository.GetCustomers(queryStringParameters);
                var metadata = new
                {
                    result.TotalCount,
                    result.PageSize,
                    result.CurrentPage,
                    result.TotalPages,
                    result.HasNext,
                    result.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving records");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<customers>> GetCustomer(int id)
        {
            try
            {
                var result = await _Repository.GetCustomer(id);
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
        public async Task<ActionResult<customers>> PostCustomer(customers cust)
        {
            try
            {
                if (cust == null)
                    return BadRequest();
                var created_customer = await _Repository.AddCustomer(cust);
                return CreatedAtAction(nameof(GetCustomer), new { id = created_customer.customer_id }, created_customer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<customers>> PutCustomer(int id, customers cust)
        {
            try
            {
                var cust_to_update = await _Repository.GetCustomer(id);
                if (cust_to_update == null)
                    return NotFound();
                return await _Repository.UpdateCustomer(cust);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<customers>> DeleteCustomer(int id)
        {
            try
            {
                var cust_to_delete = await _Repository.GetCustomer(id);
                if (cust_to_delete == null)
                {
                    return NotFound();
                }
                var result = await _Repository.DeleteCustomer(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting record");
            }
        }
    }
}
