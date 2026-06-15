using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockDemo.Api.Data;
using StockDemo.Api.Models;

namespace StockDemo.Api.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly StockDemoDbContext  _dbContext;
        public StockRepository(StockDemoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetTotalStockAsync(int productId)
        {
            return await _dbContext.Stocks
                        .AsNoTracking()
                        .Where(s => s.ProductId == productId)
                        .SumAsync(s =>s.Quantity);
        }
    }
}