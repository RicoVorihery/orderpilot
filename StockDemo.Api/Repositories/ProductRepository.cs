using Microsoft.EntityFrameworkCore;
using StockDemo.Api.Data;
using StockDemo.Api.Models;

namespace StockDemo.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly StockDemoDbContext _dbContext;

    public ProductRepository(StockDemoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _dbContext.Products.AsNoTracking().SingleOrDefaultAsync(p=>p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(string? search)
    {
        if(string.IsNullOrWhiteSpace(search))
        {
            return await _dbContext.Products
                        .AsNoTracking()
                        .ToListAsync();
        }
        
        return await _dbContext.Products
            .AsNoTracking()
            .Where(p => EF.Functions.ILike(p.Label, $"%{search}%"))
            .ToListAsync();
    }
}
