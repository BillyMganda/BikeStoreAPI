using BikeStoresAPI.FilterData;
using BikeStoresAPI.Interfaces;
using BikeStoresAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BikeStoresAPI.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    [Authorize]
    public class brandsController : ControllerBase
    {
        private readonly Ibrands_repository _Repository;
        public brandsController(Ibrands_repository repository)
        {
            _Repository = repository;
        }

        [HttpGet]
        public IActionResult GetBrands([FromQuery] QueryStringParameters queryStringParameters)
        {
            try
            {
                var result = _Repository.GetBrands(queryStringParameters);
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
        public async Task<ActionResult<brands>> GetBrand(int id)
        {
            try
            {
                var result = await _Repository.GetBrand(id);
                if(result == null) 
                    return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving record");
            }
        }

        [HttpPost]
        public async Task<ActionResult<brands>> PostBrand(brands brands)
        {
            try
            {
                if (brands == null)
                    return BadRequest();
                var created_brand = await _Repository.AddBrand(brands);
                return CreatedAtAction(nameof(GetBrand), new { id = created_brand.brand_id }, created_brand);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating record");
            }
        }
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult<brands>> PutBrand(int id, brands brand)
        {
            try
            {                
                var brand_to_update = await _Repository.GetBrand(id);
                if(brand_to_update == null)
                    return NotFound();                
                return await _Repository.UpdateBrand(brand);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating record");
            }
        }
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<brands>> DeleteBrand(int id)
        {
            try
            {
                var brand_to_delete = await _Repository.GetBrand(id);
                if (brand_to_delete == null)
                {
                    return NotFound();
                }
                var result = await _Repository.DeleteBrand(id);                
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting record");
            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<brands>> SearchBrand(string name)
        {
            try
            {
                var result = await _Repository.SearchBrands(name);
                if(result.Any())
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error searching record");
            }
        }
    }
}
