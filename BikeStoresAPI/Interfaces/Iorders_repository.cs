using BikeStoresAPI.Models;
using System.Threading.Tasks;

namespace BikeStoresAPI.Interfaces
{
    public interface Iorders_repository
    {
        Task<IEnumerable<orders>> GetOrders();
        Task<orders> GetOrder(int order_id);
        Task<orders> AddOrder(orders orders);
        Task<orders> UpdateOrder(orders orders);
        Task<orders> DeleteOrder(int order_id);
    }
}
