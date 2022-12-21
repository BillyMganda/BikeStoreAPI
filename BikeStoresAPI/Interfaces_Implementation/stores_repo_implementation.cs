using BikeStoresAPI.Interfaces;
using BikeStoresAPI.Models;
using BikeStoresAPI.Models.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace BikeStoresAPI.Interfaces_Implementation
{
    public class stores_repo_implementation : Istores_repository
    {
        private readonly BikeStoresDbContext _dbContext;
        public stores_repo_implementation(BikeStoresDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<stores>> GetStores()
        {
            return await _dbContext.stores.ToListAsync();
        }
        public async Task<stores> GetStore(int id)
        {
            var result = await _dbContext.stores.FirstOrDefaultAsync(b => b.store_id == id);
            return result;
        }
        public async Task<stores> AddStore(stores store)
        {
            var result = await _dbContext.stores.AddAsync(store);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<stores> UpdateStore(stores store)
        {
            var result = await _dbContext.stores.FirstOrDefaultAsync(b => b.store_id == store.store_id);
            if (result != null)
            {
                result.store_name = store.store_name;
                result.phone = store.phone;
                result.email = store.email;
                result.street = store.street;
                result.city = store.city;
                result.state = store.state;
                result.zip_code = store.zip_code;

                await _dbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<stores> DeleteStore(int id)
        {
            var result = await _dbContext.stores.FirstOrDefaultAsync(b => b.store_id == id);
            if (result != null)
            {
                _dbContext.stores.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
            return result;
        }
    }
}
