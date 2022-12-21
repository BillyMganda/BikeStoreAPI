using BikeStoresAPI.Interfaces;
using BikeStoresAPI.Models;
using BikeStoresAPI.Models.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace BikeStoresAPI.Interfaces_Implementation
{
    public class categories_repo_implementation : Icategories_repository
    {
        private readonly BikeStoresDbContext _dbContext;
        public categories_repo_implementation(BikeStoresDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<categories>> GetCategories()
        {
            return await _dbContext.categories.ToListAsync();
        }
        public async Task<categories> GetCategory(int category_id)
        {
            var result = await _dbContext.categories.FirstOrDefaultAsync(b => b.category_id == category_id);
            return result;
        }
        public async Task<categories> AddCategory(categories category)
        {
            var result = await _dbContext.categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<categories> UpdateCategory(categories category)
        {
            var result = await _dbContext.categories.FirstOrDefaultAsync(b => b.category_id == category.category_id);
            if (result != null)
            {
                result.category_name = category.category_name;

                await _dbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
        public async Task<categories> DeleteCategory(int id)
        {
            var result = await _dbContext.categories.FirstOrDefaultAsync(b => b.category_id == id);
            if (result != null)
            {
                _dbContext.categories.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
            return result;
        }
    }
}
