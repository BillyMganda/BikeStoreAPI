using BikeStoresAPI.FilterData;
using BikeStoresAPI.Interfaces;
using BikeStoresAPI.Models;
using BikeStoresAPI.Models.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace BikeStoresAPI.Interfaces_Implementation
{
    public class customer_repo_implementation : Icustomer_repository
    {
        private readonly BikeStoresDbContext _dbContext;
        public customer_repo_implementation(BikeStoresDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public PagedList<customers> GetCustomers(QueryStringParameters queryStringParameters)
        {
            return PagedList<customers>.ToPagedList(_dbContext.customers.OrderBy(x => x.customer_id), queryStringParameters.PageNumber, queryStringParameters.PageSize);
        }
        public async Task<customers> GetCustomer(int custid)
        {
            return await _dbContext.customers.FirstOrDefaultAsync(x => x.customer_id == custid);            
        }
        public async Task<customers> AddCustomer(customers cust)
        {
            var result = await _dbContext.customers.AddAsync(cust);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<customers> UpdateCustomer(customers cust)
        {
            var result = await _dbContext.customers.FirstOrDefaultAsync(x => x.customer_id == cust.customer_id);
            if(result != null)
            {
                result.first_name = cust.first_name;
                result.last_name = cust.last_name;
                result.phone = cust.phone;
                result.email = cust.email;
                result.street = cust.street;
                result.city = cust.city;
                result.state = cust.state;
                result.zip_code = cust.zip_code;

                await _dbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
        public async Task<customers> DeleteCustomer(int custid)
        {
            var result = await _dbContext.customers.FirstOrDefaultAsync(x => x.customer_id == custid);
            if(result != null )
            {
                _dbContext.customers.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
            return result; 
        }
    }
}
