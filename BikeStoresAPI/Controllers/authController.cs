using Azure.Core;
using BikeStoresAPI.Interfaces;
using BikeStoresAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BikeStoresAPI.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        public static users user = new users();
        private readonly Iuser_repository _Repository;
        public authController(Iuser_repository repository)
        {
            _Repository = repository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<users>> RegisterUser(users_dto request)
        {
            try
            {
                _Repository.CreatePasswordHash(request.password, out byte[] password_hash, out byte[] password_salt);

                user.email = request.email;
                user.password_hash =  Convert.ToBase64String(password_hash);
                user.password_salt = Convert.ToBase64String(password_salt);
                user.date_created = DateTime.UtcNow;

                await _Repository.AddUser(user);

                return Ok("user registration successful");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "internal server error");
            }
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginNew(users_dto dto)
        {
            try
            {                
                var user_ = await _Repository.GetUser(dto.email);   
                if(user_ == null)
                {
                    return NotFound("user not found");
                }
                if (!_Repository.VerifyPasswordHash(dto.password, Convert.FromBase64String(user_.password_hash), Convert.FromBase64String(user_.password_salt)))
                {
                    return BadRequest("invalid credentials");
                }
                string token = _Repository.CreateToken(user_);
                return Ok(token);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "internal server error");
            }
        }                
    }
}
