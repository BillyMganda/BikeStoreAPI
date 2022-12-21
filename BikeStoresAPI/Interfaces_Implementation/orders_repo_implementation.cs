using BikeStoresAPI.Interfaces;
using BikeStoresAPI.Models;
using BikeStoresAPI.Models.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace BikeStoresAPI.Interfaces_Implementation
{
    public class orders_repo_implementation : Iorders_repository
    {
        private readonly BikeStoresDbContext _context;
        public orders_repo_implementation(BikeStoresDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<orders>> GetOrders()
        {
            return await _context.orders.ToListAsync();
        }
        public async Task<orders> GetOrder(int order_id)
        {
            var result = await _context.orders.FirstOrDefaultAsync(x => x.order_id == order_id);
            return result;
        }
        public async Task<orders> AddOrder(orders orders)
        {
            var result = await _context.orders.AddAsync(orders);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<orders> UpdateOrder(orders orders)
        {
            var result = await _context.orders.FirstOrDefaultAsync(x => x.order_id == orders.order_id);
            if(result != null)
            {
                result.customer_id = orders.customer_id;
                result.order_status = orders.order_status;
                result.order_date = orders.order_date;
                result.required_date = orders.required_date;
                result.shipped_date = orders.shipped_date;
                result.store_id = orders.store_id;
                result.staff_id = orders.staff_id;

                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }
        public async Task<orders> DeleteOrder(int order_id)
        {
            var result = await _context.orders.FirstOrDefaultAsync(x => x.order_id == order_id);
            if(result != null)
            {
                _context.orders.Remove(result);
                await _context.SaveChangesAsync();
            }
            return result;
        }
    }
}
