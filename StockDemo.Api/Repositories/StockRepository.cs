using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockDemo.Api.Data;
using StockDemo.Api.Models;

namespace StockDemo.Api.Repositories
{
    public class StockRepository : BaseRepository<Stock>, IStockRepository
    {
        public StockRepository(StockDemoDbContext context) : base(context)
        {
        }

        public async Task<int> GetTotalStockAsync(int productId)
        {
            return await _context.Stocks
                        .AsNoTracking()
                        .Where(s => s.ProductId == productId)
                        .SumAsync(s => s.Quantity);
        }
    }
}