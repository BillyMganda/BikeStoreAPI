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
    public class productsController : ControllerBase
    {
        private readonly Iproducts_repository _Repository;
        public productsController(Iproducts_repository repository)
        {
            _Repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            try
            {
                var result = await _Repository.GetProducts();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving records");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<products>> GetProduct(int id)
        {
            try
            {
                var result = await _Repository.GetProduct(id);
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
        public async Task<ActionResult<products>> PostProduct(products prod)
        {
            try
            {
                if (prod == null)
                    return BadRequest();
                var created_prod = await _Repository.AddProduct(prod);
                return CreatedAtAction(nameof(GetProduct), new { id = created_prod.product_id }, created_prod);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<products>> PutProduct(int id, products prod)
        {
            try
            {
                var prod_to_update = await _Repository.GetProduct(id);
                if (prod_to_update == null)
                    return NotFound();
                return await _Repository.UpdateProduct(prod);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<products>> DeleteProduct(int id)
        {
            try
            {
                var prod_to_delete = await _Repository.GetProduct(id);
                if (prod_to_delete == null)
                {
                    return NotFound();
                }
                var result = await _Repository.DeleteProduct(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting record");
            }
        }

        [HttpGet("complex")]
        public async Task<ActionResult<products_complex_dto>> getComplex()
        {            
            var result = await _Repository.getComplex();
            return Ok(result);
        }
    }
}
