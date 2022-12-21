using BikeStoresAPI.Models;

namespace BikeStoresAPI.Interfaces
{
    public interface Iuser_repository
    {
        Task<IEnumerable<users>> GetUsers();
        Task<users> GetUser(string email);
        Task<users> AddUser(users user);
        Task<users> UpdateUser(users user);
        Task<users> DeleteUser(int id);
        public void CreatePasswordHash(string password, out byte[] password_hash, out byte[] password_salt);
        public bool VerifyPasswordHash(string password, byte[] password_hash, byte[] password_salt);
        public string CreateToken(users user);
        public bool is_user_unique(string email);
    }
}
