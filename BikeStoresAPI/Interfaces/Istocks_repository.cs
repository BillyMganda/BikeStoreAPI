using BikeStoresAPI.Models;

namespace BikeStoresAPI.Interfaces
{
    public interface Istocks_repository
    {
        Task<IEnumerable<stocks>> GetStocks();
        Task<stocks> GetStock(int Stocks_id);
        Task<stocks> AddStock(stocks Stocks);
        Task<stocks> UpdateStock(stocks Stocks);
        Task<stocks> DeleteStock(int Stocks_id);
    }
}
