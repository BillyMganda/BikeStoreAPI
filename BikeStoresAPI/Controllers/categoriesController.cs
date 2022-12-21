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
    public class categoriesController : ControllerBase
    {
        private readonly Icategories_repository _Repository;
        public categoriesController(Icategories_repository repository)
        {
            _Repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            try
            {
                var result = await _Repository.GetCategories();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving records");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<categories>> GetCategory(int id)
        {
            try
            {
                var result = await _Repository.GetCategory(id);
                if (result == null || id < 0)
                    return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving record");
            }
        }

        [HttpPost]
        public async Task<ActionResult<categories>> PostCategory(categories cat)
        {
            try
            {
                if (cat == null)
                    return BadRequest();
                var created_category = await _Repository.AddCategory(cat);
                return CreatedAtAction(nameof(GetCategory), new { id = created_category.category_id }, created_category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<categories>> PutCategory(int id, categories cat)
        {
            try
            {
                var cat_to_update = await _Repository.GetCategory(id);
                if (cat_to_update == null)
                    return NotFound();
                return await _Repository.UpdateCategory(cat);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<categories>> DeleteCategory(int id)
        {
            try
            {
                var cat_to_delete = await _Repository.GetCategory(id);
                if (cat_to_delete == null)
                {
                    return NotFound();
                }
                var result = await _Repository.DeleteCategory(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting record");
            }
        }
    }
}
