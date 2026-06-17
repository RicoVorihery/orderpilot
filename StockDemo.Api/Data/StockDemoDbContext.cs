using Microsoft.EntityFrameworkCore;
using StockDemo.Api.Models;

namespace StockDemo.Api.Data;

public class StockDemoDbContext : DbContext
{
    public StockDemoDbContext(DbContextOptions<StockDemoDbContext> options)
    : base(options)
    {

    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Stock> Stocks => Set<Stock>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();
    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderLine> OrderLines => Set<OrderLine>();



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasPostgresEnum<OrderStatus>();
    }
}