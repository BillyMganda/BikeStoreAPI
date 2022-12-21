using BikeStoresAPI.Models;

namespace BikeStoresAPI.Interfaces
{
    public interface Iproducts_repository
    {
        Task<IEnumerable<products>> GetProducts();
        Task<products> GetProduct(int prod_id);
        Task<products> AddProduct(products prod);
        Task<products> UpdateProduct(products prod);
        Task<products> DeleteProduct(int prod_id);
        Task<IEnumerable<products_complex_dto>> getComplex();
    }
}
