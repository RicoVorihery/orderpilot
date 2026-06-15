using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockDemo.Api.Repositories
{
    public interface IStockRepository
    {
        Task<int> GetTotalStockAsync(int productId);
    }
}