using BikeStoresAPI.Interfaces;
using BikeStoresAPI.Models;
using BikeStoresAPI.Models.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace BikeStoresAPI.Interfaces_Implementation
{
    public class products_repo_implementation : Iproducts_repository
    {
        private readonly BikeStoresDbContext _context;
        public products_repo_implementation(BikeStoresDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<products>> GetProducts()
        {
            return await _context.products.ToListAsync();            
        }
        public async Task<products> GetProduct(int prod_id)
        {
            var result = await _context.products.FirstOrDefaultAsync(x => x.product_id == prod_id);
            return result;
        }
        public async Task<products> AddProduct(products prod)
        {
            var result = await _context.products.AddAsync(prod);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<products> UpdateProduct(products prod)
        {
            var result = await _context.products.FirstOrDefaultAsync(b => b.product_id == prod.product_id);
            if (result != null)
            {
                result.product_name = prod.product_name;
                result.brand_id = prod.brand_id;
                result.category_id = prod.category_id;
                result.model_year = prod.model_year;
                result.list_price = prod.list_price;

                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }
        public async Task<products> DeleteProduct(int prod_id)
        {
            var result = await _context.products.FirstOrDefaultAsync(b => b.product_id == prod_id);
            if (result != null)
            {
                _context.products.Remove(result);
                await _context.SaveChangesAsync();
            }
            return result;
        }

        //Loading Related Entities
        public async Task<IEnumerable<products_complex_dto>> getComplex()
        {
            List<products> products_data = await _context.products.ToListAsync();
            List<brands> brands_data = await _context.brands.ToListAsync();
            List<categories> category_data = await _context.categories.ToListAsync();
            List<products_complex_dto> complex_Dtos = new List<products_complex_dto>();
            var qs = (from product in products_data
                      join brand in brands_data on product.brand_id equals brand.brand_id
                      join category in category_data on product.category_id equals category.category_id
                      select new
                      {
                          product_id = product.product_id,
                          product_name = product.product_name,
                          model_year = product.model_year,
                          list_price = product.list_price,
                          brand_id = product.brand_id,
                          brand_name = brand.brand_name,
                          category_id = product.category_id,
                          category_name = category.category_name
                      }).ToList();
            foreach (var item in qs)
            {
             // complex_Dtos.Add(item);
            }
            return complex_Dtos;
        }
    }
}
