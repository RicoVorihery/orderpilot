using Microsoft.EntityFrameworkCore;
using StockDemo.Api.Data;
using StockDemo.Api.Models;

namespace StockDemo.Api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly StockDemoDbContext _dbContext;

        public CustomerRepository(StockDemoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            return await _dbContext.Customers
                        .AsNoTracking()
                        .SingleOrDefaultAsync(c => c.Email == email);
        }
    }
}