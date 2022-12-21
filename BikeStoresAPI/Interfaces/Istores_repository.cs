using BikeStoresAPI.Models;

namespace BikeStoresAPI.Interfaces
{
    public interface Istores_repository
    {
        Task<IEnumerable<stores>> GetStores();
        Task<stores> GetStore(int id);
        Task<stores> AddStore(stores stores_);
        Task<stores> UpdateStore(stores stores_);
        Task<stores> DeleteStore(int id);
    }
}
