using StockDemo.Api.Models;

namespace StockDemo.Api.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> CreateDraftOrderAsync(Order order);

    }
}