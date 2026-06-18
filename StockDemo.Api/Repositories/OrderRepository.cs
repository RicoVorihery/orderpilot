using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockDemo.Api.Data;
using StockDemo.Api.Models;

namespace StockDemo.Api.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(StockDemoDbContext context) : base(context)
        {
        }

        public new async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                        .Include(o => o.OrderLines)
                        .SingleOrDefaultAsync(o => o.Id == id);
        }
        public async Task<Order> CreateDraftOrderAsync(Order order)
        {
            order.Status = OrderStatus.Draft;
            order.CreatedAt = DateTime.UtcNow;
            order.UpdatedAt = DateTime.UtcNow;
            order.DateOrder = order.DateOrder.ToUniversalTime();

            order.OrderLines.ForEach(ol =>
            {
                ol.CreatedAt = DateTime.UtcNow;
                ol.UpdatedAt = DateTime.UtcNow;
            });

            return await AddAsync(order);
        }
    }
}