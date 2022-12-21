using BikeStoresAPI.Interfaces;
using BikeStoresAPI.Models;
using BikeStoresAPI.Models.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BikeStoresAPI.Interfaces_Implementation
{
    public class users_repo_implementation : Iuser_repository
    {
        private readonly BikeStoresDbContext _context;
        private readonly IConfiguration _configuration;
        public users_repo_implementation(BikeStoresDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IEnumerable<users>> GetUsers()
        {
            return await _context.users.ToListAsync();
        }
        public async Task<users> GetUser(string email)
        {
            var user = await _context.users.FirstOrDefaultAsync(b => b.email == email);
            return user;
        }
        public async Task<users> AddUser(users user)
        {
            var result = await _context.users.AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        public Task<users> UpdateUser(users user)
        {
            throw new NotImplementedException();
        }
        public Task<users> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public void CreatePasswordHash(string password, out byte[] password_hash, out byte[] password_salt)
        {
            using (var hmac = new HMACSHA512())
            {
                password_salt = hmac.Key;
                password_hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] password_hash, byte[] password_salt)
        {
            using (var hmac = new HMACSHA512(password_salt))
            {
                var compute_hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return compute_hash.SequenceEqual(password_hash);
            }
        }

        public string CreateToken(users user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.email)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSetting:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: creds
                );
            string jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt_token;
        }

        public  bool is_user_unique(string email)
        {
            var result =  _context.users.FirstOrDefaultAsync(b => b.email == email);
            if (result == null)
                return true;
            return false;
        }
    }
}
