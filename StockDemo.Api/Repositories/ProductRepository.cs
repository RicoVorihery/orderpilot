using Microsoft.EntityFrameworkCore;
using StockDemo.Api.Data;
using StockDemo.Api.Models;

namespace StockDemo.Api.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(StockDemoDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(string? search)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return await _context.Products
                        .AsNoTracking()
                        .ToListAsync();
        }

        return await _context.Products
            .AsNoTracking()
            .Where(p => EF.Functions.ILike(p.Label, $"%{search}%"))
            .ToListAsync();
    }
}
