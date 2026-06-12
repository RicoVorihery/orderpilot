using StockDemo.Api.Models;

namespace StockDemo.Api.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync(string? search);
    Task<Product?> GetProductByIdAsync(int id);
    
}
