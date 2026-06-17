using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockDemo.Api.Models;

namespace StockDemo.Api.Repositories
{
    public interface IStockRepository : IRepository<Stock>
    {
        Task<int> GetTotalStockAsync(int productId);
    }
}