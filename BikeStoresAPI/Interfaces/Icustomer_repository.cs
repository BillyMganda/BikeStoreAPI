using BikeStoresAPI.FilterData;
using BikeStoresAPI.Models;

namespace BikeStoresAPI.Interfaces
{
    public interface Icustomer_repository
    {
        public PagedList<customers> GetCustomers(QueryStringParameters queryStringParameters);
        Task<customers> GetCustomer(int custid);
        Task<customers> AddCustomer(customers cust);
        Task<customers> UpdateCustomer(customers cust);
        Task<customers> DeleteCustomer(int custid);
    }
}
