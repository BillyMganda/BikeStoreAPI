using BikeStoresAPI.Interfaces;
using BikeStoresAPI.Models;
using BikeStoresAPI.Models.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace BikeStoresAPI.Interfaces_Implementation
{
    public class staffs_repo_implementation : Istaffs_repository
    {
        private readonly BikeStoresDbContext _dbContext;
        public staffs_repo_implementation(BikeStoresDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<staffs>> GetStaffs()
        {
            return await _dbContext.staffs.ToListAsync();
        }

        public async Task<staffs> GetStaff(int id)
        {
            var result = await _dbContext.staffs.FirstOrDefaultAsync(b => b.staff_id == id);
            return result;
        }

        public async Task<staffs> AddStaff(staffs staffs)
        {
            var result = await _dbContext.staffs.AddAsync(staffs);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<staffs> UpdateStaff(staffs staffs)
        {
            var result = await _dbContext.staffs.FirstOrDefaultAsync(b => b.staff_id == staffs.staff_id);
            if (result != null)
            {
                result.first_name = staffs.first_name;
                result.last_name = staffs.last_name;
                result.email = staffs.email;
                result.phone = staffs.phone;
                result.active = staffs.active;
                result.store_id = staffs.store_id;
                result.manager_id = staffs.manager_id;

                await _dbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<staffs> DeleteStaff(int id)
        {
            var result = await _dbContext.staffs.FirstOrDefaultAsync(b => b.staff_id == id);
            if (result != null)
            {
                _dbContext.staffs.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
            return result;
        }
    }
}
