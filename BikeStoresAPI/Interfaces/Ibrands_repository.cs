using BikeStoresAPI.FilterData;
using BikeStoresAPI.Models;


namespace BikeStoresAPI.Interfaces
{
    public interface Ibrands_repository
    {
        public PagedList<brands> GetBrands(QueryStringParameters queryStringParameters);
        Task<brands> GetBrand(int brand_id);
        Task<brands> AddBrand(brands brands_);
        Task<brands> UpdateBrand(brands brands_);
        Task<brands> DeleteBrand(int brand_id);
        Task<IEnumerable<brands>> SearchBrands(string name);
    }
}
