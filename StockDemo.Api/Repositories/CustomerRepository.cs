using Microsoft.EntityFrameworkCore;
using StockDemo.Api.Data;
using StockDemo.Api.Models;

namespace StockDemo.Api.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(StockDemoDbContext context) : base(context)
        {
        }
        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            return await _context.Customers
                        .AsNoTracking()
                        .SingleOrDefaultAsync(c => c.Email == email);
        }
    }
}