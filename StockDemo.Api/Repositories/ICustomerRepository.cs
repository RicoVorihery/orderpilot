using StockDemo.Api.Models;

namespace StockDemo.Api.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer?> GetCustomerByEmailAsync(string email);
    }
}