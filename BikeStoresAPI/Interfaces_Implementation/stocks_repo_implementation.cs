using BikeStoresAPI.Interfaces;
using BikeStoresAPI.Models;
using BikeStoresAPI.Models.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace BikeStoresAPI.Interfaces_Implementation
{
    public class stocks_repo_implementation : Istocks_repository
    {
        private readonly BikeStoresDbContext _context;
        public stocks_repo_implementation(BikeStoresDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<stocks>> GetStocks()
        {
            return await _context.stocks.ToListAsync();
        }

        public async Task<stocks> GetStock(int id)
        {
            var result = await _context.stocks.FirstOrDefaultAsync(b => b.store_id == id);
            return result;
        }

        public async Task<stocks> AddStock(stocks stock)
        {
            var result = await _context.stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<stocks> UpdateStock(stocks stock)
        {
            var result = await _context.stocks.FirstOrDefaultAsync(b => b.store_id == stock.store_id);
            if (result != null)
            {
                result.product_id = stock.product_id;
                result.quantity = stock.quantity;

                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<stocks> DeleteStock(int id)
        {
            var result = await _context.stocks.FirstOrDefaultAsync(b => b.store_id == id);
            if (result != null)
            {
                _context.stocks.Remove(result);
                await _context.SaveChangesAsync();
            }
            return result;
        }
    }
}
