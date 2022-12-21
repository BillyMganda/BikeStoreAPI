using BikeStoresAPI.FilterData;
using BikeStoresAPI.Interfaces;
using BikeStoresAPI.Models;
using BikeStoresAPI.Models.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;

namespace BikeStoresAPI.Interfaces_Implementation
{
    public class brands_repo_implementation : Ibrands_repository
    {
        private readonly BikeStoresDbContext _dbContext;
        public brands_repo_implementation(BikeStoresDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PagedList<brands> GetBrands(QueryStringParameters queryStringParameters)
        {
            return PagedList<brands>.ToPagedList(_dbContext.brands.OrderBy(on => on.brand_id), queryStringParameters.PageNumber, queryStringParameters.PageSize);
        }

        public async Task<brands> GetBrand(int bransid_)
        {
            var result = await _dbContext.brands.FirstOrDefaultAsync(b => b.brand_id == bransid_);
            return result;
        }

        public async Task<brands> AddBrand(brands brand)
        {
            var result = await _dbContext.brands.AddAsync(brand);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
         
        public async Task<brands> UpdateBrand(brands brand)
        {
            var result = await _dbContext.brands.FirstOrDefaultAsync(b => b.brand_id == brand.brand_id);
            if(result != null)
            {                               
                result.brand_name = brand.brand_name;

                await _dbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<brands> DeleteBrand(int brandid)
        {
            var result = await _dbContext.brands.FirstOrDefaultAsync(b => b.brand_id == brandid);
            if (result != null)
            {
                _dbContext.brands.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IEnumerable<brands>> SearchBrands(string name)
        {
            IQueryable<brands> query = _dbContext.brands;
            if(!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.brand_name.Contains(name));
            }
            return await query.ToListAsync();
        }
    }
}
