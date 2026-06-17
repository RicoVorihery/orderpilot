using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockDemo.Api.Data;
using StockDemo.Api.Models;

namespace StockDemo.Api.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(StockDemoDbContext context) : base(context)
        {
        }
    }
}