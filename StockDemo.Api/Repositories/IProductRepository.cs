using StockDemo.Api.Models;

namespace StockDemo.Api.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsAsync(string? search);

}
