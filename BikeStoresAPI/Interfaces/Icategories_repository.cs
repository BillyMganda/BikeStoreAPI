using BikeStoresAPI.Models;

namespace BikeStoresAPI.Interfaces
{
    public interface Icategories_repository
    {
        Task<IEnumerable<categories>> GetCategories();
        Task<categories> GetCategory(int category_id);
        Task<categories> AddCategory(categories categories);
        Task<categories> UpdateCategory(categories categories);
        Task<categories> DeleteCategory(int category_id);
    }
}
