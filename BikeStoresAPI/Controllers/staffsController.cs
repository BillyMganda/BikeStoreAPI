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
    public class staffsController : ControllerBase
    {
        private readonly Istaffs_repository _Repository;
        public staffsController(Istaffs_repository repository)
        {
            _Repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetStaffs()
        {
            try
            {
                var result = await _Repository.GetStaffs();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving records");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<staffs>> GetStaff(int id)
        {
            try
            {
                var result = await _Repository.GetStaff(id);
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
        public async Task<ActionResult<staffs>> PostStaff(staffs staff)
        {
            try
            {
                if (staff == null)
                    return BadRequest();
                var created_staff = await _Repository.AddStaff(staff);
                return CreatedAtAction(nameof(GetStaff), new { id = created_staff.staff_id }, created_staff);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<staffs>> PutStaff(int id, staffs staff)
        {
            try
            {
                var staff_to_update = await _Repository.GetStaff(id);
                if (staff_to_update == null)
                    return NotFound();
                return await _Repository.UpdateStaff(staff);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<staffs>> DeleteStaff(int id)
        {
            try
            {
                var staff_to_delete = await _Repository.GetStaff(id);
                if (staff_to_delete == null)
                {
                    return NotFound();
                }
                var result = await _Repository.DeleteStaff(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting record");
            }
        }
    }
}
