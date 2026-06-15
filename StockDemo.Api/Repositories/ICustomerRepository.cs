using StockDemo.Api.Models;

namespace StockDemo.Api.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerByEmailAsync(string email);
    }
}